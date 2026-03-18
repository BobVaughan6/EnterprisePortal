# 企业资质排序功能设计文档

**日期：** 2026-03-18
**功能：** 在管理后台添加企业资质排序字段的显示和编辑功能

## 背景

企业资质模块的排序功能在后端和前端门户已完整实现，但管理后台缺少排序字段的显示和编辑界面。本次需求是在管理后台补充这部分功能。

## 当前状态

### 已实现
- 数据库 `company_qualifications` 表已有 `sort_order` 字段（int 类型，默认值 0）
- 后端实体 `CompanyQualification` 已有 `SortOrder` 属性
- 后端 DTO（`CompanyQualificationDto`、`CreateCompanyQualificationDto`、`UpdateCompanyQualificationDto`）已包含 `sortOrder` 字段
- 后端查询接口 `GetAllQualificationsAsync` 已按 `SortOrder` 升序排序
- 前端门户 `About.vue` 已按 `sortOrder` 排序显示资质列表

### 待实现
- 管理后台表格中显示排序字段
- 管理后台表单中编辑排序字段

## 设计方案

### 1. 表格列添加

在 `hailong-admin/src/views/config/Qualifications.vue` 的表格中添加"排序"列：

**位置：** 在"序号"列之后，"资质名称"列之前

**配置：**
- 列标题：排序
- 绑定字段：`sortOrder`
- 列宽：80px
- 对齐方式：居中

### 2. 表单字段添加

在新增/编辑对话框的表单中添加"排序"输入框：

**位置：** 在"资质描述"字段之后，"状态"字段之前

**组件：** `el-input-number`

**配置：**
- 标签：排序
- 绑定字段：`formData.sortOrder`
- 最小值：0
- 步长：1
- 默认值：0
- 提示文字：数字越小越靠前，相同数字按颁发日期排序

### 3. 数据处理

**表单数据初始化：**
- 在 `formData` 对象中添加 `sortOrder: 0`
- 新增时默认值为 0
- 编辑时回填现有的 `sortOrder` 值

**表格排序：**
- 获取列表后按 `sortOrder` 升序排列（后端已实现，前端无需额外处理）

## 实现文件

- `hailong-admin/src/views/config/Qualifications.vue`

## 验收标准

1. 表格中显示"排序"列，展示每条资质的排序值
2. 新增资质时可以设置排序值，默认为 0
3. 编辑资质时可以修改排序值，回填现有值
4. 提交后排序值正确保存到数据库
5. 表格列表按排序值升序显示
6. 前端门户按排序值正确显示资质列表

## 技术说明

- 使用 Element Plus 的 `el-input-number` 组件确保输入值为有效数字
- 排序逻辑完全依赖后端实现，前端只负责显示和编辑
- 排序值相同时，后端按颁发日期降序排列（已实现）
