<template>
  <footer class="bg-hailong-dark text-white py-12 border-t border-hailong-cyan/20">
    <div class="container-wide">
      <div class="grid grid-cols-1 md:grid-cols-4 gap-8 mb-8">
        <div>
          <h3 class="text-xl font-bold mb-4 text-hailong-cyan font-tech">海隆咨询</h3>
          <p class="text-sm text-gray-400">海纳百川，才望兼隆</p>
        </div>
        <div>
          <h4 class="font-semibold mb-4">业务范围</h4>
          <ul class="space-y-2 text-sm text-gray-400">
            <li><a href="#" class="hover:text-hailong-cyan transition-colors">政府采购代理</a></li>
            <li><a href="#" class="hover:text-hailong-cyan transition-colors">工程招标代理</a></li>
            <li><a href="#" class="hover:text-hailong-cyan transition-colors">造价咨询</a></li>
          </ul>
        </div>
        <div>
          <h4 class="font-semibold mb-4">快速链接</h4>
          <ul class="space-y-2 text-sm text-gray-400">
            <li><router-link to="/" class="hover:text-hailong-cyan transition-colors">首页</router-link></li>
            <li><router-link to="/about" class="hover:text-hailong-cyan transition-colors">关于海隆</router-link></li>
            <li><router-link to="/announcements" class="hover:text-hailong-cyan transition-colors">公告信息</router-link></li>
            <li><router-link to="/policies" class="hover:text-hailong-cyan transition-colors">政策法规</router-link></li>
          </ul>
        </div>
        <div>
          <h4 class="font-semibold mb-4">联系我们</h4>
          <ul class="space-y-2 text-sm text-gray-400">
            <li>电话: 400-123-4567</li>
            <li>邮箱: info@hailong.com</li>
            <li>地址: 某市某区某街道</li>
          </ul>
        </div>
      </div>
      <div class="border-t border-gray-800 pt-8 text-center text-gray-400 text-sm">
        <p>© 2025 海隆咨询有限公司 版权所有</p>
      </div>
    </div>
  </footer>
</template>

<script setup>
// 页脚组件
</script>

<style scoped>
/* 组件特定样式 */
</style>