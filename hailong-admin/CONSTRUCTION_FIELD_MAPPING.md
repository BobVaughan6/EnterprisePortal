# 建设工程公告管理 - 字段映射说明文档

## 概述

本文档详细说明了建设工程公告管理页面中，前端表单字段、后端DTO字段、数据库字段之间的对应关系。

---

## 1. 列表查询（GET /api/construction/announcements）

### 查询参数映射

| 前端字段 | 后端DTO字段 (ConstructionProjectQueryViewModel) | 说明 |
|---------|-----------------------------------------------|------|
| `keyword` | `Keyword` | 关键词搜索（标题、招标人、中标人） |
| `type` | `Type` | 公告类型 |
| `region` | `Region` | 项目区域 |
| `startDate` | `StartDate` | 开始日期 |
| `endDate` | `EndDate` | 结束日期 |
| `pageIndex` | `PageIndex` | 页码（默认1） |
| `pageSize` | `PageSize` | 每页数量（默认10） |

### 返回数据映射

| 前端显示字段 | 后端DTO字段 (ConstructionProjectDto) | 数据库字段 | 说明 |
|------------|-----------------------------------|-----------|------|
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

## 2. 创建公告（POST /api/construction/announcements）

### 请求数据映射

| 前端表单字段 | 后端DTO字段 (CreateConstructionProjectDto) | 数据库字段 | 验证规则 | 说明 |
|------------|------------------------------------------|-----------|---------|------|
| `title` | `Title` | `title` | Required, MaxLength(255) | 公告标题 |
| `type` | `Type` | `notice_type` | Required, RegEx(招标公告\|中标公告\|变更公告) | 公告类型 |
| `content` | `Content` | `content` | Required | 公告内容（富文本） |
| `tenderer` | `Tenderer` | `bidder` | MaxLength(255) | 招标人（可选） |
| `winner` | `Winner` | `winner` | MaxLength(255) | 中标人（可选） |
| `region` | `Region` | `project_region` | Required, MaxLength(50) | 项目区域 |
| `publishDate` | `PublishDate` | `publish_time` | Required | 发布日期 |

**注意事项：**
- 前端 `tenderer` → 后端 `Tenderer` → 数据库 `bidder`（招标人）
- 前端 `winner` → 后端 `Winner` → 数据库 `winner`（中标人）

---

## 3. 更新公告（PUT /api/construction/announcements/{id}）

### 请求数据映射

| 前端表单字段 | 后端DTO字段 (UpdateConstructionProjectDto) | 数据库字段 | 验证规则 | 说明 |
|------------|------------------------------------------|-----------|---------|------|
| `title` | `Title` | `title` | MaxLength(255) | 公告标题（可选） |
| `type` | `Type` | `notice_type` | RegEx(招标公告\|中标公告\|变更公告) | 公告类型（可选） |
| `content` | `Content` | `content` | - | 公告内容（可选） |
| `tenderer` | `Tenderer` | `bidder` | MaxLength(255) | 招标人（可选） |
| `winner` | `Winner` | `winner` | MaxLength(255) | 中标人（可选） |
| `region` | `Region` | `project_region` | MaxLength(50) | 项目区域（可选） |
| `publishDate` | `PublishDate` | `publish_time` | - | 发布日期（可选） |

---

## 4. 详情查询（GET /api/construction/announcements/{id}）

### 返回数据映射

| 前端字段 | 后端DTO字段 (ConstructionProjectDto) | 数据库字段 | 说明 |
|---------|-----------------------------------|-----------|------|
| `id` | `Id` | `id` | 公告ID |
| `title` | `Title` | `title` | 公告标题 |
| `type` | `NoticeType` | `notice_type` | 公告类型 |
| `content` | `Content` | `content` | 公告内容（富文本） |
| `tenderer` | `Bidder` | `bidder` | 招标人 |
| `winner` | `Winner` | `winner` | 中标人 |
| `region` | `ProjectRegion` | `project_region` | 项目区域 |
| `publishDate` | `PublishTime` | `publish_time` | 发布时间 |
| `viewCount` | `ViewCount` | `view_count` | 访问量 |

**特殊处理：**
- 前端编辑时，需要将后端 `NoticeType` 映射到 `type`
- 前端编辑时，需要将后端 `Bidder` 映射到 `tenderer`（招标人）
- 前端编辑时，需要将后端 `Winner` 映射到 `winner`（中标人）
- 前端编辑时，需要将后端 `PublishTime` 映射到 `publishDate`

---

## 5. 删除公告（DELETE /api/construction/announcements/{id}）

### 请求参数

| 参数 | 类型 | 说明 |
|------|------|------|
| `id` | int | 公告ID（路径参数） |

### 返回数据

| 字段 | 类型 | 说明 |
|------|------|------|
| `success` | boolean | 是否成功 |
| `message` | string | 提示信息 |

---

## 6. 枚举值说明

### 公告类型 (notice_type)

| 值 | 说明 |
|----|------|
| `招标公告` | 建设工程招标公告 |
| `中标公告` | 建设工程中标公告 |
| `变更公告` | 建设工程变更公告 |

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

### construction_project_notices 表

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
  winner: [
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
  winner: formData.winner || null,      // 前端winner → 后端Winner
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
  winner: res.data.winner || '',        // 后端Winner → 前端winner（中标人）
  region: res.data.projectRegion,       // 后端ProjectRegion → 前端region
  publishDate: res.data.publishTime     // 后端PublishTime → 前端publishDate
})
```

---

## 9. 与政府采购公告的差异

### 相同点
- 数据结构完全一致
- 字段验证规则相同
- 表单布局相同
- 功能逻辑相同

### 差异点

| 项目 | 政府采购公告 | 建设工程公告 |
|------|------------|------------|
| **API路径** | `/api/gov-procurement/announcements` | `/api/construction/announcements` |
| **公告类型** | 采购公告、更正公告、结果公告 | 招标公告、中标公告、变更公告 |
| **Controller** | `GovProcurementController` | `ConstructionProjectController` |
| **DTO** | `GovProcurementDto` | `ConstructionProjectDto` |
| **数据库表** | `government_procurement_notices` | `construction_project_notices` |

---

## 10. 使用说明

### 访问路径
```
http://localhost:5173/construction
```

### 功能清单
- ✅ 关键词搜索（标题、招标人、中标人）
- ✅ 公告类型筛选（招标公告、中标公告、变更公告）
- ✅ 项目区域筛选（河南省18个地市）
- ✅ 时间范围筛选
- ✅ 分页查询
- ✅ 新增公告
- ✅ 编辑公告
- ✅ 删除公告（软删除，二次确认）
- ✅ 富文本编辑器
- ✅ 表单验证

---

## 11. 注意事项

1. **API路径**：确保使用 `/api/construction/announcements`
2. **公告类型**：必须是招标公告、中标公告或变更公告
3. **字段映射**：注意 `tenderer/winner` 与数据库字段的对应
4. **表单验证**：严格按照后端DTO的验证规则
5. **软删除**：删除操作不会真正删除数据，只是标记为已删除

---

**文档版本**：v1.0  
**创建日期**：2025-12-03  
**最后更新**：2025-12-03
