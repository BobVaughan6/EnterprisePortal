#!/bin/bash

###############################################################################
# 海隆咨询官网 - CentOS 7 一键部署脚本
# 使用方法: chmod +x deploy-centos7.sh && sudo ./deploy-centos7.sh
###############################################################################

set -e  # 遇到错误立即退出

# 颜色定义
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
NC='\033[0m' 

# 打印函数
print_info() { echo -e "${GREEN}[INFO]${NC} $1"; }
print_warn() { echo -e "${YELLOW}[WARN]${NC} $1"; }
print_error() { echo -e "${RED}[ERROR]${NC} $1"; }
print_step() {
    echo -e "\n${GREEN}========================================${NC}"
    echo -e "${GREEN}$1${NC}"
    echo -e "${GREEN}========================================${NC}\n"
}

# 检查是否为root用户
if [ "$EUID" -ne 0 ]; then 
    print_error "请使用root用户运行此脚本"
    exit 1
fi

# 获取服务器IP
SERVER_IP=$(ip addr | grep 'inet ' | grep -v '127.0.0.1' | awk '{print $2}' | cut -d/ -f1 | head -n1)

print_step "欢迎使用海隆咨询官网一键部署脚本 (修复版)"
echo "服务器IP: $SERVER_IP"

# 询问用户配置
read -p "请输入MySQL root密码 (默认: Hailong@2025): " MYSQL_ROOT_PASSWORD
MYSQL_ROOT_PASSWORD=${MYSQL_ROOT_PASSWORD:-Hailong@2025}

read -p "请输入MySQL应用密码 (默认: HailongApp@2025): " MYSQL_APP_PASSWORD
MYSQL_APP_PASSWORD=${MYSQL_APP_PASSWORD:-HailongApp@2025}

read -p "请输入JWT密钥 (默认自动生成): " JWT_SECRET
if [ -z "$JWT_SECRET" ]; then JWT_SECRET=$(openssl rand -base64 32); fi

read -p "项目文件路径 (默认: /opt/hailong/project): " PROJECT_PATH
PROJECT_PATH=${PROJECT_PATH:-/opt/hailong/project}

echo ""
read -p "确认开始部署? (y/n): " CONFIRM
if [ "$CONFIRM" != "y" ]; then print_warn "部署已取消"; exit 0; fi

###############################################################################
# 第一步：修复 YUM 源（解决 CentOS 7 停服问题）
###############################################################################
print_step "第一步：修复 YUM 源并安装基础软件"

print_info "正在更换阿里云镜像源..."
mv /etc/yum.repos.d/CentOS-Base.repo /etc/yum.repos.d/CentOS-Base.repo.backup || true
curl -o /etc/yum.repos.d/CentOS-Base.repo https://mirrors.aliyun.com/repo/Centos-7.repo

print_info "清理并刷新 YUM 缓存..."
yum clean all
yum makecache

print_info "安装基础工具..."
yum install -y wget curl git unzip vim net-tools

###############################################################################
# 第二步：安装 .NET 7.0
###############################################################################
print_step "第二步：安装 .NET 7.0 运行时"

if ! command -v dotnet &> /dev/null; then
    rpm -Uvh --force https://packages.microsoft.com/config/centos/7/packages-microsoft-prod.rpm || true
    yum install -y aspnetcore-runtime-7.0 dotnet-sdk-7.0
else
    print_info ".NET 已安装: $(dotnet --version)"
fi

###############################################################################
# 第三步：安装 MySQL 8.0 (修复 GPG 密钥和冲突问题)
###############################################################################
print_step "第三步：安装 MySQL 8.0"

if ! command -v mysql &> /dev/null; then
    print_info "配置 MySQL 仓库..."
    wget -N https://dev.mysql.com/get/mysql80-community-release-el7-3.noarch.rpm
    # 使用 -Uvh --force 解决已经安装旧版本导致的冲突
    rpm -Uvh --force mysql80-community-release-el7-3.noarch.rpm || true
    
    print_info "导入最新 GPG 密钥..."
    rpm --import https://repo.mysql.com/RPM-GPG-KEY-mysql-2023 || true
    
    print_info "安装 MySQL 服务 (跳过 GPG 校验)..."
    yum install -y mysql-community-server --nogpgcheck
    
    print_info "启动 MySQL..."
    systemctl start mysqld
    systemctl enable mysqld
    
    # 自动处理临时密码
    TEMP_PASSWORD=$(grep 'temporary password' /var/log/mysqld.log | awk '{print $NF}')
    if [ -n "$TEMP_PASSWORD" ]; then
        print_info "修改 root 初始密码..."
        mysql --connect-expired-password -u root -p"$TEMP_PASSWORD" <<EOF
ALTER USER 'root'@'localhost' IDENTIFIED BY '$MYSQL_ROOT_PASSWORD';
FLUSH PRIVILEGES;
EOF
    fi
else
    print_info "MySQL 已存在，跳过安装"
fi

###############################################################################
# 第四步：创建数据库和用户 (增强幂等性)
###############################################################################
print_step "第四步：创建数据库和用户"

# 使用 --force 强制执行，防止因用户已存在导致脚本退出
mysql -u root -p"$MYSQL_ROOT_PASSWORD" --force <<EOF
CREATE DATABASE IF NOT EXISTS hailong_consulting CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
DROP USER IF EXISTS 'hailong_app'@'localhost';
CREATE USER 'hailong_app'@'localhost' IDENTIFIED BY '$MYSQL_APP_PASSWORD';
GRANT ALL PRIVILEGES ON hailong_consulting.* TO 'hailong_app'@'localhost';
FLUSH PRIVILEGES;
EOF

print_info "数据库创建成功"

###############################################################################
# 第五步：安装Nginx
###############################################################################
print_step "第五步：安装Nginx"

if ! command -v nginx &> /dev/null; then
    print_info "安装EPEL仓库..."
    yum install -y epel-release
    
    print_info "安装Nginx..."
    yum install -y nginx
    
    print_info "启动Nginx..."
    systemctl start nginx
    systemctl enable nginx
else
    print_info "Nginx已安装"
fi

###############################################################################
# 第六步：安装Node.js
###############################################################################
print_step "第六步：安装Node.js"

if ! command -v node &> /dev/null; then
    print_info "安装 Node.js 16 (CentOS 7 兼容版本)..."
    curl -sL https://rpm.nodesource.com/setup_16.x | bash -
    yum install -y nodejs
    
    print_info "Node.js版本: $(node --version)"
    print_info "npm版本: $(npm --version)"
else
    print_info "Node.js已安装，版本: $(node --version)"
fi

###############################################################################
# 第七步：部署后端API
###############################################################################
print_step "第七步：部署后端API"

if [ ! -d "$PROJECT_PATH" ]; then
    print_error "项目路径不存在: $PROJECT_PATH"
    print_info "请先将项目文件上传到服务器"
    exit 1
fi

print_info "创建部署目录..."
mkdir -p /var/www/hailong-api

print_info "发布后端应用..."
cd "$PROJECT_PATH/BackEnd/HailongConsulting.API"
dotnet publish -c Release -o /var/www/hailong-api

print_info "配置应用..."
cat > /var/www/hailong-api/appsettings.json <<EOF
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3306;Database=hailong_consulting;User=hailong_app;Password=$MYSQL_APP_PASSWORD;CharSet=utf8mb4;"
  },
  "Jwt": {
    "Key": "$JWT_SECRET",
    "Issuer": "HailongConsulting.API",
    "Audience": "HailongConsulting.Client",
    "ExpireHours": 24
  },
  "Serilog": {
    "MinimumLevel": {
      "Default": "Information",
      "Override": {
        "Microsoft": "Warning",
        "Microsoft.EntityFrameworkCore": "Warning"
      }
    },
    "WriteTo": [
      {
        "Name": "File",
        "Args": {
          "path": "logs/log-.txt",
          "rollingInterval": "Day",
          "outputTemplate": "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}"
        }
      }
    ]
  },
  "AllowedHosts": "*"
}
EOF

print_info "创建systemd服务..."
cat > /etc/systemd/system/hailong-api.service <<EOF
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
EOF

print_info "启动后端服务..."
systemctl daemon-reload
systemctl start hailong-api
systemctl enable hailong-api

sleep 3
if systemctl is-active --quiet hailong-api; then
    print_info "后端API启动成功"
else
    print_error "后端API启动失败，请查看日志: journalctl -u hailong-api -n 50"
fi

###############################################################################
# 第八步：部署前端
###############################################################################
print_step "第八步：部署前端"

# 部署后台管理系统
if [ -d "$PROJECT_PATH/hailong-admin" ]; then
    print_info "构建后台管理系统..."
    cd "$PROJECT_PATH/hailong-admin"
    
    # 配置API地址
    cat > .env.production <<EOF
VITE_API_BASE_URL=http://$SERVER_IP:5001
EOF
    
    print_info "安装依赖..."
    npm install --registry=https://registry.npmmirror.com
    
    print_info "构建项目..."
    npm run build
    
    print_info "部署到Nginx..."
    mkdir -p /var/www/hailong-admin
    cp -r dist/* /var/www/hailong-admin/
    
    print_info "后台管理系统部署成功"
else
    print_warn "未找到后台管理系统目录"
fi

# 部署前端门户
if [ -d "$PROJECT_PATH/hailong-protral" ]; then
    print_info "构建前端门户..."
    cd "$PROJECT_PATH/hailong-protral"
    
    cat > .env.production <<EOF
VITE_API_BASE_URL=http://$SERVER_IP:5001
EOF
    
    npm install --registry=https://registry.npmmirror.com
    npm run build
    
    mkdir -p /var/www/hailong-protral
    cp -r dist/* /var/www/hailong-protral/
    
    print_info "前端门户部署成功"
fi

###############################################################################
# 第九步：配置Nginx
###############################################################################
print_step "第九步：配置Nginx"

print_info "配置后端API代理..."
cat > /etc/nginx/conf.d/hailong-api.conf <<'EOF'
server {
    listen 5001;
    server_name _;

    client_max_body_size 100M;

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
        proxy_connect_timeout 60s;
        proxy_send_timeout 60s;
        proxy_read_timeout 60s;
    }

    location /uploads/ {
        alias /var/www/hailong-api/wwwroot/uploads/;
        expires 30d;
    }
}
EOF

print_info "配置后台管理系统..."
cat > /etc/nginx/conf.d/hailong-admin.conf <<'EOF'
server {
    listen 8080;
    server_name _;

    root /var/www/hailong-admin;
    index index.html;

    gzip on;
    gzip_vary on;
    gzip_min_length 1024;
    gzip_types text/plain text/css text/xml text/javascript application/javascript application/json;

    location / {
        try_files $uri $uri/ /index.html;
    }

    location ~* \.(jpg|jpeg|png|gif|ico|css|js|svg|woff|woff2|ttf|eot)$ {
        expires 30d;
        add_header Cache-Control "public, immutable";
    }
}
EOF

print_info "配置前端门户..."
cat > /etc/nginx/conf.d/hailong-protral.conf <<'EOF'
server {
    listen 80;
    server_name _;

    root /var/www/hailong-protral;
    index index.html;

    gzip on;
    gzip_vary on;
    gzip_min_length 1024;
    gzip_types text/plain text/css text/xml text/javascript application/javascript application/json;

    location / {
        try_files $uri $uri/ /index.html;
    }

    location ~* \.(jpg|jpeg|png|gif|ico|css|js|svg|woff|woff2|ttf|eot)$ {
        expires 30d;
        add_header Cache-Control "public, immutable";
    }
}
EOF

print_info "测试Nginx配置..."
nginx -t

print_info "重启Nginx..."
systemctl restart nginx

###############################################################################
# 第十步：配置防火墙 (增加容错)
###############################################################################
print_step "第十步：配置防火墙"

if command -v firewall-cmd &> /dev/null; then
    systemctl start firewalld || print_warn "防火墙服务启动失败，请检查系统限制"
    if systemctl is-active --quiet firewalld; then
        firewall-cmd --permanent --add-port={80,8080,5001,22}/tcp
        firewall-cmd --reload
        print_info "防火墙端口已开放"
    fi
fi

print_step "部署完成！"
echo "请查看 /root/hailong-deploy-info.txt 获取访问信息"

###############################################################################
# 部署完成
###############################################################################
print_step "部署完成！"

echo ""
echo "=========================================="
echo "  部署信息"
echo "=========================================="
echo ""
echo "服务器IP: $SERVER_IP"
echo ""
echo "访问地址："
echo "  - 前端门户:     http://$SERVER_IP"
echo "  - 后台管理:     http://$SERVER_IP:8080"
echo "  - API接口:      http://$SERVER_IP:5001"
echo ""
echo "默认登录信息："
echo "  - 用户名: admin"
echo "  - 密码: admin123"
echo ""
echo "数据库信息："
echo "  - 数据库名: hailong_consulting"
echo "  - 用户名: hailong_app"
echo "  - 密码: $MYSQL_APP_PASSWORD"
echo ""
echo "常用命令："
echo "  - 查看API状态:  systemctl status hailong-api"
echo "  - 查看API日志:  journalctl -u hailong-api -f"
echo "  - 重启API:      systemctl restart hailong-api"
echo "  - 重启Nginx:    systemctl restart nginx"
echo ""
echo "=========================================="
echo ""

print_info "请在浏览器中访问上述地址进行测试"
print_warn "首次登录后请立即修改默认密码！"

# 保存配置信息
cat > /root/hailong-deploy-info.txt <<EOF
海隆咨询官网部署信息
部署时间: $(date)
服务器IP: $SERVER_IP

访问地址:
- 前端门户: http://$SERVER_IP
- 后台管理: http://$SERVER_IP:8080
- API接口: http://$SERVER_IP:5001

数据库信息:
- MySQL Root密码: $MYSQL_ROOT_PASSWORD
- MySQL应用密码: $MYSQL_APP_PASSWORD
- 数据库名: hailong_consulting
- 用户名: hailong_app

JWT密钥: $JWT_SECRET

项目路径: $PROJECT_PATH
部署路径: /var/www/hailong-api
EOF

print_info "部署信息已保存到: /root/hailong-deploy-info.txt"