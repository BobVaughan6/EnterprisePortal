# 海隆咨询官网数据库文档

## 📋 文档概述

本目录包含海隆咨询官网的完整数据库设计文档和SQL脚本。

---

## 📁 文件列表

| 文件名 | 说明 | 用途 |
|--------|------|------|
| [`hailong_consulting_schema.sql`](hailong_consulting_schema.sql) | 数据库结构脚本 | 创建数据库、表、索引、视图 |
| [`hailong_consulting_init_data.sql`](hailong_consulting_init_data.sql) | 初始化数据脚本 | 插入初始数据（管理员、区域、示例数据） |
| [`数据字典.md`](数据字典.md) | 数据字典文档 | 详细的表结构、字段说明 |
| [`ER关系图说明.md`](ER关系图说明.md) | ER关系图说明 | 实体关系图和数据库架构说明 |
| `README.md` | 本文件 | 使用说明和快速开始指南 |

---

## 🚀 快速开始

### 1. 环境要求

- MySQL 8.0 或更高版本
- 数据库管理工具（推荐：MySQL Workbench、Navicat、DBeaver）
- 或命令行工具（mysql client）

### 2. 创建数据库

#### 方法一：使用命令行

```bash
# 1. 登录MySQL
mysql -u root -p

# 2. 执行建表脚本
source /path/to/hailong_consulting_schema.sql

# 3. 执行初始化数据脚本
source /path/to/hailong_consulting_init_data.sql

# 4. 验证安装
USE hailong_consulting;
SHOW TABLES;
SELECT * FROM v_homepage_statistics;
```

#### 方法二：使用MySQL Workbench

1. 打开 MySQL Workbench
2. 连接到数据库服务器
3. 点击 `File` → `Open SQL Script`
4. 选择 `hailong_consulting_schema.sql` 并执行
5. 选择 `hailong_consulting_init_data.sql` 并执行
6. 刷新左侧数据库列表，查看新创建的数据库

#### 方法三：使用Navicat

1. 打开 Navicat
2. 连接到数据库服务器
3. 右键点击连接 → `执行SQL文件`
4. 选择 `hailong_consulting_schema.sql` 并执行
5. 选择 `hailong_consulting_init_data.sql` 并执行

### 3. 验证安装

执行以下SQL验证数据库是否正确创建：

```sql
-- 切换到数据库
USE hailong_consulting;

-- 查看所有表
SHOW TABLES;

-- 查看管理员账号
SELECT id, username, real_name, email FROM admin_users WHERE is_deleted = 0;

-- 查看角色信息
SELECT id, role_name, role_code FROM admin_roles WHERE is_deleted = 0;

-- 查看首页统计数据
SELECT * FROM v_homepage_statistics;

-- 查看区域字典
SELECT region_code, region_name, region_type FROM region_dictionary WHERE is_deleted = 0;
```

---

## 👤 默认管理员账号

| 用户名 | 密码 | 角色 | 说明 |
|--------|------|------|------|
| admin | 123456 | 超级管理员 | 拥有所有权限 |
| editor | 123456 | 内容管理员 | 负责内容发布 |

**⚠️ 安全提示**: 
- 首次登录后请立即修改默认密码
- 密码使用 SHA2(password, 256) 加密存储
- 生产环境请使用更强的密码策略

---

## 📊 数据库结构概览

### 模块划分

```
hailong_consulting (数据库)
├── 用户权限管理模块 (2张表)
│   ├── admin_users (管理员账号表)
│   └── admin_roles (角色权限表)
│
├── 公告信息模块 (2张表)
│   ├── government_procurement_notices (政府采购公告表)
│   └── construction_project_notices (建设工程公告表)
│
├── 信息发布模块 (4张表)
│   ├── company_announcements (公司公告表)
│   ├── policy_regulations (政策法规表)
│   ├── policy_information (政策信息表)
│   └── notice_announcements (通知公告表)
│
├── 系统配置模块 (6张表)
│   ├── carousel_banners (轮播图表)
│   ├── company_profile (企业简介表)
│   ├── business_scope (业务范围表)
│   ├── company_honors (企业荣誉表)
│   ├── major_achievements (重要业绩表)
│   └── friendly_links (友情链接表)
│
├── 统计模块 (2张表)
│   ├── visit_statistics (访问统计表)
│   └── transaction_data (交易数据表)
│
├── 辅助模块 (2张表)
│   ├── region_dictionary (区域字典表)
│   └── system_logs (系统日志表)
│
└── 视图 (3个)
    ├── v_homepage_statistics (首页统计视图)
    ├── v_transaction_type_statistics (交易类型统计视图)
    └── v_region_ranking_statistics (地区排行统计视图)
```

### 统计信息

- **总表数**: 18张
- **总视图数**: 3个
- **外键约束**: 1个
- **字符集**: utf8mb4
- **存储引擎**: InnoDB

---

## 🔑 核心功能说明

### 1. 用户权限管理

- 基于角色的权限控制（RBAC）
- 支持多角色管理
- 记录登录日志和操作日志

### 2. 公告信息管理

- 政府采购公告（采购公告、更正公告、结果公告）
- 建设工程公告（招标公告、中标公告、变更公告）
- 支持富文本内容
- 支持全文搜索
- 访问量统计

### 3. 信息发布

- 公司公告
- 政策法规
- 政策信息
- 通知公告
- 统一的内容管理

### 4. 系统配置

- 轮播图管理
- 企业简介
- 业务范围展示
- 企业荣誉展示
- 重要业绩展示
- 友情链接管理

### 5. 数据统计

- 访问统计分析
- 交易数据统计
- 首页可视化数据
- 地区排行统计
- 交易类型占比

---

## 📖 常用SQL示例

### 查询最新公告

```sql
-- 查询最新的政府采购公告（前10条）
SELECT id, title, notice_type, publish_time, view_count
FROM government_procurement_notices
WHERE is_deleted = 0
ORDER BY is_top DESC, publish_time DESC
LIMIT 10;
```

### 查询首页统计数据

```sql
-- 使用视图查询首页统计
SELECT * FROM v_homepage_statistics;

-- 查询交易类型占比
SELECT * FROM v_transaction_type_statistics;

-- 查询地区排行TOP5
SELECT * FROM v_region_ranking_statistics LIMIT 5;
```

### 全文搜索

```sql
-- 搜索包含"医疗设备"的公告
SELECT id, title, publish_time
FROM government_procurement_notices
WHERE MATCH(title, content) AGAINST('医疗设备' IN NATURAL LANGUAGE MODE)
  AND is_deleted = 0
ORDER BY publish_time DESC;
```

### 统计分析

```sql
-- 按月统计项目数量和金额
SELECT 
  DATE_FORMAT(transaction_date, '%Y-%m') AS month,
  COUNT(*) AS project_count,
  SUM(project_amount) AS total_amount
FROM transaction_data
WHERE is_deleted = 0
GROUP BY month
ORDER BY month DESC;

-- 按地区统计项目分布
SELECT 
  project_region,
  COUNT(*) AS count,
  SUM(project_amount) AS total_amount
FROM transaction_data
WHERE is_deleted = 0
GROUP BY project_region
ORDER BY total_amount DESC;
```

---

## 🔧 维护操作

### 备份数据库

```bash
# 完整备份
mysqldump -u root -p hailong_consulting > backup_$(date +%Y%m%d_%H%M%S).sql

# 仅备份结构
mysqldump -u root -p --no-data hailong_consulting > schema_backup.sql

# 仅备份数据
mysqldump -u root -p --no-create-info hailong_consulting > data_backup.sql
```

### 恢复数据库

```bash
# 恢复完整数据库
mysql -u root -p hailong_consulting < backup_20231203_100000.sql

# 恢复到新数据库
mysql -u root -p -e "CREATE DATABASE hailong_consulting_new;"
mysql -u root -p hailong_consulting_new < backup_20231203_100000.sql
```

### 清理软删除数据

```sql
-- 查看软删除数据量
SELECT 
  'admin_users' AS table_name, COUNT(*) AS deleted_count 
FROM admin_users WHERE is_deleted = 1
UNION ALL
SELECT 'government_procurement_notices', COUNT(*) 
FROM government_procurement_notices WHERE is_deleted = 1;

-- 物理删除软删除数据（谨慎操作！）
-- DELETE FROM admin_users WHERE is_deleted = 1 AND updated_at < DATE_SUB(NOW(), INTERVAL 1 YEAR);
```

### 优化表

```sql
-- 优化所有表
OPTIMIZE TABLE admin_users, admin_roles, 
  government_procurement_notices, construction_project_notices,
  company_announcements, policy_regulations, policy_information, notice_announcements,
  carousel_banners, company_profile, business_scope, company_honors, major_achievements, friendly_links,
  visit_statistics, transaction_data, region_dictionary, system_logs;
```

---

## 📈 性能优化建议

### 1. 索引优化

```sql
-- 查看索引使用情况
SHOW INDEX FROM government_procurement_notices;

-- 分析查询性能
EXPLAIN SELECT * FROM government_procurement_notices 
WHERE project_region = '郑州市' AND is_deleted = 0;
```

### 2. 查询优化

- 避免 `SELECT *`，只查询需要的字段
- 使用 `LIMIT` 限制结果集大小
- 合理使用索引
- 避免在 WHERE 子句中使用函数

### 3. 缓存策略

- 使用 Redis 缓存热点数据
- 缓存首页统计数据（5分钟更新一次）
- 缓存常用配置信息

---

## 🔒 安全建议

### 1. 密码安全

```sql
-- 修改管理员密码
UPDATE admin_users 
SET password = SHA2('new_password', 256) 
WHERE username = 'admin';
```

### 2. 权限控制

```sql
-- 创建只读用户
CREATE USER 'readonly'@'localhost' IDENTIFIED BY 'password';
GRANT SELECT ON hailong_consulting.* TO 'readonly'@'localhost';

-- 创建应用用户（读写权限）
CREATE USER 'app_user'@'%' IDENTIFIED BY 'strong_password';
GRANT SELECT, INSERT, UPDATE, DELETE ON hailong_consulting.* TO 'app_user'@'%';
FLUSH PRIVILEGES;
```

### 3. 定期备份

- 每日全量备份
- 每小时增量备份
- 异地备份存储

---

## 📞 技术支持

如有问题或建议，请联系技术团队。

---

## 📝 更新日志

### Version 1.0 (2025-12-03)

- ✅ 完成数据库结构设计
- ✅ 创建18张数据表
- ✅ 创建3个统计视图
- ✅ 添加初始化数据
- ✅ 完善索引优化
- ✅ 编写完整文档

---

## 📄 许可证

本项目数据库设计归海隆咨询所有。

---

**最后更新**: 2025-12-03  
**数据库版本**: 1.0  
**MySQL版本要求**: 8.0+