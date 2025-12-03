# API 调用示例文档

本文档提供了基于实际后端接口的 API 调用示例。

## 目录

1. [认证相关 API](#认证相关-api)
2. [政府采购公告 API](#政府采购公告-api)
3. [建设工程公告 API](#建设工程公告-api)
4. [信息发布 API](#信息发布-api)
5. [系统配置 API](#系统配置-api)

---

## 认证相关 API

### 1. 登录

**接口**: `POST /api/auth/login`

**请求参数**:
```javascript
{
  username: "admin",      // 必填：用户名
  password: "123456"      // 必填：密码
}
```

**调用示例**:
```javascript
import { authApi } from '@/api'

const login = async () => {
  try {
    const res = await authApi.login({
      username: 'admin',
      password: '123456'
    })
    
    if (res.success) {
      console.log('Token:', res.data.token)
      console.log('用户信息:', res.data)
      // res.data 包含:
      // {
      //   userId: 1,
      //   username: "admin",
      //   fullName: "管理员",
      //   email: "admin@example.com",
      //   role: "admin",
      //   token: "eyJhbGciOiJIUzI1NiIs...",
      //   refreshToken: "...",
      //   expiresAt: "2024-12-04T10:30:00"
      // }
    }
  } catch (error) {
    console.error('登录失败:', error)
  }
}
```

### 2. 获取当前用户信息

**接口**: `GET /api/auth/me`

**调用示例**:
```javascript
import { authApi } from '@/api'

const getCurrentUser = async () => {
  try {
    const res = await authApi.getCurrentUser()
    if (res.success) {
      console.log('当前用户:', res.data)
    }
  } catch (error) {
    console.error('获取用户信息失败:', error)
  }
}
```

### 3. 修改密码

**接口**: `POST /api/auth/change-password`

**请求参数**:
```javascript
{
  oldPassword: "123456",     // 必填：旧密码
  newPassword: "newpass123"  // 必填：新密码（至少6位）
}
```

**调用示例**:
```javascript
import { authApi } from '@/api'

const changePassword = async () => {
  try {
    const res = await authApi.changePassword({
      oldPassword: '123456',
      newPassword: 'newpass123'
    })
    
    if (res.success) {
      console.log('密码修改成功')
    }
  } catch (error) {
    console.error('密码修改失败:', error)
  }
}
```

---

## 政府采购公告 API

### 1. 获取公告列表（分页）

**接口**: `GET /api/gov-procurement/announcements`

**查询参数**:
```javascript
{
  title: "关键字",                    // 可选：标题关键字
  announcementType: "招标公告",       // 可选：公告类型
  projectName: "项目名称",            // 可选：项目名称
  pageIndex: 1,                      // 可选：页码（默认1）
  pageSize: 10                       // 可选：每页数量（默认10）
}
```

**调用示例**:
```javascript
import { govProcurementApi } from '@/api'

const getList = async () => {
  try {
    const res = await govProcurementApi.getAnnouncements({
      title: '采购',
      announcementType: '招标公告',
      pageIndex: 1,
      pageSize: 10
    })
    
    if (res.success) {
      console.log('公告列表:', res.data.items)
      console.log('总数:', res.data.totalCount)
      console.log('总页数:', res.data.totalPages)
      // res.data 结构:
      // {
      //   items: [...],
      //   totalCount: 100,
      //   totalPages: 10,
      //   currentPage: 1,
      //   pageSize: 10
      // }
    }
  } catch (error) {
    console.error('获取列表失败:', error)
  }
}
```

### 2. 获取公告详情

**接口**: `GET /api/gov-procurement/announcements/{id}`

**调用示例**:
```javascript
import { govProcurementApi } from '@/api'

const getDetail = async (id) => {
  try {
    const res = await govProcurementApi.getAnnouncement(id)
    if (res.success) {
      console.log('公告详情:', res.data)
    }
  } catch (error) {
    console.error('获取详情失败:', error)
  }
}
```

### 3. 创建公告

**接口**: `POST /api/gov-procurement/announcements`

**请求参数**:
```javascript
{
  title: "公告标题",                      // 必填
  announcementType: "招标公告",           // 必填
  projectName: "某某项目",                // 可选
  projectCode: "XM2024001",              // 可选
  purchaser: "采购单位",                  // 可选
  publishDate: "2024-12-03",             // 可选
  content: "<p>公告内容...</p>",         // 必填：HTML格式
  attachments: "file1.pdf,file2.pdf"     // 可选：附件列表
}
```

**调用示例**:
```javascript
import { govProcurementApi } from '@/api'

const createAnnouncement = async () => {
  try {
    const res = await govProcurementApi.createAnnouncement({
      title: '某某项目招标公告',
      announcementType: '招标公告',
      projectName: '办公设备采购项目',
      projectCode: 'XM2024001',
      purchaser: '某某单位',
      publishDate: '2024-12-03',
      content: '<p>详细公告内容...</p>'
    })
    
    if (res.success) {
      console.log('创建成功，ID:', res.data.id)
    }
  } catch (error) {
    console.error('创建失败:', error)
  }
}
```

### 4. 更新公告

**接口**: `PUT /api/gov-procurement/announcements/{id}`

**调用示例**:
```javascript
import { govProcurementApi } from '@/api'

const updateAnnouncement = async (id) => {
  try {
    const res = await govProcurementApi.updateAnnouncement(id, {
      title: '修改后的标题',
      content: '<p>修改后的内容...</p>'
    })
    
    if (res.success) {
      console.log('更新成功')
    }
  } catch (error) {
    console.error('更新失败:', error)
  }
}
```

### 5. 删除公告

**接口**: `DELETE /api/gov-procurement/announcements/{id}`

**调用示例**:
```javascript
import { govProcurementApi } from '@/api'

const deleteAnnouncement = async (id) => {
  try {
    const res = await govProcurementApi.deleteAnnouncement(id)
    if (res.success) {
      console.log('删除成功')
    }
  } catch (error) {
    console.error('删除失败:', error)
  }
}
```

---

## 建设工程公告 API

建设工程公告 API 与政府采购公告 API 结构相同，只是基础路径不同：

- 基础路径: `/api/construction/announcements`
- 使用 `constructionApi` 代替 `govProcurementApi`

**调用示例**:
```javascript
import { constructionApi } from '@/api'

// 获取列表
const res = await constructionApi.getAnnouncements({ pageIndex: 1, pageSize: 10 })

// 创建公告
const res = await constructionApi.createAnnouncement({ title: '...', content: '...' })
```

---

## 信息发布 API

### 1. 分页查询信息

**接口**: `GET /api/info-publish`

**查询参数**:
```javascript
{
  category: "company_news",  // 必填：分类（company_news/policies/policy_info/notices）
  keyword: "关键字",          // 可选：搜索关键字
  pageIndex: 1,              // 可选：页码
  pageSize: 10               // 可选：每页数量
}
```

**调用示例**:
```javascript
import { infoPublishApi } from '@/api'

const getList = async () => {
  try {
    const res = await infoPublishApi.getPagedList({
      category: 'company_news',
      keyword: '公告',
      pageIndex: 1,
      pageSize: 10
    })
    
    if (res.success) {
      console.log('信息列表:', res.data.items)
    }
  } catch (error) {
    console.error('获取列表失败:', error)
  }
}
```

### 2. 创建信息（支持文件上传）

**接口**: `POST /api/info-publish`

**调用示例**:
```javascript
import { infoPublishApi } from '@/api'

const createInfo = async (fileList) => {
  try {
    // 创建 FormData
    const formData = new FormData()
    formData.append('category', 'company_news')
    formData.append('title', '公司公告标题')
    formData.append('content', '<p>公告内容...</p>')
    formData.append('isTop', false)
    formData.append('publishTime', '2024-12-03 10:00:00')
    
    // 添加文件
    fileList.forEach(file => {
      formData.append('files', file)
    })
    
    const res = await infoPublishApi.create(formData)
    
    if (res.success) {
      console.log('创建成功:', res.data)
    }
  } catch (error) {
    console.error('创建失败:', error)
  }
}
```

### 3. 更新信息

**接口**: `PUT /api/info-publish/{id}`

**调用示例**:
```javascript
import { infoPublishApi } from '@/api'

const updateInfo = async (id) => {
  try {
    const formData = new FormData()
    formData.append('title', '修改后的标题')
    formData.append('content', '<p>修改后的内容...</p>')
    formData.append('isTop', true)
    
    const res = await infoPublishApi.update(id, 'company_news', formData)
    
    if (res.success) {
      console.log('更新成功')
    }
  } catch (error) {
    console.error('更新失败:', error)
  }
}
```

---

## 系统配置 API

### 1. 轮播图管理

#### 获取所有轮播图
```javascript
import { configApi } from '@/api'

const res = await configApi.getBanners()
if (res.success) {
  console.log('轮播图列表:', res.data)
}
```

#### 创建轮播图
```javascript
const res = await configApi.createBanner({
  title: '轮播图标题',
  imageUrl: 'https://example.com/banner.jpg',
  linkUrl: 'https://example.com',
  sortOrder: 1,
  status: true
})
```

#### 更新轮播图
```javascript
const res = await configApi.updateBanner(id, {
  title: '修改后的标题',
  sortOrder: 2
})
```

#### 删除轮播图
```javascript
const res = await configApi.deleteBanner(id)
```

### 2. 企业简介

#### 获取企业简介
```javascript
const res = await configApi.getCompanyProfile()
if (res.success) {
  console.log('企业简介:', res.data)
}
```

#### 更新企业简介
```javascript
const res = await configApi.updateCompanyProfile({
  title: '企业简介',
  content: '<p>公司介绍...</p>',
  imageUrl: 'https://example.com/company.jpg'
})
```

### 3. 重要业绩

#### 获取所有业绩
```javascript
const res = await configApi.getAchievements()
```

#### 创建业绩
```javascript
const res = await configApi.createAchievement({
  projectName: '某某项目',
  projectType: '咨询服务',
  projectAmount: 500.00,
  clientName: '某某客户',
  completionDate: '2024-12-03',
  description: '项目描述',
  sortOrder: 1
})
```

### 4. 友情链接

#### 获取所有链接
```javascript
const res = await configApi.getFriendlyLinks()
```

#### 创建链接
```javascript
const res = await configApi.createFriendlyLink({
  linkName: '友情链接',
  linkUrl: 'https://example.com',
  logoUrl: 'https://example.com/logo.png',
  sortOrder: 1,
  status: true
})
```

---

## 错误处理

所有 API 调用都应该使用 try-catch 进行错误处理：

```javascript
try {
  const res = await someApi.someMethod(params)
  
  if (res.success) {
    // 成功处理
    console.log('操作成功:', res.data)
  } else {
    // 失败处理
    console.error('操作失败:', res.message)
  }
} catch (error) {
  // 异常处理
  console.error('请求异常:', error)
  
  // 显示错误提示
  ElMessage.error(error.message || '网络错误')
}
```

## 响应拦截器自动处理

项目已配置 Axios 响应拦截器，会自动处理以下情况：

- **401 未授权**: 自动跳转到登录页
- **403 禁止访问**: 显示权限错误提示
- **404 未找到**: 显示资源不存在提示
- **500 服务器错误**: 显示服务器错误提示

拦截器配置位置：`src/api/request.js`

---

## 注意事项

1. **Token 认证**: 除登录接口外，所有接口都需要 Token 认证
2. **分页参数**: pageIndex 从 1 开始
3. **日期格式**: 使用 `YYYY-MM-DD` 或 `YYYY-MM-DD HH:mm:ss` 格式
4. **文件上传**: 使用 FormData，Content-Type 自动设置为 multipart/form-data
5. **富文本内容**: content 字段存储 HTML 格式

---

**API 文档持续更新中...**
