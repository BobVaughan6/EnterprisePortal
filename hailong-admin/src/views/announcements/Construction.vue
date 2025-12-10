<template>
  <div class="page-container">
    <el-card>
      <template #header>
        <div class="card-header">
          <span>建设工程公告管理</span>
          <el-button type="primary" icon="Plus" @click="handleAdd">新增公告</el-button>
        </div>
      </template>
      
      <!-- 搜索区域 -->
      <el-form :model="searchForm" inline class="search-form">
        <el-form-item label="关键词">
          <el-input 
            v-model="searchForm.keyword" 
            placeholder="搜索标题、招标人、中标人" 
            clearable 
            style="width: 220px;"
          />
        </el-form-item>
        <el-form-item label="公告类型">
          <el-select v-model="searchForm.type" placeholder="请选择" clearable style="width: 150px;">
            <el-option label="招标公告" value="招标公告" />
            <el-option label="中标公告" value="中标公告" />
            <el-option label="变更公告" value="变更公告" />
          </el-select>
        </el-form-item>
        <el-form-item label="项目区域">
          <el-select v-model="searchForm.region" placeholder="请选择" clearable style="width: 150px;">
            <el-option label="郑州" value="郑州" />
            <el-option label="洛阳" value="洛阳" />
            <el-option label="开封" value="开封" />
            <el-option label="新乡" value="新乡" />
            <el-option label="南阳" value="南阳" />
            <el-option label="安阳" value="安阳" />
            <el-option label="商丘" value="商丘" />
            <el-option label="平顶山" value="平顶山" />
            <el-option label="许昌" value="许昌" />
            <el-option label="焦作" value="焦作" />
            <el-option label="周口" value="周口" />
            <el-option label="信阳" value="信阳" />
            <el-option label="驻马店" value="驻马店" />
            <el-option label="漯河" value="漯河" />
            <el-option label="濮阳" value="濮阳" />
            <el-option label="鹤壁" value="鹤壁" />
            <el-option label="三门峡" value="三门峡" />
            <el-option label="济源" value="济源" />
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
        <el-table-column prop="noticeType" label="公告类型" width="100" align="center" />
        <el-table-column prop="bidder" label="招标人" min-width="150" show-overflow-tooltip />
        <el-table-column prop="winner" label="中标人" min-width="150" show-overflow-tooltip />
        <el-table-column prop="projectRegion" label="项目区域" width="100" align="center" />
        <el-table-column prop="publishTime" label="发布时间" width="110" align="center">
          <template #default="{ row }">
            {{ formatDate(row.publishTime) }}
          </template>
        </el-table-column>
        <el-table-column prop="viewCount" label="访问量" width="80" align="center" />
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
        @size-change="loadData"
        @current-change="loadData"
        style="margin-top: 20px; justify-content: flex-end;"
      />
    </el-card>
    
    <!-- 新增/编辑对话框 -->
    <el-dialog
      v-model="dialogVisible"
      :title="isEdit ? '编辑公告' : '新增公告'"
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
        <el-form-item label="公告标题" prop="title">
          <el-input 
            v-model="formData.title" 
            placeholder="请输入公告标题（最多255个字符）" 
            maxlength="255"
            show-word-limit
          />
        </el-form-item>
        
        <el-row :gutter="20">
          <el-col :span="12">
            <el-form-item label="公告类型" prop="noticeType">
              <el-select v-model="formData.noticeType" placeholder="请选择公告类型" style="width: 100%;">
                <el-option label="招标公告" value="bidding" />
                <el-option label="中标公告" value="result" />
                <el-option label="变更公告" value="correction" />
              </el-select>
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="项目区域" prop="projectRegion">
              <el-select v-model="formData.projectRegion" placeholder="请选择项目区域" style="width: 100%;">
                <el-option label="郑州" value="郑州" />
                <el-option label="洛阳" value="洛阳" />
                <el-option label="开封" value="开封" />
                <el-option label="新乡" value="新乡" />
                <el-option label="南阳" value="南阳" />
                <el-option label="安阳" value="安阳" />
                <el-option label="商丘" value="商丘" />
                <el-option label="平顶山" value="平顶山" />
                <el-option label="许昌" value="许昌" />
                <el-option label="焦作" value="焦作" />
                <el-option label="周口" value="周口" />
                <el-option label="信阳" value="信阳" />
                <el-option label="驻马店" value="驻马店" />
                <el-option label="漯河" value="漯河" />
                <el-option label="濮阳" value="濮阳" />
                <el-option label="鹤壁" value="鹤壁" />
                <el-option label="三门峡" value="三门峡" />
                <el-option label="济源" value="济源" />
              </el-select>
            </el-form-item>
          </el-col>
        </el-row>
        
        <el-row :gutter="20">
          <el-col :span="12">
            <el-form-item label="招标人" prop="bidder">
              <el-input
                v-model="formData.bidder"
                placeholder="请输入招标人名称（最多255个字符）"
                maxlength="255"
              />
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="中标人" prop="winner">
              <el-input
                v-model="formData.winner"
                placeholder="请输入中标人名称（最多255个字符）"
                maxlength="255"
              />
            </el-form-item>
          </el-col>
        </el-row>
        
        <el-row :gutter="20">
          <el-col :span="12">
            <el-form-item label="预算金额" prop="budgetAmount">
              <el-input-number
                v-model="formData.budgetAmount"
                :min="0"
                :precision="2"
                placeholder="请输入预算金额"
                style="width: 100%;"
              />
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="截止时间" prop="deadline">
              <el-date-picker
                v-model="formData.deadline"
                type="datetime"
                placeholder="选择截止时间"
                value-format="YYYY-MM-DD HH:mm:ss"
                style="width: 100%;"
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
        
        <el-form-item label="公告内容" prop="content">
          <RichEditor v-model="formData.content" />
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
import { announcementApi } from '@/api'
import RichEditor from '@/components/RichEditor.vue'

// 日期范围
const dateRange = ref([])

// 搜索表单
const searchForm = reactive({
  keyword: '',
  type: '',
  region: '',
  startDate: '',
  endDate: ''
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
const formRef = ref(null)

// 表单数据
const formData = reactive({
  id: null,
  title: '',
  businessType: 'CONSTRUCTION', // 固定为建设工程
  noticeType: '',
  content: '',
  bidder: '', // 招标人
  winner: '', // 中标人
  province: '',
  city: '',
  district: '',
  projectRegion: '',
  publishTime: '',
  budgetAmount: null,
  deadline: '',
  attachmentIds: []
})

// 表单验证规则
const formRules = {
  title: [
    { required: true, message: '请输入公告标题', trigger: 'blur' },
    { max: 500, message: '标题长度不能超过500个字符', trigger: 'blur' }
  ],
  noticeType: [
    { required: true, message: '请选择公告类型', trigger: 'change' }
  ],
  content: [
    { required: true, message: '请输入公告内容', trigger: 'blur' }
  ],
  projectRegion: [
    { required: true, message: '请选择项目区域', trigger: 'change' }
  ],
  publishTime: [
    { required: true, message: '请选择发布日期', trigger: 'change' }
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
      keyword: searchForm.keyword || undefined,
      businessType: 'CONSTRUCTION', // 固定为建设工程
      noticeType: searchForm.type || undefined,
      projectRegion: searchForm.region || undefined,
      startDate: searchForm.startDate || undefined,
      endDate: searchForm.endDate || undefined,
      page: pagination.pageIndex,
      pageSize: pagination.pageSize
    }
    
    const res = await announcementApi.getAnnouncementList(params)
    
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
  pagination.pageIndex = 1
  loadData()
}

/**
 * 重置
 */
const handleReset = () => {
  searchForm.keyword = ''
  searchForm.type = ''
  searchForm.region = ''
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
    title: '',
    businessType: 'CONSTRUCTION',
    noticeType: '',
    content: '',
    bidder: '',
    winner: '',
    province: '',
    city: '',
    district: '',
    projectRegion: '',
    publishTime: new Date().toISOString().slice(0, 19).replace('T', ' '),
    budgetAmount: null,
    deadline: '',
    attachmentIds: []
  })
  dialogVisible.value = true
}

/**
 * 编辑
 */
const handleEdit = async (row) => {
  isEdit.value = true
  try {
    const res = await announcementApi.getAnnouncementDetail(row.id)
    if (res.success && res.data) {
      Object.assign(formData, {
        id: res.data.id,
        title: res.data.title,
        businessType: res.data.businessType,
        noticeType: res.data.noticeType,
        content: res.data.content,
        bidder: res.data.bidder || '',
        winner: res.data.winner || '',
        province: res.data.province || '',
        city: res.data.city || '',
        district: res.data.district || '',
        projectRegion: res.data.projectRegion || '',
        publishTime: res.data.publishTime,
        budgetAmount: res.data.budgetAmount,
        deadline: res.data.deadline || '',
        attachmentIds: res.data.attachmentIds || []
      })
      dialogVisible.value = true
    } else {
      ElMessage.error(res.message || '获取公告详情失败')
    }
  } catch (error) {
    console.error('获取公告详情失败:', error)
    ElMessage.error('获取公告详情失败，请稍后重试')
  }
}

/**
 * 删除
 */
const handleDelete = async (row) => {
  try {
    await ElMessageBox.confirm(
      `确定要删除公告"${row.title}"吗？删除后将无法恢复。`, 
      '删除确认', 
      {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        type: 'warning'
      }
    )
    
    const res = await announcementApi.deleteAnnouncement(row.id)
    if (res.success) {
      ElMessage.success(res.message || '删除成功')
      // 如果当前页只有一条数据且不是第一页，则返回上一页
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
      title: formData.title,
      businessType: formData.businessType,
      noticeType: formData.noticeType,
      content: formData.content,
      bidder: formData.bidder || null,
      winner: formData.winner || null,
      province: formData.province || null,
      city: formData.city || null,
      district: formData.district || null,
      projectRegion: formData.projectRegion,
      publishTime: formData.publishTime,
      budgetAmount: formData.budgetAmount,
      deadline: formData.deadline || null,
      attachmentIds: formData.attachmentIds,
      status: 1,
      isTop: 0
    }
    
    let res
    if (isEdit.value) {
      res = await announcementApi.updateAnnouncement(formData.id, submitData)
    } else {
      res = await announcementApi.createAnnouncement(submitData)
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
