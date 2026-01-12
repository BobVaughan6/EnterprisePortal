# 🚀 海隆咨询官网 - Ubuntu 22.04 部署指南

> 支持Ubuntu 22.04 LTS，提供传统部署和Docker部署两种方式

## 📋 目录

1. [环境说明](#环境说明)
2. [部署方式选择](#部署方式选择)
3. [传统部署](#传统部署)
4. [Docker部署](#docker部署)
5. [访问系统](#访问系统)
6. [常见问题](#常见问题)

---

## 环境说明

### 系统要求

- **操作系统**: Ubuntu 22.04 LTS (Jammy Jellyfish)
- **内存**: 至少 4GB（推荐 8GB）
- **硬盘**: 至少 20GB 可用空间
- **CPU**: 2核心以上
- **网络**: 可访问互联网

### 支持的环境

- ✅ 物理服务器
- ✅ 云服务器（阿里云、腾讯云、AWS等）
- ✅ VMware虚拟机
- ✅ VirtualBox虚拟机
- ✅ WSL2 (Windows Subsystem for Linux)

---

## 部署方式选择

### 方式一：传统部署（推荐新手）

**特点**：
- ✅ 简单直观，易于理解
- ✅ 性能开销小
- ✅ 调试方便
- ✅ 直接访问文件系统

**适合场景**：
- 不熟悉Docker
- 只需要单一环境部署
- 追求最佳性能
- 需要频繁调试

**部署脚本**: [`deploy-ubuntu22.sh`](deploy-ubuntu22.sh)

### 方式二：Docker部署（推荐有经验用户）

**特点**：
- ✅ 环境隔离，不污染系统
- ✅ 易于迁移和扩展
- ✅ 版本管理方便
- ✅ 一键启动所有服务

**适合场景**：
- 熟悉Docker和容器技术
- 需要快速迁移到其他服务器
- 多环境部署（开发、测试、生产）
- 需要环境隔离

**部署脚本**: [`deploy-ubuntu22-docker.sh`](deploy-ubuntu22-docker.sh)

### 对比表格

| 特性 | 传统部署 | Docker部署 |
|------|---------|-----------|
| **部署脚本** | [`deploy-ubuntu22.sh`](deploy-ubuntu22.sh) | [`deploy-ubuntu22-docker.sh`](deploy-ubuntu22-docker.sh) |
| **难度** | ⭐⭐ 简单 | ⭐⭐⭐⭐ 较复杂 |
| **适合新手** | ✅ 推荐 | ❌ 不推荐 |
| **环境隔离** | ❌ 否 | ✅ 是 |
| **迁移便利** | ❌ 困难 | ✅ 容易 |
| **部署时间** | 15-30分钟 | 20-40分钟 |

---

## 传统部署

### 准备工作

#### 1. 获取服务器IP

```bash
ip addr show
```

记录显示的IP地址，例如：`192.168.1.100`

#### 2. 获取项目文件

**方式一：使用Git克隆（推荐）**

```bash
# 安装Git
sudo apt update
sudo apt install -y git

# 克隆项目
sudo mkdir -p /opt/hailong
cd /opt/hailong
sudo git clone https://github.com/BobVaughan6/EnterprisePortal.git project
```

**方式二：使用SCP上传**

```bash
# 在本地Windows/Mac上执行
scp -r /path/to/Protral root@服务器IP:/opt/hailong/project
```

### 快速部署

#### 第1步：进入项目目录

```Bash
cd /opt/hailong/project
```


#### 第2步：赋予执行权限

直接对项目根目录下的脚本进行授权：

```Bash
sudo chmod +x deploy-ubuntu22.sh
```

#### 第3步：运行部署脚本

由于脚本在当前目录，直接执行即可：

```Bash
sudo ./deploy-ubuntu22.sh
```

#### 第4步：按提示输入配置

```
请输入MySQL root密码 (默认: Hailong@2025): [回车]
请输入MySQL应用密码 (默认: HailongApp@2025): [回车]
请输入JWT密钥 (至少32字符，默认自动生成): [回车]
项目文件路径 (默认: /opt/hailong/project): [回车]
确认开始部署? (y/n): y
```

#### 第5步：等待部署完成

部署过程约需 **15-30分钟**。

脚本会自动完成：
- ✅ 系统更新
- ✅ 安装.NET 7.0运行时
- ✅ 安装MySQL 8.0
- ✅ 安装Nginx
- ✅ 安装Node.js
- ✅ 创建数据库
- ✅ 部署后端API
- ✅ 构建并部署前端
- ✅ 配置Nginx
- ✅ 配置防火墙

### 验证部署

```bash
# 检查服务状态
sudo systemctl status hailong-api
sudo systemctl status nginx
sudo systemctl status mysql

# 测试API
curl http://localhost:5001/api/home/statistics
```

---

## Docker部署

### 准备工作

#### 1. 获取服务器IP

```bash
ip addr show
```

#### 2. 获取项目文件

同传统部署方式。

### 快速部署

#### 第1步：进入项目目录

```Bash
cd /opt/hailong/project
```


#### 第2步：赋予执行权限

直接对项目根目录下的脚本进行授权：

```Bash
sudo chmod +x deploy-ubuntu22-docker.sh
```

#### 第3步：运行部署脚本

由于脚本在当前目录，直接执行即可：

```Bash
sudo ./deploy-ubuntu22-docker.sh
```

#### 第4步：按提示输入配置

同传统部署。

#### 第5步：等待部署完成

部署过程约需 **20-40分钟**。

脚本会自动完成：
- ✅ 安装Docker和Docker Compose
- ✅ 安装Node.js
- ✅ 构建前端项目
- ✅ 生成docker-compose.yml配置
- ✅ 构建并启动所有Docker容器
- ✅ 配置防火墙

### Docker管理命令

```bash
# 进入项目目录
cd /opt/hailong/project

# 查看容器状态
docker-compose ps

# 查看日志
docker-compose logs -f

# 重启服务
docker-compose restart

# 停止服务
docker-compose down

# 启动服务
docker-compose up -d
```

---

## 访问系统

### 在浏览器中访问

假设服务器IP是 `192.168.1.100`：

#### 1. 前端门户
```
http://192.168.1.100
```

#### 2. 后台管理
```
http://192.168.1.100:8080
```

**默认登录信息：**
- 用户名：`admin`
- 密码：`admin123`

**⚠️ 重要：首次登录后请立即修改密码！**

#### 3. API接口
```
http://192.168.1.100:5001/api/home/statistics
```

---

## 常见问题

### 问题1：无法访问网站

**解决方案**：

```bash
# 检查防火墙
sudo ufw status

# 开放端口
sudo ufw allow 80/tcp
sudo ufw allow 8080/tcp
sudo ufw allow 5001/tcp
sudo ufw reload

# 检查服务状态
sudo systemctl status nginx
sudo systemctl status hailong-api
```

### 问题2：后端API无法启动

**解决方案**：

```bash
# 查看详细错误
sudo journalctl -u hailong-api -n 100

# 检查MySQL
sudo systemctl status mysql

# 检查端口占用
sudo netstat -tlnp | grep :5000
```

### 问题3：Docker容器无法启动

**解决方案**：

```bash
# 查看容器日志
docker-compose logs 容器名

# 检查Docker服务
sudo systemctl status docker

# 重新构建
docker-compose build --no-cache
docker-compose up -d
```

### 问题4：npm install失败

**解决方案**：

```bash
# 使用国内镜像
npm config set registry https://registry.npmmirror.com

# 清除缓存
npm cache clean --force
npm install
```

### 问题5：权限问题

**解决方案**：

```bash
# 确保使用sudo或root用户
sudo ./deploy-ubuntu22.sh

# 检查文件权限
ls -la /var/www/hailong-api/
```

---

## 🔧 常用命令

### 传统部署

```bash
# 重启服务
sudo systemctl restart hailong-api
sudo systemctl restart nginx
sudo systemctl restart mysql

# 查看日志
sudo journalctl -u hailong-api -f
sudo tail -f /var/log/nginx/error.log

# 查看端口
sudo netstat -tlnp | grep :5000
sudo netstat -tlnp | grep :8080
```

### Docker部署

```bash
# 进入项目目录
cd /opt/hailong/project

# 容器管理
docker-compose ps
docker-compose logs -f
docker-compose restart
docker-compose down
docker-compose up -d

# 进入容器
docker exec -it hailong-api bash
docker exec -it hailong-mysql bash
```

---

## 🔐 安全建议

1. **修改默认密码**
   - 修改admin账户密码
   - 修改MySQL root密码

2. **配置防火墙**
   ```bash
   sudo ufw enable
   sudo ufw allow 22/tcp
   sudo ufw allow 80/tcp
   sudo ufw allow 8080/tcp
   sudo ufw allow 5001/tcp
   ```

3. **定期备份**
   ```bash
   # 备份数据库
   mysqldump -u root -p hailong_consulting > backup.sql
   
   # 备份上传文件
   tar -czf uploads-backup.tar.gz /var/www/hailong-api/wwwroot/uploads/
   ```

4. **更新系统**
   ```bash
   sudo apt update
   sudo apt upgrade -y
   ```

---

## 📚 相关文档

- [`deploy-ubuntu22.sh`](deploy-ubuntu22.sh) - Ubuntu传统部署脚本
- [`deploy-ubuntu22-docker.sh`](deploy-ubuntu22-docker.sh) - Ubuntu Docker部署脚本
- [`deploy-centos7.sh`](deploy-centos7.sh) - CentOS 7传统部署脚本
- [`deploy-centos7-docker.sh`](deploy-centos7-docker.sh) - CentOS 7 Docker部署脚本
- [`VMware-CentOS7-完整部署指南.md`](VMware-CentOS7-完整部署指南.md) - CentOS 7详细指南
- [`VMware-CentOS7-Docker部署指南.md`](VMware-CentOS7-Docker部署指南.md) - CentOS 7 Docker指南

---

## 🎯 Ubuntu vs CentOS

### 主要区别

| 特性 | Ubuntu 22.04 | CentOS 7 |
|------|-------------|----------|
| **包管理器** | apt | yum |
| **防火墙** | ufw | firewalld |
| **服务管理** | systemd | systemd |
| **默认Shell** | bash | bash |
| **更新频率** | 较快 | 较慢 |
| **社区支持** | 活跃 | 活跃 |

### 命令对比

| 操作 | Ubuntu | CentOS |
|------|--------|--------|
| **更新系统** | `apt update && apt upgrade` | `yum update` |
| **安装软件** | `apt install package` | `yum install package` |
| **防火墙** | `ufw allow 80/tcp` | `firewall-cmd --add-port=80/tcp` |
| **查看服务** | `systemctl status service` | `systemctl status service` |

---

**祝您部署顺利！** 🎉

如有问题，请参考详细文档或联系技术支持。