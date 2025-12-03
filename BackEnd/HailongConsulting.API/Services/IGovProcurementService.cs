using HailongConsulting.API.Common;
using HailongConsulting.API.Models.DTOs;

namespace HailongConsulting.API.Services;

/// <summary>
/// 政府采购公告服务接口
/// </summary>
public interface IGovProcurementService
{
    /// <summary>
    /// 获取公告列表（分页）
    /// </summary>
    Task<PagedResult<GovProcurementDto>> GetAnnouncementsAsync(GovProcurementQueryViewModel query);

    /// <summary>
    /// 根据ID获取公告详情
    /// </summary>
    Task<GovProcurementDto?> GetAnnouncementByIdAsync(int id);

    /// <summary>
    /// 创建公告
    /// </summary>
    Task<GovProcurementDto> CreateAnnouncementAsync(CreateGovProcurementDto dto);

    /// <summary>
    /// 更新公告
    /// </summary>
    Task<GovProcurementDto?> UpdateAnnouncementAsync(int id, UpdateGovProcurementDto dto);

    /// <summary>
    /// 删除公告（软删除）
    /// </summary>
    Task<bool> DeleteAnnouncementAsync(int id);

    /// <summary>
    /// 增加浏览次数
    /// </summary>
    Task<bool> IncrementViewCountAsync(int id);
}