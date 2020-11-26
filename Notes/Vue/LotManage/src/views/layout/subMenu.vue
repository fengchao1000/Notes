<template functional>
  <a-sub-menu :key="props.menuInfo.id">
    <span slot="title">
        <template v-if="props.menuInfo.icon">
          <a-icon :type="props.menuInfo.icon" />
        </template>
      <span>{{ props.menuInfo.name }}</span>
    </span>
    <template v-for="item in props.menuInfo.children">
      <a-menu-item v-if="item.children.length==0" :key="item.id">
        <template v-if="item.icon">
          <a-icon :type="item.icon" />
        </template>
        <span>{{ item.name }}</span>
      </a-menu-item>
      <sub-menu v-else :menu-info="item" :key="item.id" />
    </template>
  </a-sub-menu>
</template>
<script>
export default {
  props: ["menuInfo"]
};
</script>