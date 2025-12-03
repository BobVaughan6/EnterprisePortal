using HailongConsulting.API.Models.DTOs;

namespace HailongConsulting.API.Services;

/// <summary>
/// 首页服务接口
/// </summary>
public interface IHomeService
{
    /// <summary>
    /// 获取首页统计概览
    /// </summary>
    Task<HomeStatisticsDto> GetStatisticsOverviewAsync();

    /// <summary>
    /// 获取最新公告列表
    /// </summary>
    Task<List<RecentAnnouncementDto>> GetRecentAnnouncementsAsync();

    /// <summary>
    /// 获取重要业绩列表
    /// </summary>
    Task<List<AchievementDto>> GetAchievementsAsync();
}