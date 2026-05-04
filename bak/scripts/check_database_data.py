import pyodbc
import base64
import os

# 读取配置文件
def get_connection_string():
    config_file = r'd:\AI\tran\config.dat'
    if os.path.exists(config_file):
        with open(config_file, 'r', encoding='utf-8') as f:
            encrypted = f.read().strip()
        try:
            decoded = base64.b64decode(encrypted).decode('utf-8')
            return decoded
        except:
            print("解密配置文件失败，使用默认连接字符串")
    return "DRIVER={SQL Server};SERVER=localhost;DATABASE=WiNEX_PACS;Trusted_Connection=yes;"

# 连接数据库并查询数据
def check_database_data():
    conn_str = get_connection_string()
    print(f"连接字符串: {conn_str}")
    
    try:
        conn = pyodbc.connect(conn_str)
        cursor = conn.cursor()
        
        print("\n=== 连接成功 ===")
        
        # 检查EXAM_TASK表结构
        print("\n=== EXAM_TASK表结构 ===")
        cursor.execute("SELECT COLUMN_NAME, DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'EXAM_TASK'")
        columns = cursor.fetchall()
        for column in columns:
            print(f"{column[0]} - {column[1]}")
        
        # 检查数据量
        print("\n=== 数据量统计 ===")
        cursor.execute("SELECT COUNT(*) FROM EXAM_TASK WHERE IS_DEL = 0")
        total_count = cursor.fetchone()[0]
        print(f"总检查数: {total_count}")
        
        # 检查最近的检查数据
        print("\n=== 最近10条检查数据 ===")
        cursor.execute("SELECT TOP 10 EXAM_TASK_ID, EXAM_CATEGORY_NAME, SYSTEM_SOURCE_NO, EXAM_TASK_CREATE_TIME, EXAM_TASK_STATUS FROM EXAM_TASK WHERE IS_DEL = 0 ORDER BY EXAM_TASK_CREATE_TIME DESC")
        recent_data = cursor.fetchall()
        
        print("ID\t检查类型\t系统\t创建时间\t\t\t状态")
        print("-" * 80)
        for row in recent_data:
            task_id, category, system, create_time, status = row
            print(f"{task_id}\t{category}\t{system}\t{create_time}\t{status}")
        
        # 检查4月1号的数据
        print("\n=== 4月1号检查数据 ===")
        cursor.execute("SELECT COUNT(*) FROM EXAM_TASK WHERE IS_DEL = 0 AND CONVERT(DATE, EXAM_TASK_CREATE_TIME) = '2026-04-01'")
        april1_count = cursor.fetchone()[0]
        print(f"4月1号检查数: {april1_count}")
        
        if april1_count > 0:
            print("\n4月1号的检查数据:")
            cursor.execute("SELECT TOP 10 EXAM_TASK_ID, EXAM_CATEGORY_NAME, SYSTEM_SOURCE_NO, EXAM_TASK_CREATE_TIME, EXAM_TASK_STATUS FROM EXAM_TASK WHERE IS_DEL = 0 AND CONVERT(DATE, EXAM_TASK_CREATE_TIME) = '2026-04-01' ORDER BY EXAM_TASK_CREATE_TIME DESC")
            april1_data = cursor.fetchall()
            
            print("ID\t检查类型\t系统\t创建时间\t\t\t状态")
            print("-" * 80)
            for row in april1_data:
                task_id, category, system, create_time, status = row
                print(f"{task_id}\t{category}\t{system}\t{create_time}\t{status}")
        
        # 检查日期范围
        print("\n=== 检查日期范围 ===")
        cursor.execute("SELECT MIN(EXAM_TASK_CREATE_TIME) AS MinDate, MAX(EXAM_TASK_CREATE_TIME) AS MaxDate FROM EXAM_TASK WHERE IS_DEL = 0")
        date_range = cursor.fetchone()
        if date_range and date_range[0]:
            print(f"最早检查时间: {date_range[0]}")
            print(f"最晚检查时间: {date_range[1]}")
        else:
            print("数据库中没有检查数据")
        
        cursor.close()
        conn.close()
        
    except Exception as e:
        print(f"错误: {e}")

if __name__ == "__main__":
    print("=== 检查数据库实际数据 ===")
    check_database_data()
    print("\n=== 检查完成 ===")