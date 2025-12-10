# API 对接测试指南

## 📋 测试前准备

### 1. 环境要求

**后端环境**:
- .NET 7.0 SDK
- MySQL 8.0+
- Visual Studio 2022 或 VS Code

**前端环境**:
- Node.js 16+
- npm 或 yarn

### 2. 数据库准备

确保 MySQL 数据库已创建并配置：

```sql
CREATE DATABASE hailong_consulting CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
```

### 3. 配置文件检查

**后端配置** (`BackEnd/HailongConsulting.API/appsettings.json`):
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3306;Database=hailong_consulting;User=root;Password=你的密码;CharSet=utf8mb4;"
  }
}
```

**前端配置** (`hailong-admin/.env.development`):
```env
VITE_API_BASE_URL=http://localhost:5000
```

---

## 🚀 启动服务

### 启动后端服务

```bash
# 进入后端目录
cd BackEnd/HailongConsulting.API

# 运行数据库迁移（首次运行）
dotnet ef database update

# 启动服务
dotnet run
```

服务启动后访问: `http://localhost:5000`

Swagger 文档: `http://localhost:5000/swagger`

### 启动前端服务

```bash
# 进入前端目录
cd hailong-admin

# 安装依赖（首次运行）
npm install

# 启动开发服务器
npm run dev
```

服务启动后访问: `http://localhost:3000`

---

## 🧪 API 测试步骤

### 测试 1: 认证接口测试

#### 1.1 测试登录接口

**使用 Postman/cURL**:

```bash
curl -X POST http://localhost:5000/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{
    "username": "admin",
    "password": "admin123"
  }'
```

**预期响应**:
```json
{
  "success": true,
  "message": "登录成功",
  "data": {
    "userId": 1,
    "username": "admin",
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
    "refreshToken": "...",
    "expiresAt": "2024-01-02T10:00:00Z"
  }
}
```

**前端测试**:

1. 打开浏览器访问 `http://localhost:3000`
2. 输入用户名和密码
3. 点击登录按钮
4. 检查是否成功跳转到首页

#### 1.2 测试获取当前用户信息

```bash
curl -X GET http://localhost:5000/api/auth/me \
  -H "Authorization: Bearer YOUR_TOKEN_HERE"
```

---

### 测试 2: 首页统计接口测试

#### 2.1 测试统计概览

```bash
curl -X GET http://localhost:5000/api/home/statistics/overview
```

**预期响应**:
```json
{
  "success": true,
  "data": {
    "totalProjects": 0,
    "totalAmount": 0,
    "projectTypes": [],
    "regionRanking": []
  }
}
```

**前端测试**:

1. 登录后进入首页
2. 检查统计卡片是否正常显示
3. 检查图表是否正常渲染

#### 2.2 测试最新公告

```bash
curl -X GET http://localhost:5000/api/home/recent-announcements
```

#### 2.3 测试重要业绩

```bash
curl -X GET http://localhost:5000/api/home/achievements
```

---

### 测试 3: 系统配置接口测试

#### 3.1 测试轮播图管理

**获取轮播图列表**:
```bash
curl -X GET http://localhost:5000/api/config/banners
```

**创建轮播图**:
```bash
curl -X POST http://localhost:5000/api/config/banners \
  -H "Authorization: Bearer YOUR_TOKEN" \
  -H "Content-Type: application/json" \
  -d '{
    "title": "测试轮播图",
    "description": "这是一个测试",
    "imageId": 1,
    "linkUrl": "https://example.com",
    "sortOrder": 1,
    "status": true
  }'
```

**前端测试**:

1. 进入"系统配置" -> "轮播图管理"
2. 点击"新增轮播图"
3. 填写表单并提交
4. 检查列表是否更新
5. 测试编辑和删除功能

#### 3.2 测试企业简介

```bash
curl -X GET http://localhost:5000/api/config/company-intro
```

#### 3.3 测试友情链接

**获取友情链接**:
```bash
curl -X GET http://localhost:5000/api/config/links
```

**创建友情链接**:
```bash
curl -X POST http://localhost:5000/api/config/links \
  -H "Authorization: Bearer YOUR_TOKEN" \
  -H "Content-Type: application/json" \
  -d '{
    "name": "测试链接",
    "url": "https://example.com",
    "description": "测试描述",
    "sortOrder": 1,
    "status": true
  }'
```

**前端测试**:

1. 进入"系统配置" -> "友情链接"
2. 测试增删改查功能
3. 检查排序功能
4. 检查状态切换

---

### 测试 4: 公告管理接口测试

#### 4.1 测试政府采购公告

**获取列表**:
```bash
curl -X GET "http://localhost:5000/api/announcement?businessType=GOV_PROCUREMENT&pageNumber=1&pageSize=10"
```

**创建公告**:
```bash
curl -X POST http://localhost:5000/api/announcement \
  -H "Authorization: Bearer YOUR_TOKEN" \
  -H "Content-Type: application/json" \
  -d '{
    "title": "测试公告",
    "businessType": "GOV_PROCUREMENT",
    "noticeType": "招标公告",
    "projectName": "测试项目",
    "projectRegion": "北京市",
    "publishTime": "2024-01-01T10:00:00Z",
    "content": "这是测试内容"
  }'
```

**前端测试**:

1. 进入"公告管理" -> "政府采购"
2. 测试搜索功能
3. 测试筛选功能
4. 测试新增公告
5. 测试编辑和删除

#### 4.2 测试建设工程公告

```bash
curl -X GET "http://localhost:5000/api/announcement?businessType=CONSTRUCTION&pageNumber=1&pageSize=10"
```

---

### 测试 5: 信息发布接口测试

#### 5.1 测试公司新闻

**获取列表**:
```bash
curl -X GET "http://localhost:5000/api/info-publication?category=COMPANY_NEWS&pageNumber=1&pageSize=10"
```

**创建新闻**:
```bash
curl -X POST http://localhost:5000/api/info-publication \
  -H "Authorization: Bearer YOUR_TOKEN" \
  -H "Content-Type: application/json" \
  -d '{
    "title": "测试新闻",
    "category": "COMPANY_NEWS",
    "summary": "新闻摘要",
    "content": "新闻内容...",
    "author": "管理员",
    "publishTime": "2024-01-01T10:00:00Z"
  }'
```

**前端测试**:

1. 进入"信息发布" -> "公司新闻"
2. 测试富文本编辑器
3. 测试图片上传
4. 测试附件上传
5. 测试发布和预览

#### 5.2 测试其他分类

- 通知公告 (NOTICE)
- 政策法规 (POLICY)
- 政策解读 (POLICY_INFO)

---

### 测试 6: 文件上传测试

#### 6.1 测试图片上传

```bash
curl -X POST http://localhost:5000/api/attachment/upload \
  -H "Authorization: Bearer YOUR_TOKEN" \
  -F "file=@/path/to/image.jpg" \
  -F "category=IMAGE"
```

**前端测试**:

1. 在任何需要上传图片的地方测试
2. 检查上传进度显示
3. 检查上传成功后的预览
4. 测试删除功能

#### 6.2 测试文档上传

```bash
curl -X POST http://localhost:5000/api/attachment/upload \
  -H "Authorization: Bearer YOUR_TOKEN" \
  -F "file=@/path/to/document.pdf" \
  -F "category=DOCUMENT"
```

---

### 测试 7: 全局搜索测试

```bash
curl -X GET "http://localhost:5000/api/search?keyword=测试&pageNumber=1&pageSize=10"
```

**前端测试**:

1. 在顶部搜索框输入关键词
2. 检查搜索结果显示
3. 测试结果分类
4. 测试点击跳转

---

## ✅ 测试检查清单

### 功能测试

- [ ] 用户登录/登出
- [ ] Token 自动刷新
- [ ] 权限验证
- [ ] 首页统计数据显示
- [ ] 轮播图管理（增删改查）
- [ ] 企业简介管理
- [ ] 重要业绩管理
- [ ] 企业荣誉管理
- [ ] 友情链接管理
- [ ] 政府采购公告管理
- [ ] 建设工程公告管理
- [ ] 公司新闻管理
- [ ] 通知公告管理
- [ ] 政策法规管理
- [ ] 图片上传
- [ ] 文档上传
- [ ] 全局搜索
- [ ] 访问统计

### 性能测试

- [ ] 列表加载速度
- [ ] 图片上传速度
- [ ] 大文件上传
- [ ] 并发请求处理

### 兼容性测试

- [ ] Chrome 浏览器
- [ ] Firefox 浏览器
- [ ] Edge 浏览器
- [ ] Safari 浏览器（Mac）

### 错误处理测试

- [ ] 网络错误提示
- [ ] 401 未授权处理
- [ ] 403 权限不足提示
- [ ] 404 资源不存在
- [ ] 500 服务器错误
- [ ] 表单验证错误

---

## 🐛 常见问题排查

### 问题 1: 无法连接到后端服务

**检查项**:
1. 后端服务是否正常启动
2. 端口 5000 是否被占用
3. 防火墙是否阻止连接
4. CORS 配置是否正确

**解决方案**:
```bash
# 检查端口占用
netstat -ano | findstr :5000

# 重启后端服务
cd BackEnd/HailongConsulting.API
dotnet run
```

### 问题 2: 登录失败

**检查项**:
1. 数据库是否有用户数据
2. 密码是否正确
3. JWT 配置是否正确

**解决方案**:
```sql
-- 检查用户表
SELECT * FROM users;

-- 如果没有用户，创建默认管理员
INSERT INTO users (username, password_hash, full_name, email, role, status)
VALUES ('admin', 'hashed_password', '管理员', 'admin@hailong.com', 'Admin', 1);
```

### 问题 3: Token 过期

**检查项**:
1. Token 有效期配置
2. 刷新 Token 机制

**解决方案**:
- 前端会自动处理 Token 过期，跳转到登录页
- 可以在 `appsettings.json` 中调整 Token 有效期

### 问题 4: 文件上传失败

**检查项**:
1. 文件大小限制
2. 文件类型限制
3. 上传目录权限

**解决方案**:
```csharp
// 在 Program.cs 中配置文件大小限制
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 104857600; // 100MB
});
```

### 问题 5: CORS 错误

**检查项**:
1. CORS 策略配置
2. 请求头设置

**解决方案**:
```csharp
// 确保 Program.cs 中有 CORS 配置
app.UseCors("AllowAll");
```

---

## 📊 测试报告模板

### 测试环境

- 操作系统: Windows 11
- 浏览器: Chrome 120
- 后端版本: v1.0.0
- 前端版本: v1.0.0

### 测试结果

| 功能模块 | 测试项 | 状态 | 备注 |
|---------|--------|------|------|
| 认证模块 | 登录 | ✅ | 正常 |
| 认证模块 | 登出 | ✅ | 正常 |
| 首页统计 | 数据展示 | ✅ | 正常 |
| 轮播图管理 | 增删改查 | ✅ | 正常 |
| ... | ... | ... | ... |

### 发现的问题

1. **问题描述**: xxx
   - **严重程度**: 高/中/低
   - **复现步骤**: xxx
   - **预期结果**: xxx
   - **实际结果**: xxx

### 测试结论

- 通过率: 95%
- 主要问题: xxx
- 建议: xxx

---

## 🔧 调试技巧

### 后端调试

1. **查看日志**:
```bash
# 日志文件位置
BackEnd/HailongConsulting.API/logs/log-YYYYMMDD.txt
```

2. **使用 Swagger 测试**:
访问 `http://localhost:5000/swagger` 进行接口测试

3. **数据库查询**:
```sql
-- 查看最近的错误日志
SELECT * FROM system_logs WHERE level = 'Error' ORDER BY created_at DESC LIMIT 10;
```

### 前端调试

1. **浏览器开发者工具**:
   - Network 标签: 查看 API 请求
   - Console 标签: 查看错误信息
   - Vue DevTools: 查看组件状态

2. **查看请求详情**:
```javascript
// 在 request.js 中添加调试日志
console.log('Request:', config)
console.log('Response:', response)
```

3. **检查 Token**:
```javascript
// 在浏览器控制台执行
localStorage.getItem('hailong_admin_token')
```

---

## 📝 测试数据准备

### 创建测试用户

```sql
INSERT INTO users (username, password_hash, full_name, email, role, status, created_at, updated_at)
VALUES 
('admin', 'hashed_password', '管理员', 'admin@hailong.com', 'Admin', 1, NOW(), NOW()),
('editor', 'hashed_password', '编辑员', 'editor@hailong.com', 'Editor', 1, NOW(), NOW());
```

### 创建测试数据

```sql
-- 轮播图
INSERT INTO carousel_banners (title, description, image_id, link_url, sort_order, status, created_at, updated_at)
VALUES ('测试轮播图', '这是测试', 1, 'https://example.com', 1, 1, NOW(), NOW());

-- 友情链接
INSERT INTO friendly_links (name, url, description, sort_order, status, created_at, updated_at)
VALUES ('测试链接', 'https://example.com', '测试描述', 1, 1, NOW(), NOW());
```

---

## 🎯 下一步

测试完成后：

1. 记录所有发现的问题
2. 创建 Issue 跟踪
3. 修复问题并重新测试
4. 更新文档
5. 准备上线部署

---

## 📞 技术支持

如遇到问题，请：

1. 查看本测试指南
2. 查看 API_INTEGRATION_GUIDE.md
3. 联系开发团队