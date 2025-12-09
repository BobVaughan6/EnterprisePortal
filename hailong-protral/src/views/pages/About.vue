<template>
  <div class="min-h-screen bg-gradient-to-br from-hailong-dark via-hailong-primary to-hailong-secondary">
    <!-- 导航栏 -->
    <Header />

    <!-- 页面标题 -->
    <div class="pt-32 pb-16 text-center text-white">
      <h1 class="text-6xl font-bold mb-4 font-tech bg-gradient-to-r from-white via-hailong-cyan to-white bg-clip-text text-transparent animate-fade-in">
        关于海隆
      </h1>
      <p class="text-xl text-gray-200">海纳百川，才望兼隆</p>
    </div>

    <!-- Tab导航 -->
    <div class="bg-white/10 backdrop-blur-md sticky top-20 z-40 border-b border-white/20">
      <div class="container-wide">
        <div class="flex space-x-8 overflow-x-auto">
          <button v-for="tab in tabs" :key="tab.id"
            @click="activeTab = tab.id"
            class="px-6 py-4 text-white font-medium whitespace-nowrap transition-all border-b-2"
            :class="activeTab === tab.id ? 'border-hailong-cyan text-hailong-cyan' : 'border-transparent hover:text-hailong-cyan'">
            {{ tab.name }}
          </button>
        </div>
      </div>
    </div>

    <!-- 内容区域 -->
    <div class="py-16 bg-white">
      <div class="container-wide">
        <!-- 企业简介 -->
        <div v-show="activeTab === 'intro'" class="animate-fade-in">
          <div class="max-w-5xl mx-auto">
            <div class="bg-white rounded-2xl shadow-2xl overflow-hidden border border-gray-200">
              <div v-if="loading" class="p-12 text-center">
                <div class="inline-block animate-spin rounded-full h-12 w-12 border-b-2 border-hailong-primary"></div>
                <p class="mt-4 text-gray-600">加载中...</p>
              </div>
              <div v-else-if="error" class="p-12 text-center">
                <p class="text-red-600">{{ error }}</p>
                <button @click="fetchCompanyProfile" class="mt-4 px-6 py-2 bg-hailong-primary text-white rounded-lg hover:bg-hailong-secondary transition-colors">
                  重新加载
                </button>
              </div>
              <div v-else>
                <div v-if="companyProfile.imageUrl" class="h-96 overflow-hidden">
                  <img :src="companyProfile.imageUrl" :alt="companyProfile.title"
                    @click="openImagePreview(companyProfile.imageUrl)"
                    class="w-full h-full object-cover cursor-pointer hover:scale-105 transition-transform duration-500" />
                </div>
                <div class="p-12">
                  <h2 class="text-4xl font-bold text-hailong-dark mb-8 font-tech">{{ companyProfile.title }}</h2>
                  <div class="prose prose-lg max-w-none text-gray-700 leading-relaxed" v-html="companyProfile.content"></div>
                </div>
              </div>
            </div>
          </div>
        </div>

        <!-- 业务范围 -->
        <div v-show="activeTab === 'business'" class="animate-fade-in">
          <h2 class="text-4xl font-bold text-hailong-dark mb-8 text-center font-tech">业务范围</h2>
          <div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-8">
            <div v-for="business in businessScope" :key="business.id"
              @click="handleBusinessClick(business.id)"
              class="group bg-white rounded-2xl overflow-hidden shadow-lg hover:shadow-2xl transition-all duration-300 border border-gray-200 hover:border-hailong-primary cursor-pointer">
              <div class="h-48 overflow-hidden">
                <img :src="business.image" :alt="business.name"
                  class="w-full h-full object-cover group-hover:scale-110 transition-transform duration-500" />
              </div>
              <div class="p-6">
                <h3 class="text-2xl font-bold text-gray-900 mb-3 group-hover:text-hailong-primary transition-colors">
                  {{ business.name }}
                </h3>
                <p class="text-gray-600 mb-4">{{ business.description }}</p>
                <ul class="space-y-2">
                  <li v-for="feature in business.features" :key="feature" class="text-sm text-gray-500 flex items-center">
                    <span class="w-1.5 h-1.5 bg-hailong-secondary rounded-full mr-2"></span>
                    {{ feature }}
                  </li>
                </ul>
              </div>
            </div>
          </div>
        </div>

        <!-- 企业资质 -->
        <div v-show="activeTab === 'qualifications'" class="animate-fade-in">
          <h2 class="text-4xl font-bold text-hailong-dark mb-12 text-center font-tech">企业资质</h2>
          <div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-6">
            <div v-for="qualification in qualifications" :key="qualification.id"
              @click="handleQualificationClick(qualification.id)"
              class="group bg-white rounded-xl overflow-hidden shadow-lg hover:shadow-2xl transition-all duration-300 border border-gray-200 hover:border-hailong-primary cursor-pointer">
              <div class="relative h-40 overflow-hidden bg-gradient-to-br from-hailong-primary/10 to-hailong-secondary/10">
                <img :src="qualification.image" :alt="qualification.name"
                  class="w-full h-full object-cover opacity-80 group-hover:opacity-100 group-hover:scale-110 transition-all duration-500" />
                <div class="absolute inset-0 bg-gradient-to-t from-black/60 to-transparent"></div>
                <div class="absolute bottom-0 left-0 right-0 p-4">
                  <div class="w-10 h-10 bg-white/90 rounded-full flex items-center justify-center mb-2">
                    <svg class="w-6 h-6 text-hailong-primary" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                      <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M9 12l2 2 4-4m6 2a9 9 0 11-18 0 9 9 0 0118 0z"></path>
                    </svg>
                  </div>
                </div>
              </div>
              <div class="p-5">
                <h3 class="text-base font-bold text-gray-900 mb-2 group-hover:text-hailong-primary transition-colors line-clamp-2">
                  {{ qualification.name }}
                </h3>
                <p class="text-sm text-gray-600 line-clamp-2">{{ qualification.description }}</p>
              </div>
            </div>
          </div>
        </div>

        <!-- 企业荣誉 -->
        <div v-show="activeTab === 'honors'" class="animate-fade-in">
          <div class="max-w-6xl mx-auto">
            <h2 class="text-4xl font-bold text-hailong-dark mb-12 text-center font-tech">企业荣誉</h2>
            
            <div v-if="loadingAchievements" class="text-center py-12">
              <div class="inline-block animate-spin rounded-full h-12 w-12 border-b-2 border-hailong-primary"></div>
              <p class="mt-4 text-gray-600">加载中...</p>
            </div>
            
            <div v-else-if="achievementsError" class="text-center py-12">
              <p class="text-red-600">{{ achievementsError }}</p>
              <button @click="fetchAchievements" class="mt-4 px-6 py-2 bg-hailong-primary text-white rounded-lg hover:bg-hailong-secondary transition-colors">
                重新加载
              </button>
            </div>
            
            <div v-else class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-8">
              <div v-for="achievement in achievements" :key="achievement.id"
                @click="handleAchievementClick(achievement.id)"
                class="group bg-white rounded-2xl overflow-hidden shadow-lg hover:shadow-2xl transition-all duration-300 border border-gray-200 hover:border-hailong-primary cursor-pointer">
                <div v-if="achievement.imageUrl" class="h-48 overflow-hidden">
                  <img :src="achievement.imageUrl" :alt="achievement.projectName"
                    class="w-full h-full object-cover group-hover:scale-110 transition-transform duration-500" />
                </div>
                <div class="p-6">
                  <div class="flex items-center justify-between mb-3">
                    <span class="px-3 py-1 bg-hailong-primary/10 text-hailong-primary rounded-full text-xs font-semibold">
                      {{ achievement.projectType || '重要业绩' }}
                    </span>
                    <span class="text-sm text-gray-500">{{ formatDate(achievement.completionDate) }}</span>
                  </div>
                  <h3 class="text-xl font-bold text-gray-900 mb-2 group-hover:text-hailong-primary transition-colors line-clamp-2">
                    {{ achievement.projectName }}
                  </h3>
                  <p v-if="achievement.description" class="text-gray-600 text-sm mb-3 line-clamp-2">
                    {{ achievement.description }}
                  </p>
                  <div class="flex items-center justify-between">
                    <span v-if="achievement.clientName" class="text-sm text-gray-500">
                      {{ achievement.clientName }}
                    </span>
                    <span v-if="achievement.projectAmount" class="text-lg font-bold text-hailong-secondary">
                      {{ formatAmount(achievement.projectAmount) }}
                    </span>
                  </div>
                </div>
              </div>
            </div>

            <div v-if="!loadingAchievements && !achievementsError && achievements.length === 0" class="text-center py-12">
              <p class="text-gray-500">暂无企业荣誉数据</p>
            </div>
          </div>
        </div>
      </div>
    </div>

    <!-- 页脚 -->
    <Footer />

    <!-- 图片预览模态框 -->
    <div v-if="showImagePreview"
      @click="closeImagePreview"
      class="fixed inset-0 z-[100] bg-black/90 flex items-center justify-center p-4 animate-fade-in">
      <button @click="closeImagePreview"
        class="absolute top-4 right-4 text-white hover:text-hailong-cyan transition-colors z-10">
        <svg class="w-8 h-8" fill="none" stroke="currentColor" viewBox="0 0 24 24">
          <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M6 18L18 6M6 6l12 12"></path>
        </svg>
      </button>
      <img :src="previewImageUrl"
        @click.stop
        class="max-w-full max-h-full object-contain rounded-lg shadow-2xl"
        alt="预览图片" />
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import { useRouter } from 'vue-router'
import { businessScope } from '@/config/data.js'
import Header from '@/components/Header.vue'
import Footer from '@/components/Footer.vue'

const router = useRouter()

// Tab配置
const tabs = [
  { id: 'intro', name: '企业简介' },
  { id: 'business', name: '业务范围' },
  { id: 'qualifications', name: '企业资质' },
  { id: 'honors', name: '企业荣誉' }
]

const activeTab = ref('intro')

// 企业简介数据
const companyProfile = ref({
  id: 0,
  title: '',
  content: '',
  imageUrl: ''
})
const loading = ref(false)
const error = ref('')

// 企业资质列表（带图片）
const qualifications = [
  {
    id: 1,
    name: '政府采购代理机构资格证书',
    image: 'https://images.unsplash.com/photo-1450101499163-c8848c66ca85?w=800&h=600&fit=crop',
    description: '具备政府采购代理资质，为政府机关提供专业采购服务'
  },
  {
    id: 2,
    name: '工程招标代理机构资格证书',
    image: 'https://images.unsplash.com/photo-1454165804606-c3d57bc86b40?w=800&h=600&fit=crop',
    description: '拥有工程招标代理资质，提供全流程招标代理服务'
  },
  {
    id: 3,
    name: '工程造价咨询企业资质证书',
    image: 'https://images.unsplash.com/photo-1460925895917-afdab827c52f?w=800&h=600&fit=crop',
    description: '专业工程造价咨询资质，提供精准造价分析'
  },
  {
    id: 4,
    name: '工程监理企业资质证书',
    image: 'https://images.unsplash.com/photo-1503387762-592deb58ef4e?w=800&h=600&fit=crop',
    description: '工程监理资质认证，确保工程质量与安全'
  },
  {
    id: 5,
    name: 'ISO9001质量管理体系认证',
    image: 'https://images.unsplash.com/photo-1507679799987-c73779587ccf?w=800&h=600&fit=crop',
    description: '国际质量管理体系认证，保障服务质量'
  },
  {
    id: 6,
    name: 'ISO14001环境管理体系认证',
    image: 'https://images.unsplash.com/photo-1497366216548-37526070297c?w=800&h=600&fit=crop',
    description: '环境管理体系认证，践行绿色发展理念'
  },
  {
    id: 7,
    name: 'OHSAS18001职业健康安全管理体系认证',
    image: 'https://images.unsplash.com/photo-1521737711867-e3b97375f902?w=800&h=600&fit=crop',
    description: '职业健康安全管理认证，保障员工安全'
  },
  {
    id: 8,
    name: 'AAA级信用企业',
    image: 'https://images.unsplash.com/photo-1556761175-b413da4baf72?w=800&h=600&fit=crop',
    description: '最高信用等级认证，诚信经营典范'
  }
]

// 企业荣誉数据（重要业绩）
const achievements = ref([])
const loadingAchievements = ref(false)
const achievementsError = ref('')

// API基础URL
const API_BASE_URL = import.meta.env.VITE_API_BASE_URL || 'http://localhost:5000'

// 获取企业简介
const fetchCompanyProfile = async () => {
  loading.value = true
  error.value = ''
  
  try {
    const response = await fetch(`${API_BASE_URL}/api/config/company-intro`)
    const result = await response.json()
    
    if (result.success && result.data) {
      companyProfile.value = result.data
    } else {
      error.value = result.message || '获取企业简介失败'
    }
  } catch (err) {
    console.error('获取企业简介失败:', err)
    error.value = '网络错误，请稍后重试'
  } finally {
    loading.value = false
  }
}

// 获取企业荣誉（重要业绩）
const fetchAchievements = async () => {
  loadingAchievements.value = true
  achievementsError.value = ''
  
  try {
    const response = await fetch(`${API_BASE_URL}/api/config/achievements`)
    const result = await response.json()
    
    if (result.success && result.data) {
      achievements.value = result.data
    } else {
      achievementsError.value = result.message || '获取企业荣誉失败'
    }
  } catch (err) {
    console.error('获取企业荣誉失败:', err)
    achievementsError.value = '网络错误，请稍后重试'
  } finally {
    loadingAchievements.value = false
  }
}

// 格式化日期
const formatDate = (date) => {
  if (!date) return ''
  const d = new Date(date)
  return `${d.getFullYear()}年${d.getMonth() + 1}月`
}

// 格式化金额
const formatAmount = (amount) => {
  if (!amount) return ''
  return `${amount.toLocaleString()}万元`
}

// 图片预览
const showImagePreview = ref(false)
const previewImageUrl = ref('')

const openImagePreview = (imageUrl) => {
  previewImageUrl.value = imageUrl
  showImagePreview.value = true
  document.body.style.overflow = 'hidden'
}

const closeImagePreview = () => {
  showImagePreview.value = false
  previewImageUrl.value = ''
  document.body.style.overflow = ''
}

// 业务范围点击处理
const handleBusinessClick = (id) => {
  router.push(`/detail/business-scope/${id}`)
}

// 重要业绩点击处理
const handleAchievementClick = (id) => {
  router.push(`/detail/achievement/${id}`)
}

// 企业资质点击处理
const handleQualificationClick = (id) => {
  router.push(`/detail/qualification/${id}`)
}

onMounted(() => {
  fetchCompanyProfile()
  fetchAchievements()
})
</script>

<style scoped>
.line-clamp-2 {
  display: -webkit-box;
  -webkit-line-clamp: 2;
  -webkit-box-orient: vertical;
  overflow: hidden;
}

.animate-fade-in {
  animation: fadeIn 0.5s ease-in;
}

@keyframes fadeIn {
  from {
    opacity: 0;
    transform: translateY(20px);
  }
  to {
    opacity: 1;
    transform: translateY(0);
  }
}

.prose {
  color: #374151;
}

.prose :deep(h1),
.prose :deep(h2),
.prose :deep(h3),
.prose :deep(h4) {
  color: #1f2937;
  font-weight: 700;
  margin-top: 1.5em;
  margin-bottom: 0.75em;
}

.prose :deep(p) {
  margin-bottom: 1em;
  line-height: 1.75;
}

.prose :deep(ul),
.prose :deep(ol) {
  margin-left: 1.5em;
  margin-bottom: 1em;
}

.prose :deep(li) {
  margin-bottom: 0.5em;
}

.prose :deep(strong) {
  color: #1f2937;
  font-weight: 600;
}

.prose :deep(a) {
  color: #3b82f6;
  text-decoration: underline;
}

.prose :deep(img) {
  border-radius: 0.5rem;
  margin: 1.5em 0;
}
</style>