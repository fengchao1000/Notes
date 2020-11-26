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
            <a-input placeholder="请输入分类名称" v-model="curd.search.vm.CateName" />
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
          <template v-if="item.Show && item.DataIndex=='IsHot'">
            <!-- 自定义头像 -->
            <a-table-column :title="item.Title" :data-index="item.DataIndex" :key="item.DataIndex">
              <!-- <template slot-scope="text, record">
                <div>
                  <a-avatar shape="square" :size="50" :src="record.Member_Photo" />
                </div>
              </template> -->
              <template slot-scope="text, record">
                 <div>
                     <span v-if="record.IsHot" style="color:red">是</span>
                     <span v-if="!record.IsHot">否</span>
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
          <a-form :form="curd.form.vm" layout="horizontal" :label-col="{ span: 5 }" :wrapper-col="{ span: 12 }" hideRequiredMark :selfUpdate="true">
            <a-row :gutter="20">
              <a-col >
                <a-form-item label="分类名称">
                  <a-input v-model="curd.form.vm.Model.TypeName" placeholder="分类名称" />
                </a-form-item>
              </a-col>
            </a-row>
             <a-row :gutter="20">
              
            </a-row>
             <a-row :gutter="20">
              <a-col >
                <a-form-item label="是否为热门">
                    <a-switch :defaultChecked="curd.form.vm.Model.IsHot" v-model="curd.form.vm.Model.IsHot"/>
                </a-form-item>
              </a-col>
            </a-row>
            <a-row :gutter="20">
              <a-col  :span="8" style="margin-left:150px;">
                <a-form-item label="标题图片" >
                  <a-upload  name="tiltImage"  list-type="picture-card" class="avatar-uploader" :show-upload-list="false"  action="http://47.99.152.236:8085/api/Upload/" :before-upload="beforeUpload" @change="handleChange"  >
                     <img v-if="curd.form.vm.Model.LogoUrl" :src="curd.form.vm.Model.LogoUrl" alt="avatar" style="width:150px;height:100px;" />
                      <div v-else>
                            <a-icon :type="loading ? 'loading' : 'plus'" />
                             <div class="ant-upload-text">
                                   上传
                       </div>
                     </div>
                  </a-upload>
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
var _controllerName = "LotType";
//
import CurdCom from "../../components/base/curd";
function getBase64(img, callback) {
  const reader = new FileReader();
  reader.addEventListener('load', () => callback(reader.result));
  reader.readAsDataURL(img);
}
export default {
  name: _controllerName,
  data() {
    return {
      grid: 6,
      power: global.$power,
      guidEmpty: global.tools.guidEmpty,
      loading: false,
    };
  },
  components: { CurdCom},
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
      save: "save",
      setImage:'setImage',
    }),
    ...mapMutations(`vuex${_controllerName}`, {
      //重置检索文本框
      resetSearch: "resetSearch",
      pageChange: "pageChange",
      photoChange: "photoChange",
      fielsChange: "fielsChange"
    }),
    handleChange(info) {
      if (info.file.status === 'uploading') {
        this.loading = true;
        return;
      }
      console.log(info);
      if (info.file.status === 'done') {
        // Get this url from response in real world.
        getBase64(info.file.originFileObj, imageUrl => {
          this.imageUrl = imageUrl;
          this.loading = false;
          this.setImage({ptype:'title',imageUrl:info.file.response});
        });
      }
    },
    beforeUpload(file) {
      const isJpgOrPng = file.type === 'image/jpeg' || file.type === 'image/png';
      if (!isJpgOrPng) {
        this.$message.error('You can only upload JPG file!');
      }
      const isLt2M = file.size / 1024 / 1024 < 2;
      if (!isLt2M) {
        this.$message.error('Image must smaller than 2MB!');
      }
      return isJpgOrPng && isLt2M;
    },
    
  }
};
</script>

