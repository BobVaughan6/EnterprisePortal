using HailongConsulting.API.Data;
using HailongConsulting.API.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace HailongConsulting.API.Repositories;

/// <summary>
/// 系统配置仓储实现
/// </summary>
public class ConfigRepository : IConfigRepository
{
    private readonly ApplicationDbContext _context;

    public ConfigRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    #region 轮播图

    public async Task<IEnumerable<CarouselBanner>> GetAllBannersAsync()
    {
        return await _context.Set<CarouselBanner>()
            .Where(b => b.IsDeleted == 0)
            .OrderBy(b => b.SortOrder)
            .ThenByDescending(b => b.CreatedAt)
            .ToListAsync();
    }

    public async Task<CarouselBanner?> GetBannerByIdAsync(uint id)
    {
        return await _context.Set<CarouselBanner>()
            .FirstOrDefaultAsync(b => b.Id == id && b.IsDeleted == 0);
    }

    public async Task<CarouselBanner> AddBannerAsync(CarouselBanner banner)
    {
        await _context.Set<CarouselBanner>().AddAsync(banner);
        await _context.SaveChangesAsync();
        return banner;
    }

    public async Task<bool> UpdateBannerAsync(CarouselBanner banner)
    {
        _context.Set<CarouselBanner>().Update(banner);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteBannerAsync(uint id)
    {
        var banner = await GetBannerByIdAsync(id);
        if (banner == null) return false;

        banner.IsDeleted = 1;
        return await UpdateBannerAsync(banner);
    }

    #endregion

    #region 企业简介

    public async Task<CompanyProfile?> GetCompanyProfileAsync()
    {
        return await _context.Set<CompanyProfile>()
            .Where(p => p.IsDeleted == 0)
            .OrderByDescending(p => p.UpdatedAt)
            .FirstOrDefaultAsync();
    }

    public async Task<bool> UpdateCompanyProfileAsync(CompanyProfile profile)
    {
        var existing = await GetCompanyProfileAsync();
        
        if (existing == null)
        {
            // 如果不存在，创建新的
            await _context.Set<CompanyProfile>().AddAsync(profile);
        }
        else
        {
            // 如果存在，更新现有的
            existing.Title = profile.Title;
            existing.Content = profile.Content;
            existing.Highlights = profile.Highlights;
            existing.ImageIds = profile.ImageIds;
            existing.UpdatedAt = DateTime.Now;
            _context.Set<CompanyProfile>().Update(existing);
        }
        
        return await _context.SaveChangesAsync() > 0;
    }

    #endregion

    #region 重要业绩

    public async Task<IEnumerable<MajorAchievement>> GetAllAchievementsAsync()
    {
        return await _context.Set<MajorAchievement>()
            .Where(a => a.IsDeleted == 0)
            .OrderBy(a => a.SortOrder)
            .ThenByDescending(a => a.CompletionDate)
            .ToListAsync();
    }

    public async Task<MajorAchievement?> GetAchievementByIdAsync(uint id)
    {
        return await _context.Set<MajorAchievement>()
            .FirstOrDefaultAsync(a => a.Id == id && a.IsDeleted == 0);
    }

    public async Task<MajorAchievement> AddAchievementAsync(MajorAchievement achievement)
    {
        await _context.Set<MajorAchievement>().AddAsync(achievement);
        await _context.SaveChangesAsync();
        return achievement;
    }

    public async Task<bool> UpdateAchievementAsync(MajorAchievement achievement)
    {
        _context.Set<MajorAchievement>().Update(achievement);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAchievementAsync(uint id)
    {
        var achievement = await GetAchievementByIdAsync(id);
        if (achievement == null) return false;

        achievement.IsDeleted = 1;
        return await UpdateAchievementAsync(achievement);
    }

    #endregion

    #region 友情链接

    public async Task<IEnumerable<FriendlyLink>> GetAllLinksAsync()
    {
        return await _context.Set<FriendlyLink>()
            .Where(l => l.IsDeleted == 0)
            .OrderBy(l => l.SortOrder)
            .ThenByDescending(l => l.CreatedAt)
            .ToListAsync();
    }

    public async Task<FriendlyLink?> GetLinkByIdAsync(uint id)
    {
        return await _context.Set<FriendlyLink>()
            .FirstOrDefaultAsync(l => l.Id == id && l.IsDeleted == 0);
    }

    public async Task<FriendlyLink> AddLinkAsync(FriendlyLink link)
    {
        await _context.Set<FriendlyLink>().AddAsync(link);
        await _context.SaveChangesAsync();
        return link;
    }

    public async Task<bool> UpdateLinkAsync(FriendlyLink link)
    {
        _context.Set<FriendlyLink>().Update(link);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteLinkAsync(uint id)
    {
        var link = await GetLinkByIdAsync(id);
        if (link == null) return false;

        link.IsDeleted = 1;
        return await UpdateLinkAsync(link);
    }

    #endregion

    #region 访问统计

    public async Task<uint> GetTotalVisitsAsync()
    {
        var total = await _context.Set<VisitStatistic>()
            .Where(v => v.IsDeleted == 0)
            .SumAsync(v => (long)v.VisitCount);
        
        return (uint)total;
    }

    public async Task<uint> GetVisitsByDateAsync(DateOnly date)
    {
        var total = await _context.Set<VisitStatistic>()
            .Where(v => v.VisitDate == date && v.IsDeleted == 0)
            .SumAsync(v => (long)v.VisitCount);
        
        return (uint)total;
    }

    public async Task<uint> GetVisitsByDateRangeAsync(DateOnly startDate, DateOnly endDate)
    {
        var total = await _context.Set<VisitStatistic>()
            .Where(v => v.VisitDate >= startDate && v.VisitDate <= endDate && v.IsDeleted == 0)
            .SumAsync(v => (long)v.VisitCount);
        
        return (uint)total;
    }

    public async Task RecordVisitAsync(VisitStatistic statistic)
    {
        // 检查今天是否已有相同页面的访问记录
        var existing = await _context.Set<VisitStatistic>()
            .FirstOrDefaultAsync(v => 
                v.VisitDate == statistic.VisitDate && 
                v.PageUrl == statistic.PageUrl &&
                v.IsDeleted == 0);

        if (existing != null)
        {
            // 如果存在，增加访问次数
            existing.VisitCount++;
            existing.UpdatedAt = DateTime.Now;
            _context.Set<VisitStatistic>().Update(existing);
        }
        else
        {
            // 如果不存在，创建新记录
            await _context.Set<VisitStatistic>().AddAsync(statistic);
        }

        await _context.SaveChangesAsync();
    }

    #endregion
}