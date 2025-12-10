# hailong-admin 与 HailongConsulting.API 对接指南

## 📋 目录

- [概述](#概述)
- [环境配置](#环境配置)
- [认证接口](#认证接口)
- [首页统计接口](#首页统计接口)
- [系统配置接口](#系统配置接口)
- [公告管理接口](#公告管理接口)
- [信息发布接口](#信息发布接口)
- [全局搜索接口](#全局搜索接口)
- [附件管理接口](#附件管理接口)
- [错误处理](#错误处理)

---

## 概述

本文档描述了 hailong-admin 前端管理系统与 HailongConsulting.API 后端接口的完整对接方案。

### 技术栈

**前端 (hailong-admin)**
- Vue 3 + Vite
- Element Plus
- Axios
- Pinia

**后端 (HailongConsulting.API)**
- ASP.NET Core 7.0
- Entity Framework Core
- MySQL
- JWT 认证

### 基础配置

**API 基础地址**
- 开发环境: `http://localhost:5000`
- 生产环境: 根据实际部署配置

**请求格式**
- Content-Type: `application/json`
- Authorization: `Bearer {token}`

**响应格式**
```json
{
  "success": true,
  "message": "操作成功",
  "data": {}
}
```

---

## 环境配置

### 前端配置

**文件位置**: `hailong-admin/.env.development`

```env
VITE_APP_TITLE=海隆咨询后台管理系统
VITE_API_BASE_URL=http://localhost:5000
VITE_APP_PORT=3000
```

### 后端配置

**文件位置**: `BackEnd/HailongConsulting.API/appsettings.json`

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3306;Database=hailong_consulting;User=root;Password=root;CharSet=utf8mb4;"
  },
  "Jwt": {
    "Key": "YourSuperSecretKeyForJWTTokenGeneration123456",
    "Issuer": "HailongConsulting",
    "Audience": "HailongConsultingUsers",
    "ExpireHours": 24
  }
}
```

### CORS 配置

后端已配置允许所有来源的跨域请求（开发环境）：

```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});
```

---

## 认证接口

### 1. 用户登录

**接口**: `POST /api/auth/login`

**请求参数**:
```json
{
  "username": "admin",
  "password": "password123"
}
```

**响应数据**:
```json
{
  "success": true,
  "message": "登录成功",
  "data": {
    "userId": 1,
    "username": "admin",
    "fullName": "管理员",
    "email": "admin@hailong.com",
    "role": "Admin",
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
    "refreshToken": "refresh_token_string",
    "expiresAt": "2024-01-02T10:00:00Z"
  }
}
```

**前端调用**:
```javascript
import { authApi } from '@/api'

const login = async (username, password) => {
  const response = await authApi.login({ username, password })
  return response.data
}
```

### 2. 刷新 Token

**接口**: `POST /api/auth/refresh`

**请求参数**:
```json
{
  "refreshToken": "refresh_token_string"
}
```

### 3. 获取当前用户信息

**接口**: `GET /api/auth/me`

**请求头**: `Authorization: Bearer {token}`

**响应数据**:
```json
{
  "success": true,
  "data": {
    "userId": 1,
    "username": "admin",
    "fullName": "管理员",
    "email": "admin@hailong.com",
    "phone": "13800138000",
    "role": "Admin",
    "lastLogin": "2024-01-01T10:00:00Z"
  }
}
```

### 4. 修改密码

**接口**: `POST /api/auth/change-password`

**请求参数**:
```json
{
  "oldPassword": "old_password",
  "newPassword": "new_password"
}
```

### 5. 登出

**接口**: `POST /api/auth/logout`

---

## 首页统计接口

### 1. 获取统计概览

**接口**: `GET /api/home/statistics/overview`

**响应数据**:
```json
{
  "success": true,
  "data": {
    "totalProjects": 1250,
    "totalAmount": 50000.00,
    "projectTypes": [
      {
        "type": "政府采购",
        "count": 750,
        "percentage": 60.00
      },
      {
        "type": "建设工程",
        "count": 500,
        "percentage": 40.00
      }
    ],
    "regionRanking": [
      {
        "region": "北京市",
        "projectCount": 300,
        "amount": 15000.00
      }
    ]
  }
}
```

**前端调用**:
```javascript
import { getStatisticsOverview } from '@/api/home'

const fetchStatistics = async () => {
  const response = await getStatisticsOverview()
  return response.data
}
```

### 2. 获取最新公告

**接口**: `GET /api/home/recent-announcements`

**响应数据**:
```json
{
  "success": true,
  "data": [
    {
      "id": 1,
      "title": "某项目招标公告",
      "noticeType": "招标公告",
      "projectRegion": "北京市",
      "publishTime": "2024-01-01T10:00:00Z",
      "sourceType": "政府采购"
    }
  ]
}
```

### 3. 获取重要业绩

**接口**: `GET /api/home/achievements`

**响应数据**:
```json
{
  "success": true,
  "data": [
    {
      "id": 1,
      "projectName": "某重点项目",
      "projectType": "咨询服务",
      "projectAmount": 1000.00,
      "clientName": "某政府部门",
      "completionDate": "2023-12-31",
      "description": "项目描述",
      "imageUrl": null
    }
  ]
}
```

---

## 系统配置接口

### 轮播图管理

#### 1. 获取所有轮播图

**接口**: `GET /api/config/banners`

**响应数据**:
```json
{
  "success": true,
  "data": [
    {
      "id": 1,
      "title": "轮播图标题",
      "description": "轮播图描述",
      "imageId": 100,
      "linkUrl": "https://example.com",
      "sortOrder": 1,
      "status": true,
      "createdAt": "2024-01-01T10:00:00Z",
      "updatedAt": "2024-01-01T10:00:00Z"
    }
  ]
}
```

**前端调用**:
```javascript
import { configApi } from '@/api'

const fetchBanners = async () => {
  const response = await configApi.getBanners()
  return response.data
}
```

#### 2. 创建轮播图

**接口**: `POST /api/config/banners`

**请求参数**:
```json
{
  "title": "轮播图标题",
  "description": "轮播图描述",
  "imageId": 100,
  "linkUrl": "https://example.com",
  "sortOrder": 1,
  "status": true
}
```

#### 3. 更新轮播图

**接口**: `PUT /api/config/banners/{id}`

**请求参数**:
```json
{
  "title": "更新后的标题",
  "status": false
}
```

#### 4. 删除轮播图

**接口**: `DELETE /api/config/banners/{id}`

### 企业简介管理

#### 1. 获取企业简介

**接口**: `GET /api/config/company-intro`

**响应数据**:
```json
{
  "success": true,
  "data": {
    "id": 1,
    "title": "企业简介",
    "content": "企业简介内容...",
    "highlights": ["亮点1", "亮点2"],
    "imageIds": [101, 102],
    "status": true,
    "createdAt": "2024-01-01T10:00:00Z",
    "updatedAt": "2024-01-01T10:00:00Z"
  }
}
```

#### 2. 更新企业简介

**接口**: `PUT /api/config/company-intro`

**请求参数**:
```json
{
  "title": "企业简介",
  "content": "更新后的内容...",
  "highlights": ["新亮点1", "新亮点2"],
  "imageIds": [101, 102, 103],
  "status": true
}
```

### 重要业绩管理

#### 1. 获取所有重要业绩

**接口**: `GET /api/config/achievements`

#### 2. 创建重要业绩

**接口**: `POST /api/config/achievements`

**请求参数**:
```json
{
  "projectName": "项目名称",
  "projectType": "项目类型",
  "projectAmount": 1000.00,
  "clientName": "客户名称",
  "completionDate": "2023-12-31",
  "description": "项目描述",
  "imageIds": [101, 102],
  "sortOrder": 1,
  "status": true
}
```

#### 3. 更新重要业绩

**接口**: `PUT /api/config/achievements/{id}`

#### 4. 删除重要业绩

**接口**: `DELETE /api/config/achievements/{id}`

### 企业荣誉管理

#### 1. 获取所有企业荣誉

**接口**: `GET /api/config/honors`

**响应数据**:
```json
{
  "success": true,
  "data": [
    {
      "id": 1,
      "name": "荣誉名称",
      "description": "荣誉描述",
      "imageId": 100,
      "awardOrganization": "颁发机构",
      "awardDate": "2023-12-31",
      "certificateNo": "证书编号",
      "honorLevel": "国家级",
      "sortOrder": 1,
      "status": true,
      "createdAt": "2024-01-01T10:00:00Z",
      "updatedAt": "2024-01-01T10:00:00Z"
    }
  ]
}
```

#### 2. 创建企业荣誉

**接口**: `POST /api/config/honors`

#### 3. 更新企业荣誉

**接口**: `PUT /api/config/honors/{id}`

#### 4. 删除企业荣誉

**接口**: `DELETE /api/config/honors/{id}`

### 友情链接管理

#### 1. 获取所有友情链接

**接口**: `GET /api/config/links`

**响应数据**:
```json
{
  "success": true,
  "data": [
    {
      "id": 1,
      "name": "链接名称",
      "url": "https://example.com",
      "logoId": 100,
      "description": "链接描述",
      "sortOrder": 1,
      "status": true,
      "createdAt": "2024-01-01T10:00:00Z",
      "updatedAt": "2024-01-01T10:00:00Z"
    }
  ]
}
```

**前端调用**:
```javascript
import { configApi } from '@/api'

const fetchLinks = async () => {
  const response = await configApi.getFriendlyLinks()
  return response.data
}
```

#### 2. 创建友情链接

**接口**: `POST /api/config/links`

**请求参数**:
```json
{
  "name": "链接名称",
  "url": "https://example.com",
  "logoId": 100,
  "description": "链接描述",
  "sortOrder": 1,
  "status": true
}
```

#### 3. 更新友情链接

**接口**: `PUT /api/config/links/{id}`

#### 4. 删除友情链接

**接口**: `DELETE /api/config/links/{id}`

### 访问统计

#### 1. 获取访问统计

**接口**: `GET /api/config/statistics`

**响应数据**:
```json
{
  "success": true,
  "data": {
    "totalVisits": 10000,
    "todayVisits": 150,
    "yesterdayVisits": 120,
    "thisMonthVisits": 3500
  }
}
```

#### 2. 记录访问

**接口**: `POST /api/config/statistics/record`

**请求参数**:
```json
{
  "pageUrl": "/home",
  "pageTitle": "首页",
  "referer": "https://google.com"
}
```

---

## 公告管理接口

### 1. 获取公告列表（分页）

**接口**: `GET /api/announcement`

**查询参数**:
- `pageNumber`: 页码（默认1）
- `pageSize`: 每页数量（默认10）
- `businessType`: 业务类型（GOV_PROCUREMENT/CONSTRUCTION）
- `noticeType`: 公告类型
- `keyword`: 搜索关键词
- `startDate`: 开始日期
- `endDate`: 结束日期

**响应数据**:
```json
{
  "success": true,
  "data": {
    "items": [
      {
        "id": 1,
        "title": "公告标题",
        "businessType": "GOV_PROCUREMENT",
        "noticeType": "招标公告",
        "projectName": "项目名称",
        "projectRegion": "北京市",
        "publishTime": "2024-01-01T10:00:00Z",
        "content": "公告内容..."
      }
    ],
    "totalCount": 100,
    "pageNumber": 1,
    "pageSize": 10,
    "totalPages": 10
  }
}
```

**前端调用**:
```javascript
import { govProcurementApi } from '@/api'

const fetchAnnouncements = async (params) => {
  const response = await govProcurementApi.getList(params)
  return response.data
}
```

### 2. 获取公告详情

**接口**: `GET /api/announcement/{id}`

### 3. 创建公告

**接口**: `POST /api/announcement`

**请求参数**:
```json
{
  "title": "公告标题",
  "businessType": "GOV_PROCUREMENT",
  "noticeType": "招标公告",
  "projectName": "项目名称",
  "projectRegion": "北京市",
  "publishTime": "2024-01-01T10:00:00Z",
  "content": "公告内容...",
  "attachmentIds": [1, 2, 3]
}
```

### 4. 更新公告

**接口**: `PUT /api/announcement/{id}`

### 5. 删除公告

**接口**: `DELETE /api/announcement/{id}`

---

## 信息发布接口

### 1. 获取信息列表（分页）

**接口**: `GET /api/info-publication`

**查询参数**:
- `pageNumber`: 页码
- `pageSize`: 每页数量
- `category`: 分类（COMPANY_NEWS/NOTICE/POLICY/POLICY_INFO）
- `keyword`: 搜索关键词
- `startDate`: 开始日期
- `endDate`: 结束日期

**响应数据**:
```json
{
  "success": true,
  "data": {
    "items": [
      {
        "id": 1,
        "title": "信息标题",
        "category": "COMPANY_NEWS",
        "summary": "摘要",
        "content": "内容...",
        "author": "作者",
        "publishTime": "2024-01-01T10:00:00Z",
        "viewCount": 100,
        "coverImageId": 100
      }
    ],
    "totalCount": 50,
    "pageNumber": 1,
    "pageSize": 10,
    "totalPages": 5
  }
}
```

**前端调用**:
```javascript
import { infoPublishApi } from '@/api'

const fetchInfoList = async (params) => {
  const response = await infoPublishApi.getList(params)
  return response.data
}
```

### 2. 获取信息详情

**接口**: `GET /api/info-publication/{id}`

### 3. 创建信息

**接口**: `POST /api/info-publication`

**请求参数**:
```json
{
  "title": "信息标题",
  "category": "COMPANY_NEWS",
  "summary": "摘要",
  "content": "内容...",
  "author": "作者",
  "publishTime": "2024-01-01T10:00:00Z",
  "coverImageId": 100,
  "attachmentIds": [1, 2]
}
```

### 4. 更新信息

**接口**: `PUT /api/info-publication/{id}`

### 5. 删除信息

**接口**: `DELETE /api/info-publication/{id}`

---

## 全局搜索接口

### 搜索所有内容

**接口**: `GET /api/search`

**查询参数**:
- `keyword`: 搜索关键词（必填）
- `pageNumber`: 页码
- `pageSize`: 每页数量

**响应数据**:
```json
{
  "success": true,
  "data": {
    "items": [
      {
        "id": 1,
        "title": "搜索结果标题",
        "type": "ANNOUNCEMENT",
        "summary": "摘要",
        "publishTime": "2024-01-01T10:00:00Z",
        "url": "/announcement/1"
      }
    ],
    "totalCount": 20,
    "pageNumber": 1,
    "pageSize": 10,
    "totalPages": 2
  }
}
```

---

## 附件管理接口

### 1. 上传附件

**接口**: `POST /api/attachment/upload`

**请求格式**: `multipart/form-data`

**请求参数**:
- `file`: 文件（必填）
- `category`: 分类（可选）

**响应数据**:
```json
{
  "success": true,
  "data": {
    "id": 100,
    "fileName": "document.pdf",
    "fileSize": 1024000,
    "fileType": "application/pdf",
    "filePath": "/uploads/2024/01/document.pdf",
    "uploadTime": "2024-01-01T10:00:00Z"
  }
}
```

**前端调用**:
```javascript
const uploadFile = async (file) => {
  const formData = new FormData()
  formData.append('file', file)
  
  const response = await request({
    url: '/api/attachment/upload',
    method: 'POST',
    data: formData,
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
  
  return response.data
}
```

### 2. 获取附件信息

**接口**: `GET /api/attachment/{id}`

### 3. 删除附件

**接口**: `DELETE /api/attachment/{id}`

---

## 错误处理

### 统一错误响应格式

```json
{
  "success": false,
  "message": "错误信息",
  "data": null
}
```

### HTTP 状态码

- `200`: 成功
- `400`: 请求参数错误
- `401`: 未授权（Token无效或过期）
- `403`: 禁止访问（权限不足）
- `404`: 资源不存在
- `500`: 服务器内部错误

### 前端错误处理

```javascript
// request.js 响应拦截器
request.interceptors.response.use(
  response => {
    const res = response.data
    
    if (res.success === false) {
      ElMessage.error(res.message || '请求失败')
      return Promise.reject(new Error(res.message || '请求失败'))
    }
    
    return res
  },
  error => {
    if (error.response?.status === 401) {
      ElMessage.error('登录已过期，请重新登录')
      tokenUtils.clearAuth()
      router.push('/login')
    } else if (error.response?.status === 403) {
      ElMessage.error('没有权限访问该资源')
    } else if (error.response?.status === 404) {
      ElMessage.error('请求的资源不存在')
    } else if (error.response?.status === 500) {
      ElMessage.error('服务器错误，请稍后重试')
    } else {
      const message = error.response?.data?.message || error.message || '网络错误'
      ElMessage.error(message)
    }
    
    return Promise.reject(error)
  }
)
```

---

## 快速开始

### 1. 启动后端服务

```bash
cd BackEnd/HailongConsulting.API
dotnet run
```

后端服务将在 `http://localhost:5000` 启动。

### 2. 启动前端服务

```bash
cd hailong-admin
npm install
npm run dev
```

前端服务将在 `http://localhost:3000` 启动。

### 3. 测试登录

使用默认管理员账号登录：
- 用户名: `admin`
- 密码: `admin123`（根据实际数据库配置）

---

## 注意事项

1. **Token 管理**: 前端会自动在请求头中添加 Token，Token 过期后会自动跳转到登录页
2. **文件上传**: 上传文件时需要使用 `multipart/form-data` 格式
3. **日期格式**: 所有日期使用 ISO 8601 格式（`YYYY-MM-DDTHH:mm:ssZ`）
4. **分页参数**: 页码从 1 开始
5. **软删除**: 删除操作为软删除，数据不会真正从数据库中删除

---

## 更新日志

### 2024-01-01
- 初始版本
- 完成所有核心接口对接
- 添加认证、配置、公告、信息发布等模块

---

## 技术支持

如有问题，请联系开发团队。