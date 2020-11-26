<template>
 <div class="page">
        <div class="login">
            <div class="loglef">
                <div class="logg"><img src="http://47.99.152.236:8081//wel.png"></div>
                <div class="logtex">合乐彩</div>
            </div>
            <div class="logrig">
                <div class="lortit">登  录</div>
                <div class="lorform">
                    <input type="text" maxlength="11" class="user" v-model="form.vm.UserName">
                    <input type="password" class="pass" autocomplete="new-password" v-model="form.vm.UserPassword">
                </div>
                <input type="button" value="登  录" class="log" @click="check">
            </div>
        </div>
  </div>
</template>

<script>
//vuex
import { mapState, mapActions } from "vuex";
var _controllerName = "Login";
//
export default {
  name: "Login",
  data() {
    return {
      UserName: "admin",
      UserPassword: "123456",
      LoginCode: ""
    };
  },
  //计算属性
  computed: {
    ...mapState(`vuex${_controllerName}`, {
      form: state => state.form
    })
  },
  mounted() {
    global.tools.delAuthorization();
  },
  methods: {
    //获取数据
    ...mapActions(`vuex${_controllerName}`, {
      check: "check"
    })
  }
};
</script>
<style>
.ant-spin-container{height: 100%;}
#app{height: 100%;}
       .page{min-height: 100%;width: 100%;box-sizing: border-box;padding-top: 1px;background:url('http://47.99.152.236:8081//bg.png') no-repeat center top;min-width: 960px;}
       .login{width:850px;margin: 0 auto;margin-top: 200px;}
       .loglef{width: 450px;height: 424px;background: #6e86b9 url('http://47.99.152.236:8081//welbg.png') no-repeat center;padding-top: 1px; float: left;
        border-radius:5px 0 0 5px;-webkit-border-radius:5px 0 0 5px;-ms-border-radius:5px 0 0 5px;-moz-border-radius:5px 0 0 5px;-o-border-radius:5px 0 0 5px;}
       .logg{width: 74px;margin: 0 auto;margin-top: 110px;}
       .logtex{font-size: 21px;color: #fff;text-align: center;margin-top: 50px;font-weight:bold;}
       .logrig{width: 400px;height: 424px;padding-top: 1px;background: #fff;float: left;
              border-radius: 0 5px 5px 0;-webkit-border-radius: 0 5px 5px 0;-moz-border-radius: 0 5px 5px 0;-ms-border-radius: 0 5px 5px 0;-o-border-radius: 0 5px 5px 0;}
       .lortit{margin-top: 74px;color:#6e86b9;text-align: center;font-size: 20px;font-weight: bold;}
       .lorform{width: 267px;margin: 0 auto;margin-top: 60px;}
       .lorform input{width: 267px;height: 42px; border: 0;border-bottom: solid 1px #b5c4e4;box-sizing: border-box;padding-left: 60px;color:#637aab;font-size: 16px;outline: none;}
       .user{background: url('http://47.99.152.236:8081/user.png')no-repeat 12px center;}
       .pass{background: url('http://47.99.152.236:8081/pass.png')no-repeat 12px center;margin-top: 22px;}
       .log{width: 267px;height: 36px;background: #6e86b9;border: 0;color:#fff;display: block; margin: 0 auto;margin-top: 50px;cursor: pointer;
            border-radius: 18px;-webkit-border-radius: 18px;-moz-border-radius: 18px;-ms-border-radius: 18px;-o-border-radius: 18px;}
        @media screen and (max-height:960px){
            .page{background-size: 100% 100%;}
        }
        @media screen and (max-height:768px) {
        .login{margin-top: 100px;}
        .page{background-size: 100%;}
       }
       @media screen and (max-height:600px) {
        .page{background-size: 100%;}
        .login{margin-top: 32px;}
        .login{width:600px;}
        .loglef{width:320px;height: 360px;}
        .logrig{width: 280px;height: 360px;}
        .logg{margin-top: 80px;}
        .logtit{font-size: 22px;}
        .logtex{font-size: 18px;}
        .lortit{margin-top: 50px;}
        .lorform{width: 200px;margin-top: 40px;}
        .user{background-size: 14px;}
        .pass{background-size: 14px;}
        .lorform input{width: 200px;}
        .log{width: 200px;height: 28px;}
       }
</style>


