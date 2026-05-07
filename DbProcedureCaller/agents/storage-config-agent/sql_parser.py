#!/usr/bin/env python3
# -*- coding: utf-8 -*-

import re
import json
import sys

def parse_sql_procedure(sql_text):
    """
    解析SQL存储过程定义，提取参数信息
    
    Args:
        sql_text (str): SQL存储过程定义文本
        
    Returns:
        dict: 包含过程名和参数列表的字典
    """
    result = {
        'procName': '',
        'parameters': []
    }
    
    # 提取存储过程名称
    proc_name_match = re.search(
        r'CREATE\s+PROC(?:EDURE)?\s+([a-zA-Z_][a-zA-Z0-9_]*)',
        sql_text,
        re.IGNORECASE
    )
    if proc_name_match:
        result['procName'] = proc_name_match.group(1)
    
    # 提取参数定义部分
    param_section_match = re.search(
        r'CREATE\s+PROC(?:EDURE)?\s+[a-zA-Z_][a-zA-Z0-9_]*\s*\(([\s\S]*?)\)',
        sql_text,
        re.IGNORECASE
    )
    
    if param_section_match:
        params_text = param_section_match.group(1)
        
        # 按逗号分割参数（考虑括号内的逗号）
        params = []
        depth = 0
        current_param = ''
        
        for char in params_text:
            if char == '(' or char == '[':
                depth += 1
                current_param += char
            elif char == ')' or char == ']':
                depth -= 1
                current_param += char
            elif char == ',' and depth == 0:
                params.append(current_param.strip())
                current_param = ''
            else:
                current_param += char
        
        if current_param.strip():
            params.append(current_param.strip())
        
        # 解析每个参数
        for param in params:
            param = param.strip()
            if not param:
                continue
            
            parsed_param = parse_single_param(param)
            if parsed_param:
                result['parameters'].append(parsed_param)
    
    return result

def parse_single_param(param_text):
    """
    解析单个参数定义
    
    Args:
        param_text (str): 参数定义文本
        
    Returns:
        dict: 参数信息
    """
    # 移除默认值部分进行分析
    default_match = re.search(r'=\s*([^\s,)]+)', param_text)
    default_value = default_match.group(1) if default_match else ''
    
    # 提取参数名和类型
    # 模式: @参数名 类型
    param_pattern = r'(@[a-zA-Z_][a-zA-Z0-9_]*)\s+([a-zA-Z]+(?:\([^)]*\))?)'
    match = re.search(param_pattern, param_text)
    
    if not match:
        return None
    
    param_name = match.group(1)
    param_type = match.group(2).upper()
    
    # 映射类型到前端控件类型
    frontend_type = map_sql_type_to_frontend(param_type)
    
    # 生成显示名称
    display_name = generate_display_name(param_name)
    
    return {
        'name': param_name,
        'displayName': display_name,
        'type': frontend_type,
        'defaultValue': default_value,
        'options': '',
        'isRequired': not bool(default_value),
        'isMultiple': False,
        'description': ''
    }

def map_sql_type_to_frontend(sql_type):
    """
    将SQL类型映射到前端控件类型
    
    Args:
        sql_type (str): SQL数据类型
        
    Returns:
        str: 前端控件类型
    """
    sql_type = sql_type.upper()
    
    if 'DATE' in sql_type or 'TIME' in sql_type:
        return 'datetime'
    elif 'INT' in sql_type or 'BIGINT' in sql_type or 'SMALLINT' in sql_type:
        return 'int'
    elif 'DECIMAL' in sql_type or 'NUMERIC' in sql_type or 'FLOAT' in sql_type or 'REAL' in sql_type:
        return 'int'
    else:
        return 'varchar'

def generate_display_name(param_name):
    """
    从参数名生成中文显示名称
    
    Args:
        param_name (str): 参数名（带@）
        
    Returns:
        str: 中文显示名称
    """
    # 移除@符号
    name = param_name.replace('@', '')
    
    # 常见参数名映射
    name_mappings = {
        'StartDate': '开始日期',
        'BeginDate': '开始日期',
        'EndDate': '结束日期',
        'FinishDate': '结束日期',
        'StatDate': '统计日期',
        'Date': '日期',
        'StartTime': '开始时间',
        'EndTime': '结束时间',
        'SystemType': '系统类型',
        'System': '系统',
        'Department': '科室',
        'Dept': '科室',
        'Doctor': '医生',
        'Reporter': '报告医生',
        'Reviewer': '审核医生',
        'Technician': '技师',
        'Category': '检查类别',
        'Type': '类型',
        'Status': '状态',
        'Keyword': '关键词',
        'SearchText': '搜索内容',
        'Limit': '数量限制',
        'PageSize': '每页数量',
        'PageIndex': '页码'
    }
    
    if name in name_mappings:
        return name_mappings[name]
    
    # 如果没有映射，尝试驼峰命名转中文
    return camel_case_to_chinese(name)

def camel_case_to_chinese(name):
    """
    将驼峰命名转换为中文描述
    
    Args:
        name (str): 驼峰命名字符串
        
    Returns:
        str: 中文描述
    """
    # 分割驼峰命名
    words = re.findall(r'[A-Z][a-z]*|[a-z]+|[0-9]+', name)
    
    # 单词映射
    word_mappings = {
        'Start': '开始',
        'Begin': '开始',
        'End': '结束',
        'Finish': '结束',
        'Date': '日期',
        'Time': '时间',
        'Stat': '统计',
        'System': '系统',
        'Type': '类型',
        'Department': '科室',
        'Dept': '科室',
        'Doctor': '医生',
        'Reporter': '报告',
        'Reviewer': '审核',
        'Technician': '技师',
        'Category': '类别',
        'Status': '状态',
        'Keyword': '关键词',
        'Search': '搜索',
        'Text': '内容',
        'Limit': '限制',
        'Page': '页',
        'Size': '数量',
        'Index': '码',
        'ID': '编号',
        'Id': '编号',
        'Name': '名称',
        'Code': '代码',
        'No': '编号',
        'Num': '数量'
    }
    
    result = ''
    for word in words:
        if word in word_mappings:
            result += word_mappings[word]
        else:
            result += word
    
    return result

def generate_config_id(proc_name):
    """
    从存储过程名生成配置ID
    
    Args:
        proc_name (str): 存储过程名称
        
    Returns:
        str: 配置ID（小写字母+下划线）
    """
    # 移除前缀
    name = proc_name.replace('proc_', '').replace('sp_', '').replace('usp_', '')
    
    # 驼峰转下划线
    name = re.sub(r'([a-z0-9])([A-Z])', r'\1_\2', name)
    
    # 转小写
    return name.lower()

def main():
    """
    命令行入口函数
    """
    if len(sys.argv) < 2:
        print(json.dumps({
            'success': False,
            'error': '请提供SQL文件路径或SQL文本'
        }, ensure_ascii=False))
        return
    
    input_arg = sys.argv[1]
    
    # 判断是文件路径还是直接SQL文本
    if input_arg.endswith('.sql'):
        try:
            with open(input_arg, 'r', encoding='utf-8') as f:
                sql_text = f.read()
        except Exception as e:
            print(json.dumps({
                'success': False,
                'error': f'读取文件失败: {str(e)}'
            }, ensure_ascii=False))
            return
    else:
        # 尝试从标准输入读取
        if input_arg == '-':
            sql_text = sys.stdin.read()
        else:
            sql_text = input_arg
    
    # 解析SQL
    result = parse_sql_procedure(sql_text)
    
    # 生成配置ID
    if result['procName']:
        result['configId'] = generate_config_id(result['procName'])
        result['configName'] = generate_display_name(result['procName'])
    
    print(json.dumps(result, ensure_ascii=False, indent=2))

if __name__ == '__main__':
    main()