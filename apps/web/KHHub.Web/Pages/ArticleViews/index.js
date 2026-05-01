$(function () {
    var l = abp.localization.getResource('MasterDataService');

    var articleViewService = window.kHHub.masterDataService.services.articleViews.articleViews;

    var createModal = new abp.ModalManager({
        viewUrl: abp.appPath + 'ArticleViews/CreateModal',
        scriptUrl: abp.appPath + 'Pages/ArticleViews/createModal.js',
        modalClass: 'articleViewCreate',
    });

    var editModal = new abp.ModalManager({
        viewUrl: abp.appPath + 'ArticleViews/EditModal',
        scriptUrl: abp.appPath + 'Pages/ArticleViews/editModal.js',
        modalClass: 'articleViewEdit',
    });

    var getFilter = function () {
        return {
            filterText: $('#FilterText').val(),
            articleId: $('#ArticleIdFilter').val(),
        };
    };

    var canEdit = abp.auth.isGranted('MasterDataService.ArticleViews.Edit');
    var canDelete = abp.auth.isGranted('MasterDataService.ArticleViews.Delete');

    var dataTable = $('#ArticleViewsTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            processing: true,
            serverSide: true,
            paging: true,
            searching: false,
            responsive: true,
            order: [[4, 'desc']],
            ajax: abp.libs.datatables.createAjax(articleViewService.getList, getFilter),
            columnDefs: [
                {
                    data: null,
                    orderable: false,
                    render: function (data, type, row) {
                        var html = '<div class="d-flex gap-1">';
                        var id = row.articleView.id;

                        if (canEdit) {
                            html +=
                                '<button type="button" class="btn btn-sm btn-outline-primary action-edit" data-id="' +
                                id +
                                '" title="' +
                                l('Edit') +
                                '"><i class="fa fa-pen"></i></button>';
                        }

                        if (canDelete) {
                            html +=
                                '<button type="button" class="btn btn-sm btn-outline-danger action-delete" data-id="' +
                                id +
                                '" title="' +
                                l('Delete') +
                                '"><i class="fa fa-trash"></i></button>';
                        }

                        html += '</div>';
                        return html;
                    },
                },
                {
                    data: 'articleView.ipAddress',
                    defaultContent: '',
                },
                {
                    data: 'articleView.device',
                    defaultContent: '',
                },
                {
                    data: 'articleView.source',
                    defaultContent: '',
                },
                {
                    data: 'articleView.viewedAt',
                    render: function (viewedAt) {
                        if (!viewedAt) {
                            return '-';
                        }

                        var date = Date.parse(viewedAt);
                        return new Date(date).toLocaleString(abp.localization.currentCulture.name);
                    },
                },
                { data: 'articleView.duration' },
                {
                    data: 'articleView.userId',
                    defaultContent: '',
                    render: function (userId) {
                        return userId || '-';
                    },
                },
                {
                    data: 'article.title',
                    defaultContent: '',
                    render: function (title) {
                        return title || '-';
                    },
                },
            ],
        })
    );

    $('#NewArticleViewButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });

    $('#ArticleViewsTable').on('click', '.action-edit', function (e) {
        e.preventDefault();
        e.stopPropagation();
        editModal.open({ id: $(this).data('id') });
    });

    $('#ArticleViewsTable').on('click', '.action-delete', function (e) {
        e.preventDefault();
        e.stopPropagation();
        var id = $(this).data('id');

        abp.message.confirm(l('DeleteConfirmationMessage')).then(function (confirmed) {
            if (!confirmed) {
                return;
            }

            articleViewService.delete(id).then(function () {
                abp.notify.success(l('SuccessfullyDeleted'));
                dataTable.ajax.reload();
            });
        });
    });

    $('#SearchForm').on('submit', function (e) {
        e.preventDefault();
        dataTable.ajax.reload();
    });

    $('#ArticleIdFilter').on('change', function () {
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

        articleViewService.getDownloadToken().then(function (result) {
            var input = getFilter();
            var url =
                abp.appPath +
                'api/masterdata/article-views/as-excel-file' +
                abp.utils.buildQueryString([
                    { name: 'downloadToken', value: result.token },
                    { name: 'filterText', value: input.filterText },
                    { name: 'articleId', value: input.articleId },
                ]);

            var downloadWindow = window.open(url, '_blank');
            downloadWindow.focus();
        });
    });
});
