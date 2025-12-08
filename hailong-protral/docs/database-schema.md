# 海隆咨询 - 数据库管理数据结构文档

本文档定义了需要通过后台管理系统动态配置的数据结构。

## 1. 公告信息 (Announcements)

政府采购和建设工程公告统一存储在一个表中，通过类型字段区分。

```typescript
interface Announcement {
  id: number;                    // 公告ID
  title: string;                 // 公告标题
  category: string;              // 公告类别（GOV_PROCUREMENT=政府采购, CONSTRUCTION=建设工程）
  type: string;                  // 招标类型（公开招标/竞争性谈判/邀请招标等）
  budget: string;                // 预算金额（如：¥850万）
  budgetAmount: number;          // 预算金额数值（用于统计，单位：万元）
  deadline: string;              // 截止日期 (YYYY-MM-DD)
  status: string;                // 状态（进行中/即将开始/已结束）
  publishDate: string;           // 发布日期 (YYYY-MM-DD)
  content?: string;              // 公告详细内容（富文本）
  projectLocation?: string;      // 项目地点（建设工程专用）
  projectScale?: string;         // 项目规模（建设工程专用）
  region?: string;               // 所属地区（用于地区统计）
  attachments?: Attachment[];    // 附件列表
  contactPerson?: string;        // 联系人
  contactPhone?: string;         // 联系电话
  isHighlight: boolean;          // 是否重点展示
  views: number;                 // 浏览次数
  createdAt: Date;               // 创建时间
  updatedAt: Date;               // 更新时间
}
```

### 附件结构

```typescript
interface Attachment {
  id: number;                    // 附件ID
  announcementId: number;        // 关联的公告ID
  fileName: string;              // 文件名
  fileUrl: string;               // 文件URL
  fileSize: number;              // 文件大小（字节）
  fileType: string;              // 文件类型（pdf/doc/xls等）
  uploadDate: Date;              // 上传日期
}
```

### 交易数据统计说明

交易数据不单独存储，而是从公告信息表中统计获取：

```typescript
// 统计查询示例
interface TransactionStatistics {
  year: number;                  // 统计年份
  totalProjects: number;         // 项目总数 = COUNT(*)
  totalAmount: string;           // 交易总额 = SUM(budgetAmount)
  govProcurement: number;        // 政府采购数 = COUNT(WHERE category='GOV_PROCUREMENT')
  construction: number;          // 建设工程数 = COUNT(WHERE category='CONSTRUCTION')
  
  // 类型分布（从category字段统计）
  typeDistribution: {
    type: string;                // 类型名称
    count: number;               // 数量
    percentage: number;          // 百分比
  }[];
  
  // 地区排行（从region字段统计）
  regionRanking: {
    region: string;              // 地区名称
    projects: number;            // 项目数量 = COUNT(WHERE region=?)
    amount: number;              // 金额（亿元）= SUM(budgetAmount WHERE region=?)
  }[];
}
```

## 2. 资讯内容 (Contents)

新闻动态和政策法规统一存储在一个表中，通过类型字段区分。

```typescript
interface Content {
  id: number;                    // 内容ID
  title: string;                 // 标题
  contentType: string;           // 内容类型（NEWS=新闻动态, POLICY=政策法规）
  category: string;              // 分类
                                 // 新闻：公司新闻/行业动态/通知公告
                                 // 政策：法律法规/部门规章/行政法规/地方政策
  summary: string;               // 摘要
  content: string;               // 内容（富文本）
  coverImage?: string;           // 封面图片URL
  author?: string;               // 作者
  source?: string;               // 来源机构（政策专用）
  publishDate: string;           // 发布日期 (YYYY-MM-DD)
  effectiveDate?: string;        // 生效日期（政策专用）
  fileUrl?: string;              // 文件下载链接（政策专用）
  downloads: number;             // 下载次数（政策专用）
  views: number;                 // 浏览次数
  isTop: boolean;                // 是否置顶
  isPublished: boolean;          // 是否发布
  isValid: boolean;              // 是否有效（政策专用）
  tags?: string[];               // 标签
  createdAt: Date;               // 创建时间
  updatedAt: Date;               // 更新时间
}
```

## 3. 企业简介 (Company Profile)

```typescript
interface CompanyProfile {
  id: number;                    // ID
  title: string;                 // 标题
  content: string;               // 内容（富文本）
  imageUrl?: string;             // 配图URL
  displayOrder: number;          // 显示顺序
  isActive: boolean;             // 是否启用
  createdAt: Date;               // 创建时间
  updatedAt: Date;               // 更新时间
}
```

## 4. 重要业绩 (Major Achievements)

```typescript
interface MajorAchievement {
  id: number;                    // 业绩ID
  projectName: string;           // 项目名称
  projectType: string;           // 项目类型（工程/服务/货物）
  projectAmount: number;         // 项目金额（单位：万元）
  clientName?: string;           // 客户名称
  completionDate?: string;       // 完成日期 (YYYY-MM-DD)
  description?: string;          // 项目描述
  imageUrl?: string;             // 项目图片URL
  displayOrder: number;          // 显示顺序
  isHighlight: boolean;          // 是否重点展示
  createdAt: Date;               // 创建时间
  updatedAt: Date;               // 更新时间
}
```

## 5. 企业资质 (Qualifications)

```typescript
interface Qualification {
  id: number;                    // 资质ID
  name: string;                  // 资质名称
  description: string;           // 资质描述
  certificateNo?: string;        // 证书编号
  issueDate?: string;            // 颁发日期
  validUntil?: string;           // 有效期至
  image: string;                 // 资质证书图片URL
  displayOrder: number;          // 显示顺序
  isActive: boolean;             // 是否启用
  createdAt: Date;               // 创建时间
  updatedAt: Date;               // 更新时间
}
```

## 6. 业务范围 (Business Scope)

```typescript
interface BusinessScope {
  id: number;                    // 业务ID
  name: string;                  // 业务名称
  description: string;           // 业务描述
  features: string[];            // 业务特点列表
  image: string;                 // 业务配图URL
  displayOrder: number;          // 显示顺序
  isActive: boolean;             // 是否启用
  detailContent?: string;        // 详细内容（富文本）
  createdAt: Date;               // 创建时间
  updatedAt: Date;               // 更新时间
}
```

## 7. 常见问题 (FAQs)

```typescript
interface FAQ {
  id: number;                    // 问题ID
  question: string;              // 问题
  answer: string;                // 答案
  category: string;              // 分类（业务咨询/技术支持/其他）
  displayOrder: number;          // 显示顺序
  isActive: boolean;             // 是否启用
  views: number;                 // 查看次数
  createdAt: Date;               // 创建时间
  updatedAt: Date;               // 更新时间
}
```

## 数据库表关系说明

### 主要表
- `announcements` - 公告信息（包含政府采购和建设工程）
- `contents` - 资讯内容（包含新闻动态和政策法规）
- `company_profile` - 企业简介
- `major_achievements` - 重要业绩
- `qualifications` - 企业资质
- `business_scope` - 业务范围
- `faqs` - 常见问题

### 辅助表
- `attachments` - 附件表（关联公告信息）
- `users` - 管理员用户表
- `operation_logs` - 操作日志表

### 表关系
```
announcements (1) -----> (N) attachments
```

## 数据统计说明

### 交易数据统计
交易数据通过SQL查询从 `announcements` 表统计获得，不需要单独的统计表：

```sql
-- 年度统计
SELECT 
  YEAR(publishDate) as year,
  COUNT(*) as totalProjects,
  SUM(budgetAmount) as totalAmount,
  SUM(CASE WHEN category='GOV_PROCUREMENT' THEN 1 ELSE 0 END) as govProcurement,
  SUM(CASE WHEN category='CONSTRUCTION' THEN 1 ELSE 0 END) as construction
FROM announcements
WHERE YEAR(publishDate) = 2025
GROUP BY YEAR(publishDate);

-- 类型分布统计
SELECT 
  category,
  COUNT(*) as count,
  ROUND(COUNT(*) * 100.0 / (SELECT COUNT(*) FROM announcements), 2) as percentage
FROM announcements
GROUP BY category;

-- 地区排行统计
SELECT 
  region,
  COUNT(*) as projects,
  ROUND(SUM(budgetAmount) / 10000, 2) as amount
FROM announcements
WHERE region IS NOT NULL
GROUP BY region
ORDER BY projects DESC
LIMIT 10;
```

## API接口规范

### 通用响应格式

```typescript
interface ApiResponse<T> {
  success: boolean;              // 是否成功
  message: string;               // 消息
  data?: T;                      // 数据
  error?: string;                // 错误信息
  timestamp: number;             // 时间戳
}
```

### 分页响应格式

```typescript
interface PaginatedResponse<T> {
  success: boolean;
  message: string;
  data: {
    items: T[];                  // 数据列表
    total: number;               // 总数
    page: number;                // 当前页
    pageSize: number;            // 每页数量
    totalPages: number;          // 总页数
  };
  timestamp: number;
}
```

### 统计数据响应格式

```typescript
interface StatisticsResponse {
  success: boolean;
  message: string;
  data: {
    year: number;                // 统计年份
    summary: {
      totalProjects: number;     // 项目总数
      totalAmount: string;       // 交易总额
      govProcurement: number;    // 政府采购数
      construction: number;      // 建设工程数
    };
    typeDistribution: Array<{    // 类型分布
      type: string;
      count: number;
      percentage: number;
      color: string;
    }>;
    regionRanking: Array<{       // 地区排行
      region: string;
      projects: number;
      amount: number;
    }>;
  };
  timestamp: number;
}
```

## 注意事项

1. **日期格式** - 所有日期字段统一使用 ISO 8601 格式 (YYYY-MM-DD)
2. **富文本内容** - 使用 HTML 格式存储
3. **图片URL** - 建议使用CDN地址
4. **金额单位** - 统一为"万元"
5. **布尔字段** - 使用 true/false
6. **时间戳字段** - 所有表都应包含 createdAt 和 updatedAt
7. **软删除** - 删除操作建议使用软删除（添加 deletedAt 字段）
8. **操作日志** - 敏感操作需记录操作日志
9. **数据统计** - 交易数据通过SQL查询实时统计，不存储冗余数据
10. **类型区分** - 使用 category/contentType 字段区分不同类型的数据

## 索引建议

为提高查询性能，建议创建以下索引：

```sql
-- announcements 表
CREATE INDEX idx_category ON announcements(category);
CREATE INDEX idx_publish_date ON announcements(publishDate);
CREATE INDEX idx_region ON announcements(region);
CREATE INDEX idx_status ON announcements(status);

-- contents 表
CREATE INDEX idx_content_type ON contents(contentType);
CREATE INDEX idx_category ON contents(category);
CREATE INDEX idx_publish_date ON contents(publishDate);
CREATE INDEX idx_is_published ON contents(isPublished);

-- 其他表
CREATE INDEX idx_display_order ON company_profile(displayOrder);
CREATE INDEX idx_display_order ON major_achievements(displayOrder);
CREATE INDEX idx_display_order ON qualifications(displayOrder);
CREATE INDEX idx_display_order ON business_scope(displayOrder);
CREATE INDEX idx_display_order ON faqs(displayOrder);