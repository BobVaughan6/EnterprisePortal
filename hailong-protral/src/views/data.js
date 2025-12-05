// 海隆咨询 - 数据配置

export const companyInfo = {
  name: '海隆咨询',
  slogan: '海纳百川，才望兼隆',
  description: '致力于成为工程建设的专业支撑者、行业发展的可信力量，以科学、规范、公正的咨询服务助力政府与企业项目高质量实施。',
  stats: [
    { label: '服务项目', value: '5000+', unit: '个' },
    { label: '合作客户', value: '800+', unit: '家' },
    { label: '专业团队', value: '200+', unit: '人' },
    { label: '服务年限', value: '15+', unit: '年' }
  ]
}

// 企业简介
export const companyProfile = {
  title: '企业简介',
  content: '海隆工程咨询有限公司是河南省建设工程招标投标协会副会长单位,同时也是河南省注册造价工程师协会、河南省建设监理协会、河南省工程咨询协会会员单位。公司位于郑州市郑东新区金水东路雅宝东方国际广场,紧邻高铁郑州东站,地理位置优越,交通便利。\n\n公司具备造价咨询资质及司法鉴定资格,并拥有市政公用工程、机电安装工程、房屋建筑工程、化工石油工程、电力工程等多项监理资质,业务范围覆盖工程建设全过程。\n\n公司拥有现代化的技术设备、高素质的专业人才团队和优美的办公环境,以专业的工程咨询能力和良好的行业信誉,赢得了广泛的社会认可,成为各方客户信赖的工程咨询合作伙伴。\n\n公司先后荣获"河南省建设工程招标代理诚实守信单位"、"企业信用评价AAA级信用企业"、"全国重合同守信用优秀企业"、"质量·服务·诚信AAA企业"等荣誉称号,并通过了ISO9001质量管理体系、ISO14001环境管理体系和ISO45001职业健康安全管理体系认证。\n\n海纳百川,才望兼隆。公司始终以开放包容的心态,热忱欢迎各界朋友携手合作,共创未来。',
  highlights: [
    '专业资质齐全',
    '经验丰富团队',
    '服务质量优质',
    '客户口碑良好'
  ]
}

// 业务范围
export const businessScope = [
  {
    id: 1,
    name: '政府采购代理',
    description: '提供政府采购项目全流程代理服务',
    features: ['采购需求编制', '招标文件制作', '开评标组织', '合同签订协助'],
    image: 'https://images.unsplash.com/photo-1450101499163-c8848c66ca85?w=600&h=400&fit=crop'
  },
  {
    id: 2,
    name: '工程招标代理',
    description: '建设工程项目招标投标全程服务',
    features: ['招标方案策划', '资格预审', '评标专家抽取', '中标公示发布'],
    image: 'https://images.unsplash.com/photo-1541888946425-d81bb19240f5?w=600&h=400&fit=crop'
  },
  {
    id: 3,
    name: '造价咨询',
    description: '工程造价全过程咨询与管理',
    features: ['工程预算编制', '工程量清单', '结算审核', '造价分析'],
    image: 'https://images.unsplash.com/photo-1454165804606-c3d57bc86b40?w=600&h=400&fit=crop'
  },
  {
    id: 4,
    name: '项目管理',
    description: '工程项目全生命周期管理服务',
    features: ['项目策划', '进度管控', '质量监督', '风险管理'],
    image: 'https://images.unsplash.com/photo-1460925895917-afdab827c52f?w=600&h=400&fit=crop'
  },
  {
    id: 5,
    name: '咨询服务',
    description: '专业的工程技术咨询与顾问服务',
    features: ['可行性研究', '方案论证', '技术咨询', '政策解读'],
    image: 'https://images.unsplash.com/photo-1552664730-d307ca884978?w=600&h=400&fit=crop'
  },
  {
    id: 6,
    name: '培训服务',
    description: '招投标及工程管理专业培训',
    features: ['政策培训', '业务培训', '技能提升', '资质考试'],
    image: 'https://images.unsplash.com/photo-1524178232363-1fb2b075b655?w=600&h=400&fit=crop'
  }
]

// 交易数据统计
export const transactionData = {
  title: '交易数据可视化',
  yearlyStats: {
    totalProjects: 1250,
    totalAmount: '35.8亿',
    govProcurement: 680,
    construction: 570
  },
  // 交易类型占比
  typeDistribution: [
    { type: '政府采购', count: 680, percentage: 54.4, color: '#0EA5E9' },
    { type: '建设工程', count: 570, percentage: 45.6, color: '#10B981' }
  ],
  // 地区排行（前10）
  regionRanking: [
    { region: '市本级', projects: 285, amount: 8.5 },
    { region: '高新区', projects: 198, amount: 6.2 },
    { region: '经开区', projects: 165, amount: 5.1 },
    { region: '城东区', projects: 142, amount: 4.3 },
    { region: '城西区', projects: 128, amount: 3.8 }
  ]
}

// 重要业绩展示（简化版：项目名称、项目类型、项目图片、项目金额）
// 注意：金额单位统一为万元
export const majorAchievements = [
  {
    id: 1,
    projectName: '某市政府采购中心年度框架协议',
    projectType: '服务',
    projectAmount: 28000, // 万元
    imageUrl: 'https://images.unsplash.com/photo-1486406146926-c627a92ad1ab?w=400&h=300&fit=crop'
  },
  {
    id: 2,
    projectName: '某高新区基础设施建设项目',
    projectType: '工程',
    projectAmount: 52000, // 万元
    imageUrl: 'https://images.unsplash.com/photo-1503387762-592deb58ef4e?w=400&h=300&fit=crop'
  },
  {
    id: 3,
    projectName: '某医院医疗设备采购项目',
    projectType: '货物',
    projectAmount: 15000, // 万元
    imageUrl: 'https://images.unsplash.com/photo-1519494026892-80bbd2d6fd0d?w=400&h=300&fit=crop'
  },
  {
    id: 4,
    projectName: '某学校智慧校园建设项目',
    projectType: '服务',
    projectAmount: 8500, // 万元
    imageUrl: 'https://images.unsplash.com/photo-1562774053-701939374585?w=400&h=300&fit=crop'
  },
  {
    id: 5,
    projectName: '某工业园区道路工程',
    projectType: '工程',
    projectAmount: 36000, // 万元
    imageUrl: 'https://images.unsplash.com/photo-1581094794329-c8112a89af12?w=400&h=300&fit=crop'
  },
  {
    id: 6,
    projectName: '某政务服务中心装修项目',
    projectType: '工程',
    projectAmount: 6800, // 万元
    imageUrl: 'https://images.unsplash.com/photo-1497366216548-37526070297c?w=400&h=300&fit=crop'
  }
]

// 政府采购公告
export const govProcurementAnnouncements = [
  {
    id: 1,
    title: '某市教育局教学设备采购项目招标公告',
    budget: '¥850万',
    type: '公开招标',
    deadline: '2025-12-20',
    status: '进行中',
    publishDate: '2025-12-01'
  },
  {
    id: 2,
    title: '某区政府办公设备及家具采购项目',
    budget: '¥320万',
    type: '竞争性谈判',
    deadline: '2025-12-18',
    status: '进行中',
    publishDate: '2025-11-28'
  },
  {
    id: 3,
    title: '某医院医疗器械采购项目招标公告',
    budget: '¥1200万',
    type: '公开招标',
    deadline: '2025-12-25',
    status: '即将开始',
    publishDate: '2025-11-25'
  }
]

// 建设工程公告
export const constructionAnnouncements = [
  {
    id: 1,
    title: '某市政道路改造工程施工招标公告',
    budget: '¥5600万',
    type: '公开招标',
    deadline: '2025-12-22',
    status: '进行中',
    publishDate: '2025-12-02'
  },
  {
    id: 2,
    title: '某学校综合楼建设项目招标公告',
    budget: '¥3800万',
    type: '邀请招标',
    deadline: '2025-12-19',
    status: '进行中',
    publishDate: '2025-11-29'
  },
  {
    id: 3,
    title: '某工业园区配套设施建设工程',
    budget: '¥8200万',
    type: '公开招标',
    deadline: '2025-12-28',
    status: '即将开始',
    publishDate: '2025-11-26'
  }
]

// 政策法规
export const policyRegulations = [
  {
    id: 1,
    title: '政府采购法实施条例（2024修订）',
    category: '法律法规',
    date: '2024-11-15',
    summary: '最新修订的政府采购法实施条例，规范政府采购行为...',
    downloads: 3200
  },
  {
    id: 2,
    title: '招标投标法实施细则',
    category: '法律法规',
    date: '2024-10-20',
    summary: '招标投标活动的具体实施规范和要求...',
    downloads: 2800
  },
  {
    id: 3,
    title: '工程造价咨询管理办法',
    category: '部门规章',
    date: '2024-09-10',
    summary: '工程造价咨询企业和从业人员管理规定...',
    downloads: 2100
  },
  {
    id: 4,
    title: '建设工程质量管理条例',
    category: '行政法规',
    date: '2024-08-05',
    summary: '建设工程质量管理的基本要求和责任划分...',
    downloads: 2500
  }
]

export const navLinks = [
  { name: '首页', path: '/' },
  { name: '关于海隆', path: '/about' },
  { name: '公告信息', path: '/announcements' },
  { name: '政策法规', path: '/policies' },
  { name: '实用工具', path: '/tools' },
  { name: '联系我们', path: '/contact' }
]
