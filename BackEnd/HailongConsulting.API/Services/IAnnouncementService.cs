using HailongConsulting.API.Common;
using HailongConsulting.API.Models.DTOs;

namespace HailongConsulting.API.Services;

/// <summary>
/// 公告服务接口
/// </summary>
public interface IAnnouncementService
{
    /// <summary>
    /// 创建公告
    /// </summary>
    Task<AnnouncementDto> CreateAsync(CreateAnnouncementDto createDto);
    
    /// <summary>
    /// 更新公告
    /// </summary>
    Task<AnnouncementDto?> UpdateAsync(uint id, UpdateAnnouncementDto updateDto);
    
    /// <summary>
    /// 根据ID获取公告
    /// </summary>
    Task<AnnouncementDto?> GetByIdAsync(uint id);
    
    /// <summary>
    /// 分页查询公告
    /// </summary>
    Task<PagedResult<AnnouncementDto>> GetPagedAsync(AnnouncementQueryDto queryDto);
    
    /// <summary>
    /// 删除公告
    /// </summary>
    Task<bool> DeleteAsync(uint id);
    
    /// <summary>
    /// 增加浏览次数
    /// </summary>
    Task IncrementViewCountAsync(uint id);
}