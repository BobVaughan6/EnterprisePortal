# 海隆咨询后台管理系统 - 启动指南

## 环境要求

- Node.js >= 16.0.0
- npm >= 8.0.0

## 安装步骤

### 1. 进入项目目录

```bash
cd hailong-admin
```

### 2. 安装依赖

```bash
npm install
```

如果安装速度较慢，可以使用国内镜像：

```bash
npm install --registry=https://registry.npmmirror.com
```

### 3. 配置后端 API 地址

编辑 `.env.development` 文件，确保后端 API 地址正确：

```env
VITE_API_BASE_URL=http://localhost:5000
```

> 注意：将 `http://localhost:5000` 替换为实际的后端 API 地址

### 4. 启动开发服务器

```bash
npm run dev
```

启动成功后，浏览器访问：http://localhost:3000

### 5. 登录系统

默认登录信息（请根据实际后端配置修改）：
- 用户名: `admin`
- 密码: `123456`

## 生产环境构建

### 1. 配置生产环境 API

编辑 `.env.production` 文件：

```env
VITE_API_BASE_URL=https://api.yourdomain.com
```

### 2. 执行构建

```bash
npm run build
```

构建完成后，产物位于 `dist/` 目录。

### 3. 部署到服务器

将 `dist/` 目录下的所有文件上传到 Web 服务器（如 Nginx）。

#### Nginx 配置示例

```nginx
server {
    listen 80;
    server_name admin.yourdomain.com;
    
    root /var/www/hailong-admin/dist;
    index index.html;
    
    # 处理 Vue Router 的 History 模式
    location / {
        try_files $uri $uri/ /index.html;
    }
    
    # API 代理（可选，如果需要）
    location /api {
        proxy_pass http://localhost:5000;
        proxy_set_header Host $host;
        proxy_set_header X-Real-IP $remote_addr;
    }
}
```

## 常见问题

### 1. 依赖安装失败

**问题**: npm install 报错或卡住

**解决方案**:
```bash
# 清除 npm 缓存
npm cache clean --force

# 删除 node_modules 和 package-lock.json
rm -rf node_modules package-lock.json

# 重新安装
npm install
```

### 2. 启动后无法访问后端 API

**问题**: 登录或请求接口时报 404 或网络错误

**检查事项**:
1. 确认后端服务已启动
2. 检查 `.env.development` 中的 API 地址是否正确
3. 检查浏览器控制台的网络请求，查看实际请求的 URL
4. 如果是跨域问题，确认后端已配置 CORS

### 3. 登录后 Token 无效

**问题**: 登录成功但后续请求返回 401

**检查事项**:
1. 查看浏览器 LocalStorage 是否存储了 Token
2. 检查后端返回的 Token 格式是否正确
3. 确认后端 JWT 配置正确

### 4. 富文本编辑器无法上传图片

**问题**: 编辑器上传图片失败

**解决方案**:
1. 检查后端是否实现了 `/api/upload/image` 接口
2. 修改 `src/components/RichEditor.vue` 中的上传配置

### 5. 构建后部署访问空白

**问题**: 部署到服务器后页面空白

**解决方案**:
1. 检查浏览器控制台是否有报错
2. 确认 Nginx 配置了 `try_files` 规则支持 History 路由
3. 检查资源路径是否正确

## 开发模式热更新

项目使用 Vite，支持热模块替换（HMR），修改代码后浏览器会自动刷新。

如果 HMR 不生效，尝试：
```bash
# 重启开发服务器
Ctrl + C
npm run dev
```

## 目录结构说明

```
hailong-admin/
├── src/
│   ├── api/              # API 接口（重点关注）
│   ├── components/       # 公共组件
│   ├── views/           # 页面组件（主要开发区域）
│   ├── router/          # 路由配置
│   ├── stores/          # 状态管理
│   └── utils/           # 工具函数
├── .env.development     # 开发环境变量（需配置）
├── .env.production      # 生产环境变量（需配置）
└── vite.config.js       # Vite 配置
```

## 技术支持

如遇到问题，请：
1. 查看本文档的常见问题部分
2. 检查浏览器控制台的错误信息
3. 查看后端日志
4. 联系项目开发者

## 更新日志

### v1.0.0 (2024-12-03)
- 初始版本发布
- 完成基础功能模块
- 对接后端 API

---

**祝您使用愉快！**
