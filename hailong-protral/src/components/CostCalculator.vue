<template>
  <div class="bg-white rounded-xl shadow-2xl p-8 border border-hailong-primary/20">
    <div class="flex items-center justify-between mb-6">
      <h2 class="text-3xl font-bold text-hailong-dark flex items-center gap-3">
        <svg class="w-8 h-8 text-hailong-primary" fill="none" stroke="currentColor" viewBox="0 0 24 24">
          <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M12 8c-1.657 0-3 .895-3 2s1.343 2 3 2 3 .895 3 2-1.343 2-3 2m0-8c1.11 0 2.08.402 2.599 1M12 8V7m0 1v8m0 0v1m0-1c-1.11 0-2.08-.402-2.599-1M21 12a9 9 0 11-18 0 9 9 0 0118 0z" />
        </svg>
        造价费用计算工具
      </h2>
      <button @click="$emit('close')" class="text-gray-400 hover:text-gray-600 transition-colors">
        <svg class="w-6 h-6" fill="none" stroke="currentColor" viewBox="0 0 24 24">
          <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M6 18L18 6M6 6l12 12" />
        </svg>
      </button>
    </div>

    <div class="grid grid-cols-1 lg:grid-cols-2 gap-8">
      <!-- 输入区域 -->
      <div class="space-y-6">
        <div>
          <label class="block text-sm font-semibold text-gray-700 mb-3">咨询项目类型</label>
          <select v-model="formData.projectType" @change="calculate" class="w-full px-4 py-3 border-2 border-gray-200 rounded-lg focus:border-hailong-primary focus:outline-none transition-colors text-base">
            <option value="">请选择咨询项目类型</option>
            <option v-for="type in consultingTypes" :key="type.value" :value="type.value">{{ type.label }}</option>
          </select>
        </div>

        <div>
          <label class="block text-sm font-semibold text-gray-700 mb-3">工程专业类型</label>
          <select v-model="formData.engineeringType" @change="calculate" class="w-full px-4 py-3 border-2 border-gray-200 rounded-lg focus:border-hailong-primary focus:outline-none transition-colors text-base">
            <option value="">请选择工程专业类型</option>
            <option v-for="type in engineeringTypes" :key="type.value" :value="type.value">{{ type.label }}（系数 {{ type.coefficient }}）</option>
          </select>
        </div>

        <div>
          <label class="block text-sm font-semibold text-gray-700 mb-3">{{ getAmountLabel() }}</label>
          <input v-model.number="formData.amount" type="number" step="0.01" min="0" placeholder="请输入金额" class="w-full px-4 py-3 border-2 border-gray-200 rounded-lg focus:border-hailong-primary focus:outline-none transition-colors text-lg" @input="calculate" />
          <p class="text-xs text-gray-500 mt-2">单位：万元</p>
        </div>

        <div v-if="formData.projectType === 'list_preparation'" class="flex items-center gap-3">
          <input type="checkbox" id="onlyList" v-model="formData.onlyList" @change="calculate" class="w-5 h-5 text-hailong-primary border-gray-300 rounded focus:ring-hailong-primary" />
          <label for="onlyList" class="text-sm text-gray-700 cursor-pointer">仅编制工程量清单（按60%计算）</label>
        </div>

        <div v-if="formData.projectType === 'settlement_audit'">
          <label class="block text-sm font-semibold text-gray-700 mb-3">审核类型</label>
          <div class="grid grid-cols-2 gap-3">
            <button @click="formData.auditType = 'first'; calculate()" :class="['py-3 px-4 rounded-lg font-medium transition-all border-2', formData.auditType === 'first' ? 'bg-gradient-to-r from-hailong-primary to-hailong-secondary text-white border-hailong-primary shadow-lg' : 'bg-white text-gray-700 border-gray-200 hover:border-hailong-primary']">一审</button>
            <button @click="formData.auditType = 'second'; calculate()" :class="['py-3 px-4 rounded-lg font-medium transition-all border-2', formData.auditType === 'second' ? 'bg-gradient-to-r from-hailong-primary to-hailong-secondary text-white border-hailong-primary shadow-lg' : 'bg-white text-gray-700 border-gray-200 hover:border-hailong-primary']">二审</button>
          </div>
        </div>

        <div v-if="formData.projectType === 'dispute_appraisal'">
          <label class="block text-sm font-semibold text-gray-700 mb-3">现场踏勘天数</label>
          <input v-model.number="formData.inspectionDays" type="number" step="1" min="0" placeholder="请输入天数" class="w-full px-4 py-3 border-2 border-gray-200 rounded-lg focus:border-hailong-primary focus:outline-none transition-colors text-lg" @input="calculate" />
          <p class="text-xs text-gray-500 mt-2">现场踏勘费：3000-5000元/天</p>
        </div>

        <div v-if="showReductionFields()">
          <label class="block text-sm font-semibold text-gray-700 mb-3">核减额（万元）</label>
          <input v-model.number="formData.reductionAmount" type="number" step="0.01" min="0" placeholder="请输入核减额" class="w-full px-4 py-3 border-2 border-gray-200 rounded-lg focus:border-hailong-primary focus:outline-none transition-colors text-lg" @input="calculate" />
        </div>

        <div v-if="showReductionFields()">
          <label class="block text-sm font-semibold text-gray-700 mb-3">核增额（万元）</label>
          <input v-model.number="formData.increaseAmount" type="number" step="0.01" min="0" placeholder="请输入核增额" class="w-full px-4 py-3 border-2 border-gray-200 rounded-lg focus:border-hailong-primary focus:outline-none transition-colors text-lg" @input="calculate" />
        </div>

        <div>
          <label class="block text-sm font-semibold text-gray-700 mb-3">优惠折扣（%）</label>
          <input v-model.number="formData.discount" type="number" step="1" min="0" max="100" placeholder="请输入折扣比例" class="w-full px-4 py-3 border-2 border-gray-200 rounded-lg focus:border-hailong-primary focus:outline-none transition-colors text-lg" @input="calculate" />
          <p class="text-xs text-gray-500 mt-2">范围：0-100，默认100（无折扣）</p>
        </div>

        <button @click="calculate" class="w-full py-4 bg-gradient-to-r from-hailong-primary to-hailong-secondary text-white rounded-lg font-bold text-lg hover:shadow-xl transition-all">开始计算</button>
      </div>

      <!-- 输出区域 -->
      <div class="space-y-6">
        <div class="bg-gradient-to-br from-hailong-primary/5 to-hailong-secondary/5 rounded-xl p-6 border border-hailong-primary/20">
          <h3 class="text-lg font-bold text-hailong-dark mb-4">计算结果</h3>
          <div class="space-y-3">
            <div class="flex justify-between items-center py-2 border-b border-gray-200">
              <span class="text-gray-700 text-sm">基本收费：</span>
              <span class="text-lg font-bold text-gray-800">{{ formatCurrency(result.basicFee) }}</span>
            </div>
            <div class="flex justify-between items-center py-2 border-b border-gray-200">
              <span class="text-gray-700 text-sm">专业调整后：</span>
              <span class="text-lg font-bold text-hailong-primary">{{ formatCurrency(result.adjustedFee) }}</span>
            </div>
            <div v-if="result.reductionFee > 0" class="flex justify-between items-center py-2 border-b border-gray-200">
              <span class="text-gray-700 text-sm">核减核增费：</span>
              <span class="text-lg font-bold text-orange-600">{{ formatCurrency(result.reductionFee) }}</span>
            </div>
            <div v-if="result.otherFee > 0" class="flex justify-between items-center py-2 border-b border-gray-200">
              <span class="text-gray-700 text-sm">其他费用：</span>
              <span class="text-lg font-bold text-purple-600">{{ formatCurrency(result.otherFee) }}</span>
            </div>
            <div class="flex justify-between items-center py-2 border-b border-gray-200">
              <span class="text-gray-700 text-sm">小计：</span>
              <span class="text-xl font-bold text-gray-900">{{ formatCurrency(result.subtotal) }}</span>
            </div>
            <div class="flex justify-between items-center py-3 bg-green-50 rounded-lg px-3">
              <span class="text-gray-700 font-medium">优惠后收费：</span>
              <span class="text-2xl font-bold text-green-600">{{ formatCurrency(result.finalFee) }}</span>
            </div>
          </div>
        </div>

        <div v-if="result.report" class="bg-gray-50 rounded-xl p-6 border border-gray-200">
          <div class="flex items-center justify-between mb-4">
            <h3 class="text-lg font-bold text-hailong-dark">详细计算报告</h3>
            <button @click="copyReport" class="flex items-center gap-2 px-4 py-2 bg-hailong-primary text-white rounded-lg hover:bg-hailong-secondary transition-colors text-sm font-medium">
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
          <li>本计算器采用差额定率累进法计费</li>
          <li>造价咨询服务费不足3000元时，按3000元收取</li>
          <li>工程主材、设备均应计入计费基数</li>
          <li>计算结果依据《河南省建设工程造价咨询行业服务收费市场参考价格》，仅供参考</li>
          <li>实际收费可根据项目具体情况协商确定</li>
        </ul>
      </div>
    </div>
  </div>
</template>

<script setup>
import { reactive } from 'vue'

const consultingTypes = [
  { label: '投资估算编制或审核', value: 'investment_estimate' },
  { label: '设计概算编制或审核', value: 'design_estimate' },
  { label: '施工图预算编制或审核', value: 'construction_budget' },
  { label: '工程量清单及招标控制价编制', value: 'list_preparation' },
  { label: '工程量清单及招标控制价审核', value: 'list_audit' },
  { label: '施工阶段造价咨询', value: 'construction_consulting' },
  { label: '工程结算编制', value: 'settlement_preparation' },
  { label: '工程结算审核', value: 'settlement_audit' },
  { label: '工程竣工决算编制', value: 'final_account_preparation' },
  { label: '工程竣工决算专项审核', value: 'final_account_audit' },
  { label: '方案测算/比选', value: 'scheme_comparison' },
  { label: '后评估', value: 'post_evaluation' },
  { label: '工程造价纠纷鉴定/鉴证', value: 'dispute_appraisal' }
]

const engineeringTypes = [
  { label: '房屋建筑工程', value: 'building', coefficient: 1.0 },
  { label: '市政工程', value: 'municipal', coefficient: 0.8 },
  { label: '水利电力工程', value: 'water_power', coefficient: 1.0 },
  { label: '机场场道工程', value: 'airport', coefficient: 1.0 },
  { label: '公路道路工程', value: 'highway', coefficient: 0.8 },
  { label: '城市轨道工程', value: 'rail', coefficient: 0.8 },
  { label: '桥梁隧道工程', value: 'bridge_tunnel', coefficient: 1.0 },
  { label: '港口工程', value: 'port', coefficient: 0.8 },
  { label: '井巷矿山工程', value: 'mine', coefficient: 1.1 },
  { label: '园林绿化工程', value: 'landscape', coefficient: 1.1 },
  { label: '装饰装修工程', value: 'decoration', coefficient: 1.2 },
  { label: '仿古建筑工程', value: 'antique', coefficient: 1.2 },
  { label: '安装工程', value: 'installation', coefficient: 1.3 },
  { label: '其他工程', value: 'other', coefficient: 1.0 }
]

const feeRatesTable = {
  investment_estimate: [{ max: 500, rate: 1.5 }, { max: 1000, rate: 1.2 }, { max: 5000, rate: 1.0 }, { max: 10000, rate: 0.8 }, { max: 50000, rate: 0.6 }, { max: Infinity, rate: 0.5 }],
  design_estimate: [{ max: 500, rate: 3.0 }, { max: 1000, rate: 2.5 }, { max: 5000, rate: 2.2 }, { max: 10000, rate: 2.0 }, { max: 50000, rate: 1.6 }, { max: Infinity, rate: 1.5 }],
  construction_budget: [{ max: 500, rate: 4.0 }, { max: 1000, rate: 3.7 }, { max: 5000, rate: 3.4 }, { max: 10000, rate: 2.9 }, { max: 50000, rate: 2.6 }, { max: Infinity, rate: 2.2 }],
  list_preparation: [{ max: 500, rate: 5.0 }, { max: 1000, rate: 4.6 }, { max: 5000, rate: 4.2 }, { max: 10000, rate: 3.6 }, { max: 50000, rate: 3.2 }, { max: Infinity, rate: 2.8 }],
  list_audit: [{ max: 500, rate: 4.0 }, { max: 1000, rate: 3.7 }, { max: 5000, rate: 3.4 }, { max: 10000, rate: 2.9 }, { max: 50000, rate: 2.6 }, { max: Infinity, rate: 2.2 }],
  construction_consulting: [{ max: 500, rate: 10.0 }, { max: 1000, rate: 9.0 }, { max: 5000, rate: 8.0 }, { max: 10000, rate: 7.0 }, { max: 50000, rate: 5.6 }, { max: Infinity, rate: 4.0 }],
  settlement_preparation: [{ max: 500, rate: 5.0 }, { max: 1000, rate: 4.5 }, { max: 5000, rate: 4.0 }, { max: 10000, rate: 3.5 }, { max: 50000, rate: 3.2 }, { max: Infinity, rate: 2.8 }],
  settlement_audit: [{ max: 500, rate: 3.6 }, { max: 1000, rate: 3.0 }, { max: 5000, rate: 2.5 }, { max: 10000, rate: 2.0 }, { max: 50000, rate: 1.5 }, { max: Infinity, rate: 1.2 }],
  final_account_preparation: [{ max: 1000, rate: 4.0 }, { max: 10000, rate: 3.0 }, { max: Infinity, rate: 2.0 }],
  final_account_audit: [{ max: 1000, rate: 3.0 }, { max: 10000, rate: 2.0 }, { max: Infinity, rate: 1.0 }],
  scheme_comparison: [{ max: 500, rate: 3.0 }, { max: 1000, rate: 2.5 }, { max: 5000, rate: 2.2 }, { max: 10000, rate: 2.0 }, { max: 50000, rate: 1.6 }, { max: Infinity, rate: 1.5 }],
  post_evaluation: [{ max: 500, rate: 2.5 }, { max: 1000, rate: 2.2 }, { max: 5000, rate: 1.9 }, { max: 10000, rate: 1.7 }, { max: 50000, rate: 1.5 }, { max: Infinity, rate: 1.3 }],
  dispute_appraisal: [{ max: 500, rate: 10.0 }, { max: 1000, rate: 9.0 }, { max: 5000, rate: 8.0 }, { max: 10000, rate: 8.0 }, { max: 50000, rate: 8.0 }, { max: Infinity, rate: 8.0 }]
}

const formData = reactive({
  projectType: '',
  engineeringType: '',
  amount: null,
  onlyList: false,
  auditType: 'first',
  inspectionDays: 0,
  reductionAmount: 0,
  increaseAmount: 0,
  discount: 100
})

const result = reactive({
  basicFee: 0,
  adjustedFee: 0,
  reductionFee: 0,
  otherFee: 0,
  subtotal: 0,
  finalFee: 0,
  report: ''
})

const getAmountLabel = () => {
  const labels = {
    investment_estimate: '总投资（万元）',
    design_estimate: '概算价（万元）',
    construction_budget: '预算价（万元）',
    list_preparation: '招标控制价（万元）',
    list_audit: '招标控制价（万元）',
    construction_consulting: '合同价（万元）',
    settlement_preparation: '送审价（万元）',
    settlement_audit: '送审价（万元）',
    final_account_preparation: '决算价（万元）',
    final_account_audit: '决算价（万元）',
    scheme_comparison: '方案金额（万元）',
    post_evaluation: '项目金额（万元）',
    dispute_appraisal: '争议金额（万元）'
  }
  return labels[formData.projectType] || '计费基数（万元）'
}

const showReductionFields = () => {
  return ['list_audit', 'construction_consulting', 'settlement_audit'].includes(formData.projectType)
}

const calculate = () => {
  if (!formData.projectType || !formData.engineeringType || !formData.amount || formData.amount <= 0) {
    result.basicFee = 0
    result.adjustedFee = 0
    result.reductionFee = 0
    result.otherFee = 0
    result.subtotal = 0
    result.finalFee = 0
    result.report = ''
    return
  }

  let basicFee = 0
  const amount = formData.amount
  const rates = feeRatesTable[formData.projectType]
  
  if (!rates) return

  let remainingAmount = amount
  let prevMax = 0
  let details = []

  for (const rateItem of rates) {
    if (remainingAmount <= 0) break
    const segmentMax = rateItem.max === Infinity ? remainingAmount + prevMax : rateItem.max
    const segmentAmount = Math.min(remainingAmount, segmentMax - prevMax)
    
    if (segmentAmount > 0) {
      const segmentFee = segmentAmount * 10000 * (rateItem.rate / 1000)
      basicFee += segmentFee
      details.push({
        range: `${prevMax}---${rateItem.max === Infinity ? '以上' : rateItem.max}`,
        amount: segmentAmount,
        rate: rateItem.rate,
        fee: segmentFee
      })
      remainingAmount -= segmentAmount
      prevMax = segmentMax
    }
  }

  if (basicFee < 3000) basicFee = 3000
  if (formData.onlyList && formData.projectType === 'list_preparation') basicFee *= 0.6

  const engineeringType = engineeringTypes.find(t => t.value === formData.engineeringType)
  const coefficient = engineeringType?.coefficient || 1.0
  const adjustedFee = basicFee * coefficient

  let reductionFee = 0
  if (showReductionFields()) {
    const totalReduction = (formData.reductionAmount || 0) + (formData.increaseAmount || 0)
    if (totalReduction > 0) {
      if (formData.projectType === 'list_audit') {
        reductionFee = totalReduction * 10000 * 0.04
      } else if (formData.projectType === 'construction_consulting') {
        reductionFee = totalReduction * 10000 * 0.07
      } else if (formData.projectType === 'settlement_audit') {
        reductionFee = totalReduction * 10000 * (formData.auditType === 'first' ? 0.07 : 0.10)
      }
    }
  }

  let otherFee = 0
  if (formData.projectType === 'dispute_appraisal' && formData.inspectionDays > 0) {
    otherFee = formData.inspectionDays * 4000
  }

  const subtotal = adjustedFee + reductionFee + otherFee
  const discount = Math.max(0, Math.min(100, formData.discount || 100))
  const finalFee = subtotal * (discount / 100)

  const projectTypeLabel = consultingTypes.find(t => t.value === formData.projectType)?.label || ''
  const engineeringTypeLabel = engineeringTypes.find(t => t.value === formData.engineeringType)?.label || ''
  
  let report = `咨询项目：${projectTypeLabel}\n`
  report += `工程类型：${engineeringTypeLabel}（调整系数${coefficient}）\n`
  report += `计费基数：${amount}万元\n`
  report += `------------------------------\n`
  report += `基本收费计算（差额定率累进）：\n`
  
  details.forEach(detail => {
    report += `${detail.range}：${detail.amount}×${detail.rate}‰=${detail.fee.toFixed(2)}元\n`
  })
  
  report += `------------------------------\n`
  report += `基本收费小计：${basicFee.toFixed(2)}元\n`
  if (formData.onlyList) report += `仅编制工程量清单（60%）\n`
  report += `专业调整系数：${coefficient}\n`
  report += `调整后收费：${adjustedFee.toFixed(2)}元\n`
  if (reductionFee > 0) report += `核减核增费：${reductionFee.toFixed(2)}元\n`
  if (otherFee > 0) report += `其他费用：${otherFee.toFixed(2)}元\n`
  report += `------------------------------\n`
  if (discount < 100) report += `最终收费（应用${discount}%折扣）：${finalFee.toFixed(2)}元`
  else report += `最终收费：${finalFee.toFixed(2)}元`

  result.basicFee = basicFee
  result.adjustedFee = adjustedFee
  result.reductionFee = reductionFee
  result.otherFee = otherFee
  result.subtotal = subtotal
  result.finalFee = finalFee
  result.report = report
}

const formatCurrency = (value) => {
  if (!value) return '¥0.00'
  return `¥${value.toFixed(2)}`
}

const copyReport = async () => {
  try {
    await navigator.clipboard.writeText(result.report)
    alert('计算报告已复制到剪贴板！')
  } catch (err) {
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
input[type="number"]::-webkit-inner-spin-button,
input[type="number"]::-webkit-outer-spin-button {
  -webkit-appearance: none;
  margin: 0;
}

input[type="number"] {
  -moz-appearance: textfield;
}
</style>