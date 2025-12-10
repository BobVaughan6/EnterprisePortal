import request from './request'

/**
 * 系统配置 API
 * 包含：企业简介、业务范围、企业资质、重要业绩、企业荣誉、轮播图、友情链接
 */

// ==================== 企业简介 ====================
/**
 * 获取企业简介列表
 */
export const getCompanyProfileList = (params) => {
  return request({
    url: '/api/config/company-profile',
    method: 'get',
    params
  })
}

/**
 * 获取企业简介详情
 */
export const getCompanyProfileDetail = (id) => {
  return request({
    url: `/api/config/company-profile/${id}`,
    method: 'get'
  })
}

/**
 * 创建/更新企业简介
 */
export const saveCompanyProfile = (data) => {
  return request({
    url: '/api/config/company-profile',
    method: 'post',
    data
  })
}

// ==================== 业务范围 ====================
/**
 * 获取业务范围列表
 */
export const getBusinessScopeList = (params) => {
  return request({
    url: '/api/config/business-scope',
    method: 'get',
    params
  })
}

/**
 * 获取业务范围详情
 */
export const getBusinessScopeDetail = (id) => {
  return request({
    url: `/api/config/business-scope/${id}`,
    method: 'get'
  })
}

/**
 * 创建业务范围
 */
export const createBusinessScope = (data) => {
  return request({
    url: '/api/config/business-scope',
    method: 'post',
    data
  })
}

/**
 * 更新业务范围
 */
export const updateBusinessScope = (id, data) => {
  return request({
    url: `/api/config/business-scope/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除业务范围
 */
export const deleteBusinessScope = (id) => {
  return request({
    url: `/api/config/business-scope/${id}`,
    method: 'delete'
  })
}

/**
 * 更新业务范围排序
 */
export const updateBusinessScopeSort = (data) => {
  return request({
    url: '/api/config/business-scope/sort',
    method: 'put',
    data
  })
}

// ==================== 企业资质 ====================
/**
 * 获取企业资质列表
 */
export const getQualificationList = (params) => {
  return request({
    url: '/api/config/qualifications',
    method: 'get',
    params
  })
}

/**
 * 获取企业资质详情
 */
export const getQualificationDetail = (id) => {
  return request({
    url: `/api/config/qualifications/${id}`,
    method: 'get'
  })
}

/**
 * 创建企业资质
 */
export const createQualification = (data) => {
  return request({
    url: '/api/config/qualifications',
    method: 'post',
    data
  })
}

/**
 * 更新企业资质
 */
export const updateQualification = (id, data) => {
  return request({
    url: `/api/config/qualifications/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除企业资质
 */
export const deleteQualification = (id) => {
  return request({
    url: `/api/config/qualifications/${id}`,
    method: 'delete'
  })
}

// ==================== 重要业绩 ====================
/**
 * 获取重要业绩列表
 */
export const getAchievementList = (params) => {
  return request({
    url: '/api/config/achievements',
    method: 'get',
    params
  })
}

/**
 * 获取重要业绩详情
 */
export const getAchievementDetail = (id) => {
  return request({
    url: `/api/config/achievements/${id}`,
    method: 'get'
  })
}

/**
 * 创建重要业绩
 */
export const createAchievement = (data) => {
  return request({
    url: '/api/config/achievements',
    method: 'post',
    data
  })
}

/**
 * 更新重要业绩
 */
export const updateAchievement = (id, data) => {
  return request({
    url: `/api/config/achievements/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除重要业绩
 */
export const deleteAchievement = (id) => {
  return request({
    url: `/api/config/achievements/${id}`,
    method: 'delete'
  })
}

// ==================== 企业荣誉 ====================
/**
 * 获取企业荣誉列表
 */
export const getHonorList = (params) => {
  return request({
    url: '/api/config/honors',
    method: 'get',
    params
  })
}

/**
 * 获取企业荣誉详情
 */
export const getHonorDetail = (id) => {
  return request({
    url: `/api/config/honors/${id}`,
    method: 'get'
  })
}

/**
 * 创建企业荣誉
 */
export const createHonor = (data) => {
  return request({
    url: '/api/config/honors',
    method: 'post',
    data
  })
}

/**
 * 更新企业荣誉
 */
export const updateHonor = (id, data) => {
  return request({
    url: `/api/config/honors/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除企业荣誉
 */
export const deleteHonor = (id) => {
  return request({
    url: `/api/config/honors/${id}`,
    method: 'delete'
  })
}

// ==================== 轮播图 ====================
/**
 * 获取轮播图列表
 */
export const getBannerList = (params) => {
  return request({
    url: '/api/config/banners',
    method: 'get',
    params
  })
}

/**
 * 获取轮播图详情
 */
export const getBannerDetail = (id) => {
  return request({
    url: `/api/config/banners/${id}`,
    method: 'get'
  })
}

/**
 * 创建轮播图
 */
export const createBanner = (data) => {
  return request({
    url: '/api/config/banners',
    method: 'post',
    data
  })
}

/**
 * 更新轮播图
 */
export const updateBanner = (id, data) => {
  return request({
    url: `/api/config/banners/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除轮播图
 */
export const deleteBanner = (id) => {
  return request({
    url: `/api/config/banners/${id}`,
    method: 'delete'
  })
}

/**
 * 更新轮播图排序
 */
export const updateBannerSort = (data) => {
  return request({
    url: '/api/config/banners/sort',
    method: 'put',
    data
  })
}

// ==================== 友情链接 ====================
/**
 * 获取友情链接列表
 */
export const getFriendlyLinkList = (params) => {
  return request({
    url: '/api/config/friendly-links',
    method: 'get',
    params
  })
}

/**
 * 获取友情链接详情
 */
export const getFriendlyLinkDetail = (id) => {
  return request({
    url: `/api/config/friendly-links/${id}`,
    method: 'get'
  })
}

/**
 * 创建友情链接
 */
export const createFriendlyLink = (data) => {
  return request({
    url: '/api/config/friendly-links',
    method: 'post',
    data
  })
}

/**
 * 更新友情链接
 */
export const updateFriendlyLink = (id, data) => {
  return request({
    url: `/api/config/friendly-links/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除友情链接
 */
export const deleteFriendlyLink = (id) => {
  return request({
    url: `/api/config/friendly-links/${id}`,
    method: 'delete'
  })
}

/**
 * 更新友情链接排序
 */
export const updateFriendlyLinkSort = (data) => {
  return request({
    url: '/api/config/friendly-links/sort',
    method: 'put',
    data
  })
}

export default {
  // 企业简介
  getCompanyProfileList,
  getCompanyProfileDetail,
  saveCompanyProfile,
  // 业务范围
  getBusinessScopeList,
  getBusinessScopeDetail,
  createBusinessScope,
  updateBusinessScope,
  deleteBusinessScope,
  updateBusinessScopeSort,
  // 企业资质
  getQualificationList,
  getQualificationDetail,
  createQualification,
  updateQualification,
  deleteQualification,
  // 重要业绩
  getAchievementList,
  getAchievementDetail,
  createAchievement,
  updateAchievement,
  deleteAchievement,
  // 企业荣誉
  getHonorList,
  getHonorDetail,
  createHonor,
  updateHonor,
  deleteHonor,
  // 轮播图
  getBannerList,
  getBannerDetail,
  createBanner,
  updateBanner,
  deleteBanner,
  updateBannerSort,
  // 友情链接
  getFriendlyLinkList,
  getFriendlyLinkDetail,
  createFriendlyLink,
  updateFriendlyLink,
  deleteFriendlyLink,
  updateFriendlyLinkSort
}