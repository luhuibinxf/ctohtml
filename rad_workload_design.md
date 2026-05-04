
# 放射科工作量统计系统 - 存储设计（项目规范版）

## 一、数据库表设计

### 1.1 核心数据表

#### 表名: `EXAM_WORKLOAD_STAT` (工作量统计表)

| 字段名 | 类型 | 长度 | 约束 | 说明 | 对照源表字段 |
|-------|------|------|------|------|-------------|
| ID | INT | - | PRIMARY KEY, IDENTITY(1,1) | 主键自增 | - |
| STAT_DATE | DATE | - | NOT NULL | 统计日期 | - |
| SYSTEM_SOURCE_NO | VARCHAR | 20 | NOT NULL | 系统类型编码 | EXAM_TASK.SYSTEM_SOURCE_NO |
| SYSTEM_NAME | VARCHAR | 50 | NULL | 系统名称 | 映射值(UIS=超声等) |
| EXAM_CATEGORY_NAME | VARCHAR | 100 | NULL | 检查类别名称 | EXAM_TASK.EXAM_CATEGORY_NAME |
| EXEC_DEPT_NAME | VARCHAR | 100 | NULL | 执行科室名称 | EXAM_TASK.EXEC_DEPT_NAME |
| REPORTER_NAME | VARCHAR | 50 | NULL | 报告医生姓名 | EXAM_REPORT.REPORTER_NAME |
| REVIEWER_NAME | VARCHAR | 50 | NULL | 审核医生姓名 | EXAM_REPORT.REVIEWER_NAME |
| TECHNICIAN_NAME | VARCHAR | 50 | NULL | 技师姓名 | EXAM_TASK.TECHNICIAN_NAME |
| ENCOUNTER_TYPE_NO | VARCHAR | 20 | NULL | 病人类型编码 | EXAM_TASK.ENCOUNTER_TYPE_NO |
| ENCOUNTER_TYPE_NAME | VARCHAR | 50 | NULL | 病人类型名称 | 映射值(门诊/住院等) |
| NEG_POS_CODE | VARCHAR | 20 | NULL | 阴阳性编码 | EXAM_REPORT.NEG_POS_CODE |
| NEG_POS_NAME | VARCHAR | 20 | NULL | 阴阳性名称 | 映射值(阳性/阴性) |
| WORK_GROUP | VARCHAR | 50 | NULL | 工作分组 | 普放组/CT组/MRI组 |
| EXAM_SUBTYPE | VARCHAR | 100 | NULL | 检查子类型 | DR/CT平扫/MRI平扫等 |
| TASK_COUNT | INT | - | DEFAULT 0 | 任务数量 | COUNT(*) |
| POSITIVE_COUNT | INT | - | DEFAULT 0 | 阳性数量 | SUM(阳性) |
| NEGATIVE_COUNT | INT | - | DEFAULT 0 | 阴性数量 | SUM(阴性) |
| POSITIVE_RATE | DECIMAL | 5,2 | NULL | 阳性率 | 计算值 |
| IS_DEL | INT | - | DEFAULT 0 | 删除标志 | 0=正常,1=删除 |
| CREATED_AT | DATETIME | - | DEFAULT GETDATE() | 创建时间 | - |
| UPDATED_AT | DATETIME | - | NULL | 更新时间 | - |

**索引设计:**
```sql
CREATE INDEX IX_EXAM_WORKLOAD_STAT_DATE ON EXAM_WORKLOAD_STAT(STAT_DATE)
CREATE INDEX IX_EXAM_WORKLOAD_SYSTEM ON EXAM_WORKLOAD_STAT(SYSTEM_SOURCE_NO)
CREATE INDEX IX_EXAM_WORKLOAD_DEPT ON EXAM_WORKLOAD_STAT(EXEC_DEPT_NAME)
CREATE INDEX IX_EXAM_WORKLOAD_REPORTER ON EXAM_WORKLOAD_STAT(REPORTER_NAME)
CREATE INDEX IX_EXAM_WORKLOAD_ISDEL ON EXAM_WORKLOAD_STAT(IS_DEL)
```

#### 表名: `EXAM_WORKLOAD_CONFIG` (统计配置表)

| 字段名 | 类型 | 长度 | 约束 | 说明 |
|-------|------|------|------|------|
| ID | INT | - | PRIMARY KEY, IDENTITY(1,1) | 主键自增 |
| CONFIG_KEY | VARCHAR | 50 | UNIQUE NOT NULL | 配置键 |
| CONFIG_VALUE | VARCHAR | 500 | NULL | 配置值 |
| CONFIG_DESC | VARCHAR | 200 | NULL | 配置描述 |
| IS_ACTIVE | INT | - | DEFAULT 1 | 是否启用(1=启用,0=禁用) |
| CREATED_AT | DATETIME | - | DEFAULT GETDATE() | 创建时间 |

**预设配置项:**
| CONFIG_KEY | CONFIG_VALUE | CONFIG_DESC |
|-----------|-------------|-------------|
| AUTO_STAT_HOUR | 02 | 自动统计执行时间(凌晨2点) |
| RETENTION_DAYS | 365 | 数据保留天数 |
| POSITIVE_THRESHOLD_RIS | 60 | 放射科阳性率阈值(%) |
| SYNC_BATCH_SIZE | 1000 | 同步批处理大小 |

---

## 二、存储过程设计

### 2.1 存储过程: `USP_EXAM_GetWorkloadByDate`

**功能:** 按日期范围查询工作量统计

```sql
CREATE PROCEDURE USP_EXAM_GetWorkloadByDate
    @StartDate DATE,
    @EndDate DATE,
    @SystemSourceNo VARCHAR(20) = NULL,
    @ExecDeptName VARCHAR(100) = NULL,
    @ReporterName VARCHAR(50) = NULL,
    @ExamCategoryName VARCHAR(100) = NULL,
    @EncounterTypeNo VARCHAR(20) = NULL,
    @NegPosCode VARCHAR(20) = NULL,
    @PageSize INT = 100,
    @PageIndex INT = 1
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        STAT_DATE,
        SYSTEM_NAME AS 系统,
        EXAM_CATEGORY_NAME AS 检查类型,
        EXEC_DEPT_NAME AS 执行科室,
        REPORTER_NAME AS 报告医生,
        REVIEWER_NAME AS 审核医生,
        ENCOUNTER_TYPE_NAME AS 病人类型,
        NEG_POS_NAME AS 结果状态,
        TASK_COUNT AS 任务数量,
        POSITIVE_COUNT AS 阳性数量,
        NEGATIVE_COUNT AS 阴性数量,
        POSITIVE_RATE AS 阳性率
    FROM EXAM_WORKLOAD_STAT
    WHERE IS_DEL = 0
        AND STAT_DATE BETWEEN @StartDate AND @EndDate
        AND (@SystemSourceNo IS NULL OR SYSTEM_SOURCE_NO = @SystemSourceNo)
        AND (@ExecDeptName IS NULL OR EXEC_DEPT_NAME = @ExecDeptName)
        AND (@ReporterName IS NULL OR REPORTER_NAME = @ReporterName)
        AND (@ExamCategoryName IS NULL OR EXAM_CATEGORY_NAME = @ExamCategoryName)
        AND (@EncounterTypeNo IS NULL OR ENCOUNTER_TYPE_NO = @EncounterTypeNo)
        AND (@NegPosCode IS NULL OR NEG_POS_CODE = @NegPosCode)
    ORDER BY STAT_DATE DESC, TASK_COUNT DESC
    OFFSET (@PageIndex - 1) * @PageSize ROWS
    FETCH NEXT @PageSize ROWS ONLY;

    SELECT COUNT(*) AS TotalCount
    FROM EXAM_WORKLOAD_STAT
    WHERE IS_DEL = 0
        AND STAT_DATE BETWEEN @StartDate AND @EndDate
        AND (@SystemSourceNo IS NULL OR SYSTEM_SOURCE_NO = @SystemSourceNo)
        AND (@ExecDeptName IS NULL OR EXEC_DEPT_NAME = @ExecDeptName)
        AND (@ReporterName IS NULL OR REPORTER_NAME = @ReporterName)
        AND (@ExamCategoryName IS NULL OR EXAM_CATEGORY_NAME = @ExamCategoryName)
        AND (@EncounterTypeNo IS NULL OR ENCOUNTER_TYPE_NO = @EncounterTypeNo)
        AND (@NegPosCode IS NULL OR NEG_POS_CODE = @NegPosCode);
END
GO
```

### 2.2 存储过程: `USP_EXAM_GetWorkloadSummary`

**功能:** 获取工作量汇总统计

```sql
CREATE PROCEDURE USP_EXAM_GetWorkloadSummary
    @StartDate DATE,
    @EndDate DATE,
    @GroupBy VARCHAR(20) = 'DEPARTMENT' -- DEPARTMENT/REPORTER/CATEGORY/SYSTEM
AS
BEGIN
    SET NOCOUNT ON;

    IF @GroupBy = 'DEPARTMENT'
    BEGIN
        SELECT
            EXEC_DEPT_NAME AS GROUP_NAME,
            SUM(TASK_COUNT) AS TOTAL_TASK_COUNT,
            SUM(POSITIVE_COUNT) AS TOTAL_POSITIVE_COUNT,
            SUM(NEGATIVE_COUNT) AS TOTAL_NEGATIVE_COUNT,
            ROUND(CASE WHEN SUM(TASK_COUNT) > 0 THEN SUM(POSITIVE_COUNT) * 100.0 / SUM(TASK_COUNT) ELSE 0 END, 2) AS POSITIVE_RATE,
            COUNT(DISTINCT REPORTER_NAME) AS REPORTER_COUNT
        FROM EXAM_WORKLOAD_STAT
        WHERE IS_DEL = 0 AND STAT_DATE BETWEEN @StartDate AND @EndDate
        GROUP BY EXEC_DEPT_NAME
        ORDER BY TOTAL_TASK_COUNT DESC;
    END
    ELSE IF @GroupBy = 'REPORTER'
    BEGIN
        SELECT
            REPORTER_NAME AS GROUP_NAME,
            SUM(TASK_COUNT) AS TOTAL_TASK_COUNT,
            SUM(POSITIVE_COUNT) AS TOTAL_POSITIVE_COUNT,
            SUM(NEGATIVE_COUNT) AS TOTAL_NEGATIVE_COUNT,
            ROUND(CASE WHEN SUM(TASK_COUNT) > 0 THEN SUM(POSITIVE_COUNT) * 100.0 / SUM(TASK_COUNT) ELSE 0 END, 2) AS POSITIVE_RATE
        FROM EXAM_WORKLOAD_STAT
        WHERE IS_DEL = 0 AND STAT_DATE BETWEEN @StartDate AND @EndDate AND REPORTER_NAME IS NOT NULL
        GROUP BY REPORTER_NAME
        ORDER BY TOTAL_TASK_COUNT DESC;
    END
    ELSE IF @GroupBy = 'CATEGORY'
    BEGIN
        SELECT
            EXAM_CATEGORY_NAME AS GROUP_NAME,
            SUM(TASK_COUNT) AS TOTAL_TASK_COUNT,
            SUM(POSITIVE_COUNT) AS TOTAL_POSITIVE_COUNT,
            SUM(NEGATIVE_COUNT) AS TOTAL_NEGATIVE_COUNT,
            ROUND(CASE WHEN SUM(TASK_COUNT) > 0 THEN SUM(POSITIVE_COUNT) * 100.0 / SUM(TASK_COUNT) ELSE 0 END, 2) AS POSITIVE_RATE
        FROM EXAM_WORKLOAD_STAT
        WHERE IS_DEL = 0 AND STAT_DATE BETWEEN @StartDate AND @EndDate AND EXAM_CATEGORY_NAME IS NOT NULL
        GROUP BY EXAM_CATEGORY_NAME
        ORDER BY TOTAL_TASK_COUNT DESC;
    END
    ELSE IF @GroupBy = 'SYSTEM'
    BEGIN
        SELECT
            SYSTEM_NAME AS GROUP_NAME,
            SUM(TASK_COUNT) AS TOTAL_TASK_COUNT,
            SUM(POSITIVE_COUNT) AS TOTAL_POSITIVE_COUNT,
            SUM(NEGATIVE_COUNT) AS TOTAL_NEGATIVE_COUNT,
            ROUND(CASE WHEN SUM(TASK_COUNT) > 0 THEN SUM(POSITIVE_COUNT) * 100.0 / SUM(TASK_COUNT) ELSE 0 END, 2) AS POSITIVE_RATE
        FROM EXAM_WORKLOAD_STAT
        WHERE IS_DEL = 0 AND STAT_DATE BETWEEN @StartDate AND @EndDate
        GROUP BY SYSTEM_NAME
        ORDER BY TOTAL_TASK_COUNT DESC;
    END
END
GO
```

### 2.3 存储过程: `USP_EXAM_GetDailyTrend`

**功能:** 获取每日趋势数据

```sql
CREATE PROCEDURE USP_EXAM_GetDailyTrend
    @StartDate DATE,
    @EndDate DATE,
    @SystemSourceNo VARCHAR(20) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        STAT_DATE,
        SUM(TASK_COUNT) AS TOTAL_TASK_COUNT,
        SUM(POSITIVE_COUNT) AS TOTAL_POSITIVE_COUNT,
        SUM(NEGATIVE_COUNT) AS TOTAL_NEGATIVE_COUNT,
        ROUND(CASE WHEN SUM(TASK_COUNT) > 0 THEN SUM(POSITIVE_COUNT) * 100.0 / SUM(TASK_COUNT) ELSE 0 END, 2) AS POSITIVE_RATE
    FROM EXAM_WORKLOAD_STAT
    WHERE IS_DEL = 0
        AND STAT_DATE BETWEEN @StartDate AND @EndDate
        AND (@SystemSourceNo IS NULL OR SYSTEM_SOURCE_NO = @SystemSourceNo)
    GROUP BY STAT_DATE
    ORDER BY STAT_DATE;
END
GO
```

### 2.4 存储过程: `USP_EXAM_SyncWorkloadData`

**功能:** 同步原始数据到统计表

```sql
CREATE PROCEDURE USP_EXAM_SyncWorkloadData
    @SyncDate DATE = NULL
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @TargetDate DATE = ISNULL(@SyncDate, CAST(GETDATE() AS DATE));

    DELETE FROM EXAM_WORKLOAD_STAT 
    WHERE STAT_DATE = @TargetDate AND IS_DEL = 0;

    INSERT INTO EXAM_WORKLOAD_STAT (
        STAT_DATE,
        SYSTEM_SOURCE_NO,
        SYSTEM_NAME,
        EXAM_CATEGORY_NAME,
        EXEC_DEPT_NAME,
        REPORTER_NAME,
        REVIEWER_NAME,
        TECHNICIAN_NAME,
        ENCOUNTER_TYPE_NO,
        ENCOUNTER_TYPE_NAME,
        NEG_POS_CODE,
        NEG_POS_NAME,
        TASK_COUNT,
        POSITIVE_COUNT,
        NEGATIVE_COUNT,
        POSITIVE_RATE
    )
    SELECT
        @TargetDate AS STAT_DATE,
        t.SYSTEM_SOURCE_NO,
        CASE t.SYSTEM_SOURCE_NO
            WHEN 'UIS' THEN '超声'
            WHEN 'RIS' THEN '放射'
            WHEN 'EIS' THEN '内镜'
            WHEN 'PIS' THEN '病理'
            WHEN 'NMS' THEN '核医学'
            ELSE t.SYSTEM_SOURCE_NO
        END AS SYSTEM_NAME,
        ISNULL(t.EXAM_CATEGORY_NAME, '') AS EXAM_CATEGORY_NAME,
        ISNULL(t.EXEC_DEPT_NAME, '') AS EXEC_DEPT_NAME,
        ISNULL(r.REPORTER_NAME, '') AS REPORTER_NAME,
        ISNULL(r.REVIEWER_NAME, '') AS REVIEWER_NAME,
        ISNULL(t.TECHNICIAN_NAME, '') AS TECHNICIAN_NAME,
        ISNULL(t.ENCOUNTER_TYPE_NO, '') AS ENCOUNTER_TYPE_NO,
        ISNULL(CASE t.ENCOUNTER_TYPE_NO
            WHEN '1' THEN '门诊' WHEN '2' THEN '住院' WHEN '3' THEN '急诊' WHEN '4' THEN '体检'
            WHEN '138138' THEN '门诊' WHEN '138139' THEN '急诊' WHEN '138140' THEN '体检' WHEN '145235' THEN '住院'
            WHEN 'OPD' THEN '门诊' WHEN 'IPD' THEN '住院' WHEN 'EMER' THEN '急诊' WHEN 'CHECKUP' THEN '体检'
            ELSE ISNULL(d.cValue, t.ENCOUNTER_TYPE_NO)
        END, '') AS ENCOUNTER_TYPE_NAME,
        ISNULL(r.NEG_POS_CODE, '') AS NEG_POS_CODE,
        ISNULL(CASE r.NEG_POS_CODE
            WHEN '383927' THEN '阳性' WHEN '383926' THEN '阴性'
            WHEN 'P' THEN '阳性' WHEN 'N' THEN '阴性' WHEN 'Y' THEN '阳性'
            WHEN 'POS' THEN '阳性' WHEN 'NEG' THEN '阴性' ELSE '未知'
        END, '') AS NEG_POS_NAME,
        COUNT(*) AS TASK_COUNT,
        SUM(CASE WHEN r.NEG_POS_CODE IN ('383927', 'P', 'Y', 'POS', '阳性') THEN 1 ELSE 0 END) AS POSITIVE_COUNT,
        SUM(CASE WHEN r.NEG_POS_CODE IN ('383926', 'N', 'NEG', '阴性') THEN 1 ELSE 0 END) AS NEGATIVE_COUNT,
        ROUND(CASE WHEN COUNT(*) > 0 THEN SUM(CASE WHEN r.NEG_POS_CODE IN ('383927', 'P', 'Y', 'POS', '阳性') THEN 1.0 ELSE 0.0 END) * 100.0 / COUNT(*) ELSE 0.0 END, 2) AS POSITIVE_RATE
    FROM EXAM_TASK t WITH(NOLOCK)
    INNER JOIN EXAM_REPORT r WITH(NOLOCK) ON t.EXAM_TASK_ID = r.EXAM_TASK_ID
    LEFT JOIN Pacs_SysDict d WITH(NOLOCK) ON d.TableName = 'EXAM_TASK' 
        AND d.FieldName = 'ENCOUNTER_TYPE_NO' 
        AND (CAST(d.nValue AS VARCHAR(50)) = t.ENCOUNTER_TYPE_NO OR d.cValue = t.ENCOUNTER_TYPE_NO)
    WHERE t.IS_DEL = 0
        AND CAST(t.CREATED_AT AS DATE) = @TargetDate
    GROUP BY
        t.SYSTEM_SOURCE_NO,
        t.EXAM_CATEGORY_NAME,
        t.EXEC_DEPT_NAME,
        r.REPORTER_NAME,
        r.REVIEWER_NAME,
        t.TECHNICIAN_NAME,
        t.ENCOUNTER_TYPE_NO,
        CASE t.ENCOUNTER_TYPE_NO
            WHEN '1' THEN '门诊' WHEN '2' THEN '住院' WHEN '3' THEN '急诊' WHEN '4' THEN '体检'
            WHEN '138138' THEN '门诊' WHEN '138139' THEN '急诊' WHEN '138140' THEN '体检' WHEN '145235' THEN '住院'
            WHEN 'OPD' THEN '门诊' WHEN 'IPD' THEN '住院' WHEN 'EMER' THEN '急诊' WHEN 'CHECKUP' THEN '体检'
            ELSE ISNULL(d.cValue, t.ENCOUNTER_TYPE_NO)
        END,
        r.NEG_POS_CODE,
        CASE r.NEG_POS_CODE
            WHEN '383927' THEN '阳性' WHEN '383926' THEN '阴性'
            WHEN 'P' THEN '阳性' WHEN 'N' THEN '阴性' WHEN 'Y' THEN '阳性'
            WHEN 'POS' THEN '阳性' WHEN 'NEG' THEN '阴性' ELSE '未知'
        END;

    SELECT @@ROWCOUNT AS SyncCount;
END
GO
```

---

## 三、数据字典映射（与项目保持一致）

### 3.1 系统类型映射

| CODE | NAME |
|-----|------|
| UIS | 超声 |
| RIS | 放射 |
| EIS | 内镜 |
| PIS | 病理 |
| NMS | 核医学 |

### 3.2 病人类型映射

| CODE | NAME |
|-----|------|
| 1 | 门诊 |
| 2 | 住院 |
| 3 | 急诊 |
| 4 | 体检 |
| 138138 | 门诊 |
| 138139 | 急诊 |
| 138140 | 体检 |
| 145235 | 住院 |
| OPD | 门诊 |
| IPD | 住院 |
| EMER | 急诊 |
| CHECKUP | 体检 |

### 3.3 阴阳性映射

| CODE | NAME |
|-----|------|
| 383927 | 阳性 |
| 383926 | 阴性 |
| P | 阳性 |
| N | 阴性 |
| Y | 阳性 |
| POS | 阳性 |
| NEG | 阴性 |

---

## 四、API接口规范

### 4.1 接口列表

| API路径 | HTTP方法 | 对应存储过程 | 功能描述 |
|--------|---------|------------|---------|
| `/rad/workload` | POST | USP_EXAM_GetWorkloadByDate | 按条件查询工作量 |
| `/rad/workload/summary` | POST | USP_EXAM_GetWorkloadSummary | 获取汇总统计 |
| `/rad/workload/trend` | POST | USP_EXAM_GetDailyTrend | 获取每日趋势 |
| `/rad/workload/sync` | POST | USP_EXAM_SyncWorkloadData | 同步数据 |

### 4.2 请求/响应格式

**POST /rad/workload**

请求:
```json
{
    "startDate": "2026-04-01",
    "endDate": "2026-04-30",
    "systemSourceNo": "RIS",
    "execDeptName": null,
    "reporterName": null,
    "examCategoryName": null,
    "encounterTypeNo": null,
    "negPosCode": null,
    "pageSize": 100,
    "pageIndex": 1
}
```

响应:
```json
{
    "success": true,
    "data": [
        {
            "statDate": "2026-04-30",
            "系统": "放射",
            "检查类型": "CT",
            "执行科室": "放射科",
            "报告医生": "张三",
            "审核医生": "李四",
            "病人类型": "门诊",
            "结果状态": "阳性",
            "任务数量": 120,
            "阳性数量": 72,
            "阴性数量": 48,
            "阳性率": 60.00
        }
    ],
    "totalCount": 500
}
```

---

## 五、SQL脚本文件

### 5.1 创建表脚本: `CreateWorkloadTables.sql`

```sql
CREATE TABLE EXAM_WORKLOAD_STAT (
    ID INT PRIMARY KEY IDENTITY(1,1),
    STAT_DATE DATE NOT NULL,
    SYSTEM_SOURCE_NO VARCHAR(20) NOT NULL,
    SYSTEM_NAME VARCHAR(50),
    EXAM_CATEGORY_NAME VARCHAR(100),
    EXEC_DEPT_NAME VARCHAR(100),
    REPORTER_NAME VARCHAR(50),
    REVIEWER_NAME VARCHAR(50),
    TECHNICIAN_NAME VARCHAR(50),
    ENCOUNTER_TYPE_NO VARCHAR(20),
    ENCOUNTER_TYPE_NAME VARCHAR(50),
    NEG_POS_CODE VARCHAR(20),
    NEG_POS_NAME VARCHAR(20),
    TASK_COUNT INT DEFAULT 0,
    POSITIVE_COUNT INT DEFAULT 0,
    NEGATIVE_COUNT INT DEFAULT 0,
    POSITIVE_RATE DECIMAL(5,2),
    IS_DEL INT DEFAULT 0,
    CREATED_AT DATETIME DEFAULT GETDATE(),
    UPDATED_AT DATETIME
);

CREATE TABLE EXAM_WORKLOAD_CONFIG (
    ID INT PRIMARY KEY IDENTITY(1,1),
    CONFIG_KEY VARCHAR(50) UNIQUE NOT NULL,
    CONFIG_VALUE VARCHAR(500),
    CONFIG_DESC VARCHAR(200),
    IS_ACTIVE INT DEFAULT 1,
    CREATED_AT DATETIME DEFAULT GETDATE()
);

CREATE INDEX IX_EXAM_WORKLOAD_STAT_DATE ON EXAM_WORKLOAD_STAT(STAT_DATE);
CREATE INDEX IX_EXAM_WORKLOAD_SYSTEM ON EXAM_WORKLOAD_STAT(SYSTEM_SOURCE_NO);
CREATE INDEX IX_EXAM_WORKLOAD_DEPT ON EXAM_WORKLOAD_STAT(EXEC_DEPT_NAME);
CREATE INDEX IX_EXAM_WORKLOAD_REPORTER ON EXAM_WORKLOAD_STAT(REPORTER_NAME);
CREATE INDEX IX_EXAM_WORKLOAD_ISDEL ON EXAM_WORKLOAD_STAT(IS_DEL);

INSERT INTO EXAM_WORKLOAD_CONFIG (CONFIG_KEY, CONFIG_VALUE, CONFIG_DESC) VALUES
('AUTO_STAT_HOUR', '02', '自动统计执行时间'),
('RETENTION_DAYS', '365', '数据保留天数'),
('POSITIVE_THRESHOLD_RIS', '60', '放射科阳性率阈值(%)'),
('SYNC_BATCH_SIZE', '1000', '同步批处理大小');
GO
```

---

## 六、与现有项目的兼容性说明

| 项目特性 | 当前设计 | 项目规范 | 兼容性 |
|---------|---------|---------|-------|
| 数据库类型 | SQL Server | SQL Server | ✅ 一致 |
| 表命名风格 | EXAM_前缀 | EXAM_前缀 | ✅ 一致 |
| 删除标志 | IS_DEL | IS_DEL | ✅ 一致 |
| 时间字段 | CREATED_AT/UPDATED_AT | CREATED_AT/UPDATED_AT | ✅ 一致 |
| 系统编码映射 | 统一映射表 | 统一映射表 | ✅ 一致 |
| 参数化查询 | 支持 | 支持 | ✅ 一致 |
| 存储过程命名 | USP_EXAM_前缀 | USP_前缀 | ✅ 兼容 |
