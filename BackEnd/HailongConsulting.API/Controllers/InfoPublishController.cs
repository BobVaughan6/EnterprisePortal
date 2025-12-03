using HailongConsulting.API.Common;
using HailongConsulting.API.Models.DTOs;
using HailongConsulting.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace HailongConsulting.API.Controllers
{
    [ApiController]
    [Route("api/info-publish")]
    public class InfoPublishController : ControllerBase
    {
        private readonly IInfoPublishService _infoPublishService;
        private readonly ILogger<InfoPublishController> _logger;

        public InfoPublishController(
            IInfoPublishService infoPublishService,
            ILogger<InfoPublishController> logger)
        {
            _infoPublishService = infoPublishService;
            _logger = logger;
        }

        /// <summary>
        /// 创建信息
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<ApiResponse<InfoPublishDto>>> Create(
            [FromForm] CreateInfoPublishDto dto,
            [FromForm] List<IFormFile>? files)
        {
            try
            {
                var result = await _infoPublishService.CreateAsync(dto, files);
                return Ok(ApiResponse<InfoPublishDto>.SuccessResult(result, "创建成功"));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ApiResponse<InfoPublishDto>.FailResult(ex.Message));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "创建信息失败");
                return StatusCode(500, ApiResponse<InfoPublishDto>.FailResult("创建失败"));
            }
        }

        /// <summary>
        /// 分页查询信息
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<ApiResponse<PagedResult<InfoPublishDto>>>> GetPaged(
            [FromQuery] string category,
            [FromQuery] string? keyword,
            [FromQuery] int pageIndex = 1,
            [FromQuery] int pageSize = 10)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(category))
                {
                    return BadRequest(ApiResponse<PagedResult<InfoPublishDto>>.FailResult("分类参数不能为空"));
                }

                var result = await _infoPublishService.GetPagedAsync(category, keyword, pageIndex, pageSize);
                return Ok(ApiResponse<PagedResult<InfoPublishDto>>.SuccessResult(result));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ApiResponse<PagedResult<InfoPublishDto>>.FailResult(ex.Message));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "查询信息失败");
                return StatusCode(500, ApiResponse<PagedResult<InfoPublishDto>>.FailResult("查询失败"));
            }
        }

        /// <summary>
        /// 获取信息详情（访问量+1）
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<InfoPublishDto>>> GetById(
            [FromQuery] string category,
            int id)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(category))
                {
                    return BadRequest(ApiResponse<InfoPublishDto>.FailResult("分类参数不能为空"));
                }

                var result = await _infoPublishService.GetByIdAsync(category, id);
                if (result == null)
                {
                    return NotFound(ApiResponse<InfoPublishDto>.FailResult("未找到该信息"));
                }

                return Ok(ApiResponse<InfoPublishDto>.SuccessResult(result));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "获取信息详情失败");
                return StatusCode(500, ApiResponse<InfoPublishDto>.FailResult("获取失败"));
            }
        }

        /// <summary>
        /// 更新信息
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<InfoPublishDto>>> Update(
            int id,
            [FromForm] string category,
            [FromForm] UpdateInfoPublishDto dto,
            [FromForm] List<IFormFile>? files)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(category))
                {
                    return BadRequest(ApiResponse<InfoPublishDto>.FailResult("分类参数不能为空"));
                }

                var result = await _infoPublishService.UpdateAsync(category, id, dto, files);
                return Ok(ApiResponse<InfoPublishDto>.SuccessResult(result, "更新成功"));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ApiResponse<InfoPublishDto>.FailResult(ex.Message));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ApiResponse<InfoPublishDto>.FailResult(ex.Message));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "更新信息失败");
                return StatusCode(500, ApiResponse<InfoPublishDto>.FailResult("更新失败"));
            }
        }

        /// <summary>
        /// 删除信息（软删除）
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> Delete(
            [FromQuery] string category,
            int id)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(category))
                {
                    return BadRequest(ApiResponse<bool>.FailResult("分类参数不能为空"));
                }

                var result = await _infoPublishService.DeleteAsync(category, id);
                if (!result)
                {
                    return NotFound(ApiResponse<bool>.FailResult("未找到该信息"));
                }

                return Ok(ApiResponse<bool>.SuccessResult(true, "删除成功"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "删除信息失败");
                return StatusCode(500, ApiResponse<bool>.FailResult("删除失败"));
            }
        }
    }
}