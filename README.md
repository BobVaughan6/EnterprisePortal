# 海隆咨询官网项目说明文档

## 📋 项目概述

**项目名称**: 海隆咨询官网（Hailong Consulting Official Website）

**项目定位**: 企业形象展示、业务资质宣传、公告信息发布、专业工具提供

**技术架构**: 前后端分离架构
- **前端**: Vue 3 + Vite + Tailwind CSS
- **后端**: .NET 7 Web API
- **数据库**: MySQL 8.0+

**开发周期**: 2025年

**目标用户**: 
- 政府采购人员
- 建设工程招标人
- 企业采购人员
- 行业从业人员

---

## 🎯 核心功能模块

### 1. 首页展示

#### 1.1 顶部导航
- 响应式导航栏
- 多级菜单支持
- 移动端汉堡菜单

#### 1.2 轮播Banner
- 可配置大图轮播
- 支持标题、副标题、跳转链接
- 自动播放 + 手动切换
- 后台管理支持排序、启用/禁用

**数据表**: `carousel_banners`

#### 1.3 企业简介
- 图文展示企业概况
- 支持富文本编辑
- 响应式布局

**数据表**: `company_profile`

#### 1.4 业务范围展示
- 多个业务类别卡片展示
- 图标 + 标题 + 描述
- Hover效果

**数据表**: `business_scope`

#### 1.5 交易数据可视化
使用图表库（ECharts/AntV）展示：
- 代理项目总数
- 代理总额统计
- 交易类型占比（饼图）
- 地区排行榜（柱状图/条形图）

**数据来源**: 
- `v_homepage_statistics` (首页统计视图)
- `v_transaction_type_statistics` (交易类型统计视图)
- `v_region_ranking_statistics` (地区排行统计视图)

#### 1.6 重要业绩展示
- 小图滚动展示
- 无限循环滚动
- 点击放大查看

**数据表**: `major_achievements`

#### 1.7 公告信息预览
- 最新公告列表
- 政府采购 + 建设工程分类显示
- 支持查看更多跳转

**数据表**: 
- `government_procurement_notices`
- `construction_project_notices`

#### 1.8 站内搜索
- 首页快速搜索入口
- 支持关键词、时间、区域筛选
- 跳转到搜索结果页

---

### 2. 关于海隆

#### 2.1 企业简介
- 公司历史沿革
- 企业文化理念
- 组织架构
- 图文结合展示

**数据表**: `company_profile`

#### 2.2 企业资质与业务范围
- 资质证书展示
- 业务领域介绍
- 服务范围说明
- 图文并茂

**数据表**: `business_scope`

#### 2.3 企业荣誉展示
- 荣誉证书墙
- 奖项列表
- 时间线展示

**数据表**: `company_honors`

---

### 3. 公告信息系统

#### 3.1 政府采购公告

**公告类型**:
- 采购公告
- 更正公告
- 结果公告

**字段结构**:
```
- 公告标题 (title)
- 招标人/采购人 (purchaser)
- 中标人 (winner)
- 发布人 (publisher)
- 访问量 (view_count) - 自动统计
- 发布时间 (publish_time)
- 项目区域 (project_region)
  - 省内 (省会、洛阳、南阳、新乡等17个地市)
  - 省外
- 公告内容 (content) - 富文本
- 附件 (attachments) - 可多个
```

**数据表**: `government_procurement_notices`

**功能特性**:
- 列表分页展示
- 多条件筛选（类型、区域、时间）
- 详情页查看（访问量+1）
- 附件下载
- 相关公告推荐

---

#### 3.2 建设工程公告

**公告类型**:
- 招标公告
- 中标公告
- 变更公告

**字段结构**:
```
- 公告标题 (title)
- 招标人 (tenderer)
- 中标人 (winner)
- 点击量 (click_count) - 自动统计
- 发布时间 (publish_time)
- 项目区域 (project_region)
  - 省内各地市
  - 省外
- 公告内容 (content) - 富文本
- 附件 (attachments)
```

**数据表**: `construction_project_notices`

**功能特性**:
- 与政府采购公告功能类似
- 独立的筛选和展示逻辑

---

### 4. 全局搜索功能

#### 4.1 搜索条件

**支持的筛选项**:
- ✅ 时间区间（开始时间 - 结束时间）
- ✅ 关键词（标题、招标人、中标人）
- ✅ 项目区域（省内各地市、省外）
- ✅ 业务类别（政府采购 / 建设工程）
- ✅ 公告类型（根据业务类别动态变化）

#### 4.2 搜索结果

**展示内容**:
- 标题（关键词高亮）
- 招标人/采购人
- 中标人
- 发布时间
- 项目区域
- 访问量/点击量

**功能**:
- 分页展示（默认10条/页）
- 排序（按时间、访问量）
- 结果统计（共XX条记录）
- 快速跳转详情

**实现**:
- 后端API: `/api/search`
- 支持模糊查询（LIKE）
- 多表联合查询
- 性能优化（索引）

---

### 5. 信息发布栏目

#### 5.1 公司公告
**用途**: 发布公司内部公告、通知
**数据表**: `company_announcements`
**字段**: 标题、内容、发布人、发布时间、访问量

#### 5.2 政策法规
**用途**: 发布行业相关法律法规
**数据表**: `policy_regulations`
**字段**: 标题、内容、文号、发布机关、实施日期、附件

#### 5.3 政策信息
**用途**: 发布政策解读、行业动态
**数据表**: `policy_information`
**字段**: 标题、内容、来源、发布时间

#### 5.4 通知公告
**用途**: 专家库录入邀请、重要通知
**数据表**: `notice_announcements`
**字段**: 标题、内容、类型、发布时间、截止时间

#### 5.5 专家库
**实现方式**: 外链到问卷星
**功能**:
- PC端：直接跳转问卷星链接
- 移动端：显示二维码扫码填写
- 后台可配置问卷星链接

**后台管理**:
所有信息发布栏目均支持完整的CRUD操作
- ✅ 创建（Create）
- ✅ 查询（Read）
- ✅ 更新（Update）
- ✅ 删除（Delete/软删除）

---

### 6. 实用工具模块

#### 6.1 招标代理费计算工具

**适用范围**:
- 工程类项目
- 货物类项目
- 服务类项目

**计算依据**: 
《河南省招标代理服务收费指导意见》（豫招协〔2023〕002号）

**功能特性**:
- 输入项目金额
- 选择项目类型（工程/货物/服务）
- 设置优惠比例（可选）
- 前端实时计算（无需后端）
- 显示计算过程和结果
- 显示收费标准表格
- 支持查看文件依据

**计算公式**（分档累进）:
```javascript
// 示例：工程类
if (金额 <= 100万) {
  费用 = 金额 × 1.5%
} else if (金额 <= 500万) {
  费用 = 100万 × 1.5% + (金额 - 100万) × 0.8%
}
// ... 更多档次
费用 = 费用 × (1 - 优惠比例)
```

**数据表**: `fee_calculation_standards` (存储收费标准配置)

---

#### 6.2 造价咨询费用计算工具

**功能特性**:
- 输入工程造价金额
- 选择工程类型
- 选择计费方式
- 前端实时计算
- 显示收费标准说明
- 提供标准文件下载

**计算依据**: 
豫价协〔2022〕6号文件

**数据表**: `costing_fee_standards`

---

#### 6.3 司法鉴定费用计算工具

**功能特性**:
- 输入鉴定标的金额
- 选择鉴定类型
- 分段累进计费
- 显示计算明细
- 提供计算示例

**计算依据**: 
《司法鉴定收费管理办法》

**分段计费示例**:
```
不超过10万元: 1000元
10万-50万: 2.5%
50万-100万: 1.5%
100万-200万: 1%
...
```

**实现**: 前端JavaScript计算 + 后端配置管理

**数据表**: `judicial_appraisal_standards`

---

### 7. 联系我们

**固定信息**:
- 📞 固定电话: 0371-55894666
- 📍 地址: 河南省郑州市郑东新区金水东路雅宝·东方国际广场2号楼13层
- 📧 邮箱: （可配置）
- 🕐 工作时间: （可配置）

**功能**:
- 在线留言表单
- 地图定位（嵌入百度/高德地图）
- 联系方式展示

---

### 8. 网站底部

#### 8.1 友情链接
**链接分类**:
- 省级单位（河南省政府采购网、河南省公共资源交易中心等）
- 地市级单位（郑州、洛阳、开封等各地市交易中心）
- 国家级单位（中国政府采购网、全国公共资源交易平台等）

**数据表**: `friendly_links`
**字段**: 链接名称、URL、排序、分类、是否新窗口打开

#### 8.2 访问统计
**功能**: 
- 总访问量统计（第XXX位访问者）
- 今日访问量
- 在线人数（可选）

**数据表**: `visit_statistics`

**实现方式**:
- IP去重统计
- Cookie/Session标识
- 定时更新缓存

#### 8.3 附件上传/下载
**支持格式**: 
- 文档: PDF, DOC, DOCX, XLS, XLSX
- 图片: JPG, PNG, GIF
- 压缩包: ZIP, RAR

**限制**:
- 单文件大小: 10MB
- 文件类型白名单验证
- 病毒扫描（可选）

---

## 🔧 后台管理系统（CMS）

### 管理员权限体系

**角色类型**:
- 超级管理员（Super Admin）- 所有权限
- 内容管理员（Content Manager）- 内容发布权限
- 审核员（Auditor）- 内容审核权限

**数据表**: 
- `admin_users` (管理员表)
- `admin_roles` (角色表)

**默认账号**:
```
用户名: admin
密码: 123456
角色: 超级管理员
```

⚠️ **安全提示**: 生产环境请立即修改默认密码！

---

### 后台功能模块

#### 1. 公告管理
**政府采购公告**:
- ✅ 新增公告（富文本编辑器）
- ✅ 编辑公告
- ✅ 删除公告（软删除）
- ✅ 批量操作
- ✅ 搜索筛选（标题、类型、时间、区域）
- ✅ 附件上传管理

**建设工程公告**:
- 功能同上

#### 2. 信息发布管理
- ✅ 公司公告管理
- ✅ 政策法规管理
- ✅ 政策信息管理
- ✅ 通知公告管理
- ✅ 富文本编辑
- ✅ 附件管理

#### 3. 网站配置管理
**轮播图管理**:
- 上传图片（推荐尺寸：1920x600）
- 设置标题、副标题、跳转链接
- 排序调整
- 启用/禁用

**企业信息管理**:
- 企业简介编辑
- 业务范围管理
- 企业荣誉管理
- 重要业绩管理

**友情链接管理**:
- 添加/编辑/删除链接
- 分类管理
- 排序调整

#### 4. 工具配置管理
- 代理费收费标准配置
- 造价费收费标准配置
- 司法鉴定费标准配置
- 附件上传（标准文件）
- 计算公式参数配置

#### 5. 统计分析
**访问统计**:
- 总访问量
- 日访问量趋势
- 页面访问排行
- 地域分布

**内容统计**:
- 公告访问量排行
- 热门搜索关键词
- 用户行为分析

#### 6. 系统管理
- 管理员账号管理
- 角色权限管理
- 操作日志查看
- 系统参数配置

---

## 🎨 前端技术实现

### 技术栈
```json
{
  "framework": "Vue 3",
  "buildTool": "Vite",
  "cssFramework": "Tailwind CSS",
  "router": "Vue Router",
  "stateManagement": "Pinia",
  "httpClient": "Axios",
  "charts": "ECharts / Apache ECharts",
  "richTextEditor": "Quill / TinyMCE",
  "uiComponents": "Element Plus / Ant Design Vue"
}
```

### 响应式设计
**断点设置** (基于Tailwind):
- `sm`: 640px - 手机横屏
- `md`: 768px - 平板
- `lg`: 1024px - 小屏电脑
- `xl`: 1280px - 标准电脑
- `2xl`: 1536px - 大屏

**适配策略**:
- 移动端优先（Mobile First）
- 弹性布局（Flexbox/Grid）
- 图片响应式（srcset）
- 导航自适应（汉堡菜单）

### 性能优化
- ✅ 路由懒加载
- ✅ 图片懒加载
- ✅ 组件按需引入
- ✅ Gzip压缩
- ✅ CDN加速
- ✅ 浏览器缓存策略

---

## ⚙️ 后端技术实现

### 技术架构

**.NET 7 Web API** 分层架构:

```
Controllers (控制器层)
    ↓ 调用
Services (业务逻辑层)
    ↓ 调用
Repositories (数据访问层)
    ↓ 使用
UnitOfWork (工作单元)
    ↓ 管理
DbContext (EF Core)
    ↓ 映射
Database (MySQL)
```

### 核心组件

#### 1. 控制器 (Controllers)
```
- AuthController - 认证授权
- ConfigController - 系统配置
- GovProcurementController - 政府采购公告
- ConstructionProjectController - 建设工程公告
- InfoPublishController - 信息发布
- SearchController - 全局搜索
- HomeController - 首页数据
```

#### 2. 服务层 (Services)
```
- IAuthService / AuthService - 认证服务
- IConfigService / ConfigService - 配置服务
- IGovProcurementService - 政府采购服务
- IConstructionProjectService - 建设工程服务
- IInfoPublishService - 信息发布服务
- ISearchService - 搜索服务
- IHomeService - 首页服务
- FileUploadService - 文件上传服务
```

#### 3. 仓储层 (Repositories)
```
- IRepository<T> - 通用仓储接口
- Repository<T> - 通用仓储实现
- IGovProcurementRepository - 政府采购仓储
- IConstructionProjectRepository - 建设工程仓储
- ... (其他专用仓储)
- IUnitOfWork - 工作单元接口
- UnitOfWork - 工作单元实现
```

### 统一响应格式

```csharp
public class ApiResponse<T>
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }
    public DateTime Timestamp { get; set; }
}
```

### 分页响应格式

```csharp
public class PagedResult<T>
{
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
    public bool HasPreviousPage { get; set; }
    public bool HasNextPage { get; set; }
    public List<T> Items { get; set; }
}
```

### 中间件

```
- ExceptionHandlingMiddleware - 全局异常处理
- JwtAuthenticationMiddleware - JWT认证
- RequestLoggingMiddleware - 请求日志记录
```

### 安全机制

#### 1. 认证 (Authentication)
- JWT Token认证
- Token过期时间: 7天
- Refresh Token机制

#### 2. 授权 (Authorization)
- 基于角色的访问控制（RBAC）
- 基于策略的授权
- 资源级权限控制

#### 3. 数据安全
- 密码SHA256加密
- SQL注入防护（参数化查询）
- XSS防护（输入验证）
- CSRF防护
- 文件上传类型验证

#### 4. 日志记录
**数据表**: `system_logs`
**记录内容**:
- 用户操作日志
- 异常错误日志
- 系统运行日志
- 安全审计日志

---

## 💾 数据库设计

### 数据库概览

**数据库名称**: `hailong_consulting`

**表统计**:
- 总表数: 18张
- 业务表: 15张
- 系统表: 3张
- 视图: 3个

### 核心数据表

#### 1. 用户权限管理 (2张表)
```sql
- admin_users (管理员账号表)
- admin_roles (角色权限表)
```

#### 2. 公告信息 (2张表)
```sql
- government_procurement_notices (政府采购公告)
- construction_project_notices (建设工程公告)
```

#### 3. 信息发布 (4张表)
```sql
- company_announcements (公司公告)
- policy_regulations (政策法规)
- policy_information (政策信息)
- notice_announcements (通知公告)
```

#### 4. 系统配置 (6张表)
```sql
- carousel_banners (轮播图)
- company_profile (企业简介)
- business_scope (业务范围)
- company_honors (企业荣誉)
- major_achievements (重要业绩)
- friendly_links (友情链接)
```

#### 5. 统计模块 (2张表)
```sql
- visit_statistics (访问统计)
- transaction_data (交易数据)
```

#### 6. 辅助模块 (2张表)
```sql
- region_dictionary (区域字典)
- system_logs (系统日志)
```

### 核心视图

```sql
-- 首页统计视图
v_homepage_statistics
  - 总代理项目数
  - 总代理金额
  - 各业务类型统计

-- 交易类型统计视图
v_transaction_type_statistics
  - 政府采购占比
  - 建设工程占比
  - 各类型金额统计

-- 地区排行统计视图
v_region_ranking_statistics
  - 各地市项目数量
  - 各地市项目金额
  - 排名统计
```

### 通用字段

所有业务表均包含以下审计字段:
```sql
created_at DATETIME DEFAULT CURRENT_TIMESTAMP
updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
is_deleted TINYINT(1) DEFAULT 0
```

### 索引设计

**核心索引**:
```sql
-- 公告表索引
idx_notice_type (notice_type)
idx_publish_time (publish_time)
idx_project_region (project_region)
idx_is_deleted (is_deleted)

-- 组合索引
idx_type_region_time (notice_type, project_region, publish_time)
```

**全文索引**:
```sql
-- 用于搜索优化
FULLTEXT idx_title (title)
FULLTEXT idx_content (content)
```

---

## 🚀 部署与运维

### 环境要求

#### 开发环境
```
- Node.js >= 18.0
- .NET SDK >= 7.0
- MySQL >= 8.0
- Visual Studio 2022 或 VS Code
```

#### 生产环境
```
- 服务器: CentOS 7+ / Ubuntu 20.04+
- Web服务器: Nginx
- 运行时: .NET Runtime 7.0
- 数据库: MySQL 8.0
```

### 部署流程

#### 1. 数据库部署
```bash
# 1. 创建数据库
mysql -u root -p < SQL/hailong_consulting_schema.sql

# 2. 导入初始数据
mysql -u root -p < SQL/hailong_consulting_init_data.sql

# 3. 验证
mysql -u root -p -e "USE hailong_consulting; SHOW TABLES;"
```

#### 2. 后端部署
```bash
# 1. 发布项目
cd BackEnd/HailongConsulting.API
dotnet publish -c Release -o ./publish

# 2. 配置appsettings.json
# 修改数据库连接字符串、JWT密钥等

# 3. 配置Nginx反向代理
# 监听80端口，转发到Kestrel (5000端口)

# 4. 配置systemd服务
# 创建hailong-api.service

# 5. 启动服务
sudo systemctl start hailong-api
sudo systemctl enable hailong-api
```

#### 3. 前端部署
```bash
# 1. 构建生产版本
cd FrondEnd
npm install
npm run build

# 2. 部署到Nginx
cp -r dist/* /var/www/hailong-consulting/

# 3. 配置Nginx
# 配置静态文件服务、路由重写、Gzip压缩
```

### Nginx配置示例

```nginx
# 前端站点
server {
    listen 80;
    server_name www.hailongzixun.com;
    
    root /var/www/hailong-consulting;
    index index.html;
    
    # Gzip压缩
    gzip on;
    gzip_types text/plain text/css application/json application/javascript;
    
    # SPA路由支持
    location / {
        try_files $uri $uri/ /index.html;
    }
    
    # API代理
    location /api/ {
        proxy_pass http://localhost:5000/api/;
        proxy_http_version 1.1;
        proxy_set_header Upgrade $http_upgrade;
        proxy_set_header Connection 'upgrade';
        proxy_set_header Host $host;
        proxy_cache_bypass $http_upgrade;
    }
    
    # 静态资源缓存
    location ~* \.(jpg|jpeg|png|gif|ico|css|js)$ {
        expires 30d;
    }
}
```

### 运维监控

**日志管理**:
```
- 应用日志: /logs/hailong-api-{Date}.log
- Nginx访问日志: /var/log/nginx/access.log
- Nginx错误日志: /var/log/nginx/error.log
- 系统日志: systemd journal
```

**性能监控**:
- CPU、内存使用率
- 数据库连接池状态
- API响应时间
- 错误率统计

**备份策略**:
```bash
# 数据库每日备份
0 2 * * * mysqldump -u root -p hailong_consulting > /backup/db_$(date +\%Y\%m\%d).sql

# 附件文件每周备份
0 3 * * 0 tar -czf /backup/uploads_$(date +\%Y\%m\%d).tar.gz /var/www/uploads/
```

---

## 📱 SEO优化

### 1. 技术SEO

**HTML结构**:
```html
<!DOCTYPE html>
<html lang="zh-CN">
<head>
    <meta charset="UTF-8">
    <title>海隆咨询 - 专业招标代理、造价咨询、司法鉴定服务</title>
    <meta name="description" content="河南海隆工程咨询有限公司...">
    <meta name="keywords" content="招标代理,造价咨询,司法鉴定,河南">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link rel="canonical" href="https://www.hailongzixun.com">
</head>
```

**语义化标签**:
- `<header>`, `<nav>`, `<main>`, `<article>`, `<section>`, `<footer>`
- 正确使用 `<h1>` ~ `<h6>` 层级
- 图片添加 `alt` 属性

**URL规范化**:
```
- 首页: /
- 关于我们: /about
- 政府采购: /procurement
- 建设工程: /construction
- 公告详情: /notice/[id]
- 搜索结果: /search?keyword=xxx
```

### 2. Sitemap生成

```xml
<?xml version="1.0" encoding="UTF-8"?>
<urlset xmlns="http://www.sitemaps.org/schemas/sitemap/0.9">
    <url>
        <loc>https://www.hailongzixun.com/</loc>
        <lastmod>2025-12-03</lastmod>
        <changefreq>daily</changefreq>
        <priority>1.0</priority>
    </url>
    <!-- 动态生成公告页面URL -->
</urlset>
```

### 3. Robots.txt

```
User-agent: *
Allow: /
Disallow: /admin/
Disallow: /api/

Sitemap: https://www.hailongzixun.com/sitemap.xml
```

### 4. 结构化数据 (Schema.org)

```json
{
  "@context": "https://schema.org",
  "@type": "Organization",
  "name": "河南海隆工程咨询有限公司",
  "url": "https://www.hailongzixun.com",
  "logo": "https://www.hailongzixun.com/logo.png",
  "contactPoint": {
    "@type": "ContactPoint",
    "telephone": "+86-371-55894666",
    "contactType": "customer service"
  }
}
```

---

## 📊 数据统计与分析

### 访问统计

**统计维度**:
- 总访问量（UV/PV）
- 日/周/月访问趋势
- 页面访问排行
- 地域分布
- 来源渠道
- 浏览器/设备统计

**实现方案**:
- 自建统计（visit_statistics表）
- 集成百度统计
- 集成Google Analytics（可选）

### 内容统计

**公告统计**:
- 发布数量统计
- 访问量排行
- 热门关键词
- 区域分布

**用户行为**:
- 搜索关键词分析
- 工具使用频次
- 附件下载统计
- 用户路径分析

---

## 🔐 安全规范

### 1. 认证授权
- ✅ JWT Token认证
- ✅ Token有效期管理
- ✅ 密码强度要求
- ✅ 登录失败限制
- ✅ 会话超时控制

### 2. 数据安全
- ✅ 密码加密存储（SHA256）
- ✅ 敏感数据传输加密（HTTPS）
- ✅ SQL注入防护
- ✅ XSS攻击防护
- ✅ CSRF防护

### 3. 文件安全
- ✅ 文件类型白名单
- ✅ 文件大小限制
- ✅ 文件名过滤
- ✅ 病毒扫描（可选）
- ✅ 存储路径隔离

### 4. 操作审计
- ✅ 管理员操作日志
- ✅ 数据变更记录
- ✅ 异常行为告警
- ✅ 日志定期备份

---

## 📚 参考文档

### 项目文档
- [项目总结](BackEnd/HailongConsulting.API/PROJECT_SUMMARY.md)
- [API测试指南](BackEnd/HailongConsulting.API/API_TEST_GUIDE.md)
- [配置API指南](BackEnd/HailongConsulting.API/CONFIG_API_GUIDE.md)
- [政府采购API示例](BackEnd/HailongConsulting.API/GOV_PROCUREMENT_API_EXAMPLES.md)
- [建设工程API示例](BackEnd/HailongConsulting.API/CONSTRUCTION_PROJECT_API_EXAMPLES.md)
- [全局搜索API指南](BackEnd/HailongConsulting.API/GLOBAL_SEARCH_API_GUIDE.md)
- [首页API指南](BackEnd/HailongConsulting.API/HOME_API_GUIDE.md)
- [信息发布API指南](BackEnd/HailongConsulting.API/INFO_PUBLISH_API_GUIDE.md)

### 数据库文档
- [数据库README](SQL/README.md)
- [数据字典](SQL/数据字典.md)
- [ER关系图说明](SQL/ER关系图说明.md)

### 行业标准文件
- 豫招协〔2023〕002号 - 河南省招标代理服务收费指导意见
- 豫价协〔2022〕6号 - 工程造价咨询服务收费标准
- 《司法鉴定收费管理办法》

---

## 👥 团队与支持

### 开发团队
- 后端开发: .NET Team
- 前端开发: Vue Team
- 数据库设计: DBA Team
- UI/UX设计: Design Team

### 技术支持
- 📧 技术支持邮箱: support@hailongzixun.com
- 📞 技术支持电话: 0371-55894666
- 🕐 支持时间: 工作日 9:00-18:00

---

## 📝 更新日志

### Version 1.0.0 (2025-12-03)
- ✅ 完成数据库设计与初始化
- ✅ 完成后端API基础架构
- ✅ 完成认证授权模块
- ✅ 完成公告信息管理模块
- ✅ 完成信息发布模块
- ✅ 完成全局搜索功能
- ✅ 完成系统配置管理
- ✅ 完成前端项目初始化
- ✅ 完成响应式布局框架
- 🔄 前端页面开发中...
- 🔄 工具模块开发中...

---

## 📄 许可证

本项目为海隆咨询官网专有项目，版权所有 © 2025 河南海隆工程咨询有限公司

---

## 🎯 下一步计划

### 短期目标（1-2周）
- [ ] 完成前端首页开发
- [ ] 完成公告列表页开发
- [ ] 完成公告详情页开发
- [ ] 完成搜索功能前端实现
- [ ] 完成后台管理界面

### 中期目标（1个月）
- [ ] 完成所有功能模块开发
- [ ] 完成三个计算工具开发
- [ ] 完成测试与BUG修复
- [ ] 完成性能优化
- [ ] 完成SEO优化

### 长期目标（2-3个月）
- [ ] 上线生产环境
- [ ] 收集用户反馈
- [ ] 功能迭代优化
- [ ] 移动端APP开发（可选）

---

**文档版本**: v1.0
**最后更新**: 2025年12月3日
**维护团队**: 海隆咨询技术部
