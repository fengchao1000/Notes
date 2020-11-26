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
            <a-input placeholder="请输入 ApiUrl" v-model="curd.search.vm.AppLog_Api" />
          </a-col>
          <a-col :xs="24" :sm="24" :md="12" :lg="6" :xl="6" class="pb-20">
            <a-input placeholder="请输入 IP" v-model="curd.search.vm.AppLog_IP" />
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
                <a-form-item label="ApiUrl">
                  <a-input
                    v-model="curd.form.vm.Model.AppLog_Api"
                    placeholder="ApiUrl"
                    :disabled="true"
                  />
                </a-form-item>
              </a-col>
              <a-col :span="grid">
                <a-form-item label="IP">
                  <a-input
                    v-model="curd.form.vm.Model.AppLog_IP"
                    placeholder="IP"
                    :disabled="true"
                  />
                </a-form-item>
              </a-col>
            </a-row>
            <a-row :gutter="20">
              <a-col :span="24">
                <a-form-item label="Body信息">
                  <a-textarea
                    placeholder="Body信息"
                    :autosize="{ minRows: 5, maxRows: 10 }"
                    v-model="curd.form.vm.Model.AppLog_FormBody"
                    :disabled="true"
                  />
                </a-form-item>
              </a-col>
            </a-row>
            <a-row :gutter="20">
              <a-col :span="grid">
                <a-form-item label="地址栏信息">
                  <a-textarea
                    placeholder="地址栏信息"
                    :autosize="{ minRows: 5, maxRows: 10 }"
                    v-model="curd.form.vm.Model.AppLog_QueryString"
                    :disabled="true"
                  />
                </a-form-item>
              </a-col>
              <a-col :span="grid">
                <a-form-item label="表单信息">
                  <a-textarea
                    placeholder="表单信息"
                    :autosize="{ minRows: 5, maxRows: 10 }"
                    v-model="curd.form.vm.Model.AppLog_Form"
                    :disabled="true"
                  />
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
var _controllerName = "AppLog";
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

