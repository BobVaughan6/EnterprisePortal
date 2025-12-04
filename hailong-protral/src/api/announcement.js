import axios from 'axios'

// 创建axios实例
const request = axios.create({
  baseURL: import.meta.env.VITE_API_BASE_URL || '/api',
  timeout: 10000
})

// 请求拦截器
request.interceptors.request.use(
  config => {
    // 可以在这里添加token等
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
 * 获取公告列表
 * @param {Object} params - 查询参数
 * @param {string} params.category - 公告类别 (GOV_PROCUREMENT: 政府采购, CONSTRUCTION: 建设工程)
 * @param {string} params.keyword - 关键词搜索
 * @param {string} params.type - 公告类型
 * @param {Array} params.regions - 项目区域数组
 * @param {string} params.startDate - 开始日期 (YYYY-MM-DD)
 * @param {string} params.endDate - 结束日期 (YYYY-MM-DD)
 * @param {number} params.page - 页码 (从1开始)
 * @param {number} params.pageSize - 每页数量
 * @returns {Promise}
 */
export function getAnnouncementList(params) {
  return request({
    url: '/announcements',
    method: 'get',
    params
  })
}

/**
 * 获取公告详情
 * @param {number} id - 公告ID
 * @returns {Promise}
 */
export function getAnnouncementDetail(id) {
  return request({
    url: `/announcements/${id}`,
    method: 'get'
  })
}

/**
 * 获取公告类型枚举
 * @param {string} category - 公告类别
 * @returns {Promise}
 */
export function getAnnouncementTypes(category) {
  return request({
    url: '/announcements/types',
    method: 'get',
    params: { category }
  })
}

/**
 * 获取项目区域列表
 * @returns {Promise}
 */
export function getRegions() {
  return request({
    url: '/announcements/regions',
    method: 'get'
  })
}

/**
 * 增加公告访问量
 * @param {number} id - 公告ID
 * @returns {Promise}
 */
export function incrementViews(id) {
  return request({
    url: `/announcements/${id}/views`,
    method: 'post'
  })
}