<template>
  <div class="relative h-screen flex items-center justify-center pt-20">
    <div class="absolute inset-0">
      <div
        class="absolute top-1/4 left-1/4 w-96 h-96 bg-hailong-primary rounded-full filter blur-3xl opacity-20 animate-float">
      </div>
      <div
        class="absolute bottom-1/4 right-1/4 w-96 h-96 bg-hailong-secondary rounded-full filter blur-3xl opacity-20 animate-float"
        style="animation-delay:1s"></div>
      <div v-for="i in 20" :key="i" class="absolute w-1 h-1 bg-hailong-cyan rounded-full animate-ping"
        :style="{ top: `${Math.random() * 100}%`, left: `${Math.random() * 100}%`, animationDelay: `${Math.random() * 2}s` }">
      </div>
    </div>
    <div class="relative text-center text-white px-4">
      <h1
        class="text-7xl font-bold mb-6 font-tech bg-gradient-to-r from-white via-hailong-cyan to-white bg-clip-text text-transparent animate-fade-in">
        {{ companyInfo.slogan }}</h1>
      <p class="text-2xl text-gray-200 mb-12 max-w-4xl mx-auto">{{ companyInfo.description }}</p>
      <button @click="$emit('show-contact')"
        class="inline-block px-12 py-5 bg-gradient-to-r from-hailong-primary to-hailong-secondary rounded-full text-white text-lg font-medium hover:shadow-2xl hover:shadow-hailong-primary/50 transition-all transform hover:scale-105">
        立即咨询
      </button>
    </div>
  </div>
</template>

<script setup>
import { computed } from 'vue'
import { getCompanyInfo } from '@/utils/config'

defineEmits(['show-contact'])

const companyInfo = computed(() => getCompanyInfo())
</script>
