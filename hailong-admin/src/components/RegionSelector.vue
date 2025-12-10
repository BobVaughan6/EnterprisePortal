<template>
  <div class="region-selector">
    <!-- 省份选择 -->
    <el-select
      v-model="selectedProvince"
      placeholder="请选择省份"
      clearable
      filterable
      :disabled="disabled"
      @change="handleProvinceChange"
      :style="{ width: provinceWidth }"
    >
      <el-option
        v-for="province in provinces"
        :key="province.regionCode"
        :label="province.regionName"
        :value="province.regionCode"
      />
    </el-select>

    <!-- 城市选择 -->
    <el-select
      v-if="showCity"
      v-model="selectedCity"
      placeholder="请选择城市"
      clearable
      filterable
      :disabled="disabled || !selectedProvince"
      @change="handleCityChange"
      :style="{ width: cityWidth, marginLeft: '10px' }"
    >
      <el-option
        v-for="city in cities"
        :key="city.regionCode"
        :label="city.regionName"
        :value="city.regionCode"
      />
    </el-select>

    <!-- 区县选择 -->
    <el-select
      v-if="showDistrict"
      v-model="selectedDistrict"
      placeholder="请选择区县"
      clearable
      filterable
      :disabled="disabled || !selectedCity"
      @change="handleDistrictChange"
      :style="{ width: districtWidth, marginLeft: '10px' }"
    >
      <el-option
        v-for="district in districts"
        :key="district.regionCode"
        :label="district.regionName"
        :value="district.regionCode"
      />
    </el-select>
  </div>
</template>

<script setup>
import { ref, watch, onMounted } from 'vue'
import { systemApi } from '@/api'
import { ElMessage } from 'element-plus'

const props = defineProps({
  // 是否显示城市选择
  showCity: {
    type: Boolean,
    default: true
  },
  // 是否显示区县选择
  showDistrict: {
    type: Boolean,
    default: true
  },
  // 是否禁用
  disabled: {
    type: Boolean,
    default: false
  },
  // 省份宽度
  provinceWidth: {
    type: String,
    default: '150px'
  },
  // 城市宽度
  cityWidth: {
    type: String,
    default: '150px'
  },
  // 区县宽度
  districtWidth: {
    type: String,
    default: '150px'
  },
  // 初始省份代码
  provinceCode: {
    type: String,
    default: ''
  },
  // 初始城市代码
  cityCode: {
    type: String,
    default: ''
  },
  // 初始区县代码
  districtCode: {
    type: String,
    default: ''
  }
})

const emit = defineEmits(['change', 'province-change', 'city-change', 'district-change'])

const provinces = ref([])
const cities = ref([])
const districts = ref([])

const selectedProvince = ref('')
const selectedCity = ref('')
const selectedDistrict = ref('')

// 加载省份列表
const loadProvinces = async () => {
  try {
    const res = await systemApi.regions.getProvinces()
    if (res.success && res.data) {
      provinces.value = res.data
    }
  } catch (error) {
    console.error('加载省份列表失败:', error)
    ElMessage.error('加载省份列表失败')
  }
}

// 加载城市列表
const loadCities = async (provinceCode) => {
  if (!provinceCode) {
    cities.value = []
    return
  }

  try {
    const res = await systemApi.regions.getCities(provinceCode)
    if (res.success && res.data) {
      cities.value = res.data
    }
  } catch (error) {
    console.error('加载城市列表失败:', error)
    ElMessage.error('加载城市列表失败')
  }
}

// 加载区县列表
const loadDistricts = async (cityCode) => {
  if (!cityCode) {
    districts.value = []
    return
  }

  try {
    const res = await systemApi.regions.getDistricts(cityCode)
    if (res.success && res.data) {
      districts.value = res.data
    }
  } catch (error) {
    console.error('加载区县列表失败:', error)
    ElMessage.error('加载区县列表失败')
  }
}

// 省份变化
const handleProvinceChange = async (value) => {
  selectedCity.value = ''
  selectedDistrict.value = ''
  cities.value = []
  districts.value = []

  if (value && props.showCity) {
    await loadCities(value)
  }

  emitChange()
  emit('province-change', value)
}

// 城市变化
const handleCityChange = async (value) => {
  selectedDistrict.value = ''
  districts.value = []

  if (value && props.showDistrict) {
    await loadDistricts(value)
  }

  emitChange()
  emit('city-change', value)
}

// 区县变化
const handleDistrictChange = (value) => {
  emitChange()
  emit('district-change', value)
}

// 触发change事件
const emitChange = () => {
  const province = provinces.value.find(p => p.regionCode === selectedProvince.value)
  const city = cities.value.find(c => c.regionCode === selectedCity.value)
  const district = districts.value.find(d => d.regionCode === selectedDistrict.value)

  emit('change', {
    provinceCode: selectedProvince.value,
    provinceName: province?.regionName || '',
    cityCode: selectedCity.value,
    cityName: city?.regionName || '',
    districtCode: selectedDistrict.value,
    districtName: district?.regionName || ''
  })
}

// 初始化
const init = async () => {
  await loadProvinces()

  if (props.provinceCode) {
    selectedProvince.value = props.provinceCode
    if (props.showCity) {
      await loadCities(props.provinceCode)
    }
  }

  if (props.cityCode && props.showCity) {
    selectedCity.value = props.cityCode
    if (props.showDistrict) {
      await loadDistricts(props.cityCode)
    }
  }

  if (props.districtCode && props.showDistrict) {
    selectedDistrict.value = props.districtCode
  }
}

// 监听props变化
watch(() => props.provinceCode, async (newVal) => {
  if (newVal !== selectedProvince.value) {
    selectedProvince.value = newVal
    if (newVal && props.showCity) {
      await loadCities(newVal)
    }
  }
})

watch(() => props.cityCode, async (newVal) => {
  if (newVal !== selectedCity.value) {
    selectedCity.value = newVal
    if (newVal && props.showDistrict) {
      await loadDistricts(newVal)
    }
  }
})

watch(() => props.districtCode, (newVal) => {
  if (newVal !== selectedDistrict.value) {
    selectedDistrict.value = newVal
  }
})

// 暴露方法供父组件调用
defineExpose({
  reset: () => {
    selectedProvince.value = ''
    selectedCity.value = ''
    selectedDistrict.value = ''
    cities.value = []
    districts.value = []
  },
  getSelectedRegion: () => ({
    provinceCode: selectedProvince.value,
    cityCode: selectedCity.value,
    districtCode: selectedDistrict.value
  })
})

onMounted(() => {
  init()
})
</script>

<style scoped>
.region-selector {
  display: inline-flex;
  align-items: center;
}
</style>