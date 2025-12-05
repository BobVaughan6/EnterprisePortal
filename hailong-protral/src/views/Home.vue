<template>
  <div class="min-h-screen bg-gradient-to-br from-hailong-dark via-hailong-primary to-hailong-secondary overflow-hidden">
    <Header />
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
        <button
          @click="showContactModal = true"
          class="inline-block px-12 py-5 bg-gradient-to-r from-hailong-primary to-hailong-secondary rounded-full text-white text-lg font-medium hover:shadow-2xl hover:shadow-hailong-primary/50 transition-all transform hover:scale-105">
          立即咨询
        </button>
      </div>
    </div>
    <!-- 统计数据 - 改为浅色渐变 -->
    <!-- <div class="py-20 bg-gradient-to-b from-white to-gray-50">
      <div class="container-wide">
        <div class="grid grid-cols-2 md:grid-cols-4 gap-12 text-center">
          <div v-for="stat in companyInfo.stats" :key="stat.label">
            <div
              class="text-6xl font-bold bg-gradient-to-r from-hailong-primary to-hailong-secondary bg-clip-text text-transparent mb-3">
              {{ stat.value }}</div>
            <div class="text-gray-600 text-sm">{{ stat.label }}</div>
          </div>
        </div>
      </div>
    </div> -->
    <!-- 企业简介 - 改为中性渐变 -->
    <div class="py-24 bg-gradient-to-b from-gray-50 to-white">
      <div class="container-wide">
        <div class="text-center mb-16">
          <h2 class="text-5xl font-bold text-hailong-dark mb-4 font-tech">企业简介</h2>
          <div class="w-24 h-1 bg-gradient-to-r from-hailong-primary to-hailong-secondary mx-auto"></div>
        </div>
        <div class="bg-white rounded-2xl p-8 md:p-12 shadow-lg border border-gray-200">
          <div class="grid grid-cols-1 lg:grid-cols-3 gap-8 items-center">
            <!-- 左侧：企业简介文字 -->
            <div class="lg:col-span-2">
              <p class="text-gray-700 text-lg leading-relaxed">{{ companyProfile.content }}</p>
            </div>
            <!-- 右侧：企业特色标签 -->
            <div class="grid grid-cols-2 gap-4">
              <div v-for="highlight in companyProfile.highlights" :key="highlight"
                class="text-center p-6 bg-gradient-to-br from-hailong-primary/5 to-hailong-secondary/5 rounded-xl hover:shadow-md hover:scale-105 transition-all duration-300 cursor-pointer group">
                <div class="w-12 h-12 bg-hailong-primary/10 rounded-full flex items-center justify-center mx-auto mb-3 group-hover:bg-hailong-primary/20 transition-colors">
                  <svg class="w-6 h-6 text-hailong-primary" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M5 13l4 4L19 7"></path>
                  </svg>
                </div>
                <div class="text-hailong-primary font-semibold text-sm group-hover:text-hailong-secondary transition-colors">{{ highlight }}</div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>

   
    <!-- 公告信息 - 改为浅蓝渐变 -->
    <div class="py-24 bg-gradient-to-b from-white to-blue-50/30">
      <div class="container-wide">
        <div class="text-center mb-16">
          <h2 class="text-5xl font-bold text-hailong-dark mb-4 font-tech">公告信息</h2>
          <div class="w-24 h-1 bg-gradient-to-r from-hailong-primary to-hailong-secondary mx-auto"></div>
        </div>
        <div class="grid grid-cols-1 lg:grid-cols-2 gap-8">
          <div>
            <div class="flex items-center justify-between mb-6">
              <h3 class="text-2xl font-bold text-hailong-dark">政府采购</h3>
              <router-link to="/announcements?tab=GOV_PROCUREMENT"
                class="text-hailong-primary hover:underline text-sm">查看全部 →</router-link>
            </div>
            <div class="space-y-4">
              <div v-for="announcement in govProcurementAnnouncements" :key="announcement.id"
                class="p-6 bg-white rounded-xl hover:shadow-lg transition-all cursor-pointer border-l-4 border-hailong-primary">
                <div class="flex justify-between items-start mb-3">
                  <span class="px-3 py-1 bg-blue-100 text-blue-800 rounded-full text-xs">{{ announcement.type }}</span>
                  <span class="text-xs text-gray-500">{{ announcement.publishDate }}</span>
                </div>
                <h4 class="text-lg font-bold text-gray-900 mb-2 line-clamp-2">{{ announcement.title }}</h4>
                <div class="flex justify-between items-center text-sm">
                  <span class="text-gray-600">预算: <strong class="text-hailong-primary">{{ announcement.budget }}</strong></span>
                  <span class="text-gray-500">截止: {{ announcement.deadline }}</span>
                </div>
              </div>
            </div>
          </div>
          <div>
            <div class="flex items-center justify-between mb-6">
              <h3 class="text-2xl font-bold text-hailong-dark">建设工程</h3>
              <router-link to="/announcements?tab=CONSTRUCTION"
                class="text-hailong-primary hover:underline text-sm">查看全部 →</router-link>
            </div>
            <div class="space-y-4">
              <div v-for="announcement in constructionAnnouncements" :key="announcement.id"
                class="p-6 bg-white rounded-xl hover:shadow-lg transition-all cursor-pointer border-l-4 border-hailong-secondary">
                <div class="flex justify-between items-start mb-3">
                  <span class="px-3 py-1 bg-green-100 text-green-800 rounded-full text-xs">{{ announcement.type }}</span>
                  <span class="text-xs text-gray-500">{{ announcement.publishDate }}</span>
                </div>
                <h4 class="text-lg font-bold text-gray-900 mb-2 line-clamp-2">{{ announcement.title }}</h4>
                <div class="flex justify-between items-center text-sm">
                  <span class="text-gray-600">预算: <strong class="text-hailong-secondary">{{ announcement.budget }}</strong></span>
                  <span class="text-gray-500">截止: {{ announcement.deadline }}</span>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>

    <!-- 业务范围 - 改为灰白渐变 -->
    <div class="py-24 bg-gradient-to-b from-blue-50/30 to-gray-50">
      <div class="container-wide">
        <div class="text-center mb-16">
          <h2 class="text-5xl font-bold text-hailong-dark mb-4 font-tech">业务范围</h2>
          <div class="w-24 h-1 bg-gradient-to-r from-hailong-primary to-hailong-secondary mx-auto"></div>
        </div>
        <div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-8">
          <div v-for="business in businessScope" :key="business.id"
            class="group relative bg-white rounded-2xl overflow-hidden shadow-lg hover:shadow-2xl transition-all duration-300 border border-gray-200 hover:border-hailong-primary">
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
    <!-- 企业资质 - 改为白色到浅灰渐变 -->
    <div class="py-24 bg-gradient-to-b from-gray-50 to-white">
      <div class="container-wide">
        <div class="text-center mb-16">
          <h2 class="text-5xl font-bold text-hailong-dark mb-4 font-tech">企业资质</h2>
          <div class="w-24 h-1 bg-gradient-to-r from-hailong-primary to-hailong-secondary mx-auto"></div>
        </div>
        <div class="bg-white rounded-2xl p-8 md:p-12 shadow-lg border border-gray-200">
          <div class="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 gap-4 md:gap-6">
            <div v-for="qualification in qualifications" :key="qualification"
              class="flex items-center p-4 bg-gradient-to-br from-hailong-primary/5 to-hailong-secondary/5 rounded-xl hover:shadow-md hover:scale-105 transition-all duration-300 cursor-pointer group">
              <div class="w-12 h-12 bg-hailong-primary/10 rounded-full flex items-center justify-center mr-4 flex-shrink-0 group-hover:bg-hailong-primary/20 transition-colors">
                <svg class="w-6 h-6 text-hailong-primary" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                  <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M9 12l2 2 4-4m6 2a9 9 0 11-18 0 9 9 0 0118 0z"></path>
                </svg>
              </div>
              <span class="text-gray-800 font-medium text-sm group-hover:text-hailong-primary transition-colors">{{ qualification }}</span>
            </div>
          </div>
        </div>
      </div>
    </div>
    
    <!-- 重要业绩展示 - 保持深色 -->
     <div class="py-24 bg-hailong-dark text-white">
      <div class="container-wide">
        <div class="text-center mb-16">
          <h2 class="text-5xl font-bold mb-4 font-tech">重要业绩展示</h2>
          <div class="w-24 h-1 bg-gradient-to-r from-hailong-primary to-hailong-secondary mx-auto"></div>
        </div>
        <div class="relative overflow-hidden">
          <div class="flex gap-6 animate-scroll">
            <div v-for="achievement in [...majorAchievements, ...majorAchievements]" :key="achievement.id + Math.random()"
              class="flex-shrink-0 w-80 bg-white/10 backdrop-blur-lg rounded-2xl overflow-hidden hover:bg-white/20 transition-all cursor-pointer group">
              <div class="h-48 overflow-hidden">
                <img :src="achievement.imageUrl" :alt="achievement.projectName"
                  class="w-full h-full object-cover group-hover:scale-110 transition-transform duration-500" />
              </div>
              <div class="p-6">
                <div class="flex items-center justify-between mb-3">
                  <span :class="[
                    'px-3 py-1 rounded-full text-xs font-semibold',
                    achievement.projectType === '工程' ? 'bg-hailong-primary/20 text-hailong-primary' :
                    achievement.projectType === '服务' ? 'bg-hailong-secondary/20 text-hailong-secondary' :
                    'bg-hailong-cyan/20 text-hailong-cyan'
                  ]">
                    {{ achievement.projectType }}
                  </span>
                </div>
                <h3 class="text-lg font-bold mb-3 line-clamp-2">{{ achievement.projectName }}</h3>
                <div class="text-2xl font-bold text-hailong-secondary">
                  {{ formatAmount(achievement.projectAmount) }}
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>

    <!-- 交易数据可视化 - 改为浅灰到白色渐变 -->
    <div class="py-24 bg-gradient-to-b from-white to-gray-50">
      <div class="container-wide">
        <div class="text-center mb-16">
          <h2 class="text-5xl font-bold text-hailong-dark mb-4 font-tech">交易数据可视化</h2>
          <div class="w-24 h-1 bg-gradient-to-r from-hailong-primary to-hailong-secondary mx-auto"></div>
        </div>
        <div class="grid grid-cols-2 md:grid-cols-4 gap-8 mb-12">
          <div class="bg-white rounded-2xl p-8 shadow-lg text-center">
            <div class="text-4xl font-bold text-hailong-primary mb-2">{{ transactionData.yearlyStats.totalProjects }}</div>
            <div class="text-gray-600">项目总数</div>
          </div>
          <div class="bg-white rounded-2xl p-8 shadow-lg text-center">
            <div class="text-4xl font-bold text-hailong-secondary mb-2">{{ transactionData.yearlyStats.totalAmount }}</div>
            <div class="text-gray-600">交易总额</div>
          </div>
          <div class="bg-white rounded-2xl p-8 shadow-lg text-center">
            <div class="text-4xl font-bold text-hailong-primary mb-2">{{ transactionData.yearlyStats.govProcurement }}</div>
            <div class="text-gray-600">政府采购</div>
          </div>
          <div class="bg-white rounded-2xl p-8 shadow-lg text-center">
            <div class="text-4xl font-bold text-hailong-secondary mb-2">{{ transactionData.yearlyStats.construction }}</div>
            <div class="text-gray-600">建设工程</div>
          </div>
        </div>
        <div class="grid grid-cols-1 lg:grid-cols-2 gap-8">
          <!-- 交易类型占比 -->
          <div class="bg-white rounded-2xl p-8 shadow-lg">
            <h3 class="text-2xl font-bold text-gray-900 mb-6">交易类型占比</h3>
            <div class="flex flex-col items-center justify-center h-64">
              <div class="relative w-48 h-48">
                <!-- 饼图背景圆环 -->
                <svg class="w-full h-full transform -rotate-90" viewBox="0 0 100 100">
                  <circle cx="50" cy="50" r="40" fill="none" stroke="#f3f4f6" stroke-width="20"/>
                  <!-- 政府采购 -->
                  <circle cx="50" cy="50" r="40" fill="none"
                    :stroke="transactionData.typeDistribution[0].color"
                    stroke-width="20"
                    :stroke-dasharray="`${transactionData.typeDistribution[0].percentage * 2.51} 251`"
                    class="transition-all duration-500 hover:stroke-width-[22]"/>
                  <!-- 建设工程 -->
                  <circle cx="50" cy="50" r="40" fill="none"
                    :stroke="transactionData.typeDistribution[1].color"
                    stroke-width="20"
                    :stroke-dasharray="`${transactionData.typeDistribution[1].percentage * 2.51} 251`"
                    :stroke-dashoffset="`-${transactionData.typeDistribution[0].percentage * 2.51}`"
                    class="transition-all duration-500 hover:stroke-width-[22]"/>
                </svg>
                <div class="absolute inset-0 flex items-center justify-center">
                  <div class="text-center">
                    <div class="text-3xl font-bold text-gray-900">{{ transactionData.yearlyStats.totalProjects }}</div>
                    <div class="text-sm text-gray-500">总项目</div>
                  </div>
                </div>
              </div>
              <div class="mt-6 space-y-3 w-full">
                <div v-for="item in transactionData.typeDistribution" :key="item.type"
                  class="flex items-center justify-between p-3 bg-gray-50 rounded-lg hover:bg-gray-100 transition-colors">
                  <div class="flex items-center">
                    <div class="w-4 h-4 rounded-full mr-3" :style="{ backgroundColor: item.color }"></div>
                    <span class="text-gray-700 font-medium">{{ item.type }}</span>
                  </div>
                  <div class="text-right">
                    <div class="text-lg font-bold" :style="{ color: item.color }">{{ item.count }}</div>
                    <div class="text-xs text-gray-500">{{ item.percentage }}%</div>
                  </div>
                </div>
              </div>
            </div>
          </div>

          <!-- 地区排行 -->
          <div class="bg-white rounded-2xl p-8 shadow-lg">
            <h3 class="text-2xl font-bold text-gray-900 mb-6">地区项目排行</h3>
            <div class="space-y-3">
              <div v-for="(region, index) in transactionData.regionRanking" :key="region.region"
                class="group relative">
                <div class="flex items-center justify-between mb-2">
                  <div class="flex items-center flex-1">
                    <div class="w-8 h-8 rounded-full flex items-center justify-center mr-3 font-bold text-sm"
                      :class="[
                        index === 0 ? 'bg-yellow-100 text-yellow-600' :
                        index === 1 ? 'bg-gray-100 text-gray-600' :
                        index === 2 ? 'bg-orange-100 text-orange-600' :
                        'bg-blue-50 text-blue-600'
                      ]">
                      {{ index + 1 }}
                    </div>
                    <span class="text-gray-700 font-medium">{{ region.region }}</span>
                  </div>
                  <div class="text-right ml-4">
                    <div class="text-lg font-bold text-hailong-primary">{{ region.projects }}</div>
                    <div class="text-xs text-gray-500">{{ region.amount }}亿</div>
                  </div>
                </div>
                <div class="relative h-2 bg-gray-100 rounded-full overflow-hidden">
                  <div class="absolute inset-y-0 left-0 bg-gradient-to-r from-hailong-primary to-hailong-secondary rounded-full transition-all duration-500 group-hover:opacity-80"
                    :style="{ width: (region.projects / transactionData.regionRanking[0].projects * 100) + '%' }">
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>


    <!-- 联系我们区块 - 保持深色渐变 -->
    <div class="relative py-24 bg-gradient-to-br from-hailong-dark via-hailong-primary to-hailong-secondary text-white overflow-hidden">
      <!-- 动态背景效果 -->
      <div class="absolute inset-0">
        <div class="absolute top-1/4 left-1/4 w-96 h-96 bg-hailong-primary rounded-full filter blur-3xl opacity-20 animate-float"></div>
        <div class="absolute bottom-1/4 right-1/4 w-96 h-96 bg-hailong-secondary rounded-full filter blur-3xl opacity-20 animate-float" style="animation-delay:1s"></div>
        <div v-for="i in 20" :key="'contact-' + i" class="absolute w-1 h-1 bg-hailong-cyan rounded-full animate-ping"
          :style="{ top: `${Math.random() * 100}%`, left: `${Math.random() * 100}%`, animationDelay: `${Math.random() * 2}s` }">
        </div>
      </div>
      
      <div class="container-wide relative z-10">
        <div class="text-center mb-16">
          <h2 class="text-5xl font-bold mb-4 font-tech">联系我们</h2>
          <div class="w-24 h-1 bg-white/50 mx-auto"></div>
          <p class="mt-4 text-xl text-white/80">期待与您的合作，共创美好未来</p>
        </div>
        
        <div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
          <!-- 联系电话 -->
          <div class="bg-white/10 backdrop-blur-lg rounded-2xl p-8 hover:bg-white/20 transition-all group">
            <div class="w-16 h-16 bg-white/20 rounded-full flex items-center justify-center mb-4 group-hover:scale-110 transition-transform">
              <svg class="w-8 h-8 text-white" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M3 5a2 2 0 012-2h3.28a1 1 0 01.948.684l1.498 4.493a1 1 0 01-.502 1.21l-2.257 1.13a11.042 11.042 0 005.516 5.516l1.13-2.257a1 1 0 011.21-.502l4.493 1.498a1 1 0 01.684.949V19a2 2 0 01-2 2h-1C9.716 21 3 14.284 3 6V5z"></path>
              </svg>
            </div>
            <h3 class="text-xl font-bold mb-3">联系电话</h3>
            <p class="text-white/90 text-base font-medium">0371-55894666</p>
          </div>

          <!-- 邮箱地址 -->
          <div class="bg-white/10 backdrop-blur-lg rounded-2xl p-8 hover:bg-white/20 transition-all group">
            <div class="w-16 h-16 bg-white/20 rounded-full flex items-center justify-center mb-4 group-hover:scale-110 transition-transform">
              <svg class="w-8 h-8 text-white" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M3 8l7.89 5.26a2 2 0 002.22 0L21 8M5 19h14a2 2 0 002-2V7a2 2 0 00-2-2H5a2 2 0 00-2 2v10a2 2 0 002 2z"></path>
              </svg>
            </div>
            <h3 class="text-xl font-bold mb-3">邮箱地址</h3>
            <p class="text-white/90 text-base font-medium break-all">037155894666@henanhailong.com</p>
          </div>

          <!-- 公司地址 -->
          <div class="bg-white/10 backdrop-blur-lg rounded-2xl p-8 hover:bg-white/20 transition-all group">
            <div class="w-16 h-16 bg-white/20 rounded-full flex items-center justify-center mb-4 group-hover:scale-110 transition-transform">
              <svg class="w-8 h-8 text-white" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M17.657 16.657L13.414 20.9a1.998 1.998 0 01-2.827 0l-4.244-4.243a8 8 0 1111.314 0z"></path>
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M15 11a3 3 0 11-6 0 3 3 0 016 0z"></path>
              </svg>
            </div>
            <h3 class="text-xl font-bold mb-3">公司地址</h3>
            <p class="text-white/90 text-base font-medium leading-relaxed">郑州市郑东新区金水东路85号雅宝·东方国际广场2号楼13层</p>
          </div>
        </div>
      </div>
    </div>

    <Footer />

    <!-- 联系信息模态框 -->
    <div v-if="showContactModal"
      @click="showContactModal = false"
      class="fixed inset-0 z-[100] bg-black/60 backdrop-blur-sm flex items-center justify-center p-4 animate-fade-in">
      <div @click.stop
        class="bg-white rounded-2xl shadow-2xl max-w-2xl w-full max-h-[90vh] overflow-y-auto">
        <!-- 模态框头部 -->
        <div class="bg-gradient-to-r from-hailong-primary to-hailong-secondary p-6 text-white relative">
          <button @click="showContactModal = false"
            class="absolute top-4 right-4 text-white hover:text-gray-200 transition-colors">
            <svg class="w-6 h-6" fill="none" stroke="currentColor" viewBox="0 0 24 24">
              <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M6 18L18 6M6 6l12 12"></path>
            </svg>
          </button>
          <h3 class="text-2xl font-bold">联系我们</h3>
          <p class="text-white/80 mt-2">期待与您的合作，共创美好未来</p>
        </div>

        <!-- 模态框内容 -->
        <div class="p-8">
          <div class="grid grid-cols-1 md:grid-cols-2 gap-6 items-stretch">
            <!-- 联系方式列表 -->
            <div class="space-y-6 flex flex-col justify-center">
              <div class="flex items-start">
                <div class="w-12 h-12 bg-hailong-primary/10 rounded-full flex items-center justify-center mr-4 flex-shrink-0">
                  <svg class="w-6 h-6 text-hailong-primary" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M3 5a2 2 0 012-2h3.28a1 1 0 01.948.684l1.498 4.493a1 1 0 01-.502 1.21l-2.257 1.13a11.042 11.042 0 005.516 5.516l1.13-2.257a1 1 0 011.21-.502l4.493 1.498a1 1 0 01.684.949V19a2 2 0 01-2 2h-1C9.716 21 3 14.284 3 6V5z"></path>
                  </svg>
                </div>
                <div>
                  <h4 class="font-bold text-gray-900 mb-1">联系电话</h4>
                  <p class="text-gray-600 text-sm">0371-55894666</p>
                </div>
              </div>

              <div class="flex items-start">
                <div class="w-12 h-12 bg-hailong-primary/10 rounded-full flex items-center justify-center mr-4 flex-shrink-0">
                  <svg class="w-6 h-6 text-hailong-primary" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M3 8l7.89 5.26a2 2 0 002.22 0L21 8M5 19h14a2 2 0 002-2V7a2 2 0 00-2-2H5a2 2 0 00-2 2v10a2 2 0 002 2z"></path>
                  </svg>
                </div>
                <div>
                  <h4 class="font-bold text-gray-900 mb-1">邮箱地址</h4>
                  <p class="text-gray-600 text-sm break-all">037155894666@henanhailong.com</p>
                </div>
              </div>

              <div class="flex items-start">
                <div class="w-12 h-12 bg-hailong-primary/10 rounded-full flex items-center justify-center mr-4 flex-shrink-0">
                  <svg class="w-6 h-6 text-hailong-primary" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M17.657 16.657L13.414 20.9a1.998 1.998 0 01-2.827 0l-4.244-4.243a8 8 0 1111.314 0z"></path>
                    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M15 11a3 3 0 11-6 0 3 3 0 016 0z"></path>
                  </svg>
                </div>
                <div>
                  <h4 class="font-bold text-gray-900 mb-1">公司地址</h4>
                  <p class="text-gray-600 text-sm">郑州市郑东新区金水东路85号雅宝·东方国际广场2号楼13层</p>
                </div>
              </div>

            </div>

            <!-- 工作时间 -->
            <div class="flex flex-col items-center justify-center bg-gradient-to-br from-hailong-primary/10 to-hailong-secondary/10 rounded-xl p-6 h-full">
              <div class="w-20 h-20 bg-hailong-primary/20 rounded-full flex items-center justify-center mb-4">
                <svg class="w-10 h-10 text-hailong-primary" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                  <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M12 8v4l3 3m6-3a9 9 0 11-18 0 9 9 0 0118 0z"></path>
                </svg>
              </div>
              <h4 class="font-bold text-gray-900 text-lg mb-2">工作时间</h4>
              <p class="text-gray-700 font-medium mb-1">周一至周五</p>
              <p class="text-hailong-primary text-xl font-bold mb-3">9:00-18:00</p>
              <p class="text-gray-500 text-sm text-center">节假日休息</p>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref } from 'vue'
import { companyInfo, companyProfile, businessScope, transactionData, majorAchievements, govProcurementAnnouncements, constructionAnnouncements } from './data.js'
import Header from '@/components/Header.vue'
import Footer from '@/components/Footer.vue'

// 联系信息模态框
const showContactModal = ref(false)

// 企业资质列表
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

// 格式化金额显示（单位：万元）
const formatAmount = (amount) => {
  if (amount >= 10000) {
    return (amount / 10000).toFixed(1) + '亿'
  }
  return amount.toLocaleString() + '万'
}
</script>

<style scoped>
.line-clamp-2 {
  display: -webkit-box;
  -webkit-line-clamp: 2;
  -webkit-box-orient: vertical;
  overflow: hidden
}

@keyframes scroll {
  0% {
    transform: translateX(0);
  }
  100% {
    transform: translateX(-50%);
  }
}

.animate-scroll {
  animation: scroll 30s linear infinite;
}

.animate-scroll:hover {
  animation-play-state: paused;
}
</style>
