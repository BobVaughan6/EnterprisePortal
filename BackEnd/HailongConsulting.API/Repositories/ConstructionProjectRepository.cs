using HailongConsulting.API.Data;
using HailongConsulting.API.Models.Entities;

namespace HailongConsulting.API.Repositories;

/// <summary>
/// 建设工程公告仓储实现
/// </summary>
public class ConstructionProjectRepository : Repository<ConstructionProjectAnnouncement>, IConstructionProjectRepository
{
    public ConstructionProjectRepository(ApplicationDbContext context) : base(context)
    {
    }
}