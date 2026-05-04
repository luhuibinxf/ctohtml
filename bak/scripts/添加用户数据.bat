@echo off
chcp 65001 >nul
echo ===============================
echo     添加/修改用户数据
===============================
echo.

:menu
echo 请选择操作：
echo 1. 添加新用户
echo 2. 修改现有用户
echo 3. 退出
echo.
set /p choice=请输入选项（1-3）：

if "%choice%"=="1" goto add
if "%choice%"=="2" goto update
if "%choice%"=="3" goto exit

echo 无效选项，请重新输入
echo.
goto menu

:add
echo.
echo ===============================
echo       添加新用户
===============================
echo.
set /p userid=请输入用户ID（数字）：
set /p username=请输入用户名：
set /p password=请输入密码：

rem 验证用户ID是否为数字
echo %userid%| findstr /r "^[0-9]*$" >nul
if errorlevel 1 (
    echo 错误：用户ID必须是数字类型
    pause
    goto add
)

powershell -ExecutionPolicy Bypass -File "%~dp0添加用户数据.ps1" -UserId "%userid%" -Username "%username%" -Password "%password%" -Action Add
echo.
pause
goto menu

:update
echo.
echo ===============================
echo       修改现有用户
===============================
echo.
set /p userid=请输入用户ID（数字）：
set /p username=请输入新用户名：
set /p password=请输入新密码：

rem 验证用户ID是否为数字
echo %userid%| findstr /r "^[0-9]*$" >nul
if errorlevel 1 (
    echo 错误：用户ID必须是数字类型
    pause
    goto update
)

powershell -ExecutionPolicy Bypass -File "%~dp0添加用户数据.ps1" -UserId "%userid%" -Username "%username%" -Password "%password%" -Action Update
echo.
pause
goto menu

:exit
echo 谢谢使用，再见！
pause
