
# 统计配置生成工具

## 一、工具概述

本工具用于**自动化生成**统计功能的配置和前端代码，遵循参数类型映射标准。

| 功能 | 说明 |
|------|------|
| 配置生成 | 根据输入生成 proc_configs 配置SQL |
| 前端生成 | 根据参数配置生成HTML控件代码 |
| 模板生成 | 生成标准模板文件 |
| 存储过程模板 | 生成存储过程框架 |

---

## 二、配置生成器

### 2.1 使用方法

```javascript
// 调用配置生成器
var config = ConfigGenerator.generate({
    id: "rad_workload",
    name: "影像中心工作量",
    icon: "fa-solid fa-chart-bar",
    procName: "proc_RadiologyWorkload",
    templateName: "rad_workload.html",
    parameters: [
        { name: "@StartDate", type: "datetime", displayName: "开始日期", isRequired: true },
        { name: "@EndDate", type: "datetime", displayName: "结束日期", isRequired: true },
        { name: "@StatisticsType", type: "select", displayName: "统计类型", options: "报告医生,审核医生,技师" }
    ]
});
```

### 2.2 生成器输出

**1. SQL配置语句：**
```sql
INSERT INTO proc_configs (id, name, icon, procName, templateName, parameters, enabled, sortOrder)
VALUES (
    'rad_workload',
    '影像中心工作量',
    'fa-solid fa-chart-bar',
    'proc_RadiologyWorkload',
    'rad_workload.html',
    '[{"name":"@StartDate","displayName":"开始日期","type":"datetime","isRequired":true},{"name":"@EndDate","displayName":"结束日期","type":"datetime","isRequired":true},{"name":"@StatisticsType","displayName":"统计类型","type":"select","options":"报告医生,审核医生,技师"}]',
    1,
    1
);
```

**2. HTML控件代码：**
```html
<!-- 开始日期 -->
<div class="param-group">
    <label for="input_StartDate">开始日期</label>
    <input type="date" id="input_StartDate" name="param_StartDate" required>
</div>

<!-- 结束日期 -->
<div class="param-group">
    <label for="input_EndDate">结束日期</label>
    <input type="date" id="input_EndDate" name="param_EndDate" required>
</div>

<!-- 统计类型 -->
<div class="param-group">
    <label for="input_StatisticsType">统计类型</label>
    <select id="input_StatisticsType" name="param_StatisticsType">
        <option value="">请选择</option>
        <option value="报告医生">报告医生</option>
        <option value="审核医生">审核医生</option>
        <option value="技师">技师</option>
    </select>
</div>
```

**3. 存储过程模板：**
```sql
CREATE PROCEDURE proc_RadiologyWorkload
    @StartDate DATE,
    @EndDate DATE,
    @StatisticsType VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    
    -- 在此编写查询逻辑
    
END
GO
```

---

## 三、参数类型映射

| 参数类型 | HTML控件 | 生成代码 |
|----------|----------|----------|
| datetime | `<input type="date">` | `<input type="date" id="input_X" name="param_X">` |
| datetime-local | `<input type="datetime-local">` | `<input type="datetime-local" id="input_X" name="param_X">` |
| varchar | `<input type="text">` | `<input type="text" id="input_X" name="param_X">` |
| int | `<input type="number">` | `<input type="number" id="input_X" name="param_X">` |
| decimal | `<input type="number" step="any">` | `<input type="number" step="any" id="input_X" name="param_X">` |
| select | `<select>` | `<select id="input_X" name="param_X"><option>...</option></select>` |

---

## 四、完整配置生成器代码

```javascript
var ConfigGenerator = {
    generate: function(options) {
        var result = {
            sql: this.generateSQL(options),
            html: this.generateHTML(options.parameters),
            procedure: this.generateProcedure(options)
        };
        return result;
    },
    
    generateSQL: function(options) {
        var paramsJson = JSON.stringify(options.parameters || []).replace(/'/g, "''");
        return `INSERT INTO proc_configs (id, name, icon, procName, templateName, parameters, enabled, sortOrder)
VALUES (
    '${options.id}',
    '${options.name}',
    '${options.icon || ''}',
    '${options.procName}',
    '${options.templateName || ''}',
    '${paramsJson}',
    1,
    ${options.sortOrder || 1}
);`;
    },
    
    generateHTML: function(parameters) {
        var html = '';
        parameters.forEach(function(param) {
            var paramName = param.name.replace('@', '');
            var required = param.isRequired ? ' required' : '';
            
            switch(param.type.toLowerCase()) {
                case 'datetime':
                    html += this.generateDateTimeInput(param, paramName, required);
                    break;
                case 'datetime-local':
                    html += this.generateDateTimeLocalInput(param, paramName, required);
                    break;
                case 'int':
                    html += this.generateNumberInput(param, paramName, required);
                    break;
                case 'decimal':
                    html += this.generateDecimalInput(param, paramName, required);
                    break;
                case 'select':
                    html += this.generateSelect(param, paramName, required);
                    break;
                default:
                    html += this.generateTextInput(param, paramName, required);
            }
        }, this);
        return html;
    },
    
    generateDateTimeInput: function(param, paramName, required) {
        return `<div class="param-group">
    <label for="input_${paramName}">${param.displayName}</label>
    <input type="date" id="input_${paramName}" name="param_${paramName}"${required} value="${param.defaultValue || ''}">
</div>\n`;
    },
    
    generateDateTimeLocalInput: function(param, paramName, required) {
        return `<div class="param-group">
    <label for="input_${paramName}">${param.displayName}</label>
    <input type="datetime-local" id="input_${paramName}" name="param_${paramName}"${required} value="${param.defaultValue || ''}">
</div>\n`;
    },
    
    generateNumberInput: function(param, paramName, required) {
        return `<div class="param-group">
    <label for="input_${paramName}">${param.displayName}</label>
    <input type="number" id="input_${paramName}" name="param_${paramName}"${required} value="${param.defaultValue || ''}">
</div>\n`;
    },
    
    generateDecimalInput: function(param, paramName, required) {
        return `<div class="param-group">
    <label for="input_${paramName}">${param.displayName}</label>
    <input type="number" step="any" id="input_${paramName}" name="param_${paramName}"${required} value="${param.defaultValue || ''}">
</div>\n`;
    },
    
    generateSelect: function(param, paramName, required) {
        var options = param.options ? param.options.split(',').map(o => o.trim()) : [];
        var optionsHtml = '<option value="">请选择</option>';
        options.forEach(function(opt) {
            var optParts = opt.split(':');
            var optValue = optParts.length > 1 ? optParts[0].trim() : opt;
            var optLabel = optParts.length > 1 ? optParts[1].trim() : opt;
            var selected = param.defaultValue === optValue ? ' selected' : '';
            optionsHtml += `<option value="${optValue}"${selected}>${optLabel}</option>`;
        });
        
        return `<div class="param-group">
    <label for="input_${paramName}">${param.displayName}</label>
    <select id="input_${paramName}" name="param_${paramName}"${required}>
        ${optionsHtml}
    </select>
</div>\n`;
    },
    
    generateTextInput: function(param, paramName, required) {
        return `<div class="param-group">
    <label for="input_${paramName}">${param.displayName}</label>
    <input type="text" id="input_${paramName}" name="param_${paramName}"${required} placeholder="${param.placeholder || ''}" value="${param.defaultValue || ''}">
</div>\n`;
    },
    
    generateProcedure: function(options) {
        var params = options.parameters || [];
        var paramsSQL = params.map(function(p) {
            var type = this.getSQLType(p.type);
            return `    ${p.name} ${type}`;
        }, this).join(',\n');
        
        return `CREATE PROCEDURE ${options.procName}
${paramsSQL}
AS
BEGIN
    SET NOCOUNT ON;
    
    -- 查询逻辑
    
END
GO`;
    },
    
    getSQLType: function(type) {
        switch(type.toLowerCase()) {
            case 'datetime':
            case 'datetime-local':
                return 'DATETIME';
            case 'int':
                return 'INT';
            case 'decimal':
                return 'DECIMAL(18,2)';
            case 'select':
            default:
                return 'VARCHAR(100)';
        }
    }
};
```

---

## 五、使用示例

### 示例1：生成影像中心工作量配置

```javascript
var result = ConfigGenerator.generate({
    id: "rad_workload",
    name: "影像中心工作量",
    icon: "fa-solid fa-chart-bar",
    procName: "proc_RadiologyWorkload",
    templateName: "rad_workload.html",
    parameters: [
        { name: "@StartDate", type: "datetime", displayName: "开始日期", isRequired: true },
        { name: "@EndDate", type: "datetime", displayName: "结束日期", isRequired: true },
        { name: "@StatisticsType", type: "select", displayName: "统计类型", options: "报告医生:报告医生,审核医生:审核医生,技师:技师", defaultValue: "报告医生" }
    ]
});

console.log(result.sql);      // SQL配置语句
console.log(result.html);     // HTML控件代码
console.log(result.procedure); // 存储过程模板
```

### 示例2：生成简单统计配置

```javascript
var result = ConfigGenerator.generate({
    id: "daily_count",
    name: "每日检查统计",
    procName: "proc_DailyCount",
    parameters: [
        { name: "@ReportDate", type: "datetime", displayName: "统计日期", isRequired: true },
        { name: "@SystemType", type: "select", displayName: "系统类型", options: "全部,放射,超声,内镜" }
    ]
});
```

---

## 六、输出格式说明

### SQL配置语句
直接复制到数据库执行即可完成配置

### HTML控件代码
复制到模板文件或参数区域即可使用

### 存储过程模板
复制到数据库中，补充查询逻辑即可

---

## 七、版本记录

| 版本 | 日期 | 变更说明 |
|------|------|----------|
| v1.0 | 2026-05-05 | 初始版本 |
