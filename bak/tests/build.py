import os
import subprocess
import shutil

# 检查是否安装了PyInstaller
def check_pyinstaller():
    try:
        subprocess.run(['pyinstaller', '--version'], capture_output=True, text=True, check=True)
        return True
    except (subprocess.CalledProcessError, FileNotFoundError):
        return False

# 安装PyInstaller
def install_pyinstaller():
    print("正在安装PyInstaller...")
    result = subprocess.run(['pip', 'install', 'pyinstaller'], capture_output=True, text=True)
    if result.returncode == 0:
        print("PyInstaller安装成功!")
        return True
    else:
        print(f"PyInstaller安装失败: {result.stderr}")
        return False

# 打包应用
def build_exe():
    print("开始打包应用...")
    
    # 清理之前的构建
    for dir in ['build', 'dist']:
        if os.path.exists(dir):
            shutil.rmtree(dir)
    
    # 使用PyInstaller打包
    cmd = [
        'pyinstaller',
        '--onefile',
        '--add-data', 'templates;templates',
        '--name', 'db_procedure_caller',
        'app.py'
    ]
    
    result = subprocess.run(cmd, capture_output=True, text=True)
    if result.returncode == 0:
        print("打包成功!")
        print(f"可执行文件位置: {os.path.join('dist', 'db_procedure_caller.exe')}")
        return True
    else:
        print(f"打包失败: {result.stderr}")
        return False

if __name__ == '__main__':
    if not check_pyinstaller():
        if not install_pyinstaller():
            print("无法安装PyInstaller，无法继续!")
            exit(1)
    
    build_exe()
