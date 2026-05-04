
# 放射科工作量统计系统 - 配置流程指南

## 📋 目录
1. [环境准备](#环境准备)
2. [数据库配置](#数据库配置)
3. [脚本执行流程](#脚本执行流程)
4. [测试验证步骤](#测试验证步骤)
5. [API接口测试](#api接口测试)
6. [常见问题](#常见问题)

---

## 1. 环境准备

### 1.1 系统要求
| 组件 | 版本 | 说明 |
|-----|------|------|
| SQL Server | 2019+ | 数据库服务器 |
| .NET Framework | 4.8+ | 后端服务 |
| IIS | 10+ | Web服务器（可选） |

### 1.2 目录结构
```
rad-workload/
├── sql_scripts/              # SQL脚本目录
│   ├── 01_CreateTables.sql   # 创建数据表
│   ├── 02_CreateProcedures.sql # 创建存储过程
│   └── 03_TestQueries.sql    # 测试查询脚本
├── rad_workload.html          # 前端页面
└── rad_workload_design.md     # 设计文档
```

---

## 2. 数据库配置

### 2.1 连接字符串配置
在项目的配置文件中设置数据库连接：

**appsettings.json 示例:**
```json
{
    "ConnectionStrings": {
        "DefaultConnection": "Server=localhost;Database=YOUR_DATABASE_NAME;Trusted_Connection=True;MultipleActiveResultSets=True"
    }
}
```

### 2.2 数据库权限要求
确保数据库用户具有以下权限：
- `CREATE TABLE` - 创建表
- `CREATE PROCEDURE` - 创建存储过程
- `CREATE INDEX` - 创建索引
- `INSERT/UPDATE/DELETE/SELECT` - 数据操作

---

## 3. 脚本执行流程

### 3.1 执行顺序
```
步骤1: 01_CreateTables.sql     → 创建数据表和索引
    ↓
步骤2: 02_CreateProcedures.sql → 创建存储过程
    ↓
步骤3: 插入测试数据           → 可选，用于实验
    ↓
步骤4: 验证数据               → 查询验证
```

### 3.2 使用 SQL Server Management Studio (SSMS) 执行

**步骤1: 连接数据库**
1. 打开 SSMS
2. 连接到目标数据库服务器
3. 选择要使用的数据库

**步骤2: 执行创建表脚本**
1. 打开 `01_CreateTables.sql`
2. 点击 "执行" 按钮（F5）
3. 查看执行结果，确认表创建成功

**步骤3: 执行创建存储过程脚本**
1. 打开 `02_CreateProcedures.sql`
2. 点击 "执行" 按钮（F5）
3. 查看执行结果，确认存储过程创建成功

### 3.3 使用命令行执行
```bash
# 使用 sqlcmd 执行（需要配置环境变量）
sqlcmd -S localhost -d YourDatabaseName -i "sql_scripts/01_CreateTables.sql" -o "output.txt"
sqlcmd -S localhost -d YourDatabaseName -i "sql_scripts/02_CreateProcedures.sql" -o "output.txt"
```

---

## 4. 测试验证步骤

### 4.1 插入测试数据

**方法1: 调用存储过程**
```sql
-- 插入指定日期范围的测试数据
EXEC USP_EXAM_InsertTestData '2026-04-01', '2026-04-30';
GO
```

**方法2: 手动插入单条测试数据**
```sql
INSERT INTO EXAM_WORKLOAD_STAT (
    STAT_DATE, SYSTEM_SOURCE_NO, SYSTEM_NAME, EXAM_CATEGORY_NAME,
    EXEC_DEPT_NAME, REPORTER_NAME, REVIEWER_NAME, TECHNICIAN_NAME,
    ENCOUNTER_TYPE_NO, ENCOUNTER_TYPE_NAME, NEG_POS_CODE, NEG_POS_NAME,
    TASK_COUNT, POSITIVE_COUNT, NEGATIVE_COUNT, POSITIVE_RATE
) VALUES (
    '2026-04-30', 'RIS', '放射', 'CT',
    '放射科', '张三', '李四', '王五',
    '1', '门诊', 'P', '阳性',
    50, 30, 20, 60.00
);
GO
```

### 4.2 查询验证

**验证表结构**
```sql
-- 查看表结构
SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'EXAM_WORKLOAD_STAT';

-- 查看索引
EXEC sp_helpindex 'EXAM_WORKLOAD_STAT';
```

**验证数据**
```sql
-- 查询统计数据
SELECT * FROM EXAM_WORKLOAD_STAT WHERE IS_DEL = 0 ORDER BY STAT_DATE DESC;

-- 统计记录数
SELECT COUNT(*) AS TotalRecords FROM EXAM_WORKLOAD_STAT WHERE IS_DEL = 0;
```

**调用存储过程**
```sql
-- 查询工作量数据
EXEC USP_EXAM_GetWorkloadByDate '2026-04-01', '2026-04-30';

-- 获取汇总统计（按科室）
EXEC USP_EXAM_GetWorkloadSummary '2026-04-01', '2026-04-30', 'DEPARTMENT';

-- 获取汇总统计（按医生）
EXEC USP_EXAM_GetWorkloadSummary '2026-04-01', '2026-04-30', 'REPORTER';

-- 获取每日趋势
EXEC USP_EXAM_GetDailyTrend '2026-04-01', '2026-04-30';
```

---

## 5. API接口测试

### 5.1 接口列表

| API路径 | HTTP方法 | 功能 |
|--------|---------|------|
| `/rad/workload` | POST | 查询工作量 |
| `/rad/workload/summary` | POST | 获取汇总 |
| `/rad/workload/trend` | POST | 获取趋势 |
| `/rad/workload/sync` | POST | 同步数据 |

### 5.2 使用 curl 测试

**查询工作量**
```bash
curl -X POST http://localhost:5000/rad/workload \
  -H "Content-Type: application/json" \
  -d '{
    "startDate": "2026-04-01",
    "endDate": "2026-04-30",
    "systemSourceNo": "RIS",
    "pageSize": 10,
    "pageIndex": 1
  }'
```

**获取汇总**
```bash
curl -X POST http://localhost:5000/rad/workload/summary \
  -H "Content-Type: application/json" \
  -d '{
    "startDate": "2026-04-01",
    "endDate": "2026-04-30",
    "groupBy": "DEPARTMENT"
  }'
```

**获取趋势**
```bash
curl -X POST http://localhost:5000/rad/workload/trend \
  -H "Content-Type: application/json" \
  -d '{
    "startDate": "2026-04-01",
    "endDate": "2026-04-30"
  }'
```

### 5.3 响应格式示例

**成功响应:**
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
            "任务数量": 50,
            "阳性数量": 30,
            "阴性数量": 20,
            "阳性率": 60.00
        }
    ],
    "totalCount": 100
}
```

**失败响应:**
```json
{
    "success": false,
    "error": "数据库连接失败",
    "data": []
}
```

---

## 6. 常见问题

### 6.1 问题1: 存储过程创建失败

**现象:** 执行存储过程脚本时报错

**解决:**
1. 检查数据库版本是否支持 `OFFSET/FETCH` 语法（SQL Server 2012+）
2. 确保执行用户有 `CREATE PROCEDURE` 权限
3. 检查脚本中是否有语法错误

### 6.2 问题2: 查询返回空数据

**现象:** 调用存储过程返回空结果

**解决:**
1. 检查日期参数格式是否正确（`YYYY-MM-DD`）
2. 确认 `EXAM_WORKLOAD_STAT` 表中有数据
3. 检查 `IS_DEL` 字段是否为 0

### 6.3 问题3: 同步数据失败

**现象:** 执行 `USP_EXAM_SyncWorkloadData` 时报错

**解决:**
1. 确认 `EXAM_TASK` 和 `EXAM_REPORT` 表存在
2. 确认表结构与预期一致
3. 检查数据库用户对源表有读取权限

### 6.4 问题4: 前端页面无法访问

**现象:** 打开 `rad_workload.html` 显示空白或报错

**解决:**
1. 确保网络连接正常
2. 检查浏览器控制台是否有 JavaScript 错误
3. 确认后端 API 服务正在运行

---

## 📝 实验记录模板

```
实验日期: 2026-XX-XX
数据库: [数据库名称]
服务器: [服务器地址]

执行步骤:
1. ✅ 执行 01_CreateTables.sql - 成功
2. ✅ 执行 02_CreateProcedures.sql - 成功
3. ✅ 插入测试数据 - 成功，插入 XX 条记录
4. ✅ 测试查询 - 返回 XX 条数据

测试结果:
- USP_EXAM_GetWorkloadByDate: ✅ 正常
- USP_EXAM_GetWorkloadSummary: ✅ 正常
- USP_EXAM_GetDailyTrend: ✅ 正常
- USP_EXAM_SyncWorkloadData: ✅ 正常

问题与解决:
- [问题描述] → [解决方案]
```

---

## 📞 技术支持

如遇问题，请提供以下信息：
1. 错误信息截图
2. SQL Server 版本
3. 执行的脚本内容
4. 数据库名称和连接方式
