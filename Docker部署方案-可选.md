# 🐳 海隆咨询官网 - Docker部署方案（可选）

> 如果您想使用Docker部署，可以参考本文档

## ⚠️ 重要提示

**对于不熟悉Linux的用户，我们强烈建议使用传统部署方式**（参考`快速开始-CentOS7.md`）

Docker部署需要：
- 理解Docker基本概念
- 会使用Docker命令
- 会编写Dockerfile和docker-compose.yml

## 📋 前提条件

### 1. 安装Docker

```bash
# 安装Docker
yum install -y yum-utils
yum-config-manager --add-repo https://download.docker.com/linux/centos/docker-ce.repo
yum install -y docker-ce docker-ce-cli containerd.io

# 启动Docker
systemctl start docker
systemctl enable docker

# 验证安装
docker --version
```

### 2. 安装Docker Compose

```bash
# 下载Docker Compose
curl -L "https://github.com/docker/compose/releases/download/v2.20.0/docker-compose-$(uname -s)-$(uname -m)" -o /usr/local/bin/docker-compose

# 添加执行权限
chmod +x /usr/local/bin/docker-compose

# 验证安装
docker-compose --version
```

## 📦 Docker部署方案

### 方案一：使用docker-compose.yml

创建 `docker-compose.yml` 文件：

```yaml
version: '3.8'

services:
  # MySQL数据库
  mysql:
    image: mysql:8.0
    container_name: hailong-mysql
    restart: always
    environment:
      MYSQL_ROOT_PASSWORD: Hailong@2025
      MYSQL_DATABASE: hailong_consulting
      MYSQL_USER: hailong_app
      MYSQL_PASSWORD: HailongApp@2025
    volumes:
      - mysql-data:/var/lib/mysql
      - ./SQL:/docker-entrypoint-initdb.d
    ports:
      - "3306:3306"
    networks:
      - hailong-network

  # 后端API
  api:
    build:
      context: ./BackEnd/HailongConsulting.API
      dockerfile: Dockerfile
    container_name: hailong-api
    restart: always
    environment:
      - ASPNETCORE_ENVIRONMENT=Production
      - ConnectionStrings__DefaultConnection=Server=mysql;Port=3306;Database=hailong_consulting;User=hailong_app;Password=HailongApp@2025;CharSet=utf8mb4;
    ports:
      - "5000:5000"
    depends_on:
      - mysql
    networks:
      - hailong-network
    volumes:
      - api-uploads:/app/wwwroot/uploads

  # Nginx（前端+反向代理）
  nginx:
    image: nginx:alpine
    container_name: hailong-nginx
    restart: always
    ports:
      - "80:80"
      - "8080:8080"
    volumes:
      - ./nginx.conf:/etc/nginx/nginx.conf
      - ./hailong-admin/dist:/usr/share/nginx/html/admin
      - ./hailong-protral/dist:/usr/share/nginx/html/portal
    depends_on:
      - api
    networks:
      - hailong-network

networks:
  hailong-network:
    driver: bridge

volumes:
  mysql-data:
  api-uploads:
```

### 方案二：创建Dockerfile

**后端API的Dockerfile**:

```dockerfile
# BackEnd/HailongConsulting.API/Dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["HailongConsulting.API.csproj", "./"]
RUN dotnet restore "HailongConsulting.API.csproj"
COPY . .
RUN dotnet build "HailongConsulting.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HailongConsulting.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS=http://+:5000
ENTRYPOINT ["dotnet", "HailongConsulting.API.dll"]
```

### 方案三：Nginx配置

创建 `nginx.conf`:

```nginx
events {
    worker_connections 1024;
}

http {
    include /etc/nginx/mime.types;
    default_type application/octet-stream;

    # 后台管理
    server {
        listen 8080;
        server_name _;
        root /usr/share/nginx/html/admin;
        index index.html;

        location / {
            try_files $uri $uri/ /index.html;
        }
    }

    # 前端门户
    server {
        listen 80;
        server_name _;
        root /usr/share/nginx/html/portal;
        index index.html;

        location / {
            try_files $uri $uri/ /index.html;
        }

        # API代理
        location /api/ {
            proxy_pass http://api:5000/api/;
            proxy_http_version 1.1;
            proxy_set_header Upgrade $http_upgrade;
            proxy_set_header Connection keep-alive;
            proxy_set_header Host $host;
            proxy_cache_bypass $http_upgrade;
        }
    }
}
```

## 🚀 部署步骤

### 1. 准备项目文件

```bash
cd /opt/hailong/project
```

### 2. 构建前端

```bash
# 构建后台管理
cd hailong-admin
npm install
npm run build

# 构建前端门户
cd ../hailong-protral
npm install
npm run build
```

### 3. 启动Docker容器

```bash
cd /opt/hailong/project

# 启动所有服务
docker-compose up -d

# 查看运行状态
docker-compose ps

# 查看日志
docker-compose logs -f
```

### 4. 停止服务

```bash
docker-compose down
```

## 📊 Docker常用命令

```bash
# 查看运行的容器
docker ps

# 查看所有容器
docker ps -a

# 查看容器日志
docker logs hailong-api
docker logs -f hailong-api  # 实时查看

# 进入容器
docker exec -it hailong-api bash

# 重启容器
docker restart hailong-api

# 删除容器
docker rm -f hailong-api

# 查看镜像
docker images

# 删除镜像
docker rmi <image-id>
```

## ⚖️ Docker vs 传统部署对比

### Docker的优点：
- ✅ 环境隔离，不污染系统
- ✅ 易于迁移和扩展
- ✅ 版本管理方便

### Docker的缺点：
- ❌ 学习曲线陡峭
- ❌ 需要额外的资源开销
- ❌ 调试相对复杂

### 传统部署的优点：
- ✅ 简单直观，易于理解
- ✅ 性能开销小
- ✅ 调试方便

### 传统部署的缺点：
- ❌ 环境配置复杂
- ❌ 可能影响系统环境

## 💡 我的建议

**对于您的情况（不熟悉Linux），强烈建议：**

1. **首选方案**：使用传统部署（`deploy-centos7.sh`脚本）
   - 简单快速
   - 一键完成
   - 易于维护

2. **进阶方案**：等熟悉Linux后再考虑Docker
   - 先学习Docker基础
   - 理解容器概念
   - 再尝试Docker部署

## 🎯 快速决策

**如果您：**
- ❓ 不熟悉Linux → 使用传统部署
- ❓ 不了解Docker → 使用传统部署
- ❓ 只想快速部署 → 使用传统部署
- ❓ 想学习Docker → 可以尝试Docker部署

---

**建议：先用传统方式部署成功，系统跑起来后，再慢慢学习Docker！**