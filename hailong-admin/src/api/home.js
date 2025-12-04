import request from './request'

/**
 * 获取首页统计概览
 */
export const getStatisticsOverview = () => {
  return request({
    url: '/api/home/statistics/overview',
    method: 'get'
  })
}

/**
 * 获取最新公告列表
 */
export const getRecentAnnouncements = () => {
  return request({
    url: '/api/home/recent-announcements',
    method: 'get'
  })
}

/**
 * 获取重要业绩列表
 */
export const getAchievements = () => {
  return request({
    url: '/api/home/achievements',
    method: 'get'
  })
}
