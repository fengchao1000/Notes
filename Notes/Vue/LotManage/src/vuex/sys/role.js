export default {
    namespaced: true,
    state: {
        curd: {
            controllerName: "Role",
            //搜索对象
            search: {
                state: false,
                vm: {
                    Role_Name: null
                }
            },
            //列表
            table: {
                loading: false,
                page: 1,
                rows: 10,
                totalCount: 0,
                columns: [],
                data: [],
                selectedRowKeys: []
            },
            //表单
            form: {
                state: false,
                vm: {
                    Model: {}
                }
            }
        }
    },
    mutations: {
        resetSearch(state, par) {
            var vm = state.curd.search.vm;
            for (var item in vm) vm[item] = null;
            this.dispatch(`vuex${state.curd.controllerName}/findList`);
        },
        pageChange(state, par) {
            var _curd = state.curd;
            _curd.table.page = par.page;
            _curd.table.rows = par.rows;
            this.dispatch(`vuex${state.curd.controllerName}/findList`);
        }
    },
    actions: {
        //获取数据表格
        findList(context, par) {
            var _curd = context.state.curd;
            _curd.table.loading = true;
            //收集表单数据，并组装参数
            var _search = _curd.search;
            var page = _curd.table.page;
            var rows = _curd.table.rows;
            //发送请求给接口
            global
                .post(`Admin/${_curd.controllerName}/FindList/${page}/${rows}`, _search.vm, false)
                .then(res => {
                    var data = res.data.data;
                    _curd.table.loading = false;
                    _curd.table.rows = data.Rows;
                    _curd.table.totalCount = data.TotalCount;
                    _curd.table.columns =[
                        {DataIndex:'Role_Num',Title:'角色编号',Show:true},
                        {DataIndex:'Role_Name',Title:'角色名称',Show:true},
                        {DataIndex:'Role_CreateTime',Title:'创建时间',Show:true},
                        {DataIndex:'_ukid',Title:'ID',Show:false},
                    ];
                    _curd.table.data = data.DataSource;
                });
        },
        //加载表单
        loadForm(context, par) {
            var _curd = context.state.curd;
            var Id = par;
            global
                .post(`Admin/${_curd.controllerName}/LoadForm${Id ? '/' + Id : ''}`, {}, true)
                .then(res => {
                    var data = res.data.data;
                    _curd.form.vm = data;
                    if (data.Id == global.tools.guidEmpty) _curd.form.vm.Id = null;
                    _curd.form.state = true;
                });
        },
        //删除数据
        remove(context, par) {
            var _curd = context.state.curd;
            //判断 par 如果null 则批量删除
            var _ukids = [];
            if (par) {
                _ukids.push(par);
            } else {
                var _selectedRowKeys = _curd.table.selectedRowKeys;
                _selectedRowKeys.forEach(item => _ukids.push(item));
            }

            if (_ukids.length == 0) return global.tools.msg('请选择要删除得项!', '警告');

            global
                .post(`Admin/${_curd.controllerName}/Delete`, _ukids, true)
                .then(res => {
                    var data = res.data.data;
                    //刷新列表
                    context.dispatch("findList");
                    global.tools.msg('操作成功!', '成功');
                });
        },
        //保存数据
        save(context, par) {
            var _curd = context.state.curd;
            //收集表单数据，并组装参数
            var _form = _curd.form;
            var _vm = _form.vm;
            //验证数据
            if (!_vm.Model.Role_Name) return global.tools.msg('角色名称不能为空!', '错误');
            //发送请求给接口
            global
                .post(`Admin/${_curd.controllerName}/Save`, _vm.Model, true)
                .then(res => {
                    var data = res.data.data;
                    //刷新列表
                    context.dispatch("findList");
                    _form.state = false;
                    global.tools.msg('操作成功!', '成功');
                });
        },
        //导出Excel
        exportExcel(context, par) {
            var _curd = context.state.curd;
            var _vm = _state.search.vm;
            var _parameter = {};
            for (var item in _vm) {
                _parameter[item] = _vm[item];
            }
            global.download(`Admin/${_curd.controllerName}/ExportExcel`, _parameter);
        },
    },
    getters: {}
}