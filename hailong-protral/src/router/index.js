import { createRouter, createWebHistory } from 'vue-router'

const routes = [
  {
    path: '/',
    name: 'hailongHome',
    component: () => import('@/views/Home.vue')
  },
  {
    path: '/detail/:id?',
    name: 'hailongDetail',
    component: () => import('@/views/Detail.vue')
  },
  {
    path: '/about',
    name: 'About',
    component: () => import('@/views/About.vue')
  },
  {
    path: '/announcements',
    name: 'Announcements',
    component: () => import('@/views/Announcements.vue')
  },
  {
    path: '/company-announcements',
    name: 'CompanyAnnouncements',
    component: () => import('@/views/CompanyAnnouncements.vue')
  },
  {
    path: '/policies',
    name: 'Policies',
    component: () => import('@/views/Policies.vue')
  },
  {
    path: '/tools',
    name: 'Tools',
    component: () => import('@/views/Tools.vue')
  }
]

const router = createRouter({
  history: createWebHistory(),
  routes,
  scrollBehavior(to, from, savedPosition) {
    if (savedPosition) {
      return savedPosition
    } else {
      return { top: 0 }
    }
  }
})

export default router
