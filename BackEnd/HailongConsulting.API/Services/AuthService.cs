using HailongConsulting.API.Common.Helpers;
using HailongConsulting.API.Models.DTOs;
using HailongConsulting.API.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HailongConsulting.API.Services;

/// <summary>
/// 认证服务实现
/// </summary>
public class AuthService : IAuthService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly JwtHelper _jwtHelper;
    private readonly IConfiguration _configuration;

    public AuthService(IUnitOfWork unitOfWork, JwtHelper jwtHelper, IConfiguration configuration)
    {
        _unitOfWork = unitOfWork;
        _jwtHelper = jwtHelper;
        _configuration = configuration;
    }

    public async Task<LoginResponseDto?> LoginAsync(LoginRequestDto request)
    {
        // 查找用户
        var user = await _unitOfWork.Users.FirstOrDefaultAsync(u => u.Username == request.Username);
        
        if (user == null || user.Status == 0)
        {
            return null;
        }

        // 验证密码（使用MD5）
        if (!PasswordHelper.VerifyPassword(request.Password, user.Password))
        {
            return null;
        }

        // 生成Token和RefreshToken
        var token = _jwtHelper.GenerateToken((int)user.Id, user.Username, user.Role);
        var refreshToken = _jwtHelper.GenerateRefreshToken();
        var expireHours = Convert.ToDouble(_configuration["Jwt:ExpireHours"]);

        // 保存RefreshToken
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7); // RefreshToken有效期7天
        user.LastLoginTime = DateTime.Now;
        user.UpdatedAt = DateTime.Now;
        
        _unitOfWork.Users.Update(user);
        await _unitOfWork.SaveChangesAsync();

        return new LoginResponseDto
        {
            UserId = (int)user.Id,
            Username = user.Username,
            FullName = user.RealName,
            Email = user.Email,
            Role = user.Role,
            Token = token,
            RefreshToken = refreshToken,
            ExpiresAt = DateTime.UtcNow.AddHours(expireHours)
        };
    }

    public async Task<LoginResponseDto?> RefreshTokenAsync(string refreshToken)
    {
        // 查找拥有该RefreshToken的用户
        var user = await _unitOfWork.Users.FirstOrDefaultAsync(
            u => u.RefreshToken == refreshToken && u.Status == 1);

        if (user == null || user.RefreshTokenExpiry == null || user.RefreshTokenExpiry < DateTime.UtcNow)
        {
            return null;
        }

        // 生成新的Token和RefreshToken
        var newToken = _jwtHelper.GenerateToken((int)user.Id, user.Username, user.Role);
        var newRefreshToken = _jwtHelper.GenerateRefreshToken();
        var expireHours = Convert.ToDouble(_configuration["Jwt:ExpireHours"]);

        // 更新RefreshToken
        user.RefreshToken = newRefreshToken;
        user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);
        user.UpdatedAt = DateTime.Now;
        
        _unitOfWork.Users.Update(user);
        await _unitOfWork.SaveChangesAsync();

        return new LoginResponseDto
        {
            UserId = (int)user.Id,
            Username = user.Username,
            FullName = user.RealName,
            Email = user.Email,
            Role = user.Role,
            Token = newToken,
            RefreshToken = newRefreshToken,
            ExpiresAt = DateTime.UtcNow.AddHours(expireHours)
        };
    }

    public async Task<UserInfoDto?> GetCurrentUserAsync(int userId)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(userId);
        
        if (user == null || user.Status == 0)
        {
            return null;
        }

        return new UserInfoDto
        {
            UserId = (int)user.Id,
            Username = user.Username,
            FullName = user.RealName,
            Email = user.Email,
            Phone = user.Phone,
            Role = user.Role,
            LastLogin = user.LastLoginTime
        };
    }

    public async Task<bool> ChangePasswordAsync(int userId, ChangePasswordRequestDto request)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(userId);
        
        if (user == null || user.Status == 0)
        {
            return false;
        }

        // 验证旧密码
        if (!PasswordHelper.VerifyPassword(request.OldPassword, user.Password))
        {
            return false;
        }

        // 更新密码（使用MD5）
        user.Password = PasswordHelper.HashPassword(request.NewPassword);
        user.UpdatedAt = DateTime.Now;
        
        // 出于安全考虑，修改密码后撤销RefreshToken
        user.RefreshToken = null;
        user.RefreshTokenExpiry = null;
        
        _unitOfWork.Users.Update(user);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }

    public async Task<bool> RevokeRefreshTokenAsync(int userId)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(userId);
        
        if (user == null)
        {
            return false;
        }

        user.RefreshToken = null;
        user.RefreshTokenExpiry = null;
        user.UpdatedAt = DateTime.Now;
        
        _unitOfWork.Users.Update(user);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }

    public async Task<bool> ValidateTokenAsync(string token)
    {
        var principal = _jwtHelper.ValidateToken(token);
        return await Task.FromResult(principal != null);
    }

    // 保留旧方法以兼容现有代码
    public async Task<LoginResponseDto?> LoginAsync(LoginDto loginDto)
    {
        return await LoginAsync(new LoginRequestDto
        {
            Username = loginDto.Username,
            Password = loginDto.Password
        });
    }

    public async Task<bool> ChangePasswordAsync(int userId, string oldPassword, string newPassword)
    {
        return await ChangePasswordAsync(userId, new ChangePasswordRequestDto
        {
            OldPassword = oldPassword,
            NewPassword = newPassword
        });
    }
}