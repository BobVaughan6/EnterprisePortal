using HailongConsulting.API.Models.Entities;

namespace HailongConsulting.API.Repositories;

/// <summary>
/// 用户仓储接口
/// </summary>
public interface IUserRepository : IRepository<User>
{
    /// <summary>
    /// 根据用户名获取用户
    /// </summary>
    Task<User?> GetByUsernameAsync(string username);

    /// <summary>
    /// 检查用户名是否存在
    /// </summary>
    Task<bool> UsernameExistsAsync(string username, int? excludeUserId = null);

    /// <summary>
    /// 检查邮箱是否存在
    /// </summary>
    Task<bool> EmailExistsAsync(string email, int? excludeUserId = null);

    /// <summary>
    /// 获取用户列表（分页）
    /// </summary>
    Task<(List<User> Items, int Total)> GetPagedListAsync(
        string? username,
        string? realName,
        string? role,
        sbyte? status,
        int page,
        int pageSize);
}