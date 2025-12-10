# API快速参考手册

## 📚 目录
- [附件管理](#附件管理)
- [公告管理](#公告管理)
- [信息发布](#信息发布)
- [系统配置](#系统配置)
- [统计分析](#统计分析)
- [系统管理](#系统管理)

---

## 附件管理

### API导入
```javascript
import { attachmentApi } from '@/api'
```

### 常用方法
```javascript
// 获取附件列表
attachmentApi.getAttachmentList({ page: 1, pageSize: 20, category: 'image' })

// 上传附件
const formData = new FormData()
formData.append('file', file)
formData.append('category', 'image')
formData.append('relatedType', 'announcement')
attachmentApi.uploadAttachment(formData)

// 删除附件
attachmentApi.deleteAttachment(id)

// 批量删除
attachmentApi.batchDeleteAttachments([1, 2, 3])
```

### 字段说明
| 字段 | 类型 | 说明 |
|------|------|------|
| id | number | 附件ID |
| fileName | string | 文件名称 |
| fileUrl | string | 文件访问URL |
| fileSize | number | 文件大小（字节） |
| category | string | 附件分类：image/document/video/other |
| relatedType | string | 关联类型 |
| relatedId | number | 关联记录ID |

---

## 公告管理

### API导入
```javascript
import { announcementApi } from '@/api'
```

### 常用方法
```javascript
// 获取公告列表
announcementApi.getAnnouncementList({
  page: 1,
  pageSize: 20,
  businessType: 'GOV_PROCUREMENT', // 或 'CONSTRUCTION'
  noticeType: 'bidding',
  province: '河南省'
})

// 创建公告
announcementApi.createAnnouncement({
  title: '公告标题',
  businessType: 'GOV_PROCUREMENT',
  noticeType: 'bidding',
  procurementType: 'goods',
  content: '公告内容',
  attachmentIds: [1, 2, 3]
})

// 更新公告
announcementApi.updateAnnouncement(id, data)

// 删除公告
announcementApi.deleteAnnouncement(id)

// 置顶
announcementApi.toggleAnnouncementTop(id, 1)
```

### 字段说明
| 字段 | 类型 | 说明 |
|------|------|------|
| id | number | 公告ID |
| title | string | 公告标题 |
| businessType | string | 业务类型：GOV_PROCUREMENT/CONSTRUCTION |
| noticeType | string | 公告类型：bidding/correction/result |
| procurementType | string | 采购类型：goods/service/project（仅政府采购） |
| bidder | string | 招标人 |
| winner | string | 中标人 |
| budgetAmount | number | 预算金额（万元） |
| deadline | datetime | 截止时间 |
| province | string | 省份 |
| city | string | 城市 |
| district | string | 区县 |
| projectRegion | string | 项目区域 |
| content | string | 公告内容（富文本） |
| attachmentIds | array | 附件ID列表 |
| isTop | number | 是否置顶：0/1 |
| viewCount | number | 浏览次数 |

---

## 信息发布

### API导入
```javascript
import { infoPublicationApi } from '@/api'
```

### 常用方法
```javascript
// 获取信息发布列表
infoPublicationApi.getInfoPublicationList({
  page: 1,
  pageSize: 20,
  type: 'COMPANY_NEWS', // COMPANY_NEWS/POLICY_REGULATION/POLICY_INFO/NOTICE
  category: '公司新闻'
})

// 创建信息发布
infoPublicationApi.createInfoPublication({
  type: 'COMPANY_NEWS',
  category: '公司新闻',
  title: '标题',
  summary: '摘要',
  content: '内容',
  coverImageId: 1,
  attachmentIds: [1, 2, 3]
})

// 更新信息发布
infoPublicationApi.updateInfoPublication(id, data)

// 删除信息发布
infoPublicationApi.deleteInfoPublication(id)

// 获取分类列表
infoPublicationApi.getCategoryList('COMPANY_NEWS')
```

### 字段说明
| 字段 | 类型 | 说明 |
|------|------|------|
| id | number | 信息ID |
| type | string | 信息类型：COMPANY_NEWS/POLICY_REGULATION/POLICY_INFO/NOTICE |
| category | string | 二级分类 |
| title | string | 标题 |
| summary | string | 摘要 |
| content | string | 内容（富文本） |
| coverImageId | number | 封面图片ID |
| author | string | 作者 |
| publisher | string | 发布人 |
| publishTime | datetime | 发布时间 |
| attachmentIds | array | 附件ID列表 |
| isTop | number | 是否置顶：0/1 |
| viewCount | number | 浏览次数 |

### 分类说明
**公司公告 (COMPANY_NEWS)**
- 公司新闻
- 行业动态
- 通知公告

**政策法规 (POLICY_REGULATION)**
- 法律法规
- 部门规章
- 行政法规
- 地方政策

---

## 系统配置

### API导入
```javascript
import { systemConfigApi } from '@/api'
```

### 企业简介
```javascript
// 获取企业简介
systemConfigApi.getCompanyProfileList()

// 保存企业简介
systemConfigApi.saveCompanyProfile({
  title: '企业简介',
  content: '简介内容',
  highlights: ['专业资质齐全', '经验丰富团队'],
  imageIds: [1, 2, 3]
})
```

### 业务范围
```javascript
// 获取业务范围列表
systemConfigApi.getBusinessScopeList()

// 创建业务范围
systemConfigApi.createBusinessScope({
  name: '业务名称',
  description: '业务描述',
  content: '详细内容',
  features: ['特点1', '特点2'],
  imageId: 1,
  sortOrder: 1
})

// 更新排序
systemConfigApi.updateBusinessScopeSort([
  { id: 1, sortOrder: 1 },
  { id: 2, sortOrder: 2 }
])
```

### 企业资质
```javascript
// 获取企业资质列表
systemConfigApi.getQualificationList()

// 创建企业资质
systemConfigApi.createQualification({
  name: '资质名称',
  description: '资质描述',
  imageId: 1,
  certificateNo: '证书编号',
  issueDate: '2024-01-01',
  expiryDate: '2029-01-01'
})
```

### 重要业绩
```javascript
// 获取重要业绩列表
systemConfigApi.getAchievementList()

// 创建重要业绩
systemConfigApi.createAchievement({
  projectName: '项目名称',
  projectType: '工程',
  projectAmount: 1000,
  clientName: '客户名称',
  description: '项目描述',
  completionDate: '2024-01-01',
  imageIds: [1, 2, 3]
})
```

### 企业荣誉
```javascript
// 获取企业荣誉列表
systemConfigApi.getHonorList()

// 创建企业荣誉
systemConfigApi.createHonor({
  name: '荣誉名称',
  description: '荣誉描述',
  imageId: 1,
  awardOrganization: '颁发机构',
  awardDate: '2024-01-01',
  certificateNo: '证书编号',
  honorLevel: '国家级'
})
```

### 轮播图
```javascript
// 获取轮播图列表
systemConfigApi.getBannerList()

// 创建轮播图
systemConfigApi.createBanner({
  title: '轮播图标题',
  description: '轮播图描述',
  imageId: 1,
  linkUrl: 'https://example.com',
  sortOrder: 1
})

// 更新排序
systemConfigApi.updateBannerSort([
  { id: 1, sortOrder: 1 },
  { id: 2, sortOrder: 2 }
])
```

### 友情链接
```javascript
// 获取友情链接列表
systemConfigApi.getFriendlyLinkList()

// 创建友情链接
systemConfigApi.createFriendlyLink({
  name: '链接名称',
  url: 'https://example.com',
  logoId: 1,
  description: '链接描述',
  sortOrder: 1
})
```

---

## 统计分析

### API导入
```javascript
import { statisticsApi } from '@/api'
```

### 访问统计
```javascript
// 获取访问统计列表
statisticsApi.getVisitStatisticsList({
  page: 1,
  pageSize: 20,
  startDate: '2024-01-01',
  endDate: '2024-12-31'
})

// 获取访问统计概览
statisticsApi.getVisitStatisticsOverview({
  startDate: '2024-01-01',
  endDate: '2024-12-31'
})

// 获取访问趋势
statisticsApi.getVisitTrend({
  startDate: '2024-01-01',
  endDate: '2024-12-31',
  groupBy: 'day' // day/week/month
})

// 获取热门页面
statisticsApi.getPopularPages({
  startDate: '2024-01-01',
  endDate: '2024-12-31',
  limit: 10
})

// 记录访问
statisticsApi.recordVisit({
  pageUrl: '/announcements/1',
  pageTitle: '公告标题',
  referer: 'https://example.com'
})
```

### 公告统计
```javascript
// 获取公告统计概览
statisticsApi.getAnnouncementStatisticsOverview()

// 获取公告发布趋势
statisticsApi.getAnnouncementPublishTrend({
  startDate: '2024-01-01',
  endDate: '2024-12-31',
  businessType: 'GOV_PROCUREMENT',
  groupBy: 'month'
})

// 获取公告类型分布
statisticsApi.getAnnouncementTypeDistribution()

// 获取公告区域分布
statisticsApi.getAnnouncementRegionDistribution({
  businessType: 'GOV_PROCUREMENT',
  limit: 10
})

// 获取热门公告
statisticsApi.getPopularAnnouncements({
  businessType: 'GOV_PROCUREMENT',
  limit: 10
})
```

### 首页统计
```javascript
// 获取首页统计数据
statisticsApi.getHomeStatistics()
```

---

## 系统管理

### API导入
```javascript
import { systemApi } from '@/api'
```

### 用户管理
```javascript
// 获取用户列表
systemApi.getUserList({
  page: 1,
  pageSize: 20,
  keyword: '搜索关键词',
  role: 'admin',
  status: 1
})

// 创建用户
systemApi.createUser({
  username: 'user001',
  password: '123456',
  email: 'user@example.com',
  phone: '13800138000',
  realName: '张三',
  role: 'user',
  status: 1
})

// 更新用户
systemApi.updateUser(id, data)

// 删除用户
systemApi.deleteUser(id)

// 重置密码
systemApi.resetUserPassword(id, 'newPassword')

// 修改当前用户密码
systemApi.changePassword({
  oldPassword: 'oldPassword',
  newPassword: 'newPassword'
})

// 启用/禁用用户
systemApi.toggleUserStatus(id, 1)
```

### 系统日志
```javascript
// 获取系统日志列表
systemApi.getSystemLogList({
  page: 1,
  pageSize: 20,
  username: '用户名',
  action: '操作动作',
  module: '操作模块',
  status: 'success',
  startDate: '2024-01-01',
  endDate: '2024-12-31'
})

// 获取系统日志详情
systemApi.getSystemLogDetail(id)

// 清理系统日志
systemApi.clearSystemLogs({
  beforeDate: '2024-01-01'
})

// 导出系统日志
systemApi.exportSystemLogs(params)
```

### 区域字典
```javascript
// 获取区域列表
systemApi.getRegionList({
  regionLevel: 1, // 1-省份，2-城市，3-区县
  parentCode: '410000',
  keyword: '搜索关键词'
})

// 获取省份列表
systemApi.getProvinceList()

// 获取城市列表
systemApi.getCityList('410000')

// 获取区县列表
systemApi.getDistrictList('410100')

// 创建区域
systemApi.createRegion({
  regionCode: '410000',
  regionName: '河南省',
  regionLevel: 1,
  parentCode: null,
  sortOrder: 1
})

// 批量导入区域
const formData = new FormData()
formData.append('file', file)
systemApi.importRegions(formData)

// 导出区域
systemApi.exportRegions()
```

---

## 通用字段说明

### 状态字段
- `status`: 0-禁用，1-启用
- `isTop`: 0-不置顶，1-置顶
- `isDeleted`: 0-未删除，1-已删除

### 时间字段
- `createdAt`: 创建时间
- `updatedAt`: 更新时间
- `publishTime`: 发布时间

### 排序字段
- `sortOrder`: 排序顺序（数值越小越靠前）

### JSON数组字段
- `attachmentIds`: 附件ID列表，如：`[1, 2, 3]`
- `imageIds`: 图片ID列表，如：`[1, 2, 3]`
- `highlights`: 特色标签，如：`["标签1", "标签2"]`
- `features`: 特点列表，如：`["特点1", "特点2"]`

---

## 响应格式

### 成功响应
```json
{
  "code": 200,
  "message": "success",
  "data": {
    // 数据内容
  }
}
```

### 分页响应
```json
{
  "code": 200,
  "message": "success",
  "data": {
    "items": [],
    "total": 100,
    "page": 1,
    "pageSize": 20
  }
}
```

### 错误响应
```json
{
  "code": 400,
  "message": "错误信息",
  "data": null
}
```

---

## 常用工具函数

### 文件大小格式化
```javascript
const formatFileSize = (bytes) => {
  if (!bytes) return '-'
  if (bytes < 1024) return bytes + ' B'
  if (bytes < 1024 * 1024) return (bytes / 1024).toFixed(2) + ' KB'
  if (bytes < 1024 * 1024 * 1024) return (bytes / 1024 / 1024).toFixed(2) + ' MB'
  return (bytes / 1024 / 1024 / 1024).toFixed(2) + ' GB'
}
```

### 日期格式化
```javascript
import { formatDate } from '@/utils/date'

formatDate(new Date(), 'YYYY-MM-DD HH:mm:ss')
```

### Token管理
```javascript
import { tokenUtils } from '@/utils/auth'

// 获取Token
const token = tokenUtils.getToken()

// 设置Token
tokenUtils.setToken('token_value')

// 移除Token
tokenUtils.removeToken()
```

---

**最后更新**: 2025-12-10