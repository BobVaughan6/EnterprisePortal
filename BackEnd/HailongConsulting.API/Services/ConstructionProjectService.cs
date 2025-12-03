using HailongConsulting.API.Common;
using HailongConsulting.API.Data;
using HailongConsulting.API.Models.DTOs;
using HailongConsulting.API.Models.Entities;
using HailongConsulting.API.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HailongConsulting.API.Services;

/// <summary>
/// 建设工程公告服务实现
/// </summary>
public class ConstructionProjectService : IConstructionProjectService
{
    private readonly ApplicationDbContext _context;
    private readonly IConstructionProjectRepository _repository;

    public ConstructionProjectService(ApplicationDbContext context, IConstructionProjectRepository repository)
    {
        _context = context;
        _repository = repository;
    }

    public async Task<PagedResult<ConstructionProjectDto>> GetAnnouncementsAsync(ConstructionProjectQueryViewModel query)
    {
        var announcementsQuery = _context.Set<ConstructionProjectAnnouncement>()
            .Where(a => !a.IsDeleted)
            .AsQueryable();

        // 关键词搜索（标题、招标人、中标人）
        if (!string.IsNullOrWhiteSpace(query.Keyword))
        {
            announcementsQuery = announcementsQuery.Where(a =>
                a.Title.Contains(query.Keyword) ||
                (a.Bidder != null && a.Bidder.Contains(query.Keyword)) ||
                (a.Winner != null && a.Winner.Contains(query.Keyword)));
        }

        // 公告类型筛选
        if (!string.IsNullOrWhiteSpace(query.Type))
        {
            announcementsQuery = announcementsQuery.Where(a => a.NoticeType == query.Type);
        }

        // 地区筛选（支持多个地区，逗号分隔）
        if (!string.IsNullOrWhiteSpace(query.Region))
        {
            var regions = query.Region.Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(r => r.Trim())
                .ToList();
            
            if (regions.Any())
            {
                announcementsQuery = announcementsQuery.Where(a => regions.Contains(a.ProjectRegion));
            }
        }

        // 时间区间筛选
        if (query.StartDate.HasValue)
        {
            announcementsQuery = announcementsQuery.Where(a => 
                a.PublishTime.HasValue && a.PublishTime.Value >= query.StartDate.Value);
        }

        if (query.EndDate.HasValue)
        {
            announcementsQuery = announcementsQuery.Where(a => 
                a.PublishTime.HasValue && a.PublishTime.Value <= query.EndDate.Value);
        }

        // 获取总数
        var totalCount = await announcementsQuery.CountAsync();

        // 排序
        announcementsQuery = query.SortBy?.ToLower() switch
        {
            "title" => query.IsDescending
                ? announcementsQuery.OrderByDescending(a => a.Title)
                : announcementsQuery.OrderBy(a => a.Title),
            "publishtime" => query.IsDescending
                ? announcementsQuery.OrderByDescending(a => a.PublishTime)
                : announcementsQuery.OrderBy(a => a.PublishTime),
            "viewcount" => query.IsDescending
                ? announcementsQuery.OrderByDescending(a => a.ViewCount)
                : announcementsQuery.OrderBy(a => a.ViewCount),
            _ => query.IsDescending
                ? announcementsQuery.OrderByDescending(a => a.CreatedAt)
                : announcementsQuery.OrderBy(a => a.CreatedAt)
        };

        // 分页
        var announcements = await announcementsQuery
            .Skip((query.PageIndex - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToListAsync();

        // 映射到DTO
        var announcementDtos = announcements.Select(a => new ConstructionProjectDto
        {
            Id = a.Id,
            Title = a.Title,
            NoticeType = a.NoticeType,
            Bidder = a.Bidder,
            Winner = a.Winner,
            ProjectRegion = a.ProjectRegion,
            Content = a.Content,
            Publisher = a.Publisher,
            PublishTime = a.PublishTime,
            ViewCount = a.ViewCount,
            AttachmentPath = a.AttachmentPath,
            IsTop = a.IsTop,
            CreatedAt = a.CreatedAt,
            UpdatedAt = a.UpdatedAt
        }).ToList();

        return PagedResult<ConstructionProjectDto>.Create(announcementDtos, totalCount, query.PageIndex, query.PageSize);
    }

    public async Task<ConstructionProjectDto?> GetAnnouncementByIdAsync(int id)
    {
        var announcement = await _context.Set<ConstructionProjectAnnouncement>()
            .FirstOrDefaultAsync(a => a.Id == id && !a.IsDeleted);

        if (announcement == null)
        {
            return null;
        }

        return new ConstructionProjectDto
        {
            Id = announcement.Id,
            Title = announcement.Title,
            NoticeType = announcement.NoticeType,
            Bidder = announcement.Bidder,
            Winner = announcement.Winner,
            ProjectRegion = announcement.ProjectRegion,
            Content = announcement.Content,
            Publisher = announcement.Publisher,
            PublishTime = announcement.PublishTime,
            ViewCount = announcement.ViewCount,
            AttachmentPath = announcement.AttachmentPath,
            IsTop = announcement.IsTop,
            CreatedAt = announcement.CreatedAt,
            UpdatedAt = announcement.UpdatedAt
        };
    }

    public async Task<ConstructionProjectDto> CreateAnnouncementAsync(CreateConstructionProjectDto dto)
    {
        var announcement = new ConstructionProjectAnnouncement
        {
            Title = dto.Title,
            NoticeType = dto.Type,
            Content = dto.Content,
            Bidder = dto.Tenderer,
            Winner = dto.Winner,
            ProjectRegion = dto.Region,
            PublishTime = dto.PublishDate,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };

        await _repository.AddAsync(announcement);
        await _context.SaveChangesAsync();

        return (await GetAnnouncementByIdAsync(announcement.Id))!;
    }

    public async Task<ConstructionProjectDto?> UpdateAnnouncementAsync(int id, UpdateConstructionProjectDto dto)
    {
        var announcement = await _repository.GetByIdAsync(id);

        if (announcement == null || announcement.IsDeleted)
        {
            return null;
        }

        // 更新字段
        if (!string.IsNullOrWhiteSpace(dto.Title))
            announcement.Title = dto.Title;

        if (!string.IsNullOrWhiteSpace(dto.Type))
            announcement.NoticeType = dto.Type;

        if (dto.Content != null)
            announcement.Content = dto.Content;

        if (dto.Tenderer != null)
            announcement.Bidder = dto.Tenderer;

        if (dto.Winner != null)
            announcement.Winner = dto.Winner;

        if (dto.Region != null)
            announcement.ProjectRegion = dto.Region;

        if (dto.PublishDate.HasValue)
            announcement.PublishTime = dto.PublishDate.Value;

        announcement.UpdatedAt = DateTime.Now;

        _repository.Update(announcement);
        await _context.SaveChangesAsync();

        return await GetAnnouncementByIdAsync(id);
    }

    public async Task<bool> DeleteAnnouncementAsync(int id)
    {
        var announcement = await _repository.GetByIdAsync(id);

        if (announcement == null || announcement.IsDeleted)
        {
            return false;
        }

        // 软删除
        announcement.IsDeleted = true;
        announcement.UpdatedAt = DateTime.Now;

        _repository.Update(announcement);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> IncrementViewCountAsync(int id)
    {
        var announcement = await _repository.GetByIdAsync(id);

        if (announcement == null || announcement.IsDeleted)
        {
            return false;
        }

        announcement.ViewCount++;
        _repository.Update(announcement);
        await _context.SaveChangesAsync();

        return true;
    }
}