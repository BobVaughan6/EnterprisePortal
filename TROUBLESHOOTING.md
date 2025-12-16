# 🔧 海龙咨询官网 - 故障排查手册

## 📋 目录

- [快速诊断](#快速诊断)
- [后端API问题](#后端api问题)
- [前端问题](#前端问题)
- [数据库问题](#数据库问题)
- [部署问题](#部署问题)
- [性能问题](#性能问题)
- [安全问题](#安全问题)
- [日志分析](#日志分析)

---

## 🚀 快速诊断

### 系统健康检查清单

```bash
# 1. 检查服务状态
sudo systemctl status hailong-api
sudo systemctl status nginx
sudo systemctl status mysql

# 2. 检查端口占用
sudo netstat -tlnp | grep :5000  # API端口
sudo netstat -tlnp | grep :80    # HTTP端口
sudo netstat -tlnp | grep :443   # HTTPS端口
sudo netstat -tlnp | grep :3306  # MySQL端口

# 3. 检查磁盘空间
df -h

# 4. 检查内存使用
free -h

# 5. 检查CPU使用
top -bn1 | head -20

# 6. 检查日志错误
sudo tail -100 /var/log/hailong-api/error.log
sudo tail -100 /var/log/nginx/error.log
sudo tail -100 /var/log/mysql/error.log
```

### 快速问题定位

| 症状 | 可能原因 | 快速检查 |
|------|----------|----------|
| 网站无法访问 | Nginx未启动 | `systemctl status nginx` |
| API返回500错误 | 后端服务异常 | 查看API日志 |
| 数据库连接失败 | MySQL未启动 | `systemctl status mysql` |
| 页面加载慢 | 资源未压缩 | 检查Nginx配置 |
| 登录失败 | Token过期 | 检查JWT配置 |

---

## 🔴 后端API问题

### 问题1: API服务无法启动

**症状**:
```bash
sudo systemctl start hailong-api
# 服务启动失败
```

**诊断步骤**:
```bash
# 1. 查看服务状态
sudo systemctl status hailong-api

# 2. 查看详细日志
sudo journalctl -u hailong-api -n 50 --no-pager

# 3. 手动启动测试
cd /var/www/hailong-api
dotnet HailongConsulting.API.dll
```

**常见原因和解决方案**:

#### 原因1: 端口被占用
```bash
# 检查端口占用
sudo lsof -i :5000

# 解决方案：杀死占用进程或更改端口
sudo kill -9 <PID>
# 或修改 appsettings.json 中的端口配置
```

#### 原因2: 数据库连接失败
```bash
# 检查MySQL服务
sudo systemctl status mysql

# 测试数据库连接
mysql -u hailong_user -p -h localhost hailong_consulting

# 解决方案：检查连接字符串
nano /var/www/hailong-api/appsettings.json
```

#### 原因3: 文件权限问题
```bash
# 检查文件所有者
ls -la /var/www/hailong-api/

# 修复权限
sudo chown -R www-data:www-data /var/www/hailong-api/
sudo chmod -R 755 /var/www/hailong-api/
```

#### 原因4: 依赖缺失
```bash
# 检查.NET运行时
dotnet --list-runtimes

# 安装缺失的运行时
sudo apt install -y dotnet-runtime-7.0
```

---

### 问题2: API返回500错误

**症状**:
```json
{
  "success": false,
  "message": "Internal Server Error",
  "data": null
}
```

**诊断步骤**:
```bash
# 1. 查看API日志
sudo tail -100 /var/www/hailong-api/logs/error-*.log

# 2. 查看系统日志
sudo journalctl -u hailong-api -n 100 --no-pager

# 3. 启用详细日志
# 修改 appsettings.json
"Logging": {
  "LogLevel": {
    "Default": "Debug"
  }
}
```

**常见错误和解决方案**:

#### 错误1: 数据库查询异常
```
Error: MySql.Data.MySqlClient.MySqlException: Table 'xxx' doesn't exist
```

**解决方案**:
```bash
# 检查数据库表
mysql -u hailong_user -p hailong_consulting -e "SHOW TABLES;"

# 重新执行数据库脚本
mysql -u hailong_user -p hailong_consulting < /path/to/schema.sql
```

#### 错误2: 空引用异常
```
Error: System.NullReferenceException: Object reference not set
```

**解决方案**:
- 检查代码中的空值处理
- 添加空值检查和默认值
- 查看具体的堆栈跟踪定位问题

#### 错误3: 文件上传失败
```
Error: System.IO.DirectoryNotFoundException: Could not find path
```

**解决方案**:
```bash
# 创建上传目录
sudo mkdir -p /var/www/hailong-api/wwwroot/uploads/attachments/{image,document,other}

# 设置权限
sudo chown -R www-data:www-data /var/www/hailong-api/wwwroot/uploads/
sudo chmod -R 755 /var/www/hailong-api/wwwroot/uploads/
```

---

### 问题3: JWT认证失败

**症状**:
```json
{
  "success": false,
  "message": "Unauthorized",
  "data": null
}
```

**诊断步骤**:
```bash
# 1. 检查Token是否过期
# 使用 jwt.io 解码Token查看exp字段

# 2. 检查JWT配置
cat /var/www/hailong-api/appsettings.json | grep -A 5 "Jwt"

# 3. 查看认证日志
sudo grep "Authentication" /var/www/hailong-api/logs/*.log
```

**解决方案**:

#### 方案1: Token过期
```javascript
// 前端：实现Token刷新机制
axios.interceptors.response.use(
  response => response,
  async error => {
    if (error.response?.status === 401) {
      // 刷新Token或重新登录
      await refreshToken();
    }
    return Promise.reject(error);
  }
);
```

#### 方案2: 密钥不匹配
```json
// 确保前后端使用相同的JWT密钥
// appsettings.json
{
  "Jwt": {
    "Key": "your-secret-key-must-be-at-least-32-characters",
    "Issuer": "HailongConsulting",
    "Audience": "HailongConsultingUsers"
  }
}
```

---

### 问题4: 文件上传失败

**症状**:
- 上传接口返回错误
- 文件保存失败

**诊断步骤**:
```bash
# 1. 检查上传目录权限
ls -la /var/www/hailong-api/wwwroot/uploads/

# 2. 检查磁盘空间
df -h /var/www/

# 3. 检查文件大小限制
# Nginx配置
sudo grep "client_max_body_size" /etc/nginx/nginx.conf

# ASP.NET Core配置
grep "MultipartBodyLengthLimit" /var/www/hailong-api/Program.cs
```

**解决方案**:

#### 方案1: 增加文件大小限制
```nginx
# /etc/nginx/nginx.conf
http {
    client_max_body_size 100M;
}

# 重启Nginx
sudo systemctl restart nginx
```

#### 方案2: 修复目录权限
```bash
sudo chown -R www-data:www-data /var/www/hailong-api/wwwroot/uploads/
sudo chmod -R 755 /var/www/hailong-api/wwwroot/uploads/
```

---

## 🌐 前端问题

### 问题1: 页面白屏

**症状**:
- 浏览器显示空白页面
- 控制台有JavaScript错误

**诊断步骤**:
```bash
# 1. 打开浏览器开发者工具 (F12)
# 2. 查看Console标签页的错误信息
# 3. 查看Network标签页的请求状态

# 4. 检查Nginx配置
sudo nginx -t

# 5. 查看Nginx错误日志
sudo tail -100 /var/log/nginx/error.log
```

**常见原因和解决方案**:

#### 原因1: 路由配置错误
```nginx
# /etc/nginx/sites-available/hailong-portal
location / {
    try_files $uri $uri/ /index.html;  # 确保有这行
}
```

#### 原因2: 静态资源404
```bash
# 检查文件是否存在
ls -la /var/www/hailong-portal/dist/

# 重新构建
cd /path/to/hailong-portal
npm run build
sudo cp -r dist/* /var/www/hailong-portal/
```

#### 原因3: API地址配置错误
```javascript
// 检查 .env.production
VITE_API_BASE_URL=https://api.yourdomain.com

// 重新构建
npm run build
```

---

### 问题2: API请求失败

**症状**:
```
Network Error
或
CORS Error
```

**诊断步骤**:
```javascript
// 1. 打开浏览器控制台
// 2. 查看Network标签
// 3. 检查失败的请求详情

// 4. 测试API是否可访问
curl https://api.yourdomain.com/api/health
```

**解决方案**:

#### 方案1: CORS配置
```csharp
// Program.cs
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.WithOrigins(
            "https://www.yourdomain.com",
            "https://admin.yourdomain.com"
        )
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials();
    });
});

app.UseCors("AllowAll");
```

#### 方案2: Nginx反向代理
```nginx
location /api/ {
    proxy_pass http://localhost:5000/api/;
    proxy_set_header Host $host;
    proxy_set_header X-Real-IP $remote_addr;
    proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
    proxy_set_header X-Forwarded-Proto $scheme;
}
```

---

### 问题3: 页面加载慢

**症状**:
- 首屏加载时间超过3秒
- 资源加载缓慢

**诊断步骤**:
```bash
# 1. 使用浏览器Performance工具分析
# 2. 检查Network瀑布图

# 3. 检查Gzip是否启用
curl -I -H "Accept-Encoding: gzip" https://www.yourdomain.com

# 4. 检查资源大小
du -sh /var/www/hailong-portal/dist/*
```

**优化方案**:

#### 方案1: 启用Gzip压缩
```nginx
# /etc/nginx/nginx.conf
gzip on;
gzip_vary on;
gzip_min_length 1024;
gzip_types text/plain text/css text/xml text/javascript 
           application/x-javascript application/xml+rss 
           application/javascript application/json;
```

#### 方案2: 启用浏览器缓存
```nginx
location ~* \.(jpg|jpeg|png|gif|ico|css|js)$ {
    expires 1y;
    add_header Cache-Control "public, immutable";
}
```

#### 方案3: 代码分割
```javascript
// vite.config.js
export default {
  build: {
    rollupOptions: {
      output: {
        manualChunks: {
          'element-plus': ['element-plus'],
          'echarts': ['echarts'],
          'vendor': ['vue', 'vue-router', 'pinia']
        }
      }
    }
  }
}
```

---

## 💾 数据库问题

### 问题1: 数据库连接失败

**症状**:
```
Error: Unable to connect to database
MySql.Data.MySqlClient.MySqlException: Unable to connect to any of the specified MySQL hosts
```

**诊断步骤**:
```bash
# 1. 检查MySQL服务状态
sudo systemctl status mysql

# 2. 测试数据库连接
mysql -u hailong_user -p -h localhost hailong_consulting

# 3. 检查端口监听
sudo netstat -tlnp | grep 3306

# 4. 查看MySQL错误日志
sudo tail -100 /var/log/mysql/error.log
```

**解决方案**:

#### 方案1: 启动MySQL服务
```bash
sudo systemctl start mysql
sudo systemctl enable mysql
```

#### 方案2: 重置用户密码
```sql
-- 登录MySQL
sudo mysql

-- 重置密码
ALTER USER 'hailong_user'@'localhost' IDENTIFIED BY 'new_password';
FLUSH PRIVILEGES;
```

#### 方案3: 检查防火墙
```bash
# 允许MySQL端口
sudo ufw allow 3306/tcp
```

---

### 问题2: 查询性能慢

**症状**:
- 查询响应时间超过1秒
- 数据库CPU使用率高

**诊断步骤**:
```sql
-- 1. 查看慢查询日志
SHOW VARIABLES LIKE 'slow_query_log%';

-- 2. 查看当前运行的查询
SHOW FULL PROCESSLIST;

-- 3. 分析查询执行计划
EXPLAIN SELECT * FROM Announcements WHERE Type = 'Construction';

-- 4. 查看表索引
SHOW INDEX FROM Announcements;
```

**优化方案**:

#### 方案1: 添加索引
```sql
-- 为常用查询字段添加索引
CREATE INDEX idx_type ON Announcements(Type);
CREATE INDEX idx_publish_date ON Announcements(PublishDate);
CREATE INDEX idx_region ON Announcements(Region);

-- 复合索引
CREATE INDEX idx_type_date ON Announcements(Type, PublishDate);
```

#### 方案2: 优化查询
```sql
-- 避免SELECT *
SELECT Id, Title, PublishDate FROM Announcements;

-- 使用LIMIT限制结果
SELECT * FROM Announcements LIMIT 10;

-- 避免子查询，使用JOIN
SELECT a.*, u.Username 
FROM Announcements a
JOIN Users u ON a.CreatedBy = u.Id;
```

#### 方案3: 分析和优化表
```sql
-- 分析表
ANALYZE TABLE Announcements;

-- 优化表
OPTIMIZE TABLE Announcements;
```

---

### 问题3: 数据库空间不足

**症状**:
```
Error: The table is full
或磁盘空间不足
```

**诊断步骤**:
```bash
# 1. 检查磁盘空间
df -h

# 2. 查看数据库大小
mysql -u root -p -e "
SELECT 
    table_schema AS 'Database',
    ROUND(SUM(data_length + index_length) / 1024 / 1024, 2) AS 'Size (MB)'
FROM information_schema.tables
GROUP BY table_schema;
"

# 3. 查看各表大小
mysql -u root -p hailong_consulting -e "
SELECT 
    table_name AS 'Table',
    ROUND(((data_length + index_length) / 1024 / 1024), 2) AS 'Size (MB)'
FROM information_schema.TABLES
WHERE table_schema = 'hailong_consulting'
ORDER BY (data_length + index_length) DESC;
"
```

**解决方案**:

#### 方案1: 清理日志表
```sql
-- 清理旧的系统日志
DELETE FROM SystemLogs WHERE CreatedAt < DATE_SUB(NOW(), INTERVAL 90 DAY);

-- 清理旧的访问统计
DELETE FROM VisitStatistics WHERE VisitDate < DATE_SUB(NOW(), INTERVAL 180 DAY);
```

#### 方案2: 优化表
```sql
-- 优化表以回收空间
OPTIMIZE TABLE SystemLogs;
OPTIMIZE TABLE VisitStatistics;
```

#### 方案3: 归档旧数据
```bash
# 导出旧数据
mysqldump -u root -p hailong_consulting SystemLogs \
  --where="CreatedAt < '2023-01-01'" > old_logs.sql

# 删除已归档的数据
mysql -u root -p hailong_consulting -e \
  "DELETE FROM SystemLogs WHERE CreatedAt < '2023-01-01';"
```

---

## 🚀 部署问题

### 问题1: Nginx配置错误

**症状**:
```bash
nginx: [emerg] unexpected "}" in /etc/nginx/sites-available/default:50
```

**诊断步骤**:
```bash
# 测试Nginx配置
sudo nginx -t

# 查看详细错误
sudo nginx -t 2>&1 | more
```

**解决方案**:
```bash
# 1. 检查配置文件语法
sudo nano /etc/nginx/sites-available/hailong-portal

# 2. 常见错误：
# - 缺少分号
# - 括号不匹配
# - 指令拼写错误

# 3. 修复后重新测试
sudo nginx -t

# 4. 重新加载配置
sudo systemctl reload nginx
```

---

### 问题2: SSL证书问题

**症状**:
- 浏览器显示"不安全"
- SSL证书过期

**诊断步骤**:
```bash
# 1. 检查证书有效期
sudo certbot certificates

# 2. 测试SSL配置
openssl s_client -connect yourdomain.com:443 -servername yourdomain.com

# 3. 在线测试
# 访问 https://www.ssllabs.com/ssltest/
```

**解决方案**:

#### 方案1: 续期证书
```bash
# 手动续期
sudo certbot renew

# 测试续期
sudo certbot renew --dry-run

# 强制续期
sudo certbot renew --force-renewal
```

#### 方案2: 重新申请证书
```bash
# 删除旧证书
sudo certbot delete --cert-name yourdomain.com

# 重新申请
sudo certbot --nginx -d yourdomain.com -d www.yourdomain.com
```

---

## ⚡ 性能问题

### 问题1: 高CPU使用率

**诊断步骤**:
```bash
# 1. 查看CPU使用情况
top -bn1 | head -20

# 2. 查看进程CPU使用
ps aux --sort=-%cpu | head -10

# 3. 查看API进程
ps aux | grep dotnet

# 4. 查看数据库进程
ps aux | grep mysql
```

**解决方案**:

#### 方案1: 优化代码
- 使用异步编程
- 避免死循环
- 优化算法复杂度

#### 方案2: 增加缓存
```csharp
// 添加内存缓存
services.AddMemoryCache();

// 使用缓存
public class AnnouncementService
{
    private readonly IMemoryCache _cache;
    
    public async Task<List<Announcement>> GetHotAnnouncementsAsync()
    {
        return await _cache.GetOrCreateAsync("hot_announcements", async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
            return await _repository.GetHotAnnouncementsAsync();
        });
    }
}
```

---

### 问题2: 高内存使用

**诊断步骤**:
```bash
# 1. 查看内存使用
free -h

# 2. 查看进程内存使用
ps aux --sort=-%mem | head -10

# 3. 查看详细内存信息
cat /proc/meminfo
```

**解决方案**:

#### 方案1: 优化查询
```csharp
// 使用分页避免加载大量数据
public async Task<PagedResult<Announcement>> GetAnnouncementsAsync(int page, int pageSize)
{
    var query = _context.Announcements.AsQueryable();
    var totalCount = await query.CountAsync();
    
    var items = await query
        .Skip((page - 1) * pageSize)
        .Take(pageSize)
        .ToListAsync();
    
    return new PagedResult<Announcement>(items, totalCount, page, pageSize);
}
```

#### 方案2: 释放资源
```csharp
// 使用using确保资源释放
using (var scope = serviceProvider.CreateScope())
{
    var service = scope.ServiceProvider.GetRequiredService<IMyService>();
    await service.DoWorkAsync();
}
```

---

## 📊 日志分析

### API日志位置
```bash
# 应用日志
/var/www/hailong-api/logs/

# 系统日志
sudo journalctl -u hailong-api

# Nginx日志
/var/log/nginx/access.log
/var/log/nginx/error.log

# MySQL日志
/var/log/mysql/error.log
/var/log/mysql/slow-query.log
```

### 常用日志命令
```bash
# 实时查看日志
sudo tail -f /var/www/hailong-api/logs/error-*.log

# 查看最近100行
sudo tail -100 /var/log/nginx/error.log

# 搜索特定错误
sudo grep "Exception" /var/www/hailong-api/logs/*.log

# 统计错误数量
sudo grep -c "Error" /var/www/hailong-api/logs/error-*.log

# 查看特定时间段的日志
sudo journalctl -u hailong-api --since "2024-01-01 00:00:00" --until "2024-01-01 23:59:59"
```

---

## 🆘 紧急故障处理

### 网站完全无法访问

```bash
# 1. 快速重启所有服务
sudo systemctl restart nginx
sudo systemctl restart hailong-api
sudo systemctl restart mysql

# 2. 检查服务状态
sudo systemctl status nginx
sudo systemctl status hailong-api
sudo systemctl status mysql

# 3. 查看错误日志
sudo tail -100 /var/log/nginx/error.log
sudo journalctl -u hailong-api -n 100 --no-pager

# 4. 如果仍然无法访问，回滚到上一个版本
cd /var/www/hailong-api
sudo cp -r backup/previous-version/* .
sudo systemctl restart hailong-api
```

---

## 📞 获取帮助

如果以上方法都无法解决问题：

1. **查看文档**: [README.md](README.md)、[DEPLOYMENT.md](DEPLOYMENT.md)
2. **查看日志**: 收集完整的错误日志
3. **联系技术支持**: 提供详细的错误信息和日志
4. **GitHub Issues**: 在项目仓库提交Issue

---

## 📝 故障报告模板

```markdown
### 问题描述
[简要描述问题]

### 环境信息
- 操作系统: Ubuntu 22.04
- .NET版本: 7.0
- MySQL版本: 8.0
- Nginx版本: 1.24

### 复现步骤
1. [步骤1]
2. [步骤2]
3. [步骤3]

### 错误信息
```
[粘贴错误日志]
```

### 已尝试的解决方案
- [方案1]
- [方案2]

### 相关日志
[附加相关日志文件]
```

---

**文档维护**: 开发团队  
**最后更新**: 2024-01-01