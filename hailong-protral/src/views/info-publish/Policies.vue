<template>
  <div class="min-h-screen bg-gradient-to-br from-hailong-dark via-hailong-primary to-hailong-secondary">
    <Header />
    
    <!-- 页面标题 -->
    <div class="pt-32 pb-16 text-center text-white">
      <h1 class="text-6xl font-bold mb-4 font-tech bg-gradient-to-r from-white via-hailong-cyan to-white bg-clip-text text-transparent animate-fade-in">
        政策法规
      </h1>
      <p class="text-xl text-gray-200">行业政策与法律法规</p>
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
                  placeholder="请输入政策法规标题关键字"
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
            共找到 <span class="text-hailong-primary font-semibold">{{ total }}</span> 条政策法规
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
            <p class="text-gray-500">暂无政策法规信息</p>
          </div>

          <div v-else class="space-y-4">
            <div
              v-for="item in items"
              :key="item.id"
              @click="handleViewDetail(item.id)"
              class="bg-white rounded-xl p-6 shadow-sm hover:shadow-lg transition-all cursor-pointer border-l-4 border-hailong-primary"
            >
              <!-- 标题和标签 -->
              <div class="flex items-start justify-between mb-3">
                <h3 class="text-lg font-bold text-gray-900 flex-1 hover:text-hailong-primary transition-colors line-clamp-2">
                  <span v-if="item.isTop" class="inline-block px-2 py-0.5 bg-red-500 text-white text-xs rounded mr-2">置顶</span>
                  {{ item.title }}
                </h3>
                <span 
                  v-if="item.category"
                  class="ml-4 px-3 py-1 rounded-full text-xs font-medium whitespace-nowrap bg-blue-100 text-blue-700 border border-blue-200"
                >
                  {{ item.category }}
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
                    {{ formatDate(item.publishTime) }}
                  </span>
                  <span class="flex items-center gap-1">
                    <svg class="w-4 h-4" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                      <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M15 12a3 3 0 11-6 0 3 3 0 016 0z" />
                      <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M2.458 12C3.732 7.943 7.523 5 12 5c4.478 0 8.268 2.943 9.542 7-1.274 4.057-5.064 7-9.542 7-4.477 0-8.268-2.943-9.542-7z" />
                    </svg>
                    {{ item.viewCount || 0 }} 次浏览
                  </span>
                  <span v-if="item.attachments && item.attachments.length > 0" class="flex items-center gap-1">
                    <svg class="w-4 h-4" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                      <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M4 16v1a3 3 0 003 3h10a3 3 0 003-3v-1m-4-4l-4 4m0 0l-4-4m4 4V4" />
                    </svg>
                    {{ item.attachments.length }} 个附件
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
import { getPolicyRegulationsList } from '@/api/infoPublication'

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
    const response = await getPolicyRegulationsList({
      keyword: keyword.value,
      pageNumber: currentPage.value,
      pageSize: pageSize.value
    })
    
    if (response.success) {
      items.value = response.data.items
      total.value = response.data.totalCount
    } else {
      console.error('加载列表失败:', response.message)
      items.value = []
      total.value = 0
    }
  } catch (error) {
    console.error('加载列表失败:', error)
    items.value = []
    total.value = 0
  } finally {
    loading.value = false
  }
}

// 格式化日期
const formatDate = (dateString) => {
  if (!dateString) return ''
  const date = new Date(dateString)
  return date.toLocaleDateString('zh-CN', {
    year: 'numeric',
    month: '2-digit',
    day: '2-digit'
  }).replace(/\//g, '-')
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

// 查看详情
const handleViewDetail = (id) => {
  router.push(`/detail/policy/${id}`)
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