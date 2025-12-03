# 建设工程公告 API 使用示例

## 基础信息

- **基础URL**: `http://localhost:5000/api/construction/announcements`
- **认证方式**: JWT Bearer Token（创建、更新、删除操作需要）

## API 端点列表

### 1. 获取公告列表（分页 + 搜索）

**请求**
```http
GET /api/construction/announcements?pageIndex=1&pageSize=10&keyword=郑州&type=招标公告&region=郑州,洛阳&startDate=2024-01-01&endDate=2024-12-31&sortBy=publishtime&isDescending=true
```

**查询参数**
| 参数 | 类型 | 必填 | 说明 |
|------|------|------|------|
| pageIndex | int | 否 | 页码，默认1 |
| pageSize | int | 否 | 每页数量，默认10 |
| keyword | string | 否 | 关键词（搜索标题、招标人、中标人） |
| type | string | 否 | 公告类型：招标公告、中标公告、变更公告 |
| region | string | 否 | 地区（支持多个，逗号分隔） |
| startDate | datetime | 否 | 开始日期 |
| endDate | datetime | 否 | 结束日期 |
| sortBy | string | 否 | 排序字段：title、publishtime、viewcount |
| isDescending | bool | 否 | 是否降序，默认true |

**响应示例**
```json
{
  "success": true,
  "message": "获取成功",
  "data": {
    "items": [
      {
        "id": 1,
        "title": "郑州市某建设工程招标公告",
        "noticeType": "招标公告",
        "bidder": "郑州市建设局",
        "winner": null,
        "projectRegion": "郑州",
        "content": "<p>公告内容...</p>",
        "publisher": "管理员",
        "publishTime": "2024-01-15T10:00:00",
        "viewCount": 150,
        "attachmentPath": "/uploads/file.pdf",
        "isTop": false,
        "createdAt": "2024-01-15T09:00:00",
        "updatedAt": "2024-01-15T09:00:00"
      }
    ],
    "totalCount": 100,
    "pageIndex": 1,
    "pageSize": 10,
    "totalPages": 10
  }
}
```

### 2. 获取公告详情（自动增加访问量）

**请求**
```http
GET /api/construction/announcements/1
```

**响应示例**
```json
{
  "success": true,
  "message": "获取成功",
  "data": {
    "id": 1,
    "title": "郑州市某建设工程招标公告",
    "noticeType": "招标公告",
    "bidder": "郑州市建设局",
    "winner": null,
    "projectRegion": "郑州",
    "content": "<p>详细公告内容...</p>",
    "publisher": "管理员",
    "publishTime": "2024-01-15T10:00:00",
    "viewCount": 151,
    "attachmentPath": "/uploads/file.pdf",
    "isTop": false,
    "createdAt": "2024-01-15T09:00:00",
    "updatedAt": "2024-01-15T09:00:00"
  }
}
```

### 3. 创建公告（需要认证）

**请求**
```http
POST /api/construction/announcements
Authorization: Bearer {your_jwt_token}
Content-Type: application/json

{
  "title": "洛阳市某建设工程招标公告",
  "type": "招标公告",
  "content": "<p>公告详细内容...</p>",
  "tenderer": "洛阳市建设局",
  "winner": null,
  "region": "洛阳",
  "publishDate": "2024-01-20T10:00:00"
}
```

**请求体字段**
| 字段 | 类型 | 必填 | 说明 |
|------|------|------|------|
| title | string | 是 | 公告标题（最大255字符） |
| type | string | 是 | 公告类型：招标公告、中标公告、变更公告 |
| content | string | 是 | 公告内容（富文本） |
| tenderer | string | 否 | 招标人（最大255字符） |
| winner | string | 否 | 中标人（最大255字符） |
| region | string | 是 | 项目区域（最大50字符） |
| publishDate | datetime | 是 | 发布日期 |

**响应示例**
```json
{
  "success": true,
  "message": "创建成功",
  "data": {
    "id": 2,
    "title": "洛阳市某建设工程招标公告",
    "noticeType": "招标公告",
    "bidder": "洛阳市建设局",
    "winner": null,
    "projectRegion": "洛阳",
    "content": "<p>公告详细内容...</p>",
    "publisher": null,
    "publishTime": "2024-01-20T10:00:00",
    "viewCount": 0,
    "attachmentPath": null,
    "isTop": false,
    "createdAt": "2024-01-20T09:00:00",
    "updatedAt": "2024-01-20T09:00:00"
  }
}
```

### 4. 更新公告（需要认证）

**请求**
```http
PUT /api/construction/announcements/2
Authorization: Bearer {your_jwt_token}
Content-Type: application/json

{
  "title": "洛阳市某建设工程招标公告（更新）",
  "type": "中标公告",
  "content": "<p>更新后的公告内容...</p>",
  "winner": "某建筑公司"
}
```

**请求体字段（所有字段可选）**
| 字段 | 类型 | 必填 | 说明 |
|------|------|------|------|
| title | string | 否 | 公告标题 |
| type | string | 否 | 公告类型 |
| content | string | 否 | 公告内容 |
| tenderer | string | 否 | 招标人 |
| winner | string | 否 | 中标人 |
| region | string | 否 | 项目区域 |
| publishDate | datetime | 否 | 发布日期 |

**响应示例**
```json
{
  "success": true,
  "message": "更新成功",
  "data": {
    "id": 2,
    "title": "洛阳市某建设工程招标公告（更新）",
    "noticeType": "中标公告",
    "bidder": "洛阳市建设局",
    "winner": "某建筑公司",
    "projectRegion": "洛阳",
    "content": "<p>更新后的公告内容...</p>",
    "publisher": null,
    "publishTime": "2024-01-20T10:00:00",
    "viewCount": 0,
    "attachmentPath": null,
    "isTop": false,
    "createdAt": "2024-01-20T09:00:00",
    "updatedAt": "2024-01-20T10:30:00"
  }
}
```

### 5. 删除公告（软删除，需要认证）

**请求**
```http
DELETE /api/construction/announcements/2
Authorization: Bearer {your_jwt_token}
```

**响应示例**
```json
{
  "success": true,
  "message": "删除成功"
}
```

## 公告类型枚举

根据数据库设计，建设工程公告支持以下类型：

- `招标公告` - 建设工程招标公告
- `中标公告` - 建设工程中标公告
- `变更公告` - 建设工程变更公告

## 错误响应示例

### 未找到资源
```json
{
  "success": false,
  "message": "公告不存在",
  "data": null
}
```

### 验证错误
```json
{
  "success": false,
  "message": "请求参数无效",
  "data": null
}
```

### 未授权
```json
{
  "success": false,
  "message": "未授权访问",
  "data": null
}
```

## 使用 cURL 示例

### 获取公告列表
```bash
curl -X GET "http://localhost:5000/api/construction/announcements?pageIndex=1&pageSize=10&type=招标公告"
```

### 获取公告详情
```bash
curl -X GET "http://localhost:5000/api/construction/announcements/1"
```

### 创建公告
```bash
curl -X POST "http://localhost:5000/api/construction/announcements" \
  -H "Authorization: Bearer YOUR_JWT_TOKEN" \
  -H "Content-Type: application/json" \
  -d '{
    "title": "新建设工程公告",
    "type": "招标公告",
    "content": "<p>公告内容</p>",
    "tenderer": "某建设单位",
    "region": "郑州",
    "publishDate": "2024-01-20T10:00:00"
  }'
```

### 更新公告
```bash
curl -X PUT "http://localhost:5000/api/construction/announcements/1" \
  -H "Authorization: Bearer YOUR_JWT_TOKEN" \
  -H "Content-Type: application/json" \
  -d '{
    "title": "更新后的标题",
    "winner": "中标单位名称"
  }'
```

### 删除公告
```bash
curl -X DELETE "http://localhost:5000/api/construction/announcements/1" \
  -H "Authorization: Bearer YOUR_JWT_TOKEN"
```

## 注意事项

1. **认证要求**：创建、更新、删除操作需要JWT认证
2. **角色权限**：
   - 创建和更新：需要 `admin` 或 `manager` 角色
   - 删除：仅 `admin` 角色
3. **软删除**：删除操作为软删除，数据不会真正从数据库中移除
4. **自动访问量**：获取详情时会自动增加访问量计数
5. **公告类型**：必须是以下之一：招标公告、中标公告、变更公告
6. **地区筛选**：支持多个地区，使用逗号分隔
7. **时间格式**：使用ISO 8601格式（例如：2024-01-20T10:00:00）

## 与政府采购公告的区别

建设工程公告与政府采购公告的主要区别：

1. **公告类型不同**：
   - 政府采购：采购公告、更正公告、结果公告
   - 建设工程：招标公告、中标公告、变更公告

2. **API路径不同**：
   - 政府采购：`/api/gov-procurement/announcements`
   - 建设工程：`/api/construction/announcements`

3. **数据表不同**：
   - 政府采购：`government_procurement_notices`
   - 建设工程：`construction_project_notices`

其他功能和字段结构完全相同。