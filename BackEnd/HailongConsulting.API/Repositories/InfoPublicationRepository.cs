using HailongConsulting.API.Data;
using HailongConsulting.API.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace HailongConsulting.API.Repositories;

/// <summary>
/// 信息发布仓储实现
/// </summary>
public class InfoPublicationRepository : Repository<InfoPublication>, IInfoPublicationRepository
{
    public InfoPublicationRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<InfoPublication>> GetByTypeAsync(string type)
    {
        return await _dbSet
            .Where(i => i.Type == type && i.IsDeleted == 0)
            .OrderByDescending(i => i.PublishTime)
            .ToListAsync();
    }

    public async Task<IEnumerable<InfoPublication>> GetByTypeCategoryAsync(string type, string? category)
    {
        var query = _dbSet.Where(i => i.Type == type && i.IsDeleted == 0);

        if (!string.IsNullOrEmpty(category))
            query = query.Where(i => i.Category == category);

        return await query
            .OrderByDescending(i => i.PublishTime)
            .ToListAsync();
    }

    public async Task<(IEnumerable<InfoPublication> Items, int TotalCount)> GetPagedPublicationsAsync(
        string? type,
        string? category,
        string? keyword,
        int pageIndex,
        int pageSize)
    {
        var query = _dbSet.Where(i => i.IsDeleted == 0);

        if (!string.IsNullOrEmpty(type))
            query = query.Where(i => i.Type == type);

        if (!string.IsNullOrEmpty(category))
            query = query.Where(i => i.Category == category);

        if (!string.IsNullOrEmpty(keyword))
        {
            query = query.Where(i => 
                i.Title.Contains(keyword) || 
                (i.Summary != null && i.Summary.Contains(keyword)));
        }

        var totalCount = await query.CountAsync();

        var items = await query
            .OrderByDescending(i => i.PublishTime)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (items, totalCount);
    }

    public async Task SoftDeleteAsync(uint id)
    {
        var publication = await _dbSet.FindAsync(id);
        if (publication != null)
        {
            publication.IsDeleted = 1;
            publication.UpdatedAt = DateTime.UtcNow;
        }
    }

    public async Task IncrementViewCountAsync(uint id)
    {
        var publication = await _dbSet.FindAsync(id);
        if (publication != null)
        {
            publication.ViewCount++;
            publication.UpdatedAt = DateTime.UtcNow;
        }
    }
}