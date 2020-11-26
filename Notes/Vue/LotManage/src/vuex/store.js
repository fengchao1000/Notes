import Vue from 'vue';
import Vuex from 'vuex'; //引入 vuex
import store from './store' //注册store
// Module
import vuexApp from './module/app' // app.vue
import vuexLogin from './module/login';
//sys
import vuexUser from './sys/user'
import vuexRole from './sys/role'
import vuexFunction from './sys/function'
import vuexMenus from './sys/menus'
import vuexRoleFunction from './sys/roleFunction'
import vuexChangePassword from './sys/changePassword'
import vuexAppLog from './sys/appLog'
//base
import vuexMember from './base/member'

//game
import vuexTableInfo from './game/tableInfo'
import vuexGame from './game/game'
import vuexCategory  from './lot/category'
import vuexLotType  from './lot/lottype'
import vuexLotInfo  from './lot/lotInfo'

Vue.use(Vuex); //使用 vuex
export default new Vuex.Store({
    modules: {
        vuexLogin,
        vuexApp,
        //sys
        vuexUser,
        vuexRole,
        vuexFunction,
        vuexMenus,
        vuexRoleFunction,
        vuexChangePassword,
        vuexAppLog,
        //base
        vuexMember,
        vuexTableInfo,
        vuexGame,
        vuexCategory,
        vuexLotType,
        vuexLotInfo
    }
})