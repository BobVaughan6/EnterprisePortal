# 全局搜索API文档

## API概述
全局搜索API支持跨政府采购和建设工程公告的统一搜索功能。

## 接口地址
```
POST /api/search
```

## 请求参数

### 请求体 (JSON)
```json
{
  "keyword": "学校建设",           // 搜索关键词（匹配标题、招标人、中标人字段）
  "category": "all",              // all|gov_procurement|construction
  "announcementType": "",         // 公告类型（可选，根据数据库类型字段）
  "region": [],                   // 区域数组（值必须匹配数据库区域字段的枚举值）
  "startDate": "2024-01-01",      // 开始时间（匹配数据库时间字段）
  "endDate": "2025-12-31",        // 结束时间
  "pageIndex": 1,                 // 页码
  "pageSize": 20                  // 每页数量
}
```

### 参数说明

| 参数名 | 类型 | 必填 | 说明 |
|--------|------|------|------|
| keyword | string | 否 | 搜索关键词，支持模糊匹配标题、招标人、中标人 |
| category | string | 否 | 分类筛选：all-全部、gov_procurement-政府采购、construction-建设工程，默认all |
| announcementType | string | 否 | 公告类型筛选 |
| region | array | 否 | 区域筛选，支持多选 |
| startDate | datetime | 否 | 开始时间 |
| endDate | datetime | 否 | 结束时间 |
| pageIndex | int | 是 | 页码，从1开始 |
| pageSize | int | 是 | 每页数量 |

## 响应结果

### 成功响应
```json
{
  "success": true,
  "message": "操作成功",
  "data": {
    "total": 156,
    "pageIndex": 1,
    "pageSize": 20,
    "items": [
      {
        "id": 123,
        "title": "XX<em>学校建设</em>项目招标公告",
        "category": "construction",
        "type": "招标公告",
        "tenderer": "XX教育局",
        "region": "郑州市",
        "publishDate": "2025-11-20T00:00:00",
        "viewCount": 256
      }
    ]
  }
}
```

### 响应字段说明

| 字段名 | 类型 | 说明 |
|--------|------|------|
| success | boolean | 请求是否成功 |
| message | string | 响应消息 |
| data.total | int | 总记录数 |
| data.pageIndex | int | 当前页码 |
| data.pageSize | int | 每页数量 |
| data.items | array | 搜索结果列表 |
| items[].id | int | 公告ID |
| items[].title | string | 公告标题（关键词已用`<em>`标签包裹） |
| items[].category | string | 分类：construction-建设工程、gov_procurement-政府采购 |
| items[].type | string | 公告类型 |
| items[].tenderer | string | 招标人 |
| items[].region | string | 区域 |
| items[].publishDate | datetime | 发布日期 |
| items[].viewCount | int | 访问量 |

## 功能特性

1. **跨表搜索**：同时搜索政府采购和建设工程两张表
2. **关键词高亮**：搜索结果中的关键词自动用`<em>`标签包裹
3. **多条件筛选**：支持分类、类型、区域、时间范围等多维度筛选
4. **模糊匹配**：关键词支持模糊匹配标题、招标人、中标人字段
5. **软删除过滤**：自动过滤已删除的记录
6. **时间排序**：结果按发布时间倒序排列

## 请求示例

### 示例1：搜索所有包含"学校"的公告
```bash
curl -X POST "http://localhost:5000/api/search" \
  -H "Content-Type: application/json" \
  -d '{
    "keyword": "学校",
    "category": "all",
    "pageIndex": 1,
    "pageSize": 20
  }'
```

### 示例2：搜索郑州市的建设工程招标公告
```bash
curl -X POST "http://localhost:5000/api/search" \
  -H "Content-Type: application/json" \
  -d '{
    "keyword": "",
    "category": "construction",
    "announcementType": "招标公告",
    "region": ["郑州市"],
    "pageIndex": 1,
    "pageSize": 20
  }'
```

### 示例3：搜索2024年的政府采购公告
```bash
curl -X POST "http://localhost:5000/api/search" \
  -H "Content-Type: application/json" \
  -d '{
    "category": "gov_procurement",
    "startDate": "2024-01-01",
    "endDate": "2024-12-31",
    "pageIndex": 1,
    "pageSize": 20
  }'
```

### 示例4：多条件组合搜索
```bash
curl -X POST "http://localhost:5000/api/search" \
  -H "Content-Type: application/json" \
  -d '{
    "keyword": "建设",
    "category": "all",
    "region": ["郑州市", "洛阳市"],
    "startDate": "2024-01-01",
    "endDate": "2025-12-31",
    "pageIndex": 1,
    "pageSize": 20
  }'
```

## 错误处理

### 错误响应示例
```json
{
  "success": false,
  "message": "搜索失败，请稍后重试",
  "data": null
}
```

## 数据库字段映射

### 政府采购公告表 (government_procurement_notices)
- **title**: 公告标题
- **notice_type**: 公告类型
- **bidder**: 招标人
- **winner**: 中标人
- **project_region**: 项目区域
- **publish_time**: 发布时间
- **view_count**: 访问量
- **is_deleted**: 软删除标记

### 建设工程公告表 (construction_project_notices)
- **title**: 公告标题
- **notice_type**: 公告类型
- **bidder**: 招标人
- **winner**: 中标人
- **project_region**: 项目区域
- **publish_time**: 发布时间
- **view_count**: 访问量
- **is_deleted**: 软删除标记

## 注意事项

1. 所有搜索字段名与数据库字段名完全一致
2. 关键词搜索不区分大小写
3. 区域值必须与数据库中的枚举值完全匹配
4. 时间范围包含起始和结束日期
5. 分页从1开始，不是0
6. 自动过滤软删除记录（is_deleted = 0）

## 性能优化建议

1. 数据库已建立全文索引：`FULLTEXT INDEX ft_title_content (title, content)`
2. 已建立常用查询字段索引：notice_type, project_region, publish_time
3. 建议合理设置pageSize，避免一次查询过多数据
4. 对于高频搜索关键词，可考虑添加缓存机制

## 版本历史

- **v1.0** (2025-12-03): 初始版本，支持基础全局搜索功能