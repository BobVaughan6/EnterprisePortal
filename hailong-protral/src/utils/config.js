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

// 导出默认配置
export default siteConfig