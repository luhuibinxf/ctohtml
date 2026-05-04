
# 统计分析系统 - 技术文档

---

## 目录

1. [需求文档](#一需求文档)
   - 1.1 业务需求概述
   - 1.2 映射关系配置
2. [技术文档](#二技术文档)
   - 2.1 系统架构
   - 2.2 核心文件结构
   - 2.3 数据库表结构
   - 2.4 API接口说明
3. [优化方案](#三优化方案)
   - 3.1 配置化方案概述
   - 3.2 数据库表设计
   - 3.3 配置化API设计
   - 3.4 前端动态渲染方案
   - 3.5 实施计划
4. [方案路径与技术路径](#四方案路径与技术路径)
   - 4.1 方案路径
   - 4.2 技术路径
5. [风险评估与资源需求](#五风险评估与资源需求)

---

## 一、需求文档

### 1.1 业务需求概述

| 序号 | 需求名称 | 需求描述 | 优先级 | 状态 |
|:---:|---------|---------|:---:|:---:|
| 1 | 每日分析功能 | 验证每日分析功能能否正常显示数据 | 高 | ✅ |
| 2 | 真实数据展示 | 使用真实数据库数据，不要模拟数据 | 高 | ✅ |
| 3 | 端口配置 | 默认端口9094 | 高 | ✅ |
| 4 | 病人类型映射 | 138138=门诊, 138139=急诊, 138140=体检, 145235=住院 | 高 | ✅ |
| 5 | 阴阳性映射 | 383927=阳性, 383926=阴性 | 高 | ✅ |
| 6 | 系统映射 | UIS=超声, RIS=放射, EIS=内镜, PIS=病理, NMS=核医学 | 高 | ✅ |
| 7 | 前端排序 | 添加表头拖动功能和前端排序（不调用API） | 中 | ✅ |
| 8 | 快捷查询 | 快捷访问按钮（今天、昨天、本周、本月、上月） | 中 | ✅ |
| 9 | 统计按钮 | 科室统计、医生统计、检查类型统计按钮 | 中 | ✅ |
| 10 | 阳性率阈值 | 超声/内镜系统>=50%，放射系统>=60% | 中 | ✅ |
| 11 | 导航栏布局 | 面包屑式布局（返回 > 统计分析 > 每日分析） | 中 | ✅ |
| 12 | 参数联动 | 根据系统筛选联动显示对应人员和科室 | 中 | ✅ |
| 13 | 参数搜索 | 下拉框支持搜索和首字母筛选 | 中 | ✅ |
| 14 | 配置化方案 | 参数和SQL语句存储到数据库，前端动态渲染 | 高 | 待实现 |

### 1.2 映射关系配置

#### 1.2.1 病人类型映射

| 编码 | 显示值 | 说明 |
|:---:|-------|-----|

| 138138 | 门诊 | 门诊病人 |
| 138139 | 急诊 | 急诊病人 |
| 138140 | 体检 | 体检病人 |
| 145235 | 住院 | 住院病人 |

#### 1.2.2 阴阳性映射

| 编码 | 显示值 | 说明 |
|:---:|-------|-----|
| 383927 | 阳性 | 阳性结果 |
| 383926 | 阴性 | 阴性结果 |

#### 1.2.3 系统映射

| 编码 | 显示值 | 说明 |
|:---:|-------|-----|
| UIS | 超声 | 超声系统 |
| RIS | 放射 | 放射系统 |
| EIS | 内镜 | 内镜系统 |
| PIS | 病理 | 病理系统 |
| NMS | 核医学 | 核医学系统 |

#### 1.2.4 阳性率阈值配置

| 系统 | 阳性率阈值 | 说明 |
|:---:|:---:|-----|
| 超声（UIS） | >= 50% | 显示为高 |
| 内镜（EIS） | >= 50% | 显示为高 |
| 放射（RIS） | >= 60% | 显示为高 |
| 其他系统 | >= 50% | 默认阈值 |

---

## 二、技术文档

### 2.1 系统架构

```
┌─────────────────────────────────────────────────────────────────┐
│                        前端界面层                              │
│  ┌─────────────┐  ┌───────────────┐  ┌───────────────────┐   │
│  │  登录页面   │  │   主菜单页面  │  │ 每日分析页面       │   │
│  └──────┬──────┘  └───────┬───────┘  └─────────┬─────────┘   │
└─────────┼─────────────────┼─────────────────────┼─────────────┘
          │                 │                     │
          ▼                 ▼                     ▼
┌─────────────────────────────────────────────────────────────────┐
│                        API层 (HTTP Listener)                   │
│  ┌─────────────────────────────────────────────────────────┐   │
│  │ /api/analysis          - 获取分析数据                   │   │
│  │ /api/options           - 获取下拉框选项                  │   │
│  │ /api/department-statistics - 科室统计                  │   │
│  │ /api/doctor-statistics     - 医生统计                  │   │
│  │ /api/category-statistics   - 检查类型统计              │   │
│  └─────────────────────────────────────────────────────────┘   │
└─────────────────────────────────────────────────────────────────┘
          │
          ▼
┌─────────────────────────────────────────────────────────────────┐
│                        业务逻辑层                              │
│  ┌─────────────────────────────────────────────────────────┐   │
│  │ DailyAnalysisService.cs                                  │   │
│  │   - GetAnalysisData()      获取分析数据                  │   │
│  │   - GetAllOptions()        获取所有选项                  │   │
│  │   - GetDepartmentStatistics() 科室统计                  │   │
│  │   - GetDoctorStatistics()     医生统计                  │   │
│  │   - GetCategoryStatistics()   检查类型统计              │   │
│  └─────────────────────────────────────────────────────────┘   │
└─────────────────────────────────────────────────────────────────┘
          │
          ▼
┌─────────────────────────────────────────────────────────────────┐
│                        数据访问层                              │
│  ┌─────────────────────────────────────────────────────────┐   │
│  │ DatabaseConnection.cs   - 数据库连接管理                │   │
│  │ ConnectionStrings.cs    - 连接字符串配置                │   │
│  └─────────────────────────────────────────────────────────┘   │
└─────────────────────────────────────────────────────────────────┘
          │
          ▼
┌─────────────────────────────────────────────────────────────────┐
│                        数据库层 (SQL Server)                   │
│  ┌──────────────┐  ┌──────────────┐  ┌──────────────────┐   │
│  │   EXAM_TASK  │  │ EXAM_REPORT  │  │  Pacs_SysDict    │   │
│  │ 检查任务表   │  │ 检查报告表   │  │  系统字典表      │   │
│  └──────────────┘  └──────────────┘  └──────────────────┘   │
└─────────────────────────────────────────────────────────────────┘
```

### 2.2 核心文件结构

```
d:\AI\tran\
├── DbProcedureCaller/
│   ├── API/
│   │   └── ApiHandler.cs          # API请求处理
│   ├── Config/
│   │   └── ConnectionStrings.cs   # 数据库连接配置
│   ├── Services/
│   │   └── DailyAnalysisService.cs # 每日分析业务逻辑
│   ├── templates/
│   │   ├── index.html             # 主页面
│   │   └── js/
│   │       └── dailyAnalysis.js   # 每日分析前端逻辑
│   └── DbProcedureCaller.csproj   # 项目配置
├── bak/                           # 备份文件
├── WiNEX_PACS_Schema.md           # 数据库Schema文档
└── sql_query.txt                  # SQL查询语句备份
```

### 2.3 数据库表结构

#### 2.3.1 EXAM_TASK（检查任务表）

| 字段名 | 类型 | 说明 |
|-------|------|-----|
| EXAM_TASK_ID | VARCHAR(50) | 任务ID（主键） |
| SYSTEM_SOURCE_NO | VARCHAR(20) | 系统编码 |
| EXEC_DEPT_NAME | VARCHAR(100) | 执行科室 |
| EXAM_CATEGORY_NAME | VARCHAR(100) | 检查类型 |
| ENCOUNTER_TYPE_NO | VARCHAR(20) | 病人类型编码 |
| TECHNICIAN_NAME | VARCHAR(50) | 技师姓名 |
| CREATED_AT | DATETIME | 创建时间 |
| IS_DEL | INT | 删除标记 |

#### 2.3.2 EXAM_REPORT（检查报告表）

| 字段名 | 类型 | 说明 |
|-------|------|-----|
| EXAM_REPORT_ID | VARCHAR(50) | 报告ID（主键） |
| EXAM_TASK_ID | VARCHAR(50) | 关联任务ID |
| REPORTER_NAME | VARCHAR(50) | 报告医生 |
| REVIEWER_NAME | VARCHAR(50) | 审核医生 |
| NEG_POS_CODE | VARCHAR(20) | 阴阳性编码 |

### 2.4 API接口说明

#### 2.4.1 获取分析数据

**接口**: `POST /api/analysis`

**请求参数**:

| 参数名 | 类型 | 必填 | 说明 |
|-------|------|:---:|-----|
| startDate | string | 是 | 开始日期（YYYY-MM-DD） |
| endDate | string | 是 | 结束日期（YYYY-MM-DD） |
| system | string | 否 | 系统编码 |
| reporter | string | 否 | 报告医生 |
| reviewer | string | 否 | 审核医生 |
| technician | string | 否 | 技师 |
| department | string | 否 | 执行科室 |
| category | string | 否 | 检查类型 |
| patientType | string | 否 | 病人类型 |
| resultStatus | string | 否 | 结果状态 |

**响应示例**:

```json
{
  "success": true,
  "data": [
    {
      "系统": "超声",
      "报告医生": "吴小华",
      "审核医生": "李发仁",
      "技师": "陈俊华",
      "执行科室": "超声医学科-（总）",
      "检查类型": "体检彩超",
      "病人类型": "门诊",
      "结果状态": "阳性",
      "任务数量": 150,
      "阳性数量": 45,
      "阴性数量": 105,
      "阳性率": 30.00
    }
  ],
  "total": 1000
}
```

#### 2.4.2 获取下拉框选项

**接口**: `GET /api/options?system={system}`

**请求参数**:

| 参数名 | 类型 | 必填 | 说明 |
|-------|------|:---:|-----|
| system | string | 否 | 系统编码（用于联动筛选） |

**响应示例**:

```json
{
  "success": true,
  "data": {
    "systems": ["超声", "放射", "内镜", "病理", "核医学"],
    "reporters": ["吴小华", "李发仁", "陈俊华"],
    "reviewers": ["吴小华", "李发仁", "陈俊华"],
    "technicians": ["吴小华", "李发仁", "陈俊华"],
    "departments": ["超声医学科-（总）", "医学影像科-（总）"],
    "categories": ["体检彩超", "CT", "彩超"],
    "patientTypes": ["门诊", "急诊", "体检", "住院"],
    "resultStatuses": ["阳性", "阴性"]
  }
}
```

---

## 三、优化方案

### 3.1 配置化方案概述

**目标**: 将参数配置和SQL语句存储到数据库，实现全员可配置，无需修改代码。

### 3.2 数据库表设计

#### 3.2.1 tjfx_QUERY_CONFIG（查询配置表）

| 字段名 | 类型 | 说明 |
|-------|------|-----|
| ID | INT | 主键（自增） |
| QUERY_NAME | NVARCHAR(100) | 查询名称 |
| QUERY_TYPE | NVARCHAR(50) | 查询类型（ANALYSIS/STATISTICS） |
| QUERY_SQL | NVARCHAR(MAX) | SQL语句模板 |
| PARAMS_MAPPING | NVARCHAR(MAX) | 参数映射JSON |
| IS_ACTIVE | BIT | 是否启用 |
| DESCRIPTION | NVARCHAR(255) | 描述 |
| CREATED_BY | NVARCHAR(50) | 创建人 |
| CREATED_TIME | DATETIME | 创建时间 |
| UPDATED_BY | NVARCHAR(50) | 更新人 |
| UPDATED_TIME | DATETIME | 更新时间 |

#### 3.2.2 tjfx_QUERY_PARAM（参数配置表）

| 字段名 | 类型 | 说明 |
|-------|------|-----|
| ID | INT | 主键（自增） |
| QUERY_ID | INT | 关联查询ID |
| PARAM_NAME | NVARCHAR(50) | 参数名 |
| DISPLAY_NAME | NVARCHAR(100) | 显示名称 |
| PARAM_TYPE | NVARCHAR(20) | 参数类型（date/select/text） |
| IS_REQUIRED | BIT | 是否必填 |
| DEFAULT_VALUE | NVARCHAR(200) | 默认值 |
| QUERY_SQL | NVARCHAR(MAX) | 选项查询SQL |
| SORT_ORDER | INT | 排序顺序 |

#### 3.2.3 tjfx_CODE_MAPPING（编码映射表）

| 字段名 | 类型 | 说明 |
|-------|------|-----|
| ID | INT | 主键（自增） |
| CATEGORY | NVARCHAR(50) | 映射类别（SYSTEM/PATIENT_TYPE/RESULT_STATUS） |
| SOURCE_VALUE | NVARCHAR(100) | 原始编码 |
| TARGET_VALUE | NVARCHAR(100) | 映射后显示值 |
| SORT_ORDER | INT | 排序顺序 |
| IS_ACTIVE | BIT | 是否启用 |
| DESCRIPTION | NVARCHAR(255) | 描述 |

### 3.3 配置化API设计

#### 3.3.1 获取查询配置

**接口**: `GET /api/config/queries/{queryId}`

**响应示例**:

```json
{
  "success": true,
  "data": {
    "queryId": 1,
    "queryName": "每日分析",
    "queryType": "ANALYSIS",
    "querySql": "SELECT ... FROM EXAM_TASK t INNER JOIN EXAM_REPORT r ON ... WHERE {WHERE_CLAUSE} GROUP BY ...",
    "parameters": [
      {
        "paramName": "startDate",
        "displayName": "开始日期",
        "paramType": "date",
        "isRequired": true,
        "defaultValue": null
      },
      {
        "paramName": "system",
        "displayName": "系统",
        "paramType": "select",
        "isRequired": false,
        "querySql": "SELECT DISTINCT SYSTEM_SOURCE_NO AS value, SYSTEM_SOURCE_NO AS label FROM EXAM_TASK"
      }
    ]
  }
}
```

#### 3.3.2 执行配置化查询

**接口**: `POST /api/query/execute/{queryId}`

**请求参数**:

```json
{
  "parameters": {
    "startDate": "2026-01-01",
    "endDate": "2026-01-31",
    "system": "UIS"
  }
}
```

#### 3.3.3 管理编码映射

**接口**: `GET/POST/PUT/DELETE /api/config/mappings`

**请求/响应示例**:

```json
{
  "category": "PATIENT_TYPE",
  "sourceValue": "138138",
  "targetValue": "门诊",
  "description": "门诊病人"
}
```

### 3.4 前端动态渲染方案

```javascript
// 获取查询配置
fetch('/api/config/queries/1')
    .then(res => res.json())
    .then(config => {
        const formContainer = document.getElementById('queryForm');
        
        // 动态渲染参数表单
        config.parameters.forEach(param => {
            const fieldGroup = document.createElement('div');
            fieldGroup.className = 'form-group';
            
            const label = document.createElement('label');
            label.textContent = param.displayName + (param.isRequired ? '*' : '');
            fieldGroup.appendChild(label);
            
            if (param.paramType === 'date') {
                const input = document.createElement('input');
                input.type = 'date';
                input.name = param.paramName;
                input.required = param.isRequired;
                fieldGroup.appendChild(input);
            } else if (param.paramType === 'select') {
                const select = document.createElement('select');
                select.name = param.paramName;
                
                // 加载选项
                fetch('/api/query/options', {
                    method: 'POST',
                    body: JSON.stringify({ querySql: param.querySql })
                })
                .then(res => res.json())
                .then(options => {
                    options.forEach(opt => {
                        const option = document.createElement('option');
                        option.value = opt.value;
                        option.textContent = opt.label;
                        select.appendChild(option);
                    });
                });
                
                fieldGroup.appendChild(select);
            }
            
            formContainer.appendChild(fieldGroup);
        });
    });
```

### 3.5 实施计划

| 阶段 | 任务 | 预计时间 | 负责人 |
|:---:|-----|:---:|-----|
| 1 | 创建配置表（tjfx_QUERY_CONFIG, tjfx_QUERY_PARAM, tjfx_CODE_MAPPING） | 1天 | 开发 |
| 2 | 初始化配置数据（默认查询配置、参数配置、映射配置） | 1天 | 开发 |
| 3 | 修改后端API支持配置化查询 | 2天 | 开发 |
| 4 | 修改前端支持动态渲染参数表单 | 2天 | 开发 |
| 5 | 添加配置管理界面（管理映射和查询配置） | 2天 | 开发 |
| 6 | 测试和验证 | 1天 | 测试 |

---

## 四、方案路径与技术路径

### 4.1 方案路径

#### 阶段0：现状评估与需求确认

| 任务 | 描述 | 输出 |
|-----|-----|-----|
| 1.1 | 评估当前代码结构 | 代码审计报告 |
| 1.2 | 确认需求优先级 | 需求优先级矩阵 |
| 1.3 | 确定配置化范围 | 配置化需求清单 |

#### 阶段1：数据库配置表设计与创建

| 任务 | 描述 | 输出 |
|-----|-----|-----|
| 2.1 | 设计配置表结构 | 数据库设计文档 |
| 2.2 | 创建配置表 | SQL脚本 |
| 2.3 | 初始化配置数据 | 初始数据脚本 |

#### 阶段2：后端API重构

| 任务 | 描述 | 输出 |
|-----|-----|-----|
| 3.1 | 创建配置管理服务 | ConfigService.cs |
| 3.2 | 实现通用查询执行API | 配置化查询接口 |
| 3.3 | 实现编码映射服务 | 动态映射接口 |
| 3.4 | 重构现有业务逻辑 | DailyAnalysisService优化 |

#### 阶段3：前端动态渲染实现

| 任务 | 描述 | 输出 |
|-----|-----|-----|
| 4.1 | 实现参数配置加载 | 动态表单组件 |
| 4.2 | 实现动态表单渲染 | 通用表单组件 |
| 4.3 | 实现结果表格渲染 | 通用表格组件 |
| 4.4 | 添加配置管理界面 | 配置管理页面 |

#### 阶段4：测试与验证

| 任务 | 描述 | 输出 |
|-----|-----|-----|
| 5.1 | 单元测试 | 测试用例覆盖 |
| 5.2 | 集成测试 | API测试 |
| 5.3 | 用户验收测试 | UAT报告 |
| 5.4 | 性能测试 | 性能报告 |

#### 阶段5：部署与上线

| 任务 | 描述 | 输出 |
|-----|-----|-----|
| 6.1 | 数据库迁移 | 数据迁移脚本 |
| 6.2 | 代码部署 | 部署包 |
| 6.3 | 用户培训 | 操作手册 |
| 6.4 | 上线监控 | 监控告警配置 |

### 4.2 技术路径

#### 4.2.1 数据库层技术方案

**索引设计**:

```sql
-- 查询配置表索引
CREATE UNIQUE INDEX UX_QUERY_NAME ON tjfx_QUERY_CONFIG (QUERY_NAME);
CREATE INDEX IX_QUERY_TYPE ON tjfx_QUERY_CONFIG (QUERY_TYPE);
CREATE INDEX IX_QUERY_ACTIVE ON tjfx_QUERY_CONFIG (IS_ACTIVE);

-- 参数配置表索引
CREATE INDEX IX_PARAM_QUERY_ID ON tjfx_QUERY_PARAM (QUERY_ID);
CREATE INDEX IX_PARAM_NAME ON tjfx_QUERY_PARAM (PARAM_NAME);

-- 编码映射表索引
CREATE UNIQUE INDEX UX_MAPPING_CATEGORY ON tjfx_CODE_MAPPING (CATEGORY, SOURCE_VALUE);
CREATE INDEX IX_MAPPING_CATEGORY ON tjfx_CODE_MAPPING (CATEGORY);
CREATE INDEX IX_MAPPING_ACTIVE ON tjfx_CODE_MAPPING (IS_ACTIVE);
```

#### 4.2.2 后端技术方案

**服务架构**:

```
┌─────────────────────────────────────────────────────────────┐
│                    Service Layer                            │
├─────────────────────────────────────────────────────────────┤
│  ConfigService          QueryService          MappingService│
│  ├─ GetQueryConfig()    ├─ ExecuteQuery()     ├─ GetMapping()│
│  ├─ SaveQueryConfig()   ├─ GetParams()        ├─ SaveMapping()│
│  ├─ DeleteQueryConfig() └─ ValidateParams()   └─ DeleteMapping()│
│  └─ GetAllQueries()                                        │
└─────────────────────────────────────────────────────────────┘
                            │
                            ▼
┌─────────────────────────────────────────────────────────────┐
│                    Data Access Layer                        │
│  SqlHelper              ConnectionManager                   │
│  ├─ ExecuteQuery()      ├─ GetConnection()                 │
│  ├─ ExecuteNonQuery()   └─ ReleaseConnection()             │
│  └─ GetDataTable()                                         │
└─────────────────────────────────────────────────────────────┘
```

**API接口列表**:

| 接口 | 方法 | 描述 |
|-----|------|-----|
| /api/config/queries | GET | 获取所有查询配置 |
| /api/config/queries/{id} | GET | 获取单个查询配置 |
| /api/config/queries | POST | 保存查询配置 |
| /api/config/queries/{id} | PUT | 更新查询配置 |
| /api/config/queries/{id} | DELETE | 删除查询配置 |
| /api/config/params | GET | 获取参数配置 |
| /api/config/params | POST | 保存参数配置 |
| /api/config/mappings | GET | 获取编码映射 |
| /api/config/mappings | POST | 保存编码映射 |
| /api/query/execute/{queryId} | POST | 执行配置化查询 |
| /api/query/options | POST | 获取动态选项 |

---

## 五、风险评估与资源需求

### 5.1 风险评估

| 风险 | 概率 | 影响 | 应对措施 |
|-----|:---:|:---:|---------|
| SQL注入攻击 | 中 | 高 | 使用参数化查询，禁止动态拼接 |
| 配置错误导致系统崩溃 | 低 | 高 | 配置验证，备份机制 |
| 性能下降 | 中 | 中 | 缓存机制，索引优化 |
| 数据迁移失败 | 低 | 高 | 备份数据，回滚方案 |
| 用户操作错误 | 高 | 低 | 权限控制，操作确认 |

### 5.2 资源需求

| 资源类型 | 数量 | 说明 |
|---------|:---:|-----|
| 开发人员 | 2人 | C#后端 + JavaScript前端 |
| 测试人员 | 1人 | 功能测试、性能测试 |
| 数据库管理员 | 1人 | 数据库设计、迁移 |
| 服务器 | 1台 | 测试/生产环境 |
| 时间 | 2周 | 完整实施周期 |

### 5.3 预期收益

| 指标 | 优化前 | 优化后 | 提升 |
|-----|-------|-------|-----|
| 映射修改效率 | 需要改代码、重新部署 | 界面配置，即时生效 | 效率提升10倍+ |
| 新增查询功能 | 需要开发人员参与 | 管理员配置即可 | 无需开发介入 |
| 维护成本 | 高（依赖技术人员） | 低（管理员操作） | 降低80% |
| 系统灵活性 | 低（硬编码） | 高（配置化） | 灵活性提升 |
| 响应速度 | 中等 | 快（缓存机制） | 性能提升 |

---

## 文档信息

| 项目 | 内容 |
|-----|-----|
| 文档版本 | v1.0 |
| 创建日期 | 2026-05-01 |
| 最后更新 | 2026-05-01 |
| 作者 | 统计分析系统开发团队 |
| 项目地址 | https://github.com/luhuibinxf/stat-analysis-system |

---
