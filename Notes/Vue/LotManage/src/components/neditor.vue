<template>
  <vue-neditor-wrap v-model="content" :config="config" :destroy="false" @ready="ready"></vue-neditor-wrap>
</template>
<script>
import VueNeditorWrap from "vue-neditor-wrap";
export default {
  name: "neditorCom",
  props: {
    text: String
  },
  components: {
    VueNeditorWrap
  },
  data() {
    return {
      config: {
        // 如果需要上传功能,找后端小伙伴要服务器接口地址
        serverUrl: "https://localhost:5001/Admin/Upload/Single",
        // 你的UEditor资源存放的路径,相对于打包后的index.html
        UEDITOR_HOME_URL: "/neditor/",
        // 编辑器不自动被内容撑高
        autoHeightEnabled: false,
        // 初始容器高度
        initialFrameHeight: 300,
        // 初始容器宽度
        initialFrameWidth: "100%",
        // 关闭自动保存
        enableAutoSave: false
      },
      content: this.text == null ? "<p>请输入内容!</p>" : this.text
    };
  },
  watch: {
    content(newV) {
      this.$emit("update:text", newV == null ? "<p>请输入内容!</p>" : newV);
    },
    text(newV) {
      this.content = newV == null ? "<p>请输入内容!</p>" : newV;
    }
  },
  mounted() {},
  methods: {
    ready(obj) {
      console.log("neditor-ready>>", obj);
    }
  }
};
</script>