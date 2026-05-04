import pyodbc

print("=== 数据库连接测试 ===")

try:
    # 尝试连接数据库
    conn = pyodbc.connect('DRIVER={SQL Server};SERVER=localhost;DATABASE=WiNEX_PACS;Trusted_Connection=yes;')
    print("数据库连接成功！")
    
    # 检查数据库名称
    cursor = conn.cursor()
    cursor.execute("SELECT DB_NAME()")
    db_name = cursor.fetchone()[0]
    print("当前数据库: " + db_name)
    
    # 检查表是否存在
    cursor.execute("SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'EXAM_TASK'")
    table_exists = cursor.fetchone()[0] > 0
    print("EXAM_TASK表存在: " + str(table_exists))
    
    if table_exists:
        # 检查数据量
        cursor.execute("SELECT COUNT(*) FROM EXAM_TASK")
        total_count = cursor.fetchone()[0]
        print("EXAM_TASK表总数据量: " + str(total_count))
        
        # 检查有效数据量
        cursor.execute("SELECT COUNT(*) FROM EXAM_TASK WHERE IS_DEL = 0")
        valid_count = cursor.fetchone()[0]
        print("有效数据量 (IS_DEL = 0): " + str(valid_count))
        
        # 检查最近的数据
        if valid_count > 0:
            print("\n最近5条检查数据:")
            print("-" * 80)
            print("ID\t检查类型\t系统\t创建时间")
            print("-" * 80)
            
            cursor.execute("SELECT TOP 5 EXAM_TASK_ID, EXAM_CATEGORY_NAME, SYSTEM_SOURCE_NO, EXAM_TASK_CREATE_TIME FROM EXAM_TASK WHERE IS_DEL = 0 ORDER BY EXAM_TASK_CREATE_TIME DESC")
            rows = cursor.fetchall()
            
            for row in rows:
                task_id, category, system, create_time = row
                print(str(task_id) + "\t" + category + "\t" + system + "\t" + str(create_time))
        
        # 检查4月1号的数据
        cursor.execute("SELECT COUNT(*) FROM EXAM_TASK WHERE IS_DEL = 0 AND CONVERT(DATE, EXAM_TASK_CREATE_TIME) = '2026-04-01'")
        april1_count = cursor.fetchone()[0]
        print("\n4月1号检查数据量: " + str(april1_count))
        
        if april1_count > 0:
            print("\n4月1号检查数据:")
            print("-" * 80)
            print("ID\t检查类型\t系统\t创建时间")
            print("-" * 80)
            
            cursor.execute("SELECT TOP 5 EXAM_TASK_ID, EXAM_CATEGORY_NAME, SYSTEM_SOURCE_NO, EXAM_TASK_CREATE_TIME FROM EXAM_TASK WHERE IS_DEL = 0 AND CONVERT(DATE, EXAM_TASK_CREATE_TIME) = '2026-04-01' ORDER BY EXAM_TASK_CREATE_TIME DESC")
            april1_rows = cursor.fetchall()
            
            for row in april1_rows:
                task_id, category, system, create_time = row
                print(str(task_id) + "\t" + category + "\t" + system + "\t" + str(create_time))
    
    cursor.close()
    conn.close()
    
    print("\n数据库检查完成！")
    
except Exception as e:
    print("错误: " + str(e))
    print("\n可能的原因:")
    print("1. SQL Server服务未运行")
    print("2. 数据库名称不正确")
    print("3. 权限不足")
    print("4. 网络连接问题")