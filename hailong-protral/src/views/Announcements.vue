<template>
  <div class="min-h-screen bg-gradient-to-br from-hailong-dark via-hailong-primary to-hailong-secondary">
    <Header />
    
    <!-- 页面标题 -->
    <div class="pt-32 pb-16 text-center text-white">
      <h1 class="text-6xl font-bold mb-4 font-tech bg-gradient-to-r from-white via-hailong-cyan to-white bg-clip-text text-transparent animate-fade-in">
        公告信息
      </h1>
      <p class="text-xl text-gray-200">招标采购信息</p>
    </div>

    <!-- 内容区域 -->
    <div class="py-16 bg-white">
      <div class="container-wide">
        <div class="animate-fade-in">
      <!-- Tab切换 -->
      <div class="bg-white rounded-xl shadow-sm p-2 mb-6">
        <div class="flex gap-2">
          <button
            v-for="tab in tabs"
            :key="tab.value"
            @click="activeTab = tab.value"
            :class="[
              'flex-1 py-3 px-6 rounded-lg font-medium transition-all',
              activeTab === tab.value
                ? 'bg-gradient-to-r from-hailong-primary to-hailong-secondary text-white shadow-lg'
                : 'text-gray-600 hover:bg-gray-50'
            ]"
          >
            {{ tab.label }}
          </button>
        </div>
      </div>

      <!-- 高级搜索区域 -->
      <div class="bg-white rounded-xl shadow-sm mb-6 overflow-hidden">
        <div
          class="flex items-center justify-between p-4 cursor-pointer hover:bg-gray-50 transition-colors"
          @click="searchExpanded = !searchExpanded"
        >
          <div class="flex items-center gap-2">
            <svg class="w-5 h-5 text-hailong-primary" fill="none" stroke="currentColor" viewBox="0 0 24 24">
              <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M21 21l-6-6m2-5a7 7 0 11-14 0 7 7 0 0114 0z" />
            </svg>
            <span class="font-medium text-gray-700">高级搜索</span>
          </div>
          <svg
            :class="['w-5 h-5 text-gray-400 transition-transform', searchExpanded ? 'rotate-180' : '']"
            fill="none"
            stroke="currentColor"
            viewBox="0 0 24 24"
          >
            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M19 9l-7 7-7-7" />
          </svg>
        </div>

        <transition name="expand">
          <div v-show="searchExpanded" class="border-t border-gray-100 p-6">
            <div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4 mb-4">
              <!-- 关键词搜索 -->
              <div>
                <label class="block text-sm font-medium text-gray-700 mb-2">关键词</label>
                <input
                  v-model="searchParams.keyword"
                  type="text"
                  placeholder="请输入关键词"
                  class="w-full px-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-hailong-primary focus:border-transparent outline-none transition-all"
                />
              </div>

              <!-- 公告类型 -->
              <div>
                <label class="block text-sm font-medium text-gray-700 mb-2">公告类型</label>
                <select
                  v-model="searchParams.type"
                  class="w-full px-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-hailong-primary focus:border-transparent outline-none transition-all"
                >
                  <option value="">全部类型</option>
                  <option v-for="type in announcementTypes" :key="type.value" :value="type.value">
                    {{ type.label }}
                  </option>
                </select>
              </div>

              <!-- 项目区域 -->
              <div>
                <label class="block text-sm font-medium text-gray-700 mb-2">项目区域</label>
                <select
                  v-model="searchParams.regions"
                  multiple
                  class="w-full px-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-hailong-primary focus:border-transparent outline-none transition-all"
                  style="height: 42px"
                >
                  <option v-for="region in regions" :key="region.value" :value="region.value">
                    {{ region.label }}
                  </option>
                </select>
              </div>

              <!-- 开始日期 -->
              <div>
                <label class="block text-sm font-medium text-gray-700 mb-2">开始日期</label>
                <input
                  v-model="searchParams.startDate"
                  type="date"
                  class="w-full px-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-hailong-primary focus:border-transparent outline-none transition-all"
                />
              </div>

              <!-- 结束日期 -->
              <div>
                <label class="block text-sm font-medium text-gray-700 mb-2">结束日期</label>
                <input
                  v-model="searchParams.endDate"
                  type="date"
                  class="w-full px-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-hailong-primary focus:border-transparent outline-none transition-all"
                />
              </div>
            </div>

            <!-- 搜索按钮 -->
            <div class="flex gap-3">
              <button
                @click="handleSearch"
                class="px-8 py-2 bg-gradient-to-r from-hailong-primary to-hailong-secondary text-white rounded-lg hover:shadow-lg transition-all"
              >
                搜索
              </button>
              <button
                @click="handleReset"
                class="px-8 py-2 bg-gray-100 text-gray-700 rounded-lg hover:bg-gray-200 transition-all"
              >
                重置
              </button>
            </div>
          </div>
        </transition>
      </div>

      <!-- 结果统计 -->
      <div class="mb-4 text-sm text-gray-600">
        共找到 <span class="text-hailong-primary font-semibold">{{ total }}</span> 条公告
      </div>

      <!-- 公告列表 -->
      <div v-if="loading" class="text-center py-20">
        <div class="inline-block animate-spin rounded-full h-12 w-12 border-4 border-hailong-primary border-t-transparent"></div>
        <p class="mt-4 text-gray-500">加载中...</p>
      </div>

      <div v-else-if="announcements.length === 0" class="text-center py-20">
        <svg class="w-20 h-20 mx-auto text-gray-300 mb-4" fill="none" stroke="currentColor" viewBox="0 0 24 24">
          <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M9 12h6m-6 4h6m2 5H7a2 2 0 01-2-2V5a2 2 0 012-2h5.586a1 1 0 01.707.293l5.414 5.414a1 1 0 01.293.707V19a2 2 0 01-2 2z" />
        </svg>
        <p class="text-gray-500">暂无公告信息</p>
      </div>

      <div v-else class="space-y-4">
        <div
          v-for="announcement in announcements"
          :key="announcement.id"
          @click="handleViewDetail(announcement.id)"
          :class="[
            'bg-white rounded-xl p-6 shadow-sm hover:shadow-lg transition-all cursor-pointer border-l-4',
            activeTab === 'GOV_PROCUREMENT' ? 'border-hailong-primary' : 'border-hailong-secondary'
          ]"
        >
          <!-- 标题和类型 -->
          <div class="flex items-start justify-between mb-3">
            <h3 class="text-lg font-bold text-gray-900 flex-1 hover:text-hailong-primary transition-colors line-clamp-2">
              {{ announcement.title }}
            </h3>
            <span
              :class="[
                'ml-4 px-3 py-1 rounded-full text-xs font-medium whitespace-nowrap flex items-center gap-1',
                getTypeStyle(announcement.type)
              ]"
            >
              <svg v-if="announcement.type === '公开招标'" class="w-3 h-3" fill="currentColor" viewBox="0 0 20 20">
                <path d="M9 2a1 1 0 000 2h2a1 1 0 100-2H9z"/>
                <path fill-rule="evenodd" d="M4 5a2 2 0 012-2 3 3 0 003 3h2a3 3 0 003-3 2 2 0 012 2v11a2 2 0 01-2 2H6a2 2 0 01-2-2V5zm3 4a1 1 0 000 2h.01a1 1 0 100-2H7zm3 0a1 1 0 000 2h3a1 1 0 100-2h-3zm-3 4a1 1 0 100 2h.01a1 1 0 100-2H7zm3 0a1 1 0 100 2h3a1 1 0 100-2h-3z" clip-rule="evenodd"/>
              </svg>
              <svg v-else-if="announcement.type === '中标公示'" class="w-3 h-3" fill="currentColor" viewBox="0 0 20 20">
                <path fill-rule="evenodd" d="M6.267 3.455a3.066 3.066 0 001.745-.723 3.066 3.066 0 013.976 0 3.066 3.066 0 001.745.723 3.066 3.066 0 012.812 2.812c.051.643.304 1.254.723 1.745a3.066 3.066 0 010 3.976 3.066 3.066 0 00-.723 1.745 3.066 3.066 0 01-2.812 2.812 3.066 3.066 0 00-1.745.723 3.066 3.066 0 01-3.976 0 3.066 3.066 0 00-1.745-.723 3.066 3.066 0 01-2.812-2.812 3.066 3.066 0 00-.723-1.745 3.066 3.066 0 010-3.976 3.066 3.066 0 00.723-1.745 3.066 3.066 0 012.812-2.812zm7.44 5.252a1 1 0 00-1.414-1.414L9 10.586 7.707 9.293a1 1 0 00-1.414 1.414l2 2a1 1 0 001.414 0l4-4z" clip-rule="evenodd"/>
              </svg>
              <svg v-else class="w-3 h-3" fill="currentColor" viewBox="0 0 20 20">
                <path fill-rule="evenodd" d="M18 10a8 8 0 11-16 0 8 8 0 0116 0zm-7 4a1 1 0 11-2 0 1 1 0 012 0zm-1-9a1 1 0 00-1 1v4a1 1 0 102 0V6a1 1 0 00-1-1z" clip-rule="evenodd"/>
              </svg>
              {{ announcement.type }}
            </span>
          </div>

          <!-- 详细信息 -->
          <div class="grid grid-cols-2 md:grid-cols-4 gap-4 text-sm">
            <div v-if="announcement.bidder">
              <span class="text-gray-500">招标人：</span>
              <span class="text-gray-700 font-medium">{{ announcement.bidder }}</span>
            </div>
            <div v-if="announcement.winner">
              <span class="text-gray-500">中标人：</span>
              <span class="text-gray-700 font-medium">{{ announcement.winner }}</span>
            </div>
            <div v-if="announcement.region">
              <span class="text-gray-500">项目区域：</span>
              <span class="text-gray-700 font-medium">{{ announcement.region }}</span>
            </div>
            <div>
              <span class="text-gray-500">发布时间：</span>
              <span class="text-gray-700 font-medium">{{ announcement.publishDate }}</span>
            </div>
          </div>

          <!-- 底部信息 -->
          <div class="flex items-center justify-between mt-4 pt-4 border-t border-gray-100">
            <div class="flex items-center gap-4 text-xs text-gray-500">
              <span class="flex items-center gap-1">
                <svg class="w-4 h-4" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                  <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M15 12a3 3 0 11-6 0 3 3 0 016 0z" />
                  <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M2.458 12C3.732 7.943 7.523 5 12 5c4.478 0 8.268 2.943 9.542 7-1.274 4.057-5.064 7-9.542 7-4.477 0-8.268-2.943-9.542-7z" />
                </svg>
                {{ announcement.views || 0 }} 次浏览
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
import { ref, computed, watch, onMounted } from 'vue'
import { useRouter, useRoute } from 'vue-router'
import Header from '@/components/Header.vue'
import Footer from '@/components/Footer.vue'
import { getAnnouncementList, getAnnouncementTypes, getRegions, incrementViews } from '@/api/announcement.js'

const router = useRouter()
const route = useRoute()

// Tab选项
const tabs = [
  { label: '政府采购', value: 'GOV_PROCUREMENT' },
  { label: '建设工程', value: 'CONSTRUCTION' }
]

// 当前激活的Tab
const activeTab = ref('GOV_PROCUREMENT')

// 搜索展开状态
const searchExpanded = ref(false)

// 搜索参数
const searchParams = ref({
  keyword: '',
  type: '',
  regions: [],
  startDate: '',
  endDate: ''
})

// 公告类型选项
const announcementTypes = ref([
  { label: '公开招标', value: 'PUBLIC_BIDDING' },
  { label: '中标公示', value: 'WINNING_ANNOUNCEMENT' },
  { label: '变更公告', value: 'CHANGE_ANNOUNCEMENT' }
])

// 区域选项
const regions = ref([
  { label: '北京市', value: 'BEIJING' },
  { label: '上海市', value: 'SHANGHAI' },
  { label: '广东省', value: 'GUANGDONG' },
  { label: '浙江省', value: 'ZHEJIANG' },
  { label: '江苏省', value: 'JIANGSU' },
  { label: '山东省', value: 'SHANDONG' },
  { label: '四川省', value: 'SICHUAN' },
  { label: '湖北省', value: 'HUBEI' }
])

// 公告列表
const announcements = ref([])

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

// 加载公告列表
const loadAnnouncements = async () => {
  loading.value = true
  try {
    const params = {
      category: activeTab.value,
      keyword: searchParams.value.keyword,
      type: searchParams.value.type,
      regions: searchParams.value.regions,
      startDate: searchParams.value.startDate,
      endDate: searchParams.value.endDate,
      page: currentPage.value,
      pageSize: pageSize.value
    }
    
    // 模拟API调用 - 实际项目中替换为真实API
    // const response = await getAnnouncementList(params)
    // announcements.value = response.data.list
    // total.value = response.data.total
    
    // 模拟数据
    await new Promise(resolve => setTimeout(resolve, 500))
    
    const mockData = generateMockData(activeTab.value)
    announcements.value = mockData.slice(
      (currentPage.value - 1) * pageSize.value,
      currentPage.value * pageSize.value
    )
    total.value = mockData.length
  } catch (error) {
    console.error('加载公告列表失败:', error)
    announcements.value = []
    total.value = 0
  } finally {
    loading.value = false
  }
}

// 生成模拟数据
const generateMockData = (category) => {
  const data = []
  const count = 25
  const types = ['公开招标', '中标公示', '变更公告']
  
  for (let i = 1; i <= count; i++) {
    const typeIndex = i % 3
    const type = types[typeIndex]
    
    // 根据类型生成不同的标题
    let title = ''
    if (category === 'GOV_PROCUREMENT') {
      const dept = ['教育局', '卫生局', '交通局', '财政局'][i % 4]
      const item = ['办公设备', '医疗器械', '教学设备', '车辆'][i % 4]
      if (type === '公开招标') {
        title = `某市${dept}${item}采购项目招标公告`
      } else if (type === '中标公示') {
        title = `某市${dept}${item}采购项目中标公示`
      } else {
        title = `某市${dept}${item}采购项目变更公告`
      }
    } else {
      const project = ['道路', '桥梁', '学校', '医院'][i % 4]
      const action = ['改造', '建设', '维修', '扩建'][i % 4]
      if (type === '公开招标') {
        title = `某市${project}${action}工程施工招标公告`
      } else if (type === '中标公示') {
        title = `某市${project}${action}工程施工中标公示`
      } else {
        title = `某市${project}${action}工程施工变更公告`
      }
    }
    
    data.push({
      id: i,
      title: title,
      type: type,
      bidder: `某市${['建设局', '交通局', '教育局', '卫生局'][i % 4]}`,
      // 只有中标公示才有中标人
      winner: type === '中标公示' ? `某${['建设', '科技', '工程', '实业'][i % 4]}有限公司` : null,
      region: ['北京市', '上海市', '广东省', '浙江省', '江苏省'][i % 5],
      publishDate: `2025-${String(11 + (i % 2)).padStart(2, '0')}-${String((i % 28) + 1).padStart(2, '0')}`,
      views: Math.floor(Math.random() * 1000) + 100
    })
  }
  
  return data
}

// 搜索
const handleSearch = () => {
  currentPage.value = 1
  loadAnnouncements()
}

// 重置
const handleReset = () => {
  searchParams.value = {
    keyword: '',
    type: '',
    regions: [],
    startDate: '',
    endDate: ''
  }
  currentPage.value = 1
  loadAnnouncements()
}

// 页码变化
const handlePageChange = (page) => {
  if (page < 1 || page > totalPages.value) return
  currentPage.value = page
  loadAnnouncements()
  window.scrollTo({ top: 0, behavior: 'smooth' })
}

// 获取类型样式
const getTypeStyle = (type) => {
  switch (type) {
    case '公开招标':
      return 'bg-hailong-primary/10 text-hailong-primary border border-hailong-primary/20'
    case '中标公示':
      return 'bg-hailong-secondary/10 text-hailong-secondary border border-hailong-secondary/20'
    case '变更公告':
      return 'bg-hailong-cyan/10 text-hailong-cyan border border-hailong-cyan/20'
    default:
      return 'bg-gray-100 text-gray-800 border border-gray-200'
  }
}

// 查看详情
const handleViewDetail = async (id) => {
  try {
    // 增加访问量
    // await incrementViews(id)
    
    // 跳转到详情页
    router.push(`/detail/announcement/${id}`)
  } catch (error) {
    console.error('查看详情失败:', error)
  }
}

// 监听Tab变化
watch(activeTab, () => {
  currentPage.value = 1
  loadAnnouncements()
})

// 组件挂载时加载数据
onMounted(() => {
  // 从URL参数中获取tab类型
  const tabParam = route.query.tab
  if (tabParam && (tabParam === 'GOV_PROCUREMENT' || tabParam === 'CONSTRUCTION')) {
    activeTab.value = tabParam
  }
  loadAnnouncements()
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


.expand-enter-from,
.expand-leave-to {
  max-height: 0;
  opacity: 0;
}
</style>