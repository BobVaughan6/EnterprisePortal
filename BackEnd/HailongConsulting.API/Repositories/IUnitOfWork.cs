using HailongConsulting.API.Models.Entities;

namespace HailongConsulting.API.Repositories;

/// <summary>
/// 工作单元接口
/// </summary>
public interface IUnitOfWork : IDisposable
{
    IRepository<User> Users { get; }
    IAttachmentRepository Attachments { get; }
    IAnnouncementRepository Announcements { get; }
    IInfoPublicationRepository InfoPublications { get; }
    IRepository<BusinessScope> BusinessScopes { get; }
    IRepository<CompanyQualification> CompanyQualifications { get; }
    IRegionDictionaryRepository RegionDictionaries { get; }
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