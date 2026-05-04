---
name: "xray-workload-stat"
description: "放射科（RIS）书写工作量统计 - 自动连接数据库执行SQL查询，生成统计数据表格，验证数据准确性。Invoke when user asks for X-ray workload statistics, doctor work stats, or needs to generate statistical reports."
---

# 放射科书写工作量统计Skill

## 功能概述

本Skill用于放射科（RIS）书写工作量的自动统计，支持：
1. 连接WiNEX_PACS数据库执行SQL查询
2. 按日期、医生、检查类型分组统计
3. 验证数据准确性（普放组+CT组+MRI组=总数据）
4. 生成格式化的统计报告

## 数据库信息

- **数据库**：WiNEX_PACS
- **检查系统分类**：RIS（放射类）
- **核心表**：EXAM_TASK, EXAM_REPORT
- **时间字段**：REGISTER_AT（登记时间）
- **状态过滤**：EXAM_TASK_STATUS >= 50

## 检查类型分类

### 普放组
| EXAM_CATEGORY_NAME | 分类 |
|-------------------|------|
| 普放 | DR |
| 普放(新) | DR |
| 钼靶 | 钼靶 |
| 消化道造影 | 造影 |
| 消化道造影(新) | 造影 |

### CT组
| EXAM_CATEGORY_NAME | 分类 |
|-------------------|------|
| CT | CT平扫 |
| CT(新) | CT增强 |
| CT增强 | CT增强 |
| CTA | CT增强 |
| CTV | CT增强 |
| CTU | CT增强 |
| CTP | CT增强 |
| CCTA | CT增强 |

### MRI组
| EXAM_CATEGORY_NAME | 分类 |
|-------------------|------|
| 核磁共振 | MRI平扫 |
| 核磁共振(新) | MRI增强 |
| MRI | MRI平扫 |
| MRI(新) | MRI增强 |
| MRA | MRA |
| MRV | MRV |
| MRU | MRU |
| MRCP | MRCP |
| MRS | MRS |

## SQL执行命令

使用SQLCMD.EXE连接数据库：

```powershell
# 连接数据库
SQLCMD.EXE -S localhost -d WiNEX_PACS -Q "USE WiNEX_PACS; SELECT ..." -W

# 带参数的日期查询
DECLARE @TargetDate DATETIME = '2026-04-01';
```

## 验证流程

### 1. 验证总数据量
```sql
SELECT COUNT(*) FROM EXAM_TASK t
JOIN EXAM_REPORT r ON t.EXAM_TASK_ID = r.EXAM_TASK_ID
WHERE t.IS_DEL=0 AND r.IS_DEL=0
  AND t.SYSTEM_SOURCE_NO='RIS'
  AND t.EXAM_TASK_STATUS>=50
  AND CAST(t.REGISTER_AT AS DATE)='2026-04-01'
  AND r.REPORTER_NAME IS NOT NULL;
```

### 2. 验证各组分类
- 普放组 = DR + 钼靶 + 造影
- CT组 = CT平扫 + CT增强
- MRI组 = MRI平扫 + MRA + MRV + MRU + MRCP + MRS

### 3. 验证公式
确保：普放组 + CT组 + MRI组 = 总数据量

## 使用示例

### 用户请求示例
- "帮我统计4月1日的放射科工作量"
- "生成放射科工作量报告"
- "验证4月2日的医生工作量数据"

### 执行步骤
1. 连接WiNEX_PACS数据库
2. 执行总数据量查询
3. 执行各检查类别查询
4. 执行按医生分组统计
5. 验证数据准确性
6. 生成格式化报告

## 注意事项

1. **避免使用LIKE**：尽量使用精确匹配(IN)提高性能
2. **时间字段**：使用REGISTER_AT，不是EXAM_COMPLETION_AT
3. **状态过滤**：必须添加EXAM_TASK_STATUS >= 50
4. **非空过滤**：确保r.REPORTER_NAME IS NOT NULL

## 输出格式

验证结果应包含：
- 总数据量
- 各检查类别分布
- 各组统计数据
- 验证结论（通过/失败）
- 数据差异说明（如有）