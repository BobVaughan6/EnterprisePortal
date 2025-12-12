/**
 * API统一导出文件
 */

// 导出所有API模块
export * from './home'
export * from './config'
export * from './announcement'

// 导出request实例，供其他地方使用
export { default as request } from './request'

// 默认导出所有API
export default {
  ...require('./home'),
  ...require('./config'),
  ...require('./announcement')
}