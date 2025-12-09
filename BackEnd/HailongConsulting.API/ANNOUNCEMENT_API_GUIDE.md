# 统一公告管理 API 文档

## 概述
统一公告管理 API 整合了政府采购公告和建设工程公告，提供统一的接口进行公告的创建、查询、更新和删除操作。

## 基础信息
- **Base URL**: `/api/announcements`
- **认证方式**: JWT Bearer Token（部分接口需要）

## 业务类型说明

| 业务类型 | 值 | 说明 |
|----------|-----|------|
| 政府采购 | GOV_PROCUREMENT | 政府采购相关公告 |
| 建设工程 | CONSTRUCTION | 建设工程相关公告 |

## 公告类型说明

| 公告类型 | 值 | 说明 |
|----------|-----|------|
| 招标公告 | bidding | 招标/采购公告 |
| 更正公告 | correction | 更正公告 |
| 结果公告 | result | 中标/成交结果公告 |

## API 接口列表

### 1. 创建公告

**接口**: `POST /api/announcements`

**认证**: 需要

**请求体**:
```json
{
  "title": "某项目招标公告",
  "businessType": "GOV_PROCUREMENT",
  "noticeType": "bidding",
  "projectName": "某政府采购项目",
  "projectCode": "PROJ-2024-001",
  "purchaser": "某政府单位",
  "agent": "某招标代理公司",
  "budget": 1000000.00,
  "province": "北京市",
  "city": "北京市",
  "district": "朝阳区",
  "publishDate": "2024-01-01T00:00:00Z",
  "deadline": "2024-01-15T00:00:00Z",
  "content": "<p>公告详细内容...</p>",
  "contactPerson": "张三",
  "contactPhone": "010-12345678",
  "contactEmail": "zhangsan@example.com",
  "attachmentIds": "[1,2,3]",
  "sourceUrl": "https://example.com/notice/123",
  "isTop": 0,
  "status": 1
}
```

**响应示例**:
```json
{
  "success": true,
  "message": "创建公告成功",
  "data": {
    "id": 1,
    "title": "某项目招标公告",
    "businessType": "GOV_PROCUREMENT",
    "noticeType": "bidding",
    "projectName": "某政府采购项目",
    "projectCode": "PROJ-2024-001",
    "purchaser": "某政府单位",
    "agent": "某招标代理公司",
    "budget": 1000000.00,
    "province": "北京市",
    "city": "北京市",
    "district": "朝阳区",
    "publishDate": "2024-01-01T00:00:00Z",
    "deadline": "2024-01-15T00:00:00Z",
    "content": "<p>公告详细内容...</p>",
    "contactPerson": "张三",
    "contactPhone": "010-12345678",
    "contactEmail": "zhangsan@example.com",
    "attachmentIds": [1, 2, 3],
    "sourceUrl": "https://example.com/notice/123",
    "viewCount": 0,
    "isTop": false,
    "status": 1,
    "createdAt": "2024-01-01T12:00:00Z",
    "updatedAt": "2024-01-01T12:00:00Z"
  }
}
```

---

### 2. 更新公告

**接口**: `PUT /api/announcements/{id}`

**认证**: 需要

**路径参数**:
- `id`: 公告ID

**请求体**: 同创建公告（所有字段可选）

**响应示例**:
```json
{
  "success": true,
  "message": "更新公告成功",
  "data": {
    "id": 1,
    "title": "某项目招标公告（已更新）",
    ...
  }
}
```

---

### 3. 获取公告详情

**接口**: `GET /api/announcements/{id}`

**认证**: 不需要

**路径参数**:
- `id`: 公告ID

**响应示例**:
```json
{
  "success": true,
  "message": "获取公告成功",
  "data": {
    "id": 1,
    "title": "某项目招标公告",
    "businessType": "GOV_PROCUREMENT",
    "noticeType": "bidding",
    "projectName": "某政府采购项目",
    "projectCode": "PROJ-2024-001",
    "purchaser": "某政府单位",
    "agent": "某招标代理公司",
    "budget": 1000000.00,
    "province": "北京市",
    "city": "北京市",
    "district": "朝阳区",
    "publishDate": "2024-01-01T00:00:00Z",
    "deadline": "2024-01-15T00:00:00Z",
    "content": "<p>公告详细内容...</p>",
    "contactPerson": "张三",
    "contactPhone": "010-12345678",
    "contactEmail": "zhangsan@example.com",
    "attachmentIds": [1, 2, 3],
    "sourceUrl": "https://example.com/notice/123",
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

### 4. 分页查询公告列表

**接口**: `GET /api/announcements`

**认证**: 不需要

**查询参数**:
- `businessType`: string (可选) - 业务类型：GOV_PROCUREMENT/CONSTRUCTION
- `noticeType`: string (可选) - 公告类型：bidding/correction/result
- `province`: string (可选) - 省份
- `city`: string (可选) - 城市
- `district`: string (可选) - 区县
- `keyword`: string (可选) - 关键词搜索（标题、项目名称）
- `pageNumber`: int (可选) - 页码，默认 1
- `pageSize`: int (可选) - 每页数量，默认 10

**请求示例**:
```
GET /api/announcements?businessType=GOV_PROCUREMENT&noticeType=bidding&province=北京市&keyword=采购&pageNumber=1&pageSize=10
```

**响应示例**:
```json
{
  "success": true,
  "message": "获取公告列表成功",
  "data": {
    "items": [
      {
        "id": 1,
        "title": "某项目招标公告",
        "businessType": "GOV_PROCUREMENT",
        "noticeType": "bidding",
        ...
      },
      {
        "id": 2,
        "title": "另一项目招标公告",
        ...
      }
    ],
    "totalCount": 100,
    "pageIndex": 1,
    "pageSize": 10,
    "totalPages": 10
  }
}
```

---

### 5. 删除公告

**接口**: `DELETE /api/announcements/{id}`

**认证**: 需要

**路径参数**:
- `id`: 公告ID

**响应示例**:
```json
{
  "success": true,
  "message": "删除公告成功",
  "data": true
}
```

---

### 6. 获取政府采购公告列表

**接口**: `GET /api/announcements/gov-procurement`

**认证**: 不需要

**查询参数**: 同"分页查询公告列表"（自动设置 businessType=GOV_PROCUREMENT）

**请求示例**:
```
GET /api/announcements/gov-procurement?noticeType=bidding&keyword=采购&pageNumber=1&pageSize=10
```

**响应示例**: 同"分页查询公告列表"

---

### 7. 获取建设工程公告列表

**接口**: `GET /api/announcements/construction`

**认证**: 不需要

**查询参数**: 同"分页查询公告列表"（自动设置 businessType=CONSTRUCTION）

**请求示例**:
```
GET /api/announcements/construction?noticeType=bidding&province=北京市&pageNumber=1&pageSize=10
```

**响应示例**: 同"分页查询公告列表"

---

## 字段说明

### 必填字段
- `title`: 公告标题
- `businessType`: 业务类型（GOV_PROCUREMENT/CONSTRUCTION）
- `noticeType`: 公告类型（bidding/correction/result）
- `content`: 公告内容（HTML格式）

### 可选字段
- `projectName`: 项目名称
- `projectCode`: 项目编号
- `purchaser`: 采购人/招标人
- `agent`: 代理机构
- `budget`: 预算金额
- `province`: 省份
- `city`: 城市
- `district`: 区县
- `publishDate`: 发布日期
- `deadline`: 截止日期
- `contactPerson`: 联系人
- `contactPhone`: 联系电话
- `contactEmail`: 联系邮箱
- `attachmentIds`: 附件ID列表（JSON数组字符串）
- `sourceUrl`: 来源链接
- `isTop`: 是否置顶（0/1）
- `status`: 状态（0-草稿，1-已发布）

## 使用示例

### JavaScript (Axios)

```javascript
// 创建公告
const createAnnouncement = async (data) => {
  const response = await axios.post('/api/announcements', data, {
    headers: {
      'Authorization': `Bearer ${token}`
    }
  });
  return response.data;
};

// 查询公告列表
const getAnnouncements = async (params) => {
  const response = await axios.get('/api/announcements', { params });
  return response.data;
};

// 获取政府采购公告
const getGovProcurementAnnouncements = async (params) => {
  const response = await axios.get('/api/announcements/gov-procurement', { params });
  return response.data;
};

// 获取建设工程公告
const getConstructionAnnouncements = async (params) => {
  const response = await axios.get('/api/announcements/construction', { params });
  return response.data;
};

// 获取公告详情
const getAnnouncementDetail = async (id) => {
  const response = await axios.get(`/api/announcements/${id}`);
  return response.data;
};

// 更新公告
const updateAnnouncement = async (id, data) => {
  const response = await axios.put(`/api/announcements/${id}`, data, {
    headers: {
      'Authorization': `Bearer ${token}`
    }
  });
  return response.data;
};

// 删除公告
const deleteAnnouncement = async (id) => {
  const response = await axios.delete(`/api/announcements/${id}`, {
    headers: {
      'Authorization': `Bearer ${token}`
    }
  });
  return response.data;
};
```

### C# (.NET)

```csharp
// 创建公告
var announcement = new CreateAnnouncementDto
{
    Title = "某项目招标公告",
    BusinessType = "GOV_PROCUREMENT",
    NoticeType = "bidding",
    Content = "<p>公告详细内容...</p>",
    // ... 其他字段
};

using var client = new HttpClient();
client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
var json = JsonSerializer.Serialize(announcement);
var content = new StringContent(json, Encoding.UTF8, "application/json");
var response = await client.PostAsync("/api/announcements", content);
var result = await response.Content.ReadAsStringAsync();

// 查询公告列表
var queryString = $"?businessType=GOV_PROCUREMENT&pageNumber=1&pageSize=10";
var response = await client.GetAsync($"/api/announcements{queryString}");
var result = await response.Content.ReadAsStringAsync();
```

## 错误码说明

| 错误码 | 说明 |
|--------|------|
| 400 | 请求参数错误 |
| 401 | 未授权（需要登录） |
| 404 | 公告不存在 |
| 500 | 服务器内部错误 |

## 注意事项

1. **附件关联**: 创建公告时，attachmentIds 应为 JSON 数组字符串格式，如 "[1,2,3]"
2. **软删除**: 删除公告使用软删除，不会立即删除数据
3. **浏览统计**: 每次访问公告详情接口会自动增加浏览次数
4. **区域筛选**: 支持省市区三级筛选
5. **关键词搜索**: 支持在标题和项目名称中搜索关键词
6. **置顶功能**: isTop 字段控制公告是否置顶显示

## 更新日志

### v2.0 (2024-01-01)
- 统一政府采购和建设工程公告管理
- 支持按业务类型和公告类型筛选
- 支持省市区三级区域筛选
- 支持关键词搜索
- 支持置顶功能
- 自动浏览次数统计