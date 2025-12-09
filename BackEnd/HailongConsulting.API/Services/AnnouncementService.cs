using AutoMapper;
using HailongConsulting.API.Common;
using HailongConsulting.API.Models.DTOs;
using HailongConsulting.API.Models.Entities;
using HailongConsulting.API.Repositories;

namespace HailongConsulting.API.Services;

/// <summary>
/// 公告服务实现
/// </summary>
public class AnnouncementService : IAnnouncementService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<AnnouncementService> _logger;

    public AnnouncementService(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ILogger<AnnouncementService> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<AnnouncementDto> CreateAsync(CreateAnnouncementDto createDto)
    {
        try
        {
            var announcement = _mapper.Map<Announcement>(createDto);
            announcement.CreatedAt = DateTime.UtcNow;
            announcement.UpdatedAt = DateTime.UtcNow;
            announcement.ViewCount = 0;

            await _unitOfWork.Announcements.AddAsync(announcement);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<AnnouncementDto>(announcement);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "创建公告失败");
            throw;
        }
    }

    public async Task<AnnouncementDto?> UpdateAsync(uint id, UpdateAnnouncementDto updateDto)
    {
        try
        {
            var announcement = await _unitOfWork.Announcements.FirstOrDefaultAsync(a => a.Id == id && a.IsDeleted == 0);
            if (announcement == null)
                return null;

            _mapper.Map(updateDto, announcement);
            announcement.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.Announcements.Update(announcement);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<AnnouncementDto>(announcement);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "更新公告失败，ID: {Id}", id);
            throw;
        }
    }

    public async Task<AnnouncementDto?> GetByIdAsync(uint id)
    {
        var announcement = await _unitOfWork.Announcements.FirstOrDefaultAsync(a => a.Id == id && a.IsDeleted == 0);
        return announcement == null ? null : _mapper.Map<AnnouncementDto>(announcement);
    }

    public async Task<PagedResult<AnnouncementDto>> GetPagedAsync(AnnouncementQueryDto queryDto)
    {
        var (items, totalCount) = await _unitOfWork.Announcements.GetPagedAnnouncementsAsync(
            queryDto.BusinessType,
            queryDto.NoticeType,
            queryDto.Province,
            queryDto.City,
            queryDto.District,
            queryDto.Keyword,
            queryDto.PageNumber,
            queryDto.PageSize);

        var dtos = _mapper.Map<List<AnnouncementDto>>(items);

        return new PagedResult<AnnouncementDto>
        {
            Items = dtos,
            TotalCount = totalCount,
            PageIndex = queryDto.PageNumber,
            PageSize = queryDto.PageSize
        };
    }

    public async Task<bool> DeleteAsync(uint id)
    {
        try
        {
            await _unitOfWork.Announcements.SoftDeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "删除公告失败，ID: {Id}", id);
            return false;
        }
    }

    public async Task IncrementViewCountAsync(uint id)
    {
        try
        {
            var announcement = await _unitOfWork.Announcements.FirstOrDefaultAsync(a => a.Id == id && a.IsDeleted == 0);
            if (announcement != null)
            {
                announcement.ViewCount++;
                _unitOfWork.Announcements.Update(announcement);
                await _unitOfWork.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "增加浏览次数失败，ID: {Id}", id);
        }
    }
}