import tools from '../../js/tools'

export default {
    namespaced: true,
    state: {
        curd: {
            controllerName: "Menus",
            //搜索对象
            search: {
                state: false,
                vm: {
                    Menu_Name: null,
                    Menu_ParentID: tools.guidEmpty
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
            },
            tree: {}
        }
    },
    mutations: {
        resetSearch(state, par) {
            var vm = state.curd.search.vm;
            for (var item in vm) vm[item] = null;
            vm.Menu_ParentID = tools.guidEmpty;
            this.dispatch(`vuex${state.curd.controllerName}/findList`);
        },
        pageChange(state, par) {
            var _curd = state.curd;
            _curd.table.page = par.page;
            _curd.table.rows = par.rows;
            this.dispatch(`vuex${state.curd.controllerName}/findList`);
        },
        onSelect(state, par) {
            var _curd = state.curd;
            _curd.search.vm.Menu_ParentID = par.selectedKeys[0];
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
                    _curd.table.columns = [
                        {DataIndex:'Menu_Num',Title:'编号',Show:true},
                        {DataIndex:'Menu_Name',Title:'菜单名称',Show:true},
                        {DataIndex:'Menu_IsShow',Title:'显示',Show:true},
                        {DataIndex:'Menu_Url',Title:'路径',Show:true},
                        {DataIndex:'父级菜单',Title:'父级菜单',Show:true},
                        {DataIndex:'Menu_Icon',Title:'图标',Show:true},
                        {DataIndex:'Menu_CreateTime',Title:'创建时间',Show:true},
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
                    context.dispatch("menuFunctionTree");
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
            if (!_vm.Model.Menu_Name) return global.tools.msg('菜单名称不能为空!', '错误');
            _vm.Model.Menu_ParentID = _curd.search.vm.Menu_ParentID;
            //发送请求给接口
            global
                .post(`Admin/${_curd.controllerName}/Save`, _vm, true)
                .then(res => {
                    var data = res.data.data;
                    //刷新列表
                    context.dispatch("findList");
                    context.dispatch("menuFunctionTree");
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
        //获取菜单功能树
        menuFunctionTree(context, par) {
            var _curd = context.state.curd;
            //发送请求给接口
            global
                .post(`Admin/${_curd.controllerName}/MenuFunctionTree`, {}, false)
                .then(res => {
                    var data = res.data.data;
                    _curd.tree = data;
                });
        },
    },
    getters: {}
}