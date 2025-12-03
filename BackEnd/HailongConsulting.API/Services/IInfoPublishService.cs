using HailongConsulting.API.Common;
using HailongConsulting.API.Models.DTOs;

namespace HailongConsulting.API.Services
{
    public interface IInfoPublishService
    {
        Task<InfoPublishDto> CreateAsync(CreateInfoPublishDto dto, List<IFormFile>? files);
        Task<PagedResult<InfoPublishDto>> GetPagedAsync(string category, string? keyword, int pageIndex, int pageSize);
        Task<InfoPublishDto?> GetByIdAsync(string category, int id);
        Task<InfoPublishDto> UpdateAsync(string category, int id, UpdateInfoPublishDto dto, List<IFormFile>? files);
        Task<bool> DeleteAsync(string category, int id);
    }
}