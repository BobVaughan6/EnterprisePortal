# 海隆咨询官网前端门户

基于 Vue 3 + Tailwind CSS 开发的企业官网前端门户系统。

## 📋 项目概述

**项目名称**: 海隆咨询官网前端门户 (hailong-protral)

**技术架构**: Vue 3 + Vite + Tailwind CSS

**开发状态**: ✅ 已完成并投入使用

## 🛠 技术栈

| 技术 | 版本 | 用途 |
|------|------|------|
| Vue | 3.4.0 | 前端框架 |
| Vite | 5.0.0 | 构建工具 |
| Vue Router | 4.2.5 | 路由管理 |
| Tailwind CSS | 3.4.0 | CSS框架 |
| Axios | (内置) | HTTP客户端 |

## 📁 项目结构

```
hailong-protral/
├── public/                          # 静态资源
│   ├── favicon.ico                  # 网站图标
│   └── images/                      # 公共图片
├── src/
│   ├── api/                        # API接口封装
│   │   ├── request.js              # Axios封装
│   │   ├── announcement.js         # 公告API
│   │   ├── infoPublication.js      # 信息发布API
│   │   ├── config.js               # 配置API
│   │   ├── region.js               # 区域API
│   │   └── search.js               # 搜索API
│   ├── assets/                     # 资源文件
│   │   ├── images/                 # 图片资源
│   │   └── styles/                 # 样式文件
│   ├── components/                 # 公共组件
│   │   ├── Header.vue              # 顶部导航栏
│   │   ├── Footer.vue              # 底部信息栏
│   │   ├── Breadcrumb.vue          # 面包屑导航
│   │   ├── Pagination.vue          # 分页组件
│   │   └── home/                   # 首页组件
│   │       ├── HeroSection.vue     # 轮播Banner
│   │       ├── AboutSection.vue    # 企业简介
│   │       ├── BusinessSection.vue # 业务范围
│   │       ├── DataSection.vue     # 数据统计
│   │       ├── AchievementSection.vue # 业绩展示
│   │       └── NewsSection.vue     # 新闻公告
│   ├── router/                     # 路由配置
│   │   └── index.js                # 路由定义
│   ├── utils/                      # 工具函数
│   │   ├── config.js               # 配置工具
│   │   ├── date.js                 # 日期格式化
│   │   └── request.js              # 请求工具
│   ├── views/                      # 页面组件
│   │   ├── Home.vue                # 首页
│   │   ├── pages/                  # 静态页面
│   │   │   ├── About.vue           # 关于我们
│   │   │   ├── Business.vue        # 业务范围
│   │   │   ├── Qualifications.vue  # 企业资质
│   │   │   ├── Honors.vue          # 企业荣誉
│   │   │   ├── Achievements.vue    # 重要业绩
│   │   │   ├── Contact.vue         # 联系我们
│   │   │   ├── ExpertDatabase.vue  # 专家库
│   │   │   └── Tools.vue           # 实用工具
│   │   ├── announcements/          # 公告页面
│   │   │   ├── GovProcurement.vue  # 政府采购公告列表
│   │   │   └── Construction.vue    # 建设工程公告列表
│   │   ├── news/                   # 新闻页面
│   │   │   ├── CompanyNews.vue     # 公司新闻列表
│   │   │   └── PolicyRegulation.vue # 政策法规列表
│   │   ├── details/                # 详情页面
│   │   │   ├── AnnouncementDetail.vue      # 公告详情
│   │   │   ├── CompanyAnnouncementDetail.vue # 公司公告详情
│   │   │   └── PolicyDetail.vue    # 政策法规详情
│   │   ├── search/                 # 搜索页面
│   │   │   └── SearchResult.vue    # 搜索结果
│   │   └── tools/                  # 工具页面
│   │       ├── BiddingFeeCalculator.vue    # 招标代理费计算器
│   │       ├── CostingFeeCalculator.vue    # 造价咨询费计算器
│   │       └── JudicialFeeCalculator.vue   # 司法鉴定费计算器
│   ├── App.vue                     # 根组件
│   ├── main.js                     # 入口文件
│   └── style.css                   # 全局样式
├── .env.development                # 开发环境配置
├── .env.production                 # 生产环境配置
├── .gitignore                      # Git忽略文件
├── index.html                      # HTML模板
├── package.json                    # 项目依赖
├── postcss.config.js               # PostCSS配置
├── tailwind.config.js              # Tailwind配置
├── vite.config.js                  # Vite配置
└── README.md                       # 项目说明
```

## 🚀 快速开始

### 1. 环境要求

- **Node.js** >= 18.0
- **npm** >= 9.0 或 **pnpm** >= 8.0

### 2. 安装依赖

```bash
cd hailong-protral
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

访问 http://localhost:5173

### 5. 构建生产版本

```bash
npm run build
```

构建产物位于 `dist/` 目录。

### 6. 预览生产构建

```bash
npm run preview
```

## 📱 功能模块

### 1. 首页

#### 轮播Banner
- ✅ 自动轮播
- ✅ 手动切换
- ✅ 响应式设计
- ✅ 支持跳转链接

#### 企业简介
- ✅ 图文展示
- ✅ 企业特色标签
- ✅ 响应式布局

#### 业务范围
- ✅ 卡片式展示
- ✅ Hover效果
- ✅ 图标展示
- ✅ 详情跳转

#### 数据统计
- ✅ 动态数字展示
- ✅ 数据可视化
- ✅ 实时更新

#### 业绩展示
- ✅ 图片轮播
- ✅ 无限滚动
- ✅ 点击放大

#### 最新公告
- ✅ 分类展示
- ✅ 时间排序
- ✅ 快速跳转

### 2. 关于我们

#### 企业简介
- ✅ 公司历史
- ✅ 企业文化
- ✅ 组织架构
- ✅ 富文本展示

#### 业务范围
- ✅ 业务分类
- ✅ 详细介绍
- ✅ 服务流程

#### 企业资质
- ✅ 资质证书展示
- ✅ 证书详情
- ✅ 图片预览

#### 企业荣誉
- ✅ 荣誉墙展示
- ✅ 时间线布局
- ✅ 荣誉详情

#### 重要业绩
- ✅ 项目列表
- ✅ 项目详情
- ✅ 图片展示

### 3. 公告信息

#### 政府采购公告
- ✅ 列表展示（分页）
- ✅ 多条件筛选
  - 公告类型（采购公告/更正公告/结果公告）
  - 项目区域（省市区三级）
  - 时间范围
  - 关键词搜索
- ✅ 公告详情
- ✅ 附件下载
- ✅ 相关公告推荐
- ✅ 访问量统计

#### 建设工程公告
- ✅ 列表展示（分页）
- ✅ 多条件筛选
  - 公告类型（招标公告/中标公告/变更公告）
  - 项目区域
  - 时间范围
  - 关键词搜索
- ✅ 公告详情
- ✅ 附件下载
- ✅ 相关公告推荐

### 4. 信息发布

#### 公司新闻
- ✅ 新闻列表
- ✅ 分类筛选
- ✅ 新闻详情
- ✅ 富文本展示
- ✅ 附件下载

#### 政策法规
- ✅ 法规列表
- ✅ 分类筛选
- ✅ 法规详情
- ✅ 文件下载

### 5. 全局搜索

- ✅ 关键词搜索
- ✅ 高级筛选
  - 业务类别
  - 公告类型
  - 时间范围
  - 项目区域
- ✅ 搜索结果列表
- ✅ 关键词高亮
- ✅ 分页展示
- ✅ 结果统计

### 6. 实用工具

#### 招标代理费计算器
- ✅ 项目金额输入
- ✅ 项目类型选择（工程/货物/服务）
- ✅ 优惠比例设置
- ✅ 实时计算
- ✅ 计算过程展示
- ✅ 收费标准表格
- ✅ 依据文件查看

#### 造价咨询费计算器
- ✅ 工程造价输入
- ✅ 工程类型选择
- ✅ 计费方式选择
- ✅ 实时计算
- ✅ 收费标准说明
- ✅ 标准文件下载

#### 司法鉴定费计算器
- ✅ 鉴定标的输入
- ✅ 鉴定类型选择
- ✅ 分段累进计费
- ✅ 计算明细展示
- ✅ 计算示例

### 7. 专家库

#### 专家信息录入
- ✅ 电脑端填写
  - 居中弹窗表单
  - 在线填写专家信息
  - 表单验证
- ✅ 手机端填写
  - 二维码扫码填写
  - 可切换显示/隐藏二维码
  - 适合不方便使用电脑的用户
- ✅ 响应式布局
  - 移动端：二维码居中显示
  - 电脑端：二维码在公告框内右侧
- ✅ 联系方式展示
- ✅ 温馨提示说明

### 8. 联系我们

- ✅ 联系方式展示
  - 固定电话：0371-55894666
  - 公司地址：河南省郑州市郑东新区金水东路雅宝·东方国际广场2号楼13层
  - 电子邮箱
  - 工作时间
- ✅ 地图定位（百度/高德地图）
- ✅ 在线留言表单
- ✅ 二维码展示

### 9. 底部信息

#### 友情链接
- ✅ 分类展示
  - 省级单位
  - 地市级单位
  - 国家级单位
- ✅ 新窗口打开
- ✅ 响应式布局

#### 访问统计
- ✅ 总访问量
- ✅ 今日访问量
- ✅ 在线人数

## 🎨 响应式设计

### 断点设置

基于 Tailwind CSS 断点：

| 断点 | 最小宽度 | 设备类型 |
|------|---------|---------|
| `sm` | 640px | 手机横屏 |
| `md` | 768px | 平板 |
| `lg` | 1024px | 小屏电脑 |
| `xl` | 1280px | 标准电脑 |
| `2xl` | 1536px | 大屏 |

### 适配策略

- ✅ 移动端优先（Mobile First）
- ✅ 弹性布局（Flexbox/Grid）
- ✅ 图片响应式
- ✅ 导航自适应（汉堡菜单）
- ✅ 字体大小自适应
- ✅ 触摸友好

## 🔌 API调用示例

### 获取公告列表

```javascript
import { getAnnouncements } from '@/api/announcement'

const fetchAnnouncements = async () => {
  try {
    const response = await getAnnouncements({
      businessType: 'GOV_PROCUREMENT',
      pageIndex: 1,
      pageSize: 10
    })
    
    if (response.success) {
      console.log('公告列表', response.data.items)
    }
  } catch (error) {
    console.error('获取失败', error)
  }
}
```

### 获取公告详情

```javascript
import { getAnnouncementById } from '@/api/announcement'

const fetchDetail = async (id) => {
  try {
    const response = await getAnnouncementById(id)
    
    if (response.success) {
      console.log('公告详情', response.data)
    }
  } catch (error) {
    console.error('获取失败', error)
  }
}
```

### 全局搜索

```javascript
import { globalSearch } from '@/api/search'

const search = async () => {
  try {
    const response = await globalSearch({
      keyword: '招标',
      businessType: 'GOV_PROCUREMENT',
      startDate: '2025-01-01',
      endDate: '2025-12-31',
      province: '河南省',
      pageIndex: 1,
      pageSize: 10
    })
    
    if (response.success) {
      console.log('搜索结果', response.data.items)
    }
  } catch (error) {
    console.error('搜索失败', error)
  }
}
```

### 获取首页统计数据

```javascript
import { getHomeStatistics } from '@/api/config'

const fetchStatistics = async () => {
  try {
    const response = await getHomeStatistics()
    
    if (response.success) {
      console.log('统计数据', response.data)
    }
  } catch (error) {
    console.error('获取失败', error)
  }
}
```

## 🎯 性能优化

### 已实现的优化

- ✅ 路由懒加载
- ✅ 图片懒加载
- ✅ 组件按需引入
- ✅ 代码分割
- ✅ Gzip压缩
- ✅ 浏览器缓存策略
- ✅ CDN加速（可选）

### 优化建议

```javascript
// 路由懒加载
const Home = () => import('@/views/Home.vue')

// 图片懒加载
<img loading="lazy" src="image.jpg" alt="description">

// 组件按需引入
import { ref, computed } from 'vue'
```

## 🔧 开发指南

### 添加新页面

1. **创建页面组件**:

```vue
<!-- src/views/example/NewPage.vue -->
<template>
  <div class="container mx-auto px-4 py-8">
    <h1 class="text-3xl font-bold mb-6">新页面</h1>
    <p>页面内容...</p>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'

// 页面逻辑
onMounted(() => {
  console.log('页面已加载')
})
</script>
```

2. **添加路由**:

```javascript
// src/router/index.js
{
  path: '/new-page',
  name: 'NewPage',
  component: () => import('@/views/example/NewPage.vue'),
  meta: { title: '新页面' }
}
```

3. **添加导航链接**:

```vue
<!-- src/components/Header.vue -->
<router-link to="/new-page" class="nav-link">
  新页面
</router-link>
```

### 使用Tailwind CSS

```vue
<template>
  <!-- 响应式布局 -->
  <div class="container mx-auto px-4">
    <!-- 网格布局 -->
    <div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
      <!-- 卡片 -->
      <div class="bg-white rounded-lg shadow-md p-6 hover:shadow-lg transition-shadow">
        <h3 class="text-xl font-semibold mb-2">标题</h3>
        <p class="text-gray-600">内容...</p>
      </div>
    </div>
  </div>
</template>
```

### 自定义Tailwind配置

```javascript
// tailwind.config.js
module.exports = {
  content: [
    "./index.html",
    "./src/**/*.{vue,js,ts,jsx,tsx}",
  ],
  theme: {
    extend: {
      colors: {
        primary: '#1e40af',
        secondary: '#64748b',
      },
      fontFamily: {
        sans: ['Microsoft YaHei', 'sans-serif'],
      },
    },
  },
  plugins: [],
}
```

## 🚀 部署

### 构建生产版本

```bash
npm run build
```

### Nginx配置示例

```nginx
server {
    listen 80;
    server_name www.hailongzixun.com;
    
    root /var/www/hailong-protral/dist;
    index index.html;
    
    # Gzip压缩
    gzip on;
    gzip_vary on;
    gzip_min_length 1024;
    gzip_types text/plain text/css text/xml text/javascript application/x-javascript application/xml+rss application/json;
    
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
    location ~* \.(jpg|jpeg|png|gif|ico|css|js|svg|woff|woff2|ttf|eot)$ {
        expires 30d;
        add_header Cache-Control "public, immutable";
    }
    
    # 安全头
    add_header X-Frame-Options "SAMEORIGIN" always;
    add_header X-Content-Type-Options "nosniff" always;
    add_header X-XSS-Protection "1; mode=block" always;
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
docker build -t hailong-protral .
docker run -d -p 80:80 --name hailong-protral hailong-protral
```

## 📊 SEO优化

### Meta标签

```html
<head>
  <meta charset="UTF-8">
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <title>海隆咨询 - 专业招标代理、造价咨询、司法鉴定服务</title>
  <meta name="description" content="河南海隆工程咨询有限公司，提供专业的招标代理、造价咨询、司法鉴定服务">
  <meta name="keywords" content="招标代理,造价咨询,司法鉴定,河南,郑州,海隆咨询">
  <link rel="canonical" href="https://www.hailongzixun.com">
</head>
```

### 结构化数据

```javascript
// 在页面中添加结构化数据
const structuredData = {
  "@context": "https://schema.org",
  "@type": "Organization",
  "name": "河南海隆工程咨询有限公司",
  "url": "https://www.hailongzixun.com",
  "logo": "https://www.hailongzixun.com/logo.png",
  "contactPoint": {
    "@type": "ContactPoint",
    "telephone": "+86-371-55894666",
    "contactType": "customer service"
  }
}
```

### Sitemap生成

在 `public/` 目录创建 `sitemap.xml`:

```xml
<?xml version="1.0" encoding="UTF-8"?>
<urlset xmlns="http://www.sitemaps.org/schemas/sitemap/0.9">
  <url>
    <loc>https://www.hailongzixun.com/</loc>
    <lastmod>2025-12-16</lastmod>
    <changefreq>daily</changefreq>
    <priority>1.0</priority>
  </url>
  <url>
    <loc>https://www.hailongzixun.com/about</loc>
    <changefreq>monthly</changefreq>
    <priority>0.8</priority>
  </url>
  <!-- 更多URL -->
</urlset>
```

## 🐛 故障排查

### 常见问题

**1. 页面空白**

```
检查项：
- 控制台是否有错误
- API地址是否正确
- 路由配置是否正确
- 组件是否正确导入
```

**2. 样式不生效**

```
检查项：
- Tailwind CSS是否正确配置
- PostCSS是否正确配置
- 样式文件是否正确引入
- 浏览器缓存
```

**3. 图片不显示**

```
检查项：
- 图片路径是否正确
- 图片是否存在
- 网络请求是否成功
- CORS配置
```

**4. API请求失败**

```
检查项：
- API地址是否正确
- 后端服务是否运行
- CORS配置
- 网络连接
```

## 📚 相关文档

- [项目总体说明](../README.md)
- [后端API文档](../BackEnd/HailongConsulting.API/README.md)
- [后台管理文档](../hailong-admin/README.md)
- [数据库文档](../SQL/README.md)

## 📄 许可证

Copyright © 2025 河南海隆工程咨询有限公司

---

**最后更新**: 2025年12月16日  
**维护团队**: 海隆咨询技术部