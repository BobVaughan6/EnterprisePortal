import axios from 'axios'

// 创建axios实例
const request = axios.create({
  baseURL: import.meta.env.VITE_API_BASE_URL || '/api',
  timeout: 10000
})

// 请求拦截器
request.interceptors.request.use(
  config => {
    return config
  },
  error => {
    return Promise.reject(error)
  }
)

// 响应拦截器
request.interceptors.response.use(
  response => {
    return response.data
  },
  error => {
    console.error('API请求错误:', error)
    return Promise.reject(error)
  }
)

/**
 * 获取首页统计概览
 * @returns {Promise}
 */
export function getStatisticsOverview() {
  return request({
    url: '/api/home/statistics/overview',
    method: 'get'
  })
}

/**
 * 获取最新公告列表
 * @returns {Promise}
 */
export function getRecentAnnouncements() {
  return request({
    url: '/api/home/recent-announcements',
    method: 'get'
  })
}