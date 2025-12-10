<template>
  <div class="page-container">
    <el-card>
      <template #header>
        <div class="card-header">
          <span>地区字典</span>
          <el-button type="primary" icon="Plus" @click="handleAdd">新增地区</el-button>
        </div>
      </template>

      <!-- 搜索表单 -->
      <el-form :inline="true" :model="searchForm" class="search-form">
        <el-form-item label="地区名称">
          <el-input v-model="searchForm.name" placeholder="请输入地区名称" clearable />
        </el-form-item>
        <el-form-item label="地区代码">
          <el-input v-model="searchForm.code" placeholder="请输入地区代码" clearable />
        </el-form-item>
        <el-form-item label="级别">
          <el-select v-model="searchForm.level" placeholder="请选择级别" clearable>
            <el-option label="省级" :value="1" />
            <el-option label="市级" :value="2" />
            <el-option label="区县级" :value="3" />
          </el-select>
        </el-form-item>
        <el-form-item>
          <el-button type="primary" icon="Search" @click="handleSearch">搜索</el-button>
          <el-button icon="Refresh" @click="handleReset">重置</el-button>
        </el-form-item>
      </el-form>

      <!-- 数据表格 -->
      <el-table 
        :data="tableData" 
        v-loading="loading" 
        border 
        stripe
        row-key="id"
        :tree-props="{ children: 'children', hasChildren: 'hasChildren' }"
      >
        <el-table-column prop="name" label="地区名称" min-width="200" />
        <el-table-column prop="code" label="地区代码" width="120" />
        <el-table-column label="级别" width="100" align="center">
          <template #default="{ row }">
            <el-tag :type="getLevelTag(row.level)" size="small">
              {{ getLevelText(row.level) }}
            </el-tag>
          </template>
        </el-table-column>
        <el-table-column prop="sortOrder" label="排序" width="80" align="center" />
        <el-table-column label="状态" width="80" align="center">
          <template #default="{ row }">
            <el-tag :type="row.status === 1 ? 'success' : 'info'" size="small">
              {{ row.status === 1 ? '启用' : '禁用' }}
            </el-tag>
          </template>
        </el-table-column>
        <el-table-column prop="createdAt" label="创建时间" width="160" />
        <el-table-column label="操作" width="200" fixed="right">
          <template #default="{ row }">
            <el-button type="primary" size="small" link @click="handleEdit(row)">编辑</el-button>
            <el-button 
              type="success" 
              size="small" 
              link 
              @click="handleAddChild(row)"
              v-if="row.level < 3"
            >添加下级</el-button>
            <el-button type="danger" size="small" link @click="handleDelete(row)">删除</el-button>
          </template>
        </el-table-column>
      </el-table>
    </el-card>

    <!-- 新增/编辑对话框 -->
    <el-dialog 
      v-model="dialogVisible" 
      :title="dialogTitle" 
      width="600px"
      @close="handleDialogClose"
    >
      <el-form :model="formData" :rules="rules" ref="formRef" label-width="100px">
        <el-form-item label="上级地区" v-if="formData.parentId">
          <el-input :value="formData.parentName" disabled />
        </el-form-item>
        
        <el-form-item label="地区名称" prop="name">
          <el-input v-model="formData.name" placeholder="请输入地区名称" maxlength="100" />
        </el-form-item>
        
        <el-form-item label="地区代码" prop="code">
          <el-input v-model="formData.code" placeholder="请输入地区代码" maxlength="20" />
          <div class="form-tip">行政区划代码，如：110000（北京市）</div>
        </el-form-item>
        
        <el-form-item label="级别" prop="level">
          <el-select v-model="formData.level" placeholder="请选择级别" style="width: 100%" :disabled="!!formData.parentId">
            <el-option label="省级" :value="1" />
            <el-option label="市级" :value="2" />
            <el-option label="区县级" :value="3" />
          </el-select>
        </el-form-item>
        
        <el-form-item label="排序" prop="sortOrder">
          <el-input-number v-model="formData.sortOrder" :min="0" :max="9999" />
          <span class="form-tip" style="margin-left: 10px;">数字越小越靠前</span>
        </el-form-item>
        
        <el-form-item label="状态">
          <el-switch 
            v-model="formData.status" 
            :active-value="1" 
            :inactive-value="0"
            active-text="启用" 
            inactive-text="禁用" 
          />
        </el-form-item>
      </el-form>
      
      <template #footer>
        <el-button @click="dialogVisible = false">取消</el-button>
        <el-button type="primary" @click="handleSubmit" :loading="submitting">确定</el-button>
      </template>
    </el-dialog>
  </div>
</template>

<script setup>
import { ref, reactive, onMounted } from 'vue'
import { ElMessage, ElMessageBox } from 'element-plus'
import { systemApi } from '@/api'

const tableData = ref([])
const loading = ref(false)
const dialogVisible = ref(false)
const dialogTitle = ref('')
const submitting = ref(false)
const formRef = ref(null)

const searchForm = reactive({
  name: '',
  code: '',
  level: null
})

const formData = reactive({
  id: null,
  parentId: null,
  parentName: '',
  name: '',
  code: '',
  level: 1,
  sortOrder: 0,
  status: 1
})

const rules = {
  name: [
    { required: true, message: '请输入地区名称', trigger: 'blur' },
    { min: 2, max: 100, message: '地区名称长度在 2 到 100 个字符', trigger: 'blur' }
  ],
  code: [
    { required: true, message: '请输入地区代码', trigger: 'blur' },
    { pattern: /^\d{6}$/, message: '地区代码必须是6位数字', trigger: 'blur' }
  ],
  level: [
    { required: true, message: '请选择级别', trigger: 'change' }
  ],
  sortOrder: [
    { required: true, message: '请输入排序', trigger: 'blur' }
  ]
}

const getLevelText = (level) => {
  const levelMap = {
    1: '省级',
    2: '市级',
    3: '区县级'
  }
  return levelMap[level] || '-'
}

const getLevelTag = (level) => {
  const tagMap = {
    1: 'danger',
    2: 'warning',
    3: 'primary'
  }
  return tagMap[level] || ''
}

const loadData = async () => {
  loading.value = true
  try {
    const res = await systemApi.regions.getTree(searchForm)
    if (res.success) {
      tableData.value = res.data || []
    } else {
      ElMessage.error(res.message || '加载数据失败')
    }
  } catch (error) {
    console.error('加载数据失败:', error)
    ElMessage.error('加载数据失败，请稍后重试')
  } finally {
    loading.value = false
  }
}

const handleSearch = () => {
  loadData()
}

const handleReset = () => {
  Object.assign(searchForm, {
    name: '',
    code: '',
    level: null
  })
  loadData()
}

const handleAdd = () => {
  dialogTitle.value = '新增地区'
  Object.assign(formData, {
    id: null,
    parentId: null,
    parentName: '',
    name: '',
    code: '',
    level: 1,
    sortOrder: 0,
    status: 1
  })
  dialogVisible.value = true
}

const handleAddChild = (row) => {
  dialogTitle.value = '新增下级地区'
  Object.assign(formData, {
    id: null,
    parentId: row.id,
    parentName: row.name,
    name: '',
    code: '',
    level: row.level + 1,
    sortOrder: 0,
    status: 1
  })
  dialogVisible.value = true
}

const handleEdit = (row) => {
  dialogTitle.value = '编辑地区'
  Object.assign(formData, {
    id: row.id,
    parentId: row.parentId,
    parentName: row.parentName || '',
    name: row.name,
    code: row.code,
    level: row.level,
    sortOrder: row.sortOrder,
    status: row.status
  })
  dialogVisible.value = true
}

const handleSubmit = async () => {
  if (!formRef.value) return
  
  await formRef.value.validate(async (valid) => {
    if (!valid) return
    
    submitting.value = true
    try {
      const res = formData.id 
        ? await systemApi.regions.update(formData.id, formData)
        : await systemApi.regions.create(formData)
      
      if (res.success) {
        ElMessage.success(formData.id ? '更新成功' : '创建成功')
        dialogVisible.value = false
        loadData()
      } else {
        ElMessage.error(res.message || '操作失败')
      }
    } catch (error) {
      console.error('提交失败:', error)
      ElMessage.error('操作失败，请稍后重试')
    } finally {
      submitting.value = false
    }
  })
}

const handleDelete = async (row) => {
  try {
    await ElMessageBox.confirm(
      row.children && row.children.length > 0 
        ? '该地区包含下级地区，删除后下级地区也将被删除，确定要删除吗？' 
        : '确定要删除该地区吗？', 
      '提示', 
      {
        type: 'warning'
      }
    )
    
    const res = await systemApi.regions.delete(row.id)
    if (res.success) {
      ElMessage.success('删除成功')
      loadData()
    } else {
      ElMessage.error(res.message || '删除失败')
    }
  } catch (error) {
    if (error !== 'cancel') {
      console.error('删除失败:', error)
      ElMessage.error('删除失败，请稍后重试')
    }
  }
}

const handleDialogClose = () => {
  formRef.value?.resetFields()
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

.search-form {
  margin-bottom: 20px;
}

.form-tip {
  font-size: 12px;
  color: #999;
  margin-top: 5px;
}
</style>