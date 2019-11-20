$(function () {

    //注意：js中的字段和方法都是驼峰命名法

    var l = abp.localization.getResource('Notes');//Notes来之哪里？

    var createModal = new abp.ModalManager(abp.appPath + 'Categorys/CreateModal');//Categorys对应文件夹或者命名空间名字
    var editModal = new abp.ModalManager(abp.appPath + 'Categorys/EditModal');

    //CategorysTable对应index.cshtml中的<abp-table striped-rows="true" id="CategorysTable">
    var dataTable = $('#CategorysTable').DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        autoWidth: false,
        scrollCollapse: true,
        order: [[1, "asc"]],
        ajax: abp.libs.datatables.createAjax(fC.notes.category.getList),//ajax请求，驼峰命名法
        //显示 "Actions" 下拉按钮
        columnDefs: [
            {
                rowAction: {
                    items:
                        [
                            {
                                text: l('Edit'),
                                action: function (data) {
                                    editModal.open({ id: data.record.id });//打开编辑用的 modal 对话框
                                }
                            },
                            {
                                text: l('Delete'),
                                confirmMessage: function (data) {
                                    return l('BookDeletionConfirmationMessage', data.record.name);
                                },
                                action: function (data) {
                                    fC.notes.category
                                        .delete(data.record.id)//删除ajax请求
                                        .then(function () {
                                            abp.notify.info(l('SuccessfullyDeleted'));
                                            dataTable.ajax.reload();
                                        });
                                }
                            }
                        ]
                }
            },
            { data: "name" },
            { data: "description" }
            //驼峰命名法
        ]
    }));
     

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    //对应Index.cshtml页面的<abp-button id="NewCategoryButton"
    $('#NewCategoryButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});