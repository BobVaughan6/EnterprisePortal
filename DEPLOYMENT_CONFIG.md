# 部署配置说明

## 🔧 当前环境配置

### 后端服务

**实际运行地址**: `https://localhost:49522`

**API 基础路径**: `https://localhost:49522/api`

**关键接口**:
- 登录: `https://localhost:49522/api/Auth/login`
- Swagger: `https://localhost:49522/swagger`

### 前端服务

**开发环境**: `http://localhost:3000`

**API 配置**: 已更新为 `https://localhost:49522`

---

## 📝 配置文件更新

### 1. 前端环境配置

**文件**: `hailong-admin/.env.development`

```env
# 开发环境配置
VITE_APP_TITLE=海隆咨询后台管理系统
VITE_API_BASE_URL=https://localhost:49522
VITE_APP_PORT=3000
```

✅ **已更新完成**

### 2. 生产环境配置

**文件**: `hailong-admin/.env.production`

```env
# 生产环境配置
VITE_APP_TITLE=海隆咨询后台管理系统
VITE_API_BASE_URL=https://your-production-domain.com
VITE_APP_PORT=3000
```

⚠️ **部署到生产环境时需要修改**

---

## 🚀 启动步骤

### 方式一：使用 Visual Studio

1. **启动后端**:
   - 在 Visual Studio 中打开 `BackEnd/Protral.sln`
   - 按 F5 或点击"运行"按钮
   - 后端将自动在 `https://localhost:49522` 启动

2. **启动前端**:
   ```bash
   cd hailong-admin
   npm run dev
   ```
   - 前端将在 `http://localhost:3000` 启动

### 方式二：使用命令行

1. **启动后端**:
   ```bash
   cd BackEnd/HailongConsulting.API
   dotnet run --launch-profile https
   ```

2. **启动前端**:
   ```bash
   cd hailong-admin
   npm run dev
   ```

---

## 🔐 HTTPS 证书配置

由于后端使用 HTTPS，可能会遇到证书问题。

### 开发环境证书信任

**Windows**:
```bash
dotnet dev-certs https --trust
```

**Mac/Linux**:
```bash
dotnet dev-certs https --trust
```

### 浏览器证书警告

如果浏览器提示证书不安全：

1. **Chrome/Edge**: 
   - 点击"高级"
   - 点击"继续访问 localhost (不安全)"

2. **Firefox**:
   - 点击"高级"
   - 点击"接受风险并继续"

### 前端开发环境跳过证书验证

如果遇到 HTTPS 证书问题，可以在开发环境中配置 axios 忽略证书验证：

**文件**: `hailong-admin/src/api/request.js`

```javascript
// 仅在开发环境使用
if (import.meta.env.DEV) {
  // 注意：这仅用于开发环境，生产环境不要使用
  process.env.NODE_TLS_REJECT_UNAUTHORIZED = '0'
}
```

⚠️ **警告**: 仅在开发环境使用，生产环境必须使用有效的 SSL 证书！

---

## 🧪 测试 API 连接

### 1. 测试后端是否正常运行

在浏览器中访问：
```
https://localhost:49522/swagger
```

应该能看到 Swagger API 文档页面。

### 2. 测试登录接口

使用 curl 或 Postman：

```bash
curl -k -X POST https://localhost:49522/api/Auth/login \
  -H "Content-Type: application/json" \
  -d '{
    "username": "admin",
    "password": "admin123"
  }'
```

**注意**: `-k` 参数用于跳过 SSL 证书验证（仅开发环境）

### 3. 测试前端连接

1. 启动前端服务
2. 打开浏览器访问 `http://localhost:3000`
3. 打开浏览器开发者工具（F12）
4. 查看 Network 标签
5. 尝试登录
6. 检查请求是否发送到 `https://localhost:49522/api/Auth/login`

---

## 🔍 常见问题排查

### 问题 1: CORS 错误

**错误信息**: 
```
Access to XMLHttpRequest at 'https://localhost:49522/api/...' from origin 'http://localhost:3000' has been blocked by CORS policy
```

**解决方案**:

检查后端 CORS 配置（`Program.cs`）：

```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// 确保在中间件中启用
app.UseCors("AllowAll");
```

### 问题 2: SSL 证书错误

**错误信息**: 
```
NET::ERR_CERT_AUTHORITY_INVALID
```

**解决方案**:

1. 信任开发证书：
   ```bash
   dotnet dev-certs https --trust
   ```

2. 或在浏览器中手动接受证书

### 问题 3: 端口被占用

**错误信息**: 
```
Failed to bind to address https://localhost:49522
```

**解决方案**:

1. 检查端口占用：
   ```bash
   netstat -ano | findstr :49522
   ```

2. 结束占用进程或更改端口

### 问题 4: 前端无法连接后端

**检查清单**:

1. ✅ 后端服务是否正常运行
2. ✅ 前端 `.env.development` 配置是否正确
3. ✅ 浏览器控制台是否有错误信息
4. ✅ Network 标签中请求 URL 是否正确

---

## 📊 端口配置说明

### 当前端口分配

| 服务 | 协议 | 端口 | 地址 |
|------|------|------|------|
| 后端 API | HTTPS | 49522 | https://localhost:49522 |
| 前端开发服务器 | HTTP | 3000 | http://localhost:3000 |
| MySQL 数据库 | TCP | 3306 | localhost:3306 |

### 修改端口配置

#### 修改后端端口

**文件**: `BackEnd/HailongConsulting.API/Properties/launchSettings.json`

```json
{
  "profiles": {
    "https": {
      "commandName": "Project",
      "dotnetRunMessages": true,
      "launchBrowser": true,
      "applicationUrl": "https://localhost:49522;http://localhost:5000",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    }
  }
}
```

#### 修改前端端口

**文件**: `hailong-admin/.env.development`

```env
VITE_APP_PORT=3000
```

或在启动时指定：
```bash
npm run dev -- --port 3001
```

---

## 🌐 生产环境部署

### 1. 后端部署

**IIS 部署**:
1. 发布项目：
   ```bash
   dotnet publish -c Release -o ./publish
   ```

2. 在 IIS 中创建网站
3. 配置 HTTPS 证书
4. 更新 `appsettings.json` 中的数据库连接字符串

**Docker 部署**:
```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY ./publish .
EXPOSE 443
ENTRYPOINT ["dotnet", "HailongConsulting.API.dll"]
```

### 2. 前端部署

**构建生产版本**:
```bash
cd hailong-admin
npm run build
```

**部署到 Nginx**:
```nginx
server {
    listen 80;
    server_name your-domain.com;
    
    location / {
        root /var/www/hailong-admin/dist;
        try_files $uri $uri/ /index.html;
    }
    
    location /api {
        proxy_pass https://your-backend-server;
        proxy_set_header Host $host;
        proxy_set_header X-Real-IP $remote_addr;
    }
}
```

### 3. 环境变量配置

**生产环境** (`hailong-admin/.env.production`):
```env
VITE_APP_TITLE=海隆咨询后台管理系统
VITE_API_BASE_URL=https://api.your-domain.com
```

---

## ✅ 配置验证清单

部署前请确认：

- [ ] 后端服务能正常启动在 `https://localhost:49522`
- [ ] Swagger 文档可以访问
- [ ] 前端 `.env.development` 已更新为正确的 API 地址
- [ ] 前端能成功调用后端 API
- [ ] 登录功能正常
- [ ] CORS 配置正确
- [ ] SSL 证书已信任（开发环境）
- [ ] 数据库连接正常
- [ ] 所有 API 接口测试通过

---

## 📞 技术支持

如遇到配置问题：

1. 查看后端日志：`BackEnd/HailongConsulting.API/logs/`
2. 查看浏览器控制台错误信息
3. 检查 Network 标签中的请求详情
4. 参考 `API_INTEGRATION_GUIDE.md` 和 `API_TEST_GUIDE.md`

---

## 🔄 更新记录

### 2024-01-01
- ✅ 更新前端 API 地址为 `https://localhost:49522`
- ✅ 添加 HTTPS 证书配置说明
- ✅ 添加端口配置说明
- ✅ 添加常见问题排查指南