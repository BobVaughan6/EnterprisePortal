import request from './request'

/**
 * 政府采购公告 API
 */
export const govProcurementApi = {
  /**
   * 获取公告列表（分页）
   */
  getAnnouncements(params) {
    return request({
      url: '/api/gov-procurement/announcements',
      method: 'GET',
      params
    })
  },

  /**
   * 根据ID获取公告详情
   */
  getAnnouncement(id) {
    return request({
      url: `/api/gov-procurement/announcements/${id}`,
      method: 'GET'
    })
  },

  /**
   * 创建公告
   */
  createAnnouncement(data) {
    return request({
      url: '/api/gov-procurement/announcements',
      method: 'POST',
      data
    })
  },

  /**
   * 更新公告
   */
  updateAnnouncement(id, data) {
    return request({
      url: `/api/gov-procurement/announcements/${id}`,
      method: 'PUT',
      data
    })
  },

  /**
   * 删除公告
   */
  deleteAnnouncement(id) {
    return request({
      url: `/api/gov-procurement/announcements/${id}`,
      method: 'DELETE'
    })
  }
}

export default govProcurementApi
