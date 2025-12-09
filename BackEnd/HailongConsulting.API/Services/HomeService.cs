using HailongConsulting.API.Data;
using HailongConsulting.API.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace HailongConsulting.API.Services;

/// <summary>
/// 首页服务实现
/// </summary>
public class HomeService : IHomeService
{
    private readonly ApplicationDbContext _context;

    public HomeService(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// 获取首页统计概览
    /// </summary>
    public async Task<HomeStatisticsDto> GetStatisticsOverviewAsync()
    {
        // 统计政府采购公告数量
        var govCount = await _context.Announcements
            .Where(x => x.IsDeleted == 0 && x.BusinessType == "GOV_PROCUREMENT")
            .CountAsync();

        // 统计建设工程公告数量
        var constructionCount = await _context.Announcements
            .Where(x => x.IsDeleted == 0 && x.BusinessType == "CONSTRUCTION")
            .CountAsync();

        // 总项目数
        var totalProjects = govCount + constructionCount;

        // 计算交易类型占比
        var projectTypes = new List<ProjectTypeStatDto>();
        
        if (totalProjects > 0)
        {
            projectTypes.Add(new ProjectTypeStatDto
            {
                Type = "政府采购",
                Count = govCount,
                Percentage = Math.Round((decimal)govCount * 100 / totalProjects, 2)
            });

            projectTypes.Add(new ProjectTypeStatDto
            {
                Type = "建设工程",
                Count = constructionCount,
                Percentage = Math.Round((decimal)constructionCount * 100 / totalProjects, 2)
            });
        }

        // 统计地区排行（政府采购）
        var govRegionStats = await _context.Announcements
            .Where(x => x.IsDeleted == 0 && x.BusinessType == "GOV_PROCUREMENT")
            .GroupBy(x => x.ProjectRegion)
            .Select(g => new
            {
                Region = g.Key,
                Count = g.Count()
            })
            .ToListAsync();

        // 统计地区排行（建设工程）
        var constructionRegionStats = await _context.Announcements
            .Where(x => x.IsDeleted == 0 && x.BusinessType == "CONSTRUCTION")
            .GroupBy(x => x.ProjectRegion)
            .Select(g => new
            {
                Region = g.Key,
                Count = g.Count()
            })
            .ToListAsync();

        // 合并地区统计
        var regionRanking = govRegionStats
            .Concat(constructionRegionStats)
            .GroupBy(x => x.Region)
            .Select(g => new RegionRankingDto
            {
                Region = g.Key,
                ProjectCount = g.Sum(x => x.Count),
                Amount = 0 // 数据库中没有金额字段，返回0
            })
            .OrderByDescending(x => x.ProjectCount)
            .Take(5)
            .ToList();

        return new HomeStatisticsDto
        {
            TotalProjects = totalProjects,
            TotalAmount = 0, // 数据库中没有金额字段，返回0
            ProjectTypes = projectTypes,
            RegionRanking = regionRanking
        };
    }

    /// <summary>
    /// 获取最新公告列表
    /// </summary>
    public async Task<List<RecentAnnouncementDto>> GetRecentAnnouncementsAsync()
    {
        // 获取最新5条政府采购公告
        var govAnnouncements = await _context.Announcements
            .Where(x => x.IsDeleted == 0 && x.BusinessType == "GOV_PROCUREMENT")
            .OrderByDescending(x => x.PublishTime)
            .Take(5)
            .Select(x => new RecentAnnouncementDto
            {
                Id = (int)x.Id,
                Title = x.Title,
                NoticeType = x.NoticeType,
                ProjectRegion = x.ProjectRegion,
                PublishTime = x.PublishTime,
                SourceType = "政府采购"
            })
            .ToListAsync();

        // 获取最新5条建设工程公告
        var constructionAnnouncements = await _context.Announcements
            .Where(x => x.IsDeleted == 0 && x.BusinessType == "CONSTRUCTION")
            .OrderByDescending(x => x.PublishTime)
            .Take(5)
            .Select(x => new RecentAnnouncementDto
            {
                Id = (int)x.Id,
                Title = x.Title,
                NoticeType = x.NoticeType,
                ProjectRegion = x.ProjectRegion,
                PublishTime = x.PublishTime,
                SourceType = "建设工程"
            })
            .ToListAsync();

        // 合并并按发布时间排序
        var allAnnouncements = govAnnouncements
            .Concat(constructionAnnouncements)
            .OrderByDescending(x => x.PublishTime)
            .Take(10)
            .ToList();

        return allAnnouncements;
    }

    /// <summary>
    /// 获取重要业绩列表
    /// </summary>
    public async Task<List<AchievementDto>> GetAchievementsAsync()
    {
        var achievements = await _context.MajorAchievements
            .Where(x => x.IsDeleted == 0)
            .OrderBy(x => x.SortOrder)
            .ThenByDescending(x => x.CompletionDate)
            .Select(x => new AchievementDto
            {
                Id = x.Id,
                ProjectName = x.ProjectName,
                ProjectType = x.ProjectType,
                ProjectAmount = x.ProjectAmount,
                ClientName = x.ClientName,
                CompletionDate = x.CompletionDate,
                Description = x.Description,
                ImageUrl = null // 新实体使用ImageIds，这里暂时返回null
            })
            .ToListAsync();

        return achievements;
    }
}