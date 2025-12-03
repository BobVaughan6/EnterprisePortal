using HailongConsulting.API.Models.DTOs;
using HailongConsulting.API.Repositories;

namespace HailongConsulting.API.Services;

/// <summary>
/// 全局搜索Service实现
/// </summary>
public class GlobalSearchService : IGlobalSearchService
{
    private readonly IGlobalSearchRepository _searchRepository;

    public GlobalSearchService(IGlobalSearchRepository searchRepository)
    {
        _searchRepository = searchRepository;
    }

    /// <summary>
    /// 全局搜索
    /// </summary>
    public async Task<GlobalSearchResponseDto> SearchAsync(GlobalSearchRequestDto request)
    {
        return await _searchRepository.SearchAsync(request);
    }
}