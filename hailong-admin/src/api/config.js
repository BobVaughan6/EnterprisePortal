import request from './request'

/**
 * 系统配置相关 API
 */
export const configApi = {
  // ========== 轮播图管理 ==========
  /**
   * 获取所有轮播图
   */
  getBanners() {
    return request({
      url: '/api/config/banners',
      method: 'GET'
    })
  },

  /**
   * 根据ID获取轮播图
   */
  getBanner(id) {
    return request({
      url: `/api/config/banners/${id}`,
      method: 'GET'
    })
  },

  /**
   * 创建轮播图
   */
  createBanner(data) {
    return request({
      url: '/api/config/banners',
      method: 'POST',
      data
    })
  },

  /**
   * 更新轮播图
   */
  updateBanner(id, data) {
    return request({
      url: `/api/config/banners/${id}`,
      method: 'PUT',
      data
    })
  },

  /**
   * 删除轮播图
   */
  deleteBanner(id) {
    return request({
      url: `/api/config/banners/${id}`,
      method: 'DELETE'
    })
  },

  // ========== 企业简介管理 ==========
  /**
   * 获取企业简介
   */
  getCompanyProfile() {
    return request({
      url: '/api/config/company-profile',
      method: 'GET'
    })
  },

  /**
   * 更新企业简介
   */
  updateCompanyProfile(data) {
    return request({
      url: '/api/config/company-profile',
      method: 'PUT',
      data
    })
  },

  // ========== 重要业绩管理 ==========
  /**
   * 获取所有重要业绩
   */
  getAchievements() {
    return request({
      url: '/api/config/achievements',
      method: 'GET'
    })
  },

  /**
   * 根据ID获取重要业绩
   */
  getAchievement(id) {
    return request({
      url: `/api/config/achievements/${id}`,
      method: 'GET'
    })
  },

  /**
   * 创建重要业绩
   */
  createAchievement(data) {
    return request({
      url: '/api/config/achievements',
      method: 'POST',
      data
    })
  },

  /**
   * 更新重要业绩
   */
  updateAchievement(id, data) {
    return request({
      url: `/api/config/achievements/${id}`,
      method: 'PUT',
      data
    })
  },

  /**
   * 删除重要业绩
   */
  deleteAchievement(id) {
    return request({
      url: `/api/config/achievements/${id}`,
      method: 'DELETE'
    })
  },

  // ========== 友情链接管理 ==========
  /**
   * 获取所有友情链接
   */
  getFriendlyLinks() {
    return request({
      url: '/api/config/friendly-links',
      method: 'GET'
    })
  },

  /**
   * 根据ID获取友情链接
   */
  getFriendlyLink(id) {
    return request({
      url: `/api/config/friendly-links/${id}`,
      method: 'GET'
    })
  },

  /**
   * 创建友情链接
   */
  createFriendlyLink(data) {
    return request({
      url: '/api/config/friendly-links',
      method: 'POST',
      data
    })
  },

  /**
   * 更新友情链接
   */
  updateFriendlyLink(id, data) {
    return request({
      url: `/api/config/friendly-links/${id}`,
      method: 'PUT',
      data
    })
  },

  /**
   * 删除友情链接
   */
  deleteFriendlyLink(id) {
    return request({
      url: `/api/config/friendly-links/${id}`,
      method: 'DELETE'
    })
  }
}

export default configApi
