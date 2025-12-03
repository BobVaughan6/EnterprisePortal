<template>
  <div class="file-upload">
    <el-upload
      ref="uploadRef"
      v-model:file-list="fileList"
      :action="uploadUrl"
      :headers="uploadHeaders"
      :data="uploadData"
      :on-success="handleSuccess"
      :on-error="handleError"
      :on-remove="handleRemove"
      :before-upload="beforeUpload"
      :on-exceed="handleExceed"
      :limit="limit"
      :accept="accept"
      :auto-upload="false"
      multiple
      :disabled="disabled"
    >
      <template #trigger>
        <el-button type="primary" :icon="Upload" :disabled="disabled">
          选择文件
        </el-button>
      </template>
      <template #tip>
        <div class="el-upload__tip">
          支持上传 PDF、DOC、DOCX、XLS、XLSX 格式文件，单个文件不超过 {{ maxSizeMB }}MB，最多上传 {{ limit }} 个文件
        </div>
      </template>
    </el-upload>
    
    <!-- 已上传文件列表（用于编辑时显示已有附件） -->
    <div v-if="existingFiles.length > 0" class="existing-files">
      <div class="existing-files-title">已上传附件：</div>
      <div 
        v-for="(file, index) in existingFiles" 
        :key="index" 
        class="existing-file-item"
      >
        <el-icon class="file-icon"><Document /></el-icon>
        <span class="file-name">{{ getFileName(file) }}</span>
        <el-button 
          type="danger" 
          size="small" 
          :icon="Delete" 
          link 
          @click="removeExistingFile(index)"
          :disabled="disabled"
        >
          删除
        </el-button>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, computed, watch } from 'vue'
import { ElMessage } from 'element-plus'
import { Upload, Document, Delete } from '@element-plus/icons-vue'
import { tokenUtils } from '@/utils/auth'
import { API_CONFIG } from '@/config/api.config'

const props = defineProps({
  modelValue: {
    type: Array,
    default: () => []
  },
  // 文件分类（用于后端存储路径）
  category: {
    type: String,
    default: 'info-publish'
  },
  // 最大文件大小（MB）
  maxSizeMB: {
    type: Number,
    default: 10
  },
  // 最多上传文件数量
  limit: {
    type: Number,
    default: 5
  },
  // 是否禁用
  disabled: {
    type: Boolean,
    default: false
  }
})

const emit = defineEmits(['update:modelValue', 'change'])

// 上传组件引用
const uploadRef = ref()

// 文件列表（新上传的文件）
const fileList = ref([])

// 已存在的文件（从服务器加载的）
const existingFiles = ref([...props.modelValue])

// 上传地址（此处使用占位，实际通过手动上传处理）
const uploadUrl = computed(() => `${API_CONFIG.baseURL}/api/upload/file`)

// 上传请求头
const uploadHeaders = computed(() => {
  const token = tokenUtils.getToken()
  return token ? { Authorization: `Bearer ${token}` } : {}
})

// 上传附加数据
const uploadData = computed(() => ({
  category: props.category
}))

// 允许的文件类型
const accept = '.pdf,.doc,.docx,.xls,.xlsx'

// 允许的文件扩展名
const allowedExtensions = ['.pdf', '.doc', '.docx', '.xls', '.xlsx']

/**
 * 监听外部值变化
 */
watch(() => props.modelValue, (newVal) => {
  existingFiles.value = [...(newVal || [])]
}, { deep: true })

/**
 * 上传前校验
 */
const beforeUpload = (file) => {
  // 检查文件类型
  const fileName = file.name.toLowerCase()
  const isValidType = allowedExtensions.some(ext => fileName.endsWith(ext))
  
  if (!isValidType) {
    ElMessage.error(`只支持上传 ${allowedExtensions.join(', ')} 格式的文件`)
    return false
  }
  
  // 检查文件大小
  const maxSize = props.maxSizeMB * 1024 * 1024
  if (file.size > maxSize) {
    ElMessage.error(`文件大小不能超过 ${props.maxSizeMB}MB`)
    return false
  }
  
  return true
}

/**
 * 上传成功
 */
const handleSuccess = (response, file, fileList) => {
  if (response.success && response.data) {
    // 将上传成功的文件路径添加到已存在文件列表
    existingFiles.value.push(response.data.filePath || response.data.url)
    updateValue()
    ElMessage.success('文件上传成功')
  } else {
    ElMessage.error(response.message || '文件上传失败')
    // 移除失败的文件
    const index = fileList.indexOf(file)
    if (index > -1) {
      fileList.splice(index, 1)
    }
  }
}

/**
 * 上传失败
 */
const handleError = (error, file, fileList) => {
  console.error('文件上传失败:', error)
  ElMessage.error('文件上传失败，请重试')
  
  // 移除失败的文件
  const index = fileList.indexOf(file)
  if (index > -1) {
    fileList.splice(index, 1)
  }
}

/**
 * 移除文件（新上传列表）
 */
const handleRemove = (file, fileList) => {
  // 新上传的文件被移除时无需处理已存在文件列表
  updateValue()
}

/**
 * 移除已存在的文件
 */
const removeExistingFile = (index) => {
  existingFiles.value.splice(index, 1)
  updateValue()
}

/**
 * 超出文件数量限制
 */
const handleExceed = () => {
  ElMessage.warning(`最多只能上传 ${props.limit} 个文件`)
}

/**
 * 从文件路径获取文件名
 */
const getFileName = (filePath) => {
  if (!filePath) return ''
  const parts = filePath.split('/')
  return parts[parts.length - 1]
}

/**
 * 更新值
 */
const updateValue = () => {
  emit('update:modelValue', existingFiles.value)
  emit('change', existingFiles.value)
}

/**
 * 获取新上传的文件（用于表单提交）
 */
const getNewFiles = () => {
  return fileList.value.map(item => item.raw).filter(Boolean)
}

/**
 * 获取所有文件路径（包括已存在的和新上传的）
 */
const getAllFilePaths = () => {
  return existingFiles.value
}

/**
 * 清空文件列表
 */
const clearFiles = () => {
  fileList.value = []
  existingFiles.value = []
  updateValue()
}

/**
 * 手动触发上传
 */
const submit = () => {
  if (uploadRef.value) {
    uploadRef.value.submit()
  }
}

// 暴露方法给父组件
defineExpose({
  getNewFiles,
  getAllFilePaths,
  clearFiles,
  submit
})
</script>

<style scoped>
.file-upload {
  width: 100%;
}

.existing-files {
  margin-top: 16px;
  padding: 12px;
  background-color: #f5f7fa;
  border-radius: 4px;
}

.existing-files-title {
  font-size: 14px;
  color: #606266;
  margin-bottom: 8px;
  font-weight: 500;
}

.existing-file-item {
  display: flex;
  align-items: center;
  padding: 8px 0;
  border-bottom: 1px solid #e4e7ed;
}

.existing-file-item:last-child {
  border-bottom: none;
}

.file-icon {
  font-size: 18px;
  color: #409eff;
  margin-right: 8px;
}

.file-name {
  flex: 1;
  font-size: 14px;
  color: #303133;
  overflow: hidden;
  text-overflow: ellipsis;
  white-space: nowrap;
}

:deep(.el-upload__tip) {
  margin-top: 8px;
  font-size: 12px;
  color: #909399;
}
</style>
