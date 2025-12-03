# 政府采购公告 API 使用示例

## API 基础信息

- **基础URL**: `http://localhost:5000/api/gov-procurement/announcements`
- **认证方式**: JWT Bearer Token（创建、更新、删除操作需要）
- **数据库表**: `government_procurement_notices`

---

## 1. 创建公告 (POST)

**端点**: `POST /api/gov-procurement/announcements`

**需要认证**: ✅ 是（admin 或 manager 角色）

**请求头**:
```
Authorization: Bearer {your_jwt_token}
Content-Type: application/json
```

**请求体示例**:

### 采购公告
```json
{
  "title": "郑州市政府办公设备采购项目公告",
  "type": "采购公告",
  "content": "<h2>项目概况</h2><p>本项目为郑州市政府办公设备采购项目，预算金额500万元...</p><h3>采购内容</h3><ul><li>台式电脑 100台</li><li>笔记本电脑 50台</li><li>打印机 20台</li></ul>",
  "tenderer": "郑州市政府采购中心",
  "bidder": null,
  "region": "郑州",
  "publishDate": "2024-01-15T09:00:00"
}
```

### 更正公告
```json
{
  "title": "洛阳市医疗设备采购项目更正公告",
  "type": "更正公告",
  "content": "<h2>更正说明</h2><p>原公告中投标截止时间有误，现更正如下...</p>",
  "tenderer": "洛阳市卫生局",
  "bidder": null,
  "region": "洛阳",
  "publishDate": "2024-01-18T14:00:00"
}
```

### 结果公告
```json
{
  "title": "开封市教育信息化建设项目中标结果公告",
  "type": "结果公告",
  "content": "<h2>中标结果</h2><p>经评审，确定以下单位为中标单位...</p>",
  "tenderer": "开封市教育局",
  "bidder": "河南某某科技有限公司",
  "region": "开封",
  "publishDate": "2024-02-01T10:00:00"
}
```

**成功响应** (201 Created):
```json
{
  "success": true,
  "message": "创建成功",
  "data": {
    "id": 1,
    "title": "郑州市政府办公设备采购项目公告",
    "noticeType": "采购公告",
    "content": "<h2>项目概况</h2><p>本项目为郑州市政府办公设备采购项目...</p>",
    "bidder": "郑州市政府采购中心",
    "winner": null,
    "projectRegion": "郑州",
    "publisher": null,
    "publishTime": "2024-01-15T09:00:00",
    "viewCount": 0,
    "attachmentPath": null,
    "isTop": false,
    "createdAt": "2024-01-15T08:30:00",
    "updatedAt": "2024-01-15T08:30:00"
  }
}
```

---

## 2. 获取公告列表 (GET)

**端点**: `GET /api/gov-procurement/announcements`

**需要认证**: ❌ 否

**查询参数**:

| 参数 | 类型 | 必填 | 说明 | 示例 |
|------|------|------|------|------|
| pageIndex | int | 否 | 页码（默认1） | 1 |
| pageSize | int | 否 | 每页数量（默认10） | 20 |
| keyword | string | 否 | 关键词（搜索标题、招标人、中标人） | 办公设备 |
| type | string | 否 | 公告类型 | 采购公告 |
| region | string | 否 | 地区（多个用逗号分隔） | 郑州,洛阳 |
| startDate | datetime | 否 | 开始日期 | 2024-01-01 |
| endDate | datetime | 否 | 结束日期 | 2024-12-31 |
| sortBy | string | 否 | 排序字段 | publishTime |
| isDescending | bool | 否 | 是否降序（默认true） | true |

**请求示例**:

### 基础查询
```
GET /api/gov-procurement/announcements?pageIndex=1&pageSize=10
```

### 关键词搜索
```
GET /api/gov-procurement/announcements?keyword=办公设备&pageIndex=1&pageSize=10
```

### 按类型筛选
```
GET /api/gov-procurement/announcements?type=采购公告&pageIndex=1&pageSize=10
```

### 按地区筛选（多个地区）
```
GET /api/gov-procurement/announcements?region=郑州,洛阳,开封&pageIndex=1&pageSize=10
```

### 时间区间筛选
```
GET /api/gov-procurement/announcements?startDate=2024-01-01&endDate=2024-12-31&pageIndex=1&pageSize=10
```

### 综合查询
```
GET /api/gov-procurement/announcements?keyword=办公&type=采购公告&region=郑州&startDate=2024-01-01&endDate=2024-12-31&sortBy=publishTime&isDescending=true&pageIndex=1&pageSize=20
```

**成功响应** (200 OK):
```json
{
  "success": true,
  "message": "获取成功",
  "data": {
    "items": [
      {
        "id": 1,
        "title": "郑州市政府办公设备采购项目公告",
        "noticeType": "采购公告",
        "content": "<h2>项目概况</h2><p>本项目为郑州市政府办公设备采购项目...</p>",
        "bidder": "郑州市政府采购中心",
        "winner": null,
        "projectRegion": "郑州",
        "publisher": null,
        "publishTime": "2024-01-15T09:00:00",
        "viewCount": 125,
        "attachmentPath": null,
        "isTop": false,
        "createdAt": "2024-01-15T08:30:00",
        "updatedAt": "2024-01-15T08:30:00"
      },
      {
        "id": 2,
        "title": "洛阳市医疗设备采购项目公告",
        "noticeType": "采购公告",
        "content": "<h2>项目概况</h2><p>本项目为洛阳市医疗设备采购项目...</p>",
        "bidder": "洛阳市卫生局",
        "winner": null,
        "projectRegion": "洛阳",
        "publisher": null,
        "publishTime": "2024-01-16T10:00:00",
        "viewCount": 89,
        "attachmentPath": null,
        "isTop": false,
        "createdAt": "2024-01-16T09:30:00",
        "updatedAt": "2024-01-16T09:30:00"
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

## 3. 获取公告详情 (GET)

**端点**: `GET /api/gov-procurement/announcements/{id}`

**需要认证**: ❌ 否

**路径参数**:
- `id`: 公告ID

**请求示例**:
```
GET /api/gov-procurement/announcements/1
```

**成功响应** (200 OK):
```json
{
  "success": true,
  "message": "获取成功",
  "data": {
    "id": 1,
    "title": "郑州市政府办公设备采购项目公告",
    "noticeType": "采购公告",
    "content": "<h2>项目概况</h2><p>本项目为郑州市政府办公设备采购项目，预算金额500万元...</p><h3>采购内容</h3><ul><li>台式电脑 100台</li><li>笔记本电脑 50台</li><li>打印机 20台</li></ul>",
    "bidder": "郑州市政府采购中心",
    "winner": null,
    "projectRegion": "郑州",
    "publisher": null,
    "publishTime": "2024-01-15T09:00:00",
    "viewCount": 126,
    "attachmentPath": null,
    "isTop": false,
    "createdAt": "2024-01-15T08:30:00",
    "updatedAt": "2024-01-15T08:30:00"
  }
}
```

**注意**: 每次访问详情页面，`viewCount` 会自动增加1。

**失败响应** (404 Not Found):
```json
{
  "success": false,
  "message": "公告不存在",
  "data": null
}
```

---

## 4. 更新公告 (PUT)

**端点**: `PUT /api/gov-procurement/announcements/{id}`

**需要认证**: ✅ 是（admin 或 manager 角色）

**请求头**:
```
Authorization: Bearer {your_jwt_token}
Content-Type: application/json
```

**路径参数**:
- `id`: 公告ID

**请求体示例** (所有字段都是可选的):
```json
{
  "title": "郑州市政府办公设备采购项目公告（已更新）",
  "content": "<h2>项目概况</h2><p>本项目为郑州市政府办公设备采购项目，预算金额调整为600万元...</p>",
  "publishDate": "2024-01-15T10:00:00"
}
```

**完整更新示例**:
```json
{
  "title": "郑州市政府办公设备采购项目中标结果公告",
  "type": "结果公告",
  "content": "<h2>中标结果</h2><p>经评审，确定以下单位为中标单位...</p>",
  "tenderer": "郑州市政府采购中心",
  "bidder": "河南某某科技有限公司",
  "region": "郑州",
  "publishDate": "2024-02-01T10:00:00"
}
```

**成功响应** (200 OK):
```json
{
  "success": true,
  "message": "更新成功",
  "data": {
    "id": 1,
    "title": "郑州市政府办公设备采购项目公告（已更新）",
    "noticeType": "采购公告",
    "content": "<h2>项目概况</h2><p>本项目为郑州市政府办公设备采购项目，预算金额调整为600万元...</p>",
    "bidder": "郑州市政府采购中心",
    "winner": null,
    "projectRegion": "郑州",
    "publisher": null,
    "publishTime": "2024-01-15T10:00:00",
    "viewCount": 126,
    "attachmentPath": null,
    "isTop": false,
    "createdAt": "2024-01-15T08:30:00",
    "updatedAt": "2024-01-20T14:30:00"
  }
}
```

**失败响应** (404 Not Found):
```json
{
  "success": false,
  "message": "公告不存在",
  "data": null
}
```

---

## 5. 删除公告 (DELETE)

**端点**: `DELETE /api/gov-procurement/announcements/{id}`

**需要认证**: ✅ 是（仅 admin 角色）

**请求头**:
```
Authorization: Bearer {your_jwt_token}
```

**路径参数**:
- `id`: 公告ID

**请求示例**:
```
DELETE /api/gov-procurement/announcements/1
```

**成功响应** (200 OK):
```json
{
  "success": true,
  "message": "删除成功"
}
```

**失败响应** (404 Not Found):
```json
{
  "success": false,
  "message": "公告不存在"
}
```

**注意**: 这是软删除，数据不会真正从数据库中删除，只是标记为已删除（`is_deleted = 1`）。

---

## 完整的 cURL 示例

### 1. 创建公告
```bash
curl -X POST "http://localhost:5000/api/gov-procurement/announcements" \
  -H "Authorization: Bearer YOUR_JWT_TOKEN" \
  -H "Content-Type: application/json" \
  -d '{
    "title": "郑州市政府办公设备采购项目公告",
    "type": "采购公告",
    "content": "<h2>项目概况</h2><p>本项目为郑州市政府办公设备采购项目...</p>",
    "tenderer": "郑州市政府采购中心",
    "region": "郑州",
    "publishDate": "2024-01-15T09:00:00"
  }'
```

### 2. 获取公告列表
```bash
curl -X GET "http://localhost:5000/api/gov-procurement/announcements?keyword=办公&type=采购公告&region=郑州&pageIndex=1&pageSize=10"
```

### 3. 获取公告详情
```bash
curl -X GET "http://localhost:5000/api/gov-procurement/announcements/1"
```

### 4. 更新公告
```bash
curl -X PUT "http://localhost:5000/api/gov-procurement/announcements/1" \
  -H "Authorization: Bearer YOUR_JWT_TOKEN" \
  -H "Content-Type: application/json" \
  -d '{
    "title": "郑州市政府办公设备采购项目公告（已更新）",
    "content": "<h2>项目概况</h2><p>更新后的内容...</p>"
  }'
```

### 5. 删除公告
```bash
curl -X DELETE "http://localhost:5000/api/gov-procurement/announcements/1" \
  -H "Authorization: Bearer YOUR_JWT_TOKEN"
```

---

## 数据库表结构

使用现有的 `government_procurement_notices` 表：

```sql
CREATE TABLE `government_procurement_notices` (
  `id` INT UNSIGNED AUTO_INCREMENT PRIMARY KEY COMMENT '公告ID',
  `title` VARCHAR(255) NOT NULL COMMENT '公告标题',
  `notice_type` VARCHAR(50) NOT NULL COMMENT '公告类型：采购公告、更正公告、结果公告',
  `bidder` VARCHAR(255) DEFAULT NULL COMMENT '招标人',
  `winner` VARCHAR(255) DEFAULT NULL COMMENT '中标人',
  `project_region` VARCHAR(50) NOT NULL COMMENT '项目区域：郑州、洛阳、开封、新乡等',
  `content` LONGTEXT NOT NULL COMMENT '公告内容（富文本）',
  `publisher` VARCHAR(50) DEFAULT NULL COMMENT '发布人',
  `publish_time` DATETIME DEFAULT NULL COMMENT '发布时间',
  `view_count` INT UNSIGNED DEFAULT 0 COMMENT '访问量',
  `attachment_path` VARCHAR(500) DEFAULT NULL COMMENT '附件路径',
  `is_top` TINYINT DEFAULT 0 COMMENT '是否置顶：0-否，1-是',
  `is_deleted` TINYINT DEFAULT 0 COMMENT '软删除：0-未删除，1-已删除',
  `created_at` DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `updated_at` DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '更新时间',
  INDEX `idx_notice_type` (`notice_type`),
  INDEX `idx_project_region` (`project_region`),
  INDEX `idx_publish_time` (`publish_time`),
  INDEX `idx_is_top` (`is_top`),
  INDEX `idx_is_deleted` (`is_deleted`),
  FULLTEXT INDEX `ft_title_content` (`title`, `content`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci COMMENT='政府采购公告表';
```

---

## 字段映射说明

| API字段 | 数据库字段 | 说明 |
|---------|-----------|------|
| title | title | 公告标题 |
| type | notice_type | 公告类型 |
| tenderer | bidder | 招标人 |
| bidder | winner | 中标人 |
| region | project_region | 项目区域 |
| content | content | 公告内容 |
| publishDate | publish_time | 发布时间 |

---

## 错误响应格式

所有错误响应都遵循统一格式：

```json
{
  "success": false,
  "message": "错误描述信息",
  "data": null
}
```

常见HTTP状态码：
- `200 OK`: 请求成功
- `201 Created`: 创建成功
- `400 Bad Request`: 请求参数错误
- `401 Unauthorized`: 未认证
- `403 Forbidden`: 无权限
- `404 Not Found`: 资源不存在
- `500 Internal Server Error`: 服务器错误

---

## 特殊功能说明

### 1. 自动增加访问量
每次调用 `GET /api/gov-procurement/announcements/{id}` 获取详情时，该公告的 `view_count` 会自动增加1。

### 2. 模糊搜索
`keyword` 参数支持对以下字段进行模糊搜索：
- 标题 (title)
- 招标人 (bidder)
- 中标人 (winner)

### 3. 多地区筛选
`region` 参数支持多个地区，使用逗号分隔：
```
region=郑州,洛阳,开封,新乡
```

支持的地区包括：郑州、洛阳、开封、新乡、焦作、安阳、鹤壁、濮阳、许昌、漯河、三门峡、南阳、商丘、信阳、周口、驻马店、济源等河南省内城市。

### 4. 软删除
删除操作不会真正删除数据，只是将 `is_deleted` 标记为 `1`。已删除的公告不会在列表和详情中显示。

### 5. 排序支持
`sortBy` 参数支持以下字段：
- `title`: 按标题排序
- `publishTime`: 按发布时间排序
- `viewCount`: 按浏览次数排序
- 默认: 按创建时间排序

---

## 使用步骤

1. 确保数据库已创建 `government_procurement_notices` 表（已在 `hailong_consulting_schema.sql` 中定义）

2. 启动API服务：
   ```bash
   cd BackEnd/HailongConsulting.API
   dotnet run
   ```

3. 访问Swagger文档：`http://localhost:5000`

4. 使用本文档中的示例进行API调用