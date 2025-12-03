<template>
  <div class="tab-content">
    <el-alert
      title="提示"
      type="info"
      description="企业信息为单条记录，只需编辑更新即可"
      :closable="false"
      style="margin-bottom: 20px;"
    />
    
    <el-form 
      ref="formRef" 
      :model="formData" 
      :rules="formRules" 
      label-width="120px"
      v-loading="loading"
    >
      <el-form-item label="企业标题" prop="title">
        <el-input 
          v-model="formData.title" 
          placeholder="请输入企业标题" 
          clearable 
          style="max-width: 500px;"
        />
      </el-form-item>
      
      <el-form-item label="企业简介" prop="content">
        <div style="border: 1px solid #dcdfe6; border-radius: 4px;">
          <Toolbar
            :editor="editorRef"
            :defaultConfig="toolbarConfig"
            mode="default"
            style="border-bottom: 1px solid #dcdfe6"
          />
          <Editor
            v-model="formData.content"
            :defaultConfig="editorConfig"
            mode="default"
            style="height: 400px; overflow-y: hidden;"
            @onCreated="handleCreated"
          />
        </div>
        <div style="margin-top: 8px; color: #909399; font-size: 12px;">
          支持富文本编辑，可插入图片、设置文字格式等
        </div>
      </el-form-item>
      
      <el-form-item label="企业图片" prop="imageUrl">
        <el-input 
          v-model="formData.imageUrl" 
          placeholder="请输入企业图片URL（可选）" 
          clearable 
          style="max-width: 500px;"
        />
        <div v-if="formData.imageUrl" style="margin-top: 10px;">
          <el-image 
            :src="formData.imageUrl" 
            :preview-src-list="[formData.imageUrl]"
            fit="contain"
            style="max-width: 300px; max-height: 200px; border: 1px solid #dcdfe6; border-radius: 4px;"
          />
        </div>
        <div style="margin-top: 8px; color: #909399; font-size: 12px;">
          建议尺寸：800x600px
        </div>
<script setup>
import { ref, reactive, onMounted, onBeforeUnmount, shallowRef } from 'vue'
import { ElMessage } from 'element-plus'
import { configApi } from '@/api'
import { Editor, Toolbar } from '@wangeditor/editor-for-vue'
import '@wangeditor/editor/dist/css/style.css'

const loading = ref(false)
const submitting = ref(false)
const formRef = ref(null)
const editorRef = shallowRef()
<script setup>
import { ref, reactive, onMounted, onBeforeUnmount, nextTick } from 'vue'
import { ElMessage } from 'element-plus'
import { configApi } from '@/api'
import Quill from 'quill'
import 'quill/dist/quill.snow.css'

const loading = ref(false)
const formRules = {
  title: [
    { required: true, message: '请输入企业标题', trigger: 'blur' }
  ],
  content: [
    { required: true, message: '请输入企业简介', trigger: 'blur' }
  ]
}

// 编辑器配置
const toolbarConfig = {}
const editorConfig = {
  placeholder: '请输入企业简介内容...',
  MENU_CONF: {}
}

const handleCreated = (editor) => {
  editorRef.value = editor
}       [{ 'align': [] }],
        ['link', 'image'],
        ['clean']
      ]
    }
  })
  
  // 监听内容变化
  quillEditor.on('text-change', () => {
    formData.content = quillEditor.root.innerHTML
  })
}

const loadData = async () => {
  loading.value = true
  try {
    const res = await configApi.getCompanyProfile()
    if (res.success && res.data) {
      formData.title = res.data.title || ''
      formData.content = res.data.content || ''
      formData.imageUrl = res.data.imageUrl || ''
      
      // 设置编辑器内容
      if (quillEditor && formData.content) {
        quillEditor.root.innerHTML = formData.content
      }
    } else {
      // 如果没有数据，初始化为空
const loadData = async () => {
  loading.value = true
  try {
    const res = await configApi.getCompanyProfile()
    if (res.success && res.data) {
      formData.title = res.data.title || ''
      formData.content = res.data.content || ''
      formData.imageUrl = res.data.imageUrl || ''
    } else {
      // 如果没有数据，初始化为空
      formData.title = ''
      formData.content = ''
      formData.imageUrl = ''
    }
  } catch (error) {
    console.error('加载数据失败:', error)
    ElMessage.warning('暂无企业信息，请填写后保存')
  } finally {
    loading.value = false
  }
}     const submitData = {
        title: formData.title,
        content: formData.content,
        imageUrl: formData.imageUrl || null
      }
      
      const res = await configApi.updateCompanyProfile(submitData)
      
      if (res.success) {
        ElMessage.success('保存成功')
const handleSubmit = async () => {
  if (!formRef.value) return
  
  await formRef.value.validate(async (valid) => {
    if (!valid) return
    
    submitting.value = true
    try {
      const submitData = {
        title: formData.title,
        content: formData.content,
        imageUrl: formData.imageUrl || null
      }
      
      const res = await configApi.updateCompanyProfile(submitData)
      
      if (res.success) {
        ElMessage.success('保存成功')
        loadData()
      } else {
        ElMessage.error(res.message || '保存失败')
      }
    } catch (error) {
      console.error('保存失败:', error)
      ElMessage.error('保存失败')
    } finally {
      submitting.value = false
    }
  })
}

:deep(.ql-editor) {
  min-height: 400px;
}
</style>
onMounted(() => {
  loadData()
})

onBeforeUnmount(() => {
  const editor = editorRef.value
  if (editor) {
    editor.destroy()
  }
})<style scoped>
.tab-content {
  padding: 20px 0;
}
</style>