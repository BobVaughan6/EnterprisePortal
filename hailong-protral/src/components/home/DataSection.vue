<template>
  <!-- 重要业绩展示 -->
  <div class="py-24 bg-hailong-dark text-white">
    <div class="container-wide">
      <div class="text-center mb-16">
        <h2 class="text-5xl font-bold mb-4 font-tech">重要业绩展示</h2>
        <div class="w-24 h-1 bg-gradient-to-r from-hailong-primary to-hailong-secondary mx-auto"></div>
      </div>
      <div v-if="achievementsLoading" class="text-center py-8 text-white/70">加载中...</div>
      <div v-else-if="achievementsList.length === 0" class="text-center py-8 text-white/70">暂无业绩数据</div>
      <div v-else class="relative overflow-hidden">
        <div class="flex gap-6 animate-scroll">
          <div v-for="achievement in [...achievementsList, ...achievementsList]"
            :key="achievement.id + Math.random()"
            @click="$emit('achievement-click', achievement.id)"
            class="flex-shrink-0 w-80 bg-white/10 backdrop-blur-lg rounded-2xl overflow-hidden hover:bg-white/20 transition-all cursor-pointer group">
            <div class="h-48 overflow-hidden bg-gray-700">
              <img v-if="achievement.imageUrl" :src="achievement.imageUrl" :alt="achievement.projectName"
                class="w-full h-full object-cover group-hover:scale-110 transition-transform duration-500" />
              <div v-else class="w-full h-full flex items-center justify-center text-gray-500">
                <svg class="w-16 h-16" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                  <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M4 16l4.586-4.586a2 2 0 012.828 0L16 16m-2-2l1.586-1.586a2 2 0 012.828 0L20 14m-6-6h.01M6 20h12a2 2 0 002-2V6a2 2 0 00-2-2H6a2 2 0 00-2 2v12a2 2 0 002 2z"></path>
                </svg>
              </div>
            </div>
            <div class="p-6">
              <div v-if="achievement.projectType" class="flex items-center justify-between mb-3">
                <span :class="[
                  'px-3 py-1 rounded-full text-xs font-semibold',
                  achievement.projectType === '工程' ? 'bg-hailong-primary/20 text-hailong-primary' :
                    achievement.projectType === '服务' ? 'bg-hailong-secondary/20 text-hailong-secondary' :
                      'bg-hailong-cyan/20 text-hailong-cyan'
                ]">
                  {{ achievement.projectType }}
                </span>
              </div>
              <h3 class="text-lg font-bold mb-3 line-clamp-2">{{ achievement.projectName }}</h3>
              <div v-if="achievement.projectAmount" class="text-2xl font-bold text-hailong-secondary">
                {{ formatAmount(achievement.projectAmount) }}
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>

  <!-- 交易数据可视化 -->
  <div class="py-24 bg-gradient-to-b from-white to-gray-50">
    <div class="container-wide">
      <div class="text-center mb-16">
        <h2 class="text-5xl font-bold text-hailong-dark mb-4 font-tech">交易数据可视化</h2>
        <div class="w-24 h-1 bg-gradient-to-r from-hailong-primary to-hailong-secondary mx-auto"></div>
      </div>
      <div v-if="statisticsLoading" class="text-center py-8 text-gray-500">加载中...</div>
      <div v-else-if="!statistics.totalProjects" class="text-center py-8 text-gray-500">暂无统计数据</div>
      <div v-else>
        <div class="grid grid-cols-2 md:grid-cols-4 gap-8 mb-12">
          <div class="bg-white rounded-2xl p-8 shadow-lg text-center">
            <div class="text-4xl font-bold text-hailong-primary mb-2">{{ statistics.totalProjects || 0 }}</div>
            <div class="text-gray-600">项目总数</div>
          </div>
          <div class="bg-white rounded-2xl p-8 shadow-lg text-center">
            <div class="text-4xl font-bold text-hailong-secondary mb-2">{{ formatTotalAmount(statistics.totalAmount) }}</div>
            <div class="text-gray-600">交易总额</div>
          </div>
          <div class="bg-white rounded-2xl p-8 shadow-lg text-center">
            <div class="text-4xl font-bold text-hailong-primary mb-2">{{ getProjectTypeCount('政府采购') }}</div>
            <div class="text-gray-600">政府采购</div>
          </div>
          <div class="bg-white rounded-2xl p-8 shadow-lg text-center">
            <div class="text-4xl font-bold text-hailong-secondary mb-2">{{ getProjectTypeCount('建设工程') }}</div>
            <div class="text-gray-600">建设工程</div>
          </div>
        </div>
        <div class="grid grid-cols-1 lg:grid-cols-2 gap-8">
          <!-- 交易类型占比 -->
          <div class="bg-white rounded-2xl p-8 shadow-lg">
            <h3 class="text-2xl font-bold text-gray-900 mb-6">交易类型占比</h3>
            <div v-if="typeDistribution.length === 0" class="text-center py-8 text-gray-500">暂无类型数据</div>
            <div v-else class="flex flex-col items-center justify-center h-64">
              <div class="relative w-48 h-48">
                <svg class="w-full h-full transform -rotate-90" viewBox="0 0 100 100">
                  <circle cx="50" cy="50" r="40" fill="none" stroke="#f3f4f6" stroke-width="20" />
                  <circle v-if="typeDistribution.length > 0" cx="50" cy="50" r="40" fill="none" :stroke="typeDistribution[0].color"
                    stroke-width="20" :stroke-dasharray="`${typeDistribution[0].percentage * 2.51} 251`"
                    class="transition-all duration-500 hover:stroke-width-[22]" />
                  <circle v-if="typeDistribution.length > 1" cx="50" cy="50" r="40" fill="none" :stroke="typeDistribution[1].color"
                    stroke-width="20" :stroke-dasharray="`${typeDistribution[1].percentage * 2.51} 251`"
                    :stroke-dashoffset="`-${typeDistribution[0].percentage * 2.51}`"
                    class="transition-all duration-500 hover:stroke-width-[22]" />
                </svg>
                <div class="absolute inset-0 flex items-center justify-center">
                  <div class="text-center">
                    <div class="text-3xl font-bold text-gray-900">{{ statistics.totalProjects || 0 }}</div>
                    <div class="text-sm text-gray-500">总项目</div>
                  </div>
                </div>
              </div>
              <div class="mt-6 space-y-3 w-full">
                <div v-for="item in typeDistribution" :key="item.type"
                  class="flex items-center justify-between p-3 bg-gray-50 rounded-lg hover:bg-gray-100 transition-colors">
                  <div class="flex items-center">
                    <div class="w-4 h-4 rounded-full mr-3" :style="{ backgroundColor: item.color }"></div>
                    <span class="text-gray-700 font-medium">{{ item.type }}</span>
                  </div>
                  <div class="text-right">
                    <div class="text-lg font-bold" :style="{ color: item.color }">{{ item.count }}</div>
                    <div class="text-xs text-gray-500">{{ item.percentage }}%</div>
                  </div>
                </div>
              </div>
            </div>
          </div>

          <!-- 地区排行 -->
          <div class="bg-white rounded-2xl p-8 shadow-lg">
            <h3 class="text-2xl font-bold text-gray-900 mb-6">地区项目排行</h3>
            <div v-if="regionRanking.length === 0" class="text-center py-8 text-gray-500">暂无地区数据</div>
            <div v-else class="space-y-3">
              <div v-for="(region, index) in regionRanking" :key="region.region" class="group relative">
                <div class="flex items-center justify-between mb-2">
                  <div class="flex items-center flex-1">
                    <div class="w-8 h-8 rounded-full flex items-center justify-center mr-3 font-bold text-sm" :class="[
                      index === 0 ? 'bg-yellow-100 text-yellow-600' :
                        index === 1 ? 'bg-gray-100 text-gray-600' :
                          index === 2 ? 'bg-orange-100 text-orange-600' :
                            'bg-blue-50 text-blue-600'
                    ]">
                      {{ index + 1 }}
                    </div>
                    <span class="text-gray-700 font-medium">{{ region.region }}</span>
                  </div>
                  <div class="text-right ml-4">
                    <div class="text-lg font-bold text-hailong-primary">{{ region.projectCount }}</div>
                    <div class="text-xs text-gray-500">{{ formatRegionAmount(region.amount) }}</div>
                  </div>
                </div>
                <div class="relative h-2 bg-gray-100 rounded-full overflow-hidden">
                  <div
                    class="absolute inset-y-0 left-0 bg-gradient-to-r from-hailong-primary to-hailong-secondary rounded-full transition-all duration-500 group-hover:opacity-80"
                    :style="{ width: (region.projectCount / regionRanking[0].projectCount * 100) + '%' }">
                  </div>
                </div>
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
import { getStatisticsOverview } from '@/api/home'
import { getMajorAchievements } from '@/api/config'

defineEmits(['achievement-click'])

// 重要业绩数据
const achievementsLoading = ref(false)
const achievementsList = ref([])

// 统计数据
const statisticsLoading = ref(false)
const statistics = ref({
  totalProjects: 0,
  totalAmount: 0,
  projectTypes: [],
  regionRanking: []
})

// 交易类型占比 - 根据后端返回的projectTypes计算
const typeDistribution = computed(() => {
  if (!statistics.value.projectTypes || statistics.value.projectTypes.length === 0) {
    return []
  }
  
  // 映射颜色
  return statistics.value.projectTypes.map(item => ({
    type: item.type,
    count: item.count,
    percentage: Number(item.percentage.toFixed(1)),
    color: item.type === '政府采购' ? '#0EA5E9' : '#10B981'
  }))
})

// 地区排行
const regionRanking = computed(() => {
  return statistics.value.regionRanking || []
})

// 获取指定类型的项目数量
const getProjectTypeCount = (type) => {
  const projectType = statistics.value.projectTypes?.find(item => item.type === type)
  return projectType ? projectType.count : 0
}

// 加载重要业绩
const loadAchievements = async () => {
  achievementsLoading.value = true
  try {
    const response = await getMajorAchievements()
    if (response.success && response.data) {
      achievementsList.value = response.data
    }
  } catch (error) {
    console.error('加载重要业绩失败:', error)
  } finally {
    achievementsLoading.value = false
  }
}

// 加载统计数据
const loadStatistics = async () => {
  statisticsLoading.value = true
  try {
    const response = await getStatisticsOverview()
    if (response.success && response.data) {
      statistics.value = {
        totalProjects: response.data.totalProjects || 0,
        totalAmount: response.data.totalAmount || 0,
        projectTypes: response.data.projectTypes || [],
        regionRanking: response.data.regionRanking || []
      }
    }
  } catch (error) {
    console.error('加载统计数据失败:', error)
  } finally {
    statisticsLoading.value = false
  }
}

// 格式化金额（项目金额）
const formatAmount = (amount) => {
  if (!amount) return '0'
  if (amount >= 10000) {
    return (amount / 10000).toFixed(1) + '亿'
  }
  return amount.toLocaleString() + '万'
}

// 格式化总金额
const formatTotalAmount = (amount) => {
  if (!amount || amount === 0) return '0'
  if (amount >= 10000) {
    return (amount / 10000).toFixed(1) + '亿'
  }
  return amount.toLocaleString() + '万'
}

// 格式化地区金额
const formatRegionAmount = (amount) => {
  if (!amount || amount === 0) return '0万'
  if (amount >= 10000) {
    return (amount / 10000).toFixed(1) + '亿'
  }
  return (amount / 10000).toFixed(2) + '亿'
}

onMounted(() => {
  loadAchievements()
  loadStatistics()
})
</script>

<style scoped>
.line-clamp-2 {
  display: -webkit-box;
  -webkit-line-clamp: 2;
  -webkit-box-orient: vertical;
  overflow: hidden
}

@keyframes scroll {
  0% {
    transform: translateX(0);
  }

  100% {
    transform: translateX(-50%);
  }
}

.animate-scroll {
  animation: scroll 30s linear infinite;
}

.animate-scroll:hover {
  animation-play-state: paused;
}
</style>