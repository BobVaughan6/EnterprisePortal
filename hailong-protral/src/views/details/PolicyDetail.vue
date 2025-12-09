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
          <router-link to="/policies" class="hover:text-hailong-primary transition-colors">政策法规</router-link>
          <svg class="w-4 h-4 mx-2" fill="none" stroke="currentColor" viewBox="0 0 24 24">
            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M9 5l7 7-7 7" />
          </svg>
          <span class="text-gray-900">法规详情</span>
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

        <div v-else-if="!policy" class="text-center py-20">
          <svg class="w-20 h-20 mx-auto text-gray-300 mb-4" fill="none" stroke="currentColor" viewBox="0 0 24 24">
            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M9 12h6m-6 4h6m2 5H7a2 2 0 01-2-2V5a2 2 0 012-2h5.586a1 1 0 01.707.293l5.414 5.414a1 1 0 01.293.707V19a2 2 0 01-2 2z" />
          </svg>
          <p class="text-gray-500 mb-4">政策法规不存在或已被删除</p>
          <router-link to="/policies" class="text-hailong-primary hover:underline">返回列表</router-link>
        </div>

        <div v-else class="max-w-5xl mx-auto">
          <!-- 法规头部 -->
          <div class="bg-white rounded-xl shadow-sm p-8 mb-6">
            <!-- 置顶标识 -->
            <div v-if="policy.isTop" class="mb-4">
              <span class="inline-flex items-center gap-1 px-3 py-1 bg-red-500 text-white text-sm rounded-full font-medium">
                <svg class="w-4 h-4" fill="currentColor" viewBox="0 0 20 20">
                  <path d="M2 6a2 2 0 012-2h6a2 2 0 012 2v8a2 2 0 01-2 2H4a2 2 0 01-2-2V6zM14.553 7.106A1 1 0 0014 8v4a1 1 0 00.553.894l2 1A1 1 0 0018 13V7a1 1 0 00-1.447-.894l-2 1z" />
                </svg>
                置顶
              </span>
            </div>

            <!-- 标题 -->
            <h1 class="text-3xl font-bold text-gray-900 mb-6 leading-tight">
              {{ policy.title }}
            </h1>

            <!-- 元信息 -->
            <div class="flex flex-wrap items-center gap-6 pb-6 border-b border-gray-200">
              <div class="flex items-center gap-2 text-sm text-gray-600">
                <svg class="w-5 h-5 text-hailong-primary" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                  <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M7 7h.01M7 3h5c.512 0 1.024.195 1.414.586l7 7a2 2 0 010 2.828l-7 7a2 2 0 01-2.828 0l-7-7A1.994 1.994 0 013 12V7a4 4 0 014-4z" />
                </svg>
                <span class="font-medium">分类：</span>
                <span class="px-3 py-1 rounded-full text-xs font-medium bg-blue-100 text-blue-700 border border-blue-200">
                  {{ policy.category }}
                </span>
              </div>

              <div v-if="policy.source" class="flex items-center gap-2 text-sm text-gray-600">
                <svg class="w-5 h-5 text-hailong-primary" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                  <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M19 21V5a2 2 0 00-2-2H7a2 2 0 00-2 2v16m14 0h2m-2 0h-5m-9 0H3m2 0h5M9 7h1m-1 4h1m4-4h1m-1 4h1m-5 10v-5a1 1 0 011-1h2a1 1 0 011 1v5m-4 0h4" />
                </svg>
                <span class="font-medium">来源：</span>
                <span>{{ policy.source }}</span>
              </div>

              <div class="flex items-center gap-2 text-sm text-gray-600">
                <svg class="w-5 h-5 text-hailong-primary" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                  <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M8 7V3m8 4V3m-9 8h10M5 21h14a2 2 0 002-2V7a2 2 0 00-2-2H5a2 2 0 00-2 2v12a2 2 0 002 2z" />
                </svg>
                <span class="font-medium">发布时间：</span>
                <span>{{ policy.publishDate }}</span>
              </div>

              <div v-if="policy.effectiveDate" class="flex items-center gap-2 text-sm text-gray-600">
                <svg class="w-5 h-5 text-green-600" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                  <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M9 12l2 2 4-4m6 2a9 9 0 11-18 0 9 9 0 0118 0z" />
                </svg>
                <span class="font-medium">生效时间：</span>
                <span>{{ policy.effectiveDate }}</span>
              </div>

              <div class="flex items-center gap-2 text-sm text-gray-600">
                <svg class="w-5 h-5 text-hailong-primary" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                  <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M15 12a3 3 0 11-6 0 3 3 0 016 0z" />
                  <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M2.458 12C3.732 7.943 7.523 5 12 5c4.478 0 8.268 2.943 9.542 7-1.274 4.057-5.064 7-9.542 7-4.477 0-8.268-2.943-9.542-7z" />
                </svg>
                <span class="font-medium">浏览：</span>
                <span>{{ policy.views || 0 }} 次</span>
              </div>

              <div v-if="policy.downloads" class="flex items-center gap-2 text-sm text-gray-600">
                <svg class="w-5 h-5 text-hailong-primary" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                  <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M4 16v1a3 3 0 003 3h10a3 3 0 003-3v-1m-4-4l-4 4m0 0l-4-4m4 4V4" />
                </svg>
                <span class="font-medium">下载：</span>
                <span>{{ policy.downloads }} 次</span>
              </div>
            </div>

            <!-- 摘要 -->
            <div v-if="policy.summary" class="mt-6 p-4 bg-gradient-to-r from-blue-50 to-cyan-50 rounded-lg border-l-4 border-blue-500">
              <div class="flex items-start gap-3">
                <svg class="w-5 h-5 text-blue-600 flex-shrink-0 mt-0.5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                  <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M13 16h-1v-4h-1m1-4h.01M21 12a9 9 0 11-18 0 9 9 0 0118 0z" />
                </svg>
                <div>
                  <h3 class="font-bold text-gray-900 mb-2">内容摘要</h3>
                  <p class="text-gray-700 leading-relaxed">{{ policy.summary }}</p>
                </div>
              </div>
            </div>
          </div>

          <!-- 法规内容 -->
          <div class="bg-white rounded-xl shadow-sm p-8 mb-6">
            <h2 class="text-2xl font-bold text-gray-900 mb-6 pb-4 border-b border-gray-200 flex items-center gap-3">
              <div class="w-10 h-10 bg-hailong-primary/10 rounded-lg flex items-center justify-center">
                <svg class="w-6 h-6 text-hailong-primary" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                  <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M9 12h6m-6 4h6m2 5H7a2 2 0 01-2-2V5a2 2 0 012-2h5.586a1 1 0 01.707.293l5.414 5.414a1 1 0 01.293.707V19a2 2 0 01-2 2z" />
                </svg>
              </div>
              法规正文
            </h2>
            <div class="prose prose-lg max-w-none text-gray-700 leading-relaxed" v-html="policy.content"></div>
          </div>

          <!-- 文件下载 -->
          <div v-if="policy.fileUrl" class="bg-gradient-to-r from-hailong-primary/5 to-hailong-secondary/5 rounded-xl p-8 mb-6">
            <h2 class="text-xl font-bold text-gray-900 mb-4 flex items-center gap-2">
              <svg class="w-6 h-6 text-hailong-primary" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M12 10v6m0 0l-3-3m3 3l3-3m2 8H7a2 2 0 01-2-2V5a2 2 0 012-2h5.586a1 1 0 01.707.293l5.414 5.414a1 1 0 01.293.707V19a2 2 0 01-2 2z" />
              </svg>
              文件下载
            </h2>
            <a
              :href="policy.fileUrl"
              target="_blank"
              class="flex items-center justify-between p-6 bg-white rounded-lg hover:shadow-lg transition-all group"
            >
              <div class="flex items-center gap-4">
                <div class="w-14 h-14 bg-hailong-primary/10 rounded-lg flex items-center justify-center">
                  <svg class="w-7 h-7 text-hailong-primary" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M7 21h10a2 2 0 002-2V9.414a1 1 0 00-.293-.707l-5.414-5.414A1 1 0 0012.586 3H7a2 2 0 00-2 2v14a2 2 0 002 2z" />
                  </svg>
                </div>
                <div>
                  <div class="text-lg font-bold text-gray-900 group-hover:text-hailong-primary transition-colors">
                    {{ policy.title }}.pdf
                  </div>
                  <div class="text-sm text-gray-500 mt-1">
                    点击下载完整文件
                  </div>
                </div>
              </div>
              <svg class="w-6 h-6 text-gray-400 group-hover:text-hailong-primary transition-colors" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M4 16v1a3 3 0 003 3h10a3 3 0 003-3v-1m-4-4l-4 4m0 0l-4-4m4 4V4" />
              </svg>
            </a>
          </div>

          <!-- 标签 -->
          <div v-if="policy.tags && policy.tags.length > 0" class="bg-white rounded-xl shadow-sm p-6 mb-6">
            <div class="flex items-center gap-3 flex-wrap">
              <span class="text-sm font-medium text-gray-600">相关标签：</span>
              <span
                v-for="tag in policy.tags"
                :key="tag"
                class="px-3 py-1 bg-gray-100 text-gray-700 rounded-full text-sm hover:bg-hailong-primary/10 hover:text-hailong-primary transition-colors cursor-pointer"
              >
                # {{ tag }}
              </span>
            </div>
          </div>

          <!-- 相关法规 -->
          <div v-if="relatedPolicies.length > 0" class="bg-white rounded-xl shadow-sm p-8 mb-6">
            <h2 class="text-xl font-bold text-gray-900 mb-6 pb-4 border-b border-gray-200">
              相关法规
            </h2>
            <div class="space-y-4">
              <router-link
                v-for="related in relatedPolicies"
                :key="related.id"
                :to="`/detail/policy/${related.id}`"
                class="flex items-start gap-4 p-4 rounded-lg hover:bg-gray-50 transition-colors group"
              >
                <div class="flex-shrink-0 w-2 h-2 bg-hailong-primary rounded-full mt-2"></div>
                <div class="flex-1">
                  <h3 class="text-gray-900 font-medium group-hover:text-hailong-primary transition-colors line-clamp-2">
                    {{ related.title }}
                  </h3>
                  <div class="flex items-center gap-4 mt-2 text-xs text-gray-500">
                    <span>{{ related.category }}</span>
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
              to="/policies"
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

// 政策法规数据
const policy = ref(null)
const loading = ref(true)
const relatedPolicies = ref([])

// 分享
const handleShare = () => {
  if (navigator.share) {
    navigator.share({
      title: policy.value.title,
      text: policy.value.summary,
      url: window.location.href
    }).catch(err => console.log('分享失败:', err))
  } else {
    navigator.clipboard.writeText(window.location.href).then(() => {
      alert('链接已复制到剪贴板')
    })
  }
}

// 打印
const handlePrint = () => {
  window.print()
}

// 加载政策法规详情
const loadPolicyDetail = async () => {
  loading.value = true
  try {
    const id = route.params.id
    
    // 模拟API调用
    await new Promise(resolve => setTimeout(resolve, 500))
    
    // 模拟数据
    policy.value = {
      id: id,
      title: '中华人民共和国政府采购法（2024年修订）',
      category: '法律法规',
      source: '全国人民代表大会常务委员会',
      publishDate: '2024-03-15',
      effectiveDate: '2024-07-01',
      views: 2856,
      downloads: 1234,
      isTop: true,
      summary: '为了规范政府采购行为，提高政府采购资金的使用效益，维护国家利益和社会公共利益，保护政府采购当事人的合法权益，促进廉政建设，制定本法。',
      content: `
        <h3>第一章 总则</h3>
        <p><strong>第一条</strong> 为了规范政府采购行为，提高政府采购资金的使用效益，维护国家利益和社会公共利益，保护政府采购当事人的合法权益，促进廉政建设，制定本法。</p>
        
        <p><strong>第二条</strong> 在中华人民共和国境内进行的政府采购适用本法。</p>
        <p>本法所称政府采购，是指各级国家机关、事业单位和团体组织，使用财政性资金采购依法制定的集中采购目录以内的或者采购限额标准以上的货物、工程和服务的行为。</p>
        
        <p><strong>第三条</strong> 政府采购应当遵循公开透明原则、公平竞争原则、公正原则和诚实信用原则。</p>
        
        <h3>第二章 政府采购当事人</h3>
        <p><strong>第四条</strong> 政府采购当事人是指在政府采购活动中享有权利和承担义务的各类主体，包括采购人、供应商和采购代理机构等。</p>
        
        <p><strong>第五条</strong> 采购人是指依法进行政府采购的国家机关、事业单位、团体组织。</p>
        
        <p><strong>第六条</strong> 供应商是指向采购人提供货物、工程或者服务的法人、其他组织或者自然人。</p>
        
        <h3>第三章 政府采购方式</h3>
        <p><strong>第七条</strong> 政府采购采用以下方式：</p>
        <ul>
          <li>（一）公开招标；</li>
          <li>（二）邀请招标；</li>
          <li>（三）竞争性谈判；</li>
          <li>（四）单一来源采购；</li>
          <li>（五）询价；</li>
          <li>（六）国务院政府采购监督管理部门认定的其他采购方式。</li>
        </ul>
        
        <p><strong>第八条</strong> 公开招标应作为政府采购的主要采购方式。</p>
        
        <h3>第四章 政府采购程序</h3>
        <p><strong>第九条</strong> 采购人应当按照本法规定的采购方式和采购程序开展政府采购活动。</p>
        
        <p><strong>第十条</strong> 政府采购应当采购本国货物、工程和服务。但有下列情形之一的除外：</p>
        <ul>
          <li>（一）需要采购的货物、工程或者服务在中国境内无法获取或者无法以合理的商业条件获取的；</li>
          <li>（二）为在中国境外使用而进行采购的；</li>
          <li>（三）其他法律、行政法规另有规定的。</li>
        </ul>
        
        <h3>第五章 政府采购合同</h3>
        <p><strong>第十一条</strong> 采购人与中标、成交供应商应当在中标、成交通知书发出之日起三十日内，按照采购文件确定的事项签订政府采购合同。</p>
        
        <p><strong>第十二条</strong> 政府采购合同应当采用书面形式。</p>
        
        <h3>第六章 监督检查</h3>
        <p><strong>第十三条</strong> 各级人民政府财政部门是负责政府采购监督管理的部门，依法履行对政府采购活动的监督管理职责。</p>
        
        <p><strong>第十四条</strong> 政府采购监督管理部门应当加强对政府采购活动及集中采购机构的监督检查。</p>
        
        <h3>第七章 法律责任</h3>
        <p><strong>第十五条</strong> 采购人、采购代理机构违反本法规定的，责令限期改正，给予警告，可以并处罚款，对直接负责的主管人员和其他直接责任人员，由其行政主管部门或者有关机关给予处分，并予通报。</p>
        
        <h3>第八章 附则</h3>
        <p><strong>第十六条</strong> 本法自2024年7月1日起施行。</p>
      `,
      fileUrl: '#',
      tags: ['政府采购', '招标投标', '法律法规', '2024修订']
    }

    // 加载相关法规
    relatedPolicies.value = [
      {
        id: 2,
        title: '中华人民共和国招标投标法实施条例',
        category: '行政法规',
        publishDate: '2024-01-10',
        views: 1856
      },
      {
        id: 3,
        title: '政府采购货物和服务招标投标管理办法',
        category: '部门规章',
        publishDate: '2024-02-15',
        views: 1523
      },
      {
        id: 4,
        title: '工程建设项目招标范围和规模标准规定',
        category: '部门规章',
        publishDate: '2023-12-20',
        views: 1342
      }
    ]
  } catch (error) {
    console.error('加载政策法规详情失败:', error)
    policy.value = null
  } finally {
    loading.value = false
  }
}

// 组件挂载时加载数据
onMounted(() => {
  loadPolicyDetail()
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

.prose ul {
  list-style-type: none;
  padding-left: 0;
  margin-bottom: 1rem;
}

.prose li {
  margin-bottom: 0.5rem;
  line-height: 1.75;
  padding-left: 1.5rem;
  position: relative;
}

.prose li::before {
  content: "•";
  position: absolute;
  left: 0.5rem;
  color: #3b82f6;
}

.prose strong {
  color: #1f2937;
  font-weight: 600;
}

@media print {
  header, footer, .no-print {
    display: none !important;
  }
}
</style>