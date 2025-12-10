import request from './request'

/**
 * 系统配置 API
 * 包含：企业简介、重要业绩、企业荣誉、轮播图、友情链接
 * 注意：业务范围和企业资质的后端API尚未实现
 */

// ==================== 企业简介 ====================
export const companyProfile = {
  get: () => request({
    url: '/api/config/company-intro',
    method: 'get'
  }),
  update: (data) => request({
    url: '/api/config/company-intro',
    method: 'put',
    data
  })
}

// ==================== 业务范围 ====================
// 注意：后端API尚未实现，暂时返回空数据
export const businessScope = {
  getList: (params) => {
    console.warn('业务范围API尚未在后端实现')
    return Promise.resolve({ success: true, data: [], message: '业务范围功能开发中' })
  },
  getDetail: (id) => {
    console.warn('业务范围API尚未在后端实现')
    return Promise.resolve({ success: false, message: '业务范围功能开发中' })
  },
  create: (data) => {
    console.warn('业务范围API尚未在后端实现')
    return Promise.resolve({ success: false, message: '业务范围功能开发中' })
  },
  update: (id, data) => {
    console.warn('业务范围API尚未在后端实现')
    return Promise.resolve({ success: false, message: '业务范围功能开发中' })
  },
  delete: (id) => {
    console.warn('业务范围API尚未在后端实现')
    return Promise.resolve({ success: false, message: '业务范围功能开发中' })
  },
  updateSort: (id, direction) => {
    console.warn('业务范围API尚未在后端实现')
    return Promise.resolve({ success: false, message: '业务范围功能开发中' })
  }
}

// ==================== 企业资质 ====================
// 注意：后端API尚未实现，暂时返回空数据
export const qualifications = {
  getList: (params) => {
    console.warn('企业资质API尚未在后端实现')
    return Promise.resolve({ success: true, data: { items: [], totalCount: 0 }, message: '企业资质功能开发中' })
  },
  getDetail: (id) => {
    console.warn('企业资质API尚未在后端实现')
    return Promise.resolve({ success: false, message: '企业资质功能开发中' })
  },
  create: (data) => {
    console.warn('企业资质API尚未在后端实现')
    return Promise.resolve({ success: false, message: '企业资质功能开发中' })
  },
  update: (id, data) => {
    console.warn('企业资质API尚未在后端实现')
    return Promise.resolve({ success: false, message: '企业资质功能开发中' })
  },
  delete: (id) => {
    console.warn('企业资质API尚未在后端实现')
    return Promise.resolve({ success: false, message: '企业资质功能开发中' })
  }
}

// ==================== 重要业绩 ====================
export const achievements = {
  getList: (params) => request({
    url: '/api/config/achievements',
    method: 'get',
    params
  }),
  getDetail: (id) => request({
    url: `/api/config/achievements/${id}`,
    method: 'get'
  }),
  create: (data) => request({
    url: '/api/config/achievements',
    method: 'post',
    data
  }),
  update: (id, data) => request({
    url: `/api/config/achievements/${id}`,
    method: 'put',
    data
  }),
  delete: (id) => request({
    url: `/api/config/achievements/${id}`,
    method: 'delete'
  }),
  updateSort: (id, direction) => request({
    url: `/api/config/achievements/${id}/sort`,
    method: 'put',
    data: { direction }
  })
}

// ==================== 企业荣誉 ====================
export const honors = {
  getList: (params) => request({
    url: '/api/config/honors',
    method: 'get',
    params
  }),
  getDetail: (id) => request({
    url: `/api/config/honors/${id}`,
    method: 'get'
  }),
  create: (data) => request({
    url: '/api/config/honors',
    method: 'post',
    data
  }),
  update: (id, data) => request({
    url: `/api/config/honors/${id}`,
    method: 'put',
    data
  }),
  delete: (id) => request({
    url: `/api/config/honors/${id}`,
    method: 'delete'
  })
}

// ==================== 轮播图 ====================
export const banners = {
  getList: (params) => request({
    url: '/api/config/banners',
    method: 'get',
    params
  }),
  getDetail: (id) => request({
    url: `/api/config/banners/${id}`,
    method: 'get'
  }),
  create: (data) => request({
    url: '/api/config/banners',
    method: 'post',
    data
  }),
  update: (id, data) => request({
    url: `/api/config/banners/${id}`,
    method: 'put',
    data
  }),
  delete: (id) => request({
    url: `/api/config/banners/${id}`,
    method: 'delete'
  }),
  updateSort: (id, direction) => request({
    url: `/api/config/banners/${id}/sort`,
    method: 'put',
    data: { direction }
  })
}

// ==================== 友情链接 ====================
export const friendlyLinks = {
  getList: (params) => request({
    url: '/api/config/links',
    method: 'get',
    params
  }),
  getDetail: (id) => request({
    url: `/api/config/links/${id}`,
    method: 'get'
  }),
  create: (data) => request({
    url: '/api/config/links',
    method: 'post',
    data
  }),
  update: (id, data) => request({
    url: `/api/config/links/${id}`,
    method: 'put',
    data
  }),
  delete: (id) => request({
    url: `/api/config/links/${id}`,
    method: 'delete'
  }),
  updateSort: (id, direction) => request({
    url: `/api/config/links/${id}/sort`,
    method: 'put',
    data: { direction }
  })
}

export default {
  companyProfile,
  businessScope,
  qualifications,
  achievements,
  honors,
  banners,
  friendlyLinks
}