import { uuid } from 'vue-uuid'
import { _ } from 'core-js';
export default {
    namespaced: true,
    state: {
        curd: {
            controllerName: "game",
            //搜索对象
            search: {
                state: false,
                vm: {
                    TableName: null,
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
            retableInfo:{
                state:false,
                tables:[]
            },
            rowsInfo:{
                state:false,
                tableID:'',
                columnsInfo:[]
            },
            rangTimeInfo:{
                state:false,
                rangTime:[]
            },
            //表单
            form: {
                state: false,
                vm: {
                    Model: {},
                    rangTime:[]
                    
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
        deleRow(context, par){
            var _curd = context.state.curd;
            console.log(par);
            for(var index in _curd.retableInfo.tables){
                   for(var index2 in _curd.retableInfo.tables[index].tableData){
                          if(_curd.retableInfo.tables[index].tableData[index2].rowID==par.rowID){
                            _curd.retableInfo.tables[index].tableData.splice(index2,1);
                            return;
                          }
                   }
            }
        },
        setRow(context, par){
            var _curd = context.state.curd;
            var row={}
            for(var index in _curd.rowsInfo.columnsInfo){
                row[_curd.rowsInfo.columnsInfo[index].name]=_curd.rowsInfo.columnsInfo[index].key;
            }
            row["rowID"]=uuid.v1();
            //找到对应的Table
           for(var index in _curd.retableInfo.tables){
               if(_curd.retableInfo.tables[index].tempID==par){
                _curd.retableInfo.tables[index].tableData.push(row);
               }
           }
           _curd.rowsInfo.state=false;
        },

        addTableRow(context, par){
            var _curd = context.state.curd;
            let dataInfo=[];
            for(let index in par.tableColumns){
                if(par.tableColumns[index].dataIndex!='action'){
                    dataInfo.push({
                        dIndex:index,
                        title:par.tableColumns[index].title,
                        name:par.tableColumns[index].key,
                        key:''
                    })
                }
                
            }
            _curd.rowsInfo.columnsInfo=dataInfo;
            _curd.rowsInfo.tableID=par.tempID
            _curd.rowsInfo.state=true;
        },
        deleteTable(context, par){
             console.log(par);
             var _curd = context.state.curd;
             for (const index in _curd.retableInfo.tables) {
                 console.log(index);
                 console.log(_curd.retableInfo.tables[index]);
                 console.log(par);
                if(_curd.retableInfo.tables[index].tempID==par.tempID){
                    _curd.retableInfo.tables.splice(index,1);
                    return;
                }
             }
        },
        //获取数据表格
        setTables(context, par){
            var _curd = context.state.curd;
            for (const index in _curd.retableInfo.tables) {
                 if(_curd.retableInfo.tables[index].tableID==par){
                    global.tools.msg("不能添加重复的奖励");
                    return;
                 }
            }
            if(par){
                global.get("/admin/tableinfo/getbyId/"+par).then(res=>{
                    
                    let columnObj=JSON.parse(res.data.data.Columns);
                    var colums=[];
                    for(let item of columnObj){
                        colums.push({
                            dataIndex:item.CloumnEnName,
                            title:item.CloumnName,
                            key:item.CloumnEnName
                        })
                    }
                    colums.push({
                        dataIndex:'action',
                        title:'操作',
                        scopedSlots: { customRender: 'action' },
                    })
                    var row={
                        tempID:uuid.v1(),
                        tableColumns:colums,
                        tableTitle:'',
                        tableData:[],
                        tableID:res.data.data.TableID,
                        fields:columnObj,
                        compareFields:''

                    };
                    _curd.retableInfo.tables.push(row);
                    _curd.retableInfo.state=true;

                })
            }
        },
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
                        {DataIndex:'GameTitle',Title:'游戏名称',Show:true},
                        {DataIndex:'GameType',Title:'游戏类型',Show:true},
                        {DataIndex:'StartTime',Title:'开始时间',Show:true},
                        {DataIndex:'EndTime',Title:'结束时间',Show:true},
                        {DataIndex:'GameCompany',Title:'游戏公司',Show:true},
                        {DataIndex:'Reward',Title:'最大奖励',Show:true},
                        {DataIndex:'Rebate',Title:'充值返利',Show:true},
                        {DataIndex:'Tags',Title:'标签',Show:true},
                        {DataIndex:'JoinNums',Title:'已加入的人数',Show:true},
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
                    if (data.Id == 0) {
                        _curd.form.vm.Id = 0
                    }else{
                        //处理列
                        //_curd.columnLst.columns=JSON.parse(_curd.form.vm.Model.Columns);
                        _curd.retableInfo.tables=[];
                        if(res.data.data.Model.GameID==0){
                            _curd.rangTimeInfo.rangTime=[];
                            _curd.retableInfo.tables=[];
                            _curd.form.vm.Model.ImageTitle='';
                            _curd.form.vm.Model.GameDetailBackgroundImage='';

                        }else{
                            _curd.rangTimeInfo.rangTime=[res.data.data.Model.StartTime,res.data.data.Model.EndTime];
                            let configs=res.data.data.confgis;
                        let tableConfigs =res.data.data.tableConfigs;
                        for(var index in configs){
                             let tbale ={};
                             for (const key in tableConfigs) {
                                 if(tableConfigs[key].TableID==configs[index].TableID){
                                    tbale=tableConfigs[key];
                                    break;
                                 }
                             }
                             let columnObj=JSON.parse(tbale.Columns);
                             var colums=[];
                                for(let item of columnObj){
                                    colums.push({
                                        dataIndex:item.CloumnEnName,
                                        title:item.CloumnName,
                                        key:item.CloumnEnName
                                    })
                                }
                            colums.push({
                                dataIndex:'action',
                                title:'操作',
                                scopedSlots: { customRender: 'action' },
                            })
                             var row ={
                                tempID:uuid.v1(),
                                tableTitle:configs[index].TableTitle,
                                tableData:JSON.parse(configs[index].TableContent),
                                tableColumns:colums,
                                tableID:configs[index].TableID,
                                fields:columnObj,
                                compareFields:configs[index].CompareFields,
                                gameID:configs[index].GameID
                             };
                             _curd.retableInfo.tables.push(row);
                        }
                    }
                       
                        
                        console.log(_curd.form.vm.rangTime);
                    }
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
            if (!_vm.Model.GameTitle) return global.tools.msg('游戏名称不能为空!', '错误');
            if (!_vm.Model.GameType) return global.tools.msg('请选择游戏类型!', '错误');
            if (!_vm.Model.StartTime) return global.tools.msg('选择试玩时间!', '错误');
            if (!_vm.Model.EndTime) return global.tools.msg('选择试玩时间!', '错误');

            if (!_vm.Model.Reward) return global.tools.msg('请输入总奖励!', '错误');
            
            if(_vm.Model.Rebate && !_vm.Model.RebateDesc){
                return global.tools.msg('请输入试玩说明!', '错误');
            }
            if (!_vm.Model.AllowMax) return global.tools.msg('请输入允许试玩的最大人数!', '错误');
            if (!_vm.Model.RegUserInfoInterface) return global.tools.msg('请输入用户信息接口地址!', '错误');
            if (!_vm.Model.GameCompany) return global.tools.msg('请输入游戏公司!', '错误');

            if (!_vm.Model.ImageTitle) return global.tools.msg('请上传标题图片!', '错误');
            if (!_vm.Model.GameDetailBackgroundImage) return global.tools.msg('请上传背景图片!', '错误');
            if (!_vm.Model.GameDesc) return global.tools.msg('请输入试玩说明!', '错误');
            
            //组装奖励信息
            if(_curd.retableInfo.tables.length<1){
                return global.tools.msg('请至少填写一个奖励信息!', '错误');
            }
            var gameTableConfig=[];
            for (const tIndex in _curd.retableInfo.tables) {
                let config={};
                config.TableID=_curd.retableInfo.tables[tIndex].tableID;
                config.TableContent=JSON.stringify(_curd.retableInfo.tables[tIndex].tableData);
                config.CompareFields=_curd.retableInfo.tables[tIndex].compareFields,
                config.TableTitle=_curd.retableInfo.tables[tIndex].tableTitle
                gameTableConfig.push(config);
            }
            //发送请求给接口
            global
                .post(`Admin/${_curd.controllerName}/Save`, { game:_vm.Model,configs:gameTableConfig}, true)
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
        setTime(context,par){
           console.log(par);
           var _curd = context.state.curd;
           _curd.form.vm.Model.StartTime=par[0];
           _curd.form.vm.Model.EndTime=par[1];
           _curd.rangTimeInfo.rangTime=par;
           _curd.rangTimeInfo.state=true;
        },
        setImage(context,par){
            var _curd = context.state.curd;
            if(par.ptype=="title"){
                _curd.form.vm.Model.ImageTitle=par.imageUrl;
            }
            else{
                _curd.form.vm.Model.GameDetailBackgroundImage=par.imageUrl;
            }
        }
        
    },
    getters: {}
}