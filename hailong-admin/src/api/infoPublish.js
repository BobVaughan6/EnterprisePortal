import request from './request'

/**
 * 信息发布 API
 */
export const infoPublishApi = {
  /**
   * 创建信息
   * @param {FormData} formData - 包含文件的表单数据
   */
  create(formData) {
    return request({
      url: '/api/info-publish',
      method: 'POST',
      data: formData,
      headers: {
        'Content-Type': 'multipart/form-data'
      }
    })
  },

  /**
   * 分页查询信息
   * @param {Object} params - { category, keyword, pageIndex, pageSize }
   */
  getPagedList(params) {
    return request({
      url: '/api/info-publish',
      method: 'GET',
      params
    })
  },

  /**
   * 获取信息详情
   * @param {number} id 
   * @param {string} category 
   */
  getById(id, category) {
    return request({
      url: `/api/info-publish/${id}`,
      method: 'GET',
      params: { category }
    })
  },

  /**
   * 更新信息
   * @param {number} id 
   * @param {string} category 
   * @param {FormData} formData 
   */
  update(id, category, formData) {
    return request({
      url: `/api/info-publish/${id}`,
      method: 'PUT',
      params: { category },
      data: formData,
      headers: {
        'Content-Type': 'multipart/form-data'
      }
    })
  },

  /**
   * 删除信息
   * @param {number} id 
   * @param {string} category 
   */
  delete(id, category) {
    return request({
      url: `/api/info-publish/${id}`,
      method: 'DELETE',
      params: { category }
    })
  }
}

export default infoPublishApi
