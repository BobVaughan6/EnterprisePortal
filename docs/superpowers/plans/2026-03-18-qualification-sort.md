# 企业资质排序功能实现计划

> **For agentic workers:** REQUIRED: Use superpowers:subagent-driven-development (if subagents available) or superpowers:executing-plans to implement this plan. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** 在管理后台添加企业资质排序字段的显示和编辑功能

**Architecture:** 在现有的 Qualifications.vue 组件中添加排序列和表单字段，利用已有的后端 sortOrder 字段支持

**Tech Stack:** Vue 3, Element Plus, Composition API

---

## File Structure

**Modify:**
- `hailong-admin/src/views/config/Qualifications.vue` - 添加排序列和表单字段

## Tasks

### Task 1: 添加表格排序列

**Files:**
- Modify: `hailong-admin/src/views/config/Qualifications.vue:13-14`

- [ ] **Step 1: 在表格中添加排序列**

在"序号"列之后添加排序列：

```vue
<el-table-column type="index" label="序号" width="60" />
<el-table-column prop="sortOrder" label="排序" width="80" align="center" />
<el-table-column prop="name" label="资质名称" min-width="200" />
```

- [ ] **Step 2: 验证表格显示**

启动开发服务器，访问企业资质管理页面，确认：
- 排序列显示在序号列之后
- 列宽为 80px，内容居中对齐
- 显示每条记录的 sortOrder 值

- [ ] **Step 3: Commit**

```bash
git add hailong-admin/src/views/config/Qualifications.vue
git commit -m "feat: 添加企业资质表格排序列

Co-Authored-By: Claude Sonnet 4.6 <noreply@anthropic.com>"
```

---

### Task 2: 添加表单排序字段

**Files:**
- Modify: `hailong-admin/src/views/config/Qualifications.vue:128-130`

- [ ] **Step 1: 在表单中添加排序输入框**

在"资质描述"字段之后，"状态"字段之前添加排序字段。修改后的代码：

```vue
<el-form-item label="资质描述">
  <el-input
    v-model="formData.description"
    type="textarea"
    :rows="4"
    placeholder="请输入资质描述（最多500个字符）"
    maxlength="500"
    show-word-limit
  />
</el-form-item>

<el-form-item label="排序">
  <el-input-number
    v-model="formData.sortOrder"
    :min="0"
    :step="1"
    controls-position="right"
    style="width: 200px;"
  />
  <div class="form-tip">数字越小越靠前，相同数字按颁发日期排序</div>
</el-form-item>

<el-form-item label="状态">
  <el-radio-group v-model="formData.status">
    <el-radio :value="true">有效</el-radio>
    <el-radio :value="false">失效</el-radio>
  </el-radio-group>
</el-form-item>
```

- [ ] **Step 2: 验证表单显示**

打开新增/编辑对话框，确认：
- 排序字段显示在资质描述和状态之间
- 使用数字输入框，最小值为 0
- 显示提示文字

- [ ] **Step 3: Commit**

```bash
git add hailong-admin/src/views/config/Qualifications.vue
git commit -m "feat: 添加企业资质表单排序字段

Co-Authored-By: Claude Sonnet 4.6 <noreply@anthropic.com>"
```

---

### Task 3: 更新表单数据结构

**Files:**
- Modify: `hailong-admin/src/views/config/Qualifications.vue:163-173,227-237,249-259,315-326`

- [ ] **Step 1: 在 formData 中添加 sortOrder 字段**

修改 formData 定义（line 163-173）：

```javascript
// 表单数据
const formData = reactive({
  id: null,
  name: '',
  certificateNumber: '',
  issuingAuthority: '',
  issueDate: '',
  expiryDate: '',
  certificateImageId: [],
  description: '',
  sortOrder: 0,
  status: true
})
```

- [ ] **Step 2: 更新 handleAdd 方法中的重置逻辑**

修改 handleAdd 方法（line 227-237）：

```javascript
const handleAdd = () => {
  isEdit.value = false
  Object.assign(formData, {
    id: null,
    name: '',
    certificateNumber: '',
    issuingAuthority: '',
    issueDate: '',
    expiryDate: '',
    certificateImageId: [],
    description: '',
    sortOrder: 0,
    status: true
  })
  dialogVisible.value = true
}
```

- [ ] **Step 3: 更新 handleEdit 方法中的回填逻辑**

修改 handleEdit 方法中的 Object.assign 部分（line 249-259）：

```javascript
Object.assign(formData, {
  id: res.data.id,
  name: res.data.name,
  certificateNumber: res.data.certificateNumber,
  issuingAuthority: res.data.issuingAuthority || '',
  issueDate: res.data.issueDate,
  expiryDate: res.data.expiryDate,
  certificateImageId: res.data.certificateImageId ? [res.data.certificateImageId] : [],
  description: res.data.description || '',
  sortOrder: res.data.sortOrder || 0,
  status: res.data.status
})
```

- [ ] **Step 4: 更新 handleSubmit 方法中的提交数据**

修改 submitData 对象（line 315-326）：

```javascript
const submitData = {
  name: formData.name,
  certificateNumber: formData.certificateNumber,
  issuingAuthority: formData.issuingAuthority || null,
  issueDate: formData.issueDate,
  expiryDate: formData.expiryDate,
  certificateImageId: formData.certificateImageId && formData.certificateImageId.length > 0
    ? formData.certificateImageId[0]
    : null,
  description: formData.description || null,
  sortOrder: formData.sortOrder || 0,
  status: formData.status
}
```

- [ ] **Step 5: 验证数据流**

测试完整流程：
1. 新增资质，设置排序值为 5，提交
2. 编辑该资质，确认排序值回填为 5
3. 修改排序值为 3，提交
4. 刷新页面，确认排序值为 3
5. 检查表格列表按排序值升序显示

Expected: 所有操作正常，数据正确保存和显示

- [ ] **Step 6: Commit**

```bash
git add hailong-admin/src/views/config/Qualifications.vue
git commit -m "feat: 完善企业资质排序字段数据处理

Co-Authored-By: Claude Sonnet 4.6 <noreply@anthropic.com>"
```

---

### Task 4: 验证门户显示

**Files:**
- Verify: `hailong-protral/src/views/pages/About.vue`

- [ ] **Step 1: 测试门户排序显示**

1. 在管理后台创建/编辑多条资质，设置不同的排序值（如 1, 3, 2, 5）
2. 访问门户的"关于我们"页面，切换到"企业资质"标签
3. 确认资质按排序值升序显示（1, 2, 3, 5）

Expected: 门户按排序值正确显示资质列表

- [ ] **Step 2: 测试相同排序值的情况**

1. 创建两条资质，设置相同的排序值（如都为 10）
2. 设置不同的颁发日期
3. 确认相同排序值时按颁发日期降序排列

Expected: 排序逻辑符合设计要求

---

## Verification Checklist

完成所有任务后，验证以下内容：

- [ ] 管理后台表格显示排序列
- [ ] 新增资质时可以设置排序值，默认为 0
- [ ] 编辑资质时排序值正确回填
- [ ] 提交后排序值正确保存到数据库
- [ ] 管理后台表格按排序值升序显示
- [ ] 门户页面按排序值正确显示资质列表
- [ ] 所有更改已提交到 git

---

## Notes

- 后端接口已完整支持 sortOrder 字段，无需修改后端代码
- 排序逻辑由后端实现，前端只负责显示和编辑
- 使用 Element Plus 的 el-input-number 组件确保输入值为有效数字
