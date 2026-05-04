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
def query_exam_types():
    conn_str = get_connection_string()
    print(f"连接字符串: {conn_str}")
    
    try:
        conn = pyodbc.connect(conn_str)
        cursor = conn.cursor()
        
        print("\n=== 连接成功 ===")
        
        # 查询所有检查类型
        print("\n=== 所有检查类型及其执行科室 ===")
        print("检查类型\t\t执行科室")
        print("-" * 40)
        
        # 查询所有检查类别
        cursor.execute("SELECT DISTINCT EXAM_CATEGORY_NAME FROM EXAM_TASK WHERE IS_DEL = 0 ORDER BY EXAM_CATEGORY_NAME")
        exam_types = cursor.fetchall()
        
        for row in exam_types:
            exam_category = row[0]
            
            # 映射执行科室
            if '胃镜' in exam_category or '肠镜' in exam_category or '内镜' in exam_category:
                dept = '消化内镜(总)'
            elif exam_category in ['普放', '普放(新)', '钼靶', '消化道造影', '消化道造影(新)', 
                                'CT', 'CT(新)', 'CTA', 'CTA(新)', 'CT重建', 'CT三维重建',
                                '核磁共振', '核磁共振增强', 'MRI增强', 'MRA', 'MRV', 'MRU', 'MRCP', 'MRS']:
                dept = '放射科'
            else:
                dept = '其他科室'
            
            print(f"{exam_category}\t\t{dept}")
        
        # 统计各执行科室的检查类型数量
        print("\n=== 各执行科室检查类型数量 ===")
        dept_count = {}
        
        for row in exam_types:
            exam_category = row[0]
            if '胃镜' in exam_category or '肠镜' in exam_category or '内镜' in exam_category:
                dept = '消化内镜(总)'
            elif exam_category in ['普放', '普放(新)', '钼靶', '消化道造影', '消化道造影(新)', 
                                'CT', 'CT(新)', 'CTA', 'CTA(新)', 'CT重建', 'CT三维重建',
                                '核磁共振', '核磁共振增强', 'MRI增强', 'MRA', 'MRV', 'MRU', 'MRCP', 'MRS']:
                dept = '放射科'
            else:
                dept = '其他科室'
            
            if dept in dept_count:
                dept_count[dept] += 1
            else:
                dept_count[dept] = 1
        
        for dept, count in dept_count.items():
            print(f"{dept}: {count} 种检查类型")
        
        cursor.close()
        conn.close()
        
    except Exception as e:
        print(f"错误: {e}")

if __name__ == "__main__":
    print("=== 查询所有检查类型对应的执行科室 ===")
    query_exam_types()
    print("\n=== 查询完成 ===")