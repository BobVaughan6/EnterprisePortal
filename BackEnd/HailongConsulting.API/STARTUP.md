# 海隆咨询API - 启动指南

## 📋 前置要求

### 必需软件
- **.NET 7 SDK** - [下载地址](https://dotnet.microsoft.com/download/dotnet/7.0)
- **MySQL 8.0+** - [下载地址](https://dev.mysql.com/downloads/mysql/)
- **IDE** (选择其一):
  - Visual Studio 2022 (推荐)
  - Visual Studio Code + C# 扩展
  - JetBrains Rider

### 验证安装
```bash
# 检查.NET版本
dotnet --version
# 应显示: 7.0.x

# 检查MySQL
mysql --version
# 应显示: mysql Ver 8.0.x
```

## 🚀 快速启动

### 1. 克隆/下载项目
```bash
cd BackEnd/HailongConsulting.API
```

### 2. 安装依赖包
```bash
dotnet restore
```

### 3. 配置数据库

#### 3.1 创建MySQL数据库
```bash
# 登录MySQL
mysql -u root -p

# 创建数据库
CREATE DATABASE hailong_consulting CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;

# 退出MySQL
exit;
```

#### 3.2 执行数据库脚本
```bash
# 在项目根目录执行
cd ../../SQL

# 创建表结构
mysql -u root -p hailong_consulting < hailong_consulting_schema.sql

# 导入初始数据
mysql -u root -p hailong_consulting < hailong_consulting_init_data.sql
```

### 4. 配置连接字符串

编辑 `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3306;Database=hailong_consulting;User=root;Password=你的密码;CharSet=utf8mb4;"
  }
}
```

**重要**: 将 `你的密码` 替换为你的MySQL root密码。

### 5. 配置JWT密钥

编辑 `appsettings.json` 中的JWT配置:

```json
{
  "Jwt": {
    "Key": "YourSuperSecretKeyForJWTTokenGeneration123456",
    "Issuer": "HailongConsulting",
    "Audience": "HailongConsultingUsers",
    "ExpireHours": 24
  }
}
```

**生产环境**: 请使用更强的密钥（建议64字符以上的随机字符串）。

### 6. 运行项目

#### 使用命令行
```bash
cd BackEnd/HailongConsulting.API
dotnet run
```

#### 使用Visual Studio
1. 打开 `HailongConsulting.API.csproj`
2. 按 `F5` 或点击"运行"按钮

#### 使用VS Code
1. 打开项目文件夹
2. 按 `F5` 或使用调试面板

### 7. 验证启动

项目启动后，你应该看到类似输出：
```
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5000
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: https://localhost:5001
```

### 8. 访问API文档

打开浏览器访问:
- **Swagger UI**: http://localhost:5000 或 https://localhost:5001

## 🧪 测试API

### 使用Swagger UI测试

1. 访问 Swagger UI
2. 展开 `/api/auth/login` 端点
3. 点击 "Try it out"
4. 输入测试凭据:
```json
{
  "username": "admin",
  "password": "admin123"
}
```
5. 点击 "Execute"
6. 复制返回的 `token`

### 使用认证Token

1. 点击页面右上角的 "Authorize" 按钮
2. 输入: `Bearer {你的token}`
3. 点击 "Authorize"
4. 现在可以测试需要认证的端点了

### 使用Postman测试

#### 登录请求
```http
POST http://localhost:5000/api/auth/login
Content-Type: application/json

{
  "username": "admin",
  "password": "admin123"
}
```

#### 使用Token访问受保护端点
```http
GET http://localhost:5000/api/projects
Authorization: Bearer {你的token}
```

## 📝 默认测试账号

根据初始数据脚本，默认账号为:

| 用户名 | 密码 | 角色 | 说明 |
|--------|------|------|------|
| admin | admin123 | admin | 管理员账号 |
| manager | manager123 | manager | 项目经理账号 |
| user | user123 | user | 普通用户账号 |

**注意**: 生产环境请立即修改这些默认密码！

## 🔧 常见问题

### 问题1: 数据库连接失败

**错误信息**: `Unable to connect to any of the specified MySQL hosts`

**解决方案**:
1. 确认MySQL服务正在运行
2. 检查连接字符串中的主机、端口、用户名和密码
3. 确认MySQL允许本地连接
4. 检查防火墙设置

### 问题2: 端口被占用

**错误信息**: `Failed to bind to address http://127.0.0.1:5000`

**解决方案**:
修改 `Properties/launchSettings.json` 中的端口:
```json
{
  "applicationUrl": "https://localhost:5002;http://localhost:5001"
}
```

### 问题3: JWT认证失败

**错误信息**: `401 Unauthorized`

**解决方案**:
1. 确认Token格式正确: `Bearer {token}`
2. 检查Token是否过期
3. 验证JWT配置中的Key、Issuer和Audience

### 问题4: EF Core迁移错误

**解决方案**:
```bash
# 删除现有迁移
rm -rf Migrations/

# 重新创建迁移
dotnet ef migrations add InitialCreate

# 更新数据库
dotnet ef database update
```

### 问题5: 日志文件权限错误

**解决方案**:
确保应用程序有权限在 `logs/` 目录写入文件:
```bash
# Linux/Mac
chmod 755 logs/

# Windows
# 右键 logs 文件夹 -> 属性 -> 安全 -> 编辑权限
```

## 🔒 安全配置

### 生产环境检查清单

- [ ] 修改所有默认密码
- [ ] 使用强JWT密钥（64+字符）
- [ ] 配置HTTPS证书
- [ ] 限制CORS允许的域名
- [ ] 使用环境变量存储敏感信息
- [ ] 启用请求速率限制
- [ ] 配置日志级别为Warning或Error
- [ ] 禁用Swagger UI（或限制访问）
- [ ] 定期更新NuGet包
- [ ] 配置数据库备份

### 使用User Secrets（开发环境）

```bash
# 初始化User Secrets
dotnet user-secrets init

# 设置连接字符串
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=localhost;..."

# 设置JWT密钥
dotnet user-secrets set "Jwt:Key" "YourSecretKey"
```

### 使用环境变量（生产环境）

```bash
# Linux/Mac
export ConnectionStrings__DefaultConnection="Server=..."
export Jwt__Key="YourSecretKey"

# Windows
set ConnectionStrings__DefaultConnection=Server=...
set Jwt__Key=YourSecretKey
```

## 📊 性能优化

### 数据库连接池
默认已配置，无需额外设置。

### 启用响应压缩
在 `Program.cs` 中添加:
```csharp
builder.Services.AddResponseCompression();
app.UseResponseCompression();
```

### 启用响应缓存
```csharp
builder.Services.AddResponseCaching();
app.UseResponseCaching();
```

## 📈 监控和日志

### 查看日志
日志文件位置: `logs/log-{Date}.txt`

### 日志级别
- Development: Debug
- Production: Information

### 修改日志级别
编辑 `appsettings.json`:
```json
{
  "Serilog": {
    "MinimumLevel": {
      "Default": "Information"
    }
  }
}
```

## 🚢 部署

### 发布应用
```bash
# 发布Release版本
dotnet publish -c Release -o ./publish

# 发布到指定运行时
dotnet publish -c Release -r linux-x64 --self-contained
```

### Docker部署
```bash
# 构建镜像
docker build -t hailong-api .

# 运行容器
docker run -d -p 5000:80 --name hailong-api \
  -e ConnectionStrings__DefaultConnection="Server=..." \
  hailong-api
```

### IIS部署
1. 安装 .NET 7 Hosting Bundle
2. 发布应用到文件夹
3. 在IIS中创建新站点
4. 配置应用程序池（无托管代码）
5. 设置环境变量

## 📞 获取帮助

- **文档**: 查看 `README.md`
- **API文档**: 访问 Swagger UI
- **问题反馈**: 联系开发团队

## 📄 许可证

Copyright © 2025 海隆咨询