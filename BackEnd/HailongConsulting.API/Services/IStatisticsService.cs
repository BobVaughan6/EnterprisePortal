using HailongConsulting.API.Models.DTOs;

namespace HailongConsulting.API.Services;

/// <summary>
/// 统计服务扩展接口 - 访问统计相关
/// </summary>
public interface IVisitStatisticsExtension
{
    /// <summary>
    /// 获取总访问量
    /// </summary>
    Task<int> GetTotalVisitsAsync();
    
    /// <summary>
    /// 获取指定日期的访问量
    /// </summary>
    Task<int> GetVisitsByDateAsync(DateOnly date);
    
    /// <summary>
    /// 获取日期范围内的访问量
    /// </summary>
    Task<int> GetVisitsByDateRangeAsync(DateOnly startDate, DateOnly endDate);
    
    /// <summary>
    /// 获取独立访客数
    /// </summary>
    Task<int> GetUniqueVisitorsAsync(DateOnly startDate, DateOnly endDate);
    
    /// <summary>
    /// 获取访问趋势数据
    /// </summary>
    Task<List<VisitTrendDataDto>> GetVisitTrendAsync(DateOnly startDate, DateOnly endDate, string groupBy);
    
    /// <summary>
    /// 获取热门页面Top10
    /// </summary>
    Task<List<PopularPageDto>> GetPopularPagesAsync(int limit, int days);
    
    /// <summary>
    /// 获取访问来源统计
    /// </summary>
    Task<List<VisitSourceDto>> GetVisitSourcesAsync();
    
    /// <summary>
    /// 获取访问页面总数（不同页面URL的数量）
    /// </summary>
    Task<int> GetTotalPagesAsync();
}

/// <summary>
/// 统计服务扩展接口 - 公告统计相关
/// </summary>
public interface IAnnouncementStatisticsExtension
{
    Task<AnnouncementStatisticsOverviewDto> GetStatisticsOverviewAsync();
    Task<List<AnnouncementPublishTrendDto>> GetPublishTrendAsync(DateOnly startDate, DateOnly endDate, string? businessType, string groupBy);
    Task<List<AnnouncementTypeDistributionDto>> GetTypeDistributionAsync();
    Task<List<AnnouncementRegionDistributionDto>> GetRegionDistributionAsync(string? businessType, int limit);
    Task<List<PopularAnnouncementDto>> GetPopularAnnouncementsAsync(string? businessType, int limit);
    Task<List<AnnouncementStatusDistributionDto>> GetStatusDistributionAsync();
    Task<int> GetTotalCountAsync();
    Task<int> GetTodayAddedCountAsync();
}

/// <summary>
/// 统计服务扩展接口 - 信息发布统计相关
/// </summary>
public interface IInfoPublicationStatisticsExtension
{
    Task<InfoPublicationStatisticsOverviewDto> GetStatisticsOverviewAsync();
    Task<List<InfoPublicationPublishTrendDto>> GetPublishTrendAsync(DateOnly startDate, DateOnly endDate, string groupBy);
    Task<List<InfoPublicationTypeDistributionDto>> GetTypeDistributionAsync();
    Task<List<PopularInfoPublicationDto>> GetPopularInfoPublicationsAsync(int limit);
    Task<List<AuthorPublishStatisticDto>> GetAuthorStatisticsAsync();
    Task<int> GetTotalCountAsync();
    Task<int> GetTodayAddedCountAsync();
}

/// <summary>
/// 统计服务扩展接口 - 附件统计相关
/// </summary>
public interface IAttachmentStatisticsExtension
{
    Task<int> GetTotalCountAsync();
}