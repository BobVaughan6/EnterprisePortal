<template>
  <div class="min-h-screen bg-gradient-to-br from-huawei-dark via-huawei-primary to-huawei-secondary overflow-hidden">
    <Header />
    <div class="relative h-screen flex items-center justify-center pt-20">
      <div class="absolute inset-0">
        <div
          class="absolute top-1/4 left-1/4 w-96 h-96 bg-huawei-primary rounded-full filter blur-3xl opacity-20 animate-float">
        </div>
        <div
          class="absolute bottom-1/4 right-1/4 w-96 h-96 bg-huawei-secondary rounded-full filter blur-3xl opacity-20 animate-float"
          style="animation-delay:1s"></div>
        <div v-for="i in 20" :key="i" class="absolute w-1 h-1 bg-huawei-cyan rounded-full animate-ping"
          :style="{ top: `${Math.random() * 100}%`, left: `${Math.random() * 100}%`, animationDelay: `${Math.random() * 2}s` }">
        </div>
      </div>
      <div class="relative text-center text-white px-4">
        <h1
          class="text-7xl font-bold mb-6 font-tech bg-gradient-to-r from-white via-huawei-cyan to-white bg-clip-text text-transparent animate-fade-in">
          {{ companyInfo.slogan }}</h1>
        <p class="text-2xl text-gray-200 mb-12 max-w-4xl mx-auto">{{ companyInfo.description }}</p><router-link
          to="/huawei/detail/contact"
          class="inline-block px-12 py-5 bg-gradient-to-r from-huawei-primary to-huawei-secondary rounded-full text-white text-lg font-medium hover:shadow-2xl hover:shadow-huawei-primary/50 transition-all transform hover:scale-105">立即咨询</router-link>
      </div>
    </div>
    <div class="py-20 bg-white">
      <div class="container-wide">
        <div class="grid grid-cols-2 md:grid-cols-4 gap-12 text-center">
          <div v-for="stat in companyInfo.stats" :key="stat.label">
            <div
              class="text-6xl font-bold bg-gradient-to-r from-huawei-primary to-huawei-secondary bg-clip-text text-transparent mb-3">
              {{ stat.value }}</div>
            <div class="text-gray-600 text-sm">{{ stat.label }}</div>
          </div>
        </div>
      </div>
    </div>
    <div class="py-24 bg-gradient-to-b from-white to-gray-50">
      <div class="container-wide">
        <div class="text-center mb-16">
          <h2 class="text-5xl font-bold text-huawei-dark mb-4 font-tech">企业简介</h2>
          <div class="w-24 h-1 bg-gradient-to-r from-huawei-primary to-huawei-secondary mx-auto"></div>
        </div>
        <div class="max-w-4xl mx-auto">
          <div class="bg-white rounded-2xl p-12 shadow-lg border border-gray-200">
            <p class="text-gray-700 text-lg leading-relaxed mb-8">{{ companyProfile.content }}</p>
            <div class="grid grid-cols-2 md:grid-cols-4 gap-6">
              <div v-for="highlight in companyProfile.highlights" :key="highlight"
                class="text-center p-4 bg-gradient-to-br from-huawei-primary/5 to-huawei-secondary/5 rounded-xl">
                <div class="text-huawei-primary font-semibold">{{ highlight }}</div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div class="py-24 bg-white">
      <div class="container-wide">
        <div class="text-center mb-16">
          <h2 class="text-5xl font-bold text-huawei-dark mb-4 font-tech">业务范围</h2>
          <div class="w-24 h-1 bg-gradient-to-r from-huawei-primary to-huawei-secondary mx-auto"></div>
        </div>
        <div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-8">
          <div v-for="business in businessScope" :key="business.id"
            class="group relative bg-white rounded-2xl overflow-hidden shadow-lg hover:shadow-2xl transition-all duration-300 border border-gray-200 hover:border-huawei-primary">
            <div class="h-48 overflow-hidden">
              <img :src="business.image" :alt="business.name"
                class="w-full h-full object-cover group-hover:scale-110 transition-transform duration-500" />
            </div>
            <div class="p-6">
              <h3 class="text-2xl font-bold text-gray-900 mb-3 group-hover:text-huawei-primary transition-colors">{{
                business.name }}</h3>
              <p class="text-gray-600 mb-4">{{ business.description }}</p>
              <ul class="space-y-2">
                <li v-for="feature in business.features" :key="feature" class="text-sm text-gray-500 flex items-center">
                  <span class="w-1.5 h-1.5 bg-huawei-secondary rounded-full mr-2"></span>{{ feature }}
                </li>
              </ul>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div class="py-24 bg-gradient-to-b from-gray-50 to-white">
      <div class="container-wide">
        <div class="text-center mb-16">
          <h2 class="text-5xl font-bold text-huawei-dark mb-4 font-tech">交易数据可视化</h2>
          <div class="w-24 h-1 bg-gradient-to-r from-huawei-primary to-huawei-secondary mx-auto"></div>
        </div>
        <div class="grid grid-cols-2 md:grid-cols-4 gap-8 mb-12">
          <div class="bg-white rounded-2xl p-8 shadow-lg text-center">
            <div class="text-4xl font-bold text-huawei-primary mb-2">{{ transactionData.yearlyStats.totalProjects }}</div>
            <div class="text-gray-600">项目总数</div>
          </div>
          <div class="bg-white rounded-2xl p-8 shadow-lg text-center">
            <div class="text-4xl font-bold text-huawei-secondary mb-2">{{ transactionData.yearlyStats.totalAmount }}</div>
            <div class="text-gray-600">交易总额</div>
          </div>
          <div class="bg-white rounded-2xl p-8 shadow-lg text-center">
            <div class="text-4xl font-bold text-huawei-primary mb-2">{{ transactionData.yearlyStats.govProcurement }}</div>
            <div class="text-gray-600">政府采购</div>
          </div>
          <div class="bg-white rounded-2xl p-8 shadow-lg text-center">
            <div class="text-4xl font-bold text-huawei-secondary mb-2">{{ transactionData.yearlyStats.construction }}</div>
            <div class="text-gray-600">建设工程</div>
          </div>
        </div>
        <div class="bg-white rounded-2xl p-8 shadow-lg">
          <h3 class="text-2xl font-bold text-gray-900 mb-6">月度项目趋势</h3>
          <div class="flex items-end justify-between h-64 gap-2">
            <div v-for="data in transactionData.monthlyTrend" :key="data.month" class="flex-1 flex flex-col items-center">
              <div class="w-full bg-gradient-to-t from-huawei-primary to-huawei-secondary rounded-t-lg transition-all hover:opacity-80"
                :style="{ height: (data.projects / 150 * 100) + '%' }"></div>
              <div class="text-xs text-gray-600 mt-2">{{ data.month }}</div>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div class="py-24 bg-huawei-dark text-white">
      <div class="container-wide">
        <div class="text-center mb-16">
          <h2 class="text-5xl font-bold mb-4 font-tech">重要业绩展示</h2>
          <div class="w-24 h-1 bg-gradient-to-r from-huawei-primary to-huawei-secondary mx-auto"></div>
        </div>
        <div class="relative overflow-hidden">
          <div class="flex gap-6 animate-scroll">
            <div v-for="achievement in [...majorAchievements, ...majorAchievements]" :key="achievement.id + Math.random()"
              class="flex-shrink-0 w-80 bg-white/10 backdrop-blur-lg rounded-2xl overflow-hidden hover:bg-white/20 transition-all cursor-pointer group">
              <div class="h-48 overflow-hidden">
                <img :src="achievement.image" :alt="achievement.title"
                  class="w-full h-full object-cover group-hover:scale-110 transition-transform duration-500" />
              </div>
              <div class="p-6">
                <div class="text-huawei-cyan text-sm font-semibold mb-2">{{ achievement.year }}年</div>
                <h3 class="text-lg font-bold mb-3 line-clamp-2">{{ achievement.title }}</h3>
                <div class="text-2xl font-bold text-huawei-secondary">{{ achievement.amount }}</div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div class="py-24 bg-white">
      <div class="container-wide">
        <div class="text-center mb-16">
          <h2 class="text-5xl font-bold text-huawei-dark mb-4 font-tech">公告信息预览</h2>
          <div class="w-24 h-1 bg-gradient-to-r from-huawei-primary to-huawei-secondary mx-auto"></div>
        </div>
        <div class="grid grid-cols-1 lg:grid-cols-2 gap-8">
          <div>
            <div class="flex items-center justify-between mb-6">
              <h3 class="text-2xl font-bold text-huawei-dark">政府采购公告</h3>
              <router-link to="/huawei/detail/announcements"
                class="text-huawei-primary hover:underline text-sm">查看全部 →</router-link>
            </div>
            <div class="space-y-4">
              <div v-for="announcement in govProcurementAnnouncements" :key="announcement.id"
                class="p-6 bg-gradient-to-r from-gray-50 to-white rounded-xl hover:shadow-lg transition-all cursor-pointer border-l-4 border-huawei-primary">
                <div class="flex justify-between items-start mb-3">
                  <span class="px-3 py-1 bg-blue-100 text-blue-800 rounded-full text-xs">{{ announcement.type }}</span>
                  <span class="text-xs text-gray-500">{{ announcement.publishDate }}</span>
                </div>
                <h4 class="text-lg font-bold text-gray-900 mb-2 line-clamp-2">{{ announcement.title }}</h4>
                <div class="flex justify-between items-center text-sm">
                  <span class="text-gray-600">预算: <strong class="text-huawei-primary">{{ announcement.budget }}</strong></span>
                  <span class="text-gray-500">截止: {{ announcement.deadline }}</span>
                </div>
              </div>
            </div>
          </div>
          <div>
            <div class="flex items-center justify-between mb-6">
              <h3 class="text-2xl font-bold text-huawei-dark">建设工程公告</h3>
              <router-link to="/huawei/detail/announcements"
                class="text-huawei-primary hover:underline text-sm">查看全部 →</router-link>
            </div>
            <div class="space-y-4">
              <div v-for="announcement in constructionAnnouncements" :key="announcement.id"
                class="p-6 bg-gradient-to-r from-gray-50 to-white rounded-xl hover:shadow-lg transition-all cursor-pointer border-l-4 border-huawei-secondary">
                <div class="flex justify-between items-start mb-3">
                  <span class="px-3 py-1 bg-green-100 text-green-800 rounded-full text-xs">{{ announcement.type }}</span>
                  <span class="text-xs text-gray-500">{{ announcement.publishDate }}</span>
                </div>
                <h4 class="text-lg font-bold text-gray-900 mb-2 line-clamp-2">{{ announcement.title }}</h4>
                <div class="flex justify-between items-center text-sm">
                  <span class="text-gray-600">预算: <strong class="text-huawei-secondary">{{ announcement.budget }}</strong></span>
                  <span class="text-gray-500">截止: {{ announcement.deadline }}</span>
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
import { companyInfo, companyProfile, businessScope, transactionData, majorAchievements, govProcurementAnnouncements, constructionAnnouncements } from './data.js'
import Header from '@/components/Header.vue'
import Footer from '@/components/Footer.vue'
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
