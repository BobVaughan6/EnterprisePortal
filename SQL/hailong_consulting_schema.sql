-- ============================================
-- 海隆咨询官网数据库结构设计
-- 数据库名：hailong_consulting
-- MySQL版本：8.0+
-- 创建时间：2025-12-03
-- ============================================

-- 创建数据库
CREATE DATABASE IF NOT EXISTS `hailong_consulting` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;

USE `hailong_consulting`;

-- ============================================
-- 1. 用户权限管理模块
-- ============================================

-- 1.1 管理员账号表
CREATE TABLE `admin_users` (
  `id` INT UNSIGNED AUTO_INCREMENT PRIMARY KEY COMMENT '管理员ID',
  `username` VARCHAR(50) NOT NULL UNIQUE COMMENT '用户名',
  `password` VARCHAR(255) NOT NULL COMMENT '密码（加密存储）',
  `real_name` VARCHAR(50) DEFAULT NULL COMMENT '真实姓名',
  `email` VARCHAR(100) DEFAULT NULL COMMENT '邮箱',
  `phone` VARCHAR(20) DEFAULT NULL COMMENT '手机号',
  `role_id` INT UNSIGNED DEFAULT NULL COMMENT '角色ID',
  `status` TINYINT DEFAULT 1 COMMENT '状态：0-禁用，1-启用',
  `last_login_time` DATETIME DEFAULT NULL COMMENT '最后登录时间',
  `last_login_ip` VARCHAR(50) DEFAULT NULL COMMENT '最后登录IP',
  `is_deleted` TINYINT DEFAULT 0 COMMENT '软删除：0-未删除，1-已删除',
  `created_at` DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `updated_at` DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '更新时间',
  INDEX `idx_username` (`username`),
  INDEX `idx_role_id` (`role_id`),
  INDEX `idx_status` (`status`),
  INDEX `idx_is_deleted` (`is_deleted`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci COMMENT='管理员账号表';

-- 1.2 用户表（用于API认证）
CREATE TABLE `users` (
  `user_id` INT UNSIGNED AUTO_INCREMENT PRIMARY KEY COMMENT '用户ID',
  `username` VARCHAR(50) NOT NULL UNIQUE COMMENT '用户名',
  `password_hash` VARCHAR(255) NOT NULL COMMENT '密码哈希（MD5）',
  `email` VARCHAR(100) NOT NULL COMMENT '邮箱',
  `phone` VARCHAR(20) DEFAULT NULL COMMENT '手机号',
  `full_name` VARCHAR(50) NOT NULL COMMENT '真实姓名',
  `role` VARCHAR(20) NOT NULL DEFAULT 'user' COMMENT '角色：admin-管理员，user-普通用户',
  `refresh_token` VARCHAR(255) DEFAULT NULL COMMENT '刷新令牌',
  `refresh_token_expiry` DATETIME DEFAULT NULL COMMENT '刷新令牌过期时间',
  `is_active` TINYINT DEFAULT 1 COMMENT '状态：0-禁用，1-启用',
  `created_at` DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `updated_at` DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '更新时间',
  `last_login` DATETIME DEFAULT NULL COMMENT '最后登录时间',
  INDEX `idx_username` (`username`),
  INDEX `idx_email` (`email`),
  INDEX `idx_refresh_token` (`refresh_token`),
  INDEX `idx_role` (`role`),
  INDEX `idx_is_active` (`is_active`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci COMMENT='用户表（API认证）';

-- 1.3 角色表
CREATE TABLE `admin_roles` (
  `id` INT UNSIGNED AUTO_INCREMENT PRIMARY KEY COMMENT '角色ID',
  `role_name` VARCHAR(50) NOT NULL COMMENT '角色名称',
  `role_code` VARCHAR(50) NOT NULL UNIQUE COMMENT '角色代码',
  `description` VARCHAR(255) DEFAULT NULL COMMENT '角色描述',
  `permissions` TEXT DEFAULT NULL COMMENT '权限列表（JSON格式）',
  `status` TINYINT DEFAULT 1 COMMENT '状态：0-禁用，1-启用',
  `is_deleted` TINYINT DEFAULT 0 COMMENT '软删除：0-未删除，1-已删除',
  `created_at` DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `updated_at` DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '更新时间',
  INDEX `idx_role_code` (`role_code`),
  INDEX `idx_status` (`status`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci COMMENT='角色权限表';

-- ============================================
-- 2. 公告信息模块（核心）
-- ============================================

-- 2.1 政府采购公告表
CREATE TABLE `government_procurement_notices` (
  `id` INT UNSIGNED AUTO_INCREMENT PRIMARY KEY COMMENT '公告ID',
  `title` VARCHAR(255) NOT NULL COMMENT '公告标题',
  `notice_type` VARCHAR(50) NOT NULL COMMENT '公告类型：采购公告、更正公告、结果公告',
  `bidder` VARCHAR(255) DEFAULT NULL COMMENT '招标人',
  `winner` VARCHAR(255) DEFAULT NULL COMMENT '中标人',
  `project_region` VARCHAR(50) NOT NULL COMMENT '项目区域：郑州、洛阳、开封、新乡等',
  `content` LONGTEXT NOT NULL COMMENT '公告内容（富文本）',
  `publisher` VARCHAR(50) DEFAULT NULL COMMENT '发布人',
  `publish_time` DATETIME DEFAULT NULL COMMENT '发布时间',
  `view_count` INT UNSIGNED DEFAULT 0 COMMENT '访问量',
  `attachment_path` VARCHAR(500) DEFAULT NULL COMMENT '附件路径',
  `is_top` TINYINT DEFAULT 0 COMMENT '是否置顶：0-否，1-是',
  `is_deleted` TINYINT DEFAULT 0 COMMENT '软删除：0-未删除，1-已删除',
  `created_at` DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `updated_at` DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '更新时间',
  INDEX `idx_notice_type` (`notice_type`),
  INDEX `idx_project_region` (`project_region`),
  INDEX `idx_publish_time` (`publish_time`),
  INDEX `idx_is_top` (`is_top`),
  INDEX `idx_is_deleted` (`is_deleted`),
  FULLTEXT INDEX `ft_title_content` (`title`, `content`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci COMMENT='政府采购公告表';

-- 2.2 建设工程公告表
CREATE TABLE `construction_project_notices` (
  `id` INT UNSIGNED AUTO_INCREMENT PRIMARY KEY COMMENT '公告ID',
  `title` VARCHAR(255) NOT NULL COMMENT '公告标题',
  `notice_type` VARCHAR(50) NOT NULL COMMENT '公告类型：招标公告、中标公告、变更公告',
  `bidder` VARCHAR(255) DEFAULT NULL COMMENT '招标人',
  `winner` VARCHAR(255) DEFAULT NULL COMMENT '中标人',
  `project_region` VARCHAR(50) NOT NULL COMMENT '项目区域：郑州、洛阳、开封、新乡等',
  `content` LONGTEXT NOT NULL COMMENT '公告内容（富文本）',
  `publisher` VARCHAR(50) DEFAULT NULL COMMENT '发布人',
  `publish_time` DATETIME DEFAULT NULL COMMENT '发布时间',
  `view_count` INT UNSIGNED DEFAULT 0 COMMENT '访问量',
  `attachment_path` VARCHAR(500) DEFAULT NULL COMMENT '附件路径',
  `is_top` TINYINT DEFAULT 0 COMMENT '是否置顶：0-否，1-是',
  `is_deleted` TINYINT DEFAULT 0 COMMENT '软删除：0-未删除，1-已删除',
  `created_at` DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `updated_at` DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '更新时间',
  INDEX `idx_notice_type` (`notice_type`),
  INDEX `idx_project_region` (`project_region`),
  INDEX `idx_publish_time` (`publish_time`),
  INDEX `idx_is_top` (`is_top`),
  INDEX `idx_is_deleted` (`is_deleted`),
  FULLTEXT INDEX `ft_title_content` (`title`, `content`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci COMMENT='建设工程公告表';

-- ============================================
-- 3. 信息发布模块
-- ============================================

-- 3.1 公司公告表
CREATE TABLE `company_announcements` (
  `id` INT UNSIGNED AUTO_INCREMENT PRIMARY KEY COMMENT '公告ID',
  `title` VARCHAR(255) NOT NULL COMMENT '公告标题',
  `content` LONGTEXT NOT NULL COMMENT '公告内容（富文本）',
  `publish_time` DATETIME DEFAULT NULL COMMENT '发布时间',
  `view_count` INT UNSIGNED DEFAULT 0 COMMENT '访问量',
  `attachment_path` VARCHAR(500) DEFAULT NULL COMMENT '附件路径',
  `is_top` TINYINT DEFAULT 0 COMMENT '是否置顶：0-否，1-是',
  `is_deleted` TINYINT DEFAULT 0 COMMENT '软删除：0-未删除，1-已删除',
  `created_at` DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `updated_at` DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '更新时间',
  INDEX `idx_publish_time` (`publish_time`),
  INDEX `idx_is_top` (`is_top`),
  INDEX `idx_is_deleted` (`is_deleted`),
  FULLTEXT INDEX `ft_title_content` (`title`, `content`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci COMMENT='公司公告表';

-- 3.2 政策法规表
CREATE TABLE `policy_regulations` (
  `id` INT UNSIGNED AUTO_INCREMENT PRIMARY KEY COMMENT '法规ID',
  `title` VARCHAR(255) NOT NULL COMMENT '法规标题',
  `content` LONGTEXT NOT NULL COMMENT '法规内容（富文本）',
  `publish_time` DATETIME DEFAULT NULL COMMENT '发布时间',
  `view_count` INT UNSIGNED DEFAULT 0 COMMENT '访问量',
  `attachment_path` VARCHAR(500) DEFAULT NULL COMMENT '附件路径',
  `is_top` TINYINT DEFAULT 0 COMMENT '是否置顶：0-否，1-是',
  `is_deleted` TINYINT DEFAULT 0 COMMENT '软删除：0-未删除，1-已删除',
  `created_at` DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `updated_at` DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '更新时间',
  INDEX `idx_publish_time` (`publish_time`),
  INDEX `idx_is_top` (`is_top`),
  INDEX `idx_is_deleted` (`is_deleted`),
  FULLTEXT INDEX `ft_title_content` (`title`, `content`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci COMMENT='政策法规表';

-- 3.3 政策信息表
CREATE TABLE `policy_information` (
  `id` INT UNSIGNED AUTO_INCREMENT PRIMARY KEY COMMENT '信息ID',
  `title` VARCHAR(255) NOT NULL COMMENT '信息标题',
  `content` LONGTEXT NOT NULL COMMENT '信息内容（富文本）',
  `publish_time` DATETIME DEFAULT NULL COMMENT '发布时间',
  `view_count` INT UNSIGNED DEFAULT 0 COMMENT '访问量',
  `attachment_path` VARCHAR(500) DEFAULT NULL COMMENT '附件路径',
  `is_top` TINYINT DEFAULT 0 COMMENT '是否置顶：0-否，1-是',
  `is_deleted` TINYINT DEFAULT 0 COMMENT '软删除：0-未删除，1-已删除',
  `created_at` DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `updated_at` DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '更新时间',
  INDEX `idx_publish_time` (`publish_time`),
  INDEX `idx_is_top` (`is_top`),
  INDEX `idx_is_deleted` (`is_deleted`),
  FULLTEXT INDEX `ft_title_content` (`title`, `content`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci COMMENT='政策信息表';

-- 3.4 通知公告表
CREATE TABLE `notice_announcements` (
  `id` INT UNSIGNED AUTO_INCREMENT PRIMARY KEY COMMENT '通知ID',
  `title` VARCHAR(255) NOT NULL COMMENT '通知标题',
  `content` LONGTEXT NOT NULL COMMENT '通知内容（富文本）',
  `publish_time` DATETIME DEFAULT NULL COMMENT '发布时间',
  `view_count` INT UNSIGNED DEFAULT 0 COMMENT '访问量',
  `attachment_path` VARCHAR(500) DEFAULT NULL COMMENT '附件路径',
  `is_top` TINYINT DEFAULT 0 COMMENT '是否置顶：0-否，1-是',
  `is_deleted` TINYINT DEFAULT 0 COMMENT '软删除：0-未删除，1-已删除',
  `created_at` DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `updated_at` DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '更新时间',
  INDEX `idx_publish_time` (`publish_time`),
  INDEX `idx_is_top` (`is_top`),
  INDEX `idx_is_deleted` (`is_deleted`),
  FULLTEXT INDEX `ft_title_content` (`title`, `content`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci COMMENT='通知公告表';

-- ============================================
-- 4. 系统配置模块
-- ============================================

-- 4.1 轮播图表
CREATE TABLE `carousel_banners` (
  `id` INT UNSIGNED AUTO_INCREMENT PRIMARY KEY COMMENT '轮播图ID',
  `title` VARCHAR(255) DEFAULT NULL COMMENT '轮播图标题',
  `image_url` VARCHAR(500) NOT NULL COMMENT '图片URL',
  `link_url` VARCHAR(500) DEFAULT NULL COMMENT '跳转链接',
  `sort_order` INT DEFAULT 0 COMMENT '排序顺序（数字越小越靠前）',
  `status` TINYINT DEFAULT 1 COMMENT '状态：0-禁用，1-启用',
  `is_deleted` TINYINT DEFAULT 0 COMMENT '软删除：0-未删除，1-已删除',
  `created_at` DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `updated_at` DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '更新时间',
  INDEX `idx_sort_order` (`sort_order`),
  INDEX `idx_status` (`status`),
  INDEX `idx_is_deleted` (`is_deleted`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci COMMENT='轮播图表';

-- 4.2 企业简介表
CREATE TABLE `company_profile` (
  `id` INT UNSIGNED AUTO_INCREMENT PRIMARY KEY COMMENT '简介ID',
  `title` VARCHAR(255) NOT NULL COMMENT '简介标题',
  `content` LONGTEXT NOT NULL COMMENT '简介内容（富文本）',
  `image_url` VARCHAR(500) DEFAULT NULL COMMENT '配图URL',
  `is_deleted` TINYINT DEFAULT 0 COMMENT '软删除：0-未删除，1-已删除',
  `created_at` DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `updated_at` DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '更新时间',
  INDEX `idx_is_deleted` (`is_deleted`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci COMMENT='企业简介表';

-- 4.3 业务范围表
CREATE TABLE `business_scope` (
  `id` INT UNSIGNED AUTO_INCREMENT PRIMARY KEY COMMENT '业务ID',
  `title` VARCHAR(255) NOT NULL COMMENT '业务标题',
  `description` TEXT DEFAULT NULL COMMENT '业务描述',
  `icon_url` VARCHAR(500) DEFAULT NULL COMMENT '图标URL',
  `sort_order` INT DEFAULT 0 COMMENT '排序顺序',
  `is_deleted` TINYINT DEFAULT 0 COMMENT '软删除：0-未删除，1-已删除',
  `created_at` DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `updated_at` DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '更新时间',
  INDEX `idx_sort_order` (`sort_order`),
  INDEX `idx_is_deleted` (`is_deleted`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci COMMENT='业务范围表';

-- 4.4 企业荣誉表
CREATE TABLE `company_honors` (
  `id` INT UNSIGNED AUTO_INCREMENT PRIMARY KEY COMMENT '荣誉ID',
  `title` VARCHAR(255) NOT NULL COMMENT '荣誉标题',
  `description` TEXT DEFAULT NULL COMMENT '荣誉描述',
  `image_url` VARCHAR(500) DEFAULT NULL COMMENT '荣誉图片URL',
  `award_date` DATE DEFAULT NULL COMMENT '获奖日期',
  `sort_order` INT DEFAULT 0 COMMENT '排序顺序',
  `is_deleted` TINYINT DEFAULT 0 COMMENT '软删除：0-未删除，1-已删除',
  `created_at` DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `updated_at` DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '更新时间',
  INDEX `idx_award_date` (`award_date`),
  INDEX `idx_sort_order` (`sort_order`),
  INDEX `idx_is_deleted` (`is_deleted`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci COMMENT='企业荣誉表';

-- 4.5 重要业绩表
CREATE TABLE `major_achievements` (
  `id` INT UNSIGNED AUTO_INCREMENT PRIMARY KEY COMMENT '业绩ID',
  `project_name` VARCHAR(255) NOT NULL COMMENT '项目名称',
  `project_type` VARCHAR(50) DEFAULT NULL COMMENT '项目类型',
  `project_amount` DECIMAL(15,2) DEFAULT NULL COMMENT '项目金额（万元）',
  `client_name` VARCHAR(255) DEFAULT NULL COMMENT '客户名称',
  `completion_date` DATE DEFAULT NULL COMMENT '完成日期',
  `description` TEXT DEFAULT NULL COMMENT '项目描述',
  `image_url` VARCHAR(500) DEFAULT NULL COMMENT '项目图片URL',
  `sort_order` INT DEFAULT 0 COMMENT '排序顺序',
  `is_deleted` TINYINT DEFAULT 0 COMMENT '软删除：0-未删除，1-已删除',
  `created_at` DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `updated_at` DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '更新时间',
  INDEX `idx_completion_date` (`completion_date`),
  INDEX `idx_sort_order` (`sort_order`),
  INDEX `idx_is_deleted` (`is_deleted`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci COMMENT='重要业绩表';

-- 4.6 友情链接表
CREATE TABLE `friendly_links` (
  `id` INT UNSIGNED AUTO_INCREMENT PRIMARY KEY COMMENT '链接ID',
  `link_name` VARCHAR(100) NOT NULL COMMENT '链接名称',
  `link_url` VARCHAR(500) NOT NULL COMMENT '链接URL',
  `logo_url` VARCHAR(500) DEFAULT NULL COMMENT 'Logo URL',
  `sort_order` INT DEFAULT 0 COMMENT '排序顺序',
  `status` TINYINT DEFAULT 1 COMMENT '状态：0-禁用，1-启用',
  `is_deleted` TINYINT DEFAULT 0 COMMENT '软删除：0-未删除，1-已删除',
  `created_at` DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `updated_at` DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '更新时间',
  INDEX `idx_sort_order` (`sort_order`),
  INDEX `idx_status` (`status`),
  INDEX `idx_is_deleted` (`is_deleted`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci COMMENT='友情链接表';

-- ============================================
-- 5. 统计模块
-- ============================================

-- 5.1 访问统计表
CREATE TABLE `visit_statistics` (
  `id` INT UNSIGNED AUTO_INCREMENT PRIMARY KEY COMMENT '统计ID',
  `visit_date` DATE NOT NULL COMMENT '访问日期',
  `page_url` VARCHAR(500) DEFAULT NULL COMMENT '页面URL',
  `page_title` VARCHAR(255) DEFAULT NULL COMMENT '页面标题',
  `visitor_ip` VARCHAR(50) DEFAULT NULL COMMENT '访问者IP',
  `user_agent` VARCHAR(500) DEFAULT NULL COMMENT '用户代理',
  `referer` VARCHAR(500) DEFAULT NULL COMMENT '来源页面',
  `visit_count` INT UNSIGNED DEFAULT 1 COMMENT '访问次数',
  `is_deleted` TINYINT DEFAULT 0 COMMENT '软删除：0-未删除，1-已删除',
  `created_at` DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `updated_at` DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '更新时间',
  INDEX `idx_visit_date` (`visit_date`),
  INDEX `idx_page_url` (`page_url`(255)),
  INDEX `idx_visitor_ip` (`visitor_ip`),
  INDEX `idx_is_deleted` (`is_deleted`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci COMMENT='访问统计表';

-- 5.2 交易数据表（用于首页可视化）
CREATE TABLE `transaction_data` (
  `id` INT UNSIGNED AUTO_INCREMENT PRIMARY KEY COMMENT '交易ID',
  `project_name` VARCHAR(255) NOT NULL COMMENT '项目名称',
  `project_type` VARCHAR(50) NOT NULL COMMENT '交易类型：政府采购、建设工程等',
  `project_amount` DECIMAL(15,2) NOT NULL COMMENT '项目金额（万元）',
  `project_region` VARCHAR(50) NOT NULL COMMENT '项目区域',
  `transaction_date` DATE NOT NULL COMMENT '交易日期',
  `client_name` VARCHAR(255) DEFAULT NULL COMMENT '客户名称',
  `status` VARCHAR(50) DEFAULT '已完成' COMMENT '项目状态',
  `is_deleted` TINYINT DEFAULT 0 COMMENT '软删除：0-未删除，1-已删除',
  `created_at` DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `updated_at` DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '更新时间',
  INDEX `idx_project_type` (`project_type`),
  INDEX `idx_project_region` (`project_region`),
  INDEX `idx_transaction_date` (`transaction_date`),
  INDEX `idx_status` (`status`),
  INDEX `idx_is_deleted` (`is_deleted`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci COMMENT='交易数据表';

-- ============================================
-- 6. 项目区域字典表
-- ============================================

CREATE TABLE `region_dictionary` (
  `id` INT UNSIGNED AUTO_INCREMENT PRIMARY KEY COMMENT '区域ID',
  `region_code` VARCHAR(50) NOT NULL UNIQUE COMMENT '区域代码',
  `region_name` VARCHAR(50) NOT NULL COMMENT '区域名称',
  `region_type` VARCHAR(20) NOT NULL COMMENT '区域类型：省内、省外',
  `parent_id` INT UNSIGNED DEFAULT 0 COMMENT '父级区域ID',
  `sort_order` INT DEFAULT 0 COMMENT '排序顺序',
  `is_deleted` TINYINT DEFAULT 0 COMMENT '软删除：0-未删除，1-已删除',
  `created_at` DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `updated_at` DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '更新时间',
  INDEX `idx_region_code` (`region_code`),
  INDEX `idx_region_type` (`region_type`),
  INDEX `idx_parent_id` (`parent_id`),
  INDEX `idx_is_deleted` (`is_deleted`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci COMMENT='项目区域字典表';

-- ============================================
-- 7. 系统日志表
-- ============================================

CREATE TABLE `system_logs` (
  `id` BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY COMMENT '日志ID',
  `user_id` INT UNSIGNED DEFAULT NULL COMMENT '操作用户ID',
  `username` VARCHAR(50) DEFAULT NULL COMMENT '操作用户名',
  `action` VARCHAR(100) NOT NULL COMMENT '操作动作',
  `module` VARCHAR(50) DEFAULT NULL COMMENT '操作模块',
  `description` TEXT DEFAULT NULL COMMENT '操作描述',
  `ip_address` VARCHAR(50) DEFAULT NULL COMMENT 'IP地址',
  `user_agent` VARCHAR(500) DEFAULT NULL COMMENT '用户代理',
  `request_data` TEXT DEFAULT NULL COMMENT '请求数据（JSON）',
  `response_data` TEXT DEFAULT NULL COMMENT '响应数据（JSON）',
  `status` VARCHAR(20) DEFAULT 'success' COMMENT '操作状态：success、error',
  `created_at` DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  INDEX `idx_user_id` (`user_id`),
  INDEX `idx_action` (`action`),
  INDEX `idx_module` (`module`),
  INDEX `idx_status` (`status`),
  INDEX `idx_created_at` (`created_at`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci COMMENT='系统操作日志表';

-- ============================================
-- 添加外键约束
-- ============================================

ALTER TABLE `admin_users` 
  ADD CONSTRAINT `fk_admin_users_role` 
  FOREIGN KEY (`role_id`) REFERENCES `admin_roles`(`id`) 
  ON DELETE SET NULL ON UPDATE CASCADE;

-- ============================================
-- 创建视图：首页统计数据视图
-- ============================================

CREATE OR REPLACE VIEW `v_homepage_statistics` AS
SELECT 
  (SELECT COUNT(*) FROM `transaction_data` WHERE `is_deleted` = 0) AS total_projects,
  (SELECT COALESCE(SUM(`project_amount`), 0) FROM `transaction_data` WHERE `is_deleted` = 0) AS total_amount,
  (SELECT COUNT(DISTINCT `project_region`) FROM `transaction_data` WHERE `is_deleted` = 0) AS total_regions,
  (SELECT COUNT(*) FROM `government_procurement_notices` WHERE `is_deleted` = 0) AS gov_procurement_count,
  (SELECT COUNT(*) FROM `construction_project_notices` WHERE `is_deleted` = 0) AS construction_count;

-- ============================================
-- 创建视图：交易类型占比统计
-- ============================================

CREATE OR REPLACE VIEW `v_transaction_type_statistics` AS
SELECT 
  `project_type`,
  COUNT(*) AS project_count,
  COALESCE(SUM(`project_amount`), 0) AS total_amount,
  ROUND(COUNT(*) * 100.0 / (SELECT COUNT(*) FROM `transaction_data` WHERE `is_deleted` = 0), 2) AS percentage
FROM `transaction_data`
WHERE `is_deleted` = 0
GROUP BY `project_type`
ORDER BY project_count DESC;

-- ============================================
-- 创建视图：地区排行统计
-- ============================================

CREATE OR REPLACE VIEW `v_region_ranking_statistics` AS
SELECT 
  `project_region`,
  COUNT(*) AS project_count,
  COALESCE(SUM(`project_amount`), 0) AS total_amount
FROM `transaction_data`
WHERE `is_deleted` = 0
GROUP BY `project_region`
ORDER BY total_amount DESC, project_count DESC
LIMIT 10;

-- ============================================
-- 数据库结构创建完成
-- ============================================