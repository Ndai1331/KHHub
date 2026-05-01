$(function () {
    var l = abp.localization.getResource('MasterDataService');

    var articleService = window.kHHub.masterDataService.services.articles.articles;

    var getFilter = function () {
        return {
            filterText: $('#FilterText').val(),
            type: $('#TypeFilter').val(),
            status: $('#StatusFilter').val(),
            articleCategoryId: $('#ArticleCategoryIdFilter').val(),
        };
    };

    var canEdit = abp.auth.isGranted('MasterDataService.Articles.Edit');
    var canDelete = abp.auth.isGranted('MasterDataService.Articles.Delete');

    var getStatusBadge = function (status) {
        if (status === 2) {
            return '<span class="badge bg-success-subtle text-success">' + l('Enum:ArticleStatus.2') + '</span>';
        }
        if (status === 1) {
            return '<span class="badge bg-warning-subtle text-warning">' + l('Enum:ArticleStatus.1') + '</span>';
        }
        if (status === 0) {
            return '<span class="badge bg-secondary-subtle text-secondary">' + l('Enum:ArticleStatus.0') + '</span>';
        }

        return '<span class="badge bg-light text-dark">' + l('Enum:ArticleStatus.' + status) + '</span>';
    };

    var dataTable = $('#ArticlesTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            processing: true,
            serverSide: true,
            paging: true,
            searching: false,
            responsive: true,
            order: [[6, 'desc']],
            ajax: abp.libs.datatables.createAjax(articleService.getList, getFilter),
            rowCallback: function (row, data) {
                $(row).attr('data-id', data.article.id);
                $(row).addClass('article-row');
            },
            columnDefs: [
                {
                    data: null,
                    orderable: false,
                    render: function (data, type, row) {
                        var html = '<div class="d-flex gap-1">';

                        if (canEdit) {
                            html +=
                                '<button type="button" class="btn btn-sm btn-outline-primary action-edit" data-id="' +
                                row.article.id +
                                '" title="' +
                                l('Edit') +
                                '"><i class="fa fa-pen"></i></button>';
                        }

                        if (canDelete) {
                            html +=
                                '<button type="button" class="btn btn-sm btn-outline-danger action-delete" data-id="' +
                                row.article.id +
                                '" title="' +
                                l('Delete') +
                                '"><i class="fa fa-trash"></i></button>';
                        }

                        html += '</div>';
                        return html;
                    },
                },
                {
                    data: 'article.title',
                    orderable: false,
                    render: function (title, type, row) {
                        var src = row.article.thumbnailUrl || '';
                        if (!src) {
                            return '<div class="article-thumb bg-light border"></div>';
                        }
                        return (
                            '<img class="article-thumb border" src="' +
                            src +
                            '" alt="' +
                            (row.article.title || 'thumbnail') +
                            '" />'
                        );
                    },
                },
                {
                    data: 'article.title',
                    render: function (title, type, row) {
                        return (
                            '<div class="fw-semibold">' +
                            (title || '') +
                            '</div><div class="text-muted small">' +
                            (row.article.slug || '') +
                            '</div>'
                        );
                    },
                },
                {
                    data: 'articleCategory.name',
                    defaultContent: '',
                    render: function (value) {
                        return value || '-';
                    },
                },
        {
            data: 'article.type',
            render: function (type) {
                if (type === undefined || type === null) {
                    return '';
                }
                return l('Enum:ArticleType.' + type);
            },
        },
                {
                    data: 'article.status',
                    render: function (status) {
                        if (status === undefined || status === null) {
                            return '';
                        }
                        return getStatusBadge(status);
                    },
                },
                {
                    data: 'article.publishedAt',
                    render: function (publishedAt) {
                        if (!publishedAt) {
                            return '-';
                        }
                        return new Date(Date.parse(publishedAt)).toLocaleDateString(
                            abp.localization.currentCulture.name
                        );
                    },
                },
                { data: 'article.authorName' },
            ],
        })
    );

    $('#NewArticleButton').click(function (e) {
        e.preventDefault();
        window.location.href = abp.appPath + 'Articles/Create';
    });

    $('#ArticlesTable').on('click', '.action-edit', function (e) {
        e.preventDefault();
        e.stopPropagation();
        var id = $(this).data('id');
        window.location.href = abp.appPath + 'Articles/Edit?id=' + id;
    });

    $('#ArticlesTable').on('click', '.action-delete', function (e) {
        e.preventDefault();
        e.stopPropagation();
        var id = $(this).data('id');

        abp.message.confirm(l('DeleteConfirmationMessage')).then(function (confirmed) {
            if (!confirmed) {
                return;
            }

            articleService.delete(id).then(function () {
                abp.notify.success(l('SuccessfullyDeleted'));
                dataTable.ajax.reload();
            });
        });
    });

    $('#ArticlesTable tbody').on('click', 'tr.article-row', function (e) {
        if ($(e.target).closest('button, a, input, .dropdown').length > 0) {
            return;
        }

        if (!canEdit) {
            return;
        }

        var id = $(this).attr('data-id');
        if (id) {
            window.location.href = abp.appPath + 'Articles/Edit?id=' + id;
        }
    });

    $('#SearchForm').on('submit', function (e) {
        e.preventDefault();
        dataTable.ajax.reload();
    });

    $('#ExportToExcelButton').click(function (e) {
        e.preventDefault();

        articleService.getDownloadToken().then(function (result) {
            var input = getFilter();
            var url =
                abp.appPath +
                'api/masterdata/articles/as-excel-file' +
                abp.utils.buildQueryString([
                    { name: 'downloadToken', value: result.token },
                    { name: 'filterText', value: input.filterText },
                    { name: 'type', value: input.type },
                    { name: 'status', value: input.status },
                    { name: 'articleCategoryId', value: input.articleCategoryId },
                ]);

            var downloadWindow = window.open(url, '_blank');
            downloadWindow.focus();
        });
    });

    $('#StatusFilter, #TypeFilter, #ArticleCategoryIdFilter').on('change', function () {
        dataTable.ajax.reload();
    });
});
