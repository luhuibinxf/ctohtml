#!/usr/bin/env python
# -*- coding: utf-8 -*-
"""
验证每日分析界面的框大小样式
"""

import os

def verify_styles():
    """验证样式配置"""
    js_file = r"d:\AI\tran\DbProcedureCaller\bin\Debug\net8.0-windows\templates\js\dailyAnalysis.js"
    
    if not os.path.exists(js_file):
        print("❌ 文件不存在:", js_file)
        return
    
    with open(js_file, 'r', encoding='utf-8') as f:
        content = f.read()
    
    print("📋 每日分析界面框大小验证")
    print("=" * 50)
    
    # 检查样式配置
    style_checks = [
        ("宽度统一", "width: 140px", content.count("width: 140px")),
        ("高度统一", "height: 38px", content.count("height: 38px")),
        ("多选框高度", "height: 62px", content.count("height: 62px")),
        ("容器宽度", "width: 160px", content.count("width: 160px")),
        ("样式类定义", ".da-filter-item select", content.count(".da-filter-item select")),
        ("样式类定义", ".da-filter-item input", content.count(".da-filter-item input")),
        ("样式类定义", ".da-filter-input", content.count(".da-filter-input"))
    ]
    
    all_pass = True
    for check_name, check_str, count in style_checks:
        status = "✅" if count > 0 else "❌"
        print(f"{status} {check_name}: {count} 处")
        if count == 0:
            all_pass = False
    
    print("=" * 50)
    
    if all_pass:
        print("🎉 所有样式配置验证通过！")
        print("\n📐 框大小配置：")
        print("   - 下拉框/输入框: 140px × 38px")
        print("   - 多选下拉框: 140px × 62px")
        print("   - 容器宽度: 160px")
        print("\n💡 请按 Ctrl+Shift+F5 强制刷新浏览器查看效果")
    else:
        print("❌ 部分样式配置缺失，请检查代码")

if __name__ == "__main__":
    verify_styles()