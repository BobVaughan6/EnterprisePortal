<template>
  <div class="min-h-screen bg-gradient-to-br from-hailong-dark via-hailong-primary to-hailong-secondary">
    <Header />

    <!-- 页面标题 -->
    <div class="pt-32 pb-16 text-center text-white">
      <h1
        class="text-6xl font-bold mb-4 font-tech bg-gradient-to-r from-white via-hailong-cyan to-white bg-clip-text text-transparent animate-fade-in">
        联系我们
      </h1>
      <p class="text-xl text-gray-200">期待与您的合作，共创美好未来</p>
    </div>

    <!-- 内容区域 -->
    <div class="py-16 bg-white">
      <div class="container-wide">
        <div class="animate-fade-in">
          <!-- 联系方式卡片 -->
          <div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-8 mb-16">
            <!-- 联系电话 -->
            <div
              class="group bg-white rounded-2xl p-8 shadow-lg hover:shadow-2xl transition-all duration-300 border border-gray-200 hover:border-hailong-primary">
              <div
                class="w-16 h-16 bg-gradient-to-br from-hailong-primary/10 to-hailong-secondary/10 rounded-full flex items-center justify-center mb-6 group-hover:scale-110 transition-transform">
                <svg class="w-8 h-8 text-hailong-primary" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                  <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
                    d="M3 5a2 2 0 012-2h3.28a1 1 0 01.948.684l1.498 4.493a1 1 0 01-.502 1.21l-2.257 1.13a11.042 11.042 0 005.516 5.516l1.13-2.257a1 1 0 011.21-.502l4.493 1.498a1 1 0 01.684.949V19a2 2 0 01-2 2h-1C9.716 21 3 14.284 3 6V5z">
                  </path>
                </svg>
              </div>
              <h3 class="text-2xl font-bold text-gray-900 mb-3 group-hover:text-hailong-primary transition-colors">联系电话
              </h3>
              <p class="text-gray-600 mb-4">工作日 9:00-18:00</p>
              <a href="tel:0371-55894666"
                class="text-xl font-bold text-hailong-primary hover:text-hailong-secondary transition-colors">
                0371-55894666
              </a>
            </div>

            <!-- 邮箱地址 -->
            <div
              class="group bg-white rounded-2xl p-8 shadow-lg hover:shadow-2xl transition-all duration-300 border border-gray-200 hover:border-hailong-primary">
              <div
                class="w-16 h-16 bg-gradient-to-br from-hailong-primary/10 to-hailong-secondary/10 rounded-full flex items-center justify-center mb-6 group-hover:scale-110 transition-transform">
                <svg class="w-8 h-8 text-hailong-primary" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                  <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
                    d="M3 8l7.89 5.26a2 2 0 002.22 0L21 8M5 19h14a2 2 0 002-2V7a2 2 0 00-2-2H5a2 2 0 00-2 2v10a2 2 0 002 2z">
                  </path>
                </svg>
              </div>
              <h3 class="text-2xl font-bold text-gray-900 mb-3 group-hover:text-hailong-primary transition-colors">邮箱地址
              </h3>
              <p class="text-gray-600 mb-4">欢迎来信咨询</p>
              <a href="mailto:037155894666@henanhailong.com"
                class="text-lg font-bold text-hailong-primary hover:text-hailong-secondary transition-colors break-all">
                037155894666@henanhailong.com
              </a>
            </div>

            <!-- 公司地址 -->
            <div
              class="group bg-white rounded-2xl p-8 shadow-lg hover:shadow-2xl transition-all duration-300 border border-gray-200 hover:border-hailong-primary">
              <div
                class="w-16 h-16 bg-gradient-to-br from-hailong-primary/10 to-hailong-secondary/10 rounded-full flex items-center justify-center mb-6 group-hover:scale-110 transition-transform">
                <svg class="w-8 h-8 text-hailong-primary" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                  <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
                    d="M17.657 16.657L13.414 20.9a1.998 1.998 0 01-2.827 0l-4.244-4.243a8 8 0 1111.314 0z"></path>
                  <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
                    d="M15 11a3 3 0 11-6 0 3 3 0 016 0z"></path>
                </svg>
              </div>
              <h3 class="text-2xl font-bold text-gray-900 mb-3 group-hover:text-hailong-primary transition-colors">公司地址
              </h3>
              <p class="text-gray-600 mb-4">欢迎莅临指导</p>
              <p class="text-lg font-medium text-gray-800 leading-relaxed">
                郑州市郑东新区金水东路85号<br />雅宝·东方国际广场2号楼13层
              </p>
            </div>

            <!-- 工作时间 -->
            <div
              class="group bg-white rounded-2xl p-8 shadow-lg hover:shadow-2xl transition-all duration-300 border border-gray-200 hover:border-hailong-primary">
              <div
                class="w-16 h-16 bg-gradient-to-br from-hailong-primary/10 to-hailong-secondary/10 rounded-full flex items-center justify-center mb-6 group-hover:scale-110 transition-transform">
                <svg class="w-8 h-8 text-hailong-primary" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                  <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
                    d="M12 8v4l3 3m6-3a9 9 0 11-18 0 9 9 0 0118 0z"></path>
                </svg>
              </div>
              <h3 class="text-2xl font-bold text-gray-900 mb-3 group-hover:text-hailong-primary transition-colors">工作时间
              </h3>
              <p class="text-gray-600 mb-4">周一至周五</p>
              <div class="space-y-3">
                <div class="flex items-center justify-between">
                  <span class="text-sm text-gray-600">工作日</span>
                  <span class="text-lg font-bold text-hailong-primary">9:00-18:00</span>
                </div>
                <div class="flex items-center justify-between">
                  <span class="text-sm text-gray-600">周末</span>
                  <span class="text-sm text-gray-500">休息</span>
                </div>
              </div>
            </div>
          </div>

          <!-- 交通指引与地图位置 -->
          <div class="grid grid-cols-1 lg:grid-cols-2 gap-8">
            <!-- 交通指引 -->
            <div class="bg-white rounded-2xl p-8 shadow-lg border border-gray-200">
              <div class="flex items-center mb-6">
                <div class="w-12 h-12 bg-hailong-primary/10 rounded-full flex items-center justify-center mr-4">
                  <svg class="w-6 h-6 text-hailong-primary" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
                      d="M9 20l-5.447-2.724A1 1 0 013 16.382V5.618a1 1 0 011.447-.894L9 7m0 13l6-3m-6 3V7m6 10l4.553 2.276A1 1 0 0021 18.382V7.618a1 1 0 00-.553-.894L15 4m0 13V4m0 0L9 7">
                    </path>
                  </svg>
                </div>
                <h3 class="text-2xl font-bold text-gray-900">交通指引</h3>
              </div>
              <div class="grid grid-cols-1 gap-4">
                <!-- 地铁 -->
                <div class="p-4 bg-gradient-to-br from-blue-50 to-blue-100/50 rounded-xl">
                  <div class="flex items-center mb-3">
                    <div class="w-8 h-8 bg-blue-500 rounded-full flex items-center justify-center mr-2">
                      <svg class="w-5 h-5 text-white" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                        <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
                          d="M13 10V3L4 14h7v7l9-11h-7z"></path>
                      </svg>
                    </div>
                    <h4 class="text-base font-bold text-gray-900">地铁出行</h4>
                  </div>
                  <ul class="space-y-1 text-sm text-gray-700 ml-10">
                    <li>地铁1号线会展中心站D口</li>
                    <li>步行约500米即可到达</li>
                  </ul>
                </div>
                <!-- 公交 -->
                <div class="p-4 bg-gradient-to-br from-green-50 to-green-100/50 rounded-xl">
                  <div class="flex items-center mb-3">
                    <div class="w-8 h-8 bg-green-500 rounded-full flex items-center justify-center mr-2">
                      <svg class="w-5 h-5 text-white" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                        <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
                          d="M8 7h12m0 0l-4-4m4 4l-4 4m0 6H4m0 0l4 4m-4-4l4-4"></path>
                      </svg>
                    </div>
                    <h4 class="text-base font-bold text-gray-900">公交出行</h4>
                  </div>
                  <ul class="space-y-1 text-sm text-gray-700 ml-10">
                    <li>26路、43路、47路、115路</li>
                    <li>金水东路站下车即到</li>
                  </ul>
                </div>
                <!-- 自驾 -->
                <div class="p-4 bg-gradient-to-br from-orange-50 to-orange-100/50 rounded-xl">
                  <div class="flex items-center mb-3">
                    <div class="w-8 h-8 bg-orange-500 rounded-full flex items-center justify-center mr-2">
                      <svg class="w-5 h-5 text-white" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                        <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
                          d="M9 20l-5.447-2.724A1 1 0 013 16.382V5.618a1 1 0 011.447-.894L9 7m0 13l6-3m-6 3V7m6 10l4.553 2.276A1 1 0 0021 18.382V7.618a1 1 0 00-.553-.894L15 4m0 13V4m0 0L9 7">
                        </path>
                      </svg>
                    </div>
                    <h4 class="text-base font-bold text-gray-900">自驾出行</h4>
                  </div>
                  <ul class="space-y-1 text-sm text-gray-700 ml-10">
                    <li>导航搜索"雅宝东方国际广场"</li>
                    <li>大厦配有地下停车场</li>
                  </ul>
                </div>
                <!-- 周边地标 -->
                <div class="p-4 bg-gradient-to-br from-purple-50 to-purple-100/50 rounded-xl">
                  <div class="flex items-center mb-3">
                    <div class="w-8 h-8 bg-purple-500 rounded-full flex items-center justify-center mr-2">
                      <svg class="w-5 h-5 text-white" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                        <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
                          d="M19 21V5a2 2 0 00-2-2H7a2 2 0 00-2 2v16m14 0h2m-2 0h-5m-9 0H3m2 0h5M9 7h1m-1 4h1m4-4h1m-1 4h1m-5 10v-5a1 1 0 011-1h2a1 1 0 011 1v5m-4 0h4">
                        </path>
                      </svg>
                    </div>
                    <h4 class="text-base font-bold text-gray-900">周边地标</h4>
                  </div>
                  <ul class="space-y-1 text-sm text-gray-700 ml-10">
                    <li>郑州国际会展中心</li>
                    <li>河南省体育中心</li>
                  </ul>
                </div>
              </div>
            </div>

            <!-- 地图位置 -->
            <div class="bg-white rounded-2xl p-8 shadow-lg border border-gray-200">
              <div class="flex items-center mb-6">
                <div class="w-12 h-12 bg-hailong-primary/10 rounded-full flex items-center justify-center mr-4">
                  <svg class="w-6 h-6 text-hailong-primary" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
                      d="M17.657 16.657L13.414 20.9a1.998 1.998 0 01-2.827 0l-4.244-4.243a8 8 0 1111.314 0z"></path>
                    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M15 11a3 3 0 11-6 0 3 3 0 016 0z">
                    </path>
                  </svg>
                </div>
                <h3 class="text-2xl font-bold text-gray-900">公司位置</h3>
              </div>

              <div id="map-container" class="h-96 bg-gray-100 rounded-xl overflow-hidden mb-4"></div>
              <div class="p-4 bg-gray-50 rounded-lg">
                <p class="text-sm text-gray-600 flex items-start">
                  <svg class="w-4 h-4 mr-2 text-hailong-primary flex-shrink-0 mt-0.5" fill="none" stroke="currentColor"
                    viewBox="0 0 24 24">
                    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
                      d="M13 16h-1v-4h-1m1-4h.01M21 12a9 9 0 11-18 0 9 9 0 0118 0z"></path>
                  </svg>
                  <span>使用滚轮可缩放地图查看详细位置</span>
                </p>
              </div>
            </div>
          </div>

    <!-- 常见问题 -->
    <div class="mt-16">
      <div class="text-center mb-12">
        <h2 class="text-4xl font-bold text-hailong-dark mb-4 font-tech">常见问题</h2>
        <div class="w-24 h-1 bg-gradient-to-r from-hailong-primary to-hailong-secondary mx-auto"></div>
      </div>
      <div class="grid grid-cols-1 md:grid-cols-2 gap-6">
        <div v-for="(faq, index) in faqs" :key="index"
          class="bg-white rounded-xl p-6 shadow-sm hover:shadow-lg transition-all border-l-4 border-hailong-primary">
          <h4 class="text-lg font-bold text-gray-900 mb-3 flex items-start">
            <span
              class="inline-block w-6 h-6 bg-hailong-primary text-white rounded-full text-sm flex items-center justify-center mr-2 flex-shrink-0 mt-0.5">Q</span>
            {{ faq.question }}
          </h4>
          <p class="text-gray-600 ml-8">{{ faq.answer }}</p>
        </div>
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
import Header from '@/components/Header.vue'
import Footer from '@/components/Footer.vue'

// 确保页面加载时滚动到顶部
onMounted(() => {
  window.scrollTo(0, 0)
  initMap()
})

// 初始化地图
const initMap = () => {
  // 动态加载高德地图API
  const script = document.createElement('script')
  script.src = 'https://webapi.amap.com/maps?v=2.0&key=21c2669531206e2b32dd6a3b1020c581'
  script.onload = () => {
    const map = new AMap.Map('map-container', {
      zoom: 16,
      center: [113.773301, 34.768953],
      viewMode: '2D',
      dragEnable: true, // 禁用拖拽
      keyboardEnable: false, // 禁用键盘
      doubleClickZoom: false, // 禁用双击放大
      scrollWheel: true, // 允许滚轮缩放
      touchZoom: true, // 允许触摸缩放
      rotateEnable: false, // 禁用旋转
      pitchEnable: false, // 禁用倾斜
      jogEnable: false, // 禁用惯性拖拽
      animateEnable: false // 禁用动画
    })

    // 添加标记点
    const marker = new AMap.Marker({
      position: [113.773301, 34.768953],
      // title: '海隆工程咨询有限公司',
      map: map,
      // label: {
      //   content: '海隆工程咨询有限公司',
      //   direction: 'top'
      // }
    })

    // 创建信息窗口内容
    const infoWindow = new AMap.InfoWindow({
      content: `
        <div style="
          padding: 12px;
          min-width: 220px;
          font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, sans-serif;
          background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
          border-radius: 8px;
          box-shadow: 0 8px 16px rgba(0,0,0,0.15);
        ">
          <div style="
            background: white;
            border-radius: 6px;
            padding: 12px;
          ">
            <h3 style="
              margin: 0 0 10px 0;
              font-size: 15px;
              font-weight: bold;
              color: #1a202c;
              padding-bottom: 8px;
              border-bottom: 2px solid #667eea;
            ">
              🏢 海隆工程咨询
            </h3>
            <div style="margin: 8px 0; display: flex; align-items: center;">
              <span style="color: #667eea; font-size: 14px; margin-right: 6px;">📞</span>
              <a href="tel:0371-55894666" style="
                font-size: 14px;
                color: #667eea;
                text-decoration: none;
                font-weight: 600;
              ">0371-55894666</a>
            </div>
            <div style="margin: 8px 0; display: flex; align-items: start;">
              <span style="color: #667eea; font-size: 14px; margin-right: 6px; margin-top: 2px;">📍</span>
              <div style="font-size: 12px; color: #4a5568; line-height: 1.4;">
                金水东路85号雅宝国际2号楼13层
              </div>
            </div>
          </div>
        </div>
      `,
      offset: new AMap.Pixel(0, -30),
      closeWhenClickMap: true
    })

    // 默认打开信息窗口
    infoWindow.open(map, marker.getPosition())

    // 点击标记点打开信息窗口
    marker.on('click', () => {
      infoWindow.open(map, marker.getPosition())
    })
  }
  document.head.appendChild(script)
}

// 常见问题
const faqs = [
  {
    question: '如何联系海隆咨询？',
    answer: '您可以通过电话、邮箱或在线留言的方式联系我们，我们会在工作时间内尽快回复您。'
  },
  {
    question: '海隆咨询的主要业务范围是什么？',
    answer: '我们主要提供政府采购代理、工程招标代理、造价咨询、工程监理等专业服务。'
  },
  {
    question: '如何获取项目报价？',
    answer: '请通过电话或邮箱联系我们，提供项目基本信息，我们会根据具体情况为您提供专业报价。'
  },
  {
    question: '海隆咨询的服务区域有哪些？',
    answer: '我们主要服务于河南省及周边地区，具体服务范围可通过电话咨询了解详情。'
  }
]
</script>

<style scoped>
.animate-fade-in {
  animation: fadeIn 0.5s ease-in;
}

@keyframes fadeIn {
  from {
    opacity: 0;
    transform: translateY(20px);
  }

  to {
    opacity: 1;
    transform: translateY(0);
  }
}
</style>