import request from './request'

/**
 * 统计分析 API
 * 包含：访问统计、公告统计等
 */

// ==================== 访问统计 ====================
/**
 * 获取访问统计列表
 * @param {Object} params - 查询参数
 * @param {number} params.page - 页码
 * @param {number} params.pageSize - 每页数量
 * @param {string} params.startDate - 开始日期
 * @param {string} params.endDate - 结束日期
 * @param {string} params.pageUrl - 页面URL
 */
export const getVisitStatisticsList = (params) => {
  return request({
    url: '/api/statistics/visits',
    method: 'get',
    params
  })
}

/**
 * 获取访问统计概览
 * @param {Object} params - 查询参数
 * @param {string} params.startDate - 开始日期
 * @param {string} params.endDate - 结束日期
 */
export const getVisitStatisticsOverview = (params) => {
  return request({
    url: '/api/statistics/visits/overview',
    method: 'get',
    params
  })
}

/**
 * 获取访问趋势数据
 * @param {Object} params - 查询参数
 * @param {string} params.startDate - 开始日期
 * @param {string} params.endDate - 结束日期
 * @param {string} params.groupBy - 分组方式：day/week/month
 */
export const getVisitTrend = (params) => {
  return request({
    url: '/api/statistics/visits/trend',
    method: 'get',
    params
  })
}

/**
 * 获取热门页面统计
 * @param {Object} params - 查询参数
 * @param {string} params.startDate - 开始日期
 * @param {string} params.endDate - 结束日期
 * @param {number} params.limit - 返回数量
 */
export const getPopularPages = (params) => {
  return request({
    url: '/api/statistics/visits/popular-pages',
    method: 'get',
    params
  })
}

/**
 * 记录访问统计
 * @param {Object} data - 访问数据
 * @param {string} data.pageUrl - 页面URL
 * @param {string} data.pageTitle - 页面标题
 * @param {string} data.referer - 来源页面
 */
export const recordVisit = (data) => {
  return request({
    url: '/api/statistics/visits/record',
    method: 'post',
    data
  })
}

// ==================== 公告统计 ====================
/**
 * 获取公告统计概览
 * @param {Object} params - 查询参数
 */
export const getAnnouncementStatisticsOverview = (params) => {
  return request({
    url: '/api/statistics/announcements/overview',
    method: 'get',
    params
  })
}

/**
 * 获取公告发布趋势
 * @param {Object} params - 查询参数
 * @param {string} params.startDate - 开始日期
 * @param {string} params.endDate - 结束日期
 * @param {string} params.businessType - 业务类型
 * @param {string} params.groupBy - 分组方式：day/week/month
 */
export const getAnnouncementPublishTrend = (params) => {
  return request({
    url: '/api/statistics/announcements/publish-trend',
    method: 'get',
    params
  })
}

/**
 * 获取公告类型分布
 * @param {Object} params - 查询参数
 */
export const getAnnouncementTypeDistribution = (params) => {
  return request({
    url: '/api/statistics/announcements/type-distribution',
    method: 'get',
    params
  })
}

/**
 * 获取公告区域分布
 * @param {Object} params - 查询参数
 * @param {string} params.businessType - 业务类型
 * @param {number} params.limit - 返回数量
 */
export const getAnnouncementRegionDistribution = (params) => {
  return request({
    url: '/api/statistics/announcements/region-distribution',
    method: 'get',
    params
  })
}

/**
 * 获取热门公告
 * @param {Object} params - 查询参数
 * @param {string} params.businessType - 业务类型
 * @param {number} params.limit - 返回数量
 */
export const getPopularAnnouncements = (params) => {
  return request({
    url: '/api/statistics/announcements/popular',
    method: 'get',
    params
  })
}

// ==================== 信息发布统计 ====================
/**
 * 获取信息发布统计概览
 */
export const getInfoPublicationStatisticsOverview = (params) => {
  return request({
    url: '/api/statistics/info-publications/overview',
    method: 'get',
    params
  })
}

/**
 * 获取信息发布趋势
 */
export const getInfoPublicationPublishTrend = (params) => {
  return request({
    url: '/api/statistics/info-publications/publish-trend',
    method: 'get',
    params
  })
}

/**
 * 获取信息类型分布
 */
export const getInfoPublicationTypeDistribution = (params) => {
  return request({
    url: '/api/statistics/info-publications/type-distribution',
    method: 'get',
    params
  })
}

// ==================== 首页统计 ====================
/**
 * 获取首页统计数据
 */
export const getHomeStatistics = () => {
  return request({
    url: '/api/statistics/home',
    method: 'get'
  })
}

export default {
  // 访问统计
  getVisitStatisticsList,
  getVisitStatisticsOverview,
  getVisitTrend,
  getPopularPages,
  recordVisit,
  // 公告统计
  getAnnouncementStatisticsOverview,
  getAnnouncementPublishTrend,
  getAnnouncementTypeDistribution,
  getAnnouncementRegionDistribution,
  getPopularAnnouncements,
  // 信息发布统计
  getInfoPublicationStatisticsOverview,
  getInfoPublicationPublishTrend,
  getInfoPublicationTypeDistribution,
  // 首页统计
  getHomeStatistics
}