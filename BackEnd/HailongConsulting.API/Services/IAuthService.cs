using HailongConsulting.API.Models.DTOs;

namespace HailongConsulting.API.Services;

/// <summary>
/// 认证服务接口
/// </summary>
public interface IAuthService
{
    /// <summary>
    /// 用户登录
    /// </summary>
    Task<LoginResponseDto?> LoginAsync(LoginRequestDto request);

    /// <summary>
    /// 刷新Token
    /// </summary>
    Task<LoginResponseDto?> RefreshTokenAsync(string refreshToken);

    /// <summary>
    /// 获取当前用户信息
    /// </summary>
    Task<UserInfoDto?> GetCurrentUserAsync(int userId);

    /// <summary>
    /// 修改密码
    /// </summary>
    Task<bool> ChangePasswordAsync(int userId, ChangePasswordRequestDto request);

    /// <summary>
    /// 撤销刷新令牌（登出）
    /// </summary>
    Task<bool> RevokeRefreshTokenAsync(int userId);

    /// <summary>
    /// 验证Token
    /// </summary>
    Task<bool> ValidateTokenAsync(string token);
    
    // 保留旧方法以兼容现有代码
    Task<LoginResponseDto?> LoginAsync(LoginDto loginDto);
    Task<bool> ChangePasswordAsync(int userId, string oldPassword, string newPassword);
}