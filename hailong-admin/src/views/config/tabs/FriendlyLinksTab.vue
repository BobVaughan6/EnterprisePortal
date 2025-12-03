<template>
  <div class="tab-content">
    <div class="toolbar">
      <el-button type="primary" :icon="Plus" @click="handleAdd">新增链接</el-button>
    </div>
    
    <!-- 表格 -->
    <el-table :data="tableData" v-loading="loading" border stripe>
      <el-table-column type="index" label="序号" width="60" align="center" />
      <el-table-column label="Logo" width="100" align="center">
        <template #default="{ row }">
          <el-image 
            v-if="row.logoUrl"
            :src="row.logoUrl" 
            :preview-src-list="[row.logoUrl]"
            fit="contain"
            style="width: 60px; height: 40px; border-radius: 4px;"
          />
          <span v-else style="color: #909399;">-</span>
        </template>
      </el-table-column>
      <el-table-column prop="linkName" label="链接名称" min-width="200" show-overflow-tooltip />
      <el-table-column prop="linkUrl" label="链接地址" min-width="300" show-overflow-tooltip />
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
      :title="isEdit ? '编辑链接' : '新增链接'"
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
        <el-form-item label="链接名称" prop="linkName">
          <el-input v-model="formData.linkName" placeholder="请输入链接名称" clearable />
        </el-form-item>
        
        <el-form-item label="链接地址" prop="linkUrl">
          <el-input v-model="formData.linkUrl" placeholder="请输入链接URL，如：https://www.example.com" clearable />
        </el-form-item>
        
        <el-form-item label="Logo地址" prop="logoUrl">
          <el-input v-model="formData.logoUrl" placeholder="请输入Logo URL（可选）" clearable />
          <div v-if="formData.logoUrl" style="margin-top: 10px;">
            <el-image 
              :src="formData.logoUrl" 
              :preview-src-list="[formData.logoUrl]"
              fit="contain"
              style="max-width: 150px; max-height: 80px; border: 1px solid #dcdfe6; border-radius: 4px;"
            />
          </div>
          <div style="margin-top: 8px; color: #909399; font-size: 12px;">
            建议尺寸：200x100px
          </div>
        </el-form-item>
        
        <el-form-item label="排序" prop="sortOrder">
          <el-input-number v-model="formData.sortOrder" :min="0" :max="9999" />
          <span style="margin-left: 10px; color: #909399; font-size: 12px;">
            数字越小越靠前
          </span>
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
  linkName: '',
  linkUrl: '',
  logoUrl: '',
  sortOrder: 0,
  status: true
})

const formRules = {
  linkName: [
    { required: true, message: '请输入链接名称', trigger: 'blur' }
  ],
  linkUrl: [
    { required: true, message: '请输入链接地址', trigger: 'blur' },
    { type: 'url', message: '请输入正确的URL格式', trigger: 'blur' }
  ]
}

const loadData = async () => {
  loading.value = true
  try {
    const res = await configApi.getFriendlyLinks()
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
    linkName: '',
    linkUrl: '',
    logoUrl: '',
    sortOrder: 0,
    status: true
  })
  dialogVisible.value = true
}

const handleEdit = (row) => {
  isEdit.value = true
  Object.assign(formData, {
    id: row.id,
    linkName: row.linkName,
    linkUrl: row.linkUrl,
    logoUrl: row.logoUrl || '',
    sortOrder: row.sortOrder,
    status: row.status
  })
  dialogVisible.value = true
}

const handleDelete = async (row) => {
  try {
    await ElMessageBox.confirm('确定要删除该友情链接吗？', '提示', {
      confirmButtonText: '确定',
      cancelButtonText: '取消',
      type: 'warning'
    })
    
    const res = await configApi.deleteFriendlyLink(row.id)
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
      const submitData = {
        linkName: formData.linkName,
        linkUrl: formData.linkUrl,
        logoUrl: formData.logoUrl || null,
        sortOrder: formData.sortOrder,
        status: formData.status
      }
      
      let res
      if (isEdit.value) {
        res = await configApi.updateFriendlyLink(formData.id, submitData)
      } else {
        res = await configApi.createFriendlyLink(submitData)
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
