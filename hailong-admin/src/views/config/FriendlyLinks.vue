<template>
  <div class="page-container">
    <el-card>
      <template #header>
        <div class="card-header">
          <span>友情链接</span>
          <el-button type="primary" icon="Plus" @click="handleAdd">新增链接</el-button>
        </div>
      </template>
      
      <el-table :data="tableData" v-loading="loading" border stripe>
        <el-table-column prop="id" label="ID" width="80" />
        <el-table-column prop="linkName" label="链接名称" min-width="200" />
        <el-table-column prop="linkUrl" label="链接地址" min-width="250" show-overflow-tooltip />
        <el-table-column prop="sortOrder" label="排序" width="100" />
        <el-table-column prop="status" label="状态" width="100">
          <template #default="{ row }">
            <el-tag :type="row.status ? 'success' : 'danger'">
              {{ row.status ? '启用' : '禁用' }}
            </el-tag>
          </template>
        </el-table-column>
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
    const res = await configApi.getFriendlyLinks()
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
    await ElMessageBox.confirm('确定要删除该链接吗？', '提示', {
      type: 'warning'
    })
    const res = await configApi.deleteFriendlyLink(row.id)
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
