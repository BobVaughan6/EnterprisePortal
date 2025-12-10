<template>
  <el-cascader
    v-model="selectedRegion"
    :options="regionOptions"
    :props="cascaderProps"
    :placeholder="placeholder"
    :disabled="disabled"
    :clearable="clearable"
    :filterable="filterable"
    :show-all-levels="showAllLevels"
    @change="handleChange"
    :style="{ width: width }"
  />
</template>

<script setup>
import { ref, watch, onMounted } from 'vue'
import { systemApi } from '@/api'
import { ElMessage } from 'element-plus'

const props = defineProps({
  // 初始值（区域代码数组，如：['410000', '410100', '410122']）
  modelValue: {
    type: Array,
    default: () => []
  },
  // 占位符
  placeholder: {
    type: String,
    default: '请选择省/市/区'
  },
  // 是否禁用
  disabled: {
    type: Boolean,
    default: false
  },
  // 是否可清空
  clearable: {
    type: Boolean,
    default: true
  },
  // 是否可搜索
  filterable: {
    type: Boolean,
    default: true
  },
  // 是否显示完整路径
  showAllLevels: {
    type: Boolean,
    default: true
  },
  // 宽度
  width: {
    type: String,
    default: '100%'
  },
  // 最大级别（1=省，2=市，3=区）
  maxLevel: {
    type: Number,
    default: 3
  }
})

const emit = defineEmits(['update:modelValue', 'change'])

const selectedRegion = ref([])
const regionOptions = ref([])
const isInitializing = ref(false)

// 级联选择器配置 - 不使用懒加载，改为一次性加载所有数据
const cascaderProps = {
  value: 'regionCode',
  label: 'regionName',
  children: 'children',
  checkStrictly: false,
  emitPath: true
}

// 构建树形结构的区域数据
const buildRegionTree = async () => {
  try {
    // 加载所有省份
    const provinceRes = await systemApi.regions.getProvinces()
    if (!provinceRes.success || !provinceRes.data) {
      return []
    }

    const provinces = provinceRes.data

    // 如果只需要省级，直接返回
    if (props.maxLevel === 1) {
      return provinces.map(p => ({
        regionCode: p.regionCode,
        regionName: p.regionName
      }))
    }

    // 加载所有省份的城市
    const provinceWithCities = await Promise.all(
      provinces.map(async (province) => {
        const cityRes = await systemApi.regions.getCities(province.regionCode)
        const cities = cityRes.success && cityRes.data ? cityRes.data : []

        // 如果只需要到市级
        if (props.maxLevel === 2) {
          return {
            regionCode: province.regionCode,
            regionName: province.regionName,
            children: cities.map(c => ({
              regionCode: c.regionCode,
              regionName: c.regionName
            }))
          }
        }

        // 需要到区县级，加载所有城市的区县
        const citiesWithDistricts = await Promise.all(
          cities.map(async (city) => {
            const districtRes = await systemApi.regions.getDistricts(city.regionCode)
            const districts = districtRes.success && districtRes.data ? districtRes.data : []

            return {
              regionCode: city.regionCode,
              regionName: city.regionName,
              children: districts.map(d => ({
                regionCode: d.regionCode,
                regionName: d.regionName
              }))
            }
          })
        )

        return {
          regionCode: province.regionCode,
          regionName: province.regionName,
          children: citiesWithDistricts
        }
      })
    )

    return provinceWithCities
  } catch (error) {
    console.error('加载区域数据失败:', error)
    ElMessage.error('加载区域数据失败')
    return []
  }
}

// 初始化区域数据
const initRegionData = async () => {
  isInitializing.value = true
  regionOptions.value = await buildRegionTree()
  isInitializing.value = false
}

// 从树形数据中查找区域名称
const findRegionName = (code, tree) => {
  for (const node of tree) {
    if (node.regionCode === code) {
      return node.regionName
    }
    if (node.children) {
      const found = findRegionName(code, node.children)
      if (found) return found
    }
  }
  return ''
}

// 处理选择变化
const handleChange = (value) => {
  emit('update:modelValue', value)
  
  // 获取选中的区域信息
  if (value && value.length > 0) {
    const regionInfo = {
      provinceCode: value[0] || '',
      provinceName: findRegionName(value[0], regionOptions.value),
      cityCode: value[1] || '',
      cityName: value[1] ? findRegionName(value[1], regionOptions.value) : '',
      districtCode: value[2] || '',
      districtName: value[2] ? findRegionName(value[2], regionOptions.value) : '',
      fullPath: value
    }

    emit('change', regionInfo)
  } else {
    // 清空选择
    emit('change', {
      provinceCode: '',
      provinceName: '',
      cityCode: '',
      cityName: '',
      districtCode: '',
      districtName: '',
      fullPath: []
    })
  }
}

// 监听外部值变化
watch(() => props.modelValue, (newVal) => {
  if (JSON.stringify(newVal) !== JSON.stringify(selectedRegion.value)) {
    selectedRegion.value = newVal || []
  }
}, { immediate: true })

// 暴露方法
defineExpose({
  reset: () => {
    selectedRegion.value = []
  },
  getValue: () => selectedRegion.value
})

onMounted(async () => {
  await initRegionData()
  
  // 设置初始值
  if (props.modelValue && props.modelValue.length > 0) {
    selectedRegion.value = [...props.modelValue]
  }
})
</script>

<style scoped>
/* 可以添加自定义样式 */
</style>