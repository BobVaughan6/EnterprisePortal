using HailongConsulting.API.Common;
using HailongConsulting.API.Models.DTOs;

namespace HailongConsulting.API.Services;

/// <summary>
/// 系统日志服务接口
/// </summary>
public interface ISystemLogService
{
    /// <summary>
    /// 获取系统日志列表（分页）
    /// </summary>
    Task<PagedResult<SystemLogDto>> GetPagedListAsync(SystemLogQueryDto query);

    /// <summary>
    /// 根据ID获取系统日志
    /// </summary>
    Task<SystemLogDto?> GetByIdAsync(ulong id);

    /// <summary>
    /// 创建系统日志
    /// </summary>
    Task<SystemLogDto> CreateAsync(CreateSystemLogDto dto);

    /// <summary>
    /// 清空系统日志
    /// </summary>
    Task<bool> ClearLogsAsync();

    /// <summary>
    /// 删除指定日期之前的日志
    /// </summary>
    Task<int> DeleteLogsBeforeDateAsync(DateTime date);
}