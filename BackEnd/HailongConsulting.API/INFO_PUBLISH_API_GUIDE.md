# 信息发布模块 API 使用指南

## 概述

信息发布模块提供统一的API接口，支持四种类型的信息发布：
- `company` - 公司公告
- `policy-regulation` - 政策法规
- `policy-information` - 政策信息
- `notice` - 通知公告

## 数据库设计

采用**多表设计**方案，每种类型对应独立的数据表：
- `company_announcements` - 公司公告表
- `policy_regulations` - 政策法规表
- `policy_information` - 政策信息表
- `notice_announcements` - 通知公告表

## API 端点

### 1. 创建信息

**端点**: `POST /api/info-publish`

**请求类型**: `multipart/form-data`

**参数**:
```json
{
  "category": "company",           // 必填: company | policy-regulation | policy-information | notice
  "title": "公告标题",              // 必填
  "content": "公告内容（富文本）",   // 必填
  "publishTime": "2024-01-01T10:00:00",  // 可选，默认当前时间
  "isTop": false,                  // 可选，是否置顶，默认false
  "files": [文件1, 文件2]           // 可选，附件列表
}
```

**支持的文件类型**: `.pdf`, `.doc`, `.docx`, `.xls`, `.xlsx`, `.jpg`, `.jpeg`, `.png`

**文件大小限制**: 10MB

**响应示例**:
```json
{
  "success": true,
  "message": "创建成功",
  "data": {
    "id": 1,
    "category": "company",
    "title": "公告标题",
    "content": "公告内容",
    "publishTime": "2024-01-01T10:00:00",
    "viewCount": 0,
    "attachments": [
      "/uploads/attachments/company/202401/file1_20240101100000_abc12345.pdf",
      "/uploads/attachments/company/202401/file2_20240101100001_def67890.docx"
    ],
    "isTop": false,
    "createdAt": "2024-01-01T10:00:00",
    "updatedAt": "2024-01-01T10:00:00"
  }
}
```

**cURL 示例**:
```bash
curl -X POST "http://localhost:5000/api/info-publish" \
  -H "Content-Type: multipart/form-data" \
  -F "category=company" \
  -F "title=重要公告" \
  -F "content=这是一条重要公告内容" \
  -F "publishTime=2024-01-01T10:00:00" \
  -F "isTop=true" \
  -F "files=@/path/to/file1.pdf" \
  -F "files=@/path/to/file2.docx"
```

---

### 2. 分页查询信息

**端点**: `GET /api/info-publish`

**参数**:
- `category` (必填): 信息分类
- `keyword` (可选): 搜索关键词（标题或内容）
- `pageIndex` (可选): 页码，默认1
- `pageSize` (可选): 每页数量，默认10

**响应示例**:
```json
{
  "success": true,
  "message": "查询成功",
  "data": {
    "items": [
      {
        "id": 1,
        "category": "company",
        "title": "公告标题",
        "content": "公告内容",
        "publishTime": "2024-01-01T10:00:00",
        "viewCount": 100,
        "attachments": ["/uploads/attachments/company/202401/file.pdf"],
        "isTop": true,
        "createdAt": "2024-01-01T10:00:00",
        "updatedAt": "2024-01-01T10:00:00"
      }
    ],
    "totalCount": 50,
    "pageIndex": 1,
    "pageSize": 10,
    "totalPages": 5
  }
}
```

**cURL 示例**:
```bash
# 查询公司公告
curl -X GET "http://localhost:5000/api/info-publish?category=company&pageIndex=1&pageSize=10"

# 带关键词搜索
curl -X GET "http://localhost:5000/api/info-publish?category=policy-regulation&keyword=采购&pageIndex=1&pageSize=10"
```

---

### 3. 获取信息详情

**端点**: `GET /api/info-publish/{id}`

**参数**:
- `id` (路径参数): 信息ID
- `category` (查询参数，必填): 信息分类

**功能**: 获取详情时自动增加访问量

**响应示例**:
```json
{
  "success": true,
  "message": "查询成功",
  "data": {
    "id": 1,
    "category": "company",
    "title": "公告标题",
    "content": "公告内容",
    "publishTime": "2024-01-01T10:00:00",
    "viewCount": 101,  // 访问量已增加
    "attachments": ["/uploads/attachments/company/202401/file.pdf"],
    "isTop": true,
    "createdAt": "2024-01-01T10:00:00",
    "updatedAt": "2024-01-01T10:00:00"
  }
}
```

**cURL 示例**:
```bash
curl -X GET "http://localhost:5000/api/info-publish/1?category=company"
```

---

### 4. 更新信息

**端点**: `PUT /api/info-publish/{id}`

**请求类型**: `multipart/form-data`

**参数**:
```json
{
  "category": "company",           // 必填（表单字段）
  "title": "更新后的标题",          // 必填
  "content": "更新后的内容",        // 必填
  "publishTime": "2024-01-02T10:00:00",  // 可选
  "isTop": true,                   // 必填
  "files": [新文件1, 新文件2]       // 可选，新上传的附件
}
```

**注意**: 
- 如果上传新文件，会追加到现有附件列表
- 不会删除原有附件

**响应示例**:
```json
{
  "success": true,
  "message": "更新成功",
  "data": {
    "id": 1,
    "category": "company",
    "title": "更新后的标题",
    "content": "更新后的内容",
    "publishTime": "2024-01-02T10:00:00",
    "viewCount": 101,
    "attachments": [
      "/uploads/attachments/company/202401/old_file.pdf",
      "/uploads/attachments/company/202401/new_file.pdf"
    ],
    "isTop": true,
    "createdAt": "2024-01-01T10:00:00",
    "updatedAt": "2024-01-02T10:00:00"
  }
}
```

**cURL 示例**:
```bash
curl -X PUT "http://localhost:5000/api/info-publish/1" \
  -H "Content-Type: multipart/form-data" \
  -F "category=company" \
  -F "title=更新后的标题" \
  -F "content=更新后的内容" \
  -F "isTop=true" \
  -F "files=@/path/to/new_file.pdf"
```

---

### 5. 删除信息

**端点**: `DELETE /api/info-publish/{id}`

**参数**:
- `id` (路径参数): 信息ID
- `category` (查询参数，必填): 信息分类

**功能**: 软删除，不会真正删除数据

**响应示例**:
```json
{
  "success": true,
  "message": "删除成功",
  "data": true
}
```

**cURL 示例**:
```bash
curl -X DELETE "http://localhost:5000/api/info-publish/1?category=company"
```

---

## 附件存储格式

### 数据库存储
附件路径以JSON数组格式存储在 `attachment_path` 字段：
```json
[
  "/uploads/attachments/company/202401/file1_20240101100000_abc12345.pdf",
  "/uploads/attachments/company/202401/file2_20240101100001_def67890.docx"
]
```

### 文件系统路径
```
wwwroot/
  └── uploads/
      └── attachments/
          ├── company/          # 公司公告
          ├── policy-regulation/ # 政策法规
          ├── policy-information/ # 政策信息
          └── notice/           # 通知公告
              └── 202401/       # 年月目录
                  ├── file1_20240101100000_abc12345.pdf
                  └── file2_20240101100001_def67890.docx
```

### 访问URL
```
http://localhost:5000/uploads/attachments/company/202401/file1_20240101100000_abc12345.pdf
```

---

## 错误处理

所有API都遵循统一的错误响应格式：

```json
{
  "success": false,
  "message": "错误信息",
  "data": null
}
```

常见错误：
- `400 Bad Request`: 参数错误或验证失败
- `404 Not Found`: 资源不存在
- `500 Internal Server Error`: 服务器内部错误