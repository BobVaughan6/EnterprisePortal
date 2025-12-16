<template>
  <div class="min-h-screen bg-gradient-to-br from-hailong-dark via-hailong-primary to-hailong-secondary">
    <!-- 导航栏 -->
    <Header />

    <!-- 页面内容 -->
    <div class="pt-32 pb-16">
      <div class="container-wide max-w-4xl mx-auto">
        <!-- 标题部分 -->
        <div class="text-center mb-12">
          <h1 class="text-5xl font-bold mb-6 font-tech bg-gradient-to-r from-white via-hailong-cyan to-white bg-clip-text text-transparent animate-fade-in">
            加入海隆专家库
          </h1>
        </div>

        <!-- 主要内容卡片 -->
        <div class="bg-white rounded-2xl shadow-2xl overflow-hidden border border-gray-200 animate-fade-in">
          <div class="p-8 md:p-12">
            <!-- 二维码和公告内容区域 -->
            <div class="flex flex-col gap-8 mb-8">
              <!-- 移动端：显示二维码 -->
              <div class="flex-shrink-0 mx-auto md:hidden">
                <div class="bg-gradient-to-br from-hailong-primary/10 to-hailong-secondary/10 p-6 rounded-xl border-2 border-hailong-cyan/30">
                  <div class="bg-white rounded-lg p-3">
                    <img :src="qrCodeImage" alt="专家库二维码" class="w-48 h-48 object-contain" />
                    <p class="text-xs text-gray-600 mt-2 text-center font-medium">扫码填写信息</p>
                  </div>
                </div>
              </div>

              <!-- 公告内容区域 -->
              <div class="w-full">
                <div class="prose prose-lg max-w-none">
                  <div class="bg-gradient-to-r from-hailong-primary/5 to-hailong-secondary/5 p-6 md:p-8 rounded-xl border-l-4 border-hailong-primary">
                    <p class="text-gray-700 leading-relaxed mb-4">
                      海隆工程咨询有限公司现已启动代理专家库信息录入工作，特此诚邀您参与。
                    </p>
                    <p class="text-gray-700 leading-relaxed mb-4">
                      为便于后续项目合作，请您认真、如实填写相关信息。我们将在资料审核通过后，将您的信息正式录入专家库抽取系统。
                    </p>
                    <p class="text-gray-700 leading-relaxed font-medium mb-6">
                      感谢您的支持与配合！
                    </p>
                    
                    <!-- 电脑端：填写按钮 -->
                    <div class="hidden md:block mt-6">
                      <a
                        href="https://www.wjx.cn/vm/tkPK2g4.aspx"
                        target="_blank"
                        rel="noopener noreferrer"
                        class="inline-flex items-center px-8 py-3 bg-gradient-to-r from-hailong-primary to-hailong-secondary text-white font-semibold rounded-lg hover:shadow-xl transform hover:-translate-y-0.5 transition-all duration-200"
                      >
                        <svg class="w-5 h-5 mr-2" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                          <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M11 5H6a2 2 0 00-2 2v11a2 2 0 002 2h11a2 2 0 002-2v-5m-1.414-9.414a2 2 0 112.828 2.828L11.828 15H9v-2.828l8.586-8.586z"></path>
                        </svg>
                        立即填写专家信息
                        <svg class="w-5 h-5 ml-2" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                          <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M13 7l5 5m0 0l-5 5m5-5H6"></path>
                        </svg>
                      </a>
                    </div>
                  </div>
                </div>
              </div>
            </div>

            <!-- 分隔线 -->
            <div class="border-t border-gray-200 my-8"></div>

            <!-- 联系方式区域 -->
            <div class="bg-gray-50 p-6 rounded-xl border border-gray-200">
              <h3 class="text-xl font-bold text-hailong-dark mb-4 flex items-center">
                <svg class="w-6 h-6 mr-2 text-hailong-primary" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                  <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M3 5a2 2 0 012-2h3.28a1 1 0 01.948.684l1.498 4.493a1 1 0 01-.502 1.21l-2.257 1.13a11.042 11.042 0 005.516 5.516l1.13-2.257a1 1 0 011.21-.502l4.493 1.498a1 1 0 01.684.949V19a2 2 0 01-2 2h-1C9.716 21 3 14.284 3 6V5z"></path>
                </svg>
                联系方式
              </h3>
              <div class="space-y-3">
                <div class="flex items-center text-gray-800">
                  <span class="font-medium text-gray-900 mr-2">联系电话：</span>
                  <a href="tel:0371-55894666" class="text-hailong-primary hover:text-hailong-secondary transition-colors font-semibold text-lg">
                    {{ companyInfo.contact.phone }}
                  </a>
                </div>
                <div class="flex items-center text-gray-800">
                  <span class="font-medium text-gray-900 mr-2">联系邮箱：</span>
                  <a :href="`mailto:${companyInfo.contact.email}`" class="text-hailong-primary hover:text-hailong-secondary transition-colors">
                    {{ companyInfo.contact.email }}
                  </a>
                </div>
                <div class="flex items-start text-gray-800">
                  <span class="font-medium text-gray-900 mr-2 flex-shrink-0">办公地址：</span>
                  <span>{{ companyInfo.contact.address.fullAddress }}</span>
                </div>
              </div>
            </div>

            <!-- 温馨提示 -->
            <div class="mt-8 bg-blue-50 border border-blue-200 rounded-lg p-4">
              <div class="flex items-start">
                <svg class="w-5 h-5 text-blue-500 mr-2 flex-shrink-0 mt-0.5" fill="currentColor" viewBox="0 0 20 20">
                  <path fill-rule="evenodd" d="M18 10a8 8 0 11-16 0 8 8 0 0116 0zm-7-4a1 1 0 11-2 0 1 1 0 012 0zM9 9a1 1 0 000 2v3a1 1 0 001 1h1a1 1 0 100-2v-3a1 1 0 00-1-1H9z" clip-rule="evenodd"></path>
                </svg>
                <div class="text-sm text-blue-800">
                  <p class="font-medium mb-1">温馨提示：</p>
                  <ul class="list-disc list-inside space-y-1 text-blue-700">
                    <li class="md:hidden">请使用微信扫描二维码填写专家信息</li>
                    <li class="hidden md:list-item">电脑端请点击二维码跳转至填写页面</li>
                    <li>请确保填写信息真实、准确、完整</li>
                    <li>如有疑问，请拨打联系电话咨询</li>
                  </ul>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>

    <!-- 页脚 -->
    <Footer />
  </div>
</template>

<script setup>
import { computed } from 'vue'
import Header from '@/components/Header.vue'
import Footer from '@/components/Footer.vue'
import { getCompanyInfo } from '@/utils/config'
import qrCodeImage from '@/assets/QR_CODE.png'

// 获取公司信息
const companyInfo = computed(() => ({
  contact: getCompanyInfo().contact || {
    phone: '0371-55894666',
    email: '037155894666@henanhailong.com',
    address: {
      fullAddress: '郑州市郑东新区金水东路85号雅宝·东方国际广场2号楼13层'
    }
  }
}))
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

.prose {
  color: #374151;
}

.prose p {
  margin-bottom: 0;
}
</style>