<template>
  <div class="min-h-screen bg-gradient-to-br from-huawei-dark via-huawei-primary to-huawei-secondary overflow-hidden">
    <nav
      class="fixed top-0 left-0 right-0 z-50 bg-huawei-dark/90 backdrop-blur-md text-white shadow-2xl border-b border-huawei-cyan/30">
      <div class="container-wide">
        <div class="flex items-center justify-between h-20">
          <div class="flex items-center space-x-3">
            <div
              class="text-3xl font-bold font-tech bg-gradient-to-r from-huawei-primary to-huawei-secondary bg-clip-text text-transparent">
              HUAWEI</div>
            <div class="text-sm text-gray-400">| Enterprise</div>
          </div>
          <div class="hidden md:flex space-x-8"><router-link v-for="link in navLinks" :key="link.name" :to="link.path"
              class="hover:text-huawei-cyan transition-colors text-sm font-medium">{{ link.name }}</router-link></div>
        </div>
      </div>
    </nav>
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
          <h2 class="text-5xl font-bold text-huawei-dark mb-4 font-tech">解决方案</h2>
          <div class="w-24 h-1 bg-gradient-to-r from-huawei-primary to-huawei-secondary mx-auto"></div>
        </div>
        <div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-8">
          <div v-for="solution in solutions" :key="solution.id"
            class="group relative bg-white rounded-2xl p-8 shadow-lg hover:shadow-2xl transition-all duration-300 border border-gray-200 hover:border-huawei-primary overflow-hidden">
            <div
              class="absolute top-0 right-0 w-32 h-32 bg-gradient-to-br from-huawei-primary/10 to-transparent rounded-bl-full">
            </div>
            <div class="relative">
              <div class="text-5xl mb-4">{{ solution.icon }}</div>
              <h3 class="text-2xl font-bold text-gray-900 mb-3 group-hover:text-huawei-primary transition-colors">{{
                solution.name }}</h3>
              <p class="text-gray-600 mb-4">{{ solution.description }}</p>
              <ul class="space-y-2">
                <li v-for="feature in solution.features" :key="feature" class="text-sm text-gray-500 flex items-center">
                  <span class="w-1.5 h-1.5 bg-huawei-secondary rounded-full mr-2"></span>{{ feature }}</li>
              </ul>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div class="py-24 bg-huawei-dark text-white">
      <div class="container-wide">
        <div class="text-center mb-16">
          <h2 class="text-5xl font-bold mb-4 font-tech">成功案例</h2>
          <div class="w-24 h-1 bg-gradient-to-r from-huawei-primary to-huawei-secondary mx-auto"></div>
        </div>
        <div class="grid grid-cols-1 md:grid-cols-3 gap-8">
          <div v-for="case_ in cases" :key="case_.id"
            class="bg-white/10 backdrop-blur-lg rounded-2xl overflow-hidden hover:bg-white/20 transition-all cursor-pointer group">
            <div class="h-56 overflow-hidden"><img :src="case_.image" :alt="case_.title"
                class="w-full h-full object-cover group-hover:scale-110 transition-transform duration-500" /></div>
            <div class="p-8">
              <div class="text-huawei-cyan text-sm font-semibold mb-2">{{ case_.city }}</div>
              <h3 class="text-2xl font-bold mb-3">{{ case_.title }}</h3>
              <div class="space-y-2 text-sm text-gray-300">
                <p><strong>成果：</strong>{{ case_.achievement }}</p>
                <p class="text-huawei-secondary"><strong>影响：</strong>{{ case_.impact }}</p>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div class="py-24 bg-white">
      <div class="container-wide">
        <div class="flex items-center justify-between mb-12">
          <h2 class="text-4xl font-bold text-huawei-dark font-tech">招标信息</h2><router-link to="/detail/bidding"
            class="text-huawei-primary hover:underline">查看全部 →</router-link>
        </div>
        <div class="space-y-4">
          <div v-for="bid in biddings" :key="bid.id"
            class="p-6 bg-gradient-to-r from-gray-50 to-white rounded-xl hover:shadow-lg transition-all cursor-pointer border-l-4 border-huawei-primary">
            <div class="flex justify-between items-start">
              <div class="flex-1">
                <h3 class="text-xl font-bold text-gray-900 mb-2">{{ bid.title }}</h3>
                <div class="flex gap-6 text-sm text-gray-600"><span>预算: <strong class="text-huawei-primary">{{
                      bid.budget }}</strong></span><span>范围: {{ bid.scope }}</span></div>
              </div>
              <div class="text-right">
                <div class="text-sm text-gray-500 mb-2">截止: {{ bid.deadline }}</div><span
                  class="px-4 py-1 bg-green-100 text-green-800 rounded-full text-xs">{{ bid.status }}</span>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div class="py-24 bg-gradient-to-br from-huawei-primary via-huawei-secondary to-huawei-dark text-white">
      <div class="container-wide">
        <div class="text-center mb-16">
          <h2 class="text-5xl font-bold mb-4 font-tech">行业洞察</h2>
          <div class="w-24 h-1 bg-white/50 mx-auto"></div>
        </div>
        <div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-8">
          <div v-for="insight in insights.slice(0, 6)" :key="insight.id"
            class="bg-white text-gray-900 rounded-2xl overflow-hidden hover:shadow-2xl transition-all transform hover:-translate-y-2 cursor-pointer">
            <div
              class="h-48 overflow-hidden bg-gradient-to-br from-huawei-primary/20 to-huawei-secondary/20 flex items-center justify-center">
              <img :src="insight.image" :alt="insight.title" class="w-full h-full object-cover opacity-80" /></div>
            <div class="p-6">
              <div class="flex items-center justify-between mb-3 text-sm"><span
                  class="text-huawei-primary font-semibold">{{ insight.category }}</span><span class="text-gray-500">下载:
                  {{ insight.downloads }}</span></div>
              <h3 class="text-xl font-bold mb-2 line-clamp-2">{{ insight.title }}</h3>
              <p class="text-gray-600 text-sm line-clamp-2 mb-4">{{ insight.summary }}</p>
              <div class="text-huawei-primary text-sm font-medium">查看详情 →</div>
            </div>
          </div>
        </div>
      </div>
    </div>
    <footer class="bg-huawei-dark text-white py-12 border-t border-huawei-cyan/20">
      <div class="container-wide">
        <div class="grid grid-cols-1 md:grid-cols-4 gap-8 mb-8">
          <div>
            <h3 class="text-xl font-bold mb-4 text-huawei-cyan font-tech">华为政企</h3>
            <p class="text-sm text-gray-400">数字化转型的使能者</p>
          </div>
          <div>
            <h4 class="font-semibold mb-4">解决方案</h4>
            <ul class="space-y-2 text-sm text-gray-400">
              <li><a href="#" class="hover:text-huawei-cyan">智慧城市</a></li>
              <li><a href="#" class="hover:text-huawei-cyan">数字政府</a></li>
            </ul>
          </div>
          <div>
            <h4 class="font-semibold mb-4">行业</h4>
            <ul class="space-y-2 text-sm text-gray-400">
              <li><a href="#" class="hover:text-huawei-cyan">金融</a></li>
              <li><a href="#" class="hover:text-huawei-cyan">制造</a></li>
            </ul>
          </div>
          <div>
            <h4 class="font-semibold mb-4">联系我们</h4>
            <ul class="space-y-2 text-sm text-gray-400">
              <li>电话: 400-822-9999</li>
              <li>邮箱: enterprise@huawei.com</li>
            </ul>
          </div>
        </div>
        <div class="border-t border-gray-800 pt-8 text-center text-gray-400 text-sm">
          <p>© 2025 华为技术有限公司</p>
        </div>
      </div>
    </footer>
  </div>
</template>

<script setup>
import { companyInfo, solutions, cases, insights, biddings, navLinks } from './data.js'
</script>

<style scoped>
.line-clamp-2 {
  display: -webkit-box;
  -webkit-line-clamp: 2;
  -webkit-box-orient: vertical;
  overflow: hidden
}
</style>
