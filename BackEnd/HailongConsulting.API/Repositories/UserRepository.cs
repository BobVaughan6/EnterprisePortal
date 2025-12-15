using HailongConsulting.API.Data;
using HailongConsulting.API.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace HailongConsulting.API.Repositories;

/// <summary>
/// 用户仓储实现
/// </summary>
public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext context) : base(context)
    {
    }

    /// <summary>
    /// 根据用户名获取用户
    /// </summary>
    public async Task<User?> GetByUsernameAsync(string username)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Username == username && u.IsDeleted == 0);
    }

    /// <summary>
    /// 检查用户名是否存在
    /// </summary>
    public async Task<bool> UsernameExistsAsync(string username, int? excludeUserId = null)
    {
        var query = _context.Users.Where(u => u.Username == username && u.IsDeleted == 0);
        
        if (excludeUserId.HasValue)
        {
            query = query.Where(u => u.Id != excludeUserId.Value);
        }
        
        return await query.AnyAsync();
    }

    /// <summary>
    /// 检查邮箱是否存在
    /// </summary>
    public async Task<bool> EmailExistsAsync(string email, int? excludeUserId = null)
    {
        var query = _context.Users.Where(u => u.Email == email && u.IsDeleted == 0);
        
        if (excludeUserId.HasValue)
        {
            query = query.Where(u => u.Id != excludeUserId.Value);
        }
        
        return await query.AnyAsync();
    }

    /// <summary>
    /// 获取用户列表（分页）
    /// </summary>
    public async Task<(List<User> Items, int Total)> GetPagedListAsync(
        string? username,
        string? realName,
        string? role,
        sbyte? status,
        int page,
        int pageSize)
    {
        var query = _context.Users.Where(u => u.IsDeleted == 0);

        // 应用筛选条件
        if (!string.IsNullOrWhiteSpace(username))
        {
            query = query.Where(u => u.Username.Contains(username));
        }

        if (!string.IsNullOrWhiteSpace(realName))
        {
            query = query.Where(u => u.RealName.Contains(realName));
        }

        if (!string.IsNullOrWhiteSpace(role))
        {
            query = query.Where(u => u.Role == role);
        }

        if (status.HasValue)
        {
            query = query.Where(u => u.Status == status.Value);
        }

        // 获取总数
        var total = await query.CountAsync();

        // 分页查询
        var items = await query
            .OrderByDescending(u => u.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (items, total);
    }
}