#!/usr/bin/env python
# -*- coding: utf-8 -*-
"""
从.doc格式Word文档中提取病人类型映射数据
使用 pywin32 调用 Microsoft Word
"""

import os
import sys
import re

def extract_text_from_doc(doc_path):
    """使用 pywin32 从.doc文件提取文本"""
    try:
        import win32com.client
        import pythoncom
        
        pythoncom.CoInitialize()
        word = win32com.client.Dispatch("Word.Application")
        word.Visible = False
        
        doc = word.Documents.Open(doc_path)
        text = doc.Content.Text
        doc.Close()
        word.Quit()
        
        return text
    except ImportError:
        print("错误: 请先安装 pywin32 库")
        print("安装命令: pip install pywin32")
        sys.exit(1)
    except Exception as e:
        print(f"读取文档失败: {str(e)}")
        return ""

def parse_patient_types(text):
    """从文本中解析病人类型映射"""
    mappings = {}
    
    # 查找可能的病人类型表格模式
    patterns = [
        # 模式1: 编码 空格/制表符 名称
        re.compile(r'(\d{4,6})\s+([\u4e00-\u9fa5]{2,4})'),
        # 模式2: 编码->名称
        re.compile(r'(\d{4,6})\s*[-→>]\s*([\u4e00-\u9fa5]{2,4})'),
        # 模式3: ENCOUNTER_TYPE_NO 相关
        re.compile(r'ENCOUNTER_TYPE_NO\s*[:：]\s*(\d+)\s*[:：]\s*([\u4e00-\u9fa5]{2,4})'),
        # 模式4: 门诊/住院等关键词附近的编码
        re.compile(r'(\d{4,6})\s*(门诊|住院|急诊|体检|住院部|复诊|初诊)'),
    ]
    
    for pattern in patterns:
        matches = pattern.findall(text)
        for match in matches:
            code = match[0]
            name = match[1]
            if code and name:
                mappings[code] = name
    
    return mappings

def generate_csharp_code(mappings):
    """生成C#映射代码"""
    if not mappings:
        return ""
    
    code = "var mappings = new Dictionary<string, string>\n{\n"
    for code_val, name in sorted(mappings.items()):
        code += f'    {{ "{code_val}", "{name}" }},\n'
    code += "};"
    
    return code

def main():
    doc_path = r"d:\AI\tran\.trae\agents\winex-stat-validator\docs\WiNEX-WiNEX影像系统-实体清单.doc"
    
    if not os.path.exists(doc_path):
        print(f"文档不存在: {doc_path}")
        sys.exit(1)
    
    print(f"正在读取文档: {doc_path}")
    print("=" * 50)
    
    # 提取文本
    text = extract_text_from_doc(doc_path)
    
    if not text:
        print("无法读取文档内容")
        sys.exit(1)
    
    print(f"文档字符数: {len(text)}")
    
    # 解析病人类型映射
    mappings = parse_patient_types(text)
    
    if not mappings:
        print("\n未找到任何病人类型映射")
        print("尝试搜索文档中的关键字...")
        
        # 搜索关键词
        keywords = ['病人类型', '就诊类型', 'ENCOUNTER', 'TYPE_NO', '门诊', '住院', '急诊', '体检']
        print("\n文档中包含以下关键词:")
        for keyword in keywords:
            if keyword in text:
                print(f"  ✓ {keyword}")
        
        sys.exit(0)
    
    # 输出结果
    print("\n" + "=" * 50)
    print("提取到的病人类型映射:")
    print("-" * 50)
    for code, name in sorted(mappings.items()):
        print(f"{code} → {name}")
    
    print("\n" + "=" * 50)
    print("生成的C#代码:")
    print("-" * 50)
    print(generate_csharp_code(mappings))
    
    # 保存到文件
    output_file = "patient_type_mappings.txt"
    with open(output_file, 'w', encoding='utf-8') as f:
        f.write("# 病人类型映射\n")
        f.write("# 生成自: " + doc_path + "\n")
        f.write("# 生成时间: " + __import__('datetime').datetime.now().strftime('%Y-%m-%d %H:%M:%S') + "\n")
        f.write("\n## 映射关系\n")
        for code, name in sorted(mappings.items()):
            f.write(f"{code} → {name}\n")
        f.write("\n## C# 代码\n")
        f.write(generate_csharp_code(mappings))
    
    print(f"\n结果已保存到: {output_file}")

if __name__ == "__main__":
    main()