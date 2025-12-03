import { defineStore } from 'pinia'
import { ref } from 'vue'
import { authApi } from '@/api'
import { tokenUtils } from '@/utils/auth'

/**
 * 用户状态管理
 */
export const useUserStore = defineStore('user', () => {
  // 状态
  const userInfo = ref(tokenUtils.getUserInfo())
  const token = ref(tokenUtils.getToken())

  /**
   * 登录
   */
  const login = async (loginForm) => {
    try {
      const res = await authApi.login(loginForm)
      
      if (res.success && res.data) {
        // 保存Token
        token.value = res.data.token
        tokenUtils.setToken(res.data.token)
        tokenUtils.setRefreshToken(res.data.refreshToken)
        
        // 保存用户信息
        const user = {
          userId: res.data.userId,
          username: res.data.username,
          fullName: res.data.fullName,
          email: res.data.email,
          role: res.data.role
        }
        userInfo.value = user
        tokenUtils.setUserInfo(user)
        
        return true
      }
      return false
    } catch (error) {
      console.error('登录失败:', error)
      return false
    }
  }

  /**
   * 获取当前用户信息
   */
  const getCurrentUser = async () => {
    try {
      const res = await authApi.getCurrentUser()
      if (res.success && res.data) {
        userInfo.value = res.data
        tokenUtils.setUserInfo(res.data)
        return true
      }
      return false
    } catch (error) {
      console.error('获取用户信息失败:', error)
      return false
    }
  }

  /**
   * 修改密码
   */
  const changePassword = async (passwordData) => {
    try {
      const res = await authApi.changePassword(passwordData)
      return res.success
    } catch (error) {
      console.error('修改密码失败:', error)
      return false
    }
  }

  /**
   * 退出登录
   */
  const logout = () => {
    token.value = null
    userInfo.value = null
    tokenUtils.clearAuth()
  }

  return {
    userInfo,
    token,
    login,
    getCurrentUser,
    changePassword,
    logout
  }
})

export default useUserStore
