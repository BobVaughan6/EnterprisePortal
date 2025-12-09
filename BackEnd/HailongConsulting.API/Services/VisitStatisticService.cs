using HailongConsulting.API.Models.Entities;
using HailongConsulting.API.Repositories;
using Microsoft.AspNetCore.Http;

namespace HailongConsulting.API.Services;

/// <summary>
/// 访问统计服务实现
/// </summary>
public class VisitStatisticService : IVisitStatisticService
{
    private readonly IVisitStatisticRepository _visitStatisticRepository;
    private readonly IAnnouncementRepository _announcementRepository;
    private readonly IInfoPublicationRepository _infoPublicationRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<VisitStatisticService> _logger;

    public VisitStatisticService(
        IVisitStatisticRepository visitStatisticRepository,
        IAnnouncementRepository announcementRepository,
        IInfoPublicationRepository infoPublicationRepository,
        IUnitOfWork unitOfWork,
        ILogger<VisitStatisticService> logger)
    {
        _visitStatisticRepository = visitStatisticRepository;
        _announcementRepository = announcementRepository;
        _infoPublicationRepository = infoPublicationRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task RecordVisitAsync(string pageUrl, string? pageTitle = null, string? visitorIp = null, string? userAgent = null, string? referer = null)
    {
        try
        {
            await _visitStatisticRepository.RecordVisitAsync(pageUrl, pageTitle, visitorIp, userAgent, referer);
            await _unitOfWork.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "记录访问统计失败，PageUrl: {PageUrl}", pageUrl);
            throw;
        }
    }

    public async Task IncrementAnnouncementViewAsync(uint announcementId, HttpRequest request)
    {
        try
        {
            // 1. 增加业务表的浏览次数
            await _announcementRepository.IncrementViewCountAsync(announcementId);

            // 2. 记录详细的访问统计
            var pageUrl = $"/api/announcements/{announcementId}";
            var announcement = await _announcementRepository.GetByIdAsync((int)announcementId);
            var pageTitle = announcement?.Title;
            var visitorIp = GetClientIpAddress(request);
            var userAgent = request.Headers["User-Agent"].ToString();
            var referer = request.Headers["Referer"].ToString();

            await _visitStatisticRepository.RecordVisitAsync(pageUrl, pageTitle, visitorIp, userAgent, referer);

            // 3. 保存所有更改
            await _unitOfWork.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "增加公告浏览次数失败，AnnouncementId: {AnnouncementId}", announcementId);
            throw;
        }
    }

    public async Task IncrementPublicationViewAsync(uint publicationId, HttpRequest request)
    {
        try
        {
            // 1. 增加业务表的浏览次数
            await _infoPublicationRepository.IncrementViewCountAsync(publicationId);

            // 2. 记录详细的访问统计
            var pageUrl = $"/api/info-publications/{publicationId}";
            var publication = await _infoPublicationRepository.GetByIdAsync((int)publicationId);
            var pageTitle = publication?.Title;
            var visitorIp = GetClientIpAddress(request);
            var userAgent = request.Headers["User-Agent"].ToString();
            var referer = request.Headers["Referer"].ToString();

            await _visitStatisticRepository.RecordVisitAsync(pageUrl, pageTitle, visitorIp, userAgent, referer);

            // 3. 保存所有更改
            await _unitOfWork.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "增加信息发布浏览次数失败，PublicationId: {PublicationId}", publicationId);
            throw;
        }
    }

    public async Task<IEnumerable<VisitStatistic>> GetVisitStatisticsAsync(DateOnly startDate, DateOnly endDate)
    {
        try
        {
            return await _visitStatisticRepository.GetByDateRangeAsync(startDate, endDate);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "获取访问统计失败，StartDate: {StartDate}, EndDate: {EndDate}", startDate, endDate);
            throw;
        }
    }

    public async Task<IEnumerable<(string PageUrl, string? PageTitle, uint TotalViews)>> GetTopPagesAsync(int topCount = 10, int days = 30)
    {
        try
        {
            return await _visitStatisticRepository.GetTopPagesAsync(topCount, days);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "获取热门页面统计失败，TopCount: {TopCount}, Days: {Days}", topCount, days);
            throw;
        }
    }

    /// <summary>
    /// 获取客户端IP地址
    /// </summary>
    private string? GetClientIpAddress(HttpRequest request)
    {
        // 尝试从X-Forwarded-For头获取（适用于代理/负载均衡器）
        var forwardedFor = request.Headers["X-Forwarded-For"].FirstOrDefault();
        if (!string.IsNullOrEmpty(forwardedFor))
        {
            var ips = forwardedFor.Split(',');
            return ips[0].Trim();
        }

        // 尝试从X-Real-IP头获取
        var realIp = request.Headers["X-Real-IP"].FirstOrDefault();
        if (!string.IsNullOrEmpty(realIp))
        {
            return realIp;
        }

        // 直接从连接获取
        return request.HttpContext.Connection.RemoteIpAddress?.ToString();
    }
}