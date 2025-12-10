<template>
  <div class="page-container">
    <!-- 概览卡片 -->
    <el-row :gutter="20" class="overview-cards">
      <el-col :span="6">
        <el-card shadow="hover">
          <div class="stat-card">
            <div class="stat-icon" style="background: #409eff;">
              <el-icon><View /></el-icon>
            </div>
            <div class="stat-content">
              <div class="stat-value">{{ overview.totalVisits }}</div>
              <div class="stat-label">总访问量</div>
            </div>
          </div>
        </el-card>
      </el-col>
      <el-col :span="6">
        <el-card shadow="hover">
          <div class="stat-card">
            <div class="stat-icon" style="background: #67c23a;">
              <el-icon><User /></el-icon>
            </div>
            <div class="stat-content">
              <div class="stat-value">{{ overview.uniqueVisitors }}</div>
              <div class="stat-label">独立访客</div>
            </div>
          </div>
        </el-card>
      </el-col>
      <el-col :span="6">
        <el-card shadow="hover">
          <div class="stat-card">
            <div class="stat-icon" style="background: #e6a23c;">
              <el-icon><Calendar /></el-icon>
            </div>
            <div class="stat-content">
              <div class="stat-value">{{ overview.todayVisits }}</div>
              <div class="stat-label">今日访问</div>
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
              <div class="stat-value">{{ overview.avgDuration }}s</div>
              <div class="stat-label">平均停留时间</div>
            </div>
          </div>
        </el-card>
      </el-col>
    </el-row>

    <!-- 访问趋势图表 -->
    <el-card class="chart-card">
      <template #header>
        <div class="card-header">
          <span>访问趋势</span>
          <el-radio-group v-model="trendPeriod" size="small" @change="loadTrendData">
            <el-radio-button label="7">最近7天</el-radio-button>
            <el-radio-button label="30">最近30天</el-radio-button>
            <el-radio-button label="90">最近90天</el-radio-button>
          </el-radio-group>
        </div>
      </template>
      <div ref="trendChartRef" style="height: 400px;"></div>
    </el-card>

    <!-- 热门页面 -->
    <el-card>
      <template #header>
        <span>热门页面 TOP 10</span>
      </template>
      <el-table :data="hotPages" v-loading="loading" border stripe>
        <el-table-column type="index" label="排名" width="80" align="center" />
        <el-table-column prop="pagePath" label="页面路径" min-width="300" show-overflow-tooltip />
        <el-table-column prop="pageTitle" label="页面标题" min-width="200" show-overflow-tooltip />
        <el-table-column prop="visitCount" label="访问次数" width="120" align="center" sortable />
        <el-table-column prop="uniqueVisitors" label="独立访客" width="120" align="center" sortable />
        <el-table-column prop="avgDuration" label="平均停留(秒)" width="140" align="center" sortable />
      </el-table>
    </el-card>

    <!-- 访问记录 -->
    <el-card style="margin-top: 20px;">
      <template #header>
        <div class="card-header">
          <span>访问记录</span>
          <el-button type="primary" size="small" @click="exportData">导出数据</el-button>
        </div>
      </template>
      
      <!-- 搜索区域 -->
      <el-form :model="searchForm" inline class="search-form">
        <el-form-item label="页面路径">
          <el-input 
            v-model="searchForm.pagePath" 
            placeholder="请输入页面路径" 
            clearable 
            style="width: 200px;"
          />
        </el-form-item>
        <el-form-item label="时间范围">
          <el-date-picker
            v-model="dateRange"
            type="daterange"
            range-separator="至"
            start-placeholder="开始日期"
            end-placeholder="结束日期"
            value-format="YYYY-MM-DD"
            style="width: 240px;"
          />
        </el-form-item>
        <el-form-item>
          <el-button type="primary" icon="Search" @click="handleSearch">搜索</el-button>
          <el-button icon="Refresh" @click="handleReset">重置</el-button>
        </el-form-item>
      </el-form>
      
      <el-table :data="tableData" v-loading="tableLoading" border stripe>
        <el-table-column type="index" label="序号" width="60" />
        <el-table-column prop="pagePath" label="页面路径" min-width="200" show-overflow-tooltip />
        <el-table-column prop="pageTitle" label="页面标题" min-width="150" show-overflow-tooltip />
        <el-table-column prop="ipAddress" label="IP地址" width="140" />
        <el-table-column prop="userAgent" label="浏览器" width="120" show-overflow-tooltip />
        <el-table-column prop="visitTime" label="访问时间" width="160" align="center" />
        <el-table-column prop="duration" label="停留时间(秒)" width="120" align="center" />
      </el-table>
      
      <!-- 分页 -->
      <el-pagination
        v-model:current-page="pagination.pageIndex"
        v-model:page-size="pagination.pageSize"
        :total="pagination.total"
        :page-sizes="[10, 20, 50, 100]"
        layout="total, sizes, prev, pager, next, jumper"
        @size-change="loadTableData"
        @current-change="loadTableData"
        style="margin-top: 20px; justify-content: flex-end;"
      />
    </el-card>
  </div>
</template>

<script setup>
import { ref, reactive, onMounted, onUnmounted } from 'vue'
import { ElMessage } from 'element-plus'
import { View, User, Calendar, TrendCharts } from '@element-plus/icons-vue'
import { statisticsApi } from '@/api'
import * as echarts from 'echarts'

// 概览数据
const overview = reactive({
  totalVisits: 0,
  uniqueVisitors: 0,
  todayVisits: 0,
  avgDuration: 0
})

// 趋势图表
const trendChartRef = ref(null)
let trendChart = null
const trendPeriod = ref('7')

// 热门页面
const hotPages = ref([])
const loading = ref(false)

// 访问记录
const dateRange = ref([])
const searchForm = reactive({
  pagePath: '',
  startDate: '',
  endDate: ''
})

const pagination = reactive({
  pageIndex: 1,
  pageSize: 10,
  total: 0
})

const tableData = ref([])
const tableLoading = ref(false)

/**
 * 加载概览数据
 */
const loadOverview = async () => {
  try {
    const res = await statisticsApi.visit.getOverview()
    if (res.success && res.data) {
      Object.assign(overview, res.data)
    }
  } catch (error) {
    console.error('加载概览数据失败:', error)
  }
}

/**
 * 加载趋势数据
 */
const loadTrendData = async () => {
  try {
    const res = await statisticsApi.visit.getTrend({ days: parseInt(trendPeriod.value) })
    if (res.success && res.data) {
      renderTrendChart(res.data)
    }
  } catch (error) {
    console.error('加载趋势数据失败:', error)
  }
}

/**
 * 渲染趋势图表
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
      data: ['访问量', '独立访客']
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
        name: '访问量',
        type: 'line',
        data: data.visits || [],
        smooth: true,
        itemStyle: { color: '#409eff' }
      },
      {
        name: '独立访客',
        type: 'line',
        data: data.uniqueVisitors || [],
        smooth: true,
        itemStyle: { color: '#67c23a' }
      }
    ]
  }
  
  trendChart.setOption(option)
}

/**
 * 加载热门页面
 */
const loadHotPages = async () => {
  loading.value = true
  try {
    const res = await statisticsApi.visit.getHotPages({ limit: 10 })
    if (res.success && res.data) {
      hotPages.value = res.data
    }
  } catch (error) {
    console.error('加载热门页面失败:', error)
  } finally {
    loading.value = false
  }
}

/**
 * 加载访问记录
 */
const loadTableData = async () => {
  tableLoading.value = true
  try {
    if (dateRange.value && dateRange.value.length === 2) {
      searchForm.startDate = dateRange.value[0]
      searchForm.endDate = dateRange.value[1]
    } else {
      searchForm.startDate = ''
      searchForm.endDate = ''
    }
    
    const params = {
      pagePath: searchForm.pagePath || undefined,
      startDate: searchForm.startDate || undefined,
      endDate: searchForm.endDate || undefined,
      page: pagination.pageIndex,
      pageSize: pagination.pageSize
    }
    
    const res = await statisticsApi.visit.getList(params)
    if (res.success && res.data) {
      tableData.value = res.data.items || []
      pagination.total = res.data.totalCount || 0
    }
  } catch (error) {
    console.error('加载访问记录失败:', error)
    ElMessage.error('加载访问记录失败')
  } finally {
    tableLoading.value = false
  }
}

/**
 * 搜索
 */
const handleSearch = () => {
  pagination.pageIndex = 1
  loadTableData()
}

/**
 * 重置
 */
const handleReset = () => {
  searchForm.pagePath = ''
  dateRange.value = []
  handleSearch()
}

/**
 * 导出数据
 */
const exportData = () => {
  ElMessage.info('导出功能开发中...')
}

/**
 * 窗口大小改变时重绘图表
 */
const handleResize = () => {
  if (trendChart) {
    trendChart.resize()
  }
}

onMounted(() => {
  loadOverview()
  loadTrendData()
  loadHotPages()
  loadTableData()
  window.addEventListener('resize', handleResize)
})

onUnmounted(() => {
  if (trendChart) {
    trendChart.dispose()
  }
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

.search-form {
  margin-bottom: 16px;
}

.search-form :deep(.el-form-item) {
  margin-bottom: 10px;
}
</style>