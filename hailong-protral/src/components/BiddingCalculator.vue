<template>
  <div class="bg-white rounded-xl shadow-2xl p-8 border border-hailong-primary/20">
    <div class="flex items-center justify-between mb-6">
      <h2 class="text-3xl font-bold text-hailong-dark flex items-center gap-3">
        <svg class="w-8 h-8 text-hailong-primary" fill="none" stroke="currentColor" viewBox="0 0 24 24">
          <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M9 7h6m0 10v-3m-3 3h.01M9 17h.01M9 14h.01M12 14h.01M15 11h.01M12 11h.01M9 11h.01M7 21h10a2 2 0 002-2V5a2 2 0 00-2-2H7a2 2 0 00-2 2v14a2 2 0 002 2z" />
        </svg>
        招标代理服务费计算工具
      </h2>
      <button
        @click="$emit('close')"
        class="text-gray-400 hover:text-gray-600 transition-colors"
      >
        <svg class="w-6 h-6" fill="none" stroke="currentColor" viewBox="0 0 24 24">
          <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M6 18L18 6M6 6l12 12" />
        </svg>
      </button>
    </div>

    <div class="grid grid-cols-1 lg:grid-cols-2 gap-8">
      <!-- 输入区域 -->
      <div class="space-y-6">
        <div>
          <label class="block text-sm font-semibold text-gray-700 mb-3">服务类型</label>
          <div class="grid grid-cols-3 gap-3">
            <button
              v-for="type in serviceTypes"
              :key="type.value"
              @click="formData.serviceType = type.value; calculate()"
              :class="[
                'py-3 px-4 rounded-lg font-medium transition-all border-2',
                formData.serviceType === type.value
                  ? 'bg-gradient-to-r from-hailong-primary to-hailong-secondary text-white border-hailong-primary shadow-lg'
                  : 'bg-white text-gray-700 border-gray-200 hover:border-hailong-primary'
              ]"
            >
              {{ type.label }}
            </button>
          </div>
        </div>

        <div>
          <label class="block text-sm font-semibold text-gray-700 mb-3">项目金额（万元）</label>
          <input
            v-model.number="formData.amount"
            type="number"
            step="0.01"
            min="0"
            placeholder="请输入项目金额"
            class="w-full px-4 py-3 border-2 border-gray-200 rounded-lg focus:border-hailong-primary focus:outline-none transition-colors text-lg"
            @input="calculate"
          />
          <p class="text-xs text-gray-500 mt-2">请输入正数，支持小数</p>
        </div>

        <div>
          <label class="block text-sm font-semibold text-gray-700 mb-3">优惠折扣（%）</label>
          <input
            v-model.number="formData.discount"
            type="number"
            step="1"
            min="0"
            max="100"
            placeholder="请输入折扣比例"
            class="w-full px-4 py-3 border-2 border-gray-200 rounded-lg focus:border-hailong-primary focus:outline-none transition-colors text-lg"
            @input="calculate"
          />
          <p class="text-xs text-gray-500 mt-2">范围：0-100，默认100（无折扣）</p>
        </div>

        <button
          @click="calculate"
          class="w-full py-4 bg-gradient-to-r from-hailong-primary to-hailong-secondary text-white rounded-lg font-bold text-lg hover:shadow-xl transition-all"
        >
          开始计算
        </button>
      </div>

      <!-- 输出区域 -->
      <div class="space-y-6">
        <div class="bg-gradient-to-br from-hailong-primary/5 to-hailong-secondary/5 rounded-xl p-6 border border-hailong-primary/20">
          <h3 class="text-lg font-bold text-hailong-dark mb-4">计算结果</h3>
          
          <div class="space-y-4">
            <div v-if="result.standardFee > 0" class="space-y-2 text-sm mb-4">
              <div class="flex justify-between items-center">
                <span class="text-gray-600">服务类型：</span>
                <span class="font-medium text-gray-800">{{ getServiceTypeLabel() }}</span>
              </div>
              <div class="flex justify-between items-center">
                <span class="text-gray-600">项目金额：</span>
                <span class="font-medium text-gray-800">{{ formData.amount }} 万元</span>
              </div>
              <div v-if="formData.discount < 100" class="flex justify-between items-center">
                <span class="text-gray-600">优惠折扣：</span>
                <span class="font-medium text-gray-800">{{ formData.discount }}%</span>
              </div>
            </div>

            <div class="flex justify-between items-center py-3 border-b border-gray-200">
              <span class="text-gray-700 font-medium">标准收费：</span>
              <span class="text-2xl font-bold text-hailong-primary">{{ formatCurrency(result.standardFee) }}</span>
            </div>
            
            <div class="flex justify-between items-center py-3 bg-green-50 rounded-lg px-3">
              <span class="text-gray-700 font-medium">优惠后收费：</span>
              <span class="text-2xl font-bold text-green-600">{{ formatCurrency(result.discountedFee) }}</span>
            </div>
          </div>
        </div>

        <div v-if="result.report" class="bg-gray-50 rounded-xl p-6 border border-gray-200">
          <div class="flex items-center justify-between mb-4">
            <h3 class="text-lg font-bold text-hailong-dark">计算报告</h3>
            <button
              @click="copyReport"
              class="flex items-center gap-2 px-4 py-2 bg-hailong-primary text-white rounded-lg hover:bg-hailong-secondary transition-colors text-sm font-medium"
            >
              <svg class="w-4 h-4" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M8 16H6a2 2 0 01-2-2V6a2 2 0 012-2h8a2 2 0 012 2v2m-6 12h8a2 2 0 002-2v-8a2 2 0 00-2-2h-8a2 2 0 00-2 2v8a2 2 0 002 2z" />
              </svg>
              一键复制
            </button>
          </div>
          <div class="text-sm text-gray-700 whitespace-pre-wrap leading-relaxed" style="font-family: 'Courier New', Courier, monospace;">{{ result.report }}</div>
        </div>
      </div>
    </div>

    <!-- 页面底部说明 -->
    <div class="mt-8 pt-6 border-t border-gray-200">
      <div class="text-sm text-gray-600 leading-relaxed space-y-2">
        <p><span class="font-semibold text-gray-700">说明：</span></p>
        <ul class="list-disc list-inside space-y-1 ml-4">
          <li>本计算器提供的费用测算结果仅供参考估价，不能作为实际收费依据，实际费用应以双方签订的正式咨询服务合同为准。</li>
          <li>计算结果依据《河南省招标投标协会关于印发&lt;河南省招标代理服务收费指导意见&gt;的通知》（豫招协〔2023〕002号）文件提出的计算方法得出</li>
          <li>实际收费可根据项目具体情况协商确定</li>
        </ul>
      </div>
    </div>
  </div>
</template>

<script setup>
import { reactive } from 'vue'

// 服务类型
const serviceTypes = [
  { label: '工程', value: 'engineering' },
  { label: '货物', value: 'goods' },
  { label: '服务', value: 'service' }
]

// 表单数据
const formData = reactive({
  serviceType: 'engineering',
  amount: null,
  discount: 100
})

// 计算结果
const result = reactive({
  standardFee: 0,
  discountedFee: 0,
  report: ''
})

// 费率配置（分段累进）
const feeRates = [
  { min: 0, max: 100, rate: 1.500 },
  { min: 100, max: 500, rate: 0.800 },
  { min: 500, max: 1000, rate: 0.450 },
  { min: 1000, max: 5000, rate: 0.250 },
  { min: 5000, max: 10000, rate: 0.100 }
]

// 计算费用
const calculate = () => {
  if (!formData.amount || formData.amount <= 0) {
    result.standardFee = 0
    result.discountedFee = 0
    result.report = ''
    return
  }

  let amount = formData.amount
  let totalFee = 0
  let details = []

  // 分段计算
  for (let i = 0; i < feeRates.length; i++) {
    const rate = feeRates[i]
    
    if (amount <= 0) break
    
    if (amount > rate.min) {
      let segmentAmount = 0
      
      if (amount >= rate.max) {
        segmentAmount = rate.max - rate.min
      } else {
        segmentAmount = amount - rate.min
      }
      
      const segmentFee = segmentAmount * 10000 * (rate.rate / 100)
      totalFee += segmentFee
      
      details.push({
        range: `${rate.min}---${rate.max}`,
        amount: segmentAmount,
        rate: rate.rate,
        fee: segmentFee
      })
    }
  }

  // 如果超过10000万元
  if (amount > 10000) {
    details.push({
      range: '10000以上',
      amount: amount - 10000,
      rate: '面议',
      fee: 0
    })
  }

  // 应用折扣
  const discount = Math.max(0, Math.min(100, formData.discount || 100))
  const discountedFee = totalFee * (discount / 100)

  // 生成报告
  const serviceTypeLabel = serviceTypes.find(t => t.value === formData.serviceType)?.label || '服务'
  let report = `计算类型为：${serviceTypeLabel}招标\n`
  report += `中标金额为：${formData.amount}万元\n`
  report += `------------------------------\n`
  
  details.forEach(detail => {
    if (detail.rate !== '面议') {
      report += `${detail.range}：${detail.amount}×${detail.rate}％=${detail.fee.toFixed(2)}元\n`
    }
  })
  
  report += `------------------------------\n`
  report += `各项结果累计得：${totalFee.toFixed(2)}元\n`
  
  if (discount < 100) {
    report += `按${discount}％计算得：${discountedFee.toFixed(2)}元`
  }

  result.standardFee = totalFee
  result.discountedFee = discountedFee
  result.report = report
}

// 获取服务类型标签
const getServiceTypeLabel = () => {
  const type = serviceTypes.find(t => t.value === formData.serviceType)
  return type ? type.label : ''
}

// 格式化货币
const formatCurrency = (value) => {
  if (!value) return '¥0.00'
  return `¥${value.toFixed(2)}`
}

// 复制报告
const copyReport = async () => {
  try {
    await navigator.clipboard.writeText(result.report)
    alert('计算报告已复制到剪贴板！')
  } catch (err) {
    // 降级方案
    const textarea = document.createElement('textarea')
    textarea.value = result.report
    textarea.style.position = 'fixed'
    textarea.style.opacity = '0'
    document.body.appendChild(textarea)
    textarea.select()
    try {
      document.execCommand('copy')
      alert('计算报告已复制到剪贴板！')
    } catch (e) {
      alert('复制失败，请手动复制')
    }
    document.body.removeChild(textarea)
  }
}
</script>

<style scoped>
/* 隐藏数字输入框的上下箭头 */
input[type="number"]::-webkit-inner-spin-button,
input[type="number"]::-webkit-outer-spin-button {
  -webkit-appearance: none;
  margin: 0;
}

input[type="number"] {
  -moz-appearance: textfield;
}
</style>