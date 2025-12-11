namespace HailongConsulting.API.Models.DTOs;

/// <summary>
/// 轮播图DTO
/// </summary>
public class CarouselBannerDto
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public int ImageId { get; set; }
    public string? LinkUrl { get; set; }
    public int SortOrder { get; set; }
    public bool Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

/// <summary>
/// 创建轮播图DTO
/// </summary>
public class CreateCarouselBannerDto
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public int ImageId { get; set; }
    public string? LinkUrl { get; set; }
    public int SortOrder { get; set; } = 0;
    public bool Status { get; set; } = true;
}

/// <summary>
/// 更新轮播图DTO
/// </summary>
public class UpdateCarouselBannerDto
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public int? ImageId { get; set; }
    public string? LinkUrl { get; set; }
    public int? SortOrder { get; set; }
    public bool? Status { get; set; }
}

/// <summary>
/// 企业简介DTO
/// </summary>
public class CompanyProfileDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public List<string>? Highlights { get; set; }
    public List<int>? ImageIds { get; set; }
    public bool Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    /// <summary>
    /// 获取第一张图片ID（用于单图片显示）
    /// </summary>
    public int? ImageId => ImageIds?.FirstOrDefault();
    
    /// <summary>
    /// 图片完整URL列表（由Service层填充）
    /// </summary>
    public List<string>? ImageUrls { get; set; }
}

/// <summary>
/// 更新企业简介DTO
/// </summary>
public class UpdateCompanyProfileDto
{
    public string? Title { get; set; }
    public string? Content { get; set; }
    public List<string>? Highlights { get; set; }
    public List<int>? ImageIds { get; set; }
    public bool? Status { get; set; }
}

/// <summary>
/// 重要业绩DTO
/// </summary>
public class MajorAchievementDto
{
    public int Id { get; set; }
    public string ProjectName { get; set; } = string.Empty;
    public string? ProjectType { get; set; }
    public decimal? ProjectAmount { get; set; }
    public string? ClientName { get; set; }
    public DateOnly? CompletionDate { get; set; }
    public string? Description { get; set; }
    public List<int>? ImageIds { get; set; }
    public int SortOrder { get; set; }
    public bool Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    /// <summary>
    /// 获取第一张图片ID（用于单图片显示）
    /// </summary>
    public int? ImageId => ImageIds?.FirstOrDefault();
    
    /// <summary>
    /// 图片完整URL列表（由Service层填充）
    /// </summary>
    public List<string>? ImageUrls { get; set; }
}

/// <summary>
/// 创建重要业绩DTO
/// </summary>
public class CreateMajorAchievementDto
{
    public string ProjectName { get; set; } = string.Empty;
    public string? ProjectType { get; set; }
    public decimal? ProjectAmount { get; set; }
    public string? ClientName { get; set; }
    public DateOnly? CompletionDate { get; set; }
    public string? Description { get; set; }
    public List<int>? ImageIds { get; set; }
    public int SortOrder { get; set; } = 0;
    public bool Status { get; set; } = true;
}

/// <summary>
/// 更新重要业绩DTO
/// </summary>
public class UpdateMajorAchievementDto
{
    public string? ProjectName { get; set; }
    public string? ProjectType { get; set; }
    public decimal? ProjectAmount { get; set; }
    public string? ClientName { get; set; }
    public DateOnly? CompletionDate { get; set; }
    public string? Description { get; set; }
    public List<int>? ImageIds { get; set; }
    public int? SortOrder { get; set; }
    public bool? Status { get; set; }
}

/// <summary>
/// 友情链接DTO
/// </summary>
public class FriendlyLinkDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public int? LogoId { get; set; }
    public string? Description { get; set; }
    public int SortOrder { get; set; }
    public bool Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

/// <summary>
/// 创建友情链接DTO
/// </summary>
public class CreateFriendlyLinkDto
{
    public string Name { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public int? LogoId { get; set; }
    public string? Description { get; set; }
    public int SortOrder { get; set; } = 0;
    public bool Status { get; set; } = true;
}

/// <summary>
/// 更新友情链接DTO
/// </summary>
public class UpdateFriendlyLinkDto
{
    public string? Name { get; set; }
    public string? Url { get; set; }
    public int? LogoId { get; set; }
    public string? Description { get; set; }
    public int? SortOrder { get; set; }
    public bool? Status { get; set; }
}

/// <summary>
/// 访问统计DTO
/// </summary>
public class VisitStatisticDto
{
    public int TotalVisits { get; set; }
    public int TodayVisits { get; set; }
    public int YesterdayVisits { get; set; }
    public int ThisMonthVisits { get; set; }
}

/// <summary>
/// 记录访问DTO
/// </summary>
public class RecordVisitDto
{
    public string? PageUrl { get; set; }
    public string? PageTitle { get; set; }
    public string? Referer { get; set; }
}

/// <summary>
/// 企业荣誉DTO
/// </summary>
public class CompanyHonorDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int? ImageId { get; set; }
    public string? AwardOrganization { get; set; }
    public DateOnly? AwardDate { get; set; }
    public string? CertificateNo { get; set; }
    public string? HonorLevel { get; set; }
    public int SortOrder { get; set; }
    public bool Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

/// <summary>
/// 创建企业荣誉DTO
/// </summary>
public class CreateCompanyHonorDto
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int? ImageId { get; set; }
    public string? AwardOrganization { get; set; }
    public DateOnly? AwardDate { get; set; }
    public string? CertificateNo { get; set; }
    public string? HonorLevel { get; set; }
    public int SortOrder { get; set; } = 0;
    public bool Status { get; set; } = true;
}

/// <summary>
/// 更新企业荣誉DTO
/// </summary>
public class UpdateCompanyHonorDto
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int? ImageId { get; set; }
    public string? AwardOrganization { get; set; }
    public DateOnly? AwardDate { get; set; }
    public string? CertificateNo { get; set; }
    public string? HonorLevel { get; set; }
    public int? SortOrder { get; set; }
    public bool? Status { get; set; }
}
// ==================== 业务范围 ====================

/// <summary>
/// 业务范围DTO
/// </summary>
public class BusinessScopeDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Content { get; set; }
    public List<string>? Features { get; set; }
    public int? ImageId { get; set; }
    public int SortOrder { get; set; }
    public bool Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

/// <summary>
/// 创建业务范围DTO
/// </summary>
public class CreateBusinessScopeDto
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Content { get; set; }
    public List<string>? Features { get; set; }
    public int? ImageId { get; set; }
    public int SortOrder { get; set; } = 0;
    public bool Status { get; set; } = true;
}

/// <summary>
/// 更新业务范围DTO
/// </summary>
public class UpdateBusinessScopeDto
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Content { get; set; }
    public List<string>? Features { get; set; }
    public int? ImageId { get; set; }
    public int? SortOrder { get; set; }
    public bool? Status { get; set; }
}

// ==================== 企业资质 ====================

/// <summary>
/// 企业资质DTO
/// </summary>
public class CompanyQualificationDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? CertificateNumber { get; set; }
    public string? IssuingAuthority { get; set; }
    public DateOnly? IssueDate { get; set; }
    public DateOnly? ExpiryDate { get; set; }
    public int? CertificateImageId { get; set; }
    public string? Description { get; set; }
    public int SortOrder { get; set; }
    public bool Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

/// <summary>
/// 创建企业资质DTO
/// </summary>
public class CreateCompanyQualificationDto
{
    public string Name { get; set; } = string.Empty;
    public string? CertificateNumber { get; set; }
    public string? IssuingAuthority { get; set; }
    public DateOnly? IssueDate { get; set; }
    public DateOnly? ExpiryDate { get; set; }
    public int? CertificateImageId { get; set; }
    public string? Description { get; set; }
    public int SortOrder { get; set; } = 0;
    public bool Status { get; set; } = true;
}

/// <summary>
/// 更新企业资质DTO
/// </summary>
public class UpdateCompanyQualificationDto
{
    public string? Name { get; set; }
    public string? CertificateNumber { get; set; }
    public string? IssuingAuthority { get; set; }
    public DateOnly? IssueDate { get; set; }
    public DateOnly? ExpiryDate { get; set; }
    public int? CertificateImageId { get; set; }
    public string? Description { get; set; }
    public int? SortOrder { get; set; }
    public bool? Status { get; set; }
}