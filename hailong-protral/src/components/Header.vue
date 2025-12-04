<template>
  <nav class="fixed top-0 left-0 right-0 z-50 bg-huawei-dark/90 backdrop-blur-md text-white shadow-2xl border-b border-huawei-cyan/30">
    <div class="container-wide">
      <div class="flex items-center justify-between h-20">
        <div class="flex items-center space-x-3">
          <img :src="logoUrl" alt="海隆咨询" class="h-12 w-auto" />
          <div class="text-3xl font-bold font-tech bg-gradient-to-r from-huawei-primary to-huawei-secondary bg-clip-text text-transparent">
            海隆咨询
          </div>
        </div>
        <div class="hidden md:flex items-center space-x-8">
          <router-link v-for="link in navLinks" :key="link.name" :to="link.path"
            class="hover:text-huawei-cyan transition-colors text-sm font-medium"
            :class="{ 'text-huawei-cyan': isActive(link.path) }">
            {{ link.name }}
          </router-link>
        </div>
        <!-- 移动端菜单按钮 -->
        <button @click="toggleMobileMenu" class="md:hidden text-white">
          <svg class="w-6 h-6" fill="none" stroke="currentColor" viewBox="0 0 24 24">
            <path v-if="!showMobileMenu" stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M4 6h16M4 12h16M4 18h16"></path>
            <path v-else stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M6 18L18 6M6 6l12 12"></path>
          </svg>
        </button>
      </div>
    </div>
    <!-- 移动端菜单 -->
    <div v-if="showMobileMenu" class="md:hidden bg-huawei-dark/95 border-t border-huawei-cyan/30">
      <div class="container-wide py-4">
        <router-link v-for="link in navLinks" :key="link.name" :to="link.path"
          @click="showMobileMenu = false"
          class="block py-3 px-4 hover:bg-huawei-primary/20 rounded-lg transition-colors"
          :class="{ 'bg-huawei-primary/30 text-huawei-cyan': isActive(link.path) }">
          {{ link.name }}
        </router-link>
      </div>
    </div>
  </nav>
</template>

<script setup>
import { ref } from 'vue'
import { useRoute } from 'vue-router'
import { navLinks } from '@/views/huawei/data.js'
import logoUrl from '@/assets/logo.png'

const route = useRoute()
const showMobileMenu = ref(false)

const isActive = (path) => {
  return route.path === path || route.path.startsWith(path + '/')
}

const toggleMobileMenu = () => {
  showMobileMenu.value = !showMobileMenu.value
}
</script>

<style scoped>
/* 组件特定样式 */
</style>