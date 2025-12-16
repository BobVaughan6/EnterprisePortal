# 🔧 海龙咨询官网 - 运维手册

## 📋 目录

- [日常维护](#日常维护)
- [备份策略](#备份策略)
- [监控告警](#监控告警)
- [性能调优](#性能调优)
- [安全维护](#安全维护)
- [日志管理](#日志管理)
- [更新升级](#更新升级)
- [应急预案](#应急预案)

---

## 📅 日常维护

### 每日检查清单

```bash
#!/bin/bash
# daily_check.sh - 每日健康检查脚本

echo "=== 海龙咨询官网 - 每日健康检查 ==="
echo "检查时间: $(date)"
echo ""

# 1. 检查服务状态
echo "1. 服务状态检查"
systemctl is-active --quiet nginx && echo "✓ Nginx: 运行中" || echo "✗ Nginx: 已停止"
systemctl is-active --quiet hailong-api && echo "✓ API: 运行中" || echo "✗ API: 已停止"
systemctl is-active --quiet mysql && echo "✓ MySQL: 运行中" || echo "✗ MySQL: 已停止"
echo ""

# 2. 检查磁盘空间
echo "2. 磁盘空间检查"
df -h | grep -E "Filesystem|/$|/var"
echo ""

# 3. 检查内存使用
echo "3. 内存使用检查"
free -h
echo ""

# 4. 检查CPU负载
echo "4. CPU负载检查"
uptime
echo ""

# 5. 检查最近的错误日志
echo "5. 最近的错误日志"
echo "API错误数: $(grep -c "Error" /var/www/hailong-api/logs/error-$(date +%Y%m%d).log 2>/dev/null || echo 0)"
echo "Nginx错误数: $(grep -c "error" /var/log/nginx/error.log 2>/dev/null || echo 0)"
echo ""

# 6. 检查SSL证书有效期
echo "6. SSL证书检查"
certbot certificates 2>/dev/null | grep -A 2 "Certificate Name"
echo ""

# 7. 检查数据库连接
echo "7. 数据库连接检查"
mysql -u hailong_user -p'your_password' -e "SELECT 'Database OK' as Status;" 2>/dev/null && echo "✓ 数据库连接正常" || echo "✗ 数据库连接失败"
echo ""

echo "=== 检查完成 ==="
```

**使用方法**:
```bash
# 创建脚本
sudo nano /usr/local/bin/daily_check.sh

# 添加执行权限
sudo chmod +x /usr/local/bin/daily_check.sh

# 手动执行
sudo /usr/local/bin/daily_check.sh

# 设置定时任务（每天早上8点执行）
sudo crontab -e
# 添加：
0 8 * * * /usr/local/bin/daily_check.sh > /var/log/daily_check.log 2>&1
```

---

### 每周维护任务

#### 1. 日志清理
```bash
#!/bin/bash
# weekly_log_cleanup.sh - 每周日志清理

echo "开始清理旧日志..."

# 清理30天前的API日志
find /var/www/hailong-api/logs/ -name "*.log" -mtime +30 -delete
echo "✓ API日志清理完成"

# 清理30天前的Nginx日志
find /var/log/nginx/ -name "*.log.*" -mtime +30 -delete
echo "✓ Nginx日志清理完成"

# 清理MySQL慢查询日志
if [ -f /var/log/mysql/slow-query.log ]; then
    > /var/log/mysql/slow-query.log
    echo "✓ MySQL慢查询日志已清空"
fi

echo "日志清理完成"
```

#### 2. 数据库优化
```bash
#!/bin/bash
# weekly_db_optimize.sh - 每周数据库优化

echo "开始数据库优化..."

mysql -u root -p'your_password' hailong_consulting <<EOF
-- 分析表
ANALYZE TABLE Announcements;
ANALYZE TABLE InfoPublications;
ANALYZE TABLE Attachments;
ANALYZE TABLE SystemLogs;

-- 优化表
OPTIMIZE TABLE Announcements;
OPTIMIZE TABLE InfoPublications;
OPTIMIZE TABLE Attachments;
OPTIMIZE TABLE SystemLogs;

-- 清理90天前的系统日志
DELETE FROM SystemLogs WHERE CreatedAt < DATE_SUB(NOW(), INTERVAL 90 DAY);

-- 清理180天前的访问统计
DELETE FROM VisitStatistics WHERE VisitDate < DATE_SUB(NOW(), INTERVAL 180 DAY);
EOF

echo "数据库优化完成"
```

#### 3. 系统更新检查
```bash
#!/bin/bash
# weekly_update_check.sh - 每周系统更新检查

echo "检查系统更新..."

# 更新包列表
sudo apt update

# 列出可更新的包
echo "可更新的包:"
apt list --upgradable

# 检查安全更新
echo ""
echo "安全更新:"
sudo unattended-upgrades --dry-run
```

---

### 每月维护任务

#### 1. 完整备份
```bash
# 执行完整备份（包括数据库和文件）
sudo /usr/local/bin/backup_full.sh
```

#### 2. 性能报告
```bash
#!/bin/bash
# monthly_performance_report.sh - 每月性能报告

echo "=== 海龙咨询官网 - 月度性能报告 ==="
echo "报告时间: $(date)"
echo ""

# 1. 访问统计
echo "1. 访问统计"
mysql -u hailong_user -p'your_password' hailong_consulting -e "
SELECT 
    DATE_FORMAT(VisitDate, '%Y-%m') as Month,
    SUM(VisitCount) as TotalVisits,
    AVG(VisitCount) as AvgDailyVisits
FROM VisitStatistics
WHERE VisitDate >= DATE_SUB(CURDATE(), INTERVAL 1 MONTH)
GROUP BY DATE_FORMAT(VisitDate, '%Y-%m');
"

# 2. 内容统计
echo ""
echo "2. 内容统计"
mysql -u hailong_user -p'your_password' hailong_consulting -e "
SELECT 
    '公告总数' as Item,
    COUNT(*) as Count
FROM Announcements
UNION ALL
SELECT 
    '信息公开总数',
    COUNT(*)
FROM InfoPublications
UNION ALL
SELECT 
    '附件总数',
    COUNT(*)
FROM Attachments;
"

# 3. 系统资源使用
echo ""
echo "3. 系统资源使用"
echo "平均CPU使用率:"
sar -u 1 10 | grep Average

echo ""
echo "平均内存使用:"
free -h

echo ""
echo "磁盘使用情况:"
df -h | grep -E "Filesystem|/$|/var"

# 4. 数据库大小
echo ""
echo "4. 数据库大小"
mysql -u hailong_user -p'your_password' -e "
SELECT 
    table_name AS 'Table',
    ROUND(((data_length + index_length) / 1024 / 1024), 2) AS 'Size (MB)'
FROM information_schema.TABLES
WHERE table_schema = 'hailong_consulting'
ORDER BY (data_length + index_length) DESC;
"

echo ""
echo "=== 报告完成 ==="
```

---

## 💾 备份策略

### 自动备份脚本

#### 1. 数据库备份
```bash
#!/bin/bash
# backup_database.sh - 数据库备份脚本

# 配置
DB_USER="hailong_user"
DB_PASS="your_password"
DB_NAME="hailong_consulting"
BACKUP_DIR="/var/backups/mysql"
DATE=$(date +%Y%m%d_%H%M%S)
RETENTION_DAYS=30

# 创建备份目录
mkdir -p $BACKUP_DIR

# 执行备份
echo "开始备份数据库: $DB_NAME"
mysqldump -u $DB_USER -p$DB_PASS $DB_NAME | gzip > $BACKUP_DIR/${DB_NAME}_${DATE}.sql.gz

# 检查备份是否成功
if [ $? -eq 0 ]; then
    echo "✓ 数据库备份成功: ${DB_NAME}_${DATE}.sql.gz"
    
    # 删除旧备份
    find $BACKUP_DIR -name "${DB_NAME}_*.sql.gz" -mtime +$RETENTION_DAYS -delete
    echo "✓ 已清理 ${RETENTION_DAYS} 天前的备份"
else
    echo "✗ 数据库备份失败"
    exit 1
fi

# 显示备份文件大小
ls -lh $BACKUP_DIR/${DB_NAME}_${DATE}.sql.gz
```

#### 2. 文件备份
```bash
#!/bin/bash
# backup_files.sh - 文件备份脚本

# 配置
SOURCE_DIRS=(
    "/var/www/hailong-api/wwwroot/uploads"
    "/var/www/hailong-api/appsettings.json"
    "/etc/nginx/sites-available"
)
BACKUP_DIR="/var/backups/files"
DATE=$(date +%Y%m%d_%H%M%S)
RETENTION_DAYS=30

# 创建备份目录
mkdir -p $BACKUP_DIR

# 执行备份
echo "开始备份文件..."
tar -czf $BACKUP_DIR/files_${DATE}.tar.gz ${SOURCE_DIRS[@]}

# 检查备份是否成功
if [ $? -eq 0 ]; then
    echo "✓ 文件备份成功: files_${DATE}.tar.gz"
    
    # 删除旧备份
    find $BACKUP_DIR -name "files_*.tar.gz" -mtime +$RETENTION_DAYS -delete
    echo "✓ 已清理 ${RETENTION_DAYS} 天前的备份"
else
    echo "✗ 文件备份失败"
    exit 1
fi

# 显示备份文件大小
ls -lh $BACKUP_DIR/files_${DATE}.tar.gz
```

#### 3. 完整备份
```bash
#!/bin/bash
# backup_full.sh - 完整备份脚本

echo "=== 开始完整备份 ==="
echo "时间: $(date)"
echo ""

# 执行数据库备份
echo "1. 备份数据库..."
/usr/local/bin/backup_database.sh
echo ""

# 执行文件备份
echo "2. 备份文件..."
/usr/local/bin/backup_files.sh
echo ""

# 备份到远程服务器（可选）
# echo "3. 同步到远程服务器..."
# rsync -avz /var/backups/ user@remote-server:/backups/hailong/

echo "=== 备份完成 ==="
```

### 设置自动备份

```bash
# 编辑crontab
sudo crontab -e

# 添加定时任务
# 每天凌晨2点执行数据库备份
0 2 * * * /usr/local/bin/backup_database.sh >> /var/log/backup.log 2>&1

# 每天凌晨3点执行文件备份
0 3 * * * /usr/local/bin/backup_files.sh >> /var/log/backup.log 2>&1

# 每周日凌晨4点执行完整备份
0 4 * * 0 /usr/local/bin/backup_full.sh >> /var/log/backup.log 2>&1
```

### 备份恢复

#### 恢复数据库
```bash
# 解压备份文件
gunzip /var/backups/mysql/hailong_consulting_20240101_020000.sql.gz

# 恢复数据库
mysql -u hailong_user -p hailong_consulting < /var/backups/mysql/hailong_consulting_20240101_020000.sql

# 验证恢复
mysql -u hailong_user -p hailong_consulting -e "SHOW TABLES;"
```

#### 恢复文件
```bash
# 解压备份文件
tar -xzf /var/backups/files/files_20240101_030000.tar.gz -C /tmp/restore

# 恢复文件
sudo cp -r /tmp/restore/var/www/hailong-api/wwwroot/uploads/* /var/www/hailong-api/wwwroot/uploads/

# 修复权限
sudo chown -R www-data:www-data /var/www/hailong-api/wwwroot/uploads/
```

---

## 📊 监控告警

### 系统监控

#### 1. 服务监控脚本
```bash
#!/bin/bash
# monitor_services.sh - 服务监控脚本

# 配置
ALERT_EMAIL="admin@yourdomain.com"
LOG_FILE="/var/log/service_monitor.log"

# 检查服务函数
check_service() {
    local service=$1
    if ! systemctl is-active --quiet $service; then
        echo "[$(date)] 警告: $service 服务已停止" >> $LOG_FILE
        
        # 尝试重启服务
        systemctl start $service
        sleep 5
        
        if systemctl is-active --quiet $service; then
            echo "[$(date)] $service 服务已自动重启" >> $LOG_FILE
        else
            echo "[$(date)] 错误: $service 服务重启失败" >> $LOG_FILE
            # 发送告警邮件
            echo "$service 服务异常，请立即检查" | mail -s "服务告警" $ALERT_EMAIL
        fi
    fi
}

# 检查各个服务
check_service nginx
check_service hailong-api
check_service mysql
```

#### 2. 磁盘空间监控
```bash
#!/bin/bash
# monitor_disk.sh - 磁盘空间监控

THRESHOLD=80
ALERT_EMAIL="admin@yourdomain.com"

# 检查磁盘使用率
USAGE=$(df -h / | awk 'NR==2 {print $5}' | sed 's/%//')

if [ $USAGE -gt $THRESHOLD ]; then
    echo "警告: 磁盘使用率已达 ${USAGE}%" | mail -s "磁盘空间告警" $ALERT_EMAIL
    
    # 记录日志
    echo "[$(date)] 磁盘使用率: ${USAGE}%" >> /var/log/disk_monitor.log
    
    # 清理临时文件
    find /tmp -type f -mtime +7 -delete
    find /var/tmp -type f -mtime +7 -delete
fi
```

#### 3. 性能监控
```bash
#!/bin/bash
# monitor_performance.sh - 性能监控

LOG_FILE="/var/log/performance_monitor.log"

# 记录CPU使用率
CPU_USAGE=$(top -bn1 | grep "Cpu(s)" | awk '{print $2}' | cut -d'%' -f1)
echo "[$(date)] CPU使用率: ${CPU_USAGE}%" >> $LOG_FILE

# 记录内存使用率
MEM_USAGE=$(free | grep Mem | awk '{printf("%.2f"), $3/$2 * 100.0}')
echo "[$(date)] 内存使用率: ${MEM_USAGE}%" >> $LOG_FILE

# 记录API响应时间
API_RESPONSE=$(curl -o /dev/null -s -w '%{time_total}' https://api.yourdomain.com/api/health)
echo "[$(date)] API响应时间: ${API_RESPONSE}s" >> $LOG_FILE

# 如果响应时间超过2秒，发送告警
if (( $(echo "$API_RESPONSE > 2" | bc -l) )); then
    echo "API响应时间过长: ${API_RESPONSE}s" | mail -s "性能告警" admin@yourdomain.com
fi
```

### 设置监控定时任务

```bash
sudo crontab -e

# 每5分钟检查一次服务状态
*/5 * * * * /usr/local/bin/monitor_services.sh

# 每小时检查一次磁盘空间
0 * * * * /usr/local/bin/monitor_disk.sh

# 每10分钟记录一次性能指标
*/10 * * * * /usr/local/bin/monitor_performance.sh
```

---

## ⚡ 性能调优

### MySQL性能优化

#### 1. 配置优化
```ini
# /etc/mysql/mysql.conf.d/mysqld.cnf

[mysqld]
# 基础配置
max_connections = 200
max_allowed_packet = 64M
thread_cache_size = 8
table_open_cache = 2000

# InnoDB配置
innodb_buffer_pool_size = 1G
innodb_log_file_size = 256M
innodb_flush_log_at_trx_commit = 2
innodb_flush_method = O_DIRECT

# 查询缓存
query_cache_type = 1
query_cache_size = 64M
query_cache_limit = 2M

# 慢查询日志
slow_query_log = 1
slow_query_log_file = /var/log/mysql/slow-query.log
long_query_time = 2
```

#### 2. 索引优化
```sql
-- 分析慢查询
SELECT * FROM mysql.slow_log ORDER BY query_time DESC LIMIT 10;

-- 为常用查询添加索引
CREATE INDEX idx_type_date ON Announcements(Type, PublishDate);
CREATE INDEX idx_category_date ON InfoPublications(Category, PublishDate);

-- 分析索引使用情况
SHOW INDEX FROM Announcements;
ANALYZE TABLE Announcements;
```

### Nginx性能优化

```nginx
# /etc/nginx/nginx.conf

user www-data;
worker_processes auto;
worker_rlimit_nofile 65535;

events {
    worker_connections 4096;
    use epoll;
    multi_accept on;
}

http {
    # 基础优化
    sendfile on;
    tcp_nopush on;
    tcp_nodelay on;
    keepalive_timeout 65;
    types_hash_max_size 2048;
    
    # Gzip压缩
    gzip on;
    gzip_vary on;
    gzip_min_length 1024;
    gzip_comp_level 6;
    gzip_types text/plain text/css text/xml text/javascript 
               application/x-javascript application/xml+rss 
               application/javascript application/json;
    
    # 缓存配置
    open_file_cache max=10000 inactive=30s;
    open_file_cache_valid 60s;
    open_file_cache_min_uses 2;
    open_file_cache_errors on;
    
    # 客户端缓冲区
    client_body_buffer_size 128k;
    client_max_body_size 100m;
    client_header_buffer_size 1k;
    large_client_header_buffers 4 4k;
}
```

### ASP.NET Core性能优化

```csharp
// Program.cs

// 1. 启用响应压缩
builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
    options.Providers.Add<GzipCompressionProvider>();
});

// 2. 配置Kestrel
builder.WebHost.ConfigureKestrel(options =>
{
    options.Limits.MaxConcurrentConnections = 100;
    options.Limits.MaxConcurrentUpgradedConnections = 100;
    options.Limits.MaxRequestBodySize = 100 * 1024 * 1024; // 100MB
});

// 3. 添加内存缓存
builder.Services.AddMemoryCache();

// 4. 配置响应缓存
builder.Services.AddResponseCaching();

// 5. 使用中间件
app.UseResponseCompression();
app.UseResponseCaching();
```

---

## 🔒 安全维护

### 安全检查清单

#### 1. 系统安全
```bash
# 检查系统更新
sudo apt update
sudo apt list --upgradable

# 检查安全更新
sudo unattended-upgrades --dry-run

# 检查开放端口
sudo netstat -tlnp

# 检查防火墙状态
sudo ufw status

# 检查失败的登录尝试
sudo grep "Failed password" /var/log/auth.log | tail -20
```

#### 2. 应用安全
```bash
# 检查文件权限
ls -la /var/www/hailong-api/
ls -la /var/www/hailong-portal/
ls -la /var/www/hailong-admin/

# 检查配置文件权限
ls -l /var/www/hailong-api/appsettings.json

# 检查上传目录权限
ls -la /var/www/hailong-api/wwwroot/uploads/
```

#### 3. 数据库安全
```sql
-- 检查用户权限
SELECT User, Host, authentication_string FROM mysql.user;

-- 检查数据库权限
SHOW GRANTS FOR 'hailong_user'@'localhost';

-- 删除匿名用户
DELETE FROM mysql.user WHERE User='';

-- 删除测试数据库
DROP DATABASE IF EXISTS test;
```

### SSL证书维护

```bash
# 检查证书有效期
sudo certbot certificates

# 测试自动续期
sudo certbot renew --dry-run

# 手动续期
sudo certbot renew

# 强制续期
sudo certbot renew --force-renewal
```

---

## 📝 日志管理

### 日志轮转配置

```bash
# /etc/logrotate.d/hailong-api
/var/www/hailong-api/logs/*.log {
    daily
    rotate 30
    compress
    delaycompress
    notifempty
    create 0640 www-data www-data
    sharedscripts
    postrotate
        systemctl reload hailong-api > /dev/null 2>&1 || true
    endscript
}
```

### 日志分析

```bash
# 分析API访问日志
awk '{print $1}' /var/log/nginx/access.log | sort | uniq -c | sort -rn | head -10

# 分析错误日志
grep "Error" /var/www/hailong-api/logs/*.log | wc -l

# 分析慢查询
mysqldumpslow -s t -t 10 /var/log/mysql/slow-query.log
```

---

## 🔄 更新升级

### 应用更新流程

```bash
#!/bin/bash
# update_application.sh - 应用更新脚本

echo "=== 开始更新应用 ==="

# 1. 备份当前版本
echo "1. 备份当前版本..."
/usr/local/bin/backup_full.sh

# 2. 停止服务
echo "2. 停止服务..."
sudo systemctl stop hailong-api

# 3. 更新代码
echo "3. 更新代码..."
cd /var/www/hailong-api
sudo git pull origin main

# 4. 还原配置文件
echo "4. 还原配置文件..."
sudo cp /var/backups/appsettings.json ./appsettings.json

# 5. 重新发布
echo "5. 重新发布..."
dotnet publish -c Release -o /var/www/hailong-api

# 6. 启动服务
echo "6. 启动服务..."
sudo systemctl start hailong-api

# 7. 检查服务状态
echo "7. 检查服务状态..."
sleep 5
sudo systemctl status hailong-api

echo "=== 更新完成 ==="
```

---

## 🆘 应急预案

### 服务故障应急

```bash
# 1. 快速重启所有服务
sudo systemctl restart nginx hailong-api mysql

# 2. 如果仍然失败，回滚到备份版本
cd /var/www/hailong-api
sudo rm -rf *
sudo tar -xzf /var/backups/files/files_latest.tar.gz -C /

# 3. 恢复数据库
gunzip < /var/backups/mysql/hailong_consulting_latest.sql.gz | mysql -u root -p hailong_consulting

# 4. 重启服务
sudo systemctl restart hailong-api
```

### 数据丢失应急

```bash
# 1. 立即停止所有写操作
sudo systemctl stop hailong-api

# 2. 从最近的备份恢复
gunzip < /var/backups/mysql/hailong_consulting_latest.sql.gz | mysql -u root -p hailong_consulting

# 3. 验证数据完整性
mysql -u hailong_user -p hailong_consulting -e "SELECT COUNT(*) FROM Announcements;"

# 4. 重启服务
sudo systemctl start hailong-api
```

---

## 📞 联系方式

- **技术支持**: support@yourdomain.com
- **紧急联系**: +86 xxx-xxxx-xxxx
- **文档**: [README.md](README.md)

---

**文档维护**: 运维团队  
**最后更新**: 2024-01-01