# 统计分析系统架构说明

## 设计理念

1. **功能模块分离** - 每个功能独立成文件，方便后期优化和添加功能
2. **配置集中管理** - 所有配置在一个地方，不拆分，防止配置不到位导致问题
3. **统一API调用** - 参数和功能通过统一的API入口调用

## 项目结构

```
DbProcedureCaller/
├── Core/                      # 核心功能
│   ├── DatabaseConnection.cs   # 数据库连接管理（连接池）
│   └── LogHelper.cs           # 日志帮助类
│
├── Config/                    # 配置集中管理
│   └── ConnectionStrings.cs   # 数据库连接字符串配置
│
├── Services/                  # 业务逻辑服务（独立功能模块）
│   ├── UserService.cs         # 用户服务（登录、用户管理）
│   ├── DailyAnalysisService.cs # 每日分析服务（独立功能）
│   ├── StatConfigService.cs   # 统计配置服务
│   └── ProcedureService.cs    # 存储过程服务
│
├── Models/                    # 数据模型
│   └── StatModels.cs          # 统计相关模型
│
└── API/                       # 统一API入口
    └── ApiHandler.cs          # API请求处理器
```

## 核心原则

### 配置集中（不拆分）
- 所有配置项集中在 `Config/` 目录
- `ConnectionStrings.cs` 包含：
  - 数据库连接字符串获取
  - 默认管理员账号密码
  - 默认端口等常量
- 配置文件位置：`config.dat`、`server_config.dat`

### 功能独立（可冗余）
每个服务类完全独立，包含：
- 完整的业务逻辑
- 自己的数据库操作
- 自己的错误处理
- 自己的日志记录

例如 `DailyAnalysisService.cs`：
- 包含所有参数获取逻辑
- 包含SQL查询构建
- 包含结果处理
- 可以单独修改不影响其他模块

### 统一API调用
- 所有HTTP请求通过 `ApiHandler.cs` 统一处理
- 根据URL和HTTP方法分发到对应的服务
- 服务之间不直接调用，通过API层解耦

## 使用示例

### 添加新的统计功能
1. 在 `Services/` 创建新的服务类（如 `NewStatService.cs`）
2. 在服务类中实现完整的业务逻辑
3. 在 `API/ApiHandler.cs` 中添加新的处理方法
4. 不需要修改其他现有代码

### 修改现有功能
1. 找到对应的服务类文件
2. 修改该服务类中的逻辑
3. 重新编译该服务类
4. 其他模块不受影响

### 数据库配置修改
1. 修改 `Config/ConnectionStrings.cs`
2. 或修改 `config.dat` 文件内容
3. 其他代码无需修改

## 编译方式

```bash
# 编译整个项目
dotnet build DbProcedureCaller.csproj

# 编译单个文件（用于快速测试）
dotnet build Services/DailyAnalysisService.cs
```

## 依赖关系

```
ApiHandler (API层)
    ↓ 调用
UserService / DailyAnalysisService (服务层)
    ↓ 调用
DatabaseConnection / LogHelper (核心层)
    ↓ 使用
ConnectionStrings (配置层)
```

这种架构确保：
- 修改一个模块不会影响其他模块
- 可以独立测试每个服务
- 后期添加新功能只需要添加新文件
- 配置集中管理，避免分散导致的不一致问题