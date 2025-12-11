<template>
  <!-- 重要业绩展示 -->
  <div class="py-24 bg-hailong-dark text-white">
    <div class="container-wide">
      <div class="text-center mb-16">
        <h2 class="text-5xl font-bold mb-4 font-tech">重要业绩展示</h2>
        <div class="w-24 h-1 bg-gradient-to-r from-hailong-primary to-hailong-secondary mx-auto"></div>
      </div>
      <div class="relative overflow-hidden">
        <div class="flex gap-6 animate-scroll">
          <div v-for="achievement in [...majorAchievements, ...majorAchievements]"
            :key="achievement.id + Math.random()"
            @click="$emit('achievement-click', achievement.id)"
            class="flex-shrink-0 w-80 bg-white/10 backdrop-blur-lg rounded-2xl overflow-hidden hover:bg-white/20 transition-all cursor-pointer group">
            <div class="h-48 overflow-hidden">
              <img :src="achievement.imageUrl" :alt="achievement.projectName"
                class="w-full h-full object-cover group-hover:scale-110 transition-transform duration-500" />
            </div>
            <div class="p-6">
              <div class="flex items-center justify-between mb-3">
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
              <div class="text-2xl font-bold text-hailong-secondary">
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
      <div class="grid grid-cols-2 md:grid-cols-4 gap-8 mb-12">
        <div class="bg-white rounded-2xl p-8 shadow-lg text-center">
          <div class="text-4xl font-bold text-hailong-primary mb-2">{{ transactionData.yearlyStats.totalProjects }}
          </div>
          <div class="text-gray-600">项目总数</div>
        </div>
        <div class="bg-white rounded-2xl p-8 shadow-lg text-center">
          <div class="text-4xl font-bold text-hailong-secondary mb-2">{{ transactionData.yearlyStats.totalAmount }}
          </div>
          <div class="text-gray-600">交易总额</div>
        </div>
        <div class="bg-white rounded-2xl p-8 shadow-lg text-center">
          <div class="text-4xl font-bold text-hailong-primary mb-2">{{ transactionData.yearlyStats.govProcurement }}
          </div>
          <div class="text-gray-600">政府采购</div>
        </div>
        <div class="bg-white rounded-2xl p-8 shadow-lg text-center">
          <div class="text-4xl font-bold text-hailong-secondary mb-2">{{ transactionData.yearlyStats.construction }}
          </div>
          <div class="text-gray-600">建设工程</div>
        </div>
      </div>
      <div class="grid grid-cols-1 lg:grid-cols-2 gap-8">
        <!-- 交易类型占比 -->
        <div class="bg-white rounded-2xl p-8 shadow-lg">
          <h3 class="text-2xl font-bold text-gray-900 mb-6">交易类型占比</h3>
          <div class="flex flex-col items-center justify-center h-64">
            <div class="relative w-48 h-48">
              <svg class="w-full h-full transform -rotate-90" viewBox="0 0 100 100">
                <circle cx="50" cy="50" r="40" fill="none" stroke="#f3f4f6" stroke-width="20" />
                <circle cx="50" cy="50" r="40" fill="none" :stroke="transactionData.typeDistribution[0].color"
                  stroke-width="20" :stroke-dasharray="`${transactionData.typeDistribution[0].percentage * 2.51} 251`"
                  class="transition-all duration-500 hover:stroke-width-[22]" />
                <circle cx="50" cy="50" r="40" fill="none" :stroke="transactionData.typeDistribution[1].color"
                  stroke-width="20" :stroke-dasharray="`${transactionData.typeDistribution[1].percentage * 2.51} 251`"
                  :stroke-dashoffset="`-${transactionData.typeDistribution[0].percentage * 2.51}`"
                  class="transition-all duration-500 hover:stroke-width-[22]" />
              </svg>
              <div class="absolute inset-0 flex items-center justify-center">
                <div class="text-center">
                  <div class="text-3xl font-bold text-gray-900">{{ transactionData.yearlyStats.totalProjects }}</div>
                  <div class="text-sm text-gray-500">总项目</div>
                </div>
              </div>
            </div>
            <div class="mt-6 space-y-3 w-full">
              <div v-for="item in transactionData.typeDistribution" :key="item.type"
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
          <div class="space-y-3">
            <div v-for="(region, index) in transactionData.regionRanking" :key="region.region" class="group relative">
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
                  <div class="text-lg font-bold text-hailong-primary">{{ region.projects }}</div>
                  <div class="text-xs text-gray-500">{{ region.amount }}亿</div>
                </div>
              </div>
              <div class="relative h-2 bg-gray-100 rounded-full overflow-hidden">
                <div
                  class="absolute inset-y-0 left-0 bg-gradient-to-r from-hailong-primary to-hailong-secondary rounded-full transition-all duration-500 group-hover:opacity-80"
                  :style="{ width: (region.projects / transactionData.regionRanking[0].projects * 100) + '%' }">
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
import { transactionData, majorAchievements } from '@/config/data.js'

defineEmits(['achievement-click'])

const formatAmount = (amount) => {
  if (amount >= 10000) {
    return (amount / 10000).toFixed(1) + '亿'
  }
  return amount.toLocaleString() + '万'
}
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