<template>
  <!-- 业务范围 -->
  <div class="py-24 bg-gradient-to-b from-blue-50/30 to-gray-50">
    <div class="container-wide">
      <div class="text-center mb-16">
        <h2 class="text-5xl font-bold text-hailong-dark mb-4 font-tech">业务范围</h2>
        <div class="w-24 h-1 bg-gradient-to-r from-hailong-primary to-hailong-secondary mx-auto"></div>
      </div>
      <div v-if="loading" class="text-center py-8 text-gray-500">加载中...</div>
      <div v-else-if="businessList.length === 0" class="text-center py-8 text-gray-500">暂无业务范围数据</div>
      <div v-else class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-8">
        <div v-for="business in businessList" :key="business.id"
          @click="$emit('business-click', business.id)"
          class="group relative bg-white rounded-2xl overflow-hidden shadow-lg hover:shadow-2xl transition-all duration-300 border border-gray-200 hover:border-hailong-primary cursor-pointer">
          <div class="h-48 overflow-hidden bg-gray-200">
            <img v-if="getBusinessImage(business)" :src="getBusinessImage(business)" :alt="business.name"
              class="w-full h-full object-cover group-hover:scale-110 transition-transform duration-500" />
            <div v-else class="w-full h-full flex items-center justify-center text-gray-400">
              <svg class="w-16 h-16" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M4 16l4.586-4.586a2 2 0 012.828 0L16 16m-2-2l1.586-1.586a2 2 0 012.828 0L20 14m-6-6h.01M6 20h12a2 2 0 002-2V6a2 2 0 00-2-2H6a2 2 0 00-2 2v12a2 2 0 002 2z"></path>
              </svg>
            </div>
          </div>
          <div class="p-6">
            <h3 class="text-2xl font-bold text-gray-900 mb-3 group-hover:text-hailong-primary transition-colors">{{
              business.name }}</h3>
            <p class="text-gray-600 mb-4">{{ business.description }}</p>
            <div v-if="business.features && business.features.length > 0">
              <ul class="space-y-2">
                <li v-for="(feature, index) in business.features.slice(0, 4)" :key="index" class="text-sm text-gray-500 flex items-center">
                  <span class="w-1.5 h-1.5 bg-hailong-secondary rounded-full mr-2"></span>{{ feature }}
                </li>
              </ul>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>

  <!-- 企业资质 -->
  <div class="py-24 bg-gradient-to-b from-gray-50 to-white">
    <div class="container-wide">
      <div class="text-center mb-16">
        <h2 class="text-5xl font-bold text-hailong-dark mb-4 font-tech">企业资质</h2>
        <div class="w-24 h-1 bg-gradient-to-r from-hailong-primary to-hailong-secondary mx-auto"></div>
      </div>
      <div class="bg-white rounded-2xl p-8 md:p-12 shadow-lg border border-gray-200">
        <div v-if="qualificationsLoading" class="text-center py-8 text-gray-500">加载中...</div>
        <div v-else-if="qualificationsList.length === 0" class="text-center py-8 text-gray-500">暂无资质数据</div>
        <div v-else class="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 gap-4 md:gap-6">
          <div v-for="qualification in qualificationsList" :key="qualification.id"
            @click="$emit('qualification-click', qualification.id)"
            class="flex items-center p-4 bg-gradient-to-br from-hailong-primary/5 to-hailong-secondary/5 rounded-xl hover:shadow-md hover:scale-105 transition-all duration-300 cursor-pointer group">
            <div
              class="w-12 h-12 bg-hailong-primary/10 rounded-full flex items-center justify-center mr-4 flex-shrink-0 group-hover:bg-hailong-primary/20 transition-colors">
              <svg class="w-6 h-6 text-hailong-primary" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
                  d="M9 12l2 2 4-4m6 2a9 9 0 11-18 0 9 9 0 0118 0z"></path>
              </svg>
            </div>
            <span class="text-gray-800 font-medium text-sm group-hover:text-hailong-primary transition-colors">{{
              qualification.name }}</span>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import { getBusinessScope, getCompanyQualifications } from '@/api/config'

defineEmits(['business-click', 'qualification-click'])

// 业务范围数据
const loading = ref(false)
const businessList = ref([])

// 企业资质数据
const qualificationsLoading = ref(false)
const qualificationsList = ref([])

// 获取业务图片
const getBusinessImage = (business) => {
  // 如果有imageId，构建图片URL
  if (business.imageId) {
    return `${import.meta.env.VITE_API_BASE_URL}/attachments/${business.imageId}`
  }
  return null
}

// 加载业务范围
const loadBusinessScope = async () => {
  loading.value = true
  try {
    const response = await getBusinessScope()
    if (response.success && response.data) {
      businessList.value = response.data
    }
  } catch (error) {
    console.error('加载业务范围失败:', error)
  } finally {
    loading.value = false
  }
}

// 加载企业资质
const loadQualifications = async () => {
  qualificationsLoading.value = true
  try {
    const response = await getCompanyQualifications()
    if (response.success && response.data) {
      qualificationsList.value = response.data
    }
  } catch (error) {
    console.error('加载企业资质失败:', error)
  } finally {
    qualificationsLoading.value = false
  }
}

onMounted(() => {
  loadBusinessScope()
  loadQualifications()
})
</script>