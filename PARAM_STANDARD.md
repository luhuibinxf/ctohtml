
# 统计分析系统 - 参数标准化方案

## 一、参数标准架构

```
┌─────────────────────────────────────────────────────────────┐
│                    参数标准化体系                            │
├─────────────────────────────────────────────────────────────┤
│  参数类型定义                                               │
│    ├── 基础类型：datetime / varchar / int / decimal        │
│    └── 扩展类型：select / checkbox / dateRange             │
├─────────────────────────────────────────────────────────────┤
│  参数控件映射                                               │
│    ├── datetime → <input type="date">                     │
│    ├── varchar + options → <select>                       │
│    ├── varchar → <input type="text">                      │
│    └── int → <input type="number">                        │
├─────────────────────────────────────────────────────────────┤
│  参数命名规范                                               │
│    ├── 存储过程参数：@ParamName                            │
│    ├── 前端控件name：param_ParamName                       │
│    └── 前端控件id：input_ParamName                         │
└─────────────────────────────────────────────────────────────┘
```

---

## 二、参数类型标准

### 2.1 基础类型定义

| 类型代码 | 类型名称 | 数据库类型 | 前端控件 | 说明 |
|----------|----------|------------|----------|------|
| `datetime` | 日期时间 | DATETIME | `<input type="date">` | 日期选择器 |
| `datetime-local` | 日期时间（含时分） | DATETIME | `<input type="datetime-local">` | 日期时间选择器 |
| `varchar` | 字符串 | VARCHAR | `<input type="text">` | 文本输入框 |
| `int` | 整数 | INT | `<input type="number">` | 数字输入框 |
| `decimal` | 小数 | DECIMAL | `<input type="number" step="any">` | 小数输入框 |
| `select` | 下拉选择 | VARCHAR | `<select>` | 下拉选择框 |
| `checkbox` | 复选框 | BIT/INT | `<input type="checkbox">` | 布尔选择 |

### 2.2 扩展类型定义

| 类型代码 | 说明 | 实现方式 |
|----------|------|----------|
| `dateRange` | 日期范围 | 两个 date 控件组合 |
| `multiSelect` | 多选 | `<select multiple>` |
| `autoComplete` | 自动补全 | 带搜索的下拉框 |
| `treeSelect` | 树形选择 | 级联选择器 |

---

## 三、参数配置标准

### 3.1 参数配置JSON结构

```json
{
  "name": "@StartDate",           // ✅ 必填，存储过程参数名（必须带@）
  "displayName": "开始日期",       // ✅ 必填，界面显示名称
  "type": "datetime",             // ✅ 必填，参数类型
  "defaultValue": "",             // ❌ 默认值
  "options": "",                  // ❌ 选项列表（select类型必填）
  "isRequired": false,            // ❌ 是否必填
  "isMultiple": false,            // ❌ 是否多选
  "placeholder": "",              // ❌ 占位提示
  "description": "",              // ❌ 参数描述
  "controlId": "",                // ❌ 前端控件ID（自动生成时可省略）
  "group": "time",                // ❌ 参数分组
  "sortOrder": 1                  // ❌ 排序序号
}
```

### 3.2 参数配置示例

```json
[
  {
    "name": "@StartDate",
    "displayName": "开始日期",
    "type": "datetime",
    "defaultValue": "",
    "isRequired": true,
    "group": "time",
    "sortOrder": 1
  },
  {
    "name": "@EndDate",
    "displayName": "结束日期",
    "type": "datetime",
    "defaultValue": "",
    "isRequired": true,
    "group": "time",
    "sortOrder": 2
  },
  {
    "name": "@StatisticsType",
    "displayName": "统计类型",
    "type": "select",
    "defaultValue": "报告医生",
    "options": "报告医生,审核医生,技师",
    "group": "filter",
    "sortOrder": 1
  },
  {
    "name": "@SystemType",
    "displayName": "系统类型",
    "type": "select",
    "defaultValue": "",
    "options": "全部,放射,超声,内镜,病理",
    "group": "filter",
    "sortOrder": 2
  }
]
```

---

## 四、前端控件命名规范

### 4.1 控件ID命名规则

```
input_{参数名去掉@}    - 输入控件
label_{参数名去掉@}   - 标签
group_{参数名去掉@}   - 控件分组容器
```

**示例：**

| 参数名 | 控件ID | 说明 |
|--------|--------|------|
| `@StartDate` | `input_StartDate` | 日期输入框 |
| `@StartDate` | `label_StartDate` | 标签 |
| `@StartDate` | `group_StartDate` | 分组容器 |

### 4.2 控件渲染标准

```html
<!-- 日期类型 -->
<div class="param-group" id="group_StartDate">
    <label for="input_StartDate" id="label_StartDate">开始日期</label>
    <input type="date" id="input_StartDate" name="param_StartDate" class="param-input">
</div>

<!-- 选择类型 -->
<div class="param-group" id="group_StatisticsType">
    <label for="input_StatisticsType" id="label_StatisticsType">统计类型</label>
    <select id="input_StatisticsType" name="param_StatisticsType" class="param-input">
        <option value="">请选择</option>
        <option value="报告医生" selected>报告医生</option>
        <option value="审核医生">审核医生</option>
        <option value="技师">技师</option>
    </select>
</div>
```

---

## 五、参数分组标准

### 5.1 分组定义

| 分组代码 | 分组名称 | 说明 |
|----------|----------|------|
| `time` | 时间参数 | 日期、时间相关 |
| `filter` | 筛选参数 | 分类、状态筛选 |
| `system` | 系统参数 | 系统类型、模块 |
| `custom` | 自定义参数 | 业务特定参数 |

### 5.2 分组渲染顺序

```
1. time      - 时间参数（最先显示）
2. system    - 系统参数
3. filter    - 筛选参数
4. custom    - 自定义参数（最后显示）
```

---

## 六、参数校验标准

### 6.1 必填校验

```javascript
// 校验规则
if (param.isRequired && !value) {
    showError(`${param.displayName}不能为空`);
    return false;
}
```

### 6.2 类型校验

| 类型 | 校验规则 |
|------|----------|
| `datetime` | 验证日期格式 YYYY-MM-DD |
| `int` | 验证是否为整数 |
| `decimal` | 验证是否为数字 |
| `select` | 验证是否在选项列表中 |

### 6.3 日期范围校验

```javascript
// 开始日期 <= 结束日期
if (startDate > endDate) {
    showError('开始日期不能大于结束日期');
    return false;
}
```

---

## 七、参数传递标准

### 7.1 请求格式

```json
{
  "procName": "proc_RadiologyWorkload",
  "parameters": {
    "@StartDate": "2026-05-01",
    "@EndDate": "2026-05-31",
    "@StatisticsType": "报告医生"
  }
}
```

### 7.2 参数名处理

```javascript
// 统一添加@前缀
function formatParamName(name) {
    return name.startsWith('@') ? name : '@' + name;
}
```

---

## 八、模块化实现方案

### 8.1 参数配置模块

```javascript
// 参数配置管理器
var ParamManager = {
    // 加载配置
    loadConfig: function(configId) { ... },
    
    // 渲染控件
    renderParams: function(params) { ... },
    
    // 收集参数值
    collectParams: function() { ... },
    
    // 校验参数
    validateParams: function(params) { ... },
    
    // 重置参数
    resetParams: function() { ... }
};
```

### 8.2 控件工厂

```javascript
// 控件渲染工厂
var ControlFactory = {
    datetime: function(param) { /* 渲染日期控件 */ },
    varchar: function(param) { /* 渲染文本控件 */ },
    int: function(param) { /* 渲染数字控件 */ },
    select: function(param) { /* 渲染下拉控件 */ },
    
    create: function(param) {
        var type = param.type.toLowerCase();
        if (this[type]) {
            return this[type](param);
        }
        return this.varchar(param); // 默认文本控件
    }
};
```

---

## 九、扩展说明

### 9.1 添加新参数类型

1. 在 `ControlFactory` 中添加类型处理函数
2. 在 `PARAM_TYPES` 常量中注册类型
3. 更新校验规则

### 9.2 添加新统计功能

1. 在数据库中添加配置记录
2. 创建存储过程
3. 配置参数（可选）
4. 创建模板（可选）

---

## 十、版本记录

| 版本 | 日期 | 变更说明 |
|------|------|----------|
| v1.0 | 2026-05-05 | 初始版本 |
