@echo off

echo === 测试8080端口访问 ===
echo.

:: 检查8080端口是否被监听
netstat -an | findstr :8080
echo.

:: 尝试访问8080端口
curl -s http://localhost:8080/ > test_response.txt

if %errorlevel% equ 0 (
    echo ✅ 访问成功!
    echo 响应内容:
    type test_response.txt | head -20
) else (
    echo ❌ 访问失败!
)

del test_response.txt 2>nul
echo.
echo === 测试完成 ===
pause