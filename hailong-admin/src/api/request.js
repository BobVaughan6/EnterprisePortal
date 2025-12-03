import axios from 'axios'
import { ElMessage } from 'element-plus'
import { API_CONFIG } from '@/config/api.config'
import { tokenUtils } from '@/utils/auth'
import router from '@/router'

/**
 * 创建 axios 实例
 */
const request = axios.create({
  baseURL: API_CONFIG.baseURL,
  timeout: API_CONFIG.timeout,
  headers: {
    'Content-Type': 'application/json'
  }
})

/**
 * 请求拦截器
 */
request.interceptors.request.use(
  config => {
    // 自动添加 Token
    const token = tokenUtils.getToken()
    if (token) {
      config.headers.Authorization = `Bearer ${token}`
    }
    return config
  },
  error => {
    console.error('请求错误:', error)
    return Promise.reject(error)
  }
)

/**
 * 响应拦截器
 */
request.interceptors.response.use(
  response => {
    const res = response.data
    
    // 后端返回的统一格式: { success, message, data }
    if (res.success === false) {
      ElMessage.error(res.message || '请求失败')
      return Promise.reject(new Error(res.message || '请求失败'))
    }
    
    return res
  },
  async error => {
    console.error('响应错误:', error)
    
    // 处理 401 未授权
    if (error.response?.status === 401) {
      ElMessage.error('登录已过期，请重新登录')
      tokenUtils.clearAuth()
      router.push('/login')
      return Promise.reject(error)
    }
    
    // 处理 403 禁止访问
    if (error.response?.status === 403) {
      ElMessage.error('没有权限访问该资源')
      return Promise.reject(error)
    }
    
    // 处理 404
    if (error.response?.status === 404) {
      ElMessage.error('请求的资源不存在')
      return Promise.reject(error)
    }
    
    // 处理 500
    if (error.response?.status === 500) {
      ElMessage.error('服务器错误，请稍后重试')
      return Promise.reject(error)
    }
    
    // 其他错误
    const message = error.response?.data?.message || error.message || '网络错误'
    ElMessage.error(message)
    return Promise.reject(error)
  }
)

export default request
