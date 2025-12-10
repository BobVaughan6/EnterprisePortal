using HailongConsulting.API.Models.DTOs;

namespace HailongConsulting.API.Services;

/// <summary>
/// 系统配置服务接口
/// </summary>
public interface IConfigService
{
    // 轮播图
    Task<IEnumerable<CarouselBannerDto>> GetAllBannersAsync();
    Task<CarouselBannerDto?> GetBannerByIdAsync(int id);
    Task<CarouselBannerDto> CreateBannerAsync(CreateCarouselBannerDto dto);
    Task<bool> UpdateBannerAsync(int id, UpdateCarouselBannerDto dto);
    Task<bool> DeleteBannerAsync(int id);

    // 企业简介
    Task<CompanyProfileDto?> GetCompanyProfileAsync();
    Task<bool> UpdateCompanyProfileAsync(UpdateCompanyProfileDto dto);

    // 重要业绩
    Task<IEnumerable<MajorAchievementDto>> GetAllAchievementsAsync();
    Task<MajorAchievementDto?> GetAchievementByIdAsync(int id);
    Task<MajorAchievementDto> CreateAchievementAsync(CreateMajorAchievementDto dto);
    Task<bool> UpdateAchievementAsync(int id, UpdateMajorAchievementDto dto);
    Task<bool> DeleteAchievementAsync(int id);

    // 企业荣誉
    Task<IEnumerable<CompanyHonorDto>> GetAllHonorsAsync();
    Task<CompanyHonorDto?> GetHonorByIdAsync(int id);
    Task<CompanyHonorDto> CreateHonorAsync(CreateCompanyHonorDto dto);
    Task<bool> UpdateHonorAsync(int id, UpdateCompanyHonorDto dto);
    Task<bool> DeleteHonorAsync(int id);

    // 友情链接
    Task<IEnumerable<FriendlyLinkDto>> GetAllLinksAsync();
    Task<FriendlyLinkDto?> GetLinkByIdAsync(int id);
    Task<FriendlyLinkDto> CreateLinkAsync(CreateFriendlyLinkDto dto);
    Task<bool> UpdateLinkAsync(int id, UpdateFriendlyLinkDto dto);
    Task<bool> DeleteLinkAsync(int id);

    // 访问统计
    Task<VisitStatisticDto> GetVisitStatisticsAsync();
    Task RecordVisitAsync(RecordVisitDto dto, string? ipAddress, string? userAgent);
}