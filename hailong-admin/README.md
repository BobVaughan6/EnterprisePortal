# 海隆咨询官网后台管理系统

基于 Vue 3 + Element Plus 开发的现代化后台管理系统。

## 📋 项目概述

**项目名称**: 海隆咨询官网后台管理系统 (hailong-admin)

**技术架构**: Vue 3 + Vite + Element Plus

**开发状态**: ✅ 已完成并投入使用

## 🛠 技术栈

| 技术 | 版本 | 用途 |
|------|------|------|
| Vue | 3.4.0 | 前端框架 |
| Vite | 5.0.0 | 构建工具 |
| Element Plus | 2.5.0 | UI组件库 |
| Pinia | 2.1.7 | 状态管理 |
| Vue Router | 4.2.5 | 路由管理 |
| Axios | 1.6.2 | HTTP客户端 |
| WangEditor | 5.1.23 | 富文本编辑器 |
| ECharts | 6.0.0 | 数据可视化 |

## 📁 项目结构

```
hailong-admin/
├── public/                          # 静态资源
├── src/
│   ├── api/                        # API接口封装
│   │   ├── request.js              # Axios封装（拦截器、错误处理）
│   │   ├── index.js                # API统一导出
│   │   ├── auth.js                 # 认证相关API
│   │   ├── announcement.js         # 公告管理API
│   │   ├── infoPublication.js      # 信息发布API
│   │   ├── attachment.js           # 附件管理API
│   │   ├── systemConfig.js         # 系统配置API
│   │   ├── statistics.js           # 统计分析API
│   │   ├── system.js               # 系统管理API
│   │   └── user.js                 # 用户管理API
│   ├── assets/                     # 资源文件
│   │   ├── logo.png                # Logo图片
│   │   └── hailong.ico             # 网站图标
│   ├── components/                 # 公共组件
│   │   ├── Header.vue              # 顶部导航栏
│   │   ├── Sidebar.vue             # 侧边栏导航
│   │   ├── RichEditor.vue          # 富文本编辑器
│   │   ├── FileUpload.vue          # 文件上传组件
│   │   ├── RegionSelector.vue      # 区域选择器
│   │   └── RegionCascader.vue      # 区域级联选择器
│   ├── config/                     # 配置文件
│   │   └── api.config.js           # API配置
│   ├── router/                     # 路由配置
│   │   └── index.js                # 路由定义 + 权限守卫
│   ├── stores/                     # Pinia状态管理
│   │   └── user.js                 # 用户状态
│   ├── utils/                      # 工具函数
│   │   ├── auth.js                 # Token存储工具
│   │   ├── date.js                 # 日期格式化工具
│   │   └── chartOptions.js         # 图表配置工具
│   ├── views/                      # 页面组件
│   │   ├── Login.vue               # 登录页
│   │   ├── Layout.vue              # 主框架布局
│   │   ├── Dashboard.vue           # 首页仪表盘
│   │   ├── announcements/          # 公告管理
│   │   │   ├── GovProcurement.vue  # 政府采购公告
│   │   │   └── Construction.vue    # 建设工程公告
│   │   ├── infoPublications/       # 信息发布
│   │   │   ├── CompanyNews.vue     # 公司新闻
│   │   │   └── PolicyRegulation.vue # 政策法规
│   │   ├── attachments/            # 附件管理
│   │   │   └── AttachmentList.vue  # 附件列表
│   │   ├── config/                 # 系统配置
│   │   │   ├── Banners.vue         # 轮播图管理
│   │   │   ├── CompanyProfile.vue  # 企业简介
│   │   │   ├── BusinessScope.vue   # 业务范围
│   │   │   ├── Qualifications.vue  # 企业资质
│   │   │   ├── Honors.vue          # 企业荣誉
│   │   │   ├── Achievements.vue    # 重要业绩
│   │   │   └── FriendlyLinks.vue   # 友情链接
│   │   ├── system/                 # 系统管理
│   │   │   ├── Users.vue           # 用户管理
│   │   │   ├── SystemLogs.vue      # 系统日志
│   │   │   └── Profile.vue         # 个人资料
│   │   └── statistics/             # 统计分析
│   │       └── Dashboard.vue       # 数据统计
│   ├── App.vue                     # 根组件
│   ├── main.js                     # 入口文件
│   └── style.css                   # 全局样式
├── .env.development                # 开发环境配置
├── .env.production                 # 生产环境配置
├── .gitignore                      # Git忽略文件
├── index.html                      # HTML模板
├── package.json                    # 项目依赖
├── vite.config.js                  # Vite配置
└── README.md                       # 项目说明
```

## 🚀 快速开始

### 1. 环境要求

- **Node.js** >= 18.0
- **npm** >= 9.0 或 **pnpm** >= 8.0

### 2. 安装依赖

```bash
cd hailong-admin
npm install
```

或使用 pnpm:

```bash
pnpm install
```

### 3. 配置后端API地址

编辑 `.env.development` 文件：

```env
# 开发环境API地址
VITE_API_BASE_URL=http://localhost:5000
```

编辑 `.env.production` 文件：

```env
# 生产环境API地址
VITE_API_BASE_URL=https://api.yourdomain.com
```

### 4. 启动开发服务器

```bash
npm run dev
```

访问 http://localhost:3000

### 5. 构建生产版本

```bash
npm run build
```

构建产物位于 `dist/` 目录。

### 6. 预览生产构建

```bash
npm run preview
```

## 🔑 默认登录账号

```
用户名: admin
密码: Admin@123456
```

⚠️ **安全提示**: 首次登录后请立即修改密码！

## 📱 功能模块

### 1. 用户认证

- ✅ 登录（用户名/密码）
- ✅ Token自动管理（localStorage）
- ✅ 请求拦截器自动添加Authorization头
- ✅ 401自动跳转登录页
- ✅ 修改密码
- ✅ 退出登录

### 2. 首页仪表盘

- ✅ 数据统计卡片（总项目数、总用户数、今日访问量等）
- ✅ 访问趋势图表（ECharts）
- ✅ 公告统计图表
- ✅ 最新公告列表
- ✅ 快捷操作入口

### 3. 公告管理

#### 政府采购公告
- ✅ 列表展示（分页、搜索、筛选）
- ✅ 新增公告（富文本编辑器）
- ✅ 编辑公告
- ✅ 删除公告（软删除）
- ✅ 批量操作
- ✅ 附件上传管理
- ✅ 区域选择（省市区三级联动）
- ✅ 公告类型选择
- ✅ 预览功能

#### 建设工程公告
- ✅ 功能同政府采购公告
- ✅ 独立的公告类型配置

### 4. 信息发布管理

#### 公司新闻
- ✅ 新闻列表（分页、搜索）
- ✅ 新增/编辑新闻
- ✅ 富文本内容编辑
- ✅ 封面图片上传
- ✅ 附件管理
- ✅ 置顶功能
- ✅ 发布/下架

#### 政策法规
- ✅ 法规列表管理
- ✅ 分类管理
- ✅ 富文本编辑
- ✅ 附件上传
- ✅ 发布时间设置

### 5. 附件管理

- ✅ 附件列表展示
- ✅ 按类型筛选（图片/文档/其他）
- ✅ 按关联类型筛选
- ✅ 附件预览
- ✅ 附件下载
- ✅ 批量删除
- ✅ 存储空间统计

### 6. 系统配置

#### 轮播图管理
- ✅ 轮播图列表
- ✅ 新增/编辑轮播图
- ✅ 图片上传（推荐尺寸：1920x600）
- ✅ 标题、描述、链接设置
- ✅ 排序调整（拖拽排序）
- ✅ 启用/禁用

#### 企业简介
- ✅ 富文本编辑
- ✅ 图片上传
- ✅ 企业特色标签管理
- ✅ 实时预览

#### 业务范围
- ✅ 业务列表管理
- ✅ 业务图标上传
- ✅ 业务特点编辑
- ✅ 排序管理

#### 企业资质
- ✅ 资质证书管理
- ✅ 证书图片上传
- ✅ 证书信息编辑
- ✅ 有效期管理

#### 企业荣誉
- ✅ 荣誉列表管理
- ✅ 荣誉证书上传
- ✅ 荣誉级别设置
- ✅ 获奖日期管理

#### 重要业绩
- ✅ 业绩项目管理
- ✅ 项目图片上传
- ✅ 项目金额设置
- ✅ 完成日期管理

#### 友情链接
- ✅ 链接列表管理
- ✅ 链接分类
- ✅ Logo上传
- ✅ 排序管理

### 7. 系统管理

#### 用户管理
- ✅ 用户列表
- ✅ 新增/编辑用户
- ✅ 角色分配
- ✅ 启用/禁用用户
- ✅ 重置密码

#### 系统日志
- ✅ 操作日志查询
- ✅ 按用户筛选
- ✅ 按操作类型筛选
- ✅ 按时间范围筛选
- ✅ 日志详情查看
- ✅ 日志导出

#### 个人资料
- ✅ 查看个人信息
- ✅ 修改密码
- ✅ 修改邮箱
- ✅ 修改手机号

### 8. 统计分析

- ✅ 访问统计（日/周/月）
- ✅ 公告统计
- ✅ 用户行为分析
- ✅ 数据可视化图表
- ✅ 数据导出

## 🔌 API调用示例

### 登录

```javascript
import { authApi } from '@/api'

const login = async () => {
  try {
    const res = await authApi.login({
      username: 'admin',
      password: 'Admin@123456'
    })
    
    if (res.success) {
      console.log('登录成功', res.data)
      // Token已自动存储到localStorage
    }
  } catch (error) {
    console.error('登录失败', error)
  }
}
```

### 获取公告列表

```javascript
import { announcementApi } from '@/api'

const getAnnouncements = async () => {
  try {
    const res = await announcementApi.getList({
      businessType: 'GOV_PROCUREMENT',
      keyword: '招标',
      pageIndex: 1,
      pageSize: 10
    })
    
    if (res.success) {
      console.log('公告列表', res.data.items)
      console.log('总数', res.data.totalCount)
    }
  } catch (error) {
    console.error('获取失败', error)
  }
}
```

### 上传附件

```javascript
import { attachmentApi } from '@/api'

const uploadFile = async (file) => {
  try {
    const formData = new FormData()
    formData.append('file', file)
    formData.append('category', 'image')
    formData.append('relatedType', 'announcement')
    
    const res = await attachmentApi.upload(formData)
    
    if (res.success) {
      console.log('上传成功', res.data)
      return res.data.id
    }
  } catch (error) {
    console.error('上传失败', error)
  }
}
```

### 创建公告

```javascript
import { announcementApi } from '@/api'

const createAnnouncement = async () => {
  try {
    const res = await announcementApi.create({
      title: '招标公告标题',
      businessType: 'GOV_PROCUREMENT',
      noticeType: 'bidding',
      content: '<p>公告内容...</p>',
      province: '河南省',
      city: '郑州市',
      district: '金水区',
      bidder: '招标单位',
      publishTime: '2025-12-16 12:00:00',
      attachmentIds: [1, 2, 3]
    })
    
    if (res.success) {
      console.log('创建成功', res.data)
    }
  } catch (error) {
    console.error('创建失败', error)
  }
}
```

## 🎨 组件使用示例

### 富文本编辑器

```vue
<template>
  <RichEditor v-model="content" :height="400" />
</template>

<script setup>
import { ref } from 'vue'
import RichEditor from '@/components/RichEditor.vue'

const content = ref('<p>初始内容</p>')
</script>
```

### 文件上传

```vue
<template>
  <FileUpload
    :file-list="fileList"
    :max-count="5"
    accept="image/*"
    @success="handleUploadSuccess"
    @remove="handleRemove"
  />
</template>

<script setup>
import { ref } from 'vue'
import FileUpload from '@/components/FileUpload.vue'

const fileList = ref([])

const handleUploadSuccess = (file) => {
  fileList.value.push(file)
}

const handleRemove = (file) => {
  const index = fileList.value.findIndex(f => f.id === file.id)
  if (index > -1) {
    fileList.value.splice(index, 1)
  }
}
</script>
```

### 区域选择器

```vue
<template>
  <RegionCascader
    v-model:province="province"
    v-model:city="city"
    v-model:district="district"
  />
</template>

<script setup>
import { ref } from 'vue'
import RegionCascader from '@/components/RegionCascader.vue'

const province = ref('')
const city = ref('')
const district = ref('')
</script>
```

## 🔧 开发指南

### 添加新页面

1. **创建页面组件**:

```vue
<!-- src/views/example/NewPage.vue -->
<template>
  <div class="new-page">
    <h1>新页面</h1>
  </div>
</template>

<script setup>
// 页面逻辑
</script>

<style scoped>
.new-page {
  padding: 20px;
}
</style>
```

2. **添加路由**:

```javascript
// src/router/index.js
{
  path: '/new-page',
  name: 'NewPage',
  component: () => import('@/views/example/NewPage.vue'),
  meta: { requiresAuth: true, title: '新页面' }
}
```

3. **添加菜单**:

```vue
<!-- src/components/Sidebar.vue -->
<el-menu-item index="/new-page">
  <el-icon><Document /></el-icon>
  <span>新页面</span>
</el-menu-item>
```

### 添加新API

```javascript
// src/api/example.js
import request from './request'

export const exampleApi = {
  // 获取列表
  getList(params) {
    return request.get('/api/example', { params })
  },
  
  // 获取详情
  getById(id) {
    return request.get(`/api/example/${id}`)
  },
  
  // 创建
  create(data) {
    return request.post('/api/example', data)
  },
  
  // 更新
  update(id, data) {
    return request.put(`/api/example/${id}`, data)
  },
  
  // 删除
  delete(id) {
    return request.delete(`/api/example/${id}`)
  }
}
```

### 状态管理

```javascript
// src/stores/example.js
import { defineStore } from 'pinia'

export const useExampleStore = defineStore('example', {
  state: () => ({
    data: [],
    loading: false
  }),
  
  getters: {
    count: (state) => state.data.length
  },
  
  actions: {
    async fetchData() {
      this.loading = true
      try {
        // 调用API
        const res = await exampleApi.getList()
        this.data = res.data
      } finally {
        this.loading = false
      }
    }
  }
})
```

## 🎯 最佳实践

### 1. 代码规范

- 使用 Vue 3 Composition API
- 组件命名使用 PascalCase
- 文件命名使用 kebab-case
- 使用 ESLint 进行代码检查

### 2. 性能优化

- 路由懒加载
- 组件按需引入
- 图片懒加载
- 合理使用 computed 和 watch
- 避免不必要的响应式数据

### 3. 安全建议

- Token 存储在 localStorage
- 敏感操作二次确认
- 文件上传类型验证
- XSS 防护（富文本内容过滤）
- CSRF 防护

### 4. 用户体验

- 加载状态提示
- 操作成功/失败提示
- 表单验证提示
- 空状态提示
- 错误边界处理

## 🚀 部署

### 构建生产版本

```bash
npm run build
```

### Nginx配置示例

```nginx
server {
    listen 80;
    server_name admin.yourdomain.com;
    
    root /var/www/hailong-admin/dist;
    index index.html;
    
    # Gzip压缩
    gzip on;
    gzip_types text/plain text/css application/json application/javascript text/xml application/xml application/xml+rss text/javascript;
    
    # SPA路由支持
    location / {
        try_files $uri $uri/ /index.html;
    }
    
    # API代理
    location /api/ {
        proxy_pass http://localhost:5000/api/;
        proxy_http_version 1.1;
        proxy_set_header Upgrade $http_upgrade;
        proxy_set_header Connection 'upgrade';
        proxy_set_header Host $host;
        proxy_set_header X-Real-IP $remote_addr;
        proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
        proxy_cache_bypass $http_upgrade;
    }
    
    # 静态资源缓存
    location ~* \.(jpg|jpeg|png|gif|ico|css|js|woff|woff2|ttf|svg)$ {
        expires 30d;
        add_header Cache-Control "public, immutable";
    }
}
```

### Docker部署

创建 `Dockerfile`:

```dockerfile
FROM node:18-alpine as build

WORKDIR /app
COPY package*.json ./
RUN npm install
COPY . .
RUN npm run build

FROM nginx:alpine
COPY --from=build /app/dist /usr/share/nginx/html
COPY nginx.conf /etc/nginx/conf.d/default.conf
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
```

构建和运行：

```bash
docker build -t hailong-admin .
docker run -d -p 3000:80 --name hailong-admin hailong-admin
```

## 🐛 故障排查

### 常见问题

**1. 登录后立即退出**

```
检查项：
- Token是否正确存储
- API地址是否正确
- 后端JWT配置是否正确
```

**2. 文件上传失败**

```
检查项：
- 文件大小是否超限
- 文件类型是否允许
- 后端上传接口是否正常
- 网络连接是否正常
```

**3. 富文本编辑器不显示**

```
检查项：
- WangEditor是否正确安装
- 组件是否正确引入
- 样式是否正确加载
```

**4. 图表不显示**

```
检查项：
- ECharts是否正确安装
- 数据格式是否正确
- 容器尺寸是否正确
```

## 📚 相关文档

- [项目总体说明](../README.md)
- [后端API文档](../BackEnd/HailongConsulting.API/README.md)
- [前端门户文档](../hailong-protral/README.md)
- [数据库文档](../SQL/README.md)

## 📄 许可证

Copyright © 2025 河南海隆工程咨询有限公司

---

**最后更新**: 2025年12月16日  
**维护团队**: 海隆咨询技术部
