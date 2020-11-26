export default {
    namespaced: true,
    state: {
        form: {
            vm: {
                UserName: "admin",
                UserPassword: "123456",
                LoginCode: ""
            }
        }

    },
    mutations: {

    },
    actions: {
        check(context, par) {
            var state = context.state;
            if (!state.form.vm.UserName) return global.tools.notice("用户名不能为空!");
            if (!state.form.vm.UserPassword) return global.tools.notice("密码不能为空!");
            global
                .post("Admin/Authorization/Check", state.form.vm, true)
                .then(res => {
                    var data = res.data.data;
                    global.tools.setAuthorization(data.token);
                    global.$router.push("/");
                });
        }
    },
    getters: {}
}