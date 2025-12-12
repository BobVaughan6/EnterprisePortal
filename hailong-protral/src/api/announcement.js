import request from './request'

/**
 * 获取公告列表
 * @param {Object} params - 查询参数
 * @param {string} params.businessType - 业务类型 (GOV_PROCUREMENT: 政府采购, CONSTRUCTION: 建设工程)
 * @param {string} params.keyword - 关键词搜索
 * @param {string} params.noticeType - 公告类型
 * @param {Array} params.regions - 项目区域数组
 * @param {string} params.startDate - 开始日期 (YYYY-MM-DD)
 * @param {string} params.endDate - 结束日期 (YYYY-MM-DD)
 * @param {number} params.page - 页码 (从1开始)
 * @param {number} params.pageSize - 每页数量
 * @returns {Promise}
 */
export function getAnnouncementList(params) {
  return request({
    url: '/api/announcements',
    method: 'get',
    params
  })
}

/**
 * 获取公告详情
 * @param {number} id - 公告ID
 * @returns {Promise}
 */
export function getAnnouncementDetail(id) {
  return request({
    url: `/api/announcements/${id}`,
    method: 'get'
  })
}

/**
 * 获取政府采购公告列表
 * @param {Object} params - 查询参数
 * @returns {Promise}
 */
export function getGovProcurementList(params) {
  return request({
    url: '/api/announcements/gov-procurement',
    method: 'get',
    params
  })
}

/**
 * 获取建设工程公告列表
 * @param {Object} params - 查询参数
 * @returns {Promise}
 */
export function getConstructionList(params) {
  return request({
    url: '/api/announcements/construction',
    method: 'get',
    params
  })
}

/**
 * 增加公告访问量（已在后端getById接口中自动处理）
 * @param {number} id - 公告ID
 * @returns {Promise}
 */
export function incrementViews(id) {
  // 注意：访问量已在获取详情时自动增加，此接口保留用于兼容
  return Promise.resolve({ success: true })
}