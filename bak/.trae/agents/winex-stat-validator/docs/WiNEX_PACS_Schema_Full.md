# WiNEX_PACS 数据库完整Schema

> 生成时间: 2026-04-20 21:42:51

- 总表数: 446
- 生成脚本: generate_full_schema.py

---

## APPOINTMENT_ATTENTION_POINT

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| APPOINTMENT_ATTENTION_POINT_ID | numeric(19,0) | - | 否 | ✓ |
| EXAM_ITEM_DIC_ID | numeric(19,0) | - | 是 |  |
| ATTENTION_POINT_DESC | nvarchar(-1) | -1 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |

---

## APPOINTMENT_DAY_SCHEDULE

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| APPOINTMENT_DAY_SCHEDULE_ID | numeric(19,0) | - | 否 | ✓ |
| EXAM_APPOINTMENT_SCHEDULE_ID | numeric(19,0) | - | 否 |  |
| SYSTEM_SOURCE_NO | nvarchar(32) | 32 | 是 |  |
| APPOINTMENT_DATE | date | - | 是 |  |
| APPOINTMENT_START_TIME | nvarchar(10) | 10 | 是 |  |
| APPOINTMENT_END_TIME | nvarchar(10) | 10 | 是 |  |
| APPOINTMENT_TOTAL_COUNT | int(10,0) | - | 是 |  |
| APPOINTMENT_REAL_COUNT | int(10,0) | - | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |

---

## AUTOBUILD_UPDATEDATAINFO

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| TABLENAME | varchar(500) | 500 | 否 |  |
| EXECTYPE | varchar(500) | 500 | 否 |  |
| EXECTIME | datetime | - | 否 |  |

---

## BUSINESS_DBPATCH_TFS_RECORD

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| DBPATCH_TFS_ID | varchar(256) | 256 | 否 | ✓ |
| OPERATE_TIME | datetime | - | 是 |  |
| OPERATOR | varchar(256) | 256 | 是 |  |
| EXEC_HOSPITAL_SOID | varchar(256) | 256 | 是 |  |
| WORK_ITEM_ID | varchar(36) | 36 | 是 |  |
| PRODUCT_ID | varchar(64) | 64 | 是 |  |
| TFS_VERSION | numeric(19,0) | - | 是 |  |
| ITERATION_ID | varchar(1000) | 1000 | 是 |  |

---

## BUSINESS_DBPATCH_VIEW_RECORD

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| ID | varchar(64) | 64 | 否 | ✓ |
| OPERATE_TIME | datetime | - | 是 |  |
| OPERATOR | varchar(256) | 256 | 是 |  |
| WORK_ITEM_ID | varchar(36) | 36 | 是 |  |
| DBPATCH_TFS_ID | varchar(256) | 256 | 否 |  |
| VIEW_VERSION | numeric(19,0) | - | 是 |  |
| SQL_VERSION | numeric(19,0) | - | 是 |  |
| VIEW_NAME | varchar(256) | 256 | 否 |  |
| VIEW_SQL | varchar(-1) | -1 | 否 |  |

---

## CA_SIGNATURERECORD

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| SERIALNO | int(10,0) | - | 否 | ✓ |
| BUSSINESSID | varchar(50) | 50 | 否 |  |
| SIGNID | varchar(50) | 50 | 是 |  |
| ClientCert | varchar(2000) | 2000 | 是 |  |
| ServerCert | varchar(2000) | 2000 | 是 |  |
| REPORTDATA | text(2147483647) | 2147483647 | 是 |  |
| SIGNDATA | text(2147483647) | 2147483647 | 是 |  |
| OPERCODE | varchar(20) | 20 | 否 |  |
| OPERNAME | varchar(20) | 20 | 否 |  |
| OPERPCNAME | varchar(255) | 255 | 是 |  |
| KeySN | varchar(40) | 40 | 是 |  |
| SIGNERCODE | varchar(20) | 20 | 否 |  |
| SIGNERNAME | varchar(50) | 50 | 否 |  |
| SIGNDATE | datetime | - | 否 |  |
| SIGNTYPE | int(10,0) | - | 否 |  |
| TIMESTAMPREQUEST | varchar(8000) | 8000 | 是 |  |
| TIMESTAMP | varchar(8000) | 8000 | 是 |  |

---

## ChineseCode

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| SerialNo | numeric(18,0) | - | 否 | ✓ |
| Character | char(6) | 6 | 否 |  |
| Spell | char(6) | 6 | 是 |  |
| Spell2 | char(6) | 6 | 是 |  |
| TypeCode1 | char(6) | 6 | 是 |  |
| TypeCode2 | char(6) | 6 | 是 |  |
| TypeCode3 | char(6) | 6 | 是 |  |
| TypeCode4 | varchar(30) | 30 | 是 |  |
| TypeCode5 | char(6) | 6 | 是 |  |

---

## CONTRAST_MEDIUM_ALLERGY

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| CONTRAST_MEDIUM_ALLERGY_ID | numeric(19,0) | - | 否 | ✓ |
| BIZ_ROLE_ID | numeric(19,0) | - | 否 |  |
| CONTRAST_MEDIUM_NO | nvarchar(32) | 32 | 是 |  |
| CONTRAST_MEDIUM_NAME | nvarchar(64) | 64 | 是 |  |
| ALLERGY_LEVEL_CODE | numeric(19,0) | - | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |

---

## Dept

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| DeptNo | varchar(12) | 12 | 否 | ✓ |
| DeptName | varchar(50) | 50 | 是 |  |
| ExternCode | varchar(20) | 20 | 是 |  |
| DeptType | char(1) | 1 | 是 |  |
| DeptClass | int(10,0) | - | 是 |  |
| Parent | varchar(12) | 12 | 是 |  |
| MemCode1 | varchar(30) | 30 | 是 |  |
| MemCode2 | varchar(30) | 30 | 是 |  |
| ExHospFlag | char(1) | 1 | 是 |  |
| DeptPhone | varchar(40) | 40 | 是 |  |
| HOSPITALCODE | varchar(50) | 50 | 否 | ✓ |

---

## DIAG_DOC_GROUP

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| DIAG_DOC_GROUP_ID | numeric(19,0) | - | 否 | ✓ |
| DIAG_DOC_GROUP_NAME | nvarchar(64) | 64 | 是 |  |
| SEQ_NO | int(10,0) | - | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(64) | 64 | 是 |  |
| SYSTEM_SOURCE_NO | nvarchar(32) | 32 | 是 |  |

---

## DIAG_DOC_GROUP_USER

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| DIAG_DOC_GROUP_USER_ID | numeric(19,0) | - | 否 | ✓ |
| USER_ID | numeric(19,0) | - | 否 |  |
| DIAG_DOC_GROUP_ID | numeric(19,0) | - | 否 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(64) | 64 | 是 |  |

---

## DIAG_TEMPLATE_CONTENT

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| DIAG_TEMPLATE_CONTENT_ID | numeric(19,0) | - | 否 |  |
| DIAG_TEMPLATE_TREE_ID | numeric(19,0) | - | 否 |  |
| TEMPLATE_ITEM_NO | nvarchar(20) | 20 | 是 |  |
| TEMPLATE_CONTENT | nvarchar(-1) | -1 | 是 |  |
| ENCIPHER_FLAG | numeric(19,0) | - | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 否 |  |
| CREATED_NAME | nvarchar(40) | 40 | 否 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(40) | 40 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(40) | 40 | 是 |  |

---

## DIAG_TEMPLATE_CONTENT_20260331095701BAK

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| DIAG_TEMPLATE_CONTENT_ID | numeric(19,0) | - | 否 |  |
| DIAG_TEMPLATE_TREE_ID | numeric(19,0) | - | 否 |  |
| TEMPLATE_ITEM_NO | nvarchar(20) | 20 | 是 |  |
| TEMPLATE_CONTENT | nvarchar(-1) | -1 | 是 |  |
| ENCIPHER_FLAG | numeric(19,0) | - | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 否 |  |
| CREATED_NAME | nvarchar(40) | 40 | 否 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(40) | 40 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(40) | 40 | 是 |  |

---

## DIAG_TEMPLATE_TREE

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| DIAG_TEMPLATE_TREE_ID | numeric(19,0) | - | 否 |  |
| EXAM_CATEGORY_ID | numeric(19,0) | - | 是 |  |
| TEMPLATE_NODE_NO | nvarchar(64) | 64 | 是 |  |
| TEMPLATE_NODE_NAME | nvarchar(64) | 64 | 是 |  |
| PARENT_TEMPLATE_TREE_ID | numeric(19,0) | - | 是 |  |
| DICT_TEMPLATE_NODE_NO | nvarchar(64) | 64 | 是 |  |
| MEM1_NO | nvarchar(30) | 30 | 是 |  |
| MEM2_NO | nvarchar(30) | 30 | 是 |  |
| NODE_PROPERTY | nvarchar(40) | 40 | 是 |  |
| NODE_TYPE_STATUS | numeric(19,0) | - | 是 |  |
| PRIVATE_BY | numeric(19,0) | - | 是 |  |
| PRIVATE_USER_NAME | nvarchar(32) | 32 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| SYSTEM_SOURCE_NO | nvarchar(32) | 32 | 是 |  |
| DEFAULT_FLAG | numeric(19,0) | - | 是 |  |
| SEQ_NO | int(10,0) | - | 是 |  |
| EXAM_MAIN_CATEGORY_NO | varchar(32) | 32 | 是 |  |
| GENDER_CODE | numeric(19,0) | - | 是 |  |
| STRUCT_FLAG | numeric(19,0) | - | 是 |  |
| CLINIC_DIAG_DESC | nvarchar(1024) | 1024 | 是 |  |
| LEAF_NODE_FLAG | numeric(19,0) | - | 是 |  |

---

## DIAG_TEMPLATE_TREE_2026033095140BAK

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| DIAG_TEMPLATE_TREE_ID | numeric(19,0) | - | 否 |  |
| EXAM_CATEGORY_ID | numeric(19,0) | - | 是 |  |
| TEMPLATE_NODE_NO | nvarchar(64) | 64 | 是 |  |
| TEMPLATE_NODE_NAME | nvarchar(64) | 64 | 是 |  |
| PARENT_TEMPLATE_TREE_ID | numeric(19,0) | - | 是 |  |
| DICT_TEMPLATE_NODE_NO | nvarchar(64) | 64 | 是 |  |
| MEM1_NO | nvarchar(30) | 30 | 是 |  |
| MEM2_NO | nvarchar(30) | 30 | 是 |  |
| NODE_PROPERTY | nvarchar(40) | 40 | 是 |  |
| NODE_TYPE_STATUS | numeric(19,0) | - | 是 |  |
| PRIVATE_BY | numeric(19,0) | - | 是 |  |
| PRIVATE_USER_NAME | nvarchar(32) | 32 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| SYSTEM_SOURCE_NO | nvarchar(32) | 32 | 是 |  |
| DEFAULT_FLAG | numeric(19,0) | - | 是 |  |
| SEQ_NO | int(10,0) | - | 是 |  |
| EXAM_MAIN_CATEGORY_NO | varchar(32) | 32 | 是 |  |
| GENDER_CODE | numeric(19,0) | - | 是 |  |
| STRUCT_FLAG | numeric(19,0) | - | 是 |  |
| CLINIC_DIAG_DESC | nvarchar(1024) | 1024 | 是 |  |
| LEAF_NODE_FLAG | numeric(19,0) | - | 是 |  |

---

## DOC_JOB_SCHEDULE

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| DOC_JOB_SCHEDULE_ID | numeric(19,0) | - | 否 | ✓ |
| DOC_SCHEDULE_DIC_ID | numeric(19,0) | - | 否 |  |
| USER_ID | numeric(19,0) | - | 否 |  |
| SYSTEM_SOURCE_NO | nvarchar(32) | 32 | 是 |  |
| WORK_DATE | date | - | 是 |  |
| LOCK_FLAG | numeric(19,0) | - | 是 |  |
| SUSPEND_FLAG | numeric(19,0) | - | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(64) | 64 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |

---

## DOC_SCHEDULE_DIC

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| DOC_SCHEDULE_DIC_ID | numeric(19,0) | - | 否 | ✓ |
| DIAG_DOC_GROUP_ID | numeric(19,0) | - | 否 |  |
| SYSTEM_SOURCE_NO | nvarchar(32) | 32 | 是 |  |
| DOC_SCHEDULE_DIC_NAME | nvarchar(64) | 64 | 是 |  |
| TASK_START_RELATIVE_DAYS | int(10,0) | - | 是 |  |
| TASK_START_TIME | nvarchar(10) | 10 | 是 |  |
| TASK_END_RELATIVE_DAYS | int(10,0) | - | 是 |  |
| TASK_END_TIME | nvarchar(10) | 10 | 是 |  |
| EXAM_TASK_STATUS | numeric(19,0) | - | 是 |  |
| ASSIGN_BASE_TYPE_CODE | numeric(19,0) | - | 是 |  |
| ASSIGN_BASE_NUMBER | int(10,0) | - | 是 |  |
| ASSIGN_MAX_WAITING_NUMBER | int(10,0) | - | 是 |  |
| DOC_SCHEDULE_DIC_PTY | varchar(-1) | -1 | 是 |  |
| DOC_SCHEDULE_DIC_GROUP_NO | nvarchar(64) | 64 | 是 |  |
| ENABLED_FLAG | numeric(19,0) | - | 是 |  |
| SEQ_NO | smallint(5,0) | - | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(64) | 64 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |

---

## EXAM_AI_DATABASE_SCRIPT

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_AI_DATABASE_SCRIPT_ID | numeric(19,0) | - | 否 | ✓ |
| VERSION_TIME_STAMP | numeric(19,0) | - | 是 |  |
| FILE_PATH | nvarchar(255) | 255 | 是 |  |
| SCRIPT_TYPE_NO | varchar(256) | 256 | 是 |  |
| SCRIPT_RUN_STATUS_NO | varchar(256) | 256 | 是 |  |
| SCRIPT_CONTENT | nvarchar(-1) | -1 | 是 |  |
| SCRIPT_COMMENTS | nvarchar(-1) | -1 | 是 |  |
| SCRIPT_ERROR | nvarchar(-1) | -1 | 是 |  |
| SYSTEM_SOURCE_NO | varchar(64) | 64 | 否 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| CREATED_AT | datetime | - | 否 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |

---

## EXAM_AI_HISTORY

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_AI_HISTORY_ID | numeric(19,0) | - | 否 | ✓ |
| FROM_ID | numeric(19,0) | - | 是 |  |
| HISTORY_TYPE_NO | varchar(256) | 256 | 是 |  |
| HISTORY_VALUE | nvarchar(-1) | -1 | 是 |  |
| SYSTEM_SOURCE_NO | varchar(64) | 64 | 否 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NO | varchar(256) | 256 | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| CREATED_AT | datetime | - | 否 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |

---

## EXAM_AI_INTERFACE

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_AI_INTERFACE_ID | numeric(19,0) | - | 否 | ✓ |
| INTERFACE_NAME | nvarchar(128) | 128 | 否 |  |
| INTERFACE_VALUE | nvarchar(-1) | -1 | 否 |  |
| SEQ_NO | int(10,0) | - | 否 |  |
| INTERFACE_CATEGORY_NO | varchar(128) | 128 | 否 |  |
| SYSTEM_SOURCE_NO | varchar(64) | 64 | 否 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NO | varchar(256) | 256 | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| CREATED_AT | datetime | - | 否 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |

---

## EXAM_AI_MANUAL_CHECK

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_AI_MANUAL_CHECK_ID | numeric(19,0) | - | 否 | ✓ |
| EXAM_AI_STRUC_RESULT_MNG_ID | numeric(19,0) | - | 否 |  |
| MANUAL_CHECK_DOC_NAME | nvarchar(32) | 32 | 是 |  |
| MANUAL_CHECK_DOC_NO | varchar(64) | 64 | 是 |  |
| MANUAL_MODIFY_REC | nvarchar(512) | 512 | 是 |  |
| SEQ_NO | int(10,0) | - | 是 |  |
| SYSTEM_SOURCE_NO | varchar(64) | 64 | 否 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NO | varchar(256) | 256 | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| CREATED_AT | datetime | - | 否 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |

---

## EXAM_AI_REQUEST_RECORD

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_AI_REQUEST_RECORD_ID | numeric(19,0) | - | 否 | ✓ |
| SCENE_NAME | nvarchar(128) | 128 | 是 |  |
| SCENE_NO | varchar(256) | 256 | 是 |  |
| REQUEST_AT | datetime | - | 否 |  |
| REQUEST_SOURCE | nvarchar(255) | 255 | 是 |  |
| REQUEST_PARAMETERS | nvarchar(-1) | -1 | 否 |  |
| RESPONSE_AT | datetime | - | 是 |  |
| CALL_DURATION | int(10,0) | - | 是 |  |
| RESPONSE_RESULT | nvarchar(-1) | -1 | 是 |  |
| ERROR_MESSAGE | nvarchar(2000) | 2000 | 是 |  |
| SYSTEM_SOURCE_NO | varchar(64) | 64 | 否 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NO | varchar(128) | 128 | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| CREATED_AT | datetime | - | 否 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |

---

## EXAM_AI_RULE_TASK

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_AI_RULE_TASK_ID | numeric(19,0) | - | 否 | ✓ |
| RULE_NAME | nvarchar(64) | 64 | 是 |  |
| RULE_TASK_NAME | nvarchar(64) | 64 | 是 |  |
| RULE_CUE_WORD | varchar(-1) | -1 | 是 |  |
| RULE_TASK_STATUS_NO | varchar(64) | 64 | 是 |  |
| RULE_VERIFY_TOTAL | numeric(20,0) | - | 是 |  |
| RULE_VERIFY_CORRECT | numeric(21,0) | - | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 否 |  |
| CREATED_NAME | nvarchar(64) | 64 | 否 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |
| SYSTEM_SOURCE_NO | varchar(64) | 64 | 否 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 否 |  |
| TASK_CREATED_AT | datetime | - | 是 |  |

---

## EXAM_AI_SCENE_INTERFACE

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_AI_SCENE_INTERFACE_ID | numeric(19,0) | - | 否 | ✓ |
| EXAM_AI_DICTIONARY_ID | numeric(19,0) | - | 否 |  |
| EXAM_AI_INTERFACE_ID | numeric(19,0) | - | 否 |  |
| RESULT_MAPPING | nvarchar(-1) | -1 | 否 |  |
| SEQ_NO | int(10,0) | - | 否 |  |
| SYSTEM_SOURCE_NO | varchar(64) | 64 | 否 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NO | varchar(256) | 256 | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| CREATED_AT | datetime | - | 否 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |

---

## EXAM_AI_SCHEDULE_TASK

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_AI_SCHEDULE_TASK_ID | numeric(19,0) | - | 否 | ✓ |
| RELATED_TASK_NO | varchar(32) | 32 | 是 |  |
| SCHEDULE_TASK_CATEGORY_NO | varchar(128) | 128 | 否 |  |
| SCHEDULE_TASK_SUBCATEGORY_NO | varchar(128) | 128 | 是 |  |
| SCHEDULE_TASK_NAME | nvarchar(50) | 50 | 否 |  |
| SCHEDULE_TASK_COMPLETED_AT | datetime | - | 是 |  |
| SCHEDULE_TASK_DESC | nvarchar(2000) | 2000 | 是 |  |
| SCHEDULE_TASK_EXPIRED_AT | datetime | - | 是 |  |
| SCHEDULE_TASK_CYCLE | numeric(19,0) | - | 是 |  |
| RETRY_COUNT | numeric(19,0) | - | 是 |  |
| SCHEDULE_TASK_PLANNED_AT | datetime | - | 否 |  |
| SCHEDULE_TASK_SCRIPT_TYPE_NO | varchar(256) | 256 | 否 |  |
| SCHEDULE_TASK_SCRIPT | nvarchar(-1) | -1 | 否 |  |
| SCHEDULE_TASK_STATUS_NO | varchar(128) | 128 | 否 |  |
| SCHEDULE_TASK_EXECUTED_AT | datetime | - | 是 |  |
| SCHEDULE_TASK_FAIL_DESC | nvarchar(-1) | -1 | 是 |  |
| MAX_RETRY_COUNT | numeric(19,0) | - | 是 |  |
| EXTERNAL_TASK_FLAG | numeric(19,0) | - | 是 |  |
| PARENT_EXAM_SCHEDULE_TASK_ID | numeric(19,0) | - | 是 |  |
| SCHEDULE_TASK_SOURCE_ELEMENT | nvarchar(-1) | -1 | 是 |  |
| SYSTEM_SOURCE_NO | varchar(64) | 64 | 否 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NO | varchar(128) | 128 | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| CREATED_AT | datetime | - | 否 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |

---

## EXAM_AI_SCHEDULE_TASK_HISTORY

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_AI_SCHEDULE_TASK_HISTORY_ID | numeric(19,0) | - | 否 | ✓ |
| EXAM_AI_SCHEDULE_TASK_ID | numeric(19,0) | - | 否 |  |
| RELATED_TASK_NO | varchar(32) | 32 | 是 |  |
| SCHEDULE_TASK_CATEGORY_NO | varchar(128) | 128 | 否 |  |
| SCHEDULE_TASK_SUBCATEGORY_NO | varchar(128) | 128 | 是 |  |
| SCHEDULE_TASK_NAME | nvarchar(50) | 50 | 否 |  |
| SCHEDULE_TASK_COMPLETED_AT | datetime | - | 是 |  |
| SCHEDULE_TASK_DESC | nvarchar(2000) | 2000 | 是 |  |
| SCHEDULE_TASK_EXPIRED_AT | datetime | - | 是 |  |
| SCHEDULE_TASK_CYCLE | numeric(19,0) | - | 是 |  |
| RETRY_COUNT | numeric(19,0) | - | 是 |  |
| SCHEDULE_TASK_PLANNED_AT | datetime | - | 否 |  |
| SCHEDULE_TASK_SCRIPT_TYPE_NO | varchar(128) | 128 | 否 |  |
| SCHEDULE_TASK_SCRIPT | nvarchar(-1) | -1 | 否 |  |
| SCHEDULE_TASK_STATUS_NO | varchar(128) | 128 | 否 |  |
| SCHEDULE_TASK_EXECUTED_AT | datetime | - | 是 |  |
| SCHEDULE_TASK_FAIL_DESC | nvarchar(-1) | -1 | 是 |  |
| MAX_RETRY_COUNT | numeric(19,0) | - | 是 |  |
| EXTERNAL_TASK_FLAG | numeric(19,0) | - | 是 |  |
| PARENT_EXAM_SCHEDULE_TASK_ID | numeric(19,0) | - | 是 |  |
| SCHEDULE_TASK_SOURCE_ELEMENT | nvarchar(-1) | -1 | 是 |  |
| SYSTEM_SOURCE_NO | varchar(64) | 64 | 否 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NO | varchar(256) | 256 | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| CREATED_AT | datetime | - | 否 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |

---

## EXAM_AI_SDI_ITEM

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_AI_SDI_ITEM_ID | numeric(19,0) | - | 否 | ✓ |
| ITEM_NAME | nvarchar(64) | 64 | 否 |  |
| ITEM_REPORT_CONTENT_SCOPE | nvarchar(256) | 256 | 否 |  |
| ITEM_INDICATORS_CONTENT | nvarchar(-1) | -1 | 否 |  |
| ITEM_PROCESSING_STEPS | nvarchar(-1) | -1 | 否 |  |
| SYSTEM_SOURCE_NO | varchar(64) | 64 | 否 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| CREATED_AT | datetime | - | 否 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |

---

## EXAM_AI_SDI_SCOPE

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_AI_SDI_SCOPE_ID | numeric(19,0) | - | 否 | ✓ |
| EXAM_AI_SDI_ITEM_ID | numeric(19,0) | - | 否 |  |
| SCOPE_TYPE_NO | varchar(64) | 64 | 否 |  |
| SCOPE_CONTENT | nvarchar(128) | 128 | 否 |  |
| SYSTEM_SOURCE_NO | varchar(64) | 64 | 否 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| CREATED_AT | datetime | - | 否 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |

---

## EXAM_AI_SETTINGS

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_AI_SETTINGS_ID | numeric(19,0) | - | 否 | ✓ |
| SETTING_DOMAIN_NO | varchar(64) | 64 | 否 |  |
| SETTING_DOMAIN_NAME | nvarchar(32) | 32 | 否 |  |
| SYSTEM_SOURCE_NO | varchar(128) | 128 | 否 |  |
| SETTING_ITEM_KEY | nvarchar(32) | 32 | 否 |  |
| SETTING_ITEM_SECTION | nvarchar(64) | 64 | 否 |  |
| SETTING_ITEM_ENTRY | nvarchar(64) | 64 | 否 |  |
| SETTING_ITEM_DATA_TYPE_NO | varchar(64) | 64 | 否 |  |
| SETTING_DOMAIN_IDENTITY | varchar(128) | 128 | 是 |  |
| SETTING_VALUE | nvarchar(-1) | -1 | 是 |  |
| REMARK | nvarchar(255) | 255 | 否 |  |
| PRIVATE_BY | numeric(19,0) | - | 否 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NO | varchar(256) | 256 | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| CREATED_AT | datetime | - | 否 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |

---

## EXAM_AI_STRUC_ITEMS

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_AI_STRUC_ITEMS_ID | numeric(19,0) | - | 否 | ✓ |
| ITEM_NAME | nvarchar(64) | 64 | 是 |  |
| ITEM_STATUS_NO | varchar(64) | 64 | 是 |  |
| LEAD_DOC_NAME | nvarchar(32) | 32 | 是 |  |
| LEAD_DOC_NO | varchar(32) | 32 | 是 |  |
| EXEC_START_AT | datetime | - | 是 |  |
| EXEC_END_AT | datetime | - | 是 |  |
| COPILOT_AUTO_CHECK_FLAG | numeric(19,0) | - | 是 |  |
| ITEM_DESCRIBETION | nvarchar(128) | 128 | 是 |  |
| ITEM_REPORT_CONTENT_SCOPE | nvarchar(256) | 256 | 是 |  |
| ITEM_INDICATORS_CONTENT | nvarchar(-1) | -1 | 是 |  |
| ITEM_EXPORT_DATA_SCOPE | nvarchar(-1) | -1 | 是 |  |
| ITEM_QUERY_CONDITION | nvarchar(-1) | -1 | 是 |  |
| SEQ_NO | int(10,0) | - | 是 |  |
| PROCESSING_STEPS | nvarchar(-1) | -1 | 是 |  |
| IMPORT_PROJECT | smallint(5,0) | - | 是 |  |
| SYSTEM_SOURCE_NO | varchar(64) | 64 | 否 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NO | varchar(256) | 256 | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| CREATED_AT | datetime | - | 否 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |

---

## EXAM_AI_STRUC_REPORT_TEMP

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_AI_STRUC_REPORT_TEMP_ID | numeric(19,0) | - | 否 | ✓ |
| EXAM_AI_STRUC_RESULT_MNG_ID | numeric(19,0) | - | 否 |  |
| STATUS_NO | varchar(32) | 32 | 是 |  |
| EXAM_FINDINGS_PLAIN_TEXT | nvarchar(-1) | -1 | 是 |  |
| EXAM_CONCLUSION_PLAIN_TEXT | nvarchar(-1) | -1 | 是 |  |
| AI_ASSISTANT_FLAG | numeric(19,0) | - | 是 |  |
| REPORT_START_AT | datetime | - | 是 |  |
| REPORT_AT | datetime | - | 是 |  |
| REPORT_NO | varchar(64) | 64 | 是 |  |
| REPORT_NAME | nvarchar(32) | 32 | 是 |  |
| EXAM_REPORT_ID | numeric(19,0) | - | 是 |  |
| SEQ_NO | int(10,0) | - | 否 |  |
| SYSTEM_SOURCE_NO | varchar(64) | 64 | 否 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NO | varchar(256) | 256 | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| CREATED_AT | datetime | - | 否 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |

---

## EXAM_AI_STRUC_RESULT

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_AI_STRUC_RESULT_ID | numeric(19,0) | - | 否 | ✓ |
| EXAM_AI_STRUC_RESULT_MNG_ID | numeric(19,0) | - | 否 |  |
| STRUC_INDICATOR | numeric(32,0) | - | 是 |  |
| RESULT | nvarchar(128) | 128 | 是 |  |
| UNIT | nvarchar(32) | 32 | 是 |  |
| MATCH_FLAG | numeric(19,0) | - | 是 |  |
| SEQ_NO | int(10,0) | - | 是 |  |
| GROUP_NO | varchar(64) | 64 | 是 |  |
| SYSTEM_SOURCE_NO | varchar(64) | 64 | 否 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NO | varchar(256) | 256 | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| CREATED_AT | datetime | - | 否 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |

---

## EXAM_AI_STRUC_RESULT_MNG

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_AI_STRUC_RESULT_MNG_ID | numeric(19,0) | - | 否 | ✓ |
| EXAM_AI_REPORT_ID | numeric(19,0) | - | 否 |  |
| EXAM_AI_STRUC_ITEMS_ID | numeric(19,0) | - | 否 |  |
| STRUC_AT | datetime | - | 是 |  |
| COPILOT_CHECK_FLAG | numeric(19,0) | - | 是 |  |
| COPILOT_CHECK_RESULT_FLAG | numeric(19,0) | - | 是 |  |
| COPILOT_CHECK_RESULT_CONTENT | nvarchar(-1) | -1 | 是 |  |
| COPILOT_CHECK_AT | datetime | - | 是 |  |
| MANUAL_CHECK_FLAG | numeric(19,0) | - | 是 |  |
| MANUAL_CHECK_DOC_NAME | nvarchar(32) | 32 | 是 |  |
| MANUAL_CHECK_DOC_NO | varchar(64) | 64 | 是 |  |
| MANUAL_CHECK_AT | datetime | - | 是 |  |
| MANUAL_MODIFY_REC | nvarchar(512) | 512 | 是 |  |
| COPILOT_MANUAL_VALID_FLAG | numeric(19,0) | - | 是 |  |
| COPILOT_MANUAL_CHECK_FLAG | numeric(19,0) | - | 是 |  |
| COPILOT_MANUAL_VALID_REMARK | nvarchar(256) | 256 | 是 |  |
| SEQ_NO | int(10,0) | - | 是 |  |
| SYSTEM_SOURCE_NO | varchar(64) | 64 | 否 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NO | varchar(256) | 256 | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| CREATED_AT | datetime | - | 否 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |

---

## EXAM_APPOINT_CHARGE_ITEM

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_APPOINT_CHARGE_ITEM_ID | numeric(19,0) | - | 否 | ✓ |
| EXAM_TASK_ID | numeric(19,0) | - | 是 |  |
| EXAM_REQUISITION_NO | varchar(64) | 64 | 是 |  |
| EXAM_ORDER_ITEM_NO | varchar(64) | 64 | 是 |  |
| EXAM_ITEM_GROUP_NO | varchar(64) | 64 | 是 |  |
| EXAM_ITEM_NO | varchar(64) | 64 | 是 |  |
| EXAM_ITEM_NAME | nvarchar(128) | 128 | 是 |  |
| EXTERN_ITEM_TYPE_NO | varchar(50) | 50 | 是 |  |
| BOOK_CENTER_NO | varchar(64) | 64 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| SYSTEM_SOURCE_NO | varchar(64) | 64 | 是 |  |

---

## EXAM_APPOINTMENT

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_APPOINTMENT_ID | numeric(19,0) | - | 否 | ✓ |
| EXAM_TASK_ID | numeric(19,0) | - | 否 |  |
| APPOINTMENT_DAY_SCHEDULE_ID | numeric(19,0) | - | 是 |  |
| SYSTEM_SOURCE_NO | nvarchar(32) | 32 | 是 |  |
| EXAM_TRIAGE_QUEUE_ID | numeric(19,0) | - | 是 |  |
| APPOINTMENT_DATE | date | - | 是 |  |
| APPOINTMENT_START_TIME | nvarchar(10) | 10 | 是 |  |
| APPOINTMENT_END_TIME | nvarchar(10) | 10 | 是 |  |
| APPOINTMENT_DESC | nvarchar(256) | 256 | 是 |  |
| APPOINTMENT_ADDRESS | nvarchar(256) | 256 | 是 |  |
| APPOINTMENT_NO | nvarchar(8) | 8 | 是 |  |
| APPOINTMENT_PRINT_FLAG | numeric(19,0) | - | 是 |  |
| EXAM_REQUISITION_NO | nvarchar(64) | 64 | 是 |  |
| APPOINTMENT_BY | numeric(19,0) | - | 是 |  |
| APPOINTMENT_NAME | nvarchar(64) | 64 | 是 |  |
| EXTERNAL_SYS_NAME | varchar(256) | 256 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |
| BATCH_NO | nvarchar(64) | 64 | 是 |  |
| START_NO | nvarchar(64) | 64 | 是 |  |
| BOOK_CENTER_EXT_ID | nvarchar(64) | 64 | 是 |  |

---

## EXAM_APPOINTMENT_ITEM

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_APPOINTMENT_ITEM_ID | nvarchar(256) | 256 | 否 | ✓ |
| EXAM_APPOINTMENT_SCHEDULE_ID | numeric(19,0) | - | 否 |  |
| EXAM_ITEM_DIC_ID | numeric(19,0) | - | 是 |  |
| URGENT_APPOINTMENT_FLAG | numeric(19,0) | - | 是 |  |
| EMERGENCY_APPOINTMENT_FLAG | numeric(19,0) | - | 是 |  |
| ITEM_OCCUPANCY_NUMBER | int(10,0) | - | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |

---

## EXAM_APPOINTMENT_SCHEDULE

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_APPOINTMENT_SCHEDULE_ID | numeric(19,0) | - | 否 | ✓ |
| EXAM_TRIAGE_QUEUE_ID | numeric(19,0) | - | 否 |  |
| SYSTEM_SOURCE_NO | nvarchar(32) | 32 | 是 |  |
| APPOINTMENT_DESC | nvarchar(256) | 256 | 是 |  |
| APPOINTMENT_ADDRESS | nvarchar(256) | 256 | 是 |  |
| APPOINTMENT_TYPE_CODE | numeric(19,0) | - | 是 |  |
| APPOINTMENT_START_DATE | date | - | 是 |  |
| APPOINTMENT_END_DATE | date | - | 是 |  |
| APPOINTMENT_MAX_DAYS | int(10,0) | - | 是 |  |
| APPOINTMENT_DAY_START_TIME | nvarchar(10) | 10 | 是 |  |
| APPOINTMENT_DAY_END_TIME | nvarchar(10) | 10 | 是 |  |
| APPOINTMENT_TIME_INTERVAL | int(10,0) | - | 是 |  |
| APPOINTMENT_MAX_PERSONS | int(10,0) | - | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |

---

## EXAM_ATTACHMENT

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_ATTACHMENT_ID | numeric(19,0) | - | 否 | ✓ |
| EXAM_TASK_ID | numeric(19,0) | - | 是 |  |
| SYSTEM_SOURCE_NO | nvarchar(32) | 32 | 是 |  |
| FILE_SIZE | bigint(19,0) | - | 是 |  |
| FILE_TYPE_NO | nvarchar(20) | 20 | 是 |  |
| PC_MAC_ADDRESS | nvarchar(128) | 128 | 是 |  |
| UPLOAD_PC_NAME | nvarchar(50) | 50 | 是 |  |
| FILE_PACS_IMAGE_UID | nvarchar(128) | 128 | 是 |  |
| FILE_PATH | nvarchar(256) | 256 | 是 |  |
| FILE_CREATED_AT | datetime | - | 是 |  |
| UPLOAD_PACS_FLAG | numeric(19,0) | - | 是 |  |
| UPLOAD_PACS_ERROR_MSG | nvarchar(-1) | -1 | 是 |  |
| FILE_ANNOTATION | nvarchar(-1) | -1 | 是 |  |
| MEMO | nvarchar(200) | 200 | 是 |  |
| FILE_SOURCE_NO | varchar(50) | 50 | 是 |  |
| FILE_PACS_STUDY_UID | varchar(128) | 128 | 是 |  |
| FILE_PACS_SERIES_UID | varchar(128) | 128 | 是 |  |
| MODALITY_PERF_PROC_STEP_ID | numeric(19,0) | - | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |

---

## EXAM_ATTACHMENT_20260415BAK

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_ATTACHMENT_ID | numeric(19,0) | - | 否 |  |
| EXAM_TASK_ID | numeric(19,0) | - | 是 |  |
| SYSTEM_SOURCE_NO | nvarchar(32) | 32 | 是 |  |
| FILE_SIZE | bigint(19,0) | - | 是 |  |
| FILE_TYPE_NO | nvarchar(20) | 20 | 是 |  |
| PC_MAC_ADDRESS | nvarchar(128) | 128 | 是 |  |
| UPLOAD_PC_NAME | nvarchar(50) | 50 | 是 |  |
| FILE_PACS_IMAGE_UID | nvarchar(128) | 128 | 是 |  |
| FILE_PATH | nvarchar(256) | 256 | 是 |  |
| FILE_CREATED_AT | datetime | - | 是 |  |
| UPLOAD_PACS_FLAG | numeric(19,0) | - | 是 |  |
| UPLOAD_PACS_ERROR_MSG | nvarchar(-1) | -1 | 是 |  |
| FILE_ANNOTATION | nvarchar(-1) | -1 | 是 |  |
| MEMO | nvarchar(200) | 200 | 是 |  |
| FILE_SOURCE_NO | varchar(50) | 50 | 是 |  |
| FILE_PACS_STUDY_UID | varchar(128) | 128 | 是 |  |
| FILE_PACS_SERIES_UID | varchar(128) | 128 | 是 |  |
| MODALITY_PERF_PROC_STEP_ID | numeric(19,0) | - | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |

---

## EXAM_CASE_TAG_DICT

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_CASE_TAG_DICT_ID | numeric(19,0) | - | 否 | ✓ |
| CASE_TAG_NO | varchar(32) | 32 | 否 |  |
| CASE_TAG_NAME | nvarchar(64) | 64 | 否 |  |
| CASE_TAG_PROPERTY | varchar(32) | 32 | 否 |  |
| PARENT_CASE_TAG_DICT_ID | numeric(19,0) | - | 否 |  |
| PRIVATE_BY | numeric(19,0) | - | 是 |  |
| PRIVATE_NAME | nvarchar(64) | 64 | 是 |  |
| PRIVATE_NO | varchar(32) | 32 | 是 |  |
| CASE_TAG_LEVEL | varchar(64) | 64 | 是 |  |
| SEQ_NO | int(10,0) | - | 否 |  |
| HOSPITAL_NAME | varchar(64) | 64 | 否 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_NAME | nvarchar(64) | 64 | 否 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |
| SYSTEM_SOURCE_NO | varchar(64) | 64 | 否 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |

---

## EXAM_CASE_TAG_DICT_20260318BAK

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_CASE_TAG_DICT_ID | numeric(19,0) | - | 否 |  |
| CASE_TAG_NO | varchar(32) | 32 | 否 |  |
| CASE_TAG_NAME | nvarchar(64) | 64 | 否 |  |
| CASE_TAG_PROPERTY | varchar(32) | 32 | 否 |  |
| PARENT_CASE_TAG_DICT_ID | numeric(19,0) | - | 否 |  |
| PRIVATE_BY | numeric(19,0) | - | 是 |  |
| PRIVATE_NAME | nvarchar(64) | 64 | 是 |  |
| PRIVATE_NO | varchar(32) | 32 | 是 |  |
| CASE_TAG_LEVEL | varchar(64) | 64 | 是 |  |
| SEQ_NO | int(10,0) | - | 否 |  |
| HOSPITAL_NAME | varchar(64) | 64 | 否 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_NAME | nvarchar(64) | 64 | 否 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |
| SYSTEM_SOURCE_NO | varchar(64) | 64 | 否 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |

---

## EXAM_CATEGORY

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_CATEGORY_ID | numeric(19,0) | - | 否 | ✓ |
| EXAM_CATEGORY_NO | varchar(20) | 20 | 是 |  |
| EXAM_CATEGORY_NAME | nvarchar(64) | 64 | 是 |  |
| SYSTEM_SOURCE_NO | nvarchar(20) | 20 | 否 |  |
| EXEC_DEPT_ID | numeric(19,0) | - | 是 |  |
| VISIBLE_FLAG | numeric(19,0) | - | 是 |  |
| SEQ_NO | int(10,0) | - | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 否 |  |
| CREATED_NAME | nvarchar(40) | 40 | 否 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(40) | 40 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(40) | 40 | 是 |  |
| EXEC_DEPT_NO | nvarchar(32) | 32 | 是 |  |
| EXAM_MAIN_CATEGORY_NO | varchar(20) | 20 | 是 |  |
| EXEC_DEPT_NAME | nvarchar(64) | 64 | 是 |  |

---

## EXAM_CATEGORY_EQUIPMENT

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_CATEGORY_EQUIPMENT_ID | numeric(19,0) | - | 否 | ✓ |
| EXAM_CATEGORY_ID | numeric(19,0) | - | 否 |  |
| EXAM_EQUIPMENT_ID | numeric(19,0) | - | 否 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |

---

## EXAM_CATEGORY_FEE

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_CATEGORY_FEE_ID | numeric(19,0) | - | 否 | ✓ |
| EXAM_CATEGORY_ID | numeric(19,0) | - | 是 |  |
| EXAM_ITEM_NO | nvarchar(64) | 64 | 否 |  |
| EXAM_ITEM_NAME | nvarchar(256) | 256 | 否 |  |
| EXTERN_ITEM_TYPE_NO | nvarchar(50) | 50 | 是 |  |
| SYSTEM_SOURCE_NO | nvarchar(32) | 32 | 否 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_CLUDING_FILM_FEE | numeric(19,0) | - | 是 |  |
| EXAM_ITEM_TYPE_NO | varchar(64) | 64 | 否 |  |
| BUSINESS_TYPE_NO | varchar(64) | 64 | 是 |  |
| BUSINESS_TYPE_NAME | nvarchar(256) | 256 | 是 |  |
| BUSINESS_GROUP_NO | varchar(64) | 64 | 否 |  |
| BUSINESS_GROUP_NAME | nvarchar(256) | 256 | 否 |  |

---

## EXAM_CATEGORY_PRINTFILE

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_CATEGORY_PRINTFILE_ID | numeric(19,0) | - | 否 | ✓ |
| EXAM_CATEGORY_ID | numeric(19,0) | - | 否 |  |
| PRINT_FILE_NO | nvarchar(40) | 40 | 是 |  |
| PRINT_FILE_NAME | nvarchar(100) | 100 | 是 |  |
| DEFAULT_FLAG | numeric(19,0) | - | 是 |  |
| VISIBLE_FLAG | numeric(19,0) | - | 是 |  |
| SEQ_NO | int(10,0) | - | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 否 |  |
| CREATED_NAME | nvarchar(40) | 40 | 否 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(40) | 40 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(40) | 40 | 是 |  |
| SYSTEM_SOURCE_NO | nvarchar(20) | 20 | 否 |  |
| ENCOUNTER_TYPE_NO | nvarchar(32) | 32 | 是 |  |
| PRINT_TIMES | int(10,0) | - | 是 |  |
| TEMPLATE_DESIGN_CONTENT | nvarchar(-1) | -1 | 是 |  |
| IMAGE_LAYOUT_SETTING | nvarchar(-1) | -1 | 是 |  |
| PRINT_PAPER_SIZE | varchar(64) | 64 | 是 |  |
| PRINT_PAPER_SETTING | nvarchar(-1) | -1 | 是 |  |

---

## EXAM_CATEGORY_ROLE

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_CATEGORY_ROLE_ID | numeric(19,0) | - | 否 |  |
| ROLE_ID | numeric(19,0) | - | 否 |  |
| ROLE_NO | numeric(19,0) | - | 否 |  |
| ROLE_NAME | nvarchar(128) | 128 | 是 |  |
| EXAM_CATEGORY_ID | numeric(19,0) | - | 否 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| SYSTEM_SOURCE_NO | nvarchar(50) | 50 | 否 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |

---

## EXAM_CATEGORY_TECHNO_RULE

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_CATEGORY_TECHNO_RULE_ID | numeric(19,0) | - | 否 | ✓ |
| TECHNO_RULE_ID | numeric(19,0) | - | 否 |  |
| EXAM_CATEGORY_ID | numeric(19,0) | - | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 否 |  |
| CREATED_NAME | nvarchar(40) | 40 | 否 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(40) | 40 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(40) | 40 | 是 |  |

---

## EXAM_CHARGE_ITEM

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_CHARGE_ITEM_ID | numeric(19,0) | - | 否 | ✓ |
| EXAM_REQUISITION_ID | numeric(19,0) | - | 是 |  |
| EXAM_TASK_ID | numeric(19,0) | - | 否 |  |
| EXAM_REQUISITION_NO | nvarchar(64) | 64 | 是 |  |
| SYSTEM_SOURCE_NO | nvarchar(32) | 32 | 是 |  |
| EXAM_ORDER_ITEM_NO | nvarchar(64) | 64 | 是 |  |
| EXAM_ITEM_NO | nvarchar(64) | 64 | 是 |  |
| EXAM_ITEM_NAME | nvarchar(256) | 256 | 是 |  |
| EXAM_ITEM_GROUP_NO | nvarchar(64) | 64 | 是 |  |
| EXTERN_ITEM_TYPE_NO | nvarchar(50) | 50 | 是 |  |
| EXAM_ITEM_PRICE | numeric(19,2) | - | 是 |  |
| EXAM_ITEM_QTY | numeric(19,2) | - | 是 |  |
| EXAM_ITEM_UNIT | nvarchar(64) | 64 | 是 |  |
| ITEM_SOURCE_CODE | numeric(19,0) | - | 是 |  |
| CONFIRMED_NO | nvarchar(32) | 32 | 是 |  |
| ITEM_CHARGE_STATUS | numeric(19,0) | - | 是 |  |
| REFUND_REASON | nvarchar(256) | 256 | 是 |  |
| NEED_CONFIRM_FLAG | numeric(19,0) | - | 是 |  |
| LINK_ITEM_NO | varchar(64) | 64 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |
| BOOK_CENTER_NO | varchar(128) | 128 | 是 |  |
| APPLY_AT | datetime | - | 是 |  |
| ACCOUNT_STATUS | nvarchar(100) | 100 | 是 |  |

---

## EXAM_CHECK_RECORD

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_CHECK_RECORD_ID | numeric(19,0) | - | 否 | ✓ |
| EXAM_TASK_ID | numeric(19,0) | - | 否 |  |
| CHECK_TYPE_NO | varchar(32) | 32 | 否 |  |
| CHECK_CONTENT | nvarchar(2000) | 2000 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |
| SYSTEM_SOURCE_NO | varchar(32) | 32 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |

---

## EXAM_CLASSIC_CASE

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_CLASSIC_CASE_ID | numeric(19,0) | - | 否 | ✓ |
| EXAM_TASK_ASSIGNMENT_ID | numeric(19,0) | - | 否 |  |
| EXAM_TASK_ID | numeric(19,0) | - | 否 |  |
| CLASSIC_CASE_DESC | nvarchar(-1) | -1 | 是 |  |
| CLASSIC_CASE_REMARK | nvarchar(2000) | 2000 | 是 |  |
| ENABLED_FLAG | numeric(19,0) | - | 是 |  |
| SYSTEM_SOURCE_NO | varchar(32) | 32 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |

---

## EXAM_CLASSIC_CASE_TAG

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_CLASSIC_CASE_TAG_ID | numeric(19,0) | - | 否 | ✓ |
| EXAM_CLASSIC_CASE_ID | numeric(19,0) | - | 否 |  |
| CLASSIC_CASE_TAG_NO | varchar(64) | 64 | 是 |  |
| CLASSIC_CASE_TAG_NAME | nvarchar(64) | 64 | 是 |  |
| SYSTEM_SOURCE_NO | nvarchar(32) | 32 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |

---

## EXAM_CONSULTANT

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_CONSULTANT_ID | numeric(19,0) | - | 否 | ✓ |
| EXAM_CONSULTATION_ID | numeric(19,0) | - | 否 |  |
| CONSULTANT_BY | numeric(19,0) | - | 是 |  |
| CONSULTANT_NAME | nvarchar(64) | 64 | 是 |  |
| SEQ_NO | smallint(5,0) | - | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(64) | 64 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |

---

## EXAM_CONSULTATION

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_CONSULTATION_ID | numeric(19,0) | - | 否 | ✓ |
| EXAM_TASK_ASSIGNMENT_ID | numeric(19,0) | - | 否 |  |
| EXAM_TASK_ID | numeric(19,0) | - | 否 |  |
| SYSTEM_SOURCE_NO | nvarchar(32) | 32 | 是 |  |
| EXAM_CATEGORY_ID | numeric(19,0) | - | 是 |  |
| CONSULTATION_HOST_BY | numeric(19,0) | - | 是 |  |
| CONSULTATION_HOST_NAME | nvarchar(64) | 64 | 是 |  |
| CONSULTATION_AT | datetime | - | 是 |  |
| CONSULTATION_SPEECH_CONTENT | nvarchar(-1) | -1 | 是 |  |
| IMAGE_READING_SUMMARY | nvarchar(2000) | 2000 | 是 |  |
| CONSULTATION_CONCLUSION | nvarchar(2000) | 2000 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(64) | 64 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |

---

## EXAM_COPILOT_HISTORY

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_COPILOT_HISTORY_ID | numeric(19,0) | - | 否 | ✓ |
| EXAM_TASK_ID | numeric(19,0) | - | 否 |  |
| COPILOT_TYPE_NO | varchar(50) | 50 | 是 |  |
| COPILOT_AT | datetime | - | 是 |  |
| CURRENT_EXAM_COMPLETION_AT | datetime | - | 是 |  |
| CURRENT_EXAM_ITEM_NAME | nvarchar(256) | 256 | 是 |  |
| CURRENT_EXAM_FINDINGS | nvarchar(-1) | -1 | 是 |  |
| CURRENT_EXAM_CONCLUSION | nvarchar(-1) | -1 | 是 |  |
| LAST_EXAM_COMPLETION_AT | datetime | - | 是 |  |
| LAST_EXAM_ITEM_NAME | nvarchar(256) | 256 | 是 |  |
| LAST_EXAM_FINDINGS | nvarchar(-1) | -1 | 是 |  |
| LAST_EXAM_CONCLUSION | nvarchar(-1) | -1 | 是 |  |
| COPILOT_RESULT | nvarchar(-1) | -1 | 是 |  |
| COPILOT_FINISH_AT | datetime | - | 是 |  |
| COPILOT_BY | numeric(19,0) | - | 是 |  |
| SYSTEM_SOURCE_NO | varchar(32) | 32 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |

---

## EXAM_CRITICAL_VALUES

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_CRITICAL_VALUES_ID | numeric(19,0) | - | 否 | ✓ |
| EXAM_TASK_ID | numeric(19,0) | - | 否 |  |
| SYSTEM_SOURCE_NO | nvarchar(32) | 32 | 是 |  |
| CALLER_ID | numeric(19,0) | - | 是 |  |
| CALLER_NAME | nvarchar(32) | 32 | 是 |  |
| CALLED_AT | datetime | - | 是 |  |
| PATIENT_TEL | nvarchar(32) | 32 | 是 |  |
| INFORMED_WARD_ID | numeric(19,0) | - | 是 |  |
| INFORMED_WARD_NO | nvarchar(16) | 16 | 是 |  |
| INFORMED_WARD_NAME | nvarchar(32) | 32 | 是 |  |
| INFORMED_WARD_TEL | nvarchar(32) | 32 | 是 |  |
| INFORMED_DEPT_ID | numeric(19,0) | - | 是 |  |
| INFORMED_DEPT_NO | nvarchar(16) | 16 | 是 |  |
| INFORMED_DEPT_NAME | nvarchar(32) | 32 | 是 |  |
| INFORMED_DEPT_TEL | nvarchar(32) | 32 | 是 |  |
| INFORMED_DOC_ID | numeric(19,0) | - | 是 |  |
| INFORMED_DOC_NO | nvarchar(16) | 16 | 是 |  |
| INFORMED_DOC_NAME | nvarchar(32) | 32 | 是 |  |
| INFORMED_DOC_TEL | nvarchar(32) | 32 | 是 |  |
| CRITICAL_VALUES_NO | varchar(128) | 128 | 是 |  |
| CRITICAL_VALUES_DESC | nvarchar(256) | 256 | 是 |  |
| CRITICAL_VALUES_STATUS | numeric(19,0) | - | 是 |  |
| COMFORMER_ID | numeric(19,0) | - | 是 |  |
| COMFORMER_NO | nvarchar(16) | 16 | 是 |  |
| COMFORMER_NAME | nvarchar(32) | 32 | 是 |  |
| COMFORMED_AT | datetime | - | 是 |  |
| COMFORMED_DESC | nvarchar(256) | 256 | 是 |  |
| NOTIFIED_PROCESSOR_ID | numeric(19,0) | - | 是 |  |
| NOTIFIED_PROCESSOR_NO | nvarchar(16) | 16 | 是 |  |
| NOTIFIED_PROCESSOR_NAME | nvarchar(32) | 32 | 是 |  |
| NOTIFIED_PROCEDURE_AT | datetime | - | 是 |  |
| NOTIFIED_PROCEDURE_DESC | nvarchar(256) | 256 | 是 |  |
| PROCESSOR_ID | numeric(19,0) | - | 是 |  |
| PROCESSOR_NO | nvarchar(16) | 16 | 是 |  |
| PROCESSOR_NAME | nvarchar(32) | 32 | 是 |  |
| PROCESSED_AT | datetime | - | 是 |  |
| PROCEDURE_DESC | nvarchar(256) | 256 | 是 |  |
| REVIEWER_BY | numeric(19,0) | - | 是 |  |
| REVIEWER_NAME | nvarchar(64) | 64 | 是 |  |
| REVIEWER_NO | varchar(32) | 32 | 是 |  |
| REVIEW_AT | datetime | - | 是 |  |
| REVIEW_DESC | nvarchar(256) | 256 | 是 |  |
| APPLY_BY | numeric(19,0) | - | 是 |  |
| APPLY_NAME | nvarchar(64) | 64 | 是 |  |
| APPLY_AT | datetime | - | 是 |  |
| PUBLISH_BY | numeric(19,0) | - | 是 |  |
| PUBLISH_NAME | nvarchar(64) | 64 | 是 |  |
| PUBLISH_AT | datetime | - | 是 |  |
| DEPT_CONFIRM_BY | numeric(19,0) | - | 是 |  |
| DEPT_CONFIRM_NAME | nvarchar(64) | 64 | 是 |  |
| DEPT_CONFIRM_AT | datetime | - | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |

---

## EXAM_DATABASE_SCRIPT

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_DATABASE_SCRIPT_ID | numeric(19,0) | - | 否 | ✓ |
| VERSION_TIME_STAMP | numeric(19,0) | - | 是 |  |
| FILE_PATH | varchar(200) | 200 | 是 |  |
| SCRIPT_TYPE_NO | varchar(100) | 100 | 是 |  |
| SCRIPT_RUN_STATUS_NO | varchar(100) | 100 | 是 |  |
| SCRIPT_CONTENT | nvarchar(-1) | -1 | 是 |  |
| SCRIPT_COMMENTS | nvarchar(-1) | -1 | 是 |  |
| SCRIPT_ERROR | nvarchar(-1) | -1 | 是 |  |
| SYSTEM_SOURCE_NO | varchar(64) | 64 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |

---

## EXAM_DIAG_TREAT

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_DIAG_TREAT_ID | numeric(19,0) | - | 否 | ✓ |
| EXAM_TASK_ID | numeric(19,0) | - | 否 |  |
| DIAG_TREAT_STATUS_NO | varchar(256) | 256 | 否 |  |
| SEQ_NO | int(10,0) | - | 否 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 否 |  |
| CREATED_NAME | nvarchar(64) | 64 | 否 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | varchar(64) | 64 | 否 |  |
| SYSTEM_SOURCE_NO | varchar(64) | 64 | 否 |  |

---

## EXAM_DIAG_TREAT_ELEMENT

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_DIAG_TREAT_ELEMENT_ID | numeric(19,0) | - | 否 | ✓ |
| PARENT_ITEM_ID | numeric(19,0) | - | 是 |  |
| EXAM_ELEMENT_ITEM_ID | numeric(19,0) | - | 否 |  |
| ITEM_PROPERTY | nvarchar(32) | 32 | 是 |  |
| MODULE_TYPE_NO | varchar(32) | 32 | 是 |  |
| EXAM_CATEGORY_NO | varchar(32) | 32 | 是 |  |
| EXAM_ITEM_NO | varchar(32) | 32 | 是 |  |
| MODULE_TYPE_NAME | nvarchar(64) | 64 | 是 |  |
| EXAM_CATEGORY_NAME | nvarchar(64) | 64 | 是 |  |
| EXAM_ITEM_NAME | nvarchar(64) | 64 | 是 |  |
| LOGIC_RELATE | nvarchar(-1) | -1 | 是 |  |
| ENABLE_FLAG | numeric(19,0) | - | 否 |  |
| SEQ_NO | int(10,0) | - | 否 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 否 |  |
| CREATED_NAME | nvarchar(64) | 64 | 否 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | varchar(64) | 64 | 否 |  |
| SYSTEM_SOURCE_NO | varchar(64) | 64 | 否 |  |

---

## EXAM_DIAG_TREAT_RESULT

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_DIAG_TREAT_RESULT_ID | numeric(19,0) | - | 否 | ✓ |
| EXAM_DIAG_TREAT_ID | numeric(19,0) | - | 否 |  |
| EXAM_ELEMENT_ITEM_ID | numeric(19,0) | - | 否 |  |
| DIAG_TREAT_ITEM_NO | varchar(32) | 32 | 否 |  |
| SEQ_NO | int(10,0) | - | 否 |  |
| DIAG_TREAT_ITEM_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| DIAG_TREAT_ITEM_RESULT_NO | varchar(32) | 32 | 是 |  |
| CREATED_AT | datetime | - | 否 |  |
| DIAG_TREAT_ITEM_RESULT | nvarchar(128) | 128 | 是 |  |
| CREATED_BY | numeric(19,0) | - | 否 |  |
| CREATED_NAME | nvarchar(64) | 64 | 否 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | varchar(64) | 64 | 否 |  |
| SYSTEM_SOURCE_NO | varchar(64) | 64 | 否 |  |

---

## EXAM_DICTIONARY

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_DICTIONARY_ID | numeric(19,0) | - | 否 |  |
| SYSTEM_SOURCE_NO | nvarchar(20) | 20 | 否 |  |
| DICT_CLASS_NAME | nvarchar(40) | 40 | 是 |  |
| DICT_ITEM_NO | nvarchar(40) | 40 | 是 |  |
| DICT_ITEM_NAME | nvarchar(64) | 64 | 是 |  |
| DICT_EXTERN_NO | nvarchar(50) | 50 | 是 |  |
| MEM1_NO | nvarchar(30) | 30 | 是 |  |
| MEM2_NO | nvarchar(30) | 30 | 是 |  |
| ALTER_FLAG | numeric(19,0) | - | 是 |  |
| VISIBLE_FLAG | numeric(19,0) | - | 是 |  |
| SEQ_NO | int(10,0) | - | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |

---

## EXAM_DOC_TASK

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_DOC_TASK_ID | numeric(19,0) | - | 否 | ✓ |
| EXAM_TASK_ID | numeric(19,0) | - | 否 |  |
| DOC_SCHEDULE_DIC_ID | numeric(19,0) | - | 是 |  |
| SYSTEM_SOURCE_NO | nvarchar(32) | 32 | 是 |  |
| TASK_EXECUTOR_BY | numeric(19,0) | - | 是 |  |
| TASK_ASSIGNMENT_AT | datetime | - | 是 |  |
| DOC_TASK_STATUS | numeric(19,0) | - | 是 |  |
| EXAM_TASK_STATUS | numeric(19,0) | - | 是 |  |
| DOC_TASK_WORKLOAD | numeric(19,2) | - | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |

---

## EXAM_ELEMENT_ITEM

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_ELEMENT_ITEM_ID | numeric(19,0) | - | 否 | ✓ |
| ELEMENT_ITEM_GROUP_NAME | nvarchar(128) | 128 | 是 |  |
| BIND_MODEL_VALUE | varchar(64) | 64 | 是 |  |
| BIND_MODEL_NAME | nvarchar(256) | 256 | 是 |  |
| ELEMENT_ITEM_TYPE_NO | nvarchar(64) | 64 | 是 |  |
| ELEMENT_ITEM_DEFAULT_VALUE | nvarchar(64) | 64 | 是 |  |
| DATA_SOURCE_TYPE_NO | nvarchar(64) | 64 | 是 |  |
| DATA_SOURCE_FUNCTION | nvarchar(-1) | -1 | 是 |  |
| ELEMENT_ITEM_RELATION_SET | nvarchar(256) | 256 | 是 |  |
| ELEMENT_ITEM_RELATION_DEFAULT | nvarchar(32) | 32 | 是 |  |
| ELEMENT_ITEM_REGEX_TEXT | nvarchar(1024) | 1024 | 是 |  |
| TRIGGER_EVENT_TYPE_NO | varchar(64) | 64 | 是 |  |
| TRIGGER_EVENT_FUNCTION | nvarchar(-1) | -1 | 是 |  |
| ELEMENT_ITEM_CSS | nvarchar(-1) | -1 | 是 |  |
| IS_FILTER | numeric(19,0) | - | 是 |  |
| IS_READ_ONLY | numeric(19,0) | - | 是 |  |
| REQUIRED_FLAG | numeric(19,0) | - | 是 |  |
| SEQ_NO | int(10,0) | - | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 否 |  |
| CREATED_NAME | nvarchar(40) | 40 | 否 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(40) | 40 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(40) | 40 | 是 |  |
| SYSTEM_SOURCE_NO | nvarchar(32) | 32 | 是 |  |
| ELEMENT_ITEM_NAME | nvarchar(32) | 32 | 是 |  |
| ELEMENT_ITEM_PLACEHOLDER | nvarchar(100) | 100 | 是 |  |
| MODEL_SOURCE | nvarchar(64) | 64 | 是 |  |
| IS_DICTIONARY | numeric(19,0) | - | 是 |  |
| IS_INTERFACE | numeric(19,0) | - | 是 |  |
| ELEMENT_ITEM_DATA_PROPERTY | nvarchar(32) | 32 | 是 |  |
| BIND_MODEL_KEY | nvarchar(64) | 64 | 是 |  |
| ELEMENT_ITEM_BOUNDARY | nvarchar(128) | 128 | 是 |  |
| ELEMENT_ITEM_NO | varchar(32) | 32 | 是 |  |

---

## EXAM_ENCOUNTER

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_ENCOUNTER_ID | numeric(19,0) | - | 否 | ✓ |
| BIZ_ROLE_ID | numeric(19,0) | - | 是 |  |
| PATIENT_NO | nvarchar(64) | 64 | 是 |  |
| ENCOUNTER_NO | nvarchar(64) | 64 | 是 |  |
| ENC_REG_SEQ_NO | nvarchar(64) | 64 | 是 |  |
| SYSTEM_SOURCE_NO | nvarchar(32) | 32 | 是 |  |
| EXAM_ENCOUNTER_AT | datetime | - | 是 |  |
| MOTHER_ENCOUNTER_NO | varchar(64) | 64 | 是 |  |
| ESS_NO | varchar(256) | 256 | 是 |  |
| ESS_NAME | nvarchar(256) | 256 | 是 |  |
| ESS_CATEGORY_NO | varchar(64) | 64 | 是 |  |
| ESS_CATEGORY_NAME | nvarchar(64) | 64 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |

---

## EXAM_EQUIPMENT

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_EQUIPMENT_ID | numeric(19,0) | - | 否 | ✓ |
| EQUIPMENT_NAME | nvarchar(50) | 50 | 是 |  |
| EQUIPMENT_ADDRESS | nvarchar(200) | 200 | 是 |  |
| MODALITY_NO | nvarchar(64) | 64 | 是 |  |
| EQUIPMENT_HOSTNAME | nvarchar(256) | 256 | 是 |  |
| EQUIPMENT_IP | nvarchar(20) | 20 | 是 |  |
| EQUIPMENT_PORT | nvarchar(10) | 10 | 是 |  |
| LICENSE_NO | nvarchar(255) | 255 | 是 |  |
| SYSTEM_SOURCE_NO | nvarchar(32) | 32 | 否 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| EQUIPMENT_WAITING_COUNT | int(10,0) | - | 是 |  |
| VISIBLE_FLAG | numeric(19,0) | - | 是 |  |
| SEQ_NO | int(10,0) | - | 是 |  |
| MOBILE_EQUIPMENT_TYPE | nvarchar(64) | 64 | 是 |  |
| MOBILE_EQUIPMENT_TYPE_NO | varchar(64) | 64 | 是 |  |
| EQUIPMENT_OTHER_PROPERTY | varchar(512) | 512 | 是 |  |

---

## EXAM_EQUIPMENT_OPERATION

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_EQUIPMENT_OPERATION_ID | numeric(19,0) | - | 否 | ✓ |
| SYSTEM_SOURCE_NO | nvarchar(32) | 32 | 是 |  |
| EXAM_EQUIPMENT_ID | numeric(19,0) | - | 否 |  |
| ENABLED_FLAG | numeric(19,0) | - | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| WORKING_USER_BY | numeric(19,0) | - | 是 |  |
| EQUIPMENT_OPERATION_ELEMENT | varchar(4000) | 4000 | 是 |  |

---

## EXAM_EQUIPMENT_TABOO_ITEMS

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_EQUIPMENT_TABOO_ITEMS_ID | numeric(19,0) | - | 否 | ✓ |
| EXAM_EQUIPMENT_ID | numeric(19,0) | - | 否 |  |
| EXAM_ITEM_DIC_ID | numeric(19,0) | - | 否 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |
| SYSTEM_SOURCE_NO | nvarchar(32) | 32 | 是 |  |

---

## EXAM_FILM_FEE_DIC

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_FILM_FEE_DIC_ID | numeric(19,0) | - | 否 | ✓ |
| EXAM_FILM_FEE_NO | varchar(128) | 128 | 是 |  |
| EXAM_FILM_FEE_NAME | varchar(128) | 128 | 是 |  |
| EXAM_FILM_FEE_QTY | numeric(19,2) | - | 是 |  |
| EXAM_FILM_FEE_UNIT | nvarchar(64) | 64 | 是 |  |
| EXAM_FILM_FEE_SPEC | varchar(256) | 256 | 是 |  |
| VISIBLE_FLAG | numeric(19,0) | - | 是 |  |
| SEQ_NO | smallint(5,0) | - | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 否 |  |
| CREATED_NAME | nvarchar(64) | 64 | 否 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |
| SYSTEM_SOURCE_NO | varchar(64) | 64 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |

---

## EXAM_FOLLOW_UP

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_FOLLOW_UP_ID | numeric(19,0) | - | 否 | ✓ |
| EXAM_TASK_ID | numeric(19,0) | - | 否 |  |
| EXAM_TASK_ASSIGNMENT_ID | numeric(19,0) | - | 否 |  |
| SYSTEM_SOURCE_NO | nvarchar(32) | 32 | 是 |  |
| EXAM_CATEGORY_ID | numeric(19,0) | - | 是 |  |
| NEG_POS_CODE | nvarchar(10) | 10 | 是 |  |
| FOLLOW_UP_AT | datetime | - | 是 |  |
| FOLLOW_UP_BY | numeric(19,0) | - | 是 |  |
| FOLLOW_UP_NAME | nvarchar(64) | 64 | 是 |  |
| FOLLOW_UP_DESC | nvarchar(-1) | -1 | 是 |  |
| FOLLOW_UP_RESULT | nvarchar(64) | 64 | 是 |  |
| FOLLOW_UP_POSITION_REMARK | nvarchar(1000) | 1000 | 是 |  |
| FOLLOW_UP_QUALITATIVE_REMARK | nvarchar(1000) | 1000 | 是 |  |
| FOLLOW_UP_PATHOLOGIC_DIAGNOSIS | nvarchar(1000) | 1000 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(64) | 64 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |

---

## EXAM_FOLLOW_UP_ELEMENT

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_FOLLOW_UP_ELEMENT_ID | numeric(19,0) | - | 否 | ✓ |
| EXAM_FOLLOW_UP_ID | numeric(19,0) | - | 否 |  |
| FOLLOW_UP_ELEMENT_NO | nvarchar(64) | 64 | 是 |  |
| FOLLOW_UP_ELEMENT_VALUE | nvarchar(-1) | -1 | 是 |  |
| REFERENCE_FLAG | numeric(19,0) | - | 是 |  |
| REFERENCE_REPORT_NO | nvarchar(64) | 64 | 是 |  |
| REFERENCE_REPORT_CATEGORY | nvarchar(64) | 64 | 是 |  |
| SEQ_NO | smallint(5,0) | - | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(64) | 64 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |

---

## EXAM_HISTORY

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_HISTORY_ID | numeric(19,0) | - | 否 | ✓ |
| SYSTEM_SOURCE_NO | nvarchar(32) | 32 | 是 |  |
| FROM_ID | numeric(19,0) | - | 是 |  |
| HISTORY_TYPE | nvarchar(100) | 100 | 是 |  |
| HISTORY_VALUE | nvarchar(-1) | -1 | 是 |  |
| HISTORY_SOURCE_NO | varchar(100) | 100 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |

---

## EXAM_HISTORY_20260416BAK

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_HISTORY_ID | numeric(19,0) | - | 否 |  |
| SYSTEM_SOURCE_NO | nvarchar(32) | 32 | 是 |  |
| FROM_ID | numeric(19,0) | - | 是 |  |
| HISTORY_TYPE | nvarchar(100) | 100 | 是 |  |
| HISTORY_VALUE | nvarchar(-1) | -1 | 是 |  |
| HISTORY_SOURCE_NO | varchar(100) | 100 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |

---

## EXAM_HOLIDAY

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_HOLIDAY_ID | numeric(19,0) | - | 否 |  |
| HOLIDAY_DATE | date | - | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(128) | 128 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(128) | 128 | 是 |  |

---

## EXAM_IMAGE_INDEX_RESULT

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_IMAGE_INDEX_RESULT_ID | numeric(19,0) | - | 否 | ✓ |
| PPS_STUDYUID | varchar(128) | 128 | 是 |  |
| IMAGE_INDEX_NO | varchar(128) | 128 | 是 |  |
| IMAGE_INDEX_NAME | nvarchar(128) | 128 | 是 |  |
| IMAGE_INDEX_VALUE | nvarchar(256) | 256 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 否 |  |
| CREATED_NAME | nvarchar(64) | 64 | 否 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |
| SYSTEM_SOURCE_NO | varchar(64) | 64 | 否 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 否 |  |

---

## EXAM_IMG_FILM

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_IMG_FILM_ID | numeric(19,0) | - | 否 | ✓ |
| MODALITY_PERF_PROC_STEP_ID | numeric(19,0) | - | 否 |  |
| FILM_STUDY_UID | nvarchar(128) | 128 | 是 |  |
| FILM_SERIES_UID | nvarchar(128) | 128 | 是 |  |
| FILM_IMAGE_UID | nvarchar(128) | 128 | 是 |  |
| FILM_TYPE_CODE | numeric(19,0) | - | 是 |  |
| FILM_SIZE_NO | nvarchar(64) | 64 | 是 |  |
| FILM_COUNT | int(10,0) | - | 是 |  |
| FILM_PRINT_STATUS | numeric(19,0) | - | 是 |  |
| FILM_PRINT_TIMES | int(10,0) | - | 是 |  |
| FILM_PRINT_AT | datetime | - | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |

---

## EXAM_IMG_RELATIONSHIP

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_IMG_RELATIONSHIP_ID | numeric(19,0) | - | 否 | ✓ |
| MODALITY_PERF_PROC_STEP_ID | numeric(19,0) | - | 否 |  |
| EXAM_TASK_ID | numeric(19,0) | - | 否 |  |
| SYSTEM_SOURCE_NO | nvarchar(32) | 32 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |

---

## EXAM_ITEM_BUSINESS_GROUP

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_ITEM_BUSINESS_GROUP_ID | numeric(19,0) | - | 否 | ✓ |
| ITEM_ID | numeric(19,0) | - | 否 |  |
| BUSINESS_TYPE_NO | varchar(64) | 64 | 是 |  |
| BUSINESS_TYPE_NAME | nvarchar(256) | 256 | 是 |  |
| BUSINESS_GROUP_NO | varchar(64) | 64 | 是 |  |
| BUSINESS_GROUP_NAME | nvarchar(256) | 256 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 是 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| SYSTEM_SOURCE_NO | varchar(64) | 64 | 是 |  |
| IS_DEL | smallint(5,0) | - | 是 |  |
| CREATED_AT | datetime | - | 是 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |

---

## EXAM_ITEM_DIC

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_ITEM_DIC_ID | numeric(19,0) | - | 否 | ✓ |
| DIAG_TEMPLATE_TREE_ID | numeric(19,0) | - | 是 |  |
| EXAM_CATEGORY_ID | numeric(19,0) | - | 是 |  |
| EXAM_ITEM_NO | nvarchar(50) | 50 | 是 |  |
| EXAM_ITEM_NAME | nvarchar(50) | 50 | 是 |  |
| EXAM_ITEM_GROUP | nvarchar(64) | 64 | 是 |  |
| LOCAL_ITEM_FLAG | numeric(19,0) | - | 是 |  |
| POSITION_NO | nvarchar(64) | 64 | 是 |  |
| RADIATION_TIMES | int(10,0) | - | 是 |  |
| EXTERN_NO | nvarchar(50) | 50 | 是 |  |
| SYSTEM_SOURCE_NO | nvarchar(32) | 32 | 否 |  |
| VISIBLE_FLAG | numeric(19,0) | - | 是 |  |
| SEQ_NO | int(10,0) | - | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| ITEM_GROUP_SEQ_NO | int(10,0) | - | 是 |  |
| EXTERN_ITEM_TYPE_NO | nvarchar(50) | 50 | 是 |  |
| EXAM_ITEM_ATTRIBUTE_TAG | nvarchar(256) | 256 | 是 |  |
| CHILDREN_ITEM_IDS | varchar(256) | 256 | 是 |  |
| EXAM_ITEM_SECOND_NAME | nvarchar(64) | 64 | 是 |  |
| EXAM_CATEGORY_IDS | varchar(500) | 500 | 是 |  |
| PROBE_FREQUENCY_NAME | nvarchar(128) | 128 | 是 |  |
| MEM1_NO | nvarchar(30) | 30 | 是 |  |
| MEM2_NO | nvarchar(30) | 30 | 是 |  |
| PROBE_FREQUENCY_NO | varchar(64) | 64 | 是 |  |
| IMAGE_BODY_PART | nvarchar(100) | 100 | 是 |  |
| INSPECTION_ROUTE_NO | nvarchar(64) | 64 | 是 |  |
| INSPECTION_ROUTE_NAME | nvarchar(128) | 128 | 是 |  |

---

## EXAM_ITEM_DIC_METHOD

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_ITEM_DIC_METHOD_ID | numeric(19,0) | - | 否 | ✓ |
| EXAM_ITEM_DIC_ID | numeric(19,0) | - | 否 |  |
| EXAM_METHOD_ID | numeric(19,0) | - | 否 |  |
| SEQ_NO | int(10,0) | - | 是 |  |
| DEFAULT_FLAG | numeric(19,0) | - | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| SYSTEM_SOURCE_NO | nvarchar(50) | 50 | 否 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |

---

## EXAM_ITEM_PROTOCOL

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_ITEM_PROTOCOL_ID | numeric(19,0) | - | 否 | ✓ |
| WORKLIST_RULE_ID | numeric(19,0) | - | 否 |  |
| EXAM_ITEM_DIC_ID | numeric(19,0) | - | 否 |  |
| PROTOCOL_NO | nvarchar(50) | 50 | 是 |  |
| PROTOCOL_MEANING | nvarchar(50) | 50 | 是 |  |
| PROTOCOL_DESIGNATOR | nvarchar(50) | 50 | 是 |  |
| EQUIPMENT_VERSION | nvarchar(50) | 50 | 是 |  |
| CHARACTERSET_CODE | numeric(19,0) | - | 是 |  |
| VISIBLE_FLAG | numeric(19,0) | - | 是 |  |
| SEQ_NO | int(10,0) | - | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |

---

## EXAM_ITEM_WORKLOAD_WEIGHT

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_ITEM_WORKLOAD_WEIGHT_ID | numeric(19,0) | - | 否 | ✓ |
| EXAM_ITEM_DIC_ID | numeric(19,0) | - | 否 |  |
| SYSTEM_SOURCE_NO | nvarchar(32) | 32 | 是 |  |
| EXAM_TASK_STATUS | numeric(19,0) | - | 是 |  |
| EXAM_ITEM_WEIGHT | numeric(19,2) | - | 是 |  |
| FUNCTION_POSITION_NO | varchar(128) | 128 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |

---

## EXAM_LOGIC_RULE

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_LOGIC_RULE_ID | numeric(19,0) | - | 否 | ✓ |
| EXAM_LOGIC_RULE_NO | nvarchar(64) | 64 | 是 |  |
| EXAM_LOGIC_RULE_NAME | nvarchar(256) | 256 | 是 |  |
| EXAM_LOGIC_RULE_CONDITION | nvarchar(-1) | -1 | 是 |  |
| ENABLED_FLAG | numeric(19,0) | - | 是 |  |
| SEQ_NO | int(10,0) | - | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| SYSTEM_SOURCE_NO | nvarchar(32) | 32 | 是 |  |
| CURRENT_NUMBER | int(10,0) | - | 是 |  |

---

## EXAM_MDM_DATA_DIC

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_MDM_DATA_DIC_ID | numeric(19,0) | - | 否 | ✓ |
| SERVICE_CODE_NO | varchar(64) | 64 | 是 |  |
| SERVICE_NAME | varchar(64) | 64 | 是 |  |
| MDM_DATA_KEY | varchar(64) | 64 | 是 |  |
| MDM_DATA_NO | varchar(64) | 64 | 是 |  |
| MDM_DATA_NAME | varchar(64) | 64 | 是 |  |
| MDM_TYPE_NO | varchar(64) | 64 | 是 |  |
| GROUP_NO | varchar(64) | 64 | 是 |  |
| GROUP_NAME | varchar(64) | 64 | 是 |  |
| CLASS_NO | varchar(64) | 64 | 是 |  |
| CLASS_NAME | varchar(64) | 64 | 是 |  |
| EXTERN1_NO | varchar(64) | 64 | 是 |  |
| EXTERN2_NO | varchar(64) | 64 | 是 |  |
| MEM1_NO | varchar(64) | 64 | 是 |  |
| MEM2_NO | varchar(64) | 64 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| SYSTEM_SOURCE_NO | varchar(64) | 64 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| ITEM_UNIT | nvarchar(64) | 64 | 是 |  |
| ITEM_PRICE | numeric(19,2) | - | 是 |  |
| ITEM_SPEC | varchar(256) | 256 | 是 |  |
| MEMO | varchar(256) | 256 | 是 |  |
| COUNTRY_NO | nvarchar(128) | 128 | 是 |  |
| COUNTRY_NO_NAME | nvarchar(256) | 256 | 是 |  |
| CS_ID | numeric(19,0) | - | 是 |  |
| CS_NAME | nvarchar(64) | 64 | 是 |  |

---

## EXAM_MDM_DATA_RELATE

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_MDM_DATA_RELATE_ID | numeric(19,0) | - | 否 | ✓ |
| MDM_DATA_NO | varchar(128) | 128 | 否 |  |
| RELATE_DATA_NO | varchar(128) | 128 | 否 |  |
| RELATE_ITEM_QTY | numeric(19,2) | - | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 否 |  |
| CREATED_NAME | nvarchar(64) | 64 | 否 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |
| SYSTEM_SOURCE_NO | varchar(64) | 64 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |

---

## EXAM_MED_SUPP_RECORD

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_MED_SUPP_RECORD_ID | numeric(19,0) | - | 否 | ✓ |
| EXAM_TASK_ID | numeric(19,0) | - | 否 |  |
| SUPPLY_TYPE_NO | varchar(32) | 32 | 是 |  |
| SUPPLY_ID | numeric(19,0) | - | 是 |  |
| SUPPLY_NO | varchar(64) | 64 | 是 |  |
| SUPPLY_NAME | nvarchar(128) | 128 | 是 |  |
| SPECIFICATION | varchar(64) | 64 | 是 |  |
| UNIT | nvarchar(64) | 64 | 是 |  |
| USAGE_QUANTITY | int(10,0) | - | 是 |  |
| PRICE | numeric(16,6) | - | 是 |  |
| USAGE_TIME | datetime | - | 是 |  |
| USAGE_PERSON_NO | varchar(32) | 32 | 是 |  |
| USAGE_PERSON_NAME | nvarchar(64) | 64 | 是 |  |
| USAGE_WAY | nvarchar(128) | 128 | 是 |  |
| OPERATOR_NO | varchar(32) | 32 | 是 |  |
| OPERATOR_NAME | nvarchar(64) | 64 | 是 |  |
| OPERATE_TIME | datetime | - | 是 |  |
| REMARK | nvarchar(256) | 256 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |
| SYSTEM_SOURCE_NO | nvarchar(32) | 32 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |

---

## EXAM_MESSAGE

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_MESSAGE_ID | numeric(19,0) | - | 否 | ✓ |
| MESSAGE_TYPE_NO | varchar(100) | 100 | 是 |  |
| MESSAGE_CONTENT | nvarchar(-1) | -1 | 是 |  |
| MESSAGE_REMIND_PRIORITY | int(10,0) | - | 是 |  |
| MESSAGE_OBJECT_NO | varchar(100) | 100 | 是 |  |
| REMIND_TYPE_NO | varchar(100) | 100 | 是 |  |
| REMIND_APPOINTED_FLAG | numeric(19,0) | - | 是 |  |
| REMIND_APPOINTED_AT | datetime | - | 是 |  |
| MESSAGE_SOURCE_OBJECT | varchar(128) | 128 | 是 |  |
| MESSAGE_INITIATE_BY | numeric(19,0) | - | 是 |  |
| MESSAGE_INITIATE_AT | datetime | - | 是 |  |
| SYSTEM_SOURCE_NO | varchar(32) | 32 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(40) | 40 | 是 |  |

---

## EXAM_MESSAGE_DETAILS

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_MESSAGE_DETAILS_ID | numeric(19,0) | - | 否 | ✓ |
| EXAM_MESSAGE_ID | numeric(19,0) | - | 是 |  |
| MESSAGE_RECEIVE_OBJECT | varchar(256) | 256 | 是 |  |
| MESSAGE_REMIND_AT | datetime | - | 是 |  |
| MESSAGE_READ_FLAG | numeric(19,0) | - | 是 |  |
| MESSAGE_READ_AT | datetime | - | 是 |  |
| SYSTEM_SOURCE_NO | varchar(32) | 32 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |

---

## EXAM_METHOD

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_METHOD_ID | numeric(19,0) | - | 否 | ✓ |
| EXAM_CATEGORY_ID | numeric(19,0) | - | 是 |  |
| SYSTEM_SOURCE_NO | nvarchar(32) | 32 | 是 |  |
| METHOD_DESC | nvarchar(1024) | 1024 | 是 |  |
| VISIBLE_FLAG | numeric(19,0) | - | 是 |  |
| DEFAULT_FLAG | numeric(19,0) | - | 是 |  |
| SEQ_NO | int(10,0) | - | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |

---

## EXAM_MIGRATION_FAIL

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_MIGRATION_FAIL_ID | numeric(19,0) | - | 否 | ✓ |
| EXAM_MIGRATION_TASK_ID | numeric(19,0) | - | 是 |  |
| EXAM_MIGRATION_TASK_NAME | nvarchar(64) | 64 | 是 |  |
| MIGRATION_TASK_GROUP_ID | numeric(19,0) | - | 是 |  |
| EXAM_REPORT_DATE | date | - | 是 |  |
| SERIAL_NUMBER | varchar(64) | 64 | 是 |  |
| FAIL_REASON | nvarchar(2000) | 2000 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |

---

## EXAM_MIGRATION_RELATIONSHIP

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_MIGRATION_RELATIONSHIP_ID | numeric(19,0) | - | 否 | ✓ |
| EXAM_TASK_ID | numeric(19,0) | - | 是 |  |
| SERIAL_NUMBER | varchar(64) | 64 | 是 |  |
| SYSTEM_SOURCE_NO | varchar(32) | 32 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |

---

## EXAM_MIGRATION_TASK

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_MIGRATION_TASK_ID | numeric(19,0) | - | 否 | ✓ |
| EXAM_MIGRATION_TASK_NAME | nvarchar(256) | 256 | 是 |  |
| MIGRATION_TASK_GROUP_ID | numeric(19,0) | - | 是 |  |
| MIGRATION_TASK_STATUS_NO | varchar(64) | 64 | 是 |  |
| REPORT_TOTAL | numeric(19,0) | - | 是 |  |
| FINISH_TOTAL | numeric(19,0) | - | 是 |  |
| TASK_START_AT | datetime | - | 是 |  |
| TASK_FINISH_AT | datetime | - | 是 |  |
| FAIL_TOTAL | numeric(19,0) | - | 是 |  |
| TASK_TIME_SPEND | numeric(19,0) | - | 是 |  |
| EXAM_REPORT_DATE | date | - | 是 |  |
| REMARK | nvarchar(2000) | 2000 | 是 |  |
| SYSTEM_SOURCE_NO | varchar(32) | 32 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |

---

## EXAM_MIRROR

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_MIRROR_ID | numeric(19,0) | - | 否 | ✓ |
| EXAM_TASK_ID | numeric(19,0) | - | 否 |  |
| EXAM_MIRROR_CARD_NO | varchar(64) | 64 | 是 |  |
| EXAM_MIRROR_NO | varchar(64) | 64 | 是 |  |
| EXAM_MIRROR_NAME | nvarchar(64) | 64 | 是 |  |
| EXAM_MIRROR_STATUS_NO | varchar(64) | 64 | 是 |  |
| CLEANED_BY | numeric(19,0) | - | 是 |  |
| CLEANED_NAME | nvarchar(64) | 64 | 是 |  |
| CLEANED_START_AT | datetime | - | 是 |  |
| CLEANED_END_AT | datetime | - | 是 |  |
| CLEANED_TIME_LENGTH | nvarchar(32) | 32 | 是 |  |
| CLEANED_FLOW_NODES | nvarchar(-1) | -1 | 是 |  |
| MIRROR_USE_REASON_NO | varchar(80) | 80 | 是 |  |
| MIRROR_USE_REASON_DESC | nvarchar(128) | 128 | 是 |  |
| EXAM_TRANCE_NO | varchar(512) | 512 | 是 |  |
| MIRROR_ERROR_MSG | nvarchar(300) | 300 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |
| SYSTEM_SOURCE_NO | varchar(64) | 64 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |

---

## EXAM_MPPS_REQ

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_MPPS_REQ_ID | bigint(19,0) | - | 否 | ✓ |
| STUDY_UID | varchar(128) | 128 | 否 |  |
| ACCESSION_NO | varchar(64) | 64 | 是 |  |
| PATIENT_NO | varchar(64) | 64 | 是 |  |
| MODALITY_NO | varchar(64) | 64 | 是 |  |
| IMAGE_STUDY_DATETIME | datetime | - | 否 |  |
| IMAGE_DB_DATETIME | datetime | - | 否 |  |
| IMAGE_COUNT | int(10,0) | - | 否 |  |
| STUDY_DESC | varchar(128) | 128 | 是 |  |
| IMAGE_BODY_PART | varchar(128) | 128 | 是 |  |
| IMAGE_EQUIPMENT | varchar(64) | 64 | 是 |  |
| PERF_PHYSICIAN_NAME | nvarchar(64) | 64 | 是 |  |
| PATIENT_NAME | nvarchar(64) | 64 | 是 |  |
| HOSPITAL_NO | varchar(32) | 32 | 否 |  |
| DEVICE_MANUFACTURE | varchar(128) | 128 | 是 |  |
| DEVICE_MODEL | varchar(128) | 128 | 是 |  |
| DEVICE_NO | varchar(128) | 128 | 是 |  |
| OPERATE_TYPE_NO | varchar(32) | 32 | 否 |  |
| SERIES_UID | varchar(128) | 128 | 是 |  |
| IMAGE_UID | varchar(128) | 128 | 是 |  |
| FILE_PATH | varchar(256) | 256 | 是 |  |
| FRAME_TOTAL | int(10,0) | - | 是 |  |
| ERROR_MESSAGE | nvarchar(-1) | -1 | 是 |  |
| CREATED_BY | numeric(19,0) | - | 否 |  |
| CREATED_NAME | nvarchar(64) | 64 | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| SYSTEM_SOURCE_NO | varchar(32) | 32 | 否 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| MPPS_EXEC_FLAG | numeric(19,0) | - | 是 |  |
| RUN_STATUS_NO | nvarchar(64) | 64 | 是 |  |
| IMAGE_WIDTH | int(10,0) | - | 是 |  |
| IMAGE_HEIGHT | int(10,0) | - | 是 |  |

---

## EXAM_MUTUAL_ITEM

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_MUTUAL_ITEM_ID | numeric(19,0) | - | 否 | ✓ |
| EXAM_MUTUAL_ITEM_NO | varchar(256) | 256 | 是 |  |
| EXAM_MUTUAL_ITEM_NAME | nvarchar(256) | 256 | 是 |  |
| EXAM_CATEGORY_NO | varchar(256) | 256 | 是 |  |
| EXAM_MUTUAL_LEVEL_NO | varchar(256) | 256 | 是 |  |
| SEQ_NO | int(10,0) | - | 是 |  |
| SYSTEM_SOURCE_NO | varchar(64) | 64 | 否 |  |
| EXAM_MUTUAL_ITEM_ATTRIBUTE_TAG | nvarchar(256) | 256 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| EXAM_ITEM_DIC_IDS | nvarchar(512) | 512 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| CREATED_AT | datetime | - | 否 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |

---

## EXAM_OTHER_RESULT

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_OTHER_RESULT_ID | numeric(19,0) | - | 否 | ✓ |
| EXAM_TASK_ID | numeric(19,0) | - | 否 |  |
| SYSTEM_SOURCE_NO | nvarchar(32) | 32 | 是 |  |
| REPORT_ITEM_NO | nvarchar(32) | 32 | 是 |  |
| REPORT_ITEM_NAME | nvarchar(64) | 64 | 是 |  |
| REPORT_ITEM_RESULT | nvarchar(-1) | -1 | 是 |  |
| REPORT_ITEM_REF_VALUE | nvarchar(128) | 128 | 是 |  |
| REPORT_ITEM_STATUS | numeric(19,0) | - | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |

---

## EXAM_PATH_RETROSPECT

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_PATH_RETROSPECT_ID | numeric(19,0) | - | 否 | ✓ |
| EXAM_TASK_ID | numeric(19,0) | - | 否 |  |
| NODE_NO | varchar(64) | 64 | 否 |  |
| NODE_NAME | nvarchar(64) | 64 | 否 |  |
| SEQ_NO | int(10,0) | - | 否 |  |
| NODE_AT | datetime | - | 否 |  |
| NODE_CREATED_AT | datetime | - | 否 |  |
| NODE_STATUS_NO | varchar(64) | 64 | 否 |  |
| NODE_STATUS_NO_DESC | nvarchar(256) | 256 | 是 |  |
| NODE_TYPE_NO | varchar(64) | 64 | 否 |  |
| EXAM_HISTORY_ID | numeric(19,0) | - | 是 |  |
| EXAM_TRIAGE_HISTORY_ID | numeric(19,0) | - | 是 |  |
| EXAM_SCHEDULE_TASK_ID | numeric(19,0) | - | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 否 |  |
| SYSTEM_SOURCE_NO | varchar(32) | 32 | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |

---

## EXAM_PATIENT_TECH_NO

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_PATIENT_TECH_NO_ID | numeric(19,0) | - | 否 | ✓ |
| TECH_NO | nvarchar(20) | 20 | 是 |  |
| BIZ_ROLE_ID | numeric(19,0) | - | 否 |  |
| EXAM_CATEGORY_ID | numeric(19,0) | - | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| SYSTEM_SOURCE_NO | nvarchar(32) | 32 | 是 |  |

---

## EXAM_PERSON

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_PERSON_ID | numeric(19,0) | - | 否 | ✓ |
| FULL_NAME | nvarchar(128) | 128 | 是 |  |
| GENDER_CODE | numeric(19,0) | - | 是 |  |
| BIRTH_DATE | date | - | 是 |  |
| TELEPHONE_NO | varchar(24) | 24 | 是 |  |
| PAT_EMAIL | nvarchar(128) | 128 | 是 |  |
| NATIONALITY_CODE | numeric(19,0) | - | 是 |  |
| NATIONALITY_NAME | nvarchar(30) | 30 | 是 |  |
| NATION_CODE | numeric(19,0) | - | 是 |  |
| NATION_NAME | nvarchar(30) | 30 | 是 |  |
| MARITAL_STATUS_CODE | numeric(19,0) | - | 是 |  |
| MARITAL_STATUS_NAME | nvarchar(10) | 10 | 是 |  |
| RELIGION_CODE | numeric(19,0) | - | 是 |  |
| RELIGION_NAME | nvarchar(20) | 20 | 是 |  |
| PINYIN | varchar(64) | 64 | 是 |  |
| JIANPIN | varchar(64) | 64 | 是 |  |
| WUBI | varchar(64) | 64 | 是 |  |
| OCCUPATION_CODE | numeric(19,0) | - | 是 |  |
| OCCUPATION_NAME | nvarchar(30) | 30 | 是 |  |
| ADDRESS | nvarchar(128) | 128 | 是 |  |
| PORTRAIT_URL | varchar(256) | 256 | 是 |  |
| REAL_NAME_AUTH_LEVEL_CODE | numeric(19,0) | - | 是 |  |
| REAL_NAME_AUTH_LEVEL_NAME | nvarchar(10) | 10 | 是 |  |
| PATIENT_CATEGORY_CODE | numeric(19,0) | - | 是 |  |
| PATIENT_CATEGORY_NAME | nvarchar(10) | 10 | 是 |  |
| MPI | varchar(64) | 64 | 是 |  |
| PATIENT_NO | varchar(32) | 32 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |

---

## EXAM_PERSON_IDENTIFICATION

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_PERSON_IDENTIFICATION_ID | numeric(19,0) | - | 否 | ✓ |
| EXAM_PERSON_ID | numeric(19,0) | - | 否 |  |
| IDCARD_NO | varchar(256) | 256 | 是 |  |
| IDCARD_TYPE_CODE | numeric(19,0) | - | 是 |  |
| ENABLED_FLAG | numeric(19,0) | - | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 是 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 是 |  |
| CREATED_AT | datetime | - | 是 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |

---

## EXAM_PERSON_MEDICAL_NUMBER

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_PERSON_MEDICAL_NUMBER_ID | numeric(19,0) | - | 否 | ✓ |
| EXAM_PERSON_ID | numeric(19,0) | - | 是 |  |
| MEDICAL_NUMBER_TYPE_NO | varchar(40) | 40 | 是 |  |
| MEDICAL_NUMBER | varchar(256) | 256 | 是 |  |
| PASSWORD_HASH | varchar(256) | 256 | 是 |  |
| OR_CODE_NO | varchar(256) | 256 | 是 |  |
| ENABLED_FLAG | numeric(19,0) | - | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |

---

## EXAM_PIS_REQUISITION

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_PIS_REQUISITION_ID | numeric(19,0) | - | 否 | ✓ |
| EXAM_TASK_ID | numeric(19,0) | - | 是 |  |
| APPLY_DOC_NO | varchar(64) | 64 | 是 |  |
| APPLY_DOC_NAME | nvarchar(64) | 64 | 是 |  |
| APPLY_DEPT_ID | numeric(19,0) | - | 是 |  |
| APPLY_DEPT_NO | varchar(64) | 64 | 是 |  |
| APPLY_DEPT_NAME | nvarchar(64) | 64 | 是 |  |
| REQUISITION_CONTENT | nvarchar(-1) | -1 | 是 |  |
| REQUISITION_PRINT_TIMES | int(10,0) | - | 是 |  |
| REQUISITION_PRINT_AT | datetime | - | 是 |  |
| BARCODE_PRINT_TIMES | int(10,0) | - | 是 |  |
| BARCODE_PRINT_AT | datetime | - | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |
| SYSTEM_SOURCE_NO | varchar(64) | 64 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |

---

## EXAM_PRINT

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_PRINT_ID | numeric(19,0) | - | 否 | ✓ |
| EXAM_TASK_ID | numeric(19,0) | - | 否 |  |
| SYSTEM_SOURCE_NO | nvarchar(32) | 32 | 是 |  |
| PRINT_FILE_NO | nvarchar(64) | 64 | 是 |  |
| PRINT_FILE_NAME | nvarchar(100) | 100 | 是 |  |
| EXAM_CATEGORY_PRINTFILE_ID | numeric(19,0) | - | 是 |  |
| PRINTER_BY | numeric(19,0) | - | 是 |  |
| PRINTED_AT | datetime | - | 是 |  |
| EXAM_TASK_STATUS | numeric(19,0) | - | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |

---

## EXAM_QC_ENTRY

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_QC_ENTRY_ID | numeric(19,0) | - | 否 | ✓ |
| EXAM_QC_GROUP_ID | numeric(19,0) | - | 否 |  |
| SYSTEM_SOURCE_NO | nvarchar(32) | 32 | 是 |  |
| QC_ENTRY_NAME | nvarchar(256) | 256 | 是 |  |
| QC_ENTRY_MODE | varchar(50) | 50 | 是 |  |
| QC_ENTRY_SCORE | numeric(19,0) | - | 是 |  |
| QC_ENTRY_DESC | nvarchar(-1) | -1 | 是 |  |
| SEQ_NO | smallint(5,0) | - | 是 |  |
| SCORE_ITEM_TYPE_NO | varchar(128) | 128 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |

---

## EXAM_QC_ENTRY_RULE

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_QC_ENTRY_RULE_ID | numeric(19,0) | - | 否 | ✓ |
| EXAM_QC_ENTRY_ID | numeric(19,0) | - | 否 |  |
| QC_RULE_NO | varchar(64) | 64 | 是 |  |
| DEDUCTION_TYPE_NO | varchar(128) | 128 | 是 |  |
| DEDUCTION_VALUE | numeric(19,0) | - | 是 |  |
| SEQ_NO | int(10,0) | - | 是 |  |
| SYSTEM_SOURCE_NO | varchar(256) | 256 | 是 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |

---

## EXAM_QC_GROUP

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_QC_GROUP_ID | numeric(19,0) | - | 否 | ✓ |
| SYSTEM_SOURCE_NO | nvarchar(32) | 32 | 是 |  |
| QC_GROUP_NAME | nvarchar(64) | 64 | 是 |  |
| QC_GROUP_CATEGORY_NO | nvarchar(64) | 64 | 是 |  |
| QC_GROUP_SCORE_STANDARD_NO | nvarchar(32) | 32 | 是 |  |
| DEFAULT_FLAG | numeric(19,0) | - | 是 |  |
| SEQ_NO | smallint(5,0) | - | 是 |  |
| SCORE_METHOD | varchar(128) | 128 | 是 |  |
| IMAGE_SCORE | nvarchar(-1) | -1 | 是 |  |
| REPORT_SCORE | nvarchar(-1) | -1 | 是 |  |
| ENABLED_FLAG | numeric(19,0) | - | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |

---

## EXAM_QC_GROUP_EVALUATION

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_QC_GROUP_EVALUATION_ID | numeric(19,0) | - | 否 | ✓ |
| EXAM_QC_GROUP_ID | numeric(19,0) | - | 否 |  |
| SYSTEM_SOURCE_NO | nvarchar(32) | 32 | 是 |  |
| QC_GROUP_EVALUATION_NO | nvarchar(64) | 64 | 是 |  |
| LOWER_LIMIT_RELATION | nvarchar(32) | 32 | 是 |  |
| LOWER_LIMIT_VALUE | numeric(19,0) | - | 是 |  |
| UPPER_LIMIT_RELATION | nvarchar(32) | 32 | 是 |  |
| UPPER_LIMIT_VALUE | numeric(19,0) | - | 是 |  |
| QC_GROUP_EVALUATION_DESC | nvarchar(1024) | 1024 | 是 |  |
| SEQ_NO | smallint(5,0) | - | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |

---

## EXAM_QC_ITEM

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_QC_ITEM_ID | numeric(19,0) | - | 否 | ✓ |
| EXAM_QC_ITEM_GROUP_ID | numeric(19,0) | - | 否 |  |
| EXAM_ITEM_DIC_ID | numeric(19,0) | - | 否 |  |
| SYSTEM_SOURCE_NO | nvarchar(32) | 32 | 是 |  |
| EXAM_ITEM_NAME | nvarchar(50) | 50 | 是 |  |
| EXAM_CATEGORY_ID | numeric(19,0) | - | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |

---

## EXAM_QC_ITEM_ENTRY

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_QC_ITEM_ENTRY_ID | numeric(19,0) | - | 否 | ✓ |
| EXAM_QC_ENTRY_ID | numeric(19,0) | - | 否 |  |
| EXAM_QC_ITEM_GROUP_ID | numeric(19,0) | - | 否 |  |
| SYSTEM_SOURCE_NO | nvarchar(32) | 32 | 是 |  |
| QC_ITEM_ENTRY_DESC | nvarchar(-1) | -1 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |

---

## EXAM_QC_ITEM_GROUP

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_QC_ITEM_GROUP_ID | numeric(19,0) | - | 否 | ✓ |
| EXAM_QC_GROUP_ID | numeric(19,0) | - | 否 |  |
| SYSTEM_SOURCE_NO | nvarchar(32) | 32 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |

---

## EXAM_QC_RECORD

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_QC_RECORD_ID | numeric(19,0) | - | 否 | ✓ |
| EXAM_TASK_ID | numeric(19,0) | - | 否 |  |
| SYSTEM_SOURCE_NO | nvarchar(32) | 32 | 是 |  |
| QC_RECORD_LEVEL_NO | nvarchar(64) | 64 | 是 |  |
| QC_RECORD_NODE_NO | nvarchar(64) | 64 | 是 |  |
| EXAM_QC_GROUP_ID | numeric(19,0) | - | 是 |  |
| QC_GROUP_NAME | nvarchar(64) | 64 | 是 |  |
| QC_GROUP_CATEGORY_NO | nvarchar(64) | 64 | 是 |  |
| QC_DOCTOR_BY | numeric(19,0) | - | 是 |  |
| QC_DOCTOR_NAME | nvarchar(32) | 32 | 是 |  |
| QC_AT | datetime | - | 是 |  |
| QC_RECORD_STATUS_NO | nvarchar(32) | 32 | 是 |  |
| QC_RECORD_GROUP_SCORE | numeric(19,0) | - | 是 |  |
| QC_GROUP_EVALUATION_NO | nvarchar(64) | 64 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |

---

## EXAM_QC_RECORD_SCORE

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_QC_RECORD_SCORE_ID | numeric(19,0) | - | 否 | ✓ |
| EXAM_QC_RECORD_ID | numeric(19,0) | - | 否 |  |
| SYSTEM_SOURCE_NO | nvarchar(32) | 32 | 是 |  |
| QC_ENTRY_NAME | nvarchar(256) | 256 | 是 |  |
| QC_ENTRY_MODE | varchar(50) | 50 | 是 |  |
| QC_ENTRY_SCORE | numeric(19,0) | - | 是 |  |
| QC_ITEM_ENTRY_DESC | nvarchar(-1) | -1 | 是 |  |
| QC_ITEM_SCORE_MODE | nvarchar(64) | 64 | 是 |  |
| QC_ITEM_RESULT | numeric(19,0) | - | 是 |  |
| SCORE_ITEM_TYPE_NO | varchar(128) | 128 | 是 |  |
| MAX_DEDUCTION | numeric(19,0) | - | 是 |  |
| SEQ_NO | int(10,0) | - | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |
| QC_ITEM_RESULT_REMARK | nvarchar(128) | 128 | 是 |  |

---

## EXAM_QC_RULE_SCORE

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_QC_RULE_SCORE_ID | numeric(19,0) | - | 否 | ✓ |
| EXAM_QC_RECORD_SCORE_ID | numeric(19,0) | - | 否 |  |
| QC_RULE_NO | varchar(64) | 64 | 是 |  |
| DEDUCTION_TYPE_NO | varchar(128) | 128 | 是 |  |
| DEDUCTION_VALUE | numeric(19,0) | - | 是 |  |
| DEDUCTED_VALUE | numeric(19,0) | - | 是 |  |
| SEQ_NO | int(10,0) | - | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 是 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| SYSTEM_SOURCE_NO | varchar(64) | 64 | 是 |  |
| IS_DEL | smallint(5,0) | - | 是 |  |
| CREATED_AT | datetime | - | 是 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |

---

## EXAM_QC_STATISTICS_RULE

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_QC_STATISTICS_RULE_ID | numeric(19,0) | - | 否 | ✓ |
| RULE_NAME | varchar(128) | 128 | 否 |  |
| RULE_TYPE_NO | varchar(32) | 32 | 否 |  |
| RULE_DESC | nvarchar(-1) | -1 | 否 |  |
| ENABLE_FLAG | numeric(19,0) | - | 否 |  |
| PERCENT_FLAG | numeric(19,0) | - | 否 |  |
| NUMERATOR_NAME | nvarchar(128) | 128 | 否 |  |
| DENOMINATOR_NAME | nvarchar(128) | 128 | 否 |  |
| NUMERATOR_TYPE_NO | varchar(32) | 32 | 否 |  |
| DENOMINATOR_TYPE_NO | varchar(32) | 32 | 否 |  |
| NUMERATOR_CONTENT | nvarchar(-1) | -1 | 是 |  |
| DENOMINATOR_CONTENT | nvarchar(-1) | -1 | 是 |  |
| DATA_RANGE | nvarchar(-1) | -1 | 是 |  |
| WARNING_RULE | nvarchar(2000) | 2000 | 是 |  |
| SEQ_NO | int(10,0) | - | 否 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 否 |  |
| CREATED_NAME | nvarchar(64) | 64 | 否 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | varchar(64) | 64 | 否 |  |
| SYSTEM_SOURCE_NO | varchar(64) | 64 | 否 |  |

---

## EXAM_REFERENCE

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_REFERENCE_ID | numeric(19,0) | - | 否 | ✓ |
| EXAM_REFERENCE_NAME | nvarchar(128) | 128 | 否 |  |
| EXAM_REFERENCE_NO | varchar(32) | 32 | 否 |  |
| REFERENCE_CATEGORY_NO | varchar(32) | 32 | 否 |  |
| CEILING | numeric(19,0) | - | 是 |  |
| CEILING_WARNING | numeric(19,0) | - | 是 |  |
| BOTTOM | numeric(19,0) | - | 是 |  |
| BOTTOM_WARNING | numeric(19,0) | - | 是 |  |
| MASCULINE_NO | varchar(32) | 32 | 是 |  |
| REFERENCE_TYPE_NO | varchar(64) | 64 | 否 |  |
| CONDITION_NO | varchar(32) | 32 | 是 |  |
| GROUP_SEQ_NO | int(10,0) | - | 是 |  |
| DEFAULT_FLAG | numeric(19,0) | - | 是 |  |
| SEQ_NO | int(10,0) | - | 是 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |
| SYSTEM_SOURCE_NO | varchar(32) | 32 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |

---

## EXAM_REFERENCE_CONDITION

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_REFERENCE_CONDITION_ID | numeric(19,0) | - | 否 | ✓ |
| EXAM_REFERENCE_ID | numeric(19,0) | - | 否 |  |
| CONDITION_NO | varchar(128) | 128 | 否 |  |
| CONDITION_VALUE | nvarchar(256) | 256 | 是 |  |
| DICTIONARY_FLAG | numeric(19,0) | - | 是 |  |
| CONDITION_DESC | nvarchar(256) | 256 | 是 |  |
| SYSTEM_SOURCE_NO | varchar(64) | 64 | 否 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| CREATED_AT | datetime | - | 否 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |

---

## EXAM_REGISTER_ITEM

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_REGISTER_ITEM_ID | numeric(19,0) | - | 否 | ✓ |
| EXAM_TASK_ID | numeric(19,0) | - | 否 |  |
| SYSTEM_SOURCE_NO | nvarchar(32) | 32 | 是 |  |
| EXAM_ITEM_DIC_ID | numeric(19,0) | - | 是 |  |
| EXAM_ITEM_NAME | nvarchar(256) | 256 | 是 |  |
| POSITION_NO | nvarchar(64) | 64 | 是 |  |
| POSITION_NAME | nvarchar(256) | 256 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |
| INSPECTION_ROUTE_NO | nvarchar(64) | 64 | 是 |  |
| INSPECTION_ROUTE_NAME | nvarchar(128) | 128 | 是 |  |

---

## EXAM_REPORT

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_REPORT_ID | numeric(19,0) | - | 否 | ✓ |
| EXAM_TASK_ID | numeric(19,0) | - | 否 |  |
| SYSTEM_SOURCE_NO | nvarchar(32) | 32 | 是 |  |
| WRITER_BY | numeric(19,0) | - | 是 |  |
| WRITER_NO | nvarchar(32) | 32 | 是 |  |
| WRITER_NAME | nvarchar(32) | 32 | 是 |  |
| WRITE_AT | datetime | - | 是 |  |
| REPORTER_BY | numeric(19,0) | - | 是 |  |
| REPORTER_NO | nvarchar(32) | 32 | 是 |  |
| REPORTER_NAME | nvarchar(32) | 32 | 是 |  |
| REPORT_AT | datetime | - | 是 |  |
| REVIEWER_BY | numeric(19,0) | - | 是 |  |
| REVIEWER_NO | nvarchar(32) | 32 | 是 |  |
| REVIEWER_NAME | nvarchar(32) | 32 | 是 |  |
| REVIEW_AT | datetime | - | 是 |  |
| SECOND_REVIEWER_BY | numeric(19,0) | - | 是 |  |
| SECOND_REVIEWER_NO | varchar(32) | 32 | 是 |  |
| SECOND_REVIEWER_NAME | nvarchar(32) | 32 | 是 |  |
| SECOND_REVIEW_AT | datetime | - | 是 |  |
| PUBLISHER_BY | numeric(19,0) | - | 是 |  |
| PUBLISHER_NAME | nvarchar(32) | 32 | 是 |  |
| PUBLIC_AT | datetime | - | 是 |  |
| OBSERVATION_TEXT | nvarchar(-1) | -1 | 是 |  |
| OBSERVATION_HTML | nvarchar(-1) | -1 | 是 |  |
| EXAM_FINDINGS_PLAIN_TEXT | nvarchar(-1) | -1 | 是 |  |
| EXAM_FINDINGS_RICH_TEXT | nvarchar(-1) | -1 | 是 |  |
| EXAM_CONCLUSION_PLAIN_TEXT | nvarchar(-1) | -1 | 是 |  |
| EXAM_CONCLUSION_RICH_TEXT | nvarchar(-1) | -1 | 是 |  |
| NEG_POS_CODE | nvarchar(10) | 10 | 是 |  |
| EXAM_CONTAGION_TYPE | nvarchar(50) | 50 | 是 |  |
| DEM_4 | nvarchar(500) | 500 | 是 |  |
| DEM_5 | nvarchar(500) | 500 | 是 |  |
| DEM_6 | nvarchar(500) | 500 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |
| RECOVERY_BY | numeric(19,0) | - | 是 |  |
| RECOVERY_NAME | nvarchar(64) | 64 | 是 |  |
| RECOVERY_NO | nvarchar(32) | 32 | 是 |  |
| RECOVERY_AT | datetime | - | 是 |  |
| RECOVERY_REASON | nvarchar(500) | 500 | 是 |  |
| RECOVERY_DESC | nvarchar(1000) | 1000 | 是 |  |
| RECORDER_BY | numeric(19,0) | - | 是 |  |
| RECORDER_NAME | nvarchar(32) | 32 | 是 |  |
| RECORDER_NO | nvarchar(32) | 32 | 是 |  |

---

## EXAM_REPORT_ROLE

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_REPORT_ROLE_ID | numeric(19,0) | - | 否 |  |
| ROLE_ID | numeric(19,0) | - | 否 |  |
| ROLE_NO | numeric(19,0) | - | 否 |  |
| ROLE_NAME | nvarchar(128) | 128 | 是 |  |
| EXAM_REPORT_NAME | nvarchar(64) | 64 | 否 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| SYSTEM_SOURCE_NO | nvarchar(50) | 50 | 否 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |

---

## EXAM_REPORT_STRUCT_DICT

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_REPORT_STRUCT_DICT_ID | numeric(19,0) | - | 否 | ✓ |
| STRUCT_DICT_NAME | nvarchar(64) | 64 | 否 |  |
| STRUCT_DICT_NO | varchar(32) | 32 | 否 |  |
| STRUCT_DICT_CLASS_NAME | nvarchar(64) | 64 | 否 |  |
| STRUCT_DICT_TYPE_NO | varchar(32) | 32 | 否 |  |
| DEFAULT_VALUE | nvarchar(128) | 128 | 是 |  |
| PROMPT_CONTENT | nvarchar(128) | 128 | 是 |  |
| REQUIRED_FLAG | numeric(19,0) | - | 是 |  |
| ACCURACY | smallint(5,0) | - | 是 |  |
| OPTION_VALUE | nvarchar(128) | 128 | 是 |  |
| OPTION_VALUE_NO | varchar(64) | 64 | 是 |  |
| EXAM_REFERENCE_NO | varchar(32) | 32 | 是 |  |
| SEQ_NO | int(10,0) | - | 是 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 否 |  |
| CREATED_NAME | nvarchar(64) | 64 | 否 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | varchar(64) | 64 | 否 |  |
| SYSTEM_SOURCE_NO | varchar(64) | 64 | 否 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |

---

## EXAM_REPORT_STRUCT_RESULT

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_REPORT_STRUCT_RESULT_ID | numeric(19,0) | - | 否 | ✓ |
| EXAM_TASK_ID | numeric(19,0) | - | 否 |  |
| REPORT_STRUCT_ITEM_TYPE_NO | varchar(32) | 32 | 否 |  |
| REPORT_STRUCT_ITEM_NO | varchar(32) | 32 | 否 |  |
| REPORT_STRUCT_ITEM_RESULT | nvarchar(128) | 128 | 是 |  |
| SEQ_NO | int(10,0) | - | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 否 |  |
| CREATED_NAME | nvarchar(64) | 64 | 否 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | varchar(64) | 64 | 否 |  |
| SYSTEM_SOURCE_NO | varchar(64) | 64 | 否 |  |

---

## EXAM_REQUISITION

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_REQUISITION_ID | numeric(19,0) | - | 否 | ✓ |
| EXAM_TASK_ID | numeric(19,0) | - | 否 |  |
| SYSTEM_SOURCE_NO | nvarchar(32) | 32 | 是 |  |
| EXAM_REQUISITION_NO | nvarchar(64) | 64 | 是 |  |
| EXAM_REQUISITION_CONTENT | varchar(-1) | -1 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |

---

## EXAM_SCHEDULE_TASK

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_SCHEDULE_TASK_ID | numeric(19,0) | - | 否 | ✓ |
| EXAM_TASK_ID | numeric(19,0) | - | 是 |  |
| SYSTEM_SOURCE_NO | nvarchar(32) | 32 | 否 |  |
| SCHEDULE_TASK_CATEGORY | nvarchar(50) | 50 | 否 |  |
| SCHEDULE_TASK_SUBCATEGORY | nvarchar(50) | 50 | 是 |  |
| SCHEDULE_TASK_NAME | nvarchar(50) | 50 | 否 |  |
| SCHEDULE_TASK_COMPLETED_AT | datetime | - | 是 |  |
| SCHEDULE_TASK_DESC | nvarchar(2000) | 2000 | 是 |  |
| SCHEDULE_TASK_EXPIRED_AT | datetime | - | 是 |  |
| SCHEDULE_TASK_CYCLE | numeric(19,0) | - | 是 |  |
| RETRY_COUNT | numeric(19,0) | - | 是 |  |
| SCHEDULE_TASK_PLANNED_AT | datetime | - | 否 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| SCHEDULE_TASK_SCRIPT_TYPE | nvarchar(50) | 50 | 否 |  |
| SCHEDULE_TASK_SCRIPT | varchar(8000) | 8000 | 否 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| SCHEDULE_TASK_STATUS_NO | nvarchar(50) | 50 | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| SCHEDULE_TASK_EXECUTED_AT | datetime | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| SCHEDULE_TASK_FAIL_DESC | varchar(8000) | 8000 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MAX_RETRY_COUNT | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |
| EXTERNAL_TASK_FLAG | numeric(19,0) | - | 是 |  |
| PARENT_EXAM_SCHEDULE_TASK_ID | numeric(19,0) | - | 是 |  |
| EXAM_TASK_STATUS | numeric(19,0) | - | 是 |  |
| SCHEDULE_TASK_SOURCE_ELEMENT | nvarchar(-1) | -1 | 是 |  |

---

## EXAM_SCHEDULE_TASK_BASE

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_SCHEDULE_TASK_ID | numeric(19,0) | - | 否 |  |
| EXAM_TASK_ID | numeric(19,0) | - | 是 |  |
| SYSTEM_SOURCE_NO | nvarchar(32) | 32 | 否 |  |
| SCHEDULE_TASK_CATEGORY | nvarchar(50) | 50 | 否 |  |
| SCHEDULE_TASK_SUBCATEGORY | nvarchar(50) | 50 | 是 |  |
| SCHEDULE_TASK_NAME | nvarchar(50) | 50 | 否 |  |
| SCHEDULE_TASK_COMPLETED_AT | datetime | - | 是 |  |
| SCHEDULE_TASK_DESC | nvarchar(2000) | 2000 | 是 |  |
| SCHEDULE_TASK_EXPIRED_AT | datetime | - | 是 |  |
| SCHEDULE_TASK_CYCLE | numeric(19,0) | - | 是 |  |
| RETRY_COUNT | numeric(19,0) | - | 是 |  |
| SCHEDULE_TASK_PLANNED_AT | datetime | - | 否 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| SCHEDULE_TASK_SCRIPT_TYPE | nvarchar(50) | 50 | 否 |  |
| SCHEDULE_TASK_SCRIPT | varchar(8000) | 8000 | 否 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| SCHEDULE_TASK_STATUS_NO | nvarchar(50) | 50 | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| SCHEDULE_TASK_EXECUTED_AT | datetime | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| SCHEDULE_TASK_FAIL_DESC | varchar(8000) | 8000 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MAX_RETRY_COUNT | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |
| EXTERNAL_TASK_FLAG | numeric(19,0) | - | 是 |  |
| PARENT_EXAM_SCHEDULE_TASK_ID | numeric(19,0) | - | 是 |  |
| EXAM_TASK_STATUS | numeric(19,0) | - | 是 |  |
| SCHEDULE_TASK_SOURCE_ELEMENT | nvarchar(-1) | -1 | 是 |  |

---

## EXAM_SCHEDULE_TASK_HISTORY

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_SCHEDULE_TASK_HISTORY_ID | numeric(19,0) | - | 否 | ✓ |
| EXAM_SCHEDULE_TASK_ID | numeric(19,0) | - | 是 |  |
| EXAM_TASK_ID | numeric(19,0) | - | 是 |  |
| SCHEDULE_TASK_CATEGORY | nvarchar(50) | 50 | 否 |  |
| SCHEDULE_TASK_SUBCATEGORY | nvarchar(50) | 50 | 是 |  |
| SCHEDULE_TASK_NAME | nvarchar(50) | 50 | 否 |  |
| SCHEDULE_TASK_EXPIRED_AT | datetime | - | 是 |  |
| SCHEDULE_TASK_CYCLE | numeric(19,0) | - | 是 |  |
| SCHEDULE_TASK_PLANNED_AT | datetime | - | 否 |  |
| SCHEDULE_TASK_SCRIPT_TYPE | nvarchar(50) | 50 | 否 |  |
| SCHEDULE_TASK_SCRIPT | nvarchar(-1) | -1 | 否 |  |
| SCHEDULE_TASK_STATUS_NO | nvarchar(50) | 50 | 否 |  |
| SCHEDULE_TASK_FAIL_DESC | nvarchar(-1) | -1 | 是 |  |
| SCHEDULE_TASK_EXECUTED_AT | datetime | - | 是 |  |
| SCHEDULE_TASK_COMPLETED_AT | datetime | - | 是 |  |
| SCHEDULE_TASK_DESC | nvarchar(2000) | 2000 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| SYSTEM_SOURCE_NO | nvarchar(32) | 32 | 否 |  |
| RETRY_COUNT | numeric(19,0) | - | 是 |  |
| MAX_RETRY_COUNT | numeric(19,0) | - | 是 |  |
| EXTERNAL_TASK_FLAG | numeric(19,0) | - | 是 |  |
| PARENT_EXAM_SCHEDULE_TASK_ID | numeric(19,0) | - | 是 |  |
| EXAM_TASK_STATUS | numeric(19,0) | - | 是 |  |
| SCHEDULE_TASK_SOURCE_ELEMENT | nvarchar(-1) | -1 | 是 |  |
| REQUEST_URL | nvarchar(-1) | -1 | 是 |  |
| REQUEST_HEADER | nvarchar(-1) | -1 | 是 |  |
| REQUEST_PARAM | nvarchar(-1) | -1 | 是 |  |
| RESPONSE_CONTENT | nvarchar(-1) | -1 | 是 |  |

---

## EXAM_SELECT_ATTACHMENT

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_SELECT_ATTACHMENT_ID | numeric(19,0) | - | 否 | ✓ |
| EXAM_TASK_ID | numeric(19,0) | - | 否 |  |
| EXAM_ATTACHMENT_ID | numeric(19,0) | - | 否 |  |
| SYSTEM_SOURCE_NO | nvarchar(32) | 32 | 是 |  |
| SEQ_NO | int(10,0) | - | 是 |  |
| REMARK | nvarchar(200) | 200 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |

---

## EXAM_SELF_SERVICE_SETTING

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_SELF_SERVICE_SETTING_ID | numeric(19,0) | - | 否 | ✓ |
| SELF_SERVICE_IP | nvarchar(20) | 20 | 是 |  |
| SELF_SERVICE_HOST_NAME | nvarchar(256) | 256 | 是 |  |
| SELF_SERVICE_ALIAS | nvarchar(256) | 256 | 是 |  |
| SELF_SERVICE_SETTING_CONTENT | varchar(-1) | -1 | 是 |  |
| SELF_SERVICE_TYPE_NO | nvarchar(64) | 64 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |

---

## EXAM_SETTINGS

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_SETTINGS_ID | numeric(19,0) | - | 否 | ✓ |
| SYSTEM_SOURCE_NO | nvarchar(32) | 32 | 是 |  |
| SETTING_ITEM_KEY | nvarchar(256) | 256 | 是 |  |
| SETTING_ITEM_DOMAIN_NO | nvarchar(16) | 16 | 是 |  |
| SETTING_ITEM_DOMAIN_NAME | nvarchar(64) | 64 | 是 |  |
| SETTING_ITEM_SECTION | nvarchar(64) | 64 | 是 |  |
| SETTING_ITEM_ENTRY | nvarchar(256) | 256 | 是 |  |
| SETTING_ITEM_DATA_TYPE | nvarchar(16) | 16 | 是 |  |
| DEFAULT_VALUE | nvarchar(-1) | -1 | 是 |  |
| PRIVATE_BY | numeric(19,0) | - | 是 |  |
| VISIBLE_FLAG | numeric(19,0) | - | 是 |  |
| SEQ_NO | smallint(5,0) | - | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 是 |  |
| HOSPITAL_NAME | nvarchar(64) | 64 | 是 |  |
| IS_DEL | smallint(5,0) | - | 是 |  |
| CREATED_AT | datetime | - | 是 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |
| DESCRIPTION | nvarchar(2000) | 2000 | 是 |  |
| EXAM_SETTINGS_PARENT_ID | numeric(19,0) | - | 是 |  |

---

## EXAM_SETTINGS_20260409

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_SETTINGS_ID | numeric(19,0) | - | 否 |  |
| SYSTEM_SOURCE_NO | nvarchar(32) | 32 | 是 |  |
| SETTING_ITEM_KEY | nvarchar(256) | 256 | 是 |  |
| SETTING_ITEM_DOMAIN_NO | nvarchar(16) | 16 | 是 |  |
| SETTING_ITEM_DOMAIN_NAME | nvarchar(64) | 64 | 是 |  |
| SETTING_ITEM_SECTION | nvarchar(64) | 64 | 是 |  |
| SETTING_ITEM_ENTRY | nvarchar(256) | 256 | 是 |  |
| SETTING_ITEM_DATA_TYPE | nvarchar(16) | 16 | 是 |  |
| DEFAULT_VALUE | nvarchar(-1) | -1 | 是 |  |
| PRIVATE_BY | numeric(19,0) | - | 是 |  |
| VISIBLE_FLAG | numeric(19,0) | - | 是 |  |
| SEQ_NO | smallint(5,0) | - | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 是 |  |
| HOSPITAL_NAME | nvarchar(64) | 64 | 是 |  |
| IS_DEL | smallint(5,0) | - | 是 |  |
| CREATED_AT | datetime | - | 是 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |
| DESCRIPTION | nvarchar(2000) | 2000 | 是 |  |
| EXAM_SETTINGS_PARENT_ID | numeric(19,0) | - | 是 |  |

---

## EXAM_SETTINGS_BACKUP

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_SETTINGS_ID | numeric(19,0) | - | 否 |  |
| SYSTEM_SOURCE_NO | nvarchar(32) | 32 | 是 |  |
| SETTING_ITEM_KEY | nvarchar(256) | 256 | 是 |  |
| SETTING_ITEM_DOMAIN_NO | nvarchar(16) | 16 | 是 |  |
| SETTING_ITEM_DOMAIN_NAME | nvarchar(64) | 64 | 是 |  |
| SETTING_ITEM_SECTION | nvarchar(64) | 64 | 是 |  |
| SETTING_ITEM_ENTRY | nvarchar(256) | 256 | 是 |  |
| SETTING_ITEM_DATA_TYPE | nvarchar(16) | 16 | 是 |  |
| DEFAULT_VALUE | nvarchar(-1) | -1 | 是 |  |
| PRIVATE_BY | numeric(19,0) | - | 是 |  |
| VISIBLE_FLAG | numeric(19,0) | - | 是 |  |
| SEQ_NO | smallint(5,0) | - | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 是 |  |
| HOSPITAL_NAME | nvarchar(64) | 64 | 是 |  |
| IS_DEL | smallint(5,0) | - | 是 |  |
| CREATED_AT | datetime | - | 是 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |
| DESCRIPTION | nvarchar(2000) | 2000 | 是 |  |
| EXAM_SETTINGS_PARENT_ID | numeric(19,0) | - | 是 |  |

---

## EXAM_SETTINGS_TEMPLATE

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_SETTINGS_TEMPLATE_ID | numeric(19,0) | - | 否 | ✓ |
| SETTING_ITEM_KEY | nvarchar(256) | 256 | 否 |  |
| SETTING_ITEM_SECTION | nvarchar(64) | 64 | 是 |  |
| EXAM_CATEGORY_ID | numeric(19,0) | - | 是 |  |
| TEMPLATE_NAME | nvarchar(256) | 256 | 是 |  |
| TEMPLATE_CONTENT | varchar(-1) | -1 | 是 |  |
| SYSTEM_SOURCE_NO | nvarchar(20) | 20 | 否 |  |
| SEQ_NO | int(10,0) | - | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 否 |  |
| CREATED_NAME | nvarchar(40) | 40 | 否 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(40) | 40 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(40) | 40 | 是 |  |

---

## EXAM_SIGNATURE

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_SIGNATURE_ID | numeric(19,0) | - | 否 | ✓ |
| USER_ID | numeric(19,0) | - | 是 |  |
| USER_NO | nvarchar(32) | 32 | 是 |  |
| SIGNATURE_FILE | nvarchar(-1) | -1 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |
| HOSPITAL_NO | varchar(32) | 32 | 是 |  |

---

## EXAM_SPECIAL_ITEM

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_SPECIAL_ITEM_ID | numeric(19,0) | - | 否 | ✓ |
| EXAM_MAIN_CATEGORY_NO | varchar(32) | 32 | 是 |  |
| SPECIAL_ITEM_CLASS_NAME | nvarchar(40) | 40 | 是 |  |
| SPECIAL_ITEM_NO | varchar(80) | 80 | 是 |  |
| SPECIAL_ITEM_NAME | nvarchar(64) | 64 | 是 |  |
| SPECIAL_ITEM_EXTERN_NO | varchar(100) | 100 | 是 |  |
| MEM1_NO | varchar(60) | 60 | 是 |  |
| MEM2_NO | varchar(60) | 60 | 是 |  |
| REMARK | nvarchar(500) | 500 | 是 |  |
| ALTER_FLAG | numeric(19,0) | - | 是 |  |
| VISIBLE_FLAG | numeric(19,0) | - | 是 |  |
| SEQ_NO | int(10,0) | - | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |
| SYSTEM_SOURCE_NO | varchar(32) | 32 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 是 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |

---

## EXAM_STATISTICAL_REPORT

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_STATISTICAL_REPORT_ID | numeric(19,0) | - | 否 | ✓ |
| STAT_REPORT_NO | varchar(100) | 100 | 是 |  |
| STAT_REPORT_NAME | nvarchar(100) | 100 | 是 |  |
| STAT_REPORT_PATH | nvarchar(500) | 500 | 是 |  |
| STAT_REPORT_SYSTEM_FLAG | numeric(19,0) | - | 是 |  |
| PARENT_STATISTICAL_REPORT_ID | numeric(19,0) | - | 是 |  |
| SEQ_NO | int(10,0) | - | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 否 |  |
| CREATED_NAME | nvarchar(40) | 40 | 否 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(40) | 40 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(40) | 40 | 是 |  |
| SYSTEM_SOURCE_NO | nvarchar(20) | 20 | 否 |  |
| STAT_NODE_PROPERTY | varchar(255) | 255 | 是 |  |

---

## EXAM_TASK

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_TASK_ID | numeric(19,0) | - | 否 | ✓ |
| EXAM_ENCOUNTER_ID | numeric(19,0) | - | 否 |  |
| SYSTEM_SOURCE_NO | nvarchar(32) | 32 | 是 |  |
| ENCOUNTER_TYPE_NO | nvarchar(32) | 32 | 是 |  |
| PATIENT_NO | varchar(64) | 64 | 是 |  |
| FULL_NAME | nvarchar(128) | 128 | 是 |  |
| PINYIN | varchar(64) | 64 | 是 |  |
| JIANPIN | varchar(64) | 64 | 是 |  |
| BIRTH_DATE | date | - | 是 |  |
| GENDER_CODE | numeric(19,0) | - | 是 |  |
| TELEPHONE_NO | nvarchar(24) | 24 | 是 |  |
| ADDRESS | nvarchar(128) | 128 | 是 |  |
| VIP_TYPE_NO | varchar(32) | 32 | 是 |  |
| IDCARD_NO | nvarchar(18) | 18 | 是 |  |
| IDCARD_TYPE_CODE | numeric(19,0) | - | 是 |  |
| AGE | nvarchar(32) | 32 | 是 |  |
| AGE_YEARS | numeric(19,0) | - | 是 |  |
| AGE_MONTHS | numeric(10,2) | - | 是 |  |
| AGE_DAYS | numeric(19,0) | - | 是 |  |
| NATION_CODE | numeric(19,0) | - | 是 |  |
| NATION_NAME | nvarchar(64) | 64 | 是 |  |
| CARD_NO | nvarchar(32) | 32 | 是 |  |
| CARD_TYPE_CODE | numeric(19,0) | - | 是 |  |
| ADMISSION_NUMBER | nvarchar(256) | 256 | 是 |  |
| WARD_ID | numeric(19,0) | - | 是 |  |
| WARD_NO | nvarchar(64) | 64 | 是 |  |
| WARD_NAME | nvarchar(50) | 50 | 是 |  |
| BED_NO | nvarchar(32) | 32 | 是 |  |
| EXAM_EQUIPMENT_ID | numeric(19,0) | - | 是 |  |
| EQUIPMENT_NAME | nvarchar(40) | 40 | 是 |  |
| EXAM_CATEGORY_ID | numeric(19,0) | - | 是 |  |
| EXAM_CATEGORY_NO | varchar(50) | 50 | 是 |  |
| EXAM_CATEGORY_NAME | nvarchar(50) | 50 | 是 |  |
| EXAM_MAIN_CATEGORY_NO | varchar(50) | 50 | 是 |  |
| LABEL_NO | nvarchar(32) | 32 | 是 |  |
| TECH_NO | nvarchar(20) | 20 | 是 |  |
| APPOINTMENT_BY | numeric(19,0) | - | 是 |  |
| APPOINTMENT_NAME | nvarchar(32) | 32 | 是 |  |
| REGISTER_BY | numeric(19,0) | - | 是 |  |
| REGISTER_NAME | nvarchar(32) | 32 | 是 |  |
| REGISTER_AT | datetime | - | 是 |  |
| APPLY_DEPT_ID | numeric(19,0) | - | 是 |  |
| APPLY_DEPT_NO | nvarchar(64) | 64 | 是 |  |
| APPLY_DEPT_NAME | nvarchar(50) | 50 | 是 |  |
| APPLY_AT | datetime | - | 是 |  |
| APPLY_DOC_ID | numeric(19,0) | - | 是 |  |
| APPLY_DOC_NO | nvarchar(64) | 64 | 是 |  |
| APPLY_DOC_NAME | nvarchar(32) | 32 | 是 |  |
| CLINIC_DIAG_DESC | nvarchar(1024) | 1024 | 是 |  |
| URGENT_FLAG | numeric(19,0) | - | 是 |  |
| SPECIAL_IDENTIFICATION_NO | nvarchar(64) | 64 | 是 |  |
| EXAM_TASK_STATUS | numeric(19,0) | - | 是 |  |
| CONFIRMED_NO | nvarchar(32) | 32 | 是 |  |
| IS_CLUDING_FILM_FEE | numeric(19,0) | - | 是 |  |
| EXAM_COMPLETION_AT | datetime | - | 是 |  |
| TECHNICIAN_BY | numeric(19,0) | - | 是 |  |
| TECHNICIAN_NAME | nvarchar(32) | 32 | 是 |  |
| EXAM_IMAGE_COUNT | int(10,0) | - | 是 |  |
| IMAGE_CREATED_AT | datetime | - | 是 |  |
| IMAGE_ENTER_AT | datetime | - | 是 |  |
| EXAM_ASSIST_STATUS | numeric(19,0) | - | 是 |  |
| EXEC_DEPT_ID | numeric(19,0) | - | 是 |  |
| EXEC_DEPT_NO | nvarchar(32) | 32 | 是 |  |
| DELETED_BY | numeric(19,0) | - | 是 |  |
| DELETED_NAME | nvarchar(100) | 100 | 是 |  |
| DELETED_AT | datetime | - | 是 |  |
| LOCK_FLAG | numeric(19,0) | - | 是 |  |
| LOCK_BY | numeric(19,0) | - | 是 |  |
| LOCK_NAME | nvarchar(100) | 100 | 是 |  |
| LOCK_PC | nvarchar(50) | 50 | 是 |  |
| CV_REPORT_ID | numeric(19,0) | - | 是 |  |
| REPORT_CA_VALUE | varchar(2048) | 2048 | 是 |  |
| REVISE_NO | varchar(64) | 64 | 是 |  |
| REMARK | nvarchar(-1) | -1 | 是 |  |
| REPORT_EXT_ID | varchar(64) | 64 | 是 |  |
| APPLY_HOSPITAL_NO | varchar(64) | 64 | 是 |  |
| APPLY_HOSPITAL_NAME | varchar(64) | 64 | 是 |  |
| APPLY_TYPE_NO | varchar(64) | 64 | 是 |  |
| APPLY_TYPE_NAME | varchar(64) | 64 | 是 |  |
| ASSIST_HOSPITAL_NO | varchar(64) | 64 | 是 |  |
| ASSIST_HOSPITAL_NAME | varchar(64) | 64 | 是 |  |
| ASSIST_STATUS_DESC | varchar(4000) | 4000 | 是 |  |
| ASSIST_REGISTER_FLAG | numeric(19,0) | - | 是 |  |
| SOURCE_HOSPITAL_SOID | numeric(19,0) | - | 是 |  |
| EXAM_CATEGORY_PRINTFILE_ID | numeric(19,0) | - | 是 |  |
| PRINT_FILE_NAME | nvarchar(100) | 100 | 是 |  |
| PDF_REPORT_FLAG | numeric(19,0) | - | 是 |  |
| ITEM_SUM_PRICE | numeric(16,6) | - | 是 |  |
| BED_SIDE_FLAG | numeric(19,0) | - | 是 |  |
| LAST_MENSTRUAL_PERIOD | date | - | 是 |  |
| GESTATIONAL_AGE | nvarchar(32) | 32 | 是 |  |
| DEM_1 | nvarchar(500) | 500 | 是 |  |
| DEM_2 | nvarchar(500) | 500 | 是 |  |
| DEM_3 | nvarchar(500) | 500 | 是 |  |
| EXAM_TRIAGE_QUEUE_ID | numeric(19,0) | - | 是 |  |
| TRIAGE_QUEUE_NAME | nvarchar(64) | 64 | 是 |  |
| TRIAGE_QUEUE_PREFIX | nvarchar(16) | 16 | 是 |  |
| PHYS_COMPANY_NO | varchar(128) | 128 | 是 |  |
| PHYS_COMPANY_NAME | nvarchar(128) | 128 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |
| HOSPITAL_NO | varchar(32) | 32 | 是 |  |
| RECOVERY_TIMES | int(10,0) | - | 是 |  |
| EXEC_DEPT_NAME | nvarchar(64) | 64 | 是 |  |
| PATIENT_SECRET_LEVEL_NO | nvarchar(16) | 16 | 是 |  |
| ASSIGN_REPORT_BY | numeric(19,0) | - | 是 |  |
| ASSIGN_REPORT_NO | varchar(64) | 64 | 是 |  |
| ASSIGN_REPORT_NAME | varchar(64) | 64 | 是 |  |
| ASSIGN_REVIEWER_BY | numeric(19,0) | - | 是 |  |
| ASSIGN_REVIEWER_NO | varchar(64) | 64 | 是 |  |
| ASSIGN_REVIEWER_NAME | varchar(64) | 64 | 是 |  |
| ASSIGN_NURSE_BY | numeric(19,0) | - | 是 |  |
| ASSIGN_NURSE_NO | varchar(64) | 64 | 是 |  |
| ASSIGN_NURSE_NAME | varchar(64) | 64 | 是 |  |
| FILM_FEE_SUM_PRICE | numeric(19,2) | - | 是 |  |
| ARREARS_REASON_NOS | nvarchar(128) | 128 | 是 |  |

---

## EXAM_TASK_ASSIGNMENT

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_TASK_ASSIGNMENT_ID | numeric(19,0) | - | 否 | ✓ |
| EXAM_TASK_ID | numeric(19,0) | - | 否 |  |
| EXAM_TASK_ASSIGN_STATUS | numeric(19,0) | - | 是 |  |
| EXAM_TASK_ASSIGN_CATEGORY_CODE | numeric(19,0) | - | 是 |  |
| SYSTEM_SOURCE_NO | varchar(64) | 64 | 是 |  |
| ASSIGNED_AT | datetime | - | 是 |  |
| ASSIGNED_BY | numeric(19,0) | - | 是 |  |
| ASSIGNED_DOCTOR_NAME | nvarchar(64) | 64 | 是 |  |
| HAVING_PATHOLOGY_FLAG | numeric(19,0) | - | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(64) | 64 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |

---

## EXAM_TASK_COMMENT

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_TASK_COMMENT_ID | numeric(19,0) | - | 否 | ✓ |
| EXAM_TASK_ID | numeric(19,0) | - | 否 |  |
| COMMENT_TEXT | nvarchar(1024) | 1024 | 是 |  |
| COMMENT_SOURCE_NAME | nvarchar(64) | 64 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |

---

## EXAM_TASK_COMMENT_TRACE

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_TASK_COMMENT_TRACE_ID | numeric(19,0) | - | 否 | ✓ |
| EXAM_TASK_COMMENT_ID | numeric(19,0) | - | 否 |  |
| READ_BY | numeric(19,0) | - | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |

---

## EXAM_TECHNOLOGY

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_TECHNOLOGY_ID | numeric(19,0) | - | 否 | ✓ |
| EXAM_TASK_ID | numeric(19,0) | - | 否 |  |
| SYSTEM_SOURCE_NO | nvarchar(32) | 32 | 是 |  |
| EXAM_METHOD | nvarchar(2000) | 2000 | 是 |  |
| INJECTION_BY | numeric(19,0) | - | 是 |  |
| INJECTION_NO | varchar(32) | 32 | 是 |  |
| INJECTION_NAME | nvarchar(32) | 32 | 是 |  |
| INSTRUCTOR_BY | numeric(19,0) | - | 是 |  |
| INSTRUCTOR_NO | varchar(32) | 32 | 是 |  |
| INSTRUCTOR_NAME | nvarchar(32) | 32 | 是 |  |
| TECHNICIAN_BY | numeric(19,0) | - | 是 |  |
| TECHNICIAN_NO | varchar(32) | 32 | 是 |  |
| TECHNICIAN_NAME | nvarchar(32) | 32 | 是 |  |
| CANNULATOR_BY | numeric(19,0) | - | 是 |  |
| CANNULATOR_NO | varchar(64) | 64 | 是 |  |
| CANNULATOR_NAME | nvarchar(32) | 32 | 是 |  |
| EXAM_COMPLETION_AT | datetime | - | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |

---

## EXAM_TRIAGE_HISTORY

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_TRIAGE_HISTORY_ID | numeric(19,0) | - | 否 | ✓ |
| FROM_ID | numeric(19,0) | - | 是 |  |
| OPERATE_TYPE_NO | varchar(50) | 50 | 是 |  |
| TRIAGE_HISTORY_VALUE | nvarchar(-1) | -1 | 是 |  |
| SYSTEM_SOURCE_NO | nvarchar(32) | 32 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |

---

## EXAM_TRIAGE_QUEUE

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_TRIAGE_QUEUE_ID | numeric(19,0) | - | 否 | ✓ |
| SYSTEM_SOURCE_NO | nvarchar(32) | 32 | 是 |  |
| TRIAGE_QUEUE_NAME | nvarchar(64) | 64 | 是 |  |
| TRIAGE_QUEUE_PREFIX | nvarchar(16) | 16 | 是 |  |
| TRIAGE_QUEUE_PTY | varchar(4000) | 4000 | 是 |  |
| VISIBLE_FLAG | numeric(19,0) | - | 是 |  |
| SEQ_NO | smallint(5,0) | - | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |

---

## EXAM_TRIAGE_SCREEN_CALL

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_TRIAGE_SCREEN_CALL_ID | numeric(19,0) | - | 否 | ✓ |
| EXAM_TASK_ID | numeric(19,0) | - | 否 |  |
| EXAM_TRIAGE_QUEUE_ID | numeric(19,0) | - | 是 |  |
| CALL_TYPE_NO | varchar(40) | 40 | 是 |  |
| FULL_NAME | nvarchar(128) | 128 | 是 |  |
| TECH_NO | nvarchar(20) | 20 | 是 |  |
| TRIAGE_QUEUE_NO | nvarchar(32) | 32 | 是 |  |
| EXAM_CATEGORY_ID | numeric(19,0) | - | 是 |  |
| EXAM_CATEGORY_NAME | varchar(64) | 64 | 是 |  |
| EXAM_EQUIPMENT_ID | numeric(19,0) | - | 是 |  |
| EQUIPMENT_NAME | varchar(64) | 64 | 是 |  |
| EXAM_TASK_STATUS | numeric(19,0) | - | 是 |  |
| TRIAGE_TASK_STATUS | numeric(19,0) | - | 是 |  |
| SYSTEM_SOURCE_NO | nvarchar(32) | 32 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |
| CALLED_SCREEN_IPS | nvarchar(2000) | 2000 | 是 |  |

---

## EXAM_TRIAGE_TASK

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_TRIAGE_TASK_ID | numeric(19,0) | - | 否 | ✓ |
| EXAM_TASK_ID | numeric(19,0) | - | 否 |  |
| SYSTEM_SOURCE_NO | nvarchar(32) | 32 | 是 |  |
| EXAM_EQUIPMENT_ID | numeric(19,0) | - | 是 |  |
| EXAM_TRIAGE_QUEUE_ID | numeric(19,0) | - | 是 |  |
| TRIAGE_QUEUE_NAME | nvarchar(64) | 64 | 是 |  |
| TRIAGE_QUEUE_PREFIX | nvarchar(16) | 16 | 是 |  |
| TRIAGE_QUEUE_AP_PREFIX | nvarchar(16) | 16 | 是 |  |
| TRIAGE_QUEUE_SEQ_NO | int(10,0) | - | 是 |  |
| TRIAGE_ROOM_SEQ_NO | int(10,0) | - | 是 |  |
| TRIAGE_TASK_DEADLINE_AT | datetime | - | 是 |  |
| TRIAGE_QUEUE_NO | nvarchar(32) | 32 | 是 |  |
| TRIAGE_TASK_STATUS | numeric(19,0) | - | 是 |  |
| TRIAGE_TASK_SUSPEND_FLAG | numeric(19,0) | - | 是 |  |
| TRIAGE_TASK_WAITING_AT | datetime | - | 是 |  |
| TRIAGE_TASK_WAITING_BY | numeric(19,0) | - | 是 |  |
| TRIAGE_TASK_START_AT | datetime | - | 是 |  |
| TRIAGE_TASK_START_BY | numeric(19,0) | - | 是 |  |
| TRIAGE_TASK_END_AT | datetime | - | 是 |  |
| TRIAGE_TASK_END_BY | numeric(19,0) | - | 是 |  |
| TRIAGE_TASK_PRIORITY_CODE | numeric(19,0) | - | 是 |  |
| TRIAGE_TASK_CALL_TIMES | int(10,0) | - | 是 |  |
| TRIAGE_TASK_CALLER_BY | numeric(19,0) | - | 是 |  |
| TRIAGE_TASK_DELETED_BY | numeric(19,0) | - | 是 |  |
| OVER_NO_TIMES | int(10,0) | - | 是 |  |
| TRIAGE_TASK_TYPE_CODE | numeric(19,0) | - | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |
| TRANSFER_FLAG | numeric(19,0) | - | 是 |  |

---

## EXAM_USER_ACCESS_RULE

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_USER_ACCESS_RULE_ID | numeric(19,0) | - | 否 | ✓ |
| USER_NO | varchar(256) | 256 | 否 |  |
| USER_NAME | nvarchar(64) | 64 | 否 |  |
| ENCOUNTER_TYPE_NOS | varchar(256) | 256 | 是 |  |
| GENDER_CODES | varchar(128) | 128 | 是 |  |
| APPLY_DEPT_NOS | varchar(512) | 512 | 是 |  |
| EXAM_CATEGORY_IDS | varchar(512) | 512 | 是 |  |
| EXAM_REGISTER_ITEM_IDS | varchar(512) | 512 | 是 |  |
| ENABLE_FLAG | numeric(19,0) | - | 是 |  |
| CREATED_BY | numeric(19,0) | - | 否 |  |
| CREATED_NAME | nvarchar(256) | 256 | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(256) | 256 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 是 |  |
| SYSTEM_SOURCE_NO | varchar(32) | 32 | 否 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(256) | 256 | 是 |  |

---

## EXAM_VIP_AUTHORIZED

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_VIP_AUTHORIZED_ID | numeric(19,0) | - | 否 | ✓ |
| EXAM_TASK_ID | numeric(19,0) | - | 否 |  |
| AUTHORIZED_ID | numeric(19,0) | - | 是 |  |
| AUTHORIZED_NO | varchar(32) | 32 | 否 |  |
| AUTHORIZED_NAME | varchar(64) | 64 | 否 |  |
| AUTHORIZED_DEPT_ID | numeric(19,0) | - | 是 |  |
| AUTHORIZED_DEPT_NO | varchar(32) | 32 | 是 |  |
| AUTHORIZED_DEPT_NAME | varchar(64) | 64 | 是 |  |
| AUTHORIZED_START_DATE | date | - | 是 |  |
| AUTHORIZED_END_DATE | date | - | 是 |  |
| AUTHORIZED_TYPE_NO | varchar(256) | 256 | 是 |  |
| HISTORY_FLAG | numeric(19,0) | - | 是 |  |
| OPERATED_AT | datetime | - | 否 |  |
| OPERATED_BY | numeric(19,0) | - | 否 |  |
| OPERATED_NAME | nvarchar(256) | 256 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |
| SYSTEM_SOURCE_NO | varchar(64) | 64 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |

---

## EXAM_WIN_GPT_SDI_ITEM

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_WIN_GPT_SDI_ITEM_ID | numeric(19,0) | - | 否 | ✓ |
| ITEM_NAME | nvarchar(64) | 64 | 否 |  |
| ITEM_REPORT_CONTENT_SCOPE | nvarchar(256) | 256 | 否 |  |
| ITEM_INDICATORS_CONTENT | nvarchar(-1) | -1 | 否 |  |
| ITEM_PROCESSING_STEPS | nvarchar(-1) | -1 | 否 |  |
| SYSTEM_SOURCE_NO | varchar(64) | 64 | 否 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| CREATED_AT | datetime | - | 否 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |

---

## EXAM_WIN_GPT_SDI_SCOPE

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| EXAM_WIN_GPT_SDI_SCOPE_ID | numeric(19,0) | - | 否 | ✓ |
| EXAM_WIN_GPT_SDI_ITEM_ID | numeric(19,0) | - | 否 |  |
| SCOPE_TYPE_NO | varchar(64) | 64 | 否 |  |
| SCOPE_CONTENT | nvarchar(128) | 128 | 否 |  |
| SYSTEM_SOURCE_NO | varchar(64) | 64 | 否 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| CREATED_AT | datetime | - | 否 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |

---

## FILM_FilmInfo

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| FilmID | int(10,0) | - | 否 | ✓ |
| PatientID | varchar(32) | 32 | 是 |  |
| PatientName | varchar(64) | 64 | 是 |  |
| StudyDate | varchar(8) | 8 | 是 |  |
| StudyUID | varchar(128) | 128 | 是 |  |
| SeriesUID | varchar(128) | 128 | 是 |  |
| ImageUID | varchar(128) | 128 | 是 |  |
| ImageCount | int(10,0) | - | 是 |  |
| FilmRow | int(10,0) | - | 是 |  |
| FilmColumn | int(10,0) | - | 是 |  |
| FilmOrientation | int(10,0) | - | 是 |  |
| FilmSize | varchar(32) | 32 | 是 |  |
| FilmTrueWidth | int(10,0) | - | 是 |  |
| FilmTrueHeight | int(10,0) | - | 是 |  |
| FilmWidth | int(10,0) | - | 是 |  |
| FilmHeight | int(10,0) | - | 是 |  |
| MaxPrintWidth | int(10,0) | - | 是 |  |
| MaxPrintHeight | int(10,0) | - | 是 |  |
| RuleType | int(10,0) | - | 是 |  |
| ShowGrid | int(10,0) | - | 是 |  |
| PosOfImgInfo | int(10,0) | - | 是 |  |
| SizeOfImgInfo | int(10,0) | - | 是 |  |
| PageInfo | varchar(2000) | 2000 | 是 |  |
| ShowPageInfo | int(10,0) | - | 是 |  |
| InvertPageInfo | int(10,0) | - | 是 |  |
| SizeOfPageInfo | int(10,0) | - | 是 |  |
| AddScout | int(10,0) | - | 是 |  |
| DoubleScout | int(10,0) | - | 是 |  |
| AddDrawAnn | int(10,0) | - | 是 |  |
| HasEFilm | int(10,0) | - | 是 |  |
| PrintTimes | int(10,0) | - | 是 |  |
| Source | int(10,0) | - | 否 |  |
| AccessionNO | varchar(32) | 32 | 是 |  |
| InsertDate | datetime | - | 否 |  |
| AddScoutSec | int(10,0) | - | 是 |  |
| ImageInter | int(10,0) | - | 是 |  |
| bEFilmDeleted | int(10,0) | - | 是 |  |
| FilmPlan | varchar(50) | 50 | 是 |  |
| PrintedTime | datetime | - | 是 |  |
| RegularLayout | int(10,0) | - | 是 |  |
| UserID | varchar(128) | 128 | 是 |  |
| ApplyNO | int(10,0) | - | 是 |  |
| UpNum | int(10,0) | - | 是 |  |
| DownNum | int(10,0) | - | 是 |  |
| LeftNum | int(10,0) | - | 是 |  |
| RightNum | int(10,0) | - | 是 |  |
| FP_PlanName | varchar(64) | 64 | 是 |  |
| FP_FilmName | varchar(64) | 64 | 是 |  |
| FP_FilmType | varchar(64) | 64 | 是 |  |
| FP_Layout | int(10,0) | - | 是 |  |
| FP_Rows | int(10,0) | - | 是 |  |
| FP_Cols | int(10,0) | - | 是 |  |
| ModifyTime | datetime | - | 是 |  |
| copyname | int(10,0) | - | 是 |  |
| FilmCount | int(10,0) | - | 是 |  |
| IsColorFilm | int(10,0) | - | 是 |  |
| PrinterName | varchar(64) | 64 | 是 |  |

---

## FILM_ImagePrintedInfo

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| ID_Num | int(10,0) | - | 否 | ✓ |
| FilmID | int(10,0) | - | 否 |  |
| InstanceUID | varchar(128) | 128 | 否 |  |
| SeriesUID | varchar(128) | 128 | 否 |  |
| StudyUID | varchar(128) | 128 | 否 |  |
| CurveType | int(10,0) | - | 否 |  |
| CurveGene | int(10,0) | - | 否 |  |
| WinLevel | int(10,0) | - | 否 |  |
| WinWidth | int(10,0) | - | 否 |  |
| IsRelativeZoom | int(10,0) | - | 是 |  |
| RelativeZoom | int(10,0) | - | 是 |  |
| ZoomPercent | int(10,0) | - | 是 |  |
| RelativeMoveX | int(10,0) | - | 是 |  |
| RelativeMoveY | int(10,0) | - | 是 |  |
| MoveX | int(10,0) | - | 否 |  |
| MoveY | int(10,0) | - | 否 |  |
| Intensity | int(10,0) | - | 否 |  |
| Contrast | int(10,0) | - | 否 |  |
| Gamma | int(10,0) | - | 否 |  |
| Inverse | int(10,0) | - | 是 |  |
| RotateAngle | int(10,0) | - | 否 |  |
| FlipStatus | int(10,0) | - | 否 |  |
| ClipRectX | int(10,0) | - | 是 |  |
| ClipRectY | int(10,0) | - | 是 |  |
| ClipRectWidth | int(10,0) | - | 是 |  |
| ClipRectHeight | int(10,0) | - | 是 |  |
| ImagePosition | int(10,0) | - | 否 |  |
| ImageSubPosition | int(10,0) | - | 否 |  |
| SubViewType | int(10,0) | - | 否 |  |
| Annotations | varchar(6000) | 6000 | 是 |  |
| InsertDate | datetime | - | 否 |  |
| IsScoutImage | int(10,0) | - | 否 |  |
| ShowScoutImage | int(10,0) | - | 是 |  |
| ScoutSeries | varchar(128) | 128 | 是 |  |
| ShowAnnotation | int(10,0) | - | 是 |  |
| LeftRightPos | varchar(2) | 2 | 是 |  |
| OffSetX | float | - | 是 |  |
| OffSetY | float | - | 是 |  |
| RoomID_Num | int(10,0) | - | 是 |  |
| FrameIndex | int(10,0) | - | 是 |  |

---

## FILM_ImageUIDDeleted

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| ID_Num | int(10,0) | - | 否 |  |
| UserID | varchar(12) | 12 | 否 |  |
| ImageUID | varchar(128) | 128 | 否 |  |
| InsertTime | datetime | - | 否 |  |
| DealTime | datetime | - | 是 |  |
| DealID | int(10,0) | - | 否 |  |

---

## FILM_MakeNum

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| UseName | varchar(20) | 20 | 是 |  |
| CurNum | int(10,0) | - | 否 |  |
| CurDate | datetime | - | 是 |  |

---

## FILM_StudyUIDSeriesUIDMapping

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| StudyUID | varchar(128) | 128 | 否 | ✓ |
| SeriesUID | varchar(128) | 128 | 否 |  |
| ImageNo | int(10,0) | - | 是 |  |

---

## FILM_UnRecogniseFilm

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| ID_NUM | int(10,0) | - | 否 |  |
| InsertTime | datetime | - | 是 |  |
| FullNetPath | varchar(512) | 512 | 是 |  |
| IpAddress | varchar(32) | 32 | 是 |  |
| Modality | varchar(32) | 32 | 是 |  |
| AETitle | varchar(64) | 64 | 是 |  |
| Status | varchar(64) | 64 | 是 |  |

---

## FILMING_CONSENSUS

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| FILMING_CONSENSUS_ID | numeric(19,0) | - | 否 | ✓ |
| SYSTEM_SOURCE_NO | nvarchar(32) | 32 | 是 |  |
| FILMING_POINT | nvarchar(-1) | -1 | 是 |  |
| STANDARD_IMAGE_DESC | nvarchar(-1) | -1 | 是 |  |
| EXAM_ITEM_NO | nvarchar(50) | 50 | 是 |  |
| VISIBLE_FLAG | numeric(19,0) | - | 是 |  |
| SEQ_NO | int(10,0) | - | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |

---

## FormatFilmInfoLog

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| SerialNo | numeric(18,0) | - | 否 |  |
| LogTime | datetime | - | 是 |  |
| LogContent | varchar(2000) | 2000 | 是 |  |
| LogUser | varchar(20) | 20 | 是 |  |
| SubSysCode | varchar(20) | 20 | 是 |  |

---

## HOSPITALINFO

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| HOSPITALCODE | varchar(50) | 50 | 否 | ✓ |
| HOSPITALNAME | varchar(100) | 100 | 否 |  |
| HOSPITALURL | varchar(200) | 200 | 是 |  |
| HOSPITALTYPE | varchar(100) | 100 | 是 |  |
| HOSPITALLEVEL | varchar(100) | 100 | 是 |  |
| AREA | varchar(100) | 100 | 是 |  |
| PARENTCODE | varchar(50) | 50 | 是 |  |
| VISIBLE | int(10,0) | - | 否 |  |
| HOSPHISURL | varchar(200) | 200 | 是 |  |
| HOSPSHORTNAME | varchar(20) | 20 | 是 |  |
| HOSPSHORTCODE | varchar(2) | 2 | 是 |  |
| DEALFLAG | int(10,0) | - | 是 |  |

---

## LIS_GRIDCONFIG

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| SERIALNO | int(10,0) | - | 否 | ✓ |
| DOMAIN | int(10,0) | - | 否 |  |
| USERID | varchar(20) | 20 | 是 |  |
| HOSPITALCODE | varchar(50) | 50 | 是 |  |
| SUBSYSCODE | varchar(20) | 20 | 否 |  |
| MODULECODE | varchar(255) | 255 | 是 |  |
| MODULENAME | varchar(500) | 500 | 是 |  |
| ORDERNO | int(10,0) | - | 是 |  |
| CAPTION | varchar(64) | 64 | 否 |  |
| FIELDNAME | varchar(64) | 64 | 否 |  |
| COLWIDTH | int(10,0) | - | 否 |  |
| SORTED | varchar(10) | 10 | 是 |  |
| REMARK | varchar(255) | 255 | 是 |  |
| RESERVEFIELD1 | varchar(64) | 64 | 是 |  |
| RESERVEFIELD2 | varchar(64) | 64 | 是 |  |
| RESERVEFIELD3 | varchar(64) | 64 | 是 |  |
| RESERVEFIELD4 | varchar(64) | 64 | 是 |  |
| RESERVEFIELD5 | varchar(64) | 64 | 是 |  |
| VISIBLE | char(1) | 1 | 是 |  |

---

## LIS_INSTRUMENT

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| HOSPITALCODE | varchar(50) | 50 | 否 |  |
| INSTID | int(10,0) | - | 否 | ✓ |
| DEPTNO | varchar(20) | 20 | 是 |  |
| INSTCODE | varchar(20) | 20 | 否 |  |
| INSTNAME | varchar(64) | 64 | 否 |  |
| INSTSHORTNAME | varchar(40) | 40 | 是 |  |
| DEFSAMPLE | varchar(20) | 20 | 是 |  |
| INDATATYPE | char(1) | 1 | 是 |  |
| SAMPLETYPE | varchar(2) | 2 | 是 |  |
| COMPUTER_NAME | varchar(255) | 255 | 是 |  |
| EXAMCODE | varchar(20) | 20 | 是 |  |
| BPLPATH | varchar(250) | 250 | 是 |  |
| REPORTTITLE | varchar(100) | 100 | 是 |  |
| REPORTINSTR | int(10,0) | - | 是 |  |
| SAMPLEADD | int(10,0) | - | 是 |  |
| TESTSYSID | varchar(20) | 20 | 是 |  |
| TESTMETHOD | varchar(50) | 50 | 是 |  |
| MODDATE | int(10,0) | - | 是 |  |
| SGBZ | int(10,0) | - | 是 |  |
| ISINSERTRESULT | int(10,0) | - | 是 |  |
| MZQSH | int(10,0) | - | 是 |  |
| ZYQSH | int(10,0) | - | 是 |  |
| JZQSH | int(10,0) | - | 是 |  |
| TJQSH | int(10,0) | - | 是 |  |
| DEVICEID | varchar(20) | 20 | 是 |  |
| SUBSYSCODE | varchar(20) | 20 | 是 |  |
| BUYTIME | datetime | - | 是 |  |
| INSTRPRICE | numeric(12,2) | - | 是 |  |
| LIMITTIME | numeric(6,2) | - | 是 |  |
| BUYCOMPANY | varchar(100) | 100 | 是 |  |
| REPAIRMETHOD | varchar(100) | 100 | 是 |  |
| FACTORY | varchar(40) | 40 | 是 |  |
| CONTACTPERSON | varchar(64) | 64 | 是 |  |
| CONTACTSPHONE | varchar(20) | 20 | 是 |  |
| WORKTYPE | int(10,0) | - | 是 |  |
| AUTO_LOAD | decimal(5,0) | - | 是 |  |
| DUPLEX_FLAG | decimal(5,0) | - | 是 |  |
| DILUTE_FLAG | char(1) | 1 | 是 |  |
| AUTOIN_FLAG | char(1) | 1 | 是 |  |
| COMM_PORT | varchar(6) | 6 | 是 |  |
| BAUD_RATE | decimal(10,0) | - | 是 |  |
| BYTE_SIZE | decimal(5,0) | - | 是 |  |
| PARITY | decimal(5,0) | - | 是 |  |
| STOP_BITS | decimal(5,0) | - | 是 |  |
| F_OUTX | decimal(5,0) | - | 是 |  |
| F_INX | decimal(5,0) | - | 是 |  |
| F_HARDWARE | decimal(5,0) | - | 是 |  |
| TX_QUEUESIZE | decimal(10,0) | - | 是 |  |
| RX_QUEUESIZE | decimal(10,0) | - | 是 |  |
| XOFF_LIM | decimal(10,0) | - | 是 |  |
| XON_CHAR | char(1) | 1 | 是 |  |
| XOFF_CHAR | char(1) | 1 | 是 |  |
| ERROR_CHAR | char(1) | 1 | 是 |  |
| EVENT_CHAR | char(1) | 1 | 是 |  |
| DRIVER_PROG | varchar(128) | 128 | 是 |  |
| SERVE_STATUS | char(1) | 1 | 是 |  |
| ITEM_TYPE | char(1) | 1 | 是 |  |
| XON_LIM | decimal(10,0) | - | 是 |  |
| PRIORITY | decimal(5,0) | - | 是 |  |
| DESCRIPTION | varchar(40) | 40 | 是 |  |
| CONNECT_DATE | datetime | - | 是 |  |
| DLLSTREAM | image(2147483647) | 2147483647 | 是 |  |
| DLLNAME | varchar(20) | 20 | 是 |  |
| LOGFLAG | char(1) | 1 | 是 |  |
| IP | varchar(50) | 50 | 是 |  |
| PORT | varchar(50) | 50 | 是 |  |
| FILENAME | varchar(50) | 50 | 是 |  |
| QC1 | int(10,0) | - | 是 |  |
| QC2 | int(10,0) | - | 是 |  |
| QC3 | int(10,0) | - | 是 |  |
| QC4 | int(10,0) | - | 是 |  |
| QC5 | int(10,0) | - | 是 |  |
| GRAPHFLAG | char(1) | 1 | 是 |  |
| FORMNAME | varchar(50) | 50 | 是 |  |
| TOTALSECTION | tinyint(3,0) | - | 是 |  |
| CUPPERSECT | tinyint(3,0) | - | 是 |  |
| JZSECTIONS | tinyint(3,0) | - | 是 |  |
| JZCUPSPERSECT | tinyint(3,0) | - | 是 |  |
| QCBATCHES | smallint(5,0) | - | 是 |  |
| QSRK | int(10,0) | - | 是 |  |
| BEFOREDATAFLAG | int(10,0) | - | 是 |  |
| AUTORUN | char(1) | 1 | 是 |  |
| PAUTORUN | char(1) | 1 | 是 |  |
| AUTODETECT | char(1) | 1 | 是 |  |
| CONVERTFORMAT | char(1) | 1 | 是 |  |
| OUTEXE | varchar(200) | 200 | 是 |  |
| OUTFILE | varchar(200) | 200 | 是 |  |
| DATAFILE | varchar(200) | 200 | 是 |  |
| LOG | char(1) | 1 | 是 |  |
| MISINST | char(1) | 1 | 是 |  |
| EXHOSPFLAG | char(1) | 1 | 是 |  |
| QCUPDATENEWDATA | char(1) | 1 | 是 |  |
| BIORADFLAG | char(1) | 1 | 是 |  |
| INSTGROUP | varchar(20) | 20 | 是 |  |
| INSTSIGNGROUP | varchar(100) | 100 | 是 |  |
| STOPFLAG | char(1) | 1 | 是 |  |
| LICENSENO | varchar(64) | 64 | 是 |  |
| PRINTIMAGE | char(1) | 1 | 是 |  |
| IMAGELIST | varchar(255) | 255 | 是 |  |
| DLLSTATUS | char(1) | 1 | 是 |  |
| INSTTYPE | varchar(20) | 20 | 是 |  |
| RELATEINSTID | varchar(255) | 255 | 是 |  |
| REDOSAMPLETYPE | varchar(2) | 2 | 是 |  |
| AUTOPUBFLAG | char(1) | 1 | 是 |  |
| PUBDELAYTIME | int(10,0) | - | 是 |  |
| AUTOPUBUSER | varchar(20) | 20 | 是 |  |
| MEMCODE1 | varchar(20) | 20 | 是 |  |
| MEMCODE2 | varchar(20) | 20 | 是 |  |
| ORDERNO | varchar(10) | 10 | 是 |  |

---

## LIS_PAGECONFIG_P

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| SERIALNO | int(10,0) | - | 否 | ✓ |
| SUBSYSCODE | varchar(20) | 20 | 否 |  |
| MODULECODE | varchar(64) | 64 | 否 |  |
| MODULENAME | varchar(64) | 64 | 否 |  |
| TABLENAME | varchar(64) | 64 | 否 |  |
| FIELDNAME | varchar(64) | 64 | 否 |  |
| FIELDINTER | varchar(64) | 64 | 否 |  |
| FIELDDESC | varchar(64) | 64 | 否 |  |
| EDITOR | varchar(20) | 20 | 否 |  |
| CLASSCODE | varchar(20) | 20 | 是 |  |
| MEMCODE1 | varchar(20) | 20 | 是 |  |
| MEMCODE2 | varchar(20) | 20 | 是 |  |
| IMENAME | varchar(40) | 40 | 是 |  |
| KEEPVALUE | char(1) | 1 | 是 |  |
| READONLY | char(1) | 1 | 是 |  |
| DEFVALUE | varchar(255) | 255 | 是 |  |
| HINTCONTENT | varchar(64) | 64 | 是 |  |
| ISCANADD | char(1) | 1 | 是 |  |
| ISSTRICT | char(1) | 1 | 是 |  |
| REMARK | varchar(255) | 255 | 是 |  |
| ORDERNO | int(10,0) | - | 是 |  |
| DETAILTYPE | varchar(64) | 64 | 是 |  |
| RESERVEFIELD1 | varchar(64) | 64 | 是 |  |
| RESERVEFIELD2 | varchar(64) | 64 | 是 |  |
| RESERVEFIELD3 | varchar(64) | 64 | 是 |  |
| RESERVEFIELD4 | varchar(64) | 64 | 是 |  |
| RESERVEFIELD5 | varchar(64) | 64 | 是 |  |
| VISIBLE | char(1) | 1 | 是 |  |
| STOPFLAG | char(1) | 1 | 是 |  |
| REQUIRED | char(1) | 1 | 是 |  |

---

## LIS_USERSACTION

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| SERIALNO | numeric(18,0) | - | 否 | ✓ |
| USERID | varchar(20) | 20 | 否 |  |
| MODULE | varchar(60) | 60 | 否 |  |
| PARAMCODE | varchar(200) | 200 | 是 |  |
| OPERATETIME | datetime | - | 否 |  |
| VALUE | varchar(200) | 200 | 是 |  |
| VALUEDESC | varchar(1000) | 1000 | 是 |  |
| DEFAULTVALUE | int(10,0) | - | 否 |  |

---

## LIS_USERSACTION_P

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| SERIALNO | numeric(18,0) | - | 否 | ✓ |
| MODULE | varchar(60) | 60 | 否 |  |
| PARAMCODE | varchar(60) | 60 | 否 |  |
| VALUE | varchar(200) | 200 | 否 |  |
| VALUEDESC | varchar(1000) | 1000 | 否 |  |

---

## MakeNum

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| UseName | varchar(100) | 100 | 否 |  |
| CurNum | int(10,0) | - | 是 |  |
| CurDate | datetime | - | 是 |  |

---

## MESSAGE_PUSH_RECORD

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| ID | numeric(18,0) | - | 否 | ✓ |
| APPLYNO | numeric(18,0) | - | 是 |  |
| PUSHSUCCESS | tinyint(3,0) | - | 是 |  |
| FAILCOUNT | tinyint(3,0) | - | 是 |  |
| FAILREASON | varchar(2000) | 2000 | 是 |  |
| TYPE | varchar(20) | 20 | 是 |  |
| SUBTYPE | varchar(10) | 10 | 是 |  |
| HOSPITALCODE | varchar(50) | 50 | 是 |  |

---

## MODALITY_PERF_PROC_STEP

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| MODALITY_PERF_PROC_STEP_ID | numeric(19,0) | - | 否 | ✓ |
| SYSTEM_SOURCE_NO | nvarchar(32) | 32 | 是 |  |
| PPS_STUDYUID | nvarchar(128) | 128 | 是 |  |
| PPS_START_AT | datetime | - | 是 |  |
| PPS_END_AT | datetime | - | 是 |  |
| STUDY_DESCRIPTION | nvarchar(128) | 128 | 是 |  |
| IMAGE_COUNT | int(10,0) | - | 是 |  |
| ACCESSION_NO | nvarchar(100) | 100 | 是 |  |
| TECH_NO | nvarchar(128) | 128 | 是 |  |
| ENGLISH_NAME | nvarchar(64) | 64 | 是 |  |
| EQUIPMENT_NAME | nvarchar(64) | 64 | 是 |  |
| PERF_PHYSICIAN_NAME | nvarchar(50) | 50 | 是 |  |
| PPS_MODALITY | nvarchar(64) | 64 | 是 |  |
| IMAGE_BODY_PART | nvarchar(100) | 100 | 是 |  |
| IMAGE_STUDY_AT | datetime | - | 是 |  |
| GENDER_CODE | numeric(19,0) | - | 是 |  |
| DICOM_META_INFO | varchar(512) | 512 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |

---

## Pacs_3DSeriesDescInfo

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| ID | int(10,0) | - | 否 | ✓ |
| SeriesDesc | varchar(256) | 256 | 否 |  |
| StationName | varchar(128) | 128 | 否 |  |
| Manufacturer | varchar(128) | 128 | 是 |  |
| InsertTime | datetime | - | 是 |  |
| ModifyTime | datetime | - | 是 |  |

---

## Pacs_AETitle

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| AEID | int(10,0) | - | 否 | ✓ |
| AEName | varchar(64) | 64 | 否 |  |
| AEType | varchar(32) | 32 | 否 |  |
| AETitle | varchar(16) | 16 | 否 |  |
| IP | varchar(32) | 32 | 否 |  |
| Port | int(10,0) | - | 否 |  |
| AEDesc | varchar(512) | 512 | 是 |  |
| AEType_StorageSCU | varchar(128) | 128 | 是 |  |
| AEType_StorageSCP | varchar(128) | 128 | 是 |  |
| AEType_QRSCU | varchar(128) | 128 | 是 |  |
| AEType_QRSCP | varchar(128) | 128 | 是 |  |
| AEType_StorageCommitSCP | varchar(128) | 128 | 是 |  |

---

## Pacs_AIResultDetail

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| ID | int(10,0) | - | 否 | ✓ |
| StudyUID | varchar(128) | 128 | 否 |  |
| SeriesUID | varchar(128) | 128 | 是 |  |
| ImageUID | varchar(128) | 128 | 是 |  |
| AIType | varchar(32) | 32 | 否 |  |
| AICompany | varchar(32) | 32 | 否 |  |
| AIItemCode | varchar(32) | 32 | 否 |  |
| AIItemResult | varchar(-1) | -1 | 是 |  |
| InsertTime | datetime | - | 是 |  |

---

## Pacs_AISendCondition

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| ID | int(10,0) | - | 否 | ✓ |
| AIType | varchar(128) | 128 | 是 |  |
| Condition | varchar(1024) | 1024 | 是 |  |

---

## Pacs_AISendImageTask

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| ID | int(10,0) | - | 否 | ✓ |
| StudyUID | varchar(128) | 128 | 否 |  |
| SeriesUID | varchar(128) | 128 | 否 |  |
| AIType | varchar(128) | 128 | 否 |  |
| InsertTime | datetime | - | 是 |  |
| ModifyTime | datetime | - | 是 |  |
| RetryTimes | int(10,0) | - | 是 |  |
| ErrorMsg | varchar(256) | 256 | 是 |  |
| ExecStatus | int(10,0) | - | 是 |  |

---

## Pacs_AllService

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| ServiceID | int(10,0) | - | 否 | ✓ |
| ServiceName | varchar(32) | 32 | 否 |  |
| ServiceDispName | varchar(64) | 64 | 否 |  |
| ServiceDesc | varchar(128) | 128 | 是 |  |
| InstallParam | varchar(64) | 64 | 是 |  |
| Enabled | int(10,0) | - | 是 |  |
| FileName | varchar(32) | 32 | 否 |  |
| LogPrefix | varchar(32) | 32 | 否 |  |

---

## Pacs_BackupDiscInfo

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| DiscID | varchar(16) | 16 | 否 | ✓ |
| PlanID | int(10,0) | - | 是 |  |
| JobID | int(10,0) | - | 是 |  |
| ComputerID | int(10,0) | - | 是 |  |
| BackupDesc | varchar(64) | 64 | 是 |  |
| BackupType | int(10,0) | - | 否 |  |
| BackupTime | datetime | - | 否 |  |
| CurrentSize | float | - | 否 |  |
| TempPath | varchar(256) | 256 | 是 |  |
| DiscStatus | int(10,0) | - | 否 |  |
| DeviceSize | float | - | 否 |  |
| BackupReport | varchar(5000) | 5000 | 是 |  |

---

## Pacs_BackupDownloadSeries

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| DiscID | varchar(16) | 16 | 否 |  |
| SeriesID | int(10,0) | - | 否 | ✓ |
| DownloadImageCount | int(10,0) | - | 是 |  |
| DownloadTime | datetime | - | 是 |  |
| CompressionID | int(10,0) | - | 是 |  |
| TotalFileSize | float | - | 是 |  |
| VerifyFailCount | int(10,0) | - | 否 |  |

---

## Pacs_BackupJob

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| JobID | int(10,0) | - | 否 | ✓ |
| PlanID | int(10,0) | - | 否 |  |
| ScheduleID | int(10,0) | - | 否 |  |
| Status | int(10,0) | - | 是 |  |
| DeviceSize | float | - | 是 |  |
| ComputerID | int(10,0) | - | 是 |  |
| CreateTime | datetime | - | 是 |  |
| BeginRunTime | datetime | - | 是 |  |
| EndRunTime | datetime | - | 是 |  |

---

## Pacs_BackupModality

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| PlanID | int(10,0) | - | 否 | ✓ |
| ModalityID | int(10,0) | - | 否 | ✓ |

---

## Pacs_BackupPlan

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| PlanID | int(10,0) | - | 否 | ✓ |
| PlanName | varchar(64) | 64 | 是 |  |
| CompressionID | int(10,0) | - | 是 |  |
| EndTime | int(10,0) | - | 否 |  |
| Enabled | int(10,0) | - | 是 |  |
| MaxDownloadDisc | int(10,0) | - | 是 |  |
| DeviceSize | float | - | 是 |  |
| ComputerID | int(10,0) | - | 否 |  |

---

## Pacs_BoneAgeCaleResult

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| ID | int(10,0) | - | 否 |  |
| StudyUID | varchar(128) | 128 | 是 |  |
| RestResult | varchar(2000) | 2000 | 是 |  |
| StudyDesc | varchar(2000) | 2000 | 是 |  |
| StudyResult | varchar(2000) | 2000 | 是 |  |
| ApplyNo | int(10,0) | - | 是 |  |
| ZWB | varchar(100) | 100 | 是 |  |
| ZGYD | varchar(100) | 100 | 是 |  |
| JJZGJD | varchar(100) | 100 | 是 |  |
| YJZGJD | varchar(500) | 500 | 是 |  |
| RGYD | varchar(500) | 500 | 是 |  |
| CGYD | varchar(500) | 500 | 是 |  |
| AGE | varchar(100) | 100 | 是 |  |
| STD | varchar(500) | 500 | 是 |  |
| Location | varchar(1000) | 1000 | 是 |  |
| Probability | varchar(100) | 100 | 是 |  |
| ImageUID | varchar(128) | 128 | 是 |  |
| SeriesUID | varchar(128) | 128 | 是 |  |
| LocationEx | varchar(400) | 400 | 是 |  |
| heatmap | varchar(5000) | 5000 | 是 |  |

---

## Pacs_CancelImageLog

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| CancelID | int(10,0) | - | 否 | ✓ |
| ImageID | int(10,0) | - | 是 |  |
| CancelTime | datetime | - | 是 |  |
| UserID | varchar(30) | 30 | 否 |  |
| SendStatus | int(10,0) | - | 是 |  |
| PatID | varchar(128) | 128 | 是 |  |
| AccessNo | varchar(64) | 64 | 是 |  |

---

## Pacs_Compression

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| CompressionID | int(10,0) | - | 否 | ✓ |
| Type | varchar(16) | 16 | 否 |  |
| Quality | int(10,0) | - | 是 |  |
| CompressDesc | varchar(128) | 128 | 是 |  |

---

## Pacs_Computer

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| ComputerID | int(10,0) | - | 否 | ✓ |
| ComputerName | varchar(64) | 64 | 是 |  |
| DeptNo | varchar(12) | 12 | 是 |  |
| Type | int(10,0) | - | 否 |  |
| HostName | varchar(64) | 64 | 是 |  |
| IP | char(15) | 15 | 是 |  |
| GroupID | int(10,0) | - | 是 |  |
| ComputerDesc | varchar(1024) | 1024 | 是 |  |
| MonitorFlag | int(10,0) | - | 是 |  |
| InsertTime | datetime | - | 是 |  |
| ModifyTime | datetime | - | 是 |  |
| ModifyUserID | varchar(12) | 12 | 是 |  |
| State | int(10,0) | - | 是 |  |
| HospCode | varchar(50) | 50 | 是 |  |
| HospName | varchar(50) | 50 | 是 |  |
| DeptName | varchar(50) | 50 | 是 |  |

---

## Pacs_DataFlowErrReport

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| InstanceID | int(10,0) | - | 否 | ✓ |
| AccessID | int(10,0) | - | 否 |  |
| ErrID | int(10,0) | - | 是 |  |
| ErrDesc | varchar(128) | 128 | 是 |  |
| ErrTime | datetime | - | 是 |  |

---

## Pacs_DataFlowSubTask

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| SubTaskID | int(10,0) | - | 否 | ✓ |
| ConditionID | int(10,0) | - | 是 |  |
| TaskID | int(10,0) | - | 否 |  |
| SeriesID | int(10,0) | - | 是 |  |
| InstanceID | int(10,0) | - | 否 |  |
| InsertTime | datetime | - | 是 |  |
| ModifyTime | datetime | - | 是 |  |
| RetryTimes | int(10,0) | - | 是 |  |
| ErrorMsg | varchar(256) | 256 | 是 |  |
| ExecStatus | int(10,0) | - | 是 |  |

---

## Pacs_DataFlowTask

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| TaskID | int(10,0) | - | 否 | ✓ |
| RuleID | int(10,0) | - | 否 |  |
| HostName | varchar(128) | 128 | 否 |  |
| ExecStatus | int(10,0) | - | 是 |  |
| BeginTime | datetime | - | 是 |  |
| EndTime | datetime | - | 是 |  |
| TotalCount | int(10,0) | - | 是 |  |
| TotalSize | int(10,0) | - | 是 |  |
| SuccessCount | int(10,0) | - | 是 |  |

---

## Pacs_DownloadMonitor

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| MonitorID | int(10,0) | - | 否 | ✓ |
| ComputerName | varchar(50) | 50 | 是 |  |
| DownloadTime | datetime | - | 是 |  |
| ImageNum | int(10,0) | - | 是 |  |
| Modality | varchar(50) | 50 | 是 |  |

---

## Pacs_ErrorCode

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| ErrorCode | int(10,0) | - | 否 | ✓ |
| Error | varchar(128) | 128 | 否 |  |

---

## Pacs_EstimateImages

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| SerialNo | numeric(18,0) | - | 否 | ✓ |
| ApplyNo | int(10,0) | - | 否 |  |
| EstimateID | int(10,0) | - | 否 |  |
| EstimateFlag | int(10,0) | - | 否 |  |
| Grade | varchar(10) | 10 | 是 |  |
| Reason | varchar(1000) | 1000 | 是 |  |
| Estimator | varchar(10) | 10 | 是 |  |
| EstimatorName | varchar(50) | 50 | 是 |  |
| EstimateTime | datetime | - | 是 |  |

---

## PACS_ExamineInfo

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| ID | int(10,0) | - | 否 |  |
| Host | varchar(64) | 64 | 否 |  |
| IP | varchar(64) | 64 | 是 |  |
| CheckType | varchar(64) | 64 | 否 |  |
| CheckModule | varchar(64) | 64 | 否 |  |
| ServiceName | varchar(64) | 64 | 是 |  |
| ServerStatus | int(10,0) | - | 是 |  |
| SpaceTotal | float | - | 是 |  |
| SpaceLeft | float | - | 是 |  |
| ImageCountInCheckTime | int(10,0) | - | 是 |  |
| ImageSizeInCheckTime | float | - | 是 |  |
| TimesInCheckTime | int(10,0) | - | 是 |  |
| Reserve1 | varchar(64) | 64 | 是 |  |
| Reserve2 | varchar(64) | 64 | 是 |  |
| Reserve3 | varchar(64) | 64 | 是 |  |
| Reserve4 | varchar(64) | 64 | 是 |  |
| Reserve5 | varchar(64) | 64 | 是 |  |
| CheckTime | datetime | - | 否 |  |

---

## Pacs_FilmList

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| FilmID | int(10,0) | - | 否 | ✓ |
| PrintTime | datetime | - | 是 |  |
| Pages | int(10,0) | - | 是 |  |
| Images | int(10,0) | - | 是 |  |
| Type | int(10,0) | - | 是 |  |
| FilmSize | varchar(16) | 16 | 是 |  |
| Orientation | int(10,0) | - | 是 |  |
| Printer | varchar(32) | 32 | 是 |  |
| UserID | varchar(64) | 64 | 是 |  |
| FilmDesc | varchar(512) | 512 | 是 |  |
| PrintCount | int(10,0) | - | 是 |  |

---

## Pacs_Group

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| GroupID | int(10,0) | - | 否 | ✓ |
| GroupName | varchar(64) | 64 | 是 |  |
| GroupDesc | varchar(128) | 128 | 是 |  |
| RouteID | int(10,0) | - | 否 |  |
| InsertTime | datetime | - | 是 |  |
| ModifyTime | datetime | - | 是 |  |
| ModifyUserID | varchar(12) | 12 | 是 |  |
| State | int(10,0) | - | 是 |  |

---

## PACS_HospitalWADOInfo

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| HospitalCode | varchar(20) | 20 | 否 |  |
| HospitalName | varchar(100) | 100 | 否 |  |
| IP | varchar(20) | 20 | 否 |  |
| Port | int(10,0) | - | 否 |  |
| WebServiceAddress | varchar(256) | 256 | 否 |  |

---

## Pacs_HPInstanceEx

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| ID | int(10,0) | - | 否 |  |
| nProtocolID | int(10,0) | - | 否 |  |
| ProtocolName | varchar(50) | 50 | 否 |  |
| ProtocolDateTime | varchar(50) | 50 | 否 |  |
| DisplayMode | varchar(50) | 50 | 否 |  |
| SpecHpMode | varchar(50) | 50 | 否 |  |
| RelationToStu | varchar(50) | 50 | 否 |  |
| RelationToSer | varchar(50) | 50 | 否 |  |
| StudyDec | varchar(200) | 200 | 是 |  |
| StudyCount | int(10,0) | - | 否 |  |
| SeriesCount | int(10,0) | - | 否 |  |
| ModalityInStudy | varchar(50) | 50 | 是 |  |
| DisplayScreen | int(10,0) | - | 否 |  |
| RoomMode | varchar(50) | 50 | 否 |  |
| RoomOrder | int(10,0) | - | 否 |  |
| ImageFilter | varchar(7000) | 7000 | 否 |  |
| WindowCenter | int(10,0) | - | 否 |  |
| WindowWidth | int(10,0) | - | 否 |  |
| Factor | int(10,0) | - | 否 |  |
| fFactor | float | - | 是 |  |
| Annotatation | varchar(50) | 50 | 否 |  |
| PatientOrientationR | varchar(50) | 50 | 否 |  |
| PatientOrientationF | varchar(50) | 50 | 否 |  |
| ImageRow | int(10,0) | - | 否 |  |
| ImageColumn | int(10,0) | - | 否 |  |
| SortCriteria | int(10,0) | - | 否 |  |
| SetWindowMode | varchar(50) | 50 | 是 |  |
| ZoomMode | varchar(50) | 50 | 是 |  |
| OrderNo | int(10,0) | - | 否 |  |
| UserID | varchar(50) | 50 | 是 |  |
| StudyBodyPart | varchar(50) | 50 | 是 |  |
| nSynchMode | int(10,0) | - | 是 |  |
| nSynchOptions | int(10,0) | - | 是 |  |
| RotateAngle | int(10,0) | - | 是 |  |
| MoveX | float | - | 是 |  |
| MoveY | float | - | 是 |  |
| stationName | varchar(255) | 255 | 是 |  |

---

## Pacs_ID_List_ImageID

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| Trad_Id | int(10,0) | - | 否 |  |
| IDName | char(1) | 1 | 是 |  |

---

## Pacs_ID_List_InstanceID

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| Trad_Id | int(10,0) | - | 否 |  |
| IDName | char(1) | 1 | 是 |  |

---

## Pacs_ID_List_SeriesID

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| Trad_Id | int(10,0) | - | 否 |  |
| IDName | char(1) | 1 | 是 |  |

---

## Pacs_ID_List_StudyID

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| Trad_Id | int(10,0) | - | 否 |  |
| IDName | char(1) | 1 | 是 |  |

---

## pacs_IF_task_list

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| ID_Key | int(10,0) | - | 否 |  |
| IFRuleid | int(10,0) | - | 是 |  |
| IFHost | varchar(200) | 200 | 否 |  |
| IFAETitle | varchar(200) | 200 | 是 |  |
| IFID | int(10,0) | - | 否 |  |
| IDType | int(10,0) | - | 否 |  |
| Target | varchar(200) | 200 | 否 |  |

---

## pacs_IFResult

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| ID_Key | int(10,0) | - | 否 |  |
| IFHost | varchar(200) | 200 | 否 |  |
| IFAETitle | varchar(200) | 200 | 是 |  |
| StudyID | int(10,0) | - | 否 |  |
| SeriesID | int(10,0) | - | 否 |  |
| ImageID | int(10,0) | - | 否 |  |
| Target | varchar(200) | 200 | 否 |  |
| InsertTime | datetime | - | 是 |  |
| ReSend | int(10,0) | - | 是 |  |
| Status | varchar(50) | 50 | 是 |  |
| TryTimes | int(10,0) | - | 是 |  |

---

## PACS_IFRule

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| RuleID | int(10,0) | - | 否 | ✓ |
| ConditionID | int(10,0) | - | 是 |  |
| RuleName | varchar(128) | 128 | 是 |  |
| RuleDesc | varchar(512) | 512 | 是 |  |
| computerID | int(10,0) | - | 否 |  |
| RuleStatus | int(10,0) | - | 是 |  |
| inserttime | datetime | - | 是 |  |
| Target | varchar(128) | 128 | 否 |  |
| modifytime | datetime | - | 是 |  |
| UseImmediateForward | int(10,0) | - | 是 |  |

---

## Pacs_Image

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| ImageID | int(10,0) | - | 否 | ✓ |
| StudyID | int(10,0) | - | 否 |  |
| SeriesID | int(10,0) | - | 否 |  |
| ImageUID | varchar(128) | 128 | 否 |  |
| AcqNo | int(10,0) | - | 是 |  |
| ImageNo | int(10,0) | - | 是 |  |
| ImageTime | datetime | - | 是 |  |
| ImageFormat | int(10,0) | - | 否 |  |
| ImageFile | varchar(256) | 256 | 否 |  |
| ImageFileSize | float | - | 是 |  |
| Caption | varchar(30) | 30 | 是 |  |
| TakeinTime | datetime | - | 否 |  |
| DataStatus | int(10,0) | - | 否 |  |
| CancelFlag | int(10,0) | - | 否 |  |
| InsertTime | datetime | - | 是 |  |
| nReserved1 | int(10,0) | - | 是 |  |
| cReserved1 | varchar(128) | 128 | 是 |  |
| cReserved2 | varchar(128) | 128 | 是 |  |
| ImageType | varchar(128) | 128 | 是 |  |
| ImageComments | varchar(2048) | 2048 | 是 |  |
| AcquisitionTime | datetime | - | 是 |  |
| ContentDateTime | datetime | - | 是 |  |
| PatientOrientation | varchar(50) | 50 | 是 |  |
| ImageFileSizeInByte | int(10,0) | - | 是 |  |
| TransferSyntaxUID | varchar(128) | 128 | 是 |  |
| SopClassUID | varchar(128) | 128 | 是 |  |
| SpecificCharacterSet | varchar(16) | 16 | 是 |  |
| Rows | int(10,0) | - | 是 |  |
| Columns | int(10,0) | - | 是 |  |
| BitAllocated | int(10,0) | - | 是 |  |
| BitStored | int(10,0) | - | 是 |  |
| HighBit | int(10,0) | - | 是 |  |
| WinWidth | varchar(64) | 64 | 是 |  |
| WinCenter | varchar(64) | 64 | 是 |  |
| WindowLevelExplanation | varchar(192) | 192 | 是 |  |
| ValidWindowLevel | int(10,0) | - | 是 |  |
| VOI_LUT_Length | int(10,0) | - | 是 |  |
| VOI_LUT_FirstPixel | int(10,0) | - | 是 |  |
| VOI_LUT_Bits | int(10,0) | - | 是 |  |
| PhotoMetric | varchar(64) | 64 | 是 |  |
| PixelRepresentation | int(10,0) | - | 是 |  |
| PixelSpacing | varchar(34) | 34 | 是 |  |
| ImagePixelSpacing | varchar(34) | 34 | 是 |  |
| FieldOfView | varchar(64) | 64 | 是 |  |
| RefUID | varchar(128) | 128 | 是 |  |
| SliceLoction | float | - | 是 |  |
| SliceThick | float | - | 是 |  |
| ImgPos | varchar(51) | 51 | 是 |  |
| ImageOrientation | varchar(102) | 102 | 是 |  |
| NumberOfFrames | int(10,0) | - | 是 |  |
| FrameTime | int(10,0) | - | 是 |  |
| CineRate | int(10,0) | - | 是 |  |
| MaskNum | int(10,0) | - | 是 |  |
| PixelAspectRatio | varchar(64) | 64 | 是 |  |
| RescaleSlope | float | - | 是 |  |
| RescaleIntercept | float | - | 是 |  |
| RescaleType | varchar(64) | 64 | 是 |  |
| cReserved3 | varchar(128) | 128 | 是 |  |
| ClipID | varchar(128) | 128 | 是 |  |
| ImageInInstance | varchar(50) | 50 | 是 |  |

---

## Pacs_ImageCondition

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| ConditionID | int(10,0) | - | 否 | ✓ |
| Name | varchar(64) | 64 | 是 |  |
| Condition | varchar(5000) | 5000 | 否 |  |
| ConditionDesc | varchar(1000) | 1000 | 是 |  |
| Type | int(10,0) | - | 是 |  |
| Enabled | int(10,0) | - | 否 |  |
| ModifyTime | datetime | - | 是 |  |
| ModifyUserID | varchar(12) | 12 | 是 |  |
| SqlType | int(10,0) | - | 否 |  |
| DefaultDFRuleID | int(10,0) | - | 是 |  |

---

## Pacs_ImageDataflowIDs

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| RunningID | int(10,0) | - | 否 |  |
| RuleID | int(10,0) | - | 否 |  |
| TaskSeriesID | int(10,0) | - | 否 |  |
| TaskSeriesCount | int(10,0) | - | 否 |  |
| CreateTime | datetime | - | 否 |  |
| ExecTime | datetime | - | 否 |  |

---

## Pacs_ImageJudgeInfo

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| ImageID | int(10,0) | - | 否 | ✓ |
| Quality | int(10,0) | - | 是 |  |
| Context | varchar(1000) | 1000 | 是 |  |

---

## pacs_ImageOperationRecordInfo

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| OperateDesp | varchar(32) | 32 | 是 |  |
| HostName | varchar(64) | 64 | 是 |  |
| HostIp | varchar(64) | 64 | 是 |  |
| RecordTime | datetime | - | 是 |  |
| StudyUID | varchar(128) | 128 | 是 |  |
| PatName | varchar(64) | 64 | 是 |  |
| CardNO | varchar(30) | 30 | 是 |  |
| AccessionNo | varchar(32) | 32 | 是 |  |
| PatID | varchar(64) | 64 | 是 |  |
| Modality | varchar(16) | 16 | 否 |  |
| StudyTime | datetime | - | 否 |  |
| SeriesUID | varchar(128) | 128 | 是 |  |
| ImageCount | int(10,0) | - | 是 |  |
| ImageType | varchar(16) | 16 | 是 |  |
| UserID | varchar(30) | 30 | 是 |  |

---

## Pacs_ImagePrinted

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| FilmID | int(10,0) | - | 否 |  |
| ImageID | int(10,0) | - | 否 |  |
| SeriesID | int(10,0) | - | 是 |  |
| StudyID | int(10,0) | - | 是 |  |
| ImageStatusID | int(10,0) | - | 是 |  |
| ImagePrintID | int(10,0) | - | 否 | ✓ |

---

## Pacs_ImageTransferForwardResult

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| ID | int(10,0) | - | 否 | ✓ |
| IFHost | varchar(128) | 128 | 否 |  |
| SeriesID | int(10,0) | - | 否 |  |
| SeriesUID | varchar(128) | 128 | 否 |  |
| TargetAEName | varchar(200) | 200 | 是 |  |
| Status | varchar(200) | 200 | 是 |  |
| InsertTime | datetime | - | 是 |  |

---

## Pacs_ImageValidate

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| InstanceID | int(10,0) | - | 否 | ✓ |
| SeriesID | int(10,0) | - | 否 |  |
| VolumeID | int(10,0) | - | 否 |  |
| ImageCount | int(10,0) | - | 是 |  |
| RealImageCount | int(10,0) | - | 是 |  |
| ImagePath | varchar(64) | 64 | 是 |  |
| ModifyTime | datetime | - | 是 |  |

---

## Pacs_Instance

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| InstanceID | int(10,0) | - | 否 | ✓ |
| SeriesID | int(10,0) | - | 否 |  |
| VolumeID | int(10,0) | - | 否 |  |
| CompressionID | int(10,0) | - | 是 |  |
| CopySource | int(10,0) | - | 是 |  |
| ImageCount | int(10,0) | - | 是 |  |
| TotalFileSize | float | - | 是 |  |
| DeleteFlag | int(10,0) | - | 是 |  |
| DiscID | varchar(16) | 16 | 是 |  |
| InsertTime | datetime | - | 是 |  |
| ModifyTime | datetime | - | 是 |  |
| ModifyUserID | varchar(12) | 12 | 是 |  |
| State | int(10,0) | - | 是 |  |
| nReserved1 | int(10,0) | - | 是 |  |
| cReserved1 | varchar(64) | 64 | 是 |  |
| cReserved2 | varchar(64) | 64 | 是 |  |
| cReserved3 | varchar(64) | 64 | 是 |  |

---

## pacs_InstanceFileStatus

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| VolumeID | int(10,0) | - | 是 |  |
| AccessID | int(10,0) | - | 是 |  |
| ImageID | int(10,0) | - | 是 |  |
| InstanceID | int(10,0) | - | 是 |  |
| ImageSize | float | - | 是 |  |
| ImageStatus | varchar(20) | 20 | 是 |  |
| ScanDateTime | datetime | - | 是 |  |

---

## Pacs_KOImage

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| KOImageID | int(10,0) | - | 否 | ✓ |
| ImageID | int(10,0) | - | 否 |  |
| ImageNo | int(10,0) | - | 是 |  |
| StudyUID | varchar(128) | 128 | 是 |  |
| ImageType | int(10,0) | - | 是 |  |
| RelImageID | int(10,0) | - | 否 |  |

---

## Pacs_Login

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| LoginID | int(10,0) | - | 否 | ✓ |
| HostName | varchar(64) | 64 | 是 |  |
| IP | char(15) | 15 | 是 |  |
| SubSysCode | varchar(8) | 8 | 是 |  |
| LoginTime | datetime | - | 是 |  |
| LoginUser | varchar(12) | 12 | 是 |  |
| LogoutTime | datetime | - | 是 |  |
| Reserved1 | varchar(256) | 256 | 是 |  |
| Reserved2 | varchar(256) | 256 | 是 |  |

---

## Pacs_MakeNum

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| NoName | char(40) | 40 | 否 | ✓ |
| CurrNo | int(10,0) | - | 是 |  |
| CurrDate | datetime | - | 是 |  |

---

## Pacs_MMCTreeViewConfig

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| TV_ID | int(10,0) | - | 否 |  |
| TV_Name | varchar(100) | 100 | 是 |  |
| Parent | int(10,0) | - | 是 |  |
| InitTree | int(10,0) | - | 是 |  |
| DetailFlag | int(10,0) | - | 是 |  |
| DetailDLL | varchar(100) | 100 | 是 |  |
| MenuDLL | varchar(100) | 100 | 是 |  |
| OrderNo | int(10,0) | - | 是 |  |
| Icon | varchar(255) | 255 | 是 |  |
| SubSysCode | varchar(20) | 20 | 否 |  |

---

## Pacs_Modality

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| ModalityID | int(10,0) | - | 否 | ✓ |
| ModalityName | varchar(16) | 16 | 否 |  |
| ModalityDesc | varchar(64) | 64 | 是 |  |
| ReportID | int(10,0) | - | 否 |  |

---

## Pacs_Monitor

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| MonitorID | int(10,0) | - | 否 | ✓ |
| Type | int(10,0) | - | 否 |  |
| ComputerID | int(10,0) | - | 是 |  |
| ImageID | int(10,0) | - | 否 |  |
| ImageFileSize | float | - | 否 |  |
| TimeBegin | datetime | - | 否 |  |
| TimeEnd | datetime | - | 是 |  |
| TotalTime | int(10,0) | - | 否 |  |
| ErrorFlag | int(10,0) | - | 否 |  |
| ErrorReason | varchar(256) | 256 | 是 |  |

---

## Pacs_Operation

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| OpID | int(10,0) | - | 否 |  |
| GrpID | int(10,0) | - | 否 |  |
| ImageID | int(10,0) | - | 否 |  |
| StudyUID | varchar(128) | 128 | 是 |  |
| WinLevel | int(10,0) | - | 是 |  |
| WinWidth | int(10,0) | - | 是 |  |
| Bright | int(10,0) | - | 是 |  |
| Contrast | int(10,0) | - | 是 |  |
| Inverse | int(10,0) | - | 是 |  |
| Zoom | int(10,0) | - | 是 |  |
| State | int(10,0) | - | 是 |  |
| Annotations | varchar(-1) | -1 | 是 |  |
| IsKey | int(10,0) | - | 是 |  |
| CreateTime | datetime | - | 是 |  |
| ModifyTime | datetime | - | 是 |  |
| IsPrinted | int(10,0) | - | 是 |  |

---

## Pacs_OperationLog

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| ID | int(10,0) | - | 否 | ✓ |
| UserID | varchar(64) | 64 | 是 |  |
| LocalHost | varchar(64) | 64 | 是 |  |
| ProductType | varchar(32) | 32 | 是 |  |
| LogType | varchar(64) | 64 | 是 |  |
| OperationType | varchar(64) | 64 | 是 |  |
| DetailLog | varchar(2048) | 2048 | 是 |  |
| StudyUID | varchar(128) | 128 | 是 |  |
| SeriesUID | varchar(128) | 128 | 是 |  |
| ImageUID | varchar(128) | 128 | 是 |  |
| Reserve1 | varchar(128) | 128 | 是 |  |
| Reserve2 | varchar(128) | 128 | 是 |  |
| Reserve3 | varchar(128) | 128 | 是 |  |
| InsertTime | datetime | - | 否 |  |

---

## Pacs_PatID_Prefix

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| Modality | varchar(16) | 16 | 否 | ✓ |
| PatID_Prefix | varchar(6) | 6 | 是 |  |
| Prefix_Desc | varchar(128) | 128 | 是 |  |
| ModifyTime | datetime | - | 是 |  |

---

## PACS_PreGetImageStatus

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| IDNUM | int(10,0) | - | 否 |  |
| HospitalCode | varchar(20) | 20 | 否 |  |
| HostName | varchar(40) | 40 | 否 |  |
| StudyUID | varchar(128) | 128 | 否 |  |
| Status | varchar(50) | 50 | 否 |  |

---

## PACS_PreGetImageTrigger

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| IDNUM | int(10,0) | - | 否 |  |
| StudyUID | varchar(128) | 128 | 否 |  |
| ApplicantHospitalCode | varchar(20) | 20 | 否 |  |
| ConsultationHospitalCode | varchar(200) | 200 | 否 |  |
| InsertDateTime | datetime | - | 否 |  |

---

## pacs_RMMeetingApplicant

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| Applicant_ID | int(10,0) | - | 否 | ✓ |
| MeetingIdentify | varchar(100) | 100 | 否 |  |
| MyIdentify | varchar(100) | 100 | 否 |  |
| PCName | varchar(100) | 100 | 否 |  |

---

## pacs_RMOperatorStatus

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| OperatorStatus_ID | int(10,0) | - | 否 | ✓ |
| MeetingIdentify | varchar(100) | 100 | 否 |  |
| RoomInfo | varchar(1000) | 1000 | 否 |  |
| CurrentIndex | varchar(10) | 10 | 否 |  |
| Rows | varchar(10) | 10 | 否 |  |
| Cols | varchar(10) | 10 | 否 |  |
| WL | varchar(20) | 20 | 否 |  |
| ZOOM | varchar(50) | 50 | 否 |  |
| Pan | varchar(50) | 50 | 否 |  |
| Events | varchar(500) | 500 | 否 |  |

---

## pacs_RMRomoteMeetings

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| Meeting_ID | int(10,0) | - | 否 | ✓ |
| Identify | varchar(100) | 100 | 否 |  |
| CurrentOperater | varchar(100) | 100 | 否 |  |
| PassWord | varchar(100) | 100 | 否 |  |

---

## Pacs_Route

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| RouteID | int(10,0) | - | 否 | ✓ |
| RouteName | varchar(64) | 64 | 是 |  |
| IsDefault | int(10,0) | - | 是 |  |
| RouteDesc | varchar(128) | 128 | 是 |  |
| InsertTime | datetime | - | 是 |  |
| ModifyTime | datetime | - | 是 |  |
| ModifyUserID | varchar(12) | 12 | 是 |  |
| State | int(10,0) | - | 是 |  |

---

## Pacs_RouteOrder

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| OrderID | int(10,0) | - | 否 | ✓ |
| OrderNo | int(10,0) | - | 否 |  |
| RouteID | int(10,0) | - | 否 |  |
| AccessID | int(10,0) | - | 否 |  |

---

## Pacs_Rule

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| RuleID | int(10,0) | - | 否 | ✓ |
| ConditionID | int(10,0) | - | 是 |  |
| RuleName | varchar(64) | 64 | 是 |  |
| RuleType | int(10,0) | - | 否 |  |
| VolumeID | int(10,0) | - | 否 |  |
| AccessID | int(10,0) | - | 否 |  |
| DefaultPriority | int(10,0) | - | 是 |  |
| CompressionID | int(10,0) | - | 是 |  |
| DeleteSource | int(10,0) | - | 是 |  |
| ComputerID | int(10,0) | - | 否 |  |
| EnableRunStart | int(10,0) | - | 是 |  |
| EnableRunEnd | int(10,0) | - | 是 |  |
| RuleStatus | int(10,0) | - | 否 |  |
| RuleDesc | varchar(128) | 128 | 是 |  |
| InsertTime | datetime | - | 是 |  |
| ModifyTime | datetime | - | 是 |  |
| ModifyUserID | varchar(12) | 12 | 是 |  |
| State | int(10,0) | - | 是 |  |

---

## Pacs_RuleDestVolume

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| RuleID | int(10,0) | - | 否 | ✓ |
| VolumeID | int(10,0) | - | 否 | ✓ |
| AccessID | int(10,0) | - | 否 |  |
| OrderNo | int(10,0) | - | 是 |  |

---

## Pacs_Schedule

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| ScheduleID | int(10,0) | - | 否 | ✓ |
| RuleID | int(10,0) | - | 否 |  |
| ScheduleType | int(10,0) | - | 否 |  |
| ScheduleName | varchar(128) | 128 | 是 |  |
| Enabled | int(10,0) | - | 是 |  |
| Freq_Type | int(10,0) | - | 是 |  |
| Freq_Interval | int(10,0) | - | 是 |  |
| Freq_Subday_Type | int(10,0) | - | 是 |  |
| Freq_Subday_Interval | int(10,0) | - | 是 |  |
| Freq_Relative_Interval | int(10,0) | - | 是 |  |
| Freq_Recurrence_Factor | int(10,0) | - | 是 |  |
| Active_Start_Date | int(10,0) | - | 是 |  |
| Active_End_Date | int(10,0) | - | 是 |  |
| Active_Start_Time | int(10,0) | - | 是 |  |
| Active_End_Time | int(10,0) | - | 是 |  |
| Next_Run_Date | int(10,0) | - | 是 |  |
| Next_Run_Time | int(10,0) | - | 是 |  |
| SubSysCode | varchar(20) | 20 | 是 |  |

---

## Pacs_Series

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| SeriesID | int(10,0) | - | 否 | ✓ |
| StudyID | int(10,0) | - | 否 |  |
| Modality | varchar(16) | 16 | 否 |  |
| SeriesUID | varchar(128) | 128 | 否 |  |
| SeriesNo | int(10,0) | - | 否 |  |
| SeriesTime | datetime | - | 否 |  |
| SeriesDesc | varchar(64) | 64 | 是 |  |
| OperatorDoctor | varchar(64) | 64 | 是 |  |
| BodyPart | varchar(32) | 32 | 是 |  |
| StationName | varchar(16) | 16 | 是 |  |
| ImageCount | int(10,0) | - | 是 |  |
| ImagePath | varchar(256) | 256 | 否 |  |
| TakeinTime | datetime | - | 否 |  |
| BackupStatus | int(10,0) | - | 否 |  |
| BackupFailCount | int(10,0) | - | 是 |  |
| InsertTime | datetime | - | 是 |  |
| nReserved1 | int(10,0) | - | 是 |  |
| cReserved1 | varchar(128) | 128 | 是 |  |
| cReserved2 | varchar(128) | 128 | 是 |  |
| ProtocolName | varchar(64) | 64 | 是 |  |
| Laterality | varchar(16) | 16 | 是 |  |
| PatientPosition | varchar(16) | 16 | 是 |  |
| ViewPosition | varchar(16) | 16 | 是 |  |
| Manufacturer | varchar(64) | 64 | 是 |  |
| ManufacturerModelName | varchar(64) | 64 | 是 |  |
| InstitutionName | varchar(64) | 64 | 是 |  |
| InstitutionDepartmentName | varchar(64) | 64 | 是 |  |
| InstitutionAddress | varchar(1024) | 1024 | 是 |  |
| DeviceSerialNumber | varchar(64) | 64 | 是 |  |
| cReserved3 | varchar(128) | 128 | 是 |  |

---

## Pacs_SeriesIDForCheck

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| Num | int(10,0) | - | 否 |  |
| SeriesID | int(10,0) | - | 是 |  |
| ExecStatus | int(10,0) | - | 是 |  |

---

## Pacs_Service

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| ServiceID | int(10,0) | - | 否 | ✓ |
| ServiceName | varchar(64) | 64 | 否 |  |
| ComputerID | int(10,0) | - | 否 |  |
| ServiceStatus | int(10,0) | - | 是 |  |
| ServiceDesc | varchar(128) | 128 | 是 |  |
| ServiceIP | varchar(15) | 15 | 是 |  |
| ServicePort | int(10,0) | - | 是 |  |
| AETitle | varchar(64) | 64 | 是 |  |
| ActiveTime | datetime | - | 是 |  |

---

## Pacs_Study

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| StudyID | int(10,0) | - | 否 | ✓ |
| StudyNo | varchar(64) | 64 | 是 |  |
| StudyUID | varchar(128) | 128 | 否 |  |
| Modality | varchar(16) | 16 | 否 |  |
| StudyTime | datetime | - | 否 |  |
| StudyDoctor | varchar(64) | 64 | 是 |  |
| StudyDesc | varchar(64) | 64 | 是 |  |
| ApplyNo | int(10,0) | - | 是 |  |
| PatID | varchar(64) | 64 | 否 |  |
| OthID | varchar(64) | 64 | 是 |  |
| AccessionNo | varchar(32) | 32 | 是 |  |
| PatName | varchar(128) | 128 | 否 |  |
| Name_Chn | varchar(128) | 128 | 是 |  |
| Sex | char(1) | 1 | 否 |  |
| Birthday | datetime | - | 是 |  |
| Age | varchar(16) | 16 | 是 |  |
| TakeinTime | datetime | - | 否 |  |
| StudyType | int(10,0) | - | 否 |  |
| DataStatus | int(10,0) | - | 否 |  |
| InsertTime | datetime | - | 是 |  |
| nReserved1 | int(10,0) | - | 是 |  |
| cReserved1 | varchar(128) | 128 | 是 |  |
| cReserved2 | varchar(128) | 128 | 是 |  |
| PatientComments | varchar(2048) | 2048 | 是 |  |
| PatientSize | varchar(16) | 16 | 是 |  |
| PatientWeight | varchar(16) | 16 | 是 |  |
| SeriesCountInStudy | int(10,0) | - | 是 |  |
| ImageCountInStudy | int(10,0) | - | 是 |  |
| BackupStatus | int(10,0) | - | 是 |  |
| BackupFailCount | int(10,0) | - | 是 |  |
| cReserved3 | varchar(128) | 128 | 是 |  |
| HospCode | varchar(50) | 50 | 是 |  |
| HospName | varchar(50) | 50 | 是 |  |
| DeptNo | varchar(12) | 12 | 是 |  |
| DeptName | varchar(50) | 50 | 是 |  |

---

## Pacs_StudyResult

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| ID | int(10,0) | - | 否 | ✓ |
| StudyUID | varchar(128) | 128 | 是 |  |
| RestResult | varchar(2000) | 2000 | 是 |  |
| StudyDesc | varchar(2000) | 2000 | 是 |  |
| StudyResult | varchar(2000) | 2000 | 是 |  |
| ApplyNo | int(10,0) | - | 是 |  |

---

## Pacs_SwapPatIDAndName

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| Modality | varchar(16) | 16 | 否 | ✓ |
| IsSwap | int(10,0) | - | 是 |  |
| Note | varchar(256) | 256 | 是 |  |

---

## Pacs_SysDict

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| TableName | varchar(20) | 20 | 否 | ✓ |
| FieldName | varchar(64) | 64 | 否 | ✓ |
| nValue | int(10,0) | - | 否 | ✓ |
| cValue | varchar(256) | 256 | 否 | ✓ |
| Note | varchar(256) | 256 | 是 |  |

---

## Pacs_t_Schedule

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| ScheduleID | int(10,0) | - | 是 |  |
| RuleID | int(10,0) | - | 是 |  |
| ScheduleType | int(10,0) | - | 是 |  |
| SubSysCode | int(10,0) | - | 是 |  |
| ComputerID | int(10,0) | - | 是 |  |

---

## PACS_TA6_ServerInfo

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| ID | int(10,0) | - | 否 |  |
| Host | varchar(50) | 50 | 否 |  |
| IP | varchar(50) | 50 | 是 |  |
| ServiceName | varchar(50) | 50 | 否 |  |
| Module | varchar(50) | 50 | 否 |  |
| ServiceStatus | int(10,0) | - | 否 |  |
| SpaceTotal | int(10,0) | - | 是 |  |
| SpaceLeft | int(10,0) | - | 是 |  |
| ModifyTime | datetime | - | 否 |  |

---

## PACS_TA6_Service

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| ID_Num | int(10,0) | - | 否 |  |
| service_name | varchar(128) | 128 | 是 |  |
| host | varchar(128) | 128 | 是 |  |
| module | varchar(128) | 128 | 是 |  |
| property_name | varchar(128) | 128 | 是 |  |
| property_value | varchar(128) | 128 | 是 |  |
| datatype | varchar(128) | 128 | 是 |  |

---

## Pacs_TaskDestVolume

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| TaskID | int(10,0) | - | 否 | ✓ |
| VolumeID | int(10,0) | - | 否 | ✓ |
| AccessID | int(10,0) | - | 否 |  |

---

## Pacs_TechData

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| ID | int(10,0) | - | 否 | ✓ |
| DataType | int(10,0) | - | 否 |  |
| HostName | varchar(1024) | 1024 | 否 |  |
| InsertTime | datetime | - | 是 |  |
| Section | varchar(1024) | 1024 | 否 |  |
| Entry | varchar(1024) | 1024 | 否 |  |
| Value | varchar(1024) | 1024 | 是 |  |

---

## Pacs_TechMonitorPubSet

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| ID | int(10,0) | - | 否 | ✓ |
| Section | varchar(128) | 128 | 否 |  |
| Entry | varchar(128) | 128 | 否 |  |
| Value | varchar(1000) | 1000 | 否 |  |
| Comment | varchar(1000) | 1000 | 否 |  |

---

## Pacs_TechMonitorSet

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| ID | int(10,0) | - | 否 | ✓ |
| HostName | varchar(128) | 128 | 否 |  |
| Section | varchar(128) | 128 | 否 |  |
| Entry | varchar(128) | 128 | 否 |  |
| Value | varchar(1000) | 1000 | 是 |  |
| TakeTime | datetime | - | 是 |  |

---

## Pacs_TechWarnInfo

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| ID | int(10,0) | - | 否 | ✓ |
| HostName | varchar(128) | 128 | 否 |  |
| InsertTime | datetime | - | 是 |  |
| ErrorMsg | varchar(1024) | 1024 | 是 |  |
| EventID | int(10,0) | - | 是 |  |
| SendFlag | int(10,0) | - | 是 |  |

---

## Pacs_UserFilmLayOut

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| OPId | int(10,0) | - | 否 |  |
| UserID | varchar(30) | 30 | 否 |  |
| layName | varchar(50) | 50 | 否 |  |
| layPlan | varchar(8000) | 8000 | 否 |  |

---

## Pacs_UsersSettings

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| SerialNo | numeric(18,0) | - | 否 | ✓ |
| SubSysCode | varchar(20) | 20 | 否 |  |
| Domain | int(10,0) | - | 否 |  |
| DomainName | varchar(20) | 20 | 否 |  |
| Section | varchar(50) | 50 | 否 |  |
| Entry | varchar(50) | 50 | 否 |  |
| DataType | varchar(20) | 20 | 是 |  |
| Value | varchar(6000) | 6000 | 是 |  |
| Visible | int(10,0) | - | 否 |  |
| Comment | varchar(250) | 250 | 是 |  |
| HOSPITALCODE | varchar(50) | 50 | 否 |  |

---

## Pacs_UsersSettingsDic

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| SerialNo | numeric(18,0) | - | 否 | ✓ |
| SubSysCode | varchar(20) | 20 | 否 |  |
| Domain | int(10,0) | - | 否 |  |
| Section | varchar(50) | 50 | 否 |  |
| Entry | varchar(50) | 50 | 否 | ✓ |
| DataType | varchar(20) | 20 | 是 |  |
| Value | varchar(6000) | 6000 | 是 |  |
| OptionValue | varchar(250) | 250 | 是 |  |
| Visible | int(10,0) | - | 否 |  |
| Comment | varchar(250) | 250 | 是 |  |
| RegistryFullPath | varchar(255) | 255 | 是 |  |
| Owner | varchar(50) | 50 | 否 |  |

---

## Pacs_ViewSettings

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| SerialNo | numeric(18,0) | - | 否 | ✓ |
| SubSysCode | varchar(20) | 20 | 否 |  |
| Domain | int(10,0) | - | 否 |  |
| DomainName | varchar(20) | 20 | 否 |  |
| Section | varchar(20) | 20 | 否 |  |
| GroupName | varchar(20) | 20 | 是 |  |
| Entry | varchar(50) | 50 | 否 |  |
| DataType | varchar(20) | 20 | 是 |  |
| Value | varchar(7000) | 7000 | 是 |  |
| Visible | int(10,0) | - | 否 |  |
| Comment | varchar(250) | 250 | 是 |  |
| HospCode | varchar(50) | 50 | 是 |  |

---

## Pacs_ViewTag

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| SerialNo | numeric(18,0) | - | 否 | ✓ |
| Modality | varchar(16) | 16 | 否 |  |
| GroupNum | varchar(4) | 4 | 否 |  |
| ElementNum | varchar(4) | 4 | 否 |  |
| TagDesc | varchar(64) | 64 | 否 |  |

---

## Pacs_Volume

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| VolumeID | int(10,0) | - | 否 | ✓ |
| Label | varchar(64) | 64 | 是 |  |
| LocalPath | varchar(256) | 256 | 是 |  |
| DefaultAccessID | int(10,0) | - | 是 |  |
| VolumeDesc | varchar(128) | 128 | 是 |  |
| DeptNo | varchar(12) | 12 | 是 |  |
| ComputerID | int(10,0) | - | 是 |  |
| SpaceLimit | int(10,0) | - | 是 |  |
| SpaceUsed | float | - | 是 |  |
| SpaceTotal | float | - | 是 |  |
| VolumeType | int(10,0) | - | 是 |  |
| MonitorFlag | int(10,0) | - | 是 |  |
| Online | int(10,0) | - | 是 |  |
| VolumeStatus | int(10,0) | - | 是 |  |
| InsertTime | datetime | - | 是 |  |
| ModifyTime | datetime | - | 是 |  |
| ModifyUserID | varchar(12) | 12 | 是 |  |
| State | int(10,0) | - | 是 |  |
| SubNode | varchar(20) | 20 | 是 |  |

---

## Pacs_VolumeAccess

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| AccessID | int(10,0) | - | 否 | ✓ |
| VolumeID | int(10,0) | - | 否 |  |
| AccessType | int(10,0) | - | 否 |  |
| RemotePath | varchar(256) | 256 | 是 |  |
| Host | varchar(64) | 64 | 是 |  |
| IP | char(80) | 80 | 是 |  |
| Port | int(10,0) | - | 是 |  |
| UserName | varchar(64) | 64 | 是 |  |
| UserPassword | varchar(64) | 64 | 是 |  |
| AccessDesc | varchar(128) | 128 | 是 |  |
| InsertTime | datetime | - | 是 |  |
| ModifyTime | datetime | - | 是 |  |
| ModifyUserID | varchar(12) | 12 | 是 |  |

---

## Pacs_WithRIS

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| TAG | varchar(64) | 64 | 否 | ✓ |
| Value | varchar(64) | 64 | 否 | ✓ |
| PacsField | varchar(64) | 64 | 是 |  |
| RisField | varchar(64) | 64 | 是 |  |
| Note | varchar(64) | 64 | 是 |  |

---

## PATHOLOGICAL_NO_RULE

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| PATHOLOGICAL_NO_RULE_ID | numeric(19,0) | - | 否 |  |
| RULE_NAME | nvarchar(64) | 64 | 否 |  |
| CURRENT_NUMBER | int(10,0) | - | 是 |  |
| LAST_NEW_NUMBER | int(10,0) | - | 是 |  |
| PRE_PART | nvarchar(16) | 16 | 是 |  |
| SUFFIX_PART | nvarchar(16) | 16 | 是 |  |
| CREATE_MODE_CODE | numeric(19,0) | - | 是 |  |
| CREATE_CYCLE_CODE | numeric(19,0) | - | 是 |  |
| ZERO_FILL_LENGTH | smallint(5,0) | - | 是 |  |
| RECYCLE_FLAG | numeric(19,0) | - | 否 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(128) | 128 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(128) | 128 | 是 |  |

---

## PIS_ARCHIVE

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| PIS_ARCHIVE_ID | numeric(19,0) | - | 否 |  |
| ARCHIVED_OBJECT_CATEGORY | numeric(19,0) | - | 否 |  |
| ARCHIVED_OBJECT_ID | numeric(19,0) | - | 否 |  |
| ARCHIVED_OBJECT_NO | varchar(32) | 32 | 是 |  |
| PIS_STORAGE_UNIT_ID | numeric(19,0) | - | 是 |  |
| ARCHIVING_DOCTOR_ID | numeric(19,0) | - | 是 |  |
| ARCHIVING_DOCTOR_NO | varchar(32) | 32 | 是 |  |
| ARCHIVING_DOCTOR_NAME | nvarchar(128) | 128 | 是 |  |
| ARCHIVED_AT | datetime | - | 是 |  |
| REMARK | nvarchar(128) | 128 | 是 |  |
| CANCEL_FLAG | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(128) | 128 | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(128) | 128 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |

---

## PIS_BORROWING

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| PIS_BORROWING_ID | numeric(19,0) | - | 否 |  |
| BORROWING_NO | varchar(16) | 16 | 是 |  |
| BORROWING_PURPOSE_CATEGORY | numeric(19,0) | - | 否 |  |
| BORROWING_PURPOSE | varchar(64) | 64 | 是 |  |
| BORROWING_DESTINATION | nvarchar(128) | 128 | 是 |  |
| BORROWING_CONTACT_NAME | nvarchar(256) | 256 | 是 |  |
| BORROWING_CONTACT_PHONE | varchar(32) | 32 | 是 |  |
| BORROWING_IDCARD_TYPE_CODE | numeric(19,0) | - | 是 |  |
| BORROWING_IDCARD_NO | varchar(18) | 18 | 是 |  |
| LENDING_DOCTOR_ID | numeric(19,0) | - | 是 |  |
| LENDING_DOCTOR_NO | varchar(32) | 32 | 是 |  |
| LENDING_DOCTOR_NAME | nvarchar(128) | 128 | 是 |  |
| LENDING_AT | datetime | - | 是 |  |
| BORROWING_DEPOSIT | numeric(16,6) | - | 是 |  |
| BORROWING_REMARK | nvarchar(512) | 512 | 是 |  |
| PLANNED_RETURNED_AT | datetime | - | 是 |  |
| CONSULTATION_RESULT | nvarchar(1024) | 1024 | 是 |  |
| MODIFIED_NAME | nvarchar(128) | 128 | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(128) | 128 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| CONSULTATION_FLAG | numeric(19,0) | - | 是 |  |

---

## PIS_BORROWING_DETAIL

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| PIS_BORROWING_DETAIL_ID | numeric(19,0) | - | 否 |  |
| PIS_EXAM_TASK_ID | numeric(19,0) | - | 是 |  |
| PIS_BORROWING_ID | numeric(19,0) | - | 是 |  |
| PIS_ARCHIVE_ID | numeric(19,0) | - | 否 |  |
| BORROWING_STATUS | numeric(19,0) | - | 是 |  |
| RETURNED_DOCTOR_ID | numeric(19,0) | - | 是 |  |
| RETURNED_DOCTOR_NO | varchar(32) | 32 | 是 |  |
| RETURNED_DOCTOR_NAME | nvarchar(128) | 128 | 是 |  |
| RETURNED_AT | datetime | - | 是 |  |
| MODIFIED_NAME | nvarchar(128) | 128 | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(128) | 128 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |

---

## PIS_CASE_WATCH

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| PIS_CASE_WATCH_ID | numeric(19,0) | - | 否 |  |
| PIS_EXAM_TASK_ID | numeric(19,0) | - | 否 |  |
| PIS_WATCH_FOLDER_ID | numeric(19,0) | - | 是 |  |
| WATCH_DOCTOR_ID | numeric(19,0) | - | 是 |  |
| WATCH_DOCTOR_NO | varchar(32) | 32 | 是 |  |
| WATCH_DOCTOR_NAME | nvarchar(128) | 128 | 是 |  |
| WATCHED_AT | datetime | - | 是 |  |
| PRIVATE_FLAG | numeric(19,0) | - | 是 |  |
| WATCH_TITLE_NAME | nvarchar(64) | 64 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(128) | 128 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(128) | 128 | 是 |  |

---

## PIS_COMMON_QC_RESULT

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| PIS_COMMON_QC_RESULT_ID | numeric(19,0) | - | 否 |  |
| QC_YEAR | varchar(4) | 4 | 是 |  |
| QC_MONTH | varchar(2) | 2 | 是 |  |
| QC_YEAR_MONTH | varchar(8) | 8 | 是 |  |
| QC_TYPE_NO | varchar(2) | 2 | 是 |  |
| NUMERATOR | numeric(19,0) | - | 是 |  |
| DENOMINATOR | numeric(19,0) | - | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| CREATED_NAME | nvarchar(128) | 128 | 是 |  |
| MODIFIED_NAME | nvarchar(128) | 128 | 是 |  |

---

## PIS_DEHYDRATE_BASKET

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| PIS_DEHYDRATE_BASKET_ID | numeric(19,0) | - | 否 |  |
| BASKET_NO | varchar(32) | 32 | 是 |  |
| BASKET_CAPACITY | smallint(5,0) | - | 是 |  |
| BASKET_USAGE | smallint(5,0) | - | 是 |  |
| REMARK | nvarchar(256) | 256 | 是 |  |
| IDLE_FLAG | numeric(19,0) | - | 否 |  |
| MODIFIED_NAME | nvarchar(128) | 128 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(128) | 128 | 是 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_AT | datetime | - | 否 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |

---

## PIS_DEHYDRATOR

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| PIS_DEHYDRATOR_ID | numeric(19,0) | - | 否 |  |
| DEHYDRATOR_NO | varchar(64) | 64 | 是 |  |
| DEHYDRATOR_NAME | nvarchar(128) | 128 | 是 |  |
| ENABLED_FLAG | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(128) | 128 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(128) | 128 | 是 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_AT | datetime | - | 否 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |

---

## PIS_DELAY_RECORD

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| PIS_DELAY_RECORD_ID | numeric(19,0) | - | 否 |  |
| PIS_EXAM_TASK_ID | numeric(19,0) | - | 否 |  |
| DELAY_DOCTOR_ID | numeric(19,0) | - | 是 |  |
| DELAY_DOCTOR_NO | varchar(32) | 32 | 是 |  |
| DELAY_DOCTOR_NAME | nvarchar(128) | 128 | 是 |  |
| DELAYED_TO_AT | datetime | - | 是 |  |
| DELAY_REASON | nvarchar(256) | 256 | 是 |  |
| PRE_DIAG_CONTENT | nvarchar(-1) | -1 | 是 |  |
| PUBLISH_DOCTOR_ID | numeric(19,0) | - | 是 |  |
| PUBLISH_DOCTOR_NO | varchar(32) | 32 | 是 |  |
| PUBLISH_DOCTOR_NAME | nvarchar(128) | 128 | 是 |  |
| PUBLISHED_AT | datetime | - | 是 |  |
| MODIFIED_NAME | nvarchar(128) | 128 | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(128) | 128 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |

---

## PIS_DIAG_TEMPLATE_CONTENT

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| PIS_DIAG_TEMPLATE_CONTENT_ID | numeric(19,0) | - | 否 | ✓ |
| PIS_DIAG_TEMPLATE_TREE_ID | numeric(19,0) | - | 否 |  |
| TEMPLATE_ITEM_NO | varchar(32) | 32 | 否 |  |
| TEMPLATE_CONTENT | nvarchar(-1) | -1 | 是 |  |
| ENCIPHER_FLAG | numeric(19,0) | - | 否 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(128) | 128 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(128) | 128 | 是 |  |

---

## PIS_DIAG_TEMPLATE_TREE

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| PIS_DIAG_TEMPLATE_TREE_ID | numeric(19,0) | - | 否 |  |
| TEMPLATE_NODE_NO | varchar(32) | 32 | 否 |  |
| TEMPLATE_NODE_NAME | nvarchar(64) | 64 | 否 |  |
| PARENT_TEMPLATE_TREE_ID | numeric(19,0) | - | 否 |  |
| MEM1_NO | varchar(64) | 64 | 是 |  |
| MEM2_NO | varchar(64) | 64 | 是 |  |
| NODE_PROPERTY | varchar(16) | 16 | 否 |  |
| PRIVATE_USER_ID | numeric(19,0) | - | 是 |  |
| PRIVATE_USER_NAME | nvarchar(32) | 32 | 是 |  |
| LEAF_NODE_FLAG | numeric(19,0) | - | 否 |  |
| DEFAULT_FLAG | numeric(19,0) | - | 否 |  |
| SEQ_NO | smallint(5,0) | - | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(128) | 128 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(128) | 128 | 是 |  |
| APPLY_TYPE_NO | nvarchar(16) | 16 | 是 |  |
| PIS_EXAM_CATEGORY_IDS | nvarchar(100) | 100 | 是 |  |

---

## PIS_DICTIONARY

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| PIS_DICTIONARY_ID | numeric(19,0) | - | 否 |  |
| DICT_NAME | nvarchar(64) | 64 | 否 |  |
| ENABLED_FLAG | numeric(19,0) | - | 否 |  |
| SEQ_NO | int(10,0) | - | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(128) | 128 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(128) | 128 | 是 |  |

---

## PIS_DICTIONARY_ITEM

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| PIS_DICTIONARY_ITEM_ID | numeric(19,0) | - | 否 |  |
| PIS_DICTIONARY_ID | numeric(19,0) | - | 否 |  |
| DICT_NAME | nvarchar(64) | 64 | 否 |  |
| DICT_ITEM_NO | varchar(256) | 256 | 否 |  |
| DICT_ITEM_NAME | nvarchar(128) | 128 | 否 |  |
| DICT_ITEM_EXTERN_NO | varchar(32) | 32 | 是 |  |
| MEM1_NO | varchar(32) | 32 | 是 |  |
| MEM2_NO | varchar(32) | 32 | 是 |  |
| ALTER_FLAG | numeric(19,0) | - | 否 |  |
| VISIBLE_FLAG | numeric(19,0) | - | 否 |  |
| SEQ_NO | int(10,0) | - | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(128) | 128 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(128) | 128 | 是 |  |

---

## PIS_EMBEDDING_BOX

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| PIS_EMBEDDING_BOX_ID | numeric(19,0) | - | 否 |  |
| PIS_EXAM_TASK_ID | numeric(19,0) | - | 否 |  |
| EMBEDDING_BOX_NO | varchar(16) | 16 | 是 |  |
| PRINT_TIMES | smallint(5,0) | - | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| CREATED_NAME | nvarchar(128) | 128 | 是 |  |
| MODIFIED_NAME | nvarchar(128) | 128 | 是 |  |
| TIME_STAMP | timestamp | - | 否 |  |

---

## PIS_EXAM_ATTACHMENT

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| PIS_EXAM_ATTACHMENT_ID | numeric(19,0) | - | 否 |  |
| PIS_EXAM_TASK_ID | numeric(19,0) | - | 否 |  |
| FILE_SOURCE_CODE | numeric(19,0) | - | 是 |  |
| FILE_NAME | varchar(128) | 128 | 是 |  |
| FILE_SIZE | int(10,0) | - | 是 |  |
| FILE_TYPE_NO | nvarchar(32) | 32 | 是 |  |
| FILE_DESC | nvarchar(128) | 128 | 是 |  |
| PC_MAC_ADDRESS | nvarchar(128) | 128 | 是 |  |
| UPLOAD_PC_NAME | nvarchar(64) | 64 | 是 |  |
| FILE_PACS_IMAGE_UID | nvarchar(128) | 128 | 是 |  |
| FILE_PATH | nvarchar(256) | 256 | 是 |  |
| FILE_CREATED_AT | datetime | - | 是 |  |
| UPLOAD_PACS_FLAG | numeric(19,0) | - | 是 |  |
| UPLOAD_PACS_ERROR_MSG | nvarchar(-1) | -1 | 是 |  |
| MEMO | nvarchar(256) | 256 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(128) | 128 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(128) | 128 | 是 |  |
| FILE_PACS_STUDY_UID | nvarchar(128) | 128 | 是 |  |

---

## PIS_EXAM_CATEG_PRINT_FILE

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| PIS_EXAM_CATEG_PRINT_FILE_ID | numeric(19,0) | - | 否 |  |
| PIS_EXAM_CATEGORY_ID | numeric(19,0) | - | 否 |  |
| PIS_PRINT_FILE_ID | numeric(19,0) | - | 否 |  |
| SEQ_NO | int(10,0) | - | 是 |  |
| SEND_HOSPITAL_SOID | numeric(19,0) | - | 是 |  |
| SEND_HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |
| DEFAULT_FLAG | numeric(19,0) | - | 是 |  |
| VISIBLE_FLAG | numeric(19,0) | - | 是 |  |

---

## PIS_EXAM_CATEGORY

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| PIS_EXAM_CATEGORY_ID | numeric(19,0) | - | 否 |  |
| PIS_FLOW_ID | numeric(19,0) | - | 否 |  |
| EXAM_CATEGORY_NO | varchar(32) | 32 | 否 |  |
| EXAM_CATEGORY_NAME | nvarchar(64) | 64 | 否 |  |
| EXAM_CATEGORY_ALIAS | nvarchar(16) | 16 | 是 |  |
| EXEC_DEPT_ID | numeric(19,0) | - | 是 |  |
| EXEC_DEPT_NO | varchar(32) | 32 | 是 |  |
| DEFAULT_VALUE | nvarchar(-1) | -1 | 是 |  |
| PATH_NO_RULE_ID | numeric(19,0) | - | 是 |  |
| VISIBLE_FLAG | numeric(19,0) | - | 否 |  |
| SEQ_NO | int(10,0) | - | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |

---

## PIS_EXAM_CATEGORY_FEE

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| PIS_EXAM_CATEGORY_FEE_ID | numeric(19,0) | - | 否 | ✓ |
| PIS_EXAM_CATEGORY_ID | numeric(19,0) | - | 否 |  |
| EXAM_ITEM_NO | varchar(32) | 32 | 否 |  |
| EXAM_ITEM_NAME | nvarchar(128) | 128 | 否 |  |
| EXTERN_ITEM_TYPE_NO | varchar(32) | 32 | 是 |  |
| EXAM_ITEM_PRICE | numeric(16,6) | - | 是 |  |
| CONTAIN_SPECIAL_EXAM_FLAG | numeric(19,0) | - | 是 |  |
| SOURCE_HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| SOURCE_HOSPITAL_NAME | varchar(128) | 128 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |

---

## PIS_EXAM_CHARGE_ITEM

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| PIS_EXAM_CHARGE_ITEM_ID | numeric(19,0) | - | 否 |  |
| PIS_EXAM_REQUISITION_ID | numeric(19,0) | - | 是 |  |
| PIS_EXAM_TASK_ID | numeric(19,0) | - | 否 |  |
| EXAM_REQUISITION_NO | varchar(32) | 32 | 是 |  |
| EXAM_ORDER_ITEM_NO | varchar(32) | 32 | 是 |  |
| EXAM_ITEM_NO | varchar(32) | 32 | 是 |  |
| EXAM_ITEM_NAME | nvarchar(128) | 128 | 是 |  |
| EXAM_ITEM_GROUP_NO | varchar(32) | 32 | 是 |  |
| EXTERN_ITEM_TYPE_NO | varchar(32) | 32 | 是 |  |
| EXAM_ITEM_PRICE | numeric(16,6) | - | 是 |  |
| EXAM_ITEM_QTY | smallint(5,0) | - | 是 |  |
| EXAM_ITEM_UNIT | nvarchar(64) | 64 | 是 |  |
| ITEM_SOURCE_CODE | numeric(19,0) | - | 是 |  |
| CONFIRMED_NO | nvarchar(32) | 32 | 是 |  |
| ITEM_CHARGE_STATUS | numeric(19,0) | - | 是 |  |
| APPLY_AT | datetime | - | 是 |  |
| APPLY_DEPT_ID | numeric(19,0) | - | 是 |  |
| APPLY_DEPT_NO | varchar(32) | 32 | 是 |  |
| APPLY_DEPT_NAME | nvarchar(128) | 128 | 是 |  |
| APPLY_DOCTOR_ID | numeric(19,0) | - | 是 |  |
| APPLY_DOCTOR_NO | varchar(32) | 32 | 是 |  |
| APPLY_DOCTOR_NAME | nvarchar(128) | 128 | 是 |  |
| RECEIVE_AT | datetime | - | 是 |  |
| REGISTERED_NO | varchar(128) | 128 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(128) | 128 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(128) | 128 | 是 |  |
| ENCOUNTER_TYPE_CODE | numeric(19,0) | - | 是 |  |
| ENCOUNTER_NO | varchar(32) | 32 | 是 |  |
| PATIENT_NO | varchar(32) | 32 | 是 |  |
| EXEC_DEPT_ID | numeric(19,0) | - | 是 |  |
| EXEC_DEPT_NO | varchar(32) | 32 | 是 |  |
| EXEC_DEPT_NAME | nvarchar(128) | 128 | 是 |  |
| EXEC_DOCTOR_ID | numeric(19,0) | - | 是 |  |
| EXEC_DOCTOR_NO | varchar(32) | 32 | 是 |  |
| EXEC_DOCTOR_NAME | nvarchar(128) | 128 | 是 |  |
| EXEC_AT | datetime | - | 是 |  |
| CLINIC_DIAG_DESC | nvarchar(1024) | 1024 | 是 |  |
| ADD_PAYMENT_DOCTOR_ID | numeric(19,0) | - | 是 |  |
| ADD_PAYMENT_DOCTOR_NO | nvarchar(32) | 32 | 是 |  |
| ADD_PAYMENT_DOCTOR_NAME | nvarchar(128) | 128 | 是 |  |
| ADD_PAYMENT_AT | datetime | - | 是 |  |
| ADD_PAYMENT_FLAG | nvarchar(32) | 32 | 是 |  |
| ADD_PAYMENT_STATUS | nvarchar(32) | 32 | 是 |  |

---

## PIS_EXAM_CRITICAL_VALUES

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| PIS_EXAM_CRITICAL_VALUES_ID | numeric(19,0) | - | 否 | ✓ |
| PIS_EXAM_TASK_ID | numeric(19,0) | - | 否 |  |
| PIS_EXAM_REPORT_ID | numeric(19,0) | - | 否 |  |
| SYSTEM_SOURCE_NO | nvarchar(32) | 32 | 是 |  |
| CALLER_ID | numeric(19,0) | - | 是 |  |
| CALLER_NAME | nvarchar(32) | 32 | 是 |  |
| CALLED_AT | datetime | - | 是 |  |
| PATIENT_TEL | nvarchar(32) | 32 | 是 |  |
| INFORMED_WARD_ID | numeric(19,0) | - | 是 |  |
| INFORMED_WARD_NO | nvarchar(16) | 16 | 是 |  |
| INFORMED_WARD_NAME | nvarchar(32) | 32 | 是 |  |
| INFORMED_WARD_TEL | nvarchar(32) | 32 | 是 |  |
| INFORMED_DEPT_ID | numeric(19,0) | - | 是 |  |
| INFORMED_DEPT_NO | nvarchar(16) | 16 | 是 |  |
| INFORMED_DEPT_NAME | nvarchar(32) | 32 | 是 |  |
| INFORMED_DEPT_TEL | nvarchar(32) | 32 | 是 |  |
| INFORMED_DOC_ID | numeric(19,0) | - | 是 |  |
| INFORMED_DOC_NO | nvarchar(16) | 16 | 是 |  |
| INFORMED_DOC_NAME | nvarchar(32) | 32 | 是 |  |
| INFORMED_DOC_TEL | nvarchar(32) | 32 | 是 |  |
| CRITICAL_VALUES_NO | varchar(128) | 128 | 是 |  |
| CRITICAL_VALUES_DESC | nvarchar(256) | 256 | 是 |  |
| CRITICAL_VALUES_STATUS | numeric(19,0) | - | 是 |  |
| COMFORMER_ID | numeric(19,0) | - | 是 |  |
| COMFORMER_NO | nvarchar(16) | 16 | 是 |  |
| COMFORMER_NAME | nvarchar(32) | 32 | 是 |  |
| COMFORMED_AT | datetime | - | 是 |  |
| COMFORMED_DESC | nvarchar(256) | 256 | 是 |  |
| NOTIFIED_PROCESSOR_ID | numeric(19,0) | - | 是 |  |
| NOTIFIED_PROCESSOR_NO | nvarchar(16) | 16 | 是 |  |
| NOTIFIED_PROCESSOR_NAME | nvarchar(32) | 32 | 是 |  |
| NOTIFIED_PROCEDURE_AT | datetime | - | 是 |  |
| NOTIFIED_PROCEDURE_DESC | nvarchar(256) | 256 | 是 |  |
| PROCESSOR_ID | numeric(19,0) | - | 是 |  |
| PROCESSOR_NO | nvarchar(16) | 16 | 是 |  |
| PROCESSOR_NAME | nvarchar(32) | 32 | 是 |  |
| PROCESSED_AT | datetime | - | 是 |  |
| PROCEDURE_DESC | nvarchar(256) | 256 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |

---

## PIS_EXAM_ELEMENT_ITEM

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| PIS_EXAM_ELEMENT_ITEM_ID | numeric(19,0) | - | 否 |  |
| ELEMENT_ITEM_GROUP_NAME | nvarchar(128) | 128 | 是 |  |
| BIND_MODEL_KEY | nvarchar(64) | 64 | 是 |  |
| BIND_MODEL_VALUE | nvarchar(64) | 64 | 是 |  |
| BIND_MODEL_NAME | nvarchar(256) | 256 | 是 |  |
| ELEMENT_ITEM_TYPE_NO | nvarchar(64) | 64 | 是 |  |
| ELEMENT_ITEM_DEFAULT_VALUE | nvarchar(64) | 64 | 是 |  |
| ELEMENT_ITEM_DATA_PROPERTY | nvarchar(32) | 32 | 是 |  |
| MODEL_SOURCE | nvarchar(64) | 64 | 是 |  |
| DATA_SOURCE_TYPE_NO | nvarchar(64) | 64 | 是 |  |
| DATA_SOURCE_FUNCTION | varchar(-1) | -1 | 是 |  |
| ELEMENT_ITEM_NAME | nvarchar(64) | 64 | 是 |  |
| ELEMENT_ITEM_PLACEHOLDER | nvarchar(128) | 128 | 是 |  |
| ELEMENT_ITEM_RELATION_SET | nvarchar(256) | 256 | 是 |  |
| ELEMENT_ITEM_RELATION_DEFAULT | nvarchar(32) | 32 | 是 |  |
| ELEMENT_ITEM_REGEX_TEXT | nvarchar(1024) | 1024 | 是 |  |
| TRIGGER_EVENT_TYPE_NO | nvarchar(64) | 64 | 是 |  |
| TRIGGER_EVENT_FUNCTION | varchar(4000) | 4000 | 是 |  |
| ELEMENT_ITEM_CSS | varchar(-1) | -1 | 是 |  |
| ELEMENT_ITEM_BOUNDARY | nvarchar(128) | 128 | 是 |  |
| IS_FILTER | numeric(19,0) | - | 是 |  |
| IS_DICTIONARY | numeric(19,0) | - | 是 |  |
| IS_INTERFACE | numeric(19,0) | - | 是 |  |
| IS_READ_ONLY | numeric(19,0) | - | 是 |  |
| REQUIRED_FLAG | numeric(19,0) | - | 是 |  |
| SEQ_NO | smallint(5,0) | - | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |
| TIME_STAMP | timestamp | - | 否 |  |

---

## PIS_EXAM_REJECT_TASK

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| PIS_EXAM_REJECT_TASK_ID | numeric(19,0) | - | 否 |  |
| PIS_EXAM_REQUISITION_ID | numeric(19,0) | - | 否 |  |
| PIS_REGISTRATION_ID | numeric(19,0) | - | 否 |  |
| PIS_EXAM_CATEGORY_ID | numeric(19,0) | - | 是 |  |
| REMARK | nvarchar(512) | 512 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |

---

## PIS_EXAM_REPORT

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| PIS_EXAM_REPORT_ID | numeric(19,0) | - | 否 | ✓ |
| PIS_EXAM_TASK_ID | numeric(19,0) | - | 否 |  |
| ADD_REPORT_FLAG | numeric(19,0) | - | 否 |  |
| ADD_REPORT_SEQ_NO | int(10,0) | - | 是 |  |
| FIRST_DIAG_DOCTOR_ID | numeric(19,0) | - | 是 |  |
| FIRST_DIAG_DOCTOR_NO | varchar(32) | 32 | 是 |  |
| FIRST_DIAG_DOCTOR_NAME | nvarchar(128) | 128 | 是 |  |
| FIRST_DIAG_AT | datetime | - | 是 |  |
| SECOND_DIAG_DOCTOR_ID | numeric(19,0) | - | 是 |  |
| SECOND_DIAG_DOCTOR_NO | varchar(32) | 32 | 是 |  |
| SECOND_DIAG_DOCTOR_NAME | nvarchar(128) | 128 | 是 |  |
| SECOND_DIAG_AT | datetime | - | 是 |  |
| REVIEW_DOCTOR_ID | numeric(19,0) | - | 是 |  |
| REVIEW_DOCTOR_NO | varchar(32) | 32 | 是 |  |
| REVIEW_DOCTOR_NAME | nvarchar(128) | 128 | 是 |  |
| REVIEW_AT | datetime | - | 是 |  |
| PUBLISH_DOCTOR_ID | numeric(19,0) | - | 是 |  |
| PUBLISH_DOCTOR_NO | varchar(32) | 32 | 是 |  |
| PUBLISH_DOCTOR_NAME | nvarchar(128) | 128 | 是 |  |
| PUBLISH_AT | datetime | - | 是 |  |
| WITHDRAW_PUBLISH_DOCTOR_ID | numeric(19,0) | - | 是 |  |
| WITHDRAW_PUBLISH_DOCTOR_NO | varchar(32) | 32 | 是 |  |
| WITHDRAW_PUBLISH_DOCTOR_NAME | nvarchar(128) | 128 | 是 |  |
| WITHDRAW_PUBLISHED_AT | datetime | - | 是 |  |
| WITHDRAW_PUBLISH_REASON | nvarchar(512) | 512 | 是 |  |
| REPORT_STATUS | numeric(19,0) | - | 是 |  |
| REPORT_STATUS_NO | varchar(32) | 32 | 是 |  |
| GENERAL_FINDINGS | nvarchar(-1) | -1 | 是 |  |
| MICROSCOPE_FINDINGS | nvarchar(-1) | -1 | 是 |  |
| DIAG_CONTENT | nvarchar(-1) | -1 | 是 |  |
| ADD_COMMENTS | nvarchar(1024) | 1024 | 是 |  |
| NEG_POS_CODE | numeric(19,0) | - | 是 |  |
| PRINT_TIMES | smallint(5,0) | - | 否 |  |
| PRINTED_AT | datetime | - | 是 |  |
| PRINT_DOCTOR_ID | numeric(19,0) | - | 是 |  |
| PRINT_DOCTOR_NO | varchar(32) | 32 | 是 |  |
| PRINT_DOCTOR_NAME | nvarchar(128) | 128 | 是 |  |
| PIS_PRINT_FILE_ID | numeric(19,0) | - | 是 |  |
| REPORT_PDF_NO | varchar(128) | 128 | 是 |  |
| REPORT_PDF_SRC_EXT_FLAG | numeric(19,0) | - | 是 |  |
| LOCK_FLAG | numeric(19,0) | - | 是 |  |
| LOCKED_BY | numeric(19,0) | - | 是 |  |
| LOCKED_NAME | nvarchar(128) | 128 | 是 |  |
| LOCKED_PC | nvarchar(128) | 128 | 是 |  |
| LOCKED_AT | datetime | - | 是 |  |
| OVERDUE_REMIND_FLAG | numeric(19,0) | - | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(128) | 128 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(128) | 128 | 是 |  |
| REPORT_QC_DOCTOR_ID | numeric(19,0) | - | 是 |  |
| REPORT_QC_DOCTOR_NO | varchar(32) | 32 | 是 |  |
| REPORT_QC_DOCTOR_NAME | nvarchar(128) | 128 | 是 |  |
| REVISE_STATUS_NO | varchar(2) | 2 | 是 |  |
| EXT_REPORT_NO | varchar(64) | 64 | 是 |  |
| FROZEN_REPORT_IDS | nvarchar(256) | 256 | 是 |  |
| FROZEN_REPORT_ID_FLAG | numeric(19,0) | - | 是 |  |

---

## PIS_EXAM_REPORT_LOG

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| PIS_EXAM_REPORT_LOG_ID | numeric(19,0) | - | 否 | ✓ |
| PIS_EXAM_REPORT_ID | numeric(19,0) | - | 否 |  |
| PIS_EXAM_TASK_ID | numeric(19,0) | - | 否 |  |
| ADD_REPORT_FLAG | numeric(18,0) | - | 否 |  |
| ADD_REPORT_SEQ_NO | int(10,0) | - | 是 |  |
| FIRST_DIAG_DOCTOR_ID | numeric(19,0) | - | 是 |  |
| FIRST_DIAG_DOCTOR_NO | varchar(32) | 32 | 是 |  |
| FIRST_DIAG_DOCTOR_NAME | nvarchar(128) | 128 | 是 |  |
| FIRST_DIAG_AT | datetime | - | 是 |  |
| SECOND_DIAG_DOCTOR_ID | numeric(19,0) | - | 是 |  |
| SECOND_DIAG_DOCTOR_NO | varchar(32) | 32 | 是 |  |
| SECOND_DIAG_DOCTOR_NAME | nvarchar(128) | 128 | 是 |  |
| SECOND_DIAG_AT | datetime | - | 是 |  |
| REVIEW_DOCTOR_ID | numeric(19,0) | - | 是 |  |
| REVIEW_DOCTOR_NO | varchar(32) | 32 | 是 |  |
| REVIEW_DOCTOR_NAME | nvarchar(128) | 128 | 是 |  |
| REVIEW_AT | datetime | - | 是 |  |
| PUBLISH_DOCTOR_ID | numeric(19,0) | - | 是 |  |
| PUBLISH_DOCTOR_NO | varchar(32) | 32 | 是 |  |
| PUBLISH_DOCTOR_NAME | nvarchar(128) | 128 | 是 |  |
| PUBLISH_AT | datetime | - | 是 |  |
| REPORT_STATUS | numeric(19,0) | - | 是 |  |
| REPORT_STATUS_NO | varchar(32) | 32 | 是 |  |
| GENERAL_FINDINGS | nvarchar(-1) | -1 | 是 |  |
| MICROSCOPE_FINDINGS | nvarchar(-1) | -1 | 是 |  |
| DIAG_CONTENT | nvarchar(-1) | -1 | 是 |  |
| ADD_COMMENTS | nvarchar(1024) | 1024 | 是 |  |
| NEG_POS_CODE | numeric(19,0) | - | 是 |  |
| PRINT_TIMES | smallint(5,0) | - | 否 |  |
| PRINTED_AT | datetime | - | 是 |  |
| PRINT_DOCTOR_ID | numeric(19,0) | - | 是 |  |
| PRINT_DOCTOR_NO | varchar(32) | 32 | 是 |  |
| PRINT_DOCTOR_NAME | nvarchar(128) | 128 | 是 |  |
| PIS_PRINT_FILE_ID | numeric(19,0) | - | 是 |  |
| REPORT_PDF_NO | varchar(128) | 128 | 是 |  |
| REPORT_PDF_SRC_EXT_FLAG | numeric(19,0) | - | 是 |  |
| LOCK_FLAG | numeric(19,0) | - | 是 |  |
| LOCKED_BY | numeric(19,0) | - | 是 |  |
| LOCKED_NAME | nvarchar(128) | 128 | 是 |  |
| LOCKED_PC | varchar(128) | 128 | 是 |  |
| LOCK_PC | varchar(128) | 128 | 是 |  |
| LOCKED_AT | datetime | - | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(128) | 128 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(128) | 128 | 是 |  |
| IP_ADDRESS | varchar(64) | 64 | 是 |  |
| WITHDRAW_PUBLISH_DOCTOR_ID | numeric(19,0) | - | 是 |  |
| WITHDRAW_PUBLISH_DOCTOR_NO | varchar(32) | 32 | 是 |  |
| WITHDRAW_PUBLISH_DOCTOR_NAME | nvarchar(128) | 128 | 是 |  |
| WITHDRAW_PUBLISHED_AT | datetime | - | 是 |  |
| WITHDRAW_PUBLISH_REASON | nvarchar(512) | 512 | 是 |  |

---

## PIS_EXAM_REQUISITION

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| PIS_EXAM_REQUISITION_ID | numeric(19,0) | - | 否 | ✓ |
| EXAM_REQUISITION_NO | varchar(32) | 32 | 是 |  |
| EXAM_REQUISITION_CONTENT | varchar(-1) | -1 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(128) | 128 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(128) | 128 | 是 |  |

---

## PIS_EXAM_REQUISITION_TASK

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| PIS_EXAM_REQUISITION_TASK_ID | numeric(19,0) | - | 否 |  |
| PIS_EXAM_REQUISITION_ID | numeric(19,0) | - | 否 |  |
| PIS_EXAM_TASK_ID | numeric(19,0) | - | 否 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 是 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(128) | 128 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(128) | 128 | 是 |  |

---

## PIS_EXAM_RESULT

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| PIS_EXAM_RESULT_ID | numeric(19,0) | - | 否 |  |
| PIS_EXAM_REPORT_ID | numeric(19,0) | - | 否 |  |
| REPORT_ITEM_NO | nvarchar(64) | 64 | 是 |  |
| REPORT_ITEM_NAME | nvarchar(64) | 64 | 是 |  |
| REPORT_ITEM_RESULT | nvarchar(-1) | -1 | 是 |  |
| REPORT_ITEM_REF_VALUE | nvarchar(128) | 128 | 是 |  |
| REPORT_ITEM_STATUS | numeric(19,0) | - | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |

---

## PIS_EXAM_RESULT_LOG

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| PIS_EXAM_RESULT_LOG_ID | numeric(19,0) | - | 否 | ✓ |
| PIS_EXAM_REPORT_LOG_ID | numeric(19,0) | - | 否 |  |
| PIS_EXAM_RESULT_ID | numeric(19,0) | - | 否 |  |
| PIS_EXAM_REPORT_ID | numeric(19,0) | - | 否 |  |
| REPORT_ITEM_NO | varchar(64) | 64 | 是 |  |
| REPORT_ITEM_NAME | nvarchar(64) | 64 | 是 |  |
| REPORT_ITEM_RESULT | nvarchar(-1) | -1 | 是 |  |
| REPORT_ITEM_REF_VALUE | nvarchar(128) | 128 | 是 |  |
| REPORT_ITEM_STATUS | numeric(19,0) | - | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |
| IP_ADDRESS | varchar(64) | 64 | 是 |  |

---

## PIS_EXAM_SELECT_ATTACHMENT

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| PIS_EXAM_SELECT_ATTACHMENT_ID | numeric(19,0) | - | 否 | ✓ |
| PIS_EXAM_ATTACHMENT_ID | numeric(19,0) | - | 否 |  |
| PIS_EXAM_REPORT_ID | numeric(19,0) | - | 否 |  |
| SEQ_NO | int(10,0) | - | 是 |  |
| REMARK | nvarchar(256) | 256 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(128) | 128 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(128) | 128 | 是 |  |

---

## PIS_EXAM_SETTINGS

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| PIS_EXAM_SETTINGS_ID | numeric(19,0) | - | 否 | ✓ |
| SETTING_ITEM_KEY | varchar(64) | 64 | 否 |  |
| SETTING_ITEM_DOMAIN_NO | varchar(32) | 32 | 否 |  |
| SETTING_ITEM_DOMAIN_NAME | nvarchar(64) | 64 | 否 |  |
| SETTING_ITEM_SECTION | nvarchar(64) | 64 | 否 |  |
| SETTING_ITEM_ENTRY | nvarchar(256) | 256 | 否 |  |
| SETTING_ITEM_DATA_TYPE | varchar(16) | 16 | 否 |  |
| SETTING_ITEM_VALUE_SET | nvarchar(256) | 256 | 是 |  |
| DEFAULT_VALUE | nvarchar(-1) | -1 | 是 |  |
| PRIVATE_BY | numeric(19,0) | - | 是 |  |
| VISIBLE_FLAG | numeric(19,0) | - | 是 |  |
| REMARK | nvarchar(256) | 256 | 是 |  |
| SEQ_NO | int(10,0) | - | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(128) | 128 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(128) | 128 | 是 |  |

---

## PIS_EXAM_STATISTICAL_REPORT

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| PIS_EXAM_STATISTICAL_REPORT_ID | numeric(19,0) | - | 否 | ✓ |
| STAT_REPORT_NO | varchar(128) | 128 | 是 |  |
| STAT_REPORT_NAME | nvarchar(256) | 256 | 是 |  |
| STAT_REPORT_PATH | nvarchar(1024) | 1024 | 是 |  |
| STAT_REPORT_SYSTEM_FLAG | numeric(19,0) | - | 是 |  |
| PARENT_STATISTICAL_REPORT_ID | numeric(19,0) | - | 是 |  |
| SEQ_NO | numeric(19,0) | - | 是 |  |
| STAT_NODE_LEAF_FLAG | numeric(19,0) | - | 是 |  |
| STAT_NODE_PROPERTY | varchar(64) | 64 | 是 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(128) | 128 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(128) | 128 | 是 |  |

---

## PIS_EXAM_TASK

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| PIS_EXAM_TASK_ID | numeric(19,0) | - | 否 | ✓ |
| PIS_EXAM_REQUISITION_ID | numeric(19,0) | - | 否 |  |
| PIS_REGISTRATION_ID | numeric(19,0) | - | 否 |  |
| PATH_NO | varchar(16) | 16 | 否 |  |
| PIS_EXAM_CATEGORY_ID | numeric(19,0) | - | 是 |  |
| PRIORITY_FLAG | numeric(19,0) | - | 是 |  |
| SPECIMEN_USED_UP_FLAG | numeric(19,0) | - | 是 |  |
| SPECIMEN_LOCATION | nvarchar(128) | 128 | 是 |  |
| DRAWN_AT | datetime | - | 是 |  |
| DRAW_DOCTOR_ID | numeric(19,0) | - | 是 |  |
| DRAW_DOCTOR_NO | varchar(32) | 32 | 是 |  |
| DRAW_DOCTOR_NAME | nvarchar(128) | 128 | 是 |  |
| SLICED_AT | datetime | - | 是 |  |
| EXAM_STATUS | numeric(19,0) | - | 否 |  |
| EXAM_STATUS_NO | nvarchar(32) | 32 | 是 |  |
| ARREARAGE_FLAG | numeric(19,0) | - | 是 |  |
| EDITION_STATUS | numeric(19,0) | - | 是 |  |
| EDITED_BY | numeric(19,0) | - | 是 |  |
| EDITOR_NAME | nvarchar(128) | 128 | 是 |  |
| EDITOR_PC | nvarchar(64) | 64 | 是 |  |
| ITEM_CHARGE_STATUS | numeric(19,0) | - | 是 |  |
| REMARK | nvarchar(512) | 512 | 是 |  |
| DUNNING_FLAG | numeric(19,0) | - | 是 |  |
| ARREARAGE_REMIND_FLAG | numeric(19,0) | - | 是 |  |
| DUNNING_REMARK | nvarchar(256) | 256 | 是 |  |
| DUNNING_AT | datetime | - | 是 |  |
| TIMELY_REPORT_QC_STATUS | numeric(19,0) | - | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |
| CHARGE_REASON | nvarchar(64) | 64 | 是 |  |
| OVERDUE_AT | datetime | - | 是 |  |
| DUE_AT | datetime | - | 是 |  |
| SPECIMEN_SEND_STATUS_NO | varchar(16) | 16 | 是 |  |
| EXT_TASK_NO | varchar(64) | 64 | 是 |  |
| ADD_PAYMENT_FLAG | nvarchar(32) | 32 | 是 |  |

---

## PIS_EXAM_TASK_RELATE

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| PIS_EXAM_TASK_RELATE_ID | numeric(19,0) | - | 否 | ✓ |
| TARGET_PIS_EXAM_TASK_ID | numeric(19,0) | - | 否 |  |
| SOURCE_PIS_EXAM_TASK_ID | numeric(19,0) | - | 否 |  |
| MERGE_FLAG | numeric(19,0) | - | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(128) | 128 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(128) | 128 | 是 |  |

---

## PIS_FLOW

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| PIS_FLOW_ID | numeric(19,0) | - | 否 | ✓ |
| FLOW_NO | varchar(32) | 32 | 否 |  |
| FLOW_NAME | nvarchar(64) | 64 | 否 |  |
| FLOW_VALUE | nvarchar(-1) | -1 | 是 |  |
| ENABLED_FLAG | numeric(19,0) | - | 是 |  |
| SEQ_NO | int(10,0) | - | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(128) | 128 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(128) | 128 | 是 |  |

---

## PIS_FOLLOW_UP

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| PIS_FOLLOW_UP_ID | numeric(19,0) | - | 否 | ✓ |
| PIS_EXAM_TASK_ID | numeric(19,0) | - | 否 |  |
| FOLLOW_UP_REASON | nvarchar(256) | 256 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(128) | 128 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(128) | 128 | 是 |  |

---

## PIS_IN_HOSP_CONSULT

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| PIS_IN_HOSP_CONSULT_ID | numeric(19,0) | - | 否 | ✓ |
| PIS_EXAM_TASK_ID | numeric(19,0) | - | 否 |  |
| CONSULT_INITIATE_DOCTOR_ID | numeric(19,0) | - | 否 |  |
| CONSULT_INITIATE_DOCTOR_NO | varchar(32) | 32 | 是 |  |
| CONSULT_INITIATE_DOCTOR_NAME | nvarchar(128) | 128 | 是 |  |
| CONSULT_PRE_PTCP_DOCTOR_DATA | varchar(512) | 512 | 是 |  |
| PRE_CONSULTED_AT | datetime | - | 是 |  |
| CONSULT_STATUS | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(128) | 128 | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(128) | 128 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |

---

## PIS_MOLECULAR_DETECTION

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| PIS_MOLECULAR_DETECTION_ID | numeric(19,0) | - | 否 | ✓ |
| PIS_SPECIMEN_ID | numeric(19,0) | - | 否 |  |
| EXEC_DOCTOR_ID | numeric(19,0) | - | 是 |  |
| EXEC_DOCTOR_NO | varchar(32) | 32 | 是 |  |
| EXEC_DOCTOR_NAME | nvarchar(128) | 128 | 是 |  |
| EXECUTED_AT | datetime | - | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| CREATED_NAME | nvarchar(128) | 128 | 是 |  |
| MODIFIED_NAME | nvarchar(128) | 128 | 是 |  |

---

## PIS_PACKAGE

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| PIS_PACKAGE_ID | numeric(19,0) | - | 否 | ✓ |
| PACKAGE_NAME | nvarchar(128) | 128 | 否 |  |
| PACKAGE_CLASS_CODE | numeric(19,0) | - | 是 |  |
| SEQ_NO | int(10,0) | - | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(128) | 128 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(128) | 128 | 是 |  |
| OWNER_ID | numeric(19,0) | - | 是 |  |

---

## PIS_PRINT_FILE

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| PIS_PRINT_FILE_ID | numeric(19,0) | - | 否 | ✓ |
| PRINT_FILE_TYPE | numeric(19,0) | - | 否 |  |
| PRINT_FILE_NAME | nvarchar(64) | 64 | 否 |  |
| VISIBLE_FLAG | numeric(19,0) | - | 否 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |
| TEMPLATE_DESIGN_CONTENT | nvarchar(-1) | -1 | 是 |  |

---

## PIS_QUERY_CRITERIA

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| PIS_QUERY_CRITERIA_ID | numeric(19,0) | - | 否 | ✓ |
| FIELD_USE_CASE_NO | varchar(64) | 64 | 是 |  |
| FIELD_CN_NAME | nvarchar(64) | 64 | 是 |  |
| FIELD_EN_NAME | varchar(512) | 512 | 是 |  |
| DYNAMIC_SQL | varchar(4000) | 4000 | 是 |  |
| OPERATORS | nvarchar(128) | 128 | 是 |  |
| FIELD_VALUE_INPUT_WAY | varchar(32) | 32 | 是 |  |
| FIELD_OPTIONAL_VALUE | varchar(512) | 512 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(128) | 128 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(128) | 128 | 是 |  |

---

## PIS_QUERY_CRITERIA_GROUP

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| PIS_QUERY_CRITERIA_GROUP_ID | numeric(19,0) | - | 否 | ✓ |
| GROUP_NAME | nvarchar(64) | 64 | 是 |  |
| GROUP_CONTENT | varchar(-1) | -1 | 是 |  |
| SEQ_NO | int(10,0) | - | 是 |  |
| OWNER_ID | numeric(19,0) | - | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(128) | 128 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(128) | 128 | 是 |  |

---

## PIS_REAGENT

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| PIS_REAGENT_ID | numeric(19,0) | - | 否 | ✓ |
| REAGENT_NAME | nvarchar(64) | 64 | 否 |  |
| REAGENT_CLASS_CODE | numeric(19,0) | - | 否 |  |
| REAGENT_CLASS_NO | nvarchar(32) | 32 | 否 |  |
| ROCHE_REAGENT_NO | varchar(64) | 64 | 是 |  |
| LECIA_REAGENT_NO | varchar(64) | 64 | 是 |  |
| DAKO_REAGENT_NO | varchar(64) | 64 | 是 |  |
| TALENT_REAGENT_NO | varchar(64) | 64 | 是 |  |
| GAS_REAGENT_NO | varchar(64) | 64 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(128) | 128 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(128) | 128 | 是 |  |
| REAGENT_NO | varchar(64) | 64 | 是 |  |

---

## PIS_REAGENT_PACKAGE

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| PIS_REAGENT_PACKAGE_ID | numeric(19,0) | - | 否 | ✓ |
| PIS_PACKAGE_ID | numeric(19,0) | - | 否 |  |
| PIS_REAGENT_ID | numeric(19,0) | - | 否 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(128) | 128 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(128) | 128 | 是 |  |
| SEQ_NO | numeric(19,0) | - | 是 |  |

---

## PIS_REGISTRATION

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| PIS_REGISTRATION_ID | numeric(19,0) | - | 否 | ✓ |
| PATIENT_NO | varchar(32) | 32 | 是 |  |
| ENCOUNTER_NO | varchar(32) | 32 | 是 |  |
| ENC_REG_SEQ_NO | varchar(32) | 32 | 是 |  |
| ENCOUNTER_TYPE_NO | varchar(32) | 32 | 是 |  |
| ENCOUNTER_TYPE_CODE | numeric(19,0) | - | 是 |  |
| EXAM_REQUISITION_NO | varchar(32) | 32 | 是 |  |
| FULL_NAME | nvarchar(128) | 128 | 是 |  |
| PINYIN | varchar(128) | 128 | 是 |  |
| BIRTH_DATE | date | - | 是 |  |
| GENDER_CODE | numeric(19,0) | - | 是 |  |
| TELEPHONE_NO | varchar(32) | 32 | 是 |  |
| ADDRESS | nvarchar(128) | 128 | 是 |  |
| IDCARD_NO | varchar(18) | 18 | 是 |  |
| IDCARD_TYPE_CODE | numeric(19,0) | - | 是 |  |
| AGE | nvarchar(32) | 32 | 是 |  |
| AGE_YEARS | numeric(3,0) | - | 是 |  |
| AGE_MONTHS | numeric(3,0) | - | 是 |  |
| AGE_DAYS | numeric(3,0) | - | 是 |  |
| NATION_CODE | numeric(19,0) | - | 是 |  |
| NATION_NAME | nvarchar(64) | 64 | 是 |  |
| CARD_NO | nvarchar(32) | 32 | 是 |  |
| CARD_TYPE_CODE | numeric(19,0) | - | 是 |  |
| HOSP_NO | varchar(20) | 20 | 是 |  |
| INVOICE_NO | varchar(32) | 32 | 是 |  |
| WARD_ID | numeric(19,0) | - | 是 |  |
| WARD_NO | varchar(32) | 32 | 是 |  |
| WARD_NAME | nvarchar(128) | 128 | 是 |  |
| BED_NO | nvarchar(32) | 32 | 是 |  |
| SEND_HOSPITAL_SOID | numeric(19,0) | - | 是 |  |
| SEND_HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| APPLY_DEPT_ID | numeric(19,0) | - | 是 |  |
| APPLY_DEPT_NO | varchar(32) | 32 | 是 |  |
| APPLY_DEPT_NAME | nvarchar(128) | 128 | 是 |  |
| APPLY_AT | datetime | - | 是 |  |
| APPLY_DOCTOR_ID | numeric(19,0) | - | 是 |  |
| APPLY_DOCTOR_NO | varchar(32) | 32 | 是 |  |
| APPLY_DOCTOR_NAME | nvarchar(128) | 128 | 是 |  |
| SEND_DOCTOR_ID | numeric(19,0) | - | 是 |  |
| SEND_DOCTOR_NO | varchar(32) | 32 | 是 |  |
| SEND_DOCTOR_NAME | nvarchar(128) | 128 | 是 |  |
| SEND_DEPT_ID | numeric(19,0) | - | 是 |  |
| SEND_DEPT_NO | varchar(32) | 32 | 是 |  |
| SEND_DEPT_NAME | nvarchar(128) | 128 | 是 |  |
| SEND_AT | datetime | - | 是 |  |
| CLINIC_DIAG_DESC | nvarchar(1024) | 1024 | 是 |  |
| OPERATION_NO | varchar(32) | 32 | 是 |  |
| NATIVE_PLACE | varchar(128) | 128 | 是 |  |
| MARITAL_STATUS_CODE | numeric(19,0) | - | 是 |  |
| MARITAL_STATUS_NAME | nvarchar(64) | 64 | 是 |  |
| OCCP_CODE | numeric(19,0) | - | 是 |  |
| OCCP_NAME | nvarchar(64) | 64 | 是 |  |
| REGISTER_DOCTOR_ID | numeric(19,0) | - | 是 |  |
| REGISTER_DOCTOR_NO | varchar(32) | 32 | 是 |  |
| REGISTER_DOCTOR_NAME | nvarchar(128) | 128 | 是 |  |
| REGISTERED_AT | datetime | - | 是 |  |
| CHARGE_TYPE_NO | varchar(128) | 128 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(128) | 128 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(128) | 128 | 是 |  |

---

## PIS_REPORT_REMIND_RULE

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| PIS_REPORT_REMIND_RULE_ID | numeric(19,0) | - | 否 | ✓ |
| RULE_NAME | nvarchar(64) | 64 | 是 |  |
| REMIND_BUSINESS_TYPE | numeric(19,0) | - | 是 |  |
| PRECONDITION_FILED_DISP_NAME | nvarchar(64) | 64 | 是 |  |
| PRECONDITION_FILED_ID_NAME | varchar(128) | 128 | 是 |  |
| PRECONDITION_OPERATOR | varchar(8) | 8 | 是 |  |
| PRECONDITION_FILED_VALUE | nvarchar(64) | 64 | 是 |  |
| PRECONDITION_DYNAMIC_SQL | varchar(4000) | 4000 | 是 |  |
| TEST_BASIS_FILED_DISP_NAMES | nvarchar(256) | 256 | 是 |  |
| TEST_BASIS_FILED_ID_NAMES | varchar(512) | 512 | 是 |  |
| TEST_BASIS_OPERATOR | varchar(8) | 8 | 是 |  |
| TEST_BASIS_FILED_VALUE | varchar(64) | 64 | 是 |  |
| TEST_BASIS_DYNAMIC_SQL | varchar(4000) | 4000 | 是 |  |
| REMIND_METHOD_TYPE | numeric(19,0) | - | 是 |  |
| SEQ_NO | int(10,0) | - | 是 |  |
| ENABLED_FLAG | numeric(19,0) | - | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(128) | 128 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(128) | 128 | 是 |  |

---

## PIS_REPORT_UPLOAD_LOG

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| PIS_REPORT_UPLOAD_LOG_ID | numeric(19,0) | - | 否 | ✓ |
| PIS_EXAM_REPORT_ID | numeric(19,0) | - | 否 |  |
| UPLOAD_TYPE | numeric(19,0) | - | 否 |  |
| SUCCESS_FLAG | numeric(19,0) | - | 否 |  |
| FAIL_REASON | nvarchar(-1) | -1 | 是 |  |
| UPLOAD_TIMES | smallint(5,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| MODIFIED_NAME | nvarchar(128) | 128 | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| UPLOADED_AT | datetime | - | 是 |  |

---

## PIS_SECTION

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| PIS_SECTION_ID | numeric(19,0) | - | 否 | ✓ |
| PIS_EXAM_TASK_ID | numeric(19,0) | - | 否 |  |
| SECTION_OBJECT_ID | numeric(19,0) | - | 否 |  |
| SECTION_SOURCE_CODE | numeric(19,0) | - | 是 |  |
| SECTION_STATUS | numeric(19,0) | - | 是 |  |
| SECTION_STATUS_NO | nvarchar(32) | 32 | 是 |  |
| SECTION_DOCTOR_ID | numeric(19,0) | - | 是 |  |
| SECTION_DOCTOR_NO | varchar(32) | 32 | 是 |  |
| SECTION_DOCTOR_NAME | nvarchar(128) | 128 | 是 |  |
| SECTION_DOCTOR_IDS | varchar(256) | 256 | 是 |  |
| SECTION_DOCTOR_NOS | varchar(256) | 256 | 是 |  |
| SECTION_DOCTOR_NAMES | nvarchar(256) | 256 | 是 |  |
| SLICED_AT | datetime | - | 是 |  |
| PIS_SLIDE_ID | numeric(19,0) | - | 是 |  |
| SEQ_NO | smallint(5,0) | - | 是 |  |
| STAIN_DOCTOR_ID | numeric(19,0) | - | 是 |  |
| STAIN_DOCTOR_NO | varchar(32) | 32 | 是 |  |
| STAIN_DOCTOR_NAME | nvarchar(128) | 128 | 是 |  |
| STAIN_DOCTOR_IDS | varchar(256) | 256 | 是 |  |
| STAIN_DOCTOR_NOS | varchar(256) | 256 | 是 |  |
| STAIN_DOCTOR_NAMES | nvarchar(256) | 256 | 是 |  |
| STAINED_AT | datetime | - | 是 |  |
| SEAL_DOCTOR_ID | numeric(19,0) | - | 是 |  |
| SEAL_DOCTOR_NO | varchar(32) | 32 | 是 |  |
| SEAL_DOCTOR_NAME | nvarchar(128) | 128 | 是 |  |
| SEALED_AT | datetime | - | 是 |  |
| SECTION_QC_SCORE | smallint(5,0) | - | 是 |  |
| SECTION_QC_LEVEL | nvarchar(32) | 32 | 是 |  |
| SECTION_QC_DOCTOR_ID | numeric(19,0) | - | 是 |  |
| SECTION_QC_DOCTOR_NO | varchar(32) | 32 | 是 |  |
| SECTION_QC_DOCTOR_NAME | nvarchar(128) | 128 | 是 |  |
| SECTION_QC_AT | datetime | - | 是 |  |
| MODIFIED_NAME | nvarchar(128) | 128 | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(128) | 128 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |

---

## PIS_SECTION_DEFECT_STD

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| PIS_SECTION_DEFECT_STD_ID | numeric(19,0) | - | 否 | ✓ |
| PIS_SECTION_HQ_STD_ID | numeric(19,0) | - | 否 |  |
| DEFECT_STD_NAME | nvarchar(128) | 128 | 是 |  |
| DEFECT_STD_UPPER_LIMIT_SCORE | smallint(5,0) | - | 是 |  |
| DEFECT_STD_LOWER_LIMIT_SCORE | smallint(5,0) | - | 是 |  |
| SEQ_NO | int(10,0) | - | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(128) | 128 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(128) | 128 | 是 |  |

---

## PIS_SECTION_HQ_STD

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| PIS_SECTION_HQ_STD_ID | numeric(19,0) | - | 否 | ✓ |
| PIS_SECTION_QC_RULE_ID | numeric(19,0) | - | 否 |  |
| HQ_STD_NAME | nvarchar(128) | 128 | 否 |  |
| HQ_STD_FULL_SCORE | smallint(5,0) | - | 是 |  |
| SEQ_NO | int(10,0) | - | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(128) | 128 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(128) | 128 | 是 |  |

---

## PIS_SECTION_QC_RESULT

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| PIS_SECTION_QC_RESULT_ID | numeric(19,0) | - | 否 | ✓ |
| PIS_SECTION_ID | numeric(19,0) | - | 否 |  |
| PIS_SECTION_DEFECT_STD_ID | numeric(19,0) | - | 否 |  |
| DEFECT_SCORE | smallint(5,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(128) | 128 | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(128) | 128 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |

---

## PIS_SECTION_QC_RULE

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| PIS_SECTION_QC_RULE_ID | numeric(19,0) | - | 否 | ✓ |
| SECTION_QC_CATEGORY | numeric(19,0) | - | 是 |  |
| SECTION_QC_CONTENT | varchar(1024) | 1024 | 是 |  |
| SEQ_NO | int(10,0) | - | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(128) | 128 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(128) | 128 | 是 |  |

---

## PIS_SLIDE

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| PIS_SLIDE_ID | numeric(19,0) | - | 否 | ✓ |
| SLIDE_NO | varchar(32) | 32 | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| MODIFIED_NAME | nvarchar(128) | 128 | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |

---

## PIS_SPECIAL_ORDER

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| PIS_SPECIAL_ORDER_ID | numeric(19,0) | - | 否 | ✓ |
| PIS_EXAM_TASK_ID | numeric(19,0) | - | 否 |  |
| ORDER_BATCH_NO | varchar(32) | 32 | 是 |  |
| ORDER_CLASS_CODE | numeric(19,0) | - | 是 |  |
| ORDER_OBJECT_TYPE | numeric(19,0) | - | 是 |  |
| ORDER_OBJECT_ID | numeric(19,0) | - | 是 |  |
| ORDER_OBJECT_NO | varchar(32) | 32 | 是 |  |
| ORDER_OBJECT_NAME | nvarchar(64) | 64 | 是 |  |
| PIS_REAGENT_ID | numeric(19,0) | - | 是 |  |
| ORDER_STATUS | numeric(19,0) | - | 是 |  |
| ORDER_REJECT_REASON | nvarchar(256) | 256 | 是 |  |
| TAG_PRINT_TIMES | smallint(5,0) | - | 是 |  |
| WORKSHEET_PRINT_TIMES | smallint(5,0) | - | 是 |  |
| PRESC_DOCTOR_ID | numeric(19,0) | - | 是 |  |
| PRESC_DOCTOR_NO | varchar(32) | 32 | 是 |  |
| PRESC_DOCTOR_NAME | nvarchar(128) | 128 | 是 |  |
| PRESCIBED_AT | datetime | - | 是 |  |
| REVIEW_DOCTOR_ID | numeric(19,0) | - | 是 |  |
| REVIEW_DOCTOR_NO | varchar(32) | 32 | 是 |  |
| REVIEW_DOCTOR_NAME | nvarchar(128) | 128 | 是 |  |
| REVIEWED_AT | datetime | - | 是 |  |
| CONFIRM_DOCTOR_ID | numeric(19,0) | - | 是 |  |
| CONFIRM_DOCTOR_NO | varchar(32) | 32 | 是 |  |
| CONFIRM_DOCTOR_NAME | nvarchar(128) | 128 | 是 |  |
| CONFIRMED_AT | datetime | - | 是 |  |
| REJECT_DOCTOR_ID | numeric(19,0) | - | 是 |  |
| REJECT_DOCTOR_NO | varchar(32) | 32 | 是 |  |
| REJECT_DOCTOR_NAME | nvarchar(128) | 128 | 是 |  |
| REJECTED_AT | datetime | - | 是 |  |
| EXEC_DOCTOR_ID | numeric(19,0) | - | 是 |  |
| EXEC_DOCTOR_NO | varchar(32) | 32 | 是 |  |
| EXEC_DOCTOR_NAME | nvarchar(128) | 128 | 是 |  |
| EXECUTE_START_AT | datetime | - | 是 |  |
| EXECUTED_AT | datetime | - | 是 |  |
| ORDER_REAGENT_REMARK | nvarchar(512) | 512 | 是 |  |
| REMARK | nvarchar(256) | 256 | 是 |  |
| MARKET_RESULT | nvarchar(32) | 32 | 是 |  |
| SEQ_NO | int(10,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(128) | 128 | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(128) | 128 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |

---

## PIS_SPECIMEN

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| PIS_SPECIMEN_ID | numeric(19,0) | - | 否 | ✓ |
| PIS_EXAM_TASK_ID | numeric(19,0) | - | 是 |  |
| PIS_EXAM_REPORT_ID | numeric(19,0) | - | 是 |  |
| PIS_EXAM_REJECT_TASK_ID | numeric(19,0) | - | 是 |  |
| SPECIMEN_NAME | nvarchar(64) | 64 | 是 |  |
| BODY_SITE_NAME | nvarchar(64) | 64 | 是 |  |
| SPECIMEN_QTY | numeric(3,0) | - | 是 |  |
| SEPCIMEN_NO | varchar(32) | 32 | 是 |  |
| COLLECTED_AT | datetime | - | 是 |  |
| FIXED_AT | datetime | - | 是 |  |
| FIXATIVE_NAME | nvarchar(64) | 64 | 是 |  |
| FIXATIVE_NO | varchar(32) | 32 | 是 |  |
| FIXATIVE_CODE | numeric(19,0) | - | 是 |  |
| SPECIMEN_SIZE_NAME | nvarchar(64) | 64 | 是 |  |
| SPECIMEN_SIZE_NO | varchar(32) | 32 | 是 |  |
| SPECIMEN_SIZE_CODE | numeric(19,0) | - | 是 |  |
| RECEIPT_DOCTOR_ID | numeric(19,0) | - | 是 |  |
| RECEIPT_DOCTOR_NAME | nvarchar(128) | 128 | 是 |  |
| RECEIPT_DOCTOR_NO | varchar(32) | 32 | 是 |  |
| REJECTION_FLAG | numeric(19,0) | - | 否 |  |
| REJECTION_REASON | nvarchar(128) | 128 | 是 |  |
| RECEIPTED_AT | datetime | - | 是 |  |
| ADDITIONAL_FLAG | numeric(19,0) | - | 是 |  |
| TIMELY_REPORT_QC_DURATION | smallint(5,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(128) | 128 | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| CREATED_NAME | nvarchar(128) | 128 | 是 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_AT | datetime | - | 否 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| LAUNCH_SENDER_ID | numeric(19,0) | - | 是 |  |
| LAUNCH_SENDER_NO | varchar(32) | 32 | 是 |  |
| LAUNCH_SENDER_NAME | nvarchar(128) | 128 | 是 |  |
| SEND_RECEIPTER_ID | numeric(19,0) | - | 是 |  |
| SEND_RECEIPTER_NO | varchar(32) | 32 | 是 |  |
| SEND_RECEIPTER_NAME | nvarchar(128) | 128 | 是 |  |
| LAUNCH_SEND_TIME | datetime | - | 是 |  |
| SEND_STATUS_NO | varchar(16) | 16 | 是 |  |
| SEND_BATCH_NO | varchar(32) | 32 | 是 |  |

---

## PIS_STATISTICAL_REPORT

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| PIS_STATISTICAL_REPORT_ID | numeric(19,0) | - | 否 | ✓ |
| PIS_STATISTICAL_REPORT_NAME | nvarchar(64) | 64 | 是 |  |
| PIS_STATISTICAL_REPORT_URL | varchar(1024) | 1024 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(128) | 128 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(128) | 128 | 是 |  |
| SEQ_NO | int(10,0) | - | 是 |  |

---

## PIS_STORAGE_LOCATION

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| PIS_STORAGE_LOCATION_ID | numeric(19,0) | - | 否 | ✓ |
| PARENT_STORAGE_LOCATION_ID | numeric(19,0) | - | 否 |  |
| STORAGE_LOCATION_NAME | nvarchar(64) | 64 | 否 |  |
| STORAGE_LOCATION_NO | varchar(16) | 16 | 是 |  |
| STORAGE_UNIT_FLAG | numeric(19,0) | - | 否 |  |
| SEQ_NO | smallint(5,0) | - | 是 |  |
| ENABLED_FLAG | numeric(19,0) | - | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(128) | 128 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(128) | 128 | 是 |  |

---

## PIS_STORAGE_UNIT

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| PIS_STORAGE_UNIT_ID | numeric(19,0) | - | 否 | ✓ |
| PIS_STORAGE_LOCATION_ID | numeric(19,0) | - | 否 |  |
| STORAGE_UNIT_NAME | nvarchar(64) | 64 | 是 |  |
| STORAGE_UNIT_NO | varchar(16) | 16 | 是 |  |
| STORAGE_UNIT_CAPACITY | int(10,0) | - | 是 |  |
| STORAGE_UNIT_USAGE | int(10,0) | - | 是 |  |
| ENABLED_FLAG | numeric(19,0) | - | 否 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(128) | 128 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(128) | 128 | 是 |  |

---

## PIS_TASK_RECYCLING

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| PIS_TASK_RECYCLING_ID | numeric(19,0) | - | 否 | ✓ |
| PATH_NO | varchar(16) | 16 | 否 |  |
| PATH_NO_REUSED_FLAG | numeric(19,0) | - | 否 |  |
| PIS_EXAM_TASK_ID | numeric(19,0) | - | 否 |  |
| PIS_EXAM_CATEGROY_ID | numeric(19,0) | - | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(128) | 128 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(128) | 128 | 是 |  |

---

## PIS_TECH_ORDER

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| PIS_TECH_ORDER_ID | numeric(19,0) | - | 否 | ✓ |
| PIS_EXAM_TASK_ID | numeric(19,0) | - | 否 |  |
| ORDER_BATCH_NO | varchar(32) | 32 | 是 |  |
| ORDER_CLASS_CODE | numeric(19,0) | - | 是 |  |
| ORDER_OBJECT_ID | numeric(19,0) | - | 是 |  |
| ORDER_STATUS | numeric(19,0) | - | 是 |  |
| EXEC_QTY | smallint(5,0) | - | 是 |  |
| PRESC_DOCTOR_ID | numeric(19,0) | - | 是 |  |
| PRESC_DOCTOR_NO | varchar(32) | 32 | 是 |  |
| PRESC_DOCTOR_NAME | nvarchar(128) | 128 | 是 |  |
| PRESCIBED_AT | datetime | - | 是 |  |
| REVIEW_DOCTOR_ID | numeric(19,0) | - | 是 |  |
| REVIEW_DOCTOR_NO | varchar(32) | 32 | 是 |  |
| REVIEW_DOCTOR_NAME | nvarchar(128) | 128 | 是 |  |
| REVIEWED_AT | datetime | - | 是 |  |
| EXEC_DOCTOR_ID | numeric(19,0) | - | 是 |  |
| EXEC_DOCTOR_NO | varchar(32) | 32 | 是 |  |
| EXEC_DOCTOR_NAME | nvarchar(128) | 128 | 是 |  |
| EXECUTED_AT | datetime | - | 是 |  |
| REMARK | nvarchar(256) | 256 | 是 |  |
| MARKET_RESULT | nvarchar(32) | 32 | 是 |  |
| MODIFIED_NAME | nvarchar(128) | 128 | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(128) | 128 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |

---

## PIS_WATCH_FOLDER

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| PIS_WATCH_FOLDER_ID | numeric(19,0) | - | 否 | ✓ |
| PARENT_WATCH_FOLDER_ID | numeric(19,0) | - | 否 |  |
| FOLDER_NAME | nvarchar(64) | 64 | 否 |  |
| OWNER_ID | numeric(19,0) | - | 是 |  |
| SEQ_NO | smallint(5,0) | - | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(128) | 128 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(128) | 128 | 是 |  |

---

## PIS_WAX_BLOCK

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| PIS_WAX_BLOCK_ID | numeric(19,0) | - | 否 | ✓ |
| PIS_EXAM_TASK_ID | numeric(19,0) | - | 是 |  |
| PIS_SPECIMEN_ID | numeric(19,0) | - | 是 |  |
| PIS_TECH_ORDER_ID | numeric(19,0) | - | 是 |  |
| WAX_BLOCK_NO | varchar(8) | 8 | 是 |  |
| TASK_SOURCE_CODE | numeric(19,0) | - | 是 |  |
| TISSUE_NAME | nvarchar(64) | 64 | 是 |  |
| TISSUE_QTY | varchar(16) | 16 | 是 |  |
| TISSUE_UNIT_CODE | numeric(19,0) | - | 是 |  |
| WAX_BLOCK_STATUS | numeric(19,0) | - | 是 |  |
| WAX_BLOCK_STATUS_NO | nvarchar(32) | 32 | 是 |  |
| PRINT_TIMES | smallint(5,0) | - | 否 |  |
| DECALCIFICATION_FLAG | numeric(19,0) | - | 是 |  |
| RENOVATION_FLAG | numeric(19,0) | - | 是 |  |
| RESERVATION_FLAG | numeric(19,0) | - | 是 |  |
| DRAW_DOCTOR_ID | numeric(19,0) | - | 是 |  |
| DRAW_DOCTOR_NO | varchar(32) | 32 | 是 |  |
| DRAW_DOC_NAME | nvarchar(128) | 128 | 是 |  |
| DRAWN_AT | datetime | - | 是 |  |
| DRAW_REC_DOCTOR_ID | numeric(19,0) | - | 是 |  |
| DRAW_REC_DOCTOR_NO | varchar(32) | 32 | 是 |  |
| DRAW_REC_DOCTOR_NAME | nvarchar(128) | 128 | 是 |  |
| DRAW_REC_AT | datetime | - | 是 |  |
| TISSUE_CK_DOCTOR_ID | numeric(19,0) | - | 是 |  |
| TISSUE_CK_DOCTOR_NO | varchar(32) | 32 | 是 |  |
| TISSUE_CK_DOCTOR_NAME | nvarchar(128) | 128 | 是 |  |
| TISSUE_CHECKED_AT | datetime | - | 是 |  |
| DRAW_REMARK | nvarchar(128) | 128 | 是 |  |
| DEHYDRATE_DOCTOR_ID | numeric(19,0) | - | 是 |  |
| DEHYDRATE_DOCTOR_NO | varchar(32) | 32 | 是 |  |
| DEHYDRATE_DOCTOR_NAME | nvarchar(128) | 128 | 是 |  |
| DEHYDRATE_DOCTOR_IDS | varchar(256) | 256 | 是 |  |
| DEHYDRATE_DOCTOR_NOS | varchar(256) | 256 | 是 |  |
| DEHYDRATE_DOCTOR_NAMES | nvarchar(256) | 256 | 是 |  |
| DEHYDRATE_START_AT | datetime | - | 是 |  |
| DEHYDRATE_TERMINATE_AT | datetime | - | 是 |  |
| PIS_DEHYDRATE_BASKET_ID | numeric(19,0) | - | 是 |  |
| PIS_DEHYDRATOR_ID | numeric(19,0) | - | 是 |  |
| EMBEDDING_DOCTOR_ID | numeric(19,0) | - | 是 |  |
| EMBEDDING_DOCTOR_NO | varchar(32) | 32 | 是 |  |
| EMBEDDING_DOCTOR_NAME | nvarchar(128) | 128 | 是 |  |
| EMBEDDED_AT | datetime | - | 是 |  |
| EMBEDDING_DOCTOR_IDS | varchar(256) | 256 | 是 |  |
| EMBEDDING_DOCTOR_NOS | varchar(256) | 256 | 是 |  |
| EMBEDDING_DOCTOR_NAMES | nvarchar(256) | 256 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(128) | 128 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(128) | 128 | 是 |  |
| DRAW_QUALITY_EVALUATION | numeric(19,0) | - | 是 |  |

---

## PLATFORM_BACKUP_RECORD

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| PLATFORM_BACKUP_RECORD_ID | numeric(19,0) | - | 否 | ✓ |
| BACKUP_TABLE_NAME | varchar(100) | 100 | 是 |  |
| BACKUP_STATEMENT | nvarchar(-1) | -1 | 是 |  |
| BACKUP_AT | datetime | - | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |

---

## PLATFORM_CATEGORY_POWER

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| PLATFORM_CATEGORY_POWER_ID | numeric(19,0) | - | 否 | ✓ |
| PLATFORM_ROLE_OPERATE_POWER_ID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NO | varchar(64) | 64 | 否 |  |
| SYSTEM_SOURCE_NO | varchar(64) | 64 | 否 |  |
| EXAM_CATEGORY_NO | varchar(64) | 64 | 否 |  |
| CHECKED_FLAG | numeric(19,0) | - | 否 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| CREATED_AT | datetime | - | 否 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |

---

## PLATFORM_DATABASE_SCRIPT

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| PLATFORM_DATABASE_SCRIPT_ID | numeric(19,0) | - | 否 | ✓ |
| VERSION_TIME_STAMP | numeric(19,0) | - | 是 |  |
| FILE_PATH | varchar(200) | 200 | 是 |  |
| SCRIPT_TYPE_NO | varchar(100) | 100 | 是 |  |
| SCRIPT_RUN_STATUS_NO | varchar(100) | 100 | 是 |  |
| SCRIPT_CONTENT | nvarchar(-1) | -1 | 是 |  |
| SCRIPT_COMMENTS | nvarchar(-1) | -1 | 是 |  |
| SCRIPT_ERROR | nvarchar(-1) | -1 | 是 |  |
| SYSTEM_SOURCE_NO | varchar(64) | 64 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |

---

## PLATFORM_DICTIONARY

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| PLATFORM_DICTIONARY_ID | numeric(19,0) | - | 否 | ✓ |
| SYSTEM_SOURCE_NO | varchar(64) | 64 | 否 |  |
| DICT_CLASS_NAME | nvarchar(64) | 64 | 否 |  |
| DICT_ITEM_NO | varchar(64) | 64 | 否 |  |
| DICT_ITEM_NAME | nvarchar(64) | 64 | 否 |  |
| DICT_EXTERN_NO | varchar(64) | 64 | 是 |  |
| MEM1_NO | varchar(64) | 64 | 是 |  |
| MEM2_NO | varchar(64) | 64 | 是 |  |
| ALTER_FLAG | numeric(19,0) | - | 否 |  |
| VISIBLE_FLAG | numeric(19,0) | - | 否 |  |
| SEQ_NO | int(10,0) | - | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| CREATED_AT | datetime | - | 否 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |

---

## PLATFORM_ELEMENT_ITEM

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| PLATFORM_ELEMENT_ITEM_ID | numeric(19,0) | - | 否 | ✓ |
| ELEMENT_ITEM_GROUP_NAME | nvarchar(128) | 128 | 是 |  |
| BIND_MODEL_VALUE | nvarchar(64) | 64 | 是 |  |
| BIND_MODEL_NAME | nvarchar(256) | 256 | 是 |  |
| ELEMENT_ITEM_TYPE_NO | varchar(64) | 64 | 是 |  |
| ELEMENT_ITEM_DEFAULT_VALUE | nvarchar(64) | 64 | 是 |  |
| DATA_SOURCE_TYPE_NO | varchar(64) | 64 | 是 |  |
| DATA_SOURCE_FUNCTION | varchar(-1) | -1 | 是 |  |
| ELEMENT_ITEM_RELATION_SET | nvarchar(256) | 256 | 是 |  |
| ELEMENT_ITEM_RELATION_DEFAULT | nvarchar(32) | 32 | 是 |  |
| ELEMENT_ITEM_REGEX_TEXT | nvarchar(1024) | 1024 | 是 |  |
| TRIGGER_EVENT_TYPE_NO | varchar(64) | 64 | 是 |  |
| TRIGGER_EVENT_FUNCTION | varchar(4000) | 4000 | 是 |  |
| ELEMENT_ITEM_CSS | varchar(-1) | -1 | 是 |  |
| IS_FILTER | numeric(19,0) | - | 是 |  |
| IS_READ_ONLY | numeric(19,0) | - | 是 |  |
| REQUIRED_FLAG | numeric(19,0) | - | 是 |  |
| SEQ_NO | int(10,0) | - | 是 |  |
| SYSTEM_SOURCE_NO | varchar(64) | 64 | 是 |  |
| ELEMENT_ITEM_NAME | nvarchar(64) | 64 | 是 |  |
| ELEMENT_ITEM_PLACEHOLDER | nvarchar(100) | 100 | 是 |  |
| MODEL_SOURCE | nvarchar(64) | 64 | 是 |  |
| IS_DICTIONARY | numeric(19,0) | - | 是 |  |
| IS_INTERFACE | numeric(19,0) | - | 是 |  |
| ELEMENT_ITEM_DATA_PROPERTY | nvarchar(32) | 32 | 是 |  |
| BIND_MODEL_KEY | nvarchar(64) | 64 | 是 |  |
| ELEMENT_ITEM_BOUNDARY | nvarchar(128) | 128 | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |

---

## PLATFORM_FLOW_GROUP

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| PLATFORM_FLOW_GROUP_ID | numeric(19,0) | - | 否 | ✓ |
| SYSTEM_SOURCE_NO | varchar(64) | 64 | 否 |  |
| GROUP_NAME | nvarchar(32) | 32 | 否 |  |
| GROUP_LOCAL_ADDRESS | nvarchar(128) | 128 | 否 |  |
| GROUP_PRIVATE_ADDRESS | nvarchar(128) | 128 | 是 |  |
| GROUP_INTERNET_ADDRESS | nvarchar(128) | 128 | 是 |  |
| CONTACT_NAME | nvarchar(64) | 64 | 是 |  |
| CONTACT_PHONE | nvarchar(32) | 32 | 是 |  |
| GROUP_DESCRIPTION | nvarchar(500) | 500 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| CREATED_AT | datetime | - | 否 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |

---

## PLATFORM_FLOW_HOS_CONFIG

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| PLATFORM_FLOW_HOS_CONFIG_ID | numeric(19,0) | - | 否 | ✓ |
| SYSTEM_SOURCE_NO | varchar(64) | 64 | 否 |  |
| FLOW_HOSPITAL_NO | varchar(64) | 64 | 否 |  |
| PARENT_FLOW_HOSPITAL_NO | varchar(64) | 64 | 否 |  |
| FLOW_CONDITION | nvarchar(-1) | -1 | 否 |  |
| FLOW_MODE_NO | varchar(32) | 32 | 是 |  |
| RECEIVE_FLAG | numeric(19,0) | - | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| CREATED_AT | datetime | - | 否 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |

---

## PLATFORM_FLOW_HOSPITAL_INFO

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| PLATFORM_FLOW_HOSPITAL_INFO_ID | numeric(19,0) | - | 否 | ✓ |
| SYSTEM_SOURCE_NO | varchar(64) | 64 | 否 |  |
| PLATFORM_FLOW_GROUP_ID | numeric(19,0) | - | 否 |  |
| FLOW_HOSPITAL_NO | varchar(64) | 64 | 否 |  |
| FLOW_HOSPITAL_NAME | nvarchar(64) | 64 | 否 |  |
| HOSPITAL_LOCAL_ADDRESS | nvarchar(100) | 100 | 是 |  |
| HOSPITAL_PRIVATE_ADDRESS | nvarchar(100) | 100 | 是 |  |
| HOSPITAL_INTERNET_ADDRESS | nvarchar(100) | 100 | 是 |  |
| CONTACT_NAME | nvarchar(64) | 64 | 是 |  |
| CONTACT_PHONE | nvarchar(64) | 64 | 是 |  |
| DESCRIPTION | nvarchar(500) | 500 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| CREATED_AT | datetime | - | 否 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |

---

## PLATFORM_FLOW_PROXY_RECORD

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| PLATFORM_FLOW_PROXY_RECORD_ID | numeric(19,0) | - | 否 | ✓ |
| PLATFORM_REPORT_ID | numeric(19,0) | - | 否 |  |
| SERIAL_NUMBER | nvarchar(32) | 32 | 否 |  |
| SYSTEM_SOURCE_NO | varchar(64) | 64 | 否 |  |
| FROM_HOSPITAL_NO | varchar(64) | 64 | 否 |  |
| FROM_HOSPITAL_NAME | nvarchar(64) | 64 | 否 |  |
| TO_HOSPITAL_NO | varchar(64) | 64 | 是 |  |
| TO_HOSPITAL_NAME | nvarchar(64) | 64 | 是 |  |
| ASSIST_TYPE_NO | varchar(64) | 64 | 是 |  |
| ASSIST_TYPE_NAME | nvarchar(64) | 64 | 是 |  |
| FLOW_STATUS_NO | varchar(64) | 64 | 是 |  |
| PROXY_STATUS_NO | varchar(64) | 64 | 是 |  |
| PROXY_ERROR_MSG | nvarchar(-1) | -1 | 是 |  |
| REPORT_STATUS_NO | varchar(32) | 32 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| CREATED_AT | datetime | - | 否 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |

---

## PLATFORM_GROUP

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| PLATFORM_GROUP_ID | numeric(19,0) | - | 否 | ✓ |
| GROUP_NO | varchar(64) | 64 | 否 |  |
| GROUP_NAME | nvarchar(64) | 64 | 否 |  |
| GROUP_TYPE_NO | varchar(64) | 64 | 否 |  |
| GROUP_TYPE_NAME | nvarchar(64) | 64 | 是 |  |
| CONTACT_NAME | nvarchar(64) | 64 | 是 |  |
| CONTACT_PHONE | nvarchar(32) | 32 | 是 |  |
| DESCRIPTION | nvarchar(500) | 500 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| CREATED_AT | datetime | - | 否 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |

---

## PLATFORM_HOSPITAL_GROUP

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| PLATFORM_HOSPITAL_GROUP_ID | numeric(19,0) | - | 否 | ✓ |
| PLATFORM_GROUP_ID | numeric(19,0) | - | 是 |  |
| FLOW_HOSPITAL_NO | varchar(64) | 64 | 否 |  |
| FLOW_HOSPITAL_NAME | nvarchar(64) | 64 | 否 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| CREATED_AT | datetime | - | 否 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |

---

## PLATFORM_MANUAL_CHECK

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| PLATFORM_MANUAL_CHECK_ID | numeric(19,0) | - | 否 | ✓ |
| PLATFORM_STRUC_RESULT_MNG_ID | numeric(19,0) | - | 否 |  |
| MANUAL_CHECK_DOC_NAME | nvarchar(32) | 32 | 是 |  |
| MANUAL_CHECK_DOC_NO | varchar(64) | 64 | 是 |  |
| MANUAL_MODIFY_REC | nvarchar(512) | 512 | 是 |  |
| SEQ_NO | int(10,0) | - | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |
| SYSTEM_SOURCE_NO | varchar(64) | 64 | 否 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 是 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |

---

## PLATFORM_OTHER_RESULT

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| PLATFORM_OTHER_RESULT_ID | numeric(19,0) | - | 否 | ✓ |
| PLATFORM_REPORT_ID | numeric(19,0) | - | 否 |  |
| REPORT_RESULT_NO | varchar(100) | 100 | 是 |  |
| REPORT_RESULT_NAME | nvarchar(100) | 100 | 是 |  |
| REPORT_RESULT_PLAIN_TEXT | nvarchar(-1) | -1 | 是 |  |
| REPORT_RESULT_RICH_TEXT | nvarchar(-1) | -1 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |

---

## PLATFORM_PATIENT

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| PLATFORM_PATIENT_ID | numeric(19,0) | - | 否 | ✓ |
| PATIENT_NAME | nvarchar(128) | 128 | 否 |  |
| PATIENT_NAME_SPELLING | nvarchar(128) | 128 | 是 |  |
| IDCARD_NO | varchar(18) | 18 | 是 |  |
| GENDER_CODE | numeric(19,0) | - | 否 |  |
| BIRTH_DATE | date | - | 是 |  |
| GROUP_NO | varchar(64) | 64 | 是 |  |
| ACTIVE_FLAG | numeric(19,0) | - | 是 |  |
| REGISTRATION_AT | datetime | - | 否 |  |
| HOSPITAL_NO | varchar(64) | 64 | 否 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| CREATED_AT | datetime | - | 否 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |

---

## PLATFORM_PATIENT_CARDS

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| PLATFORM_PATIENT_CARDS_ID | numeric(19,0) | - | 否 | ✓ |
| PATIENT_GROUP_NO | varchar(64) | 64 | 否 |  |
| CARD_TYPE_NO | varchar(64) | 64 | 否 |  |
| CARD_NO | varchar(64) | 64 | 否 |  |
| SEQ_NO | int(10,0) | - | 否 |  |
| HOSPITAL_NO | varchar(64) | 64 | 否 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| CREATED_AT | datetime | - | 否 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |

---

## PLATFORM_PRINT_RECORD

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| PLATFORM_PRINT_RECORD_ID | numeric(19,0) | - | 否 | ✓ |
| PLATFORM_REPORT_ID | numeric(19,0) | - | 否 |  |
| PRINT_USER_NAME | nvarchar(64) | 64 | 是 |  |
| PRINT_USER_NO | varchar(64) | 64 | 是 |  |
| PRINT_AT | datetime | - | 是 |  |
| HOSPITAL_NO | varchar(64) | 64 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |

---

## PLATFORM_REPORT

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| PLATFORM_REPORT_ID | numeric(19,0) | - | 否 | ✓ |
| PLATFORM_PATIENT_ID | numeric(19,0) | - | 否 |  |
| PATIENT_TYPE_NO | varchar(64) | 64 | 否 |  |
| PATIENT_NO | varchar(64) | 64 | 是 |  |
| ADMISSION_NUMBER | nvarchar(32) | 32 | 是 |  |
| CURE_NO | varchar(64) | 64 | 是 |  |
| IDCARD_NO | varchar(64) | 64 | 是 |  |
| AGE | nvarchar(32) | 32 | 是 |  |
| WARD_NO | varchar(64) | 64 | 是 |  |
| WARD_NAME | nvarchar(32) | 32 | 是 |  |
| BED_NO | varchar(64) | 64 | 是 |  |
| CLINIC_DIAG_DESC | nvarchar(2000) | 2000 | 是 |  |
| EXAM_CATEGORY_NO | varchar(64) | 64 | 是 |  |
| EXAM_ITEM_NAMES | nvarchar(1000) | 1000 | 否 |  |
| APPLY_DEPT_NAME | nvarchar(32) | 32 | 是 |  |
| APPLY_DEPT_NO | varchar(64) | 64 | 是 |  |
| APPLY_DOC_NAME | nvarchar(32) | 32 | 是 |  |
| APPLY_AT | datetime | - | 是 |  |
| APPLY_DOC_NO | varchar(64) | 64 | 是 |  |
| REPORT_NO | varchar(64) | 64 | 是 |  |
| REPORT_NAME | nvarchar(32) | 32 | 是 |  |
| REPORT_AT | datetime | - | 是 |  |
| REVIEWER_NAME | nvarchar(32) | 32 | 是 |  |
| REVIEWER_NO | varchar(64) | 64 | 是 |  |
| REVIEW_AT | datetime | - | 是 |  |
| PUBLISHER_NAME | nvarchar(32) | 32 | 是 |  |
| PUBLISHER_NO | varchar(64) | 64 | 是 |  |
| PUBLIC_AT | datetime | - | 是 |  |
| PRINT_PAPER_SIZE | nvarchar(32) | 32 | 是 |  |
| SERIAL_NUMBER | nvarchar(32) | 32 | 否 |  |
| SYSTEM_SOURCE_NO | varchar(64) | 64 | 否 |  |
| PDF_PATH | nvarchar(300) | 300 | 是 |  |
| PRINT_COUNT | int(10,0) | - | 是 |  |
| CANCEL_FLAG | numeric(19,0) | - | 是 |  |
| CARD_TYPE_NO | varchar(64) | 64 | 是 |  |
| CARD_NO | varchar(64) | 64 | 是 |  |
| EXAM_POSITION_NAME | nvarchar(1000) | 1000 | 是 |  |
| EXAM_DEPT_NAME | nvarchar(32) | 32 | 是 |  |
| APPOINT_AT | datetime | - | 是 |  |
| APPOINT_DOC_NAME | nvarchar(32) | 32 | 是 |  |
| REGISTER_AT | datetime | - | 是 |  |
| REGISTER_DOC_NAME | nvarchar(32) | 32 | 是 |  |
| EXAM_AT | datetime | - | 是 |  |
| EXAM_DOC_NAME | nvarchar(32) | 32 | 是 |  |
| EXAM_EQUIPMENT_NAME | nvarchar(100) | 100 | 是 |  |
| TECH_NO | varchar(100) | 100 | 是 |  |
| NEG_POS_CODE | numeric(19,0) | - | 是 |  |
| ADDRESS | nvarchar(500) | 500 | 是 |  |
| TELEPHONE_NO | varchar(256) | 256 | 是 |  |
| UPLOAD_AT | datetime | - | 是 |  |
| HOSPITAL_NO | varchar(64) | 64 | 否 |  |
| IDCARD_TYPE | numeric(19,0) | - | 是 |  |
| LABEL_NO | varchar(64) | 64 | 是 |  |
| EXEM_DEPT_NO | varchar(64) | 64 | 是 |  |
| URGENT_FLAG | numeric(19,0) | - | 是 |  |
| REPORT_STATUS_NO | varchar(32) | 32 | 是 |  |
| FILE_FLAG | numeric(19,0) | - | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| CREATED_AT | datetime | - | 否 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |

---

## PLATFORM_REPORT_CHARGE_ITEM

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| PLATFORM_REPORT_CHARGE_ITEM_ID | numeric(19,0) | - | 否 | ✓ |
| PLATFORM_REPORT_REQUISITION_ID | numeric(19,0) | - | 否 |  |
| PLATFORM_REPORT_ID | numeric(19,0) | - | 否 |  |
| EXAM_ORDER_ITEM_NO | varchar(256) | 256 | 是 |  |
| EXAM_ITEM_NO | varchar(64) | 64 | 是 |  |
| EXAM_ITEM_NAME | nvarchar(64) | 64 | 是 |  |
| EXAM_ITEM_GROUP_NO | varchar(64) | 64 | 是 |  |
| EXAM_ITEM_QTY | numeric(19,0) | - | 是 |  |
| EXAM_ITEM_UNIT | nvarchar(32) | 32 | 是 |  |
| CONFIRM_NO | varchar(64) | 64 | 是 |  |
| EXAM_ITEM_PRICE | numeric(16,4) | - | 是 |  |
| ITEM_CHARGE_FLAG | numeric(19,0) | - | 是 |  |
| REFUND_REASON | nvarchar(256) | 256 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| CREATED_AT | datetime | - | 否 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |

---

## PLATFORM_REPORT_ECG_RESULT

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| PLATFORM_REPORT_ECG_RESULT_ID | numeric(19,0) | - | 否 | ✓ |
| PLATFORM_REPORT_ID | numeric(19,0) | - | 是 |  |
| EXAM_FINDINGS_PLAIN_TEXT | nvarchar(-1) | -1 | 是 |  |
| EXAM_FINDINGS_RICH_TEXT | nvarchar(-1) | -1 | 是 |  |
| EXAM_CONCLUSION_PLAIN_TEXT | nvarchar(-1) | -1 | 是 |  |
| EXAM_CONCLUSION_RICH_TEXT | varchar(50) | 50 | 是 |  |
| EXAM_METHOD | nvarchar(1000) | 1000 | 是 |  |
| HEART_RATE | nvarchar(128) | 128 | 是 |  |
| P_R_INTERVAL | varchar(128) | 128 | 是 |  |
| QPS_TIME_LIMIT | varchar(128) | 128 | 是 |  |
| QT_TIME_LIMIT | varchar(128) | 128 | 是 |  |
| QTC_TIME_LIMIT | varchar(128) | 128 | 是 |  |
| SV1 | varchar(128) | 128 | 是 |  |
| RV5 | varchar(128) | 128 | 是 |  |
| SV2 | varchar(128) | 128 | 是 |  |
| RV6 | varchar(128) | 128 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(256) | 256 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |

---

## PLATFORM_REPORT_EIS_RESULT

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| PLATFORM_REPORT_EIS_RESULT_ID | numeric(19,0) | - | 否 | ✓ |
| PLATFORM_REPORT_ID | numeric(19,0) | - | 是 |  |
| EXAM_FINDINGS_RICH_TEXT | nvarchar(-1) | -1 | 是 |  |
| EXAM_FINDINGS_PLAIN_TEXT | nvarchar(-1) | -1 | 是 |  |
| EXAM_CONCLUSION_PLAIN_TEXT | nvarchar(-1) | -1 | 是 |  |
| EXAM_CONCLUSION_RICH_TEXT | nvarchar(-1) | -1 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |

---

## PLATFORM_REPORT_IMAGE

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| PLATFORM_REPORT_IMAGE_ID | numeric(19,0) | - | 否 | ✓ |
| PLATFORM_REPORT_ID | numeric(19,0) | - | 否 |  |
| IMAGE_NO | varchar(64) | 64 | 否 |  |
| IMAGE_UID | nvarchar(64) | 64 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| CREATED_AT | datetime | - | 否 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |

---

## PLATFORM_REPORT_LIS_RESULT

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| PLATFORM_REPORT_LIS_RESULT_ID | numeric(19,0) | - | 否 | ✓ |
| PLATFORM_REPORT_ID | numeric(19,0) | - | 否 |  |
| ITEM_NO | varchar(64) | 64 | 是 |  |
| ITEM_NAME | nvarchar(64) | 64 | 是 |  |
| LIS_RESULT | nvarchar(256) | 256 | 是 |  |
| LIS_AT | datetime | - | 是 |  |
| UNIT | nvarchar(32) | 32 | 是 |  |
| NEG_POS_CODE | numeric(19,0) | - | 是 |  |
| REFERENCE_VALUE | nvarchar(32) | 32 | 是 |  |
| HIGH_AND_LOW_POINTS | nvarchar(32) | 32 | 是 |  |
| PRINT_NUM | numeric(19,0) | - | 是 |  |
| SPECIMEN_TYPE_NO | varchar(64) | 64 | 是 |  |
| SPECIMEN_SAMPLE_AT | datetime | - | 是 |  |
| INDICATOR_FLAG | numeric(19,0) | - | 是 |  |
| SPECIMEN_REVEIVE_AT | datetime | - | 是 |  |
| SEQ_NO | int(10,0) | - | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |
| SYSTEM_SOURCE_NO | varchar(64) | 64 | 否 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 是 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |

---

## PLATFORM_REPORT_PIS_RESULT

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| PLATFORM_REPORT_PIS_RESULT_ID | numeric(19,0) | - | 否 | ✓ |
| PLATFORM_REPORT_ID | numeric(19,0) | - | 是 |  |
| EXAM_MATERIAL_PLAIN_TEXT | nvarchar(-1) | -1 | 是 |  |
| EXAM_MATERIAL_RICH_TEXT | nvarchar(-1) | -1 | 是 |  |
| EXAM_FINDINGS_PLAIN_TEXT | nvarchar(-1) | -1 | 是 |  |
| EXAM_FINDINGS_RICH_TEXT | nvarchar(-1) | -1 | 是 |  |
| EXAM_CONCLUSION_PLAIN_TEXT | nvarchar(-1) | -1 | 是 |  |
| EXAM_CONCLUSION_RICH_TEXT | nvarchar(-1) | -1 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |

---

## PLATFORM_REPORT_REQUISITION

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| PLATFORM_REPORT_REQUISITION_ID | numeric(19,0) | - | 否 | ✓ |
| PLATFORM_REPORT_ID | numeric(19,0) | - | 否 |  |
| SERIAL_NUMBER | nvarchar(32) | 32 | 否 |  |
| EXAM_REQUISITION_CONTENT | nvarchar(-1) | -1 | 是 |  |
| APPLY_HOSPITAL_NO | varchar(64) | 64 | 是 |  |
| APPLY_HOSPITAL_NAME | nvarchar(64) | 64 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| CREATED_AT | datetime | - | 否 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |

---

## PLATFORM_REPORT_RIS_RESULT

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| PLATFORM_REPORT_RIS_RESULT_ID | numeric(19,0) | - | 否 | ✓ |
| PLATFORM_REPORT_ID | numeric(19,0) | - | 是 |  |
| EXAM_FINDINGS_PLAIN_TEXT | nvarchar(-1) | -1 | 是 |  |
| EXAM_FINDINGS_RICH_TEXT | nvarchar(-1) | -1 | 是 |  |
| EXAM_CONCLUSION_PLAIN_TEXT | nvarchar(-1) | -1 | 是 |  |
| EXAM_CONCLUSION_RICH_TEXT | nvarchar(-1) | -1 | 是 |  |
| EXAM_METHOD | nvarchar(1000) | 1000 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| CREATED_AT | datetime | - | 否 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |

---

## PLATFORM_REPORT_UIS_RESULT

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| PLATFORM_REPORT_UIS_RESULT_ID | numeric(19,0) | - | 否 | ✓ |
| PLATFORM_REPORT_ID | numeric(19,0) | - | 是 |  |
| EXAM_FINDINGS_PLAIN_TEXT | nvarchar(-1) | -1 | 是 |  |
| EXAM_FINDINGS_RICH_TEXT | nvarchar(-1) | -1 | 是 |  |
| EXAM_CONCLUSION_PLAIN_TEXT | nvarchar(-1) | -1 | 是 |  |
| EXAM_CONCLUSION_RICH_TEXT | nvarchar(-1) | -1 | 是 |  |
| EXAM_OBSERVATION_PLAIN_TEXT | nvarchar(-1) | -1 | 是 |  |
| EXAM_OBSERVATION_RICH_TEXT | nvarchar(-1) | -1 | 是 |  |
| EXAM_METHOD | nvarchar(1000) | 1000 | 是 |  |
| EXAM_PROBE_RATE | varchar(100) | 100 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(256) | 256 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |

---

## PLATFORM_ROLE_OPERATE_POWER

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| PLATFORM_ROLE_OPERATE_POWER_ID | numeric(19,0) | - | 否 | ✓ |
| ROLE_NO | varchar(64) | 64 | 否 |  |
| ROLE_NAME | nvarchar(64) | 64 | 否 |  |
| OPERATE_NO | varchar(64) | 64 | 否 |  |
| OPERATE_NO_NAME | nvarchar(64) | 64 | 否 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CHECKED_FLAG | numeric(19,0) | - | 否 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NO | varchar(64) | 64 | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| CREATED_AT | datetime | - | 否 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |

---

## PLATFORM_SCHEDULE_TASK

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| PLATFORM_SCHEDULE_TASK_ID | numeric(19,0) | - | 否 | ✓ |
| SYSTEM_SOURCE_NO | varchar(64) | 64 | 否 |  |
| PLATFORM_REPORT_ID | numeric(19,0) | - | 否 |  |
| SCHEDULE_TASK_NAME | nvarchar(64) | 64 | 是 |  |
| SCHEDULE_TASK_STATUS_NO | varchar(64) | 64 | 是 |  |
| SCHEDULE_TASK_FAIL_DESC | varchar(-1) | -1 | 是 |  |
| RETRY_COUNT | numeric(19,0) | - | 是 |  |
| SCHEDULE_TASK_PLANNED_AT | datetime | - | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NO | varchar(64) | 64 | 是 |  |
| HOSPITAL_NAME | nvarchar(256) | 256 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |

---

## PLATFORM_SETTINGS

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| PLATFORM_SETTINGS_ID | numeric(19,0) | - | 否 | ✓ |
| SETTING_DOMAIN_NO | varchar(64) | 64 | 否 |  |
| SETTING_DOMAIN_NAME | nvarchar(32) | 32 | 否 |  |
| SYSTEM_SOURCE_NO | varchar(64) | 64 | 否 |  |
| SETTING_ITEM_KEY | nvarchar(32) | 32 | 否 |  |
| SETTING_ITEM_SECTION | nvarchar(64) | 64 | 否 |  |
| SETTING_ITEM_ENTRY | nvarchar(64) | 64 | 否 |  |
| SETTING_ITEM_DATA_TYPE_NO | varchar(64) | 64 | 否 |  |
| SETTING_DOMAIN_IDENTITY | nvarchar(64) | 64 | 是 |  |
| SETTING_VALUE | nvarchar(-1) | -1 | 是 |  |
| SETTING_DATASOURCE_CLASSNAME | nvarchar(-1) | -1 | 是 |  |
| PRIVATE_BY | numeric(19,0) | - | 是 |  |
| REMARK | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| CREATED_AT | datetime | - | 否 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |

---

## PLATFORM_STRUC_ITEMS

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| PLATFORM_STRUC_ITEMS_ID | numeric(19,0) | - | 否 | ✓ |
| ITEM_NAME | nvarchar(19) | 19 | 是 |  |
| ITEM_STATUS_NO | varchar(64) | 64 | 是 |  |
| LEAD_DOC_NAME | nvarchar(32) | 32 | 是 |  |
| LEAD_DOC_NO | varchar(32) | 32 | 是 |  |
| EXEC_START_AT | time | - | 是 |  |
| EXEC_END_AT | time | - | 是 |  |
| COPILOT_AUTO_CHECK_FLAG | numeric(19,0) | - | 是 |  |
| ITEM_DESCRIBETION | nvarchar(128) | 128 | 是 |  |
| ITEM_REPORT_CONTENT_SCOPE | nvarchar(256) | 256 | 是 |  |
| ITEM_INDICATORS_CONTENT | nvarchar(-1) | -1 | 是 |  |
| ITEM_EXPORT_DATA_SCOPE | nvarchar(-1) | -1 | 是 |  |
| ITEM_QUERY_CONDITION | nvarchar(-1) | -1 | 是 |  |
| SEQ_NO | int(10,0) | - | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |
| SYSTEM_SOURCE_NO | varchar(64) | 64 | 否 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 是 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| HOSPITAL_NO | varchar(64) | 64 | 否 |  |

---

## PLATFORM_STRUC_RESULT

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| PLATFORM_STRUC_RESULT_ID | numeric(19,0) | - | 否 | ✓ |
| PLATFORM_STRUC_RESULT_MNG_ID | numeric(19,0) | - | 否 |  |
| STRUC_INDICATOR | nvarchar(32) | 32 | 是 |  |
| RESULT | nvarchar(128) | 128 | 是 |  |
| UNIT | nvarchar(19) | 19 | 是 |  |
| MATCH_FLAG | numeric(19,0) | - | 是 |  |
| SEQ_NO | int(10,0) | - | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |
| SYSTEM_SOURCE_NO | varchar(64) | 64 | 否 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 是 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |

---

## PLATFORM_STRUC_RESULT_MNG

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| PLATFORM_STRUC_RESULT_MNG_ID | numeric(19,0) | - | 否 | ✓ |
| PLATFORM_REPORT_ID | numeric(19,0) | - | 否 |  |
| PLATFORM_STRUC_ITEMS_ID | numeric(19,0) | - | 否 |  |
| STRUC_AT | datetime | - | 是 |  |
| COPILOT_CHECK_FLAG | numeric(19,0) | - | 是 |  |
| COPILOT_CHECK_RESULT_FLAG | numeric(19,0) | - | 是 |  |
| COPILOT_CHECK_RESULT_CONTENT | nvarchar(-1) | -1 | 是 |  |
| COPILOT_CHECK_AT | datetime | - | 是 |  |
| COPILOT_MANUAL_CHECK_FLAG | numeric(19,0) | - | 是 |  |
| COPILOT_MANUAL_VALID_FLAG | numeric(19,0) | - | 是 |  |
| COPILOT_MANUAL_VALID_REMARK | nvarchar(256) | 256 | 是 |  |
| MANUAL_CHECK_FLAG | numeric(19,0) | - | 是 |  |
| MANUAL_CHECK_DOC_NAME | nvarchar(32) | 32 | 是 |  |
| MANUAL_CHECK_DOC_NO | varchar(64) | 64 | 是 |  |
| MANUAL_CHECK_AT | datetime | - | 是 |  |
| MANUAL_MODIFY_REC | nvarchar(512) | 512 | 是 |  |
| SEQ_NO | int(10,0) | - | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |
| SYSTEM_SOURCE_NO | varchar(64) | 64 | 否 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 是 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| HOSPITAL_NO | varchar(64) | 64 | 否 |  |

---

## PLATFORM_USER_SIGNATURE

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| PLATFORM_USER_SIGNATURE_ID | numeric(19,0) | - | 否 | ✓ |
| USER_NO | varchar(64) | 64 | 否 |  |
| USER_NAME | nvarchar(64) | 64 | 否 |  |
| SIGNATURE_FILE | nvarchar(-1) | -1 | 是 |  |
| USER_HOSPITAL_NO | varchar(64) | 64 | 是 |  |
| USER_HOSPITAL_NAME | nvarchar(64) | 64 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| CREATED_AT | datetime | - | 否 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |

---

## RIS_LeaveRelateLog

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| StudyUID | varchar(128) | 128 | 是 |  |
| PatientID | varchar(64) | 64 | 是 |  |
| AccessionNo | varchar(32) | 32 | 是 |  |
| UpdateDate | datetime | - | 是 |  |

---

## RIS_SPS

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| SPS_ID | int(10,0) | - | 否 | ✓ |
| SPS_AETitle | varchar(64) | 64 | 是 |  |
| SPS_StartDateTime | datetime | - | 否 |  |
| SPS_PhysiciansName | varchar(64) | 64 | 是 |  |
| SPS_Modality | varchar(64) | 64 | 是 |  |
| SPS_Description | varchar(128) | 128 | 是 |  |
| SPS_CodeValue | varchar(512) | 512 | 是 |  |
| SPS_CodingSchemeVersion | varchar(128) | 128 | 是 |  |
| SPS_CodingSchemeDesignator | varchar(128) | 128 | 是 |  |
| SPS_CodeMeaning | varchar(128) | 128 | 是 |  |
| RP_ID | int(10,0) | - | 否 |  |
| RP_StudyUID | varchar(128) | 128 | 否 |  |
| RP_Description | varchar(128) | 128 | 是 |  |
| ApplyNo | int(10,0) | - | 否 |  |
| AccessionNo | varchar(32) | 32 | 是 |  |
| PatientID | varchar(64) | 64 | 否 |  |
| PatientEnglishName | varchar(64) | 64 | 是 |  |
| PPS_ID | varchar(20) | 20 | 是 |  |
| PPS_StudyUID | varchar(128) | 128 | 是 |  |
| PPS_StartDateTime | datetime | - | 是 |  |
| PPS_EndDateTime | datetime | - | 是 |  |
| PPS_Description | varchar(128) | 128 | 是 |  |
| PPS_ProtocolName | varchar(128) | 128 | 是 |  |
| PPS_IsPGP | int(10,0) | - | 是 |  |
| PPS_Status | int(10,0) | - | 否 |  |
| PPS_SopInstanceUID | varchar(128) | 128 | 是 |  |
| PPS_ImageCount | int(10,0) | - | 是 |  |
| IMG_Modality | varchar(20) | 20 | 是 |  |
| IMG_BodyPart | varchar(100) | 100 | 是 |  |
| IMG_StudyDateTime | datetime | - | 是 |  |
| PatientName | varchar(64) | 64 | 是 |  |
| StudyDescription | varchar(100) | 100 | 是 |  |
| StationName | varchar(50) | 50 | 是 |  |
| RefPhysiciansName | varchar(50) | 50 | 是 |  |
| InstitutionName | varchar(50) | 50 | 是 |  |
| PerformingPhysiciansName | varchar(50) | 50 | 是 |  |

---

## RIS_WebAccessNodeInfo

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| SerialNo | numeric(18,0) | - | 否 |  |
| ApplyNo | int(10,0) | - | 否 |  |
| AccessType | int(10,0) | - | 是 |  |
| AccessSystem | int(10,0) | - | 是 |  |
| AccessUserID | varchar(50) | 50 | 是 |  |
| AccessUserName | varchar(100) | 100 | 是 |  |
| AccessTime | datetime | - | 是 |  |
| AccessComputerName | varchar(100) | 100 | 是 |  |
| AccessIP | varchar(30) | 30 | 是 |  |
| InserTime | datetime | - | 是 |  |
| HospitalCode | varchar(50) | 50 | 是 |  |
| StudyUID | varchar(128) | 128 | 是 |  |
| SubSysCode | varchar(40) | 40 | 是 |  |

---

## ROLE

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| BIZ_ROLE_ID | numeric(19,0) | - | 否 | ✓ |
| PERSON_ID | numeric(19,0) | - | 否 |  |
| BIZ_ROLE_TYPE_CODE | numeric(19,0) | - | 是 |  |
| CONFIDENTIALITY_CODE | numeric(19,0) | - | 是 |  |
| DESENSITISE_SCHEME_ID | numeric(19,0) | - | 是 |  |
| CONFIDENTIALITY_LEVEL_CODE | numeric(19,0) | - | 是 |  |
| ANONYM | nvarchar(128) | 128 | 是 |  |
| FAKE_IDENTITY_FLAG | numeric(19,0) | - | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| MODIFIED_AT | datetime | - | 是 |  |

---

## SCHEDULE_DAY_OF_WEEK

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| SCHEDULE_DAY_OF_WEEK_ID | numeric(19,0) | - | 否 | ✓ |
| EXAM_APPOINTMENT_SCHEDULE_ID | numeric(19,0) | - | 否 |  |
| DAY_OF_WEEK_CODE | numeric(19,0) | - | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |

---

## SCHEDULED_PROC_STEP

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| SCHEDULED_PROC_STEP_ID | numeric(19,0) | - | 否 | ✓ |
| EXAM_TASK_ID | numeric(19,0) | - | 否 |  |
| SYSTEM_SOURCE_NO | nvarchar(32) | 32 | 是 |  |
| SPS_NO | nvarchar(64) | 64 | 是 |  |
| SPS_AE_TITLE | nvarchar(64) | 64 | 是 |  |
| SPS_START_AT | datetime | - | 是 |  |
| SPS_PHYSICIANS_NAME | nvarchar(64) | 64 | 是 |  |
| SPS_MODALITY | nvarchar(64) | 64 | 是 |  |
| SPS_DESCRIPTION | nvarchar(128) | 128 | 是 |  |
| SPS_TRGT_CODE | nvarchar(128) | 128 | 是 |  |
| SPS_VERSION | nvarchar(128) | 128 | 是 |  |
| SPS_DESIGNATOR | nvarchar(128) | 128 | 是 |  |
| SPS_MEANING | nvarchar(128) | 128 | 是 |  |
| RP_NO | nvarchar(32) | 32 | 是 |  |
| RP_STUDYUID | nvarchar(255) | 255 | 是 |  |
| RP_DESCRIPTION | nvarchar(128) | 128 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |

---

## SCREEN_BASE_CONFIG

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| SCREEN_BASE_CONFIG_ID | numeric(19,0) | - | 否 | ✓ |
| SCREEN_TRIAGE_PERIPHERAL_ID | numeric(19,0) | - | 否 |  |
| CONFIG_ITEM_NO | varchar(64) | 64 | 否 |  |
| CONFIG_ITEM_NAME | nvarchar(200) | 200 | 否 |  |
| CONFIG_ITEM_DATA_TYPE_NO | varchar(64) | 64 | 否 |  |
| CONFIG_ITEM_VALUE | nvarchar(-1) | -1 | 是 |  |
| CREATED_BY | numeric(19,0) | - | 否 |  |
| CREATED_NAME | nvarchar(256) | 256 | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(256) | 256 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 是 |  |
| SYSTEM_SOURCE_NO | varchar(32) | 32 | 否 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(256) | 256 | 是 |  |

---

## SCREEN_BLOCK_CONFIG

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| SCREEN_BLOCK_CONFIG_ID | numeric(19,0) | - | 否 | ✓ |
| SCREEN_TRIAGE_PERIPHERAL_ID | numeric(19,0) | - | 否 |  |
| BLOCK_NO | varchar(64) | 64 | 否 |  |
| BLOCK_TITLE | nvarchar(200) | 200 | 是 |  |
| CLASSIFY_NO | varchar(64) | 64 | 是 |  |
| CLASSIFY_TYPE_NO | varchar(64) | 64 | 是 |  |
| PARENT_CLASSIFY_NO | varchar(64) | 64 | 是 |  |
| RESOURCE_TYPE_NO | varchar(64) | 64 | 是 |  |
| MEDIA_RESOURCE_ID | numeric(19,0) | - | 是 |  |
| MEDIA_RESOURCE_PATH | nvarchar(500) | 500 | 是 |  |
| NOTICE_CONTENT | nvarchar(-1) | -1 | 是 |  |
| BLOCK_PAGE_NUMBER_MODEL | varchar(64) | 64 | 是 |  |
| CUSTOM_NUMBER | smallint(5,0) | - | 是 |  |
| EXAM_TRIAGE_STATUS_LIST | varchar(200) | 200 | 是 |  |
| CREATED_BY | numeric(19,0) | - | 否 |  |
| CREATED_NAME | nvarchar(256) | 256 | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(256) | 256 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 是 |  |
| SYSTEM_SOURCE_NO | varchar(32) | 32 | 否 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(256) | 256 | 是 |  |

---

## SCREEN_DICTIONARY

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| SCREEN_DICTIONARY_ID | numeric(19,0) | - | 否 | ✓ |
| DICT_CLASS_NAME | varchar(50) | 50 | 是 |  |
| DICT_ITEM_NO | varchar(50) | 50 | 是 |  |
| DICT_ITEM_NAME | varchar(64) | 64 | 是 |  |
| DICT_EXTERN_NO | varchar(50) | 50 | 是 |  |
| MEM1_NO | varchar(30) | 30 | 是 |  |
| MEM2_NO | varchar(30) | 30 | 是 |  |
| ALTER_FLAG | numeric(19,0) | - | 是 |  |
| VISIBLE_FLAG | numeric(19,0) | - | 是 |  |
| SEQ_NO | int(10,0) | - | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 是 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |

---

## SCREEN_JSON_BASECONFIG

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| SCREEN_TRIAGE_PERIPHERAL_ID | numeric(19,0) | - | 是 |  |
| SCREENDISPLAYENABLE | int(10,0) | - | 是 |  |
| TOTALBLOCK | int(10,0) | - | 是 |  |
| PAGEBLOCKX | int(10,0) | - | 是 |  |
| PAGEBLOCKY | int(10,0) | - | 是 |  |
| SCREENTEMPLATE | nvarchar(50) | 50 | 是 |  |
| SCREENTITLE | nvarchar(50) | 50 | 是 |  |
| NOTICECONTENT | nvarchar(100) | 100 | 是 |  |
| PAGEINTERVAL | int(10,0) | - | 是 |  |
| SOUNDENABLE | int(10,0) | - | 是 |  |
| BROADCASTTIMES | int(10,0) | - | 是 |  |
| BROADCASTSPEED | int(10,0) | - | 是 |  |

---

## SCREEN_JSON_BLOCKCONFIG

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| SCREEN_TRIAGE_PERIPHERAL_ID | numeric(19,0) | - | 是 |  |
| BLOCKID | nvarchar(50) | 50 | 是 |  |
| PAGEINDEX | int(10,0) | - | 是 |  |
| BLOCKTITLE | nvarchar(50) | 50 | 是 |  |
| CLASSIFYID | nvarchar(50) | 50 | 是 |  |
| CLASSIFYTYPE | nvarchar(20) | 20 | 是 |  |
| RESOURCETYPECODE | nvarchar(20) | 20 | 是 |  |
| BLOCKWAITNUM | int(10,0) | - | 是 |  |
| ACTIVEBLOCKNUMBER | int(10,0) | - | 是 |  |

---

## SCREEN_MEDIA_RESOURCE

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| SCREEN_MEDIA_RESOURCE_ID | numeric(19,0) | - | 否 | ✓ |
| MEDIA_RESOURCE_NAME | varchar(255) | 255 | 是 |  |
| MEDIA_RESOURCE_SIZE | numeric(19,0) | - | 是 |  |
| MEDIA_RESOURCE_PATH | varchar(255) | 255 | 是 |  |
| MEDIA_RESOURCE_TYPE_NO | varchar(255) | 255 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 是 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |

---

## SCREEN_PERIPHERAL_SETTING

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| SCREEN_PERIPHERAL_SETTING_ID | numeric(19,0) | - | 否 | ✓ |
| SCREEN_TRIAGE_PERIPHERAL_ID | numeric(19,0) | - | 否 |  |
| PERIPHERAL_SETTING_NAME | varchar(64) | 64 | 是 |  |
| PERIPHERAL_TYPE_NO | varchar(64) | 64 | 是 |  |
| PERIPHERAL_SETTING_LOGO | nvarchar(-1) | -1 | 是 |  |
| PERIPHERAL_SETTING_CONTENT | nvarchar(-1) | -1 | 是 |  |
| PERIPHERAL_SETTING_GROUP | varchar(128) | 128 | 是 |  |
| SYSTEM_SOURCE_NO | varchar(64) | 64 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 是 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |

---

## SCREEN_PLAY_SOUND_CONFIG

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| SCREEN_PLAY_SOUND_CONFIG_ID | numeric(19,0) | - | 否 | ✓ |
| SCREEN_TRIAGE_PERIPHERAL_ID | numeric(19,0) | - | 否 |  |
| CLASSIFY_NO | varchar(64) | 64 | 否 |  |
| CLASSIFY_TYPE_NO | varchar(64) | 64 | 否 |  |
| ROOM_TITLE | nvarchar(200) | 200 | 否 |  |
| CREATED_BY | numeric(19,0) | - | 否 |  |
| CREATED_NAME | nvarchar(256) | 256 | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(256) | 256 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 是 |  |
| SYSTEM_SOURCE_NO | varchar(32) | 32 | 否 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(256) | 256 | 是 |  |

---

## SCREEN_SETTINGS

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| SCREEN_SETTINGS_ID | numeric(19,0) | - | 否 | ✓ |
| SETTING_ITEM_KEY | varchar(256) | 256 | 是 |  |
| SETTING_ITEM_DOMAIN_NO | varchar(20) | 20 | 是 |  |
| SETTING_ITEM_DOMAIN_NAME | varchar(64) | 64 | 是 |  |
| SETTING_ITEM_SECTION | varchar(64) | 64 | 是 |  |
| SETTING_ITEM_ENTRY | varchar(256) | 256 | 是 |  |
| SETTING_ITEM_DATATYPE | varchar(20) | 20 | 是 |  |
| DEFAULT_VALUE | nvarchar(-1) | -1 | 是 |  |
| PRIVATE_BY | numeric(19,0) | - | 是 |  |
| VISIBLE_FLAG | numeric(19,0) | - | 是 |  |
| SEQ_NO | int(10,0) | - | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 是 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |

---

## SCREEN_TRIAGE_PERIPHERAL

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| SCREEN_TRIAGE_PERIPHERAL_ID | numeric(19,0) | - | 否 | ✓ |
| TRIAGE_PERIPHERAL_IP | varchar(32) | 32 | 是 |  |
| TRIAGE_PERIPHERAL_HOST_NAME | varchar(64) | 64 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |

---

## SLAVE

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| HOSPITALCODE | varchar(50) | 50 | 否 | ✓ |
| SUBSYSCODE | varchar(20) | 20 | 否 | ✓ |
| CLASSCODE | varchar(20) | 20 | 否 | ✓ |
| GROUPCODE | varchar(20) | 20 | 是 |  |
| SLAVENO | varchar(64) | 64 | 否 | ✓ |
| SLAVENAME | varchar(2000) | 2000 | 否 |  |
| ENGNAME | varchar(100) | 100 | 是 |  |
| REMARK | varchar(-1) | -1 | 是 |  |
| EXTERNCODE | varchar(64) | 64 | 是 |  |
| SLAVENUM | varchar(20) | 20 | 是 |  |
| ORDERNO | int(10,0) | - | 是 |  |
| PARENTCLASSCODE | varchar(20) | 20 | 是 |  |
| PARENTCODE | varchar(20) | 20 | 是 |  |
| PARENTNAME | varchar(64) | 64 | 是 |  |
| MEMCODE1 | varchar(64) | 64 | 是 |  |
| MEMCODE2 | varchar(64) | 64 | 是 |  |
| SLAVECOLOR | varchar(20) | 20 | 是 |  |
| RESERVEFIELD1 | varchar(64) | 64 | 是 |  |
| RESERVEFIELD2 | varchar(64) | 64 | 是 |  |
| RESERVEFIELD3 | varchar(64) | 64 | 是 |  |
| RESERVEFIELD4 | varchar(64) | 64 | 是 |  |
| RESERVEFIELD5 | varchar(64) | 64 | 是 |  |
| VISIBLE | char(1) | 1 | 是 |  |
| SYSFLAG | char(1) | 1 | 是 |  |
| STOPFLAG | char(1) | 1 | 是 |  |
| TIMETEMP | timestamp | - | 是 |  |

---

## SLAVE_CLASS

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| SUBSYSCODE | varchar(20) | 20 | 否 | ✓ |
| CLASSCODE | varchar(20) | 20 | 否 | ✓ |
| GROUPCODE | varchar(20) | 20 | 是 |  |
| ORDERNO | int(10,0) | - | 是 |  |
| MEMCODE1 | varchar(20) | 20 | 是 |  |
| MEMCODE2 | varchar(20) | 20 | 是 |  |
| PARENTCLASSCODE | varchar(20) | 20 | 是 |  |
| VISIBLE | char(1) | 1 | 是 |  |
| SYSFLAG | char(1) | 1 | 是 |  |
| CODEREADONLY | char(1) | 1 | 是 |  |
| COLORFLAG | char(1) | 1 | 是 |  |
| CANDELFLAG | char(1) | 1 | 是 |  |
| DEVIDEFLAG | char(1) | 1 | 是 |  |
| PUBLICFLAG | char(1) | 1 | 是 |  |

---

## SLAVE_P

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| SUBSYSCODE | varchar(20) | 20 | 否 | ✓ |
| CLASSCODE | varchar(20) | 20 | 否 | ✓ |
| GROUPCODE | varchar(20) | 20 | 是 |  |
| SLAVENO | varchar(64) | 64 | 否 | ✓ |
| SLAVENAME | varchar(2000) | 2000 | 是 |  |
| ENGNAME | varchar(100) | 100 | 是 |  |
| REMARK | varchar(-1) | -1 | 是 |  |
| EXTERNCODE | varchar(64) | 64 | 是 |  |
| SLAVENUM | varchar(20) | 20 | 是 |  |
| ORDERNO | int(10,0) | - | 是 |  |
| PARENTCLASSCODE | varchar(20) | 20 | 是 |  |
| PARENTCODE | varchar(20) | 20 | 是 |  |
| PARENTNAME | varchar(64) | 64 | 是 |  |
| MEMCODE1 | varchar(64) | 64 | 是 |  |
| MEMCODE2 | varchar(64) | 64 | 是 |  |
| RESERVEFIELD1 | varchar(64) | 64 | 是 |  |
| RESERVEFIELD2 | varchar(64) | 64 | 是 |  |
| RESERVEFIELD3 | varchar(64) | 64 | 是 |  |
| RESERVEFIELD4 | varchar(64) | 64 | 是 |  |
| RESERVEFIELD5 | varchar(64) | 64 | 是 |  |
| VISIBLE | char(1) | 1 | 是 |  |
| SYSFLAG | char(1) | 1 | 是 |  |
| TIMETEMP | timestamp | - | 否 |  |

---

## STATISTICSMENU

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| SERIALNO | int(10,0) | - | 否 | ✓ |
| HOSPITALCODE | varchar(50) | 50 | 否 |  |
| SUBSYSCODE | varchar(20) | 20 | 否 |  |
| MENUCLASS | varchar(100) | 100 | 否 |  |
| MENUCODE | varchar(100) | 100 | 否 |  |
| MENUNAME | varchar(100) | 100 | 否 |  |
| MENUTYPE | varchar(20) | 20 | 是 |  |
| MENUSRC | varchar(255) | 255 | 是 |  |
| PARENTMENUCODE | varchar(100) | 100 | 是 |  |
| ORDERNO | int(10,0) | - | 是 |  |
| PROCNAME | varchar(32) | 32 | 是 |  |
| GROUPFIELD | varchar(4000) | 4000 | 是 |  |
| STATISTICSFIELD | varchar(8000) | 8000 | 是 |  |
| SHOWCHART | int(10,0) | - | 是 |  |
| YTYSTATISTISC | int(10,0) | - | 是 |  |
| PARAMSMAP | varchar(8000) | 8000 | 是 |  |
| IMPORTID | int(10,0) | - | 是 |  |
| MERGECELLFIELD | varchar(200) | 200 | 是 |  |
| ROWMERGECELLFIELD | varchar(200) | 200 | 是 |  |
| TITLEMERGEVALUE | varchar(8000) | 8000 | 是 |  |
| ECHARTSPARAM | varchar(8000) | 8000 | 是 |  |
| EXPORTPARAM | varchar(8000) | 8000 | 是 |  |
| SHOWNO | varchar(10) | 10 | 是 |  |

---

## STATISTICSMENU_P

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| SERIALNO | int(10,0) | - | 否 | ✓ |
| SUBSYSCODE | varchar(20) | 20 | 否 |  |
| MENUCODE | varchar(100) | 100 | 否 |  |
| MENUNAME | varchar(100) | 100 | 否 |  |
| MENUTYPE | varchar(20) | 20 | 是 |  |
| MENUSRC | varchar(255) | 255 | 是 |  |
| PARENTMENUCODE | varchar(100) | 100 | 是 |  |
| ORDERNO | int(10,0) | - | 是 |  |
| PROCNAME | varchar(32) | 32 | 是 |  |
| GROUPFIELD | varchar(4000) | 4000 | 是 |  |
| STATISTICSFIELD | varchar(8000) | 8000 | 是 |  |
| SHOWCHART | int(10,0) | - | 是 |  |
| YTYSTATISTISC | int(10,0) | - | 是 |  |
| PARAMSMAP | varchar(8000) | 8000 | 是 |  |
| MERGECELLFIELD | varchar(200) | 200 | 是 |  |
| ROWMERGECELLFIELD | varchar(200) | 200 | 是 |  |
| TITLEMERGEVALUE | varchar(8000) | 8000 | 是 |  |
| ECHARTSPARAM | varchar(8000) | 8000 | 是 |  |
| EXPORTPARAM | varchar(8000) | 8000 | 是 |  |

---

## SubSysInfo

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| SubSysCode | varchar(20) | 20 | 否 | ✓ |
| SystemGroup | varchar(20) | 20 | 是 |  |
| SubSysName | varchar(100) | 100 | 是 |  |
| OrderNo | int(10,0) | - | 否 |  |
| VersionNo | varchar(20) | 20 | 是 |  |

---

## SYS_CLIENT_PRINTLIST

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| SERIALNO | numeric(18,0) | - | 否 | ✓ |
| MACADDRESS | varchar(64) | 64 | 否 |  |
| COMPUTERNAME | varchar(125) | 125 | 否 |  |
| IPADDRESS | varchar(50) | 50 | 是 |  |
| COMPUTERDESC | varchar(255) | 255 | 否 |  |
| COMPUTERGROUP | varchar(125) | 125 | 是 |  |
| ADDRESS | varchar(255) | 255 | 是 |  |
| PRINTERNAME | varchar(255) | 255 | 否 |  |
| DEFAULTFLAG | char(1) | 1 | 是 |  |

---

## SYS_CLIENTFILE_BACKUP

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| SERIALNO | numeric(18,0) | - | 否 | ✓ |
| HOSPITALCODE | varchar(50) | 50 | 否 |  |
| MACADDRESS | varchar(64) | 64 | 否 |  |
| FILETYPE | varchar(20) | 20 | 否 |  |
| FILENAME | varchar(64) | 64 | 否 |  |
| FILECONTENT | image(2147483647) | 2147483647 | 是 |  |
| UPDATETIME | datetime | - | 是 |  |
| FILEPATH | varchar(255) | 255 | 是 |  |

---

## SYS_CLIENTINTERFACE

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| SERIALNO | numeric(18,0) | - | 否 | ✓ |
| HOSPITALCODE | varchar(50) | 50 | 否 |  |
| MACADDRESS | varchar(64) | 64 | 否 |  |
| INTERFACE | varchar(64) | 64 | 是 |  |
| INTERFACENAME | varchar(125) | 125 | 是 |  |
| REQUESTDATE | datetime | - | 是 |  |
| REQUESTCNT | int(10,0) | - | 是 |  |
| REQSUCCCNT | int(10,0) | - | 是 |  |
| REQFAILCNT | int(10,0) | - | 是 |  |

---

## SYS_CLIENTINTERFACE_LOG

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| SERIALNO | numeric(18,0) | - | 否 | ✓ |
| HOSPITALCODE | varchar(50) | 50 | 否 |  |
| MACADDRESS | varchar(64) | 64 | 否 |  |
| INTERFACE | varchar(64) | 64 | 是 |  |
| INTERFACENAME | varchar(125) | 125 | 是 |  |
| CALLRESULT | char(1) | 1 | 是 |  |
| REQUESTTIME | datetime | - | 是 |  |
| REQUESTPARAM | text(2147483647) | 2147483647 | 是 |  |
| RESPONSEPARAM | text(2147483647) | 2147483647 | 是 |  |
| LOGCONTENT | text(2147483647) | 2147483647 | 是 |  |

---

## SYS_CLIENTUPDATE

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| SERIALNO | numeric(18,0) | - | 否 | ✓ |
| HOSPITALCODE | varchar(50) | 50 | 否 |  |
| MACADDRESS | varchar(64) | 64 | 否 |  |
| UPDATEPROGRAM | varchar(64) | 64 | 是 |  |
| TASKID | numeric(18,0) | - | 是 |  |
| UPDATETIME | datetime | - | 是 |  |
| UPDATEPROGRAMDIR | varchar(400) | 400 | 是 |  |

---

## SYS_CLIENTUPDATE_FILE

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| SERIALNO | numeric(18,0) | - | 否 | ✓ |
| HOSPITALCODE | varchar(50) | 50 | 否 |  |
| MACADDRESS | varchar(64) | 64 | 否 |  |
| TASKID | numeric(18,0) | - | 否 |  |
| FILESEQNO | numeric(18,0) | - | 否 |  |
| UPDATEFILE | varchar(125) | 125 | 是 |  |
| UPDATETIME | datetime | - | 是 |  |
| FILEPATH | varchar(255) | 255 | 是 |  |

---

## SYS_CLINICDESC

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| HOSPITALCODE | varchar(50) | 50 | 否 | ✓ |
| CLINICCODE | varchar(20) | 20 | 否 | ✓ |
| CLINICDESC | varchar(125) | 125 | 否 |  |
| ICDCODE | varchar(20) | 20 | 是 |  |
| EXTERNCODE | varchar(20) | 20 | 是 |  |
| MEMCODE1 | varchar(20) | 20 | 是 |  |
| MEMCODE2 | varchar(20) | 20 | 是 |  |
| REMARK | varchar(64) | 64 | 是 |  |

---

## SYS_CLINICDESC_P

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| CLINICCODE | varchar(20) | 20 | 否 | ✓ |
| CLINICDESC | varchar(125) | 125 | 否 |  |
| ICDCODE | varchar(20) | 20 | 是 |  |
| EXTERNCODE | varchar(20) | 20 | 是 |  |
| MEMCODE1 | varchar(20) | 20 | 是 |  |
| MEMCODE2 | varchar(20) | 20 | 是 |  |
| REMARK | varchar(64) | 64 | 是 |  |

---

## SYS_COLORMARK

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| SERIALNO | numeric(18,0) | - | 否 | ✓ |
| SUBSYSCODE | varchar(20) | 20 | 否 |  |
| DOMAIN | int(10,0) | - | 否 |  |
| MODELCODE | varchar(64) | 64 | 否 |  |
| MODELNAME | varchar(128) | 128 | 否 |  |
| MARK_SECTION | varchar(64) | 64 | 否 |  |
| MARK_GROUP | varchar(64) | 64 | 否 |  |
| MARK_GROUP_NAME | varchar(128) | 128 | 是 |  |
| MARK_ENTRY | varchar(64) | 64 | 否 |  |
| EXAMPLE_TEXT | varchar(128) | 128 | 是 |  |
| TEXT_BOLD | char(1) | 1 | 否 |  |
| TEXT_ITALIC | char(1) | 1 | 否 |  |
| TEXT_SIZE | int(10,0) | - | 否 |  |
| ICON_NAME | varchar(64) | 64 | 是 |  |
| TEXT_COLOR | varchar(20) | 20 | 是 |  |
| BACKGROUND_COLOR | varchar(20) | 20 | 是 |  |
| MARK_MEAN | varchar(255) | 255 | 是 |  |
| MARK_REMARK | varchar(255) | 255 | 是 |  |
| MARK_ORDERNO | varchar(64) | 64 | 是 |  |

---

## SYS_DEPT

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| DEPTID | int(10,0) | - | 否 | ✓ |
| HOSPITALCODE | varchar(50) | 50 | 否 |  |
| DEPTTYPE | varchar(20) | 20 | 否 |  |
| DEPTCODE | varchar(20) | 20 | 否 |  |
| DEPTNAME | varchar(100) | 100 | 是 |  |
| MEMCODE1 | varchar(64) | 64 | 是 |  |
| MEMCODE2 | varchar(64) | 64 | 是 |  |
| EXTERNCODE | varchar(64) | 64 | 是 |  |
| DEPTCLASS | varchar(20) | 20 | 是 |  |
| DEPTPHONE | varchar(32) | 32 | 是 |  |
| REMARK | varchar(255) | 255 | 是 |  |
| STOPFLAG | char(1) | 1 | 是 |  |
| STANDARDCODE | varchar(255) | 255 | 是 |  |
| STANDARDNAME | varchar(255) | 255 | 是 |  |
| AREACODE | varchar(20) | 20 | 是 |  |
| AREANAME | varchar(64) | 64 | 是 |  |
| IMPORT_VALUE | int(10,0) | - | 是 |  |
| TIMETEMP | timestamp | - | 否 |  |
| HRDEPTCODE | varchar(100) | 100 | 是 |  |
| HRDEPTNAME | varchar(200) | 200 | 是 |  |

---

## SYS_HISINTERFACE

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| SERIALNO | numeric(18,0) | - | 否 | ✓ |
| HOSPITALCODE | varchar(50) | 50 | 否 |  |
| CALLPCNAME | varchar(255) | 255 | 否 |  |
| PROCEDURENAME | varchar(64) | 64 | 是 |  |
| PROCEDUREDESC | varchar(125) | 125 | 是 |  |
| CALLRESULT | char(1) | 1 | 是 |  |
| REQUESTTIME | datetime | - | 是 |  |
| RESPONSETIME | datetime | - | 是 |  |
| DURATION | int(10,0) | - | 是 |  |
| REQUESTPARAM | text(2147483647) | 2147483647 | 是 |  |
| RESPONSEPARAM | text(2147483647) | 2147483647 | 是 |  |
| EXECSQLSCRIPT | varchar(2000) | 2000 | 是 |  |

---

## SYS_HOSPCLIENT

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| HOSPITALCODE | varchar(50) | 50 | 否 | ✓ |
| MACADDRESS | varchar(64) | 64 | 否 | ✓ |
| COMPUTERNAME | varchar(125) | 125 | 否 |  |
| IPADDRESS | varchar(50) | 50 | 是 |  |
| COMPUTERDESC | varchar(500) | 500 | 否 |  |
| CONFIGURATIONDESC | varchar(500) | 500 | 是 |  |
| COMPUTERGROUP | varchar(125) | 125 | 是 |  |
| ADDRESS | varchar(255) | 255 | 是 |  |
| LOCALSVR | char(1) | 1 | 是 |  |
| SVRSTATUS | char(1) | 1 | 是 |  |
| LOCALSVRTIME | datetime | - | 是 |  |
| LOCALSVRUPDATE | datetime | - | 是 |  |

---

## SYS_HOSPCLIENT_PRINTER

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| SERIALNO | numeric(18,0) | - | 否 | ✓ |
| HOSPITALCODE | varchar(50) | 50 | 否 |  |
| MACADDRESS | varchar(64) | 64 | 否 |  |
| PRINTERNAME | varchar(255) | 255 | 否 |  |
| PRINTERTYPE | varchar(20) | 20 | 是 |  |
| DEFAULTFLAG | char(1) | 1 | 是 |  |

---

## SYS_HOSPITALINFO

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| HOSPITALCODE | varchar(50) | 50 | 否 | ✓ |
| HOSPITALNAME | varchar(100) | 100 | 否 |  |
| HOSPSHORTNAME | varchar(50) | 50 | 是 |  |
| HOSPBARPRE | varchar(20) | 20 | 是 |  |
| HOSPITALURL | varchar(255) | 255 | 是 |  |
| PAGEURL | varchar(255) | 255 | 是 |  |
| PARENTCODE | varchar(20) | 20 | 是 |  |
| AREA | varchar(100) | 100 | 是 |  |
| HOSPITALLEVAL | varchar(20) | 20 | 是 |  |
| HOSPITALTYPE | varchar(20) | 20 | 是 |  |
| ORGCODE | varchar(50) | 50 | 是 |  |
| HOSPPHOTO | image(2147483647) | 2147483647 | 是 |  |
| HOSPLOGO | image(2147483647) | 2147483647 | 是 |  |
| HOSPPHONE | varchar(32) | 32 | 是 |  |
| MEMCODE1 | varchar(20) | 20 | 是 |  |
| MEMCODE2 | varchar(20) | 20 | 是 |  |
| REMARK | varchar(255) | 255 | 是 |  |
| STOPFLAG | char(1) | 1 | 是 |  |
| DELIVERYLEVEL | varchar(10) | 10 | 是 |  |
| STANDARDCODE | varchar(255) | 255 | 是 |  |
| STANDARDNAME | varchar(255) | 255 | 是 |  |
| ORDERNO | varchar(20) | 20 | 是 |  |

---

## SYS_INTERFACECONFIG

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| INTERFACEID | numeric(18,0) | - | 否 | ✓ |
| HOSPITALCODE | varchar(50) | 50 | 否 |  |
| INTERFACEGROUP | varchar(20) | 20 | 否 |  |
| SUBTYPE | varchar(255) | 255 | 否 |  |
| SUBTYPEDESC | varchar(1000) | 1000 | 是 |  |
| SUBTYPEFLAG | int(10,0) | - | 是 |  |
| INTERFACENAME | varchar(32) | 32 | 否 |  |
| CONNECTSTRING | varchar(255) | 255 | 否 |  |
| INTERFACETYPE | varchar(20) | 20 | 否 |  |
| INTERFACEPARAMETER | varchar(500) | 500 | 是 |  |
| REMARK | varchar(2000) | 2000 | 是 |  |
| STOPFLAG | char(1) | 1 | 是 |  |
| SYSTEMVERSION | varchar(20) | 20 | 是 |  |

---

## SYS_INTERFACECONFIG_P

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| INTERFACEID | numeric(18,0) | - | 否 |  |
| INTERFACEGROUP | varchar(20) | 20 | 否 |  |
| SUBTYPE | varchar(32) | 32 | 否 |  |
| INTERFACENAME | varchar(32) | 32 | 否 |  |
| INTERFACEURL | varchar(255) | 255 | 否 |  |
| INTERFACETYPE | varchar(20) | 20 | 否 |  |
| INTERFACEPARAMETER | varchar(500) | 500 | 否 |  |
| REMARK | varchar(255) | 255 | 是 |  |
| STOPFLAG | char(1) | 1 | 是 |  |

---

## SYS_LABEL

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| SERIALNO | numeric(18,0) | - | 否 |  |
| MODELCODE | varchar(50) | 50 | 否 |  |
| LABELVALUE | varchar(255) | 255 | 是 |  |

---

## SYS_LOG

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| SERIALNO | numeric(18,0) | - | 否 | ✓ |
| HOSPITALCODE | varchar(50) | 50 | 是 |  |
| OPERATEPCNAME | varchar(255) | 255 | 是 |  |
| OPERATEID | varchar(50) | 50 | 是 |  |
| OPERATENAME | varchar(64) | 64 | 是 |  |
| OPERATETIME | datetime | - | 是 |  |
| MODELCODE | varchar(50) | 50 | 否 |  |
| MODELNAME | varchar(100) | 100 | 是 |  |
| OPERATETYPE | varchar(20) | 20 | 是 |  |
| REMARK | varchar(255) | 255 | 是 |  |
| KEYVALUE_NEW | varchar(1000) | 1000 | 是 |  |
| KEYVALUE_OLD | varchar(1000) | 1000 | 是 |  |
| LOGCONTENT | text(2147483647) | 2147483647 | 是 |  |

---

## SYS_LOGCONFIG

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| MODELCODE | varchar(50) | 50 | 否 | ✓ |
| MODELNAME | varchar(64) | 64 | 是 |  |
| KEYCLOUMN | varchar(1000) | 1000 | 是 |  |
| KEYCLOUMN_OLD | varchar(1000) | 1000 | 是 |  |
| LABELCLOUM | varchar(1000) | 1000 | 是 |  |
| SCRIPT | varchar(-1) | -1 | 是 |  |
| WHERE_ADD | varchar(200) | 200 | 是 |  |
| WHERE_UPDATE | varchar(1000) | 1000 | 是 |  |
| WHERE_DELETE | varchar(1000) | 1000 | 是 |  |
| WHERE_PARAM | varchar(1000) | 1000 | 是 |  |
| CONTRASTMODE | char(1) | 1 | 是 |  |
| SPECIALCLOUM | varchar(1000) | 1000 | 是 |  |
| REMARK | varchar(1000) | 1000 | 是 |  |

---

## SYS_MENU

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| MENUID | int(10,0) | - | 否 | ✓ |
| SUBSYSCODE | varchar(20) | 20 | 否 |  |
| MENUCODE | varchar(50) | 50 | 否 |  |
| MENUNAME | varchar(100) | 100 | 否 |  |
| MENUURL | varchar(255) | 255 | 否 |  |
| PARAMS | varchar(255) | 255 | 是 |  |
| MAINMENUCODE | varchar(20) | 20 | 是 |  |
| MAINMENUNAME | varchar(64) | 64 | 是 |  |
| MAINMENUORDER | int(10,0) | - | 是 |  |
| MENUORDER | int(10,0) | - | 是 |  |
| ICONCLASS | varchar(64) | 64 | 是 |  |
| ICONURL | varchar(255) | 255 | 是 |  |
| REPORTID | int(10,0) | - | 是 |  |
| REMARK | varchar(255) | 255 | 是 |  |
| SRCMENUCODE | varchar(50) | 50 | 是 |  |
| MEMCODE1 | varchar(20) | 20 | 是 |  |
| MEMCODE2 | varchar(20) | 20 | 是 |  |
| EXTERNCODE | varchar(20) | 20 | 是 |  |
| VISIBLE | char(1) | 1 | 是 |  |
| STOPFLAG | char(1) | 1 | 是 |  |
| FRAMEFLAG | char(1) | 1 | 是 |  |

---

## SYS_MENU_LOG

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| SERIALNO | numeric(18,0) | - | 否 |  |
| HOSPITALCODE | varchar(50) | 50 | 是 |  |
| OPERATEPCNAME | varchar(255) | 255 | 是 |  |
| OPERATEID | varchar(50) | 50 | 是 |  |
| OPERATENAME | varchar(64) | 64 | 是 |  |
| OPERATETIME | datetime | - | 是 |  |
| MODELCODE | varchar(50) | 50 | 否 |  |
| MODELNAME | varchar(150) | 150 | 是 |  |
| MENUCODE | varchar(50) | 50 | 否 |  |
| MENUNAME | varchar(150) | 150 | 是 |  |
| OPERATETYPE | varchar(200) | 200 | 是 |  |
| OPERATETYPENAME | varchar(200) | 200 | 是 |  |
| REMARK | varchar(255) | 255 | 是 |  |
| LOGCONTENT | text(2147483647) | 2147483647 | 是 |  |
| THREADID | varchar(50) | 50 | 是 |  |

---

## SYS_MENU_P

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| SUBSYSCODE | varchar(20) | 20 | 否 | ✓ |
| MENUCODE | varchar(50) | 50 | 否 | ✓ |
| MENUNAME | varchar(100) | 100 | 否 |  |
| MENUURL | varchar(255) | 255 | 否 |  |
| PARAMS | varchar(255) | 255 | 是 |  |
| MAINMENUCODE | varchar(20) | 20 | 是 |  |
| MAINMENUNAME | varchar(64) | 64 | 是 |  |
| MAINMENUORDER | int(10,0) | - | 是 |  |
| MENUORDER | int(10,0) | - | 是 |  |
| ICONCLASS | varchar(64) | 64 | 是 |  |
| ICONURL | varchar(255) | 255 | 是 |  |
| REPORTID | int(10,0) | - | 是 |  |
| REMARK | varchar(255) | 255 | 是 |  |
| MEMCODE1 | varchar(20) | 20 | 是 |  |
| MEMCODE2 | varchar(20) | 20 | 是 |  |
| VISIBLE | char(1) | 1 | 是 |  |
| STOPFLAG | char(1) | 1 | 是 |  |
| FRAMEFLAG | char(1) | 1 | 是 |  |
| EXTERNCODE | varchar(20) | 20 | 是 |  |

---

## SYS_MENU_SQLLOG

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| SERIALNO | numeric(18,0) | - | 否 |  |
| HOSPITALCODE | varchar(50) | 50 | 是 |  |
| OPERATEPCNAME | varchar(255) | 255 | 是 |  |
| OPERATEID | varchar(50) | 50 | 是 |  |
| OPERATENAME | varchar(64) | 64 | 是 |  |
| OPERATETIME | datetime | - | 是 |  |
| SQL | text(2147483647) | 2147483647 | 是 |  |
| THREADID | varchar(50) | 50 | 是 |  |

---

## SYS_MENUPOWER_CHANGE

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| SERIALNO | numeric(18,0) | - | 否 | ✓ |
| SUBSYSCODE | varchar(20) | 20 | 否 |  |
| MENUCODE | varchar(50) | 50 | 否 |  |
| POWERCODE | varchar(50) | 50 | 否 |  |
| EXCUTEFLAG | int(10,0) | - | 是 |  |

---

## SYS_MENUPOWERS

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| MENUID | int(10,0) | - | 否 | ✓ |
| POWERCODE | varchar(50) | 50 | 否 | ✓ |
| POWERNAME | varchar(100) | 100 | 否 |  |
| PARENTCODE | varchar(50) | 50 | 是 |  |
| EDITOR | varchar(40) | 40 | 是 |  |
| CLASSCODE | varchar(64) | 64 | 是 |  |
| VALUE | varchar(250) | 250 | 是 |  |
| VALUEDESC | varchar(2000) | 2000 | 是 |  |
| ORDERNO | int(10,0) | - | 是 |  |
| REPORTRIGHTS | char(1) | 1 | 是 |  |
| REMARK | varchar(255) | 255 | 是 |  |
| VIEWHINT | char(1) | 1 | 是 |  |
| POWERGROUP | varchar(200) | 200 | 是 |  |

---

## SYS_MENUPOWERS_P

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| SUBSYSCODE | varchar(20) | 20 | 否 | ✓ |
| MENUCODE | varchar(50) | 50 | 否 | ✓ |
| POWERCODE | varchar(50) | 50 | 否 | ✓ |
| POWERNAME | varchar(100) | 100 | 否 |  |
| PARENTCODE | varchar(50) | 50 | 是 |  |
| EDITOR | varchar(40) | 40 | 是 |  |
| CLASSCODE | varchar(64) | 64 | 是 |  |
| VALUE | varchar(250) | 250 | 是 |  |
| VALUEDESC | varchar(2000) | 2000 | 是 |  |
| REPORTRIGHTS | char(1) | 1 | 是 |  |
| ORDERNO | int(10,0) | - | 是 |  |
| REMARK | varchar(255) | 255 | 是 |  |
| VIEWHINT | char(1) | 1 | 是 |  |
| POWERGROUP | varchar(200) | 200 | 是 |  |

---

## SYS_MODULE_MENU

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| ID | int(10,0) | - | 否 |  |
| HOSPITALCODE | varchar(50) | 50 | 否 | ✓ |
| SYSCODE | varchar(20) | 20 | 否 | ✓ |
| MAINMENUCODE | varchar(50) | 50 | 否 | ✓ |
| MENUCODE | varchar(50) | 50 | 否 | ✓ |
| MENUNAME | varchar(64) | 64 | 否 |  |
| DOMAIN | varchar(10) | 10 | 否 | ✓ |
| DOMAINNAME | varchar(255) | 255 | 否 |  |
| ORDERNO | int(10,0) | - | 否 |  |
| VISIBLE | char(1) | 1 | 否 |  |

---

## SYS_MODULE_MENU_P

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| ID | int(10,0) | - | 否 |  |
| SYSCODE | varchar(20) | 20 | 否 | ✓ |
| MAINMENUCODE | varchar(50) | 50 | 否 | ✓ |
| MENUCODE | varchar(50) | 50 | 否 | ✓ |
| MENUNAME | varchar(64) | 64 | 否 |  |
| ICON | varchar(64) | 64 | 是 |  |
| PATH | varchar(255) | 255 | 是 |  |
| ORDERNO | int(10,0) | - | 否 |  |
| VISIBLE | char(1) | 1 | 否 |  |

---

## SYS_MODULESHORTCUT

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| SERIALNO | int(10,0) | - | 否 | ✓ |
| DOMAIN | int(10,0) | - | 否 |  |
| DOMAINNAME | varchar(255) | 255 | 是 |  |
| HOSPITALCODE | varchar(50) | 50 | 是 |  |
| SUBSYSCODE | varchar(20) | 20 | 否 |  |
| MODULECODE | varchar(64) | 64 | 否 |  |
| MODULENAME | varchar(64) | 64 | 否 |  |
| MENUCODE | varchar(20) | 20 | 是 |  |
| MENUNAME | varchar(64) | 64 | 是 |  |
| REMARK | varchar(255) | 255 | 是 |  |
| ORDERNO | int(10,0) | - | 是 |  |

---

## SYS_MODULESHORTCUT_P

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| SERIALNO | int(10,0) | - | 否 | ✓ |
| SUBSYSCODE | varchar(20) | 20 | 否 |  |
| MODULECODE | varchar(64) | 64 | 否 |  |
| MODULENAME | varchar(64) | 64 | 否 |  |
| MENUCODE | varchar(64) | 64 | 是 |  |
| MENUNAME | varchar(64) | 64 | 是 |  |
| REMARK | varchar(255) | 255 | 是 |  |
| ORDERNO | int(10,0) | - | 是 |  |

---

## SYS_MSG_CENTER

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| BUSSINESSCODE | varchar(32) | 32 | 否 | ✓ |
| BUSSINESSNAME | varchar(64) | 64 | 否 |  |
| BUSSINESSTYPE | varchar(20) | 20 | 否 |  |
| DOMAINTYPE | varchar(20) | 20 | 是 |  |
| MEMCODE1 | varchar(20) | 20 | 是 |  |
| MEMCODE2 | varchar(20) | 20 | 是 |  |
| REMARK | varchar(255) | 255 | 是 |  |
| ORDERNO | int(10,0) | - | 是 |  |
| SYSFLAG | char(1) | 1 | 是 |  |
| STOPFLAG | char(1) | 1 | 是 |  |
| ONLINEFLAG | char(1) | 1 | 是 |  |
| REMINDFREQUENCY | int(10,0) | - | 是 |  |
| REMINDMODE | char(1) | 1 | 是 |  |
| LASTCALCDATE | datetime | - | 是 |  |
| CURTIMESTAMP | varchar(64) | 64 | 是 |  |
| VOICETEXT | varchar(255) | 255 | 是 |  |

---

## SYS_MSG_LOG

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| SERIALNO | int(10,0) | - | 否 | ✓ |
| HOSPITALCODE | varchar(50) | 50 | 否 |  |
| BUSSINESSCODE | varchar(32) | 32 | 否 |  |
| BUSSINESSID | varchar(64) | 64 | 否 |  |
| SUBBUSSINESSID | varchar(64) | 64 | 否 |  |
| OPERATE | varchar(64) | 64 | 是 |  |
| OPERATECODE | varchar(20) | 20 | 是 |  |
| OPERATENAME | varchar(64) | 64 | 是 |  |
| OPERATEPCNAME | varchar(255) | 255 | 是 |  |
| OPERATEDATE | datetime | - | 是 |  |
| REMARK | varchar(1000) | 1000 | 是 |  |
| EXPIREDATE | datetime | - | 是 |  |

---

## SYS_MSG_NOTICE

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| MESSAGE_ID | numeric(18,0) | - | 否 | ✓ |
| MESSAGE_TITLE | varchar(128) | 128 | 否 |  |
| MESSAGE_TYPE_CODE | varchar(20) | 20 | 否 |  |
| MESSAGE_TYPE_NAME | varchar(64) | 64 | 否 |  |
| MESSAGE_STATUS | int(10,0) | - | 否 |  |
| MESSAGE_CREATE_TIME | datetime | - | 是 |  |
| MESSAGE_CREATE_CODE | varchar(20) | 20 | 是 |  |
| MESSAGE_CREATE_NAME | varchar(64) | 64 | 是 |  |
| MESSAGE_CREATE_PC | varchar(255) | 255 | 是 |  |
| MESSAGE_PUB_TIME | datetime | - | 是 |  |
| MESSAGE_PUB_CODE | varchar(20) | 20 | 是 |  |
| MESSAGE_PUB_NAME | varchar(64) | 64 | 是 |  |
| MESSAGE_PUB_PC | varchar(255) | 255 | 是 |  |
| MESSAGE_EXPIRE_DATE | datetime | - | 是 |  |
| MESSAGE_CONTENT | text(2147483647) | 2147483647 | 是 |  |
| MESSAGE_STOPFLAG | char(1) | 1 | 是 |  |
| MESSAGE_GLOBAL_STATUS | char(1) | 1 | 是 |  |
| HOSPITALCODE | varchar(50) | 50 | 否 |  |

---

## SYS_MSG_NOTICE_USERS

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| SERIALNO | numeric(18,0) | - | 否 | ✓ |
| MESSAGE_ID | numeric(18,0) | - | 否 |  |
| MESSAGE_NOTICE_USERID | varchar(20) | 20 | 否 |  |
| MESSAGE_NOTICE_USERNAME | varchar(64) | 64 | 否 |  |
| MESSAGE_READ_TIMES | int(10,0) | - | 否 |  |
| MESSAGE_READ_TIME | datetime | - | 是 |  |
| MESSAGE_READ_PC | varchar(255) | 255 | 是 |  |
| MESSAGE_READ_LASTTIME | datetime | - | 是 |  |
| MESSAGE_READ_LASTPC | varchar(255) | 255 | 是 |  |

---

## SYS_MSG_PARAM

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| SERIALNO | numeric(18,0) | - | 否 | ✓ |
| BUSSINESSCODE | varchar(32) | 32 | 否 |  |
| DOMAIN | int(10,0) | - | 否 |  |
| PARAMCODE | varchar(64) | 64 | 否 |  |
| PARAMNAME | varchar(255) | 255 | 否 |  |
| PARENTCODE | varchar(64) | 64 | 是 |  |
| PARAMGROUP | varchar(64) | 64 | 否 |  |
| EDITOR | varchar(40) | 40 | 是 |  |
| MINVALUE | int(10,0) | - | 是 |  |
| MAXVALUE | int(10,0) | - | 是 |  |
| CLASSCODE | varchar(64) | 64 | 是 |  |
| VALUE | varchar(250) | 250 | 是 |  |
| VALUEDESC | varchar(2000) | 2000 | 是 |  |
| VISIBLE | char(1) | 1 | 否 |  |
| REMARK | varchar(250) | 250 | 是 |  |
| VIEWHINT | char(1) | 1 | 是 |  |
| ORDERNO | varchar(20) | 20 | 是 |  |

---

## SYS_MSG_SUB_PARAM

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| SERIALNO | numeric(18,0) | - | 否 | ✓ |
| HOSPITALCODE | varchar(50) | 50 | 否 |  |
| BUSSINESSCODE | varchar(32) | 32 | 否 |  |
| DOMAIN | int(10,0) | - | 否 |  |
| DOMAINNAME | varchar(255) | 255 | 否 |  |
| PARAMCODE | varchar(64) | 64 | 否 |  |
| PARAMNAME | varchar(255) | 255 | 否 |  |
| VALUE | varchar(250) | 250 | 是 |  |
| VALUEDESC | varchar(2000) | 2000 | 是 |  |

---

## SYS_MSG_USERCLIENT

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| SERIALNO | numeric(18,0) | - | 否 | ✓ |
| HOSPITALCODE | varchar(50) | 50 | 否 |  |
| BUSSINESSCODE | varchar(32) | 32 | 否 |  |
| DOMAIN | int(10,0) | - | 否 |  |
| DOMAINNAME | varchar(255) | 255 | 否 |  |
| EFFCONID | int(10,0) | - | 是 |  |
| STOPFLAG | char(1) | 1 | 是 |  |

---

## SYS_PROPERTIES

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| ID | int(10,0) | - | 否 |  |
| HOSPITALCODE | varchar(50) | 50 | 否 | ✓ |
| SERVICETYPE | varchar(64) | 64 | 否 | ✓ |
| GROUPCODE | varchar(128) | 128 | 否 | ✓ |
| GROUPNAME | varchar(128) | 128 | 否 |  |
| SERVICENAME | varchar(128) | 128 | 是 |  |
| UPDATEURL | varchar(255) | 255 | 否 |  |
| PKEY | varchar(64) | 64 | 否 | ✓ |
| VALUE | varchar(1000) | 1000 | 否 |  |
| VALUEDESC | varchar(2000) | 2000 | 是 |  |
| LABEL | varchar(64) | 64 | 否 |  |
| DESCRIPTION | varchar(128) | 128 | 是 |  |
| VALUETYPE | varchar(64) | 64 | 否 |  |
| DEFAULTVALUE | varchar(1000) | 1000 | 是 |  |
| VALUELIST | varchar(2000) | 2000 | 是 |  |
| PARENTKEY | varchar(800) | 800 | 是 |  |
| CLASSCODE | varchar(64) | 64 | 是 |  |
| ORDERNO | int(10,0) | - | 是 |  |
| FNLVALUE | varchar(1000) | 1000 | 是 |  |
| URLPREFIX | varchar(32) | 32 | 是 |  |
| URLSUFFIX | varchar(128) | 128 | 是 |  |
| VALIDFLAG | char(1) | 1 | 否 |  |

---

## SYS_REMIND_CALC_DETAIL

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| SERIALNO | numeric(18,0) | - | 否 | ✓ |
| HOSPITALCODE | varchar(50) | 50 | 否 |  |
| BUSSINESSCODE | varchar(32) | 32 | 否 |  |
| DOMAIN | int(10,0) | - | 否 |  |
| DOMAINNAME | varchar(255) | 255 | 否 |  |
| BUSSINESSID | varchar(64) | 64 | 否 |  |
| SUBBUSSINESSID | varchar(64) | 64 | 是 |  |
| SYSTYPE | varchar(20) | 20 | 是 |  |
| REMARK | varchar(255) | 255 | 是 |  |

---

## SYS_REMIND_CALC_LIST

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| SERIALNO | numeric(18,0) | - | 否 | ✓ |
| HOSPITALCODE | varchar(50) | 50 | 否 |  |
| BUSSINESSCODE | varchar(32) | 32 | 否 |  |
| DOMAIN | int(10,0) | - | 否 |  |
| DOMAINNAME | varchar(255) | 255 | 否 |  |
| CALCRESULT | int(10,0) | - | 否 |  |
| REMARK | varchar(255) | 255 | 是 |  |

---

## SYS_REPORTDICT

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| NODEID | int(10,0) | - | 否 | ✓ |
| HOSPITALCODE | varchar(50) | 50 | 否 |  |
| SUBSYSCODE | varchar(20) | 20 | 否 |  |
| REPORTCODE | varchar(64) | 64 | 是 |  |
| NAME | varchar(64) | 64 | 是 |  |
| SHORTNAME | varchar(64) | 64 | 是 |  |
| REPORTURL | varchar(255) | 255 | 是 |  |
| MEMCODE1 | varchar(20) | 20 | 是 |  |
| MEMCODE2 | varchar(20) | 20 | 是 |  |
| REMARK | varchar(1000) | 1000 | 是 |  |
| PARENTNODEID | int(10,0) | - | 否 |  |

---

## SYS_ROLE

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| ROLEID | int(10,0) | - | 否 | ✓ |
| HOSPITALCODE | varchar(50) | 50 | 否 |  |
| SUBSYSCODE | varchar(20) | 20 | 否 |  |
| ROLENAME | varchar(64) | 64 | 否 |  |
| REMARK | varchar(100) | 100 | 是 |  |
| STOPFLAG | char(1) | 1 | 是 |  |

---

## SYS_ROLE_DEFMENU

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| ROLEID | int(10,0) | - | 否 | ✓ |
| MENUID | int(10,0) | - | 否 | ✓ |

---

## SYS_ROLERIGHTS

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| SERIALNO | int(10,0) | - | 否 | ✓ |
| ROLEID | int(10,0) | - | 否 |  |
| MENUID | int(10,0) | - | 否 |  |
| POWERCODE | varchar(50) | 50 | 是 |  |
| VALUE | varchar(2000) | 2000 | 是 |  |
| VALUEDESC | varchar(2000) | 2000 | 是 |  |
| AUTORUN | char(1) | 1 | 是 |  |

---

## SYS_SETTINGS

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| SERIALNO | numeric(18,0) | - | 否 | ✓ |
| HOSPITALCODE | varchar(50) | 50 | 否 |  |
| SUBSYSCODE | varchar(20) | 20 | 否 |  |
| DOMAIN | int(10,0) | - | 否 |  |
| DOMAINNAME | varchar(255) | 255 | 否 |  |
| PARAMCODE | varchar(64) | 64 | 否 |  |
| PARAMNAME | varchar(255) | 255 | 否 |  |
| VALUE | varchar(250) | 250 | 是 |  |
| VALUEDESC | varchar(2000) | 2000 | 是 |  |

---

## SYS_SETTINGSDIC

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| SERIALNO | numeric(18,0) | - | 否 | ✓ |
| SUBSYSCODE | varchar(20) | 20 | 否 |  |
| DOMAIN | int(10,0) | - | 否 |  |
| SECTION | varchar(64) | 64 | 否 |  |
| PARAMCODE | varchar(64) | 64 | 否 |  |
| PARAMNAME | varchar(255) | 255 | 否 |  |
| PARENTCODE | varchar(64) | 64 | 是 |  |
| PARAMGROUP | varchar(64) | 64 | 否 |  |
| EDITOR | varchar(40) | 40 | 是 |  |
| MINVALUE | int(10,0) | - | 是 |  |
| MAXVALUE | int(10,0) | - | 是 |  |
| CLASSCODE | varchar(64) | 64 | 是 |  |
| VALUE | varchar(250) | 250 | 是 |  |
| VALUEDESC | varchar(2000) | 2000 | 是 |  |
| VISIBLE | char(1) | 1 | 否 |  |
| REMARK | varchar(250) | 250 | 是 |  |
| VIEWHINT | char(1) | 1 | 是 |  |
| ORDERNO | varchar(20) | 20 | 是 |  |

---

## SYS_SUBSYSINFO

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| SUBSYSCODE | varchar(20) | 20 | 否 | ✓ |
| SUBSYSNAME | varchar(100) | 100 | 否 |  |
| PARENTCODE | varchar(20) | 20 | 是 |  |
| ORDERNO | int(10,0) | - | 是 |  |
| VERSIONNO | varchar(64) | 64 | 是 |  |
| REMARK | varchar(255) | 255 | 是 |  |
| SUBSYSICON | image(2147483647) | 2147483647 | 是 |  |
| SUBSYSPHOTO | image(2147483647) | 2147483647 | 是 |  |
| MENUEPREFIX | varchar(50) | 50 | 是 |  |

---

## SYS_SYSTEMSERVICE

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| ID | int(10,0) | - | 否 | ✓ |
| SERVICETYPE | varchar(64) | 64 | 是 |  |
| SERVICENAME | varchar(64) | 64 | 否 |  |
| IP | varchar(20) | 20 | 是 |  |
| PORT | varchar(20) | 20 | 是 |  |
| CONTEXTPATH | varchar(20) | 20 | 是 |  |
| DESCRIPTION | varchar(2000) | 2000 | 是 |  |
| PATH | varchar(200) | 200 | 是 |  |

---

## SYS_TEMPLATE

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| SERIALNO | int(10,0) | - | 否 | ✓ |
| HOSPITALCODE | varchar(50) | 50 | 否 |  |
| SUBSYSCODE | varchar(20) | 20 | 否 |  |
| TEMPLATETYPE | varchar(20) | 20 | 否 |  |
| TEMPLATENAME | varchar(64) | 64 | 否 |  |
| FILENAME | varchar(64) | 64 | 否 |  |
| NROW | int(10,0) | - | 是 |  |
| NCOLUMN | int(10,0) | - | 是 |  |
| TEMPLATE | image(2147483647) | 2147483647 | 是 |  |
| PRINTERTYPE | varchar(20) | 20 | 是 |  |
| REMARK | varchar(255) | 255 | 是 |  |
| UPDATEBYCODE | varchar(20) | 20 | 否 |  |
| UPDATETIME | datetime | - | 否 |  |
| UPDATEYPCNAME | varchar(255) | 255 | 是 |  |
| TEMPATEPAGETYPE | varchar(64) | 64 | 是 |  |
| TEMPLATEPAGETYPE | varchar(64) | 64 | 是 |  |

---

## SYS_TEMPLATE_P

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| SERIALNO | int(10,0) | - | 否 | ✓ |
| SUBSYSCODE | varchar(20) | 20 | 否 |  |
| TEMPLATETYPE | varchar(20) | 20 | 否 |  |
| TEMPLATENAME | varchar(64) | 64 | 否 |  |
| FILENAME | varchar(64) | 64 | 否 |  |
| TEMPLATE | image(2147483647) | 2147483647 | 是 |  |
| PRINTERTYPE | varchar(20) | 20 | 是 |  |
| REMARK | varchar(255) | 255 | 是 |  |
| NROW | int(10,0) | - | 是 |  |
| NCOLUMN | int(10,0) | - | 是 |  |
| TEMPATEPAGETYPE | varchar(64) | 64 | 是 |  |
| TEMPLATEPAGETYPE | varchar(64) | 64 | 是 |  |

---

## SYS_UPDATEFILE

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| FILESEQNO | numeric(18,0) | - | 否 | ✓ |
| TASKID | numeric(18,0) | - | 否 |  |
| FILENAME | varchar(64) | 64 | 是 |  |
| FILEPATH | varchar(125) | 125 | 是 |  |
| UPDATEPROGRAM | varchar(64) | 64 | 是 |  |
| FILECONTENT | image(2147483647) | 2147483647 | 是 |  |
| FILEURL | varchar(255) | 255 | 是 |  |
| UPDATETIME | datetime | - | 是 |  |
| UPDATEUSERCODE | varchar(20) | 20 | 是 |  |
| UPDATEUSERNAME | varchar(64) | 64 | 是 |  |
| UPDATEPC | varchar(255) | 255 | 是 |  |
| REGSVRFLAG | char(1) | 1 | 是 |  |
| REGSVRTYPE | char(1) | 1 | 是 |  |

---

## SYS_UPDATEFILE_CLIENT

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| SERIALNO | numeric(18,0) | - | 否 | ✓ |
| TASKID | numeric(18,0) | - | 否 |  |
| DOMAIN | char(1) | 1 | 否 |  |
| DOMAINNAME | varchar(255) | 255 | 否 |  |

---

## SYS_UPDATETASK

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| TASKID | numeric(18,0) | - | 否 | ✓ |
| TASKNAME | varchar(64) | 64 | 是 |  |
| REMARK | varchar(255) | 255 | 是 |  |
| TASKTYPE | char(1) | 1 | 是 |  |
| UPDATETIME | datetime | - | 是 |  |
| STOPFLAG | char(1) | 1 | 是 |  |

---

## SYS_USER_SETTINGS

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| SERIALNO | numeric(18,0) | - | 否 | ✓ |
| HOSPITALCODE | varchar(50) | 50 | 否 |  |
| USERID | varchar(64) | 64 | 否 |  |
| PARAMCODE | varchar(64) | 64 | 否 |  |
| PARAMNAME | varchar(255) | 255 | 否 |  |
| VALUE | varchar(250) | 250 | 是 |  |
| VALUEDESC | varchar(2000) | 2000 | 是 |  |

---

## SYS_USER_SETTINGSDIC

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| SERIALNO | numeric(18,0) | - | 否 | ✓ |
| SUBSYSCODE | varchar(20) | 20 | 否 |  |
| PARAMCODE | varchar(64) | 64 | 否 |  |
| PARAMNAME | varchar(255) | 255 | 否 |  |
| PARENTCODE | varchar(64) | 64 | 是 |  |
| PARAMGROUP | varchar(64) | 64 | 否 |  |
| EDITOR | varchar(40) | 40 | 是 |  |
| MINVALUE | int(10,0) | - | 是 |  |
| MAXVALUE | int(10,0) | - | 是 |  |
| CLASSCODE | varchar(64) | 64 | 是 |  |
| VALUE | varchar(250) | 250 | 是 |  |
| VALUEDESC | varchar(2000) | 2000 | 是 |  |
| VISIBLE | char(1) | 1 | 否 |  |
| REMARK | varchar(250) | 250 | 是 |  |
| VIEWHINT | char(1) | 1 | 是 |  |
| ORDERNO | varchar(20) | 20 | 是 |  |

---

## SYS_USERDEPT

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| SERIALNO | numeric(18,0) | - | 否 | ✓ |
| HOSPITALCODE | varchar(50) | 50 | 否 |  |
| USERID | int(10,0) | - | 否 |  |
| DEPTID | int(10,0) | - | 否 |  |
| STANDARDCODE | varchar(255) | 255 | 是 |  |
| STANDARDNAME | varchar(255) | 255 | 是 |  |
| DEPTTYPE | varchar(20) | 20 | 是 |  |

---

## SYS_USERMENU_COMMON

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| SERIALNO | int(10,0) | - | 否 | ✓ |
| USERID | int(10,0) | - | 否 |  |
| MENUID | int(10,0) | - | 否 |  |
| ORDERNO | int(10,0) | - | 否 |  |
| AUTORUN | char(1) | 1 | 是 |  |

---

## SYS_USERREPORT_COMMON

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| SERIALNO | int(10,0) | - | 否 | ✓ |
| USERID | int(10,0) | - | 否 |  |
| NODEID | int(10,0) | - | 否 |  |
| REPORTCODE | varchar(64) | 64 | 是 |  |
| ORDERNO | int(10,0) | - | 否 |  |
| REPORTTYPE | varchar(20) | 20 | 是 |  |

---

## SYS_USERROLE

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| SERIALNO | int(10,0) | - | 否 | ✓ |
| USERID | int(10,0) | - | 否 |  |
| ROLEID | int(10,0) | - | 否 |  |
| INSTID | int(10,0) | - | 是 |  |

---

## SYS_USERS

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| USERID | int(10,0) | - | 否 | ✓ |
| HOSPITALCODE | varchar(50) | 50 | 否 |  |
| LOGCODE | varchar(20) | 20 | 否 |  |
| USERCODE | varchar(20) | 20 | 否 |  |
| USERNAME | varchar(64) | 64 | 否 |  |
| EXTERNCODE | varchar(64) | 64 | 是 |  |
| PASSWORD | varchar(64) | 64 | 是 |  |
| VERIFYPASSWORD | varchar(64) | 64 | 是 |  |
| DEPTID | int(10,0) | - | 是 |  |
| PROTITLECODE | varchar(64) | 64 | 是 |  |
| PROTITLENAME | varchar(64) | 64 | 是 |  |
| MOBILEPHONE | varchar(32) | 32 | 是 |  |
| IDNUM | varchar(20) | 20 | 是 |  |
| IME | varchar(50) | 50 | 是 |  |
| EXPIREDATE | datetime | - | 是 |  |
| USERSIGNATURE | image(2147483647) | 2147483647 | 是 |  |
| USERPHOTO2 | image(2147483647) | 2147483647 | 是 |  |
| USERPHOTO2URL | varchar(255) | 255 | 是 |  |
| USERPHOTO | image(2147483647) | 2147483647 | 是 |  |
| USERPHOTOURL | varchar(255) | 255 | 是 |  |
| MEMCODE1 | varchar(64) | 64 | 是 |  |
| MEMCODE2 | varchar(64) | 64 | 是 |  |
| LOGINTIMES | datetime | - | 是 |  |
| PASSWORDUPDATE | datetime | - | 是 |  |
| EXPIRELOGINTIMES | int(10,0) | - | 是 |  |
| ILLEGALLOGINTIMES | int(10,0) | - | 是 |  |
| ILLEGALLOGINDATE | datetime | - | 是 |  |
| ILLEGALLOCKTIME | datetime | - | 是 |  |
| USERTYPE | varchar(20) | 20 | 是 |  |
| SIGNATURECODE | varchar(100) | 100 | 是 |  |
| SIGNCAFLAG | char(1) | 1 | 是 |  |
| LOGOUTTIMES | datetime | - | 是 |  |
| MANAGEWARD | char(1) | 1 | 是 |  |
| MANAGEDEPT | char(1) | 1 | 是 |  |
| STOPFLAG | char(1) | 1 | 是 |  |
| QRCODE | varchar(255) | 255 | 是 |  |
| STANDARDCODE | varchar(255) | 255 | 是 |  |
| STANDARDNAME | varchar(255) | 255 | 是 |  |
| REMARK | varchar(255) | 255 | 是 |  |
| IMPORT_VALUE | int(10,0) | - | 是 |  |
| YSZGZJ | varchar(100) | 100 | 是 |  |
| CASIGNATUREFLAG | char(1) | 1 | 是 |  |
| QUALIFICATION | varchar(255) | 255 | 是 |  |

---

## SYS_VERSION

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| SYSTEMNAME | varchar(512) | 512 | 否 |  |
| RELEASETIME | datetime | - | 否 |  |
| INSERTTIME | datetime | - | 否 |  |

---

## TECHNO_RULE

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| TECHNO_RULE_ID | numeric(19,0) | - | 否 | ✓ |
| TECHNO_RULE_NAME | nvarchar(64) | 64 | 是 |  |
| CURRENT_NUMBER | int(10,0) | - | 是 |  |
| PRE_PART | nvarchar(32) | 32 | 是 |  |
| CREATE_MODE_CODE | numeric(19,0) | - | 是 |  |
| CREATE_CYCLE_CODE | numeric(19,0) | - | 是 |  |
| TOTAL_LENGTH | int(10,0) | - | 是 |  |
| ZERO_FILL_LENGTH | int(10,0) | - | 是 |  |
| RECYCLE_FLAG | numeric(19,0) | - | 是 |  |
| SYSTEM_SOURCE_NO | nvarchar(32) | 32 | 否 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |

---

## TECHNOLOGY_OTHER_INFO

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| TECHNOLOGY_OTHER_INFO_ID | numeric(19,0) | - | 否 | ✓ |
| EXAM_TECHNOLOGY_ID | numeric(19,0) | - | 否 |  |
| SYSTEM_SOURCE_NO | nvarchar(32) | 32 | 是 |  |
| TECHNOLOGY_ITEM_NO | nvarchar(32) | 32 | 是 |  |
| TECHNOLOGY_ITEM_NAME | nvarchar(64) | 64 | 是 |  |
| TECHNOLOGY_ITEM_RESULT | nvarchar(1024) | 1024 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |

---

## USER_INFO

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| USER_ID | numeric(19,0) | - | 否 | ✓ |
| EMPLOYEE_ID | numeric(19,0) | - | 否 |  |
| USER_CODE | varchar(64) | 64 | 否 |  |
| ACTIVATED_AT | datetime | - | 是 |  |
| DEACTIVATED_AT | datetime | - | 是 |  |
| USER_STATUS | numeric(19,0) | - | 否 |  |
| LOCK_FLAG | numeric(19,0) | - | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| MODIFIED_AT | datetime | - | 是 |  |

---

## USER_TABOO_ITEMS

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| USER_TABOO_ITEMS_ID | numeric(19,0) | - | 否 | ✓ |
| EXAM_ITEM_DIC_ID | numeric(19,0) | - | 否 |  |
| USER_ID | numeric(19,0) | - | 否 |  |
| SYSTEM_SOURCE_NO | nvarchar(32) | 32 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |

---

## Users

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| UserID | varchar(30) | 30 | 否 | ✓ |
| UserName | varchar(20) | 20 | 是 |  |
| Password | varchar(20) | 20 | 是 |  |
| DeptNo | varchar(12) | 12 | 是 |  |
| UserIME | varchar(50) | 50 | 是 |  |
| MemCode1 | varchar(30) | 30 | 是 |  |
| MemCode2 | varchar(30) | 30 | 是 |  |
| ExternCode | varchar(20) | 20 | 是 |  |
| UserType | varchar(1000) | 1000 | 是 |  |
| StopUse | char(1) | 1 | 是 |  |
| Phone | varchar(20) | 20 | 是 |  |
| Address | varchar(80) | 80 | 是 |  |
| AddressPhone | varchar(20) | 20 | 是 |  |
| eMail | varchar(50) | 50 | 是 |  |
| SignImage | image(2147483647) | 2147483647 | 是 |  |
| SignFlag | varchar(2) | 2 | 是 |  |
| ExpireLoginTimes | int(10,0) | - | 是 |  |
| IllegalLoginTimes | int(10,0) | - | 是 |  |
| IllegalLockTime | datetime | - | 是 |  |
| IDCard | varchar(32) | 32 | 是 |  |
| UserPhoto | image(2147483647) | 2147483647 | 是 |  |
| UserTitle | varchar(20) | 20 | 是 |  |
| HOSPITALCODE | varchar(50) | 50 | 否 |  |
| LOGNO | varchar(50) | 50 | 否 |  |
| OtherFlag | int(10,0) | - | 是 |  |
| Expire | datetime | - | 是 |  |
| VerifyPassword | varchar(20) | 20 | 是 |  |

---

## Version

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| SystemName | varchar(512) | 512 | 否 |  |
| ReleaseTime | datetime | - | 否 |  |
| InsertTime | datetime | - | 否 |  |
| HOSPITALCODE | varchar(64) | 64 | 否 |  |
| HospitalGrade | int(10,0) | - | 是 |  |

---

## WINNING_BACKUPOBJECT

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| OBJECTNO | int(10,0) | - | 否 |  |
| UPDATENO | int(10,0) | - | 否 |  |
| SUBSYSNAME | varchar(100) | 100 | 否 | ✓ |
| REQUIRENAME | varchar(500) | 500 | 是 |  |
| OBJECTNAME | varchar(255) | 255 | 否 | ✓ |
| OBJECTPATH | varchar(300) | 300 | 否 | ✓ |
| OBJECTCONTENT | image(2147483647) | 2147483647 | 是 |  |
| OBJECTTYPE | varchar(500) | 500 | 是 |  |
| DATABASENAME | varchar(200) | 200 | 是 |  |
| RECODETIME | datetime | - | 是 |  |
| OBJECTGROUP | varchar(500) | 500 | 否 | ✓ |
| REGISTFLAG | int(10,0) | - | 是 |  |

---

## WINNING_COMMONSCRIPTINFO

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| UPDATENO | int(10,0) | - | 是 |  |
| SUBSYSNAME | varchar(100) | 100 | 是 |  |
| REQUIRENAME | varchar(500) | 500 | 是 |  |
| OBJECTNAME | varchar(255) | 255 | 是 |  |
| OBJECTTYPE | varchar(500) | 500 | 是 |  |
| DATABASENAME | varchar(200) | 200 | 是 |  |
| RECODETIME | datetime | - | 是 |  |
| MAXCHANGENO | int(10,0) | - | 是 |  |
| UPDATENAME | varchar(500) | 500 | 是 |  |
| FLAG | int(10,0) | - | 是 |  |
| PACKNAME | varchar(500) | 500 | 是 |  |

---

## WINNING_DATABASECONNECT

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| ID | int(10,0) | - | 否 |  |
| DATABASENAME | varchar(300) | 300 | 否 |  |
| CONNECTSTRING | varchar(500) | 500 | 是 |  |
| CONNECTSTRINGMEDICAL | varchar(500) | 500 | 是 |  |
| HOSPITALFLAG | int(10,0) | - | 否 |  |
| NOTPUBFLAG | int(10,0) | - | 否 |  |
| NOTMEDICALFLAG | int(10,0) | - | 否 |  |
| DELETEFLAG | int(10,0) | - | 否 |  |
| LINUXFLAG | int(10,0) | - | 是 |  |
| CONNECTSTRINGPOPULATION | varchar(500) | 500 | 是 |  |
| NOTPOPULATIONFLAG | int(10,0) | - | 否 |  |

---

## WINNING_DEMANDDATABASEINFO

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| REQUIRENAME | varchar(500) | 500 | 是 |  |
| DATABASENAME | varchar(2000) | 2000 | 是 |  |
| RECODETIME | datetime | - | 是 |  |

---

## WINNING_DOWNLOADRECORD

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| ID | int(10,0) | - | 否 |  |
| SUBSYSNAME | varchar(100) | 100 | 否 | ✓ |
| UPDATENO | int(10,0) | - | 是 |  |
| FILEID | int(10,0) | - | 否 | ✓ |
| CLIENTPATH | varchar(500) | 500 | 否 | ✓ |
| HOSPITALCODE | varchar(1000) | 1000 | 是 |  |
| DOWNLOADTIME | datetime | - | 是 |  |
| FILEUPDATETIME | datetime | - | 是 |  |
| FLAG | varchar(200) | 200 | 是 |  |

---

## WINNING_EXECSCRIPTTIME

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| ID | int(10,0) | - | 否 |  |
| SCRIPTTXT | text(2147483647) | 2147483647 | 是 |  |
| STARTTIME | datetime | - | 是 |  |
| STOPTIME | datetime | - | 是 |  |
| USERTIME | varchar(100) | 100 | 是 |  |
| DATABASENAME | varchar(200) | 200 | 是 |  |
| DEMANDID | varchar(300) | 300 | 是 |  |
| EXECRESULT | text(2147483647) | 2147483647 | 是 |  |
| OBJECTNAME | varchar(1000) | 1000 | 是 |  |
| EXECSTARTFLAG | varchar(1000) | 1000 | 是 |  |

---

## WINNING_FILEINFO

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| FILENAME | varchar(200) | 200 | 是 |  |
| FILECONTENT | text(2147483647) | 2147483647 | 是 |  |
| SUBSYSCODE | varchar(200) | 200 | 是 |  |
| DATABASENAME | varchar(200) | 200 | 是 |  |
| DATABASETYPE | varchar(100) | 100 | 是 |  |
| UPDATETIME | datetime | - | 是 |  |

---

## WINNING_TOOLVERSIONINFO

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| TOOLVERSION | varchar(500) | 500 | 是 |  |
| UPDATETIME | datetime | - | 是 |  |
| HOSTNAME | varchar(500) | 500 | 是 |  |
| TOOLPATH | varchar(500) | 500 | 是 |  |

---

## WINNING_UPDATEDETAIL

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| UPDATENO | int(10,0) | - | 否 | ✓ |
| SUBSYSNAME | varchar(100) | 100 | 否 | ✓ |
| REQUIRENAME | varchar(500) | 500 | 是 |  |
| OBJECTNAME | varchar(255) | 255 | 否 | ✓ |
| OBJECTPATH | varchar(300) | 300 | 否 | ✓ |
| OBJECTTYPE | varchar(500) | 500 | 是 |  |
| DATABASENAME | varchar(200) | 200 | 是 |  |
| OBJECTGROUP | varchar(500) | 500 | 否 | ✓ |
| RECODETIME | datetime | - | 是 |  |

---

## WINNING_UPDATEFILEINFO

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| OBJECTNO | int(10,0) | - | 否 |  |
| UPDATENO | int(10,0) | - | 否 |  |
| SUBSYSNAME | varchar(100) | 100 | 否 | ✓ |
| REQUIRENAME | varchar(500) | 500 | 是 |  |
| OBJECTNAME | varchar(255) | 255 | 否 | ✓ |
| OBJECTPATH | varchar(300) | 300 | 否 | ✓ |
| OBJECTCONTENT | image(2147483647) | 2147483647 | 是 |  |
| OBJECTTYPE | varchar(500) | 500 | 是 |  |
| DATABASENAME | varchar(200) | 200 | 是 |  |
| RECODETIME | datetime | - | 是 |  |
| OBJECTGROUP | varchar(500) | 500 | 否 | ✓ |
| REGISTFLAG | int(10,0) | - | 是 |  |
| LASTUPDATENO | int(10,0) | - | 否 |  |

---

## WINNING_UPDATERECODE

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| UPDATENO | int(10,0) | - | 否 | ✓ |
| SUBSYSNAME | varchar(300) | 300 | 否 |  |
| UPDATENAME | varchar(300) | 300 | 否 |  |
| REQUIRENAME | varchar(500) | 500 | 是 |  |
| MAXCHANGENO | int(10,0) | - | 否 |  |
| LARGEVERSIONDEMANID | int(10,0) | - | 是 |  |
| STATUS | varchar(20) | 20 | 是 |  |
| MODEL | varchar(500) | 500 | 是 |  |
| UPDATETIME | datetime | - | 是 |  |
| ROLLBACKTIME | datetime | - | 是 |  |
| DESCRIPT | varchar(2000) | 2000 | 是 |  |

---

## WORKLIST_ENCOUNTER_TYPE

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| WORKLIST_ENCOUNTER_TYPE_ID | numeric(19,0) | - | 否 | ✓ |
| WORKLIST_RULE_ID | numeric(19,0) | - | 否 |  |
| ENCOUNTER_TYPE_NO | nvarchar(32) | 32 | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |

---

## WORKLIST_EQUIPMENT

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| WORKLIST_EQUIPMENT_ID | numeric(19,0) | - | 否 | ✓ |
| WORKLIST_RULE_ID | numeric(19,0) | - | 否 |  |
| EXAM_EQUIPMENT_ID | numeric(19,0) | - | 否 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |

---

## WORKLIST_EXAM_CATEGORY

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| WORKLIST_EXAM_CATEGORY_ID | numeric(19,0) | - | 否 | ✓ |
| EXAM_CATEGORY_ID | numeric(19,0) | - | 否 |  |
| WORKLIST_RULE_ID | numeric(19,0) | - | 否 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |

---

## WORKLIST_RULE

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| WORKLIST_RULE_ID | numeric(19,0) | - | 否 | ✓ |
| SYSTEM_SOURCE_NO | nvarchar(32) | 32 | 是 |  |
| EQP_AE_TITLE | nvarchar(20) | 20 | 是 |  |
| RULE_CONDITION | nvarchar(-1) | -1 | 是 |  |
| CHARACTERSET_CODE | numeric(19,0) | - | 是 |  |
| SEND_ITEM_MODE_CODE | numeric(19,0) | - | 是 |  |
| EXAM_TASK_STATUS | numeric(19,0) | - | 是 |  |
| HAVING_IMAGE_FLAG | numeric(19,0) | - | 是 |  |
| SPLIT_NAME_FLAG | numeric(19,0) | - | 是 |  |
| SPLIT_POSITION_FLAG | numeric(19,0) | - | 是 |  |
| SEND_NAME_PINYIN_FLAG | numeric(19,0) | - | 是 |  |
| SEND_INSTANCE_UID_FLAG | numeric(19,0) | - | 是 |  |
| FILTER_MODALITY_FLAG | numeric(19,0) | - | 是 |  |
| EXAM_TASK_STATUS_NOS | varchar(64) | 64 | 是 |  |
| TRIAGE_TASK_STATUS_NOS | varchar(64) | 64 | 是 |  |
| WORKLIST_CONDITION | nvarchar(-1) | -1 | 是 |  |
| VISIBLE_FLAG | numeric(19,0) | - | 是 |  |
| SEQ_NO | int(10,0) | - | 是 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(128) | 128 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 是 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |

---

## WORKLIST_TRIAGE_QUEUE

| 字段名 | 类型 | 长度 | 可空 | 主键 |
|--------|------|------|------|------|
| WORKLIST_TRIAGE_QUEUE_ID | numeric(19,0) | - | 否 | ✓ |
| EXAM_TRIAGE_QUEUE_ID | numeric(19,0) | - | 否 |  |
| WORKLIST_RULE_ID | numeric(19,0) | - | 否 |  |
| HOSPITAL_SOID | numeric(19,0) | - | 否 |  |
| HOSPITAL_NAME | nvarchar(64) | 64 | 是 |  |
| IS_DEL | smallint(5,0) | - | 否 |  |
| CREATED_AT | datetime | - | 否 |  |
| CREATED_BY | numeric(19,0) | - | 否 |  |
| CREATED_NAME | nvarchar(64) | 64 | 是 |  |
| MODIFIED_AT | datetime | - | 是 |  |
| MODIFIED_BY | numeric(19,0) | - | 是 |  |
| MODIFIED_NAME | nvarchar(64) | 64 | 是 |  |

---

