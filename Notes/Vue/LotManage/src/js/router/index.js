import Vue from 'vue'
import VueRouter from 'vue-router'
//防止重复点击路由 报错
const originalPush = VueRouter.prototype.push
VueRouter.prototype.push = function push(location) {
    return originalPush.call(this, location).catch(err => err)
};
//
import Layout from '../../views/layout/layout'
import Login from '../../views/login'
//
// import Home from '../../views/Home'
// import Home1 from '../../views/Home1'
//系统管理
// import User from '../../views/sys/User'
//
Vue.use(VueRouter)

const routers = [
    // {
    //     path: '/',
    //     name: '/',
    //     component: Layout,
    //     redirect: "/Home",
    //     children: [{
    //         path: '/Home',
    //         name: "1",
    //         component: Home,
    //         meta: { title: '首页' },
    //     }, {
    //         path: '/Chart',
    //         name: "2",
    //         component: Home1,
    //         meta: { title: '图表' },
    //     }, {
    //         path: '/User',
    //         name: '3',
    //         component: User,
    //         meta: { title: '列表Curd' },
    //     }]
    // },
    { path: '/login', name: '/login', component: Login },
    // { path: '*', redirect: "/Home" }
];

const vueRouter = new VueRouter({
    // mode: 'history',
    routes: routers
});

//监听路由
vueRouter.beforeEach((to, from, next) => {
    console.log('路由拦截器', from, to);

    global.tools.loading.start();

    if (to.path == '/login') {
        global.tools.loading.close();
        return next();
    }
    
    console.log(global.tools.getAuthorization());
    //检查是否登录授权
    if (!global.tools.getAuthorization()) {
        global.tools.loading.close();
        global.tools.alert('请先登录授权!', "警告", () => global.$router.push("/login"));
        return;
    }

    //获取后台菜单数据 然后动态添加路由
    if (global.$vuex.state.vuexApp.menus.length == 0) {
        global.$vuex.dispatch('vuexApp/getMenus', (_children) => {
            var _router = [{
                path: '/',
                name: "/",
                component: Layout,
                children: _children,
                redirect: "/home"
            },
            { path: '*', redirect: "/login" }
            ];
            vueRouter.addRoutes(_router);
            // console.log('_router', _router);
            global.$vuex.commit('vuexApp/setRouterConfig', vueRouter.options.routes);
            // console.log('vueRouter.options.routes', vueRouter);
            //刚 add 完路由 需要手动告诉 next 跳转哪里 所以需要 将 to 传递过去不然容易出现bug
            return next(to);
        });
    } else {
        //路由拦截器
        if (to.meta.hasOwnProperty('title')) {
            global.$vuex.commit('vuexApp/add', to);
        }
        return checkRouter(to, next);
    }
});
vueRouter.afterEach(() => {
    global.tools.loading.close();
});

//检查权限
var checkRouter = function (to, next) {
    //判断是否需要权限判断
    var _title = to['meta']['title'];
    if (!_title) return next();
    //判断权限
    var _menuId = to.meta.menuId;
    var _powerState = global.$vuex.state.vuexApp.powerAll.find(w => w.MenuID == _menuId);
    if (!_powerState || !_powerState.Have) {
        global.tools.alert('请先登录授权!', "警告", () => global.$router.push("/login"));
        return;
    }
    //添加选项卡
    global.$vuex.commit('vuexApp/add', to);
    global.$power = _powerState;
    return next();
};

export default vueRouter;