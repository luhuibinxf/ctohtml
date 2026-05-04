#!/usr/bin/env python
# -*- coding: utf-8 -*-
"""
从Word文档中提取病人类型映射数据
支持 .docx 格式文档
"""

import os
import sys

try:
    from docx import Document
except ImportError:
    print("错误: 请先安装 python-docx 库")
    print("安装命令: pip install python-docx")
    sys.exit(1)

def extract_tables_from_doc(doc_path):
    """从Word文档中提取所有表格"""
    try:
        doc = Document(doc_path)
        tables_data = []
        
        for table_idx, table in enumerate(doc.tables):
            table_data = []
            for row in table.rows:
                cells = [cell.text.strip() for cell in row.cells]
                table_data.append(cells)
            if table_data:
                tables_data.append({
                    'index': table_idx + 1,
                    'rows': table_data
                })
        
        return tables_data
    except Exception as e:
        print(f"读取文档失败: {str(e)}")
        return []

def find_patient_type_tables(tables):
    """查找可能包含病人类型映射的表格"""
    patient_tables = []
    keywords = ['病人类型', '就诊类型', 'ENCOUNTER_TYPE', 'TYPE_NO', '编码', '类型代码']
    
    for table in tables:
        first_row = table['rows'][0] if table['rows'] else []
        row_text = ' '.join([str(cell) for cell in first_row]).upper()
        
        for keyword in keywords:
            if keyword.upper() in row_text:
                patient_tables.append(table)
                break
    
    return patient_tables

def extract_mappings_from_table(table):
    """从表格中提取编码-名称映射"""
    mappings = {}
    
    # 跳过表头，从第二行开始
    for row in table['rows'][1:]:
        if len(row) >= 2:
            code = row[0].strip()
            name = row[1].strip()
            
            # 检查是否是有效的编码（数字或字母数字组合）
            if code and (code.isdigit() or code.replace('-', '').isalnum()):
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
    # 默认文档路径
    default_paths = [
        "WiNEX-WiNEX影像系统-实体清单.docx",
        "WiNEX-WiNEX影像系统-实体清单.doc",
        "../WiNEX-WiNEX影像系统-实体清单.docx",
        "../WiNEX-WiNEX影像系统-实体清单.doc"
    ]
    
    doc_path = None
    for path in default_paths:
        if os.path.exists(path):
            doc_path = path
            break
    
    if not doc_path:
        print("请提供Word文档路径作为参数")
        print("使用方法:")
        print("  python extract_patient_types.py <文档路径>")
        print(f"\n在当前目录及上级目录未找到以下文件:")
        for path in default_paths:
            print(f"  - {path}")
        sys.exit(1)
    
    print(f"正在读取文档: {doc_path}")
    print("=" * 50)
    
    # 提取表格
    tables = extract_tables_from_doc(doc_path)
    print(f"共找到 {len(tables)} 个表格")
    
    # 查找病人类型相关表格
    patient_tables = find_patient_type_tables(tables)
    print(f"找到 {len(patient_tables)} 个可能包含病人类型映射的表格")
    
    if not patient_tables:
        print("\n未找到病人类型映射表格，将显示所有表格供您选择:")
        patient_tables = tables
    
    # 提取映射
    all_mappings = {}
    for table in patient_tables:
        mappings = extract_mappings_from_table(table)
        all_mappings.update(mappings)
    
    if not all_mappings:
        print("\n未找到任何编码-名称映射")
        print("请检查文档内容或手动提供映射关系")
        sys.exit(0)
    
    # 输出结果
    print("\n" + "=" * 50)
    print("提取到的病人类型映射:")
    print("-" * 50)
    for code, name in sorted(all_mappings.items()):
        print(f"{code} → {name}")
    
    print("\n" + "=" * 50)
    print("生成的C#代码:")
    print("-" * 50)
    print(generate_csharp_code(all_mappings))
    
    # 保存到文件
    output_file = "patient_type_mappings.txt"
    with open(output_file, 'w', encoding='utf-8') as f:
        f.write("# 病人类型映射\n")
        f.write("# 生成自: " + doc_path + "\n")
        f.write("# 生成时间: " + __import__('datetime').datetime.now().strftime('%Y-%m-%d %H:%M:%S') + "\n")
        f.write("\n## 映射关系\n")
        for code, name in sorted(all_mappings.items()):
            f.write(f"{code} → {name}\n")
        f.write("\n## C# 代码\n")
        f.write(generate_csharp_code(all_mappings))
    
    print(f"\n结果已保存到: {output_file}")

if __name__ == "__main__":
    main()