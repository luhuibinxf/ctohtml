@echo off
echo === 完整测试开始 ===

REM 1. 清理旧进程
echo.
echo [1] 清理旧进程...
taskkill /F /IM DbProcedureCaller.exe 2>NUL

REM 2. 启动程序
echo.
echo [2] 启动程序...
start /B "DbProcedureCaller" "d:\AI\tran\DbProcedureCaller.exe"

REM 3. 等待30秒
echo.
echo [3] 等待程序初始化 (30秒)...
for /L %%i in (1,1,30) do (
    echo %%i 
    timeout /T 1 /NOBREAK >NUL
)
echo.

REM 4. 测试8080端口
echo [4] 测试8080端口...
try {
    curl http://localhost:8080/
} catch {
    echo 访问失败
}

REM 5. 检查端口
echo.
echo [5] 检查8080端口监听...
netstat -an | findstr 8080

REM 6. 清理
echo.
echo [6] 清理...
taskkill /F /IM DbProcedureCaller.exe 2>NUL
echo 已关闭程序

echo.
echo === 测试完成 ===
pause