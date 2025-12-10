using HailongConsulting.API.Common;
using HailongConsulting.API.Models.DTOs;

namespace HailongConsulting.API.Services;

/// <summary>
/// 附件服务接口
/// </summary>
public interface IAttachmentService
{
    /// <summary>
    /// 获取分页附件列表
    /// </summary>
    Task<PagedResult<AttachmentDto>> GetPagedAsync(int page, int pageSize, string? category = null, string? relatedType = null, string? keyword = null);
    
    /// <summary>
    /// 上传附件
    /// </summary>
    Task<AttachmentDto> UploadAsync(UploadAttachmentDto uploadDto);
    
    /// <summary>
    /// 根据ID获取附件
    /// </summary>
    Task<AttachmentDto?> GetByIdAsync(int id);
    
    /// <summary>
    /// 根据关联类型和ID获取附件列表
    /// </summary>
    Task<IEnumerable<AttachmentDto>> GetByRelatedAsync(string relatedType, int relatedId);
    
    /// <summary>
    /// 根据ID列表获取附件
    /// </summary>
    Task<IEnumerable<AttachmentDto>> GetByIdsAsync(IEnumerable<int> ids);
    
    /// <summary>
    /// 删除附件
    /// </summary>
    Task<bool> DeleteAsync(int id);
    
    /// <summary>
    /// 批量删除附件
    /// </summary>
    Task<bool> DeleteRangeAsync(IEnumerable<int> ids);
}