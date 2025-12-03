<template>
  <div class="page-container">
    <el-card>
      <template #header>
        <div class="card-header">
          <span>信息发布管理</span>
          <el-button type="primary" :icon="Plus" @click="handleAdd">新增信息</el-button>
        </div>
      </template>
      
      <!-- Tab 标签页 -->
      <el-tabs v-model="activeTab" @tab-change="handleTabChange">
        <el-tab-pane label="公司公告" name="company_announcements" />
        <el-tab-pane label="政策法规" name="policy_regulations" />
        <el-tab-pane label="政策信息" name="policy_information" />
        <el-tab-pane label="通知公告" name="notice_announcements" />
      </el-tabs>
      
      <!-- 搜索区域 -->
      <el-form :model="searchForm" inline class="search-form">
        <el-form-item label="关键词">
          <el-input 
            v-model="searchForm.keyword" 
            placeholder="搜索标题或内容" 
            clearable 
            style="width: 250px;"
            @keyup.enter="handleSearch"
          />
        </el-form-item>
        <el-form-item>
          <el-button type="primary" :icon="Search" @click="handleSearch">搜索</el-button>
          <el-button :icon="Refresh" @click="handleReset">重置</el-button>
        </el-form-item>
      </el-form>
      
      <!-- 表格 -->
      <el-table :data="tableData" v-loading="loading" border stripe>
        <el-table-column type="index" label="序号" width="60" align="center" />
        <el-table-column prop="title" label="标题" min-width="250" show-overflow-tooltip />
        <el-table-column prop="publishTime" label="发布时间" width="180" align="center">
          <template #default="{ row }">
            {{ formatDateTime(row.publishTime) }}
          </template>
        </el-table-column>
        <el-table-column prop="viewCount" label="访问量" width="100" align="center" />
        <el-table-column prop="isTop" label="置顶" width="80" align="center">
          <template #default="{ row }">
            <el-tag :type="row.isTop ? 'success' : 'info'" size="small">
              {{ row.isTop ? '是' : '否' }}
            </el-tag>
          </template>
        </el-table-column>
        <el-table-column prop="attachments" label="附件" width="80" align="center">
          <template #default="{ row }">
            <el-tag v-if="row.attachments && row.attachments.length > 0" type="warning" size="small">
              {{ row.attachments.length }}
            </el-tag>
            <span v-else>-</span>
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
      
      <!-- 分页 -->
      <el-pagination
        v-model:current-page="pagination.pageIndex"
        v-model:page-size="pagination.pageSize"
        :total="pagination.total"
        :page-sizes="[10, 20, 50, 100]"
        layout="total, sizes, prev, pager, next, jumper"
        @size-change="handlePageSizeChange"
        @current-change="handlePageChange"
        style="margin-top: 20px; justify-content: flex-end;"
      />
    </el-card>
    
    <!-- 新增/编辑对话框 -->
    <el-dialog
      v-model="dialogVisible"
      :title="isEdit ? '编辑信息' : '新增信息'"
      width="1000px"
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
          <el-input 
            v-model="formData.title" 
            placeholder="请输入标题（最多255个字符）" 
            maxlength="255"
            show-word-limit
          />
        </el-form-item>
        
        <el-row :gutter="20">
          <el-col :span="12">
            <el-form-item label="发布时间" prop="publishTime">
              <el-date-picker 
                v-model="formData.publishTime" 
                type="datetime" 
                placeholder="选择发布时间"
                value-format="YYYY-MM-DD HH:mm:ss"
                style="width: 100%;"
              />
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="是否置顶" prop="isTop">
              <el-switch 
                v-model="formData.isTop"
                active-text="是"
                inactive-text="否"
              />
            </el-form-item>
          </el-col>
        </el-row>
        
        <el-form-item label="内容" prop="content">
          <RichEditor v-model="formData.content" placeholder="请输入内容..." />
        </el-form-item>
        
        <el-form-item label="附件上传">
          <FileUpload 
            ref="fileUploadRef"
            v-model="formData.attachments"
            :category="activeTab"
            :max-size-m-b="10"
            :limit="10"
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
import { ref, reactive, onMounted, nextTick } from 'vue'
import { ElMessage, ElMessageBox } from 'element-plus'
import { Plus, Search, Refresh } from '@element-plus/icons-vue'
import { infoPublishApi } from '@/api'
import RichEditor from '@/components/RichEditor.vue'
import FileUpload from '@/components/FileUpload.vue'

// 当前激活的Tab
const activeTab = ref('company_announcements')

// Tab配置
const tabConfig = {
  'company_announcements': '公司公告',
  'policy_regulations': '政策法规',
  'policy_information': '政策信息',
  'notice_announcements': '通知公告'
}

// 搜索表单
const searchForm = reactive({
  keyword: ''
})

// 分页信息
const pagination = reactive({
  pageIndex: 1,
  pageSize: 10,
  total: 0
})

// 表格数据
const tableData = ref([])
const loading = ref(false)

// 对话框
const dialogVisible = ref(false)
const isEdit = ref(false)
const submitting = ref(false)
const formRef = ref()
const fileUploadRef = ref()

// 表单数据
const formData = reactive({
  id: null,
  category: '',
  title: '',
  content: '',
  publishTime: null,
  isTop: false,
  attachments: []
})

// 表单验证规则
const formRules = {
  title: [
    { required: true, message: '请输入标题', trigger: 'blur' },
    { max: 255, message: '标题最多255个字符', trigger: 'blur' }
  ],
  content: [
    { required: true, message: '请输入内容', trigger: 'blur' }
  ]
}

/**
 * 加载数据
 */
const loadData = async () => {
  loading.value = true
  try {
    const params = {
      category: activeTab.value,
      keyword: searchForm.keyword,
      pageIndex: pagination.pageIndex,
      pageSize: pagination.pageSize
    }
    
    const res = await infoPublishApi.getPagedList(params)
    
    if (res.success && res.data) {
      tableData.value = res.data.items || []
      pagination.total = res.data.totalCount || 0
    } else {
      tableData.value = []
      pagination.total = 0
    }
  } catch (error) {
    console.error('加载数据失败:', error)
    ElMessage.error('加载数据失败')
    tableData.value = []
    pagination.total = 0
  } finally {
    loading.value = false
  }
}

/**
 * Tab切换
 */
const handleTabChange = () => {
  // 重置搜索和分页
  searchForm.keyword = ''
  pagination.pageIndex = 1
  loadData()
}

/**
 * 搜索
 */
const handleSearch = () => {
  pagination.pageIndex = 1
  loadData()
}

/**
 * 重置
 */
const handleReset = () => {
  searchForm.keyword = ''
  pagination.pageIndex = 1
  loadData()
}

/**
 * 页码变化
 */
const handlePageChange = () => {
  loadData()
}

/**
 * 每页数量变化
 */
const handlePageSizeChange = () => {
  pagination.pageIndex = 1
  loadData()
}

/**
 * 新增
 */
const handleAdd = () => {
  isEdit.value = false
  resetForm()
  dialogVisible.value = true
}

/**
 * 编辑
 */
const handleEdit = async (row) => {
  isEdit.value = true
  try {
    const res = await infoPublishApi.getById(row.id, activeTab.value)
    if (res.success && res.data) {
      formData.id = res.data.id
      formData.category = activeTab.value
      formData.title = res.data.title
      formData.content = res.data.content
      formData.publishTime = res.data.publishTime
      formData.isTop = res.data.isTop
      formData.attachments = res.data.attachments || []
      
      dialogVisible.value = true
    } else {
      ElMessage.error(res.message || '获取信息详情失败')
    }
  } catch (error) {
    console.error('获取信息详情失败:', error)
    ElMessage.error('获取信息详情失败')
  }
}

/**
 * 提交表单
 */
const handleSubmit = async () => {
  if (!formRef.value) return
  
  await formRef.value.validate(async (valid) => {
    if (!valid) return
    
    submitting.value = true
    
    try {
      // 创建FormData对象
      const formDataObj = new FormData()
      formDataObj.append('category', activeTab.value)
      formDataObj.append('title', formData.title)
      formDataObj.append('content', formData.content)
      formDataObj.append('isTop', formData.isTop)
      
      if (formData.publishTime) {
        formDataObj.append('publishTime', formData.publishTime)
      }
      
      // 添加新上传的文件
      if (fileUploadRef.value) {
        const newFiles = fileUploadRef.value.getNewFiles()
        newFiles.forEach(file => {
          formDataObj.append('files', file)
        })
      }
      
      let res
      if (isEdit.value) {
        // 编辑模式
        res = await infoPublishApi.update(formData.id, formDataObj)
      } else {
        // 新增模式
        res = await infoPublishApi.create(formDataObj)
      }
      
      if (res.success) {
        ElMessage.success(isEdit.value ? '更新成功' : '创建成功')
        dialogVisible.value = false
        loadData()
      } else {
        ElMessage.error(res.message || (isEdit.value ? '更新失败' : '创建失败'))
      }
    } catch (error) {
      console.error('提交失败:', error)
      ElMessage.error(isEdit.value ? '更新失败' : '创建失败')
    } finally {
      submitting.value = false
    }
  })
}

/**
 * 删除
 */
const handleDelete = async (row) => {
  try {
    await ElMessageBox.confirm(
      '确定要删除该信息吗？删除后将无法恢复。',
      '删除确认',
      {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        type: 'warning'
      }
    )
    
    const res = await infoPublishApi.delete(row.id, activeTab.value)
    
    if (res.success) {
      ElMessage.success('删除成功')
      
      // 如果当前页没有数据了，返回上一页
      if (tableData.value.length === 1 && pagination.pageIndex > 1) {
        pagination.pageIndex--
      }
      
      loadData()
    } else {
      ElMessage.error(res.message || '删除失败')
    }
  } catch (error) {
    if (error !== 'cancel') {
      console.error('删除失败:', error)
      ElMessage.error('删除失败')
    }
  }
}

/**
 * 重置表单
 */
const resetForm = () => {
  formData.id = null
  formData.category = activeTab.value
  formData.title = ''
  formData.content = ''
  formData.publishTime = null
  formData.isTop = false
  formData.attachments = []
  
  if (formRef.value) {
    formRef.value.clearValidate()
  }
}

/**
 * 格式化日期时间
 */
const formatDateTime = (dateTime) => {
  if (!dateTime) return '-'
  return dateTime.replace('T', ' ').substring(0, 19)
}

// 组件挂载时加载数据
onMounted(() => {
  loadData()
})
</script>

<style scoped>
.page-container {
  padding: 20px;
}

.card-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  font-size: 16px;
  font-weight: 500;
}

.search-form {
  margin-top: 20px;
  margin-bottom: 20px;
}

:deep(.el-table) {
  font-size: 14px;
}

:deep(.el-dialog__body) {
  max-height: 70vh;
  overflow-y: auto;
}

:deep(.el-form-item__label) {
  font-weight: 500;
}
</style>
