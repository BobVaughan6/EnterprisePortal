using HailongConsulting.API.Common;
using HailongConsulting.API.Data;
using HailongConsulting.API.Models.DTOs;
using HailongConsulting.API.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace HailongConsulting.API.Services
{
    public class InfoPublishService : IInfoPublishService
    {
        private readonly ApplicationDbContext _context;
        private readonly IFileUploadService _fileUploadService;
        private readonly IWebHostEnvironment _environment;
        private readonly ILogger<InfoPublishService> _logger;

        public InfoPublishService(
            ApplicationDbContext context,
            IFileUploadService fileUploadService,
            IWebHostEnvironment environment,
            ILogger<InfoPublishService> logger)
        {
            _context = context;
            _fileUploadService = fileUploadService;
            _environment = environment;
            _logger = logger;
        }

        public async Task<InfoPublishDto> CreateAsync(CreateInfoPublishDto dto, List<IFormFile>? files)
        {
            var attachmentPaths = new List<string>();

            if (files != null && files.Any())
            {
                attachmentPaths = await _fileUploadService.UploadMultipleFilesAsync(files, dto.Category);
            }

            var attachmentJson = attachmentPaths.Any() ? JsonSerializer.Serialize(attachmentPaths) : null;

            switch (dto.Category.ToLower())
            {
                case "company":
                    var companyAnnouncement = new CompanyAnnouncement
                    {
                        Title = dto.Title,
                        Content = dto.Content,
                        PublishTime = dto.PublishTime ?? DateTime.Now,
                        IsTop = dto.IsTop,
                        AttachmentPath = attachmentJson,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    };
                    _context.CompanyAnnouncements.Add(companyAnnouncement);
                    await _context.SaveChangesAsync();
                    return MapToDto(companyAnnouncement, "company");

                case "policy-regulation":
                    var policyRegulation = new PolicyRegulation
                    {
                        Title = dto.Title,
                        Content = dto.Content,
                        PublishTime = dto.PublishTime ?? DateTime.Now,
                        IsTop = dto.IsTop,
                        AttachmentPath = attachmentJson,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    };
                    _context.PolicyRegulations.Add(policyRegulation);
                    await _context.SaveChangesAsync();
                    return MapToDto(policyRegulation, "policy-regulation");

                case "policy-information":
                    var policyInformation = new PolicyInformation
                    {
                        Title = dto.Title,
                        Content = dto.Content,
                        PublishTime = dto.PublishTime ?? DateTime.Now,
                        IsTop = dto.IsTop,
                        AttachmentPath = attachmentJson,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    };
                    _context.PolicyInformation.Add(policyInformation);
                    await _context.SaveChangesAsync();
                    return MapToDto(policyInformation, "policy-information");

                case "notice":
                    var noticeAnnouncement = new NoticeAnnouncement
                    {
                        Title = dto.Title,
                        Content = dto.Content,
                        PublishTime = dto.PublishTime ?? DateTime.Now,
                        IsTop = dto.IsTop,
                        AttachmentPath = attachmentJson,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    };
                    _context.NoticeAnnouncements.Add(noticeAnnouncement);
                    await _context.SaveChangesAsync();
                    return MapToDto(noticeAnnouncement, "notice");

                default:
                    throw new ArgumentException($"不支持的分类: {dto.Category}");
            }
        }

        public async Task<PagedResult<InfoPublishDto>> GetPagedAsync(string category, string? keyword, int pageIndex, int pageSize)
        {
            IQueryable<object> query = category.ToLower() switch
            {
                "company" => _context.CompanyAnnouncements.Where(x => !x.IsDeleted),
                "policy-regulation" => _context.PolicyRegulations.Where(x => !x.IsDeleted),
                "policy-information" => _context.PolicyInformation.Where(x => !x.IsDeleted),
                "notice" => _context.NoticeAnnouncements.Where(x => !x.IsDeleted),
                _ => throw new ArgumentException($"不支持的分类: {category}")
            };

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query = category.ToLower() switch
                {
                    "company" => ((IQueryable<CompanyAnnouncement>)query).Where(x => x.Title.Contains(keyword) || x.Content.Contains(keyword)),
                    "policy-regulation" => ((IQueryable<PolicyRegulation>)query).Where(x => x.Title.Contains(keyword) || x.Content.Contains(keyword)),
                    "policy-information" => ((IQueryable<PolicyInformation>)query).Where(x => x.Title.Contains(keyword) || x.Content.Contains(keyword)),
                    "notice" => ((IQueryable<NoticeAnnouncement>)query).Where(x => x.Title.Contains(keyword) || x.Content.Contains(keyword)),
                    _ => query
                };
            }

            var totalCount = await query.CountAsync();

            var items = category.ToLower() switch
            {
                "company" => await ((IQueryable<CompanyAnnouncement>)query)
                    .OrderByDescending(x => x.IsTop)
                    .ThenByDescending(x => x.PublishTime)
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .Select(x => MapToDto(x, category))
                    .ToListAsync(),
                "policy-regulation" => await ((IQueryable<PolicyRegulation>)query)
                    .OrderByDescending(x => x.IsTop)
                    .ThenByDescending(x => x.PublishTime)
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .Select(x => MapToDto(x, category))
                    .ToListAsync(),
                "policy-information" => await ((IQueryable<PolicyInformation>)query)
                    .OrderByDescending(x => x.IsTop)
                    .ThenByDescending(x => x.PublishTime)
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .Select(x => MapToDto(x, category))
                    .ToListAsync(),
                "notice" => await ((IQueryable<NoticeAnnouncement>)query)
                    .OrderByDescending(x => x.IsTop)
                    .ThenByDescending(x => x.PublishTime)
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .Select(x => MapToDto(x, category))
                    .ToListAsync(),
                _ => new List<InfoPublishDto>()
            };

            return new PagedResult<InfoPublishDto>
            {
                Items = items,
                TotalCount = totalCount,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
        }

        public async Task<InfoPublishDto?> GetByIdAsync(string category, int id)
        {
            object? entity = category.ToLower() switch
            {
                "company" => await _context.CompanyAnnouncements.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted),
                "policy-regulation" => await _context.PolicyRegulations.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted),
                "policy-information" => await _context.PolicyInformation.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted),
                "notice" => await _context.NoticeAnnouncements.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted),
                _ => null
            };

            if (entity == null) return null;

            // 增加访问量
            switch (category.ToLower())
            {
                case "company":
                    ((CompanyAnnouncement)entity).ViewCount++;
                    break;
                case "policy-regulation":
                    ((PolicyRegulation)entity).ViewCount++;
                    break;
                case "policy-information":
                    ((PolicyInformation)entity).ViewCount++;
                    break;
                case "notice":
                    ((NoticeAnnouncement)entity).ViewCount++;
                    break;
            }

            await _context.SaveChangesAsync();

            return MapToDto(entity, category);
        }

        public async Task<InfoPublishDto> UpdateAsync(string category, int id, UpdateInfoPublishDto dto, List<IFormFile>? files)
        {
            object? entity = category.ToLower() switch
            {
                "company" => await _context.CompanyAnnouncements.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted),
                "policy-regulation" => await _context.PolicyRegulations.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted),
                "policy-information" => await _context.PolicyInformation.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted),
                "notice" => await _context.NoticeAnnouncements.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted),
                _ => null
            };

            if (entity == null)
                throw new KeyNotFoundException($"未找到ID为 {id} 的记录");

            var attachmentPaths = new List<string>();
            if (files != null && files.Any())
            {
                attachmentPaths = await _fileUploadService.UploadMultipleFilesAsync(files, category);
            }

            var attachmentJson = attachmentPaths.Any() ? JsonSerializer.Serialize(attachmentPaths) : null;

            switch (category.ToLower())
            {
                case "company":
                    var ca = (CompanyAnnouncement)entity;
                    ca.Title = dto.Title;
                    ca.Content = dto.Content;
                    ca.PublishTime = dto.PublishTime;
                    ca.IsTop = dto.IsTop;
                    if (attachmentJson != null) ca.AttachmentPath = attachmentJson;
                    ca.UpdatedAt = DateTime.Now;
                    break;
                case "policy-regulation":
                    var pr = (PolicyRegulation)entity;
                    pr.Title = dto.Title;
                    pr.Content = dto.Content;
                    pr.PublishTime = dto.PublishTime;
                    pr.IsTop = dto.IsTop;
                    if (attachmentJson != null) pr.AttachmentPath = attachmentJson;
                    pr.UpdatedAt = DateTime.Now;
                    break;
                case "policy-information":
                    var pi = (PolicyInformation)entity;
                    pi.Title = dto.Title;
                    pi.Content = dto.Content;
                    pi.PublishTime = dto.PublishTime;
                    pi.IsTop = dto.IsTop;
                    if (attachmentJson != null) pi.AttachmentPath = attachmentJson;
                    pi.UpdatedAt = DateTime.Now;
                    break;
                case "notice":
                    var na = (NoticeAnnouncement)entity;
                    na.Title = dto.Title;
                    na.Content = dto.Content;
                    na.PublishTime = dto.PublishTime;
                    na.IsTop = dto.IsTop;
                    if (attachmentJson != null) na.AttachmentPath = attachmentJson;
                    na.UpdatedAt = DateTime.Now;
                    break;
            }

            await _context.SaveChangesAsync();
            return MapToDto(entity, category);
        }

        public async Task<bool> DeleteAsync(string category, int id)
        {
            object? entity = category.ToLower() switch
            {
                "company" => await _context.CompanyAnnouncements.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted),
                "policy-regulation" => await _context.PolicyRegulations.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted),
                "policy-information" => await _context.PolicyInformation.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted),
                "notice" => await _context.NoticeAnnouncements.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted),
                _ => null
            };

            if (entity == null) return false;

            switch (category.ToLower())
            {
                case "company":
                    ((CompanyAnnouncement)entity).IsDeleted = true;
                    break;
                case "policy-regulation":
                    ((PolicyRegulation)entity).IsDeleted = true;
                    break;
                case "policy-information":
                    ((PolicyInformation)entity).IsDeleted = true;
                    break;
                case "notice":
                    ((NoticeAnnouncement)entity).IsDeleted = true;
                    break;
            }

            await _context.SaveChangesAsync();
            return true;
        }

        private InfoPublishDto MapToDto(object entity, string category)
        {
            return entity switch
            {
                CompanyAnnouncement ca => new InfoPublishDto
                {
                    Id = ca.Id,
                    Category = category,
                    Title = ca.Title,
                    Content = ca.Content,
                    PublishTime = ca.PublishTime,
                    ViewCount = ca.ViewCount,
                    Attachments = ParseAttachments(ca.AttachmentPath),
                    IsTop = ca.IsTop,
                    CreatedAt = ca.CreatedAt,
                    UpdatedAt = ca.UpdatedAt
                },
                PolicyRegulation pr => new InfoPublishDto
                {
                    Id = pr.Id,
                    Category = category,
                    Title = pr.Title,
                    Content = pr.Content,
                    PublishTime = pr.PublishTime,
                    ViewCount = pr.ViewCount,
                    Attachments = ParseAttachments(pr.AttachmentPath),
                    IsTop = pr.IsTop,
                    CreatedAt = pr.CreatedAt,
                    UpdatedAt = pr.UpdatedAt
                },
                PolicyInformation pi => new InfoPublishDto
                {
                    Id = pi.Id,
                    Category = category,
                    Title = pi.Title,
                    Content = pi.Content,
                    PublishTime = pi.PublishTime,
                    ViewCount = pi.ViewCount,
                    Attachments = ParseAttachments(pi.AttachmentPath),
                    IsTop = pi.IsTop,
                    CreatedAt = pi.CreatedAt,
                    UpdatedAt = pi.UpdatedAt
                },
                NoticeAnnouncement na => new InfoPublishDto
                {
                    Id = na.Id,
                    Category = category,
                    Title = na.Title,
                    Content = na.Content,
                    PublishTime = na.PublishTime,
                    ViewCount = na.ViewCount,
                    Attachments = ParseAttachments(na.AttachmentPath),
                    IsTop = na.IsTop,
                    CreatedAt = na.CreatedAt,
                    UpdatedAt = na.UpdatedAt
                },
                _ => throw new ArgumentException("不支持的实体类型")
            };
        }

        private List<string>? ParseAttachments(string? attachmentPath)
        {
            if (string.IsNullOrWhiteSpace(attachmentPath))
                return null;

            try
            {
                return JsonSerializer.Deserialize<List<string>>(attachmentPath);
            }
            catch
            {
                return null;
            }
        }
    }
}