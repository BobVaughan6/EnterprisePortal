<template>
  <div class="min-h-screen bg-white">
    <nav class="bg-hailong-dark text-white shadow-lg"><div class="container-wide"><div class="flex items-center justify-between h-20"><router-link to="/" class="text-3xl font-bold font-tech bg-gradient-to-r from-hailong-primary to-hailong-secondary bg-clip-text text-transparent">hailong</router-link><div class="hidden md:flex space-x-8"><router-link v-for="link in navLinks" :key="link.name" :to="link.path" class="hover:text-hailong-cyan transition-colors text-sm font-medium">{{ link.name }}</router-link></div></div></div></nav>
    <div class="bg-gradient-to-r from-hailong-primary via-hailong-secondary to-hailong-dark py-20"><div class="container-wide text-white"><h1 class="text-6xl font-bold mb-4 font-tech">{{ pageTitle }}</h1><p class="text-xl">数字化 智能化 让城市更智慧</p></div></div>
    <div class="container-wide py-20"><div class="max-w-4xl mx-auto"><img src="https://images.unsplash.com/photo-1451187580459-43490279c0fa?w=1200&h=600&fit=crop" alt="Detail" class="w-full h-96 object-cover rounded-2xl shadow-2xl mb-12"/><div class="prose prose-lg max-w-none"><h2 class="text-4xl font-bold bg-gradient-to-r from-hailong-primary to-hailong-secondary bg-clip-text text-transparent mb-8 font-tech">关于华为政企</h2><p class="text-gray-700 text-lg leading-relaxed mb-8">华为政企业务致力于成为数字中国的底座、智慧城市的使能者,通过云计算、大数据、人工智能、5G等新ICT技术,助力政府与企业数字化转型。</p><div class="bg-gradient-to-r from-hailong-primary/10 to-hailong-secondary/10 p-10 rounded-2xl mb-8"><h3 class="text-3xl font-bold text-hailong-dark mb-6 font-tech">核心能力</h3><div class="grid grid-cols-1 md:grid-cols-2 gap-6"><div class="space-y-3"><h4 class="text-xl font-bold text-hailong-primary">技术创新</h4><p class="text-gray-700">持续研发投入,引领ICT技术发展</p></div><div class="space-y-3"><h4 class="text-xl font-bold text-hailong-primary">生态合作</h4><p class="text-gray-700">携手28000+合作伙伴共建生态</p></div><div class="space-y-3"><h4 class="text-xl font-bold text-hailong-primary">全栈能力</h4><p class="text-gray-700">云网边端协同的完整解决方案</p></div><div class="space-y-3"><h4 class="text-xl font-bold text-hailong-primary">安全可信</h4><p class="text-gray-700">端到端的安全保障体系</p></div></div></div></div><div class="mt-16 pt-8 border-t flex justify-center"><router-link to="/" class="px-8 py-4 bg-gradient-to-r from-hailong-primary to-hailong-secondary text-white rounded-lg hover:shadow-xl transition-all">← 返回首页</router-link></div></div></div>
    <footer class="bg-hailong-dark text-white py-8 mt-20"><div class="container-wide text-center"><p class="text-gray-400">© 2025 华为技术有限公司</p></div></footer>
  </div>
</template>

<script setup>
import { computed } from 'vue'
import { useRoute } from 'vue-router'
import { navLinks } from './data.js'
const route = useRoute()
const pageTitle = computed(() => {
  const titles = { about: '关于我们', solutions: '解决方案', cases: '成功案例', insights: '行业洞察', bidding: '招标信息', partners: '生态合作', contact: '联系我们' }
  return titles[route.params.id] || '详情页'
})
</script>
