-- 创建每日查询配置表
CREATE TABLE DAILY_QUERY_CONFIG (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    QUERY_NAME NVARCHAR(100) NOT NULL,
    QUERY_TYPE NVARCHAR(50) NOT NULL,
    QUERY_SQL NVARCHAR(MAX) NOT NULL,
    DESCRIPTION NVARCHAR(255),
    CREATED_BY NVARCHAR(50),
    CREATED_TIME DATETIME DEFAULT GETDATE(),
    UPDATED_BY NVARCHAR(50),
    UPDATED_TIME DATETIME DEFAULT GETDATE()
);

-- 创建每日查询历史表
CREATE TABLE DAILY_QUERY_HISTORY (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    QUERY_DATE DATE NOT NULL,
    QUERY_TYPE NVARCHAR(50) NOT NULL,
    QUERY_PARAMS NVARCHAR(MAX),
    EXECUTION_TIME DATETIME DEFAULT GETDATE(),
    EXECUTION_STATUS NVARCHAR(20),
    ROW_COUNT INT,
    ERROR_MESSAGE NVARCHAR(MAX)
);

-- 插入默认查询配置
INSERT INTO DAILY_QUERY_CONFIG (QUERY_NAME, QUERY_TYPE, QUERY_SQL, DESCRIPTION)
VALUES 
('系统信息汇总', 'system', 'SELECT SYSTEM_SOURCE_NO AS "系统标识", COUNT(*) AS "任务数量", MAX(EXAM_TASK_CREATE_TIME) AS "最新任务时间" FROM EXAM_TASK WHERE IS_DEL = 0 GROUP BY SYSTEM_SOURCE_NO ORDER BY COUNT(*) DESC', '统计各系统的任务数量'),
('执行科室汇总', 'department', 'SELECT CASE WHEN EXAM_CATEGORY_NAME IN (''肠镜(老)'', ''肠镜(新)'', ''超声内镜'', ''胃镜(老)'', ''胃镜(新)'', ''消化肠镜'', ''消化胃镜'') THEN ''消化内镜(总)'' WHEN EXAM_CATEGORY_NAME IN (''支气管镜(新)'', ''支气管镜(总)'') THEN ''呼吸内镜科'' WHEN EXAM_CATEGORY_NAME IN (''CT'', ''CT(新)'',''核磁共振'', ''钼靶'', ''普放'', ''普放(新)'',''消化道造影'', ''消化道造影(新)'') THEN ''放射科'' WHEN EXAM_CATEGORY_NAME = ''介入超声'' THEN ''超声科'' WHEN EXAM_CATEGORY_NAME = ''脑电'' THEN ''神经内科'' WHEN EXAM_CATEGORY_NAME IN (''体检彩超'', ''新城体检彩超'') THEN ''体检科'' ELSE ''其他科室'' END AS "执行科室", COUNT(*) AS "任务数量" FROM EXAM_TASK WHERE IS_DEL = 0 GROUP BY CASE WHEN EXAM_CATEGORY_NAME IN (''肠镜(老)'', ''肠镜(新)'', ''超声内镜'', ''胃镜(老)'', ''胃镜(新)'', ''消化肠镜'', ''消化胃镜'') THEN ''消化内镜(总)'' WHEN EXAM_CATEGORY_NAME IN (''支气管镜(新)'', ''支气管镜(总)'') THEN ''呼吸内镜科'' WHEN EXAM_CATEGORY_NAME IN (''CT'', ''CT(新)'',''核磁共振'', ''钼靶'', ''普放'', ''普放(新)'',''消化道造影'', ''消化道造影(新)'') THEN ''放射科'' WHEN EXAM_CATEGORY_NAME = ''介入超声'' THEN ''超声科'' WHEN EXAM_CATEGORY_NAME = ''脑电'' THEN ''神经内科'' WHEN EXAM_CATEGORY_NAME IN (''体检彩超'', ''新城体检彩超'') THEN ''体检科'' ELSE ''其他科室'' END ORDER BY COUNT(*) DESC', '按科室汇总任务数量'),
('检查类别汇总', 'category', 'SELECT EXAM_CATEGORY_NAME AS "检查类别", COUNT(*) AS "数量", MAX(EXAM_TASK_CREATE_TIME) AS "最新检查时间" FROM EXAM_TASK WHERE IS_DEL = 0 GROUP BY EXAM_CATEGORY_NAME ORDER BY COUNT(*) DESC', '统计各检查类别的数量'),
('报告医生工作量', 'reporter', 'SELECT REPORTER_NAME AS "医生姓名", COUNT(*) AS "报告数量", MAX(REPORT_TIME) AS "最新报告时间" FROM EXAM_REPORT WHERE IS_DEL = 0 GROUP BY REPORTER_NAME ORDER BY COUNT(*) DESC', '统计报告医生的工作量'),
('审核医生工作量', 'reviewer', 'SELECT REVIEWER_NAME AS "医生姓名", COUNT(*) AS "审核数量", MAX(REVIEW_TIME) AS "最新审核时间" FROM EXAM_REPORT WHERE IS_DEL = 0 GROUP BY REVIEWER_NAME ORDER BY COUNT(*) DESC', '统计审核医生的工作量'),
('技师工作量', 'technician', 'SELECT TECHNICIAN_NAME AS "技师姓名", COUNT(*) AS "检查数量", MAX(EXAM_TASK_CREATE_TIME) AS "最新检查时间" FROM EXAM_TASK WHERE IS_DEL = 0 GROUP BY TECHNICIAN_NAME ORDER BY COUNT(*) DESC', '统计技师的工作量'),
('系统状态汇总', 'status', 'SELECT EXAM_TASK_STATUS AS "任务状态", COUNT(*) AS "数量", MAX(EXAM_TASK_CREATE_TIME) AS "最新时间" FROM EXAM_TASK WHERE IS_DEL = 0 GROUP BY EXAM_TASK_STATUS ORDER BY COUNT(*) DESC', '统计各任务状态的数量'),
('执行科室工作量排名', 'rank', 'SELECT CASE WHEN EXAM_CATEGORY_NAME IN (''肠镜(老)'', ''肠镜(新)'', ''超声内镜'', ''胃镜(老)'', ''胃镜(新)'', ''消化肠镜'', ''消化胃镜'') THEN ''消化内镜(总)'' WHEN EXAM_CATEGORY_NAME IN (''支气管镜(新)'', ''支气管镜(总)'') THEN ''呼吸内镜科'' WHEN EXAM_CATEGORY_NAME IN (''CT'', ''CT(新)'',''核磁共振'', ''钼靶'', ''普放'', ''普放(新)'',''消化道造影'', ''消化道造影(新)'') THEN ''放射科'' WHEN EXAM_CATEGORY_NAME = ''介入超声'' THEN ''超声科'' WHEN EXAM_CATEGORY_NAME = ''脑电'' THEN ''神经内科'' WHEN EXAM_CATEGORY_NAME IN (''体检彩超'', ''新城体检彩超'') THEN ''体检科'' ELSE ''其他科室'' END AS "执行科室", COUNT(*) AS "总任务数", SUM(CASE WHEN EXAM_TASK_STATUS = ''已完成'' THEN 1 ELSE 0 END) AS "已完成数", ROUND(SUM(CASE WHEN EXAM_TASK_STATUS = ''已完成'' THEN 1 ELSE 0 END) * 100.0 / COUNT(*), 2) AS "完成率", MAX(EXAM_TASK_CREATE_TIME) AS "最新任务时间" FROM EXAM_TASK WHERE IS_DEL = 0 GROUP BY CASE WHEN EXAM_CATEGORY_NAME IN (''肠镜(老)'', ''肠镜(新)'', ''超声内镜'', ''胃镜(老)'', ''胃镜(新)'', ''消化肠镜'', ''消化胃镜'') THEN ''消化内镜(总)'' WHEN EXAM_CATEGORY_NAME IN (''支气管镜(新)'', ''支气管镜(总)'') THEN ''呼吸内镜科'' WHEN EXAM_CATEGORY_NAME IN (''CT'', ''CT(新)'',''核磁共振'', ''钼靶'', ''普放'', ''普放(新)'',''消化道造影'', ''消化道造影(新)'') THEN ''放射科'' WHEN EXAM_CATEGORY_NAME = ''介入超声'' THEN ''超声科'' WHEN EXAM_CATEGORY_NAME = ''脑电'' THEN ''神经内科'' WHEN EXAM_CATEGORY_NAME IN (''体检彩超'', ''新城体检彩超'') THEN ''体检科'' ELSE ''其他科室'' END ORDER BY COUNT(*) DESC', '执行科室工作量排名和完成率');
