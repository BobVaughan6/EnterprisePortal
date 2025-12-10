using HailongConsulting.API.Models.DTOs;

namespace HailongConsulting.API.Services;

/// <summary>
/// 区域字典服务接口
/// </summary>
public interface IRegionDictionaryService
{
    /// <summary>
    /// 获取区域列表
    /// </summary>
    Task<List<RegionDictionaryDto>> GetRegionsAsync(RegionDictionaryQueryDto query);

    /// <summary>
    /// 获取区域树形结构
    /// </summary>
    Task<List<RegionTreeNodeDto>> GetRegionTreeAsync();

    /// <summary>
    /// 获取省份列表
    /// </summary>
    Task<List<RegionDictionaryDto>> GetProvincesAsync();

    /// <summary>
    /// 获取城市列表
    /// </summary>
    Task<List<RegionDictionaryDto>> GetCitiesAsync(string provinceCode);

    /// <summary>
    /// 获取区县列表
    /// </summary>
    Task<List<RegionDictionaryDto>> GetDistrictsAsync(string cityCode);

    /// <summary>
    /// 根据ID获取区域详情
    /// </summary>
    Task<RegionDictionaryDto?> GetRegionByIdAsync(int id);

    /// <summary>
    /// 根据区域代码获取区域详情
    /// </summary>
    Task<RegionDictionaryDto?> GetRegionByCodeAsync(string regionCode);

    /// <summary>
    /// 创建区域
    /// </summary>
    Task<RegionDictionaryDto> CreateRegionAsync(CreateRegionDictionaryDto dto);

    /// <summary>
    /// 更新区域
    /// </summary>
    Task<RegionDictionaryDto> UpdateRegionAsync(int id, UpdateRegionDictionaryDto dto);

    /// <summary>
    /// 删除区域
    /// </summary>
    Task DeleteRegionAsync(int id);

    /// <summary>
    /// 批量导入区域数据
    /// </summary>
    Task<int> ImportRegionsAsync(List<CreateRegionDictionaryDto> regions);
}