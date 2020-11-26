<template>
  <div class="app-layout">
    <a-layout>
      <a-layout-header>
        <Header
          :headerTheme="appSkin.headerTheme"
          :siderCollapsedState.sync="menuCollapsed"
          @onAppSkin="appSkin.state=true;"
        >
          <!-- <template>xxx管理系统</template>
          <template slot="user">HZY</template>-->
        </Header>
      </a-layout-header>
      <a-layout :hasSider="true">
        <template v-if="isMobile">
          <a-drawer
            width="256"
            placement="left"
            :bodyStyle="{padding:'0',height:'100%'}"
            :wrapStyle="{overflow:'hidden',overflowY:'auto'}"
            :maskClosable="true"
            :closable="false"
            :visible="menuCollapsed"
            @close="menuCollapsed=!menuCollapsed"
            class="app-menu-drawer"
          >
            <a-layout-sider
              width="256"
              :theme="appSkin.menuTheme"
              :collapsed="false"
              :style="{height:'100%'}"
            >
              <div
                class="app-logo"
                :class="appSkin.menuTheme=='dark'?'app-logo-dark':'app-logo-light'"
              >HZY Antd Of Vue</div>
              <Menus
                :isMobile="isMobile"
                :theme="appSkin.menuTheme"
                :siderCollapsedState="menuCollapsed"
                :data="menusData"
              />
            </a-layout-sider>
          </a-drawer>
        </template>
        <template v-else>
          <a-layout-sider width="256" :theme="appSkin.menuTheme" :collapsed="menuCollapsed">
            <Menus
              :isMobile="isMobile"
              :theme="appSkin.menuTheme"
              :siderCollapsedState="menuCollapsed"
              :data="menusData"
            />
          </a-layout-sider>
        </template>
        <a-layout>
          <Tabs
            :tabsList.sync="tabsList"
            @onCloseSelf="onCloseSelf"
            @onCloseOther="onCloseOther"
            @onCloseAll="onCloseAll"
            @onTabClick="onTabClick"
          />
          <a-layout-content>
            <!-- 方式1 切换带有特效-->
            <!-- <transition name="slide-fade">
              <keep-alive>
                <router-view />
              </keep-alive>
            </transition>-->
            <!-- 方式2 切换带有特效 -->
            <transition name="page-toggle">
              <keep-alive>
                <router-view />
              </keep-alive>
            </transition>
            <!-- 方式3 切换没有特效 -->
            <!-- <keep-alive>
              <router-view />
            </keep-alive>-->
            <!-- 方式4 切换没有特效 并且 不会缓存页面信息 -->
            <!-- <router-view /> -->
          </a-layout-content>
        </a-layout>
      </a-layout>
    </a-layout>
    <!--系统设置表单 start-->
    <a-drawer
      placement="right"
      width="300px"
      :closable="false"
      @close="appSkin.state=!appSkin.state"
      :visible="appSkin.state"
    >
      <a-divider>主题颜色</a-divider>
      <div class="app-skin-list mb-5 text-center">
        <div
          class="app-skin-item"
          v-for="(item,index) in appSkin.headerThemeArray"
          :key="index"
          :style="{background:item}"
          @click="onHeaderTheme(index)"
        ></div>
      </div>
      <a-divider>菜单颜色</a-divider>
      <div class="mt-5 text-center">
        <a-radio-group
          name="radioGroup"
          :defaultValue="1"
          v-model="appSkin.menuTheme"
          @change="setMenuTheme()"
        >
          <a-radio value="dark">暗色</a-radio>
          <a-radio value="light">亮色</a-radio>
        </a-radio-group>
      </div>
    </a-drawer>
    <!--系统设置表单 end-->
  </div>
</template>
<script>
// 创建前(beforeCreate) 对应的钩子函数为beforeCreate。此阶段为...
// 2.创建后(created) 对应的钩子函数为created。在这个阶段vue实例已经创建...
// 3.载入前(beforeMount) 对应的钩子函数是beforemount,在这一阶段...
// 4.载入后(mounted) 对应的钩子函数是mounted。mounted是平时我们使用最...
// 5.更新前(beforeUpdate) 对应的钩子函数是beforeUpdate。在这一阶段...
import Header from "./header";
import Menus from "./menus";
import Tabs from "./tabs";
//
import themeColor from "../../js/themeColor";
//vuex
import { mapState, mapMutations, mapActions } from "vuex";
export default {
  name: "app-layout",
  data() {
    return {
      menuCollapsed: false, //菜单收展状态
      screenWidth: 0,
      screenHeight: 0,
      isMobile: false,
      appSkin: {
        state: false,
        menuTheme: this.getMenuTheme(),
        headerTheme: this.getHeaderTheme(),
        headerThemeArray: [
          "#ffffff",
          "#001529",
          "linear-gradient(90deg, #1d42ab,#2173dc,#1e93ff,#409eff)",
          "#997b71",
          "#0bb2d4",
          "#11c26d",
          "#757575",
          "#667afa",
          "#eb6709",
          "#f74584",
          "#9463f7",
          "#ff4c52",
          "#17b3a3",
          "#fcb900"
        ]
      }
    };
  },
  components: { Header, Menus, Tabs },
  computed: {
    ...mapState("vuexApp", {
      tabsList: state => state.tabsList,
      menusData: state => state.menus,
      userName: state => state.userName //用户名称
    })
  },
  created() {},
  mounted() {
    this.checkWindowWidthChange();
    //设置主题
    if (this.appSkin.headerTheme > 0)
      this.updateTheme(this.appSkin.headerTheme);
  },
  methods: {
    //选中头部颜色
    onHeaderTheme(index) {
      if (this.appSkin.headerTheme !== index) {
        //设置主题
        this.updateTheme(index);
      }
      this.appSkin.headerTheme = index;
      this.setHeaderTheme(index);
    },
    //存储头部颜色
    setHeaderTheme(index) {
      localStorage.setItem("hzyAntdVue-HeaderTheme", index);
    },
    //获取头部颜色
    getHeaderTheme() {
      let theme = localStorage.getItem("hzyAntdVue-HeaderTheme");
      var headrTheme = parseInt(theme ? theme : 0);
      return headrTheme;
    },
    //存储菜单颜色
    setMenuTheme() {
      localStorage.setItem("hzyAntdVue-MenuTheme", this.appSkin.menuTheme);
    },
    //获取菜单颜色 //light|dark //菜单主题颜色
    getMenuTheme() {
      let theme = localStorage.getItem("hzyAntdVue-MenuTheme");
      return theme ? theme : "dark";
    },
    //更新主题颜色
    updateTheme(index) {
      var primaryColor = this.appSkin.headerThemeArray[index];
      if (index === 0) {
        primaryColor = "#1890ff";
      } else if (index === 1) {
        primaryColor = "#11c26d";
      }
      themeColor.updatePrimaryColor(primaryColor);
    },
    //检测窗口变化
    checkWindowChange(call) {
      this.screenWidth = window.innerWidth;
      this.screenHeight = window.innerHeight;
      call();
      window.onresize = () => {
        return (() => {
          this.screenWidth = window.innerWidth;
          this.screenHeight = window.innerHeight;
          call();
        })();
      };
    },
    //监听窗口宽度改变
    checkWindowWidthChange() {
      var _this = this;
      this.checkWindowChange(() => {
        setTimeout(function() {
          if (_this.screenWidth <= 576) {
            _this.isMobile = true;
            _this.menuCollapsed = false;
          } else if (_this.screenWidth > 576 && _this.screenWidth < 1200) {
            _this.isMobile = false;
            if (!_this.menuCollapsed) _this.menuCollapsed = true;
          } else if (_this.screenWidth >= 1200) {
            _this.isMobile = false;
            if (_this.menuCollapsed) _this.menuCollapsed = false;
          }
        }, 50);
      });
    },
    ...mapMutations("vuexApp", {
      add: "add", //添加选项卡
      onCloseSelf: "onCloseSelf",
      onCloseOther: "onCloseOther",
      onCloseAll: "onCloseAll",
      onTabClick: "onTabClick"
    })
    // ...mapActions("appVuex", { postAuthorize: "postAuthorize" })
  }
};
</script>
<style lang="less" scoped>
.app-layout {
  height: 100%;
  width: 100%;
  position: fixed;

  .ant-layout {
    height: 100%;
    overflow: hidden;
  }

  .ant-layout-sider {
    -webkit-box-shadow: 2px 0 8px 0 rgba(29, 35, 41, 0.05);
    box-shadow: 2px 0 8px 0 rgba(29, 35, 41, 0.05);
  }

  .ant-layout-content {
    overflow: hidden;
    overflow-y: auto;
    // padding: 20px;
    margin: 0;
    min-height: calc(100vh - 100px);
  }
}

.ant-drawer {
  .app-skin-list {
    width: 100%;
    display: inline-block;
    .app-skin-item {
      width: 40px;
      height: 40px;
      float: left;
      margin: 8px;
      cursor: pointer;
      border-radius: 40px;
      border: 1px solid #f5222d;
    }
  }
}

/* 可以设置不同的进入和离开动画 */
/* 设置持续时间和动画函数 */
.slide-fade-enter-active {
  transition: all 0.6s ease;
}
.slide-fade-leave-active {
  transition: all 0.2s cubic-bezier(1, 0.5, 0.8, 1);
}
.slide-fade-enter, .slide-fade-leave-to
/* .slide-fade-leave-active for below version 2.1.8 */ {
  opacity: 0;
  transform: translateX(100%);
}
</style>