<template>
  <div class="page-container">
    <!-- 概览卡片 -->
    <el-row :gutter="20" class="overview-cards">
      <el-col :span="6">
        <el-card shadow="hover">
          <div class="stat-card">
            <div class="stat-icon" style="background: #409eff;">
              <el-icon><DocumentCopy /></el-icon>
            </div>
            <div class="stat-content">
              <div class="stat-value">{{ overview.totalCount }}</div>
              <div class="stat-label">信息总数</div>
            </div>
          </div>
        </el-card>
      </el-col>
      <el-col :span="6">
        <el-card shadow="hover">
          <div class="stat-card">
            <div class="stat-icon" style="background: #67c23a;">
              <el-icon><Calendar /></el-icon>
            </div>
            <div class="stat-content">
              <div class="stat-value">{{ overview.todayCount }}</div>
              <div class="stat-label">今日发布</div>
            </div>
          </div>
        </el-card>
      </el-col>
      <el-col :span="6">
        <el-card shadow="hover">
          <div class="stat-card">
            <div class="stat-icon" style="background: #e6a23c;">
              <el-icon><View /></el-icon>
            </div>
            <div class="stat-content">
              <div class="stat-value">{{ overview.totalViews }}</div>
              <div class="stat-label">总浏览量</div>
            </div>
          </div>
        </el-card>
      </el-col>
      <el-col :span="6">
        <el-card shadow="hover">
          <div class="stat-card">
            <div class="stat-icon" style="background: #f56c6c;">
              <el-icon><TrendCharts /></el-icon>
            </div>
            <div class="stat-content">
              <div class="stat-value">{{ overview.avgViews }}</div>
              <div class="stat-label">平均浏览量</div>
            </div>
          </div>
        </el-card>
      </el-col>
    </el-row>

    <el-row :gutter="20">
      <!-- 发布趋势 -->
      <el-col :span="12">
        <el-card class="chart-card">
          <template #header>
            <span>发布趋势（最近30天）</span>
          </template>
          <div ref="trendChartRef" style="height: 350px;"></div>
        </el-card>
      </el-col>

      <!-- 类型分布 -->
      <el-col :span="12">
        <el-card class="chart-card">
          <template #header>
            <span>类型分布</span>
          </template>
          <div ref="typeChartRef" style="height: 350px;"></div>
        </el-card>
      </el-col>
    </el-row>

    <!-- 热门信息 -->
    <el-card style="margin-top: 20px;">
      <template #header>
        <div class="card-header">
          <span>热门信息 TOP 20</span>
          <el-radio-group v-model="hotInfoType" size="small" @change="loadHotInfo">
            <el-radio-button label="">全部</el-radio-button>
            <el-radio-button label="COMPANY_NEWS">公司公告</el-radio-button>
            <el-radio-button label="POLICY_REGULATION">政策法规</el-radio-button>
            <el-radio-button label="POLICY_INFO">政策信息</el-radio-button>
            <el-radio-button label="NOTICE">通知公告</el-radio-button>
          </el-radio-group>
        </div>
      </template>
      <el-table :data="hotInfo" v-loading="loading" border stripe>
        <el-table-column type="index" label="排名" width="80" align="center" />
        <el-table-column prop="title" label="信息标题" min-width="300" show-overflow-tooltip />
        <el-table-column prop="type" label="信息类型" width="120" align="center">
          <template #default="{ row }">
            <el-tag :type="getTypeColor(row.type)">
              {{ getTypeName(row.type) }}
            </el-tag>
          </template>
        </el-table-column>
        <el-table-column prop="author" label="作者" width="120" align="center" />
        <el-table-column prop="viewCount" label="浏览量" width="100" align="center" sortable />
        <el-table-column prop="publishTime" label="发布时间" width="110" align="center">
          <template #default="{ row }">
            {{ formatDate(row.publishTime) }}
          </template>
        </el-table-column>
      </el-table>
    </el-card>
  </div>
</template>

<script setup>
import { ref, reactive, onMounted, onUnmounted } from 'vue'
import { DocumentCopy, Calendar, View, TrendCharts } from '@element-plus/icons-vue'
import { statisticsApi } from '@/api'
import * as echarts from 'echarts'

// 概览数据
const overview = reactive({
  totalCount: 0,
  todayCount: 0,
  totalViews: 0,
  avgViews: 0
})

// 图表实例
let trendChart = null
let typeChart = null

const trendChartRef = ref(null)
const typeChartRef = ref(null)

// 热门信息
const hotInfo = ref([])
const loading = ref(false)
const hotInfoType = ref('')

// 类型映射
const typeNameMap = {
  'COMPANY_NEWS': '公司公告',
  'POLICY_REGULATION': '政策法规',
  'POLICY_INFO': '政策信息',
  'NOTICE': '通知公告'
}

const typeColorMap = {
  'COMPANY_NEWS': 'primary',
  'POLICY_REGULATION': 'success',
  'POLICY_INFO': 'warning',
  'NOTICE': 'danger'
}

/**
 * 获取类型名称
 */
const getTypeName = (type) => {
  return typeNameMap[type] || type
}

/**
 * 获取类型颜色
 */
const getTypeColor = (type) => {
  return typeColorMap[type] || 'info'
}

/**
 * 格式化日期
 */
const formatDate = (dateStr) => {
  if (!dateStr) return '-'
  return new Date(dateStr).toLocaleDateString('zh-CN')
}

/**
 * 加载概览数据
 */
const loadOverview = async () => {
  try {
    const res = await statisticsApi.infoPublication.getOverview()
    if (res.success && res.data) {
      Object.assign(overview, res.data)
    }
  } catch (error) {
    console.error('加载概览数据失败:', error)
  }
}

/**
 * 加载发布趋势
 */
const loadTrendData = async () => {
  try {
    const res = await statisticsApi.infoPublication.getTrend({ days: 30 })
    if (res.success && res.data) {
      renderTrendChart(res.data)
    }
  } catch (error) {
    console.error('加载趋势数据失败:', error)
  }
}

/**
 * 渲染发布趋势图表
 */
const renderTrendChart = (data) => {
  if (!trendChart) {
    trendChart = echarts.init(trendChartRef.value)
  }
  
  const option = {
    tooltip: {
      trigger: 'axis'
    },
    legend: {
      data: ['公司公告', '政策法规', '政策信息', '通知公告']
    },
    xAxis: {
      type: 'category',
      data: data.dates || []
    },
    yAxis: {
      type: 'value'
    },
    series: [
      {
        name: '公司公告',
        type: 'line',
        data: data.companyNews || [],
        smooth: true,
        itemStyle: { color: '#409eff' }
      },
      {
        name: '政策法规',
        type: 'line',
        data: data.policyRegulation || [],
        smooth: true,
        itemStyle: { color: '#67c23a' }
      },
      {
        name: '政策信息',
        type: 'line',
        data: data.policyInfo || [],
        smooth: true,
        itemStyle: { color: '#e6a23c' }
      },
      {
        name: '通知公告',
        type: 'line',
        data: data.notice || [],
        smooth: true,
        itemStyle: { color: '#f56c6c' }
      }
    ]
  }
  
  trendChart.setOption(option)
}

/**
 * 加载类型分布
 */
const loadTypeDistribution = async () => {
  try {
    const res = await statisticsApi.infoPublication.getTypeDistribution()
    if (res.success && res.data) {
      renderTypeChart(res.data)
    }
  } catch (error) {
    console.error('加载类型分布失败:', error)
  }
}

/**
 * 渲染类型分布图表
 */
const renderTypeChart = (data) => {
  if (!typeChart) {
    typeChart = echarts.init(typeChartRef.value)
  }
  
  const option = {
    tooltip: {
      trigger: 'item',
      formatter: '{a} <br/>{b}: {c} ({d}%)'
    },
    legend: {
      orient: 'vertical',
      right: 10,
      top: 'center'
    },
    series: [
      {
        name: '信息类型',
        type: 'pie',
        radius: ['40%', '70%'],
        avoidLabelOverlap: false,
        itemStyle: {
          borderRadius: 10,
          borderColor: '#fff',
          borderWidth: 2
        },
        label: {
          show: false,
          position: 'center'
        },
        emphasis: {
          label: {
            show: true,
            fontSize: 20,
            fontWeight: 'bold'
          }
        },
        labelLine: {
          show: false
        },
        data: data || []
      }
    ]
  }
  
  typeChart.setOption(option)
}

/**
 * 加载热门信息
 */
const loadHotInfo = async () => {
  loading.value = true
  try {
    const params = {
      type: hotInfoType.value || undefined,
      limit: 20
    }
    const res = await statisticsApi.infoPublication.getHotInfo(params)
    if (res.success && res.data) {
      hotInfo.value = res.data
    }
  } catch (error) {
    console.error('加载热门信息失败:', error)
  } finally {
    loading.value = false
  }
}

/**
 * 窗口大小改变时重绘图表
 */
const handleResize = () => {
  if (trendChart) trendChart.resize()
  if (typeChart) typeChart.resize()
}

onMounted(() => {
  loadOverview()
  loadTrendData()
  loadTypeDistribution()
  loadHotInfo()
  window.addEventListener('resize', handleResize)
})

onUnmounted(() => {
  if (trendChart) trendChart.dispose()
  if (typeChart) typeChart.dispose()
  window.removeEventListener('resize', handleResize)
})
</script>

<style scoped>
.page-container {
  padding: 0;
}

.overview-cards {
  margin-bottom: 20px;
}

.stat-card {
  display: flex;
  align-items: center;
}

.stat-icon {
  width: 60px;
  height: 60px;
  border-radius: 8px;
  display: flex;
  align-items: center;
  justify-content: center;
  font-size: 28px;
  color: white;
  margin-right: 15px;
}

.stat-content {
  flex: 1;
}

.stat-value {
  font-size: 24px;
  font-weight: bold;
  color: #303133;
  margin-bottom: 5px;
}

.stat-label {
  font-size: 14px;
  color: #909399;
}

.chart-card {
  margin-bottom: 20px;
}

.card-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
}
</style>