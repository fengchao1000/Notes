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
            <a-input placeholder="请输入 功能名称" v-model="curd.search.vm.Function_Name" />
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
      <!--重写 列-->
      <template slot="columns">
        <template v-for="(item) in curd.table.columns">
          <template v-if="item.Show && item.DataIndex=='Member_Photo'">
            <!-- 自定义头像 -->
            <a-table-column :title="item.Title" :data-index="item.DataIndex" :key="item.DataIndex">
              <template slot-scope="text, record">
                <div>
                  <a-avatar shape="square" :size="50" :src="record.Member_Photo" />
                </div>
              </template>
            </a-table-column>
          </template>
          <template v-else-if="item.Show">
            <a-table-column :title="item.Title" :data-index="item.DataIndex" :key="item.DataIndex" />
          </template>
        </template>
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
          style="top:10px;"
          :title="curd.form.vm.Id?'编辑/查看':'新建'"
          :width="1200"
          :visible="curd.form.state"
          @ok="save"
          @cancel="curd.form.state=false"
        >
          <a-form :form="curd.form.vm" layout="vertical" hideRequiredMark>
            <a-row :gutter="20">
              <a-col :span="grid">
                <a-form-item label="编号">
                  <a-input v-model="curd.form.vm.Model.Member_Num" placeholder="编号" type="number" />
                </a-form-item>
              </a-col>
              <a-col :span="grid">
                <a-form-item label="会员名称">
                  <a-input v-model="curd.form.vm.Model.Member_Name" placeholder="会员名称" />
                </a-form-item>
              </a-col>
              <a-col :span="grid">
                <a-form-item label="手机号码">
                  <a-input
                    v-model="curd.form.vm.Model.Member_Phone"
                    placeholder="手机号码"
                    type="number"
                  />
                </a-form-item>
              </a-col>
              <a-col :span="grid">
                <a-form-item label="生日">
                  <a-date-picker
                    class="w100"
                    format="YYYY-MM-DD"
                    v-model="curd.form.vm.Model.Member_Birthday"
                    @change="(date, dateString)=>curd.form.vm.Model.Member_Birthday=dateString"
                    placeholder="生日"
                  />
                </a-form-item>
              </a-col>
            </a-row>
            <a-row :gutter="20">
              <a-col :span="grid">
                <a-form-item label="性别">
                  <a-radio-group
                    name="radioGroup"
                    default-value="男"
                    v-model="curd.form.vm.Model.Member_Sex"
                  >
                    <a-radio value="男">男</a-radio>
                    <a-radio value="女">女</a-radio>
                  </a-radio-group>
                </a-form-item>
              </a-col>
              <a-col :span="grid">
                <a-form-item label="头像">
                  <input type="file" @change="photoChange" />
                  <a-avatar
                    shape="square"
                    v-if="curd.form.vm.Model.Member_Photo"
                    :size="100"
                    :src="curd.form.vm.Model.Member_Photo"
                  />
                </a-form-item>
              </a-col>
              <a-col :span="12">
                <a-form-item label="文件">
                  <input type="file" @change="fielsChange" multiple="multiple" />
                  <ul class="list-group" v-if="curd.form.vm.Model.Member_FilePath">
                    <li
                      class="list-group-item"
                      v-for="(item,index) in curd.form.vm.Model.Member_FilePath.split(',')"
                      :key="index"
                    >
                      <a v-if="curd.form.vm.Id" :href="item" target="_blank">{{item}}</a>
                      <a v-else href="javascript:void(0);">{{item}}</a>
                    </li>
                  </ul>
                </a-form-item>
              </a-col>
            </a-row>
            <a-row :gutter="20">
              <a-col :span="24">
                <a-form-item label="简介">
                  <NEditor :text.sync="curd.form.vm.Model.Member_Introduce" />
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
var _controllerName = "Member";
//
import CurdCom from "../../components/base/curd";
import NEditor from "../../components/neditor";
//时间格式化 插件
// import moment from "moment";
// import "moment/locale/zh-cn";

export default {
  name: _controllerName,
  data() {
    return {
      grid: 6,
      power: global.$power,
      guidEmpty: global.tools.guidEmpty
    };
  },
  components: { CurdCom, NEditor },
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
    window.console.log(this.power);
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
      pageChange: "pageChange",
      photoChange: "photoChange",
      fielsChange: "fielsChange"
    })
  }
};
</script>

