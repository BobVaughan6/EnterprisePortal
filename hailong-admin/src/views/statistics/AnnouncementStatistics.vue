<template>
  <div class="page-container">
    <!-- 概览卡片 -->
    <el-row :gutter="20" class="overview-cards">
      <el-col :span="6">
        <el-card shadow="hover">
          <div class="stat-card">
            <div class="stat-icon" style="background: #409eff;">
              <el-icon><Document /></el-icon>
            </div>
            <div class="stat-content">
              <div class="stat-value">{{ overview.totalCount }}</div>
              <div class="stat-label">公告总数</div>
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
            <span>发布趋势</span>
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

    <el-row :gutter="20" style="margin-top: 20px;">
      <!-- 区域分布 -->
      <el-col :span="12">
        <el-card>
          <template #header>
            <span>区域分布 TOP 10</span>
          </template>
          <div ref="regionChartRef" style="height: 400px;"></div>
        </el-card>
      </el-col>

      <!-- 状态分布 -->
      <el-col :span="12">
        <el-card>
          <template #header>
            <span>公告状态分布</span>
          </template>
          <div ref="statusChartRef" style="height: 400px;"></div>
        </el-card>
      </el-col>
    </el-row>

    <!-- 时间段分析 -->
    <el-card style="margin-top: 20px;">
      <template #header>
        <span>发布时间段分析</span>
      </template>
      <div ref="timeChartRef" style="height: 300px;"></div>
    </el-card>

    <!-- 热门公告 -->
    <el-card style="margin-top: 20px;">
      <template #header>
        <div class="card-header">
          <span>热门公告 TOP 20</span>
          <el-button type="primary" size="small" icon="Download" @click="exportData" :loading="exportLoading">
            导出统计
          </el-button>
        </div>
      </template>
      <el-table :data="hotAnnouncements" v-loading="loading" border stripe>
        <el-table-column type="index" label="排名" width="80" align="center" />
        <el-table-column prop="title" label="公告标题" min-width="300" show-overflow-tooltip />
        <el-table-column prop="businessType" label="业务类型" width="120" align="center">
          <template #default="{ row }">
            <el-tag :type="row.businessType === 'GOV_PROCUREMENT' ? 'primary' : 'success'">
              {{ row.businessType === 'GOV_PROCUREMENT' ? '政府采购' : '建设工程' }}
            </el-tag>
          </template>
        </el-table-column>
        <el-table-column prop="noticeType" label="公告类型" width="120" align="center" />
        <el-table-column prop="projectRegion" label="项目区域" width="120" align="center" />
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
import { ElMessage } from 'element-plus'
import { Document, Calendar, View, TrendCharts } from '@element-plus/icons-vue'
import { statisticsApi } from '@/api'
import * as echarts from 'echarts'
import { getDoughnutChartOption, getHorizontalBarChartOption } from '@/utils/chartOptions'

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
let regionChart = null
let statusChart = null
let timeChart = null

const trendChartRef = ref(null)
const typeChartRef = ref(null)
const regionChartRef = ref(null)
const statusChartRef = ref(null)
const timeChartRef = ref(null)

// 热门公告
const hotAnnouncements = ref([])
const loading = ref(false)
const exportLoading = ref(false)

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
    const res = await statisticsApi.announcement.getOverview()
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
    const res = await statisticsApi.announcement.getTrend({ days: 30 })
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
      data: ['政府采购', '建设工程']
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
        name: '政府采购',
        type: 'line',
        data: data.govProcurement || [],
        smooth: true,
        itemStyle: { color: '#409eff' }
      },
      {
        name: '建设工程',
        type: 'line',
        data: data.construction || [],
        smooth: true,
        itemStyle: { color: '#67c23a' }
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
    const res = await statisticsApi.announcement.getTypeDistribution()
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
        name: '公告类型',
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
 * 加载区域分布
 */
const loadRegionDistribution = async () => {
  try {
    const res = await statisticsApi.announcement.getRegionDistribution({ limit: 10 })
    if (res.success && res.data) {
      renderRegionChart(res.data)
    }
  } catch (error) {
    console.error('加载区域分布失败:', error)
  }
}

/**
 * 渲染区域分布图表
 */
const renderRegionChart = (data) => {
  if (!regionChart) {
    regionChart = echarts.init(regionChartRef.value)
  }
  
  const option = {
    tooltip: {
      trigger: 'axis',
      axisPointer: {
        type: 'shadow'
      }
    },
    grid: {
      left: '3%',
      right: '4%',
      bottom: '3%',
      containLabel: true
    },
    xAxis: {
      type: 'value'
    },
    yAxis: {
      type: 'category',
      data: data.regions || []
    },
    series: [
      {
        name: '公告数量',
        type: 'bar',
        data: data.counts || [],
        itemStyle: {
          color: new echarts.graphic.LinearGradient(0, 0, 1, 0, [
            { offset: 0, color: '#83bff6' },
            { offset: 0.5, color: '#188df0' },
            { offset: 1, color: '#188df0' }
          ])
        }
      }
    ]
  }
  
  regionChart.setOption(option)
}

/**
 * 加载热门公告
 */
const loadHotAnnouncements = async () => {
  loading.value = true
  try {
    const res = await statisticsApi.announcement.getHotAnnouncements({ limit: 20 })
    if (res.success && res.data) {
      hotAnnouncements.value = res.data
    }
  } catch (error) {
    console.error('加载热门公告失败:', error)
  } finally {
    loading.value = false
  }
}

/**
 * 加载状态分布
 */
const loadStatusDistribution = async () => {
  try {
    const res = await statisticsApi.announcement.getStatusDistribution()
    if (res.success && res.data) {
      renderStatusChart(res.data)
    }
  } catch (error) {
    console.error('加载状态分布失败:', error)
  }
}

/**
 * 渲染状态分布图表
 */
const renderStatusChart = (data) => {
  if (!statusChart) {
    statusChart = echarts.init(statusChartRef.value)
  }
  
  const chartData = data.map(item => ({
    name: item.status,
    value: item.count
  }))
  
  const option = getDoughnutChartOption(chartData, {
    radius: ['40%', '70%'],
    center: ['50%', '50%']
  })
  
  statusChart.setOption(option)
}

/**
 * 加载时间段分析
 */
const loadTimeAnalysis = async () => {
  try {
    const res = await statisticsApi.announcement.getTimeAnalysis()
    if (res.success && res.data) {
      renderTimeChart(res.data)
    }
  } catch (error) {
    console.error('加载时间段分析失败:', error)
  }
}

/**
 * 渲染时间段分析图表
 */
const renderTimeChart = (data) => {
  if (!timeChart) {
    timeChart = echarts.init(timeChartRef.value)
  }
  
  const hours = data.map(item => `${item.hour}:00`)
  const counts = data.map(item => item.count)
  
  const option = {
    tooltip: {
      trigger: 'axis',
      axisPointer: {
        type: 'shadow'
      }
    },
    grid: {
      left: '3%',
      right: '4%',
      bottom: '3%',
      top: '10%',
      containLabel: true
    },
    xAxis: {
      type: 'category',
      data: hours,
      axisLabel: {
        interval: 1,
        rotate: 0
      }
    },
    yAxis: {
      type: 'value',
      name: '发布数量'
    },
    series: [
      {
        name: '发布数量',
        type: 'bar',
        data: counts,
        itemStyle: {
          color: new echarts.graphic.LinearGradient(0, 0, 0, 1, [
            { offset: 0, color: '#83bff6' },
            { offset: 0.5, color: '#188df0' },
            { offset: 1, color: '#188df0' }
          ]),
          borderRadius: [5, 5, 0, 0]
        },
        barWidth: '60%'
      }
    ]
  }
  
  timeChart.setOption(option)
}

/**
 * 导出统计数据
 */
const exportData = async () => {
  exportLoading.value = true
  try {
    const res = await statisticsApi.announcement.export()
    
    // 创建下载链接
    const blob = new Blob([res], { type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' })
    const url = window.URL.createObjectURL(blob)
    const link = document.createElement('a')
    link.href = url
    link.download = `公告统计_${new Date().getTime()}.xlsx`
    document.body.appendChild(link)
    link.click()
    document.body.removeChild(link)
    window.URL.revokeObjectURL(url)
    
    ElMessage.success('导出成功')
  } catch (error) {
    console.error('导出失败:', error)
    ElMessage.error('导出失败')
  } finally {
    exportLoading.value = false
  }
}

/**
 * 窗口大小改变时重绘图表
 */
const handleResize = () => {
  if (trendChart) trendChart.resize()
  if (typeChart) typeChart.resize()
  if (regionChart) regionChart.resize()
  if (statusChart) statusChart.resize()
  if (timeChart) timeChart.resize()
}

onMounted(() => {
  loadOverview()
  loadTrendData()
  loadTypeDistribution()
  loadRegionDistribution()
  loadStatusDistribution()
  loadTimeAnalysis()
  loadHotAnnouncements()
  window.addEventListener('resize', handleResize)
})

onUnmounted(() => {
  if (trendChart) trendChart.dispose()
  if (typeChart) typeChart.dispose()
  if (regionChart) regionChart.dispose()
  if (statusChart) statusChart.dispose()
  if (timeChart) timeChart.dispose()
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