<template>
  <div class="min-h-screen bg-gradient-to-br from-hailong-dark via-hailong-primary to-hailong-secondary">
    <Header />
    
    <!-- 页面标题 -->
    <div class="pt-32 pb-16 text-center text-white">
      <h1 class="text-6xl font-bold mb-4 font-tech bg-gradient-to-r from-white via-hailong-cyan to-white bg-clip-text text-transparent animate-fade-in">
        新闻中心
      </h1>
      <p class="text-xl text-gray-200">企业动态与重要通知</p>
    </div>

    <!-- 内容区域 -->
    <div class="py-16 bg-white">
      <div class="container-wide">
        <div class="animate-fade-in">
          <!-- 搜索区域 -->
          <div class="bg-white rounded-xl shadow-sm p-6 mb-6">
            <div class="flex gap-3">
              <div class="relative flex-1">
                <svg class="absolute left-4 top-1/2 transform -translate-y-1/2 w-5 h-5 text-gray-400" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                  <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M21 21l-6-6m2-5a7 7 0 11-14 0 7 7 0 0114 0z" />
                </svg>
                <input
                  v-model="keyword"
                  type="text"
                  placeholder="请输入新闻标题关键字"
                  class="w-full pl-12 pr-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-hailong-primary focus:border-transparent outline-none transition-all"
                  @keyup.enter="handleSearch"
                />
              </div>
              <button
                @click="handleSearch"
                class="px-8 py-3 bg-gradient-to-r from-hailong-primary to-hailong-secondary text-white rounded-lg hover:shadow-lg transition-all font-medium"
              >
                搜索
              </button>
              <button
                @click="handleReset"
                class="px-6 py-3 bg-gray-100 text-gray-700 rounded-lg hover:bg-gray-200 transition-all font-medium"
              >
                重置
              </button>
            </div>
          </div>

          <!-- 结果统计 -->
          <div class="mb-4 text-sm text-gray-600">
            共找到 <span class="text-hailong-primary font-semibold">{{ total }}</span> 条新闻
          </div>

          <!-- 列表 -->
          <div v-if="loading" class="text-center py-20">
            <div class="inline-block animate-spin rounded-full h-12 w-12 border-4 border-hailong-primary border-t-transparent"></div>
            <p class="mt-4 text-gray-500">加载中...</p>
          </div>

          <div v-else-if="items.length === 0" class="text-center py-20">
            <svg class="w-20 h-20 mx-auto text-gray-300 mb-4" fill="none" stroke="currentColor" viewBox="0 0 24 24">
              <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M9 12h6m-6 4h6m2 5H7a2 2 0 01-2-2V5a2 2 0 012-2h5.586a1 1 0 01.707.293l5.414 5.414a1 1 0 01.293.707V19a2 2 0 01-2 2z" />
            </svg>
            <p class="text-gray-500">暂无新闻信息</p>
          </div>

          <div v-else class="space-y-4">
            <div
              v-for="item in items"
              :key="item.id"
              @click="handleViewDetail(item.id)"
              class="bg-white rounded-xl p-6 shadow-sm hover:shadow-lg transition-all cursor-pointer border-l-4 border-hailong-secondary"
            >
              <!-- 标题和标签 -->
              <div class="flex items-start justify-between mb-3">
                <h3 class="text-lg font-bold text-gray-900 flex-1 hover:text-hailong-primary transition-colors line-clamp-2">
                  <span v-if="item.isTop" class="inline-block px-2 py-0.5 bg-red-500 text-white text-xs rounded mr-2">置顶</span>
                  {{ item.title }}
                </h3>
                <span
                  :class="[
                    'ml-4 px-3 py-1 rounded-full text-xs font-medium whitespace-nowrap',
                    getTypeStyle(item.type)
                  ]"
                >
                  {{ item.type }}
                </span>
              </div>

              <!-- 摘要 -->
              <p v-if="item.summary" class="text-gray-600 text-sm mb-4 line-clamp-2">
                {{ item.summary }}
              </p>

              <!-- 底部信息 -->
              <div class="flex items-center justify-between pt-4 border-t border-gray-100">
                <div class="flex items-center gap-4 text-xs text-gray-500">
                  <span class="flex items-center gap-1">
                    <svg class="w-4 h-4" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                      <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M8 7V3m8 4V3m-9 8h10M5 21h14a2 2 0 002-2V7a2 2 0 00-2-2H5a2 2 0 00-2 2v12a2 2 0 002 2z" />
                    </svg>
                    {{ item.publishDate }}
                  </span>
                  <span class="flex items-center gap-1">
                    <svg class="w-4 h-4" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                      <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M15 12a3 3 0 11-6 0 3 3 0 016 0z" />
                      <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M2.458 12C3.732 7.943 7.523 5 12 5c4.478 0 8.268 2.943 9.542 7-1.274 4.057-5.064 7-9.542 7-4.477 0-8.268-2.943-9.542-7z" />
                    </svg>
                    {{ item.views || 0 }} 次浏览
                  </span>
                </div>
                <span class="text-hailong-primary text-sm font-medium hover:underline">
                  查看详情 →
                </span>
              </div>
            </div>
          </div>

          <!-- 分页组件 -->
          <div v-if="total > 0" class="mt-8 flex items-center justify-center gap-2">
            <button
              @click="handlePageChange(currentPage - 1)"
              :disabled="currentPage === 1"
              :class="[
                'px-4 py-2 rounded-lg border transition-all',
                currentPage === 1
                  ? 'border-gray-200 text-gray-400 cursor-not-allowed'
                  : 'border-gray-300 text-gray-700 hover:bg-gray-50'
              ]"
            >
              上一页
            </button>

            <div class="flex gap-1">
              <button
                v-for="page in displayPages"
                :key="page"
                @click="page !== '...' && handlePageChange(page)"
                :class="[
                  'px-4 py-2 rounded-lg transition-all',
                  page === currentPage
                    ? 'bg-gradient-to-r from-hailong-primary to-hailong-secondary text-white'
                    : page === '...'
                    ? 'text-gray-400 cursor-default'
                    : 'text-gray-700 hover:bg-gray-50 border border-gray-300'
                ]"
              >
                {{ page }}
              </button>
            </div>

            <button
              @click="handlePageChange(currentPage + 1)"
              :disabled="currentPage === totalPages"
              :class="[
                'px-4 py-2 rounded-lg border transition-all',
                currentPage === totalPages
                  ? 'border-gray-200 text-gray-400 cursor-not-allowed'
                  : 'border-gray-300 text-gray-700 hover:bg-gray-50'
              ]"
            >
              下一页
            </button>
          </div>
        </div>
      </div>
    </div>

    <Footer />
  </div>
</template>

<script setup>
import { ref, computed, onMounted } from 'vue'
import { useRouter } from 'vue-router'
import Header from '@/components/Header.vue'
import Footer from '@/components/Footer.vue'

const router = useRouter()

// 搜索关键字
const keyword = ref('')

// 列表数据
const items = ref([])

// 加载状态
const loading = ref(false)

// 分页参数
const currentPage = ref(1)
const pageSize = ref(10)
const total = ref(0)

// 总页数
const totalPages = computed(() => Math.ceil(total.value / pageSize.value))

// 显示的页码
const displayPages = computed(() => {
  const pages = []
  const maxDisplay = 7
  
  if (totalPages.value <= maxDisplay) {
    for (let i = 1; i <= totalPages.value; i++) {
      pages.push(i)
    }
  } else {
    if (currentPage.value <= 4) {
      for (let i = 1; i <= 5; i++) {
        pages.push(i)
      }
      pages.push('...')
      pages.push(totalPages.value)
    } else if (currentPage.value >= totalPages.value - 3) {
      pages.push(1)
      pages.push('...')
      for (let i = totalPages.value - 4; i <= totalPages.value; i++) {
        pages.push(i)
      }
    } else {
      pages.push(1)
      pages.push('...')
      for (let i = currentPage.value - 1; i <= currentPage.value + 1; i++) {
        pages.push(i)
      }
      pages.push('...')
      pages.push(totalPages.value)
    }
  }
  
  return pages
})

// 加载列表
const loadItems = async () => {
  loading.value = true
  try {
    // 模拟API调用
    await new Promise(resolve => setTimeout(resolve, 500))
    
    const mockData = generateMockData()
    items.value = mockData.slice(
      (currentPage.value - 1) * pageSize.value,
      currentPage.value * pageSize.value
    )
    total.value = mockData.length
  } catch (error) {
    console.error('加载列表失败:', error)
    items.value = []
    total.value = 0
  } finally {
    loading.value = false
  }
}

// 生成模拟数据
const generateMockData = () => {
  const data = []
  const types = ['企业动态', '通知公告', '人事任免', '制度文件']
  
  const titles = {
    '企业动态': [
      '海隆咨询荣获"2024年度优秀咨询企业"称号',
      '公司成功中标某市重大基础设施项目咨询服务',
      '海隆咨询与某高新区签署战略合作协议',
      '公司参加全国工程咨询行业年度峰会',
      '海隆咨询助力某市智慧城市建设项目顺利验收',
      '公司荣获"AAA级信用企业"称号',
      '海隆咨询成功举办工程咨询行业交流会'
    ],
    '通知公告': [
      '关于2025年元旦放假安排的通知',
      '关于开展年度安全生产检查的通知',
      '关于召开2024年度工作总结会议的通知',
      '关于加强项目质量管理的通知',
      '关于规范公文处理流程的通知',
      '关于开展员工培训的通知'
    ],
    '人事任免': [
      '关于任命张三为市场部经理的通知',
      '关于李四同志职务调整的通知',
      '关于王五同志退休的通知',
      '关于聘任赵六为技术顾问的通知',
      '关于表彰2024年度优秀员工的决定'
    ],
    '制度文件': [
      '海隆咨询项目管理制度（2024年修订版）',
      '员工考勤管理办法',
      '差旅费报销管理规定',
      '合同管理实施细则',
      '保密管理制度'
    ]
  }
  
  let id = 1
  for (const type of types) {
    const typeTitle = titles[type]
    for (let i = 0; i < typeTitle.length; i++) {
      data.push({
        id: id++,
        title: typeTitle[i],
        type: type,
        summary: '这是一条新闻摘要，简要介绍新闻的主要内容和重点信息...',
        publishDate: `2024-${String(Math.floor(Math.random() * 12) + 1).padStart(2, '0')}-${String(Math.floor(Math.random() * 28) + 1).padStart(2, '0')}`,
        views: Math.floor(Math.random() * 500) + 50,
        isTop: id <= 3
      })
    }
  }
  
  return data
}

// 搜索
const handleSearch = async () => {
  currentPage.value = 1
  await loadItems()
}

// 重置
const handleReset = () => {
  keyword.value = ''
  handleSearch()
}

// 页码变化
const handlePageChange = (page) => {
  if (page < 1 || page > totalPages.value) return
  currentPage.value = page
  loadItems()
  window.scrollTo({ top: 0, behavior: 'smooth' })
}

// 获取类型样式
const getTypeStyle = (type) => {
  switch (type) {
    case '企业动态':
      return 'bg-blue-100 text-blue-700 border border-blue-200'
    case '通知公告':
      return 'bg-green-100 text-green-700 border border-green-200'
    case '人事任免':
      return 'bg-purple-100 text-purple-700 border border-purple-200'
    case '制度文件':
      return 'bg-orange-100 text-orange-700 border border-orange-200'
    default:
      return 'bg-gray-100 text-gray-800 border border-gray-200'
  }
}

// 查看详情
const handleViewDetail = (id) => {
  router.push(`/detail/company-announcement/${id}`)
}

// 组件挂载时加载数据
onMounted(() => {
  loadItems()
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
</style>