
# 统计分析系统 - 模块化配置方案

## 一、方案目标

**核心目标**：让任何用户都能**无需编码**，通过简单配置即可添加新的统计功能。

| 目标 | 说明 |
|------|------|
| **零代码** | 无需编写代码，只需配置 |
| **模块化** | 配置即功能，独立运行 |
| **标准化** | 参数、样式、交互统一标准 |
| **易扩展** | 快速添加新统计功能 |
| **自助式** | 用户可自行配置，无需技术支持 |

---

## 二、系统架构

```
┌────────────────────────────────────────────────────────────────┐
│                    统计分析系统架构                            │
├────────────────────────────────────────────────────────────────┤
│  配置层                                                        │
│    ├── proc_configs (存储过程配置表)                          │
│    └── 参数配置 (JSON格式)                                   │
├────────────────────────────────────────────────────────────────┤
│  服务层                                                        │
│    ├── 参数管理服务 (ParamService)                           │
│    ├── 存储过程执行服务 (ProcService)                        │
│    └── 模板渲染服务 (TemplateService)                       │
├────────────────────────────────────────────────────────────────┤
│  前端层                                                        │
│    ├── 参数控件渲染器                                         │
│    ├── 结果展示组件                                           │
│    └── 自定义模板容器                                         │
└────────────────────────────────────────────────────────────────┘
```

---

## 三、配置数据结构

### 3.1 存储过程配置表 (proc_configs)

| 字段名 | 类型 | 必填 | 说明 |
|--------|------|------|------|
| `id` | VARCHAR(50) | ✅ | 唯一标识（小写+下划线） |
| `name` | VARCHAR(100) | ✅ | 显示名称 |
| `icon` | VARCHAR(50) | ❌ | FontAwesome图标类名 |
| `procName` | VARCHAR(100) | ✅ | 存储过程名称 |
| `templateName` | VARCHAR(100) | ❌ | 自定义模板文件名 |
| `parameters` | TEXT | ❌ | 参数配置JSON |
| `enabled` | BIT | ✅ | 是否启用 |
| `sortOrder` | INT | ❌ | 排序序号 |
| `category` | VARCHAR(50) | ❌ | 分类 |

### 3.2 参数配置JSON结构

```json
{
  "name": "@ParamName",           // ✅ 存储过程参数名（必须带@）
  "displayName": "显示名称",       // ✅ 界面显示名
  "type": "datetime",             // ✅ 参数类型
  "defaultValue": "",             // ❌ 默认值
  "options": "",                  // ❌ 下拉选项（逗号分隔）
  "isRequired": false,            // ❌ 是否必填
  "placeholder": "",              // ❌ 占位提示
  "group": "time"                 // ❌ 参数分组
}
```

**参数类型对照表：**

| 类型 | 前端控件 | 说明 |
|------|----------|------|
| `datetime` | `<input type="date">` | 日期选择器 |
| `datetime-local` | `<input type="datetime-local">` | 日期时间选择器 |
| `varchar` | `<input type="text">` | 文本输入框 |
| `int` | `<input type="number">` | 整数输入框 |
| `select` | `<select>` | 下拉选择框 |

---

## 四、完整配置流程

### 4.1 添加新统计功能步骤

```
┌──────────────────────────────────────────────────────────────┐
│                    添加统计功能流程                          │
├──────────────────────────────────────────────────────────────┤
│  Step 1: 创建存储过程                                        │
│    └── 在数据库中创建存储过程                                │
│        例: CREATE PROCEDURE proc_MyStatistics               │
│            @StartDate DATE, @EndDate DATE                   │
│            AS BEGIN ... END                                 │
├──────────────────────────────────────────────────────────────┤
│  Step 2: 添加配置记录                                        │
│    └── 在 proc_configs 表中插入配置                         │
│        INSERT INTO proc_configs (id, name, procName, ...)   │
├──────────────────────────────────────────────────────────────┤
│  Step 3: 配置参数（可选）                                     │
│    └── 设置 parameters JSON                                 │
│        [{"name":"@StartDate","type":"datetime",...}]        │
├──────────────────────────────────────────────────────────────┤
│  Step 4: 创建模板（可选）                                     │
│    └── 在 templates/ 目录下创建 HTML 文件                   │
├──────────────────────────────────────────────────────────────┤
│  Step 5: 刷新页面即可使用                                    │
│    └── 系统自动加载配置并渲染菜单和控件                      │
└──────────────────────────────────────────────────────────────┘
```

### 4.2 配置示例

**1. 数据库配置：**

```sql
INSERT INTO proc_configs (
    id, 
    name, 
    icon, 
    procName, 
    templateName, 
    parameters, 
    enabled, 
    sortOrder
) VALUES (
    'rad_workload',
    '影像中心工作量',
    'fa-solid fa-chart-bar',
    'proc_RadiologyWorkload',
    'rad_workload.html',
    '[
        {"name":"@StartDate","displayName":"开始日期","type":"datetime","isRequired":true},
        {"name":"@EndDate","displayName":"结束日期","type":"datetime","isRequired":true},
        {"name":"@StatisticsType","displayName":"统计类型","type":"select","options":"报告医生,审核医生,技师","defaultValue":"报告医生"}
    ]',
    1,
    1
);
```

**2. 存储过程：**

```sql
CREATE PROCEDURE proc_RadiologyWorkload
    @StartDate DATE,
    @EndDate DATE,
    @StatisticsType VARCHAR(50) = '报告医生'
AS
BEGIN
    SET NOCOUNT ON;
    
    -- 查询逻辑
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

## 五、前端交互流程

### 5.1 用户操作流程

```
┌──────────────────────────────────────────────────────────────┐
│                    用户操作流程                              │
├──────────────────────────────────────────────────────────────┤
│  1. 进入统计分析页面                                         │
│     └── 系统加载所有配置，渲染左侧菜单                        │
│                                                             │
│  2. 点击菜单                                                │
│     ├── 加载对应配置                                         │
│     ├── 渲染参数输入控件                                     │
│     └── 显示默认值                                          │
│                                                             │
│  3. 输入参数                                                │
│     └── 用户填写或选择参数值                                 │
│                                                             │
│  4. 执行查询                                                │
│     ├── 收集参数值                                          │
│     ├── 调用 /execute-stored-proc API                       │
│     └── 获取数据                                             │
│                                                             │
│  5. 展示结果                                                │
│     ├── 有模板：渲染自定义模板                               │
│     └── 无模板：使用通用表格展示                             │
└──────────────────────────────────────────────────────────────┘
```

### 5.2 参数控件渲染规则

| 参数类型 | 渲染结果 |
|----------|----------|
| `datetime` | `<input type="date" id="input_StartDate">` |
| `varchar` | `<input type="text" id="input_Name">` |
| `int` | `<input type="number" id="input_Count">` |
| `select` | `<select id="input_Type"><option>...</option></select>` |

---

## 六、模板开发规范

### 6.1 模板文件结构

```html
<!-- templates/my_template.html -->
<!DOCTYPE html>
<html>
<head>
    <style>
        /* 自定义样式 */
        .my-table { width: 100%; }
    </style>
</head>
<body>
    <!-- 页面内容 -->
    <div class="container">
        <h2>统计报表</h2>
        <table class="my-table">
            <!-- 数据渲染区域 -->
        </table>
    </div>
    
    <script>
        // 自定义脚本
        function loadData() { /* ... */ }
    </script>
</body>
</html>
```

### 6.2 模板占位符

| 占位符 | 说明 | 示例 |
|--------|------|------|
| `{{data}}` | 数据源JSON | `[{"name":"张三",...}]` |
| `{{columnNames}}` | 列名数组 | `["姓名","数量"]` |
| `{{rowCount}}` | 行数 | `10` |

---

## 七、API接口规范

### 7.1 获取配置列表

```
GET /get-proc-configs

响应：
{
  "success": true,
  "data": [
    {
      "id": "rad_workload",
      "name": "影像中心工作量",
      "icon": "fa-solid fa-chart-bar",
      "procName": "proc_RadiologyWorkload",
      "parameters": "[{...}]"
    }
  ]
}
```

### 7.2 执行存储过程

```
POST /execute-stored-proc
Content-Type: application/json

请求：
{
  "procName": "proc_RadiologyWorkload",
  "parameters": {
    "@StartDate": "2026-05-01",
    "@EndDate": "2026-05-31"
  }
}

响应：
{
  "success": true,
  "data": [
    {"报告医生": "张三", "任务数量": 120}
  ]
}
```

---

## 八、配置检查表

| 检查项 | 说明 | 状态 |
|--------|------|------|
| ✅ 参数名一致性 | 配置中的name与存储过程参数名完全一致 | |
| ✅ 必填项检查 | required参数必须填写 | |
| ✅ 类型匹配 | 参数值类型与存储过程定义匹配 | |
| ✅ 模板存在性 | 如果配置了templateName，文件必须存在 | |
| ✅ SQL注入防护 | 后端使用参数化查询 | |

---

## 九、扩展说明

### 9.1 添加新统计功能

1. **创建存储过程** → 定义参数和查询逻辑
2. **添加配置** → 在 proc_configs 表中插入记录
3. **配置参数** → 设置 parameters JSON（可选）
4. **创建模板** → 自定义展示样式（可选）
5. **完成** → 刷新页面即可使用

### 9.2 修改现有统计

1. **修改配置** → 更新 proc_configs 记录
2. **修改存储过程** → 更新查询逻辑
3. **修改模板** → 更新展示样式（可选）
4. **完成** → 刷新页面即可生效

---

## 十、客户自助操作指南

### 10.1 准备工作

1. 确保已创建存储过程
2. 了解存储过程的参数名称和类型

### 10.2 配置步骤

**步骤1：登录系统**
- 进入"综合配置" → "存储过程管理"

**步骤2：添加配置**
- 点击"新增"按钮
- 填写：ID、名称、存储过程名
- 配置参数（可选）
- 上传模板（可选）

**步骤3：启用配置**
- 设置"启用"为是
- 保存配置

**步骤4：测试**
- 返回统计分析页面
- 点击新添加的菜单
- 输入参数并执行查询

---

## 十一、版本记录

| 版本 | 日期 | 变更说明 |
|------|------|----------|
| v1.0 | 2026-05-05 | 初始版本 |

---

## 十二、FAQ

### Q1：如何添加日期范围参数？
**A**：添加两个 datetime 类型参数：@StartDate 和 @EndDate

### Q2：如何添加下拉选项？
**A**：设置 type 为 select，并在 options 中填写选项（逗号分隔）

### Q3：模板中的数据如何渲染？
**A**：使用 {{data}} 占位符，系统会自动替换为JSON数据

### Q4：如何自定义样式？
**A**：在模板的 `<style>` 标签中添加自定义样式

### Q5：参数如何设置默认值？
**A**：在参数配置中设置 defaultValue 字段

---

**确认核对清单：**

- [ ] 参数类型标准
- [ ] 配置数据结构
- [ ] 配置流程
- [ ] 模板机制
- [ ] API接口
- [ ] 自助操作指南

请您核对以上方案，如有不满足的地方请指出，我会立即调整！
