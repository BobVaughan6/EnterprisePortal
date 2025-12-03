# 信息发布管理页面 - 快速集成指南

## 📋 前置检查

在开始之前，请确认：

- [x] 后端 `InfoPublishController.cs` 已部署
- [x] 后端 `FileUploadService.cs` 已配置
- [x] 数据库表已创建（4个表：company_announcements, policy_regulations, policy_information, notice_announcements）
- [x] 前端已安装必要依赖

---

## 🚀 快速开始

### 步骤1: 文件已创建

以下文件已自动创建：

```
hailong-admin/
├── src/
│   ├── components/
│   │   └── FileUpload.vue          ✅ 文件上传组件
│   ├── views/
│   │   └── info-publish/
│   │       └── InfoPublish.vue     ✅ 统一管理页面
│   └── api/
│       └── infoPublish.js          ✅ API接口（已优化）
└── INFO_PUBLISH_IMPLEMENTATION.md  ✅ 详细文档
```

### 步骤2: 配置路由

在 `src/router/index.js` 中添加路由配置：

```javascript
{
  path: '/info-publish',
  name: 'InfoPublish',
  component: () => import('@/views/info-publish/InfoPublish.vue'),
  meta: {
    title: '信息发布管理',
    requiresAuth: true,
    icon: 'Document'
  }
}
```

### 步骤3: 添加菜单项（可选）

如果使用侧边栏菜单，在菜单配置中添加：

```javascript
{
  title: '信息发布',
  icon: 'Document',
  path: '/info-publish',
  children: [] // 或者添加子菜单
}
```

### 步骤4: 验证依赖

确保已安装以下依赖：

```json
{
  "element-plus": "^2.x.x",
  "@element-plus/icons-vue": "^2.x.x",
  "@wangeditor/editor-for-vue": "^5.x.x",
  "axios": "^1.x.x"
}
```

如未安装，运行：

```bash
npm install @wangeditor/editor-for-vue
```

---

## 🔧 配置说明

### 后端API基础地址

确认 `src/config/api.config.js` 中的配置：

```javascript
export const API_CONFIG = {
  baseURL: 'http://localhost:5000', // 修改为你的后端地址
  timeout: 30000
}
```

### 文件上传配置

FileUpload组件默认配置：
- 支持格式: PDF, DOC, DOCX, XLS, XLSX
- 单文件最大: 10MB
- 最多文件数: 10个

如需修改，在使用组件时传入props：

```vue
<FileUpload 
  :max-size-m-b="20"    <!-- 修改为20MB -->
  :limit="5"            <!-- 最多5个文件 -->
/>
```

---

## 📝 使用示例

### 基本使用

1. 启动后端服务
2. 启动前端开发服务器：`npm run dev`
3. 访问页面：`http://localhost:5173/info-publish`

### Tab切换逻辑

页面会根据当前激活的Tab自动加载对应分类的数据：

- **公司公告**: `company_announcements`
- **政策法规**: `policy_regulations`  
- **政策信息**: `policy_information`
- **通知公告**: `notice_announcements`

### 新增信息流程

1. 点击"新增信息"按钮
2. 填写标题（必填）
3. 选择发布时间（可选）
4. 设置是否置顶
5. 编辑内容（富文本）
6. 上传附件（可选）
7. 点击"提交"

### 编辑信息流程

1. 点击表格行的"编辑"按钮
2. 系统自动加载详情数据
3. 修改需要更新的字段
4. 可以删除已有附件或添加新附件
5. 点击"提交"保存

---

## 🎯 功能清单

### 列表页面
- [x] Tab标签页切换
- [x] 关键词搜索
- [x] 分页显示
- [x] 访问量统计
- [x] 置顶状态显示
- [x] 附件数量显示

### 新增/编辑
- [x] 标题输入（带字数限制）
- [x] 富文本内容编辑
- [x] 发布时间选择
- [x] 置顶开关
- [x] 多文件上传
- [x] 已有附件管理
- [x] 表单验证

### 其他功能
- [x] 删除确认对话框
- [x] 加载状态显示
- [x] 错误提示
- [x] 成功反馈

---

## 🔍 测试检查点

### 1. 列表功能测试
- [ ] 页面初始加载显示数据
- [ ] Tab切换正常
- [ ] 搜索功能正常
- [ ] 分页功能正常
- [ ] 数据排序正确（置顶在前）

### 2. 新增功能测试
- [ ] 必填字段验证
- [ ] 富文本编辑器正常
- [ ] 文件上传成功
- [ ] 提交后列表刷新

### 3. 编辑功能测试
- [ ] 详情数据加载正确
- [ ] 已有附件显示
- [ ] 更新数据成功
- [ ] 附件管理正常

### 4. 删除功能测试
- [ ] 删除确认对话框
- [ ] 删除成功提示
- [ ] 列表自动刷新

### 5. 文件上传测试
- [ ] 支持的格式可以上传
- [ ] 不支持的格式被拦截
- [ ] 超大文件被拦截
- [ ] 超出数量限制提示

---

## ⚙️ 高级配置

### 自定义Tab配置

如需修改Tab标签，编辑 `InfoPublish.vue` 中的 `tabConfig`：

```javascript
const tabConfig = {
  'company_announcements': '公司公告',
  'policy_regulations': '政策法规',
  'policy_information': '政策信息',
  'notice_announcements': '通知公告'
  // 可以添加更多分类
}
```

### 自定义表格列

在 `InfoPublish.vue` 的 `<el-table>` 部分修改列配置：

```vue
<el-table-column prop="customField" label="自定义列" width="120" />
```

### 自定义表单字段

在对话框的 `<el-form>` 部分添加字段：

```vue
<el-form-item label="自定义字段" prop="customField">
  <el-input v-model="formData.customField" />
</el-form-item>
```

---

## 🐛 故障排查

### 问题1: 页面空白或报错
**解决方案**:
1. 检查浏览器控制台错误
2. 确认所有依赖已安装
3. 检查路由配置是否正确

### 问题2: API请求失败
**解决方案**:
1. 检查后端服务是否启动
2. 确认API基础地址配置正确
3. 检查网络请求状态码和响应

### 问题3: 文件上传失败
**解决方案**:
1. 确认后端FileUploadService正常
2. 检查文件格式和大小
3. 查看后端上传目录权限
4. 检查服务器磁盘空间

### 问题4: 富文本编辑器不显示
**解决方案**:
1. 确认 `@wangeditor/editor-for-vue` 已安装
2. 检查RichEditor组件路径是否正确
3. 查看浏览器控制台CSS加载情况

---

## 📊 数据库对应关系

| Tab名称 | category值 | 数据库表名 |
|---------|-----------|-----------|
| 公司公告 | company_announcements | company_announcements |
| 政策法规 | policy_regulations | policy_regulations |
| 政策信息 | policy_information | policy_information |
| 通知公告 | notice_announcements | notice_announcements |

---

## 🎓 扩展建议

### 1. 添加权限控制

```javascript
// 在路由meta中添加
meta: {
  requiresAuth: true,
  permissions: ['info-publish:view', 'info-publish:edit']
}
```

### 2. 添加批量操作

在表格中添加批量删除、批量置顶等功能：

```vue
<el-table @selection-change="handleSelectionChange">
  <el-table-column type="selection" width="55" />
  <!-- 其他列 -->
</el-table>

<el-button @click="handleBatchDelete">批量删除</el-button>
```

### 3. 添加数据导出

```javascript
const handleExport = () => {
  // 导出Excel逻辑
}
```

### 4. 添加审核流程

在表单中添加状态字段（草稿、待审核、已发布）

---

## ✨ 完成

恭喜！信息发布统一管理页面已成功集成到项目中。

如有任何问题，请参考 `INFO_PUBLISH_IMPLEMENTATION.md` 详细文档。
