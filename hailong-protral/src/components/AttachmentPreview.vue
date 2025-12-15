<template>
  <div v-if="visible" class="fixed inset-0 z-50 flex items-center justify-center bg-black/50 backdrop-blur-sm" @click="handleClose">
    <div class="relative w-full max-w-6xl max-h-[90vh] bg-white rounded-xl shadow-2xl overflow-hidden" @click.stop>
      <!-- 头部 -->
      <div class="flex items-center justify-between px-6 py-4 border-b border-gray-200 bg-gradient-to-r from-hailong-primary/5 to-hailong-secondary/5">
        <div class="flex items-center gap-3">
          <div class="w-10 h-10 bg-hailong-primary/10 rounded-lg flex items-center justify-center">
            <svg class="w-5 h-5 text-hailong-primary" fill="none" stroke="currentColor" viewBox="0 0 24 24">
              <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M15 12a3 3 0 11-6 0 3 3 0 016 0z" />
              <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M2.458 12C3.732 7.943 7.523 5 12 5c4.478 0 8.268 2.943 9.542 7-1.274 4.057-5.064 7-9.542 7-4.477 0-8.268-2.943-9.542-7z" />
            </svg>
          </div>
          <div>
            <h3 class="text-lg font-bold text-gray-900">{{ attachment.fileName }}</h3>
            <p class="text-sm text-gray-500">{{ formatFileSize(attachment.fileSize) }}</p>
          </div>
        </div>
        <button
          @click="handleClose"
          class="w-10 h-10 flex items-center justify-center rounded-lg hover:bg-gray-100 transition-colors"
        >
          <svg class="w-6 h-6 text-gray-500" fill="none" stroke="currentColor" viewBox="0 0 24 24">
            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M6 18L18 6M6 6l12 12" />
          </svg>
        </button>
      </div>

      <!-- 预览内容 -->
      <div class="overflow-auto max-h-[calc(90vh-140px)] p-6">
        <!-- 图片预览 -->
        <div v-if="isImage" class="flex items-center justify-center">
          <img :src="attachment.fileUrl" :alt="attachment.fileName" class="max-w-full h-auto rounded-lg shadow-lg" />
        </div>

        <!-- PDF预览 -->
        <div v-else-if="isPdf" class="w-full h-[70vh]">
          <iframe :src="attachment.fileUrl" class="w-full h-full border-0 rounded-lg"></iframe>
        </div>

        <!-- 文本文件预览 -->
        <div v-else-if="isText" class="bg-gray-50 rounded-lg p-6">
          <pre class="text-sm text-gray-700 whitespace-pre-wrap font-mono">{{ textContent }}</pre>
        </div>

        <!-- 不支持预览的文件类型 -->
        <div v-else class="text-center py-12">
          <div class="w-20 h-20 mx-auto mb-4 bg-gray-100 rounded-full flex items-center justify-center">
            <svg class="w-10 h-10 text-gray-400" fill="none" stroke="currentColor" viewBox="0 0 24 24">
              <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M7 21h10a2 2 0 002-2V9.414a1 1 0 00-.293-.707l-5.414-5.414A1 1 0 0012.586 3H7a2 2 0 00-2 2v14a2 2 0 002 2z" />
            </svg>
          </div>
          <p class="text-gray-500 mb-4">此文件类型不支持在线预览</p>
          <p class="text-sm text-gray-400 mb-6">文件类型: {{ fileExtension.toUpperCase() }}</p>
          <a
            :href="attachment.fileUrl"
            target="_blank"
            download
            class="inline-flex items-center gap-2 px-6 py-3 bg-gradient-to-r from-hailong-primary to-hailong-secondary text-white rounded-lg hover:shadow-lg transition-all font-medium"
          >
            <svg class="w-5 h-5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
              <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M4 16v1a3 3 0 003 3h10a3 3 0 003-3v-1m-4-4l-4 4m0 0l-4-4m4 4V4" />
            </svg>
            下载文件
          </a>
        </div>
      </div>

      <!-- 底部操作栏 -->
      <div class="flex items-center justify-between px-6 py-4 border-t border-gray-200 bg-gray-50">
        <div class="text-sm text-gray-500">
          <span class="font-medium">文件类型:</span> {{ fileExtension.toUpperCase() }}
        </div>
        <div class="flex items-center gap-3">
          <a
            :href="attachment.fileUrl"
            target="_blank"
            class="px-4 py-2 bg-white border border-gray-300 text-gray-700 rounded-lg hover:bg-gray-50 transition-all font-medium flex items-center gap-2"
          >
            <svg class="w-4 h-4" fill="none" stroke="currentColor" viewBox="0 0 24 24">
              <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M10 6H6a2 2 0 00-2 2v10a2 2 0 002 2h10a2 2 0 002-2v-4M14 4h6m0 0v6m0-6L10 14" />
            </svg>
            新窗口打开
          </a>
          <a
            :href="attachment.fileUrl"
            download
            class="px-4 py-2 bg-gradient-to-r from-hailong-primary to-hailong-secondary text-white rounded-lg hover:shadow-lg transition-all font-medium flex items-center gap-2"
          >
            <svg class="w-4 h-4" fill="none" stroke="currentColor" viewBox="0 0 24 24">
              <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M4 16v1a3 3 0 003 3h10a3 3 0 003-3v-1m-4-4l-4 4m0 0l-4-4m4 4V4" />
            </svg>
            下载
          </a>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, computed, watch } from 'vue'

const props = defineProps({
  visible: {
    type: Boolean,
    default: false
  },
  attachment: {
    type: Object,
    required: true
  }
})

const emit = defineEmits(['close'])

const textContent = ref('')

// 获取文件扩展名
const fileExtension = computed(() => {
  const fileName = props.attachment.fileName || ''
  const parts = fileName.split('.')
  return parts.length > 1 ? parts[parts.length - 1].toLowerCase() : ''
})

// 判断是否为图片
const isImage = computed(() => {
  const imageExts = ['jpg', 'jpeg', 'png', 'gif', 'bmp', 'webp', 'svg']
  return imageExts.includes(fileExtension.value)
})

// 判断是否为PDF
const isPdf = computed(() => {
  return fileExtension.value === 'pdf'
})

// 判断是否为文本文件
const isText = computed(() => {
  const textExts = ['txt', 'md', 'json', 'xml', 'csv', 'log']
  return textExts.includes(fileExtension.value)
})

// 格式化文件大小
const formatFileSize = (bytes) => {
  if (!bytes || bytes === 0) return '0 B'
  const k = 1024
  const sizes = ['B', 'KB', 'MB', 'GB']
  const i = Math.floor(Math.log(bytes) / Math.log(k))
  return Math.round(bytes / Math.pow(k, i) * 100) / 100 + ' ' + sizes[i]
}

// 关闭预览
const handleClose = () => {
  emit('close')
}

// 加载文本内容
const loadTextContent = async () => {
  if (isText.value && props.visible) {
    try {
      const response = await fetch(props.attachment.fileUrl)
      textContent.value = await response.text()
    } catch (error) {
      console.error('加载文本内容失败:', error)
      textContent.value = '加载失败'
    }
  }
}

// 监听visible变化
watch(() => props.visible, (newVal) => {
  if (newVal) {
    loadTextContent()
  } else {
    textContent.value = ''
  }
})
</script>

<style scoped>
/* 滚动条样式 */
.overflow-auto::-webkit-scrollbar {
  width: 8px;
  height: 8px;
}

.overflow-auto::-webkit-scrollbar-track {
  background: #f1f1f1;
  border-radius: 4px;
}

.overflow-auto::-webkit-scrollbar-thumb {
  background: #888;
  border-radius: 4px;
}

.overflow-auto::-webkit-scrollbar-thumb:hover {
  background: #555;
}
</style>