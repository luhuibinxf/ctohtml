@echo off
chcp 65001 >nul
echo ===============================
echo       配置中心管理
===============================
echo.

:menu
echo 请选择操作：
echo 1. 获取配置
 echo 2. 设置配置
echo 3. 查看角色列表
echo 4. 查看权限列表
echo 5. 退出
echo.
set /p choice=请输入选项（1-5）：

if "%choice%"=="1" goto getconfig
if "%choice%"=="2" goto setconfig
if "%choice%"=="3" goto getroles
if "%choice%"=="4" goto getpermissions
if "%choice%"=="5" goto exit

echo 无效选项，请重新输入
echo.
goto menu

:getconfig
echo.
echo ===============================
echo         获取配置
===============================
echo.
set /p configkey=请输入配置键名：

powershell -ExecutionPolicy Bypass -File "%~dp0配置中心管理.ps1" -Action GetConfig -ConfigKey "%configkey%"
echo.
pause
goto menu

:setconfig
echo.
echo ===============================
echo         设置配置
===============================
echo.
set /p configkey=请输入配置键名：
set /p configvalue=请输入配置值：
set /p configtype=请输入配置类型：
set /p description=请输入配置描述：

powershell -ExecutionPolicy Bypass -File "%~dp0配置中心管理.ps1" -Action SetConfig -ConfigKey "%configkey%" -ConfigValue "%configvalue%" -ConfigType "%configtype%" -Description "%description%"
echo.
pause
goto menu

:getroles
echo.
echo ===============================
echo        查看角色列表
===============================
echo.

powershell -ExecutionPolicy Bypass -File "%~dp0配置中心管理.ps1" -Action GetRoles
echo.
pause
goto menu

:getpermissions
echo.
echo ===============================
echo        查看权限列表
===============================
echo.

powershell -ExecutionPolicy Bypass -File "%~dp0配置中心管理.ps1" -Action GetPermissions
echo.
pause
goto menu

:exit
echo 谢谢使用，再见！
pause
