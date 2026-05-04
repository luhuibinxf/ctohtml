# 统计分析系统 - 版本变更记录

## 版本 1.1.0 (2026-05-02)

### 界面优化

1. **导航栏优化**
   - 移除了主界面的返回按钮
   - 功能页面显示返回按钮在左侧
   - 移除了页面标题区域（面包屑导航）
   - 用户信息和退出按钮显示在右侧

2. **全屏显示优化**
   - 所有页面全屏显示
   - 导航栏固定在顶部
   - 内容区域自动延伸到底部

3. **样式文件分离**
   - 创建了独立的 `css/common.css` 样式文件
   - 样式与功能代码彻底分离
   - 修改样式不会影响功能代码

4. **功能页面模块化**
   - `pages/user-config.html` - 用户配置页面
   - `pages/permission-config.html` - 权限配置页面
   - `pages/server-config.html` - 服务器配置页面
   - `pages/complex-config.html` - 个性化统计设置页面

### Bug修复

1. **数据处理错误修复**
   - 修复了 `Cannot read properties of undefined (reading 'label')` 错误
   - 修复了 `Cannot read properties of undefined (reading 'formatter')` 错误
   - 在所有访问对象属性之前添加了空值检查

2. **登录功能优化**
   - 添加了详细的日志记录
   - 优化了错误处理逻辑
   - 添加了登录异常捕获

### 技术改进

1. **代码架构优化**
   - 将内联样式移至外部 CSS 文件
   - 实现了模块化的页面加载机制
   - 使用 `loadPageContent()` 函数统一加载页面

2. **性能优化**
   - 静态资源添加版本号避免缓存问题
   - JavaScript 版本: `?v=1.0.11`
   - CSS 版本: `?v=1.0.0`

### 默认配置

- 默认管理员用户名: `lhbdb`
- 默认管理员密码: `241023`
- 默认服务端口: `8081`

### 文件结构

```
templates/
├── index.html                    # 主入口页面
├── css/
│   └── common.css                # 统一样式文件
├── js/
│   └── dailyAnalysis.js          # 业务逻辑JS
└── pages/
    ├── user-config.html          # 用户配置页面
    ├── permission-config.html    # 权限配置页面
    ├── server-config.html        # 服务器配置页面
    └── complex-config.html       # 个性化统计设置页面
```

---

## 版本 1.0.0 (2026-05-01)

### 初始版本

- 统计分析系统基础框架
- 用户登录功能
- 用户管理功能
- 权限配置功能
- 服务器配置功能
- 每日分析功能
- 数据库连接测试功能
