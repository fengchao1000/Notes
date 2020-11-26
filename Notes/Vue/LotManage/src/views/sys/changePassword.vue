<template>
  <div style="padding:20px;">
    <a-row :gutter="20">
      <a-col :offset="8" :xs="24" :sm="24" :md="8" :lg="8" :xl="8">
        <a-card>
          <a-form :form="curd.form.vm" layout="vertical" hideRequiredMark>
            <a-row :gutter="20">
              <a-col :span="grid">
                <a-form-item label="旧密码">
                  <a-input v-model="curd.form.vm.Model.OldPwd" type="password" placeholder="旧密码" />
                </a-form-item>
              </a-col>
              <a-col :span="grid">
                <a-form-item label="新密码">
                  <a-input v-model="curd.form.vm.Model.NewPwd" type="password" placeholder="新密码" />
                </a-form-item>
              </a-col>
              <a-col :span="grid">
                <a-form-item label="确认密码">
                  <a-input v-model="curd.form.vm.Model.QrPwd" type="password" placeholder="确认密码" />
                </a-form-item>
              </a-col>
            </a-row>
            <a-row :gutter="20">
              <a-col :span="grid">
                <a-button type="primary" block @click="updatePassword">保存</a-button>
              </a-col>
            </a-row>
          </a-form>
        </a-card>
      </a-col>
    </a-row>
  </div>
</template>

<script>
//vuex
import { mapState, mapMutations, mapActions } from "vuex";
var _controllerName = "ChangePassword";
//
import CurdCom from "../../components/base/curd";

export default {
  name: _controllerName,
  data() {
    return {
      grid: 24,
      power: global.$power
    };
  },
  //计算属性
  computed: {
    ...mapState(`vuex${_controllerName}`, {
      //curd
      curd: state => state.curd
    })
  },
  created() {},
  mounted() {},
  methods: {
    //获取数据
    ...mapActions(`vuex${_controllerName}`, {
      updatePassword: "updatePassword"
    })
  }
};
</script>

