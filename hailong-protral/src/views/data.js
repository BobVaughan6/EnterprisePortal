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
  content: '海隆咨询是一家专业从事工程咨询服务的综合性企业，拥有政府采购代理、招标代理、造价咨询等多项资质。公司秉承"海纳百川，才望兼隆"的理念，为政府机关、企事业单位提供全方位、高质量的工程咨询服务。',
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
  monthlyTrend: [
    { month: '1月', projects: 85, amount: 2.5 },
    { month: '2月', projects: 72, amount: 2.1 },
    { month: '3月', projects: 105, amount: 3.2 },
    { month: '4月', projects: 98, amount: 2.8 },
    { month: '5月', projects: 112, amount: 3.5 },
    { month: '6月', projects: 125, amount: 3.8 },
    { month: '7月', projects: 108, amount: 3.1 },
    { month: '8月', projects: 95, amount: 2.7 },
    { month: '9月', projects: 118, amount: 3.4 },
    { month: '10月', projects: 132, amount: 4.2 },
    { month: '11月', projects: 128, amount: 3.9 },
    { month: '12月', projects: 142, amount: 4.6 }
  ]
}

// 重要业绩展示
export const majorAchievements = [
  {
    id: 1,
    title: '某市政府采购中心年度框架协议',
    amount: '2.8亿',
    year: '2024',
    image: 'https://images.unsplash.com/photo-1486406146926-c627a92ad1ab?w=400&h=300&fit=crop'
  },
  {
    id: 2,
    title: '某高新区基础设施建设项目',
    amount: '5.2亿',
    year: '2024',
    image: 'https://images.unsplash.com/photo-1503387762-592deb58ef4e?w=400&h=300&fit=crop'
  },
  {
    id: 3,
    title: '某医院医疗设备采购项目',
    amount: '1.5亿',
    year: '2024',
    image: 'https://images.unsplash.com/photo-1519494026892-80bbd2d6fd0d?w=400&h=300&fit=crop'
  },
  {
    id: 4,
    title: '某学校智慧校园建设项目',
    amount: '8500万',
    year: '2024',
    image: 'https://images.unsplash.com/photo-1562774053-701939374585?w=400&h=300&fit=crop'
  },
  {
    id: 5,
    title: '某工业园区道路工程',
    amount: '3.6亿',
    year: '2023',
    image: 'https://images.unsplash.com/photo-1581094794329-c8112a89af12?w=400&h=300&fit=crop'
  },
  {
    id: 6,
    title: '某政务服务中心装修项目',
    amount: '6800万',
    year: '2023',
    image: 'https://images.unsplash.com/photo-1497366216548-37526070297c?w=400&h=300&fit=crop'
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
