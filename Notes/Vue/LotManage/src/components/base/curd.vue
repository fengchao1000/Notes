<template>
  <div>
    <a-card :bordered="false">
      <!--表单信息区域 插槽-->
      <slot name="tools"></slot>
      <!--表单信息区域 插槽-->
      <!-- :columns="curd.table.columns" -->
      <slot>
        <a-table
          :dataSource="curd.table.data"
          :loading="curd.table.loading"
          :pagination="false"
          :rowSelection="{selectedRowKeys: selectedRowKeys, onChange: onSelectChange}"
          :rowKey="record => record._ukid"
          size="middle"
        >
          <slot name="columns">
            <template v-for="(item) in curd.table.columns">
              <template v-if="item.Show">
                <a-table-column
                  :title="item.Title"
                  :data-index="item.DataIndex"
                  :key="item.DataIndex"
                />
              </template>
            </template>
          </slot>
          <slot name="action"></slot>
        </a-table>
      </slot>
      <a-pagination
        class="pt-20 text-right"
        size="small"
        showSizeChanger
        showQuickJumper
        :pageSizeOptions="pageSizeOptions"
        :total="curd.table.totalCount"
        :defaultCurrent="curd.table.page"
        :pageSize="curd.table.rows"
        :showTotal="total => `共计 ${total} 条`"
        @showSizeChange="onShowSizeChange"
        @change="onChange"
      >
        <!-- <template slot="buildOptionText" slot-scope="props">
            <span v-if="props.value!=='50'">{{props.value}}条/页</span>
            <span v-if="props.value==='50'">全部</span>
        </template>-->
      </a-pagination>
    </a-card>
    <!--表单信息区域 插槽-->
    <slot name="form"></slot>
  </div>
</template>

<script>
export default {
  name: "CurdCom",
  props: {
    curd: Object,
    selectedRowKeys: Array,
    pageChange: Function
  },
  data() {
    return {
      pageSizeOptions: ["10", "20", "30", "40", "50", "100", "1000"]
    };
  },
  mounted() {},
  methods: {
    onChange(page, rows) {
      page = page == 0 ? 1 : page;
      this.$emit("pageChange", { page, rows });
    },
    onShowSizeChange(page, rows) {
      page = page == 0 ? 1 : page;
      this.$emit("pageChange", { page, rows });
    },
    onSelectChange(selectedRowKeys) {
      this.$emit("update:selectedRowKeys", selectedRowKeys);
    }
  }
};
</script>

