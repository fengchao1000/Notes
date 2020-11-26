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
         
        
       
          <a-form :form="curd.form.vm" layout="horizontal" :label-col="{ span: 5 }" :wrapper-col="{ span: 15 }" hideRequiredMark>
              <a-collapse v-model="activeKey">
         <a-collapse-panel key="1" header="基础信息">
            <a-row :gutter="20">
              <a-col >
                <a-form-item label="游戏名称">
                  <a-input v-model="curd.form.vm.Model.GameTitle" placeholder="游戏名称" />
                </a-form-item>
              </a-col>
            </a-row>
            <a-row :gutter="20">
              <a-col >
                <a-form-item label="游戏注册地址">
                  <a-input v-model="curd.form.vm.Model.GameRegUrl" placeholder="http://www.game.com?tid={tid}&playerid={playerid}" />
                </a-form-item>
              </a-col>
            </a-row>
             <a-row :gutter="20">
              <a-col >
                <a-form-item label="游戏地址">
                  <a-input v-model="curd.form.vm.Model.GameUrl" placeholder="http://www.game.com?tid={tid}&playerid={playerid}" />
                </a-form-item>
              </a-col>
            </a-row>
            <a-row :gutter="20">
              <a-col >
                <a-form-item label="用户信息接口">
                  <a-input v-model="curd.form.vm.Model.RegUserInfoInterface" placeholder="http://www.game.com?tid={tid}&playerid={playerid}" />
                </a-form-item>
              </a-col>
            </a-row>
            <a-row :gutter="20">
              <a-col >
                <a-form-item label="游戏排名接口">
                  <a-input v-model="curd.form.vm.Model.RankInterface" placeholder="http://www.game.com?tid={tid}" />
                </a-form-item>
              </a-col>
            </a-row>
            <a-row :gutter="20">
              <a-col >
                <a-form-item label="游戏类型">
                   <a-select default-value="页游"  v-model="curd.form.vm.Model.GameType" >
                       <a-select-option value="H5">H5</a-select-option>
                       <a-select-option value="APP">APP</a-select-option>
                       <a-select-option value="页游">页游</a-select-option>
                       <a-select-option value="休闲游戏">休闲游戏</a-select-option>
                       <a-select-option value="其他">其他</a-select-option>
                   </a-select>
                </a-form-item>
              </a-col>
            </a-row>
            <a-row :gutter="20">
              <a-col >
                <a-form-item label="标签">
                   <a-select default-value="lucy"  v-model="curd.form.vm.Model.Tags" >
                       <a-select-option value="多服">H5</a-select-option>
                       <a-select-option value="专区">专区</a-select-option>
                       <a-select-option value="H5">H5</a-select-option>
                       <a-select-option value="APP">APP</a-select-option>
                   </a-select>
                </a-form-item>
              </a-col>
            </a-row>
           <a-row :gutter="20">
              <a-col >
                <a-form-item label="试玩时间">
                  <a-range-picker :value="curd.rangTimeInfo.rangTime"  :show-time="{ format: 'HH:mm:ss' }"  valueFormat="YYYY-MM-DD HH:mm:ss"  :placeholder="['开始时间', '结束时间']" @ok="setTime" />
                </a-form-item>
              </a-col>
            </a-row>
            <a-row :gutter="20">
              <a-col >
                <a-form-item label="充值返利">
                  <a-input v-model="curd.form.vm.Model.Rebate" placeholder="%" />
                </a-form-item>
              </a-col>
            </a-row>

            <a-row :gutter="20">
              <a-col >
                <a-form-item label="总奖励">
                  <a-input v-model="curd.form.vm.Model.Reward" placeholder="万" />
                </a-form-item>
              </a-col>
            </a-row>

             <a-row :gutter="20">
              <a-col >
                <a-form-item label="游戏供应商">
                  <a-input v-model="curd.form.vm.Model.GameCompany" placeholder="" />
                </a-form-item>
              </a-col>
            </a-row>
            <a-row :gutter="20">
              <a-col >
                <a-form-item label="最大试玩人数">
                  <a-input-number v-model="curd.form.vm.Model.AllowMax" placeholder="" />
                </a-form-item>
              </a-col>
            </a-row>
            <a-row :gutter="20">
              <a-col  :span="8" style="margin-left:100px;">
                <a-form-item label="标题图片" >
                  <a-upload  name="tiltImage"  list-type="picture-card" class="avatar-uploader" :show-upload-list="false"  action="http://47.99.152.236:8085/api/Upload/" :before-upload="beforeUpload" @change="handleChange"  >
                     <img v-if="curd.form.vm.Model.ImageTitle" :src="curd.form.vm.Model.ImageTitle" alt="avatar" style="width:150px;height:100px;" />
                      <div v-else>
                            <a-icon :type="loading ? 'loading' : 'plus'" />
                             <div class="ant-upload-text">
                                   上传
                       </div>
                   </div>
                  </a-upload>
                 </a-form-item>
            </a-col>
            <a-col  :span="10">
                <a-form-item label="详情页大图">
                  <a-upload  name="bgImage"  list-type="picture-card" class="avatar-uploader" :show-upload-list="false"  action="http://47.99.152.236:8085/api/Upload/" :before-upload="beforeUpload" @change="handleChange2"  >
                     <img v-if="curd.form.vm.Model.GameDetailBackgroundImage" :src="curd.form.vm.Model.GameDetailBackgroundImage" alt="avatar" style="width:150px;height:100px;" />
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
             <a-row :gutter="20">
              <a-col >
                <a-form-item label="试玩说明">
                    <template>
                     <div><quill-editor ref="myTextEditor" v-model="curd.form.vm.Model.GameDesc"  style="height:300px;"></quill-editor></div>
                  </template>   
                </a-form-item>
              </a-col>
            </a-row>
            <a-row>
                 <a-col >
                     <div style="height:70px;"></div>
                 </a-col>
            </a-row>
            <a-row :gutter="20">
              <a-col >
                <a-form-item label="充值返利说明">
                    <template>
                     <div><quill-editor ref="myTextEditor" v-model="curd.form.vm.Model.RebateDesc"  style="height:300px;"></quill-editor></div>
                     </template>
                </a-form-item>
              </a-col>
            </a-row>
            <a-row>
                 <a-col >
                     <div style="height:150px;"></div>
                 </a-col>
            </a-row>
             </a-collapse-panel>
           <a-collapse-panel key="2" header="奖励信息" :disabled="false">
             <a-row :gutter="20">
              <a-col >
                <a-form-item label="配置奖励">
                  <a-button type="primary" @click="tableVis=true">添加</a-button>
                </a-form-item>
              </a-col>
            </a-row>
            <div v-for="(item,index) in curd.retableInfo.tables" :key="index" style="border:1px solid #fafafa">
                <a-row :gutter="20">
                    <a-col >
                        <a-form-item label="奖励说明">
                            <a-input v-model="item.tableTitle" placeholder="无说明留空" />
                        </a-form-item>
                    </a-col>
                </a-row>
                 <a-row :gutter="20">
                    <a-col >
                        <a-form-item label="计算字段">
                             <a-select  style="width:500px;" v-model="item.compareFields">
                              <a-select-option v-for="(item2,index) in item.fields" :key="index" :value="item2.CloumnEnName">
                                  {{item2.CloumnName}}
                              </a-select-option>
                            </a-select>
                        </a-form-item>
                    </a-col>
                </a-row>
                <a-row :gutter="20">
                    <a-col :span="2" style="margin-left:150px;">
                       <a-button type="primary" @click="addTableRow(item)">添加行</a-button>
                    </a-col>
                    <a-col :span="15" >
                        <a-table :columns="item.tableColumns" :data-source="item.tableData" :pagination="false" :rowKey="record => record.rowID">
                            <span slot="action" slot-scope="text, record">
                                <a @click="deleRow(record)">删除</a>
                            </span>
                        </a-table>
                    </a-col>
                    <a-button type="danger" @click="deleteTable(item)">删除表格</a-button>
                </a-row>
            </div>
           </a-collapse-panel>
            </a-collapse>
          </a-form>
        </a-modal>
         <a-modal
          class="xs-w100"
          title="填写奖励"
          :width="800"
          :visible="curd.rowsInfo.state"
          @ok="setRow(curd.rowsInfo.tableID)"
          @cancel="curd.rowsInfo.state=false"
        >
         <a-row v-for="(item,index) in curd.rowsInfo.columnsInfo" :key="index" :gutter="20">
                  <a-col >
                        <a-form-item :label="item.title">
                            <a-input v-model="item.key"  />
                        </a-form-item>
                    </a-col>
         </a-row>
         </a-modal>
      </template>
    </CurdCom>
    <a-modal
          class="xs-w100"
          title="选择奖励"
          :width="800"
          :visible="tableVis"
          @ok="selectTable"
          @cancel="tableVis=false"
        >
        <a-row :gutter="20">
            <a-col :sapn="18">
                <a-form-item label="选择奖励类型">
                     <a-select  style="width:500px;" v-model="tableId">
                        <a-select-option v-for="(item,index) in tableInfos" :key="index" :value="item.TableID">
                            {{item.TableName}}
                        </a-select-option>
                 </a-select>
                </a-form-item>
                
            </a-col>
           
        </a-row>
    </a-modal>
  </div>
</template>

<script>
//vuex
import { mapState, mapMutations, mapActions } from "vuex";
var _controllerName = "Game";
//
import CurdCom from "../../components/base/curd";
import { uuid } from 'vue-uuid'

import 'quill/dist/quill.core.css'
import 'quill/dist/quill.snow.css'
import 'quill/dist/quill.bubble.css'
import { quillEditor } from 'vue-quill-editor'
function getBase64(img, callback) {
  const reader = new FileReader();
  reader.addEventListener('load', () => callback(reader.result));
  reader.readAsDataURL(img);
}
export default {
  name: _controllerName,
  data() {
    return {
      grid: 12,
      power: global.$power,
      loading: false,
      imageUrl: '',
      imageUrl2:'',
      loading2:false,
      tableInfos:[],
      tableVis:false,
      tableId:'',
      activeKey: ['1'],
      
    };
  },
  components: { CurdCom,quillEditor },
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
    this.getTables();
  },
  mounted() {},
  methods: {
    //获取数据
    ...mapActions(`vuex${_controllerName}`, {
      findList: "findList",
      loadForm: "loadForm",
      remove: "remove",
      save: "save",
      setTime:'setTime',
      setImage:'setImage',
      setTables:'setTables',
      addTableRow:'addTableRow',
      setRow:'setRow',
      deleteTable:'deleteTable',
      deleRow:'deleRow'
    }),
    ...mapMutations(`vuex${_controllerName}`, {
      //重置检索文本框
      resetSearch: "resetSearch",
      pageChange: "pageChange"
    }),
    selectTable(){
        this.setTables(this.tableId);
        this.tableVis=false;
    },
    getTables(){
        global.get("/admin/tableInfo/GetAll").then(res=>this.tableInfos=res.data.data);
    },
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
    handleChange2(info) {
      if (info.file.status === 'uploading') {
        this.loading2 = true;
        return;
      }
      console.log(info);
      if (info.file.status === 'done') {
        // Get this url from response in real world.
        getBase64(info.file.originFileObj, imageUrl => {
          this.imageUrl2 = imageUrl;
          this.loading2 = false;
          this.setImage({ptype:'backImage',imageUrl:info.file.response});
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
   
  },
  
};
</script>

