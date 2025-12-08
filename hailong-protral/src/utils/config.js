// 配置文件加载工具
import siteConfig from '../../config/site-config.json'

/**
 * 获取站点配置
 * @returns {Object} 站点配置对象
 */
export const getSiteConfig = () => {
  return siteConfig
}

/**
 * 获取公司信息
 * @returns {Object} 公司信息
 */
export const getCompanyInfo = () => {
  return siteConfig.company
}

/**
 * 获取联系方式
 * @returns {Object} 联系方式
 */
export const getContactInfo = () => {
  return siteConfig.contact
}

/**
 * 获取交通指引
 * @returns {Object} 交通指引
 */
export const getTransportation = () => {
  return siteConfig.transportation
}

/**
 * 获取业务范围
 * @returns {Array} 业务范围列表
 */
export const getBusinessScope = () => {
  return siteConfig.businessScope
}

/**
 * 获取导航配置
 * @returns {Object} 导航配置
 */
export const getNavigation = () => {
  return siteConfig.navigation
}

/**
 * 获取常见问题
 * @returns {Array} FAQ列表
 */
export const getFAQs = () => {
  return siteConfig.faqs
}

/**
 * 获取SEO配置
 * @returns {Object} SEO配置
 */
export const getSEOConfig = () => {
  return siteConfig.seo
}

/**
 * 获取主题配置
 * @returns {Object} 主题配置
 */
export const getThemeConfig = () => {
  return siteConfig.theme
}

/**
 * 获取API配置
 * @returns {Object} API配置
 */
export const getAPIConfig = () => {
  const env = import.meta.env.MODE
  return {
    baseUrl: siteConfig.api.baseUrl[env] || siteConfig.api.baseUrl.development,
    timeout: siteConfig.api.timeout,
    retryAttempts: siteConfig.api.retryAttempts
  }
}

/**
 * 获取功能开关
 * @returns {Object} 功能开关配置
 */
export const getFeatures = () => {
  return siteConfig.features
}

/**
 * 获取上传配置
 * @returns {Object} 上传配置
 */
export const getUploadConfig = () => {
  return siteConfig.upload
}

/**
 * 获取分页配置
 * @returns {Object} 分页配置
 */
export const getPaginationConfig = () => {
  return siteConfig.pagination
}

/**
 * 获取缓存配置
 * @returns {Object} 缓存配置
 */
export const getCacheConfig = () => {
  return siteConfig.cache
}

// 导出默认配置
export default siteConfig