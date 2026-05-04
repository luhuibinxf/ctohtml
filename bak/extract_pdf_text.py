#!/usr/bin/env python
# -*- coding: utf-8 -*-

import subprocess
import os

def extract_pdf_text(pdf_path, output_path):
    try:
        result = subprocess.run(
            ['powershell', '-Command', f'(Get-Content "{pdf_path}" -Raw) -match "(?s)(.*)" | Out-Null; Write-Output "Processing PDF..."'],
            capture_output=True,
            text=True,
            encoding='utf-8'
        )
        print("PowerShell command executed")
        
        try:
            from win32com.client import Dispatch
            word = Dispatch("Word.Application")
            word.Visible = False
            
            doc = word.Documents.Open(pdf_path, ReadOnly=True)
            text = doc.Content.Text
            doc.Close()
            word.Quit()
            
            with open(output_path, 'w', encoding='utf-8') as f:
                f.write(text)
            print(f"Successfully extracted text to {output_path}")
            return text
            
        except Exception as e:
            print(f"COM method failed: {e}")
            return ""
            
    except Exception as e:
        print(f"Error: {e}")
        return ""

def main():
    pdf_path = r"d:\AI\tran\.trae\agents\winex-stat-validator\docs\检查表结构.pdf"
    output_path = "pdf_content.txt"
    
    if not os.path.exists(pdf_path):
        print(f"文件不存在: {pdf_path}")
        return
    
    print(f"正在处理PDF文件: {pdf_path}")
    text = extract_pdf_text(pdf_path, output_path)
    
    if text:
        print(f"\n提取的内容预览 (前500字符):")
        print("=" * 50)
        print(text[:500])
        print("=" * 50)

if __name__ == "__main__":
    main()