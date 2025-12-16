# 海隆咨询官网数据库文档

海隆咨询官网项目的MySQL数据库设计文档。

## 📋 数据库概述

**数据库名称**: `hailong_consulting`

**数据库版本**: MySQL 8.0+

**字符集**: utf8mb4

**排序规则**: utf8mb4_unicode_ci

**创建时间**: 2025年12月3日

**最后更新**: 2025年12月16日

## 📊 数据库统计

| 项目 | 数量 | 说明 |
|------|------|------|
| 数据表 | 15张 | 业务表 + 系统表 |
| 视图 | 1个 | 统计视图 |
| 索引 | 60+ | 普通索引 + 全文索引 |
| 存储过程 | 0个 | 暂无 |
| 触发器 | 0个 | 暂无 |

## 📁 文件说明

### 核心文件

| 文件名 | 说明 | 用途 |
|--------|------|------|
| `hailong_consulting_schema.sql` | 数据库结构文件 | 创建数据库表结构 |
| `hailong_consulting_init_data.sql` | 初始数据文件 | 导入初始数据 |
| `README.md` | 数据库文档 | 本文件 |

### 使用顺序

1. **创建数据库结构**
```bash
mysql -u root -p < hailong_consulting_schema.sql
```

2. **导入初始数据**
```bash
mysql -u root -p < hailong_consulting_init_data.sql
```

## 🗂 数据表结构

### 模块分类

#### 0. 附件管理模块（1张表）

| 表名 | 说明 | 记录数 |
|------|------|--------|
| `attachments` | 附件表（统一管理所有附件和图片） | 动态 |

#### 1. 用户权限管理模块（1张表）

| 表名 | 说明 | 记录数 |
|------|------|--------|
| `users` | 用户表（API认证） | ~10 |

#### 2. 公告信息模块（1张表）

| 表名 | 说明 | 记录数 |
|------|------|--------|
| `announcements` | 统一公告表（政府采购+建设工程） | 1000+ |

#### 3. 信息发布模块（1张表）

| 表名 | 说明 | 记录数 |
|------|------|--------|
| `info_publications` | 统一信息发布表（新闻+政策法规） | 500+ |

#### 4. 系统配置模块（7张表）

| 表名 | 说明 | 记录数 |
|------|------|--------|
| `company_profile` | 企业简介表 | 1 |
| `business_scope` | 业务范围表 | ~10 |
| `company_qualifications` | 企业资质表 | ~20 |
| `major_achievements` | 重要业绩表 | ~50 |
| `company_honors` | 企业荣誉表 | ~30 |
| `carousel_banners` | 轮播图表 | ~5 |
| `friendly_links` | 友情链接表 | ~50 |

#### 5. 统计模块（1张表）

| 表名 | 说明 | 记录数 |
|------|------|--------|
| `visit_statistics` | 访问统计表 | 动态 |

#### 6. 辅助模块（2张表）

| 表名 | 说明 | 记录数 |
|------|------|--------|
| `region_dictionary` | 项目区域字典表（省市区三级） | ~3000 |
| `system_logs` | 系统操作日志表 | 动态 |

## 📋 核心表详细说明

### 1. attachments（附件表）

**用途**: 统一管理所有附件和图片

**字段说明**:

| 字段名 | 类型 | 说明 | 索引 |
|--------|------|------|------|
| `id` | INT UNSIGNED | 附件ID（主键） | PRIMARY |
| `file_name` | VARCHAR(255) | 文件名称 | - |
| `file_path` | VARCHAR(500) | 文件路径 | - |
| `file_url` | VARCHAR(500) | 文件访问URL | - |
| `file_size` | BIGINT UNSIGNED | 文件大小（字节） | - |
| `file_type` | VARCHAR(200) | 文件类型（MIME类型） | - |
| `file_extension` | VARCHAR(20) | 文件扩展名 | - |
| `category` | VARCHAR(50) | 附件分类：image/document/other | INDEX |
| `related_type` | VARCHAR(50) | 关联类型 | INDEX |
| `related_id` | INT UNSIGNED | 关联记录ID | INDEX |
| `upload_user_id` | INT UNSIGNED | 上传用户ID | INDEX |
| `is_deleted` | TINYINT | 软删除标记 | INDEX |
| `created_at` | DATETIME | 创建时间 | - |
| `updated_at` | DATETIME | 更新时间 | - |

**索引**:
- `idx_related_type_id`: (related_type, related_id)
- `idx_category`: (category)
- `idx_upload_user_id`: (upload_user_id)
- `idx_is_deleted`: (is_deleted)

### 2. users（用户表）

**用途**: API认证和用户管理

**字段说明**:

| 字段名 | 类型 | 说明 | 索引 |
|--------|------|------|------|
| `id` | INT UNSIGNED | 用户ID（主键） | PRIMARY |
| `username` | VARCHAR(50) | 用户名 | UNIQUE |
| `password` | VARCHAR(255) | 密码（加密存储） | - |
| `email` | VARCHAR(100) | 邮箱 | INDEX |
| `phone` | VARCHAR(20) | 手机号 | - |
| `real_name` | VARCHAR(50) | 真实姓名 | - |
| `role` | VARCHAR(20) | 角色：admin/user | INDEX |
| `refresh_token` | VARCHAR(255) | 刷新令牌 | INDEX |
| `refresh_token_expiry` | DATETIME | 刷新令牌过期时间 | - |
| `status` | TINYINT | 状态：0-禁用，1-启用 | INDEX |
| `last_login_time` | DATETIME | 最后登录时间 | - |
| `last_login_ip` | VARCHAR(50) | 最后登录IP | - |
| `is_deleted` | TINYINT | 软删除标记 | INDEX |
| `created_at` | DATETIME | 创建时间 | - |
| `updated_at` | DATETIME | 更新时间 | - |

**默认账号**:
- 用户名: `admin`
- 密码: `Admin@123456`（SHA256加密）
- 角色: `admin`

### 3. announcements（统一公告表）

**用途**: 管理政府采购和建设工程公告

**字段说明**:

| 字段名 | 类型 | 说明 | 索引 |
|--------|------|------|------|
| `id` | INT UNSIGNED | 公告ID（主键） | PRIMARY |
| `title` | VARCHAR(255) | 公告标题 | FULLTEXT |
| `business_type` | VARCHAR(50) | 业务类型：GOV_PROCUREMENT/CONSTRUCTION | INDEX |
| `notice_type` | VARCHAR(50) | 公告类型 | INDEX |
| `procurement_type` | VARCHAR(50) | 采购类型（仅政府采购） | - |
| `bidder` | VARCHAR(255) | 招标人 | - |
| `winner` | VARCHAR(255) | 中标人 | - |
| `budget_amount` | DECIMAL(15,2) | 预算金额（万元） | INDEX |
| `deadline` | DATETIME | 截止时间 | INDEX |
| `province` | VARCHAR(50) | 省份 | INDEX |
| `city` | VARCHAR(50) | 城市 | INDEX |
| `district` | VARCHAR(50) | 区县 | INDEX |
| `project_region` | VARCHAR(200) | 项目区域（完整地址） | INDEX |
| `content` | LONGTEXT | 公告内容（富文本） | FULLTEXT |
| `publisher` | VARCHAR(50) | 发布人 | - |
| `publish_time` | DATETIME | 发布时间 | INDEX |
| `view_count` | INT UNSIGNED | 浏览次数 | - |
| `attachment_ids` | VARCHAR(500) | 附件ID列表（JSON数组） | - |
| `is_top` | TINYINT | 是否置顶 | INDEX |
| `status` | TINYINT | 状态：0-禁用，1-启用 | INDEX |
| `is_deleted` | TINYINT | 软删除标记 | INDEX |
| `created_at` | DATETIME | 创建时间 | - |
| `updated_at` | DATETIME | 更新时间 | - |

**业务类型**:
- `GOV_PROCUREMENT`: 政府采购
- `CONSTRUCTION`: 建设工程

**公告类型**:
- 政府采购: `bidding`（采购公告）、`correction`（更正公告）、`result`（结果公告）
- 建设工程: `bidding`（招标公告）、`correction`（更正公告）、`result`（中标公告）

### 4. info_publications（统一信息发布表）

**用途**: 管理新闻中心和政策法规

**字段说明**:

| 字段名 | 类型 | 说明 | 索引 |
|--------|------|------|------|
| `id` | INT UNSIGNED | 信息ID（主键） | PRIMARY |
| `type` | VARCHAR(50) | 信息类型：COMPANY_NEWS/POLICY_REGULATION | INDEX |
| `category` | VARCHAR(100) | 二级分类 | INDEX |
| `title` | VARCHAR(255) | 标题 | FULLTEXT |
| `summary` | VARCHAR(500) | 摘要 | - |
| `content` | LONGTEXT | 内容（富文本） | FULLTEXT |
| `cover_image_id` | INT UNSIGNED | 封面图片ID | - |
| `author` | VARCHAR(100) | 作者 | INDEX |
| `publisher` | VARCHAR(50) | 发布人 | - |
| `publish_time` | DATETIME | 发布时间 | INDEX |
| `view_count` | INT UNSIGNED | 浏览次数 | - |
| `attachment_ids` | VARCHAR(500) | 附件ID列表（JSON数组） | - |
| `is_top` | TINYINT | 是否置顶 | INDEX |
| `status` | TINYINT | 状态：0-禁用，1-启用 | INDEX |
| `is_deleted` | TINYINT | 软删除标记 | INDEX |
| `created_at` | DATETIME | 创建时间 | - |
| `updated_at` | DATETIME | 更新时间 | - |

**信息类型**:
- `COMPANY_NEWS`: 新闻中心（公司新闻/行业动态/通知公告）
- `POLICY_REGULATION`: 政策法规（法律法规/行政法规/地方政策）

### 5. region_dictionary（区域字典表）

**用途**: 省市区三级区域数据

**字段说明**:

| 字段名 | 类型 | 说明 | 索引 |
|--------|------|------|------|
| `id` | INT UNSIGNED | 区域ID（主键） | PRIMARY |
| `region_code` | VARCHAR(50) | 区域代码 | UNIQUE |
| `region_name` | VARCHAR(50) | 区域名称 | - |
| `region_level` | TINYINT | 区域级别：1-省份，2-城市，3-区县 | INDEX |
| `parent_code` | VARCHAR(50) | 父级区域代码 | INDEX |
| `sort_order` | INT | 排序顺序 | INDEX |
| `is_deleted` | TINYINT | 软删除标记 | INDEX |
| `created_at` | DATETIME | 创建时间 | - |
| `updated_at` | DATETIME | 更新时间 | - |

**数据示例**:
```
410000 - 河南省 (level=1, parent_code=NULL)
  410100 - 郑州市 (level=2, parent_code=410000)
    410101 - 中原区 (level=3, parent_code=410100)
    410102 - 二七区 (level=3, parent_code=410100)
```

### 6. system_logs（系统日志表）

**用途**: 记录系统操作日志

**字段说明**:

| 字段名 | 类型 | 说明 | 索引 |
|--------|------|------|------|
| `id` | BIGINT UNSIGNED | 日志ID（主键） | PRIMARY |
| `user_id` | INT UNSIGNED | 操作用户ID | INDEX |
| `username` | VARCHAR(50) | 操作用户名 | - |
| `action` | VARCHAR(100) | 操作动作 | INDEX |
| `module` | VARCHAR(50) | 操作模块 | INDEX |
| `description` | TEXT | 操作描述 | - |
| `ip_address` | VARCHAR(50) | IP地址 | - |
| `user_agent` | VARCHAR(500) | 用户代理 | - |
| `request_data` | TEXT | 请求数据（JSON） | - |
| `response_data` | TEXT | 响应数据（JSON） | - |
| `status` | VARCHAR(20) | 操作状态：success/error | INDEX |
| `created_at` | DATETIME | 创建时间 | INDEX |

## 📊 视图说明

### v_homepage_statistics（首页统计视图）

**用途**: 提供首页统计数据

**查询内容**:
```sql
SELECT
  (SELECT COUNT(*) FROM announcements WHERE is_deleted = 0 AND status = 1) AS total_projects,
  (SELECT COUNT(DISTINCT CONCAT_WS('-', province, city, district)) FROM announcements WHERE is_deleted = 0 AND status = 1) AS total_regions,
  (SELECT COUNT(*) FROM announcements WHERE business_type = 'GOV_PROCUREMENT' AND is_deleted = 0 AND status = 1) AS gov_procurement_count,
  (SELECT COUNT(*) FROM announcements WHERE business_type = 'CONSTRUCTION' AND is_deleted = 0 AND status = 1) AS construction_count;
```

**返回字段**:
- `total_projects`: 总项目数
- `total_regions`: 覆盖区域数
- `gov_procurement_count`: 政府采购项目数
- `construction_count`: 建设工程项目数

## 🔍 索引策略

### 普通索引

用于提高查询性能的常规索引：

```sql
-- 公告表索引
idx_business_type (business_type)
idx_notice_type (notice_type)
idx_province (province)
idx_city (city)
idx_district (district)
idx_publish_time (publish_time)
idx_is_top (is_top)
idx_status (status)
idx_is_deleted (is_deleted)
```

### 全文索引

用于全文搜索的索引：

```sql
-- 公告表全文索引
FULLTEXT idx_title_content (title, content)

-- 信息发布表全文索引
FULLTEXT idx_title_content (title, content)
```

**使用示例**:
```sql
-- 全文搜索
SELECT * FROM announcements 
WHERE MATCH(title, content) AGAINST('招标' IN NATURAL LANGUAGE MODE);
```

### 组合索引

用于多条件查询的组合索引：

```sql
-- 附件表组合索引
idx_related_type_id (related_type, related_id)
```

## 🔧 数据库维护

### 备份策略

**每日备份**:
```bash
#!/bin/bash
# 备份脚本
DATE=$(date +%Y%m%d)
mysqldump -u root -p hailong_consulting > /backup/hailong_consulting_$DATE.sql
```

**定时任务**:
```bash
# 添加到crontab
0 2 * * * /path/to/backup.sh
```

### 优化建议

**1. 定期优化表**:
```sql
OPTIMIZE TABLE announcements;
OPTIMIZE TABLE info_publications;
OPTIMIZE TABLE attachments;
```

**2. 分析表**:
```sql
ANALYZE TABLE announcements;
ANALYZE TABLE info_publications;
```

**3. 清理日志**:
```sql
-- 删除30天前的系统日志
DELETE FROM system_logs WHERE created_at < DATE_SUB(NOW(), INTERVAL 30 DAY);
```

**4. 监控表大小**:
```sql
SELECT 
    table_name AS '表名',
    ROUND(((data_length + index_length) / 1024 / 1024), 2) AS '大小(MB)'
FROM information_schema.TABLES 
WHERE table_schema = 'hailong_consulting'
ORDER BY (data_length + index_length) DESC;
```

## 📈 性能优化

### 查询优化

**1. 使用索引**:
```sql
-- 好的查询（使用索引）
SELECT * FROM announcements 
WHERE business_type = 'GOV_PROCUREMENT' 
AND status = 1 
AND is_deleted = 0
ORDER BY publish_time DESC
LIMIT 10;

-- 避免全表扫描
SELECT * FROM announcements WHERE title LIKE '%招标%'; -- 慢
SELECT * FROM announcements WHERE MATCH(title) AGAINST('招标'); -- 快
```

**2. 分页优化**:
```sql
-- 使用LIMIT优化分页
SELECT * FROM announcements 
WHERE is_deleted = 0 
ORDER BY id DESC 
LIMIT 10 OFFSET 0;

-- 大偏移量优化
SELECT * FROM announcements 
WHERE id < (SELECT id FROM announcements ORDER BY id DESC LIMIT 1000, 1)
ORDER BY id DESC 
LIMIT 10;
```

**3. 避免SELECT ***:
```sql
-- 只查询需要的字段
SELECT id, title, publish_time FROM announcements;
```

### 连接池配置

**应用程序配置**:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3306;Database=hailong_consulting;User=root;Password=xxx;CharSet=utf8mb4;MinimumPoolSize=5;MaximumPoolSize=100;ConnectionLifeTime=300;"
  }
}
```

## 🔐 安全建议

### 1. 用户权限

```sql
-- 创建应用程序专用用户
CREATE USER 'hailong_app'@'localhost' IDENTIFIED BY 'strong_password';

-- 授予必要权限
GRANT SELECT, INSERT, UPDATE, DELETE ON hailong_consulting.* TO 'hailong_app'@'localhost';

-- 刷新权限
FLUSH PRIVILEGES;
```

### 2. 密码策略

- 使用强密码
- 定期更换密码
- 密码加密存储（SHA256）

### 3. 备份加密

```bash
# 加密备份
mysqldump -u root -p hailong_consulting | gzip | openssl enc -aes-256-cbc -salt -out backup.sql.gz.enc
```

### 4. 访问控制

- 限制远程访问
- 使用防火墙规则
- 启用SSL连接

## 📚 相关文档

- [项目总体说明](../README.md)
- [后端API文档](../BackEnd/HailongConsulting.API/README.md)
- [后台管理文档](../hailong-admin/README.md)
- [前端门户文档](../hailong-protral/README.md)

## 📄 许可证

Copyright © 2025 河南海隆工程咨询有限公司

---

**最后更新**: 2025年12月16日  
**维护团队**: 海隆咨询技术部