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
    redirect: '/dashboard',
    children: [
      {
        path: '/dashboard',
        name: 'Dashboard',
        component: () => import('@/views/Dashboard.vue'),
        meta: { title: '数据看板', icon: 'DataAnalysis' }
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
        redirect: '/info-publish/news-center',
        meta: { title: '信息发布', icon: 'DocumentCopy' },
        children: [
          {
            path: '/info-publish/news-center',
            name: 'NewsCenter',
            component: () => import('@/views/info-publish/NewsCenter.vue'),
            meta: { title: '新闻中心' }
          },
          {
            path: '/info-publish/policy-regulation',
            name: 'PolicyRegulation',
            component: () => import('@/views/info-publish/PolicyRegulation.vue'),
            meta: { title: '政策法规' }
          }
        ]
      },
      {
        path: '/attachments',
        name: 'Attachments',
        component: () => import('@/views/attachments/AttachmentList.vue'),
        meta: { title: '附件管理', icon: 'FolderOpened' }
      },
      {
        path: '/statistics',
        name: 'Statistics',
        redirect: '/statistics/overview',
        meta: { title: '统计分析', icon: 'DataLine' },
        children: [
          {
            path: '/statistics/overview',
            name: 'SystemOverview',
            component: () => import('@/views/statistics/SystemOverview.vue'),
            meta: { title: '系统概览' }
          },
          {
            path: '/statistics/visit',
            name: 'VisitStatistics',
            component: () => import('@/views/statistics/VisitStatistics.vue'),
            meta: { title: '访问统计' }
          },
          {
            path: '/statistics/announcement',
            name: 'AnnouncementStatistics',
            component: () => import('@/views/statistics/AnnouncementStatistics.vue'),
            meta: { title: '公告统计' }
          },
          {
            path: '/statistics/info-publication',
            name: 'InfoPublicationStatistics',
            component: () => import('@/views/statistics/InfoPublicationStatistics.vue'),
            meta: { title: '信息发布统计' }
          }
        ]
      },
      {
        path: '/config',
        name: 'Config',
        redirect: '/config/company-profile',
        meta: { title: '系统配置', icon: 'Setting' },
        children: [
          // 轮播图管理 - 已暂时注释，不对接后台
          // {
          //   path: '/config/banners',
          //   name: 'Banners',
          //   component: () => import('@/views/config/Banners.vue'),
          //   meta: { title: '轮播图管理' }
          // },
          {
            path: '/config/company-profile',
            name: 'CompanyProfile',
            component: () => import('@/views/config/CompanyProfile.vue'),
            meta: { title: '企业信息' }
          },
          {
            path: '/config/business-scope',
            name: 'BusinessScope',
            component: () => import('@/views/config/BusinessScope.vue'),
            meta: { title: '业务范围' }
          },
          {
            path: '/config/qualifications',
            name: 'Qualifications',
            component: () => import('@/views/config/Qualifications.vue'),
            meta: { title: '资质管理' }
          },
          {
            path: '/config/honors',
            name: 'Honors',
            component: () => import('@/views/config/Honors.vue'),
            meta: { title: '荣誉管理' }
          },
          {
            path: '/config/achievements',
            name: 'Achievements',
            component: () => import('@/views/config/Achievements.vue'),
            meta: { title: '重大业绩' }
          }
          // 友情链接 - 已暂时注释
          // {
          //   path: '/config/friendly-links',
          //   name: 'FriendlyLinks',
          //   component: () => import('@/views/config/FriendlyLinks.vue'),
          //   meta: { title: '友情链接' }
          // }
        ]
      },
      {
        path: '/system',
        name: 'System',
        redirect: '/system/users',
        meta: { title: '系统管理', icon: 'Tools' },
        children: [
          {
            path: '/system/users',
            name: 'Users',
            component: () => import('@/views/system/Users.vue'),
            meta: { title: '用户管理' }
          },
          {
            path: '/system/logs',
            name: 'SystemLogs',
            component: () => import('@/views/system/SystemLogs.vue'),
            meta: { title: '系统日志' }
          },
          {
            path: '/system/regions',
            name: 'Regions',
            component: () => import('@/views/system/Regions.vue'),
            meta: { title: '地区字典' }
          },
          {
            path: '/system/change-password',
            name: 'ChangePassword',
            component: () => import('@/views/system/ChangePassword.vue'),
            meta: { title: '修改密码' }
          }
        ]
      }
    ]
  },
  // 兼容旧路由 - 重定向到 dashboard
  {
    path: '/home',
    redirect: '/dashboard'
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
      // 已登录则跳转到数据看板
      next('/dashboard')
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
