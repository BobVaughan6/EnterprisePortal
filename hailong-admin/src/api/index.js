/**
 * API 统一导出
 * 根据 hailong_consulting_schema.sql 重新组织
 */

// 认证相关
export { default as authApi } from './auth'

// 附件管理
export { default as attachmentApi } from './attachment'

// 公告管理（政府采购 + 建设工程）
export { default as announcementApi } from './announcement'

// 信息发布（公司公告、政策法规、政策信息、通知公告）
export { default as infoPublicationApi } from './infoPublication'

// 系统配置（企业简介、业务范围、企业资质、重要业绩、企业荣誉、轮播图、友情链接）
export { default as systemConfigApi } from './systemConfig'

// 统计分析（访问统计、公告统计、信息发布统计）
export { default as statisticsApi } from './statistics'

// 系统管理（用户管理、系统日志、区域字典）
export { default as systemApi } from './system'

// 请求工具
export { default as request } from './request'
