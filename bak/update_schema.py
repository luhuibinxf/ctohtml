#!/usr/bin/env python
# -*- coding: utf-8 -*-
"""
将Word文档中的病人类型映射信息整理优化到WiNEX_PACS_Schema_Full.md中
"""

import re

def read_file(file_path):
    """读取文件内容"""
    with open(file_path, 'r', encoding='utf-8', errors='ignore') as f:
        return f.read()

def write_file(file_path, content):
    """写入文件内容"""
    with open(file_path, 'w', encoding='utf-8') as f:
        f.write(content)

def find_pacs_sysdict_section(content):
    """找到Pacs_SysDict表的位置"""
    pattern = r'(## Pacs_SysDict\n\n\| 字段名 \| 类型 \| 长度 \| 可空 \| 主键 \|\n\|--------\|------\|------\|------\|------\|.*?)\n\n---\n\n## '
    match = re.search(pattern, content, re.DOTALL)
    if match:
        return match.start(), match.end()
    return None, None

def find_exam_task_section(content):
    """找到EXAM_TASK表的位置"""
    pattern = r'(## EXAM_TASK\n\n\| 字段名 \| 类型 \| 长度 \| 可空 \| 主键 \|\n\|--------\|------\|------\|------\|------\|.*?)\n\n---\n\n## '
    match = re.search(pattern, content, re.DOTALL)
    if match:
        return match.start(), match.end()
    return None, None

def generate_patient_type_mapping_section():
    """生成病人类型映射说明部分"""
    mapping_content = """
## ENCOUNTER_TYPE_NO 病人类型编码映射

### 官方映射关系（来自WiNEX-WiNEX影像系统-实体清单）

| 编码 | 中文名称 | 说明 |
|------|----------|------|
| 1 | 门诊 | 门诊患者 |
| 2 | 住院 | 住院患者 |
| 3 | 急诊 | 急诊患者 |
| 4 | 体检 | 体检患者 |
| 138138 | 门诊 | HIS标准编码 |
| 138139 | 急诊 | HIS标准编码 |
| 138140 | 体检 | HIS标准编码 |
| 145235 | 住院 | HIS标准编码 |

### NEG_POS_CODE 阴阳性编码映射

| 编码 | 中文名称 | 说明 |
|------|----------|------|
| 383927 | 阳性 | 检查结果阳性 |
| 383926 | 阴性 | 检查结果阴性 |

### 字典表查询方式

```sql
-- 从Pacs_SysDict查询病人类型映射
SELECT 
    d.nValue AS code, 
    d.cValue AS name
FROM Pacs_SysDict d
WHERE d.TableName = 'EXAM_TASK' 
  AND d.FieldName = 'ENCOUNTER_TYPE_NO'
ORDER BY d.nValue;
```

### EXAM_TASK表中ENCOUNTER_TYPE_NO字段说明

**字段名**: ENCOUNTER_TYPE_NO  
**类型**: VARCHAR(32)  
**说明**: 就诊类型编码  
**映射表**: Pacs_SysDict  
**默认值**: 无  

### EXAM_REPORT表中NEG_POS_CODE字段说明

**字段名**: NEG_POS_CODE  
**类型**: VARCHAR(32)  
**说明**: 阴阳性标记  
**映射表**: Pacs_SysDict  
**默认值**: 无  

### 常用查询示例

```sql
-- 按就诊类型统计
SELECT 
    CASE ENCOUNTER_TYPE_NO 
        WHEN '1' THEN '门诊'
        WHEN '2' THEN '住院'
        WHEN '3' THEN '急诊'
        WHEN '4' THEN '体检'
        WHEN '138138' THEN '门诊'
        WHEN '138139' THEN '急诊'
        WHEN '138140' THEN '体检'
        WHEN '145235' THEN '住院'
        ELSE ENCOUNTER_TYPE_NO 
    END AS 就诊类型,
    COUNT(*) AS 数量
FROM EXAM_TASK
WHERE IS_DEL = 0 
  AND EXAM_TASK_STATUS >= 50
GROUP BY ENCOUNTER_TYPE_NO;
```

---

"""
    return mapping_content

def update_schema_file(input_file, output_file):
    """更新Schema文件"""
    content = read_file(input_file)
    
    # 在Pacs_SysDict表之后插入病人类型映射说明
    insert_pos = content.find("## Pacs_SysDict")
    if insert_pos != -1:
        # 找到Pacs_SysDict部分的结束位置（下一个---）
        end_pos = content.find("\n\n---\n\n## ", insert_pos)
        if end_pos != -1:
            # 在---之后插入
            insert_pos = end_pos + len("\n\n---\n\n")
            new_content = content[:insert_pos] + generate_patient_type_mapping_section() + content[insert_pos:]
            write_file(output_file, new_content)
            print(f"✅ 成功更新Schema文件: {output_file}")
            return True
    
    print("❌ 未找到Pacs_SysDict部分")
    return False

def main():
    input_file = r"d:\AI\tran\.trae\agents\winex-stat-validator\docs\WiNEX_PACS_Schema_Full.md"
    output_file = r"d:\AI\tran\.trae\agents\winex-stat-validator\docs\WiNEX_PACS_Schema_Full.md"
    
    print(f"正在读取Schema文件: {input_file}")
    
    if update_schema_file(input_file, output_file):
        print("\n📋 更新内容摘要:")
        print("1. 添加了ENCOUNTER_TYPE_NO病人类型编码映射表")
        print("2. 添加了官方映射关系（1=门诊, 2=住院等）")
        print("3. 添加了HIS标准编码映射（138138=门诊等）")
        print("4. 添加了NEG_POS_CODE阴阳性编码映射（383927=阳性, 383926=阴性）")
        print("5. 添加了字典表查询方式")
        print("6. 添加了常用查询示例")
    else:
        print("更新失败")

if __name__ == "__main__":
    main()
