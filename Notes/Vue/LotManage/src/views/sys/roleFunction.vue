<template>
  <div style="padding:20px;">
    <a-row :gutter="20">
      <a-col :span="12">
        <CurdCom
          :curd="curd"
          :selectedRowKeys.sync="curd.table.selectedRowKeys"
          @pageChange="pageChange"
        >
          <!-- 检索、 工具栏 -->
          <template slot="tools">
            <a-row :gutter="20">
              <a-col :xs="24" :sm="24" :md="12" :lg="8" :xl="8" class="pb-20">
                <a-input placeholder="请输入 角色名称" v-model="curd.search.vm.Role_Name" />
              </a-col>
              <a-col :xs="24" :sm="24" :md="12" :lg="8" :xl="8" class="pb-20" style="float:right">
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
          </template>
          <!-- action 可以将 Slot = action 得删除 替换成自定义表格-->
          <template slot="action">
            <a-table-column title="操作" data-index="action">
              <template slot-scope="text, record">
                <div>
                  <a href="javascript:;" @click="roleMenuFunctionTree(record._ukid)">去设置</a>
                </div>
              </template>
            </a-table-column>
          </template>
        </CurdCom>
      </a-col>
      <a-col :span="12">
        <a-card>
          <a-table
            rowKey="id"
            :columns="columns"
            :dataSource="curd.tree.vm.list"
            :pagination="false"
          >
            <div slot="func" slot-scope="text, record">
              <a-checkbox-group
                @change="values=>onChange({values,id:record.id})"
                v-model="record.checkFunction"
                style="display: block;"
              >
                <a-row>
                  <a-col :span="6" v-for="item in record.functions" :key="item.id">
                    <a-checkbox :value="item.id">{{item.label}}</a-checkbox>
                  </a-col>
                </a-row>
              </a-checkbox-group>
            </div>
          </a-table>
        </a-card>
      </a-col>
    </a-row>
  </div>
</template>

<script>
//
const columns = [
  {
    title: "菜单",
    dataIndex: "label",
    key: "label",
    width: "30%"
  },
  {
    title: "权限",
    dataIndex: "func",
    key: "func",
    width: "70%",
    scopedSlots: { customRender: "func" }
  }
];

//vuex
import { mapState, mapMutations, mapActions } from "vuex";
var _controllerName = "RoleFunction";
//
import CurdCom from "../../components/base/curd";

export default {
  name: _controllerName,
  data() {
    return {
      grid: 24,
      power: global.$power,
      columns
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
      roleMenuFunctionTree: "roleMenuFunctionTree"
    }),
    ...mapMutations(`vuex${_controllerName}`, {
      //重置检索文本框
      resetSearch: "resetSearch",
      pageChange: "pageChange",
      onChange: "onChange",
      expand: "expand"
    })
  }
};
</script>

