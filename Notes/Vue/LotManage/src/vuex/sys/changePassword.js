export default {
    namespaced: true,
    state: {
        curd: {
            controllerName: "ChangePassword",
            //表单
            form: {
                vm: {
                    Model: {
                        OldPwd: null,
                        NewPwd: null,
                        QrPwd: null
                    }
                }
            }
        }
    },
    mutations: {

    },
    actions: {
        //更新密码
        updatePassword(context, par) {
            var _curd = context.state.curd;
            if (!_curd.form.vm.Model.OldPwd) return global.tools.msg("请输入旧密码", "错误");
            if (!_curd.form.vm.Model.NewPwd) return global.tools.msg("请输入新密码", "错误");
            if (!_curd.form.vm.Model.QrPwd) return global.tools.msg("请输入确认密码", "错误");
            if (_curd.form.vm.Model.NewPwd != _curd.form.vm.Model.QrPwd) return global.tools.msg("新密码和确认密码不一致!", "错误");
            //发送请求给接口
            global
                .post(`Admin/User/UpdatePassword`, _curd.form.vm.Model, true)
                .then(res => {
                    var data = res.data.data;
                    global.tools.msg('操作成功!', '成功');
                    for (let item in _curd.form.vm.Model) _curd.form.vm.Model[item] = null;
                });
        },
    },
    getters: {}
}