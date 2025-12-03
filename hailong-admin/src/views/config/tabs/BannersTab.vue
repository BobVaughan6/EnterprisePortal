<template>
  <div class="tab-content">
    <div class="toolbar">
      <el-button type="primary" :icon="Plus" @click="handleAdd">新增轮播图</el-button>
    </div>
    
    <!-- 表格 -->
    <el-table :data="tableData" v-loading="loading" border stripe>
      <el-table-column type="index" label="序号" width="60" align="center" />
      <el-table-column label="图片" width="150" align="center">
        <template #default="{ row }">
          <el-image 
            :src="row.imageUrl" 
            :preview-src-list="[row.imageUrl]"
            fit="cover"
            style="width: 100px; height: 60px; border-radius: 4px;"
          />
        </template>
      </el-table-column>
      <el-table-column prop="title" label="标题" min-width="200" show-overflow-tooltip />
      <el-table-column prop="linkUrl" label="链接地址" min-width="250" show-overflow-tooltip />
      <el-table-column prop="sortOrder" label="排序" width="100" align="center" />
      <el-table-column prop="status" label="状态" width="100" align="center">
        <template #default="{ row }">
          <el-tag :type="row.status ? 'success' : 'danger'" size="small">
            {{ row.status ? '启用' : '禁用' }}
          </el-tag>
        </template>
      </el-table-column>
      <el-table-column prop="createdAt" label="创建时间" width="180" align="center">
        <template #default="{ row }">
          {{ formatDateTime(row.createdAt) }}
        </template>
      </el-table-column>
      <el-table-column label="操作" width="150" fixed="right" align="center">
        <template #default="{ row }">
          <el-button type="primary" size="small" link @click="handleEdit(row)">编辑</el-button>
          <el-button type="danger" size="small" link @click="handleDelete(row)">删除</el-button>
        </template>
      </el-table-column>
    </el-table>
    
    <!-- 编辑对话框 -->
    <el-dialog
      v-model="dialogVisible"
      :title="isEdit ? '编辑轮播图' : '新增轮播图'"
      width="600px"
      destroy-on-close
      :close-on-click-modal="false"
    >
      <el-form 
        ref="formRef" 
        :model="formData" 
        :rules="formRules" 
        label-width="100px"
      >
        <el-form-item label="标题" prop="title">
          <el-input v-model="formData.title" placeholder="请输入标题（可选）" clearable />
        </el-form-item>
        <el-form-item label="图片地址" prop="imageUrl">
          <el-input v-model="formData.imageUrl" placeholder="请输入图片URL" clearable />
          <div style="margin-top: 8px; color: #909399; font-size: 12px;">
            建议尺寸：1920x600px，支持 jpg、png 格式
          </div>
        </el-form-item>
        <el-form-item label="链接地址" prop="linkUrl">
          <el-input v-model="formData.linkUrl" placeholder="请输入链接URL（可选）" clearable />
        </el-form-item>
        <el-form-item label="排序" prop="sortOrder">
          <el-input-number v-model="formData.sortOrder" :min="0" :max="9999" />
          <div style="margin-left: 10px; color: #909399; font-size: 12px;">
            数字越小越靠前
          </div>
        </el-form-item>
        <el-form-item label="状态" prop="status">
          <el-switch 
            v-model="formData.status" 
            active-text="启用" 
            inactive-text="禁用" 
          />
        </el-form-item>
      </el-form>
      <template #footer>
        <el-button @click="dialogVisible = false">取消</el-button>
        <el-button type="primary" @click="handleSubmit" :loading="submitting">提交</el-button>
      </template>
    </el-dialog>
  </div>
</template>

<script setup>
import { ref, reactive, onMounted } from 'vue'
import { ElMessage, ElMessageBox } from 'element-plus'
import { Plus } from '@element-plus/icons-vue'
import { configApi } from '@/api'
import { formatDateTime } from '@/utils/date'

const tableData = ref([])
const loading = ref(false)
const dialogVisible = ref(false)
const isEdit = ref(false)
const submitting = ref(false)
const formRef = ref(null)

const formData = reactive({
  title: '',
  imageUrl: '',
  linkUrl: '',
  sortOrder: 0,
  status: true
})

const formRules = {
  imageUrl: [
    { required: true, message: '请输入图片URL', trigger: 'blur' }
  ]
}

const loadData = async () => {
  loading.value = true
  try {
    const res = await configApi.getBanners()
    if (res.success && res.data) {
      tableData.value = res.data
    }
  } catch (error) {
    console.error('加载数据失败:', error)
    ElMessage.error('加载数据失败')
  } finally {
    loading.value = false
  }
}

const handleAdd = () => {
  isEdit.value = false
  Object.assign(formData, {
    title: '',
    imageUrl: '',
    linkUrl: '',
    sortOrder: 0,
    status: true
  })
  dialogVisible.value = true
}

const handleEdit = (row) => {
  isEdit.value = true
  Object.assign(formData, {
    id: row.id,
    title: row.title || '',
    imageUrl: row.imageUrl,
    linkUrl: row.linkUrl || '',
    sortOrder: row.sortOrder,
    status: row.status
  })
  dialogVisible.value = true
}

const handleDelete = async (row) => {
  try {
    await ElMessageBox.confirm('确定要删除该轮播图吗？', '提示', {
      confirmButtonText: '确定',
      cancelButtonText: '取消',
      type: 'warning'
    })
    
    const res = await configApi.deleteBanner(row.id)
    if (res.success) {
      ElMessage.success('删除成功')
      loadData()
    } else {
      ElMessage.error(res.message || '删除失败')
    }
  } catch (error) {
    if (error !== 'cancel') {
      ElMessage.error('删除失败')
    }
  }
}

const handleSubmit = async () => {
  if (!formRef.value) return
  
  await formRef.value.validate(async (valid) => {
    if (!valid) return
    
    submitting.value = true
    try {
      let res
      const submitData = {
        title: formData.title || null,
        imageUrl: formData.imageUrl,
        linkUrl: formData.linkUrl || null,
        sortOrder: formData.sortOrder,
        status: formData.status
      }
      
      if (isEdit.value) {
        res = await configApi.updateBanner(formData.id, submitData)
      } else {
        res = await configApi.createBanner(submitData)
      }
      
      if (res.success) {
        ElMessage.success(isEdit.value ? '更新成功' : '创建成功')
        dialogVisible.value = false
        loadData()
      } else {
        ElMessage.error(res.message || '操作失败')
      }
    } catch (error) {
      console.error('提交失败:', error)
      ElMessage.error('提交失败')
    } finally {
      submitting.value = false
    }
  })
}

onMounted(() => {
  loadData()
})
</script>

<style scoped>
.tab-content {
  padding: 20px 0;
}

.toolbar {
  margin-bottom: 20px;
}
</style>
