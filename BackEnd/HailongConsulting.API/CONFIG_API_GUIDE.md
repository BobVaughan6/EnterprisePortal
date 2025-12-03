# 系统配置管理API文档

## 概述

系统配置管理API提供了轮播图、企业简介、重要业绩、友情链接和访问统计的管理功能。

## 数据库表映射

### 1. 轮播图 (carousel_banners)

| 数据库字段 | 类型 | DTO字段 | 说明 |
|-----------|------|---------|------|
| id | INT UNSIGNED | Id | 轮播图ID |
| title | VARCHAR(255) | Title | 轮播图标题 |
| image_url | VARCHAR(500) | ImageUrl | 图片URL（必填） |
| link_url | VARCHAR(500) | LinkUrl | 跳转链接 |
| sort_order | INT | SortOrder | 排序顺序（数字越小越靠前） |
| status | TINYINT | Status | 状态：0-禁用，1-启用 |
| is_deleted | TINYINT | - | 软删除标记 |
| created_at | DATETIME | CreatedAt | 创建时间 |
| updated_at | DATETIME | UpdatedAt | 更新时间 |

### 2. 企业简介 (company_profile)

| 数据库字段 | 类型 | DTO字段 | 说明 |
|-----------|------|---------|------|
| id | INT UNSIGNED | Id | 简介ID |
| title | VARCHAR(255) | Title | 简介标题（必填） |
| content | LONGTEXT | Content | 简介内容（富文本，必填） |
| image_url | VARCHAR(500) | ImageUrl | 配图URL |
| is_deleted | TINYINT | - | 软删除标记 |
| created_at | DATETIME | CreatedAt | 创建时间 |
| updated_at | DATETIME | UpdatedAt | 更新时间 |

### 3. 重要业绩 (major_achievements)

| 数据库字段 | 类型 | DTO字段 | 说明 |
|-----------|------|---------|------|
| id | INT UNSIGNED | Id | 业绩ID |
| project_name | VARCHAR(255) | ProjectName | 项目名称（必填） |
| project_type | VARCHAR(50) | ProjectType | 项目类型 |
| project_amount | DECIMAL(15,2) | ProjectAmount | 项目金额（万元） |
| client_name | VARCHAR(255) | ClientName | 客户名称 |
| completion_date | DATE | CompletionDate | 完成日期 |
| description | TEXT | Description | 项目描述 |
| image_url | VARCHAR(500) | ImageUrl | 项目图片URL |
| sort_order | INT | SortOrder | 排序顺序 |
| is_deleted | TINYINT | - | 软删除标记 |
| created_at | DATETIME | CreatedAt | 创建时间 |
| updated_at | DATETIME | UpdatedAt | 更新时间 |

### 4. 友情链接 (friendly_links)

| 数据库字段 | 类型 | DTO字段 | 说明 |
|-----------|------|---------|------|
| id | INT UNSIGNED | Id | 链接ID |
| link_name | VARCHAR(100) | LinkName | 链接名称（必填） |
| link_url | VARCHAR(500) | LinkUrl | 链接URL（必填） |
| logo_url | VARCHAR(500) | LogoUrl | Logo URL |
| sort_order | INT | SortOrder | 排序顺序 |
| status | TINYINT | Status | 状态：0-禁用，1-启用 |
| is_deleted | TINYINT | - | 软删除标记 |
| created_at | DATETIME | CreatedAt | 创建时间 |
| updated_at | DATETIME | UpdatedAt | 更新时间 |

### 5. 访问统计 (visit_statistics)

| 数据库字段 | 类型 | DTO字段 | 说明 |
|-----------|------|---------|------|
| id | INT UNSIGNED | Id | 统计ID |
| visit_date | DATE | - | 访问日期 |
| page_url | VARCHAR(500) | PageUrl | 页面URL |
| page_title | VARCHAR(255) | PageTitle | 页面标题 |
| visitor_ip | VARCHAR(50) | - | 访问者IP（自动获取） |
| user_agent | VARCHAR(500) | - | 用户代理（自动获取） |
| referer | VARCHAR(500) | Referer | 来源页面 |
| visit_count | INT UNSIGNED | - | 访问次数 |
| is_deleted | TINYINT | - | 软删除标记 |
| created_at | DATETIME | - | 创建时间 |
| updated_at | DATETIME | - | 更新时间 |

## API端点

### 轮播图管理

#### 1. 获取所有轮播图
```http
GET /api/config/banners
```

**响应示例：**
```json
{
  "success": true,
  "message": "获取轮播图列表成功",
  "data": [
    {
      "id": 1,
      "title": "欢迎访问海隆咨询",
      "imageUrl": "/uploads/banner1.jpg",
      "linkUrl": "https://example.com",
      "sortOrder": 1,
      "status": true,
      "createdAt": "2024-01-01T10:00:00",
      "updatedAt": "2024-01-01T10:00:00"
    }
  ],
  "timestamp": "2024-01-01T10:00:00"
}
```

#### 2. 根据ID获取轮播图
```http
GET /api/config/banners/{id}
```

#### 3. 创建轮播图（需要认证）
```http
POST /api/config/banners
Authorization: Bearer {token}
Content-Type: application/json

{
  "title": "新轮播图",
  "imageUrl": "/uploads/banner.jpg",
  "linkUrl": "https://example.com",
  "sortOrder": 1,
  "status": true
}
```

#### 4. 更新轮播图（需要认证）
```http
PUT /api/config/banners/{id}
Authorization: Bearer {token}
Content-Type: application/json

{
  "title": "更新后的标题",
  "sortOrder": 2,
  "status": false
}
```

**注意：** 更新时只需提供要修改的字段，未提供的字段保持不变。

#### 5. 删除轮播图（需要认证）
```http
DELETE /api/config/banners/{id}
Authorization: Bearer {token}
```

### 企业简介管理

#### 1. 获取企业简介
```http
GET /api/config/company-intro
```

**响应示例：**
```json
{
  "success": true,
  "message": "获取企业简介成功",
  "data": {
    "id": 1,
    "title": "关于海隆咨询",
    "content": "<p>企业简介内容...</p>",
    "imageUrl": "/uploads/company.jpg",
    "createdAt": "2024-01-01T10:00:00",
    "updatedAt": "2024-01-01T10:00:00"
  },
  "timestamp": "2024-01-01T10:00:00"
}
```

#### 2. 更新企业简介（需要认证）
```http
PUT /api/config/company-intro
Authorization: Bearer {token}
Content-Type: application/json

{
  "title": "关于海隆咨询",
  "content": "<p>更新后的企业简介...</p>",
  "imageUrl": "/uploads/company.jpg"
}
```

### 重要业绩管理

#### 1. 获取所有重要业绩
```http
GET /api/config/achievements
```

**响应示例：**
```json
{
  "success": true,
  "message": "获取重要业绩列表成功",
  "data": [
    {
      "id": 1,
      "projectName": "某大型项目",
      "projectType": "政府采购",
      "projectAmount": 5000.00,
      "clientName": "某政府单位",
      "completionDate": "2024-01-01",
      "description": "项目描述...",
      "imageUrl": "/uploads/project1.jpg",
      "sortOrder": 1,
      "createdAt": "2024-01-01T10:00:00",
      "updatedAt": "2024-01-01T10:00:00"
    }
  ],
  "timestamp": "2024-01-01T10:00:00"
}
```

#### 2. 根据ID获取重要业绩
```http
GET /api/config/achievements/{id}
```

#### 3. 创建重要业绩（需要认证）
```http
POST /api/config/achievements
Authorization: Bearer {token}
Content-Type: application/json

{
  "projectName": "新项目",
  "projectType": "建设工程",
  "projectAmount": 3000.00,
  "clientName": "某企业",
  "completionDate": "2024-06-01",
  "description": "项目描述...",
  "imageUrl": "/uploads/project.jpg",
  "sortOrder": 1
}
```

#### 4. 更新重要业绩（需要认证）
```http
PUT /api/config/achievements/{id}
Authorization: Bearer {token}
Content-Type: application/json

{
  "projectAmount": 3500.00,
  "description": "更新后的描述..."
}
```

#### 5. 删除重要业绩（需要认证）
```http
DELETE /api/config/achievements/{id}
Authorization: Bearer {token}
```

### 友情链接管理

#### 1. 获取所有友情链接
```http
GET /api/config/links
```

**响应示例：**
```json
{
  "success": true,
  "message": "获取友情链接列表成功",
  "data": [
    {
      "id": 1,
      "linkName": "合作伙伴",
      "linkUrl": "https://partner.com",
      "logoUrl": "/uploads/logo1.png",
      "sortOrder": 1,
      "status": true,
      "createdAt": "2024-01-01T10:00:00",
      "updatedAt": "2024-01-01T10:00:00"
    }
  ],
  "timestamp": "2024-01-01T10:00:00"
}
```

#### 2. 根据ID获取友情链接
```http
GET /api/config/links/{id}
```

#### 3. 创建友情链接（需要认证）
```http
POST /api/config/links
Authorization: Bearer {token}
Content-Type: application/json

{
  "linkName": "新合作伙伴",
  "linkUrl": "https://newpartner.com",
  "logoUrl": "/uploads/logo.png",
  "sortOrder": 1,
  "status": true
}
```

#### 4. 更新友情链接（需要认证）
```http
PUT /api/config/links/{id}
Authorization: Bearer {token}
Content-Type: application/json

{
  "linkName": "更新后的名称",
  "status": false
}
```

#### 5. 删除友情链接（需要认证）
```http
DELETE /api/config/links/{id}
Authorization: Bearer {token}
```

### 访问统计

#### 1. 获取网站访问统计
```http
GET /api/config/statistics
```

**响应示例：**
```json
{
  "success": true,
  "message": "获取访问统计成功",
  "data": {
    "totalVisits": 10000,
    "todayVisits": 150,
    "yesterdayVisits": 120,
    "thisMonthVisits": 3500
  },
  "timestamp": "2024-01-01T10:00:00"
}
```

#### 2. 记录访问
```http
POST /api/config/statistics/record
Content-Type: application/json

{
  "pageUrl": "/home",
  "pageTitle": "首页",
  "referer": "https://google.com"
}
```

**注意：** 访问者IP和User-Agent会自动从请求头中获取。

## 字段说明

### 排序字段 (sort_order)
- 数据库字段名：`sort_order`
- 类型：INT
- 说明：数字越小，排序越靠前
- 默认值：0

### 状态字段 (status)
- 数据库字段名：`status`
- 数据库类型：TINYINT (0或1)
- DTO类型：bool (true或false)
- 说明：
  - 数据库：0-禁用，1-启用
  - API：false-禁用，true-启用

### 软删除字段 (is_deleted)
- 数据库字段名：`is_deleted`
- 类型：TINYINT (0或1)
- 说明：0-未删除，1-已删除
- 注意：API不直接暴露此字段，通过DELETE接口进行软删除

## 认证说明

标记为"需要认证"的接口需要在请求头中包含JWT令牌：

```http
Authorization: Bearer {your_jwt_token}
```

获取令牌请参考认证API文档。

## 错误响应

所有API在发生错误时返回统一格式：

```json
{
  "success": false,
  "message": "错误描述",
  "data": null,
  "timestamp": "2024-01-01T10:00:00"
}
```

常见HTTP状态码：
- 200: 成功
- 400: 请求参数错误
- 401: 未认证
- 404: 资源不存在
- 500: 服务器内部错误

## 使用示例

### 示例1：获取并显示轮播图

```javascript
// 获取轮播图列表
fetch('/api/config/banners')
  .then(response => response.json())
  .then(result => {
    if (result.success) {
      const banners = result.data.filter(b => b.status);
      // 显示启用的轮播图
      displayBanners(banners);
    }
  });
```

### 示例2：记录页面访问

```javascript
// 页面加载时记录访问
window.addEventListener('load', () => {
  fetch('/api/config/statistics/record', {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json'
    },
    body: JSON.stringify({
      pageUrl: window.location.pathname,
      pageTitle: document.title,
      referer: document.referrer
    })
  });
});
```

### 示例3：管理员更新企业简介

```javascript
// 更新企业简介（需要管理员权限）
const token = localStorage.getItem('jwt_token');

fetch('/api/config/company-intro', {
  method: 'PUT',
  headers: {
    'Content-Type': 'application/json',
    'Authorization': `Bearer ${token}`
  },
  body: JSON.stringify({
    title: '关于我们',
    content: '<p>更新后的企业简介内容...</p>',
    imageUrl: '/uploads/company-new.jpg'
  })
})
.then(response => response.json())
.then(result => {
  if (result.success) {
    alert('更新成功');
  }
});
```

## 注意事项

1. **排序规则**：所有列表接口返回的数据已按 `sort_order` 升序排序
2. **软删除**：删除操作不会真正删除数据，只是标记为已删除
3. **企业简介**：系统中只保留一条企业简介记录，更新时会自动处理
4. **访问统计**：同一天同一页面的访问会累加计数，不会创建重复记录
5. **图片URL**：建议使用相对路径，便于部署到不同环境
6. **富文本内容**：企业简介的content字段支持HTML格式
7. **日期格式**：完成日期使用ISO 8601格式（YYYY-MM-DD）
8. **金额单位**：项目金额单位为万元，保留2位小数

## 数据库索引

为提高查询性能，以下字段已建立索引：
- 轮播图：sort_order, status, is_deleted
- 企业简介：is_deleted
- 重要业绩：completion_date, sort_order, is_deleted
- 友情链接：sort_order, status, is_deleted
- 访问统计：visit_date, page_url, visitor_ip, is_deleted