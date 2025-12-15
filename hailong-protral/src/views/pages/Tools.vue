<template>
  <div class="min-h-screen bg-gradient-to-br from-hailong-dark via-hailong-primary to-hailong-secondary">
    <Header />
    
    <!-- 页面标题 -->
    <div class="pt-32 pb-16 text-center text-white">
      <h1 class="text-6xl font-bold mb-4 font-tech bg-gradient-to-r from-white via-hailong-cyan to-white bg-clip-text text-transparent animate-fade-in">
        实用工具
      </h1>
      <p class="text-xl text-gray-200">专业费用测算工具，快速准确计算</p>
    </div>

    <!-- 内容区域 -->
    <div class="py-16 bg-white">
      <div class="container-wide">
        <div class="animate-fade-in">
          <!-- 工具列表 -->
          <div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6 mb-12">
            <div
              v-for="tool in tools"
              :key="tool.id"
              class="group bg-white rounded-xl overflow-hidden shadow-lg hover:shadow-2xl transition-all duration-300 border border-gray-200 hover:border-hailong-primary"
            >
              <div class="relative h-48 overflow-hidden bg-gradient-to-br from-hailong-primary/10 to-hailong-secondary/10">
                <div class="absolute inset-0 flex items-center justify-center">
                  <svg class="w-24 h-24 text-hailong-primary/30 group-hover:text-hailong-primary/50 transition-colors" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="1.5" :d="tool.icon" />
                  </svg>
                </div>
                <div class="absolute top-4 right-4">
                  <span :class="[
                    'px-3 py-1 rounded-full text-xs font-semibold',
                    tool.status === 'available' ? 'bg-green-100 text-green-700' :
                    tool.status === 'coming' ? 'bg-yellow-100 text-yellow-700' :
                    'bg-gray-100 text-gray-700'
                  ]">
                    {{ tool.status === 'available' ? '可用' : tool.status === 'coming' ? '即将上线' : '维护中' }}
                  </span>
                </div>
              </div>
              
              <div class="p-6">
                <h3 class="text-xl font-bold text-gray-900 mb-2 group-hover:text-hailong-primary transition-colors">
                  {{ tool.name }}
                </h3>
                <p class="text-gray-600 text-sm mb-4 line-clamp-2">
                  {{ tool.description }}
                </p>
                
                <div class="flex items-center justify-end pt-4 border-t border-gray-100">
                  <button
                    v-if="tool.status === 'available'"
                    @click="handleToolClick(tool)"
                    class="px-4 py-2 bg-gradient-to-r from-hailong-primary to-hailong-secondary text-white rounded-lg hover:shadow-lg transition-all text-sm font-medium"
                  >
                    立即使用
                  </button>
                  <button
                    v-else
                    disabled
                    class="px-4 py-2 bg-gray-200 text-gray-500 rounded-lg cursor-not-allowed text-sm font-medium"
                  >
                    {{ tool.status === 'coming' ? '敬请期待' : '维护中' }}
                  </button>
                </div>
              </div>
            </div>
          </div>
          
          <!-- 招标代理服务费计算工具 -->
          <BiddingCalculator v-if="showBiddingCalculator" @close="showBiddingCalculator = false" class="mb-12" />

          <!-- 造价费用计算工具 -->
          <CostCalculator v-if="showCostCalculator" @close="showCostCalculator = false" class="mb-12" />

          <!-- 司法鉴定费用计算工具 -->
          <JudicialAppraisalCalculator v-if="showJudicialCalculator" @close="showJudicialCalculator = false" class="mb-12" />

          <!-- 使用说明 -->
          <div class="bg-gradient-to-r from-hailong-primary/5 to-hailong-secondary/5 rounded-xl p-8 border border-hailong-primary/20">
            <h2 class="text-2xl font-bold text-hailong-dark mb-4 flex items-center gap-2">
              <svg class="w-6 h-6 text-hailong-primary" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M13 16h-1v-4h-1m1-4h.01M21 12a9 9 0 11-18 0 9 9 0 0118 0z" />
              </svg>
              使用说明
            </h2>
            <div class="grid grid-cols-1 md:grid-cols-2 gap-6 text-gray-700">
              <div class="flex gap-3">
                <div class="flex-shrink-0 w-8 h-8 bg-hailong-primary text-white rounded-full flex items-center justify-center font-bold">1</div>
                <div>
                  <h3 class="font-semibold mb-1">选择工具</h3>
                  <p class="text-sm text-gray-600">根据您的需求选择合适的工具</p>
                </div>
              </div>
              <div class="flex gap-3">
                <div class="flex-shrink-0 w-8 h-8 bg-hailong-primary text-white rounded-full flex items-center justify-center font-bold">2</div>
                <div>
                  <h3 class="font-semibold mb-1">点击使用</h3>
                  <p class="text-sm text-gray-600">点击"立即使用"按钮打开工具</p>
                </div>
              </div>
              <div class="flex gap-3">
                <div class="flex-shrink-0 w-8 h-8 bg-hailong-primary text-white rounded-full flex items-center justify-center font-bold">3</div>
                <div>
                  <h3 class="font-semibold mb-1">输入数据</h3>
                  <p class="text-sm text-gray-600">按照提示输入相关数据和参数</p>
                </div>
              </div>
              <div class="flex gap-3">
                <div class="flex-shrink-0 w-8 h-8 bg-hailong-primary text-white rounded-full flex items-center justify-center font-bold">4</div>
                <div>
                  <h3 class="font-semibold mb-1">获取结果</h3>
                  <p class="text-sm text-gray-600">系统自动计算并展示结果，支持复制</p>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>

    <Footer />
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import Header from '@/components/Header.vue'
import Footer from '@/components/Footer.vue'
import BiddingCalculator from '@/components/BiddingCalculator.vue'
import CostCalculator from '@/components/CostCalculator.vue'
import JudicialAppraisalCalculator from '@/components/JudicialAppraisalCalculator.vue'

// 页面加载时滚动到顶部
onMounted(() => {
  window.scrollTo(0, 0)
})

// 工具列表
const tools = [
  {
    id: 1,
    name: '招标代理服务费计算工具',
    description: '根据河南省招标代理服务收费指导意见，快速计算工程、货物、服务类项目的代理服务费用',
    icon: 'M9 7h6m0 10v-3m-3 3h.01M9 17h.01M9 14h.01M12 14h.01M15 11h.01M12 11h.01M9 11h.01M7 21h10a2 2 0 002-2V5a2 2 0 00-2-2H7a2 2 0 00-2 2v14a2 2 0 002 2z',
    status: 'available'
  },
  {
    id: 2,
    name: '造价费用计算工具',
    description: '依据河南省建设工程造价咨询行业服务收费市场参考价格，提供专业的造价咨询费用计算',
    icon: 'M12 8c-1.657 0-3 .895-3 2s1.343 2 3 2 3 .895 3 2-1.343 2-3 2m0-8c1.11 0 2.08.402 2.599 1M12 8V7m0 1v8m0 0v1m0-1c-1.11 0-2.08-.402-2.599-1M21 12a9 9 0 11-18 0 9 9 0 0118 0z',
    status: 'available'
  },
  {
    id: 3,
    name: '司法鉴定费用计算工具',
    description: '基于建设工程造价咨询收费基准价，采用差额定率分档累进方法，精确计算工程造价纠纷鉴定等各类咨询项目费用',
    icon: 'M3 6l3 1m0 0l-3 9a5.002 5.002 0 006.001 0M6 7l3 9M6 7l6-2m6 2l3-1m-3 1l-3 9a5.002 5.002 0 006.001 0M18 7l3 9m-3-9l-6-2m0-2v2m0 16V5m0 16H9m3 0h3',
    status: 'coming'
  }
]

// 显示计算器
const showBiddingCalculator = ref(false)
const showCostCalculator = ref(false)
const showJudicialCalculator = ref(false)

// 处理工具点击
const handleToolClick = (tool) => {
  // 先关闭所有计算器
  showBiddingCalculator.value = false
  showCostCalculator.value = false
  showJudicialCalculator.value = false
  
  // 打开对应的计算器
  if (tool.id === 1) {
    showBiddingCalculator.value = true
  } else if (tool.id === 2) {
    showCostCalculator.value = true
  } else if (tool.id === 3) {
    showJudicialCalculator.value = true
  }
  
  scrollToCalculator()
}

// 滚动到计算器位置
const scrollToCalculator = () => {
  setTimeout(() => {
    const calculator = document.querySelector('.bg-white.rounded-xl.shadow-2xl')
    if (calculator) {
      const headerHeight = 80 // 考虑固定头部的高度
      const calculatorTop = calculator.getBoundingClientRect().top + window.pageYOffset - headerHeight - 20
      window.scrollTo({ top: calculatorTop, behavior: 'smooth' })
    }
  }, 100)
}
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
</style>