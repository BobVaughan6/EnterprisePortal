# 信息发布API调用示例

本文档提供信息发布管理模块的完整API调用示例。

---

## 📡 API基础配置

### 请求基础URL
```javascript
baseURL: 'http://localhost:5000/api'
```

### 认证方式
```javascript
headers: {
  'Authorization': 'Bearer {token}'
}
```

---

## 📝 API接口详解

### 1. 创建信息

**接口**: `POST /api/info-publish`

**Content-Type**: `multipart/form-data`

**请求示例**:

```javascript
// 1. 创建FormData对象
const formData = new FormData()

// 2. 添加基础字段
formData.append('category', 'company_announcements')
formData.append('title', '关于2024年度年会的通知')
formData.append('content', '<p>各部门请注意...</p>')
formData.append('publishTime', '2024-12-10 09:00:00')
formData.append('isTop', true)

// 3. 添加附件文件（可选）
const file1 = document.querySelector('#fileInput1').files[0]
const file2 = document.querySelector('#fileInput2').files[0]
formData.append('files', file1)
formData.append('files', file2)

// 4. 发送请求
const response = await infoPublishApi.create(formData)
```

**请求参数**:

| 参数 | 类型 | 必填 | 说明 |
|------|------|------|------|
| category | string | 是 | 分类（company_announcements/policy_regulations/policy_information/notice_announcements） |
| title | string | 是 | 标题（最多255字符） |
| content | string | 是 | 内容（富文本HTML） |
| publishTime | string | 否 | 发布时间（格式：YYYY-MM-DD HH:mm:ss） |
| isTop | boolean | 否 | 是否置顶（默认false） |
| files | File[] | 否 | 附件文件数组 |

**成功响应**:

```json
{
  "success": true,
  "message": "创建成功",
  "data": {
    "id": 1,
    "category": "company_announcements",
    "title": "关于2024年度年会的通知",
    "content": "<p>各部门请注意...</p>",
    "publishTime": "2024-12-10T09:00:00",
    "viewCount": 0,
    "attachments": [
      "/uploads/company_announcements/file1_20241203123456.pdf",
      "/uploads/company_announcements/file2_20241203123456.docx"
    ],
    "isTop": true,
    "createdAt": "2024-12-03T12:34:56",
    "updatedAt": "2024-12-03T12:34:56"
  }
}
```

---

### 2. 分页查询信息列表

**接口**: `GET /api/info-publish`

**请求示例**:

```javascript
const params = {
  category: 'company_announcements',
  keyword: '年会',
  pageIndex: 1,
  pageSize: 10
}

const response = await infoPublishApi.getPagedList(params)
```

**请求参数**:

| 参数 | 类型 | 必填 | 说明 |
|------|------|------|------|
| category | string | 是 | 分类 |
| keyword | string | 否 | 搜索关键词（搜索标题和内容） |
| pageIndex | number | 否 | 页码（默认1） |
| pageSize | number | 否 | 每页数量（默认10） |

**成功响应**:

```json
{
  "success": true,
  "message": null,
  "data": {
    "items": [
      {
        "id": 1,
        "category": "company_announcements",
        "title": "关于2024年度年会的通知",
        "content": "<p>各部门请注意...</p>",
        "publishTime": "2024-12-10T09:00:00",
        "viewCount": 156,
        "attachments": [
          "/uploads/company_announcements/file1.pdf"
        ],
        "isTop": true,
        "createdAt": "2024-12-03T12:34:56",
        "updatedAt": "2024-12-03T12:34:56"
      }
    ],
    "pageIndex": 1,
    "pageSize": 10,
    "totalCount": 25,
    "totalPages": 3
  }
}
```

---

### 3. 获取信息详情

**接口**: `GET /api/info-publish/{id}`

**请求示例**:

```javascript
const id = 1
const category = 'company_announcements'

const response = await infoPublishApi.getById(id, category)
```

**请求参数**:

| 参数 | 位置 | 类型 | 必填 | 说明 |
|------|------|------|------|------|
| id | path | number | 是 | 信息ID |
| category | query | string | 是 | 分类 |

**成功响应**:

```json
{
  "success": true,
  "message": null,
  "data": {
    "id": 1,
    "category": "company_announcements",
    "title": "关于2024年度年会的通知",
    "content": "<p>各部门请注意...</p>",
    "publishTime": "2024-12-10T09:00:00",
    "viewCount": 157,  // 访问量自动+1
    "attachments": [
      "/uploads/company_announcements/file1.pdf"
    ],
    "isTop": true,
    "createdAt": "2024-12-03T12:34:56",
    "updatedAt": "2024-12-03T12:34:56"
  }
}
```

**注意**: 每次调用此接口，访问量会自动增加1。

---

### 4. 更新信息

**接口**: `PUT /api/info-publish/{id}`

**Content-Type**: `multipart/form-data`

**请求示例**:

```javascript
const id = 1
const formData = new FormData()

// 添加必填字段
formData.append('category', 'company_announcements')
formData.append('title', '关于2024年度年会的通知（更新）')
formData.append('content', '<p>更新后的内容...</p>')
formData.append('publishTime', '2024-12-10 09:00:00')
formData.append('isTop', false)

// 添加新的附件（可选）
const newFile = document.querySelector('#fileInput').files[0]
if (newFile) {
  formData.append('files', newFile)
}

// 发送请求
const response = await infoPublishApi.update(id, formData)
```

**请求参数**:

| 参数 | 位置 | 类型 | 必填 | 说明 |
|------|------|------|------|------|
| id | path | number | 是 | 信息ID |
| category | formData | string | 是 | 分类 |
| title | formData | string | 是 | 标题 |
| content | formData | string | 是 | 内容 |
| publishTime | formData | string | 否 | 发布时间 |
| isTop | formData | boolean | 否 | 是否置顶 |
| files | formData | File[] | 否 | 新增附件文件 |

**成功响应**:

```json
{
  "success": true,
  "message": "更新成功",
  "data": {
    "id": 1,
    "category": "company_announcements",
    "title": "关于2024年度年会的通知（更新）",
    "content": "<p>更新后的内容...</p>",
    "publishTime": "2024-12-10T09:00:00",
    "viewCount": 157,
    "attachments": [
      "/uploads/company_announcements/file1.pdf",
      "/uploads/company_announcements/new_file_20241203145678.pdf"
    ],
    "isTop": false,
    "createdAt": "2024-12-03T12:34:56",
    "updatedAt": "2024-12-03T14:56:78"
  }
}
```

**注意**: 
- 更新时会保留原有附件
- 新上传的文件会添加到附件列表
- 如需删除附件，需在前端处理（编辑时记录要保留的附件路径）

---

### 5. 删除信息

**接口**: `DELETE /api/info-publish/{id}`

**请求示例**:

```javascript
const id = 1
const category = 'company_announcements'

const response = await infoPublishApi.delete(id, category)
```

**请求参数**:

| 参数 | 位置 | 类型 | 必填 | 说明 |
|------|------|------|------|------|
| id | path | number | 是 | 信息ID |
| category | query | string | 是 | 分类 |

**成功响应**:

```json
{
  "success": true,
  "message": "删除成功",
  "data": true
}
```

**注意**: 这是软删除，数据不会真正从数据库删除，只是标记 `is_deleted=1`。

---

## 🔄 完整使用流程示例

### 场景1: 新增一条公司公告

```javascript
// 步骤1: 准备数据
const createAnnouncement = async () => {
  const formData = new FormData()
  
  // 基础信息
  formData.append('category', 'company_announcements')
  formData.append('title', '元旦放假通知')
  formData.append('content', '<p>公司全体员工：</p><p>根据国家规定...</p>')
  formData.append('publishTime', '2024-12-25 10:00:00')
  formData.append('isTop', true)
  
  // 添加附件
  const fileInput = document.querySelector('#attachment')
  if (fileInput.files.length > 0) {
    Array.from(fileInput.files).forEach(file => {
      formData.append('files', file)
    })
  }
  
  // 步骤2: 调用API
  try {
    const res = await infoPublishApi.create(formData)
    if (res.success) {
      console.log('创建成功:', res.data)
      // 刷新列表或跳转
    }
  } catch (error) {
    console.error('创建失败:', error)
  }
}
```

### 场景2: 编辑现有信息

```javascript
// 步骤1: 获取详情
const editAnnouncement = async (id) => {
  try {
    // 获取详情数据
    const detailRes = await infoPublishApi.getById(id, 'company_announcements')
    
    if (detailRes.success) {
      const detail = detailRes.data
      
      // 步骤2: 填充表单
      formData.title = detail.title
      formData.content = detail.content
      formData.publishTime = detail.publishTime
      formData.isTop = detail.isTop
      formData.attachments = detail.attachments || []
      
      // 步骤3: 用户修改后提交
      // ...
      
      // 步骤4: 构造更新数据
      const updateFormData = new FormData()
      updateFormData.append('category', 'company_announcements')
      updateFormData.append('title', formData.title)
      updateFormData.append('content', formData.content)
      updateFormData.append('publishTime', formData.publishTime)
      updateFormData.append('isTop', formData.isTop)
      
      // 添加新文件（如果有）
      if (newFiles.length > 0) {
        newFiles.forEach(file => {
          updateFormData.append('files', file)
        })
      }
      
      // 步骤5: 提交更新
      const updateRes = await infoPublishApi.update(id, updateFormData)
      if (updateRes.success) {
        console.log('更新成功:', updateRes.data)
      }
    }
  } catch (error) {
    console.error('编辑失败:', error)
  }
}
```

### 场景3: 列表查询与分页

```javascript
// 列表查询
const loadList = async (category, keyword, page = 1, size = 10) => {
  try {
    const params = {
      category: category,
      keyword: keyword,
      pageIndex: page,
      pageSize: size
    }
    
    const res = await infoPublishApi.getPagedList(params)
    
    if (res.success) {
      const { items, totalCount, totalPages } = res.data
      
      console.log(`共 ${totalCount} 条数据，${totalPages} 页`)
      console.log('当前页数据:', items)
      
      return {
        list: items,
        total: totalCount
      }
    }
  } catch (error) {
    console.error('查询失败:', error)
  }
}

// 使用示例
loadList('company_announcements', '通知', 1, 10)
```

### 场景4: 删除确认

```javascript
const deleteAnnouncement = async (id) => {
  // 步骤1: 确认对话框
  const confirmed = await showConfirmDialog('确定要删除这条信息吗？')
  
  if (!confirmed) return
  
  // 步骤2: 调用删除API
  try {
    const res = await infoPublishApi.delete(id, 'company_announcements')
    
    if (res.success) {
      console.log('删除成功')
      // 刷新列表
      loadList('company_announcements', '', 1, 10)
    }
  } catch (error) {
    console.error('删除失败:', error)
  }
}
```

---

## 🎯 分类枚举值

使用正确的category值：

```javascript
const CATEGORIES = {
  COMPANY_ANNOUNCEMENTS: 'company_announcements',    // 公司公告
  POLICY_REGULATIONS: 'policy_regulations',          // 政策法规
  POLICY_INFORMATION: 'policy_information',          // 政策信息
  NOTICE_ANNOUNCEMENTS: 'notice_announcements'       // 通知公告
}

// 使用
formData.append('category', CATEGORIES.COMPANY_ANNOUNCEMENTS)
```

---

## ⚠️ 错误处理

### 常见错误响应

```json
{
  "success": false,
  "message": "分类参数不能为空",
  "data": null
}
```

```json
{
  "success": false,
  "message": "未找到该信息",
  "data": null
}
```

### 错误处理示例

```javascript
try {
  const res = await infoPublishApi.create(formData)
  
  if (res.success) {
    // 成功处理
  } else {
    // 失败处理
    console.error('操作失败:', res.message)
    showErrorMessage(res.message)
  }
} catch (error) {
  // 异常处理
  console.error('请求异常:', error)
  showErrorMessage('网络请求失败，请稍后重试')
}
```

---

## 📌 注意事项

1. **FormData字段名称必须与后端一致**
   - 文件字段名: `files` (数组形式)
   - 其他字段: 与DTO属性名一致

2. **日期时间格式**
   - 格式: `YYYY-MM-DD HH:mm:ss`
   - 示例: `2024-12-10 09:00:00`

3. **文件上传限制**
   - 支持格式: PDF, DOC, DOCX, XLS, XLSX
   - 单文件最大: 10MB
   - 最多文件数: 根据需求配置

4. **分类值必须准确**
   - 必须使用数据库表名作为category值
   - 区分大小写

5. **附件路径格式**
   - 后端返回格式: `/uploads/{category}/{filename}`
   - 前端使用时需拼接完整URL

---

## 🔗 相关文档

- [信息发布实现文档](./INFO_PUBLISH_IMPLEMENTATION.md)
- [快速集成指南](./INFO_PUBLISH_QUICKSTART.md)
- [后端API指南](../BackEnd/HailongConsulting.API/INFO_PUBLISH_API_GUIDE.md)
