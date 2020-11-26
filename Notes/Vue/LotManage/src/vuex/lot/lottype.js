export default {
    namespaced: true,
    state: {
        curd: {
            controllerName: "lottype",
            //搜索对象
            search: {
                state: false,
                vm: {
                    CateName: null,
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
                    Model: {},
                    
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
                        {DataIndex:'ID',Title:'编号',Show:true},
                        {DataIndex:'TypeName',Title:'分类名称',Show:true},
                        {DataIndex:'IsHot',Title:'热门',Show:true},
                        {DataIndex:'CTime',Title:'创建时间',Show:true},
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
                    if (data.Id ==0) _curd.form.vm.Id = 0;
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
                    context.dispatch("findList");
                    global.tools.msg('操作成功!', '成功');
                });
        },
        //保存数据
        save(context, par) {
            var _curd = context.state.curd;
            window.console.log(par);
            //收集表单数据，并组装参数
            var _form = _curd.form;
            var _vm = _form.vm;
            //验证数据
            if (!_vm.Model.TypeName) return global.tools.msg('请输入类型!', '错误');
            //发送请求给接口
            global
                .post(`Admin/${_curd.controllerName}/Save`, _vm.Model, true)
                .then(res => {
                    var data = res.data.data;
                    window.console.log(data);
                    //刷新列表
                    context.dispatch("findList");
                    _form.state = false;
                    global.tools.msg('操作成功!', '成功');
                });
        },
        //导出Excel
        exportExcel(context, par) {
            window.console.log(par);
            var _curd = context.state.curd;
            //var _vm = _state.search.vm;
            var _vm={};
            var _parameter = {};
            for (var item in _vm) {
                _parameter[item] = _vm[item];
            }
            global.download(`Admin/${_curd.controllerName}/ExportExcel`, _parameter);
        },
        addColumns(context, par){
            window.console.log(par);
            var _curd = context.state.curd;
            console.log(_curd.form.ColumnLst);
            _curd.columnLst.columns.push(par);
            _curd.columnLst.state=true;
            
        },
        deleRow(context,par){
            window.console.log(par);
            var _curd = context.state.curd;
            let rowIndex=0;
            for (let index = 0; index < _curd.columnLst.columns.length; index++) {
                if (_curd.columnLst.columns[index].ColumnId==par.ColumnId) {
                    rowIndex=index;
                    break;
                }
            }
            _curd.columnLst.columns.splice(rowIndex,1);
            _curd.columnLst.state=true;
        },
        setImage(context,par){
            var _curd = context.state.curd;
            _curd.form.vm.Model.LogoUrl=par.imageUrl;
        }
    },
    getters: {}
}