using HailongConsulting.API.Common;
using HailongConsulting.API.Models.DTOs;

namespace HailongConsulting.API.Services;

/// <summary>
/// 建设工程公告服务接口
/// </summary>
public interface IConstructionProjectService
{
    /// <summary>
    /// 获取公告列表（分页）
    /// </summary>
    Task<PagedResult<ConstructionProjectDto>> GetAnnouncementsAsync(ConstructionProjectQueryViewModel query);

    /// <summary>
    /// 根据ID获取公告详情
    /// </summary>
    Task<ConstructionProjectDto?> GetAnnouncementByIdAsync(int id);

    /// <summary>
    /// 创建公告
    /// </summary>
    Task<ConstructionProjectDto> CreateAnnouncementAsync(CreateConstructionProjectDto dto);

    /// <summary>
    /// 更新公告
    /// </summary>
    Task<ConstructionProjectDto?> UpdateAnnouncementAsync(int id, UpdateConstructionProjectDto dto);

    /// <summary>
    /// 删除公告（软删除）
    /// </summary>
    Task<bool> DeleteAnnouncementAsync(int id);

    /// <summary>
    /// 增加浏览次数
    /// </summary>
    Task<bool> IncrementViewCountAsync(int id);
}