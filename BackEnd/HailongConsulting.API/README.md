# 海隆咨询官网后端API

基于 .NET 7 Web API 构建的海隆咨询官网后端服务。

## 技术栈

- **.NET 7** - Web API框架
- **Entity Framework Core 7** - ORM框架
- **MySQL** - 数据库（使用Pomelo.EntityFrameworkCore.MySql）
- **JWT** - 身份认证
- **AutoMapper** - 对象映射
- **Serilog** - 日志记录
- **Swagger** - API文档

## 项目结构

```
HailongConsulting.API/
├── Controllers/          # API控制器
│   ├── AuthController.cs
│   └── ProjectsController.cs
├── Services/            # 业务逻辑层
│   ├── IAuthService.cs
│   ├── AuthService.cs
│   ├── IProjectService.cs
│   └── ProjectService.cs
├── Repositories/        # 数据访问层
│   ├── IRepository.cs
│   ├── Repository.cs
│   ├── IUnitOfWork.cs
│   └── UnitOfWork.cs
├── Models/              
│   ├── Entities/       # 数据库实体
│   │   ├── User.cs
│   │   ├── Project.cs
│   │   ├── Client.cs
│   │   └── Category.cs
│   ├── DTOs/           # 数据传输对象
│   │   ├── LoginDto.cs
│   │   └── ProjectDto.cs
│   └── ViewModels/     # 视图模型
│       └── ProjectQueryViewModel.cs
├── Data/
│   └── ApplicationDbContext.cs
├── Common/
│   ├── ApiResponse.cs     # 统一响应格式
│   ├── PagedResult.cs     # 分页结果
│   ├── MappingProfile.cs  # AutoMapper配置
│   └── Helpers/           # 工具类
│       ├── JwtHelper.cs
│       └── PasswordHelper.cs
├── Middleware/          # 中间件
│   └── ExceptionHandlingMiddleware.cs
├── Program.cs
└── appsettings.json
```

## 快速开始

### 1. 环境要求

- .NET 7 SDK
- MySQL 8.0+
- Visual Studio 2022 或 VS Code

### 2. 数据库配置

修改 `appsettings.json` 中的数据库连接字符串：

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3306;Database=hailong_consulting;User=root;Password=your_password;CharSet=utf8mb4;"
  }
}
```

### 3. 创建数据库

执行项目根目录下的SQL脚本：

```bash
# 1. 创建数据库结构
mysql -u root -p < ../../SQL/hailong_consulting_schema.sql

# 2. 导入初始数据
mysql -u root -p < ../../SQL/hailong_consulting_init_data.sql
```

### 4. 安装依赖

```bash
cd BackEnd/HailongConsulting.API
dotnet restore
```

### 5. 运行项目

```bash
dotnet run
```

或使用 Visual Studio 直接运行（F5）。

### 6. 访问API文档

项目启动后，访问：

- **Swagger UI**: http://localhost:5000 或 https://localhost:5001
- **API Base URL**: http://localhost:5000/api 或 https://localhost:5001/api

## NuGet包清单

| 包名 | 版本 | 用途 |
|------|------|------|
| AutoMapper | 12.0.1 | 对象映射 |
| AutoMapper.Extensions.Microsoft.DependencyInjection | 12.0.1 | AutoMapper依赖注入 |
| Microsoft.AspNetCore.Authentication.JwtBearer | 7.0.14 | JWT认证 |
| Microsoft.EntityFrameworkCore | 7.0.14 | EF Core框架 |
| Microsoft.EntityFrameworkCore.Design | 7.0.14 | EF Core设计时工具 |
| Pomelo.EntityFrameworkCore.MySql | 7.0.0 | MySQL数据库提供程序 |
| Serilog.AspNetCore | 7.0.0 | Serilog日志框架 |
| Serilog.Sinks.Console | 5.0.0 | 控制台日志输出 |
| Serilog.Sinks.File | 5.0.0 | 文件日志输出 |
| Swashbuckle.AspNetCore | 6.5.0 | Swagger文档生成 |
| System.IdentityModel.Tokens.Jwt | 7.0.3 | JWT令牌处理 |

## 配置说明

### appsettings.json

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "数据库连接字符串"
  },
  "Jwt": {
    "Key": "JWT密钥（至少32字符）",
    "Issuer": "签发者",
    "Audience": "受众",
    "ExpireHours": 24  // Token过期时间（小时）
  },
  "Serilog": {
    "MinimumLevel": {
      "Default": "Information",  // 默认日志级别
      "Override": {
        "Microsoft": "Warning",
        "Microsoft.EntityFrameworkCore": "Warning"
      }
    }
  }
}
```

### JWT配置

- **Key**: 用于签名JWT的密钥，建议使用至少32个字符的随机字符串
- **Issuer**: JWT签发者标识
- **Audience**: JWT受众标识
- **ExpireHours**: Token有效期（小时）

### CORS配置

默认配置允许所有来源、方法和头部。生产环境建议修改为特定域名：

```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", policy =>
    {
        policy.WithOrigins("https://yourdomain.com")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});
```

## API端点

### 认证相关

- `POST /api/auth/login` - 用户登录
- `POST /api/auth/change-password` - 修改密码
- `POST /api/auth/validate-token` - 验证Token

### 项目管理

- `GET /api/projects` - 获取项目列表（支持分页和筛选）
- `GET /api/projects/{id}` - 获取项目详情
- `POST /api/projects` - 创建项目（需要管理员权限）
- `PUT /api/projects/{id}` - 更新项目（需要管理员权限）
- `DELETE /api/projects/{id}` - 删除项目（需要管理员权限）

## 统一响应格式

所有API响应都遵循统一格式：

```json
{
  "success": true,
  "message": "操作成功",
  "data": {},
  "timestamp": "2025-12-03T10:00:00"
}
```

## 分页查询

分页查询参数：

- `pageIndex`: 页码（从1开始）
- `pageSize`: 每页大小
- `sortBy`: 排序字段
- `isDescending`: 是否降序

分页响应格式：

```json
{
  "pageIndex": 1,
  "pageSize": 10,
  "totalCount": 100,
  "totalPages": 10,
  "hasPreviousPage": false,
  "hasNextPage": true,
  "items": []
}
```

## 日志

日志文件位置：`logs/log-{Date}.txt`

日志级别：
- **Debug**: 调试信息
- **Information**: 一般信息
- **Warning**: 警告信息
- **Error**: 错误信息
- **Fatal**: 致命错误

## 开发建议

1. **数据库迁移**：使用EF Core Migrations管理数据库变更
   ```bash
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```

2. **环境变量**：敏感信息（如数据库密码、JWT密钥）建议使用环境变量或User Secrets

3. **错误处理**：所有异常都会被全局异常处理中间件捕获并返回统一格式

4. **认证授权**：
   - 使用 `[Authorize]` 特性保护需要认证的端点
   - 使用 `[Authorize(Roles = "admin")]` 限制特定角色访问

## 部署

### 发布应用

```bash
dotnet publish -c Release -o ./publish
```

### Docker部署（可选）

创建 `Dockerfile`：

```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["HailongConsulting.API.csproj", "./"]
RUN dotnet restore
COPY . .
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HailongConsulting.API.dll"]
```

## 故障排查

### 常见问题

1. **数据库连接失败**
   - 检查MySQL服务是否运行
   - 验证连接字符串是否正确
   - 确认数据库用户权限

2. **JWT认证失败**
   - 检查JWT配置是否正确
   - 确认Token是否过期
   - 验证Authorization头格式：`Bearer {token}`

3. **CORS错误**
   - 检查CORS策略配置
   - 确认前端请求包含正确的Origin头

## 许可证

Copyright © 2025 海隆咨询