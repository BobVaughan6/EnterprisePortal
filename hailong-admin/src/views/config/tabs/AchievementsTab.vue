<template>
  <div class="tab-content">
    <div class="toolbar">
      <el-button type="primary" :icon="Plus" @click="handleAdd">新增业绩</el-button>
    </div>
    
    <!-- 表格 -->
    <el-table :data="tableData" v-loading="loading" border stripe>
      <el-table-column type="index" label="序号" width="60" align="center" />
      <el-table-column label="缩略图" width="120" align="center">
        <template #default="{ row }">
          <el-image 
            v-if="row.imageUrl"
            :src="row.imageUrl" 
            :preview-src-list="[row.imageUrl]"
            fit="cover"
            style="width: 80px; height: 60px; border-radius: 4px;"
          />
          <span v-else style="color: #909399;">-</span>
        </template>
      </el-table-column>
      <el-table-column prop="projectName" label="项目名称" min-width="200" show-overflow-tooltip />
      <el-table-column prop="projectType" label="项目类型" width="120" align="center" />
      <el-table-column prop="projectAmount" label="项目金额(万元)" width="140" align="center">
        <template #default="{ row }">
          {{ row.projectAmount ? row.projectAmount.toLocaleString() : '-' }}
        </template>
      </el-table-column>
      <el-table-column prop="clientName" label="客户名称" min-width="180" show-overflow-tooltip />
      <el-table-column prop="completionDate" label="完成日期" width="120" align="center" />
      <el-table-column prop="sortOrder" label="排序" width="80" align="center" />
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
      :title="isEdit ? '编辑业绩' : '新增业绩'"
      width="700px"
      destroy-on-close
      :close-on-click-modal="false"
    >
      <el-form 
        ref="formRef" 
        :model="formData" 
        :rules="formRules" 
        label-width="120px"
      >
        <el-form-item label="项目名称" prop="projectName">
          <el-input v-model="formData.projectName" placeholder="请输入项目名称" clearable />
        </el-form-item>
        
        <el-row :gutter="20">
          <el-col :span="12">
            <el-form-item label="项目类型" prop="projectType">
              <el-input v-model="formData.projectType" placeholder="请输入项目类型" clearable />
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="项目金额" prop="projectAmount">
              <el-input-number 
                v-model="formData.projectAmount" 
                :min="0" 
                :precision="2"
                :controls="false"
                placeholder="万元"
                style="width: 100%;"
              />
            </el-form-item>
          </el-col>
        </el-row>
        
        <el-row :gutter="20">
          <el-col :span="12">
            <el-form-item label="客户名称" prop="clientName">
              <el-input v-model="formData.clientName" placeholder="请输入客户名称" clearable />
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="完成日期" prop="completionDate">
              <el-date-picker
                v-model="formData.completionDate"
                type="date"
                placeholder="选择完成日期"
                format="YYYY-MM-DD"
                value-format="YYYY-MM-DD"
                style="width: 100%;"
              />
            </el-form-item>
          </el-col>
        </el-row>
        
        <el-form-item label="项目描述" prop="description">
          <el-input 
            v-model="formData.description" 
            type="textarea" 
            :rows="4"
            placeholder="请输入项目描述"
            maxlength="500"
            show-word-limit
          />
        </el-form-item>
        
        <el-form-item label="项目图片" prop="imageUrl">
          <el-input v-model="formData.imageUrl" placeholder="请输入图片URL（可选）" clearable />
          <div v-if="formData.imageUrl" style="margin-top: 10px;">
            <el-image 
              :src="formData.imageUrl" 
              :preview-src-list="[formData.imageUrl]"
              fit="contain"
              style="max-width: 200px; max-height: 150px; border: 1px solid #dcdfe6; border-radius: 4px;"
            />
          </div>
        </el-form-item>
        
        <el-form-item label="排序" prop="sortOrder">
          <el-input-number v-model="formData.sortOrder" :min="0" :max="9999" />
          <span style="margin-left: 10px; color: #909399; font-size: 12px;">
            数字越小越靠前
          </span>
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

const tableData = ref([])
const loading = ref(false)
const dialogVisible = ref(false)
const isEdit = ref(false)
const submitting = ref(false)
const formRef = ref(null)

const formData = reactive({
  projectName: '',
  projectType: '',
  projectAmount: null,
  clientName: '',
  completionDate: '',
  description: '',
  imageUrl: '',
  sortOrder: 0
})

const formRules = {
  projectName: [
    { required: true, message: '请输入项目名称', trigger: 'blur' }
  ]
}

const loadData = async () => {
  loading.value = true
  try {
    const res = await configApi.getAchievements()
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
    projectName: '',
    projectType: '',
    projectAmount: null,
    clientName: '',
    completionDate: '',
    description: '',
    imageUrl: '',
    sortOrder: 0
  })
  dialogVisible.value = true
}

const handleEdit = (row) => {
  isEdit.value = true
  Object.assign(formData, {
    id: row.id,
    projectName: row.projectName,
    projectType: row.projectType || '',
    projectAmount: row.projectAmount,
    clientName: row.clientName || '',
    completionDate: row.completionDate || '',
    description: row.description || '',
    imageUrl: row.imageUrl || '',
    sortOrder: row.sortOrder
  })
  dialogVisible.value = true
}

const handleDelete = async (row) => {
  try {
    await ElMessageBox.confirm('确定要删除该业绩吗？', '提示', {
      confirmButtonText: '确定',
      cancelButtonText: '取消',
      type: 'warning'
    })
    
    const res = await configApi.deleteAchievement(row.id)
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
        projectName: formData.projectName,
        projectType: formData.projectType || null,
        projectAmount: formData.projectAmount,
        clientName: formData.clientName || null,
        completionDate: formData.completionDate || null,
        description: formData.description || null,
        imageUrl: formData.imageUrl || null,
        sortOrder: formData.sortOrder
      }
      
      let res
      if (isEdit.value) {
        res = await configApi.updateAchievement(formData.id, submitData)
      } else {
        res = await configApi.createAchievement(submitData)
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
