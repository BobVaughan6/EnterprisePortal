# 🐳 Docker快速部署指南

## 📋 Docker方案架构说明

### 容器架构图

```
┌─────────────────────────────────────────────────────────┐
│                    Docker Host (CentOS 7)               │
│                                                          │
│  ┌────────────────────────────────────────────────┐    │
│  │           Nginx容器 (hailong-nginx)            │    │
│  │  端口映射:                                      │    │
│  │  - 80:80    → 前端门户                         │    │
│  │  - 8080:8080 → 后台管理                        │    │
│  │  - 5001:5001 → API代理                         │    │
│  └────────────────────────────────────────────────┘    │
│                         ↓                               │
│  ┌────────────────────────────────────────────────┐    │
│  │         后端API容器 (hailong-api)              │    │
│  │  内部端口: 5000                                 │    │
│  │  .NET 7.0 应用                                  │    │
│  └────────────────────────────────────────────────┘    │
│                         ↓                               │
│  ┌────────────────────────────────────────────────┐    │
│  │         MySQL容器 (hailong-mysql)              │    │
│  │  端口映射: 3306:3306                            │    │
│  │  数据库: hailong_consulting                     │    │
│  └────────────────────────────────────────────────┘    │
│                                                          │
└─────────────────────────────────────────────────────────┘
```

### 访问流程

1. **前端门户访问**: 
   - 浏览器 → `http://服务器IP:80` → Nginx容器 → 返回前端页面

2. **后台管理访问**: 
   - 浏览器 → `http://服务器IP:8080` → Nginx容器 → 返回后台页面

3. **API调用**: 
   - 前端 → `http://服务器IP:5001/api/xxx` → Nginx容器 → API容器(5000端口) → MySQL容器

### 是的，MySQL也在Docker里！

所有服务都运行在Docker容器中：
- ✅ MySQL数据库 → `hailong-mysql`容器
- ✅ 后端API → `hailong-api`容器  
- ✅ Nginx(前端+代理) → `hailong-nginx`容器

## 🚀 快速部署步骤

### 前提条件

1. **安装Docker和Docker Compose**

```bash
# 安装Docker
yum install -y yum-utils
yum-config-manager --add-repo https://download.docker.com/linux/centos/docker-ce.repo
yum install -y docker-ce docker-ce-cli containerd.io

# 启动Docker
systemctl start docker
systemctl enable docker

# 安装Docker Compose
curl -L "https://github.com/docker/compose/releases/download/v2.20.0/docker-compose-$(uname -s)-$(uname -m)" -o /usr/local/bin/docker-compose
chmod +x /usr/local/bin/docker-compose

# 验证安装
docker --version
docker-compose --version
```

### 第1步：准备项目文件

```bash
# 将项目上传到服务器
cd /opt/hailong/project

# 确保以下文件存在：
# - docker-compose.yml
# - BackEnd/HailongConsulting.API/Dockerfile
# - nginx/nginx.conf
# - nginx/conf.d/default.conf
```

### 第2步：构建前端

```bash
# 安装Node.js（如果还没有）
curl -fsSL https://rpm.nodesource.com/setup_18.x | bash -
yum install -y nodejs

# 构建后台管理系统
cd /opt/hailong/project/hailong-admin
npm install --registry=https://registry.npmmirror.com
npm run build

# 构建前端门户（如果有）
cd /opt/hailong/project/hailong-protral
npm install --registry=https://registry.npmmirror.com
npm run build
```

### 第3步：启动Docker容器

```bash
cd /opt/hailong/project

# 启动所有服务（首次启动会自动构建镜像）
docker-compose up -d

# 查看启动状态
docker-compose ps

# 查看日志
docker-compose logs -f
```

### 第4步：验证部署

```bash
# 检查容器状态（应该都是Up状态）
docker-compose ps

# 输出示例：
# NAME              STATUS          PORTS
# hailong-mysql     Up 2 minutes    0.0.0.0:3306->3306/tcp
# hailong-api       Up 1 minute     0.0.0.0:5000->5000/tcp
# hailong-nginx     Up 1 minute     0.0.0.0:80->80/tcp, 0.0.0.0:8080->8080/tcp, 0.0.0.0:5001->5001/tcp
```

### 第5步：访问系统

在浏览器中访问（假设服务器IP是192.168.1.100）：

- **前端门户**: http://192.168.1.100
- **后台管理**: http://192.168.1.100:8080
- **API测试**: http://192.168.1.100:5001/api/home/statistics

默认登录：
- 用户名: `admin`
- 密码: `admin123`

## 🔧 Docker常用命令

### 容器管理

```bash
# 查看运行的容器
docker-compose ps

# 查看所有容器（包括停止的）
docker ps -a

# 启动所有服务
docker-compose up -d

# 停止所有服务
docker-compose down

# 重启某个服务
docker-compose restart api
docker-compose restart nginx
docker-compose restart mysql

# 停止某个服务
docker-compose stop api

# 启动某个服务
docker-compose start api
```

### 日志查看

```bash
# 查看所有服务日志
docker-compose logs -f

# 查看特定服务日志
docker-compose logs -f api
docker-compose logs -f nginx
docker-compose logs -f mysql

# 查看最近100行日志
docker-compose logs --tail=100 api
```

### 进入容器

```bash
# 进入API容器
docker exec -it hailong-api bash

# 进入MySQL容器
docker exec -it hailong-mysql bash

# 进入Nginx容器
docker exec -it hailong-nginx sh

# 在MySQL容器中执行SQL
docker exec -it hailong-mysql mysql -u root -p
```

### 数据管理

```bash
# 查看数据卷
docker volume ls

# 查看数据卷详情
docker volume inspect protral_mysql-data

# 备份MySQL数据
docker exec hailong-mysql mysqldump -u root -pHailong@2025 hailong_consulting > backup.sql

# 恢复MySQL数据
docker exec -i hailong-mysql mysql -u root -pHailong@2025 hailong_consulting < backup.sql
```

### 清理和重建

```bash
# 停止并删除所有容器
docker-compose down

# 停止并删除所有容器和数据卷（⚠️ 会删除数据库数据）
docker-compose down -v

# 重新构建并启动
docker-compose up -d --build

# 仅重新构建API
docker-compose build api
docker-compose up -d api
```

## 🔍 故障排查

### 问题1：容器无法启动

```bash
# 查看容器状态
docker-compose ps

# 查看详细日志
docker-compose logs api

# 常见原因：
# - 端口被占用
# - 配置文件错误
# - 依赖服务未就绪
```

### 问题2：API无法连接数据库

```bash
# 检查MySQL容器是否运行
docker-compose ps mysql

# 进入API容器测试连接
docker exec -it hailong-api bash
apt-get update && apt-get install -y mysql-client
mysql -h mysql -u hailong_app -pHailongApp@2025 hailong_consulting
```

### 问题3：前端无法访问

```bash
# 检查Nginx配置
docker exec hailong-nginx nginx -t

# 查看Nginx日志
docker-compose logs nginx

# 检查前端文件是否存在
docker exec hailong-nginx ls -la /usr/share/nginx/html/admin
docker exec hailong-nginx ls -la /usr/share/nginx/html/portal
```

### 问题4：端口被占用

```bash
# 查看端口占用
netstat -tlnp | grep :80
netstat -tlnp | grep :8080
netstat -tlnp | grep :5001

# 修改docker-compose.yml中的端口映射
# 例如将80改为8000：
# ports:
#   - "8000:80"
```

## 📊 性能监控

```bash
# 查看容器资源使用
docker stats

# 查看特定容器资源使用
docker stats hailong-api hailong-mysql hailong-nginx

# 查看容器详细信息
docker inspect hailong-api
```

## 🔄 更新部署

### 更新后端

```bash
cd /opt/hailong/project

# 拉取最新代码
git pull

# 重新构建并启动API
docker-compose build api
docker-compose up -d api

# 查看日志确认启动成功
docker-compose logs -f api
```

### 更新前端

```bash
# 重新构建前端
cd /opt/hailong/project/hailong-admin
npm run build

# 重启Nginx容器以加载新文件
docker-compose restart nginx
```

## 🔒 安全建议

1. **修改默认密码**
   - 修改MySQL密码
   - 修改JWT密钥
   - 修改admin登录密码

2. **配置防火墙**
```bash
firewall-cmd --permanent --add-port=80/tcp
firewall-cmd --permanent --add-port=8080/tcp
firewall-cmd --permanent --add-port=5001/tcp
firewall-cmd --reload
```

3. **定期备份**
```bash
# 创建备份脚本
cat > /root/backup-docker.sh <<'EOF'
#!/bin/bash
DATE=$(date +%Y%m%d_%H%M%S)
docker exec hailong-mysql mysqldump -u root -pHailong@2025 hailong_consulting | gzip > /backup/mysql_${DATE}.sql.gz
EOF

chmod +x /root/backup-docker.sh

# 添加定时任务
crontab -e
# 添加：0 2 * * * /root/backup-docker.sh
```

## ⚖️ Docker vs 传统部署对比

| 特性 | Docker部署 | 传统部署 |
|------|-----------|---------|
| **部署难度** | ⭐⭐⭐⭐ | ⭐⭐ |
| **学习成本** | 高 | 低 |
| **环境隔离** | ✅ 完全隔离 | ❌ 共享系统 |
| **资源占用** | 稍高 | 较低 |
| **迁移便利** | ✅ 非常方便 | ❌ 需重新配置 |
| **版本管理** | ✅ 容易 | ❌ 困难 |
| **适合新手** | ❌ 不适合 | ✅ 适合 |

## 💡 建议

**如果您：**
- ✅ 熟悉Docker → 使用Docker部署
- ✅ 需要快速迁移 → 使用Docker部署
- ✅ 多环境部署 → 使用Docker部署
- ❌ 不熟悉Linux → 使用传统部署
- ❌ 不了解Docker → 使用传统部署

---

**Docker部署完成！** 🎉

所有服务都在容器中运行，互不干扰，便于管理和维护。