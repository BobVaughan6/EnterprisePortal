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
      <!-- 搜索筛选区域 -->
      <div class="bg-white rounded-xl shadow-sm p-6 mb-6">
        <!-- 关键字搜索 - 主搜索框 -->
        <div class="mb-5">
          <div class="flex gap-3">
            <div class="relative flex-1">
              <svg class="absolute left-4 top-1/2 transform -translate-y-1/2 w-5 h-5 text-gray-400" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M21 21l-6-6m2-5a7 7 0 11-14 0 7 7 0 0114 0z" />
              </svg>
              <input
                v-model="searchParams.keyword"
                type="text"
                placeholder="请输入项目名称、招标单位等关键字"
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

        <!-- 筛选条件 -->
        <div class="space-y-3 pt-3 border-t border-gray-100">
          <!-- 业务类型和采购类型 -->
          <div class="flex items-center gap-4 flex-wrap">
            <label class="text-sm font-medium text-gray-700 whitespace-nowrap w-16">业务类型</label>
            <button
              v-for="type in businessTypes"
              :key="type.value"
              @click="searchParams.businessType = type.value"
              :class="[
                'px-5 py-1.5 rounded-lg text-sm font-medium transition-all',
                searchParams.businessType === type.value
                  ? 'bg-gradient-to-r from-hailong-primary to-hailong-secondary text-white shadow-lg'
                  : 'bg-gray-100 text-gray-700 hover:bg-gray-200'
              ]"
            >
              {{ type.label }}
            </button>

            <!-- 采购类型 - 仅在选择政府采购时显示 -->
            <template v-if="searchParams.businessType === 'GOV_PROCUREMENT'">
              <label class="text-sm font-medium text-gray-700 whitespace-nowrap w-16 ml-6">采购类型</label>
              <button
                v-for="type in procurementTypes"
                :key="type.value"
                @click="searchParams.procurementType = type.value"
                :class="[
                  'px-5 py-1.5 rounded-lg text-sm font-medium transition-all',
                  searchParams.procurementType === type.value
                    ? 'bg-gradient-to-r from-hailong-primary to-hailong-secondary text-white shadow-lg'
                    : 'bg-gray-100 text-gray-700 hover:bg-gray-200'
                ]"
              >
                {{ type.label }}
              </button>
            </template>
          </div>

          <!-- 公告类型 -->
          <div class="flex items-center gap-4">
            <label class="text-sm font-medium text-gray-700 whitespace-nowrap w-16">公告类型</label>
            <div class="flex flex-wrap gap-2">
              <button
                v-for="type in currentAnnouncementTypes"
                :key="type.value"
                @click="searchParams.type = type.value"
                :class="[
                  'px-5 py-1.5 rounded-lg text-sm font-medium transition-all',
                  searchParams.type === type.value
                    ? 'bg-gradient-to-r from-hailong-primary to-hailong-secondary text-white shadow-lg'
                    : 'bg-gray-100 text-gray-700 hover:bg-gray-200'
                ]"
              >
                {{ type.label }}
              </button>
            </div>
          </div>

          <!-- 项目区域和发布时间 -->
          <div class="flex items-center gap-4 flex-wrap">
            <div class="flex items-center gap-4">
              <label class="text-sm font-medium text-gray-700 whitespace-nowrap w-16">项目区域</label>
              
              <!-- 省份选择 -->
              <div class="relative">
                <svg class="absolute left-3 top-1/2 transform -translate-y-1/2 w-4 h-4 text-gray-400 pointer-events-none" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                  <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M17.657 16.657L13.414 20.9a1.998 1.998 0 01-2.827 0l-4.244-4.243a8 8 0 1111.314 0z" />
                  <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M15 11a3 3 0 11-6 0 3 3 0 016 0z" />
                </svg>
                <select
                  v-model="searchParams.province"
                  @change="onProvinceChange"
                  class="pl-9 pr-8 py-1.5 border border-gray-300 rounded-lg focus:ring-2 focus:ring-hailong-primary focus:border-transparent outline-none transition-all text-sm bg-white hover:border-hailong-primary cursor-pointer appearance-none"
                  style="min-width: 120px;"
                >
                  <option value="">全部省份</option>
                  <option v-for="province in provinces" :key="province.code" :value="province.code">
                    {{ province.name }}
                  </option>
                </select>
                <svg class="absolute right-3 top-1/2 transform -translate-y-1/2 w-4 h-4 text-gray-400 pointer-events-none" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                  <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M19 9l-7 7-7-7" />
                </svg>
              </div>

              <!-- 城市选择 -->
              <div class="relative" v-if="searchParams.province">
                <svg class="absolute left-3 top-1/2 transform -translate-y-1/2 w-4 h-4 text-gray-400 pointer-events-none" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                  <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M19 21V5a2 2 0 00-2-2H7a2 2 0 00-2 2v16m14 0h2m-2 0h-5m-9 0H3m2 0h5M9 7h1m-1 4h1m4-4h1m-1 4h1m-5 10v-5a1 1 0 011-1h2a1 1 0 011 1v5m-4 0h4" />
                </svg>
                <select
                  v-model="searchParams.city"
                  @change="onCityChange"
                  class="pl-9 pr-8 py-1.5 border border-gray-300 rounded-lg focus:ring-2 focus:ring-hailong-primary focus:border-transparent outline-none transition-all text-sm bg-white hover:border-hailong-primary cursor-pointer appearance-none"
                  style="min-width: 120px;"
                >
                  <option value="">全部城市</option>
                  <option v-for="city in cities" :key="city.code" :value="city.code">
                    {{ city.name }}
                  </option>
                </select>
                <svg class="absolute right-3 top-1/2 transform -translate-y-1/2 w-4 h-4 text-gray-400 pointer-events-none" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                  <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M19 9l-7 7-7-7" />
                </svg>
              </div>

              <!-- 区县选择 -->
              <div class="relative" v-if="searchParams.city">
                <svg class="absolute left-3 top-1/2 transform -translate-y-1/2 w-4 h-4 text-gray-400 pointer-events-none" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                  <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M3 12l2-2m0 0l7-7 7 7M5 10v10a1 1 0 001 1h3m10-11l2 2m-2-2v10a1 1 0 01-1 1h-3m-6 0a1 1 0 001-1v-4a1 1 0 011-1h2a1 1 0 011 1v4a1 1 0 001 1m-6 0h6" />
                </svg>
                <select
                  v-model="searchParams.district"
                  class="pl-9 pr-8 py-1.5 border border-gray-300 rounded-lg focus:ring-2 focus:ring-hailong-primary focus:border-transparent outline-none transition-all text-sm bg-white hover:border-hailong-primary cursor-pointer appearance-none"
                  style="min-width: 120px;"
                >
                  <option value="">全部区县</option>
                  <option v-for="district in districts" :key="district.code" :value="district.code">
                    {{ district.name }}
                  </option>
                </select>
                <svg class="absolute right-3 top-1/2 transform -translate-y-1/2 w-4 h-4 text-gray-400 pointer-events-none" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                  <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M19 9l-7 7-7-7" />
                </svg>
              </div>
            </div>

            <label class="text-sm font-medium text-gray-700 whitespace-nowrap w-16">发布时间</label>
            <button
              v-for="time in timeRanges"
              :key="time.value"
              @click="selectTimeRange(time.value)"
              :class="[
                'px-4 py-1.5 rounded-lg text-sm font-medium transition-all',
                searchParams.timeRange === time.value
                  ? 'bg-gradient-to-r from-hailong-primary to-hailong-secondary text-white shadow-lg'
                  : 'bg-gray-100 text-gray-700 hover:bg-gray-200'
              ]"
            >
              {{ time.label }}
            </button>
            <button
              @click="showCustomDatePicker = !showCustomDatePicker"
              :class="[
                'px-4 py-1.5 rounded-lg text-sm font-medium transition-all flex items-center gap-1',
                searchParams.timeRange === 'custom'
                  ? 'bg-gradient-to-r from-hailong-primary to-hailong-secondary text-white shadow-lg'
                  : 'bg-gray-100 text-gray-700 hover:bg-gray-200'
              ]"
            >
              <svg class="w-4 h-4" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M8 7V3m8 4V3m-9 8h10M5 21h14a2 2 0 002-2V7a2 2 0 00-2-2H5a2 2 0 00-2 2v12a2 2 0 002 2z" />
              </svg>
              自定义
            </button>

            <!-- 自定义日期选择器 -->
            <template v-if="showCustomDatePicker">
              <div class="relative">
                <svg class="absolute left-3 top-1/2 transform -translate-y-1/2 w-4 h-4 text-gray-400 pointer-events-none" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                  <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M8 7V3m8 4V3m-9 8h10M5 21h14a2 2 0 002-2V7a2 2 0 00-2-2H5a2 2 0 00-2 2v12a2 2 0 002 2z" />
                </svg>
                <input
                  type="date"
                  v-model="searchParams.startDate"
                  @change="onCustomDateChange"
                  class="pl-9 pr-3 py-1.5 border border-gray-300 rounded-lg focus:ring-2 focus:ring-hailong-primary focus:border-transparent outline-none transition-all text-sm hover:border-hailong-primary"
                />
              </div>
              <span class="text-gray-500 text-sm font-medium">至</span>
              <div class="relative">
                <svg class="absolute left-3 top-1/2 transform -translate-y-1/2 w-4 h-4 text-gray-400 pointer-events-none" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                  <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M8 7V3m8 4V3m-9 8h10M5 21h14a2 2 0 002-2V7a2 2 0 00-2-2H5a2 2 0 00-2 2v12a2 2 0 002 2z" />
                </svg>
                <input
                  type="date"
                  v-model="searchParams.endDate"
                  @change="onCustomDateChange"
                  class="pl-9 pr-3 py-1.5 border border-gray-300 rounded-lg focus:ring-2 focus:ring-hailong-primary focus:border-transparent outline-none transition-all text-sm hover:border-hailong-primary"
                />
              </div>
            </template>
          </div>
        </div>
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
            searchParams.businessType === 'GOV_PROCUREMENT' ? 'border-hailong-primary' :
            searchParams.businessType === 'CONSTRUCTION' ? 'border-hailong-secondary' : 'border-hailong-primary'
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
              <svg v-if="announcement.type === '招标公告' || announcement.type === '采购公告'" class="w-3 h-3" fill="currentColor" viewBox="0 0 20 20">
                <path d="M9 2a1 1 0 000 2h2a1 1 0 100-2H9z"/>
                <path fill-rule="evenodd" d="M4 5a2 2 0 012-2 3 3 0 003 3h2a3 3 0 003-3 2 2 0 012 2v11a2 2 0 01-2 2H6a2 2 0 01-2-2V5zm3 4a1 1 0 000 2h.01a1 1 0 100-2H7zm3 0a1 1 0 000 2h3a1 1 0 100-2h-3zm-3 4a1 1 0 100 2h.01a1 1 0 100-2H7zm3 0a1 1 0 100 2h3a1 1 0 100-2h-3z" clip-rule="evenodd"/>
              </svg>
              <svg v-else-if="announcement.type === '结果公告'" class="w-3 h-3" fill="currentColor" viewBox="0 0 20 20">
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
import cityData from '@/assets/city.json'

const router = useRouter()
const route = useRoute()

// 业务类型选项
const businessTypes = [
  { label: '全部', value: '' },
  { label: '政府采购', value: 'GOV_PROCUREMENT' },
  { label: '建设工程', value: 'CONSTRUCTION' }
]

// 公告类型选项 - 全部业务类型
const announcementTypesAll = [
  { label: '全部', value: '' },
  { label: '招标/采购公告', value: 'bidding' },
  { label: '更正公告', value: 'correction' },
  { label: '结果公告', value: 'result' }
]

// 公告类型选项 - 政府采购
const announcementTypesGov = [
  { label: '全部', value: '' },
  { label: '招标公告', value: 'bidding' },
  { label: '更正公告', value: 'correction' },
  { label: '结果公告', value: 'result' }
]

// 公告类型选项 - 建设工程
const announcementTypesConstruction = [
  { label: '全部', value: '' },
  { label: '采购公告', value: 'bidding' },
  { label: '更正公告', value: 'correction' },
  { label: '结果公告', value: 'result' }
]

// 采购类型选项（仅政府采购时显示）
const procurementTypes = [
  { label: '全部', value: '' },
  { label: '货物', value: 'goods' },
  { label: '服务', value: 'service' },
  { label: '工程', value: 'project' }
]

// 根据业务类型动态获取公告类型选项
const currentAnnouncementTypes = computed(() => {
  if (searchParams.value.businessType === 'GOV_PROCUREMENT') {
    return announcementTypesGov
  } else if (searchParams.value.businessType === 'CONSTRUCTION') {
    return announcementTypesConstruction
  } else {
    return announcementTypesAll
  }
})

// 省市区数据
const provinces = ref(cityData.provinces)
const cities = ref([])
const districts = ref([])

// 时间范围选项
const timeRanges = [
  { label: '全部', value: '' },
  { label: '当天', value: 'today' },
  { label: '近三天', value: '3days' },
  { label: '近一周', value: 'week' },
  { label: '近一月', value: 'month' }
]

// 搜索参数
const searchParams = ref({
  businessType: '',
  type: '',
  procurementType: '',
  province: '',
  city: '',
  district: '',
  region: '',
  timeRange: '',
  startDate: '',
  endDate: '',
  keyword: ''
})

// 省份变化处理
const onProvinceChange = () => {
  // 重置城市和区县
  searchParams.value.city = ''
  searchParams.value.district = ''
  districts.value = []
  
  if (searchParams.value.province) {
    // 查找选中的省份
    const selectedProvince = provinces.value.find(p => p.code === searchParams.value.province)
    if (selectedProvince) {
      cities.value = selectedProvince.cities || []
    }
  } else {
    cities.value = []
  }
}

// 城市变化处理
const onCityChange = () => {
  // 重置区县
  searchParams.value.district = ''
  
  if (searchParams.value.city) {
    // 查找选中的城市
    const selectedCity = cities.value.find(c => c.code === searchParams.value.city)
    if (selectedCity) {
      districts.value = selectedCity.districts || []
    }
  } else {
    districts.value = []
  }
}

// 自定义日期选择器显示状态
const showCustomDatePicker = ref(false)

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

// 选择时间范围
const selectTimeRange = (value) => {
  searchParams.value.timeRange = value
  showCustomDatePicker.value = false
  
  const today = new Date()
  const endDate = today.toISOString().split('T')[0]
  let startDate = ''
  
  switch (value) {
    case 'today':
      startDate = endDate
      break
    case '3days':
      startDate = new Date(today.setDate(today.getDate() - 3)).toISOString().split('T')[0]
      break
    case 'week':
      startDate = new Date(today.setDate(today.getDate() - 7)).toISOString().split('T')[0]
      break
    case 'month':
      startDate = new Date(today.setMonth(today.getMonth() - 1)).toISOString().split('T')[0]
      break
  }
  
  searchParams.value.startDate = startDate
  searchParams.value.endDate = endDate
}

// 自定义日期变化
const onCustomDateChange = () => {
  searchParams.value.timeRange = 'custom'
}

// 加载公告列表
const loadAnnouncements = async () => {
  loading.value = true
  try {
    // 构建区域参数
    let regionParam = ''
    if (searchParams.value.district) {
      const district = districts.value.find(d => d.code === searchParams.value.district)
      regionParam = district ? district.name : ''
    } else if (searchParams.value.city) {
      const city = cities.value.find(c => c.code === searchParams.value.city)
      regionParam = city ? city.name : ''
    } else if (searchParams.value.province) {
      const province = provinces.value.find(p => p.code === searchParams.value.province)
      regionParam = province ? province.name : ''
    }
    
    const params = {
      businessType: searchParams.value.businessType,
      keyword: searchParams.value.keyword,
      type: searchParams.value.type,
      region: regionParam,
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
    
    const mockData = generateMockData(searchParams.value.businessType)
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
const generateMockData = (businessType) => {
  const data = []
  const count = 25
  const types = ['bidding', 'correction', 'result']
  
  for (let i = 1; i <= count; i++) {
    const typeIndex = i % 3
    const typeValue = types[typeIndex]
    
    // 根据业务类型生成不同的标题和类型名称
    let title = ''
    let typeName = ''
    const category = businessType || (i % 2 === 0 ? 'GOV_PROCUREMENT' : 'CONSTRUCTION')
    
    if (category === 'GOV_PROCUREMENT') {
      // 政府采购
      const dept = ['教育局', '卫生局', '交通局', '财政局'][i % 4]
      const item = ['办公设备', '医疗器械', '教学设备', '车辆'][i % 4]
      if (typeValue === 'bidding') {
        title = `某市${dept}${item}采购项目招标公告`
        typeName = '招标公告'
      } else if (typeValue === 'result') {
        title = `某市${dept}${item}采购项目中标公示`
        typeName = '结果公告'
      } else {
        title = `某市${dept}${item}采购项目更正公告`
        typeName = '更正公告'
      }
    } else {
      // 建设工程
      const project = ['道路', '桥梁', '学校', '医院'][i % 4]
      const action = ['改造', '建设', '维修', '扩建'][i % 4]
      if (typeValue === 'bidding') {
        title = `某市${project}${action}工程施工采购公告`
        typeName = '采购公告'
      } else if (typeValue === 'result') {
        title = `某市${project}${action}工程施工中标公示`
        typeName = '结果公告'
      } else {
        title = `某市${project}${action}工程施工更正公告`
        typeName = '更正公告'
      }
    }
    
    data.push({
      id: i,
      title: title,
      type: typeName,
      businessType: category,
      bidder: `某市${['建设局', '交通局', '教育局', '卫生局'][i % 4]}`,
      // 只有结果公告才有中标人
      winner: typeValue === 'result' ? `某${['建设', '科技', '工程', '实业'][i % 4]}有限公司` : null,
      region: ['郑州市', '开封市', '洛阳市', '平顶山市', '安阳市', '鹤壁市', '新乡市', '焦作市', '濮阳市', '许昌市', '漯河市', '三门峡市', '南阳市', '商丘市', '信阳市', '周口市', '驻马店市', '济源市'][i % 18],
      publishDate: `2025-${String(11 + (i % 2)).padStart(2, '0')}-${String((i % 28) + 1).padStart(2, '0')}`,
      views: Math.floor(Math.random() * 1000) + 100
    })
  }
  
  return data
}

// 搜索
const handleSearch = async () => {
  currentPage.value = 1
  await loadAnnouncements()
  
  // 更新URL参数
  updateUrlParams()
}

// 重置
const handleReset = () => {
  searchParams.value = {
    businessType: '',
    type: '',
    procurementType: '',
    province: '',
    city: '',
    district: '',
    region: '',
    timeRange: '',
    startDate: '',
    endDate: '',
    keyword: ''
  }
  cities.value = []
  districts.value = []
  showCustomDatePicker.value = false
  
  selectTimeRange('')
  handleSearch()
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
    case '招标公告':
    case '采购公告':
      return 'bg-hailong-primary/10 text-hailong-primary border border-hailong-primary/20'
    case '结果公告':
      return 'bg-hailong-secondary/10 text-hailong-secondary border border-hailong-secondary/20'
    case '更正公告':
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

// 更新URL参数
const updateUrlParams = () => {
  const query = {}
  
  if (searchParams.value.businessType) query.businessType = searchParams.value.businessType
  if (searchParams.value.type) query.type = searchParams.value.type
  if (searchParams.value.province) query.province = searchParams.value.province
  if (searchParams.value.city) query.city = searchParams.value.city
  if (searchParams.value.district) query.district = searchParams.value.district
  if (searchParams.value.timeRange) query.timeRange = searchParams.value.timeRange
  if (searchParams.value.startDate) query.startDate = searchParams.value.startDate
  if (searchParams.value.endDate) query.endDate = searchParams.value.endDate
  if (searchParams.value.keyword) query.keyword = searchParams.value.keyword
  
  router.replace({ query })
}

// 从URL参数恢复筛选条件
const restoreFiltersFromUrl = () => {
  const query = route.query
  
  if (query.businessType) searchParams.value.businessType = query.businessType
  if (query.type) searchParams.value.type = query.type
  if (query.keyword) searchParams.value.keyword = query.keyword
  
  // 恢复省市区选择
  if (query.province) {
    searchParams.value.province = query.province
    onProvinceChange()
    
    if (query.city) {
      searchParams.value.city = query.city
      onCityChange()
      
      if (query.district) {
        searchParams.value.district = query.district
      }
    }
  }
  
  if (query.timeRange) {
    searchParams.value.timeRange = query.timeRange
    if (query.timeRange === 'custom') {
      showCustomDatePicker.value = true
      if (query.startDate) searchParams.value.startDate = query.startDate
      if (query.endDate) searchParams.value.endDate = query.endDate
    } else {
      selectTimeRange(query.timeRange)
    }
  } else {
    selectTimeRange('')
  }
}

// 组件挂载时加载数据
onMounted(() => {
  // 从URL恢复筛选条件
  restoreFiltersFromUrl()
  
  // 获取数据
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