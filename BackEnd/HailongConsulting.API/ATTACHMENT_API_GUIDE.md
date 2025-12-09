# 附件管理 API 文档

## 概述
附件管理 API 提供统一的文件上传、下载和管理功能，支持图片、文档、视频等多种文件类型。

## 基础信息
- **Base URL**: `/api/attachments`
- **认证方式**: JWT Bearer Token（部分接口需要）

## API 接口列表

### 1. 上传附件

**接口**: `POST /api/attachments/upload`

**认证**: 需要

**请求参数** (multipart/form-data):
```
file: IFormFile (必填) - 要上传的文件
category: string (可选) - 分类：image/document/video/other，默认 other
relatedType: string (可选) - 关联类型，如：announcement, info_publication
relatedId: uint (可选) - 关联记录ID
```

**响应示例**:
```json
{
  "success": true,
  "message": "上传附件成功",
  "data": {
    "id": 1,
    "fileName": "example.pdf",
    "filePath": "/uploads/documents/2024/01/example_20240101120000.pdf",
    "fileUrl": "https://example.com/uploads/documents/2024/01/example_20240101120000.pdf",
    "fileSize": 1024000,
    "fileType": "application/pdf",
    "fileExtension": ".pdf",
    "category": "document",
    "relatedType": "announcement",
    "relatedId": 100,
    "uploadUserId": 1,
    "createdAt": "2024-01-01T12:00:00Z"
  }
}
```

---

### 2. 批量上传附件

**接口**: `POST /api/attachments/upload/batch`

**认证**: 需要

**请求参数** (multipart/form-data):
```
files: List<IFormFile> (必填) - 要上传的文件列表
category: string (可选) - 分类
relatedType: string (可选) - 关联类型
relatedId: uint (可选) - 关联记录ID
```

**响应示例**:
```json
{
  "success": true,
  "message": "成功上传 3 个附件",
  "data": [
    {
      "id": 1,
      "fileName": "file1.jpg",
      "filePath": "/uploads/images/2024/01/file1_20240101120000.jpg",
      ...
    },
    {
      "id": 2,
      "fileName": "file2.jpg",
      "filePath": "/uploads/images/2024/01/file2_20240101120001.jpg",
      ...
    }
  ]
}
```

---

### 3. 获取附件详情

**接口**: `GET /api/attachments/{id}`

**认证**: 不需要

**路径参数**:
- `id`: 附件ID

**响应示例**:
```json
{
  "success": true,
  "message": "获取附件成功",
  "data": {
    "id": 1,
    "fileName": "example.pdf",
    "filePath": "/uploads/documents/2024/01/example_20240101120000.pdf",
    "fileUrl": "https://example.com/uploads/documents/2024/01/example_20240101120000.pdf",
    "fileSize": 1024000,
    "fileType": "application/pdf",
    "fileExtension": ".pdf",
    "category": "document",
    "relatedType": "announcement",
    "relatedId": 100,
    "uploadUserId": 1,
    "createdAt": "2024-01-01T12:00:00Z"
  }
}
```

---

### 4. 根据关联获取附件列表

**接口**: `GET /api/attachments/by-relation`

**认证**: 不需要

**查询参数**:
- `relatedType`: string (必填) - 关联类型
- `relatedId`: uint (必填) - 关联记录ID

**请求示例**:
```
GET /api/attachments/by-relation?relatedType=announcement&relatedId=100
```

**响应示例**:
```json
{
  "success": true,
  "message": "获取附件列表成功",
  "data": [
    {
      "id": 1,
      "fileName": "file1.pdf",
      ...
    },
    {
      "id": 2,
      "fileName": "file2.jpg",
      ...
    }
  ]
}
```

---

### 5. 批量获取附件

**接口**: `GET /api/attachments/batch`

**认证**: 不需要

**查询参数**:
- `ids`: string (必填) - 附件ID列表，逗号分隔，如：1,2,3

**请求示例**:
```
GET /api/attachments/batch?ids=1,2,3
```

**响应示例**:
```json
{
  "success": true,
  "message": "获取附件列表成功",
  "data": [
    {
      "id": 1,
      "fileName": "file1.pdf",
      ...
    },
    {
      "id": 2,
      "fileName": "file2.jpg",
      ...
    }
  ]
}
```

---

### 6. 删除附件

**接口**: `DELETE /api/attachments/{id}`

**认证**: 需要

**路径参数**:
- `id`: 附件ID

**响应示例**:
```json
{
  "success": true,
  "message": "删除附件成功",
  "data": true
}
```

---

### 7. 批量删除附件

**接口**: `DELETE /api/attachments/batch`

**认证**: 需要

**请求体**:
```json
[1, 2, 3]
```

**响应示例**:
```json
{
  "success": true,
  "message": "成功删除 3 个附件",
  "data": true
}
```

---

### 8. 下载附件

**接口**: `GET /api/attachments/{id}/download`

**认证**: 不需要

**路径参数**:
- `id`: 附件ID

**响应**: 文件流（直接下载文件）

---

## 附件分类说明

| 分类 | 说明 | 示例 |
|------|------|------|
| image | 图片文件 | jpg, png, gif, webp |
| document | 文档文件 | pdf, doc, docx, xls, xlsx |
| video | 视频文件 | mp4, avi, mov |
| other | 其他文件 | zip, rar, txt |

## 关联类型说明

| 关联类型 | 说明 |
|----------|------|
| announcement | 公告附件 |
| info_publication | 信息发布附件 |
| company_profile | 企业简介附件 |
| achievement | 重要业绩附件 |
| banner | 轮播图 |
| friendly_link | 友情链接Logo |

## 错误码说明

| 错误码 | 说明 |
|--------|------|
| 400 | 请求参数错误 |
| 401 | 未授权（需要登录） |
| 404 | 附件不存在 |
| 500 | 服务器内部错误 |

## 使用示例

### JavaScript (Axios)

```javascript
// 上传单个文件
const uploadFile = async (file) => {
  const formData = new FormData();
  formData.append('file', file);
  formData.append('category', 'document');
  formData.append('relatedType', 'announcement');
  formData.append('relatedId', 100);

  const response = await axios.post('/api/attachments/upload', formData, {
    headers: {
      'Content-Type': 'multipart/form-data',
      'Authorization': `Bearer ${token}`
    }
  });
  return response.data;
};

// 批量上传文件
const uploadFiles = async (files) => {
  const formData = new FormData();
  files.forEach(file => {
    formData.append('files', file);
  });
  formData.append('category', 'image');

  const response = await axios.post('/api/attachments/upload/batch', formData, {
    headers: {
      'Content-Type': 'multipart/form-data',
      'Authorization': `Bearer ${token}`
    }
  });
  return response.data;
};

// 获取附件列表
const getAttachments = async (relatedType, relatedId) => {
  const response = await axios.get('/api/attachments/by-relation', {
    params: { relatedType, relatedId }
  });
  return response.data;
};

// 删除附件
const deleteAttachment = async (id) => {
  const response = await axios.delete(`/api/attachments/${id}`, {
    headers: {
      'Authorization': `Bearer ${token}`
    }
  });
  return response.data;
};
```

### C# (.NET)

```csharp
// 上传文件
using var client = new HttpClient();
using var formData = new MultipartFormDataContent();
using var fileContent = new StreamContent(File.OpenRead("example.pdf"));
formData.Add(fileContent, "file", "example.pdf");
formData.Add(new StringContent("document"), "category");
formData.Add(new StringContent("announcement"), "relatedType");
formData.Add(new StringContent("100"), "relatedId");

client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
var response = await client.PostAsync("/api/attachments/upload", formData);
var result = await response.Content.ReadAsStringAsync();
```

## 注意事项

1. **文件大小限制**: 默认最大 50MB，可在服务器配置中调整
2. **支持的文件类型**: 根据 FileHelper 配置，默认支持常见的图片、文档、视频格式
3. **文件存储路径**: 文件按分类和日期自动组织存储
4. **软删除**: 删除附件时使用软删除，不会立即删除物理文件
5. **权限控制**: 上传和删除操作需要登录认证

## 更新日志

### v2.0 (2024-01-01)
- 统一附件管理系统
- 支持多种文件类型
- 支持关联类型和ID
- 支持批量操作
- 添加文件下载功能