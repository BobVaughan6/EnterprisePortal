<template>
  <!-- 业务范围 -->
  <div class="py-24 bg-gradient-to-b from-blue-50/30 to-gray-50">
    <div class="container-wide">
      <div class="text-center mb-16">
        <h2 class="text-5xl font-bold text-hailong-dark mb-4 font-tech">业务范围</h2>
        <div class="w-24 h-1 bg-gradient-to-r from-hailong-primary to-hailong-secondary mx-auto"></div>
      </div>
      <div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-8">
        <div v-for="business in businessScope" :key="business.id"
          @click="$emit('business-click', business.id)"
          class="group relative bg-white rounded-2xl overflow-hidden shadow-lg hover:shadow-2xl transition-all duration-300 border border-gray-200 hover:border-hailong-primary cursor-pointer">
          <div class="h-48 overflow-hidden">
            <img :src="business.image" :alt="business.name"
              class="w-full h-full object-cover group-hover:scale-110 transition-transform duration-500" />
          </div>
          <div class="p-6">
            <h3 class="text-2xl font-bold text-gray-900 mb-3 group-hover:text-hailong-primary transition-colors">{{
              business.name }}</h3>
            <p class="text-gray-600 mb-4">{{ business.description }}</p>
            <ul class="space-y-2">
              <li v-for="feature in business.features" :key="feature" class="text-sm text-gray-500 flex items-center">
                <span class="w-1.5 h-1.5 bg-hailong-secondary rounded-full mr-2"></span>{{ feature }}
              </li>
            </ul>
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
        <div class="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 gap-4 md:gap-6">
          <div v-for="(qualification, index) in qualifications" :key="index"
            @click="$emit('qualification-click', index + 1)"
            class="flex items-center p-4 bg-gradient-to-br from-hailong-primary/5 to-hailong-secondary/5 rounded-xl hover:shadow-md hover:scale-105 transition-all duration-300 cursor-pointer group">
            <div
              class="w-12 h-12 bg-hailong-primary/10 rounded-full flex items-center justify-center mr-4 flex-shrink-0 group-hover:bg-hailong-primary/20 transition-colors">
              <svg class="w-6 h-6 text-hailong-primary" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
                  d="M9 12l2 2 4-4m6 2a9 9 0 11-18 0 9 9 0 0118 0z"></path>
              </svg>
            </div>
            <span class="text-gray-800 font-medium text-sm group-hover:text-hailong-primary transition-colors">{{
              qualification }}</span>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { businessScope } from '@/config/data.js'

defineEmits(['business-click', 'qualification-click'])

const qualifications = [
  '政府采购代理机构资格证书',
  '工程招标代理机构资格证书',
  '工程造价咨询企业资质证书',
  '工程监理企业资质证书',
  'ISO9001质量管理体系认证',
  'ISO14001环境管理体系认证',
  'OHSAS18001职业健康安全管理体系认证',
  'AAA级信用企业'
]
</script>