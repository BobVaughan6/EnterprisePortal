using HailongConsulting.API.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace HailongConsulting.API.Data;

/// <summary>
/// 应用程序数据库上下文
/// </summary>
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // 用户权限管理模块
    public DbSet<User> Users { get; set; }
    public DbSet<AdminUser> AdminUsers { get; set; }
    public DbSet<AdminRole> AdminRoles { get; set; }

    // 附件管理模块
    public DbSet<Attachment> Attachments { get; set; }

    // 公告信息模块（统一公告表）
    public DbSet<Announcement> Announcements { get; set; }

    // 信息发布模块（统一信息发布表）
    public DbSet<InfoPublication> InfoPublications { get; set; }

    // 系统配置模块
    public DbSet<CompanyProfile> CompanyProfiles { get; set; }
    public DbSet<BusinessScope> BusinessScopes { get; set; }
    public DbSet<CompanyQualification> CompanyQualifications { get; set; }
    public DbSet<MajorAchievement> MajorAchievements { get; set; }
    public DbSet<CarouselBanner> CarouselBanners { get; set; }
    public DbSet<FriendlyLink> FriendlyLinks { get; set; }

    // 统计模块
    public DbSet<VisitStatistic> VisitStatistics { get; set; }

    // 区域字典
    public DbSet<RegionDictionary> RegionDictionaries { get; set; }

    // 系统日志
    public DbSet<SystemLog> SystemLogs { get; set; }

    // 旧的实体（保留以兼容现有代码，后续可以逐步迁移）
    public DbSet<Project> Projects { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<GovProcurementAnnouncement> GovProcurementAnnouncements { get; set; }
    public DbSet<ConstructionProjectAnnouncement> ConstructionProjectAnnouncements { get; set; }
    public DbSet<CompanyAnnouncement> CompanyAnnouncements { get; set; }
    public DbSet<PolicyRegulation> PolicyRegulations { get; set; }
    public DbSet<PolicyInformation> PolicyInformation { get; set; }
    public DbSet<NoticeAnnouncement> NoticeAnnouncements { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // User配置
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(e => e.Username).IsUnique();
            entity.HasIndex(e => e.Email);
            entity.HasIndex(e => e.RefreshToken);
            entity.HasIndex(e => e.Role);
            entity.HasIndex(e => e.Status);
            entity.HasIndex(e => e.IsDeleted);
        });

        // AdminUser配置
        modelBuilder.Entity<AdminUser>(entity =>
        {
            entity.HasIndex(e => e.Username).IsUnique();
            entity.HasIndex(e => e.RoleId);
            entity.HasIndex(e => e.Status);
            entity.HasIndex(e => e.IsDeleted);
        });

        // AdminRole配置
        modelBuilder.Entity<AdminRole>(entity =>
        {
            entity.HasIndex(e => e.RoleCode).IsUnique();
            entity.HasIndex(e => e.Status);
            entity.HasIndex(e => e.IsDeleted);
        });

        // Attachment配置
        modelBuilder.Entity<Attachment>(entity =>
        {
            entity.HasIndex(e => new { e.RelatedType, e.RelatedId });
            entity.HasIndex(e => e.Category);
            entity.HasIndex(e => e.UploadUserId);
            entity.HasIndex(e => e.IsDeleted);
        });

        // Announcement配置（统一公告表）
        modelBuilder.Entity<Announcement>(entity =>
        {
            entity.HasIndex(e => e.BusinessType);
            entity.HasIndex(e => e.NoticeType);
            entity.HasIndex(e => e.Province);
            entity.HasIndex(e => e.City);
            entity.HasIndex(e => e.District);
            entity.HasIndex(e => e.ProjectRegion);
            entity.HasIndex(e => e.PublishTime);
            entity.HasIndex(e => e.Deadline);
            entity.HasIndex(e => e.BudgetAmount);
            entity.HasIndex(e => e.IsTop);
            entity.HasIndex(e => e.Status);
            entity.HasIndex(e => e.IsDeleted);
        });

        // InfoPublication配置（统一信息发布表）
        modelBuilder.Entity<InfoPublication>(entity =>
        {
            entity.HasIndex(e => e.Type);
            entity.HasIndex(e => e.Category);
            entity.HasIndex(e => e.Author);
            entity.HasIndex(e => e.PublishTime);
            entity.HasIndex(e => e.IsTop);
            entity.HasIndex(e => e.Status);
            entity.HasIndex(e => e.IsDeleted);
        });

        // CompanyProfile配置
        modelBuilder.Entity<CompanyProfile>(entity =>
        {
            entity.HasIndex(e => e.Status);
            entity.HasIndex(e => e.IsDeleted);
        });

        // BusinessScope配置
        modelBuilder.Entity<BusinessScope>(entity =>
        {
            entity.HasIndex(e => e.SortOrder);
            entity.HasIndex(e => e.Status);
            entity.HasIndex(e => e.IsDeleted);
        });

        // CompanyQualification配置
        modelBuilder.Entity<CompanyQualification>(entity =>
        {
            entity.HasIndex(e => e.IssueDate);
            entity.HasIndex(e => e.SortOrder);
            entity.HasIndex(e => e.Status);
            entity.HasIndex(e => e.IsDeleted);
        });

        // MajorAchievement配置
        modelBuilder.Entity<MajorAchievement>(entity =>
        {
            entity.HasIndex(e => e.ProjectType);
            entity.HasIndex(e => e.CompletionDate);
            entity.HasIndex(e => e.SortOrder);
            entity.HasIndex(e => e.Status);
            entity.HasIndex(e => e.IsDeleted);
        });

        // CarouselBanner配置
        modelBuilder.Entity<CarouselBanner>(entity =>
        {
            entity.HasIndex(e => e.SortOrder);
            entity.HasIndex(e => e.Status);
            entity.HasIndex(e => e.IsDeleted);
        });

        // FriendlyLink配置
        modelBuilder.Entity<FriendlyLink>(entity =>
        {
            entity.HasIndex(e => e.SortOrder);
            entity.HasIndex(e => e.Status);
            entity.HasIndex(e => e.IsDeleted);
        });

        // VisitStatistic配置
        modelBuilder.Entity<VisitStatistic>(entity =>
        {
            entity.HasIndex(e => e.VisitDate);
            entity.HasIndex(e => e.PageUrl);
            entity.HasIndex(e => e.VisitorIp);
            entity.HasIndex(e => e.IsDeleted);
        });

        // RegionDictionary配置
        modelBuilder.Entity<RegionDictionary>(entity =>
        {
            entity.HasIndex(e => e.RegionCode).IsUnique();
            entity.HasIndex(e => e.RegionLevel);
            entity.HasIndex(e => e.ParentCode);
            entity.HasIndex(e => e.SortOrder);
            entity.HasIndex(e => e.IsDeleted);
        });

        // SystemLog配置
        modelBuilder.Entity<SystemLog>(entity =>
        {
            entity.HasIndex(e => e.UserId);
            entity.HasIndex(e => e.Action);
            entity.HasIndex(e => e.Module);
            entity.HasIndex(e => e.Status);
            entity.HasIndex(e => e.CreatedAt);
        });

        // 旧实体配置（保留兼容性）
        ConfigureOldEntities(modelBuilder);
    }

    private void ConfigureOldEntities(ModelBuilder modelBuilder)
    {
        // Project配置
        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasIndex(e => e.ProjectCode).IsUnique();
            
            entity.HasOne(p => p.Client)
                .WithMany(c => c.Projects)
                .HasForeignKey(p => p.ClientId)
                .OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(p => p.Category)
                .WithMany(c => c.Projects)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(p => p.ProjectManager)
                .WithMany()
                .HasForeignKey(p => p.ProjectManagerId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        // Client配置
        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasIndex(e => e.ClientCode).IsUnique();
        });

        // Category配置
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasIndex(e => e.CategoryCode).IsUnique();
            
            entity.HasOne(c => c.ParentCategory)
                .WithMany(c => c.SubCategories)
                .HasForeignKey(c => c.ParentId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // GovProcurementAnnouncement配置
        modelBuilder.Entity<GovProcurementAnnouncement>(entity =>
        {
            entity.HasIndex(e => e.NoticeType);
            entity.HasIndex(e => e.ProjectRegion);
            entity.HasIndex(e => e.PublishTime);
            entity.HasIndex(e => e.IsTop);
            entity.HasIndex(e => e.IsDeleted);
        });

        // ConstructionProjectAnnouncement配置
        modelBuilder.Entity<ConstructionProjectAnnouncement>(entity =>
        {
            entity.HasIndex(e => e.NoticeType);
            entity.HasIndex(e => e.ProjectRegion);
            entity.HasIndex(e => e.PublishTime);
            entity.HasIndex(e => e.IsTop);
            entity.HasIndex(e => e.IsDeleted);
        });

        // CompanyAnnouncement配置
        modelBuilder.Entity<CompanyAnnouncement>(entity =>
        {
            entity.HasIndex(e => e.PublishTime);
            entity.HasIndex(e => e.IsTop);
            entity.HasIndex(e => e.IsDeleted);
        });

        // PolicyRegulation配置
        modelBuilder.Entity<PolicyRegulation>(entity =>
        {
            entity.HasIndex(e => e.PublishTime);
            entity.HasIndex(e => e.IsTop);
            entity.HasIndex(e => e.IsDeleted);
        });

        // PolicyInformation配置
        modelBuilder.Entity<PolicyInformation>(entity =>
        {
            entity.HasIndex(e => e.PublishTime);
            entity.HasIndex(e => e.IsTop);
            entity.HasIndex(e => e.IsDeleted);
        });

        // NoticeAnnouncement配置
        modelBuilder.Entity<NoticeAnnouncement>(entity =>
        {
            entity.HasIndex(e => e.PublishTime);
            entity.HasIndex(e => e.IsTop);
            entity.HasIndex(e => e.IsDeleted);
        });
    }

    public override int SaveChanges()
    {
        UpdateTimestamps();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateTimestamps();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void UpdateTimestamps()
    {
        var entries = ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

        foreach (var entry in entries)
        {
            if (entry.Entity.GetType().GetProperty("UpdatedAt") != null)
            {
                entry.Property("UpdatedAt").CurrentValue = DateTime.Now;
            }

            if (entry.State == EntityState.Added && 
                entry.Entity.GetType().GetProperty("CreatedAt") != null)
            {
                entry.Property("CreatedAt").CurrentValue = DateTime.Now;
            }
        }
    }
}