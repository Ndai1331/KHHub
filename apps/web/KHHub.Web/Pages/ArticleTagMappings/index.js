$(function () {
    var l = abp.localization.getResource('MasterDataService');

    var articleTagMappingService =
        window.kHHub.masterDataService.services.articleTagMappings.articleTagMappings;

    var createModal = new abp.ModalManager({
        viewUrl: abp.appPath + 'ArticleTagMappings/CreateModal',
        scriptUrl: abp.appPath + 'Pages/ArticleTagMappings/createModal.js',
        modalClass: 'articleTagMappingCreate',
    });

    var editModal = new abp.ModalManager({
        viewUrl: abp.appPath + 'ArticleTagMappings/EditModal',
        scriptUrl: abp.appPath + 'Pages/ArticleTagMappings/editModal.js',
        modalClass: 'articleTagMappingEdit',
    });

    var getFilter = function () {
        var isPrimaryVal = $('#IsPrimaryFilter').val();
        var isPrimary =
            isPrimaryVal === undefined || isPrimaryVal === null || isPrimaryVal === ''
                ? ''
                : isPrimaryVal === 'true';

        return {
            filterText: $('#FilterText').val(),
            articleId: $('#ArticleIdFilter').val(),
            articleTagId: $('#ArticleTagIdFilter').val(),
            isPrimary: isPrimary,
        };
    };

    var canEdit = abp.auth.isGranted('MasterDataService.ArticleTagMappings.Edit');
    var canDelete = abp.auth.isGranted('MasterDataService.ArticleTagMappings.Delete');

    var dataTable = $('#ArticleTagMappingsTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            processing: true,
            serverSide: true,
            paging: true,
            searching: false,
            responsive: true,
            order: [[2, 'asc']],
            ajax: abp.libs.datatables.createAjax(articleTagMappingService.getList, getFilter),
            columnDefs: [
                {
                    data: null,
                    orderable: false,
                    render: function (data, type, row) {
                        var html = '<div class="d-flex gap-1">';
                        var id = row.articleTagMapping.id;

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
                    data: 'articleTagMapping.isPrimary',
                    render: function (isPrimary) {
                        return isPrimary
                            ? '<span class="badge bg-primary-subtle text-primary">' + l('Yes') + '</span>'
                            : '<span class="badge bg-secondary-subtle text-secondary">' + l('No') + '</span>';
                    },
                },
                { data: 'articleTagMapping.order' },
                {
                    data: 'articleTag.name',
                    defaultContent: '',
                    render: function (name, type, row) {
                        return name || row.articleTag.slug || '-';
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

    $('#NewArticleTagMappingButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });

    $('#ArticleTagMappingsTable').on('click', '.action-edit', function (e) {
        e.preventDefault();
        e.stopPropagation();
        editModal.open({ id: $(this).data('id') });
    });

    $('#ArticleTagMappingsTable').on('click', '.action-delete', function (e) {
        e.preventDefault();
        e.stopPropagation();
        var id = $(this).data('id');

        abp.message.confirm(l('DeleteConfirmationMessage')).then(function (confirmed) {
            if (!confirmed) {
                return;
            }

            articleTagMappingService.delete(id).then(function () {
                abp.notify.success(l('SuccessfullyDeleted'));
                dataTable.ajax.reload();
            });
        });
    });

    $('#SearchForm').on('submit', function (e) {
        e.preventDefault();
        dataTable.ajax.reload();
    });

    $('#ArticleIdFilter, #ArticleTagIdFilter, #IsPrimaryFilter').on('change', function () {
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

        articleTagMappingService.getDownloadToken().then(function (result) {
            var input = getFilter();
            var url =
                abp.appPath +
                'api/masterdata/article-tag-mappings/as-excel-file' +
                abp.utils.buildQueryString([
                    { name: 'downloadToken', value: result.token },
                    { name: 'filterText', value: input.filterText },
                    { name: 'articleId', value: input.articleId },
                    { name: 'articleTagId', value: input.articleTagId },
                    { name: 'isPrimary', value: input.isPrimary },
                ]);

            var downloadWindow = window.open(url, '_blank');
            downloadWindow.focus();
        });
    });
});
