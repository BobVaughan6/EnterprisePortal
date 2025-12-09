# 统一信息发布管理 API 文档

## 概述
统一信息发布管理 API 整合了公司公告、政策法规、政策信息和通知公告，提供统一的接口进行信息的创建、查询、更新和删除操作。

## 基础信息
- **Base URL**: `/api/info-publications`
- **认证方式**: JWT Bearer Token（部分接口需要）

## 信息类型说明

| 信息类型 | 值 | 说明 |
|----------|-----|------|
| 公司公告 | COMPANY_NEWS | 公司新闻、动态、公告 |
| 政策法规 | POLICY_REGULATION | 相关政策法规文件 |
| 政策信息 | POLICY_INFO | 政策解读、政策信息 |
| 通知公告 | NOTICE | 各类通知公告 |

## API 接口列表

### 1. 创建信息发布

**接口**: `POST /api/info-publications`

**认证**: 需要

**请求体**:
```json
{
  "title": "公司年度总结大会通知",
  "type": "COMPANY_NEWS",
  "category": "公司动态",
  "summary": "关于召开2024年度总结大会的通知",
  "content": "<p>详细内容...</p>",
  "author": "行政部",
  "source": "公司内部",
  "coverImageId": 1,
  "attachmentIds": "[2,3,4]",
  "publishDate": "2024-01-01T00:00:00Z",
  "isTop": 0,
  "status": 1
}
```

**响应示例**:
```json
{
  "success": true,
  "message": "创建信息发布成功",
  "data": {
    "id": 1,
    "title": "公司年度总结大会通知",
    "type": "COMPANY_NEWS",
    "category": "公司动态",
    "summary": "关于召开2024年度总结大会的通知",
    "content": "<p>详细内容...</p>",
    "author": "行政部",
    "source": "公司内部",
    "coverImageId": 1,
    "attachmentIds": [2, 3, 4],
    "publishDate": "2024-01-01T00:00:00Z",
    "viewCount": 0,
    "isTop": false,
    "status": 1,
    "createdAt": "2024-01-01T12:00:00Z",
    "updatedAt": "2024-01-01T12:00:00Z"
  }
}
```

---

### 2. 更新信息发布

**接口**: `PUT /api/info-publications/{id}`

**认证**: 需要

**路径参数**:
- `id`: 信息发布ID

**请求体**: 同创建信息发布（所有字段可选）

**响应示例**:
```json
{
  "success": true,
  "message": "更新信息发布成功",
  "data": {
    "id": 1,
    "title": "公司年度总结大会通知（已更新）",
    ...
  }
}
```

---

### 3. 获取信息发布详情

**接口**: `GET /api/info-publications/{id}`

**认证**: 不需要

**路径参数**:
- `id`: 信息发布ID

**响应示例**:
```json
{
  "success": true,
  "message": "获取信息发布成功",
  "data": {
    "id": 1,
    "title": "公司年度总结大会通知",
    "type": "COMPANY_NEWS",
    "category": "公司动态",
    "summary": "关于召开2024年度总结大会的通知",
    "content": "<p>详细内容...</p>",
    "author": "行政部",
    "source": "公司内部",
    "coverImageId": 1,
    "attachmentIds": [2, 3, 4],
    "publishDate": "2024-01-01T00:00:00Z",
    "viewCount": 10,
    "isTop": false,
    "status": 1,
    "createdAt": "2024-01-01T12:00:00Z",
    "updatedAt": "2024-01-01T12:00:00Z"
  }
}
```

**注意**: 访问此接口会自动增加浏览次数

---

### 4. 分页查询信息发布列表

**接口**: `GET /api/info-publications`

**认证**: 不需要

**查询参数**:
- `type`: string (可选) - 信息类型：COMPANY_NEWS/POLICY_REGULATION/POLICY_INFO/NOTICE
- `category`: string (可选) - 分类
- `keyword`: string (可选) - 关键词搜索（标题、摘要）
- `pageNumber`: int (可选) - 页码，默认 1
- `pageSize`: int (可选) - 每页数量，默认 10

**请求示例**:
```
GET /api/info-publications?type=COMPANY_NEWS&category=公司动态&keyword=总结&pageNumber=1&pageSize=10
```

**响应示例**:
```json
{
  "success": true,
  "message": "获取信息发布列表成功",
  "data": {
    "items": [
      {
        "id": 1,
        "title": "公司年度总结大会通知",
        "type": "COMPANY_NEWS",
        "category": "公司动态",
        "summary": "关于召开2024年度总结大会的通知",
        ...
      },
      {
        "id": 2,
        "title": "另一条公司新闻",
        ...
      }
    ],
    "totalCount": 50,
    "pageIndex": 1,
    "pageSize": 10,
    "totalPages": 5
  }
}
```

---

### 5. 删除信息发布

**接口**: `DELETE /api/info-publications/{id}`

**认证**: 需要

**路径参数**:
- `id`: 信息发布ID

**响应示例**:
```json
{
  "success": true,
  "message": "删除信息发布成功",
  "data": true
}
```

---

### 6. 获取公司公告列表

**接口**: `GET /api/info-publications/company-news`

**认证**: 不需要

**查询参数**: 同"分页查询信息发布列表"（自动设置 type=COMPANY_NEWS）

**请求示例**:
```
GET /api/info-publications/company-news?category=公司动态&pageNumber=1&pageSize=10
```

**响应示例**: 同"分页查询信息发布列表"

---

### 7. 获取政策法规列表

**接口**: `GET /api/info-publications/policy-regulations`

**认证**: 不需要

**查询参数**: 同"分页查询信息发布列表"（自动设置 type=POLICY_REGULATION）

**请求示例**:
```
GET /api/info-publications/policy-regulations?keyword=招标&pageNumber=1&pageSize=10
```

**响应示例**: 同"分页查询信息发布列表"

---

### 8. 获取政策信息列表

**接口**: `GET /api/info-publications/policy-info`

**认证**: 不需要

**查询参数**: 同"分页查询信息发布列表"（自动设置 type=POLICY_INFO）

**请求示例**:
```
GET /api/info-publications/policy-info?pageNumber=1&pageSize=10
```

**响应示例**: 同"分页查询信息发布列表"

---

### 9. 获取通知公告列表

**接口**: `GET /api/info-publications/notices`

**认证**: 不需要

**查询参数**: 同"分页查询信息发布列表"（自动设置 type=NOTICE）

**请求示例**:
```
GET /api/info-publications/notices?pageNumber=1&pageSize=10
```

**响应示例**: 同"分页查询信息发布列表"

---

## 字段说明

### 必填字段
- `title`: 标题
- `type`: 信息类型（COMPANY_NEWS/POLICY_REGULATION/POLICY_INFO/NOTICE）
- `content`: 内容（HTML格式）

### 可选字段
- `category`: 分类（二级分类）
- `summary`: 摘要
- `author`: 作者
- `source`: 来源
- `coverImageId`: 封面图片ID（关联attachments表）
- `attachmentIds`: 附件ID列表（JSON数组字符串）
- `publishDate`: 发布日期
- `isTop`: 是否置顶（0/1）
- `status`: 状态（0-草稿，1-已发布）

## 分类建议

### 公司公告 (COMPANY_NEWS)
- 公司动态
- 企业文化
- 员工风采
- 荣誉资质

### 政策法规 (POLICY_REGULATION)
- 招标投标法规
- 政府采购法规
- 建设工程法规
- 行业规范

### 政策信息 (POLICY_INFO)
- 政策解读
- 行业动态
- 市场分析
- 专家观点

### 通知公告 (NOTICE)
- 系统通知
- 业务通知
- 活动通知
- 其他通知

## 使用示例

### JavaScript (Axios)

```javascript
// 创建信息发布
const createInfoPublication = async (data) => {
  const response = await axios.post('/api/info-publications', data, {
    headers: {
      'Authorization': `Bearer ${token}`
    }
  });
  return response.data;
};

// 查询信息发布列表
const getInfoPublications = async (params) => {
  const response = await axios.get('/api/info-publications', { params });
  return response.data;
};

// 获取公司公告
const getCompanyNews = async (params) => {
  const response = await axios.get('/api/info-publications/company-news', { params });
  return response.data;
};

// 获取政策法规
const getPolicyRegulations = async (params) => {
  const response = await axios.get('/api/info-publications/policy-regulations', { params });
  return response.data;
};

// 获取政策信息
const getPolicyInfo = async (params) => {
  const response = await axios.get('/api/info-publications/policy-info', { params });
  return response.data;
};

// 获取通知公告
const getNotices = async (params) => {
  const response = await axios.get('/api/info-publications/notices', { params });
  return response.data;
};

// 获取信息详情
const getInfoPublicationDetail = async (id) => {
  const response = await axios.get(`/api/info-publications/${id}`);
  return response.data;
};

// 更新信息发布
const updateInfoPublication = async (id, data) => {
  const response = await axios.put(`/api/info-publications/${id}`, data, {
    headers: {
      'Authorization': `Bearer ${token}`
    }
  });
  return response.data;
};

// 删除信息发布
const deleteInfoPublication = async (id) => {
  const response = await axios.delete(`/api/info-publications/${id}`, {
    headers: {
      'Authorization': `Bearer ${token}`
    }
  });
  return response.data;
};
```

### C# (.NET)

```csharp
// 创建信息发布
var publication = new CreateInfoPublicationDto
{
    Title = "公司年度总结大会通知",
    Type = "COMPANY_NEWS",
    Category = "公司动态",
    Content = "<p>详细内容...</p>",
    // ... 其他字段
};

using var client = new HttpClient();
client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
var json = JsonSerializer.Serialize(publication);
var content = new StringContent(json, Encoding.UTF8, "application/json");
var response = await client.PostAsync("/api/info-publications", content);
var result = await response.Content.ReadAsStringAsync();

// 查询信息发布列表
var queryString = $"?type=COMPANY_NEWS&pageNumber=1&pageSize=10";
var response = await client.GetAsync($"/api/info-publications{queryString}");
var result = await response.Content.ReadAsStringAsync();
```

## 错误码说明

| 错误码 | 说明 |
|--------|------|
| 400 | 请求参数错误 |
| 401 | 未授权（需要登录） |
| 404 | 信息发布不存在 |
| 500 | 服务器内部错误 |

## 注意事项

1. **附件关联**: 
   - coverImageId 为单个附件ID，用于封面图片
   - attachmentIds 为 JSON 数组字符串格式，如 "[1,2,3]"，用于正文附件
2. **软删除**: 删除信息发布使用软删除，不会立即删除数据
3. **浏览统计**: 每次访问信息详情接口会自动增加浏览次数
4. **分类管理**: 支持二级分类，可根据业务需求自定义分类
5. **关键词搜索**: 支持在标题和摘要中搜索关键词
6. **置顶功能**: isTop 字段控制信息是否置顶显示
7. **发布状态**: status 字段控制信息的发布状态（0-草稿，1-已发布）

## 内容格式建议

### HTML 内容格式
```html
<h2>一级标题</h2>
<p>段落内容...</p>
<h3>二级标题</h3>
<ul>
  <li>列表项1</li>
  <li>列表项2</li>
</ul>
<p>
  <img src="/uploads/images/example.jpg" alt="图片说明" />
</p>
<p>
  <a href="https://example.com" target="_blank">外部链接</a>
</p>
```

### 摘要建议
- 长度：50-200字
- 内容：简明扼要概括主要内容
- 格式：纯文本，不包含HTML标签

## 更新日志

### v2.0 (2024-01-01)
- 统一公司公告、政策法规、政策信息、通知公告管理
- 支持按信息类型和分类筛选
- 支持关键词搜索
- 支持封面图片和附件
- 支持置顶功能
- 自动浏览次数统计
- 支持草稿和发布状态管理