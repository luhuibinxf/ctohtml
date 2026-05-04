import pyodbc

print("=== 数据库基本检查 ===")

try:
    # 连接数据库
    conn = pyodbc.connect('DRIVER={SQL Server};SERVER=localhost;DATABASE=WiNEX_PACS;Trusted_Connection=yes;')
    print("数据库连接成功")
    
    cursor = conn.cursor()
    
    # 检查EXAM_TASK表
    print("\n检查EXAM_TASK表:")
    cursor.execute("SELECT COUNT(*) FROM EXAM_TASK")
    count = cursor.fetchone()[0]
    print(f"总记录数: {count}")
    
    # 检查表结构
    print("\n表结构:")
    cursor.execute("SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'EXAM_TASK' ORDER BY ORDINAL_POSITION")
    columns = cursor.fetchall()
    for col in columns:
        print(col[0])
    
    # 检查前3条数据
    print("\n前3条数据:")
    cursor.execute("SELECT TOP 3 * FROM EXAM_TASK WHERE IS_DEL = 0")
    rows = cursor.fetchall()
    for i, row in enumerate(rows):
        print(f"\n记录 {i+1}:")
        for j, value in enumerate(row):
            if j < len(columns):
                print(f"  {columns[j][0]}: {value}")
    
    # 检查系统来源
    print("\n系统来源分布:")
    cursor.execute("SELECT SYSTEM_SOURCE_NO, COUNT(*) FROM EXAM_TASK WHERE IS_DEL = 0 GROUP BY SYSTEM_SOURCE_NO")
    systems = cursor.fetchall()
    for system in systems:
        print(f"  {system[0]}: {system[1]}条")
    
    # 检查检查类型
    print("\n检查类型分布:")
    cursor.execute("SELECT EXAM_CATEGORY_NAME, COUNT(*) FROM EXAM_TASK WHERE IS_DEL = 0 GROUP BY EXAM_CATEGORY_NAME ORDER BY COUNT(*) DESC")
    categories = cursor.fetchall()
    for cat in categories[:10]:  # 只显示前10个
        print(f"  {cat[0]}: {cat[1]}条")
    
    cursor.close()
    conn.close()
    
    print("\n检查完成")
    
except Exception as e:
    print(f"错误: {e}")