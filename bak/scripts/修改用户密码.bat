@echo off
chcp 65001 > nul
echo ========================================
echo        用户密码修改工具
echo ========================================
echo.

set /p username=请输入用户名:
set /p password=请输入新密码:

powershell -ExecutionPolicy Bypass -File "%~dp0修改用户密码.ps1" -Username "%username%" -NewPassword "%password%"

echo.
pause