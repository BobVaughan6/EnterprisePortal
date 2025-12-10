# 🚀 快速启动指南

## 一键启动项目

### Windows 用户

创建 `start.bat` 文件：

```batch
@echo off
echo ========================================
echo 海隆咨询管理系统 - 快速启动
echo ========================================
echo.

echo [1/4] 检查环境...
where dotnet >nul 2>nul
if %errorlevel% neq 0 (
    echo [错误] 未找到 .NET SDK，请先安装 .NET 7.0
    pause
    exit /b 1
)

where node >nul 2>nul
if %errorlevel% neq 0 (
    echo [错误] 未找到 Node.js，请先安装 Node.js 16+
    pause
    exit /b 1
)

echo [✓] 环境检查通过
echo.

echo [2/4] 启动后端服务...
start "后端服务" cmd /k "cd BackEnd\HailongConsulting.API && dotnet run"
timeout /t 5 /nobreak >nul

echo [3/4] 启动前端服务...
start "前端服务" cmd /k "cd hailong-admin && npm run dev"

echo.
echo [4/4] 启动完成！
echo.
echo 后端服务: http://localhost:5000
echo Swagger文档: http://localhost:5000/swagger
echo 前端服务: http://localhost:3000
echo.
echo 按任意键关闭此窗口...
pause >nul
```

### Linux/Mac 用户

创建 `start.sh` 文件：

```bash
#!/bin/bash

echo "========================================"
echo "海隆咨询管理系统 - 快速启动"
echo "========================================"
echo ""

echo "[1/4] 检查环境..."
if ! command -v dotnet &> /dev/null; then
    echo "[错误] 未找到 .NET SDK，请先安装 .NET 7.0"
    exit 1
fi

if ! command -v node &> /dev/null; then
    echo "[错误] 未找到 Node.js，请先安装 Node.js 16+"
    exit 1
fi

echo "[✓] 环境检查通过"
echo ""

echo "[2/4] 启动后端服务..."
cd BackEnd/HailongConsulting.API
dotnet run &
BACKEND_PID=$!
cd ../..

sleep 5

echo "[3/4] 启动前端服务..."
cd hailong-admin
npm run dev &
FRONTEND_PID=$!
cd ..

echo ""
echo "[4/4] 启动完成！"
echo ""
echo "后端服务: http://localhost:5000"
echo "Swagger文档: http://localhost:5000/swagger"
echo "前端服务: http://localhost:3000"
echo ""
echo "按 Ctrl+C 停止所有服务"

# 等待用户中断
trap "kill $BACKEND_PID $FRONTEND_PID; exit" INT
wait
```

---

## 手动启动步骤

### 第一步：准备环境

#### 1. 安装必要软件

- **.NET 7.0 SDK**: https://dotnet.microsoft.com/download
- **Node.js 16+**: https://nodejs.org/
- **MySQL 8.0+**: https://dev.mysql.com/downloads/

#### 2. 创建数据库

```sql
CREATE DATABASE hailong_consulting CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
```

#### 3. 配置数据库连接

编辑 `BackEnd/HailongConsulting.API/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3306;Database=hailong_consulting;User=root;Password=你的密码;CharSet=utf8mb4;"
  }
}
```

### 第二步：启动后端

```bash
# 进入后端目录
cd BackEnd/HailongConsulting.API

# 安装依赖（首次运行）
dotnet restore

# 运行数据库迁移（首次运行）
dotnet ef database update

# 启动服务
dotnet run
```

**验证后端启动**:
- 访问: http://localhost:5000
- Swagger: http://localhost:5000/swagger

### 第三步：启动前端

```bash
# 进入前端目录
cd hailong-admin

# 安装依赖（首次运行）
npm install

# 启动开发服务器
npm run dev
```

**验证前端启动**:
- 访问: http://localhost:3000

### 第四步：登录系统

1. 打开浏览器访问 http://localhost:3000
2. 使用默认账号登录：
   - 用户名: `admin`
   - 密码: `admin123` (根据实际数据库配置)

---

## 🔧 开发环境配置

### VS Code 推荐插件

**后端开发**:
- C# Dev Kit
- C# Extensions
- NuGet Package Manager
- REST Client

**前端开发**:
- Vue Language Features (Volar)
- TypeScript Vue Plugin (Volar)
- ESLint
- Prettier
- Auto Rename Tag
- Path Intellisense

### 配置文件

#### .vscode/settings.json

```json
{
  "editor.formatOnSave": true,
  "editor.defaultFormatter": "esbenp.prettier-vscode",
  "[csharp]": {
    "editor.defaultFormatter": "ms-dotnettools.csharp"
  },
  "files.exclude": {
    "**/node_modules": true,
    "**/bin": true,
    "**/obj": true
  }
}
```

#### .vscode/launch.json

```json
{
  "version": "0.2.0",
  "configurations": [
    {
      "name": ".NET Core Launch (web)",
      "type": "coreclr",
      "request": "launch",
      "preLaunchTask": "build",
      "program": "${workspaceFolder}/BackEnd/HailongConsulting.API/bin/Debug/net7.0/HailongConsulting.API.dll",
      "args": [],
      "cwd": "${workspaceFolder}/BackEnd/HailongConsulting.API",
      "stopAtEntry": false,
      "serverReadyAction": {
        "action": "openExternally",
        "pattern": "\\bNow listening on:\\s+(https?://\\S+)"
      },
      "env": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    }
  ]
}
```

---

## 📦 依赖管理

### 后端依赖

主要 NuGet 包：
- Microsoft.EntityFrameworkCore
- Pomelo.EntityFrameworkCore.MySql
- Microsoft.AspNetCore.Authentication.JwtBearer
- AutoMapper
- Serilog
- Swashbuckle.AspNetCore

查看完整依赖：
```bash
cd BackEnd/HailongConsulting.API
dotnet list package
```

### 前端依赖

主要 npm 包：
- vue@3
- vue-router@4
- pinia
- element-plus
- axios
- echarts

查看完整依赖：
```bash
cd hailong-admin
npm list --depth=0
```

---

## 🐛 常见问题

### 问题 1: 端口被占用

**错误信息**: `Address already in use`

**解决方案**:

Windows:
```bash
# 查找占用端口的进程
netstat -ano | findstr :5000
# 结束进程
taskkill /PID <进程ID> /F
```

Linux/Mac:
```bash
# 查找占用端口的进程
lsof -i :5000
# 结束进程
kill -9 <进程ID>
```

### 问题 2: 数据库连接失败

**检查清单**:
1. MySQL 服务是否启动
2. 数据库是否已创建
3. 用户名密码是否正确
4. 端口号是否正确（默认3306）

**测试连接**:
```bash
mysql -h localhost -P 3306 -u root -p
```

### 问题 3: npm install 失败

**解决方案**:

1. 清除缓存：
```bash
npm cache clean --force
```

2. 删除 node_modules 和 package-lock.json：
```bash
rm -rf node_modules package-lock.json
npm install
```

3. 使用国内镜像：
```bash
npm config set registry https://registry.npmmirror.com
```

### 问题 4: dotnet restore 失败

**解决方案**:

1. 清除 NuGet 缓存：
```bash
dotnet nuget locals all --clear
```

2. 使用国内镜像：
```bash
dotnet nuget add source https://nuget.cdn.azure.cn/v3/index.json -n huawei
```

---

## 📊 项目结构

```
Protral/
├── BackEnd/
│   └── HailongConsulting.API/          # 后端 API 项目
│       ├── Controllers/                 # 控制器
│       ├── Services/                    # 业务逻辑
│       ├── Repositories/                # 数据访问
│       ├── Models/                      # 数据模型
│       ├── Common/                      # 公共类
│       ├── Middleware/                  # 中间件
│       └── Program.cs                   # 启动文件
│
├── hailong-admin/                       # 前端管理系统
│   ├── src/
│   │   ├── api/                        # API 接口
│   │   ├── components/                 # 组件
│   │   ├── views/                      # 页面
│   │   ├── router/                     # 路由
│   │   ├── stores/                     # 状态管理
│   │   └── utils/                      # 工具函数
│   ├── public/                         # 静态资源
│   └── package.json                    # 依赖配置
│
├── API_INTEGRATION_GUIDE.md            # API 对接指南
├── API_TEST_GUIDE.md                   # API 测试指南
└── QUICK_START.md                      # 快速启动指南（本文件）
```

---

## 🎯 开发流程

### 1. 创建新功能

#### 后端开发流程

1. **创建实体类** (`Models/Entities/`)
2. **创建 DTO** (`Models/DTOs/`)
3. **创建仓储接口和实现** (`Repositories/`)
4. **创建服务接口和实现** (`Services/`)
5. **创建控制器** (`Controllers/`)
6. **配置 AutoMapper** (`Common/MappingProfile.cs`)
7. **注册服务** (`Program.cs`)

#### 前端开发流程

1. **创建 API 接口** (`src/api/`)
2. **创建页面组件** (`src/views/`)
3. **配置路由** (`src/router/index.js`)
4. **添加菜单项** (`src/components/Sidebar.vue`)

### 2. 提交代码

```bash
# 添加更改
git add .

# 提交更改
git commit -m "feat: 添加新功能"

# 推送到远程
git push origin main
```

### 3. 代码审查

- 检查代码规范
- 运行测试
- 检查文档更新

---

## 📚 相关文档

- [API 对接指南](./API_INTEGRATION_GUIDE.md)
- [API 测试指南](./API_TEST_GUIDE.md)
- [后端项目说明](./BackEnd/HailongConsulting.API/README.md)
- [前端项目说明](./hailong-admin/README.md)

---

## 🔗 有用的链接

- **后端技术栈**:
  - [ASP.NET Core 文档](https://docs.microsoft.com/aspnet/core)
  - [Entity Framework Core](https://docs.microsoft.com/ef/core)
  - [AutoMapper](https://automapper.org/)

- **前端技术栈**:
  - [Vue 3 文档](https://vuejs.org/)
  - [Element Plus](https://element-plus.org/)
  - [Vite](https://vitejs.dev/)

- **数据库**:
  - [MySQL 文档](https://dev.mysql.com/doc/)

---

## 💡 开发技巧

### 1. 热重载

- **后端**: 使用 `dotnet watch run` 实现热重载
- **前端**: Vite 自动支持热重载

### 2. 调试技巧

- **后端**: 使用 VS Code 断点调试
- **前端**: 使用 Vue DevTools

### 3. 日志查看

- **后端日志**: `BackEnd/HailongConsulting.API/logs/`
- **前端日志**: 浏览器控制台

### 4. API 测试

- 使用 Swagger UI: http://localhost:5000/swagger
- 使用 Postman 或 REST Client

---

## 🎉 开始开发

现在你已经准备好开始开发了！

1. 启动服务
2. 打开浏览器访问 http://localhost:3000
3. 开始编码！

祝你开发愉快！ 🚀