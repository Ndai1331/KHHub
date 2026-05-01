$(function () {
    var l = abp.localization.getResource('MasterDataService');

    var articleCategoryService =
        window.kHHub.masterDataService.services.articleCategories.articleCategories;

    var createModal = new abp.ModalManager({
        viewUrl: abp.appPath + 'ArticleCategories/CreateModal',
        scriptUrl: abp.appPath + 'Pages/ArticleCategories/createModal.js',
        modalClass: 'articleCategoryCreate',
    });

    var editModal = new abp.ModalManager({
        viewUrl: abp.appPath + 'ArticleCategories/EditModal',
        scriptUrl: abp.appPath + 'Pages/ArticleCategories/editModal.js',
        modalClass: 'articleCategoryEdit',
    });

    function trySyncArticleCategoryThumb(modalMgr, label) {
        var sync = window.KHHub && window.KHHub.syncArticleCategoryPreviewInModal;
        if (typeof sync !== 'function') {
            console.warn('[ArticleCategories]', label, 'syncArticleCategoryPreviewInModal missing');
            return;
        }
        var $modal = modalMgr.getModal && modalMgr.getModal();
        if (!$modal || !$modal.length) {
            console.warn('[ArticleCategories]', label, 'getModal empty');
            return;
        }
        console.log('[ArticleCategories]', label, 'trySyncArticleCategoryThumb', {
            hasUrlInput: $modal.find('.article-category-thumbnail-url').length,
        });
        sync($modal);
    }

    var getFilter = function () {
        var isActiveVal = $('#IsActiveFilter').val();
        var isActive =
            isActiveVal === undefined || isActiveVal === null || isActiveVal === ''
                ? ''
                : isActiveVal === 'true';

        return {
            filterText: $('#FilterText').val(),
            parentId: $('#ParentIdFilter').val(),
            isActive: isActive,
        };
    };

    var canEdit = abp.auth.isGranted('MasterDataService.ArticleCategories.Edit');
    var canDelete = abp.auth.isGranted('MasterDataService.ArticleCategories.Delete');

    var dataTable = $('#ArticleCategoriesTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            processing: true,
            serverSide: true,
            paging: true,
            searching: false,
            responsive: true,
            order: [[7, 'asc']],
            ajax: abp.libs.datatables.createAjax(articleCategoryService.getList, getFilter),
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
                {
                    data: 'thumbnailUrl',
                    defaultContent: '',
                    orderable: false,
                    className: 'text-nowrap',
                    render: function (url) {
                        if (!url || !String(url).trim()) {
                            return '-';
                        }
                        var u = String(url).trim();
                        var safe = u.replace(/&/g, '&amp;').replace(/"/g, '&quot;');
                        return (
                            '<a href="' +
                            safe +
                            '" target="_blank" rel="noopener noreferrer" class="d-inline-block">' +
                            '<img src="' +
                            safe +
                            '" alt="" class="rounded border" loading="lazy" ' +
                            'style="width:48px;height:48px;object-fit:cover;display:block;" /></a>'
                        );
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
                {
                    data: 'icon',
                    defaultContent: '',
                    render: function (v) {
                        return v || '-';
                    },
                },
                {
                    data: 'parentId',
                    defaultContent: '',
                    render: function (id) {
                        return id ? id : '-';
                    },
                },
                { data: 'displayOrder' },
                {
                    data: 'isActive',
                    render: function (isActive) {
                        return isActive
                            ? '<span class="badge bg-success-subtle text-success">' +
                                  l('Yes') +
                                  '</span>'
                            : '<span class="badge bg-secondary-subtle text-secondary">' +
                                  l('No') +
                                  '</span>';
                    },
                },
            ],
        })
    );

    createModal.onOpen(function () {
        trySyncArticleCategoryThumb(createModal, 'createModal.onOpen');
        setTimeout(function () {
            trySyncArticleCategoryThumb(createModal, 'createModal deferred 0');
        }, 0);
        setTimeout(function () {
            trySyncArticleCategoryThumb(createModal, 'createModal deferred 150ms');
        }, 150);
    });

    editModal.onOpen(function () {
        trySyncArticleCategoryThumb(editModal, 'editModal.onOpen');
        setTimeout(function () {
            trySyncArticleCategoryThumb(editModal, 'editModal deferred 0');
        }, 0);
        setTimeout(function () {
            trySyncArticleCategoryThumb(editModal, 'editModal deferred 150ms');
        }, 150);
    });

    $('#NewArticleCategoryButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });

    $('#ArticleCategoriesTable').on('click', '.action-edit', function (e) {
        e.preventDefault();
        e.stopPropagation();
        var id = $(this).data('id');
        editModal.open({ id: id });
    });

    $('#ArticleCategoriesTable').on('click', '.action-delete', function (e) {
        e.preventDefault();
        e.stopPropagation();
        var id = $(this).data('id');

        abp.message.confirm(l('DeleteConfirmationMessage')).then(function (confirmed) {
            if (!confirmed) {
                return;
            }

            articleCategoryService.delete(id).then(function () {
                abp.notify.success(l('SuccessfullyDeleted'));
                dataTable.ajax.reload();
            });
        });
    });

    $('#SearchForm').on('submit', function (e) {
        e.preventDefault();
        dataTable.ajax.reload();
    });

    $('#ParentIdFilter, #IsActiveFilter').on('change', function () {
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

        articleCategoryService.getDownloadToken().then(function (result) {
            var input = getFilter();
            var url =
                abp.appPath +
                'api/masterdata/article-categories/as-excel-file' +
                abp.utils.buildQueryString([
                    { name: 'downloadToken', value: result.token },
                    { name: 'filterText', value: input.filterText },
                    { name: 'parentId', value: input.parentId },
                    { name: 'isActive', value: input.isActive },
                ]);

            var downloadWindow = window.open(url, '_blank');
            downloadWindow.focus();
        });
    });
});
