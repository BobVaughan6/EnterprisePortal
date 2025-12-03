using System.Security.Cryptography;
using System.Text;

namespace HailongConsulting.API.Common.Helpers;

/// <summary>
/// 密码帮助类
/// </summary>
public static class PasswordHelper
{
    /// <summary>
    /// 生成密码哈希（使用MD5）
    /// </summary>
    public static string HashPassword(string password)
    {
        using var md5 = MD5.Create();
        var inputBytes = Encoding.UTF8.GetBytes(password);
        var hashBytes = md5.ComputeHash(inputBytes);
        
        var sb = new StringBuilder();
        foreach (var b in hashBytes)
        {
            sb.Append(b.ToString("x2"));
        }
        return sb.ToString();
    }

    /// <summary>
    /// 验证密码
    /// </summary>
    public static bool VerifyPassword(string password, string hashedPassword)
    {
        var hashOfInput = HashPassword(password);
        return string.Equals(hashOfInput, hashedPassword, StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// 生成随机密码
    /// </summary>
    public static string GenerateRandomPassword(int length = 12)
    {
        const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*";
        var random = new Random();
        var chars = new char[length];
        
        for (int i = 0; i < length; i++)
        {
            chars[i] = validChars[random.Next(validChars.Length)];
        }
        
        return new string(chars);
    }
}