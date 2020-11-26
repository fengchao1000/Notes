<template>
  <a-spin :spinning="loading" tip="Loading..." size="large">
    <a-config-provider :locale="zh_CN">
      <div id="app">
        <router-view></router-view>
      </div>
    </a-config-provider>
  </a-spin>
</template>

<script>
import zh_CN from "ant-design-vue/lib/locale-provider/zh_CN";
import moment from "moment";
import "moment/locale/zh-cn";

moment.locale("zh-cn");
//vuex
import { mapState, mapMutations, mapActions } from "vuex";
//
export default {
  name: "app",
  data() {
    return {
      zh_CN
    };
  },
  computed: {
    ...mapState("vuexApp", {
      loading: state => state.loading
    })
  }
};
</script>

<style lang="less">
body {
  margin: 0;
  color: rgba(0, 0, 0, 0.65);
  font-size: 14px;
  font-family: -apple-system, BlinkMacSystemFont, Segoe UI, PingFang SC,
    Hiragino Sans GB, Microsoft YaHei, Helvetica Neue, Helvetica, Arial,
    sans-serif, Apple Color Emoji, Segoe UI Emoji, Segoe UI Symbol;
  font-variant: tabular-nums;
  line-height: 1.5;
  background-color: #fff;
  -webkit-font-feature-settings: "tnum";
  font-feature-settings: "tnum";
  width: 100%;
  height: 100%;
  .ant-spin-nested-loading {
    height: 100%;
    width: 100%;
  }
}
#app {
  height: 100%;
  width: 100%;
}
:global {
  //页面切换动画
  .page-toggle-enter-active {
    transition: all 0.2s ease-in 0.25s;
  }
  .page-toggle-leave-active {
    transition: all 0.2s ease-out 0s;
  }
  .page-toggle-enter,
  .page-toggle-leave-to {
    opacity: 0;
    padding: 0px;
  }
}
// // 滚动条
// ::-webkit-scrollbar {
//   width: 6px;
// }

// ::-webkit-scrollbar-corner,
// ::-webkit-scrollbar-track {
//   box-shadow: inset 0 0 1px rgba(0, 0, 0, 0.3);
//   -webkit-box-shadow: inset 0 0 1px rgba(0, 0, 0, 0.3);
//   background: transparent;
// }

// ::-webkit-scrollbar-thumb {
//   background-color: #dcdfe6;
// }
</style>