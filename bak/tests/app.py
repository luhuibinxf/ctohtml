from flask import Flask, render_template, request, jsonify
import pyodbc
import os
import configparser

app = Flask(__name__)

# 读取配置文件
def load_config():
    config = configparser.ConfigParser()
    if os.path.exists('config.ini'):
        config.read('config.ini')
        return {
            'driver': config.get('Database', 'driver', fallback='{SQL Server}'),
            'server': config.get('Database', 'server', fallback='localhost'),
            'database': config.get('Database', 'database', fallback='WiNEX_PACS'),
            'uid': config.get('Database', 'uid', fallback='sa'),
            'pwd': config.get('Database', 'pwd', fallback='P@ssw0rd')
        }
    else:
        # 创建默认配置文件
        config['Database'] = {
            'driver': '{SQL Server}',
            'server': 'localhost',
            'database': 'WiNEX_PACS',
            'uid': 'sa',
            'pwd': 'P@ssw0rd'
        }
        with open('config.ini', 'w') as configfile:
            config.write(configfile)
        return {
            'driver': '{SQL Server}',
            'server': 'localhost',
            'database': 'WiNEX_PACS',
            'uid': 'sa',
            'pwd': 'P@ssw0rd'
        }

# 数据库连接配置
DB_CONFIG = load_config()

# 连接数据库
def get_db_connection():
    conn_str = f"DRIVER={DB_CONFIG['driver']};SERVER={DB_CONFIG['server']};DATABASE={DB_CONFIG['database']};UID={DB_CONFIG['uid']};PWD={DB_CONFIG['pwd']}"
    return pyodbc.connect(conn_str)

# 调用存储过程
def call_stored_procedure(procedure_name, *args):
    conn = get_db_connection()
    cursor = conn.cursor()
    
    # 构建参数占位符
    params = ','.join(['?' for _ in args])
    query = f"EXEC {procedure_name} {params}"
    
    cursor.execute(query, args)
    
    # 获取结果
    columns = [column[0] for column in cursor.description]
    results = []
    for row in cursor.fetchall():
        results.append(dict(zip(columns, row)))
    
    conn.commit()
    cursor.close()
    conn.close()
    
    return results

# 按系统分类统计接口
@app.route('/api/statistics/system', methods=['GET'])
def get_system_statistics():
    try:
        # 获取查询参数
        start_date = request.args.get('start_date', '')
        end_date = request.args.get('end_date', '')
        
        if not start_date or not end_date:
            return jsonify({'error': '缺少日期参数'}), 400
        
        # 构建SQL查询
        sql = f"""
        SELECT 
            CONVERT(DATE, REGISTER_AT) AS 日期,
            COUNT(*) AS 总人次,
            SUM(CASE WHEN SYSTEM_SOURCE_NO='RIS' THEN 1 ELSE 0 END) AS 放射,
            SUM(CASE WHEN SYSTEM_SOURCE_NO='UIS' THEN 1 ELSE 0 END) AS 超声,
            SUM(CASE WHEN SYSTEM_SOURCE_NO='EIS' THEN 1 ELSE 0 END) AS 内镜
        FROM EXAM_TASK
        WHERE IS_DEL = 0 
            AND LABEL_NO <> ''
            AND REGISTER_AT >= '{start_date}'
            AND REGISTER_AT < '{end_date}'
            AND EXAM_TASK_STATUS >= 50
        GROUP BY CONVERT(DATE, REGISTER_AT)
        """
        
        # 执行查询
        conn = get_db_connection()
        cursor = conn.cursor()
        cursor.execute(sql)
        
        # 获取结果
        columns = [column[0] for column in cursor.description]
        results = []
        for row in cursor.fetchall():
            results.append(dict(zip(columns, row)))
        
        conn.close()
        return jsonify(results)
        
    except Exception as e:
        return jsonify({'error': str(e)}), 500

# 按就诊类型统计接口
@app.route('/api/statistics/encounter', methods=['GET'])
def get_encounter_statistics():
    try:
        # 获取查询参数
        start_date = request.args.get('start_date', '')
        end_date = request.args.get('end_date', '')
        
        if not start_date or not end_date:
            return jsonify({'error': '缺少日期参数'}), 400
        
        # 构建SQL查询
        sql = f"""
        SELECT 
            ENCOUNTER_TYPE_NO,
            COUNT(*) AS 数量
        FROM EXAM_TASK
        WHERE REGISTER_AT >= '{start_date}' AND REGISTER_AT < '{end_date}'
            AND EXAM_TASK_STATUS >= 50
        GROUP BY ENCOUNTER_TYPE_NO
        """
        
        # 执行查询
        conn = get_db_connection()
        cursor = conn.cursor()
        cursor.execute(sql)
        
        # 获取结果
        columns = [column[0] for column in cursor.description]
        results = []
        for row in cursor.fetchall():
            results.append(dict(zip(columns, row)))
        
        conn.close()
        return jsonify(results)
        
    except Exception as e:
        return jsonify({'error': str(e)}), 500

# 获取检查任务详情接口
@app.route('/api/tasks/<task_id>', methods=['GET'])
def get_task_detail(task_id):
    try:
        # 构建SQL查询
        sql = f"""
        SELECT 
            EXAM_TASK_ID, SYSTEM_SOURCE_NO, ENCOUNTER_TYPE_NO, FULL_NAME, PATIENT_NO,
            REGISTER_AT, EXAM_COMPLETION_AT, EXAM_CATEGORY_NO, EXAM_CATEGORY_NAME,
            LABEL_NO, EXAM_TASK_STATUS
        FROM EXAM_TASK
        WHERE EXAM_TASK_ID = '{task_id}' AND IS_DEL = 0
        """
        
        # 执行查询
        conn = get_db_connection()
        cursor = conn.cursor()
        cursor.execute(sql)
        
        # 获取结果
        columns = [column[0] for column in cursor.description]
        row = cursor.fetchone()
        
        if not row:
            return jsonify({'error': '任务不存在'}), 404
        
        task = dict(zip(columns, row))
        conn.close()
        return jsonify(task)
        
    except Exception as e:
        return jsonify({'error': str(e)}), 500

# 健康检查接口
@app.route('/api/health', methods=['GET'])
def health_check():
    try:
        conn = get_db_connection()
        cursor = conn.cursor()
        cursor.execute("SELECT 1")
        result = cursor.fetchone()
        conn.close()
        
        if result:
            return jsonify({'status': 'healthy', 'message': '数据库连接正常'})
        else:
            return jsonify({'status': 'unhealthy', 'message': '数据库连接异常'}), 500
            
    except Exception as e:
        return jsonify({'status': 'unhealthy', 'message': str(e)}), 500

@app.route('/')
def index():
    return render_template('index.html')

@app.route('/call-procedure', methods=['POST'])
def call_procedure():
    data = request.json
    procedure_name = data.get('procedure_name')
    params = data.get('params', [])
    
    try:
        results = call_stored_procedure(procedure_name, *params)
        return jsonify({'success': True, 'data': results})
    except Exception as e:
        return jsonify({'success': False, 'error': str(e)})

if __name__ == '__main__':
    # 确保templates目录存在
    if not os.path.exists('templates'):
        os.makedirs('templates')
    app.run(debug=False, host='localhost', port=5000)
