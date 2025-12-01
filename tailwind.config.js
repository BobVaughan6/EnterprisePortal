/** @type {import('tailwindcss').Config} */
export default {
  content: [
    "./index.html",
    "./src/**/*.{vue,js,ts,jsx,tsx}",
  ],
  theme: {
    extend: {
      colors: {
        // 方案一：央企红
        zhongjian: {
          primary: '#C8102E',
          secondary: '#8B0000',
          gold: '#D4AF37',
          dark: '#1a1a1a'
        },
        // 方案二：麦肯锡蓝
        mckinsey: {
          primary: '#002244',
          secondary: '#003d5c',
          accent: '#00A3E0',
          light: '#F5F7FA'
        },
        // 方案四：德勤绿/普华橙
        deloitte: {
          primary: '#86BC25',
          secondary: '#43B02A',
          dark: '#2C3E50',
          gray: '#6B7280'
        },
        // 方案五：学术墨绿
        planning: {
          primary: '#2C5F2D',
          secondary: '#1E4620',
          earth: '#8B7355',
          light: '#F0F4F0'
        },
        // 方案六：能源蓝橙
        powerchina: {
          primary: '#0066CC',
          secondary: '#FF6600',
          dark: '#003366',
          steel: '#5A6C7D'
        },
        // 方案八：科技紫蓝
        huawei: {
          primary: '#2878FF',
          secondary: '#A259FF',
          dark: '#0F1419',
          cyan: '#00D4FF'
        }
      },
      fontFamily: {
        'songti': ['"SimSun"', '"STSong"', 'serif'],
        'yahei': ['"Microsoft YaHei"', '"微软雅黑"', 'sans-serif'],
        'tech': ['"SF Pro Display"', '"Helvetica Neue"', 'Arial', 'sans-serif']
      },
      animation: {
        'fade-in': 'fadeIn 0.6s ease-out',
        'slide-up': 'slideUp 0.8s ease-out',
        'float': 'float 3s ease-in-out infinite'
      },
      keyframes: {
        fadeIn: {
          '0%': { opacity: '0' },
          '100%': { opacity: '1' }
        },
        slideUp: {
          '0%': { transform: 'translateY(30px)', opacity: '0' },
          '100%': { transform: 'translateY(0)', opacity: '1' }
        },
        float: {
          '0%, 100%': { transform: 'translateY(0)' },
          '50%': { transform: 'translateY(-10px)' }
        }
      }
    },
  },
  plugins: [],
}
