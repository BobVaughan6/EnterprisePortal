# 海隆咨询后台管理系统 - 模块重构指南

## 📋 概述

本文档说明了基于 `hailong_consulting_schema.sql` 数据库结构的模块重构方案。

**重构日期**: 2025-12-10  
**数据库版本**: hailong_consulting_schema.sql (2025-12-08)

---

## 🗂️ 新模块结构

### 1. 附件管理模块 (Attachments)
**数据库表**: `attachments`  
**路由**: `/attachments`  
**API文件**: `src/api/attachment.js`

#### 功能特性
- 统一管理所有附件和图片
- 支持图片、文档、视频等多种类型
- 关联到各业务模块（公告、信息发布、企业简介等）
- 批量上传和删除
- 附件分类管理

#### 主要字段
- `file_name`: 文件名称
- `file_path`: 文件路径
- `file_url`: 文件访问URL
- `file_size`: 文件大小
- `file_type`: 文件类型（MIME类型）
- `category`: 附件分类（image/document/video/other）
- `related_type`: 关联类型
- `related_id`: 关联记录ID

---

### 2. 公告管理模块 (Announcements)
**数据库表**: `announcements`  
**路由**: `/announcements/*`  
**API文件**: `src/api/announcement.js`

#### 子模块
1. **政府采购公告** (`/announcements/gov-procurement`)
   - `business_type`: GOV_PROCUREMENT
   - `procurement_type`: goods/service/project

2. **建设工程公告** (`/announcements/construction`)
   - `business_type`: CONSTRUCTION

#### 公告类型 (notice_type)
- `bidding`: 招标/采购公告
- `correction`: 更正公告
- `result`: 结果公告

#### 主要字段
- `title`: 公告标题
- `business_type`: 业务类型
- `notice_type`: 公告类型
- `procurement_type`: 采购类型（仅政府采购）
- `bidder`: 招标人
- `winner`: 中标人
- `budget_amount`: 预算金额
- `deadline`: 截止时间
- `province/city/district`: 省市区
- `project_region`: 项目区域
- `content`: 公告内容（富文本）
- `attachment_ids`: 附件ID列表（JSON数组）
- `is_top`: 是否置顶
- `view_count`: 浏览次数

---

### 3. 信息发布模块 (Info Publications)
**数据库表**: `info_publications`  
**路由**: `/info-publish/*`  
**API文件**: `src/api/infoPublication.js`

#### 子模块
1. **公司公告** (`/info-publish/company-news`)
   - `type`: COMPANY_NEWS
   - 二级分类：公司新闻/行业动态/通知公告

2. **政策法规** (`/info-publish/policy-regulation`)
   - `type`: POLICY_REGULATION
   - 二级分类：法律法规/部门规章/行政法规/地方政策

3. **政策信息** (`/info-publish/policy-info`)
   - `type`: POLICY_INFO

4. **通知公告** (`/info-publish/notice`)
   - `type`: NOTICE

#### 主要字段
- `type`: 信息类型
- `category`: 二级分类
- `title`: 标题
- `summary`: 摘要
- `content`: 内容（富文本）
- `cover_image_id`: 封面图片ID
- `author`: 作者
- `publisher`: 发布人
- `publish_time`: 发布时间
- `attachment_ids`: 附件ID列表（JSON数组）
- `is_top`: 是否置顶
- `view_count`: 浏览次数

---

### 4. 系统配置模块 (System Config)
**路由**: `/config/*`  
**API文件**: `src/api/systemConfig.js`

#### 子模块

##### 4.1 企业简介 (`/config/company-profile`)
**数据库表**: `company_profile`
- `title`: 简介标题
- `content`: 简介内容（富文本）
- `highlights`: 企业特色标签（JSON数组）
- `image_ids`: 配图ID列表（JSON数组）

##### 4.2 业务范围 (`/config/business-scope`)
**数据库表**: `business_scope`
- `name`: 业务名称
- `description`: 业务描述
- `content`: 详细内容（富文本）
- `features`: 业务特点（JSON数组）
- `image_id`: 业务图片ID
- `sort_order`: 排序顺序

##### 4.3 企业资质 (`/config/qualifications`)
**数据库表**: `company_qualifications`
- `name`: 资质名称
- `description`: 资质描述
- `image_id`: 资质证书图片ID
- `certificate_no`: 证书编号
- `issue_date`: 颁发日期
- `expiry_date`: 有效期至
- `sort_order`: 排序顺序

##### 4.4 重要业绩 (`/config/achievements`)
**数据库表**: `major_achievements`
- `project_name`: 项目名称
- `project_type`: 项目类型（工程/服务/货物）
- `project_amount`: 项目金额
- `client_name`: 客户名称
- `description`: 项目描述
- `completion_date`: 完成日期
- `image_ids`: 项目图片ID列表（JSON数组）
- `sort_order`: 排序顺序

##### 4.5 企业荣誉 (`/config/honors`)
**数据库表**: `company_honors`
- `name`: 荣誉名称
- `description`: 荣誉描述
- `image_id`: 荣誉证书图片ID
- `award_organization`: 颁发机构
- `award_date`: 获奖日期
- `certificate_no`: 证书编号
- `honor_level`: 荣誉级别
- `sort_order`: 排序顺序

##### 4.6 轮播图管理 (`/config/banners`)
**数据库表**: `carousel_banners`
- `title`: 轮播图标题
- `description`: 轮播图描述
- `image_id`: 轮播图图片ID
- `link_url`: 跳转链接
- `sort_order`: 排序顺序

##### 4.7 友情链接 (`/config/friendly-links`)
**数据库表**: `friendly_links`
- `name`: 链接名称
- `url`: 链接地址
- `logo_id`: Logo图片ID
- `description`: 链接描述
- `sort_order`: 排序顺序

---

### 5. 统计分析模块 (Statistics)
**路由**: `/statistics/*`  
**API文件**: `src/api/statistics.js`

#### 子模块

##### 5.1 访问统计 (`/statistics/visit`)
**数据库表**: `visit_statistics`
- 访问统计列表
- 访问统计概览
- 访问趋势分析
- 热门页面统计
- 访问记录

##### 5.2 公告统计 (`/statistics/announcement`)
- 公告统计概览
- 公告发布趋势
- 公告类型分布
- 公告区域分布
- 热门公告排行

---

### 6. 系统管理模块 (System)
**路由**: `/system/*`  
**API文件**: `src/api/system.js`

#### 子模块

##### 6.1 用户管理 (`/system/users`)
**数据库表**: `users`
- 用户列表
- 创建/编辑用户
- 重置密码
- 启用/禁用用户
- 角色管理（admin/user）

##### 6.2 系统日志 (`/system/logs`)
**数据库表**: `system_logs`
- 操作日志列表
- 日志详情查看
- 日志清理
- 日志导出

##### 6.3 区域字典 (`/system/regions`)
**数据库表**: `region_dictionary`
- 省市区三级结构
- 区域代码管理
- 批量导入/导出
- 区域排序

##### 6.4 修改密码 (`/system/change-password`)
- 当前用户修改密码

---

## 🔄 与旧版本的对比

### 主要变更

1. **统一公告管理**
   - 旧版：政府采购和建设工程分别管理
   - 新版：统一到 `announcements` 表，通过 `business_type` 区分

2. **统一信息发布**
   - 旧版：公司公告、政策法规等分散管理
   - 新版：统一到 `info_publications` 表，通过 `type` 区分

3. **附件统一管理**
   - 新增：`attachments` 表统一管理所有附件
   - 各业务模块通过 `attachment_ids` 关联

4. **完善的系统配置**
   - 新增：业务范围、企业资质、企业荣誉等模块
   - 增强：企业简介支持多图片和特色标签

5. **统计分析功能**
   - 新增：访问统计、公告统计等分析功能
   - 支持趋势分析和数据可视化

6. **系统管理增强**
   - 新增：用户管理、系统日志
   - 新增：区域字典（省市区三级）

---

## 📁 文件结构

```
hailong-admin/
├── src/
│   ├── api/
│   │   ├── attachment.js          # 附件管理API
│   │   ├── announcement.js        # 公告管理API
│   │   ├── infoPublication.js     # 信息发布API
│   │   ├── systemConfig.js        # 系统配置API
│   │   ├── statistics.js          # 统计分析API
│   │   ├── system.js              # 系统管理API
│   │   ├── auth.js                # 认证API
│   │   └── index.js               # API统一导出
│   │
│   ├── views/
│   │   ├── attachments/           # 附件管理视图
│   │   │   └── AttachmentList.vue
│   │   │
│   │   ├── announcements/         # 公告管理视图
│   │   │   ├── GovProcurement.vue
│   │   │   └── Construction.vue
│   │   │
│   │   ├── info-publish/          # 信息发布视图
│   │   │   ├── CompanyNews.vue
│   │   │   ├── PolicyRegulation.vue
│   │   │   ├── PolicyInfo.vue
│   │   │   └── Notice.vue
│   │   │
│   │   ├── config/                # 系统配置视图
│   │   │   ├── CompanyProfile.vue
│   │   │   ├── BusinessScope.vue
│   │   │   ├── Qualifications.vue
│   │   │   ├── Achievements.vue
│   │   │   ├── Honors.vue
│   │   │   ├── Banners.vue
│   │   │   └── FriendlyLinks.vue
│   │   │
│   │   ├── statistics/            # 统计分析视图
│   │   │   ├── VisitStatistics.vue
│   │   │   └── AnnouncementStatistics.vue
│   │   │
│   │   ├── system/                # 系统管理视图
│   │   │   ├── Users.vue
│   │   │   ├── SystemLogs.vue
│   │   │   ├── Regions.vue
│   │   │   └── ChangePassword.vue
│   │   │
│   │   ├── Dashboard.vue          # 数据看板
│   │   ├── Login.vue              # 登录页
│   │   └── Layout.vue             # 布局页
│   │
│   └── router/
│       └── index.js               # 路由配置
```

---

## 🚀 迁移步骤

### 1. 数据库迁移
```sql
-- 执行 SQL/hailong_consulting_schema.sql
-- 注意：需要先备份现有数据
```

### 2. 代码更新
- ✅ 已更新路由配置 (`src/router/index.js`)
- ✅ 已创建新的API文件
- ⏳ 需要创建/更新视图组件
- ⏳ 需要更新Sidebar组件以匹配新路由

### 3. 数据迁移
- 将旧的公告数据迁移到统一的 `announcements` 表
- 将旧的信息发布数据迁移到统一的 `info_publications` 表
- 迁移附件数据到 `attachments` 表

---

## 📝 开发注意事项

### 1. 附件关联
所有需要上传附件的模块，都应该：
1. 先上传附件到 `attachments` 表
2. 获取附件ID
3. 将附件ID存储到业务表的 `attachment_ids` 字段（JSON数组格式）

示例：
```javascript
// 上传附件
const attachmentIds = []
for (const file of files) {
  const result = await attachmentApi.uploadAttachment(file)
  attachmentIds.push(result.data.id)
}

// 保存业务数据
await announcementApi.createAnnouncement({
  title: '公告标题',
  content: '公告内容',
  attachmentIds: attachmentIds // [1, 2, 3]
})
```

### 2. 软删除
所有表都支持软删除（`is_deleted` 字段），删除操作应该：
- 设置 `is_deleted = 1`
- 保留数据用于审计和恢复

### 3. 状态管理
大部分表都有 `status` 字段：
- `0`: 禁用
- `1`: 启用

### 4. 置顶功能
公告和信息发布支持置顶（`is_top` 字段）：
- `0`: 不置顶
- `1`: 置顶

### 5. 排序功能
配置类模块支持排序（`sort_order` 字段）：
- 数值越小，排序越靠前
- 支持拖拽排序

---

## 🔗 相关文档

- [数据库设计文档](../SQL/hailong_consulting_schema.sql)
- [API集成指南](../API_INTEGRATION_GUIDE.md)
- [快速启动指南](../QUICK_START.md)
- [部署配置文档](../DEPLOYMENT_CONFIG.md)

---

## 📞 技术支持

如有问题，请联系开发团队或查看项目文档。

**最后更新**: 2025-12-10