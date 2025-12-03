# 海隆咨询官网数据库ER关系图说明

## 1. 概述

本文档描述海隆咨询官网数据库的实体关系（Entity-Relationship）设计，包括各个实体、属性和关系的详细说明。

---

## 2. 数据库架构总览

数据库共包含 **18个数据表** 和 **3个视图**，分为以下6个功能模块：

1. **用户权限管理模块** (2张表)
2. **公告信息模块** (2张表)
3. **信息发布模块** (4张表)
4. **系统配置模块** (6张表)
5. **统计模块** (2张表)
6. **辅助模块** (2张表)

---

## 3. 核心实体关系图

### 3.1 用户权限管理模块

```
┌─────────────────────┐
│   admin_roles       │
│  (角色权限表)        │
├─────────────────────┤
│ PK: id              │
│     role_name       │
│     role_code (UK)  │
│     permissions     │
│     status          │
└─────────────────────┘
          │
          │ 1:N
          │
          ▼
┌─────────────────────┐
│   admin_users       │
│  (管理员账号表)      │
├─────────────────────┤
│ PK: id              │
│     username (UK)   │
│     password        │
│     real_name       │
│ FK: role_id         │
│     status          │
│     last_login_time │
└─────────────────────┘
```

**关系说明**:
- 一个角色可以分配给多个管理员（1:N）
- 外键约束：`admin_users.role_id` → `admin_roles.id`
- 删除策略：角色删除时，管理员的role_id设为NULL
- 更新策略：角色ID更新时，级联更新

---

### 3.2 公告信息模块

```
┌──────────────────────────────┐
│ government_procurement_notices│
│    (政府采购公告表)            │
├──────────────────────────────┤
│ PK: id                       │
│     title                    │
│     notice_type              │
│     bidder                   │
│     winner                   │
│     project_region           │
│     content (富文本)          │
│     publisher                │
│     publish_time             │
│     view_count               │
└──────────────────────────────┘

┌──────────────────────────────┐
│ construction_project_notices │
│    (建设工程公告表)            │
├──────────────────────────────┤
│ PK: id                       │
│     title                    │
│     notice_type              │
│     bidder                   │
│     winner                   │
│     project_region           │
│     content (富文本)          │
│     publisher                │
│     publish_time             │
│     view_count               │
└──────────────────────────────┘
```

**关系说明**:
- 两个表结构相同，分别存储不同类型的公告
- 通过 `project_region` 字段关联区域字典表
- 支持全文索引搜索（title, content）

---

### 3.3 信息发布模块

```
┌─────────────────────┐
│ company_announcements│
│   (公司公告表)       │
├─────────────────────┤
│ PK: id              │
│     title           │
│     content         │
│     publish_time    │
│     view_count      │
└─────────────────────┘

┌─────────────────────┐
│ policy_regulations  │
│   (政策法规表)       │
├─────────────────────┤
│ PK: id              │
│     title           │
│     content         │
│     publish_time    │
│     view_count      │
└─────────────────────┘

┌─────────────────────┐
│ policy_information  │
│   (政策信息表)       │
├─────────────────────┤
│ PK: id              │
│     title           │
│     content         │
│     publish_time    │
│     view_count      │
└─────────────────────┘

┌─────────────────────┐
│ notice_announcements│
│   (通知公告表)       │
├─────────────────────┤
│ PK: id              │
│     title           │
│     content         │
│     publish_time    │
│     view_count      │
└─────────────────────┘
```

**关系说明**:
- 四个表结构相似，统一字段设计
- 独立存储不同类型的信息内容
- 支持富文本内容和附件

---

### 3.4 系统配置模块

```
┌─────────────────────┐
│ carousel_banners    │
│   (轮播图表)         │
├─────────────────────┤
│ PK: id              │
│     title           │
│     image_url       │
│     link_url        │
│     sort_order      │
└─────────────────────┘

┌─────────────────────┐
│ company_profile     │
│   (企业简介表)       │
├─────────────────────┤
│ PK: id              │
│     title           │
│     content         │
│     image_url       │
└─────────────────────┘

┌─────────────────────┐
│ business_scope      │
│   (业务范围表)       │
├─────────────────────┤
│ PK: id              │
│     title           │
│     description     │
│     icon_url        │
│     sort_order      │
└─────────────────────┘

┌─────────────────────┐
│ company_honors      │
│   (企业荣誉表)       │
├─────────────────────┤
│ PK: id              │
│     title           │
│     description     │
│     image_url       │
│     award_date      │
│     sort_order      │
└─────────────────────┘

┌─────────────────────┐
│ major_achievements  │
│   (重要业绩表)       │
├─────────────────────┤
│ PK: id              │
│     project_name    │
│     project_type    │
│     project_amount  │
│     client_name     │
│     completion_date │
│     sort_order      │
└─────────────────────┘

┌─────────────────────┐
│ friendly_links      │
│   (友情链接表)       │
├─────────────────────┤
│ PK: id              │
│     link_name       │
│     link_url        │
│     logo_url        │
│     sort_order      │
└─────────────────────┘
```

**关系说明**:
- 各表独立存储不同的配置信息
- 通过 `sort_order` 字段控制显示顺序
- 支持启用/禁用状态控制

---

### 3.5 统计模块

```
┌─────────────────────┐
│ visit_statistics    │
│   (访问统计表)       │
├─────────────────────┤
│ PK: id              │
│     visit_date      │
│     page_url        │
│     page_title      │
│     visitor_ip      │
│     user_agent      │
│     visit_count     │
└─────────────────────┘

┌─────────────────────┐
│ transaction_data    │
│   (交易数据表)       │
├─────────────────────┤
│ PK: id              │
│     project_name    │
│     project_type    │
│     project_amount  │
│     project_region  │
│     transaction_date│
│     client_name     │
└─────────────────────┘
          │
          │ 关联
          ▼
┌─────────────────────┐
│ region_dictionary   │
│  (区域字典表)        │
├─────────────────────┤
│ PK: id              │
│     region_code (UK)│
│     region_name     │
│     region_type     │
│     parent_id       │
└─────────────────────┘
```

**关系说明**:
- `transaction_data` 通过 `project_region` 关联区域字典
- 用于首页数据可视化展示
- 支持按日期、类型、区域等维度统计

---

### 3.6 辅助模块

```
┌─────────────────────┐
│ region_dictionary   │
│  (区域字典表)        │
├─────────────────────┤
│ PK: id              │
│     region_code (UK)│
│     region_name     │
│     region_type     │
│     parent_id       │
│     sort_order      │
└─────────────────────┘
          │
          │ 自关联 (树形结构)
          └──────┐
                 │
                 ▼
          ┌─────────────┐
          │  parent_id  │
          └─────────────┘

┌─────────────────────┐
│ system_logs         │
│  (系统日志表)        │
├─────────────────────┤
│ PK: id              │
│     user_id         │
│     username        │
│     action          │
│     module          │
│     ip_address      │
│     status          │
└─────────────────────┘
          │
          │ 关联
          ▼
┌─────────────────────┐
│   admin_users       │
│  (管理员账号表)      │
└─────────────────────┘
```

**关系说明**:
- `region_dictionary` 支持树形结构（省-市）
- `system_logs` 记录管理员操作日志
- 通过 `user_id` 关联管理员表

---

## 4. 视图关系

### 4.1 v_homepage_statistics (首页统计视图)

```
┌─────────────────────────────┐
│  v_homepage_statistics      │
│    (首页统计数据视图)        │
├─────────────────────────────┤
│  total_projects             │
│  total_amount               │
│  total_regions              │
│  gov_procurement_count      │
│  construction_count         │
└─────────────────────────────┘
          │
          │ 聚合数据来源
          ▼
┌──────────────────────────────┐
│ transaction_data             │
│ government_procurement_notices│
│ construction_project_notices │
└──────────────────────────────┘
```

---

### 4.2 v_transaction_type_statistics (交易类型统计视图)

```
┌─────────────────────────────┐
│ v_transaction_type_statistics│
│   (交易类型占比统计视图)      │
├─────────────────────────────┤
│  project_type               │
│  project_count              │
│  total_amount               │
│  percentage                 │
└─────────────────────────────┘
          │
          │ 聚合数据来源
          ▼
┌─────────────────────┐
│ transaction_data    │
└─────────────────────┘
```

---

### 4.3 v_region_ranking_statistics (地区排行统计视图)

```
┌─────────────────────────────┐
│ v_region_ranking_statistics │
│    (地区排行统计视图)         │
├─────────────────────────────┤
│  project_region             │
│  project_count              │
│  total_amount               │
└─────────────────────────────┘
          │
          │ 聚合数据来源
          ▼
┌─────────────────────┐
│ transaction_data    │
└─────────────────────┘
```

---

## 5. 完整ER关系图（文本版）

```
                    ┌─────────────────────┐
                    │   admin_roles       │
                    │  (角色权限表)        │
                    └──────────┬──────────┘
                               │ 1:N
                               ▼
                    ┌─────────────────────┐
                    │   admin_users       │
                    │  (管理员账号表)      │
                    └──────────┬──────────┘
                               │
                               │ 记录操作
                               ▼
                    ┌─────────────────────┐
                    │   system_logs       │
                    │  (系统日志表)        │
                    └─────────────────────┘

┌──────────────────────────────┐     ┌──────────────────────────────┐
│government_procurement_notices│     │construction_project_notices  │
│    (政府采购公告表)            │     │    (建设工程公告表)            │
└──────────────┬───────────────┘     └──────────────┬───────────────┘
               │                                    │
               │ project_region                     │ project_region
               │                                    │
               └────────────────┬───────────────────┘
                                │
                                ▼
                    ┌─────────────────────┐
                    │ region_dictionary   │
                    │  (区域字典表)        │
                    └──────────┬──────────┘
                               │
                               │ 自关联
                               └──────────┐
                                          │
                    ┌─────────────────────▼──┐
                    │ transaction_data       │
                    │  (交易数据表)           │
                    └──────────┬─────────────┘
                               │
                               │ 聚合统计
                               ▼
        ┌──────────────────────┴──────────────────────┐
        │                                              │
        ▼                                              ▼
┌──────────────────────┐                  ┌──────────────────────┐
│v_homepage_statistics │                  │v_transaction_type_   │
│  (首页统计视图)       │                  │  statistics          │
└──────────────────────┘                  └──────────────────────┘
                                                      │
                                                      ▼
                                          ┌──────────────────────┐
                                          │v_region_ranking_     │
                                          │  statistics          │
                                          └──────────────────────┘

信息发布模块（独立表）:
┌─────────────────────┐  ┌─────────────────────┐
│company_announcements│  │policy_regulations   │
└─────────────────────┘  └─────────────────────┘
┌─────────────────────┐  ┌─────────────────────┐
│policy_information   │  │notice_announcements │
└─────────────────────┘  └─────────────────────┘

系统配置模块（独立表）:
┌─────────────────────┐  ┌─────────────────────┐
│carousel_banners     │  │company_profile      │
└─────────────────────┘  └─────────────────────┘
┌─────────────────────┐  ┌─────────────────────┐
│business_scope       │  │company_honors       │
└─────────────────────┘  └─────────────────────┘
┌─────────────────────┐  ┌─────────────────────┐
│major_achievements   │  │friendly_links       │
└─────────────────────┘  └─────────────────────┘

统计模块:
┌─────────────────────┐
│visit_statistics     │
│  (访问统计表)        │
└─────────────────────┘
```

---

## 6. 关系类型说明

### 6.1 一对多关系 (1:N)

| 主表 | 从表 | 关系说明 |
|------|------|---------|
| admin_roles | admin_users | 一个角色可分配给多个管理员 |
| admin_users | system_logs | 一个管理员可产生多条操作日志 |
| region_dictionary | region_dictionary | 父级区域包含多个子级区域（自关联） |

### 6.2 逻辑关联关系

| 表A | 表B | 关联字段 | 说明 |
|-----|-----|---------|------|
| government_procurement_notices | region_dictionary | project_region | 公告关联项目区域 |
| construction_project_notices | region_dictionary | project_region | 公告关联项目区域 |
| transaction_data | region_dictionary | project_region | 交易数据关联项目区域 |

**注意**: 这些是逻辑关联，未设置外键约束，以保持数据灵活性。

---

## 7. 数据流向图

### 7.1 用户登录流程

```
用户输入 → admin_users (验证) → admin_roles (权限检查) → system_logs (记录登录)
```

### 7.2 公告发布流程

```
管理员发布 → government_procurement_notices/construction_project_notices
           ↓
    关联 region_dictionary (项目区域)
           ↓
    记录到 system_logs (操作日志)
```

### 7.3 首页统计数据流程

```
transaction_data (原始数据)
    ↓
聚合计算
    ↓
v_homepage_statistics (统计视图)
    ↓
前端展示
```

---

## 8. 索引设计说明

### 8.1 主键索引
- 所有表的 `id` 字段
- 自增整数，保证唯一性

### 8.2 唯一索引
- `admin_users.username`
- `admin_roles.role_code`
- `region_dictionary.region_code`

### 8.3 外键索引
- `admin_users.role_id`

### 8.4 查询优化索引
- 状态字段：`status`, `is_deleted`, `is_top`
- 时间字段：`publish_time`, `transaction_date`, `created_at`
- 分类字段：`notice_type`, `project_type`, `project_region`

### 8.5 全文索引
- `government_procurement_notices`: (title, content)
- `construction_project_notices`: (title, content)
- 信息发布模块各表: (title, content)

---

## 9. 数据完整性约束

### 9.1 实体完整性
- 所有表都有主键约束
- 主键自增，保证唯一性

### 9.2 参照完整性
- 外键约束：`admin_users.role_id` → `admin_roles.id`
- 删除策略：SET NULL
- 更新策略：CASCADE

### 9.3 域完整性
- NOT NULL 约束：关键字段不允许为空
- DEFAULT 约束：提供合理的默认值
- CHECK 约束：通过应用层实现枚举值验证

### 9.4 用户定义完整性
- 软删除机制：`is_deleted` 字段
- 状态控制：`status` 字段
- 排序控制：`sort_order` 字段

---

## 10. 扩展性设计

### 10.1 水平扩展
- 支持按日期分区（访问统计、系统日志）
- 支持读写分离（主从复制）

### 10.2 垂直扩展
- 富文本内容使用 LONGTEXT
- 支持大数据量存储

### 10.3 功能扩展
- 预留扩展字段空间
- 使用 JSON 存储灵活配置（如权限列表）
- 模块化设计，便于添加新功能

---

## 11. 安全性设计

### 11.1 数据安全
- 密码加密存储（SHA2）
- 软删除保留历史数据
- 操作日志完整记录

### 11.2 访问控制
- 基于角色的权限管理（RBAC）
- 管理员状态控制
- 操作审计追踪

---

## 12. 性能优化建议

### 12.1 查询优化
- 合理使用索引
- 避免全表扫描
- 使用视图简化复杂查询

### 12.2 存储优化
- 定期清理软删除数据
- 归档历史数据
- 优化表结构

### 12.3 缓存策略
- 缓存热点数据
- 缓存统计结果
- 使用 Redis 提升性能

---

## 13. 总结

本数据库设计具有以下特点：

1. **模块化设计**: 清晰的功能模块划分
2. **规范化设计**: 符合第三范式要求
3. **扩展性强**: 支持水平和垂直扩展
4. **安全可靠**: 完善的权限和日志机制
5. **性能优化**: 合理的索引和视图设计
6. **易于维护**: 统一的字段命名和结构设计

---

**文档版本**: 1.0  
**创建日期**: 2025-12-03  
**最后更新**: 2025-12-03