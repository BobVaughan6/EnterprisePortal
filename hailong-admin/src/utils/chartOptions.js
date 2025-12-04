/**
 * 获取交易类型饼图配置
 * @param {Array} data - 项目类型数据 [{type: '政府采购', count: 680, percentage: 54.05}]
 */
export const getPieChartOption = (data) => {
  return {
    title: {
      text: '交易类型占比',
      left: 'center',
      top: 10,
      textStyle: {
        fontSize: 16,
        fontWeight: 'bold'
      }
    },
    tooltip: {
      trigger: 'item',
      formatter: '{b}: {c}个 ({d}%)'
    },
    legend: {
      orient: 'vertical',
      right: 10,
      top: 'center'
    },
    series: [
      {
        name: '项目类型',
        type: 'pie',
        radius: ['40%', '70%'],
        center: ['40%', '55%'],
        avoidLabelOverlap: false,
        itemStyle: {
          borderRadius: 10,
          borderColor: '#fff',
          borderWidth: 2
        },
        label: {
          show: true,
          formatter: '{b}\n{d}%'
        },
        emphasis: {
          label: {
            show: true,
            fontSize: 14,
            fontWeight: 'bold'
          }
        },
        data: data.map(item => ({
          name: item.type,
          value: item.count
        }))
      }
    ],
    color: ['#5470c6', '#91cc75', '#fac858', '#ee6666', '#73c0de']
  }
}

/**
 * 获取地区排行柱状图配置
 * @param {Array} data - 地区排行数据 [{region: '郑州市', projectCount: 356, amount: 0}]
 */
export const getBarChartOption = (data) => {
  const regions = data.map(item => item.region)
  const counts = data.map(item => item.projectCount)

  return {
    title: {
      text: '地区项目排行榜',
      left: 'center',
      top: 10,
      textStyle: {
        fontSize: 16,
        fontWeight: 'bold'
      }
    },
    tooltip: {
      trigger: 'axis',
      axisPointer: {
        type: 'shadow'
      },
      formatter: '{b}: {c}个项目'
    },
    grid: {
      left: '3%',
      right: '4%',
      bottom: '3%',
      top: 60,
      containLabel: true
    },
    xAxis: {
      type: 'category',
      data: regions,
      axisLabel: {
        interval: 0,
        rotate: 30,
        fontSize: 12
      }
    },
    yAxis: {
      type: 'value',
      name: '项目数量',
      axisLabel: {
        formatter: '{value}个'
      }
    },
    series: [
      {
        name: '项目数量',
        type: 'bar',
        data: counts,
        itemStyle: {
          color: '#5470c6',
          borderRadius: [5, 5, 0, 0]
        },
        barWidth: '60%',
        label: {
          show: true,
          position: 'top',
          formatter: '{c}个'
        }
      }
    ]
  }
}
