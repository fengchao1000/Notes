<template>
  <div class="app-header" :class="'app-header-skin-'+headerTheme">
    <div class="app-header-left">
      <div class="app-header-item" @click="changeSiderCollapsedState()" title="菜单/收展">
        <a-icon :type="siderCollapsedState?'menu-unfold':'menu-fold'" />
      </div>
      <div class="app-header-item app-logo xs-hide" title="logo">
        <slot>合乐彩后台</slot>
      </div>
    </div>
    <div class="app-header-right">
      <div class="app-header-item xs-hide" @click="clearCache()" title="清理垃圾信息">
        <a-icon type="delete" />
      </div>
      <div class="app-header-item xs-hide" @click="handleFullScreen()" title="全屏/正常">
        <a-icon :type="fullscreen?'fullscreen-exit':'fullscreen'" />
      </div>
      <div class="app-header-item" @click="$emit('onAppSkin',null);" title="皮肤">
        <a-icon type="skin" />
      </div>
      <div class="app-header-item">
        <a-dropdown>
          <div>
            <a-icon type="user" class="fs-18 mr-10" />
            <slot name="user">注销</slot>
          </div>
          <a-menu slot="overlay">
            <a-menu-item @click="logOut">
              <a-icon type="logout" class="mr-10 text-danger" />退出登录
            </a-menu-item>
          </a-menu>
        </a-dropdown>
      </div>
    </div>
  </div>
</template>
<script>
export default {
  name: "app-layout-header",
  props: {
    siderCollapsedState: Boolean,
    headerTheme: Number
  },
  data() {
    return {
      fullscreen: false
    };
  },
  mounted() {},
  methods: {
    logOut() {
      //退出登录
      this.$router.push("/login");
    },
    //收展菜单事件
    changeSiderCollapsedState() {
      var state = !this.siderCollapsedState;
      this.$emit("update:siderCollapsedState", state);
      // this.$emit("siderCollapsedCall", state);
    },
    //清理垃圾信息
    clearCache() {
      global.tools.clearCache(() => {
        global.tools.msg("清理垃圾信息中...", "警告");
        setTimeout(function() {
          window.top.location.reload();
        }, 200);
      });
    },
    // 全屏事件
    handleFullScreen() {
      let element = document.documentElement;
      if (this.fullscreen) {
        if (document.exitFullscreen) {
          document.exitFullscreen();
        } else if (document.webkitCancelFullScreen) {
          document.webkitCancelFullScreen();
        } else if (document.mozCancelFullScreen) {
          document.mozCancelFullScreen();
        } else if (document.msExitFullscreen) {
          document.msExitFullscreen();
        }
      } else {
        if (element.requestFullscreen) {
          element.requestFullscreen();
        } else if (element.webkitRequestFullScreen) {
          element.webkitRequestFullScreen();
        } else if (element.mozRequestFullScreen) {
          element.mozRequestFullScreen();
        } else if (element.msRequestFullscreen) {
          // IE11
          element.msRequestFullscreen();
        }
      }
      this.fullscreen = !this.fullscreen;
    }
  }
};
</script>
<style lang="less" scoped>
.ant-layout-header {
  background: none !important;

  .app-header {
    position: relative;
    margin: 0 -50px;
    z-index: 1;
    -webkit-box-shadow: 0 1px 4px rgba(0, 21, 41, 0.08);
    box-shadow: 0 1px 4px rgba(0, 21, 41, 0.08);
    color: #ffffff;
    // -webkit-box-shadow: 2px 0 8px 0 rgba(29, 35, 41, .05);
    // box-shadow: 2px 0 8px 0 rgba(29, 35, 41, .05);

    .app-header-item {
      position: relative;
      display: block;
      margin: 0;
      padding: 0 20px;
      white-space: nowrap;
      cursor: pointer;
      -webkit-transition: color 0.3s cubic-bezier(0.645, 0.045, 0.355, 1),
        border-color 0.3s cubic-bezier(0.645, 0.045, 0.355, 1),
        background 0.3s cubic-bezier(0.645, 0.045, 0.355, 1),
        padding 0.15s cubic-bezier(0.645, 0.045, 0.355, 1);
      transition: color 0.3s cubic-bezier(0.645, 0.045, 0.355, 1),
        border-color 0.3s cubic-bezier(0.645, 0.045, 0.355, 1),
        background 0.3s cubic-bezier(0.645, 0.045, 0.355, 1),
        padding 0.15s cubic-bezier(0.645, 0.045, 0.355, 1);

      .anticon {
        font-size: 16px;
      }
    }

    .app-header-item:hover {
      background: #e6f7ff;
      color: #1890ff;
    }

    .app-header-left {
      display: inline-flex;
    }

    .app-header-right {
      float: right;
      display: inline-flex;
    }
  }
  //===================================header 皮肤
  .app-header-skin-0 {
    background: #fff !important;
    color: rgba(0, 0, 0, 0.65);
  }
  .app-header-skin-1 {
    background: #001529 !important;
  }
  .app-header-skin-2 {
    background: linear-gradient(
      90deg,
      #1d42ab,
      #2173dc,
      #1e93ff,
      #409eff
    ) !important;
  }
  .app-header-skin-3 {
    background: #997b71 !important;
  }
  .app-header-skin-4 {
    background: #0bb2d4 !important;
  }
  .app-header-skin-5 {
    background: #11c26d !important;
  }
  .app-header-skin-6 {
    background: #757575 !important;
  }
  .app-header-skin-7 {
    background: #667afa !important;
  }
  .app-header-skin-8 {
    background: #eb6709 !important;
  }
  .app-header-skin-9 {
    background: #f74584 !important;
  }
  .app-header-skin-10 {
    background: #9463f7 !important;
  }
  .app-header-skin-11 {
    background: #ff4c52 !important;
  }
  .app-header-skin-12 {
    background: #17b3a3 !important;
  }
  .app-header-skin-13 {
    background: #fcb900 !important;
  }
}
</style>
<style lang="less">
//logo
.app-logo {
  color: linear-gradient(90deg, #1d42ab, #2173dc, #1e93ff, #409eff);
  font-size: 20px;
  font-family: Avenir, Helvetica Neue, Arial, Helvetica, sans-serif;
  font-weight: 600;
  vertical-align: middle;
}
@media (max-width: 576px) {
  .app-logo {
    position: relative;
    font-size: 18px;
    // overflow: hidden;
    // text-overflow: ellipsis;
    // white-space: nowrap; //文本不换行，这样超出一行的部分被截取，显示...
    text-align: center;
    height: 64px;
    line-height: 64px;
    width: 100%;
  }
  .app-logo-dark {
    color: #ffffff;
    background: #002140;
  }
  .app-logo-light {
    color: #1890ff;
    -webkit-box-shadow: 1px 1px 0 0 #e8e8e8;
    box-shadow: 1px 1px 0 0 #e8e8e8;
  }
}
//logo end
</style>