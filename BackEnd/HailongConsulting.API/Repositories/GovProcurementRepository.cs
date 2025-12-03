using HailongConsulting.API.Data;
using HailongConsulting.API.Models.Entities;

namespace HailongConsulting.API.Repositories;

/// <summary>
/// 政府采购公告仓储实现
/// </summary>
public class GovProcurementRepository : Repository<GovProcurementAnnouncement>, IGovProcurementRepository
{
    public GovProcurementRepository(ApplicationDbContext context) : base(context)
    {
    }
}