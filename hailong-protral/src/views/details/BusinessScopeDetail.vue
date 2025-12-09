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
          <span class="text-gray-900">业务详情</span>
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

        <div v-else-if="!business" class="text-center py-20">
          <svg class="w-20 h-20 mx-auto text-gray-300 mb-4" fill="none" stroke="currentColor" viewBox="0 0 24 24">
            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M21 13.255A23.931 23.931 0 0112 15c-3.183 0-6.22-.62-9-1.745M16 6V4a2 2 0 00-2-2h-4a2 2 0 00-2 2v2m4 6h.01M5 20h14a2 2 0 002-2V8a2 2 0 00-2-2H5a2 2 0 00-2 2v10a2 2 0 002 2z" />
          </svg>
          <p class="text-gray-500 mb-4">业务信息不存在</p>
          <router-link to="/about" class="text-hailong-primary hover:underline">返回关于我们</router-link>
        </div>

        <div v-else class="max-w-6xl mx-auto">
          <!-- 业务头部 -->
          <div class="bg-gradient-to-br from-hailong-primary via-hailong-secondary to-hailong-dark rounded-2xl shadow-xl p-12 mb-8 text-white">
            <div class="flex items-center gap-4 mb-6">
              <div class="w-16 h-16 bg-white/20 backdrop-blur-sm rounded-xl flex items-center justify-center">
                <svg class="w-8 h-8" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                  <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M21 13.255A23.931 23.931 0 0112 15c-3.183 0-6.22-.62-9-1.745M16 6V4a2 2 0 00-2-2h-4a2 2 0 00-2 2v2m4 6h.01M5 20h14a2 2 0 002-2V8a2 2 0 00-2-2H5a2 2 0 00-2 2v10a2 2 0 002 2z" />
                </svg>
              </div>
              <div>
                <h1 class="text-4xl font-bold mb-2">{{ business.name }}</h1>
                <p class="text-xl text-white/90">{{ business.description }}</p>
              </div>
            </div>
          </div>

          <!-- 业务配图 -->
          <div v-if="business.image" class="bg-white rounded-xl shadow-sm overflow-hidden mb-8">
            <img :src="business.image" :alt="business.name" class="w-full h-96 object-cover" />
          </div>

          <!-- 业务特点 -->
          <div class="bg-white rounded-xl shadow-sm p-8 mb-8">
            <h2 class="text-2xl font-bold text-gray-900 mb-6 pb-4 border-b border-gray-200 flex items-center gap-3">
              <div class="w-10 h-10 bg-hailong-primary/10 rounded-lg flex items-center justify-center">
                <svg class="w-6 h-6 text-hailong-primary" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                  <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M9 12l2 2 4-4m6 2a9 9 0 11-18 0 9 9 0 0118 0z" />
                </svg>
              </div>
              业务特点
            </h2>
            <div class="grid grid-cols-1 md:grid-cols-2 gap-6">
              <div
                v-for="(feature, index) in business.features"
                :key="index"
                class="flex items-start gap-4 p-5 bg-gradient-to-br from-gray-50 to-white rounded-xl border border-gray-100 hover:shadow-md transition-all"
              >
                <div class="flex-shrink-0 w-8 h-8 bg-gradient-to-br from-hailong-primary to-hailong-secondary rounded-lg flex items-center justify-center text-white font-bold text-sm">
                  {{ index + 1 }}
                </div>
                <div class="flex-1">
                  <p class="text-gray-700 leading-relaxed">{{ feature }}</p>
                </div>
              </div>
            </div>
          </div>

          <!-- 详细内容 -->
          <div class="bg-white rounded-xl shadow-sm p-8 mb-8">
            <h2 class="text-2xl font-bold text-gray-900 mb-6 pb-4 border-b border-gray-200 flex items-center gap-3">
              <div class="w-10 h-10 bg-hailong-secondary/10 rounded-lg flex items-center justify-center">
                <svg class="w-6 h-6 text-hailong-secondary" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                  <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M9 12h6m-6 4h6m2 5H7a2 2 0 01-2-2V5a2 2 0 012-2h5.586a1 1 0 01.707.293l5.414 5.414a1 1 0 01.293.707V19a2 2 0 01-2 2z" />
                </svg>
              </div>
              详细介绍
            </h2>
            <div class="prose prose-lg max-w-none text-gray-700 leading-relaxed" v-html="business.detailContent"></div>
          </div>

          <!-- 联系咨询 -->
          <div class="bg-gradient-to-r from-hailong-primary/10 via-hailong-secondary/10 to-hailong-cyan/10 rounded-xl p-8 mb-8">
            <div class="text-center">
              <h2 class="text-2xl font-bold text-gray-900 mb-4">需要此项服务？</h2>
              <p class="text-gray-600 mb-6">我们的专业团队随时为您提供咨询服务</p>
              <div class="flex items-center justify-center gap-4">
                <router-link
                  to="/contact"
                  class="px-8 py-3 bg-gradient-to-r from-hailong-primary to-hailong-secondary text-white rounded-lg hover:shadow-lg transition-all font-medium flex items-center gap-2"
                >
                  <svg class="w-5 h-5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M3 8l7.89 5.26a2 2 0 002.22 0L21 8M5 19h14a2 2 0 002-2V7a2 2 0 00-2-2H5a2 2 0 00-2 2v10a2 2 0 002 2z" />
                  </svg>
                  立即咨询
                </router-link>
                <a
                  href="tel:0371-12345678"
                  class="px-8 py-3 bg-white text-gray-700 rounded-lg hover:shadow-lg transition-all font-medium flex items-center gap-2 border border-gray-200"
                >
                  <svg class="w-5 h-5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M3 5a2 2 0 012-2h3.28a1 1 0 01.948.684l1.498 4.493a1 1 0 01-.502 1.21l-2.257 1.13a11.042 11.042 0 005.516 5.516l1.13-2.257a1 1 0 011.21-.502l4.493 1.498a1 1 0 01.684.949V19a2 2 0 01-2 2h-1C9.716 21 3 14.284 3 6V5z" />
                  </svg>
                  电话咨询
                </a>
              </div>
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

// 业务数据
const business = ref(null)
const loading = ref(true)

// 加载业务详情
const loadBusinessDetail = async () => {
  loading.value = true
  try {
    const id = route.params.id
    
    // TODO: 替换为实际的API调用
    // const response = await fetch(`/api/config/business-scope/${id}`)
    // business.value = await response.json()
    
    // 模拟API调用
    await new Promise(resolve => setTimeout(resolve, 500))
    
    // 模拟数据 - 对应数据库字段
    business.value = {
      id: id,
      name: '工程造价咨询', // name
      description: '为建设项目提供全过程造价管理服务', // description
      image: 'https://images.unsplash.com/photo-1454165804606-c3d57bc86b40?w=1200&h=600&fit=crop', // 从image_id获取
      features: [ // features (JSON数组)
        '拥有专业的造价工程师团队，具备丰富的项目经验',
        '采用先进的造价管理软件，提高工作效率和准确性',
        '提供全过程造价咨询服务，从项目立项到竣工结算',
        '严格的质量控制体系，确保造价成果的准确性和可靠性',
        '快速响应客户需求，提供及时的技术支持',
        '合理的收费标准，为客户节约成本'
      ],
      detailContent: `
        <h3>服务范围</h3>
        <p>工程造价咨询是指对建设项目从立项到竣工结算全过程的造价进行确定、控制和管理的专业服务。我们的服务范围包括：</p>
        <ul>
          <li>投资估算编制与审核</li>
          <li>设计概算编制与审核</li>
          <li>施工图预算编制与审核</li>
          <li>工程量清单编制</li>
          <li>招标控制价编制</li>
          <li>投标报价编制</li>
          <li>工程结算审核</li>
          <li>竣工决算审核</li>
          <li>工程造价鉴定</li>
          <li>工程造价咨询</li>
        </ul>
        
        <h3>服务优势</h3>
        <p><strong>专业团队：</strong>我们拥有一支由注册造价工程师、高级工程师组成的专业团队，具备丰富的项目经验和扎实的专业知识。</p>
        <p><strong>先进技术：</strong>采用先进的造价管理软件和BIM技术，提高工作效率和准确性，为客户提供更优质的服务。</p>
        <p><strong>质量保证：</strong>建立了完善的质量管理体系，严格执行三级审核制度，确保造价成果的准确性和可靠性。</p>
        <p><strong>全程服务：</strong>提供从项目立项到竣工结算的全过程造价咨询服务，帮助客户有效控制投资。</p>
        
        <h3>服务承诺</h3>
        <p>我们承诺为客户提供专业、高效、优质的造价咨询服务，严格遵守职业道德和行业规范，维护客户合法权益，为客户创造价值。</p>
      ` // content (富文本)
    }
  } catch (error) {
    console.error('加载业务详情失败:', error)
    business.value = null
  } finally {
    loading.value = false
  }
}

// 组件挂载时加载数据
onMounted(() => {
  loadBusinessDetail()
})
</script>

<style scoped>
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

.prose ul {
  list-style-type: disc;
  padding-left: 2rem;
  margin-bottom: 1rem;
}

.prose li {
  margin-bottom: 0.5rem;
  line-height: 1.75;
}
</style>