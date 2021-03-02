/** When your routing table is too long, you can split it into small modules **/
/** 如果你的路由表太长，可以切分成小的路由模块 **/


import Layout from '@/layout'

const settingRouter = {
  path: '/setting',
  component: Layout,
  redirect: '/setting/settings',
  alwaysShow: true,
  name: 'setting',
  meta: {
    title: 'SettingUi["Settings"]',
    icon: 'tree',
    policy: ''
  },
  children: [
    {
        path: 'settings',
        component: () => import('@/views/setting/index'),//views/setting/index 对应页面路径
        name: 'settings',
        meta: { title: 'SettingUi["Settings"]', policy: 'SettingUi.Tenant' }//SettingUi.Tenant对应API返回的policies
    }
  ]
}
export default settingRouter
