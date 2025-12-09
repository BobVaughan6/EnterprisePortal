<template>
  <div class="min-h-screen bg-gray-50">
    <Header />
    
    <!-- 面包屑导航 -->
    <div class="bg-white border-b">
      <div class="container-wide py-4">
        <div class="flex items-center text-sm text-gray-600">
          <router-link to="/" class="hover:text-hailong-primary transition-colors">首页</router-link>
          <svg class="w-4 h-4 mx-2" fill="none" stroke="currentColor" viewBox="0 0 24 24">
            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M9 5l7 7-7 7" />
          </svg>
          <router-link to="/about" class="hover:text-hailong-primary transition-colors">关于我们</router-link>
          <svg class="w-4 h-4 mx-2" fill="none" stroke="currentColor" viewBox="0 0 24 24">
            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M9 5l7 7-7 7" />
          </svg>
          <span class="text-gray-900">业绩详情</span>
        </div>
      </div>
    </div>

    <!-- 内容区域 -->
    <div class="py-12 bg-white">
      <div class="container-wide">
        <div v-if="loading" class="text-center py-20">
          <div class="inline-block animate-spin rounded-full h-12 w-12 border-4 border-hailong-primary border-t-transparent"></div>
          <p class="mt-4 text-gray-500">加载中...</p>
        </div>

        <div v-else-if="!achievement" class="text-center py-20">
          <svg class="w-20 h-20 mx-auto text-gray-300 mb-4" fill="none" stroke="currentColor" viewBox="0 0 24 24">
            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M9 12l2 2 4-4M7.835 4.697a3.42 3.42 0 001.946-.806 3.42 3.42 0 014.438 0 3.42 3.42 0 001.946.806 3.42 3.42 0 013.138 3.138 3.42 3.42 0 00.806 1.946 3.42 3.42 0 010 4.438 3.42 3.42 0 00-.806 1.946 3.42 3.42 0 01-3.138 3.138 3.42 3.42 0 00-1.946.806 3.42 3.42 0 01-4.438 0 3.42 3.42 0 00-1.946-.806 3.42 3.42 0 01-3.138-3.138 3.42 3.42 0 00-.806-1.946 3.42 3.42 0 010-4.438 3.42 3.42 0 00.806-1.946 3.42 3.42 0 013.138-3.138z" />
          </svg>
          <p class="text-gray-500 mb-4">业绩信息不存在</p>
          <router-link to="/about" class="text-hailong-primary hover:underline">返回关于我们</router-link>
        </div>

        <div v-else class="max-w-6xl mx-auto">
          <!-- 业绩头部 -->
          <div class="bg-gradient-to-br from-hailong-primary via-hailong-secondary to-hailong-dark rounded-2xl shadow-xl p-12 mb-8 text-white relative overflow-hidden">
            <!-- 背景装饰 -->
            <div class="absolute top-0 right-0 w-64 h-64 bg-white/5 rounded-full -translate-y-1/2 translate-x-1/2"></div>
            <div class="absolute bottom-0 left-0 w-48 h-48 bg-white/5 rounded-full translate-y-1/2 -translate-x-1/2"></div>
            
            <div class="relative z-10">
              <!-- 重点标识 -->
              <div v-if="achievement.isHighlight" class="inline-flex items-center gap-2 px-4 py-2 bg-yellow-400/20 backdrop-blur-sm rounded-full text-yellow-300 text-sm font-medium mb-6">
                <svg class="w-5 h-5" fill="currentColor" viewBox="0 0 20 20">
                  <path d="M9.049 2.927c.3-.921 1.603-.921 1.902 0l1.07 3.292a1 1 0 00.95.69h3.462c.969 0 1.371 1.24.588 1.81l-2.8 2.034a1 1 0 00-.364 1.118l1.07 3.292c.3.921-.755 1.688-1.54 1.118l-2.8-2.034a1 1 0 00-1.175 0l-2.8 2.034c-.784.57-1.838-.197-1.539-1.118l1.07-3.292a1 1 0 00-.364-1.118L2.98 8.72c-.783-.57-.38-1.81.588-1.81h3.461a1 1 0 00.951-.69l1.07-3.292z" />
                </svg>
                重点项目
              </div>

              <h1 class="text-4xl font-bold mb-4">{{ achievement.projectName }}</h1>
              
              <div class="flex flex-wrap items-center gap-6 text-white/90">
                <div class="flex items-center gap-2">
                  <svg class="w-5 h-5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M7 7h.01M7 3h5c.512 0 1.024.195 1.414.586l7 7a2 2 0 010 2.828l-7 7a2 2 0 01-2.828 0l-7-7A1.994 1.994 0 013 12V7a4 4 0 014-4z" />
                  </svg>
                  <span>{{ achievement.projectType }}</span>
                </div>
                <div class="flex items-center gap-2">
                  <svg class="w-5 h-5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M12 8c-1.657 0-3 .895-3 2s1.343 2 3 2 3 .895 3 2-1.343 2-3 2m0-8c1.11 0 2.08.402 2.599 1M12 8V7m0 1v8m0 0v1m0-1c-1.11 0-2.08-.402-2.599-1M21 12a9 9 0 11-18 0 9 9 0 0118 0z" />
                  </svg>
                  <span class="text-2xl font-bold">{{ formatAmount(achievement.projectAmount) }}</span>
                </div>
                <div v-if="achievement.completionDate" class="flex items-center gap-2">
                  <svg class="w-5 h-5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M8 7V3m8 4V3m-9 8h10M5 21h14a2 2 0 002-2V7a2 2 0 00-2-2H5a2 2 0 00-2 2v12a2 2 0 002 2z" />
                  </svg>
                  <span>{{ achievement.completionDate }}</span>
                </div>
              </div>
            </div>
          </div>

          <!-- 项目图片 -->
          <div v-if="achievement.imageUrl" class="bg-white rounded-xl shadow-sm overflow-hidden mb-8">
            <img :src="achievement.imageUrl" :alt="achievement.projectName" class="w-full h-96 object-cover" />
          </div>

          <!-- 项目信息卡片 -->
          <div class="grid grid-cols-1 md:grid-cols-3 gap-6 mb-8">
            <!-- 客户信息 -->
            <div v-if="achievement.clientName" class="bg-white rounded-xl shadow-sm p-6">
              <div class="flex items-center gap-3 mb-4">
                <div class="w-12 h-12 bg-hailong-primary/10 rounded-lg flex items-center justify-center">
                  <svg class="w-6 h-6 text-hailong-primary" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M19 21V5a2 2 0 00-2-2H7a2 2 0 00-2 2v16m14 0h2m-2 0h-5m-9 0H3m2 0h5M9 7h1m-1 4h1m4-4h1m-1 4h1m-5 10v-5a1 1 0 011-1h2a1 1 0 011 1v5m-4 0h4" />
                  </svg>
                </div>
                <div>
                  <div class="text-sm text-gray-500">客户单位</div>
                  <div class="text-lg font-bold text-gray-900">{{ achievement.clientName }}</div>
                </div>
              </div>
            </div>

            <!-- 项目金额 -->
            <div class="bg-white rounded-xl shadow-sm p-6">
              <div class="flex items-center gap-3 mb-4">
                <div class="w-12 h-12 bg-green-100 rounded-lg flex items-center justify-center">
                  <svg class="w-6 h-6 text-green-600" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M12 8c-1.657 0-3 .895-3 2s1.343 2 3 2 3 .895 3 2-1.343 2-3 2m0-8c1.11 0 2.08.402 2.599 1M12 8V7m0 1v8m0 0v1m0-1c-1.11 0-2.08-.402-2.599-1M21 12a9 9 0 11-18 0 9 9 0 0118 0z" />
                  </svg>
                </div>
                <div>
                  <div class="text-sm text-gray-500">项目金额</div>
                  <div class="text-lg font-bold text-gray-900">{{ formatAmount(achievement.projectAmount) }}</div>
                </div>
              </div>
            </div>

            <!-- 完成时间 -->
            <div v-if="achievement.completionDate" class="bg-white rounded-xl shadow-sm p-6">
              <div class="flex items-center gap-3 mb-4">
                <div class="w-12 h-12 bg-hailong-secondary/10 rounded-lg flex items-center justify-center">
                  <svg class="w-6 h-6 text-hailong-secondary" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M9 12l2 2 4-4m6 2a9 9 0 11-18 0 9 9 0 0118 0z" />
                  </svg>
                </div>
                <div>
                  <div class="text-sm text-gray-500">完成时间</div>
                  <div class="text-lg font-bold text-gray-900">{{ achievement.completionDate }}</div>
                </div>
              </div>
            </div>
          </div>

          <!-- 项目描述 -->
          <div class="bg-white rounded-xl shadow-sm p-8 mb-8">
            <h2 class="text-2xl font-bold text-gray-900 mb-6 pb-4 border-b border-gray-200 flex items-center gap-3">
              <div class="w-10 h-10 bg-hailong-primary/10 rounded-lg flex items-center justify-center">
                <svg class="w-6 h-6 text-hailong-primary" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                  <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M9 12h6m-6 4h6m2 5H7a2 2 0 01-2-2V5a2 2 0 012-2h5.586a1 1 0 01.707.293l5.414 5.414a1 1 0 01.293.707V19a2 2 0 01-2 2z" />
                </svg>
              </div>
              项目概述
            </h2>
            <div class="prose prose-lg max-w-none text-gray-700 leading-relaxed">
              <p>{{ achievement.description }}</p>
            </div>
          </div>

          <!-- 项目亮点 -->
          <div class="bg-white rounded-xl shadow-sm p-8 mb-8">
            <h2 class="text-2xl font-bold text-gray-900 mb-6 pb-4 border-b border-gray-200 flex items-center gap-3">
              <div class="w-10 h-10 bg-yellow-100 rounded-lg flex items-center justify-center">
                <svg class="w-6 h-6 text-yellow-600" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                  <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M5 3v4M3 5h4M6 17v4m-2-2h4m5-16l2.286 6.857L21 12l-5.714 2.143L13 21l-2.286-6.857L5 12l5.714-2.143L13 3z" />
                </svg>
              </div>
              项目亮点
            </h2>
            <div class="grid grid-cols-1 md:grid-cols-2 gap-4">
              <div
                v-for="(highlight, index) in projectHighlights"
                :key="index"
                class="flex items-start gap-3 p-4 bg-gradient-to-br from-gray-50 to-white rounded-lg border border-gray-100"
              >
                <div class="flex-shrink-0 w-6 h-6 bg-gradient-to-br from-hailong-primary to-hailong-secondary rounded-full flex items-center justify-center">
                  <svg class="w-4 h-4 text-white" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M5 13l4 4L19 7" />
                  </svg>
                </div>
                <p class="text-gray-700 leading-relaxed">{{ highlight }}</p>
              </div>
            </div>
          </div>

          <!-- 服务内容 -->
          <div class="bg-white rounded-xl shadow-sm p-8 mb-8">
            <h2 class="text-2xl font-bold text-gray-900 mb-6 pb-4 border-b border-gray-200 flex items-center gap-3">
              <div class="w-10 h-10 bg-hailong-cyan/10 rounded-lg flex items-center justify-center">
                <svg class="w-6 h-6 text-hailong-cyan" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                  <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M9 5H7a2 2 0 00-2 2v12a2 2 0 002 2h10a2 2 0 002-2V7a2 2 0 00-2-2h-2M9 5a2 2 0 002 2h2a2 2 0 002-2M9 5a2 2 0 012-2h2a2 2 0 012 2m-3 7h3m-3 4h3m-6-4h.01M9 16h.01" />
                </svg>
              </div>
              服务内容
            </h2>
            <div class="space-y-3">
              <div
                v-for="(service, index) in serviceContents"
                :key="index"
                class="flex items-center gap-3 p-4 bg-gray-50 rounded-lg hover:bg-gray-100 transition-colors"
              >
                <div class="flex-shrink-0 w-8 h-8 bg-hailong-primary text-white rounded-lg flex items-center justify-center font-bold text-sm">
                  {{ index + 1 }}
                </div>
                <span class="text-gray-700">{{ service }}</span>
              </div>
            </div>
          </div>

          <!-- 项目成果 -->
          <div class="bg-gradient-to-r from-green-50 to-blue-50 rounded-xl p-8 mb-8">
            <h2 class="text-2xl font-bold text-gray-900 mb-6 flex items-center gap-3">
              <div class="w-10 h-10 bg-white rounded-lg flex items-center justify-center shadow-sm">
                <svg class="w-6 h-6 text-green-600" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                  <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M9 12l2 2 4-4M7.835 4.697a3.42 3.42 0 001.946-.806 3.42 3.42 0 014.438 0 3.42 3.42 0 001.946.806 3.42 3.42 0 013.138 3.138 3.42 3.42 0 00.806 1.946 3.42 3.42 0 010 4.438 3.42 3.42 0 00-.806 1.946 3.42 3.42 0 01-3.138 3.138 3.42 3.42 0 00-1.946.806 3.42 3.42 0 01-4.438 0 3.42 3.42 0 00-1.946-.806 3.42 3.42 0 01-3.138-3.138 3.42 3.42 0 00-.806-1.946 3.42 3.42 0 010-4.438 3.42 3.42 0 00.806-1.946 3.42 3.42 0 013.138-3.138z" />
                </svg>
              </div>
              项目成果
            </h2>
            <div class="grid grid-cols-1 md:grid-cols-3 gap-6">
              <div
                v-for="(result, index) in projectResults"
                :key="index"
                class="bg-white rounded-xl p-6 shadow-sm"
              >
                <div class="text-3xl font-bold text-hailong-primary mb-2">{{ result.value }}</div>
                <div class="text-gray-600">{{ result.label }}</div>
              </div>
            </div>
          </div>

          <!-- 客户评价 -->
          <div v-if="clientFeedback" class="bg-white rounded-xl shadow-sm p-8 mb-8">
            <h2 class="text-2xl font-bold text-gray-900 mb-6 pb-4 border-b border-gray-200 flex items-center gap-3">
              <div class="w-10 h-10 bg-purple-100 rounded-lg flex items-center justify-center">
                <svg class="w-6 h-6 text-purple-600" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                  <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M8 10h.01M12 10h.01M16 10h.01M9 16H5a2 2 0 01-2-2V6a2 2 0 012-2h14a2 2 0 012 2v8a2 2 0 01-2 2h-5l-5 5v-5z" />
                </svg>
              </div>
              客户评价
            </h2>
            <div class="relative">
              <svg class="absolute top-0 left-0 w-12 h-12 text-gray-200 -translate-x-2 -translate-y-2" fill="currentColor" viewBox="0 0 32 32">
                <path d="M9.352 4C4.456 7.456 1 13.12 1 19.36c0 5.088 3.072 8.064 6.624 8.064 3.36 0 5.856-2.688 5.856-5.856 0-3.168-2.208-5.472-5.088-5.472-.576 0-1.344.096-1.536.192.48-3.264 3.552-7.104 6.624-9.024L9.352 4zm16.512 0c-4.8 3.456-8.256 9.12-8.256 15.36 0 5.088 3.072 8.064 6.624 8.064 3.264 0 5.856-2.688 5.856-5.856 0-3.168-2.304-5.472-5.184-5.472-.576 0-1.248.096-1.44.192.48-3.264 3.456-7.104 6.528-9.024L25.864 4z" />
              </svg>
              <p class="text-gray-700 text-lg leading-relaxed pl-10 italic">
                {{ clientFeedback }}
              </p>
            </div>
          </div>

          <!-- 返回按钮 -->
          <div class="flex justify-center">
            <router-link
              to="/about"
              class="px-6 py-3 bg-gray-100 text-gray-700 rounded-lg hover:bg-gray-200 transition-all font-medium flex items-center gap-2"
            >
              <svg class="w-5 h-5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M10 19l-7-7m0 0l7-7m-7 7h18" />
              </svg>
              返回关于我们
            </router-link>
          </div>
        </div>
      </div>
    </div>

    <Footer />
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import { useRoute } from 'vue-router'
import Header from '@/components/Header.vue'
import Footer from '@/components/Footer.vue'

const route = useRoute()

// 业绩数据
const achievement = ref(null)
const loading = ref(true)

// 项目亮点
const projectHighlights = ref([])

// 服务内容
const serviceContents = ref([])

// 项目成果
const projectResults = ref([])

// 客户评价
const clientFeedback = ref('')

// 格式化金额
const formatAmount = (amount) => {
  if (amount >= 10000) {
    return `¥${(amount / 10000).toFixed(2)}亿`
  } else {
    return `¥${amount}万`
  }
}

// 加载业绩详情
const loadAchievementDetail = async () => {
  loading.value = true
  try {
    const id = route.params.id
    
    // 模拟API调用
    await new Promise(resolve => setTimeout(resolve, 500))
    
    // 模拟数据
    achievement.value = {
      id: id,
      projectName: '某市人民医院新院区建设项目全过程造价咨询',
      projectType: '工程造价咨询',
      projectAmount: 25000, // 单位：万元
      clientName: '某市卫生健康委员会',
      completionDate: '2024年10月',
      description: '该项目为某市重点民生工程，总建筑面积约15万平方米，包括门诊楼、住院楼、医技楼等主要建筑。我公司为该项目提供了从项目立项到竣工结算的全过程造价咨询服务，通过科学的造价管理和严格的成本控制，为业主节约投资约3000万元，获得了业主的高度认可。',
      imageUrl: 'https://images.unsplash.com/photo-1519494026892-80bbd2d6fd0d?w=1200&h=600&fit=crop',
      isHighlight: true
    }

    // 项目亮点
    projectHighlights.value = [
      '项目规模大，总投资2.5亿元，是当地重点民生工程',
      '提供全过程造价咨询服务，覆盖项目全生命周期',
      '采用BIM技术辅助造价管理，提高工作效率和准确性',
      '通过科学的成本控制，为业主节约投资约3000万元',
      '项目按期完成，质量优良，获得业主高度评价',
      '积累了大型医疗建筑项目造价管理的丰富经验'
    ]

    // 服务内容
    serviceContents.value = [
      '项目投资估算编制与审核',
      '设计概算编制与审核',
      '施工图预算编制与审核',
      '工程量清单及招标控制价编制',
      '施工过程造价控制与管理',
      '工程变更及签证审核',
      '工程结算审核',
      '竣工决算审核'
    ]

    // 项目成果
    projectResults.value = [
      { value: '15万㎡', label: '建筑面积' },
      { value: '2.5亿', label: '项目总投资' },
      { value: '3000万', label: '节约投资' }
    ]

    // 客户评价
    clientFeedback.value = '海隆咨询团队专业能力强，工作认真负责，在项目全过程中提供了优质的造价咨询服务。通过科学的造价管理和严格的成本控制，有效控制了项目投资，为我们节约了大量资金。特别是在项目实施过程中，能够及时发现问题并提出合理化建议，确保了项目的顺利推进。我们对海隆咨询的服务非常满意，期待未来继续合作。'
  } catch (error) {
    console.error('加载业绩详情失败:', error)
    achievement.value = null
  } finally {
    loading.value = false
  }
}

// 组件挂载时加载数据
onMounted(() => {
  loadAchievementDetail()
})
</script>

<style scoped>
.prose p {
  margin-bottom: 1rem;
  line-height: 1.75;
}
</style>