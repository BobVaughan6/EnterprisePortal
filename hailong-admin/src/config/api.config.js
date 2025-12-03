/**
 * API 基础配置
 */
export const API_CONFIG = {
  // 基础URL - 从环境变量获取
  baseURL: import.meta.env.VITE_API_BASE_URL || 'http://localhost:5000',
  
  // 请求超时时间
  timeout: 30000,
  
  // Token存储key
  TOKEN_KEY: 'hailong_admin_token',
  REFRESH_TOKEN_KEY: 'hailong_admin_refresh_token',
  USER_INFO_KEY: 'hailong_admin_user_info'
}

export default API_CONFIG
