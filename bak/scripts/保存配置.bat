@echo off
chcp 65001 > nul
echo ========================================
echo        数据库配置保存工具
echo ========================================
echo.

set /p server=服务器地址 (默认localhost):
if "%server%"=="" set server=localhost

set /p database=数据库名 (默认WiNEX_PACS):
if "%database%"=="" set database=WiNEX_PACS

echo.
echo 认证方式:
echo   1. Windows集成认证
echo   2. SQL Server认证
set /p authtype=请选择 (1或2):

if "%authtype%"=="1" (
    powershell -ExecutionPolicy Bypass -File "%~dp0保存配置.ps1" -Server "%server%" -Database "%database%" -UseWindowsAuth
) else (
    set /p sqluser=请输入SQL用户名:
    set /p sqlpass=请输入SQL密码:
    powershell -ExecutionPolicy Bypass -File "%~dp0保存配置.ps1" -Server "%server%" -Database "%database%" -Username "%sqluser%" -Password "%sqlpass%"
)

echo.
pause