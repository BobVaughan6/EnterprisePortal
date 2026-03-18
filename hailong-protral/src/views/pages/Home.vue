<template>
  <div class="min-h-screen bg-gradient-to-br from-hailong-dark via-hailong-primary to-hailong-secondary overflow-hidden">
    <Header />

    <!-- 动态渲染首页模块 -->
    <component
      v-for="module in enabledModules"
      :key="module.name"
      :is="moduleComponents[module.component]"
      @show-contact="showContactModal = true"
      @announcement-click="handleAnnouncementClick"
      @business-click="handleBusinessClick"
      @qualification-click="handleQualificationClick"
      @achievement-click="handleAchievementClick"
    />

    <Footer />
  </div>
</template>

<script setup>
import { ref, computed } from 'vue'
import { useRouter } from 'vue-router'
import { getSiteConfig } from '@/utils/config'
import Header from '@/components/Header.vue'
import Footer from '@/components/Footer.vue'

// 导入所有模块组件
import HeroBanner from '@/components/home/modules/HeroBanner.vue'
import CompanyIntro from '@/components/home/modules/CompanyIntro.vue'
import AnnouncementList from '@/components/home/modules/AnnouncementList.vue'
import BusinessScope from '@/components/home/modules/BusinessScope.vue'
import QualificationList from '@/components/home/modules/QualificationList.vue'
import AchievementList from '@/components/home/modules/AchievementList.vue'
import DataVisualization from '@/components/home/modules/DataVisualization.vue'
import ContactInfo from '@/components/home/modules/ContactInfo.vue'

const router = useRouter()
const showContactModal = ref(false)

// 组件映射
const moduleComponents = {
  HeroBanner,
  CompanyIntro,
  AnnouncementList,
  BusinessScope,
  QualificationList,
  AchievementList,
  DataVisualization,
  ContactInfo
}

// 获取启用的模块并按 order 排序
const enabledModules = computed(() => {
  const config = getSiteConfig()
  const modules = config.homeModules || []
  return modules
    .filter(m => m.enabled)
    .sort((a, b) => a.order - b.order)
})

// 业务范围点击处理
const handleBusinessClick = (id) => {
  const savedScrollPosition = window.scrollY
  sessionStorage.setItem('homeScrollPosition', savedScrollPosition.toString())
  router.push(`/detail/business-scope/${id}`)
}

// 重要业绩点击处理
const handleAchievementClick = (id) => {
  const savedScrollPosition = window.scrollY
  sessionStorage.setItem('homeScrollPosition', savedScrollPosition.toString())
  router.push(`/detail/achievement/${id}`)
}

// 公告信息点击处理
const handleAnnouncementClick = (id) => {
  const savedScrollPosition = window.scrollY
  sessionStorage.setItem('homeScrollPosition', savedScrollPosition.toString())
  router.push(`/detail/announcement/${id}`)
}

// 企业资质点击处理
const handleQualificationClick = (id) => {
  const savedScrollPosition = window.scrollY
  sessionStorage.setItem('homeScrollPosition', savedScrollPosition.toString())
  router.push(`/detail/qualification/${id}`)
}
</script>
