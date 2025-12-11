<template>
  <div class="file-upload">
    <el-upload
      ref="uploadRef"
      v-model:file-list="fileList"
      :action="uploadUrl"
      :headers="uploadHeaders"
      :on-success="handleSuccess"
      :on-error="handleError"
      :on-remove="handleRemove"
      :before-upload="beforeUpload"
      :on-exceed="handleExceed"
      :on-preview="handlePreview"
      :limit="limit"
      :accept="computedAccept"
      :list-type="listType"
      :multiple="multiple"
      :disabled="disabled"
      :auto-upload="true"
    >
      <template v-if="listType === 'picture-card'">
        <el-icon><Plus /></el-icon>
      </template>
      <el-button v-else type="primary" :icon="Upload" :disabled="disabled">
        选择{{ fileTypeText }}
      </el-button>
      <template #tip>
        <div class="el-upload__tip">
          {{ tipText }}
        </div>
      </template>
    </el-upload>
    
    <!-- 图片预览对话框 -->
    <el-dialog v-model="previewVisible" title="图片预览" width="800px">
      <img :src="previewUrl" style="width: 100%" alt="预览图片" />
    </el-dialog>
  </div>
</template>

<script setup>
import { ref, computed, watch, onMounted } from 'vue'
import { ElMessage } from 'element-plus'
import { Upload, Plus } from '@element-plus/icons-vue'
import { tokenUtils } from '@/utils/auth'
import { API_CONFIG } from '@/config/api.config'

const props = defineProps({
  modelValue: {
    type: Array,
    default: () => []
  },
  // 文件类型：image/document/video
  fileType: {
    type: String,
    default: 'document'
  },
  // 关联类型（用于后端存储）
  relatedType: {
    type: String,
    default: ''
  },
  // 关联ID
  relatedId: {
    type: [Number, String],
    default: null
  },
  // 最大文件大小（MB）
  maxSize: {
    type: Number,
    default: 10
  },
  // 最多上传文件数量
  limit: {
    type: Number,
    default: 5
  },
  // 是否支持多选
  multiple: {
    type: Boolean,
    default: true
  },
  // 列表类型：text/picture/picture-card
  listType: {
    type: String,
    default: 'text'
  },
  // 接受的文件类型（自定义）
  accept: {
    type: String,
    default: ''
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

// 文件列表
const fileList = ref([])

// 图片预览
const previewVisible = ref(false)
const previewUrl = ref('')

// 上传地址
const uploadUrl = computed(() => `${API_CONFIG.baseURL}/api/attachments/upload`)

// 上传请求头
const uploadHeaders = computed(() => {
  const token = tokenUtils.getToken()
  return token ? { Authorization: `Bearer ${token}` } : {}
})

// 文件类型文本
const fileTypeText = computed(() => {
  const typeMap = {
    image: '图片',
    document: '文件',
    video: '视频'
  }
  return typeMap[props.fileType] || '文件'
})

// 允许的文件类型配置
const fileTypeConfig = {
  image: {
    extensions: ['.jpg', '.jpeg', '.png', '.gif', '.bmp', '.webp', '.svg'],
    accept: 'image/*'
  },
  document: {
    extensions: ['.pdf', '.doc', '.docx', '.xls', '.xlsx', '.ppt', '.pptx', '.txt', '.jpg', '.jpeg', '.png', '.gif', '.bmp', '.webp', '.svg'],
    accept: '.pdf,.doc,.docx,.xls,.xlsx,.ppt,.pptx,.txt,.jpg,.jpeg,.png,.gif,.bmp,.webp,.svg,image/*'
  },
  video: {
    extensions: ['.mp4', '.avi', '.mov', '.wmv', '.flv', '.mkv', '.webm'],
    accept: 'video/*'
  }
}

// 计算accept属性
const computedAccept = computed(() => {
  if (props.accept) return props.accept
  return fileTypeConfig[props.fileType]?.accept || '*'
})

// 提示文本
const tipText = computed(() => {
  const sizeText = `单个文件不超过 ${props.maxSize}MB`
  const limitText = props.limit > 1 ? `，最多上传 ${props.limit} 个` : ''
  
  let typeText = ''
  if (props.fileType === 'image') {
    typeText = '支持 JPG、PNG、GIF、BMP、WEBP、SVG 等图片格式'
  } else if (props.fileType === 'document') {
    typeText = '支持 PDF、DOC、DOCX、XLS、XLSX、PPT、PPTX、TXT 等文档格式，以及 JPG、PNG、GIF 等图片格式'
  } else if (props.fileType === 'video') {
    typeText = '支持 MP4、AVI、MOV、WMV、FLV、MKV、WEBM 等视频格式'
  }
  
  return `${typeText}，${sizeText}${limitText}`
})

/**
 * 初始化文件列表
 */
const initFileList = () => {
  if (props.modelValue && props.modelValue.length > 0) {
    fileList.value = props.modelValue.map((url, index) => ({
      name: getFileName(url),
      url: url,
      uid: Date.now() + index,
      status: 'success'
    }))
  } else {
    fileList.value = []
  }
}

/**
 * 从URL获取文件名
 */
const getFileName = (url) => {
  if (!url) return ''
  const parts = url.split('/')
  return parts[parts.length - 1]
}

/**
 * 监听外部值变化
 */
watch(() => props.modelValue, (newVal) => {
  if (JSON.stringify(newVal) !== JSON.stringify(getUrls())) {
    initFileList()
  }
}, { deep: true })

/**
 * 上传前校验
 */
const beforeUpload = (file) => {
  // 检查文件类型
  const fileName = file.name.toLowerCase()
  const config = fileTypeConfig[props.fileType]
  
  if (config && config.extensions) {
    const isValidType = config.extensions.some(ext => fileName.endsWith(ext))
    if (!isValidType) {
      ElMessage.error(`只支持上传 ${config.extensions.join(', ')} 格式的文件`)
      return false
    }
  }
  
  // 检查文件大小
  const maxSize = props.maxSize * 1024 * 1024
  if (file.size > maxSize) {
    ElMessage.error(`文件大小不能超过 ${props.maxSize}MB`)
    return false
  }
  
  return true
}

/**
 * 上传成功
 */
const handleSuccess = (response, file, fileListParam) => {
  if (response.success && response.data) {
    // 更新文件列表中的URL
    const index = fileListParam.findIndex(f => f.uid === file.uid)
    if (index > -1) {
      fileListParam[index].url = response.data.fileUrl || response.data.url
      fileListParam[index].response = response.data
    }
    
    updateValue()
    ElMessage.success('文件上传成功')
  } else {
    ElMessage.error(response.message || '文件上传失败')
    // 移除失败的文件
    const index = fileListParam.findIndex(f => f.uid === file.uid)
    if (index > -1) {
      fileListParam.splice(index, 1)
    }
  }
}

/**
 * 上传失败
 */
const handleError = (error, file, fileListParam) => {
  console.error('文件上传失败:', error)
  ElMessage.error('文件上传失败，请重试')
  
  // 移除失败的文件
  const index = fileListParam.findIndex(f => f.uid === file.uid)
  if (index > -1) {
    fileListParam.splice(index, 1)
  }
}

/**
 * 移除文件
 */
const handleRemove = (file, fileListParam) => {
  updateValue()
}

/**
 * 超出文件数量限制
 */
const handleExceed = () => {
  ElMessage.warning(`最多只能上传 ${props.limit} 个文件`)
}

/**
 * 预览文件
 */
const handlePreview = (file) => {
  if (props.fileType === 'image') {
    previewUrl.value = file.url
    previewVisible.value = true
  } else {
    // 非图片文件，尝试在新窗口打开
    if (file.url) {
      window.open(file.url, '_blank')
    }
  }
}

/**
 * 获取所有文件URL
 */
const getUrls = () => {
  return fileList.value
    .filter(file => file.status === 'success' && file.url)
    .map(file => file.url)
}

/**
 * 更新值
 */
const updateValue = () => {
  const urls = getUrls()
  emit('update:modelValue', urls)
  emit('change', urls)
}

/**
 * 清空文件列表
 */
const clearFiles = () => {
  fileList.value = []
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
  clearFiles,
  submit,
  getUrls
})

// 组件挂载时初始化
onMounted(() => {
  initFileList()
})
</script>

<style scoped>
.file-upload {
  width: 100%;
}

:deep(.el-upload__tip) {
  margin-top: 8px;
  font-size: 12px;
  color: #909399;
  line-height: 1.5;
}

:deep(.el-upload-list__item) {
  transition: all 0.3s;
}

:deep(.el-upload-list__item:hover) {
  background-color: #f5f7fa;
}
</style>
