using HailongConsulting.API.Models.Entities;

namespace HailongConsulting.API.Repositories;

/// <summary>
/// 系统配置仓储接口
/// </summary>
public interface IConfigRepository
{
    // 轮播图
    Task<IEnumerable<CarouselBanner>> GetAllBannersAsync();
    Task<CarouselBanner?> GetBannerByIdAsync(uint id);
    Task<CarouselBanner> AddBannerAsync(CarouselBanner banner);
    Task<bool> UpdateBannerAsync(CarouselBanner banner);
    Task<bool> DeleteBannerAsync(uint id);

    // 企业简介
    Task<CompanyProfile?> GetCompanyProfileAsync();
    Task<bool> UpdateCompanyProfileAsync(CompanyProfile profile);

    // 重要业绩
    Task<IEnumerable<MajorAchievement>> GetAllAchievementsAsync();
    Task<MajorAchievement?> GetAchievementByIdAsync(uint id);
    Task<MajorAchievement> AddAchievementAsync(MajorAchievement achievement);
    Task<bool> UpdateAchievementAsync(MajorAchievement achievement);
    Task<bool> DeleteAchievementAsync(uint id);

    // 企业荣誉
    Task<IEnumerable<CompanyHonor>> GetAllHonorsAsync();
    Task<CompanyHonor?> GetHonorByIdAsync(uint id);
    Task<CompanyHonor> AddHonorAsync(CompanyHonor honor);
    Task<bool> UpdateHonorAsync(CompanyHonor honor);
    Task<bool> DeleteHonorAsync(uint id);

    // 友情链接
    Task<IEnumerable<FriendlyLink>> GetAllLinksAsync();
    Task<FriendlyLink?> GetLinkByIdAsync(uint id);
    Task<FriendlyLink> AddLinkAsync(FriendlyLink link);
    Task<bool> UpdateLinkAsync(FriendlyLink link);
    Task<bool> DeleteLinkAsync(uint id);

    // 访问统计
    Task<uint> GetTotalVisitsAsync();
    Task<uint> GetVisitsByDateAsync(DateOnly date);
    Task<uint> GetVisitsByDateRangeAsync(DateOnly startDate, DateOnly endDate);
    Task RecordVisitAsync(VisitStatistic statistic);
}