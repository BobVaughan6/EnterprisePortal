<template>
  <div class="rich-editor">
    <Toolbar
      :editor="editorRef"
      :defaultConfig="toolbarConfig"
      :mode="mode"
      class="toolbar"
    />
    <Editor
      v-model="valueHtml"
      :defaultConfig="editorConfig"
      :mode="mode"
      class="editor"
      @onCreated="handleCreated"
      @onChange="handleChange"
    />
  </div>
</template>

<script setup>
import { ref, shallowRef, watch, onBeforeUnmount } from 'vue'
import { Editor, Toolbar } from '@wangeditor/editor-for-vue'
import '@wangeditor/editor/dist/css/style.css'

const props = defineProps({
  modelValue: {
    type: String,
    default: ''
  },
  mode: {
    type: String,
    default: 'default' // default 或 simple
  },
  placeholder: {
    type: String,
    default: '请输入内容...'
  }
})

const emit = defineEmits(['update:modelValue', 'change'])

// 编辑器实例
const editorRef = shallowRef()

// 内容 HTML
const valueHtml = ref(props.modelValue)

// 工具栏配置
const toolbarConfig = {
  excludeKeys: []
}

// 编辑器配置
const editorConfig = {
  placeholder: props.placeholder,
  MENU_CONF: {
    // 配置上传图片
    uploadImage: {
      server: '/api/upload/image',
      fieldName: 'file',
      maxFileSize: 5 * 1024 * 1024, // 5M
      allowedFileTypes: ['image/*'],
      
      // 自定义插入图片
      customInsert(res, insertFn) {
        if (res.success && res.data?.url) {
          insertFn(res.data.url, res.data.name || '', res.data.url)
        }
      }
    },
    
    // 配置上传视频
    uploadVideo: {
      server: '/api/upload/video',
      fieldName: 'file',
      maxFileSize: 50 * 1024 * 1024, // 50M
      
      customInsert(res, insertFn) {
        if (res.success && res.data?.url) {
          insertFn(res.data.url, res.data.name || '')
        }
      }
    }
  }
}

/**
 * 编辑器创建完成
 */
const handleCreated = (editor) => {
  editorRef.value = editor
}

/**
 * 内容变化
 */
const handleChange = (editor) => {
  const html = editor.getHtml()
  emit('update:modelValue', html)
  emit('change', html)
}

/**
 * 监听外部值变化
 */
watch(() => props.modelValue, (newVal) => {
  if (newVal !== valueHtml.value) {
    valueHtml.value = newVal
  }
})

/**
 * 组件销毁时，销毁编辑器
 */
onBeforeUnmount(() => {
  const editor = editorRef.value
  if (editor) {
    editor.destroy()
  }
})
</script>

<style scoped>
.rich-editor {
  border: 1px solid #ccc;
  border-radius: 4px;
  overflow: hidden;
}

.toolbar {
  border-bottom: 1px solid #ccc;
}

.editor {
  min-height: 400px;
  overflow-y: auto;
}

:deep(.w-e-text-container) {
  background-color: #fff;
}

:deep(.w-e-text-placeholder) {
  color: #999;
  font-style: normal;
}
</style>
