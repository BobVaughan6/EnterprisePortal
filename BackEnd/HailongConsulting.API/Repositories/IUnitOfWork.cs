using HailongConsulting.API.Models.Entities;

namespace HailongConsulting.API.Repositories;

/// <summary>
/// 工作单元接口
/// </summary>
public interface IUnitOfWork : IDisposable
{
    IRepository<User> Users { get; }
    IRepository<Project> Projects { get; }
    IRepository<Client> Clients { get; }
    IRepository<Category> Categories { get; }
    
    // 新增的仓储
    IAttachmentRepository Attachments { get; }
    IAnnouncementRepository Announcements { get; }
    IInfoPublicationRepository InfoPublications { get; }
    IRepository<AdminUser> AdminUsers { get; }
    IRepository<AdminRole> AdminRoles { get; }
    IRepository<BusinessScope> BusinessScopes { get; }
    IRepository<CompanyQualification> CompanyQualifications { get; }
    IRepository<RegionDictionary> RegionDictionaries { get; }
    IRepository<SystemLog> SystemLogs { get; }
    IRepository<CompanyProfile> CompanyProfiles { get; }
    IRepository<MajorAchievement> MajorAchievements { get; }
    IRepository<CarouselBanner> CarouselBanners { get; }
    IRepository<FriendlyLink> FriendlyLinks { get; }
    IRepository<VisitStatistic> VisitStatistics { get; }
    
    Task<int> SaveChangesAsync();
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
}