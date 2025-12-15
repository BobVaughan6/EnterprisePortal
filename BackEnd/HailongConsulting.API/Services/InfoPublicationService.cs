using AutoMapper;
using HailongConsulting.API.Common;
using HailongConsulting.API.Models.DTOs;
using HailongConsulting.API.Models.Entities;
using HailongConsulting.API.Repositories;

namespace HailongConsulting.API.Services;

/// <summary>
/// 信息发布服务实现
/// </summary>
public class InfoPublicationService : IInfoPublicationService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<InfoPublicationService> _logger;

    public InfoPublicationService(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ILogger<InfoPublicationService> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<InfoPublicationDto> CreateAsync(CreateInfoPublicationDto createDto)
    {
        try
        {
            var publication = _mapper.Map<InfoPublication>(createDto);
            publication.CreatedAt = DateTime.UtcNow;
            publication.UpdatedAt = DateTime.UtcNow;
            publication.ViewCount = 0;

            await _unitOfWork.InfoPublications.AddAsync(publication);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<InfoPublicationDto>(publication);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "创建信息发布失败");
            throw;
        }
    }

    public async Task<InfoPublicationDto?> UpdateAsync(int id, UpdateInfoPublicationDto updateDto)
    {
        try
        {
            var publication = await _unitOfWork.InfoPublications.FirstOrDefaultAsync(p => p.Id == id && p.IsDeleted == 0);
            if (publication == null)
                return null;

            _mapper.Map(updateDto, publication);
            publication.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.InfoPublications.Update(publication);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<InfoPublicationDto>(publication);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "更新信息发布失败，ID: {Id}", id);
            throw;
        }
    }

    public async Task<InfoPublicationDto?> GetByIdAsync(int id)
    {
        var publication = await _unitOfWork.InfoPublications.FirstOrDefaultAsync(p => p.Id == id && p.IsDeleted == 0);
        return publication == null ? null : _mapper.Map<InfoPublicationDto>(publication);
    }

    public async Task<PagedResult<InfoPublicationDto>> GetPagedAsync(InfoPublicationQueryDto queryDto)
    {
        var (items, totalCount) = await _unitOfWork.InfoPublications.GetPagedPublicationsAsync(
            queryDto.Type,
            queryDto.Category,
            queryDto.Keyword,
            queryDto.PageNumber,
            queryDto.PageSize);

        var dtos = _mapper.Map<List<InfoPublicationDto>>(items);

        return new PagedResult<InfoPublicationDto>
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
            await _unitOfWork.InfoPublications.SoftDeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "删除信息发布失败，ID: {Id}", id);
            return false;
        }
    }

    public async Task IncrementViewCountAsync(int id)
    {
        try
        {
            var publication = await _unitOfWork.InfoPublications.FirstOrDefaultAsync(p => p.Id == id && p.IsDeleted == 0);
            if (publication != null)
            {
                publication.ViewCount++;
                _unitOfWork.InfoPublications.Update(publication);
                await _unitOfWork.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "增加浏览次数失败，ID: {Id}", id);
        }
    }

    public async Task<PagedResult<InfoPublicationDto>> GetPagedForPortalAsync(InfoPublicationQueryDto queryDto)
    {
        var (items, totalCount) = await _unitOfWork.InfoPublications.GetPagedPublicationsForPortalAsync(
            queryDto.Type,
            queryDto.Category,
            queryDto.Keyword,
            queryDto.PageNumber,
            queryDto.PageSize);

        var dtos = _mapper.Map<List<InfoPublicationDto>>(items);

        return new PagedResult<InfoPublicationDto>
        {
            Items = dtos,
            TotalCount = totalCount,
            PageIndex = queryDto.PageNumber,
            PageSize = queryDto.PageSize
        };
    }
}