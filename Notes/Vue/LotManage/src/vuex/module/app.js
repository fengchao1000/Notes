import tools from '../../js/tools';

const vuexApp = {
    namespaced: true,
    state: {
        loading: false,
        userName: "欢迎 HZY !!!",
        menus: [],
        routerConfig: [],
        userName: null,
        powerAll: [],
        tabsList: tools.getTabsList() ? JSON.parse(tools.getTabsList()) : [{ key: "1", title: "首页", path: "/home", active: true, close: false }]
    },
    mutations: {
        setLoading(state, par) {
            state.loading = !!par;
        },
        add(state, param) {
            for (let index = 0; index < state.tabsList.length; index++) {
                const element = state.tabsList[index];
                element.active = false;
            }
            var key = param.name;
            var obj = {
                key: param.name,
                title: param.meta.title,
                path: param.path,
                active: true,
                close: true
            };
            //检查是否存在
            var tab = state.tabsList.find(w => w.key == key);
            if (tab && !tab.active) {
                tab.active = true;
            }
            if (!tab) {
                state.tabsList.push(obj);
            }
            tools.saveTabsList(state.tabsList);
        },
        //关闭当前
        onCloseSelf(state, param) {
            var key = param;
            var index = state.tabsList.findIndex(w => w.key == key);
            var tab = state.tabsList[index];
            if (tab.close) {
                state.tabsList.splice(index, 1);
            }
            var newTab = state.tabsList[index - 1];
            newTab.active = true;
            global.$router.push({ name: newTab.key });
            tools.saveTabsList(state.tabsList);
        },
        //关闭其他
        onCloseOther(state, param) {
            var key = param;
            var list = [];
            for (var i = 0; i < state.tabsList.length; i++) {
                var item = state.tabsList[i];
                if (!item.close || item.key == key) {
                    list.push(item);
                }
            }
            var tab = list.find(w => w.active);
            if (!tab) {
                tab = list.find(w => w.key == key);
                tab.active = true;
            }
            state.tabsList = list;
            tools.saveTabsList(state.tabsList);
        },
        //关闭所有
        onCloseAll(state, param) {
            var tab = state.tabsList.find(w => !w.close);
            tab.active = true;
            state.tabsList.push([tab]);
            tools.saveTabsList(state.tabsList);
        },
        //点击切换选项卡
        onTabClick(state, param) {
            var key = param;
            for (let index = 0; index < state.tabsList.length; index++) {
                const element = state.tabsList[index];
                element.active = false;
            }
            var tab = state.tabsList.find(w => w.key == key);
            if (tab && !tab.active) tab.active = true;
            global.$router.push({ name: tab.key });
            tools.saveTabsList(state.tabsList);
        },
        setRouterConfig(state, par) {
            state.routerConfig = par;
        },
        setUserName(state, par) {
            state.userName = par;
        },
        setMenus(state, par) {
            state.menus = par;
        },
        setPowerAll(state, par) {
            state.powerAll = par;
        }
    },
    actions: {
        //获取菜单
        getMenus(context, callBack) {
            global
                .post('Admin/Menus/SysTree', {}, true)
                .then(res => {
                    var data = res.data.data;
                    console.log('data', data);
                    // var _allList = data.allList;
                    context.commit('setMenus', data.list);
                    context.commit('setUserName', data.userName);
                    context.commit('setPowerAll', data.powerState);
                    //
                    var state = context.state;
                    var _allList = data.allList;
                    //动态获取 组件
                    function loadViews(path) {
                        return () =>
                            import(/* webpackChunkName: "lot-web-boot-script" */ `@/views${path}.vue`);
                    }
                    //路由组装
                    var _children = [];
                    for (var i = 0; i < _allList.length; i++) {
                        var _menu = _allList[i];
                        if (!_menu.Menu_Url) continue;
                        if (!_menu.Menu_RoutePath) continue;
                        var _item = _children.find(w => w.name == _menu.Menu_ID);
                        if (_item) continue;
                        //将第一个 选项卡 key 修改为数据库中的 menu id
                        var home = state.tabsList[0];
                        if (home.path.toLowerCase() === _menu.Menu_RoutePath.toLowerCase()) {
                            home.key = _menu.Menu_ID;
                        }
                        //拼接子路由
                        _children.push({
                            path: _menu.Menu_RoutePath,
                            name: _menu.Menu_ID,
                            component: loadViews(_menu.Menu_Url),
                            meta: {
                                title: _menu.Menu_Name,
                                menuId: _menu.Menu_ID,
                                pid: _menu.Menu_ParentID
                            },
                        });
                    }
                    if (callBack) callBack(_children);
                });
        }
    },
    getters: {}
}
export default vuexApp