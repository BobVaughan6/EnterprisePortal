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
          <router-link to="/announcements" class="hover:text-hailong-primary transition-colors">公告信息</router-link>
          <svg class="w-4 h-4 mx-2" fill="none" stroke="currentColor" viewBox="0 0 24 24">
            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M9 5l7 7-7 7" />
          </svg>
          <span class="text-gray-900">公告详情</span>
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

        <div v-else-if="!announcement" class="text-center py-20">
          <svg class="w-20 h-20 mx-auto text-gray-300 mb-4" fill="none" stroke="currentColor" viewBox="0 0 24 24">
            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M9 12h6m-6 4h6m2 5H7a2 2 0 01-2-2V5a2 2 0 012-2h5.586a1 1 0 01.707.293l5.414 5.414a1 1 0 01.293.707V19a2 2 0 01-2 2z" />
          </svg>
          <p class="text-gray-500 mb-4">公告不存在或已被删除</p>
          <router-link to="/announcements" class="text-hailong-primary hover:underline">返回列表</router-link>
        </div>

        <div v-else class="max-w-5xl mx-auto">
          <!-- 公告头部 -->
          <div class="bg-white rounded-xl shadow-sm p-8 mb-6">
            <!-- 标题 -->
            <h1 class="text-3xl font-bold text-gray-900 mb-6 leading-tight">
              {{ announcement.title }}
            </h1>

            <!-- 元信息 -->
            <div class="flex flex-wrap items-center gap-6 pb-6 border-b border-gray-200">
              <div class="flex items-center gap-2 text-sm text-gray-600">
                <svg class="w-5 h-5 text-hailong-primary" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                  <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M7 7h.01M7 3h5c.512 0 1.024.195 1.414.586l7 7a2 2 0 010 2.828l-7 7a2 2 0 01-2.828 0l-7-7A1.994 1.994 0 013 12V7a4 4 0 014-4z" />
                </svg>
                <span class="font-medium">类型：</span>
                <span :class="getTypeStyle(announcement.type)" class="px-3 py-1 rounded-full text-xs font-medium">
                  {{ announcement.type }}
                </span>
              </div>

              <div class="flex items-center gap-2 text-sm text-gray-600">
                <svg class="w-5 h-5 text-hailong-primary" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                  <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M8 7V3m8 4V3m-9 8h10M5 21h14a2 2 0 002-2V7a2 2 0 00-2-2H5a2 2 0 00-2 2v12a2 2 0 002 2z" />
                </svg>
                <span class="font-medium">发布时间：</span>
                <span>{{ announcement.publishDate }}</span>
              </div>

              <div class="flex items-center gap-2 text-sm text-gray-600">
                <svg class="w-5 h-5 text-hailong-primary" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                  <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M15 12a3 3 0 11-6 0 3 3 0 016 0z" />
                  <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M2.458 12C3.732 7.943 7.523 5 12 5c4.478 0 8.268 2.943 9.542 7-1.274 4.057-5.064 7-9.542 7-4.477 0-8.268-2.943-9.542-7z" />
                </svg>
                <span class="font-medium">浏览：</span>
                <span>{{ announcement.views || 0 }} 次</span>
              </div>
            </div>

            <!-- 基本信息 -->
            <div class="grid grid-cols-1 md:grid-cols-2 gap-6 mt-6">
              <div v-if="announcement.bidder" class="flex items-start gap-3">
                <div class="flex-shrink-0 w-10 h-10 bg-hailong-primary/10 rounded-lg flex items-center justify-center">
                  <svg class="w-5 h-5 text-hailong-primary" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M19 21V5a2 2 0 00-2-2H7a2 2 0 00-2 2v16m14 0h2m-2 0h-5m-9 0H3m2 0h5M9 7h1m-1 4h1m4-4h1m-1 4h1m-5 10v-5a1 1 0 011-1h2a1 1 0 011 1v5m-4 0h4" />
                  </svg>
                </div>
                <div class="flex-1">
                  <div class="text-sm text-gray-500 mb-1">招标人</div>
                  <div class="text-gray-900 font-medium">{{ announcement.bidder }}</div>
                </div>
              </div>

              <div v-if="announcement.winner" class="flex items-start gap-3">
                <div class="flex-shrink-0 w-10 h-10 bg-hailong-secondary/10 rounded-lg flex items-center justify-center">
                  <svg class="w-5 h-5 text-hailong-secondary" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M9 12l2 2 4-4M7.835 4.697a3.42 3.42 0 001.946-.806 3.42 3.42 0 014.438 0 3.42 3.42 0 001.946.806 3.42 3.42 0 013.138 3.138 3.42 3.42 0 00.806 1.946 3.42 3.42 0 010 4.438 3.42 3.42 0 00-.806 1.946 3.42 3.42 0 01-3.138 3.138 3.42 3.42 0 00-1.946.806 3.42 3.42 0 01-4.438 0 3.42 3.42 0 00-1.946-.806 3.42 3.42 0 01-3.138-3.138 3.42 3.42 0 00-.806-1.946 3.42 3.42 0 010-4.438 3.42 3.42 0 00.806-1.946 3.42 3.42 0 013.138-3.138z" />
                  </svg>
                </div>
                <div class="flex-1">
                  <div class="text-sm text-gray-500 mb-1">中标人</div>
                  <div class="text-gray-900 font-medium">{{ announcement.winner }}</div>
                </div>
              </div>

              <div v-if="announcement.region" class="flex items-start gap-3">
                <div class="flex-shrink-0 w-10 h-10 bg-hailong-cyan/10 rounded-lg flex items-center justify-center">
                  <svg class="w-5 h-5 text-hailong-cyan" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M17.657 16.657L13.414 20.9a1.998 1.998 0 01-2.827 0l-4.244-4.243a8 8 0 1111.314 0z" />
                    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M15 11a3 3 0 11-6 0 3 3 0 016 0z" />
                  </svg>
                </div>
                <div class="flex-1">
                  <div class="text-sm text-gray-500 mb-1">项目区域</div>
                  <div class="text-gray-900 font-medium">{{ announcement.region }}</div>
                </div>
              </div>

              <div v-if="announcement.budget" class="flex items-start gap-3">
                <div class="flex-shrink-0 w-10 h-10 bg-green-100 rounded-lg flex items-center justify-center">
                  <svg class="w-5 h-5 text-green-600" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M12 8c-1.657 0-3 .895-3 2s1.343 2 3 2 3 .895 3 2-1.343 2-3 2m0-8c1.11 0 2.08.402 2.599 1M12 8V7m0 1v8m0 0v1m0-1c-1.11 0-2.08-.402-2.599-1M21 12a9 9 0 11-18 0 9 9 0 0118 0z" />
                  </svg>
                </div>
                <div class="flex-1">
                  <div class="text-sm text-gray-500 mb-1">预算金额</div>
                  <div class="text-gray-900 font-medium">{{ announcement.budget }}</div>
                </div>
              </div>
            </div>
          </div>

          <!-- 公告内容 -->
          <div class="bg-white rounded-xl shadow-sm p-8 mb-6">
            <h2 class="text-xl font-bold text-gray-900 mb-6 pb-4 border-b border-gray-200">
              公告内容
            </h2>
            <div class="prose prose-lg max-w-none text-gray-700 leading-relaxed" v-html="announcement.content"></div>
          </div>

          <!-- 附件下载 -->
          <div v-if="announcement.attachments && announcement.attachments.length > 0" class="bg-white rounded-xl shadow-sm p-8 mb-6">
            <h2 class="text-xl font-bold text-gray-900 mb-6 pb-4 border-b border-gray-200">
              附件下载
            </h2>
            <div class="space-y-3">
              <a
                v-for="attachment in announcement.attachments"
                :key="attachment.id"
                :href="attachment.fileUrl"
                target="_blank"
                class="flex items-center justify-between p-4 bg-gray-50 rounded-lg hover:bg-gray-100 transition-colors group"
              >
                <div class="flex items-center gap-3">
                  <div class="w-10 h-10 bg-hailong-primary/10 rounded-lg flex items-center justify-center">
                    <svg class="w-5 h-5 text-hailong-primary" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                      <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M7 21h10a2 2 0 002-2V9.414a1 1 0 00-.293-.707l-5.414-5.414A1 1 0 0012.586 3H7a2 2 0 00-2 2v14a2 2 0 002 2z" />
                    </svg>
                  </div>
                  <div>
                    <div class="text-gray-900 font-medium group-hover:text-hailong-primary transition-colors">
                      {{ attachment.fileName }}
                    </div>
                    <div class="text-sm text-gray-500">
                      {{ formatFileSize(attachment.fileSize) }}
                    </div>
                  </div>
                </div>
                <svg class="w-5 h-5 text-gray-400 group-hover:text-hailong-primary transition-colors" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                  <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M4 16v1a3 3 0 003 3h10a3 3 0 003-3v-1m-4-4l-4 4m0 0l-4-4m4 4V4" />
                </svg>
              </a>
            </div>
          </div>

          <!-- 操作按钮 -->
          <div class="flex items-center justify-between">
            <router-link
              to="/announcements"
              class="px-6 py-3 bg-gray-100 text-gray-700 rounded-lg hover:bg-gray-200 transition-all font-medium flex items-center gap-2"
            >
              <svg class="w-5 h-5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M10 19l-7-7m0 0l7-7m-7 7h18" />
              </svg>
              返回列表
            </router-link>

            <button
              @click="handlePrint"
              class="px-6 py-3 bg-gradient-to-r from-hailong-primary to-hailong-secondary text-white rounded-lg hover:shadow-lg transition-all font-medium flex items-center gap-2"
            >
              <svg class="w-5 h-5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M17 17h2a2 2 0 002-2v-4a2 2 0 00-2-2H5a2 2 0 00-2 2v4a2 2 0 002 2h2m2 4h6a2 2 0 002-2v-4a2 2 0 00-2-2H9a2 2 0 00-2 2v4a2 2 0 002 2zm8-12V5a2 2 0 00-2-2H9a2 2 0 00-2 2v4h10z" />
              </svg>
              打印公告
            </button>
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

// 公告数据
const announcement = ref(null)
const loading = ref(true)

// 获取类型样式
const getTypeStyle = (type) => {
  switch (type) {
    case '招标公告':
    case '采购公告':
      return 'bg-hailong-primary/10 text-hailong-primary border border-hailong-primary/20'
    case '结果公告':
      return 'bg-hailong-secondary/10 text-hailong-secondary border border-hailong-secondary/20'
    case '更正公告':
      return 'bg-hailong-cyan/10 text-hailong-cyan border border-hailong-cyan/20'
    default:
      return 'bg-gray-100 text-gray-800 border border-gray-200'
  }
}

// 格式化文件大小
const formatFileSize = (bytes) => {
  if (bytes === 0) return '0 B'
  const k = 1024
  const sizes = ['B', 'KB', 'MB', 'GB']
  const i = Math.floor(Math.log(bytes) / Math.log(k))
  return Math.round(bytes / Math.pow(k, i) * 100) / 100 + ' ' + sizes[i]
}

// 打印公告
const handlePrint = () => {
  window.print()
}

// 加载公告详情
const loadAnnouncementDetail = async () => {
  loading.value = true
  try {
    const id = route.params.id
    
    // 模拟API调用
    await new Promise(resolve => setTimeout(resolve, 500))
    
    // 模拟数据
    announcement.value = {
      id: id,
      title: '某市教育局办公设备采购项目招标公告',
      type: '招标公告',
      businessType: 'GOV_PROCUREMENT',
      bidder: '某市教育局',
      winner: null,
      region: '郑州市',
      budget: '¥850万',
      publishDate: '2025-11-15',
      views: 1234,
      content: `
        <h3>一、项目基本情况</h3>
        <p>项目编号：HNZC-2025-001</p>
        <p>项目名称：某市教育局办公设备采购项目</p>
        <p>预算金额：850万元</p>
        <p>最高限价：850万元</p>
        <p>采购需求：本项目采购办公设备一批，包括台式电脑、笔记本电脑、打印机、复印机等，具体内容详见招标文件。</p>
        
        <h3>二、申请人的资格要求</h3>
        <p>1. 满足《中华人民共和国政府采购法》第二十二条规定；</p>
        <p>2. 落实政府采购政策需满足的资格要求：本项目专门面向中小企业采购；</p>
        <p>3. 本项目的特定资格要求：供应商须具有有效的营业执照、税务登记证、组织机构代码证或三证合一的营业执照。</p>
        
        <h3>三、获取招标文件</h3>
        <p>时间：2025年11月15日至2025年11月21日，每天上午8:30至11:30，下午14:30至17:30（北京时间，法定节假日除外）</p>
        <p>地点：河南省郑州市金水区经三路15号</p>
        <p>方式：现场获取</p>
        <p>售价：300元/份，售后不退</p>
        
        <h3>四、提交投标文件截止时间、开标时间和地点</h3>
        <p>提交投标文件截止时间：2025年12月06日 09:00（北京时间）</p>
        <p>开标时间：2025年12月06日 09:00（北京时间）</p>
        <p>地点：河南省郑州市金水区经三路15号开标室</p>
        
        <h3>五、公告期限</h3>
        <p>自本公告发布之日起5个工作日。</p>
        
        <h3>六、其他补充事宜</h3>
        <p>1. 投标人须按招标文件要求提供完整的投标文件；</p>
        <p>2. 投标人须按时参加开标会议；</p>
        <p>3. 本项目不接受联合体投标。</p>
      `,
      attachments: [
        {
          id: 1,
          fileName: '招标文件.pdf',
          fileUrl: '#',
          fileSize: 2048576,
          fileType: 'pdf'
        },
        {
          id: 2,
          fileName: '技术规格书.doc',
          fileUrl: '#',
          fileSize: 512000,
          fileType: 'doc'
        }
      ]
    }
  } catch (error) {
    console.error('加载公告详情失败:', error)
    announcement.value = null
  } finally {
    loading.value = false
  }
}

// 组件挂载时加载数据
onMounted(() => {
  loadAnnouncementDetail()
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

@media print {
  header, footer, .no-print {
    display: none !important;
  }
}
</style>