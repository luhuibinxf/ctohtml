# WiNEX_PACS 数据库Schema（接口开发专用）

**数据库：** WiNEX_PACS
**用途：** 影像检查数据（放射/超声/内镜/病理/核医学）
**更新日期：** 2026-05-01

---

## 📋 核心表（接口开发必用）

### 1. EXAM_TASK（检查任务主表）

**用途：** 存储所有检查任务记录

| 字段 | 类型 | 说明 | 备注 |
|------|------|------|------|
| EXAM_TASK_ID | VARCHAR(64) | 主键 | |
| SYSTEM_SOURCE_NO | VARCHAR(32) | 检查系统分类 | RIS/UES/EIS/PIS/NMS |
| ENCOUNTER_TYPE_NO | VARCHAR(32) | 就诊类型 | **见下方映射表** |
| FULL_NAME | VARCHAR(64) | 患者姓名 | |
| PATIENT_NO | VARCHAR(64) | 患者ID | |
| REGISTER_AT | DATETIME | 登记时间 | **按此分组统计** |
| EXAM_COMPLETION_AT | DATETIME | 检查完成时间 | |
| EXAM_CATEGORY_NO | VARCHAR(64) | 检查项目编码 | |
| EXAM_CATEGORY_NAME | VARCHAR(128) | 检查项目名称 | 注意：超声类叫"彩超" |
| LABEL_NO | VARCHAR(64) | 检查号/标签号 | **非空过滤** |
| EXAM_TASK_STATUS | INT | 任务状态 | **>=50表示已完成/已审核** |
| IS_DEL | INT | 删除标记 | 0=未删除 |

**常用查询条件：**
```sql
WHERE IS_DEL = 0
  AND LABEL_NO <> ''
  AND REGISTER_AT IS NOT NULL
  AND EXAM_TASK_STATUS >= 50
```

---

### 2. EXAM_CATEGORY（检查项目分类表）

**用途：** 检查项目分类定义

| 字段 | 类型 | 说明 |
|------|------|------|
| EXAM_CATEGORY_NO | VARCHAR(64) | 分类编码 |
| EXAM_CATEGORY_NAME | VARCHAR(128) | 分类名称 |
| SYSTEM_SOURCE_NO | VARCHAR(32) | 系统分类 |

**注意：** ⚠️ 此表存在重复记录，JOIN时需注意！

**重复记录示例：**
| EXAM_CATEGORY_NO | SYSTEM_SOURCE_NO | 重复数 |
|------------------|------------------|--------|
| CJ | EIS | 5条 |
| WJ | EIS | 7条 |
| ZQGJ | EIS | 2条 |

**建议：** 接口开发时直接使用 `EXAM_TASK.SYSTEM_SOURCE_NO`，避免JOIN此表

---

### 3. EXAM_REPORT（检查报告表）

**用途：** 存储检查报告信息

| 字段 | 类型 | 说明 |
|------|------|------|
| EXAM_REPORT_ID | VARCHAR(64) | 报告ID |
| EXAM_TASK_ID | VARCHAR(64) | 关联任务ID |
| WRITER_AT | DATETIME | 书写时间 |
| REPORT_AT | DATETIME | 报告时间 |
| REVIEWER_AT | DATETIME | 审核时间 |
| NEG_POS_CODE | VARCHAR(32) | 阴阳性标记 | **见下方映射表** |
| IS_DEL | INT | 删除标记 |

---

## 🔑 ENCOUNTER_TYPE_NO 就诊类型映射

| 编码 | 就诊类型 | 说明 |
|------|---------|------|
| 138138 | 门诊 | |
| 138139 | 急诊 | |
| 138140 | 体检 | |
| 145235 | 住院 | |
| 1 | 门诊 | 老系统编码 |
| 2 | 住院 | 老系统编码 |
| 3 | 急诊 | 老系统编码 |
| 4 | 体检 | 老系统编码 |
| OPD | 门诊 | 国际标准 |
| IPD | 住院 | 国际标准 |
| EMER | 急诊 | 国际标准 |
| CHECKUP | 体检 | 国际标准 |

**SQL转换示例：**
```sql
CASE ENCOUNTER_TYPE_NO
    WHEN '138138' THEN '门诊'
    WHEN '138139' THEN '急诊'
    WHEN '138140' THEN '体检'
    WHEN '145235' THEN '住院'
    ELSE ENCOUNTER_TYPE_NO
END AS 就诊类型
```

---

## 🔑 NEG_POS_CODE 阴阳性映射

| 编码 | 结果状态 | 说明 |
|------|---------|------|
| 383927 | 阳性 | |
| 383926 | 阴性 | |

**SQL转换示例：**
```sql
CASE NEG_POS_CODE
    WHEN '383927' THEN '阳性'
    WHEN '383926' THEN '阴性'
    ELSE '未知'
END AS 阴阳性
```

---

## 🔑 SYSTEM_SOURCE_NO 分类

| SYSTEM_SOURCE_NO | 检查类别 | 接口JCLBDM |
|------------------|----------|------------|
| RIS | 放射类（普放X, CT, MRI, DSA, DR等） | 1 |
| UIS | 超声类（心脏/血管/妇产/腹部超声等） | 2 |
| EIS | 内镜类（胃镜, 肠镜, 支气管镜等） | 4 |
| PIS | 病理类 | 5 |
| NMS | 核医学类 | 3 |

---

## 📊 接口开发常用SQL模板

### 1. 按系统分类统计（按登记时间）
```sql
SELECT
    CONVERT(DATE, REGISTER_AT) AS 日期,
    COUNT(*) AS 总人次,
    SUM(CASE WHEN SYSTEM_SOURCE_NO='RIS' THEN 1 ELSE 0 END) AS 放射,
    SUM(CASE WHEN SYSTEM_SOURCE_NO='UIS' THEN 1 ELSE 0 END) AS 超声,
    SUM(CASE WHEN SYSTEM_SOURCE_NO='EIS' THEN 1 ELSE 0 END) AS 内镜
FROM EXAM_TASK
WHERE IS_DEL = 0
    AND LABEL_NO <> ''
    AND REGISTER_AT >= '20260413'
    AND REGISTER_AT < '20260414'
    AND EXAM_TASK_STATUS >= 50
GROUP BY CONVERT(DATE, REGISTER_AT)
```

### 2. 按就诊类型统计
```sql
SELECT
    CASE ENCOUNTER_TYPE_NO
        WHEN '138138' THEN '门诊'
        WHEN '138139' THEN '急诊'
        WHEN '138140' THEN '体检'
        WHEN '145235' THEN '住院'
        ELSE ENCOUNTER_TYPE_NO
    END AS 就诊类型,
    COUNT(*) AS 数量
FROM EXAM_TASK
WHERE REGISTER_AT >= '20260413' AND REGISTER_AT < '20260414'
    AND EXAM_TASK_STATUS >= 50
GROUP BY ENCOUNTER_TYPE_NO
```

### 3. 验证字段数量
```sql
SELECT COUNT(*) FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = '视图名'
```

---

## ⚠️ 开发注意事项

1. **必须加过滤条件**：`EXAM_TASK_STATUS >= 50`
2. **避免JOIN EXAM_CATEGORY**：此表有重复记录，会导致数据翻倍
3. **按登记时间统计**：接口用 `REGISTER_AT`，不是 `EXAM_COMPLETION_AT`
4. **超声类名称**：数据库是"彩超"，不是"超声"
5. **就诊类型转换**：使用 ENCOUNTER_TYPE_NO 的 CASE 映射
6. **阴阳性转换**：使用 NEG_POS_CODE 的 CASE 映射（383927=阳性，383926=阴性）
