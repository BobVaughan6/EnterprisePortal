# 信息发布统一管理页面使用说明

## 📁 文件列表

### 1. 主页面组件
- **路径**: `hailong-admin/src/views/info-publish/InfoPublish.vue`
- **功能**: 信息发布统一管理页面，支持Tab切换不同类别

### 2. 文件上传组件
- **路径**: `hailong-admin/src/components/FileUpload.vue`
- **功能**: 支持多文件上传（PDF、DOC、DOCX、XLS、XLSX），单文件最大10MB

### 3. API文件
- **路径**: `hailong-admin/src/api/infoPublish.js`
- **功能**: 信息发布相关API接口封装

---

## 🎯 页面功能

### Tab标签页
页面使用Tab标签页切换不同信息类别，根据数据库表名定义：
- **公司公告** (`company_announcements`)
- **政策法规** (`policy_regulations`)
- **政策信息** (`policy_information`)
- **通知公告** (`notice_announcements`)

### 列表功能
1. **搜索**: 支持按标题或内容关键词搜索
2. **表格列**: 
   - 序号
   - 标题（支持溢出提示）
   - 发布时间
   - 访问量
   - 是否置顶（标签显示）
   - 附件数量
   - 创建时间
   - 操作按钮（编辑、删除）
3. **分页**: 支持每页10/20/50/100条数据

### 新增/编辑功能
对话框表单包含以下字段：
- **标题** (必填，最多255字符)
- **发布时间** (可选，日期时间选择器)
- **是否置顶** (开关组件)
- **内容** (必填，富文本编辑器)
- **附件上传** (支持多文件，最多10个)

---

## 🔌 API接口

### 1. 创建信息
```javascript
POST /api/info-publish
Content-Type: multipart/form-data

FormData:
- category: string (分类)
- title: string (标题)
- content: string (内容)
- publishTime: string (发布时间，可选)
- isTop: boolean (是否置顶)
- files: File[] (附件文件数组，可选)
```

### 2. 分页查询
```javascript
GET /api/info-publish?category={category}&keyword={keyword}&pageIndex={pageIndex}&pageSize={pageSize}
```

### 3. 获取详情
```javascript
GET /api/info-publish/{id}?category={category}
```

### 4. 更新信息
```javascript
PUT /api/info-publish/{id}
Content-Type: multipart/form-data

FormData:
- category: string (分类)
- title: string (标题)
- content: string (内容)
- publishTime: string (发布时间，可选)
- isTop: boolean (是否置顶)
- files: File[] (新增附件文件数组，可选)
```

### 5. 删除信息
```javascript
DELETE /api/info-publish/{id}?category={category}
```

---

## 📤 文件上传组件使用

### 基本用法
```vue
<FileUpload 
  ref="fileUploadRef"
  v-model="formData.attachments"
  :category="activeTab"
  :max-size-m-b="10"
  :limit="10"
/>
```

### Props
- `modelValue`: 已上传文件路径数组
- `category`: 文件分类（用于后端存储路径）
- `maxSizeMB`: 单个文件最大大小（MB），默认10
- `limit`: 最多上传文件数量，默认5
- `disabled`: 是否禁用

### 方法
- `getNewFiles()`: 获取新上传的文件对象数组
- `getAllFilePaths()`: 获取所有文件路径（包括已存在和新上传）
- `clearFiles()`: 清空所有文件
- `submit()`: 手动触发上传

### 支持的文件格式
- PDF (.pdf)
- Word (.doc, .docx)
- Excel (.xls, .xlsx)

---

## 🎨 页面特性

### 1. Tab自动切换
- 切换Tab时自动重置搜索条件和分页
- 加载对应分类的数据

### 2. 智能分页
- 删除最后一条数据时自动返回上一页
- 支持自定义每页显示数量

### 3. 富文本编辑
- 使用 wangEditor 富文本编辑器
- 支持图片和视频上传
- 支持常见文本格式化功能

### 4. 文件管理
- 新增时上传附件
- 编辑时显示已有附件
- 支持删除已有附件
- 支持添加新附件

### 5. 表单验证
- 标题必填且最大255字符
- 内容必填
- 文件类型和大小验证

---

## 🚀 路由配置示例

在路由配置中添加：

```javascript
{
  path: '/info-publish',
  name: 'InfoPublish',
  component: () => import('@/views/info-publish/InfoPublish.vue'),
  meta: {
    title: '信息发布管理',
    requiresAuth: true
  }
}
```

---

## 📝 注意事项

1. **后端接口对接**
   - 确保后端 `InfoPublishController` 正常运行
   - 文件上传依赖 `FileUploadService`
   - Category值必须与数据库表名一致

2. **文件上传**
   - 新增和编辑时都通过 FormData 传递文件
   - 后端自动处理文件存储和路径返回
   - 附件路径格式：`/uploads/{category}/{filename}`

3. **数据格式**
   - 日期时间格式：`YYYY-MM-DD HH:mm:ss`
   - 附件字段是字符串数组

4. **权限控制**
   - 建议添加路由权限验证
   - 根据需要添加按钮级权限控制

---

## 🐛 常见问题

### Q1: 文件上传失败
**A**: 检查以下几点：
1. 后端 FileUploadService 是否正常
2. 文件大小是否超过限制
3. 文件格式是否支持
4. 服务器上传目录权限

### Q2: 编辑时附件不显示
**A**: 确认：
1. 后端返回的 attachments 字段格式正确
2. FileUpload 组件正确绑定 v-model

### Q3: Tab切换后数据没有更新
**A**: 检查 `handleTabChange` 方法是否正确触发 `loadData()`

---

## 📊 数据流程

```
用户操作 → 前端表单 → FormData → API请求 → 后端Controller
                                              ↓
                                         FileUploadService
                                              ↓
                                         文件存储 + 路径返回
                                              ↓
                                         数据库保存
                                              ↓
                                         返回结果 → 前端更新
```

---

## ✅ 完成清单

- ✅ 文件上传组件 (FileUpload.vue)
- ✅ 统一管理页面 (InfoPublish.vue)
- ✅ API接口封装 (infoPublish.js)
- ✅ Tab标签页切换
- ✅ 列表查询和分页
- ✅ 新增/编辑功能
- ✅ 富文本编辑器
- ✅ 多文件上传
- ✅ 删除确认
- ✅ 表单验证

---

## 📞 技术支持

如有问题，请检查：
1. 浏览器控制台错误信息
2. 网络请求响应
3. 后端日志输出
