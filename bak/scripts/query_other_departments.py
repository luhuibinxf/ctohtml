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
def query_other_departments():
    conn_str = get_connection_string()
    print(f"连接字符串: {conn_str}")
    
    try:
        conn = pyodbc.connect(conn_str)
        cursor = conn.cursor()
        
        print("\n=== 连接成功 ===")
        
        # 查询其他科室的检查类型及其系统来源
        print("\n=== 其他科室检查类型详情 ===")
        print("检查类型\t\t系统来源\t建议执行科室")
        print("-" * 60)
        
        other_exam_types = [
            '彩超', '彩超(老)', '彩超(新)', '介入超声', '脑电', 
            '体检彩超', '新城体检彩超', '支气管镜(新)', '支气管镜(总)'
        ]
        
        for exam_type in other_exam_types:
            # 查询系统来源
            cursor.execute(
                "SELECT DISTINCT SYSTEM_SOURCE_NO FROM EXAM_TASK WHERE IS_DEL = 0 AND EXAM_CATEGORY_NAME = ?", 
                exam_type
            )
            systems = cursor.fetchall()
            
            system_source = systems[0][0] if systems else '未知'
            
            # 确定建议执行科室
            if '超声' in exam_type:
                suggested_dept = '超声科'
            elif '支气管镜' in exam_type:
                suggested_dept = '呼吸内镜科'
            elif '脑电' in exam_type:
                suggested_dept = '神经内科'
            elif '体检' in exam_type:
                suggested_dept = '体检科'
            else:
                suggested_dept = '其他科室'
            
            print(f"{exam_type}\t\t{system_source}\t{suggested_dept}")
        
        # 查询所有系统来源
        print("\n=== 系统来源统计 ===")
        cursor.execute("SELECT DISTINCT SYSTEM_SOURCE_NO, COUNT(*) as Count FROM EXAM_TASK WHERE IS_DEL = 0 GROUP BY SYSTEM_SOURCE_NO ORDER BY Count DESC")
        systems = cursor.fetchall()
        
        for system in systems:
            print(f"系统: {system[0]}\t检查数量: {system[1]}")
        
        cursor.close()
        conn.close()
        
    except Exception as e:
        print(f"错误: {e}")

if __name__ == "__main__":
    print("=== 查询其他科室检查类型详情 ===")
    query_other_departments()
    print("\n=== 查询完成 ===")