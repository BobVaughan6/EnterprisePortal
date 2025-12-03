<template>
  <div class="page-container">
    <el-card>
      <template #header>
        <span>企业信息</span>
      </template>
      
      <el-form :model="formData" label-width="120px" v-loading="loading">
        <el-form-item label="标题" required>
          <el-input v-model="formData.title" placeholder="请输入标题" />
        </el-form-item>
        <el-form-item label="图片地址">
          <el-input v-model="formData.imageUrl" placeholder="请输入图片URL" />
        </el-form-item>
        <el-form-item label="企业简介" required>
          <RichEditor v-model="formData.content" />
        </el-form-item>
        <el-form-item>
          <el-button type="primary" @click="handleSubmit" :loading="submitting">保存</el-button>
        </el-form-item>
      </el-form>
    </el-card>
  </div>
</template>

<script setup>
import { ref, reactive, onMounted } from 'vue'
import { ElMessage } from 'element-plus'
import { configApi } from '@/api'
import RichEditor from '@/components/RichEditor.vue'

const loading = ref(false)
const submitting = ref(false)

const formData = reactive({
  title: '',
  content: '',
  imageUrl: ''
})

const loadData = async () => {
  loading.value = true
  try {
    const res = await configApi.getCompanyProfile()
    if (res.success && res.data) {
      Object.assign(formData, res.data)
    }
  } catch (error) {
    console.error('加载数据失败:', error)
  } finally {
    loading.value = false
  }
}

const handleSubmit = async () => {
  submitting.value = true
  try {
    const res = await configApi.updateCompanyProfile(formData)
    if (res.success) {
      ElMessage.success('保存成功')
      loadData()
    }
  } catch (error) {
    ElMessage.error('保存失败')
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
</style>
