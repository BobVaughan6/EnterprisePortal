import { createRouter, createWebHistory } from 'vue-router'
import { tokenUtils } from '@/utils/auth'

// 路由配置
const routes = [
  {
    path: '/login',
    name: 'Login',
    component: () => import('@/views/Login.vue'),
    meta: { title: '登录' }
  },
  {
    path: '/',
    component: () => import('@/views/Layout.vue'),
    redirect: '/home',
    children: [
      {
        path: '/home',
        name: 'Home',
        component: () => import('@/views/Home.vue'),
        meta: { title: '首页', icon: 'Odometer' }
      },
      {
        path: '/gov-procurement',
        name: 'GovProcurement',
        component: () => import('@/views/announcements/GovProcurement.vue'),
        meta: { title: '政府采购公告', icon: 'Document' }
      },
      {
        path: '/construction',
        name: 'Construction',
        component: () => import('@/views/announcements/Construction.vue'),
        meta: { title: '建设工程公告', icon: 'Document' }
      },
      {
        path: '/info-publish',
        name: 'InfoPublish',
        redirect: '/info-publish/company-news',
        meta: { title: '信息发布', icon: 'DocumentCopy' },
        children: [
          {
            path: '/info-publish/company-news',
            name: 'CompanyNews',
            component: () => import('@/views/info-publish/CompanyNews.vue'),
            meta: { title: '公司公告', category: 'company_news' }
          },
          {
            path: '/info-publish/policies',
            name: 'Policies',
            component: () => import('@/views/info-publish/Policies.vue'),
            meta: { title: '政策法规', category: 'policies' }
          },
          {
            path: '/info-publish/policy-info',
            name: 'PolicyInfo',
            component: () => import('@/views/info-publish/PolicyInfo.vue'),
            meta: { title: '政策信息', category: 'policy_info' }
          },
          {
            path: '/info-publish/notices',
            name: 'Notices',
            component: () => import('@/views/info-publish/Notices.vue'),
            meta: { title: '通知公告', category: 'notices' }
          }
        ]
      },
      {
        path: '/config',
        name: 'Config',
        redirect: '/config/banners',
        meta: { title: '系统配置', icon: 'Setting' },
        children: [
          {
            path: '/config/banners',
            name: 'Banners',
            component: () => import('@/views/config/Banners.vue'),
            meta: { title: '轮播图管理' }
          },
          {
            path: '/config/company-profile',
            name: 'CompanyProfile',
            component: () => import('@/views/config/CompanyProfile.vue'),
            meta: { title: '企业信息' }
          },
          {
            path: '/config/achievements',
            name: 'Achievements',
            component: () => import('@/views/config/Achievements.vue'),
            meta: { title: '业绩展示' }
          },
          {
            path: '/config/friendly-links',
            name: 'FriendlyLinks',
            component: () => import('@/views/config/FriendlyLinks.vue'),
            meta: { title: '友情链接' }
          }
        ]
      },
      {
        path: '/system',
        name: 'System',
        redirect: '/system/change-password',
        meta: { title: '系统管理', icon: 'Tools' },
        children: [
          {
            path: '/system/change-password',
            name: 'ChangePassword',
            component: () => import('@/views/system/ChangePassword.vue'),
            meta: { title: '修改密码' }
          }
        ]
      }
    ]
  }
]

const router = createRouter({
  history: createWebHistory(),
  routes
})

/**
 * 路由守卫 - 权限验证
 */
router.beforeEach((to, from, next) => {
  const token = tokenUtils.getToken()
  
  // 设置页面标题
  document.title = to.meta.title ? `${to.meta.title} - 海隆咨询后台管理` : '海隆咨询后台管理系统'
  
  // 如果访问登录页，直接放行
  if (to.path === '/login') {
    if (token) {
      // 已登录则跳转到首页
      next('/home')
    } else {
      next()
    }
    return
  }
  
  // 其他页面需要验证登录
  if (!token) {
    next('/login')
    return
  }
  
  next()
})

export default router
