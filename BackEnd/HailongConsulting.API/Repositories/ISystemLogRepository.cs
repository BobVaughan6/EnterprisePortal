using HailongConsulting.API.Models.Entities;

namespace HailongConsulting.API.Repositories;

/// <summary>
/// 系统日志仓储接口
/// </summary>
public interface ISystemLogRepository : IRepository<SystemLog>
{
    /// <summary>
    /// 获取系统日志列表（分页）
    /// </summary>
    Task<(List<SystemLog> Items, int Total)> GetPagedListAsync(
        string? action,
        string? module,
        string? username,
        string? ipAddress,
        string? status,
        DateTime? startDate,
        DateTime? endDate,
        int page,
        int pageSize);

    /// <summary>
    /// 清空系统日志
    /// </summary>
    Task<int> ClearLogsAsync();

    /// <summary>
    /// 删除指定日期之前的日志
    /// </summary>
    Task<int> DeleteLogsBeforeDateAsync(DateTime date);
}