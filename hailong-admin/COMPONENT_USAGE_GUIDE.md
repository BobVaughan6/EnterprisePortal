# 公共组件使用指南

本文档详细说明了海隆咨询后台管理系统中公共组件的使用方法。

---

## 📁 组件列表

1. [FileUpload - 文件上传组件](#fileupload---文件上传组件)
2. [RichEditor - 富文本编辑器](#richeditor---富文本编辑器)
3. [Header - 页面头部](#header---页面头部)
4. [Sidebar - 侧边栏](#sidebar---侧边栏)

---

## FileUpload - 文件上传组件

### 功能特性

- ✅ 支持三种文件类型：图片(image)、文档(document)、视频(video)
- ✅ 支持单文件和多文件上传
- ✅ 支持三种展示模式：text、picture、picture-card
- ✅ 自动文件类型验证
- ✅ 文件大小限制
- ✅ 图片预览功能
- ✅ 集成附件管理API

### Props 参数

| 参数 | 类型 | 默认值 | 说明 |
|------|------|--------|------|
| modelValue | Array | [] | 文件URL数组（v-model绑定） |
| fileType | String | 'document' | 文件类型：image/document/video |
| relatedType | String | '' | 关联类型（用于后端存储） |
| relatedId | Number/String | null | 关联ID |
| maxSize | Number | 10 | 最大文件大小（MB） |
| limit | Number | 5 | 最多上传文件数量 |
| multiple | Boolean | true | 是否支持多选 |
| listType | String | 'text' | 列表类型：text/picture/picture-card |
| accept | String | '' | 自定义接受的文件类型 |
| disabled | Boolean | false | 是否禁用 |

### Events 事件

| 事件名 | 参数 | 说明 |
|--------|------|------|
| update:modelValue | urls: Array | 文件URL数组更新 |
| change | urls: Array | 文件列表变化 |

### Methods 方法

| 方法名 | 参数 | 返回值 | 说明 |
|--------|------|--------|------|
| clearFiles | - | - | 清空文件列表 |
| submit | - | - | 手动触发上传 |
| getUrls | - | Array | 获取所有文件URL |

### 使用示例

#### 1. 图片上传（卡片模式）

```vue
<template>
  <el-form-item label="产品图片">
    <FileUpload 
      v-model="formData.imageUrls" 
      file-type="image"
      :limit="5"
      :max-size="5"
      :multiple="true"
      list-type="picture-card"
    />
  </el-form-item>
</template>

<script setup>
import { reactive } from 'vue'
import FileUpload from '@/components/FileUpload.vue'

const formData = reactive({
  imageUrls: []
})
</script>
```

#### 2. 文档上传（列表模式）

```vue
<template>
  <el-form-item label="附件文档">
    <FileUpload 
      v-model="formData.attachments" 
      file-type="document"
      :limit="3"
      :max-size="10"
      list-type="text"
    />
  </el-form-item>
</template>

<script setup>
import { reactive } from 'vue'
import FileUpload from '@/components/FileUpload.vue'

const formData = reactive({
  attachments: []
})
</script>
```

#### 3. 单图上传

```vue
<template>
  <el-form-item label="封面图">
    <FileUpload 
      v-model="formData.coverImage" 
      file-type="image"
      :limit="1"
      :multiple="false"
      list-type="picture-card"
    />
  </el-form-item>
</template>

<script setup>
import { reactive } from 'vue'
import FileUpload from '@/components/FileUpload.vue'

const formData = reactive({
  coverImage: []
})
</script>
```

#### 4. 使用组件方法

```vue
<template>
  <FileUpload 
    ref="uploadRef"
    v-model="formData.files" 
    file-type="document"
  />
  <el-button @click="handleClear">清空</el-button>
  <el-button @click="handleGetUrls">获取URL</el-button>
</template>

<script setup>
import { ref, reactive } from 'vue'
import { ElMessage } from 'element-plus'
import FileUpload from '@/components/FileUpload.vue'

const uploadRef = ref()
const formData = reactive({
  files: []
})

const handleClear = () => {
  uploadRef.value?.clearFiles()
}

const handleGetUrls = () => {
  const urls = uploadRef.value?.getUrls()
  ElMessage.info(`当前文件数量: ${urls?.length || 0}`)
}
</script>
```

---

## RichEditor - 富文本编辑器

### 功能特性

- ✅ 基于 wangEditor 5
- ✅ 支持图片上传（集成附件API）
- ✅ 支持视频上传（集成附件API）
- ✅ 支持代码高亮
- ✅ 支持表格、引用、链接等
- ✅ 自定义高度
- ✅ 支持禁用状态
- ✅ 完善的工具栏

### Props 参数

| 参数 | 类型 | 默认值 | 说明 |
|------|------|--------|------|
| modelValue | String | '' | HTML内容（v-model绑定） |
| mode | String | 'default' | 模式：default/simple |
| placeholder | String | '请输入内容...' | 占位符 |
| height | Number | 400 | 编辑器高度（px） |
| disabled | Boolean | false | 是否禁用 |

### Events 事件

| 事件名 | 参数 | 说明 |
|--------|------|------|
| update:modelValue | html: String | HTML内容更新 |
| change | html: String | 内容变化 |

### Methods 方法

| 方法名 | 参数 | 返回值 | 说明 |
|--------|------|--------|------|
| getHtml | - | String | 获取HTML内容 |
| getText | - | String | 获取纯文本内容 |
| setHtml | html: String | - | 设置HTML内容 |
| clear | - | - | 清空内容 |
| focus | - | - | 聚焦编辑器 |

### 使用示例

#### 1. 基础使用

```vue
<template>
  <el-form-item label="文章内容" prop="content">
    <RichEditor 
      v-model="formData.content" 
      :height="500"
      placeholder="请输入文章内容..."
    />
  </el-form-item>
</template>

<script setup>
import { reactive } from 'vue'
import RichEditor from '@/components/RichEditor.vue'

const formData = reactive({
  content: ''
})
</script>
```

#### 2. 简洁模式

```vue
<template>
  <RichEditor 
    v-model="formData.description" 
    mode="simple"
    :height="300"
    placeholder="请输入简短描述..."
  />
</template>

<script setup>
import { reactive } from 'vue'
import RichEditor from '@/components/RichEditor.vue'

const formData = reactive({
  description: ''
})
</script>
```

#### 3. 禁用状态（只读）

```vue
<template>
  <RichEditor 
    v-model="article.content" 
    :disabled="true"
    :height="400"
  />
</template>

<script setup>
import { ref } from 'vue'
import RichEditor from '@/components/RichEditor.vue'

const article = ref({
  content: '<p>这是只读内容</p>'
})
</script>
```

#### 4. 使用组件方法

```vue
<template>
  <RichEditor 
    ref="editorRef"
    v-model="formData.content" 
  />
  <el-button @click="handleGetText">获取纯文本</el-button>
  <el-button @click="handleClear">清空</el-button>
  <el-button @click="handleFocus">聚焦</el-button>
</template>

<script setup>
import { ref, reactive } from 'vue'
import { ElMessage } from 'element-plus'
import RichEditor from '@/components/RichEditor.vue'

const editorRef = ref()
const formData = reactive({
  content: ''
})

const handleGetText = () => {
  const text = editorRef.value?.getText()
  ElMessage.info(`纯文本长度: ${text?.length || 0}`)
}

const handleClear = () => {
  editorRef.value?.clear()
}

const handleFocus = () => {
  editorRef.value?.focus()
}
</script>
```

#### 5. 表单验证

```vue
<template>
  <el-form :model="formData" :rules="rules" ref="formRef">
    <el-form-item label="文章内容" prop="content">
      <RichEditor v-model="formData.content" />
    </el-form-item>
    <el-button @click="handleSubmit">提交</el-button>
  </el-form>
</template>

<script setup>
import { ref, reactive } from 'vue'
import { ElMessage } from 'element-plus'
import RichEditor from '@/components/RichEditor.vue'

const formRef = ref()
const formData = reactive({
  content: ''
})

const rules = {
  content: [
    { required: true, message: '请输入文章内容', trigger: 'blur' },
    { 
      min: 10, 
      message: '内容长度不能少于10个字符', 
      trigger: 'blur',
      validator: (rule, value, callback) => {
        const text = value.replace(/<[^>]+>/g, '').trim()
        if (text.length < 10) {
          callback(new Error('内容长度不能少于10个字符'))
        } else {
          callback()
        }
      }
    }
  ]
}

const handleSubmit = async () => {
  await formRef.value?.validate()
  ElMessage.success('验证通过')
}
</script>
```

---

## Header - 页面头部

### 功能特性

- ✅ 显示用户信息
- ✅ 退出登录
- ✅ 面包屑导航

### 使用示例

```vue
<template>
  <Header />
</template>

<script setup>
import Header from '@/components/Header.vue'
</script>
```

---

## Sidebar - 侧边栏

### 功能特性

- ✅ 菜单导航
- ✅ 路由跳转
- ✅ 菜单折叠

### 使用示例

```vue
<template>
  <Sidebar />
</template>

<script setup>
import Sidebar from '@/components/Sidebar.vue'
</script>
```

---

## 💡 最佳实践

### 1. 文件上传组件

**推荐做法：**
- 图片上传使用 `picture-card` 模式，更直观
- 文档上传使用 `text` 模式，节省空间
- 单图上传设置 `limit="1"` 和 `multiple="false"`
- 根据实际需求设置合理的文件大小限制

**注意事项：**
- 上传的文件会自动保存到服务器
- 删除文件时只是从列表中移除，不会删除服务器文件
- 建议在表单提交时再关联文件到具体实体

### 2. 富文本编辑器

**推荐做法：**
- 根据内容长度设置合适的高度
- 简短内容使用 `simple` 模式
- 添加内容长度验证
- 提交前清理空标签

**注意事项：**
- 编辑器内上传的图片会立即保存到服务器
- 使用 `getText()` 方法获取纯文本进行长度验证
- 禁用状态下工具栏会自动隐藏上传按钮

### 3. 表单集成

```vue
<template>
  <el-form :model="formData" :rules="rules" ref="formRef">
    <!-- 标题 -->
    <el-form-item label="标题" prop="title">
      <el-input v-model="formData.title" />
    </el-form-item>
    
    <!-- 封面图 -->
    <el-form-item label="封面图" prop="coverImage">
      <FileUpload 
        v-model="formData.coverImage" 
        file-type="image"
        :limit="1"
        list-type="picture-card"
      />
    </el-form-item>
    
    <!-- 内容 -->
    <el-form-item label="内容" prop="content">
      <RichEditor v-model="formData.content" />
    </el-form-item>
    
    <!-- 附件 -->
    <el-form-item label="附件">
      <FileUpload 
        v-model="formData.attachments" 
        file-type="document"
        :limit="5"
      />
    </el-form-item>
    
    <el-form-item>
      <el-button type="primary" @click="handleSubmit">提交</el-button>
    </el-form-item>
  </el-form>
</template>

<script setup>
import { ref, reactive } from 'vue'
import { ElMessage } from 'element-plus'
import FileUpload from '@/components/FileUpload.vue'
import RichEditor from '@/components/RichEditor.vue'

const formRef = ref()
const formData = reactive({
  title: '',
  coverImage: [],
  content: '',
  attachments: []
})

const rules = {
  title: [
    { required: true, message: '请输入标题', trigger: 'blur' }
  ],
  coverImage: [
    { 
      required: true, 
      message: '请上传封面图', 
      trigger: 'change',
      validator: (rule, value, callback) => {
        if (!value || value.length === 0) {
          callback(new Error('请上传封面图'))
        } else {
          callback()
        }
      }
    }
  ],
  content: [
    { required: true, message: '请输入内容', trigger: 'blur' }
  ]
}

const handleSubmit = async () => {
  await formRef.value?.validate()
  
  // 提交数据
  console.log('提交数据:', formData)
  ElMessage.success('提交成功')
}
</script>
```

---

## 🔧 故障排除

### 问题1：文件上传失败

**可能原因：**
- 文件大小超过限制
- 文件类型不支持
- 网络问题
- 后端API未正确配置

**解决方法：**
1. 检查文件大小和类型
2. 查看浏览器控制台错误信息
3. 确认后端API地址正确
4. 检查认证token是否有效

### 问题2：富文本编辑器图片上传失败

**可能原因：**
- 图片大小超过5MB
- 后端上传接口返回格式不正确

**解决方法：**
1. 压缩图片后再上传
2. 确认后端返回格式：`{ success: true, data: { fileUrl: '...' } }`

### 问题3：组件方法调用无效

**可能原因：**
- ref未正确绑定
- 组件未挂载完成

**解决方法：**
```vue
<script setup>
import { ref, onMounted } from 'vue'

const uploadRef = ref()

// 确保在组件挂载后调用
onMounted(() => {
  // 可以安全调用组件方法
  uploadRef.value?.clearFiles()
})
</script>
```

---

**文档更新时间**: 2025-12-10
**版本**: v1.0