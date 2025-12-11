<template>
  <!-- 首屏英雄区 -->
  <div class="relative h-screen flex items-center justify-center pt-20">
    <div class="absolute inset-0">
      <div
        class="absolute top-1/4 left-1/4 w-96 h-96 bg-hailong-primary rounded-full filter blur-3xl opacity-20 animate-float">
      </div>
      <div
        class="absolute bottom-1/4 right-1/4 w-96 h-96 bg-hailong-secondary rounded-full filter blur-3xl opacity-20 animate-float"
        style="animation-delay:1s"></div>
      <div v-for="i in 20" :key="i" class="absolute w-1 h-1 bg-hailong-cyan rounded-full animate-ping"
        :style="{ top: `${Math.random() * 100}%`, left: `${Math.random() * 100}%`, animationDelay: `${Math.random() * 2}s` }">
      </div>
    </div>
    <div class="relative text-center text-white px-4">
      <h1
        class="text-7xl font-bold mb-6 font-tech bg-gradient-to-r from-white via-hailong-cyan to-white bg-clip-text text-transparent animate-fade-in">
        {{ companyInfo.slogan }}</h1>
      <p class="text-2xl text-gray-200 mb-12 max-w-4xl mx-auto">{{ companyInfo.description }}</p>
      <button @click="$emit('show-contact')"
        class="inline-block px-12 py-5 bg-gradient-to-r from-hailong-primary to-hailong-secondary rounded-full text-white text-lg font-medium hover:shadow-2xl hover:shadow-hailong-primary/50 transition-all transform hover:scale-105">
        立即咨询
      </button>
    </div>
  </div>

  <!-- 企业简介 -->
  <div class="py-24 bg-gradient-to-b from-gray-50 to-white">
    <div class="container-wide">
      <div class="text-center mb-16">
        <h2 class="text-5xl font-bold text-hailong-dark mb-4 font-tech">企业简介</h2>
        <div class="w-24 h-1 bg-gradient-to-r from-hailong-primary to-hailong-secondary mx-auto"></div>
      </div>
      <div v-if="profileLoading" class="text-center py-8 text-gray-500">加载中...</div>
      <div v-else-if="!profileContent" class="text-center py-8 text-gray-500">暂无企业简介</div>
      <div v-else class="bg-white rounded-2xl p-8 md:p-12 shadow-lg border border-gray-200">
        <div class="grid grid-cols-1 lg:grid-cols-3 gap-8 items-center">
          <div class="lg:col-span-2">
            <p class="text-gray-700 text-lg leading-relaxed whitespace-pre-line">
              {{ profileContent }}
            </p>
          </div>
          <div v-if="profileHighlights.length > 0" class="grid grid-cols-2 gap-4">
            <div v-for="highlight in profileHighlights" :key="highlight"
              class="text-center p-6 bg-gradient-to-br from-hailong-primary/5 to-hailong-secondary/5 rounded-xl hover:shadow-md hover:scale-105 transition-all duration-300 cursor-pointer group">
              <div
                class="w-12 h-12 bg-hailong-primary/10 rounded-full flex items-center justify-center mx-auto mb-3 group-hover:bg-hailong-primary/20 transition-colors">
                <svg class="w-6 h-6 text-hailong-primary" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                  <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M5 13l4 4L19 7"></path>
                </svg>
              </div>
              <div
                class="text-hailong-primary font-semibold text-sm group-hover:text-hailong-secondary transition-colors">
                {{ highlight }}</div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>

  <!-- 公告信息 -->
  <div class="py-24 bg-gradient-to-b from-white to-blue-50/30">
    <div class="container-wide">
      <div class="text-center mb-16">
        <h2 class="text-5xl font-bold text-hailong-dark mb-4 font-tech">公告信息</h2>
        <div class="w-24 h-1 bg-gradient-to-r from-hailong-primary to-hailong-secondary mx-auto"></div>
      </div>
      <div class="grid grid-cols-1 lg:grid-cols-2 gap-8">
        <!-- 政府采购 -->
        <div>
          <div class="flex items-center justify-between mb-6">
            <h3 class="text-2xl font-bold text-hailong-dark">政府采购</h3>
            <router-link to="/announcements?tab=GOV_PROCUREMENT"
              class="text-hailong-primary hover:underline text-sm">查看全部 →</router-link>
          </div>
          <div v-if="loading" class="text-center py-8 text-gray-500">加载中...</div>
          <div v-else-if="govProcurementList.length === 0" class="text-center py-8 text-gray-500">暂无公告</div>
          <div v-else class="space-y-4">
            <div v-for="announcement in govProcurementList" :key="announcement.id"
              @click="$emit('announcement-click', announcement.id)"
              class="p-6 bg-white rounded-xl hover:shadow-lg transition-all cursor-pointer border-l-4 border-hailong-primary">
              <div class="flex justify-between items-start mb-3">
                <span class="px-3 py-1 bg-blue-100 text-blue-800 rounded-full text-xs">{{ announcement.noticeType }}</span>
                <span class="text-xs text-gray-500">{{ formatDate(announcement.publishTime) }}</span>
              </div>
              <h4 class="text-lg font-bold text-gray-900 mb-2 line-clamp-2">{{ announcement.title }}</h4>
              <div class="flex justify-between items-center text-sm">
                <span class="text-gray-600">区域: <strong class="text-hailong-primary">{{ announcement.projectRegion }}</strong></span>
              </div>
            </div>
          </div>
        </div>

        <!-- 建设工程 -->
        <div>
          <div class="flex items-center justify-between mb-6">
            <h3 class="text-2xl font-bold text-hailong-dark">建设工程</h3>
            <router-link to="/announcements?tab=CONSTRUCTION"
              class="text-hailong-primary hover:underline text-sm">查看全部 →</router-link>
          </div>
          <div v-if="loading" class="text-center py-8 text-gray-500">加载中...</div>
          <div v-else-if="constructionList.length === 0" class="text-center py-8 text-gray-500">暂无公告</div>
          <div v-else class="space-y-4">
            <div v-for="announcement in constructionList" :key="announcement.id"
              @click="$emit('announcement-click', announcement.id)"
              class="p-6 bg-white rounded-xl hover:shadow-lg transition-all cursor-pointer border-l-4 border-hailong-secondary">
              <div class="flex justify-between items-start mb-3">
                <span class="px-3 py-1 bg-green-100 text-green-800 rounded-full text-xs">{{ announcement.noticeType }}</span>
                <span class="text-xs text-gray-500">{{ formatDate(announcement.publishTime) }}</span>
              </div>
              <h4 class="text-lg font-bold text-gray-900 mb-2 line-clamp-2">{{ announcement.title }}</h4>
              <div class="flex justify-between items-center text-sm">
                <span class="text-gray-600">区域: <strong class="text-hailong-secondary">{{ announcement.projectRegion }}</strong></span>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, computed, onMounted } from 'vue'
import { getCompanyInfo } from '@/utils/config'
import { getCompanyProfile } from '@/api/config'
import { getRecentAnnouncements } from '@/api/home'

defineEmits(['show-contact', 'announcement-click'])

const companyInfo = computed(() => getCompanyInfo())

// 企业简介数据
const profileLoading = ref(false)
const profileContent = ref('')
const profileHighlights = ref([])

// 公告数据
const loading = ref(false)
const govProcurementList = ref([])
const constructionList = ref([])

// 加载企业简介
const loadCompanyProfile = async () => {
  profileLoading.value = true
  try {
    const response = await getCompanyProfile()
    if (response.success && response.data) {
      profileContent.value = response.data.content || ''
      profileHighlights.value = response.data.highlights || []
    }
  } catch (error) {
    console.error('加载企业简介失败:', error)
  } finally {
    profileLoading.value = false
  }
}

// 加载最新公告
const loadRecentAnnouncements = async () => {
  loading.value = true
  try {
    const response = await getRecentAnnouncements()
    if (response.success && response.data) {
      // 根据sourceType分类公告
      govProcurementList.value = response.data
        .filter(item => item.sourceType === '政府采购')
        .slice(0, 3)
      constructionList.value = response.data
        .filter(item => item.sourceType === '建设工程')
        .slice(0, 3)
    }
  } catch (error) {
    console.error('加载最新公告失败:', error)
  } finally {
    loading.value = false
  }
}

// 格式化日期
const formatDate = (dateString) => {
  if (!dateString) return ''
  const date = new Date(dateString)
  return date.toLocaleDateString('zh-CN', { year: 'numeric', month: '2-digit', day: '2-digit' })
}

onMounted(() => {
  loadCompanyProfile()
  loadRecentAnnouncements()
})
</script>

<style scoped>
.line-clamp-2 {
  display: -webkit-box;
  -webkit-line-clamp: 2;
  -webkit-box-orient: vertical;
  overflow: hidden
}
</style>