<template>
  <!-- <a-menu
      :defaultSelectedKeys="['1']"
      :defaultOpenKeys="['2']"
      mode="inline"
      theme="dark"
      :inlineCollapsed="collapsed"
    >
      <template v-for="item in list">
        <a-menu-item v-if="!item.children" :key="item.key">
          <a-icon type="pie-chart" />
          <span>{{item.title}}</span>
        </a-menu-item>
        <sub-menu v-else :menu-info="item" :key="item.key" />
      </template>
    </a-menu>
  -->
  <!-- :defaultOpenKeys="[$route.meta.pid]" -->
  <a-menu
    mode="inline"
    :defaultSelectedKeys="[$route.name]"
    :selectedKeys="[$route.name]"
    
    :[openkeysName].sync="keys"
    :theme="theme"
    :inlineCollapsed="(isMobile)?false:siderCollapsedState"
    @select="onMenuSelected"
  >
    <template v-for="item in data">
      <a-menu-item v-if="item.children.length==0" :key="item.id">
        <template v-if="item.icon">
          <!-- <component v-bind:is="item.icon"></component> -->
          <a-icon :type="item.icon" />
        </template>
        <span>{{item.name}}</span>
      </a-menu-item>
      <sub-menu v-else :menu-info="item" :key="item.id" />
    </template>
  </a-menu>
</template>

<script>
import SubMenu from "./subMenu";

export default {
  name: "app-layout-menus",
  props: {
    isMobile: Boolean,
    siderCollapsedState: Boolean,
    theme: String,
    data: Array
  },
  data() {
    return {
      keys: [this.$route.meta.pid],
      openkeysName: ""
    };
  },
  watch: {
    $route(newV) {
      this.keys = [newV.meta.pid];
    },
    siderCollapsedState(newV) {
      if (!this.isMobile && this.siderCollapsedState) this.openkeysName = "";
      else this.openkeysName = "";
    }
  },
  components: {
    "sub-menu": SubMenu
  },
  created() {
    console.log("route-pid", this.$route.meta.pid);
  },
  mounted() {},
  methods: {
    //菜单选中
    onMenuSelected(obj) {
      //菜单选中 事件
      this.$emit("onMenuSelected", obj);
      this.$router.push({ name: obj.key });
    }
  }
};
</script>
<style lang="less" scoped>
.ant-layout-sider {
  .ant-menu {
    border-right: 0px;
    // height: 100%;
  }
}
</style>