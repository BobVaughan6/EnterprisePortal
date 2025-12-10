namespace HailongConsulting.API.Models.DTOs;

/// <summary>
/// 区域字典DTO
/// </summary>
public class RegionDictionaryDto
{
    /// <summary>
    /// 区域ID
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// 区域代码
    /// </summary>
    public string RegionCode { get; set; } = string.Empty;

    /// <summary>
    /// 区域名称
    /// </summary>
    public string RegionName { get; set; } = string.Empty;

    /// <summary>
    /// 区域级别：1-省份，2-城市，3-区县
    /// </summary>
    public int RegionLevel { get; set; }

    /// <summary>
    /// 父级区域代码
    /// </summary>
    public string? ParentCode { get; set; }

    /// <summary>
    /// 排序顺序
    /// </summary>
    public int SortOrder { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// 更新时间
    /// </summary>
    public DateTime UpdatedAt { get; set; }

    /// <summary>
    /// 子区域列表（用于树形结构）
    /// </summary>
    public List<RegionDictionaryDto>? Children { get; set; }
}

/// <summary>
/// 区域字典创建DTO
/// </summary>
public class CreateRegionDictionaryDto
{
    /// <summary>
    /// 区域代码
    /// </summary>
    public string RegionCode { get; set; } = string.Empty;

    /// <summary>
    /// 区域名称
    /// </summary>
    public string RegionName { get; set; } = string.Empty;

    /// <summary>
    /// 区域级别：1-省份，2-城市，3-区县
    /// </summary>
    public int RegionLevel { get; set; }

    /// <summary>
    /// 父级区域代码
    /// </summary>
    public string? ParentCode { get; set; }

    /// <summary>
    /// 排序顺序
    /// </summary>
    public int SortOrder { get; set; } = 0;
}

/// <summary>
/// 区域字典更新DTO
/// </summary>
public class UpdateRegionDictionaryDto
{
    /// <summary>
    /// 区域名称
    /// </summary>
    public string? RegionName { get; set; }

    /// <summary>
    /// 排序顺序
    /// </summary>
    public int? SortOrder { get; set; }
}

/// <summary>
/// 区域字典查询DTO
/// </summary>
public class RegionDictionaryQueryDto
{
    /// <summary>
    /// 搜索关键词
    /// </summary>
    public string? Keyword { get; set; }

    /// <summary>
    /// 区域级别
    /// </summary>
    public int? RegionLevel { get; set; }

    /// <summary>
    /// 父级区域代码
    /// </summary>
    public string? ParentCode { get; set; }
}

/// <summary>
/// 区域树节点DTO
/// </summary>
public class RegionTreeNodeDto
{
    /// <summary>
    /// 区域ID
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// 区域代码
    /// </summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// 区域名称
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 区域级别
    /// </summary>
    public int Level { get; set; }

    /// <summary>
    /// 父级代码
    /// </summary>
    public string? ParentCode { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    public int SortOrder { get; set; }

    /// <summary>
    /// 子节点
    /// </summary>
    public List<RegionTreeNodeDto>? Children { get; set; }
}