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

-- 2.1 统一公告表（新版）
CREATE TABLE `announcements` (
  `id` INT UNSIGNED AUTO_INCREMENT PRIMARY KEY COMMENT '公告ID',
  `title` VARCHAR(255) NOT NULL COMMENT '公告标题',
  `business_type` VARCHAR(50) NOT NULL COMMENT '业务类型：GOV_PROCUREMENT-政府采购, CONSTRUCTION-建设工程',
  `notice_type` VARCHAR(50) NOT NULL COMMENT '公告类型：bidding-招标/采购公告, correction-更正公告, result-结果公告',
  `procurement_type` VARCHAR(50) DEFAULT NULL COMMENT '采购类型（仅政府采购）：goods-货物, service-服务, project-工程',
  `bidder` VARCHAR(255) DEFAULT NULL COMMENT '招标人',
  `winner` VARCHAR(255) DEFAULT NULL COMMENT '中标人',
  `province` VARCHAR(50) DEFAULT NULL COMMENT '省份',
  `city` VARCHAR(50) DEFAULT NULL COMMENT '城市',
  `district` VARCHAR(50) DEFAULT NULL COMMENT '区县',
  `project_region` VARCHAR(200) DEFAULT NULL COMMENT '项目区域（完整地址，用于显示和搜索）',
  `content` LONGTEXT NOT NULL COMMENT '公告内容（富文本）',
  `publisher` VARCHAR(50) DEFAULT NULL COMMENT '发布人',
  `publish_time` DATETIME DEFAULT NULL COMMENT '发布时间',
  `view_count` INT UNSIGNED DEFAULT 0 COMMENT '浏览次数',
  `attachment_path` VARCHAR(500) DEFAULT NULL COMMENT '附件路径',
  `is_top` TINYINT DEFAULT 0 COMMENT '是否置顶：0-否，1-是',
  `is_deleted` TINYINT DEFAULT 0 COMMENT '软删除：0-未删除，1-已删除',
  `created_at` DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `updated_at` DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '更新时间',
  INDEX `idx_business_type` (`business_type`),
  INDEX `idx_notice_type` (`notice_type`),
  INDEX `idx_province` (`province`),
  INDEX `idx_city` (`city`),
  INDEX `idx_district` (`district`),
  INDEX `idx_project_region` (`project_region`),
  INDEX `idx_publish_time` (`publish_time`),
  INDEX `idx_is_top` (`is_top`),
  INDEX `idx_is_deleted` (`is_deleted`),
  FULLTEXT INDEX `ft_title_content` (`title`, `content`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci COMMENT='统一公告表';

-- ============================================
-- 3. 信息发布模块
-- ============================================

-- 3.1 统一信息发布表（合并公司公告、政策法规、政策信息、通知公告）
CREATE TABLE `info_publications` (
  `id` INT UNSIGNED AUTO_INCREMENT PRIMARY KEY COMMENT '信息ID',
  `type` VARCHAR(50) NOT NULL COMMENT '信息类型：COMPANY_NEWS-公司公告, POLICY_REGULATION-政策法规, POLICY_INFO-政策信息, NOTICE-通知公告',
  `title` VARCHAR(255) NOT NULL COMMENT '标题',
  `content` LONGTEXT NOT NULL COMMENT '内容（富文本）',
  `publish_time` DATETIME DEFAULT NULL COMMENT '发布时间',
  `view_count` INT UNSIGNED DEFAULT 0 COMMENT '访问量',
  `attachment_path` VARCHAR(500) DEFAULT NULL COMMENT '附件路径',
  `is_top` TINYINT DEFAULT 0 COMMENT '是否置顶：0-否，1-是',
  `is_deleted` TINYINT DEFAULT 0 COMMENT '软删除：0-未删除，1-已删除',
  `created_at` DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `updated_at` DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '更新时间',
  INDEX `idx_type` (`type`),
  INDEX `idx_publish_time` (`publish_time`),
  INDEX `idx_is_top` (`is_top`),
  INDEX `idx_is_deleted` (`is_deleted`),
  FULLTEXT INDEX `ft_title_content` (`title`, `content`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci COMMENT='统一信息发布表';

-- ============================================
-- 4. 系统配置模块
-- ============================================

-- 4.1 企业简介表
CREATE TABLE `company_profile` (
  `id` INT UNSIGNED AUTO_INCREMENT PRIMARY KEY COMMENT '简介ID',
  `title` VARCHAR(255) NOT NULL COMMENT '简介标题',
  `content` LONGTEXT NOT NULL COMMENT '简介内容（富文本）',
  `highlights` TEXT DEFAULT NULL COMMENT '企业特色标签（JSON数组格式，如：["专业资质齐全","经验丰富团队"]）',
  `image_url` VARCHAR(500) DEFAULT NULL COMMENT '配图URL',
  `is_deleted` TINYINT DEFAULT 0 COMMENT '软删除：0-未删除，1-已删除',
  `created_at` DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `updated_at` DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '更新时间',
  INDEX `idx_is_deleted` (`is_deleted`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci COMMENT='企业简介表';

-- 4.2 业务范围表
CREATE TABLE `business_scope` (
  `id` INT UNSIGNED AUTO_INCREMENT PRIMARY KEY COMMENT '业务ID',
  `name` VARCHAR(255) NOT NULL COMMENT '业务名称',
  `description` TEXT DEFAULT NULL COMMENT '业务描述',
  `features` TEXT DEFAULT NULL COMMENT '业务特点（JSON数组格式，如：["采购需求编制","招标文件制作"]）',
  `image` VARCHAR(500) DEFAULT NULL COMMENT '业务图片URL',
  `sort_order` INT DEFAULT 0 COMMENT '排序顺序',
  `is_deleted` TINYINT DEFAULT 0 COMMENT '软删除：0-未删除，1-已删除',
  `created_at` DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `updated_at` DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '更新时间',
  INDEX `idx_sort_order` (`sort_order`),
  INDEX `idx_is_deleted` (`is_deleted`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci COMMENT='业务范围表';

-- 4.3 企业资质表（原企业荣誉表，用于存储资质证书信息）
CREATE TABLE `company_qualifications` (
  `id` INT UNSIGNED AUTO_INCREMENT PRIMARY KEY COMMENT '资质ID',
  `name` VARCHAR(255) NOT NULL COMMENT '资质名称',
  `description` TEXT DEFAULT NULL COMMENT '资质描述',
  `image` VARCHAR(500) DEFAULT NULL COMMENT '资质证书图片URL',
  `certificate_no` VARCHAR(100) DEFAULT NULL COMMENT '证书编号',
  `issue_date` DATE DEFAULT NULL COMMENT '颁发日期',
  `expiry_date` DATE DEFAULT NULL COMMENT '有效期至',
  `sort_order` INT DEFAULT 0 COMMENT '排序顺序',
  `is_deleted` TINYINT DEFAULT 0 COMMENT '软删除：0-未删除，1-已删除',
  `created_at` DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `updated_at` DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '更新时间',
  INDEX `idx_issue_date` (`issue_date`),
  INDEX `idx_sort_order` (`sort_order`),
  INDEX `idx_is_deleted` (`is_deleted`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci COMMENT='企业资质表';

-- 4.4 重要业绩表（企业荣誉展示）
CREATE TABLE `major_achievements` (
  `id` INT UNSIGNED AUTO_INCREMENT PRIMARY KEY COMMENT '业绩ID',
  `project_name` VARCHAR(255) NOT NULL COMMENT '项目名称',
  `project_type` VARCHAR(50) NOT NULL COMMENT '项目类型：工程、服务、货物',
  `project_amount` DECIMAL(15,2) DEFAULT NULL COMMENT '项目金额（万元）',
  `client_name` VARCHAR(255) DEFAULT NULL COMMENT '客户名称',
  `description` TEXT DEFAULT NULL COMMENT '项目描述',
  `completion_date` DATE DEFAULT NULL COMMENT '完成日期',
  `image_url` VARCHAR(500) DEFAULT NULL COMMENT '项目图片URL',
  `sort_order` INT DEFAULT 0 COMMENT '排序顺序',
  `is_deleted` TINYINT DEFAULT 0 COMMENT '软删除：0-未删除，1-已删除',
  `created_at` DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `updated_at` DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '更新时间',
  INDEX `idx_project_type` (`project_type`),
  INDEX `idx_completion_date` (`completion_date`),
  INDEX `idx_sort_order` (`sort_order`),
  INDEX `idx_is_deleted` (`is_deleted`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci COMMENT='重要业绩表（企业荣誉展示）';

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


-- ============================================
-- 6. 项目区域字典表（省市区三级结构）
-- ============================================

CREATE TABLE `region_dictionary` (
  `id` INT UNSIGNED AUTO_INCREMENT PRIMARY KEY COMMENT '区域ID',
  `region_code` VARCHAR(50) NOT NULL COMMENT '区域代码（如：410000、410100、410101）',
  `region_name` VARCHAR(50) NOT NULL COMMENT '区域名称',
  `region_level` TINYINT NOT NULL COMMENT '区域级别：1-省份，2-城市，3-区县',
  `parent_code` VARCHAR(50) DEFAULT NULL COMMENT '父级区域代码',
  `sort_order` INT DEFAULT 0 COMMENT '排序顺序',
  `is_deleted` TINYINT DEFAULT 0 COMMENT '软删除：0-未删除，1-已删除',
  `created_at` DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `updated_at` DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '更新时间',
  UNIQUE INDEX `idx_region_code` (`region_code`),
  INDEX `idx_region_level` (`region_level`),
  INDEX `idx_parent_code` (`parent_code`),
  INDEX `idx_sort_order` (`sort_order`),
  INDEX `idx_is_deleted` (`is_deleted`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci COMMENT='项目区域字典表（省市区三级）';

-- 插入河南省区域数据（基于city.json）
INSERT INTO `region_dictionary` (`region_code`, `region_name`, `region_level`, `parent_code`, `sort_order`) VALUES
-- 省份
('410000', '河南省', 1, NULL, 1),
-- 城市
('410100', '郑州市', 2, '410000', 1),
('410200', '开封市', 2, '410000', 2),
('410300', '洛阳市', 2, '410000', 3),
('410400', '平顶山市', 2, '410000', 4),
('410500', '安阳市', 2, '410000', 5),
('410600', '鹤壁市', 2, '410000', 6),
('410700', '新乡市', 2, '410000', 7),
('410800', '焦作市', 2, '410000', 8),
('410900', '濮阳市', 2, '410000', 9),
('411000', '许昌市', 2, '410000', 10),
('411100', '漯河市', 2, '410000', 11),
('411200', '三门峡市', 2, '410000', 12),
('411300', '南阳市', 2, '410000', 13),
('411400', '商丘市', 2, '410000', 14),
('411500', '信阳市', 2, '410000', 15),
('411600', '周口市', 2, '410000', 16),
('411700', '驻马店市', 2, '410000', 17),
('419001', '济源市', 2, '410000', 18),
-- 郑州市区县
('410101', '郑州市区', 3, '410100', 1),
('410122', '中牟县', 3, '410100', 2),
('410182', '荥阳市', 3, '410100', 3),
('410183', '新郑市', 3, '410100', 4),
('410184', '新密市', 3, '410100', 5),
('410185', '登封市', 3, '410100', 6);
-- 其他城市的区县数据可以根据需要继续添加...

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
-- 创建视图：首页统计数据视图（基于统一公告表）
-- ============================================

CREATE OR REPLACE VIEW `v_homepage_statistics` AS
SELECT
  (SELECT COUNT(*) FROM `announcements` WHERE `is_deleted` = 0) AS total_projects,
  (SELECT COUNT(DISTINCT CONCAT_WS('-', `province`, `city`, `district`)) FROM `announcements` WHERE `is_deleted` = 0) AS total_regions,
  (SELECT COUNT(*) FROM `announcements` WHERE `business_type` = 'GOV_PROCUREMENT' AND `is_deleted` = 0) AS gov_procurement_count,
  (SELECT COUNT(*) FROM `announcements` WHERE `business_type` = 'CONSTRUCTION' AND `is_deleted` = 0) AS construction_count;

-- ============================================
-- 数据库结构创建完成
-- ============================================