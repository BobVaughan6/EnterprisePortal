# Announcements.vue 接口方案（最终版）

根据您的反馈，统一使用一个接口、一个对象、一张数据库表来管理所有公告。

## 📋 数据库设计

### 统一公告表：`announcements`

```sql
CREATE TABLE announcements (
    id INT PRIMARY KEY AUTO_INCREMENT,
    title VARCHAR(255) NOT NULL COMMENT '公告标题',
    business_type VARCHAR(50) NOT NULL COMMENT '业务类型：GOV_PROCUREMENT-政府采购, CONSTRUCTION-建设工程',
    notice_type VARCHAR(50) NOT NULL COMMENT '公告类型：bidding-招标/采购公告, correction-更正公告, result-结果公告',
    procurement_type VARCHAR(50) NULL COMMENT '采购类型（仅政府采购）：goods-货物, service-服务, project-工程',
    bidder VARCHAR(255) NULL COMMENT '招标人',
    winner VARCHAR(255) NULL COMMENT '中标人',
    project_region VARCHAR(50) NOT NULL COMMENT '项目区域',
    content LONGTEXT NOT NULL COMMENT '公告内容（富文本）',
    publisher VARCHAR(50) NULL COMMENT '发布人',
    publish_time DATETIME NULL COMMENT '发布时间',
    view_count INT DEFAULT 0 COMMENT '浏览次数',
    attachment_path VARCHAR(500) NULL COMMENT '附件路径',
    is_top BOOLEAN DEFAULT FALSE COMMENT '是否置顶',
    is_deleted BOOLEAN DEFAULT FALSE COMMENT '是否删除',
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    INDEX idx_business_type (business_type),
    INDEX idx_notice_type (notice_type),
    INDEX idx_region (project_region),
    INDEX idx_publish_time (publish_time)
) COMMENT='统一公告表';
```

## 🎯 接口设计

### 1. 获取公告列表

**接口路径：** `GET /api/announcements`

**请求参数：**

```typescript
{
  businessType?: string,      // 业务类型：GOV_PROCUREMENT | CONSTRUCTION | '' (空表示全部)
  type?: string,              // 公告类型：bidding | correction | result | '' (空表示全部)
  procurementType?: string,   // 采购类型：goods | service | project | '' (仅政府采购，可为空)
  keyword?: string,           // 关键词搜索（标题、招标人）
  region?: string,            // 项目区域（省/市/区县名称）
  startDate?: string,         // 开始日期 YYYY-MM-DD
  endDate?: string,           // 结束日期 YYYY-MM-DD
  page: number,               // 页码，从1开始
  pageSize: number            // 每页数量，默认10
}
```

**响应数据：**

```typescript
{
  success: true,
  message: "获取成功",
  data: {
    items: [
      {
        id: 1,
        title: "某市教育局办公设备采购项目招标公告",
        businessType: "GOV_PROCUREMENT",
        type: "招标公告",
        procurementType: "goods",
        bidder: "某市教育局",
        winner: null,
        region: "郑州市",
        publishDate: "2025-11-15",
        views: 156,
        publisher: "张三",
        attachmentPath: null,
        isTop: false
      }
    ],
    total: 25,
    page: 1,
    pageSize: 10,
    totalPages: 3
  }
}
```

### 2. 获取公告详情

**接口路径：** `GET /api/announcements/{id}`

**功能说明：**
- 获取公告完整信息
- 自动增加浏览次数
- 不需要传递 businessType 参数

**响应数据：**

```typescript
{
  success: true,
  message: "获取成功",
  data: {
    id: 1,
    title: "某市教育局办公设备采购项目招标公告",
    businessType: "GOV_PROCUREMENT",
    type: "招标公告",
    procurementType: "goods",
    bidder: "某市教育局",
    winner: null,
    region: "郑州市",
    content: "<p>公告详细内容...</p>",
    publisher: "张三",
    publishDate: "2025-11-15",
    views: 157,
    attachmentPath: "/uploads/announcement_1.pdf",
    isTop: false,
    createdAt: "2025-11-15T10:00:00",
    updatedAt: "2025-11-15T10:00:00"
  }
}
```

## 🔧 后端实现

### 1. 实体类：`Announcement.cs`

```csharp
[Table("announcements")]
public class Announcement
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [MaxLength(255)]
    [Column("title")]
    public string Title { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    [Column("business_type")]
    public string BusinessType { get; set; } = string.Empty; // GOV_PROCUREMENT, CONSTRUCTION

    [Required]
    [MaxLength(50)]
    [Column("notice_type")]
    public string NoticeType { get; set; } = string.Empty; // bidding, correction, result

    [MaxLength(50)]
    [Column("procurement_type")]
    public string? ProcurementType { get; set; } // goods, service, project (仅政府采购)

    [MaxLength(255)]
    [Column("bidder")]
    public string? Bidder { get; set; }

    [MaxLength(255)]
    [Column("winner")]
    public string? Winner { get; set; }

    [Required]
    [MaxLength(50)]
    [Column("project_region")]
    public string ProjectRegion { get; set; } = string.Empty;

    [Required]
    [Column("content", TypeName = "longtext")]
    public string Content { get; set; } = string.Empty;

    [MaxLength(50)]
    [Column("publisher")]
    public string? Publisher { get; set; }

    [Column("publish_time")]
    public DateTime? PublishTime { get; set; }

    [Column("view_count")]
    public int ViewCount { get; set; } = 0;

    [MaxLength(500)]
    [Column("attachment_path")]
    public string? AttachmentPath { get; set; }

    [Column("is_top")]
    public bool IsTop { get; set; } = false;

    [Column("is_deleted")]
    public bool IsDeleted { get; set; } = false;

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}
```

### 2. 控制器：`AnnouncementsController.cs`

```csharp
[ApiController]
[Route("api/announcements")]
public class AnnouncementsController : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<ApiResponse<PagedResult<AnnouncementDto>>>> GetAnnouncements(
        [FromQuery] AnnouncementQueryViewModel query)
    {
        // 查询逻辑：
        // 1. 根据 businessType 筛选（为空则查询全部）
        // 2. 根据 type 筛选公告类型
        // 3. 根据 procurementType 筛选（仅当 businessType=GOV_PROCUREMENT 时）
        // 4. 关键词搜索 title 和 bidder
        // 5. 区域模糊匹配 project_region
        // 6. 时间范围筛选 publish_time
        // 7. 按 is_top DESC, publish_time DESC 排序
        // 8. 分页返回
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<ActionResult<ApiResponse<AnnouncementDto>>> GetAnnouncement(int id)
    {
        // 1. 查询公告详情
        // 2. 增加 view_count
        // 3. 返回完整信息
    }
}
```

### 3. DTO：`AnnouncementDto.cs`

```csharp
public class AnnouncementDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string BusinessType { get; set; }
    public string Type { get; set; } // 转换后的显示名称
    public string? ProcurementType { get; set; }
    public string? Bidder { get; set; }
    public string? Winner { get; set; }
    public string Region { get; set; }
    public string? Content { get; set; } // 列表可不返回
    public string? Publisher { get; set; }
    public string PublishDate { get; set; }
    public int Views { get; set; }
    public string? AttachmentPath { get; set; }
    public bool IsTop { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
```

## 📊 类型映射规则

### notice_type → 前端显示

```csharp
public string GetTypeDisplayName(string noticeType, string businessType)
{
    return (noticeType, businessType) switch
    {
        ("bidding", "GOV_PROCUREMENT") => "招标公告",
        ("bidding", "CONSTRUCTION") => "采购公告",
        ("correction", _) => "更正公告",
        ("result", _) => "结果公告",
        _ => noticeType
    };
}
```

## 🔄 数据迁移

需要将现有的两张表数据迁移到新表：

```sql
-- 迁移政府采购数据
INSERT INTO announcements (
    title, business_type, notice_type, procurement_type, bidder, winner,
    project_region, content, publisher, publish_time, view_count,
    attachment_path, is_top, is_deleted, created_at, updated_at
)
SELECT 
    title, 'GOV_PROCUREMENT', notice_type, NULL, bidder, winner,
    project_region, content, publisher, publish_time, view_count,
    attachment_path, is_top, is_deleted, created_at, updated_at
FROM government_procurement_notices;

-- 迁移建设工程数据
INSERT INTO announcements (
    title, business_type, notice_type, procurement_type, bidder, winner,
    project_region, content, publisher, publish_time, view_count,
    attachment_path, is_top, is_deleted, created_at, updated_at
)
SELECT 
    title, 'CONSTRUCTION', notice_type, NULL, bidder, winner,
    project_region, content, publisher, publish_time, view_count,
    attachment_path, is_top, is_deleted, created_at, updated_at
FROM construction_project_notices;
```

## 📝 前端调用示例

```javascript
// 获取所有公告
getAnnouncementList({
  businessType: '',
  type: '',
  keyword: '教育',
  region: '郑州市',
  page: 1,
  pageSize: 10
})

// 获取政府采购公告
getAnnouncementList({
  businessType: 'GOV_PROCUREMENT',
  type: 'bidding',
  procurementType: 'goods',
  page: 1,
  pageSize: 10
})

// 获取详情
getAnnouncementDetail(1)
```

## ✅ 实现优势

1. **统一管理**：一个表、一个接口、一套逻辑
2. **易于扩展**：未来增加新业务类型只需添加 business_type 值
3. **查询高效**：通过索引优化查询性能
4. **维护简单**：减少代码重复，降低维护成本
5. **数据一致**：统一的数据结构和验证规则

## 🎯 实施步骤

1. 创建新表 `announcements`
2. 迁移现有数据
3. 创建实体类、DTO、Service、Repository
4. 实现 `AnnouncementsController`
5. 前端调用新接口
6. 测试验证
7. 下线旧接口和旧表