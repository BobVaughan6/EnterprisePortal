# 政府采购公告管理 - 字段映射说明文档

## 概述

本文档详细说明了政府采购公告管理页面中，前端表单字段、后端DTO字段、数据库字段之间的对应关系。

---

## 1. 列表查询（GET /api/gov-procurement/announcements）

### 查询参数映射

| 前端字段 | 后端DTO字段 (GovProcurementQueryViewModel) | 说明 |
|---------|-------------------------------------------|------|
| `keyword` | `Keyword` | 关键词搜索（标题、招标人、中标人） |
| `type` | `Type` | 公告类型 |
| `region` | `Region` | 项目区域 |
| `startDate` | `StartDate` | 开始日期 |
| `endDate` | `EndDate` | 结束日期 |
| `pageIndex` | `PageIndex` | 页码（默认1） |
| `pageSize` | `PageSize` | 每页数量（默认10） |

### 返回数据映射

| 前端显示字段 | 后端DTO字段 (GovProcurementDto) | 数据库字段 | 说明 |
|------------|-------------------------------|-----------|------|
| `id` | `Id` | `id` | 公告ID |
| `title` | `Title` | `title` | 公告标题 |
| `noticeType` | `NoticeType` | `notice_type` | 公告类型 |
| `bidder` | `Bidder` | `bidder` | 招标人 |
| `winner` | `Winner` | `winner` | 中标人 |
| `projectRegion` | `ProjectRegion` | `project_region` | 项目区域 |
| `publishTime` | `PublishTime` | `publish_time` | 发布时间 |
| `viewCount` | `ViewCount` | `view_count` | 访问量 |
| `createdAt` | `CreatedAt` | `created_at` | 创建时间 |
| `updatedAt` | `UpdatedAt` | `updated_at` | 更新时间 |

---

## 2. 创建公告（POST /api/gov-procurement/announcements）

### 请求数据映射

| 前端表单字段 | 后端DTO字段 (CreateGovProcurementDto) | 数据库字段 | 验证规则 | 说明 |
|------------|-------------------------------------|-----------|---------|------|
| `title` | `Title` | `title` | Required, MaxLength(255) | 公告标题 |
| `type` | `Type` | `notice_type` | Required, RegEx(采购公告\|更正公告\|结果公告) | 公告类型 |
| `content` | `Content` | `content` | Required | 公告内容（富文本） |
| `tenderer` | `Tenderer` | `bidder` | MaxLength(255) | 招标人（可选） |
| `bidder` | `Bidder` | `winner` | MaxLength(255) | 中标人（可选） |
| `region` | `Region` | `project_region` | Required, MaxLength(50) | 项目区域 |
| `publishDate` | `PublishDate` | `publish_time` | Required | 发布日期 |

**注意事项：**
- 前端 `tenderer` → 后端 `Tenderer` → 数据库 `bidder`（招标人）
- 前端 `bidder` → 后端 `Bidder` → 数据库 `winner`（中标人）

---

## 3. 更新公告（PUT /api/gov-procurement/announcements/{id}）

### 请求数据映射

| 前端表单字段 | 后端DTO字段 (UpdateGovProcurementDto) | 数据库字段 | 验证规则 | 说明 |
|------------|-------------------------------------|-----------|---------|------|
| `title` | `Title` | `title` | MaxLength(255) | 公告标题（可选） |
| `type` | `Type` | `notice_type` | RegEx(采购公告\|更正公告\|结果公告) | 公告类型（可选） |
| `content` | `Content` | `content` | - | 公告内容（可选） |
| `tenderer` | `Tenderer` | `bidder` | MaxLength(255) | 招标人（可选） |
| `bidder` | `Bidder` | `winner` | MaxLength(255) | 中标人（可选） |
| `region` | `Region` | `project_region` | MaxLength(50) | 项目区域（可选） |
| `publishDate` | `PublishDate` | `publish_time` | - | 发布日期（可选） |

**注意事项：**
- UpdateDto 所有字段均为可选（nullable）
- 只更新提供的字段，未提供的字段保持不变

---

## 4. 详情查询（GET /api/gov-procurement/announcements/{id}）

### 返回数据映射

| 前端字段 | 后端DTO字段 (GovProcurementDto) | 数据库字段 | 说明 |
|---------|-------------------------------|-----------|------|
| `id` | `Id` | `id` | 公告ID |
| `title` | `Title` | `title` | 公告标题 |
| `type` | `NoticeType` | `notice_type` | 公告类型 |
| `content` | `Content` | `content` | 公告内容（富文本） |
| `tenderer` | `Bidder` | `bidder` | 招标人 |
| `bidder` | `Winner` | `winner` | 中标人 |
| `region` | `ProjectRegion` | `project_region` | 项目区域 |
| `publishDate` | `PublishTime` | `publish_time` | 发布时间 |
| `viewCount` | `ViewCount` | `view_count` | 访问量 |

**特殊处理：**
- 前端编辑时，需要将后端 `NoticeType` 映射到 `type`
- 前端编辑时，需要将后端 `Bidder` 映射到 `tenderer`（招标人）
- 前端编辑时，需要将后端 `Winner` 映射到 `bidder`（中标人）
- 前端编辑时，需要将后端 `PublishTime` 映射到 `publishDate`

---

## 5. 删除公告（DELETE /api/gov-procurement/announcements/{id}）

### 请求参数

| 参数 | 类型 | 说明 |
|------|------|------|
| `id` | int | 公告ID（路径参数） |

### 返回数据

| 字段 | 类型 | 说明 |
|------|------|------|
| `success` | boolean | 是否成功 |
| `message` | string | 提示信息 |

**注意事项：**
- 后端实现为软删除，设置 `is_deleted = 1`
- 删除前需要二次确认

---

## 6. 枚举值说明

### 公告类型 (notice_type)

| 值 | 说明 |
|----|------|
| `采购公告` | 政府采购公告 |
| `更正公告` | 政府采购更正公告 |
| `结果公告` | 政府采购结果公告 |

### 项目区域 (project_region)

河南省18个地市：

| 值 | 说明 |
|----|------|
| `郑州` | 郑州市 |
| `洛阳` | 洛阳市 |
| `开封` | 开封市 |
| `新乡` | 新乡市 |
| `南阳` | 南阳市 |
| `安阳` | 安阳市 |
| `商丘` | 商丘市 |
| `平顶山` | 平顶山市 |
| `许昌` | 许昌市 |
| `焦作` | 焦作市 |
| `周口` | 周口市 |
| `信阳` | 信阳市 |
| `驻马店` | 驻马店市 |
| `漯河` | 漯河市 |
| `濮阳` | 濮阳市 |
| `鹤壁` | 鹤壁市 |
| `三门峡` | 三门峡市 |
| `济源` | 济源示范区 |

---

## 7. 数据库表结构

### government_procurement_notices 表

| 字段名 | 类型 | 约束 | 说明 |
|-------|------|------|------|
| `id` | INT UNSIGNED | PRIMARY KEY, AUTO_INCREMENT | 公告ID |
| `title` | VARCHAR(255) | NOT NULL | 公告标题 |
| `notice_type` | VARCHAR(50) | NOT NULL | 公告类型 |
| `bidder` | VARCHAR(255) | NULL | 招标人 |
| `winner` | VARCHAR(255) | NULL | 中标人 |
| `project_region` | VARCHAR(50) | NOT NULL | 项目区域 |
| `content` | LONGTEXT | NOT NULL | 公告内容（富文本） |
| `publisher` | VARCHAR(50) | NULL | 发布人 |
| `publish_time` | DATETIME | NULL | 发布时间 |
| `view_count` | INT UNSIGNED | DEFAULT 0 | 访问量 |
| `attachment_path` | VARCHAR(500) | NULL | 附件路径 |
| `is_top` | TINYINT | DEFAULT 0 | 是否置顶 |
| `is_deleted` | TINYINT | DEFAULT 0 | 软删除标记 |
| `created_at` | DATETIME | DEFAULT CURRENT_TIMESTAMP | 创建时间 |
| `updated_at` | DATETIME | ON UPDATE CURRENT_TIMESTAMP | 更新时间 |

### 索引

- `idx_notice_type`：公告类型索引
- `idx_project_region`：项目区域索引
- `idx_publish_time`：发布时间索引
- `idx_is_top`：置顶标记索引
- `idx_is_deleted`：删除标记索引
- `ft_title_content`：标题和内容全文索引

---

## 8. 前端实现要点

### 表单验证规则

```javascript
const formRules = {
  title: [
    { required: true, message: '请输入公告标题', trigger: 'blur' },
    { max: 255, message: '标题长度不能超过255个字符', trigger: 'blur' }
  ],
  type: [
    { required: true, message: '请选择公告类型', trigger: 'change' }
  ],
  content: [
    { required: true, message: '请输入公告内容', trigger: 'blur' }
  ],
  region: [
    { required: true, message: '请选择项目区域', trigger: 'change' },
    { max: 50, message: '区域长度不能超过50个字符', trigger: 'blur' }
  ],
  publishDate: [
    { required: true, message: '请选择发布日期', trigger: 'change' }
  ],
  tenderer: [
    { max: 255, message: '招标人名称长度不能超过255个字符', trigger: 'blur' }
  ],
  bidder: [
    { max: 255, message: '中标人名称长度不能超过255个字符', trigger: 'blur' }
  ]
}
```

### 字段映射转换

#### 提交时（前端→后端）

```javascript
const submitData = {
  title: formData.title,
  type: formData.type,
  content: formData.content,
  tenderer: formData.tenderer || null,  // 前端tenderer → 后端Tenderer
  bidder: formData.bidder || null,      // 前端bidder → 后端Bidder
  region: formData.region,
  publishDate: formData.publishDate
}
```

#### 编辑回显时（后端→前端）

```javascript
Object.assign(formData, {
  id: res.data.id,
  title: res.data.title,
  type: res.data.noticeType,            // 后端NoticeType → 前端type
  content: res.data.content,
  tenderer: res.data.bidder || '',      // 后端Bidder → 前端tenderer（招标人）
  bidder: res.data.winner || '',        // 后端Winner → 前端bidder（中标人）
  region: res.data.projectRegion,       // 后端ProjectRegion → 前端region
  publishDate: res.data.publishTime     // 后端PublishTime → 前端publishDate
})
```

---

## 9. API响应格式

### 成功响应

```json
{
  "success": true,
  "message": "操作成功",
  "data": {
    // 数据内容
  }
}
```

### 失败响应

```json
{
  "success": false,
  "message": "错误信息",
  "data": null
}
```

### 分页数据格式

```json
{
  "success": true,
  "message": "获取成功",
  "data": {
    "items": [],
    "totalCount": 100,
    "pageIndex": 1,
    "pageSize": 10,
    "totalPages": 10
  }
}
```

---

## 10. 注意事项

1. **字段命名差异**：
   - 数据库使用蛇形命名（snake_case）：`notice_type`、`project_region`
   - 后端DTO使用帕斯卡命名（PascalCase）：`NoticeType`、`ProjectRegion`
   - 前端使用驼峰命名（camelCase）：`noticeType`、`projectRegion`

2. **特殊字段映射**：
   - `bidder` 字段在数据库中表示"招标人"
   - `winner` 字段在数据库中表示"中标人"
   - 前端为了语义清晰，使用 `tenderer`（招标人）和 `bidder`（中标人）

3. **日期时间格式**：
   - 前端使用：`YYYY-MM-DD HH:mm:ss`
   - 后端接收：`DateTime` 类型
   - 数据库存储：`DATETIME` 类型

4. **软删除机制**：
   - 删除操作不会真正删除数据
   - 只是将 `is_deleted` 字段设置为 1
   - 查询时自动过滤 `is_deleted = 1` 的数据

5. **访问量统计**：
   - 获取详情时自动增加访问量
   - 通过独立接口 `IncrementViewCountAsync` 实现

---

## 11. 完整工作流程示例

### 创建公告流程

1. 用户点击"新增公告"按钮
2. 打开对话框，初始化表单数据
3. 用户填写表单（包含验证）
4. 用户点击"提交"
5. 前端验证表单
6. 映射前端字段到后端DTO字段
7. 调用 POST `/api/gov-procurement/announcements`
8. 后端验证并保存到数据库
9. 返回成功响应
10. 前端显示成功消息，关闭对话框，刷新列表

### 编辑公告流程

1. 用户点击"编辑"按钮
2. 调用 GET `/api/gov-procurement/announcements/{id}` 获取详情
3. 映射后端DTO字段到前端表单字段
4. 打开对话框，回显数据
5. 用户修改表单
6. 用户点击"提交"
7. 前端验证表单
8. 映射前端字段到后端DTO字段
9. 调用 PUT `/api/gov-procurement/announcements/{id}`
10. 后端验证并更新数据库
11. 返回成功响应
12. 前端显示成功消息，关闭对话框，刷新列表

---

## 12. 开发检查清单

- [x] API接口已实现并测试
- [x] 后端DTO字段定义正确
- [x] 数据库表结构创建
- [x] 前端API调用文件创建
- [x] Vue组件完整实现
- [x] 表单验证规则设置
- [x] 字段映射正确转换
- [x] 路由配置完成
- [x] 搜索功能实现（关键词、类型、区域、时间范围）
- [x] 分页功能实现
- [x] 新增功能实现
- [x] 编辑功能实现
- [x] 删除功能实现（带二次确认）
- [x] 富文本编辑器集成
- [x] 错误处理和提示
- [x] 加载状态显示

---

**文档版本**：v1.0  
**创建日期**：2025-12-03  
**最后更新**：2025-12-03
