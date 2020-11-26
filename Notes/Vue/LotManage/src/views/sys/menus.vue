<template>
  <div style="padding:20px;">
    <a-row :gutter="20">
      <a-col :xs="24" :sm="24" :md="10" :lg="6" :xl="5" class="pb-20">
        <a-card
          title="菜单树"
          :bodyStyle="{height:'calc(100vh - 220px)',overflow: 'hidden',overflowY: 'auto'}"
        >
          <a href="javascript:;" slot="extra" @click="resetSearch">查看一级</a>
          <a-tree
            checkable
            v-model="curd.tree.defaultCheckedKeys"
            :expandedKeys.sync="curd.tree.defaultExpandedKeys"
            :treeData="curd.tree.treeData"
            @select="(selectedKeys,info)=>onSelect({selectedKeys,info})"
          />
        </a-card>
      </a-col>
      <a-col :xs="24" :sm="24" :md="14" :lg="18" :xl="19" class="pb-20">
        <CurdCom
          :curd="curd"
          :selectedRowKeys.sync="curd.table.selectedRowKeys"
          @pageChange="pageChange"
        >
          <!-- 检索、 工具栏 -->
          <template slot="tools">
            <a-row :gutter="20">
              <a-col :xs="24" :sm="24" :md="12" :lg="6" :xl="6" class="pb-20">
                <a-input placeholder="请输入 菜单名称" v-model="curd.search.vm.Menu_Name" />
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
              <a-form :form="curd.form.vm" layout="vertical" hideRequiredMark>
                <a-row :gutter="20">
                  <a-col :span="grid">
                    <a-form-item label="编号">
                      <a-input v-model="curd.form.vm.Model.Menu_Num" placeholder="编号" />
                    </a-form-item>
                  </a-col>
                  <a-col :span="grid">
                    <a-form-item label="名称">
                      <a-input v-model="curd.form.vm.Model.Menu_Name" placeholder="名称" />
                    </a-form-item>
                  </a-col>
                </a-row>
                <a-row :gutter="20">
                  <a-col :span="grid">
                    <a-form-item label="页面路径">
                      <a-input v-model="curd.form.vm.Model.Menu_Url" placeholder="页面路径" />
                    </a-form-item>
                  </a-col>
                  <a-col :span="grid">
                    <a-form-item label="路由地址">
                      <a-input v-model="curd.form.vm.Model.Menu_RoutePath" placeholder="路由地址" />
                    </a-form-item>
                  </a-col>
                </a-row>
                <a-row :gutter="20">
                  <a-col :span="grid">
                    <a-form-item label="图标">
                      <a-input v-model="curd.form.vm.Model.Menu_Icon" placeholder="图标" />
                    </a-form-item>
                  </a-col>
                  <a-col :span="grid">
                    <a-form-item label="是否显示">
                      <a-radio-group v-model="curd.form.vm.Model.Menu_IsShow">
                        <a-radio :value="1">是</a-radio>
                        <a-radio :value="2">否</a-radio>
                      </a-radio-group>
                    </a-form-item>
                  </a-col>
                </a-row>
                <a-row :gutter="20">
                  <a-col :span="24">
                    <a-form-item label="功能">
                      <a-checkbox-group v-model="curd.form.vm.FunctionIds">
                        <a-checkbox
                          v-for="item in curd.form.vm.AllFunctionList"
                          :value="item.Function_ID"
                          :key="item.Function_ID"
                        >{{item.Function_Name}}({{item.Function_ByName}})</a-checkbox>
                      </a-checkbox-group>
                    </a-form-item>
                  </a-col>
                </a-row>
              </a-form>
            </a-modal>
          </template>
        </CurdCom>
      </a-col>
    </a-row>
  </div>
</template>

<script>
//vuex
import { mapState, mapMutations, mapActions } from "vuex";
var _controllerName = "Menus";
//
import CurdCom from "../../components/base/curd";

export default {
  name: _controllerName,
  data() {
    return {
      grid: 12,
      power: global.$power
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
    this.menuFunctionTree();
  },
  mounted() {},
  methods: {
    //获取数据
    ...mapActions(`vuex${_controllerName}`, {
      findList: "findList",
      loadForm: "loadForm",
      remove: "remove",
      save: "save",
      menuFunctionTree: "menuFunctionTree"
    }),
    ...mapMutations(`vuex${_controllerName}`, {
      //重置检索文本框
      resetSearch: "resetSearch",
      pageChange: "pageChange",
      onSelect: "onSelect"
    })
  }
};
</script>

