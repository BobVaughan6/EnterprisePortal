# 访问统计功能实现说明

## 概述

本系统实现了完整的访问统计功能，采用**双轨制设计**：
1. **业务表存储**：在 `announcements` 和 `info_publications` 表中直接存储 `view_count` 字段
2. **统计表存储**：在 `visit_statistics` 表中记录详细的访问日志

## 数据库设计

### 1. 业务表的访问计数字段

#### announcements 表
```sql
view_count INT UNSIGNED DEFAULT 0 COMMENT '浏览次数'
```

#### info_publications 表
```sql
view_count INT UNSIGNED DEFAULT 0 COMMENT '浏览次数'
```

### 2. 独立的访问统计表

#### visit_statistics 表
```sql
CREATE TABLE visit_statistics (
    id INT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    visit_date DATE NOT NULL COMMENT '访问日期',
    page_url VARCHAR(500) COMMENT '页面URL',
    page_title VARCHAR(255) COMMENT '页面标题',
    visitor_ip VARCHAR(50) COMMENT '访问者IP',
    user_agent VARCHAR(500) COMMENT '用户代理',
    referer VARCHAR(500) COMMENT '来源页面',
    visit_count INT UNSIGNED DEFAULT 1 COMMENT '访问次数',
    is_deleted TINYINT DEFAULT 0 COMMENT '软删除：0-未删除，1-已删除',
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    INDEX idx_visit_date (visit_date),
    INDEX idx_page_url (page_url)
) COMMENT='访问统计表';
```

## 核心功能

### 1. 自动访问统计

当用户访问公告或信息发布详情时，系统会自动：
- ✅ 增加业务表的 `view_count` 字段
- ✅ 在 `visit_statistics` 表中记录详细访问信息
- ✅ 记录访问者IP、浏览器信息、来源页面等

### 2. 访问统计服务

#### IVisitStatisticService 接口

```csharp
public interface IVisitStatisticService
{
    // 记录访问统计
    Task RecordVisitAsync(string pageUrl, string? pageTitle, string? visitorIp, string? userAgent, string? referer);
    
    // 增加公告浏览次数（同时更新业务表和统计表）
    Task IncrementAnnouncementViewAsync(int announcementId, HttpRequest request);
    
    // 增加信息发布浏览次数（同时更新业务表和统计表）
    Task IncrementPublicationViewAsync(int publicationId, HttpRequest request);
    
    // 获取指定日期范围的访问统计
    Task<IEnumerable<VisitStatistic>> GetVisitStatisticsAsync(DateOnly startDate, DateOnly endDate);
    
    // 获取热门页面统计
    Task<IEnumerable<(string PageUrl, string? PageTitle, int TotalViews)>> GetTopPagesAsync(int topCount = 10, int days = 30);
}
```

### 3. 控制器集成

#### AnnouncementController
```csharp
[HttpGet("{id}")]
public async Task<ActionResult<ApiResponse<AnnouncementDto>>> GetAnnouncement(int id)
{
    var announcement = await _announcementService.GetByIdAsync(id);
    if (announcement == null)
    {
        return NotFound(ApiResponse<AnnouncementDto>.FailResult("公告不存在"));
    }

    // 增加浏览次数（同时更新业务表和统计表）
    await _visitStatisticService.IncrementAnnouncementViewAsync(id, Request);

    return Ok(ApiResponse<AnnouncementDto>.SuccessResult(announcement, "获取公告成功"));
}
```

#### InfoPublicationController
```csharp
[HttpGet("{id}")]
public async Task<ActionResult<ApiResponse<InfoPublicationDto>>> GetInfoPublication(int id)
{
    var publication = await _infoPublicationService.GetByIdAsync(id);
    if (publication == null)
    {
        return NotFound(ApiResponse<InfoPublicationDto>.FailResult("信息发布不存在"));
    }

    // 增加浏览次数（同时更新业务表和统计表）
    await _visitStatisticService.IncrementPublicationViewAsync(id, Request);

    return Ok(ApiResponse<InfoPublicationDto>.SuccessResult(publication, "获取信息发布成功"));
}
```

## 工作流程

### 访问统计流程

```
用户访问详情页
    ↓
Controller 调用 VisitStatisticService.IncrementXxxViewAsync()
    ↓
1. 更新业务表的 view_count 字段 (+1)
    ↓
2. 记录详细访问信息到 visit_statistics 表
   - 提取访问者IP（支持代理/负载均衡器）
   - 记录User-Agent（浏览器信息）
   - 记录Referer（来源页面）
   - 记录页面URL和标题
    ↓
3. 保存所有更改到数据库
    ↓
完成
```

### IP地址获取优先级

系统按以下优先级获取客户端真实IP：
1. `X-Forwarded-For` 头（适用于代理/负载均衡器）
2. `X-Real-IP` 头
3. 直接连接的 RemoteIpAddress

## 数据统计功能

### 1. 日期范围统计

```csharp
// 获取最近30天的访问统计
var startDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-30));
var endDate = DateOnly.FromDateTime(DateTime.Now);
var statistics = await _visitStatisticService.GetVisitStatisticsAsync(startDate, endDate);
```

### 2. 热门页面统计

```csharp
// 获取最近30天访问量前10的页面
var topPages = await _visitStatisticService.GetTopPagesAsync(topCount: 10, days: 30);
```

## 优势特点

### 1. 双轨制设计优势

| 特性 | 业务表 view_count | 统计表 visit_statistics |
|------|------------------|------------------------|
| **查询速度** | ⚡ 极快（直接字段） | 🔍 需要聚合查询 |
| **数据详细度** | 📊 仅总数 | 📈 详细日志 |
| **适用场景** | 列表展示、排序 | 数据分析、报表 |
| **存储空间** | 💾 极小 | 💿 较大 |

### 2. 功能特点

- ✅ **自动化**：无需手动调用，访问详情页自动统计
- ✅ **准确性**：同时更新两个表，保证数据一致性
- ✅ **详细性**：记录IP、浏览器、来源等完整信息
- ✅ **可扩展**：易于添加新的统计维度
- ✅ **高性能**：业务表直接存储总数，查询快速
- ✅ **可分析**：统计表支持复杂的数据分析

### 3. 数据一致性

系统使用 `UnitOfWork` 模式确保：
- 业务表和统计表的更新在同一事务中
- 要么全部成功，要么全部回滚
- 保证数据一致性

## 使用示例

### 前端调用

```javascript
// 获取公告详情（自动统计访问）
const response = await fetch(`/api/announcements/${id}`);
const data = await response.json();

// 返回的数据中包含 viewCount
console.log(`浏览次数：${data.data.viewCount}`);
```

### 后台统计查询

```csharp
// 获取最近7天的访问统计
var statistics = await _visitStatisticService.GetVisitStatisticsAsync(
    DateOnly.FromDateTime(DateTime.Now.AddDays(-7)),
    DateOnly.FromDateTime(DateTime.Now)
);

// 获取热门页面
var topPages = await _visitStatisticService.GetTopPagesAsync(10, 30);
foreach (var page in topPages)
{
    Console.WriteLine($"{page.PageTitle}: {page.TotalViews} 次访问");
}
```

## 注意事项

1. **性能考虑**：
   - 访问统计是异步操作，不会阻塞主流程
   - 统计表按日期聚合，避免数据量过大

2. **数据清理**：
   - 建议定期清理历史统计数据（如保留最近1年）
   - 可以通过软删除标记过期数据

3. **扩展建议**：
   - 可以添加更多统计维度（如地理位置、设备类型等）
   - 可以实现访问趋势图表
   - 可以添加实时访问统计

## 相关文件

### 服务层
- `Services/IVisitStatisticService.cs` - 访问统计服务接口
- `Services/VisitStatisticService.cs` - 访问统计服务实现

### 仓储层
- `Repositories/IVisitStatisticRepository.cs` - 访问统计仓储接口
- `Repositories/VisitStatisticRepository.cs` - 访问统计仓储实现
- `Repositories/IAnnouncementRepository.cs` - 公告仓储接口（新增 IncrementViewCountAsync）
- `Repositories/AnnouncementRepository.cs` - 公告仓储实现
- `Repositories/IInfoPublicationRepository.cs` - 信息发布仓储接口（新增 IncrementViewCountAsync）
- `Repositories/InfoPublicationRepository.cs` - 信息发布仓储实现

### 控制器
- `Controllers/AnnouncementController.cs` - 公告控制器（集成访问统计）
- `Controllers/InfoPublicationController.cs` - 信息发布控制器（集成访问统计）

### 实体模型
- `Models/Entities/VisitStatistic.cs` - 访问统计实体
- `Models/Entities/Announcement.cs` - 公告实体（包含 ViewCount 字段）
- `Models/Entities/InfoPublication.cs` - 信息发布实体（包含 ViewCount 字段）

### 配置
- `Program.cs` - 服务注册配置

## 总结

本访问统计功能实现了完整的访问追踪和数据分析能力，采用双轨制设计兼顾了性能和功能需求。系统会在用户访问详情页时自动记录访问信息，无需额外操作，为后续的数据分析和运营决策提供了坚实的数据基础。