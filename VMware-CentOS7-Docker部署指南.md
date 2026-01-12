# 🐳 海隆咨询官网 - VMware CentOS 7 Docker部署指南

> 使用Docker容器化部署，适合有Docker经验的用户

## 📋 目录

1. [Docker方案说明](#docker方案说明)
2. [准备工作](#准备工作)
3. [快速部署（推荐）](#快速部署推荐)
4. [手动部署（详细步骤）](#手动部署详细步骤)
5. [访问系统](#访问系统)
6. [Docker管理](#docker管理)
7. [迁移到云服务器](#迁移到云服务器)
8. [常见问题](#常见问题)

---

## Docker方案说明

### 容器架构

```
┌─────────────────────────────────────────────────────────┐
│                Docker Host (CentOS 7)                    │
│                                                          │
│  ┌────────────────────────────────────────────────┐    │
│  │      Nginx容器 (hailong-nginx)                 │    │
│  │      端口: 80, 8080, 5001                      │    │
│  └────────────────────────────────────────────────┘    │
│                         ↓                               │
│  ┌────────────────────────────────────────────────┐    │
│  │      后端API容器 (hailong-api)                 │    │
│  │      .NET 7.0 应用                              │    │
│  └────────────────────────────────────────────────┘    │
│                         ↓                               │
│  ┌────────────────────────────────────────────────┐    │
│  │      MySQL容器 (hailong-mysql)                 │    │
│  │      MySQL 8.0                                  │    │
│  └────────────────────────────────────────────────┘    │
│                                                          │
└─────────────────────────────────────────────────────────┘
```

### Docker vs 传统部署

| 特性 | Docker部署 | 传统部署 |
|------|-----------|---------|
| **部署脚本** | [`deploy-centos7-docker.sh`](deploy-centos7-docker.sh) | [`deploy-centos7.sh`](deploy-centos7.sh) |
| **部署难度** | ⭐⭐⭐⭐ 较复杂 | ⭐⭐ 简单 |
| **学习成本** | 高（需要了解Docker） | 低 |
| **环境隔离** | ✅ 完全隔离 | ❌ 共享系统 |
| **资源占用** | 稍高 | 较低 |
| **迁移便利** | ✅ 非常方便 | ❌ 需重新配置 |
| **版本管理** | ✅ 容易 | ❌ 困难 |
| **调试难度** | ⭐⭐⭐ 较复杂 | ⭐⭐ 简单 |
| **适合新手** | ❌ 不适合 | ✅ 适合 |
| **部署时间** | 20-40分钟 | 15-30分钟 |

### Docker的优势

✅ **环境隔离**：所有服务运行在独立容器中，不污染系统环境  
✅ **快速迁移**：打包整个应用，一键迁移到其他服务器  
✅ **版本管理**：轻松回滚到之前的版本  
✅ **一致性**：开发、测试、生产环境完全一致  
✅ **易于扩展**：可以轻松增加容器实例实现负载均衡  

### 何时选择Docker部署？

**✅ 适合Docker部署的场景：**
- 熟悉Docker和容器技术
- 需要快速迁移到其他服务器
- 多环境部署（开发、测试、生产）
- 需要环境隔离
- 团队协作开发

**❌ 不适合Docker部署的场景：**
- 不熟悉Linux和Docker
- 只需要单一环境部署
- 追求最佳性能
- 需要频繁调试
- 快速上线，不想学习新技术

**💡 建议**：如果你是新手，建议先使用[传统部署方式](VMware-CentOS7-完整部署指南.md)。

---

## 准备工作

### 1. 环境要求

- **虚拟化平台**: VMware Workstation/Player
- **操作系统**: CentOS 7 64位（全新安装）
- **内存**: 至少 4GB（推荐 8GB）
- **硬盘**: 至少 30GB 可用空间（Docker镜像需要额外空间）
- **CPU**: 2核心以上
- **网络**: 可访问互联网

### 2. 获取虚拟机IP地址

```bash
sudo ifup ens33
ip addr show
```

记录显示的IP地址，例如：`192.168.108.128`



### 3. 上传项目文件

**方式一：使用Git克隆（推荐）**

如果项目代码在Git仓库中，这是最简单的方式：

```bash
# 安装Git
yum install -y git

# 克隆项目
cd /opt/hailong
git clone <你的Git仓库地址> project

# 例如：
# git clone https://github.com/yourusername/Protral.git project
# 或私有仓库：
# git clone https://gitee.com/yourusername/Protral.git project
```

**方式二：使用WinSCP上传**

1. 下载WinSCP: https://winscp.net/
2. 连接到虚拟机（IP、用户名root、密码）
3. 在虚拟机上创建目录：
   ```bash
   mkdir -p /opt/hailong/project
   ```
4. 将整个项目文件夹上传到该目录

**方式三：使用VMware共享文件夹**

```bash
# 安装VMware Tools
yum install -y open-vm-tools

# 挂载共享文件夹
mkdir -p /mnt/hgfs
vmhgfs-fuse .host:/ /mnt/hgfs -o allow_other

# 复制项目文件
mkdir -p /opt/hailong/project
cp -r /mnt/hgfs/Protral/* /opt/hailong/project/
```

**方式四：使用wget/curl下载压缩包**

如果项目打包成压缩包：

```bash
# 下载项目压缩包
cd /opt/hailong
wget https://example.com/Protral.tar.gz

# 解压
tar -xzf Protral.tar.gz
mv Protral project

# 或使用curl
curl -L https://example.com/Protral.tar.gz -o Protral.tar.gz
tar -xzf Protral.tar.gz
mv Protral project
```

---

## 快速部署（推荐）

### 使用一键部署脚本

这是最简单的方式，脚本会自动完成所有安装和配置。

#### 第1步：上传部署脚本

将 [`deploy-centos7-docker.sh`](deploy-centos7-docker.sh) 上传到虚拟机的 `/root/` 目录。

#### 第2步：赋予执行权限

```bash
cd /root
chmod +x deploy-centos7-docker.sh
```

#### 第3步：运行部署脚本

```bash
./deploy-centos7-docker.sh
```

#### 第4步：按提示输入配置信息

```
请输入MySQL root密码 (默认: Hailong@2025): [直接回车]
请输入MySQL应用密码 (默认: HailongApp@2025): [直接回车]
请输入JWT密钥 (至少32字符，默认自动生成): [直接回车]
项目文件路径 (默认: /opt/hailong/project): [直接回车]
确认开始部署? (y/n): y
```

#### 第5步：等待部署完成

部署过程大约需要 **20-40分钟**，取决于网络速度。

脚本会自动完成：
- ✅ 安装Docker和Docker Compose
- ✅ 安装Node.js
- ✅ 构建前端项目
- ✅ 生成docker-compose.yml配置
- ✅ 构建Docker镜像
- ✅ 启动所有容器
- ✅ 配置防火墙

#### 第6步：查看部署信息

部署完成后，屏幕会显示访问地址和登录信息。

部署信息也保存在：`/root/hailong-docker-deploy-info.txt`

```bash
cat /root/hailong-docker-deploy-info.txt
```

---

## 手动部署（详细步骤）

如果自动脚本失败，或者你想了解详细过程，可以按以下步骤手动部署。

### 第一步：安装Docker

```bash
# 1. 安装依赖
yum install -y yum-utils device-mapper-persistent-data lvm2

# 2. 添加Docker仓库
yum-config-manager --add-repo https://download.docker.com/linux/centos/docker-ce.repo

# 3. 安装Docker
yum install -y docker-ce docker-ce-cli containerd.io

# 4. 启动Docker
systemctl start docker
systemctl enable docker

# 5. 验证安装
docker --version
```

### 第二步：安装Docker Compose

```bash
# 1. 下载Docker Compose
curl -L "https://github.com/docker/compose/releases/download/v2.20.0/docker-compose-$(uname -s)-$(uname -m)" -o /usr/local/bin/docker-compose

# 2. 赋予执行权限
chmod +x /usr/local/bin/docker-compose

# 3. 创建软链接
ln -sf /usr/local/bin/docker-compose /usr/bin/docker-compose

# 4. 验证安装
docker-compose --version
```

### 第三步：安装Node.js

```bash
# 1. 添加Node.js仓库
curl -fsSL https://rpm.nodesource.com/setup_18.x | bash -

# 2. 安装Node.js
yum install -y nodejs

# 3. 验证安装
node --version
npm --version
```

### 第四步：构建前端

```bash
# 1. 构建后台管理系统
cd /opt/hailong/project/hailong-admin

# 配置API地址（替换为你的IP）
cat > .env.production <<EOF
VITE_API_BASE_URL=http://192.168.108.128:5001
EOF

npm install --registry=https://registry.npmmirror.com
npm run build

# 2. 构建前端门户
cd /opt/hailong/project/hailong-protral

cat > .env.production <<EOF
VITE_API_BASE_URL=http://192.168.108.128:5001
EOF

npm install --registry=https://registry.npmmirror.com
npm run build
```

### 第五步：配置docker-compose.yml

```bash
cd /opt/hailong/project
```

确保 [`docker-compose.yml`](docker-compose.yml) 文件存在且配置正确。

关键配置项：
- MySQL密码
- JWT密钥
- 数据库连接字符串

### 第六步：启动Docker容器

```bash
cd /opt/hailong/project

# 启动所有服务（首次启动会自动构建镜像）
docker-compose up -d

# 查看启动状态
docker-compose ps

# 查看日志
docker-compose logs -f
```

### 第七步：配置防火墙

```bash
# 启动防火墙
systemctl start firewalld
systemctl enable firewalld

# 开放端口
firewall-cmd --permanent --add-port=80/tcp
firewall-cmd --permanent --add-port=8080/tcp
firewall-cmd --permanent --add-port=5001/tcp
firewall-cmd --permanent --add-port=22/tcp

# 重新加载
firewall-cmd --reload

# 查看开放的端口
firewall-cmd --list-ports
```

---

## 访问系统

### 在Windows主机上访问

假设你的虚拟机IP是 `192.168.108.128`：

#### 1. 前端门户网站
```
http://192.168.108.128
```

#### 2. 后台管理系统
```
http://192.168.108.128:8080
```

**默认登录信息：**
- 用户名：`admin`
- 密码：`admin123`

**⚠️ 重要：首次登录后请立即修改密码！**

#### 3. API接口（用于测试）
```
http://192.168.108.128:5001/api/home/statistics
```

### 验证部署是否成功

#### 1. 检查容器状态

```bash
cd /opt/hailong/project
docker-compose ps
```

所有容器都应该显示 `Up` 状态。

#### 2. 检查容器日志

```bash
# 查看所有容器日志
docker-compose logs

# 查看特定容器日志
docker-compose logs api
docker-compose logs mysql
docker-compose logs nginx
```

#### 3. 测试API接口

```bash
curl http://localhost:5001/api/home/statistics
```

应该返回JSON格式的统计数据。

#### 4. 验证数据库

```bash
# 进入MySQL容器
docker exec -it hailong-mysql mysql -u root -p

# 查看数据库
SHOW DATABASES;
USE hailong_consulting;
SHOW TABLES;
```

---

## Docker管理

### 容器管理命令

```bash
# 进入项目目录
cd /opt/hailong/project

# 查看容器状态
docker-compose ps

# 启动所有服务
docker-compose up -d

# 停止所有服务
docker-compose down

# 重启所有服务
docker-compose restart

# 重启特定服务
docker-compose restart api
docker-compose restart nginx
docker-compose restart mysql

# 停止特定服务
docker-compose stop api

# 启动特定服务
docker-compose start api
```

### 查看日志

```bash
# 查看所有服务日志（实时）
docker-compose logs -f

# 查看特定服务日志
docker-compose logs -f api
docker-compose logs -f mysql
docker-compose logs -f nginx

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

### 查看资源使用

```bash
# 查看所有容器资源使用
docker stats

# 查看特定容器
docker stats hailong-api hailong-mysql hailong-nginx

# 查看Docker磁盘使用
docker system df
```

### 数据备份

#### 备份数据库

```bash
# 备份数据库
docker exec hailong-mysql mysqldump -u root -p密码 hailong_consulting > /root/backup_$(date +%Y%m%d).sql

# 压缩备份
gzip /root/backup_$(date +%Y%m%d).sql

# 恢复数据库
docker exec -i hailong-mysql mysql -u root -p密码 hailong_consulting < /root/backup.sql
```

#### 备份上传文件

```bash
# 备份上传的文件
docker run --rm -v protral_api-uploads:/data -v /root:/backup alpine tar czf /backup/uploads_$(date +%Y%m%d).tar.gz -C /data .

# 恢复上传的文件
docker run --rm -v protral_api-uploads:/data -v /root:/backup alpine tar xzf /backup/uploads.tar.gz -C /data
```

#### 自动备份脚本

```bash
# 创建备份脚本
cat > /root/backup-docker.sh <<'EOF'
#!/bin/bash
DATE=$(date +%Y%m%d_%H%M%S)
BACKUP_DIR=/backup
mkdir -p $BACKUP_DIR

# 备份数据库
docker exec hailong-mysql mysqldump -u root -p密码 hailong_consulting | gzip > $BACKUP_DIR/mysql_${DATE}.sql.gz

# 备份上传文件
docker run --rm -v protral_api-uploads:/data -v $BACKUP_DIR:/backup alpine tar czf /backup/uploads_${DATE}.tar.gz -C /data .

# 删除7天前的备份
find $BACKUP_DIR -name "*.gz" -mtime +7 -delete

echo "Backup completed: $DATE"
EOF

chmod +x /root/backup-docker.sh

# 添加定时任务（每天凌晨2点）
crontab -e
# 添加：0 2 * * * /root/backup-docker.sh >> /var/log/backup.log 2>&1
```

### 更新部署

#### 更新后端代码

```bash
cd /opt/hailong/project

# 拉取最新代码
git pull

# 重新构建并启动API
docker-compose build api
docker-compose up -d api

# 查看日志
docker-compose logs -f api
```

#### 更新前端代码

```bash
cd /opt/hailong/project

# 拉取最新代码
git pull

# 重新构建前端
cd hailong-admin
npm run build

cd ../hailong-protral
npm run build

# 重启Nginx
cd ..
docker-compose restart nginx
```

#### 完全重新部署

```bash
cd /opt/hailong/project

# 停止并删除所有容器（保留数据）
docker-compose down

# 停止并删除所有容器和数据（⚠️ 会删除数据库）
docker-compose down -v

# 重新启动
docker-compose up -d --build
```

---

## 迁移到云服务器

Docker部署的最大优势就是迁移方便！

### 方案一：使用相同的部署脚本（推荐）

**步骤：**

1. **准备云服务器**
   - 购买云服务器（阿里云、腾讯云、华为云等）
   - 选择 CentOS 7 64位系统
   - 配置：2核4GB内存起步，推荐4核8GB

2. **上传项目文件**
   ```bash
   # 在云服务器上
   mkdir -p /opt/hailong/project
   
   # 使用scp从本地上传
   scp -r /opt/hailong/project root@云服务器IP:/opt/hailong/
   ```

3. **运行部署脚本**
   ```bash
   cd /root
   chmod +x deploy-centos7-docker.sh
   ./deploy-centos7-docker.sh
   ```

4. **配置域名（如果有）**
   - 在域名服务商处添加A记录
   - 修改Nginx配置
   - 申请SSL证书

### 方案二：导出导入Docker镜像

**步骤：**

1. **在虚拟机上导出镜像**
   ```bash
   # 导出API镜像
   docker save protral-api:latest | gzip > hailong-api.tar.gz
   
   # 导出数据
   docker exec hailong-mysql mysqldump -u root -p密码 hailong_consulting | gzip > db-backup.sql.gz
   ```

2. **上传到云服务器**
   ```bash
   scp hailong-api.tar.gz root@云服务器IP:/root/
   scp db-backup.sql.gz root@云服务器IP:/root/
   ```

3. **在云服务器上导入**
   ```bash
   # 导入镜像
   docker load < hailong-api.tar.gz
   
   # 启动服务
   docker-compose up -d
   
   # 恢复数据
   gunzip < db-backup.sql.gz | docker exec -i hailong-mysql mysql -u root -p密码 hailong_consulting
   ```

### 方案三：使用Docker Registry

**步骤：**

1. **推送镜像到Docker Hub或私有Registry**
   ```bash
   docker tag protral-api:latest yourusername/hailong-api:latest
   docker push yourusername/hailong-api:latest
   ```

2. **在云服务器上拉取镜像**
   ```bash
   docker pull yourusername/hailong-api:latest
   docker-compose up -d
   ```

### 云服务器额外配置

#### 1. 配置安全组

在云服务商控制台开放端口：
- 80 (HTTP)
- 443 (HTTPS)
- 8080 (后台管理)
- 5001 (API)
- 22 (SSH)

#### 2. 配置域名

```bash
# 修改Nginx配置
vim /opt/hailong/project/nginx/conf.d/default.conf

# 将 server_name _; 改为 server_name www.yourdomain.com;

# 重启Nginx容器
docker-compose restart nginx
```

#### 3. 配置SSL证书

```bash
# 安装certbot
yum install -y certbot

# 停止Nginx容器
docker-compose stop nginx

# 申请证书
certbot certonly --standalone -d www.yourdomain.com

# 修改docker-compose.yml挂载证书
# 重启Nginx
docker-compose up -d nginx
```

---

## 常见问题

### 问题1：容器无法启动

**症状**：`docker-compose ps` 显示容器状态为 `Exit`

**解决方案**：

```bash
# 查看容器日志
docker-compose logs 容器名

# 常见原因：
# - 端口被占用
netstat -tlnp | grep :80

# - 配置文件错误
docker exec hailong-nginx nginx -t

# - 依赖服务未就绪
docker-compose ps
```

### 问题2：数据库初始化失败

**症状**：API无法连接数据库，或数据库表不存在

**解决方案**：

```bash
# 查看MySQL初始化日志
docker-compose logs mysql | grep -i "entrypoint"

# 检查SQL文件
ls -lh /opt/hailong/project/SQL/

# 重新初始化（⚠️ 会删除所有数据）
docker-compose down -v
docker-compose up -d

# 实时查看初始化过程
docker-compose logs -f mysql
```

### 问题3：API无法连接数据库

**症状**：API日志显示数据库连接错误

**解决方案**：

```bash
# 检查MySQL容器是否健康
docker-compose ps mysql

# 查看MySQL日志
docker-compose logs mysql

# 测试数据库连接
docker exec -it hailong-mysql mysql -u root -p

# 检查网络连接
docker exec -it hailong-api ping mysql

# 重启API容器
docker-compose restart api
```

### 问题4：前端无法访问

**症状**：浏览器无法打开网站

**解决方案**：

```bash
# 检查Nginx配置
docker exec hailong-nginx nginx -t

# 查看Nginx日志
docker-compose logs nginx

# 检查前端文件
docker exec hailong-nginx ls -la /usr/share/nginx/html/admin
docker exec hailong-nginx ls -la /usr/share/nginx/html/portal

# 检查防火墙
firewall-cmd --list-ports

# 重启Nginx
docker-compose restart nginx
```

### 问题5：端口被占用

**症状**：启动容器时提示端口已被占用

**解决方案**：

```bash
# 查看端口占用
netstat -tlnp | grep :80
netstat -tlnp | grep :8080
netstat-tlnp | grep :5001

# 方法1：停止占用端口的进程
kill -9 进程ID

# 方法2：修改docker-compose.yml中的端口映射
# 例如将80改为8000：
# ports:
#   - "8000:80"
```

### 问题6：Docker磁盘空间不足

**症状**：无法构建镜像或启动容器

**解决方案**：

```bash
# 查看Docker磁盘使用
docker system df

# 清理未使用的镜像
docker image prune -a

# 清理未使用的容器
docker container prune

# 清理未使用的卷
docker volume prune

# 清理所有未使用的资源
docker system prune -a --volumes
```

### 问题7：容器内时间不正确

**症状**：日志时间或数据时间不对

**解决方案**：

```bash
# 在docker-compose.yml中添加时区环境变量
environment:
  - TZ=Asia/Shanghai

# 重启容器
docker-compose restart
```

---

## 🔐 安全建议

### 1. 修改默认密码

```bash
# 修改MySQL密码
docker exec -it hailong-mysql mysql -u root -p
ALTER USER 'root'@'%' IDENTIFIED BY '新密码';
ALTER USER 'hailong_app'@'%' IDENTIFIED BY '新密码';
FLUSH PRIVILEGES;

# 同步修改docker-compose.yml中的密码
vim /opt/hailong/project/docker-compose.yml

# 重启容器
docker-compose restart
```

### 2. 限制MySQL端口访问

```bash
# 编辑docker-compose.yml
vim /opt/hailong/project/docker-compose.yml

# 将MySQL端口改为仅本地访问
mysql:
  ports:
    - "127.0.0.1:3306:3306"

# 重启
docker-compose up -d
```

### 3. 定期更新镜像

```bash
# 更新基础镜像
docker pull mysql:8.0
docker pull nginx:alpine
docker pull mcr.microsoft.com/dotnet/aspnet:7.0

# 重新构建
docker-compose build --no-cache
docker-compose up -d
```

### 4. 配置资源限制

```bash
# 在docker-compose.yml中添加资源限制
services:
  api:
    deploy:
      resources:
        limits:
          cpus: '2'
          memory: 2G
        reservations:
          cpus: '1'
          memory: 1G
```

---

## 📊 性能监控

```bash
# 实时查看容器资源使用
docker stats

# 查看容器详细信息
docker inspect hailong-api

# 查看容器进程
docker top hailong-api

# 查看网络连接
docker exec hailong-api netstat -tlnp
```

---

## 📚 相关文档

- [`deploy-centos7-docker.sh`](deploy-centos7-docker.sh) - Docker一键部署脚本
- [`Docker部署-使用说明.md`](Docker部署-使用说明.md) - Docker使用说明
- [`Docker快速部署指南.md`](Docker快速部署指南.md) - Docker详细文档
- [`VMware-CentOS7-完整部署指南.md`](VMware-CentOS7-完整部署指南.md) - 传统部署指南
- [`docker-compose.yml`](docker-compose.yml) - Docker Compose配置文件

---

**祝您部署顺利！** 🎉

如有问题，请参考详细文档或联系技术支持。