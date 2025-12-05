<template>
  <div class="min-h-screen bg-gradient-to-br from-hailong-dark via-hailong-primary to-hailong-secondary">
    <Header />
    
    <!-- 页面标题 -->
    <div class="pt-32 pb-16 text-center text-white">
      <h1 class="text-6xl font-bold mb-4 font-tech bg-gradient-to-r from-white via-hailong-cyan to-white bg-clip-text text-transparent animate-fade-in">
        实用工具
      </h1>
      <p class="text-xl text-gray-200">专业工具助力高效工作</p>
    </div>

    <!-- 内容区域 -->
    <div class="py-16 bg-white">
      <div class="container-wide">
        <div class="animate-fade-in">
          <!-- 工具分类导航 -->
          <div class="bg-white rounded-xl shadow-sm p-6 mb-8">
            <div class="flex flex-wrap gap-3">
              <button
                v-for="category in toolCategories"
                :key="category.value"
                @click="activeCategory = category.value"
                :class="[
                  'px-6 py-3 rounded-lg font-medium transition-all',
                  activeCategory === category.value
                    ? 'bg-gradient-to-r from-hailong-primary to-hailong-secondary text-white shadow-lg'
                    : 'bg-gray-100 text-gray-700 hover:bg-gray-200'
                ]"
              >
                <span class="flex items-center gap-2">
                  <svg class="w-5 h-5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" :d="category.icon" />
                  </svg>
                  {{ category.label }}
                </span>
              </button>
            </div>
          </div>

          <!-- 工具列表 -->
          <div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
            <div
              v-for="tool in filteredTools"
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
                
                <div class="flex items-center justify-between pt-4 border-t border-gray-100">
                  <div class="flex items-center gap-2 text-xs text-gray-500">
                    <svg class="w-4 h-4" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                      <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M12 4.354a4 4 0 110 5.292M15 21H3v-1a6 6 0 0112 0v1zm0 0h6v-1a6 6 0 00-9-5.197M13 7a4 4 0 11-8 0 4 4 0 018 0z" />
                    </svg>
                    <span>{{ tool.users || 0 }} 人使用</span>
                  </div>
                  
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

          <!-- 空状态 -->
          <div v-if="filteredTools.length === 0" class="text-center py-20">
            <svg class="w-20 h-20 mx-auto text-gray-300 mb-4" fill="none" stroke="currentColor" viewBox="0 0 24 24">
              <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M20 13V6a2 2 0 00-2-2H6a2 2 0 00-2 2v7m16 0v5a2 2 0 01-2 2H6a2 2 0 01-2-2v-5m16 0h-2.586a1 1 0 00-.707.293l-2.414 2.414a1 1 0 01-.707.293h-3.172a1 1 0 01-.707-.293l-2.414-2.414A1 1 0 006.586 13H4" />
            </svg>
            <p class="text-gray-500">该分类暂无工具</p>
          </div>

          <!-- 使用说明 -->
          <div class="mt-12 bg-gradient-to-r from-hailong-primary/5 to-hailong-secondary/5 rounded-xl p-8 border border-hailong-primary/20">
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
                  <p class="text-sm text-gray-600">根据您的需求选择合适的工具类别和具体工具</p>
                </div>
              </div>
              <div class="flex gap-3">
                <div class="flex-shrink-0 w-8 h-8 bg-hailong-primary text-white rounded-full flex items-center justify-center font-bold">2</div>
                <div>
                  <h3 class="font-semibold mb-1">点击使用</h3>
                  <p class="text-sm text-gray-600">点击"立即使用"按钮进入工具页面</p>
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
                  <p class="text-sm text-gray-600">系统自动计算并展示结果，支持导出和打印</p>
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
import { ref, computed } from 'vue'
import { useRouter } from 'vue-router'
import Header from '@/components/Header.vue'
import Footer from '@/components/Footer.vue'

const router = useRouter()

// 工具分类
const toolCategories = [
  { 
    label: '全部工具', 
    value: 'all',
    icon: 'M4 6a2 2 0 012-2h2a2 2 0 012 2v2a2 2 0 01-2 2H6a2 2 0 01-2-2V6zM14 6a2 2 0 012-2h2a2 2 0 012 2v2a2 2 0 01-2 2h-2a2 2 0 01-2-2V6zM4 16a2 2 0 012-2h2a2 2 0 012 2v2a2 2 0 01-2 2H6a2 2 0 01-2-2v-2zM14 16a2 2 0 012-2h2a2 2 0 012 2v2a2 2 0 01-2 2h-2a2 2 0 01-2-2v-2z'
  },
  { 
    label: '造价计算', 
    value: 'cost',
    icon: 'M9 7h6m0 10v-3m-3 3h.01M9 17h.01M9 14h.01M12 14h.01M15 11h.01M12 11h.01M9 11h.01M7 21h10a2 2 0 002-2V5a2 2 0 00-2-2H7a2 2 0 00-2 2v14a2 2 0 002 2z'
  },
  { 
    label: '文档模板', 
    value: 'template',
    icon: 'M9 12h6m-6 4h6m2 5H7a2 2 0 01-2-2V5a2 2 0 012-2h5.586a1 1 0 01.707.293l5.414 5.414a1 1 0 01.293.707V19a2 2 0 01-2 2z'
  },
  { 
    label: '数据分析', 
    value: 'analysis',
    icon: 'M9 19v-6a2 2 0 00-2-2H5a2 2 0 00-2 2v6a2 2 0 002 2h2a2 2 0 002-2zm0 0V9a2 2 0 012-2h2a2 2 0 012 2v10m-6 0a2 2 0 002 2h2a2 2 0 002-2m0 0V5a2 2 0 012-2h2a2 2 0 012 2v14a2 2 0 01-2 2h-2a2 2 0 01-2-2z'
  },
  { 
    label: '其他工具', 
    value: 'other',
    icon: 'M12 6V4m0 2a2 2 0 100 4m0-4a2 2 0 110 4m-6 8a2 2 0 100-4m0 4a2 2 0 110-4m0 4v2m0-6V4m6 6v10m6-2a2 2 0 100-4m0 4a2 2 0 110-4m0 4v2m0-6V4'
  }
]

// 当前选中的分类
const activeCategory = ref('all')

// 工具列表
const tools = [
  {
    id: 1,
    name: '工程量清单计算器',
    category: 'cost',
    description: '快速计算工程量清单，支持多种计量单位和计算规则，自动生成报表',
    icon: 'M9 7h6m0 10v-3m-3 3h.01M9 17h.01M9 14h.01M12 14h.01M15 11h.01M12 11h.01M9 11h.01M7 21h10a2 2 0 002-2V5a2 2 0 00-2-2H7a2 2 0 00-2 2v14a2 2 0 002 2z',
    status: 'coming',
    users: 1250
  },
  {
    id: 2,
    name: '造价指标查询',
    category: 'cost',
    description: '提供各类工程造价指标查询，包括人工、材料、机械等价格信息',
    icon: 'M21 21l-6-6m2-5a7 7 0 11-14 0 7 7 0 0114 0z',
    status: 'coming',
    users: 980
  },
  {
    id: 3,
    name: '招标文件模板',
    category: 'template',
    description: '提供标准化的招标文件模板，包括货物、服务、工程等各类项目',
    icon: 'M9 12h6m-6 4h6m2 5H7a2 2 0 01-2-2V5a2 2 0 012-2h5.586a1 1 0 01.707.293l5.414 5.414a1 1 0 01.293.707V19a2 2 0 01-2 2z',
    status: 'coming',
    users: 2100
  },
  {
    id: 4,
    name: '评标报告生成器',
    category: 'template',
    description: '自动生成规范的评标报告，支持多种评标方法和自定义模板',
    icon: 'M9 12h6m-6 4h6m2 5H7a2 2 0 01-2-2V5a2 2 0 012-2h5.586a1 1 0 01.707.293l5.414 5.414a1 1 0 01.293.707V19a2 2 0 01-2 2z',
    status: 'coming',
    users: 1560
  },
  {
    id: 5,
    name: '项目数据分析',
    category: 'analysis',
    description: '对项目数据进行多维度分析，生成可视化图表和分析报告',
    icon: 'M9 19v-6a2 2 0 00-2-2H5a2 2 0 00-2 2v6a2 2 0 002 2h2a2 2 0 002-2zm0 0V9a2 2 0 012-2h2a2 2 0 012 2v10m-6 0a2 2 0 002 2h2a2 2 0 002-2m0 0V5a2 2 0 012-2h2a2 2 0 012 2v14a2 2 0 01-2 2h-2a2 2 0 01-2-2z',
    status: 'coming',
    users: 850
  },
  {
    id: 6,
    name: '投标报价分析',
    category: 'analysis',
    description: '分析投标报价的合理性，识别异常报价，辅助评标决策',
    icon: 'M9 19v-6a2 2 0 00-2-2H5a2 2 0 00-2 2v6a2 2 0 002 2h2a2 2 0 002-2zm0 0V9a2 2 0 012-2h2a2 2 0 012 2v10m-6 0a2 2 0 002 2h2a2 2 0 002-2m0 0V5a2 2 0 012-2h2a2 2 0 012 2v14a2 2 0 01-2 2h-2a2 2 0 01-2-2z',
    status: 'coming',
    users: 720
  },
  {
    id: 7,
    name: '工期计算器',
    category: 'other',
    description: '计算工程工期，考虑节假日、工作日等因素，生成施工进度计划',
    icon: 'M8 7V3m8 4V3m-9 8h10M5 21h14a2 2 0 002-2V7a2 2 0 00-2-2H5a2 2 0 00-2 2v12a2 2 0 002 2z',
    status: 'coming',
    users: 1100
  },
  {
    id: 8,
    name: '单位换算工具',
    category: 'other',
    description: '支持各类工程单位换算，包括长度、面积、体积、重量等',
    icon: 'M8 7h12m0 0l-4-4m4 4l-4 4m0 6H4m0 0l4 4m-4-4l4-4',
    status: 'coming',
    users: 1850
  },
  {
    id: 9,
    name: '税费计算器',
    category: 'cost',
    description: '计算工程项目相关税费，包括增值税、企业所得税等',
    icon: 'M12 8c-1.657 0-3 .895-3 2s1.343 2 3 2 3 .895 3 2-1.343 2-3 2m0-8c1.11 0 2.08.402 2.599 1M12 8V7m0 1v8m0 0v1m0-1c-1.11 0-2.08-.402-2.599-1M21 12a9 9 0 11-18 0 9 9 0 0118 0z',
    status: 'coming',
    users: 950
  }
]

// 过滤后的工具列表
const filteredTools = computed(() => {
  if (activeCategory.value === 'all') {
    return tools
  }
  return tools.filter(tool => tool.category === activeCategory.value)
})

// 处理工具点击
const handleToolClick = (tool) => {
  // 这里可以跳转到具体的工具页面
  console.log('使用工具:', tool.name)
  // router.push(`/tools/${tool.id}`)
  
  // 暂时显示提示
  alert(`${tool.name} 功能即将上线，敬请期待！`)
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