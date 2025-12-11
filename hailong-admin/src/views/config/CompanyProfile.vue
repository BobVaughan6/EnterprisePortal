<template>
  <div class="page-container">
    <el-card>
      <template #header>
        <span>企业信息</span>
      </template>
      
      <el-form :model="formData" :rules="rules" ref="formRef" label-width="120px" v-loading="loading">
        <el-form-item label="标题" prop="title">
          <el-input v-model="formData.title" placeholder="请输入标题" maxlength="200" show-word-limit />
        </el-form-item>
        
        <el-form-item label="企业图片">
          <FileUpload
            v-model="formData.imageUrls"
            file-type="image"
            :limit="5"
            :multiple="true"
            list-type="picture-card"
          />
          <div class="form-tip">建议尺寸：1200x800，支持上传多张图片，最多5张</div>
        </el-form-item>
        
        <el-form-item label="企业简介" prop="content">
          <RichEditor v-model="formData.content" :height="400" />
        </el-form-item>
        
        <el-form-item label="联系电话">
          <el-input v-model="formData.contactPhone" placeholder="请输入联系电话" maxlength="50" />
        </el-form-item>
        
        <el-form-item label="联系邮箱">
          <el-input v-model="formData.contactEmail" placeholder="请输入联系邮箱" maxlength="100" />
        </el-form-item>
        
        <el-form-item label="公司地址">
          <el-input v-model="formData.address" placeholder="请输入公司地址" maxlength="200" />
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
        
        <el-form-item>
          <el-button type="primary" @click="handleSubmit" :loading="submitting">保存</el-button>
          <el-button @click="handleReset">重置</el-button>
        </el-form-item>
      </el-form>
    </el-card>
  </div>
</template>

<script setup>
import { ref, reactive, onMounted } from 'vue'
import { ElMessage } from 'element-plus'
import { systemConfigApi } from '@/api'
import RichEditor from '@/components/RichEditor.vue'
import FileUpload from '@/components/FileUpload.vue'

const loading = ref(false)
const submitting = ref(false)
const formRef = ref(null)

const formData = reactive({
  title: '',
  content: '',
  imageUrls: [],
  contactPhone: '',
  contactEmail: '',
  address: '',
  status: 1
})

const rules = {
  title: [
    { required: true, message: '请输入标题', trigger: 'blur' },
    { min: 2, max: 200, message: '标题长度在 2 到 200 个字符', trigger: 'blur' }
  ],
  content: [
    { required: true, message: '请输入企业简介', trigger: 'blur' }
  ]
}

const loadData = async () => {
  loading.value = true
  try {
    const res = await systemConfigApi.companyProfile.get()
    if (res.success && res.data) {
      Object.assign(formData, {
        title: res.data.title || '',
        content: res.data.content || '',
        imageUrls: res.data.imageUrls || [],
        contactPhone: res.data.contactPhone || '',
        contactEmail: res.data.contactEmail || '',
        address: res.data.address || '',
        status: res.data.status ?? 1
      })
    }
  } catch (error) {
    console.error('加载数据失败:', error)
    ElMessage.error('加载数据失败，请稍后重试')
  } finally {
    loading.value = false
  }
}

const handleSubmit = async () => {
  if (!formRef.value) return
  
  await formRef.value.validate(async (valid) => {
    if (!valid) return
    
    submitting.value = true
    try {
      const res = await systemConfigApi.companyProfile.update(formData)
      if (res.success) {
        ElMessage.success('保存成功')
        loadData()
      } else {
        ElMessage.error(res.message || '保存失败')
      }
    } catch (error) {
      console.error('保存失败:', error)
      ElMessage.error('保存失败，请稍后重试')
    } finally {
      submitting.value = false
    }
  })
}

const handleReset = () => {
  loadData()
}

onMounted(() => {
  loadData()
})
</script>

<style scoped>
.page-container {
  padding: 0;
}

.form-tip {
  font-size: 12px;
  color: #999;
  margin-top: 5px;
}
</style>
