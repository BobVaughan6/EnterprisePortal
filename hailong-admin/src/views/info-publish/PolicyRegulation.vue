<template>
  <div class="page-container">
    <el-card>
      <template #header>
        <div class="card-header">
          <span>政策法规管理</span>
          <el-button type="primary" icon="Plus" @click="handleAdd">新增政策法规</el-button>
        </div>
      </template>
      
      <!-- 搜索区域 -->
      <el-form :model="searchForm" inline class="search-form">
        <el-form-item label="关键词">
          <el-input 
            v-model="searchForm.keyword" 
            placeholder="搜索标题、发布人" 
            clearable 
            style="width: 220px;"
          />
        </el-form-item>
        <el-form-item label="分类">
          <el-select v-model="searchForm.category" placeholder="请选择" clearable style="width: 150px;">
            <el-option label="法律法规" value="法律法规" />
            <el-option label="行政法规" value="行政法规" />
            <el-option label="地方政策" value="地方政策" />
          </el-select>
        </el-form-item>
        <el-form-item label="时间范围">
          <el-date-picker
            v-model="dateRange"
            type="daterange"
            range-separator="至"
            start-placeholder="开始日期"
            end-placeholder="结束日期"
            value-format="YYYY-MM-DD"
            style="width: 240px;"
          />
        </el-form-item>
        <el-form-item>
          <el-button type="primary" icon="Search" @click="handleSearch">搜索</el-button>
          <el-button icon="Refresh" @click="handleReset">重置</el-button>
        </el-form-item>
      </el-form>
      
      <!-- 表格 -->
      <el-table :data="tableData" v-loading="loading" border stripe>
        <el-table-column type="index" label="序号" width="60" />
        <el-table-column prop="title" label="标题" min-width="200" show-overflow-tooltip />
        <el-table-column prop="category" label="分类" width="100" align="center" />
        <el-table-column prop="publisher" label="发布人" width="120" align="center" />
        <el-table-column prop="publishTime" label="发布时间" width="110" align="center">
          <template #default="{ row }">
            {{ formatDate(row.publishTime) }}
          </template>
        </el-table-column>
        <el-table-column prop="viewCount" label="浏览量" width="80" align="center" />
        <el-table-column prop="isTop" label="置顶" width="80" align="center">
          <template #default="{ row }">
            <el-tag :type="row.isTop ? 'success' : 'info'" size="small">
              {{ row.isTop ? '是' : '否' }}
            </el-tag>
          </template>
        </el-table-column>
        <el-table-column prop="status" label="状态" width="80" align="center">
          <template #default="{ row }">
            <el-tag :type="row.status === 1 ? 'success' : 'danger'" size="small">
              {{ row.status === 1 ? '启用' : '禁用' }}
            </el-tag>
          </template>
        </el-table-column>
        <el-table-column label="操作" width="200" fixed="right" align="center">
          <template #default="{ row }">
            <el-button type="primary" size="small" link @click="handleEdit(row)">编辑</el-button>
            <el-button 
              :type="row.isTop ? 'warning' : 'success'" 
              size="small" 
              link 
              @click="handleToggleTop(row)"
            >
              {{ row.isTop ? '取消置顶' : '置顶' }}
            </el-button>
            <el-button type="danger" size="small" link @click="handleDelete(row)">删除</el-button>
          </template>
        </el-table-column>
      </el-table>
      
      <!-- 分页 -->
      <el-pagination
        v-model:current-page="pagination.pageNumber"
        v-model:page-size="pagination.pageSize"
        :total="pagination.total"
        :page-sizes="[10, 20, 50, 100]"
        layout="total, sizes, prev, pager, next, jumper"
        @size-change="loadData"
        @current-change="loadData"
        style="margin-top: 20px; justify-content: flex-end;"
      />
    </el-card>
    
    <!-- 新增/编辑对话框 -->
    <el-dialog
      v-model="dialogVisible"
      :title="isEdit ? '编辑政策法规' : '新增政策法规'"
      width="900px"
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
            <el-form-item label="分类" prop="category">
              <el-select v-model="formData.category" placeholder="请选择分类" style="width: 100%;">
                <el-option label="法律法规" value="法律法规" />
                <el-option label="行政法规" value="行政法规" />
                <el-option label="地方政策" value="地方政策" />
              </el-select>
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="发布人" prop="publisher">
              <el-input
                v-model="formData.publisher"
                placeholder="请输入发布人"
                maxlength="50"
              />
            </el-form-item>
          </el-col>
        </el-row>
        
        <el-form-item label="发布时间" prop="publishTime">
          <el-date-picker
            v-model="formData.publishTime"
            type="datetime"
            placeholder="选择发布时间"
            value-format="YYYY-MM-DD HH:mm:ss"
            style="width: 100%;"
          />
        </el-form-item>
        
        <el-form-item label="摘要" prop="summary">
          <el-input
            v-model="formData.summary"
            type="textarea"
            :rows="3"
            placeholder="请输入摘要（最多500个字符）"
            maxlength="500"
            show-word-limit
          />
        </el-form-item>
        
        <el-form-item label="封面图片" prop="coverImageId">
          <FileUpload
            v-model="formData.coverImageId"
            file-type="image"
            :limit="1"
            list-type="picture-card"
          />
        </el-form-item>
        
        <el-form-item label="内容" prop="content">
          <RichEditor v-model="formData.content" />
        </el-form-item>
        
        <el-form-item label="附件" prop="attachmentIds">
          <FileUpload
            v-model="formData.attachmentIds"
            :limit="10"
            list-type="text"
          />
        </el-form-item>
        
        <el-row :gutter="20">
          <el-col :span="12">
            <el-form-item label="是否置顶" prop="isTop">
              <el-switch v-model="formData.isTop" />
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="状态" prop="status">
              <el-radio-group v-model="formData.status">
                <el-radio :label="1">启用</el-radio>
                <el-radio :label="0">禁用</el-radio>
              </el-radio-group>
            </el-form-item>
          </el-col>
        </el-row>
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
import { infoPublicationApi } from '@/api'
import RichEditor from '@/components/RichEditor.vue'
import FileUpload from '@/components/FileUpload.vue'

// 日期范围
const dateRange = ref([])

// 搜索表单
const searchForm = reactive({
  keyword: '',
  category: '',
  startDate: '',
  endDate: ''
})

// 分页信息
const pagination = reactive({
  pageNumber: 1,
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
const formRef = ref(null)

// 表单数据
const formData = reactive({
  id: null,
  type: 'POLICY_REGULATION', // 固定为政策法规
  category: '',
  title: '',
  summary: '',
  content: '',
  coverImageId: null,
  publisher: '',
  publishTime: '',
  attachmentIds: [],
  isTop: false,
  status: 1
})

// 表单验证规则
const formRules = {
  title: [
    { required: true, message: '请输入标题', trigger: 'blur' },
    { max: 255, message: '标题长度不能超过255个字符', trigger: 'blur' }
  ],
  category: [
    { required: true, message: '请选择分类', trigger: 'change' }
  ],
  content: [
    { required: true, message: '请输入内容', trigger: 'blur' }
  ],
  publishTime: [
    { required: true, message: '请选择发布时间', trigger: 'change' }
  ]
}

/**
 * 格式化日期
 */
const formatDate = (dateStr) => {
  if (!dateStr) return '-'
  const date = new Date(dateStr)
  return date.toLocaleDateString('zh-CN')
}

/**
 * 加载数据
 */
const loadData = async () => {
  loading.value = true
  try {
    // 处理时间范围
    if (dateRange.value && dateRange.value.length === 2) {
      searchForm.startDate = dateRange.value[0]
      searchForm.endDate = dateRange.value[1]
    } else {
      searchForm.startDate = ''
      searchForm.endDate = ''
    }
    
    const params = {
      type: 'POLICY_REGULATION', // 固定为政策法规
      keyword: searchForm.keyword || undefined,
      category: searchForm.category || undefined,
      startDate: searchForm.startDate || undefined,
      endDate: searchForm.endDate || undefined,
      pageNumber: pagination.pageNumber,
      pageSize: pagination.pageSize
    }
    
    const res = await infoPublicationApi.getInfoPublicationList(params)
    
    if (res.success && res.data) {
      tableData.value = res.data.items || []
      pagination.total = res.data.totalCount || 0
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

/**
 * 搜索
 */
const handleSearch = () => {
  pagination.pageNumber = 1
  loadData()
}

/**
 * 重置
 */
const handleReset = () => {
  searchForm.keyword = ''
  searchForm.category = ''
  dateRange.value = []
  handleSearch()
}

/**
 * 新增
 */
const handleAdd = () => {
  isEdit.value = false
  Object.assign(formData, {
    id: null,
    type: 'POLICY_REGULATION',
    category: '',
    title: '',
    summary: '',
    content: '',
    coverImageId: null,
    publisher: '',
    publishTime: new Date().toISOString().slice(0, 19).replace('T', ' '),
    attachmentIds: [],
    isTop: false,
    status: 1
  })
  dialogVisible.value = true
}

/**
 * 编辑
 */
const handleEdit = async (row) => {
  isEdit.value = true
  try {
    const res = await infoPublicationApi.getInfoPublicationDetail(row.id)
    if (res.success && res.data) {
      Object.assign(formData, {
        id: res.data.id,
        type: res.data.type,
        category: res.data.category || '',
        title: res.data.title,
        summary: res.data.summary || '',
        content: res.data.content,
        coverImageId: res.data.coverImageId,
        publisher: res.data.publisher || '',
        publishTime: res.data.publishTime,
        attachmentIds: res.data.attachmentIds || [],
        isTop: res.data.isTop,
        status: res.data.status
      })
      dialogVisible.value = true
    } else {
      ElMessage.error(res.message || '获取详情失败')
    }
  } catch (error) {
    console.error('获取详情失败:', error)
    ElMessage.error('获取详情失败，请稍后重试')
  }
}

/**
 * 置顶/取消置顶
 */
const handleToggleTop = async (row) => {
  try {
    const newIsTop = !row.isTop
    const res = await infoPublicationApi.updateInfoPublication(row.id, {
      isTop: newIsTop
    })
    
    if (res.success) {
      ElMessage.success(newIsTop ? '置顶成功' : '取消置顶成功')
      loadData()
    } else {
      ElMessage.error(res.message || '操作失败')
    }
  } catch (error) {
    console.error('操作失败:', error)
    ElMessage.error('操作失败，请稍后重试')
  }
}

/**
 * 删除
 */
const handleDelete = async (row) => {
  try {
    await ElMessageBox.confirm(
      `确定要删除"${row.title}"吗？删除后将无法恢复。`, 
      '删除确认', 
      {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        type: 'warning'
      }
    )
    
    const res = await infoPublicationApi.deleteInfoPublication(row.id)
    if (res.success) {
      ElMessage.success(res.message || '删除成功')
      // 如果当前页只有一条数据且不是第一页，则返回上一页
      if (tableData.value.length === 1 && pagination.pageNumber > 1) {
        pagination.pageNumber--
      }
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

/**
 * 提交表单
 */
const handleSubmit = async () => {
  if (!formRef.value) return
  
  try {
    await formRef.value.validate()
  } catch (error) {
    ElMessage.warning('请正确填写表单')
    return
  }
  
  submitting.value = true
  try {
    const submitData = {
      type: formData.type,
      category: formData.category,
      title: formData.title,
      summary: formData.summary || null,
      content: formData.content,
      coverImageId: formData.coverImageId || null,
      publisher: formData.publisher || null,
      publishTime: formData.publishTime,
      attachmentIds: formData.attachmentIds,
      isTop: formData.isTop,
      status: formData.status
    }
    
    let res
    if (isEdit.value) {
      res = await infoPublicationApi.updateInfoPublication(formData.id, submitData)
    } else {
      res = await infoPublicationApi.createInfoPublication(submitData)
    }
    
    if (res.success) {
      ElMessage.success(res.message || (isEdit.value ? '更新成功' : '创建成功'))
      dialogVisible.value = false
      loadData()
    } else {
      ElMessage.error(res.message || (isEdit.value ? '更新失败' : '创建失败'))
    }
  } catch (error) {
    console.error('提交失败:', error)
    ElMessage.error(isEdit.value ? '更新失败，请稍后重试' : '创建失败，请稍后重试')
  } finally {
    submitting.value = false
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

.search-form {
  margin-bottom: 16px;
}

.search-form :deep(.el-form-item) {
  margin-bottom: 10px;
}
</style>