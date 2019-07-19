$(function () {

    var l = abp.localization.getResource('Notes');//Notes来之哪里？

    var createModal = new abp.ModalManager(abp.appPath + 'Bookmarks/CreateModal');//Bookmarks对应文件夹或者命名空间名字
    var editModal = new abp.ModalManager(abp.appPath + 'Bookmarks/EditModal');

    //BookmarksTable对应index.cshtml中的<abp-table striped-rows="true" id="BookmarksTable">
    var dataTable = $('#BookmarksTable').DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        autoWidth: false,
        scrollCollapse: true,
        order: [[1, "asc"]],
        ajax: abp.libs.datatables.createAjax(fC.notes.bookmark.getList),//ajax请求
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
                                    fC.notes.bookmark
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
            { data: "title" },
            { data: "summary" },
            { data: "linkUrl" }  
        ]
    }));
     

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    //对应Index.cshtml页面的<abp-button id="NewBookmarkButton"
    $('#NewBookmarkButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});