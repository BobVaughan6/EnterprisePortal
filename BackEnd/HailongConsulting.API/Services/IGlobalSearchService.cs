using HailongConsulting.API.Models.DTOs;

namespace HailongConsulting.API.Services;

/// <summary>
/// 全局搜索Service接口
/// </summary>
public interface IGlobalSearchService
{
    /// <summary>
    /// 全局搜索
    /// </summary>
    Task<GlobalSearchResponseDto> SearchAsync(GlobalSearchRequestDto request);
}