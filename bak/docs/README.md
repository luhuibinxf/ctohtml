# 数据库存储过程调用工具

这是一个用于连接数据库、调用存储过程并使用HTML界面展示结果的工具。

## 功能特点
- 连接SQL Server数据库
- 调用存储过程并传递参数
- 使用HTML界面展示执行结果
- 支持自定义数据库连接配置

## 使用方法

### 1. 配置数据库连接

首次运行程序时，会自动生成 `config.ini` 配置文件。请使用文本编辑器打开该文件，修改以下配置：

```ini
[Database]
driver={SQL Server}
server=localhost  # 数据库服务器地址
database=YourDatabase  # 数据库名称
uid=username  # 用户名
pwd=password  # 密码
```

### 2. 运行程序

双击 `db_procedure_caller.exe` 可执行文件，程序会在后台启动一个本地服务器。

### 3. 访问界面

打开浏览器，访问 `http://localhost:5000` 即可看到操作界面。

### 4. 调用存储过程

在界面中：
1. 输入存储过程名称
2. 输入参数（多个参数用逗号分隔）
3. 点击"执行"按钮
4. 查看执行结果

## 技术说明

- 基于 Python + Flask 开发
- 使用 pyodbc 连接数据库
- 使用 PyInstaller 打包为可执行文件
- 界面使用 Bootstrap + jQuery

## 注意事项

- 程序需要在安装了 SQL Server 驱动的环境中运行
- 首次运行时会在当前目录生成配置文件
- 程序运行时会占用 5000 端口，请确保该端口未被其他程序占用
