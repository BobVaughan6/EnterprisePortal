# 海隆咨询官网后台管理系统

基于 Vue 3 + Element Plus 开发的现代化后台管理系统。

## 技术栈

- **前端框架**: Vue 3 (Composition API)
- **构建工具**: Vite 5
- **UI 组件库**: Element Plus
- **状态管理**: Pinia
- **路由管理**: Vue Router 4
- **HTTP 客户端**: Axios
- **富文本编辑器**: WangEditor 5

## 项目结构

```
hailong-admin/
├── public/                 # 静态资源
├── src/
│   ├── api/               # API 接口封装
│   │   ├── request.js     # Axios 封装（拦截器、错误处理）
│   │   ├── auth.js        # 认证相关 API
│   │   ├── config.js      # 系统配置 API
│   │   ├── govProcurement.js   # 政府采购 API
│   │   ├── construction.js     # 建设工程 API
│   │   ├── infoPublish.js      # 信息发布 API
│   │   └── index.js       # API 统一导出
│   ├── components/        # 公共组件
│   │   ├── Sidebar.vue    # 侧边栏导航
│   │   ├── Header.vue     # 顶部导航栏
│   │   └── RichEditor.vue # 富文本编辑器
│   ├── config/            # 配置文件
│   │   └── api.config.js  # API 配置
│   ├── router/            # 路由配置
│   │   └── index.js       # 路由定义 + 权限守卫
│   ├── stores/            # Pinia 状态管理
│   │   └── user.js        # 用户状态
│   ├── utils/             # 工具函数
│   │   └── auth.js        # Token 存储工具
│   ├── views/             # 页面组件
│   │   ├── Login.vue      # 登录页
│   │   ├── Home.vue       # 首页看板
│   │   ├── Layout.vue     # 主框架布局
│   │   ├── announcements/ # 公告管理
│   │   ├── info-publish/  # 信息发布
│   │   ├── config/        # 系统配置
│   │   └── system/        # 系统管理
│   ├── App.vue            # 根组件
│   ├── main.js            # 入口文件
│   └── style.css          # 全局样式
├── .env.development       # 开发环境配置
├── .env.production        # 生产环境配置
├── index.html             # HTML 模板
├── package.json           # 项目依赖
├── vite.config.js         # Vite 配置
└── README.md              # 项目说明
```

## 快速开始

### 1. 安装依赖

```bash
cd hailong-admin
npm install
```

### 2. 配置后端 API 地址

编辑 `.env.development` 文件，修改后端 API 地址：

```env
VITE_API_BASE_URL=http://localhost:5000
```

### 3. 启动开发服务器

```bash
npm run dev
```

访问 http://localhost:3000

### 4. 构建生产版本

```bash
npm run build
```

构建产物位于 `dist/` 目录。

## 功能模块

### 1. 用户认证

- ✅ 登录（用户名/密码）
- ✅ Token 自动管理（localStorage）
- ✅ 请求拦截器自动添加 Authorization 头
- ✅ 401 自动跳转登录页
- ✅ 修改密码

### 2. 公告管理

- ✅ 政府采购公告（增删改查、分页、搜索）
- ✅ 建设工程公告（增删改查、分页、搜索）

### 3. 信息发布

- ✅ 公司公告
- ✅ 政策法规
- ✅ 政策信息
- ✅ 通知公告
- ✅ 支持富文本编辑
- ✅ 文件附件上传

### 4. 系统配置

- ✅ 轮播图管理
- ✅ 企业信息管理
- ✅ 业绩展示
- ✅ 友情链接

### 5. 系统管理

- ✅ 修改密码
- ✅ 用户信息展示

## API 调用示例

### 登录

```javascript
import { authApi } from '@/api'

const login = async () => {
  const res = await authApi.login({
    username: 'admin',
    password: '123456'
  })
  
  if (res.success) {
    console.log('登录成功', res.data)
    // res.data 包含: { token, refreshToken, userId, username, fullName, email, role }
  }
}
```

### 获取政府采购公告列表

```javascript
import { govProcurementApi } from '@/api'

const getList = async () => {
  const res = await govProcurementApi.getAnnouncements({
    title: '关键字',
    announcementType: '招标公告',
    pageIndex: 1,
    pageSize: 10
  })
  
  if (res.success) {
    console.log('列表数据', res.data.items)
    console.log('总数', res.data.totalCount)
  }
}
```

### 创建轮播图

```javascript
import { configApi } from '@/api'

const createBanner = async () => {
  const res = await configApi.createBanner({
    title: '轮播图标题',
    imageUrl: 'https://example.com/banner.jpg',
    linkUrl: 'https://example.com',
    sortOrder: 1,
    status: true
  })
  
  if (res.success) {
    console.log('创建成功', res.data)
  }
}
```

## 后端接口对接说明

### 1. API 基础配置

后端 API 基础地址配置在 `.env.development` 和 `.env.production` 文件中：

```env
VITE_API_BASE_URL=http://localhost:5000
```

### 2. Token 认证机制

- 登录成功后，Token 自动存储到 localStorage
- 所有需要认证的请求自动添加 `Authorization: Bearer {token}` 头
- Token 过期（401）自动跳转登录页并清除本地认证信息

### 3. 响应格式

后端返回的统一格式：

```json
{
  "success": true,
  "message": "操作成功",
  "data": { ... }
}
```

### 4. 已对接的后端接口

#### 认证模块 (`/api/auth`)

- `POST /api/auth/login` - 登录
- `POST /api/auth/refresh` - 刷新 Token
- `GET /api/auth/me` - 获取当前用户信息
- `POST /api/auth/change-password` - 修改密码

#### 政府采购公告 (`/api/gov-procurement/announcements`)

- `GET /api/gov-procurement/announcements` - 获取列表
- `GET /api/gov-procurement/announcements/{id}` - 获取详情
- `POST /api/gov-procurement/announcements` - 创建公告
- `PUT /api/gov-procurement/announcements/{id}` - 更新公告
- `DELETE /api/gov-procurement/announcements/{id}` - 删除公告

#### 建设工程公告 (`/api/construction/announcements`)

- 接口结构同政府采购公告

#### 信息发布 (`/api/info-publish`)

- `GET /api/info-publish` - 分页查询
- `GET /api/info-publish/{id}` - 获取详情
- `POST /api/info-publish` - 创建信息（支持文件上传）
- `PUT /api/info-publish/{id}` - 更新信息
- `DELETE /api/info-publish/{id}` - 删除信息

#### 系统配置 (`/api/config`)

- 轮播图: `/api/config/banners`
- 企业信息: `/api/config/company-profile`
- 业绩展示: `/api/config/achievements`
- 友情链接: `/api/config/friendly-links`

## 开发建议

### 1. 新增页面

参考 `src/views/announcements/GovProcurement.vue` 示例页面，包含完整的增删改查功能。

### 2. 新增 API

在 `src/api/` 目录下创建新的 API 文件，使用统一的 `request` 实例。

### 3. 状态管理

使用 Pinia 管理全局状态，参考 `src/stores/user.js`。

### 4. 路由配置

在 `src/router/index.js` 中添加新路由，支持嵌套路由和权限控制。

## 注意事项

1. **环境变量**: 确保 `.env` 文件中的 API 地址正确
2. **Token 管理**: Token 存储在 localStorage，注意安全性
3. **跨域问题**: Vite 已配置代理，开发环境无跨域问题
4. **生产部署**: 构建后需配置 Nginx 反向代理到后端 API

## 许可证

MIT License

## 联系方式

如有问题，请联系项目维护者。
