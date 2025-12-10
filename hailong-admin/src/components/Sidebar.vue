<template>
  <div class="sidebar-container">
    <!-- Logo区域 -->
    <div class="logo-container">
      <h2 v-if="!isCollapse" class="logo-title">海隆咨询</h2>
      <h2 v-else class="logo-title-mini">HL</h2>
    </div>
    
    <!-- 菜单 -->
    <el-menu
      :default-active="activeMenu"
      :collapse="isCollapse"
      :unique-opened="true"
      background-color="#304156"
      text-color="#bfcbd9"
      active-text-color="#409eff"
      router
    >
      <el-menu-item index="/home">
        <el-icon><Odometer /></el-icon>
        <template #title>首页看板</template>
      </el-menu-item>
      
      <el-sub-menu index="announcements">
        <template #title>
          <el-icon><Document /></el-icon>
          <span>公告管理</span>
        </template>
        <el-menu-item index="/gov-procurement">政府采购公告</el-menu-item>
        <el-menu-item index="/construction">建设工程公告</el-menu-item>
      </el-sub-menu>
      
      <el-sub-menu index="info-publish">
        <template #title>
          <el-icon><DocumentCopy /></el-icon>
          <span>信息发布</span>
        </template>
        <el-menu-item index="/info-publish/company-news">公司公告</el-menu-item>
        <el-menu-item index="/info-publish/policies">政策法规</el-menu-item>
        <el-menu-item index="/info-publish/policy-info">政策信息</el-menu-item>
        <el-menu-item index="/info-publish/notices">通知公告</el-menu-item>
      </el-sub-menu>
      
      <el-menu-item index="/attachments">
        <el-icon><Paperclip /></el-icon>
        <template #title>附件管理</template>
      </el-menu-item>
      
      <el-sub-menu index="config">
        <template #title>
          <el-icon><Setting /></el-icon>
          <span>系统配置</span>
        </template>
        <el-menu-item index="/config/company-profile">企业简介</el-menu-item>
        <el-menu-item index="/config/business-scope">业务范围</el-menu-item>
        <el-menu-item index="/config/qualifications">企业资质</el-menu-item>
        <el-menu-item index="/config/achievements">重要业绩</el-menu-item>
        <el-menu-item index="/config/honors">企业荣誉</el-menu-item>
        <el-menu-item index="/config/banners">轮播图管理</el-menu-item>
        <el-menu-item index="/config/friendly-links">友情链接</el-menu-item>
      </el-sub-menu>
      
      <el-sub-menu index="statistics">
        <template #title>
          <el-icon><DataAnalysis /></el-icon>
          <span>统计分析</span>
        </template>
        <el-menu-item index="/statistics/visit">访问统计</el-menu-item>
        <el-menu-item index="/statistics/announcement">公告统计</el-menu-item>
        <el-menu-item index="/statistics/info-publication">信息发布统计</el-menu-item>
      </el-sub-menu>
      
      <el-sub-menu index="system">
        <template #title>
          <el-icon><Tools /></el-icon>
          <span>系统管理</span>
        </template>
        <el-menu-item index="/system/users">用户管理</el-menu-item>
        <el-menu-item index="/system/logs">系统日志</el-menu-item>
        <el-menu-item index="/system/regions">区域字典</el-menu-item>
        <el-menu-item index="/system/change-password">修改密码</el-menu-item>
      </el-sub-menu>
    </el-menu>
  </div>
</template>

<script setup>
import { computed } from 'vue'
import { useRoute } from 'vue-router'
import { Odometer, Document, DocumentCopy, Paperclip, Setting, DataAnalysis, Tools } from '@element-plus/icons-vue'

defineProps({
  isCollapse: {
    type: Boolean,
    default: false
  }
})

const route = useRoute()

// 当前激活的菜单
const activeMenu = computed(() => {
  return route.path
})
</script>

<style scoped>
.sidebar-container {
  height: 100%;
  display: flex;
  flex-direction: column;
}

.logo-container {
  height: 60px;
  display: flex;
  align-items: center;
  justify-content: center;
  background-color: #2b3a4b;
}

.logo-title {
  color: #fff;
  font-size: 20px;
  font-weight: bold;
  margin: 0;
}

.logo-title-mini {
  color: #fff;
  font-size: 20px;
  font-weight: bold;
  margin: 0;
}

.el-menu {
  border-right: none;
  flex: 1;
}

:deep(.el-sub-menu__title) {
  color: #bfcbd9;
}

:deep(.el-sub-menu__title:hover) {
  background-color: #263445 !important;
}

:deep(.el-menu-item:hover) {
  background-color: #263445 !important;
}

:deep(.el-menu-item.is-active) {
  background-color: #001528 !important;
}
</style>
