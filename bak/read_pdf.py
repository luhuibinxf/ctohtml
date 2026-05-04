#!/usr/bin/env python
# -*- coding: utf-8 -*-

import os
import sys

def read_pdf(pdf_path):
    try:
        from pdfplumber import PDF
        
        with PDF.open(pdf_path) as pdf:
            num_pages = len(pdf.pages)
            print("PDF文件:", pdf_path)
            print("页数:", num_pages)
            print("=" * 80)
            
            all_text = ""
            for page_num in range(num_pages):
                page = pdf.pages[page_num]
                text = page.extract_text()
                if text:
                    all_text += text
                    print("\n--- 第", page_num + 1, "页 ---")
                    print(text[:2000])
        
        return all_text
        
    except ImportError:
        print("请先安装 pdfplumber 库")
        print("安装命令: pip install pdfplumber")
        return ""
    except Exception as e:
        print("读取PDF失败:", str(e))
        return ""

def main():
    pdf_path = r"d:\AI\tran\.trae\agents\winex-stat-validator\docs\检查表结构.pdf"
    
    if not os.path.exists(pdf_path):
        print("文件不存在:", pdf_path)
        sys.exit(1)
    
    text = read_pdf(pdf_path)
    
    output_file = "pdf_content.txt"
    with open(output_file, 'w', encoding='utf-8') as f:
        f.write(text)
    print("内容已保存到:", output_file)

if __name__ == "__main__":
    main()