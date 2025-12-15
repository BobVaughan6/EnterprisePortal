using HailongConsulting.API.Data;
using HailongConsulting.API.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace HailongConsulting.API.Repositories;

/// <summary>
/// 系统日志仓储实现
/// </summary>
public class SystemLogRepository : Repository<SystemLog>, ISystemLogRepository
{
    public SystemLogRepository(ApplicationDbContext context) : base(context)
    {
    }

    /// <summary>
    /// 获取系统日志列表（分页）
    /// </summary>
    public async Task<(List<SystemLog> Items, int Total)> GetPagedListAsync(
        string? action,
        string? module,
        string? username,
        string? ipAddress,
        string? status,
        DateTime? startDate,
        DateTime? endDate,
        int page,
        int pageSize)
    {
        var query = _context.SystemLogs.AsQueryable();

        // 应用筛选条件
        if (!string.IsNullOrWhiteSpace(action))
        {
            query = query.Where(l => l.Action.Contains(action));
        }

        if (!string.IsNullOrWhiteSpace(module))
        {
            query = query.Where(l => l.Module != null && l.Module.Contains(module));
        }

        if (!string.IsNullOrWhiteSpace(username))
        {
            query = query.Where(l => l.Username != null && l.Username.Contains(username));
        }

        if (!string.IsNullOrWhiteSpace(ipAddress))
        {
            query = query.Where(l => l.IpAddress != null && l.IpAddress.Contains(ipAddress));
        }

        if (!string.IsNullOrWhiteSpace(status))
        {
            query = query.Where(l => l.Status == status);
        }

        if (startDate.HasValue)
        {
            query = query.Where(l => l.CreatedAt >= startDate.Value);
        }

        if (endDate.HasValue)
        {
            var endDateTime = endDate.Value.Date.AddDays(1).AddSeconds(-1);
            query = query.Where(l => l.CreatedAt <= endDateTime);
        }

        // 获取总数
        var total = await query.CountAsync();

        // 分页查询
        var items = await query
            .OrderByDescending(l => l.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (items, total);
    }

    /// <summary>
    /// 清空系统日志
    /// </summary>
    public async Task<int> ClearLogsAsync()
    {
        return await _context.Database.ExecuteSqlRawAsync("TRUNCATE TABLE system_logs");
    }

    /// <summary>
    /// 删除指定日期之前的日志
    /// </summary>
    public async Task<int> DeleteLogsBeforeDateAsync(DateTime date)
    {
        return await _context.Database.ExecuteSqlRawAsync(
            "DELETE FROM system_logs WHERE created_at < {0}", date);
    }
}