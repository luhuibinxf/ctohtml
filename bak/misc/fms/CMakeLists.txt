cmake_minimum_required(VERSION 3.10)
project(FamilyFinanceManager)

set(CMAKE_CXX_STANDARD 17)
set(CMAKE_CXX_STANDARD_REQUIRED ON)

# 设置编译选项
if(MSVC)
    add_compile_options(/W4 /WX)
else()
    add_compile_options(-Wall -Wextra -Wpedantic)
endif()

# 包含头文件目录
include_directories(src)

# 收集源文件
file(GLOB SOURCES "src/*.cpp")

# 创建可执行文件
add_executable(fms ${SOURCES})

# 测试目标
add_subdirectory(tests)
