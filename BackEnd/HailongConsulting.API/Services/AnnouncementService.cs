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

    public async Task<AnnouncementDto?> UpdateAsync(int id, UpdateAnnouncementDto updateDto)
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

    public async Task<AnnouncementDto?> GetByIdAsync(int id)
    {
        var announcement = await _unitOfWork.Announcements.FirstOrDefaultAsync(a => a.Id == id && a.IsDeleted == 0);
        if (announcement == null)
            return null;

        var dto = _mapper.Map<AnnouncementDto>(announcement);

        // 将区域编码转换为名称
        if (!string.IsNullOrEmpty(announcement.Province))
        {
            var province = await _unitOfWork.RegionDictionaries.GetByRegionCodeAsync(announcement.Province);
            dto.Province = province?.RegionName ?? announcement.Province;
        }

        if (!string.IsNullOrEmpty(announcement.City))
        {
            var city = await _unitOfWork.RegionDictionaries.GetByRegionCodeAsync(announcement.City);
            dto.City = city?.RegionName ?? announcement.City;
        }

        if (!string.IsNullOrEmpty(announcement.District))
        {
            var district = await _unitOfWork.RegionDictionaries.GetByRegionCodeAsync(announcement.District);
            dto.District = district?.RegionName ?? announcement.District;
        }

        return dto;
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
            queryDto.PageSize,
            queryDto.ProcurementType,
            queryDto.StartDate,
            queryDto.EndDate);

        var dtos = _mapper.Map<List<AnnouncementDto>>(items);

        // 批量转换区域编码为名称
        var regionCodes = new HashSet<string>();
        foreach (var item in items)
        {
            if (!string.IsNullOrEmpty(item.Province)) regionCodes.Add(item.Province);
            if (!string.IsNullOrEmpty(item.City)) regionCodes.Add(item.City);
            if (!string.IsNullOrEmpty(item.District)) regionCodes.Add(item.District);
        }

        // 一次性查询所有需要的区域信息
        var regions = new Dictionary<string, string>();
        foreach (var code in regionCodes)
        {
            var region = await _unitOfWork.RegionDictionaries.GetByRegionCodeAsync(code);
            if (region != null)
            {
                regions[code] = region.RegionName;
            }
        }

        // 转换每个DTO的区域编码为名称
        var itemsList = items.ToList();
        for (int i = 0; i < dtos.Count; i++)
        {
            if (!string.IsNullOrEmpty(itemsList[i].Province) && regions.ContainsKey(itemsList[i].Province))
            {
                dtos[i].Province = regions[itemsList[i].Province];
            }
            if (!string.IsNullOrEmpty(itemsList[i].City) && regions.ContainsKey(itemsList[i].City))
            {
                dtos[i].City = regions[itemsList[i].City];
            }
            if (!string.IsNullOrEmpty(itemsList[i].District) && regions.ContainsKey(itemsList[i].District))
            {
                dtos[i].District = regions[itemsList[i].District];
            }
        }

        return new PagedResult<AnnouncementDto>
        {
            Items = dtos,
            TotalCount = totalCount,
            PageIndex = queryDto.PageNumber,
            PageSize = queryDto.PageSize
        };
    }

    public async Task<bool> DeleteAsync(int id)
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

    public async Task IncrementViewCountAsync(int id)
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