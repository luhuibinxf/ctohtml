cmake_minimum_required(VERSION 3.10)

# 收集测试文件
file(GLOB TEST_SOURCES "*.cpp")

# 创建测试可执行文件
add_executable(test_fms ${TEST_SOURCES})

# 链接主项目的源文件
target_link_libraries(test_fms PRIVATE fms)

# 包含头文件目录
target_include_directories(test_fms PRIVATE ../src)
