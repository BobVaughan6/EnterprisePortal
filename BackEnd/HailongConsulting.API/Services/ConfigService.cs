using AutoMapper;
using HailongConsulting.API.Models.DTOs;
using HailongConsulting.API.Models.Entities;
using HailongConsulting.API.Repositories;

namespace HailongConsulting.API.Services;

/// <summary>
/// 系统配置服务实现
/// </summary>
public class ConfigService : IConfigService
{
    private readonly IConfigRepository _repository;
    private readonly IMapper _mapper;

    public ConfigService(IConfigRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    #region 轮播图

    public async Task<IEnumerable<CarouselBannerDto>> GetAllBannersAsync()
    {
        var banners = await _repository.GetAllBannersAsync();
        return _mapper.Map<IEnumerable<CarouselBannerDto>>(banners);
    }

    public async Task<CarouselBannerDto?> GetBannerByIdAsync(uint id)
    {
        var banner = await _repository.GetBannerByIdAsync(id);
        return banner == null ? null : _mapper.Map<CarouselBannerDto>(banner);
    }

    public async Task<CarouselBannerDto> CreateBannerAsync(CreateCarouselBannerDto dto)
    {
        var banner = _mapper.Map<CarouselBanner>(dto);
        var created = await _repository.AddBannerAsync(banner);
        return _mapper.Map<CarouselBannerDto>(created);
    }

    public async Task<bool> UpdateBannerAsync(uint id, UpdateCarouselBannerDto dto)
    {
        var banner = await _repository.GetBannerByIdAsync(id);
        if (banner == null) return false;

        // 只更新非null的字段
        if (dto.Title != null) banner.Title = dto.Title;
        if (dto.Description != null) banner.Description = dto.Description;
        if (dto.ImageId.HasValue) banner.ImageId = dto.ImageId.Value;
        if (dto.LinkUrl != null) banner.LinkUrl = dto.LinkUrl;
        if (dto.SortOrder.HasValue) banner.SortOrder = dto.SortOrder.Value;
        if (dto.Status.HasValue) banner.Status = (sbyte)(dto.Status.Value ? 1 : 0);

        return await _repository.UpdateBannerAsync(banner);
    }

    public async Task<bool> DeleteBannerAsync(uint id)
    {
        return await _repository.DeleteBannerAsync(id);
    }

    #endregion

    #region 企业简介

    public async Task<CompanyProfileDto?> GetCompanyProfileAsync()
    {
        var profile = await _repository.GetCompanyProfileAsync();
        return profile == null ? null : _mapper.Map<CompanyProfileDto>(profile);
    }

    public async Task<bool> UpdateCompanyProfileAsync(UpdateCompanyProfileDto dto)
    {
        var profile = _mapper.Map<CompanyProfile>(dto);
        return await _repository.UpdateCompanyProfileAsync(profile);
    }

    #endregion

    #region 重要业绩

    public async Task<IEnumerable<MajorAchievementDto>> GetAllAchievementsAsync()
    {
        var achievements = await _repository.GetAllAchievementsAsync();
        return _mapper.Map<IEnumerable<MajorAchievementDto>>(achievements);
    }

    public async Task<MajorAchievementDto?> GetAchievementByIdAsync(uint id)
    {
        var achievement = await _repository.GetAchievementByIdAsync(id);
        return achievement == null ? null : _mapper.Map<MajorAchievementDto>(achievement);
    }

    public async Task<MajorAchievementDto> CreateAchievementAsync(CreateMajorAchievementDto dto)
    {
        var achievement = _mapper.Map<MajorAchievement>(dto);
        var created = await _repository.AddAchievementAsync(achievement);
        return _mapper.Map<MajorAchievementDto>(created);
    }

    public async Task<bool> UpdateAchievementAsync(uint id, UpdateMajorAchievementDto dto)
    {
        var achievement = await _repository.GetAchievementByIdAsync(id);
        if (achievement == null) return false;

        // 只更新非null的字段
        if (dto.ProjectName != null) achievement.ProjectName = dto.ProjectName;
        if (dto.ProjectType != null) achievement.ProjectType = dto.ProjectType;
        if (dto.ProjectAmount.HasValue) achievement.ProjectAmount = dto.ProjectAmount;
        if (dto.ClientName != null) achievement.ClientName = dto.ClientName;
        if (dto.CompletionDate.HasValue) achievement.CompletionDate = dto.CompletionDate;
        if (dto.Description != null) achievement.Description = dto.Description;
        if (dto.ImageIds != null) achievement.ImageIds = System.Text.Json.JsonSerializer.Serialize(dto.ImageIds);
        if (dto.SortOrder.HasValue) achievement.SortOrder = dto.SortOrder.Value;

        return await _repository.UpdateAchievementAsync(achievement);
    }

    public async Task<bool> DeleteAchievementAsync(uint id)
    {
        return await _repository.DeleteAchievementAsync(id);
    }

    #endregion

    #region 友情链接

    public async Task<IEnumerable<FriendlyLinkDto>> GetAllLinksAsync()
    {
        var links = await _repository.GetAllLinksAsync();
        return _mapper.Map<IEnumerable<FriendlyLinkDto>>(links);
    }

    public async Task<FriendlyLinkDto?> GetLinkByIdAsync(uint id)
    {
        var link = await _repository.GetLinkByIdAsync(id);
        return link == null ? null : _mapper.Map<FriendlyLinkDto>(link);
    }

    public async Task<FriendlyLinkDto> CreateLinkAsync(CreateFriendlyLinkDto dto)
    {
        var link = _mapper.Map<FriendlyLink>(dto);
        var created = await _repository.AddLinkAsync(link);
        return _mapper.Map<FriendlyLinkDto>(created);
    }

    public async Task<bool> UpdateLinkAsync(uint id, UpdateFriendlyLinkDto dto)
    {
        var link = await _repository.GetLinkByIdAsync(id);
        if (link == null) return false;

        // 只更新非null的字段
        if (dto.Name != null) link.Name = dto.Name;
        if (dto.Url != null) link.Url = dto.Url;
        if (dto.LogoId.HasValue) link.LogoId = dto.LogoId;
        if (dto.Description != null) link.Description = dto.Description;
        if (dto.SortOrder.HasValue) link.SortOrder = dto.SortOrder.Value;
        if (dto.Status.HasValue) link.Status = (sbyte)(dto.Status.Value ? 1 : 0);

        return await _repository.UpdateLinkAsync(link);
    }

    public async Task<bool> DeleteLinkAsync(uint id)
    {
        return await _repository.DeleteLinkAsync(id);
    }

    #endregion

    #region 访问统计

    public async Task<VisitStatisticDto> GetVisitStatisticsAsync()
    {
        var today = DateOnly.FromDateTime(DateTime.Today);
        var yesterday = today.AddDays(-1);
        var firstDayOfMonth = new DateOnly(today.Year, today.Month, 1);

        var totalVisits = await _repository.GetTotalVisitsAsync();
        var todayVisits = await _repository.GetVisitsByDateAsync(today);
        var yesterdayVisits = await _repository.GetVisitsByDateAsync(yesterday);
        var thisMonthVisits = await _repository.GetVisitsByDateRangeAsync(firstDayOfMonth, today);

        return new VisitStatisticDto
        {
            TotalVisits = totalVisits,
            TodayVisits = todayVisits,
            YesterdayVisits = yesterdayVisits,
            ThisMonthVisits = thisMonthVisits
        };
    }

    public async Task RecordVisitAsync(RecordVisitDto dto, string? ipAddress, string? userAgent)
    {
        var statistic = new VisitStatistic
        {
            VisitDate = DateOnly.FromDateTime(DateTime.Today),
            PageUrl = dto.PageUrl,
            PageTitle = dto.PageTitle,
            VisitorIp = ipAddress,
            UserAgent = userAgent,
            Referer = dto.Referer,
            VisitCount = 1
        };

        await _repository.RecordVisitAsync(statistic);
    }

    #endregion
}