//为了兼容 ie 9
import "@babel/polyfill";
//
import Vue from 'vue'
import App from './App.vue'
//vuex
import store from './vuex/store'
//router
import router from './js/router/index'
//antd
import Antd from 'ant-design-vue'
//import 'ant-design-vue/dist/antd.css'
import './css/theme-ext.less'
import './css/utils.less';
//
import tools from './js/tools'
//http request
import { get, post, upload, download } from './js/http'
Vue.use(Antd);
//
global.$vuex = store;
global.$router = router;
global.tools = tools;
global.get = get;
global.post = post;
global.upload = upload;
global.download = download;
//权限
global.$power = {};
//
Vue.config.productionTip = false
//
new Vue({
    store,
    router, //加入我自己的路由
    render: h => h(App)
}).$mount('#app')