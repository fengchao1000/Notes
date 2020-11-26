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
            <a-input placeholder="请输入 用户名" v-model="curd.search.vm.User_Name" />
          </a-col>
          <a-col :xs="24" :sm="24" :md="12" :lg="6" :xl="6" class="pb-20">
            <a-input placeholder="请输入 登录名" v-model="curd.search.vm.User_LoginName" />
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
                <a-form-item label="用户名">
                  <a-input v-model="curd.form.vm.Model.User_Name" placeholder="用户名" />
                </a-form-item>
              </a-col>
              <a-col :span="grid">
                <a-form-item label="登录名称">
                  <a-input v-model="curd.form.vm.Model.User_LoginName" placeholder="登录名称" />
                </a-form-item>
              </a-col>
            </a-row>
            <a-row :gutter="20">
              <a-col :span="grid">
                <a-form-item label="登录密码">
                  <a-input
                    v-model="curd.form.vm.Model.User_Pwd"
                    placeholder="登录密码"
                    type="password"
                  />
                </a-form-item>
              </a-col>
              <a-col :span="grid">
                <a-form-item label="邮箱地址">
                  <a-input v-model="curd.form.vm.Model.User_Email" placeholder="邮箱地址" />
                </a-form-item>
              </a-col>
            </a-row>
            <a-row :gutter="20">
              <a-col :span="grid">
                <a-form-item label="请选择角色">
                  <a-checkbox-group v-model="curd.form.vm.RoleIds">
                    <a-checkbox
                      v-for="item in curd.form.vm.AllRoleList"
                      :value="item.Role_ID"
                      :key="item.Role_ID"
                      :checked="item.checked"
                    >{{item.Role_Name}}</a-checkbox>
                  </a-checkbox-group>
                </a-form-item>
              </a-col>
            </a-row>
          </a-form>
        </a-modal>
      </template>
    </CurdCom>
  </div>
</template>

<script>
//vuex
import { mapState, mapMutations, mapActions } from "vuex";
var _controllerName = "User";
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
  },
  mounted() {},
  methods: {
    //获取数据
    ...mapActions(`vuex${_controllerName}`, {
      findList: "findList",
      loadForm: "loadForm",
      remove: "remove",
      save: "save"
    }),
    ...mapMutations(`vuex${_controllerName}`, {
      //重置检索文本框
      resetSearch: "resetSearch",
      pageChange: "pageChange"
    })
  }
};
</script>

