<template>
  <div style="padding:20px;">
    <CurdCom
      :curd="curd"
      :selectedRowKeys.sync="curd.table.selectedRowKeys"
      @pageChange="pageChange"
    >
      <!-- 检索、 工具栏 -->
      <template slot="tools">
        <a-row :gutter="20">
          <a-col :xs="24" :sm="24" :md="12" :lg="6" :xl="6" class="pb-20">
            <a-input placeholder="请输入表格名称" v-model="curd.search.vm.TableName" />
          </a-col>
          <a-col :xs="24" :sm="24" :md="12" :lg="6" :xl="6" class="pb-20" style="float:right">
            <a-button type="primary" class="mr-10" @click="findList">查询</a-button>
            <a-button class="mr-10" @click="resetSearch">重置/刷新</a-button>
            <template v-if="Object.keys(curd.search.vm).length>3">
              <a-button type="link" @click="curd.search.state=!curd.search.state">
                <div v-show="!curd.search.state">
                  <a-icon type="down" />展开
                </div>
                <div v-show="curd.search.state">
                  <a-icon type="up" />收起
                </div>
              </a-button>
            </template>
          </a-col>
        </a-row>
        <a-row :gutter="20">
          <a-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="pb-20">
            <a-button type="primary" class="mr-10" @click="loadForm()" v-if="power.Insert">新建</a-button>
            <a-popconfirm title="您确定要删除?" @confirm="remove()" okText="确定" cancelText="取消">
              <a-button type="danger" class="mr-10" v-if="power.Delete">批量删除</a-button>
            </a-popconfirm>
          </a-col>
        </a-row>
      </template>
      <!-- action 可以将 Slot = action 得删除 替换成自定义表格-->
      <template slot="action">
        <a-table-column title="操作" data-index="action">
          <template slot-scope="text, record">
            <div>
              <a href="javascript:;" @click="loadForm(record._ukid)" v-if="power.Update">编辑</a>
              <a-divider type="vertical" />
              <a-popconfirm
                title="您确定要删除?"
                @confirm="remove(record._ukid)"
                okText="确定"
                cancelText="取消"
              >
                <a href="javascript:;" class="text-danger" v-if="power.Delete">删除</a>
              </a-popconfirm>
            </div>
          </template>
        </a-table-column>
      </template>
      <template slot="form">
        <!-- 表单 -->
        <a-modal
          class="xs-w100"
          :title="curd.form.vm.Id?'编辑/查看':'新建'"
          :width="1000"
          :visible="curd.form.state"
          @ok="save"
          @cancel="curd.form.state=false"
        >
          <a-form :form="curd.form.vm" layout="horizontal" :label-col="{ span: 5 }" :wrapper-col="{ span: 12 }" hideRequiredMark>
            <a-row :gutter="20">
              <a-col >
                <a-form-item label="表格名称">
                  <a-input v-model="curd.form.vm.Model.TableName" placeholder="表格名称" />
                </a-form-item>
              </a-col>
            </a-row>
             <a-row :gutter="20">
              <a-col >
                <a-form-item label="表格类型">
                 <a-select default-value="页游"  v-model="curd.form.vm.Model.TableType" >
                       <a-select-option value="等级">等级</a-select-option>
                       <a-select-option value="战斗力">战斗力</a-select-option>
                       <a-select-option value="冲级赛">冲级赛</a-select-option>
                       <a-select-option value="任务">任务</a-select-option>
                   </a-select>
                </a-form-item>
              </a-col>
            </a-row>
             <a-row :gutter="20">
              <a-col >
                <a-form-item label="添加列">
                  <a-button type="primary" @click="columnVisb=true">新增</a-button>
                </a-form-item>
              </a-col>
            </a-row>
            <a-row :gutter="20">
               <a-table :columns="columns" :data-source="curd.columnLst.columns" :pagination="false" :rowKey="record => record.ColumnId" >
                  <span slot="action" slot-scope="text, record">
                    <a @click="deleRow(record)">删除</a>
                  </span>
               </a-table>
            </a-row>
          </a-form>
        </a-modal>
      </template>
    </CurdCom>
     <a-modal
          class="xs-w100"
          title="添加列"
          :width="500"
          :visible="columnVisb"
          @ok="addRow"
          @cancel="columnVisb=false"
        >
       <a-form  layout="horizontal" :label-col="{ span: 5 }" :wrapper-col="{ span: 12 }" hideRequiredMark>
             <a-row :gutter="20">
              <a-col >
                <a-form-item label="列名称">
                  <a-input v-model="columnName" placeholder="列名称" />
                </a-form-item>
              </a-col>
            </a-row>
             <a-row :gutter="20">
              <a-col >
                <a-form-item label="列英文名称">
                  <a-input v-model="columnEnName" placeholder="列英文名称" />
                </a-form-item>
              </a-col>
            </a-row>
       </a-form>

    </a-modal>
  </div>
</template>

<script>
//vuex
import { mapState, mapMutations, mapActions } from "vuex";
var _controllerName = "TableInfo";
//
import CurdCom from "../../components/base/curd";
import { uuid } from 'vue-uuid'

export default {
  name: _controllerName,
  data() {
    return {
      grid: 12,
      columnName:'',
      columnEnName:'',
      columnId:'',
      power: global.$power,
      columnVisb:false,
      columns:[
           {
              
               title: '列名称',
               dataIndex: 'CloumnName',
               key: 'CloumnName',
          },
          {
              
               title: '英文名称',
               dataIndex: 'CloumnEnName',
               key: 'CloumnEnName',
          },
          {
              
               title: '操作',
               key: 'action',
               scopedSlots: { customRender: 'action' },
          },
     ],
    };
  },
  components: { CurdCom },
  //计算属性
  computed: {
    ...mapState(`vuex${_controllerName}`, {
      //curd
      curd: state => state.curd
    })
  },
  created() {
    //加载数据列表
    this.findList();
  },
  mounted() {},
  methods: {
    //获取数据
    ...mapActions(`vuex${_controllerName}`, {
      findList: "findList",
      loadForm: "loadForm",
      remove: "remove",
      save: "save",
      addColumns:'addColumns',
      deleRow:'deleRow'
    }),
    ...mapMutations(`vuex${_controllerName}`, {
      //重置检索文本框
      resetSearch: "resetSearch",
      pageChange: "pageChange"
    }),
    addRow(){
    var row={
          ColumnId:uuid.v1(),
          CloumnName:this.columnName,
          CloumnEnName:this.columnEnName
    }
    this.addColumns(row);
    this.columnName='';
    this.columnEnName='';
    }
  },
  
};
</script>

