# 首页交易数据可视化统计API文档

## API清单

### 1. 获取首页统计概览
**接口地址**: `GET /api/home/statistics/overview`

**功能说明**: 返回首页统计数据，包括总项目数、交易类型占比、地区排行榜

**请求参数**: 无

**返回示例**:
```json
{
  "success": true,
  "message": "操作成功",
  "data": {
    "totalProjects": 1258,
    "totalAmount": 0,
    "projectTypes": [
      {
        "type": "政府采购",
        "count": 680,
        "percentage": 54.05
      },
      {
        "type": "建设工程",
        "count": 578,
        "percentage": 45.95
      }
    ],
    "regionRanking": [
      {
        "region": "郑州市",
        "projectCount": 356,
        "amount": 0
      },
      {
        "region": "洛阳市",
        "projectCount": 198,
        "amount": 0
      },
      {
        "region": "开封市",
        "projectCount": 145,
        "amount": 0
      }
    ]
  }
}
```

**统计逻辑说明**:
- `totalProjects`: 统计 `government_procurement_notices` 和 `construction_project_notices` 两个表的总记录数（排除软删除）
- `totalAmount`: 数据库中没有金额字段，返回0
- `projectTypes`: 按两个公告表分别统计数量和占比
- `regionRanking`: 按 `project_region` 字段分组统计，合并两个表的数据，按项目数量降序排列，取前10条

---

### 2. 获取最新公告列表
**接口地址**: `GET /api/home/recent-announcements`

**功能说明**: 返回最新公告列表（政府采购5条+建设工程5条，合并后按时间排序取前10条）

**请求参数**: 无

**返回示例**:
```json
{
  "success": true,
  "message": "操作成功",
  "data": [
    {
      "id": 1025,
      "title": "郑州市某医院医疗设备采购项目招标公告",
      "noticeType": "采购公告",
      "projectRegion": "郑州市",
      "publishTime": "2025-12-02T14:30:00",
      "sourceType": "政府采购"
    },
    {
      "id": 856,
      "title": "洛阳市某道路建设工程中标公告",
      "noticeType": "中标公告",
      "projectRegion": "洛阳市",
      "publishTime": "2025-12-02T10:15:00",
      "sourceType": "建设工程"
    },
    {
      "id": 1024,
      "title": "开封市某学校办公设备采购结果公告",
      "noticeType": "结果公告",
      "projectRegion": "开封市",
      "publishTime": "2025-12-01T16:45:00",
      "sourceType": "政府采购"
    }
  ]
}
```

**统计逻辑说明**:
- 从 `government_procurement_notices` 表获取最新5条记录（按 `publish_time` 降序，排除软删除）
- 从 `construction_project_notices` 表获取最新5条记录（按 `publish_time` 降序，排除软删除）
- 合并两个结果集，按 `publish_time` 降序排序，取前10条

---

### 3. 获取重要业绩列表
**接口地址**: `GET /api/home/achievements`

**功能说明**: 返回重要业绩列表

**请求参数**: 无

**返回示例**:
```json
{
  "success": true,
  "message": "操作成功",
  "data": [
    {
      "id": 1,
      "projectName": "郑州市某大型医院医疗设备采购项目",
      "projectType": "政府采购",
      "projectAmount": 5800.50,
      "clientName": "郑州市某医院",
      "completionDate": "2025-10-15",
      "description": "为郑州市某三甲医院提供全套医疗设备采购咨询服务，项目金额5800万元",
      "imageUrl": "/uploads/achievements/project1.jpg"
    },
    {
      "id": 2,
      "projectName": "洛阳市某道路建设工程监理项目",
      "projectType": "建设工程",
      "projectAmount": 3200.00,
      "clientName": "洛阳市交通局",
      "completionDate": "2025-09-20",
      "description": "为洛阳市某主干道建设提供全过程工程监理服务",
      "imageUrl": "/uploads/achievements/project2.jpg"
    }
  ]
}
```

**统计逻辑说明**:
- 从 `major_achievements` 表查询所有记录（排除软删除 `is_deleted = 0`）
- 按 `sort_order` 升序排序（数字越小越靠前）
- 相同排序值的按 `completion_date` 降序排序

---

## 数据来源说明

### 数据库表
1. **政府采购公告表**: `government_procurement_notices`
   - 字段: `id`, `title`, `notice_type`, `project_region`, `publish_time`, `is_deleted`
   
2. **建设工程公告表**: `construction_project_notices`
   - 字段: `id`, `title`, `notice_type`, `project_region`, `publish_time`, `is_deleted`
   
3. **重要业绩表**: `major_achievements`
   - 字段: `id`, `project_name`, `project_type`, `project_amount`, `client_name`, `completion_date`, `description`, `image_url`, `sort_order`, `is_deleted`

### 区域字段值范围
根据数据库设计，`project_region` 字段存储河南省各地市名称，如：
- 郑州市
- 洛阳市
- 开封市
- 新乡市
- 许昌市
- 等等...

### 金额字段说明
**重要提示**: 
- `government_procurement_notices` 和 `construction_project_notices` 表中**没有金额字段**
- 因此 `totalAmount` 和地区排行中的 `amount` 字段返回 0
- 只有 `major_achievements` 表中有 `project_amount` 字段（单位：万元）

### 软删除过滤
所有查询都会过滤软删除的记录：
- `government_procurement_notices.is_deleted = false`
- `construction_project_notices.is_deleted = false`
- `major_achievements.is_deleted = 0`

---

## 测试建议

### 使用Swagger测试
1. 启动项目后访问: `http://localhost:5000`
2. 找到 `Home` 分组下的三个接口
3. 点击 "Try it out" 按钮测试

### 使用Postman测试
```bash
# 1. 获取统计概览
GET http://localhost:5000/api/home/statistics/overview

# 2. 获取最新公告
GET http://localhost:5000/api/home/recent-announcements

# 3. 获取重要业绩
GET http://localhost:5000/api/home/achievements
```

---

## 实现文件清单

1. **DTO模型**: `Models/DTOs/HomeStatisticsDto.cs`
   - `HomeStatisticsDto`: 统计概览DTO
   - `ProjectTypeStatDto`: 项目类型统计DTO
   - `RegionRankingDto`: 地区排行DTO
   - `RecentAnnouncementDto`: 最新公告DTO
   - `AchievementDto`: 重要业绩DTO

2. **服务接口**: `Services/IHomeService.cs`
   - `GetStatisticsOverviewAsync()`: 获取统计概览
   - `GetRecentAnnouncementsAsync()`: 获取最新公告
   - `GetAchievementsAsync()`: 获取重要业绩

3. **服务实现**: `Services/HomeService.cs`
   - 实现所有统计逻辑
   - 使用EF Core进行数据查询和聚合

4. **控制器**: `Controllers/HomeController.cs`
   - `GET /api/home/statistics/overview`
   - `GET /api/home/recent-announcements`
   - `GET /api/home/achievements`

5. **依赖注入**: `Program.cs`
   - 已注册 `IHomeService` 和 `HomeService`

---

## 性能优化建议

1. **添加缓存**: 统计数据可以缓存5-10分钟
2. **数据库索引**: 确保以下字段有索引
   - `project_region`
   - `publish_time`
   - `is_deleted`
   - `sort_order`
   - `completion_date`

3. **分页支持**: 如果业绩列表数据量大，建议添加分页参数

---

## 注意事项

1. **金额字段缺失**: 公告表中没有金额字段，如需统计金额需要：
   - 方案1: 在公告表中添加 `project_amount` 字段
   - 方案2: 使用 `transaction_data` 表（如果有数据）
   - 方案3: 从公告内容中解析金额（不推荐）

2. **时间字段**: `publish_time` 可能为NULL，需要处理空值情况

3. **软删除**: 所有查询都需要过滤 `is_deleted` 字段

4. **区域统计**: 如果需要按省内/省外分类，需要关联 `region_dictionary` 表