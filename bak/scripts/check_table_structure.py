import pyodbc

print("=== 检查EXAM_TASK表结构 ===")

try:
    # 连接数据库
    conn = pyodbc.connect('DRIVER={SQL Server};SERVER=localhost;DATABASE=WiNEX_PACS;Trusted_Connection=yes;')
    cursor = conn.cursor()
    
    # 检查表的所有列
    print("EXAM_TASK表列结构:")
    cursor.execute("SELECT COLUMN_NAME, DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'EXAM_TASK'")
    columns = cursor.fetchall()
    
    column_names = []
    for column in columns:
        column_names.append(column[0])
        print(column[0] + " (" + column[1] + ")")
    
    # 检查前5条数据
    print("\n前5条数据:")
    cursor.execute("SELECT TOP 5 * FROM EXAM_TASK WHERE IS_DEL = 0")
    rows = cursor.fetchall()
    
    # 打印列名
    print("\n列名:")
    for i, col_name in enumerate(column_names):
        print(f"{i}: {col_name}")
    
    # 打印数据
    print("\n数据:")
    for i, row in enumerate(rows):
        print(f"行 {i+1}:")
        for j, value in enumerate(row):
            if j < len(column_names):
                print(f"  {column_names[j]}: {value}")
    
    # 检查是否有日期相关列
    print("\n查找日期相关列:")
    date_columns = []
    for column in columns:
        if 'date' in column[1].lower() or 'time' in column[1].lower():
            date_columns.append(column[0])
            print(f"日期列: {column[0]} ({column[1]})")
    
    # 检查数据量
    cursor.execute("SELECT COUNT(*) FROM EXAM_TASK WHERE IS_DEL = 0")
    count = cursor.fetchone()[0]
    print(f"\n有效数据量: {count}")
    
    # 如果有日期列，查询最近的数据
    if date_columns:
        print(f"\n使用日期列 {date_columns[0]} 查询最近5条数据:")
        query = f"SELECT TOP 5 * FROM EXAM_TASK WHERE IS_DEL = 0 ORDER BY {date_columns[0]} DESC"
        cursor.execute(query)
        recent_rows = cursor.fetchall()
        
        for i, row in enumerate(recent_rows):
            print(f"行 {i+1}:")
            for j, value in enumerate(row):
                if j < len(column_names):
                    print(f"  {column_names[j]}: {value}")
    
    cursor.close()
    conn.close()
    
    print("\n表结构检查完成！")
    
except Exception as e:
    print("错误: " + str(e))