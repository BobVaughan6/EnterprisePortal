<template>
  <div class="page-container">
    <el-card>
      <template #header>
        <div class="card-header">
          <span>{{ pageTitle }}</span>
          <el-button type="primary" icon="Plus" @click="handleAdd">发布信息</el-button>
        </div>
      </template>
      
      <!-- 搜索区域 -->
      <el-form :model="searchForm" inline>
        <el-form-item label="标题">
          <el-input v-model="searchForm.keyword" placeholder="请输入标题关键字" clearable />
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
        <el-table-column prop="publishTime" label="发布时间" width="180" />
        <el-table-column prop="viewCount" label="浏览量" width="100" />
        <el-table-column prop="isTop" label="置顶" width="80">
          <template #default="{ row }">
            <el-tag :type="row.isTop ? 'success' : 'info'">
              {{ row.isTop ? '是' : '否' }}
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
    
    <!-- 编辑对话框 -->
    <el-dialog
      v-model="dialogVisible"
      :title="isEdit ? '编辑信息' : '发布信息'"
      width="900px"
      destroy-on-close
    >
      <el-form :model="formData" label-width="100px">
        <el-form-item label="标题" required>
          <el-input v-model="formData.title" placeholder="请输入标题" />
        </el-form-item>
        <el-form-item label="发布时间">
          <el-date-picker 
            v-model="formData.publishTime" 
            type="datetime" 
            placeholder="选择日期时间"
            value-format="YYYY-MM-DD HH:mm:ss"
          />
        </el-form-item>
        <el-form-item label="是否置顶">
          <el-switch v-model="formData.isTop" />
        </el-form-item>
        <el-form-item label="内容" required>
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
import { ref, reactive, computed, onMounted } from 'vue'
import { useRoute } from 'vue-router'
import { ElMessage, ElMessageBox } from 'element-plus'
import { infoPublishApi } from '@/api'
import RichEditor from '@/components/RichEditor.vue'

const route = useRoute()

// 当前分类
const category = computed(() => route.meta.category || 'company_news')

// 页面标题
const pageTitle = computed(() => route.meta.title || '信息发布')

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

// 表单数据
const formData = reactive({
  category: '',
  title: '',
  content: '',
  publishTime: null,
  isTop: false
})

/**
 * 加载数据
 */
const loadData = async () => {
  loading.value = true
  try {
    const params = {
      category: category.value,
      keyword: searchForm.keyword,
      pageIndex: pagination.pageIndex,
      pageSize: pagination.pageSize
    }
    
    const res = await infoPublishApi.getPagedList(params)
    
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
  searchForm.keyword = ''
  handleSearch()
}

/**
 * 新增
 */
const handleAdd = () => {
  isEdit.value = false
  Object.assign(formData, {
    category: category.value,
    title: '',
    content: '',
    publishTime: null,
    isTop: false
  })
  dialogVisible.value = true
}

/**
 * 编辑
 */
const handleEdit = async (row) => {
  isEdit.value = true
  try {
    const res = await infoPublishApi.getById(row.id, category.value)
    if (res.success && res.data) {
      Object.assign(formData, res.data)
      dialogVisible.value = true
    }
  } catch (error) {
    ElMessage.error('获取详情失败')
  }
}

/**
 * 删除
 */
const handleDelete = async (row) => {
  try {
    await ElMessageBox.confirm('确定要删除该信息吗？', '提示', {
      confirmButtonText: '确定',
      cancelButtonText: '取消',
      type: 'warning'
    })
    
    const res = await infoPublishApi.delete(row.id, category.value)
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
    const formDataObj = new FormData()
    formDataObj.append('category', formData.category)
    formDataObj.append('title', formData.title)
    formDataObj.append('content', formData.content)
    formDataObj.append('isTop', formData.isTop)
    if (formData.publishTime) {
      formDataObj.append('publishTime', formData.publishTime)
    }
    
    let res
    if (isEdit.value) {
      res = await infoPublishApi.update(formData.id, category.value, formDataObj)
    } else {
      res = await infoPublishApi.create(formDataObj)
    }
    
    if (res.success) {
      ElMessage.success(isEdit.value ? '更新成功' : '发布成功')
      dialogVisible.value = false
      loadData()
    }
  } catch (error) {
    ElMessage.error(isEdit.value ? '更新失败' : '发布失败')
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
