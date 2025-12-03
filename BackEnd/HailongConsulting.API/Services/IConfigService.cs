using HailongConsulting.API.Models.DTOs;

namespace HailongConsulting.API.Services;

/// <summary>
/// 系统配置服务接口
/// </summary>
public interface IConfigService
{
    // 轮播图
    Task<IEnumerable<CarouselBannerDto>> GetAllBannersAsync();
    Task<CarouselBannerDto?> GetBannerByIdAsync(uint id);
    Task<CarouselBannerDto> CreateBannerAsync(CreateCarouselBannerDto dto);
    Task<bool> UpdateBannerAsync(uint id, UpdateCarouselBannerDto dto);
    Task<bool> DeleteBannerAsync(uint id);

    // 企业简介
    Task<CompanyProfileDto?> GetCompanyProfileAsync();
    Task<bool> UpdateCompanyProfileAsync(UpdateCompanyProfileDto dto);

    // 重要业绩
    Task<IEnumerable<MajorAchievementDto>> GetAllAchievementsAsync();
    Task<MajorAchievementDto?> GetAchievementByIdAsync(uint id);
    Task<MajorAchievementDto> CreateAchievementAsync(CreateMajorAchievementDto dto);
    Task<bool> UpdateAchievementAsync(uint id, UpdateMajorAchievementDto dto);
    Task<bool> DeleteAchievementAsync(uint id);

    // 友情链接
    Task<IEnumerable<FriendlyLinkDto>> GetAllLinksAsync();
    Task<FriendlyLinkDto?> GetLinkByIdAsync(uint id);
    Task<FriendlyLinkDto> CreateLinkAsync(CreateFriendlyLinkDto dto);
    Task<bool> UpdateLinkAsync(uint id, UpdateFriendlyLinkDto dto);
    Task<bool> DeleteLinkAsync(uint id);

    // 访问统计
    Task<VisitStatisticDto> GetVisitStatisticsAsync();
    Task RecordVisitAsync(RecordVisitDto dto, string? ipAddress, string? userAgent);
}