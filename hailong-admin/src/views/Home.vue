<template>
  <div class="home-container">
    <el-row :gutter="20">
      <el-col :span="6">
        <el-card shadow="hover" class="stat-card">
          <div class="stat-content">
            <el-icon :size="40" color="#409eff"><Document /></el-icon>
            <div class="stat-info">
              <div class="stat-value">{{ stats.totalAnnouncements }}</div>
              <div class="stat-label">公告总数</div>
            </div>
          </div>
        </el-card>
      </el-col>
      
      <el-col :span="6">
        <el-card shadow="hover" class="stat-card">
          <div class="stat-content">
            <el-icon :size="40" color="#67c23a"><DocumentCopy /></el-icon>
            <div class="stat-info">
              <div class="stat-value">{{ stats.totalInfoPublish }}</div>
              <div class="stat-label">信息发布</div>
            </div>
          </div>
        </el-card>
      </el-col>
      
      <el-col :span="6">
        <el-card shadow="hover" class="stat-card">
          <div class="stat-content">
            <el-icon :size="40" color="#e6a23c"><View /></el-icon>
            <div class="stat-info">
              <div class="stat-value">{{ stats.totalViews }}</div>
              <div class="stat-label">总浏览量</div>
            </div>
          </div>
        </el-card>
      </el-col>
      
      <el-col :span="6">
        <el-card shadow="hover" class="stat-card">
          <div class="stat-content">
            <el-icon :size="40" color="#f56c6c"><TrendCharts /></el-icon>
            <div class="stat-info">
              <div class="stat-value">{{ stats.todayViews }}</div>
              <div class="stat-label">今日浏览</div>
            </div>
          </div>
        </el-card>
      </el-col>
    </el-row>
    
    <el-row :gutter="20" style="margin-top: 20px;">
      <el-col :span="24">
        <el-card>
          <template #header>
            <div class="card-header">
              <span>快捷操作</span>
            </div>
          </template>
          <div class="quick-actions">
            <el-button type="primary" icon="Plus" @click="goTo('/gov-procurement')">
              发布政府采购公告
            </el-button>
            <el-button type="success" icon="Plus" @click="goTo('/construction')">
              发布建设工程公告
            </el-button>
            <el-button type="warning" icon="Plus" @click="goTo('/info-publish/company-news')">
              发布公司公告
            </el-button>
            <el-button type="info" icon="Setting" @click="goTo('/config/banners')">
              管理轮播图
            </el-button>
          </div>
        </el-card>
      </el-col>
    </el-row>
    
    <el-row :gutter="20" style="margin-top: 20px;">
      <el-col :span="24">
        <el-card>
          <template #header>
            <div class="card-header">
              <span>系统信息</span>
            </div>
          </template>
          <el-descriptions :column="2" border>
            <el-descriptions-item label="系统名称">海隆咨询官网后台管理系统</el-descriptions-item>
            <el-descriptions-item label="系统版本">v1.0.0</el-descriptions-item>
            <el-descriptions-item label="当前用户">{{ userStore.userInfo?.fullName || '-' }}</el-descriptions-item>
            <el-descriptions-item label="用户角色">{{ userStore.userInfo?.role || '-' }}</el-descriptions-item>
            <el-descriptions-item label="登录账号">{{ userStore.userInfo?.username || '-' }}</el-descriptions-item>
            <el-descriptions-item label="邮箱">{{ userStore.userInfo?.email || '-' }}</el-descriptions-item>
          </el-descriptions>
        </el-card>
      </el-col>
    </el-row>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import { useRouter } from 'vue-router'
import { useUserStore } from '@/stores/user'
import { Document, DocumentCopy, View, TrendCharts } from '@element-plus/icons-vue'

const router = useRouter()
const userStore = useUserStore()

// 统计数据
const stats = ref({
  totalAnnouncements: 0,
  totalInfoPublish: 0,
  totalViews: 0,
  todayViews: 0
})

/**
 * 跳转到指定路由
 */
const goTo = (path) => {
  router.push(path)
}

/**
 * 加载统计数据
 */
const loadStats = async () => {
  // TODO: 从后端API获取统计数据
  stats.value = {
    totalAnnouncements: 128,
    totalInfoPublish: 256,
    totalViews: 15628,
    todayViews: 356
  }
}

onMounted(() => {
  loadStats()
})
</script>

<style scoped>
.home-container {
  padding: 20px;
}

.stat-card {
  cursor: pointer;
  transition: transform 0.3s;
}

.stat-card:hover {
  transform: translateY(-5px);
}

.stat-content {
  display: flex;
  align-items: center;
  gap: 20px;
}

.stat-info {
  flex: 1;
}

.stat-value {
  font-size: 28px;
  font-weight: bold;
  color: #333;
  margin-bottom: 5px;
}

.stat-label {
  font-size: 14px;
  color: #999;
}

.card-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.quick-actions {
  display: flex;
  gap: 15px;
  flex-wrap: wrap;
}
</style>
