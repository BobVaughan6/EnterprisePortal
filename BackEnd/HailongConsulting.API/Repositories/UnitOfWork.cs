using HailongConsulting.API.Data;
using HailongConsulting.API.Models.Entities;
using Microsoft.EntityFrameworkCore.Storage;

namespace HailongConsulting.API.Repositories;

/// <summary>
/// 工作单元实现
/// </summary>
public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private IDbContextTransaction? _transaction;

    private IRepository<User>? _users;
    private IAttachmentRepository? _attachments;
    private IAnnouncementRepository? _announcements;
    private IInfoPublicationRepository? _infoPublications;
    private IRepository<AdminUser>? _adminUsers;
    private IRepository<AdminRole>? _adminRoles;
    private IRepository<BusinessScope>? _businessScopes;
    private IRepository<CompanyQualification>? _companyQualifications;
    private IRepository<RegionDictionary>? _regionDictionaries;
    private IRepository<SystemLog>? _systemLogs;
    private IRepository<CompanyProfile>? _companyProfiles;
    private IRepository<MajorAchievement>? _majorAchievements;
    private IRepository<CarouselBanner>? _carouselBanners;
    private IRepository<FriendlyLink>? _friendlyLinks;
    private IRepository<VisitStatistic>? _visitStatistics;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public IRepository<User> Users => _users ??= new Repository<User>(_context);    
    public IAttachmentRepository Attachments => _attachments ??= new AttachmentRepository(_context);
    public IAnnouncementRepository Announcements => _announcements ??= new AnnouncementRepository(_context);
    public IInfoPublicationRepository InfoPublications => _infoPublications ??= new InfoPublicationRepository(_context);
    public IRepository<AdminUser> AdminUsers => _adminUsers ??= new Repository<AdminUser>(_context);
    public IRepository<AdminRole> AdminRoles => _adminRoles ??= new Repository<AdminRole>(_context);
    public IRepository<BusinessScope> BusinessScopes => _businessScopes ??= new Repository<BusinessScope>(_context);
    public IRepository<CompanyQualification> CompanyQualifications => _companyQualifications ??= new Repository<CompanyQualification>(_context);
    public IRepository<RegionDictionary> RegionDictionaries => _regionDictionaries ??= new Repository<RegionDictionary>(_context);
    public IRepository<SystemLog> SystemLogs => _systemLogs ??= new Repository<SystemLog>(_context);
    public IRepository<CompanyProfile> CompanyProfiles => _companyProfiles ??= new Repository<CompanyProfile>(_context);
    public IRepository<MajorAchievement> MajorAchievements => _majorAchievements ??= new Repository<MajorAchievement>(_context);
    public IRepository<CarouselBanner> CarouselBanners => _carouselBanners ??= new Repository<CarouselBanner>(_context);
    public IRepository<FriendlyLink> FriendlyLinks => _friendlyLinks ??= new Repository<FriendlyLink>(_context);
    public IRepository<VisitStatistic> VisitStatistics => _visitStatistics ??= new Repository<VisitStatistic>(_context);

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public async Task BeginTransactionAsync()
    {
        _transaction = await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        try
        {
            await _context.SaveChangesAsync();
            if (_transaction != null)
            {
                await _transaction.CommitAsync();
            }
        }
        catch
        {
            await RollbackTransactionAsync();
            throw;
        }
        finally
        {
            if (_transaction != null)
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }
    }

    public async Task RollbackTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public void Dispose()
    {
        _transaction?.Dispose();
        _context.Dispose();
    }
}