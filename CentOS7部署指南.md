# 海隆咨询官网 - CentOS 7 完整部署指南

> 本指南专为不熟悉Linux的用户编写，每一步都有详细说明

## 📋 准备工作

### 1. 连接到您的CentOS 7服务器

使用SSH工具（如PuTTY或Xshell）连接到您的VMware虚拟机：
- IP地址：您的虚拟机IP（可以在虚拟机中输入 `ip addr` 查看）
- 用户名：root
- 密码：您设置的密码

## 🚀 第一步：安装所有必需软件

### 1.1 更新系统并安装基础工具

```bash
# 更新系统（这可能需要几分钟）
yum update -y

# 安装基础工具
yum install -y wget curl git unzip vim
```

### 1.2 安装.NET 7.0 运行时

```bash
# 添加Microsoft软件源
rpm -Uvh https://packages.microsoft.com/config/centos/7/packages-microsoft-prod.rpm

# 安装.NET 7.0运行时
yum install -y aspnetcore-runtime-7.0

# 验证安装
dotnet --version
```

### 1.3 安装MySQL 8.0

```bash
# 下载MySQL仓库
wget https://dev.mysql.com/get/mysql80-community-release-el7-3.noarch.rpm

# 安装MySQL仓库
rpm -ivh mysql80-community-release-el7-3.noarch.rpm

# 安装MySQL服务器
yum install -y mysql-server

# 启动MySQL
systemctl start mysqld
systemctl enable mysqld

# 查看MySQL临时密码（记下这个密码）
grep 'temporary password' /var/log/mysqld.log
```

### 1.4 安装Nginx

```bash
# 安装EPEL仓库
yum install -y epel-release

# 安装Nginx
yum install -y nginx

# 启动Nginx
systemctl start nginx
systemctl enable nginx
```

### 1.5 安装Node.js（用于构建前端）

```bash
# 安装Node.js 18
curl -fsSL https://rpm.nodesource.com/setup_18.x | bash -
yum install -y nodejs

# 验证安装
node --version
npm --version
```

## 🔧 第二步：配置MySQL数据库

### 2.1 修改MySQL root密码

```bash
# 使用临时密码登录MySQL（输入刚才记下的临时密码）
mysql -u root -p

# 在MySQL命令行中执行以下命令（将your_new_password改为您的新密码）
ALTER USER 'root'@'localhost' IDENTIFIED BY 'your_new_password';
FLUSH PRIVILEGES;
EXIT;
```

### 2.2 创建数据库和用户

```bash
# 重新登录MySQL（使用新密码）
mysql -u root -p

# 在MySQL中执行以下命令
```

```sql
-- 创建数据库
CREATE DATABASE hailong_consulting CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;

-- 创建应用用户（将your_app_password改为您的应用密码）
CREATE USER 'hailong_app'@'localhost' IDENTIFIED BY 'your_app_password';

-- 授予权限
GRANT ALL PRIVILEGES ON hailong_consulting.* TO 'hailong_app'@'localhost';
FLUSH PRIVILEGES;

-- 退出
EXIT;
```

## 📦 第三步：上传项目文件

### 3.1 创建项目目录

```bash
# 创建项目目录
mkdir -p /opt/hailong
cd /opt/hailong
```

### 3.2 上传文件（三种方式任选一种）

**方式一：使用WinSCP或FileZilla上传**
1. 下载WinSCP: https://winscp.net/
2. 连接到您的服务器
3. 将整个项目文件夹上传到 `/opt/hailong/`

**方式二：使用Git克隆（如果代码在Git仓库）**
```bash
cd /opt/hailong
git clone <您的仓库地址> project
```

**方式三：从Windows共享文件夹复制**
```bash
# 如果您在VMware中设置了共享文件夹
# 假设共享文件夹挂载在/mnt/hgfs/
cp -r /mnt/hgfs/Protral /opt/hailong/project
```

## 🗄️ 第四步：导入数据库

```bash
# 进入SQL目录（根据您的实际路径调整）
cd /opt/hailong/project/SQL

# 导入数据库结构（如果有schema文件）
mysql -u root -p hailong_consulting < hailong_consulting_schema.sql

# 导入初始数据（如果有data文件）
mysql -u root -p hailong_consulting < hailong_consulting_init_data.sql

# 验证导入
mysql -u root -p -e "USE hailong_consulting; SHOW TABLES;"
```

**如果没有SQL文件，数据库会在首次运行时自动创建表结构**

## 🔨 第五步：部署后端API

### 5.1 创建部署目录

```bash
mkdir -p /var/www/hailong-api
```

### 5.2 发布后端应用

```bash
# 进入后端项目目录
cd /opt/hailong/project/BackEnd/HailongConsulting.API

# 安装.NET SDK（用于构建）
yum install -y dotnet-sdk-7.0

# 发布应用
dotnet publish -c Release -o /var/www/hailong-api

# 设置权限
chmod -R 755 /var/www/hailong-api
```

### 5.3 配置应用

```bash
# 编辑配置文件
vim /var/www/hailong-api/appsettings.json
```

按 `i` 进入编辑模式，修改以下内容：

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3306;Database=hailong_consulting;User=hailong_app;Password=your_app_password;CharSet=utf8mb4;"
  },
  "Jwt": {
    "Key": "your-super-secret-key-at-least-32-characters-long",
    "Issuer": "HailongConsulting.API",
    "Audience": "HailongConsulting.Client",
    "ExpireHours": 24
  }
}
```

按 `ESC`，然后输入 `:wq` 保存退出

### 5.4 创建系统服务

```bash
# 创建服务文件
vim /etc/systemd/system/hailong-api.service
```

输入以下内容：

```ini
[Unit]
Description=Hailong Consulting API
After=network.target

[Service]
Type=notify
WorkingDirectory=/var/www/hailong-api
ExecStart=/usr/bin/dotnet /var/www/hailong-api/HailongConsulting.API.dll
Restart=always
RestartSec=10
KillSignal=SIGINT
SyslogIdentifier=hailong-api
Environment=ASPNETCORE_ENVIRONMENT=Production
Environment=DOTNET_PRINT_TELEMETRY_MESSAGE=false
Environment=ASPNETCORE_URLS=http://localhost:5000

[Install]
WantedBy=multi-user.target
```

保存退出后启动服务：

```bash
# 重新加载systemd
systemctl daemon-reload

# 启动服务
systemctl start hailong-api

# 设置开机自启
systemctl enable hailong-api

# 检查状态
systemctl status hailong-api

# 如果有错误，查看日志
journalctl -u hailong-api -n 50
```

## 🌐 第六步：部署前端

### 6.1 构建后台管理系统

```bash
# 进入后台管理项目
cd /opt/hailong/project/hailong-admin

# 安装依赖（这可能需要几分钟）
npm install

# 修改API地址
vim .env.production
```

修改为：
```
VITE_API_BASE_URL=http://您的服务器IP:5000
```

```bash
# 构建项目
npm run build

# 部署到Nginx目录
mkdir -p /var/www/hailong-admin
cp -r dist/* /var/www/hailong-admin/
```

### 6.2 构建前端门户（如果有）

```bash
# 如果有前端门户项目
cd /opt/hailong/project/hailong-protral

# 安装依赖
npm install

# 修改API地址
vim .env.production

# 构建
npm run build

# 部署
mkdir -p /var/www/hailong-protral
cp -r dist/* /var/www/hailong-protral/
```

## 🔧 第七步：配置Nginx

### 7.1 配置后端API代理

```bash
vim /etc/nginx/conf.d/hailong-api.conf
```

输入以下内容：

```nginx
server {
    listen 5001;
    server_name _;

    location / {
        proxy_pass http://localhost:5000;
        proxy_http_version 1.1;
        proxy_set_header Upgrade $http_upgrade;
        proxy_set_header Connection keep-alive;
        proxy_set_header Host $host;
        proxy_set_header X-Real-IP $remote_addr;
        proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
        proxy_set_header X-Forwarded-Proto $scheme;
        proxy_cache_bypass $http_upgrade;
    }

    location /uploads/ {
        alias /var/www/hailong-api/wwwroot/uploads/;
        expires 30d;
    }
}
```

### 7.2 配置后台管理系统

```bash
vim /etc/nginx/conf.d/hailong-admin.conf
```

```nginx
server {
    listen 8080;
    server_name _;

    root /var/www/hailong-admin;
    index index.html;

    location / {
        try_files $uri $uri/ /index.html;
    }

    location ~* \.(jpg|jpeg|png|gif|ico|css|js|svg|woff|woff2|ttf|eot)$ {
        expires 30d;
    }
}
```

### 7.3 配置前端门户（如果有）

```bash
vim /etc/nginx/conf.d/hailong-protral.conf
```

```nginx
server {
    listen 80;
    server_name _;

    root /var/www/hailong-protral;
    index index.html;

    location / {
        try_files $uri $uri/ /index.html;
    }

    location ~* \.(jpg|jpeg|png|gif|ico|css|js|svg|woff|woff2|ttf|eot)$ {
        expires 30d;
    }
}
```

### 7.4 重启Nginx

```bash
# 测试配置
nginx -t

# 重启Nginx
systemctl restart nginx

# 检查状态
systemctl status nginx
```

## 🔥 第八步：配置防火墙

```bash
# 安装firewalld（如果没有）
yum install -y firewalld
systemctl start firewalld
systemctl enable firewalld

# 开放端口
firewall-cmd --permanent --add-port=80/tcp      # 前端门户
firewall-cmd --permanent --add-port=8080/tcp    # 后台管理
firewall-cmd --permanent --add-port=5001/tcp    # API
firewall-cmd --permanent --add-port=22/tcp      # SSH

# 重新加载防火墙
firewall-cmd --reload

# 查看开放的端口
firewall-cmd --list-ports
```

## ✅ 第九步：验证部署

### 9.1 检查服务状态

```bash
# 检查后端API
systemctl status hailong-api

# 检查Nginx
systemctl status nginx

# 检查MySQL
systemctl status mysqld
```

### 9.2 测试访问

在您的Windows浏览器中访问：

1. **后台管理系统**: `http://您的虚拟机IP:8080`
2. **API接口**: `http://您的虚拟机IP:5001/api/home/statistics`
3. **前端门户**: `http://您的虚拟机IP`

### 9.3 默认登录信息

- 用户名: `admin`
- 密码: `admin123`（首次登录后请修改）

## 🔍 常见问题排查

### 问题1：无法访问网站

```bash
# 检查防火墙
firewall-cmd --list-ports

# 检查Nginx是否运行
systemctl status nginx

# 查看Nginx错误日志
tail -f /var/log/nginx/error.log
```

### 问题2：后端API报错

```bash
# 查看API日志
journalctl -u hailong-api -n 100

# 查看应用日志
tail -f /var/www/hailong-api/logs/*.txt
```

### 问题3：数据库连接失败

```bash
# 测试数据库连接
mysql -u hailong_app -p hailong_consulting

# 检查MySQL状态
systemctl status mysqld
```

### 问题4：前端页面空白

```bash
# 检查前端文件是否存在
ls -la /var/www/hailong-admin/

# 检查Nginx配置
nginx -t

# 查看浏览器控制台错误（F12）
```

## 📝 快速命令参考

```bash
# 重启所有服务
systemctl restart hailong-api
systemctl restart nginx
systemctl restart mysqld

# 查看日志
journalctl -u hailong-api -f          # API日志
tail -f /var/log/nginx/error.log      # Nginx错误日志
tail -f /var/log/nginx/access.log     # Nginx访问日志

# 查看端口占用
netstat -tlnp | grep :5000
netstat -tlnp | grep :8080

# 查看系统资源
top                    # 实时资源监控
df -h                  # 磁盘使用
free -h                # 内存使用
```

## 🎯 下一步操作

1. **修改默认密码**：登录后台管理系统后立即修改admin密码
2. **配置域名**：如果有域名，修改Nginx配置中的server_name
3. **配置SSL**：为网站添加HTTPS证书
4. **设置备份**：配置数据库和文件的定期备份

## 📞 需要帮助？

如果遇到问题：

1. 查看本文档的"常见问题排查"部分
2. 查看 `TROUBLESHOOTING.md` 文件
3. 收集错误日志并寻求技术支持

---

**部署完成！** 🎉

现在您可以通过浏览器访问系统了。记得修改默认密码并做好数据备份！