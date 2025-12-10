using HailongConsulting.API.Data;
using HailongConsulting.API.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace HailongConsulting.API.Repositories;

/// <summary>
/// 附件仓储实现
/// </summary>
public class AttachmentRepository : Repository<Attachment>, IAttachmentRepository
{
    public AttachmentRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Attachment>> GetByRelatedAsync(string relatedType, int relatedId)
    {
        return await _dbSet
            .Where(a => a.RelatedType == relatedType && a.RelatedId == relatedId && a.IsDeleted == 0)
            .OrderByDescending(a => a.CreatedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<Attachment>> GetByIdsAsync(IEnumerable<int> ids)
    {
        return await _dbSet
            .Where(a => ids.Contains(a.Id) && a.IsDeleted == 0)
            .ToListAsync();
    }

    public async Task SoftDeleteAsync(int id)
    {
        var attachment = await _dbSet.FindAsync(id);
        if (attachment != null)
        {
            attachment.IsDeleted = 1;
            attachment.UpdatedAt = DateTime.UtcNow;
        }
    }

    public async Task SoftDeleteRangeAsync(IEnumerable<int> ids)
    {
        var attachments = await _dbSet.Where(a => ids.Contains(a.Id)).ToListAsync();
        foreach (var attachment in attachments)
        {
            attachment.IsDeleted = 1;
            attachment.UpdatedAt = DateTime.UtcNow;
        }
    }
}