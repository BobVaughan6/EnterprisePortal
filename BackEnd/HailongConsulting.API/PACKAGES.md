# NuGet包清单

## 核心包

### Microsoft.AspNetCore.OpenApi (7.0.14)
- **用途**: ASP.NET Core OpenAPI支持
- **说明**: 提供OpenAPI规范支持

### Microsoft.EntityFrameworkCore (7.0.14)
- **用途**: Entity Framework Core ORM框架
- **说明**: 数据访问层核心框架

### Microsoft.EntityFrameworkCore.Design (7.0.14)
- **用途**: EF Core设计时工具
- **说明**: 支持数据库迁移和脚手架生成

### Pomelo.EntityFrameworkCore.MySql (7.0.0)
- **用途**: MySQL数据库提供程序
- **说明**: 为EF Core提供MySQL数据库支持

## 认证授权

### Microsoft.AspNetCore.Authentication.JwtBearer (7.0.14)
- **用途**: JWT Bearer认证
- **说明**: 实现基于JWT的身份认证

### System.IdentityModel.Tokens.Jwt (7.0.3)
- **用途**: JWT令牌处理
- **说明**: 生成和验证JWT令牌

## 对象映射

### AutoMapper (12.0.1)
- **用途**: 对象到对象映射
- **说明**: 简化实体和DTO之间的转换

### AutoMapper.Extensions.Microsoft.DependencyInjection (12.0.1)
- **用途**: AutoMapper依赖注入扩展
- **说明**: 集成AutoMapper到ASP.NET Core DI容器

## 日志记录

### Serilog.AspNetCore (7.0.0)
- **用途**: Serilog日志框架
- **说明**: 结构化日志记录框架

### Serilog.Sinks.Console (5.0.0)
- **用途**: 控制台日志输出
- **说明**: 将日志输出到控制台

### Serilog.Sinks.File (5.0.0)
- **用途**: 文件日志输出
- **说明**: 将日志写入文件

## API文档

### Swashbuckle.AspNetCore (6.5.0)
- **用途**: Swagger/OpenAPI文档生成
- **说明**: 自动生成API文档和测试界面

## 安装命令

```bash
# 一次性安装所有包
dotnet add package AutoMapper --version 12.0.1
dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection --version 12.0.1
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer --version 7.0.14
dotnet add package Microsoft.AspNetCore.OpenApi --version 7.0.14
dotnet add package Microsoft.EntityFrameworkCore --version 7.0.14
dotnet add package Microsoft.EntityFrameworkCore.Design --version 7.0.14
dotnet add package Pomelo.EntityFrameworkCore.MySql --version 7.0.0
dotnet add package Serilog.AspNetCore --version 7.0.0
dotnet add package Serilog.Sinks.Console --version 5.0.0
dotnet add package Serilog.Sinks.File --version 5.0.0
dotnet add package Swashbuckle.AspNetCore --version 6.5.0
dotnet add package System.IdentityModel.Tokens.Jwt --version 7.0.3
```

## 或使用项目文件

直接使用提供的 `HailongConsulting.API.csproj` 文件，然后运行：

```bash
dotnet restore
```

## 版本说明

- 所有Microsoft包使用 **7.0.14** 版本（与.NET 7匹配）
- AutoMapper使用 **12.0.1** 版本（最新稳定版）
- Serilog使用 **7.0.0** 和 **5.0.0** 版本（最新稳定版）
- Pomelo.EntityFrameworkCore.MySql使用 **7.0.0** 版本（支持.NET 7）

## 更新包

检查并更新所有包到最新版本：

```bash
dotnet list package --outdated
dotnet add package <PackageName> --version <NewVersion>
```

## 注意事项

1. **版本兼容性**: 确保所有Microsoft包版本与.NET SDK版本匹配
2. **安全更新**: 定期检查并应用安全更新
3. **测试**: 更新包后进行充分测试
4. **生产环境**: 在生产环境部署前在测试环境验证所有包