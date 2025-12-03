<template>
  <div class="page-container">
    <el-card>
      <template #header>
        <div class="card-header">
          <span>政府采购公告管理</span>
          <el-button type="primary" icon="Plus" @click="handleAdd">新增公告</el-button>
        </div>
      </template>
      
      <!-- 搜索区域 -->
      <el-form :model="searchForm" inline>
        <el-form-item label="标题">
          <el-input v-model="searchForm.title" placeholder="请输入标题" clearable />
        </el-form-item>
        <el-form-item label="公告类型">
          <el-select v-model="searchForm.announcementType" placeholder="请选择" clearable>
            <el-option label="招标公告" value="招标公告" />
            <el-option label="中标公告" value="中标公告" />
            <el-option label="更正公告" value="更正公告" />
          </el-select>
        </el-form-item>
        <el-form-item>
          <el-button type="primary" icon="Search" @click="handleSearch">搜索</el-button>
          <el-button icon="Refresh" @click="handleReset">重置</el-button>
        </el-form-item>
      </el-form>
      
      <!-- 表格 -->
      <el-table :data="tableData" v-loading="loading" border stripe>
        <el-table-column prop="id" label="ID" width="80" />
        <el-table-column prop="title" label="标题" min-width="200" show-overflow-tooltip />
        <el-table-column prop="announcementType" label="公告类型" width="120" />
        <el-table-column prop="projectName" label="项目名称" min-width="180" show-overflow-tooltip />
        <el-table-column prop="publishDate" label="发布日期" width="120" />
        <el-table-column prop="viewCount" label="浏览量" width="100" />
        <el-table-column label="操作" width="180" fixed="right">
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
    
    <!-- 编辑对话框（示例，需完整实现） -->
    <el-dialog
      v-model="dialogVisible"
      :title="isEdit ? '编辑公告' : '新增公告'"
      width="800px"
      destroy-on-close
    >
      <el-form :model="formData" label-width="120px">
        <el-form-item label="公告标题" required>
          <el-input v-model="formData.title" placeholder="请输入公告标题" />
        </el-form-item>
        <el-form-item label="公告类型" required>
          <el-select v-model="formData.announcementType" placeholder="请选择">
            <el-option label="招标公告" value="招标公告" />
            <el-option label="中标公告" value="中标公告" />
            <el-option label="更正公告" value="更正公告" />
          </el-select>
        </el-form-item>
        <el-form-item label="项目名称">
          <el-input v-model="formData.projectName" placeholder="请输入项目名称" />
        </el-form-item>
        <el-form-item label="发布日期">
          <el-date-picker v-model="formData.publishDate" type="date" placeholder="选择日期" />
        </el-form-item>
        <el-form-item label="公告内容" required>
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
import { govProcurementApi } from '@/api'
import RichEditor from '@/components/RichEditor.vue'

// 搜索表单
const searchForm = reactive({
  title: '',
  announcementType: ''
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

// 表单数据
const formData = reactive({
  title: '',
  announcementType: '',
  projectName: '',
  publishDate: '',
  content: ''
})

/**
 * 加载数据
 */
const loadData = async () => {
  loading.value = true
  try {
    const params = {
      ...searchForm,
      pageIndex: pagination.pageIndex,
      pageSize: pagination.pageSize
    }
    
    const res = await govProcurementApi.getAnnouncements(params)
    
    if (res.success && res.data) {
      tableData.value = res.data.items || []
      pagination.total = res.data.totalCount || 0
    }
  } catch (error) {
    console.error('加载数据失败:', error)
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
  searchForm.title = ''
  searchForm.announcementType = ''
  handleSearch()
}

/**
 * 新增
 */
const handleAdd = () => {
  isEdit.value = false
  Object.assign(formData, {
    title: '',
    announcementType: '',
    projectName: '',
    publishDate: '',
    content: ''
  })
  dialogVisible.value = true
}

/**
 * 编辑
 */
const handleEdit = async (row) => {
  isEdit.value = true
  try {
    const res = await govProcurementApi.getAnnouncement(row.id)
    if (res.success && res.data) {
      Object.assign(formData, res.data)
      dialogVisible.value = true
    }
  } catch (error) {
    ElMessage.error('获取公告详情失败')
  }
}

/**
 * 删除
 */
const handleDelete = async (row) => {
  try {
    await ElMessageBox.confirm('确定要删除该公告吗？', '提示', {
      confirmButtonText: '确定',
      cancelButtonText: '取消',
      type: 'warning'
    })
    
    const res = await govProcurementApi.deleteAnnouncement(row.id)
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

/**
 * 提交表单
 */
const handleSubmit = async () => {
  submitting.value = true
  try {
    let res
    if (isEdit.value) {
      res = await govProcurementApi.updateAnnouncement(formData.id, formData)
    } else {
      res = await govProcurementApi.createAnnouncement(formData)
    }
    
    if (res.success) {
      ElMessage.success(isEdit.value ? '更新成功' : '创建成功')
      dialogVisible.value = false
      loadData()
    }
  } catch (error) {
    ElMessage.error(isEdit.value ? '更新失败' : '创建失败')
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
</style>
