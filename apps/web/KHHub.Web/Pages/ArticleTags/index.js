$(function () {
    var l = abp.localization.getResource('MasterDataService');

    var articleTagService = window.kHHub.masterDataService.services.articleTags.articleTags;

    var createModal = new abp.ModalManager({
        viewUrl: abp.appPath + 'ArticleTags/CreateModal',
        scriptUrl: abp.appPath + 'Pages/ArticleTags/createModal.js',
        modalClass: 'articleTagCreate',
    });

    var editModal = new abp.ModalManager({
        viewUrl: abp.appPath + 'ArticleTags/EditModal',
        scriptUrl: abp.appPath + 'Pages/ArticleTags/editModal.js',
        modalClass: 'articleTagEdit',
    });

    var getFilter = function () {
        return {
            filterText: $('#FilterText').val(),
        };
    };

    var canEdit = abp.auth.isGranted('MasterDataService.ArticleTags.Edit');
    var canDelete = abp.auth.isGranted('MasterDataService.ArticleTags.Delete');

    var dataTable = $('#ArticleTagsTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            processing: true,
            serverSide: true,
            paging: true,
            searching: false,
            responsive: true,
            order: [[4, 'desc']],
            ajax: abp.libs.datatables.createAjax(articleTagService.getList, getFilter),
            columnDefs: [
                {
                    data: null,
                    orderable: false,
                    render: function (data, type, row) {
                        var html = '<div class="d-flex gap-1">';

                        if (canEdit) {
                            html +=
                                '<button type="button" class="btn btn-sm btn-outline-primary action-edit" data-id="' +
                                row.id +
                                '" title="' +
                                l('Edit') +
                                '"><i class="fa fa-pen"></i></button>';
                        }

                        if (canDelete) {
                            html +=
                                '<button type="button" class="btn btn-sm btn-outline-danger action-delete" data-id="' +
                                row.id +
                                '" title="' +
                                l('Delete') +
                                '"><i class="fa fa-trash"></i></button>';
                        }

                        html += '</div>';
                        return html;
                    },
                },
                { data: 'name' },
                { data: 'slug' },
                {
                    data: 'description',
                    defaultContent: '',
                    render: function (v) {
                        return v || '-';
                    },
                },
                { data: 'usageCount' },
            ],
        })
    );

    $('#NewArticleTagButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });

    $('#ArticleTagsTable').on('click', '.action-edit', function (e) {
        e.preventDefault();
        e.stopPropagation();
        editModal.open({ id: $(this).data('id') });
    });

    $('#ArticleTagsTable').on('click', '.action-delete', function (e) {
        e.preventDefault();
        e.stopPropagation();
        var id = $(this).data('id');

        abp.message.confirm(l('DeleteConfirmationMessage')).then(function (confirmed) {
            if (!confirmed) {
                return;
            }

            articleTagService.delete(id).then(function () {
                abp.notify.success(l('SuccessfullyDeleted'));
                dataTable.ajax.reload();
            });
        });
    });

    $('#SearchForm').on('submit', function (e) {
        e.preventDefault();
        dataTable.ajax.reload();
    });

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $('#ExportToExcelButton').click(function (e) {
        e.preventDefault();

        articleTagService.getDownloadToken().then(function (result) {
            var input = getFilter();
            var url =
                abp.appPath +
                'api/masterdata/article-tags/as-excel-file' +
                abp.utils.buildQueryString([
                    { name: 'downloadToken', value: result.token },
                    { name: 'filterText', value: input.filterText },
                ]);

            var downloadWindow = window.open(url, '_blank');
            downloadWindow.focus();
        });
    });
});
