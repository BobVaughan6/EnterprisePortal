<template>
  <div class="min-h-screen bg-gradient-to-br from-hailong-dark via-hailong-primary to-hailong-secondary overflow-hidden">
    <Header />
    
    <!-- 首屏英雄区 + 企业简介 + 公告信息 -->
    <HeroSection 
      @show-contact="showContactModal = true"
      @announcement-click="handleAnnouncementClick"
    />
    
    <!-- 业务范围 + 企业资质 -->
    <BusinessSection 
      @business-click="handleBusinessClick"
      @qualification-click="handleQualificationClick"
    />
    
    <!-- 重要业绩 + 交易数据可视化 -->
    <DataSection 
      @achievement-click="handleAchievementClick"
    />
    
    <!-- 联系我们区块 + 模态框 -->
    <ContactSection 
      :show-modal="showContactModal"
      @close-modal="showContactModal = false"
    />
    
    <Footer />
  </div>
</template>

<script setup>
import { ref } from 'vue'
import { useRouter } from 'vue-router'
import Header from '@/components/Header.vue'
import Footer from '@/components/Footer.vue'
import HeroSection from '@/components/home/HeroSection.vue'
import BusinessSection from '@/components/home/BusinessSection.vue'
import DataSection from '@/components/home/DataSection.vue'
import ContactSection from '@/components/home/ContactSection.vue'

const router = useRouter()

// 联系信息模态框
const showContactModal = ref(false)

// 业务范围点击处理
const handleBusinessClick = (id) => {
  router.push(`/detail/business-scope/${id}`)
}

// 重要业绩点击处理
const handleAchievementClick = (id) => {
  router.push(`/detail/achievement/${id}`)
}

// 公告信息点击处理
const handleAnnouncementClick = (id) => {
  router.push(`/detail/announcement/${id}`)
}

// 企业资质点击处理
const handleQualificationClick = (id) => {
  router.push(`/detail/qualification/${id}`)
}
</script>
