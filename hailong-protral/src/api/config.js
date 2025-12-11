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
 * 获取公司简介
 * @returns {Promise}
 */
export function getCompanyProfile() {
  return request({
    url: '/api/config/company-intro',
    method: 'get'
  })
}

/**
 * 获取业务范围
 * @returns {Promise}
 */
export function getBusinessScope() {
  return request({
    url: '/api/config/business-scope',
    method: 'get'
  })
}

/**
 * 获取企业资质
 * @returns {Promise}
 */
export function getCompanyQualifications() {
  return request({
    url: '/api/config/qualifications',
    method: 'get'
  })
}

/**
 * 获取企业荣誉
 * @returns {Promise}
 */
export function getCompanyHonors() {
  return request({
    url: '/api/config/honors',
    method: 'get'
  })
}

/**
 * 获取重要业绩
 * @returns {Promise}
 */
export function getMajorAchievements() {
  return request({
    url: '/api/config/achievements',
    method: 'get'
  })
}