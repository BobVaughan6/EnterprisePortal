import request from './request'

/**
 * 建设工程公告 API
 */
export const constructionApi = {
  /**
   * 获取公告列表（分页）
   */
  getAnnouncements(params) {
    return request({
      url: '/api/construction/announcements',
      method: 'GET',
      params
    })
  },

  /**
   * 根据ID获取公告详情
   */
  getAnnouncement(id) {
    return request({
      url: `/api/construction/announcements/${id}`,
      method: 'GET'
    })
  },

  /**
   * 创建公告
   */
  createAnnouncement(data) {
    return request({
      url: '/api/construction/announcements',
      method: 'POST',
      data
    })
  },

  /**
   * 更新公告
   */
  updateAnnouncement(id, data) {
    return request({
      url: `/api/construction/announcements/${id}`,
      method: 'PUT',
      data
    })
  },

  /**
   * 删除公告
   */
  deleteAnnouncement(id) {
    return request({
      url: `/api/construction/announcements/${id}`,
      method: 'DELETE'
    })
  }
}

export default constructionApi
