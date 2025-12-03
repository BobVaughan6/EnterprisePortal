import { createRouter, createWebHistory } from 'vue-router'

const routes = [
  {
    path: '/',
    name: 'HuaweiHome',
    component: () => import('@/views/huawei/Home.vue')
  },
  {
    path: '/detail/:id?',
    name: 'HuaweiDetail',
    component: () => import('@/views/huawei/Detail.vue')
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
