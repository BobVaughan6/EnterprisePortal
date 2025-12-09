# 数据库结构调整总结

## 概述
根据 `hailong_consulting_schema.sql` 文档，对 HailongConsulting.API 项目进行了全面的数据库结构调整和实体重构。

## 主要变更

### 1. 新增实体类

#### 1.1 附件管理模块
- **Attachment.cs** - 统一附件管理实体
  - 统一管理所有附件和图片
  - 支持关联类型和关联ID
  - 包含文件元数据（大小、类型、扩展名等）

#### 1.2 统一公告表
- **Announcement.cs** - 合并政府采购和建设工程公告
  - 业务类型：GOV_PROCUREMENT（政府采购）、CONSTRUCTION（建设工程）
  - 公告类型：bidding（招标公告）、correction（更正公告）、result（结果公告）
  - 支持省市区三级区域
  - 附件采用JSON数组格式存储ID列表

#### 1.3 统一信息发布表
- **InfoPublication.cs** - 合并公司公告、政策法规、政策信息、通知公告
  - 信息类型：COMPANY_NEWS、POLICY_REGULATION、POLICY_INFO、NOTICE
  - 支持二级分类
  - 支持封面图片和附件

#### 1.4 系统配置模块
- **BusinessScope.cs** - 业务范围实体
- **CompanyQualification.cs** - 企业资质实体（企业荣誉、资质证书）
- **RegionDictionary.cs** - 区域字典实体（省市区三级）

#### 1.5 用户权限管理
- **AdminUser.cs** - 管理员账号实体
- **AdminRole.cs** - 角色权限实体

#### 1.6 系统日志
- **SystemLog.cs** - 系统操作日志实体

### 2. 更新的实体类

#### 2.1 User.cs
- 更新字段名以匹配数据库结构
- 添加 `real_name`、`refresh_token`、`refresh_token_expiry` 等字段
- 统一使用 `sbyte` 类型表示状态和删除标记
- 统一使用 `uint` 类型表示ID

#### 2.2 CompanyProfile.cs
- 添加 `highlights` 字段（企业特色标签，JSON格式）
- 将 `image_url` 改为 `image_ids`（JSON数组格式）
- 添加 `status` 字段

#### 2.3 MajorAchievement.cs
- 添加 `project_type` 必填字段
- 将 `image_url` 改为 `image_ids`（JSON数组格式）
- 添加 `status` 字段

#### 2.4 CarouselBanner.cs
- 添加 `description` 字段
- 将 `image_url` 改为 `image_id`（关联attachments表）

#### 2.5 FriendlyLink.cs
- 字段名调整：`link_name` -> `name`, `link_url` -> `url`
- 将 `logo_url` 改为 `logo_id`（关联attachments表）
- 添加 `description` 字段

#### 2.6 VisitStatistic.cs
- 添加更多统计字段（user_agent、referer等）
- 使用 `DateOnly` 类型表示访问日期

### 3. 新增DTO类

#### 3.1 AttachmentDto.cs
- `AttachmentDto` - 附件信息DTO
- `UploadAttachmentDto` - 附件上传DTO

#### 3.2 AnnouncementDto.cs
- `AnnouncementDto` - 公告详情DTO
- `CreateAnnouncementDto` - 创建公告DTO
- `UpdateAnnouncementDto` - 更新公告DTO
- `AnnouncementQueryDto` - 公告查询DTO

#### 3.3 InfoPublicationDto.cs
- `InfoPublicationDto` - 信息发布详情DTO
- `CreateInfoPublicationDto` - 创建信息发布DTO
- `UpdateInfoPublicationDto` - 更新信息发布DTO
- `InfoPublicationQueryDto` - 信息发布查询DTO

### 4. ApplicationDbContext 更新

#### 4.1 新增DbSet
```csharp
public DbSet<Attachment> Attachments { get; set; }
public DbSet<Announcement> Announcements { get; set; }
public DbSet<InfoPublication> InfoPublications { get; set; }
public DbSet<AdminUser> AdminUsers { get; set; }
public DbSet<AdminRole> AdminRoles { get; set; }
public DbSet<BusinessScope> BusinessScopes { get; set; }
public DbSet<CompanyQualification> CompanyQualifications { get; set; }
public DbSet<RegionDictionary> RegionDictionaries { get; set; }
public DbSet<SystemLog> SystemLogs { get; set; }
```

#### 4.2 索引配置
为所有新实体配置了适当的索引，以优化查询性能。

#### 4.3 兼容性处理
保留了旧的实体（GovProcurementAnnouncement、ConstructionProjectAnnouncement等），以确保现有代码的兼容性。

## 数据迁移策略

### 阶段1：并行运行（当前阶段）
- 新旧实体并存
- 新功能使用新实体
- 旧功能继续使用旧实体

### 阶段2：数据迁移
1. 将 `government_procurement_notices` 和 `construction_project_notices` 的数据迁移到 `announcements` 表
2. 将 `company_announcements`、`policy_regulations`、`policy_information`、`notice_announcements` 的数据迁移到 `info_publications` 表
3. 将现有的图片URL转换为附件记录

### 阶段3：清理
- 移除旧实体类
- 移除旧的DbSet
- 更新所有相关的Service和Controller

## 关键改进

### 1. 统一附件管理
- 所有附件统一存储在 `attachments` 表
- 通过 `related_type` 和 `related_id` 关联到具体记录
- 支持文件元数据管理

### 2. 统一公告管理
- 政府采购和建设工程公告合并为一张表
- 通过 `business_type` 字段区分业务类型
- 减少代码重复，简化维护

### 3. 统一信息发布
- 四种信息类型合并为一张表
- 通过 `type` 和 `category` 字段实现灵活分类
- 统一的查询和管理接口

### 4. 区域管理优化
- 新增省市区三级字典表
- 支持标准化的区域查询和筛选
- 便于数据统计和分析

### 5. 权限管理增强
- 独立的管理员账号体系
- 基于角色的权限控制
- 支持细粒度的权限配置

## 后续工作

### 必须完成
1. ✅ 创建新的实体类
2. ✅ 更新现有实体类
3. ✅ 更新 ApplicationDbContext
4. ✅ 创建新的DTO类
5. ✅ 创建/更新 Repository 接口和实现
   - ✅ IAttachmentRepository / AttachmentRepository
   - ✅ IAnnouncementRepository / AnnouncementRepository
   - ✅ IInfoPublicationRepository / InfoPublicationRepository
   - ✅ 更新 IUnitOfWork / UnitOfWork
6. ✅ 创建/更新 Service 接口和实现
   - ✅ IAttachmentService / AttachmentService
   - ✅ IAnnouncementService / AnnouncementService
   - ✅ IInfoPublicationService / InfoPublicationService
7. ⏳ 创建/更新 Controller（待完成）
   - ⏳ AttachmentController
   - ⏳ AnnouncementController
   - ⏳ InfoPublicationController
8. ✅ 更新 AutoMapper 配置
   - ✅ Attachment 映射
   - ✅ Announcement 映射（包含 JSON 序列化/反序列化）
   - ✅ InfoPublication 映射（包含 JSON 序列化/反序列化）
9. ⏳ API文档更新（待完成）

### 可选优化
1. 实现附件上传服务
2. 实现区域字典管理接口
3. 实现系统日志记录中间件
4. 实现数据迁移工具
5. 性能优化和缓存策略

## 注意事项

1. **类型变更**：所有ID字段统一使用 `uint` 类型
2. **状态字段**：统一使用 `sbyte` 类型（0/1）
3. **JSON字段**：附件ID列表、图片ID列表等使用JSON数组格式存储
4. **软删除**：所有表都支持软删除（is_deleted字段）
5. **时间戳**：自动更新 created_at 和 updated_at 字段

## 数据库字段映射

### 附件ID存储格式
```json
// attachment_ids 字段示例
[1, 2, 3, 4]

// image_ids 字段示例
[10, 11, 12]

// highlights 字段示例
["专业资质齐全", "经验丰富团队", "服务质量优秀"]
```

### 业务类型枚举
```csharp
// Announcement.BusinessType
- GOV_PROCUREMENT: 政府采购
- CONSTRUCTION: 建设工程

// Announcement.NoticeType
- bidding: 招标/采购公告
- correction: 更正公告
- result: 结果公告

// InfoPublication.Type
- COMPANY_NEWS: 公司公告
- POLICY_REGULATION: 政策法规
- POLICY_INFO: 政策信息
- NOTICE: 通知公告
```

## 版本信息
- 数据库版本：v2.0
- 更新日期：2025-12-08
- 更新人：System

## 实施总结（2025-12-09）

### 已完成的工作

#### 1. Repository 层
创建了三个新的专用 Repository：
- **AttachmentRepository**: 提供附件的 CRUD 操作和专用查询方法
  - 根据关联类型和ID查询附件
  - 根据ID列表批量查询
  - 软删除功能
  
- **AnnouncementRepository**: 提供统一公告的 CRUD 操作
  - 按业务类型查询（政府采购/建设工程）
  - 按公告类型查询（招标/更正/结果）
  - 按区域查询（省市区三级）
  - 分页查询支持关键词搜索
  
- **InfoPublicationRepository**: 提供信息发布的 CRUD 操作
  - 按类型查询（公司公告/政策法规/政策信息/通知）
  - 按分类查询
  - 分页查询支持关键词搜索

#### 2. Service 层
创建了三个新的业务服务：
- **AttachmentService**: 附件管理服务
  - 上传附件
  - 查询附件（按ID、关联类型、ID列表）
  - 删除附件（单个/批量）
  
- **AnnouncementService**: 公告管理服务
  - 创建/更新/删除公告
  - 分页查询公告
  - 增加浏览次数
  
- **InfoPublicationService**: 信息发布服务
  - 创建/更新/删除信息
  - 分页查询信息
  - 增加浏览次数

#### 3. AutoMapper 配置
更新了 MappingProfile，添加了新实体的映射规则：
- **Attachment** ↔ **AttachmentDto**
- **Announcement** ↔ **AnnouncementDto** / **CreateAnnouncementDto** / **UpdateAnnouncementDto**
- **InfoPublication** ↔ **InfoPublicationDto** / **CreateInfoPublicationDto** / **UpdateInfoPublicationDto**

特殊处理：
- JSON 数组字段的序列化/反序列化（AttachmentIds）
- sbyte 与 bool/int 类型的转换（IsTop, Status）
- 可空类型的条件映射

#### 4. UnitOfWork 更新
扩展了 IUnitOfWork 和 UnitOfWork，添加了新的 Repository 属性：
- Attachments
- Announcements
- InfoPublications
- AdminUsers, AdminRoles
- BusinessScopes, CompanyQualifications
- RegionDictionaries, SystemLogs
- CompanyProfiles, MajorAchievements
- CarouselBanners, FriendlyLinks
- VisitStatistics

### 待完成的工作

#### 1. Controller 层（高优先级）
需要创建以下 Controller：
- **AttachmentController**: 附件上传、下载、管理接口
- **AnnouncementController**: 公告的 CRUD 和查询接口
- **InfoPublicationController**: 信息发布的 CRUD 和查询接口

#### 2. API 文档（高优先级）
需要创建详细的 API 文档：
- 附件管理 API 文档
- 统一公告 API 文档
- 信息发布 API 文档

#### 3. 依赖注入配置（必须）
在 Program.cs 中注册新的服务：
```csharp
// Repository
builder.Services.AddScoped<IAttachmentRepository, AttachmentRepository>();
builder.Services.AddScoped<IAnnouncementRepository, AnnouncementRepository>();
builder.Services.AddScoped<IInfoPublicationRepository, InfoPublicationRepository>();

// Service
builder.Services.AddScoped<IAttachmentService, AttachmentService>();
builder.Services.AddScoped<IAnnouncementService, AnnouncementService>();
builder.Services.AddScoped<IInfoPublicationService, InfoPublicationService>();
```

### 技术要点

#### JSON 字段处理
附件ID列表存储为 JSON 数组格式：
```json
[1, 2, 3, 4]
```

在 AutoMapper 中使用辅助方法进行序列化/反序列化：
```csharp
private static string? SerializeUintList(List<uint>? list)
private static List<uint>? DeserializeUintList(string? json)
```

#### 软删除实现
所有实体都支持软删除，通过 `is_deleted` 字段标记：
- 0: 未删除
- 1: 已删除

查询时需要过滤已删除的记录：
```csharp
.Where(x => x.IsDeleted == 0)
```

#### 分页查询
统一使用 PagedResult<T> 返回分页数据：
```csharp
public class PagedResult<T>
{
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public int TotalPages { get; }
    public List<T> Items { get; set; }
}
```

### 已清理的旧代码

#### 实体层（已删除 - 6个文件）
- ❌ ConstructionProjectAnnouncement.cs
- ❌ GovProcurementAnnouncement.cs
- ❌ CompanyAnnouncement.cs
- ❌ PolicyRegulation.cs
- ❌ PolicyInformation.cs
- ❌ NoticeAnnouncement.cs

**原因**：
- 建设工程和政府采购公告 → 合并到 `Announcement` 实体
- 公司公告、政策法规、政策信息、通知公告 → 合并到 `InfoPublication` 实体

#### DTO 层（已删除 - 3个文件）
- ❌ ConstructionProjectDto.cs
- ❌ GovProcurementDto.cs
- ❌ InfoPublishDto.cs

**原因**：
- 建设工程和政府采购 DTO → 使用 `AnnouncementDto` 替代
- 旧信息发布 DTO → 使用 `InfoPublicationDto` 替代

#### Repository 层（已删除 - 4个文件）
- ❌ IConstructionProjectRepository.cs
- ❌ ConstructionProjectRepository.cs
- ❌ IGovProcurementRepository.cs
- ❌ GovProcurementRepository.cs

**原因**：建设工程和政府采购公告已合并到统一的 `Announcement` 表，使用 `AnnouncementRepository` 替代。

#### Service 层（已删除 - 6个文件）
- ❌ IConstructionProjectService.cs
- ❌ ConstructionProjectService.cs
- ❌ IGovProcurementService.cs
- ❌ GovProcurementService.cs
- ❌ IInfoPublishService.cs
- ❌ InfoPublishService.cs

**原因**：
- 建设工程和政府采购服务已合并到 `AnnouncementService`
- 旧的信息发布服务已被 `InfoPublicationService` 替代

**总计删除文件数：19个**

### 下一步建议

1. **立即完成依赖注入配置**，确保新服务可以被使用
2. **创建 Controller 层**，提供 RESTful API 接口
3. **编写 API 文档**，方便前端对接
4. **进行集成测试**，验证整个流程
5. **数据迁移**，将旧表数据迁移到新表（可选）

### 注意事项

1. 所有 ID 字段使用 `uint` 类型
2. 状态字段使用 `sbyte` 类型（0/1）
3. 时间字段使用 UTC 时间
4. JSON 字段需要正确序列化/反序列化
5. 查询时注意过滤软删除的记录
6. 旧的 Controller 需要更新以使用新的 Service