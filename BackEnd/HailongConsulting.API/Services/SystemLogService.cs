using AutoMapper;
using HailongConsulting.API.Common;
using HailongConsulting.API.Models.DTOs;
using HailongConsulting.API.Models.Entities;
using HailongConsulting.API.Repositories;

namespace HailongConsulting.API.Services;

/// <summary>
/// 系统日志服务实现
/// </summary>
public class SystemLogService : ISystemLogService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISystemLogRepository _systemLogRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<SystemLogService> _logger;

    public SystemLogService(
        IUnitOfWork unitOfWork,
        ISystemLogRepository systemLogRepository,
        IMapper mapper,
        ILogger<SystemLogService> logger)
    {
        _unitOfWork = unitOfWork;
        _systemLogRepository = systemLogRepository;
        _mapper = mapper;
        _logger = logger;
    }

    /// <summary>
    /// 获取系统日志列表（分页）
    /// </summary>
    public async Task<PagedResult<SystemLogDto>> GetPagedListAsync(SystemLogQueryDto query)
    {
        var (items, total) = await _systemLogRepository.GetPagedListAsync(
            query.Action,
            query.Module,
            query.Username,
            query.IpAddress,
            query.Status,
            query.StartDate,
            query.EndDate,
            query.Page,
            query.PageSize);

        var dtos = _mapper.Map<List<SystemLogDto>>(items);

        return new PagedResult<SystemLogDto>
        {
            Items = dtos,
            TotalCount = total,
            PageIndex = query.Page,
            PageSize = query.PageSize
        };
    }

    /// <summary>
    /// 根据ID获取系统日志
    /// </summary>
    public async Task<SystemLogDto?> GetByIdAsync(ulong id)
    {
        var log = await _systemLogRepository.GetByIdAsync(id);
        if (log == null)
        {
            return null;
        }

        return _mapper.Map<SystemLogDto>(log);
    }

    /// <summary>
    /// 创建系统日志
    /// </summary>
    public async Task<SystemLogDto> CreateAsync(CreateSystemLogDto dto)
    {
        var log = new SystemLog
        {
            UserId = dto.UserId,
            Username = dto.Username,
            Action = dto.Action,
            Module = dto.Module,
            Description = dto.Description,
            IpAddress = dto.IpAddress,
            UserAgent = dto.UserAgent,
            RequestData = dto.RequestData,
            ResponseData = dto.ResponseData,
            Status = dto.Status,
            CreatedAt = DateTime.Now
        };

        await _systemLogRepository.AddAsync(log);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<SystemLogDto>(log);
    }

    /// <summary>
    /// 清空系统日志
    /// </summary>
    public async Task<bool> ClearLogsAsync()
    {
        try
        {
            await _systemLogRepository.ClearLogsAsync();
            _logger.LogInformation("System logs cleared");
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to clear system logs");
            return false;
        }
    }

    /// <summary>
    /// 删除指定日期之前的日志
    /// </summary>
    public async Task<int> DeleteLogsBeforeDateAsync(DateTime date)
    {
        var count = await _systemLogRepository.DeleteLogsBeforeDateAsync(date);
        _logger.LogInformation("Deleted {Count} system logs before {Date}", count, date);
        return count;
    }
}