/**
 * 日期时间格式化工具
 */

/**
 * 格式化日期时间
 * @param {string|Date} dateTime - 日期时间
 * @param {string} format - 格式，默认 'YYYY-MM-DD HH:mm:ss'
 * @returns {string} 格式化后的日期时间
 */
export function formatDateTime(dateTime, format = 'YYYY-MM-DD HH:mm:ss') {
  if (!dateTime) return '-'
  
  const date = new Date(dateTime)
  if (isNaN(date.getTime())) return '-'
  
  const year = date.getFullYear()
  const month = String(date.getMonth() + 1).padStart(2, '0')
  const day = String(date.getDate()).padStart(2, '0')
  const hours = String(date.getHours()).padStart(2, '0')
  const minutes = String(date.getMinutes()).padStart(2, '0')
  const seconds = String(date.getSeconds()).padStart(2, '0')
  
  return format
    .replace('YYYY', year)
    .replace('MM', month)
    .replace('DD', day)
    .replace('HH', hours)
    .replace('mm', minutes)
    .replace('ss', seconds)
}

/**
 * 格式化日期
 * @param {string|Date} date - 日期
 * @returns {string} 格式化后的日期
 */
export function formatDate(date) {
  return formatDateTime(date, 'YYYY-MM-DD')
}

/**
 * 格式化时间
 * @param {string|Date} time - 时间
 * @returns {string} 格式化后的时间
 */
export function formatTime(time) {
  return formatDateTime(time, 'HH:mm:ss')
}

/**
 * 获取相对时间描述
 * @param {string|Date} dateTime - 日期时间
 * @returns {string} 相对时间描述
 */
export function getRelativeTime(dateTime) {
  if (!dateTime) return '-'
  
  const date = new Date(dateTime)
  if (isNaN(date.getTime())) return '-'
  
  const now = new Date()
  const diff = now - date
  const seconds = Math.floor(diff / 1000)
  const minutes = Math.floor(seconds / 60)
  const hours = Math.floor(minutes / 60)
  const days = Math.floor(hours / 24)
  
  if (days > 7) {
    return formatDateTime(dateTime)
  } else if (days > 0) {
    return `${days}天前`
  } else if (hours > 0) {
    return `${hours}小时前`
  } else if (minutes > 0) {
    return `${minutes}分钟前`
  } else {
    return '刚刚'
  }
}
