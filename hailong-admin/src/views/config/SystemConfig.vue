<template>
  <div class="page-container">
    <el-card>
      <template #header>
        <div class="card-header">
          <span>系统配置管理</span>
        </div>
      </template>
      
      <!-- Tab 标签页 -->
      <el-tabs v-model="activeTab" @tab-change="handleTabChange">
        <el-tab-pane label="轮播图管理" name="banners">
          <template #label>
            <el-icon><Picture /></el-icon>
            <span style="margin-left: 5px;">轮播图管理</span>
          </template>
          <BannersTab />
        </el-tab-pane>
        
        <el-tab-pane label="企业信息" name="company">
          <template #label>
            <el-icon><OfficeBuilding /></el-icon>
            <span style="margin-left: 5px;">企业信息</span>
          </template>
          <CompanyProfileTab />
        </el-tab-pane>
        
        <el-tab-pane label="重要业绩" name="achievements">
          <template #label>
            <el-icon><TrophyBase /></el-icon>
            <span style="margin-left: 5px;">重要业绩</span>
          </template>
          <AchievementsTab />
        </el-tab-pane>
        
        <el-tab-pane label="友情链接" name="links">
          <template #label>
            <el-icon><Link /></el-icon>
            <span style="margin-left: 5px;">友情链接</span>
          </template>
          <FriendlyLinksTab />
        </el-tab-pane>
      </el-tabs>
    </el-card>
  </div>
</template>

<script setup>
import { ref } from 'vue'
import { Picture, OfficeBuilding, TrophyBase, Link } from '@element-plus/icons-vue'
import BannersTab from './tabs/BannersTab.vue'
import CompanyProfileTab from './tabs/CompanyProfileTab.vue'
import AchievementsTab from './tabs/AchievementsTab.vue'
import FriendlyLinksTab from './tabs/FriendlyLinksTab.vue'

const activeTab = ref('banners')

const handleTabChange = (tabName) => {
  console.log('切换到:', tabName)
}
</script>

<style scoped>
.page-container {
  padding: 20px;
}

.card-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  font-weight: bold;
  font-size: 16px;
}

:deep(.el-tabs__item) {
  font-size: 14px;
}
</style>
