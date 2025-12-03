namespace HailongConsulting.API.Models.ViewModels;

/// <summary>
/// 项目查询视图模型
/// </summary>
public class ProjectQueryViewModel
{
    /// <summary>
    /// 搜索关键词
    /// </summary>
    public string? Keyword { get; set; }

    /// <summary>
    /// 客户ID
    /// </summary>
    public int? ClientId { get; set; }

    /// <summary>
    /// 分类ID
    /// </summary>
    public int? CategoryId { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public string? Status { get; set; }

    /// <summary>
    /// 是否精选
    /// </summary>
    public bool? IsFeatured { get; set; }

    /// <summary>
    /// 开始日期（起）
    /// </summary>
    public DateTime? StartDateFrom { get; set; }

    /// <summary>
    /// 开始日期（止）
    /// </summary>
    public DateTime? StartDateTo { get; set; }

    /// <summary>
    /// 页码
    /// </summary>
    public int PageIndex { get; set; } = 1;

    /// <summary>
    /// 每页大小
    /// </summary>
    public int PageSize { get; set; } = 10;

    /// <summary>
    /// 排序字段
    /// </summary>
    public string? SortBy { get; set; } = "CreatedAt";

    /// <summary>
    /// 是否降序
    /// </summary>
    public bool IsDescending { get; set; } = true;
}