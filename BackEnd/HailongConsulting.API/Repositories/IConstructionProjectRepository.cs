using HailongConsulting.API.Models.Entities;

namespace HailongConsulting.API.Repositories;

/// <summary>
/// 建设工程公告仓储接口
/// </summary>
public interface IConstructionProjectRepository : IRepository<ConstructionProjectAnnouncement>
{
    // 可以在这里添加特定于建设工程公告的查询方法
}