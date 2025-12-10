# 海隆咨询官网数据库文档

## 📋 文档概述

本目录包含海隆咨询官网的完整数据库设计文档和SQL脚本。

---

## 📁 文件列表

| 文件名 | 说明 |
|--------|------|
| `hailong_consulting_schema.sql` | 数据库结构脚本 |
| `hailong_consulting_init_data.sql` | 初始化数据脚本 |
| `数据字典.md` | 详细的表结构、字段说明 |
| `ER关系图说明.md` | 实体关系图和数据库架构说明 |
| `README.md` | 本文件 |

---

## 🚀 快速开始

### 环境要求

- MySQL 8.0+
- 字符集：utf8mb4
- 存储引擎：InnoDB

### 创建数据库

```bash
# 登录MySQL
mysql -u root -p

# 执行建表脚本
source /path/to/hailong_consulting_schema.sql

# 执行初始化数据脚本
source /path/to/hailong_consulting_init_data.sql

# 验证
USE hailong_consulting;
SHOW TABLES;
```

---

## 📊 数据库结构

### 核心表（14张）

```
hailong_consulting
├── attachments                 # 附件统一管理
├── users                       # 用户认证
├── announcements               # 统一公告表（政府采购+建设工程）
├── info_publications           # 统一信息发布表（新闻+政策法规）
├── company_profile             # 企业简介
├── business_scope              # 业务范围
├── company_qualifications      # 企业资质
├── major_achievements          # 重要业绩
├── company_honors              # 企业荣誉
├── carousel_banners            # 轮播图
├── friendly_links              # 友情链接
├── visit_statistics            # 访问统计
├── region_dictionary           # 区域字典（省市区三级）
└── system_logs                 # 系统日志
```

### 视图（1个）

- `v_homepage_statistics` - 首页统计视图

---

## 🔑 核心表说明

### 1. announcements（统一公告表）

**业务类型** (business_type)：
- `GOV_PROCUREMENT` - 政府采购
- `CONSTRUCTION` - 建设工程

**公告类型** (notice_type)：
- `bidding` - 招标/采购公告
- `correction` - 更正公告
- `result` - 结果公告

**核心字段**：
- 支持富文本内容
- 地区三级联动（省/市/区）
- 全文搜索（FULLTEXT索引）
- 附件关联（JSON数组）
- 置顶、浏览量统计

### 2. info_publications（统一信息发布表）

**信息类型** (type)：
- `COMPANY_NEWS` - 新闻中心
- `POLICY_REGULATION` - 政策法规

**核心字段**：
- 支持富文本内容
- 二级分类（category）
- 全文搜索（FULLTEXT索引）
- 封面图片、附件关联
- 置顶、浏览量统计

### 3. attachments（附件统一管理）

**分类** (category)：
- `image` - 图片
- `document` - 文档
- `video` - 视频
- `other` - 其他

**关联方式**：
- `related_type` - 关联类型（announcement、info_publication等）
- `related_id` - 关联记录ID

---

## 📖 常用SQL示例

### 查询公告

```sql
-- 查询政府采购公告
SELECT id, title, notice_type, publish_time, view_count
FROM announcements
WHERE business_type = 'GOV_PROCUREMENT' 
  AND is_deleted = 0 AND status = 1
ORDER BY is_top DESC, publish_time DESC
LIMIT 10;

-- 按地区查询
SELECT id, title, province, city, publish_time
FROM announcements
WHERE province = '河南省' AND city = '郑州市'
  AND is_deleted = 0 AND status = 1
ORDER BY publish_time DESC;
```

### 全文搜索

```sql
-- 搜索公告
SELECT id, title, business_type, publish_time
FROM announcements
WHERE MATCH(title, content) AGAINST('医疗设备' IN NATURAL LANGUAGE MODE)
  AND is_deleted = 0 AND status = 1;

-- 搜索信息发布
SELECT id, title, type, publish_time
FROM info_publications
WHERE MATCH(title, content) AGAINST('招标政策' IN NATURAL LANGUAGE MODE)
  AND is_deleted = 0 AND status = 1;
```

### 附件查询

```sql
-- 查询某公告的附件
SELECT a.id, a.file_name, a.file_url, a.file_size
FROM attachments a
WHERE a.related_type = 'announcement' 
  AND a.related_id = 1
  AND a.is_deleted = 0;
```

### 区域字典

```sql
-- 查询所有省份
SELECT region_code, region_name
FROM region_dictionary
WHERE region_level = 1 AND is_deleted = 0
ORDER BY sort_order;

-- 查询某省下的城市
SELECT region_code, region_name
FROM region_dictionary
WHERE region_level = 2 AND parent_code = '410000'
  AND is_deleted = 0
ORDER BY sort_order;
```

---

## 🔧 维护操作

### 备份

```bash
# 完整备份
mysqldump -u root -p hailong_consulting > backup_$(date +%Y%m%d).sql

# 压缩备份
mysqldump -u root -p hailong_consulting | gzip > backup_$(date +%Y%m%d).sql.gz
```

### 恢复

```bash
# 恢复数据库
mysql -u root -p hailong_consulting < backup_20231203.sql
```

### 优化

```sql
-- 优化表
OPTIMIZE TABLE announcements, info_publications, attachments;

-- 分析表
ANALYZE TABLE announcements, info_publications;
```

---

## 📝 更新日志

### Version 1.1.0 (2025-12-08)
- 重构为统一表结构设计
- 新增附件统一管理模块
- 优化索引设计

### Version 1.0.0 (2025-12-03)
- 初始数据库结构设计

---

**最后更新**: 2025-12-10  
**数据库版本**: 1.1.0  
**MySQL版本**: 8.0+