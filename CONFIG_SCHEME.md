
# 统计分析系统 - 自定义统计对接方案

## 一、系统架构

```
┌─────────────────────────────────────────────────────────────┐
│                    统计分析系统                              │
├─────────────────────────────────────────────────────────────┤
│  前端 (statistics.html)                                    │
│    ├── 菜单配置 (自定义统计菜单)                            │
│    ├── 参数输入区域 (搜索面板)                              │
│    └── 结果展示区域 (表格/图表)                            │
├─────────────────────────────────────────────────────────────┤
│  后端 API                                                  │
│    ├── /get-proc-configs    (获取存储过程配置)              │
│    └── /execute-stored-proc (执行存储过程)                 │
├─────────────────────────────────────────────────────────────┤
│  数据库                                                     │
│    ├── 存储过程配置表 (proc_configs)                        │
│    └── 存储过程 (实际执行的SQL)                            │
└─────────────────────────────────────────────────────────────┘
```

---

## 二、配置数据结构

### 2.1 存储过程配置表结构 (proc_configs)

| 字段名 | 类型 | 说明 | 必填 |
|--------|------|------|------|
| id | string | 配置唯一标识（小写字母+下划线） | ✅ |
| name | string | 显示名称（如"影像中心工作量"） | ✅ |
| icon | string | FontAwesome图标类名 | ❌ |
| procName | string | 存储过程名称 | ✅ |
| templateName | string | 自定义模板HTML文件名 | ❌ |
| parameters | json | 参数配置数组 | ❌ |
| enabled | bool | 是否启用 | ✅ |
| sortOrder | int | 排序序号 | ❌ |

### 2.2 参数配置结构 (parameters)

```json
{
  "name": "@StartDate",          // ✅ 存储过程参数名（必须带@）
  "displayName": "开始日期",       // ✅ 界面显示名称
  "type": "datetime",             // ✅ 参数类型：datetime/varchar/int
  "defaultValue": "",             // ❌ 默认值
  "options": "",                  // ❌ 选项列表（逗号分隔）
  "isRequired": false,            // ❌ 是否必填
  "isMultiple": false,            // ❌ 是否多选
  "description": ""               // ❌ 参数描述
}
```

---

## 三、参数类型与控件映射

| 参数类型 | 渲染控件 | 说明 |
|----------|----------|------|
| datetime | `<input type="date">` | 日期选择器 |
| int | `<input type="number">` | 数字输入框 |
| varchar + options | `<select>` | 下拉选择框 |
| varchar | `<input type="text">` | 文本输入框 |

### 选项配置格式 (options)

```
// 简单值格式（值与标签相同）
"全部,选项1,选项2"

// 键值对格式（冒号分隔）
"0:全部,1:选项1,2:选项2"
```

---

## 四、完整执行流程

```
┌──────────────────────────────────────────────────────────────────────────┐
│                        自定义统计完整流程                                │
├──────────────────────────────────────────────────────────────────────────┤
│  1. 加载阶段                                                            │
│     └── 前端请求 /get-proc-configs 获取所有配置                          │
│                                                                         │
│  2. 菜单渲染                                                            │
│     └── 根据配置列表渲染左侧自定义统计菜单                                │
│                                                                         │
│  3. 用户点击菜单                                                         │
│     ├── 加载对应配置                                                    │
│     ├── 解析 parameters 数组                                            │
│     └── 动态渲染参数输入控件到搜索面板                                    │
│                                                                         │
│  4. 用户输入参数并执行                                                   │
│     ├── 收集所有 .param-value-input 的值                                │
│     ├── 组装参数对象 {"@StartDate": "2026-05-01", ...}                 │
│     └── POST /execute-stored-proc 执行存储过程                          │
│                                                                         │
│  5. 结果展示                                                            │
│     ├── 如果有 templateName，加载并渲染自定义模板                        │
│     └── 否则使用通用表格渲染                                             │
└──────────────────────────────────────────────────────────────────────────┘
```

---

## 五、配置示例

### 5.1 配置表记录示例

```sql
INSERT INTO proc_configs (id, name, icon, procName, templateName, parameters, enabled)
VALUES (
  'rad_workload',
  '影像中心工作量',
  'fa-solid fa-chart-bar',
  'proc_RadiologyWorkload',
  'rad_workload.html',
  '[
    {"name":"@StartDate","displayName":"开始日期","type":"datetime","isRequired":true},
    {"name":"@EndDate","displayName":"结束日期","type":"datetime","isRequired":true},
    {"name":"@StatisticsType","displayName":"统计类型","type":"varchar","options":"报告医生,审核医生,技师","defaultValue":"报告医生"}
  ]',
  1
);
```

### 5.2 存储过程签名示例

```sql
CREATE PROCEDURE proc_RadiologyWorkload
    @StartDate DATE,
    @EndDate DATE,
    @StatisticsType VARCHAR(50) = '报告医生'
AS
BEGIN
    SET NOCOUNT ON;
    
    -- 查询逻辑...
    SELECT 
        报告医生,
        COUNT(*) AS 任务数量,
        SUM(CASE WHEN 阳性标识 = 1 THEN 1 ELSE 0 END) AS 阳性数
    FROM 检查记录表
    WHERE 检查日期 BETWEEN @StartDate AND @EndDate
    GROUP BY 报告医生;
END
```

---

## 六、API 接口规范

### 6.1 获取配置列表

**请求：**
```
GET /get-proc-configs
```

**响应：**
```json
{
  "success": true,
  "data": [
    {
      "id": "rad_workload",
      "name": "影像中心工作量",
      "icon": "fa-solid fa-chart-bar",
      "procName": "proc_RadiologyWorkload",
      "templateName": "rad_workload.html",
      "parameters": "[{...}]",
      "enabled": true
    }
  ]
}
```

### 6.2 执行存储过程

**请求：**
```
POST /execute-stored-proc
Content-Type: application/json

{
  "procName": "proc_RadiologyWorkload",
  "parameters": {
    "@StartDate": "2026-05-01",
    "@EndDate": "2026-05-31",
    "@StatisticsType": "报告医生"
  }
}
```

**响应：**
```json
{
  "success": true,
  "data": [
    {"报告医生": "张三", "任务数量": 120, "阳性数": 15},
    {"报告医生": "李四", "任务数量": 85, "阳性数": 8}
  ]
}
```

---

## 七、配置检查清单

| 检查项 | 说明 |
|--------|------|
| ✅ 参数名一致性 | 配置中的name必须与存储过程参数名完全一致（带@） |
| ✅ 必填项检查 | 确保required参数有值后再执行 |
| ✅ 类型匹配 | 参数值类型需与存储过程定义匹配 |
| ✅ 模板存在性 | 如果配置了templateName，确保文件存在 |
| ✅ SQL注入防护 | 后端使用参数化查询 |

---

## 八、扩展说明

### 8.1 添加新的自定义统计

1. 在数据库中添加配置记录
2. 创建对应的存储过程
3. （可选）创建自定义模板HTML文件
4. 确保参数配置正确

### 8.2 自定义模板开发规范

- 模板文件放在 `templates/` 目录下
- 使用 `{{columnNames}}`、`{{rowCount}}`、`{{data}}` 作为占位符
- 模板应为完整HTML页面，系统会自动提取body内容

---

## 九、版本记录

| 版本 | 日期 | 变更说明 |
|------|------|----------|
| v1.0 | 2026-05-05 | 初始版本 |
