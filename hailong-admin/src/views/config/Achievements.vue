<template>
  <div class="page-container">
    <el-card>
      <template #header>
        <div class="card-header">
          <span>业绩展示</span>
          <el-button type="primary" icon="Plus" @click="handleAdd">新增业绩</el-button>
        </div>
      </template>
      
      <el-table :data="tableData" v-loading="loading" border stripe>
        <el-table-column prop="id" label="ID" width="80" />
        <el-table-column prop="projectName" label="项目名称" min-width="200" show-overflow-tooltip />
        <el-table-column prop="projectType" label="项目类型" width="120" />
        <el-table-column prop="clientName" label="客户名称" width="150" />
        <el-table-column prop="projectAmount" label="项目金额" width="120">
          <template #default="{ row }">
            {{ row.projectAmount ? `¥${row.projectAmount}万` : '-' }}
          </template>
        </el-table-column>
        <el-table-column prop="completionDate" label="完成日期" width="120" />
        <el-table-column prop="sortOrder" label="排序" width="100" />
        <el-table-column label="操作" width="180" fixed="right">
          <template #default="{ row }">
            <el-button type="primary" size="small" link @click="handleEdit(row)">编辑</el-button>
            <el-button type="danger" size="small" link @click="handleDelete(row)">删除</el-button>
          </template>
        </el-table-column>
      </el-table>
    </el-card>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import { ElMessage, ElMessageBox } from 'element-plus'
import { configApi } from '@/api'

const tableData = ref([])
const loading = ref(false)

const loadData = async () => {
  loading.value = true
  try {
    const res = await configApi.getAchievements()
    if (res.success && res.data) {
      tableData.value = res.data
    }
  } catch (error) {
    console.error('加载数据失败:', error)
  } finally {
    loading.value = false
  }
}

const handleAdd = () => {
  ElMessage.info('请参考轮播图管理页面实现')
}

const handleEdit = (row) => {
  ElMessage.info('请参考轮播图管理页面实现')
}

const handleDelete = async (row) => {
  try {
    await ElMessageBox.confirm('确定要删除该业绩吗？', '提示', {
      type: 'warning'
    })
    const res = await configApi.deleteAchievement(row.id)
    if (res.success) {
      ElMessage.success('删除成功')
      loadData()
    }
  } catch (error) {
    if (error !== 'cancel') {
      ElMessage.error('删除失败')
    }
  }
}

onMounted(() => {
  loadData()
})
</script>

<style scoped>
.page-container {
  padding: 0;
}

.card-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
}
</style>
