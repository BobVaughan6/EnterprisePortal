using HailongConsulting.API.Models.DTOs;

namespace HailongConsulting.API.Repositories;

/// <summary>
/// 全局搜索Repository接口
/// </summary>
public interface IGlobalSearchRepository
{
    /// <summary>
    /// 全局搜索
    /// </summary>
    Task<GlobalSearchResponseDto> SearchAsync(GlobalSearchRequestDto request);
}