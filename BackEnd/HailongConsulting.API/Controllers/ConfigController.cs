using HailongConsulting.API.Common;
using HailongConsulting.API.Models.DTOs;
using HailongConsulting.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HailongConsulting.API.Controllers;

/// <summary>
/// 系统配置管理控制器
/// </summary>
[ApiController]
[Route("api/config")]
public class ConfigController : ControllerBase
{
    private readonly IConfigService _configService;
    private readonly ILogger<ConfigController> _logger;

    public ConfigController(IConfigService configService, ILogger<ConfigController> logger)
    {
        _configService = configService;
        _logger = logger;
    }

    #region 轮播图管理

    /// <summary>
    /// 获取所有轮播图
    /// </summary>
    [HttpGet("banners")]
    public async Task<ActionResult<ApiResponse<IEnumerable<CarouselBannerDto>>>> GetBanners()
    {
        try
        {
            var banners = await _configService.GetAllBannersAsync();
            return Ok(ApiResponse<IEnumerable<CarouselBannerDto>>.SuccessResult(banners, "获取轮播图列表成功"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "获取轮播图列表失败");
            return StatusCode(500, ApiResponse<IEnumerable<CarouselBannerDto>>.FailResult("获取轮播图列表失败"));
        }
    }

    /// <summary>
    /// 根据ID获取轮播图
    /// </summary>
    [HttpGet("banners/{id}")]
    public async Task<ActionResult<ApiResponse<CarouselBannerDto>>> GetBanner(uint id)
    {
        try
        {
            var banner = await _configService.GetBannerByIdAsync(id);
            if (banner == null)
            {
                return NotFound(ApiResponse<CarouselBannerDto>.FailResult("轮播图不存在"));
            }
            return Ok(ApiResponse<CarouselBannerDto>.SuccessResult(banner, "获取轮播图成功"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "获取轮播图失败，ID: {Id}", id);
            return StatusCode(500, ApiResponse<CarouselBannerDto>.FailResult("获取轮播图失败"));
        }
    }

    /// <summary>
    /// 创建轮播图
    /// </summary>
    [HttpPost("banners")]
    [Authorize]
    public async Task<ActionResult<ApiResponse<CarouselBannerDto>>> CreateBanner([FromBody] CreateCarouselBannerDto dto)
    {
        try
        {
            var banner = await _configService.CreateBannerAsync(dto);
            return Ok(ApiResponse<CarouselBannerDto>.SuccessResult(banner, "创建轮播图成功"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "创建轮播图失败");
            return StatusCode(500, ApiResponse<CarouselBannerDto>.FailResult("创建轮播图失败"));
        }
    }

    /// <summary>
    /// 更新轮播图
    /// </summary>
    [HttpPut("banners/{id}")]
    [Authorize]
    public async Task<ActionResult<ApiResponse<bool>>> UpdateBanner(uint id, [FromBody] UpdateCarouselBannerDto dto)
    {
        try
        {
            var result = await _configService.UpdateBannerAsync(id, dto);
            if (!result)
            {
                return NotFound(ApiResponse<bool>.FailResult("轮播图不存在"));
            }
            return Ok(ApiResponse<bool>.SuccessResult(true, "更新轮播图成功"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "更新轮播图失败，ID: {Id}", id);
            return StatusCode(500, ApiResponse<bool>.FailResult("更新轮播图失败"));
        }
    }

    /// <summary>
    /// 删除轮播图
    /// </summary>
    [HttpDelete("banners/{id}")]
    [Authorize]
    public async Task<ActionResult<ApiResponse<bool>>> DeleteBanner(uint id)
    {
        try
        {
            var result = await _configService.DeleteBannerAsync(id);
            if (!result)
            {
                return NotFound(ApiResponse<bool>.FailResult("轮播图不存在"));
            }
            return Ok(ApiResponse<bool>.SuccessResult(true, "删除轮播图成功"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "删除轮播图失败，ID: {Id}", id);
            return StatusCode(500, ApiResponse<bool>.FailResult("删除轮播图失败"));
        }
    }

    #endregion

    #region 企业简介管理

    /// <summary>
    /// 获取企业简介
    /// </summary>
    [HttpGet("company-intro")]
    public async Task<ActionResult<ApiResponse<CompanyProfileDto>>> GetCompanyProfile()
    {
        try
        {
            var profile = await _configService.GetCompanyProfileAsync();
            if (profile == null)
            {
                return NotFound(ApiResponse<CompanyProfileDto>.FailResult("企业简介不存在"));
            }
            return Ok(ApiResponse<CompanyProfileDto>.SuccessResult(profile, "获取企业简介成功"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "获取企业简介失败");
            return StatusCode(500, ApiResponse<CompanyProfileDto>.FailResult("获取企业简介失败"));
        }
    }

    /// <summary>
    /// 更新企业简介
    /// </summary>
    [HttpPut("company-intro")]
    [Authorize]
    public async Task<ActionResult<ApiResponse<bool>>> UpdateCompanyProfile([FromBody] UpdateCompanyProfileDto dto)
    {
        try
        {
            var result = await _configService.UpdateCompanyProfileAsync(dto);
            return Ok(ApiResponse<bool>.SuccessResult(result, "更新企业简介成功"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "更新企业简介失败");
            return StatusCode(500, ApiResponse<bool>.FailResult("更新企业简介失败"));
        }
    }

    #endregion

    #region 重要业绩管理

    /// <summary>
    /// 获取所有重要业绩
    /// </summary>
    [HttpGet("achievements")]
    public async Task<ActionResult<ApiResponse<IEnumerable<MajorAchievementDto>>>> GetAchievements()
    {
        try
        {
            var achievements = await _configService.GetAllAchievementsAsync();
            return Ok(ApiResponse<IEnumerable<MajorAchievementDto>>.SuccessResult(achievements, "获取重要业绩列表成功"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "获取重要业绩列表失败");
            return StatusCode(500, ApiResponse<IEnumerable<MajorAchievementDto>>.FailResult("获取重要业绩列表失败"));
        }
    }

    /// <summary>
    /// 根据ID获取重要业绩
    /// </summary>
    [HttpGet("achievements/{id}")]
    public async Task<ActionResult<ApiResponse<MajorAchievementDto>>> GetAchievement(uint id)
    {
        try
        {
            var achievement = await _configService.GetAchievementByIdAsync(id);
            if (achievement == null)
            {
                return NotFound(ApiResponse<MajorAchievementDto>.FailResult("重要业绩不存在"));
            }
            return Ok(ApiResponse<MajorAchievementDto>.SuccessResult(achievement, "获取重要业绩成功"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "获取重要业绩失败，ID: {Id}", id);
            return StatusCode(500, ApiResponse<MajorAchievementDto>.FailResult("获取重要业绩失败"));
        }
    }

    /// <summary>
    /// 创建重要业绩
    /// </summary>
    [HttpPost("achievements")]
    [Authorize]
    public async Task<ActionResult<ApiResponse<MajorAchievementDto>>> CreateAchievement([FromBody] CreateMajorAchievementDto dto)
    {
        try
        {
            var achievement = await _configService.CreateAchievementAsync(dto);
            return Ok(ApiResponse<MajorAchievementDto>.SuccessResult(achievement, "创建重要业绩成功"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "创建重要业绩失败");
            return StatusCode(500, ApiResponse<MajorAchievementDto>.FailResult("创建重要业绩失败"));
        }
    }

    /// <summary>
    /// 更新重要业绩
    /// </summary>
    [HttpPut("achievements/{id}")]
    [Authorize]
    public async Task<ActionResult<ApiResponse<bool>>> UpdateAchievement(uint id, [FromBody] UpdateMajorAchievementDto dto)
    {
        try
        {
            var result = await _configService.UpdateAchievementAsync(id, dto);
            if (!result)
            {
                return NotFound(ApiResponse<bool>.FailResult("重要业绩不存在"));
            }
            return Ok(ApiResponse<bool>.SuccessResult(true, "更新重要业绩成功"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "更新重要业绩失败，ID: {Id}", id);
            return StatusCode(500, ApiResponse<bool>.FailResult("更新重要业绩失败"));
        }
    }

    /// <summary>
    /// 删除重要业绩
    /// </summary>
    [HttpDelete("achievements/{id}")]
    [Authorize]
    public async Task<ActionResult<ApiResponse<bool>>> DeleteAchievement(uint id)
    {
        try
        {
            var result = await _configService.DeleteAchievementAsync(id);
            if (!result)
            {
                return NotFound(ApiResponse<bool>.FailResult("重要业绩不存在"));
            }
            return Ok(ApiResponse<bool>.SuccessResult(true, "删除重要业绩成功"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "删除重要业绩失败，ID: {Id}", id);
            return StatusCode(500, ApiResponse<bool>.FailResult("删除重要业绩失败"));
        }
    }

    #endregion

    #region 友情链接管理

    /// <summary>
    /// 获取所有友情链接
    /// </summary>
    [HttpGet("links")]
    public async Task<ActionResult<ApiResponse<IEnumerable<FriendlyLinkDto>>>> GetLinks()
    {
        try
        {
            var links = await _configService.GetAllLinksAsync();
            return Ok(ApiResponse<IEnumerable<FriendlyLinkDto>>.SuccessResult(links, "获取友情链接列表成功"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "获取友情链接列表失败");
            return StatusCode(500, ApiResponse<IEnumerable<FriendlyLinkDto>>.FailResult("获取友情链接列表失败"));
        }
    }

    /// <summary>
    /// 根据ID获取友情链接
    /// </summary>
    [HttpGet("links/{id}")]
    public async Task<ActionResult<ApiResponse<FriendlyLinkDto>>> GetLink(uint id)
    {
        try
        {
            var link = await _configService.GetLinkByIdAsync(id);
            if (link == null)
            {
                return NotFound(ApiResponse<FriendlyLinkDto>.FailResult("友情链接不存在"));
            }
            return Ok(ApiResponse<FriendlyLinkDto>.SuccessResult(link, "获取友情链接成功"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "获取友情链接失败，ID: {Id}", id);
            return StatusCode(500, ApiResponse<FriendlyLinkDto>.FailResult("获取友情链接失败"));
        }
    }

    /// <summary>
    /// 创建友情链接
    /// </summary>
    [HttpPost("links")]
    [Authorize]
    public async Task<ActionResult<ApiResponse<FriendlyLinkDto>>> CreateLink([FromBody] CreateFriendlyLinkDto dto)
    {
        try
        {
            var link = await _configService.CreateLinkAsync(dto);
            return Ok(ApiResponse<FriendlyLinkDto>.SuccessResult(link, "创建友情链接成功"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "创建友情链接失败");
            return StatusCode(500, ApiResponse<FriendlyLinkDto>.FailResult("创建友情链接失败"));
        }
    }

    /// <summary>
    /// 更新友情链接
    /// </summary>
    [HttpPut("links/{id}")]
    [Authorize]
    public async Task<ActionResult<ApiResponse<bool>>> UpdateLink(uint id, [FromBody] UpdateFriendlyLinkDto dto)
    {
        try
        {
            var result = await _configService.UpdateLinkAsync(id, dto);
            if (!result)
            {
                return NotFound(ApiResponse<bool>.FailResult("友情链接不存在"));
            }
            return Ok(ApiResponse<bool>.SuccessResult(true, "更新友情链接成功"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "更新友情链接失败，ID: {Id}", id);
            return StatusCode(500, ApiResponse<bool>.FailResult("更新友情链接失败"));
        }
    }

    /// <summary>
    /// 删除友情链接
    /// </summary>
    [HttpDelete("links/{id}")]
    [Authorize]
    public async Task<ActionResult<ApiResponse<bool>>> DeleteLink(uint id)
    {
        try
        {
            var result = await _configService.DeleteLinkAsync(id);
            if (!result)
            {
                return NotFound(ApiResponse<bool>.FailResult("友情链接不存在"));
            }
            return Ok(ApiResponse<bool>.SuccessResult(true, "删除友情链接成功"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "删除友情链接失败，ID: {Id}", id);
            return StatusCode(500, ApiResponse<bool>.FailResult("删除友情链接失败"));
        }
    }

    #endregion

    #region 访问统计

    /// <summary>
    /// 获取网站访问统计
    /// </summary>
    [HttpGet("statistics")]
    public async Task<ActionResult<ApiResponse<VisitStatisticDto>>> GetStatistics()
    {
        try
        {
            var statistics = await _configService.GetVisitStatisticsAsync();
            return Ok(ApiResponse<VisitStatisticDto>.SuccessResult(statistics, "获取访问统计成功"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "获取访问统计失败");
            return StatusCode(500, ApiResponse<VisitStatisticDto>.FailResult("获取访问统计失败"));
        }
    }

    /// <summary>
    /// 记录访问
    /// </summary>
    [HttpPost("statistics/record")]
    public async Task<ActionResult<ApiResponse<bool>>> RecordVisit([FromBody] RecordVisitDto dto)
    {
        try
        {
            var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
            var userAgent = HttpContext.Request.Headers["User-Agent"].ToString();
            
            await _configService.RecordVisitAsync(dto, ipAddress, userAgent);
            return Ok(ApiResponse<bool>.SuccessResult(true, "记录访问成功"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "记录访问失败");
            return StatusCode(500, ApiResponse<bool>.FailResult("记录访问失败"));
        }
    }

    #endregion
}