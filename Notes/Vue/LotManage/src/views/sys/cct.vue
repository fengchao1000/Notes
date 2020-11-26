<template>
  <div class="cct-container">
    <div class="left">
      <ul>
        <li
          v-for="(item,index) in tableNames"
          :key="index"
          @click="eventSelectedTableName(item)"
          :class="{'active':selectedTableName==item}"
        >{{item}}</li>
      </ul>
    </div>
    <div class="center">
      <ul class="mb-10">
        <li
          v-for="(item,index) in tableFields"
          :key="index"
          @click="eventSelectedField(item.ColName)"
        >
          <div class="col-1">{{item.ColName}}</div>
          <div class="col-2">
            <a-divider type="vertical" />
            {{item.ColType}}
          </div>
          <div class="col-3">
            <a-divider type="vertical" />
            {{item.ColRemark}}
            <a-tag color="pink" v-if="item.ColIsKey==1" class="ml-10">主键</a-tag>
          </div>
        </li>
      </ul>
      <template v-for="tag in selectedField">
        <a-tag
          color="purple"
          :key="tag"
          closable
          @close="eventCloseSelectedField(tag)"
          class="mr-20 mb-20"
        >{{tag}}</a-tag>
      </template>
    </div>
    <div class="right">
      <div class="top">
        <ul>
          <li
            @click="eventSelectedCodeType('Model')"
            :class="{'active':selectedCodeType=='Model'}"
          >Model</li>
          <li
            @click="eventSelectedCodeType('RegisterModel')"
            :class="{'active':selectedCodeType=='RegisterModel'}"
          >RegisterModel</li>
          <li
            @click="eventSelectedCodeType('Logic')"
            :class="{'active':selectedCodeType=='Logic'}"
          >Logic</li>
          <li
            @click="eventSelectedCodeType('Controller')"
            :class="{'active':selectedCodeType=='Controller'}"
          >Controller</li>
          <li
            @click="eventSelectedCodeType('Form')"
            :class="{'active':selectedCodeType=='Form'}"
          >Form</li>
        </ul>
      </div>
      <div class="content">
        <!-- <el-card class="box-card mb-10" v-show="selectedCodeType=='Form'">
          <div slot="header" class="clearfix">
            <span>已选择 表 和 字段</span>
            <el-button style="float: right;" type="success" @click="getFormCode">运行</el-button>
          </div>
          <template v-for="tag in selectedField">
            <el-tag
              :key="tag"
              closable
              effect="dark"
              @close="eventCloseSelectedField(tag)"
              type="success"
              class="mr-20 mb-20"
            >{{tag}}</el-tag>
          </template>
        </el-card>-->
        <a-textarea placeholder="code" :rows="4" v-model="codeArea" />
      </div>
      <div class="bottom" v-show="selectedCodeType!='RegisterModel'">
        <ul>
          <li @click="download">下载当前</li>
          <li @click="downloadAll">下载所有</li>
        </ul>
      </div>
    </div>
  </div>
</template>
<script>
import "../../css/cct.less";

export default {
  name: "CCT",
  data() {
    return {
      codeArea: "",
      selectedTableName: "",
      selectedCodeType: "",
      selectedField: [],
      tableNameAndFields: {},
      tableNames: [],
      tableFields: []
    };
  },
  created() {
    this.getTableNameAndFields(() => {
      this.selectedCodeType = "Model";
      this.getModelCode();
    });
  },
  mounted() {},
  methods: {
    eventSelectedTableName(text) {
      this.selectedTableName = text;
      this.tableFields = this.tableNameAndFields[text];
      if (this.selectedCodeType == "Model") {
        this.getModelCode();
      }
      if (this.selectedCodeType == "RegisterModel") {
        this.getDbSetCode();
      }
      if (this.selectedCodeType == "Logic") {
        this.getLogicCode();
      }
      if (this.selectedCodeType == "Controller") {
        this.getControllersCode();
      }
      if (this.selectedCodeType == "Form") {
        this.getFormCode();
      }
    },
    //选中字段
    eventSelectedField(text) {
      var item = `${this.selectedTableName}/${text}`;
      var any = this.selectedField.find(w => w == item);
      if (any) return;
      this.selectedField.push(item);
    },
    //关闭选中字段
    eventCloseSelectedField(text) {
      var index = this.selectedField.indexOf(text);
      this.selectedField.splice(index, 1);
    },
    //选择代码类型
    eventSelectedCodeType(text) {
      this.selectedCodeType = text;
      if (this.selectedCodeType == "Model") {
        this.getModelCode();
      }
      if (this.selectedCodeType == "RegisterModel") {
        this.getDbSetCode();
      }
      if (this.selectedCodeType == "Logic") {
        this.getLogicCode();
      }
      if (this.selectedCodeType == "Controller") {
        this.getControllersCode();
      }
      if (this.selectedCodeType == "Form") {
        this.getFormCode();
      }
    },
    //获取所有的 表名 及对应的 字段
    getTableNameAndFields(call) {
      global.post("/Admin/CCT/GetTableNameAndFields").then(res => {
        var data = res.data;
        this.tableNameAndFields = data.data;
        for (var item in this.tableNameAndFields) {
          this.tableNames.push(item);
        }
        this.eventSelectedTableName(this.tableNames[0]);
        if (call) call();
      });
    },
    //获取 Model 代码
    getModelCode() {
      global
        .post(`/Admin/CCT/GetModelCode/${this.selectedTableName}`)
        .then(res => {
          var data = res.data;
          this.codeArea = data.data;
        });
    },
    //获取 注册表 代码
    getDbSetCode() {
      global.post("/Admin/CCT/GetDbSetCode").then(res => {
        var data = res.data;
        this.codeArea = data.data;
      });
    },
    //获取 Logic 代码
    getLogicCode() {
      global
        .post(`/Admin/CCT/GetLogicCode/${this.selectedTableName}`)
        .then(res => {
          var data = res.data;
          this.codeArea = data.data;
        });
    },
    //获取 Controllers 代码
    getControllersCode() {
      global
        .post(`/Admin/CCT/GetControllersCode/${this.selectedTableName}`)
        .then(res => {
          var data = res.data;
          this.codeArea = data.data;
        });
    },
    //获取 Form 代码
    getFormCode() {
      global
        .post(
          `/Admin/CCT/GetFormCode/${this.selectedTableName}`,
          this.selectedField
        )
        .then(res => {
          var data = res.data;
          this.codeArea = data.data;
        });
    },
    //下载当前
    download() {
      global.download("/Admin/CCT/Download", {
        TableName: this.selectedTableName,
        CodeType: this.selectedCodeType,
        Content: this.codeArea
      });
    },
    //下载所有代码文件
    downloadAll() {
      global.download(`/Admin/CCT/DownloadAll/${this.selectedCodeType}`);
    }
  }
};
</script>