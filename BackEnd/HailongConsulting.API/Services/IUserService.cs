using HailongConsulting.API.Common;
using HailongConsulting.API.Models.DTOs;

namespace HailongConsulting.API.Services;

/// <summary>
/// 用户服务接口
/// </summary>
public interface IUserService
{
    /// <summary>
    /// 获取用户列表（分页）
    /// </summary>
    Task<PagedResult<UserDto>> GetPagedListAsync(UserQueryDto query);

    /// <summary>
    /// 根据ID获取用户
    /// </summary>
    Task<UserDto?> GetByIdAsync(int id);

    /// <summary>
    /// 创建用户
    /// </summary>
    Task<UserDto> CreateAsync(CreateUserDto dto);

    /// <summary>
    /// 更新用户
    /// </summary>
    Task<UserDto> UpdateAsync(int id, UpdateUserDto dto);

    /// <summary>
    /// 删除用户（软删除）
    /// </summary>
    Task<bool> DeleteAsync(int id);

    /// <summary>
    /// 重置用户密码
    /// </summary>
    Task<bool> ResetPasswordAsync(int id, ResetPasswordDto dto);

    /// <summary>
    /// 启用/禁用用户
    /// </summary>
    Task<bool> ToggleStatusAsync(int id);
}