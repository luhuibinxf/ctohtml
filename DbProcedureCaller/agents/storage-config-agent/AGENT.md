# 存储配置智能体

## 概述

存储配置智能体是统计分析系统的核心组件，负责管理存储过程的配置信息，支持动态加载配置、参数映射和执行调度。

## 核心功能

### 1. 配置管理
- 获取所有存储过程配置列表
- 获取单个配置详情
- 保存/更新配置信息
- 删除配置

### 2. 存储过程执行
- 动态执行存储过程
- 参数化查询支持
- 结果集返回

### 3. 元数据管理
- 搜索数据库中的存储过程
- 获取存储过程参数元数据

## 数据结构

### 配置表结构

| 字段名 | 类型 | 说明 | 必填 |
|--------|------|------|------|
| id | string | 配置唯一标识 | ✅ |
| name | string | 显示名称 | ✅ |
| icon | string | FontAwesome图标 | ❌ |
| procName | string | 存储过程名称 | ✅ |
| templateName | string | 自定义模板文件名 | ❌ |
| parameters | json | 参数配置数组 | ❌ |
| enabled | bool | 是否启用 | ✅ |
| sortOrder | int | 排序序号 | ❌ |

### 参数配置结构

```json
{
  "name": "@StartDate",
  "displayName": "开始日期",
  "type": "datetime",
  "defaultValue": "",
  "options": "",
  "isRequired": false,
  "isMultiple": false,
  "description": ""
}
```

## API 接口

| 接口 | 方法 | 说明 |
|------|------|------|
| `/get-proc-configs` | GET | 获取所有配置列表 |
| `/get-proc-config?id=xxx` | GET | 获取单个配置 |
| `/save-proc-config` | POST | 保存配置 |
| `/delete-proc-config?id=xxx` | POST | 删除配置 |
| `/execute-stored-proc` | POST | 执行存储过程 |
| `/search-stored-proc` | POST | 搜索存储过程 |
| `/get-proc-metadata` | POST | 获取元数据 |

## 参数类型映射

| 参数类型 | 渲染控件 |
|----------|----------|
| datetime | `<input type="date">` |
| int | `<input type="number">` |
| varchar + options | `<select>` |
| varchar | `<input type="text">` |

## 使用流程

1. **加载阶段**：前端请求 `/get-proc-configs` 获取所有配置
2. **菜单渲染**：根据配置列表渲染左侧菜单
3. **用户点击**：加载对应配置，解析 parameters，动态渲染参数控件
4. **执行查询**：收集参数，POST `/execute-stored-proc`
5. **结果展示**：使用通用表格或自定义模板渲染

## 配置检查清单

- ✅ 参数名一致性：配置中的name必须与存储过程参数名完全一致（带@）
- ✅ 必填项检查：确保required参数有值后再执行
- ✅ 类型匹配：参数值类型需与存储过程定义匹配
- ✅ 模板存在性：如果配置了templateName，确保文件存在
- ✅ SQL注入防护：后端使用参数化查询

## 扩展说明

### 添加新统计项

1. 在数据库中添加配置记录
2. 创建对应的存储过程
3. （可选）创建自定义模板HTML文件
4. 确保参数配置正确

### 自定义模板规范

- 模板文件放在 `templates/` 目录下
- 使用 `{{columnNames}}`、`{{rowCount}}`、`{{data}}` 作为占位符
- 模板应为完整HTML页面，系统会自动提取body内容