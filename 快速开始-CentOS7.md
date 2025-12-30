# 🚀 海隆咨询官网 - CentOS 7 快速开始

> 5分钟快速部署指南 - 专为新手设计

## 📋 准备工作

### 1. 确认您的环境
- ✅ VMware上已安装CentOS 7 64位
- ✅ 可以SSH连接到虚拟机
- ✅ 虚拟机可以访问互联网

### 2. 获取虚拟机IP地址
```bash
# 在CentOS中执行
ip addr
# 记下显示的IP地址，例如：192.168.1.100
```

## 🎯 方式一：自动部署（推荐新手）

### 第1步：上传项目文件

**使用WinSCP上传：**
1. 下载WinSCP: https://winscp.net/
2. 连接到您的CentOS（IP、用户名root、密码）
3. 在CentOS上创建目录：`/opt/hailong/project`
4. 将整个项目文件夹上传到该目录

**或使用命令行：**
```bash
# 在CentOS中执行
mkdir -p /opt/hailong/project
cd /opt/hailong/project

# 如果项目在Git仓库
git clone <您的仓库地址> .

# 如果使用VMware共享文件夹
cp -r /mnt/hgfs/Protral/* .
```

### 第2步：上传并运行部署脚本

```bash
# 1. 将 deploy-centos7.sh 上传到 /root/ 目录

# 2. 给脚本执行权限
cd /root
chmod +x deploy-centos7.sh

# 3. 运行脚本
./deploy-centos7.sh
```

### 第3步：按提示输入信息

脚本会询问以下信息（可直接回车使用默认值）：

```
请输入MySQL root密码 (默认: Hailong@2025): [直接回车]
请输入MySQL应用密码 (默认: HailongApp@2025): [直接回车]
请输入JWT密钥 (至少32字符，默认自动生成): [直接回车]
项目文件路径 (默认: /opt/hailong/project): [直接回车]
确认开始部署? (y/n): y
```

### 第4步：等待部署完成

脚本会自动完成所有安装和配置，大约需要**10-20分钟**。

部署完成后会显示访问地址！

## 🎯 方式二：手动部署

如果自动脚本失败，请参考 `CentOS7部署指南.md` 进行手动部署。

## ✅ 验证部署

### 1. 检查服务状态

```bash
# 检查后端API
systemctl status hailong-api

# 检查Nginx
systemctl status nginx

# 检查MySQL
systemctl status mysqld
```

所有服务都应该显示 `active (running)` 状态。

### 2. 在浏览器中访问

假设您的虚拟机IP是 `192.168.1.100`：

- **前端门户**: http://192.168.1.100
- **后台管理**: http://192.168.1.100:8080
- **API测试**: http://192.168.1.100:5001/api/home/statistics

### 3. 登录后台管理

- 用户名: `admin`
- 密码: `admin123`

**⚠️ 首次登录后请立即修改密码！**

## 🔧 常用命令

```bash
# 重启所有服务
systemctl restart hailong-api
systemctl restart nginx

# 查看日志
journalctl -u hailong-api -f          # API日志
tail -f /var/log/nginx/error.log      # Nginx错误日志

# 查看端口占用
netstat -tlnp | grep :5000
netstat -tlnp | grep :8080
```

## ❓ 遇到问题？

### 问题1：无法访问网站

```bash
# 检查防火墙
firewall-cmd --list-ports

# 如果端口未开放，执行：
firewall-cmd --permanent --add-port=80/tcp
firewall-cmd --permanent --add-port=8080/tcp
firewall-cmd --permanent --add-port=5001/tcp
firewall-cmd --reload
```

### 问题2：后端API无法启动

```bash
# 查看详细错误
journalctl -u hailong-api -n 100

# 常见原因：
# - 数据库连接失败：检查MySQL是否运行
# - 端口被占用：检查5000端口
# - 配置文件错误：检查 /var/www/hailong-api/appsettings.json
```

### 问题3：前端页面空白

```bash
# 检查文件是否存在
ls -la /var/www/hailong-admin/

# 检查Nginx配置
nginx -t

# 查看Nginx错误日志
tail -f /var/log/nginx/error.log
```

### 问题4：数据库连接失败

```bash
# 测试数据库连接
mysql -u hailong_app -p hailong_consulting

# 如果无法连接，检查：
# 1. MySQL是否运行
systemctl status mysqld

# 2. 用户权限是否正确
mysql -u root -p
SHOW GRANTS FOR 'hailong_app'@'localhost';
```

## 📞 获取帮助

1. **查看详细文档**: `CentOS7部署指南.md`
2. **查看故障排查**: `TROUBLESHOOTING.md`
3. **查看部署信息**: `cat /root/hailong-deploy-info.txt`

## 🎉 部署成功后的下一步

1. ✅ 修改admin默认密码
2. ✅ 配置系统基本信息
3. ✅ 上传公司资料和图片
4. ✅ 发布第一篇文章
5. ✅ 设置数据库备份

## 📝 重要文件位置

```
/var/www/hailong-api/              # 后端API
/var/www/hailong-admin/            # 后台管理
/var/www/hailong-protral/          # 前端门户
/etc/nginx/conf.d/                 # Nginx配置
/root/hailong-deploy-info.txt      # 部署信息
```

## 🔐 安全建议

1. **立即修改默认密码**
2. **定期备份数据库**
3. **更新系统安全补丁**
4. **配置SSL证书（如有域名）**
5. **限制SSH访问（修改端口、禁用root登录）**

---

**祝您部署顺利！** 🎊

如有问题，请参考详细文档或联系技术支持。