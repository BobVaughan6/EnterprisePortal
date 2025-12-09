<template>
  <div class="min-h-screen bg-gray-50">
    <Header />
    
    <!-- 面包屑导航 -->
    <div class="bg-white border-b">
      <div class="container-wide py-4">
        <div class="flex items-center text-sm text-gray-600">
          <router-link to="/" class="hover:text-hailong-primary transition-colors">首页</router-link>
          <svg class="w-4 h-4 mx-2" fill="none" stroke="currentColor" viewBox="0 0 24 24">
            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M9 5l7 7-7 7" />
          </svg>
          <router-link to="/about" class="hover:text-hailong-primary transition-colors">关于我们</router-link>
          <svg class="w-4 h-4 mx-2" fill="none" stroke="currentColor" viewBox="0 0 24 24">
            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M9 5l7 7-7 7" />
          </svg>
          <span class="text-gray-900">资质详情</span>
        </div>
      </div>
    </div>

    <!-- 内容区域 -->
    <div class="py-12 bg-white">
      <div class="container-wide">
        <div v-if="loading" class="text-center py-20">
          <div class="inline-block animate-spin rounded-full h-12 w-12 border-4 border-hailong-primary border-t-transparent"></div>
          <p class="mt-4 text-gray-500">加载中...</p>
        </div>

        <div v-else-if="!qualification" class="text-center py-20">
          <svg class="w-20 h-20 mx-auto text-gray-300 mb-4" fill="none" stroke="currentColor" viewBox="0 0 24 24">
            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M9 12l2 2 4-4m6 2a9 9 0 11-18 0 9 9 0 0118 0z" />
          </svg>
          <p class="text-gray-500 mb-4">资质信息不存在</p>
          <router-link to="/about" class="text-hailong-primary hover:underline">返回关于我们</router-link>
        </div>

        <div v-else class="max-w-5xl mx-auto">
          <!-- 资质头部 -->
          <div class="bg-gradient-to-br from-hailong-primary via-hailong-secondary to-hailong-dark rounded-2xl shadow-xl p-12 mb-8 text-white relative overflow-hidden">
            <!-- 背景装饰 -->
            <div class="absolute top-0 right-0 w-64 h-64 bg-white/5 rounded-full -translate-y-1/2 translate-x-1/2"></div>
            <div class="absolute bottom-0 left-0 w-48 h-48 bg-white/5 rounded-full translate-y-1/2 -translate-x-1/2"></div>
            
            <div class="relative z-10">
              <div class="flex items-center gap-4 mb-6">
                <div class="w-16 h-16 bg-white/20 backdrop-blur-sm rounded-xl flex items-center justify-center">
                  <svg class="w-8 h-8" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M9 12l2 2 4-4m6 2a9 9 0 11-18 0 9 9 0 0118 0z" />
                  </svg>
                </div>
                <div>
                  <h1 class="text-4xl font-bold mb-2">{{ qualification.name }}</h1>
                  <p class="text-xl text-white/90">{{ qualification.description }}</p>
                </div>
              </div>
            </div>
          </div>

          <!-- 资质证书图片 -->
          <div class="bg-white rounded-xl shadow-sm overflow-hidden mb-8">
            <img :src="qualification.image" :alt="qualification.name" class="w-full h-auto object-contain max-h-[600px]" />
          </div>

          <!-- 资质信息卡片 -->
          <div class="grid grid-cols-1 md:grid-cols-3 gap-6 mb-8">
            <!-- 证书编号 -->
            <div v-if="qualification.certificateNo" class="bg-white rounded-xl shadow-sm p-6">
              <div class="flex items-center gap-3 mb-4">
                <div class="w-12 h-12 bg-hailong-primary/10 rounded-lg flex items-center justify-center">
                  <svg class="w-6 h-6 text-hailong-primary" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M7 20l4-16m2 16l4-16M6 9h14M4 15h14" />
                  </svg>
                </div>
                <div>
                  <div class="text-sm text-gray-500">证书编号</div>
                  <div class="text-lg font-bold text-gray-900">{{ qualification.certificateNo }}</div>
                </div>
              </div>
            </div>

            <!-- 颁发日期 -->
            <div v-if="qualification.issueDate" class="bg-white rounded-xl shadow-sm p-6">
              <div class="flex items-center gap-3 mb-4">
                <div class="w-12 h-12 bg-green-100 rounded-lg flex items-center justify-center">
                  <svg class="w-6 h-6 text-green-600" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M8 7V3m8 4V3m-9 8h10M5 21h14a2 2 0 002-2V7a2 2 0 00-2-2H5a2 2 0 00-2 2v12a2 2 0 002 2z" />
                  </svg>
                </div>
                <div>
                  <div class="text-sm text-gray-500">颁发日期</div>
                  <div class="text-lg font-bold text-gray-900">{{ qualification.issueDate }}</div>
                </div>
              </div>
            </div>

            <!-- 有效期至 -->
            <div v-if="qualification.validUntil" class="bg-white rounded-xl shadow-sm p-6">
              <div class="flex items-center gap-3 mb-4">
                <div class="w-12 h-12 bg-hailong-secondary/10 rounded-lg flex items-center justify-center">
                  <svg class="w-6 h-6 text-hailong-secondary" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M9 12l2 2 4-4m6 2a9 9 0 11-18 0 9 9 0 0118 0z" />
                  </svg>
                </div>
                <div>
                  <div class="text-sm text-gray-500">有效期至</div>
                  <div class="text-lg font-bold text-gray-900">{{ qualification.validUntil }}</div>
                </div>
              </div>
            </div>
          </div>

          <!-- 资质详情 -->
          <div class="bg-white rounded-xl shadow-sm p-8 mb-8">
            <h2 class="text-2xl font-bold text-gray-900 mb-6 pb-4 border-b border-gray-200 flex items-center gap-3">
              <div class="w-10 h-10 bg-hailong-primary/10 rounded-lg flex items-center justify-center">
                <svg class="w-6 h-6 text-hailong-primary" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                  <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M13 16h-1v-4h-1m1-4h.01M21 12a9 9 0 11-18 0 9 9 0 0118 0z" />
                </svg>
              </div>
              资质说明
            </h2>
            <div class="prose prose-lg max-w-none text-gray-700 leading-relaxed">
              <p>{{ qualification.description }}</p>
              
              <h3>资质意义</h3>
              <p>该资质证明了我公司在相关领域具备专业的服务能力和资格，能够为客户提供高质量、规范化的专业服务。</p>
              
              <h3>业务范围</h3>
              <p>持有该资质，我公司可以依法开展相关业务活动，为政府机关、企事业单位提供专业的咨询和服务。</p>
              
              <h3>质量保证</h3>
              <p>我公司严格按照资质要求和行业规范开展业务，建立了完善的质量管理体系，确保服务质量和客户满意度。</p>
            </div>
          </div>

          <!-- 相关资质 -->
          <div v-if="relatedQualifications.length > 0" class="bg-white rounded-xl shadow-sm p-8 mb-8">
            <h2 class="text-xl font-bold text-gray-900 mb-6 pb-4 border-b border-gray-200">
              相关资质
            </h2>
            <div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
              <router-link
                v-for="related in relatedQualifications"
                :key="related.id"
                :to="`/detail/qualification/${related.id}`"
                class="group bg-gradient-to-br from-gray-50 to-white rounded-xl p-6 border border-gray-100 hover:border-hailong-primary hover:shadow-lg transition-all"
              >
                <div class="flex items-center gap-3 mb-3">
                  <div class="w-10 h-10 bg-hailong-primary/10 rounded-lg flex items-center justify-center group-hover:bg-hailong-primary/20 transition-colors">
                    <svg class="w-5 h-5 text-hailong-primary" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                      <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M9 12l2 2 4-4m6 2a9 9 0 11-18 0 9 9 0 0118 0z" />
                    </svg>
                  </div>
                  <h3 class="text-base font-bold text-gray-900 group-hover:text-hailong-primary transition-colors line-clamp-2 flex-1">
                    {{ related.name }}
                  </h3>
                </div>
                <p class="text-sm text-gray-600 line-clamp-2">{{ related.description }}</p>
              </router-link>
            </div>
          </div>

          <!-- 返回按钮 -->
          <div class="flex justify-center">
            <router-link
              to="/about"
              class="px-6 py-3 bg-gray-100 text-gray-700 rounded-lg hover:bg-gray-200 transition-all font-medium flex items-center gap-2"
            >
              <svg class="w-5 h-5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M10 19l-7-7m0 0l7-7m-7 7h18" />
              </svg>
              返回关于我们
            </router-link>
          </div>
        </div>
      </div>
    </div>

    <Footer />
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import { useRoute } from 'vue-router'
import Header from '@/components/Header.vue'
import Footer from '@/components/Footer.vue'

const route = useRoute()

// 资质数据
const qualification = ref(null)
const loading = ref(true)
const relatedQualifications = ref([])

// 加载资质详情
const loadQualificationDetail = async () => {
  loading.value = true
  try {
    const id = route.params.id
    
    // 模拟API调用
    await new Promise(resolve => setTimeout(resolve, 500))
    
    // 模拟数据
    const qualifications = [
      {
        id: 1,
        name: '政府采购代理机构资格证书',
        image: 'https://images.unsplash.com/photo-1450101499163-c8848c66ca85?w=1200&h=800&fit=crop',
        description: '具备政府采购代理资质，为政府机关提供专业采购服务',
        certificateNo: 'ZC-2024-001',
        issueDate: '2024-01-15',
        validUntil: '2029-01-14'
      },
      {
        id: 2,
        name: '工程招标代理机构资格证书',
        image: 'https://images.unsplash.com/photo-1454165804606-c3d57bc86b40?w=1200&h=800&fit=crop',
        description: '拥有工程招标代理资质，提供全流程招标代理服务',
        certificateNo: 'GC-2024-002',
        issueDate: '2024-02-20',
        validUntil: '2029-02-19'
      },
      {
        id: 3,
        name: '工程造价咨询企业资质证书',
        image: 'https://images.unsplash.com/photo-1460925895917-afdab827c52f?w=1200&h=800&fit=crop',
        description: '专业工程造价咨询资质，提供精准造价分析',
        certificateNo: 'ZJ-2024-003',
        issueDate: '2024-03-10',
        validUntil: '2029-03-09'
      },
      {
        id: 4,
        name: '工程监理企业资质证书',
        image: 'https://images.unsplash.com/photo-1503387762-592deb58ef4e?w=1200&h=800&fit=crop',
        description: '工程监理资质认证，确保工程质量与安全',
        certificateNo: 'JL-2024-004',
        issueDate: '2024-04-05',
        validUntil: '2029-04-04'
      },
      {
        id: 5,
        name: 'ISO9001质量管理体系认证',
        image: 'https://images.unsplash.com/photo-1507679799987-c73779587ccf?w=1200&h=800&fit=crop',
        description: '国际质量管理体系认证，保障服务质量',
        certificateNo: 'ISO-2024-005',
        issueDate: '2024-05-15',
        validUntil: '2027-05-14'
      },
      {
        id: 6,
        name: 'ISO14001环境管理体系认证',
        image: 'https://images.unsplash.com/photo-1497366216548-37526070297c?w=1200&h=800&fit=crop',
        description: '环境管理体系认证，践行绿色发展理念',
        certificateNo: 'ISO-2024-006',
        issueDate: '2024-06-20',
        validUntil: '2027-06-19'
      },
      {
        id: 7,
        name: 'OHSAS18001职业健康安全管理体系认证',
        image: 'https://images.unsplash.com/photo-1521737711867-e3b97375f902?w=1200&h=800&fit=crop',
        description: '职业健康安全管理认证，保障员工安全',
        certificateNo: 'OHSAS-2024-007',
        issueDate: '2024-07-10',
        validUntil: '2027-07-09'
      },
      {
        id: 8,
        name: 'AAA级信用企业',
        image: 'https://images.unsplash.com/photo-1556761175-b413da4baf72?w=1200&h=800&fit=crop',
        description: '最高信用等级认证，诚信经营典范',
        certificateNo: 'AAA-2024-008',
        issueDate: '2024-08-01',
        validUntil: '2027-07-31'
      }
    ]
    
    qualification.value = qualifications.find(q => q.id === parseInt(id)) || qualifications[0]
    
    // 加载相关资质（排除当前资质）
    relatedQualifications.value = qualifications
      .filter(q => q.id !== qualification.value.id)
      .slice(0, 3)
  } catch (error) {
    console.error('加载资质详情失败:', error)
    qualification.value = null
  } finally {
    loading.value = false
  }
}

// 组件挂载时加载数据
onMounted(() => {
  loadQualificationDetail()
})
</script>

<style scoped>
.line-clamp-2 {
  display: -webkit-box;
  -webkit-line-clamp: 2;
  -webkit-box-orient: vertical;
  overflow: hidden;
}

.prose h3 {
  font-size: 1.25rem;
  font-weight: 700;
  color: #1f2937;
  margin-top: 2rem;
  margin-bottom: 1rem;
}

.prose p {
  margin-bottom: 1rem;
  line-height: 1.75;
}
</style>