import request from './request'

/**
 * 系统管理 API
 * 包含：用户管理、系统日志、区域字典
 */

// ==================== 用户管理 ====================
/**
 * 获取用户列表
 * @param {Object} params - 查询参数
 * @param {number} params.page - 页码
 * @param {number} params.pageSize - 每页数量
 * @param {string} params.keyword - 搜索关键词
 * @param {string} params.role - 角色
 * @param {number} params.status - 状态
 */
export const getUserList = (params) => {
  return request({
    url: '/api/system/users',
    method: 'get',
    params
  })
}

/**
 * 获取用户详情
 * @param {number} id - 用户ID
 */
export const getUserDetail = (id) => {
  return request({
    url: `/api/system/users/${id}`,
    method: 'get'
  })
}

/**
 * 创建用户
 * @param {Object} data - 用户数据
 * @param {string} data.username - 用户名
 * @param {string} data.password - 密码
 * @param {string} data.email - 邮箱
 * @param {string} data.phone - 手机号
 * @param {string} data.realName - 真实姓名
 * @param {string} data.role - 角色
 * @param {number} data.status - 状态
 */
export const createUser = (data) => {
  return request({
    url: '/api/system/users',
    method: 'post',
    data
  })
}

/**
 * 更新用户
 * @param {number} id - 用户ID
 * @param {Object} data - 用户数据
 */
export const updateUser = (id, data) => {
  return request({
    url: `/api/system/users/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除用户
 * @param {number} id - 用户ID
 */
export const deleteUser = (id) => {
  return request({
    url: `/api/system/users/${id}`,
    method: 'delete'
  })
}

/**
 * 重置用户密码
 * @param {number} id - 用户ID
 * @param {string} newPassword - 新密码
 */
export const resetUserPassword = (id, newPassword) => {
  return request({
    url: `/api/system/users/${id}/reset-password`,
    method: 'put',
    data: { newPassword }
  })
}

/**
 * 修改当前用户密码
 * @param {Object} data - 密码数据
 * @param {string} data.oldPassword - 旧密码
 * @param {string} data.newPassword - 新密码
 */
export const changePassword = (data) => {
  return request({
    url: '/api/system/users/change-password',
    method: 'put',
    data
  })
}

/**
 * 启用/禁用用户
 * @param {number} id - 用户ID
 * @param {number} status - 状态：0-禁用，1-启用
 */
export const toggleUserStatus = (id, status) => {
  return request({
    url: `/api/system/users/${id}/status`,
    method: 'put',
    data: { status }
  })
}

// ==================== 系统日志 ====================
/**
 * 获取系统日志列表
 * @param {Object} params - 查询参数
 * @param {number} params.page - 页码
 * @param {number} params.pageSize - 每页数量
 * @param {string} params.username - 用户名
 * @param {string} params.action - 操作动作
 * @param {string} params.module - 操作模块
 * @param {string} params.status - 操作状态
 * @param {string} params.startDate - 开始日期
 * @param {string} params.endDate - 结束日期
 */
export const getSystemLogList = (params) => {
  return request({
    url: '/api/system/logs',
    method: 'get',
    params
  })
}

/**
 * 获取系统日志详情
 * @param {number} id - 日志ID
 */
export const getSystemLogDetail = (id) => {
  return request({
    url: `/api/system/logs/${id}`,
    method: 'get'
  })
}

/**
 * 清理系统日志
 * @param {Object} data - 清理参数
 * @param {string} data.beforeDate - 清理此日期之前的日志
 */
export const clearSystemLogs = (data) => {
  return request({
    url: '/api/system/logs/clear',
    method: 'post',
    data
  })
}

/**
 * 导出系统日志
 * @param {Object} params - 查询参数
 */
export const exportSystemLogs = (params) => {
  return request({
    url: '/api/system/logs/export',
    method: 'get',
    params,
    responseType: 'blob'
  })
}

// ==================== 区域字典 ====================
/**
 * 获取区域字典列表
 * @param {Object} params - 查询参数
 * @param {number} params.regionLevel - 区域级别：1-省份，2-城市，3-区县
 * @param {string} params.parentCode - 父级区域代码
 * @param {string} params.keyword - 搜索关键词
 */
export const getRegionList = (params) => {
  return request({
    url: '/api/system/regions',
    method: 'get',
    params
  })
}

/**
 * 获取省份列表
 */
export const getProvinceList = () => {
  return request({
    url: '/api/system/regions/provinces',
    method: 'get'
  })
}

/**
 * 获取城市列表
 * @param {string} provinceCode - 省份代码
 */
export const getCityList = (provinceCode) => {
  return request({
    url: '/api/system/regions/cities',
    method: 'get',
    params: { provinceCode }
  })
}

/**
 * 获取区县列表
 * @param {string} cityCode - 城市代码
 */
export const getDistrictList = (cityCode) => {
  return request({
    url: '/api/system/regions/districts',
    method: 'get',
    params: { cityCode }
  })
}

/**
 * 获取区域详情
 * @param {number} id - 区域ID
 */
export const getRegionDetail = (id) => {
  return request({
    url: `/api/system/regions/${id}`,
    method: 'get'
  })
}

/**
 * 创建区域
 * @param {Object} data - 区域数据
 * @param {string} data.regionCode - 区域代码
 * @param {string} data.regionName - 区域名称
 * @param {number} data.regionLevel - 区域级别
 * @param {string} data.parentCode - 父级区域代码
 * @param {number} data.sortOrder - 排序顺序
 */
export const createRegion = (data) => {
  return request({
    url: '/api/system/regions',
    method: 'post',
    data
  })
}

/**
 * 更新区域
 * @param {number} id - 区域ID
 * @param {Object} data - 区域数据
 */
export const updateRegion = (id, data) => {
  return request({
    url: `/api/system/regions/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除区域
 * @param {number} id - 区域ID
 */
export const deleteRegion = (id) => {
  return request({
    url: `/api/system/regions/${id}`,
    method: 'delete'
  })
}

/**
 * 批量导入区域数据
 * @param {FormData} formData - 文件数据
 */
export const importRegions = (formData) => {
  return request({
    url: '/api/system/regions/import',
    method: 'post',
    data: formData,
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

/**
 * 导出区域数据
 */
export const exportRegions = () => {
  return request({
    url: '/api/system/regions/export',
    method: 'get',
    responseType: 'blob'
  })
}

export default {
  // 用户管理
  getUserList,
  getUserDetail,
  createUser,
  updateUser,
  deleteUser,
  resetUserPassword,
  changePassword,
  toggleUserStatus,
  // 系统日志
  getSystemLogList,
  getSystemLogDetail,
  clearSystemLogs,
  exportSystemLogs,
  // 区域字典
  getRegionList,
  getProvinceList,
  getCityList,
  getDistrictList,
  getRegionDetail,
  createRegion,
  updateRegion,
  deleteRegion,
  importRegions,
  exportRegions
}