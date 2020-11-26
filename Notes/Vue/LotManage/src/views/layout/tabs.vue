<template>
  <div class="app-tabs">
    <a-tabs
      hideAdd
      :activeKey="$route.name"
      tabPosition="top"
      type="editable-card"
      :tabBarGutter="0"
      @edit="onCloseSelf"
      @tabClick="onTabClick"
    >
      <a-tab-pane v-for="(item) in tabsList" :key="item.key" :closable="item.close">
        <a-dropdown :trigger="['contextmenu']" slot="tab">
          <span style="user-select: none">{{item.title}}</span>
          <a-menu slot="overlay">
            <a-menu-item key="1" @click="onCloseSelf(item.key)">关闭当前</a-menu-item>
            <a-menu-item key="2" @click="onCloseOther(item.key)">关闭其他</a-menu-item>
            <a-menu-item key="3" @click="onCloseAll()">关闭全部</a-menu-item>
          </a-menu>
        </a-dropdown>
      </a-tab-pane>
    </a-tabs>
  </div>
</template>
<script>
export default {
  name: "app-layout-tabs",
  props: {
    tabsList: Array
  },
  data() {
    return {};
  },
  mounted() {},
  methods: {
    //关闭当前
    onCloseSelf(key) {
      this.$emit("onCloseSelf", key);
    },
    //关闭其他
    onCloseOther(key) {
      this.$emit("onCloseOther", key);
    },
    //关闭所有
    onCloseAll() {
      this.$emit("onCloseAll", null);
    },
    //点击切换选项卡
    onTabClick(key) {
      this.$emit("onTabClick", key);
    }
  }
};
</script>
<style lang="less" scope>
.app-tabs {
  background: #ffffff;
  .ant-tabs-bar {
    margin: 0 !important;
    border-bottom: 0 !important;
    .ant-tabs-tab {
      background: #ffffff !important;
      border: 0 !important;
      border-radius: 0 !important;
      padding: 0 30px !important;
      line-height: 39px !important;
    }
    .ant-tabs-tab.ant-tabs-tab-active {
      background: #f0f2f5 !important;
    }
  }
}
</style>