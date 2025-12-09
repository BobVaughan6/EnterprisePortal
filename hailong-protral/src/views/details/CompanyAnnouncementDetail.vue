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
          <router-link to="/company-announcements" class="hover:text-hailong-primary transition-colors">新闻中心</router-link>
          <svg class="w-4 h-4 mx-2" fill="none" stroke="currentColor" viewBox="0 0 24 24">
            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M9 5l7 7-7 7" />
          </svg>
          <span class="text-gray-900">新闻详情</span>
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

        <div v-else-if="!item" class="text-center py-20">
          <svg class="w-20 h-20 mx-auto text-gray-300 mb-4" fill="none" stroke="currentColor" viewBox="0 0 24 24">
            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M9 12h6m-6 4h6m2 5H7a2 2 0 01-2-2V5a2 2 0 012-2h5.586a1 1 0 01.707.293l5.414 5.414a1 1 0 01.293.707V19a2 2 0 01-2 2z" />
          </svg>
          <p class="text-gray-500 mb-4">新闻不存在或已被删除</p>
          <router-link to="/company-announcements" class="text-hailong-primary hover:underline">返回列表</router-link>
        </div>

        <div v-else class="max-w-5xl mx-auto">
          <!-- 文章头部 -->
          <div class="bg-white rounded-xl shadow-sm p-8 mb-6">
            <!-- 置顶标签 -->
            <div v-if="item.isTop" class="mb-4">
              <span class="inline-flex items-center gap-1 px-3 py-1 bg-red-500 text-white text-sm rounded-full font-medium">
                <svg class="w-4 h-4" fill="currentColor" viewBox="0 0 20 20">
                  <path d="M2 6a2 2 0 012-2h6a2 2 0 012 2v8a2 2 0 01-2 2H4a2 2 0 01-2-2V6zM14.553 7.106A1 1 0 0014 8v4a1 1 0 00.553.894l2 1A1 1 0 0018 13V7a1 1 0 00-1.447-.894l-2 1z" />
                </svg>
                置顶
              </span>
            </div>

            <!-- 标题 -->
            <h1 class="text-3xl font-bold text-gray-900 mb-6 leading-tight">
              {{ item.title }}
            </h1>

            <!-- 元信息 -->
            <div class="flex flex-wrap items-center gap-6 pb-6 border-b border-gray-200">
              <div class="flex items-center gap-2 text-sm text-gray-600">
                <svg class="w-5 h-5 text-hailong-primary" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                  <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M7 7h.01M7 3h5c.512 0 1.024.195 1.414.586l7 7a2 2 0 010 2.828l-7 7a2 2 0 01-2.828 0l-7-7A1.994 1.994 0 013 12V7a4 4 0 014-4z" />
                </svg>
                <span class="font-medium">分类：</span>
                <span :class="getTypeStyle(item.type)" class="px-3 py-1 rounded-full text-xs font-medium">
                  {{ item.type }}
                </span>
              </div>

              <div class="flex items-center gap-2 text-sm text-gray-600">
                <svg class="w-5 h-5 text-hailong-primary" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                  <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M8 7V3m8 4V3m-9 8h10M5 21h14a2 2 0 002-2V7a2 2 0 00-2-2H5a2 2 0 00-2 2v12a2 2 0 002 2z" />
                </svg>
                <span class="font-medium">发布时间：</span>
                <span>{{ item.publishDate }}</span>
              </div>

              <div v-if="item.author" class="flex items-center gap-2 text-sm text-gray-600">
                <svg class="w-5 h-5 text-hailong-primary" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                  <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M16 7a4 4 0 11-8 0 4 4 0 018 0zM12 14a7 7 0 00-7 7h14a7 7 0 00-7-7z" />
                </svg>
                <span class="font-medium">作者：</span>
                <span>{{ item.author }}</span>
              </div>

              <div class="flex items-center gap-2 text-sm text-gray-600">
                <svg class="w-5 h-5 text-hailong-primary" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                  <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M15 12a3 3 0 11-6 0 3 3 0 016 0z" />
                  <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M2.458 12C3.732 7.943 7.523 5 12 5c4.478 0 8.268 2.943 9.542 7-1.274 4.057-5.064 7-9.542 7-4.477 0-8.268-2.943-9.542-7z" />
                </svg>
                <span class="font-medium">浏览：</span>
                <span>{{ item.views || 0 }} 次</span>
              </div>
            </div>

            <!-- 摘要 -->
            <div v-if="item.summary" class="mt-6 p-4 bg-gradient-to-r from-hailong-primary/5 to-hailong-secondary/5 rounded-lg border-l-4 border-hailong-primary">
              <div class="flex items-start gap-3">
                <svg class="w-5 h-5 text-hailong-primary flex-shrink-0 mt-0.5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                  <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M13 16h-1v-4h-1m1-4h.01M21 12a9 9 0 11-18 0 9 9 0 0118 0z" />
                </svg>
                <p class="text-gray-700 leading-relaxed">{{ item.summary }}</p>
              </div>
            </div>
          </div>

          <!-- 封面图片 -->
          <div v-if="item.coverImage" class="bg-white rounded-xl shadow-sm overflow-hidden mb-6">
            <img :src="item.coverImage" :alt="item.title" class="w-full h-96 object-cover" />
          </div>

          <!-- 文章内容 -->
          <div class="bg-white rounded-xl shadow-sm p-8 mb-6">
            <div class="prose prose-lg max-w-none text-gray-700 leading-relaxed" v-html="item.content"></div>
          </div>

          <!-- 标签 -->
          <div v-if="item.tags && item.tags.length > 0" class="bg-white rounded-xl shadow-sm p-6 mb-6">
            <div class="flex items-center gap-3 flex-wrap">
              <span class="text-sm font-medium text-gray-600">标签：</span>
              <span
                v-for="tag in item.tags"
                :key="tag"
                class="px-3 py-1 bg-gray-100 text-gray-700 rounded-full text-sm hover:bg-hailong-primary/10 hover:text-hailong-primary transition-colors cursor-pointer"
              >
                # {{ tag }}
              </span>
            </div>
          </div>

          <!-- 相关新闻 -->
          <div v-if="relatedItems.length > 0" class="bg-white rounded-xl shadow-sm p-8 mb-6">
            <h2 class="text-xl font-bold text-gray-900 mb-6 pb-4 border-b border-gray-200">
              相关新闻
            </h2>
            <div class="space-y-4">
              <router-link
                v-for="related in relatedItems"
                :key="related.id"
                :to="`/detail/company-announcement/${related.id}`"
                class="flex items-start gap-4 p-4 rounded-lg hover:bg-gray-50 transition-colors group"
              >
                <div class="flex-shrink-0 w-2 h-2 bg-hailong-primary rounded-full mt-2"></div>
                <div class="flex-1">
                  <h3 class="text-gray-900 font-medium group-hover:text-hailong-primary transition-colors line-clamp-2">
                    {{ related.title }}
                  </h3>
                  <div class="flex items-center gap-4 mt-2 text-xs text-gray-500">
                    <span>{{ related.publishDate }}</span>
                    <span>{{ related.views }} 次浏览</span>
                  </div>
                </div>
              </router-link>
            </div>
          </div>

          <!-- 操作按钮 -->
          <div class="flex items-center justify-between">
            <router-link
              to="/company-announcements"
              class="px-6 py-3 bg-gray-100 text-gray-700 rounded-lg hover:bg-gray-200 transition-all font-medium flex items-center gap-2"
            >
              <svg class="w-5 h-5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M10 19l-7-7m0 0l7-7m-7 7h18" />
              </svg>
              返回列表
            </router-link>

            <div class="flex items-center gap-3">
              <button
                @click="handleShare"
                class="px-6 py-3 bg-white border border-gray-300 text-gray-700 rounded-lg hover:bg-gray-50 transition-all font-medium flex items-center gap-2"
              >
                <svg class="w-5 h-5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                  <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M8.684 13.342C8.886 12.938 9 12.482 9 12c0-.482-.114-.938-.316-1.342m0 2.684a3 3 0 110-2.684m0 2.684l6.632 3.316m-6.632-6l6.632-3.316m0 0a3 3 0 105.367-2.684 3 3 0 00-5.367 2.684zm0 9.316a3 3 0 105.368 2.684 3 3 0 00-5.368-2.684z" />
                </svg>
                分享
              </button>

              <button
                @click="handlePrint"
                class="px-6 py-3 bg-gradient-to-r from-hailong-primary to-hailong-secondary text-white rounded-lg hover:shadow-lg transition-all font-medium flex items-center gap-2"
              >
                <svg class="w-5 h-5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                  <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M17 17h2a2 2 0 002-2v-4a2 2 0 00-2-2H5a2 2 0 00-2 2v4a2 2 0 002 2h2m2 4h6a2 2 0 002-2v-4a2 2 0 00-2-2H9a2 2 0 00-2 2v4a2 2 0 002 2zm8-12V5a2 2 0 00-2-2H9a2 2 0 00-2 2v4h10z" />
                </svg>
                打印
              </button>
            </div>
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

// 新闻数据
const item = ref(null)
const loading = ref(true)
const relatedItems = ref([])

// 获取类型样式
const getTypeStyle = (type) => {
  switch (type) {
    case '企业动态':
      return 'bg-blue-100 text-blue-700 border border-blue-200'
    case '通知公告':
      return 'bg-green-100 text-green-700 border border-green-200'
    case '人事任免':
      return 'bg-purple-100 text-purple-700 border border-purple-200'
    case '制度文件':
      return 'bg-orange-100 text-orange-700 border border-orange-200'
    default:
      return 'bg-gray-100 text-gray-800 border border-gray-200'
  }
}

// 分享
const handleShare = () => {
  if (navigator.share) {
    navigator.share({
      title: item.value.title,
      text: item.value.summary,
      url: window.location.href
    }).catch(err => console.log('分享失败:', err))
  } else {
    // 复制链接到剪贴板
    navigator.clipboard.writeText(window.location.href).then(() => {
      alert('链接已复制到剪贴板')
    })
  }
}

// 打印
const handlePrint = () => {
  window.print()
}

// 加载新闻详情
const loadItemDetail = async () => {
  loading.value = true
  try {
    const id = route.params.id
    
    // 模拟API调用
    await new Promise(resolve => setTimeout(resolve, 500))
    
    // 模拟数据
    item.value = {
      id: id,
      title: '海隆咨询荣获"2024年度优秀咨询企业"称号',
      type: '企业动态',
      summary: '近日，在2024年度工程咨询行业表彰大会上，海隆咨询凭借优异的业绩表现和良好的行业口碑，荣获"2024年度优秀咨询企业"称号。这是对公司多年来坚持专业服务、创新发展的充分肯定。',
      author: '海隆咨询',
      publishDate: '2024-11-20',
      views: 856,
      isTop: true,
      coverImage: 'https://images.unsplash.com/photo-1542744173-8e7e53415bb0?w=1200&h=600&fit=crop',
      content: `
        <p>近日，在2024年度工程咨询行业表彰大会上，海隆咨询凭借优异的业绩表现和良好的行业口碑，荣获"2024年度优秀咨询企业"称号。这是对公司多年来坚持专业服务、创新发展的充分肯定。</p>
        
        <h3>专业铸就品质</h3>
        <p>海隆咨询自成立以来，始终秉承"专业、诚信、创新、共赢"的经营理念，为客户提供全方位、高质量的工程咨询服务。公司拥有一支经验丰富、专业过硬的咨询团队，涵盖工程造价、招标代理、项目管理等多个领域。</p>
        
        <h3>创新驱动发展</h3>
        <p>面对行业发展的新形势，海隆咨询积极拥抱数字化转型，引入先进的信息化管理系统，提升服务效率和质量。同时，公司不断创新服务模式，为客户提供更加精准、高效的解决方案。</p>
        
        <h3>社会责任担当</h3>
        <p>作为行业领先企业，海隆咨询始终牢记社会责任，积极参与公益事业，为行业发展和社会进步贡献力量。公司多次参与重大基础设施项目咨询工作，为地方经济发展提供有力支撑。</p>
        
        <h3>展望未来</h3>
        <p>荣誉既是肯定，更是鞭策。海隆咨询将以此为新的起点，继续深耕工程咨询领域，不断提升专业能力和服务水平，为客户创造更大价值，为行业发展做出更大贡献。</p>
        
        <p>未来，海隆咨询将继续秉承初心，砥砺前行，努力成为工程咨询行业的标杆企业，为推动行业高质量发展贡献更多力量。</p>
      `,
      tags: ['企业荣誉', '行业表彰', '优秀企业', '工程咨询']
    }

    // 加载相关新闻
    relatedItems.value = [
      {
        id: 2,
        title: '公司成功中标某市重大基础设施项目咨询服务',
        publishDate: '2024-11-15',
        views: 623
      },
      {
        id: 3,
        title: '海隆咨询与某高新区签署战略合作协议',
        publishDate: '2024-11-10',
        views: 542
      },
      {
        id: 4,
        title: '公司参加全国工程咨询行业年度峰会',
        publishDate: '2024-11-05',
        views: 489
      }
    ]
  } catch (error) {
    console.error('加载新闻详情失败:', error)
    item.value = null
  } finally {
    loading.value = false
  }
}

// 组件挂载时加载数据
onMounted(() => {
  loadItemDetail()
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

@media print {
  header, footer, .no-print {
    display: none !important;
  }
}
</style>