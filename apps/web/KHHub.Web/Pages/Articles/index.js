$(function () {
    var l = abp.localization.getResource('MasterDataService');

    var articleService = window.kHHub.masterDataService.services.articles.articles;

    var lastNpIdId = '';
    var lastNpDisplayNameId = '';

    var _lookupModal = new abp.ModalManager({
        viewUrl: abp.appPath + 'Shared/LookupModal',
        scriptUrl: abp.appPath + 'Pages/Shared/lookupModal.js',
        modalClass: 'navigationPropertyLookup',
    });

    $('.lookupCleanButton').on('click', '', function () {
        $(this).parent().find('input').val('');
    });

    _lookupModal.onClose(function () {
        var modal = $(_lookupModal.getModal());
        $('#' + lastNpIdId).val(modal.find('#CurrentLookupId').val());
        $('#' + lastNpDisplayNameId).val(modal.find('#CurrentLookupDisplayName').val());
    });

    var createModal = new abp.ModalManager({
        viewUrl: abp.appPath + 'Articles/CreateModal',
        scriptUrl: abp.appPath + 'Pages/Articles/createModal.js',
        modalClass: 'articleCreate',
    });

    var editModal = new abp.ModalManager({
        viewUrl: abp.appPath + 'Articles/EditModal',
        scriptUrl: abp.appPath + 'Pages/Articles/editModal.js',
        modalClass: 'articleEdit',
    });

    var getFilter = function () {
        return {
            filterText: $('#FilterText').val(),
            title: $('#TitleFilter').val(),
            slug: $('#SlugFilter').val(),
            summary: $('#SummaryFilter').val(),
            content: $('#ContentFilter').val(),
            thumbnailUrl: $('#ThumbnailUrlFilter').val(),
            coverImageUrl: $('#CoverImageUrlFilter').val(),
            type: $('#TypeFilter').val(),
            authorName: $('#AuthorNameFilter').val(),
            source: $('#SourceFilter').val(),
            sourceUrl: $('#SourceUrlFilter').val(),
            status: $('#StatusFilter').val(),
            publishedAtMin: $('#PublishedAtFilterMin').val(),
            publishedAtMax: $('#PublishedAtFilterMax').val(),
            isFeatured: (function () {
                var value = $('#IsFeaturedFilter').val();
                if (value === undefined || value === null || value === '') {
                    return '';
                }
                return value === 'true';
            })(),
            isHot: (function () {
                var value = $('#IsHotFilter').val();
                if (value === undefined || value === null || value === '') {
                    return '';
                }
                return value === 'true';
            })(),
            isTrending: (function () {
                var value = $('#IsTrendingFilter').val();
                if (value === undefined || value === null || value === '') {
                    return '';
                }
                return value === 'true';
            })(),
            viewCountMin: $('#ViewCountFilterMin').val(),
            viewCountMax: $('#ViewCountFilterMax').val(),
            likeCountMin: $('#LikeCountFilterMin').val(),
            likeCountMax: $('#LikeCountFilterMax').val(),
            shareCountMin: $('#ShareCountFilterMin').val(),
            shareCountMax: $('#ShareCountFilterMax').val(),
            commentCountMin: $('#CommentCountFilterMin').val(),
            commentCountMax: $('#CommentCountFilterMax').val(),
            readingTimeMin: $('#ReadingTimeFilterMin').val(),
            readingTimeMax: $('#ReadingTimeFilterMax').val(),
            seoTitle: $('#SeoTitleFilter').val(),
            seoDescription: $('#SeoDescriptionFilter').val(),
            seoKeywords: $('#SeoKeywordsFilter').val(),
            articleCategoryId: $('#ArticleCategoryIdFilter').val(),
        };
    };

    var dataTableColumns = [
        {
            rowAction: {
                items: [
                    {
                        text: l('Edit'),
                        visible: abp.auth.isGranted('MasterDataService.Articles.Edit'),
                        action: function (data) {
                            editModal.open({
                                id: data.record.article.id,
                            });
                        },
                    },
                    {
                        text: l('Delete'),
                        visible: abp.auth.isGranted('MasterDataService.Articles.Delete'),
                        confirmMessage: function () {
                            return l('DeleteConfirmationMessage');
                        },
                        action: function (data) {
                            articleService.delete(data.record.article.id).then(function () {
                                abp.notify.success(l('SuccessfullyDeleted'));
                                dataTable.ajax.reloadEx();
                            });
                        },
                    },
                ],
            },
        },
        { data: 'article.title' },
        { data: 'article.slug' },
        { data: 'article.summary' },
        { data: 'article.content' },
        { data: 'article.thumbnailUrl' },
        { data: 'article.coverImageUrl' },
        {
            data: 'article.type',

            render: function (type) {
                if (type === undefined || type === null) {
                    return '';
                }

                var localizationKey = 'Enum:ArticleType.' + type;
                var localized = l(localizationKey);

                if (localized === localizationKey) {
                    abp.log.warn('No localization found for ' + localizationKey);
                    return '';
                }

                return localized;
            },
        },
        { data: 'article.authorName' },
        { data: 'article.source' },
        { data: 'article.sourceUrl' },
        {
            data: 'article.status',

            render: function (status) {
                if (status === undefined || status === null) {
                    return '';
                }

                var localizationKey = 'Enum:ArticleStatus.' + status;
                var localized = l(localizationKey);

                if (localized === localizationKey) {
                    abp.log.warn('No localization found for ' + localizationKey);
                    return '';
                }

                return localized;
            },
        },
        {
            data: 'article.publishedAt',

            render: function (publishedAt) {
                if (!publishedAt) {
                    return '';
                }

                var date = Date.parse(publishedAt);
                return new Date(date).toLocaleDateString(abp.localization.currentCulture.name);
            },
        },
        {
            data: 'article.isFeatured',

            render: function (isFeatured) {
                return isFeatured ? '<i class="fa fa-check"></i>' : '<i class="fa fa-times"></i>';
            },
        },
        {
            data: 'article.isHot',

            render: function (isHot) {
                return isHot ? '<i class="fa fa-check"></i>' : '<i class="fa fa-times"></i>';
            },
        },
        {
            data: 'article.isTrending',

            render: function (isTrending) {
                return isTrending ? '<i class="fa fa-check"></i>' : '<i class="fa fa-times"></i>';
            },
        },
        { data: 'article.viewCount' },
        { data: 'article.likeCount' },
        { data: 'article.shareCount' },
        { data: 'article.commentCount' },
        { data: 'article.readingTime' },
        { data: 'article.seoTitle' },
        { data: 'article.seoDescription' },
        { data: 'article.seoKeywords' },
        {
            data: 'articleCategory.name',

            defaultContent: '',
        },
    ];

    if (abp.auth.isGranted('MasterDataService.Articles.Delete')) {
        dataTableColumns.unshift({
            targets: 0,
            data: null,
            orderable: false,
            className: 'select-checkbox',
            width: '0.5rem',
            render: function (data) {
                return (
                    '<input type="checkbox" class="form-check-input select-row-checkbox" data-id="' +
                    data.article.id +
                    '"/>'
                );
            },
        });
    } else {
        $('#BulkDeleteCheckboxTheader').remove();
    }

    var dataTable = $('#ArticlesTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            processing: true,
            serverSide: true,
            paging: true,
            searching: false,
            responsive: true,
            order: [[2, 'desc']],
            ajax: abp.libs.datatables.createAjax(articleService.getList, getFilter),
            columnDefs: dataTableColumns,
        })
    );

    dataTable.on('xhr', function () {
        selectOrUnselectAllCheckboxes(false);
        showOrHideContextMenu();
        $('#select_all').prop('indeterminate', false);
        $('#select_all').prop('checked', false);
    });

    function selectOrUnselectAllCheckboxes(selectAll) {
        $('.select-row-checkbox').each(function () {
            $(this).prop('checked', selectAll);
        });
    }

    $('#select_all').click(function () {
        if ($(this).is(':checked')) {
            selectOrUnselectAllCheckboxes(true);
        } else {
            $('.select-row-checkbox').each(function () {
                selectOrUnselectAllCheckboxes(false);
            });
        }

        showOrHideContextMenu();
    });

    dataTable.on('change', "input[type='checkbox'].select-row-checkbox", function () {
        var unSelectedCheckboxes = $("input[type='checkbox'].select-row-checkbox:not(:checked)");

        if (unSelectedCheckboxes.length >= 1) {
            var dataRecordTotal = dataTable.context[0].json.data.length;
            if (unSelectedCheckboxes.length === dataRecordTotal) {
                $('#select_all').prop('indeterminate', false);
                $('#select_all').prop('checked', false);
            } else {
                $('#select_all').prop('indeterminate', true);
            }
        } else {
            $('#select_all').prop('indeterminate', false);
            $('#select_all').prop('checked', true);
        }

        showOrHideContextMenu();
    });

    var showOrHideContextMenu = function () {
        var selectedCheckboxes = $("input[type='checkbox'].select-row-checkbox:is(:checked)");
        var selectedCheckboxCount = selectedCheckboxes.length;
        var dataRecordTotal = dataTable.context[0].json.data.length;
        var recordsTotal = dataTable.context[0].json.recordsTotal;

        if (selectedCheckboxCount >= 1) {
            $('#bulk-delete-context-menu').removeClass('d-none');

            $('#items-selected-info-message').html(
                selectedCheckboxCount === 1
                    ? l('OneItemOnThisPageIsSelected')
                    : l('NumberOfItemsOnThisPageAreSelected', selectedCheckboxCount)
            );

            $('#items-selected-info-message').removeClass('d-none');

            if (selectedCheckboxCount === dataRecordTotal && recordsTotal > dataRecordTotal) {
                $('#select-all-items-btn').html(l('SelectAllItems', recordsTotal));
                $('#select-all-items-btn').removeClass('d-none');

                $('#select-all-items-btn').off('click');
                $('#select-all-items-btn').click(function () {
                    $(this).data('selected', true);
                    $(this).addClass('d-none');
                    $('#items-selected-info-message').html(l('AllItemsAreSelected', recordsTotal));
                    $('#clear-selection-btn').removeClass('d-none');
                });

                $('#clear-selection-btn').off('click');
                $('#clear-selection-btn').click(function () {
                    $('#select-all-items-btn').data('selected', false);
                    $('#select_all').prop('checked', false);
                    selectOrUnselectAllCheckboxes(false);
                    showOrHideContextMenu();
                });
            } else {
                $('#select-all-items-btn').addClass('d-none');
                $('#select-all-items-btn').data('selected', false);
                $('#clear-selection-btn').addClass('d-none');
            }

            $('#delete-selected-items').off('click');
            $('#delete-selected-items').click(function () {
                if ($('#select-all-items-btn').data('selected') === true) {
                    abp.message.confirm(l('DeleteAllRecords'), function (confirmed) {
                        if (!confirmed) {
                            return;
                        }

                        articleService.deleteAll(getFilter()).then(function () {
                            dataTable.ajax.reloadEx();
                            selectOrUnselectAllCheckboxes(false);
                            showOrHideContextMenu();
                        });
                    });
                } else {
                    var selectedCheckboxes = $(
                        "input[type='checkbox'].select-row-checkbox:is(:checked)"
                    );
                    var selectedRecordsIds = [];

                    for (var i = 0; i < selectedCheckboxes.length; i++) {
                        selectedRecordsIds.push($(selectedCheckboxes[i]).data('id'));
                    }

                    abp.message.confirm(
                        l('DeleteSelectedRecords', selectedCheckboxes.length),
                        function (confirmed) {
                            if (!confirmed) {
                                return;
                            }

                            articleService.deleteByIds(selectedRecordsIds).then(function () {
                                dataTable.ajax.reloadEx();
                                selectOrUnselectAllCheckboxes(false);
                                showOrHideContextMenu();
                            });
                        }
                    );
                }
            });
        } else {
            $('#bulk-delete-context-menu').addClass('d-none');
            $('#select-all-items-btn').addClass('d-none');
            $('#items-selected-info-message').addClass('d-none');
            $('#clear-selection-btn').addClass('d-none');
        }
    };

    createModal.onResult(function () {
        dataTable.ajax.reloadEx();
        selectOrUnselectAllCheckboxes(false);
        showOrHideContextMenu();
    });

    editModal.onResult(function () {
        dataTable.ajax.reloadEx();
        selectOrUnselectAllCheckboxes(false);
        showOrHideContextMenu();
    });

    $('#NewArticleButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });

    $('#SearchForm').submit(function (e) {
        e.preventDefault();
        dataTable.ajax.reloadEx();
        selectOrUnselectAllCheckboxes(false);
        showOrHideContextMenu();
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
                    { name: 'title', value: input.title },
                    { name: 'slug', value: input.slug },
                    { name: 'summary', value: input.summary },
                    { name: 'content', value: input.content },
                    { name: 'thumbnailUrl', value: input.thumbnailUrl },
                    { name: 'coverImageUrl', value: input.coverImageUrl },
                    { name: 'type', value: input.type },
                    { name: 'authorName', value: input.authorName },
                    { name: 'source', value: input.source },
                    { name: 'sourceUrl', value: input.sourceUrl },
                    { name: 'status', value: input.status },
                    { name: 'publishedAtMin', value: input.publishedAtMin },
                    { name: 'publishedAtMax', value: input.publishedAtMax },
                    { name: 'isFeatured', value: input.isFeatured },
                    { name: 'isHot', value: input.isHot },
                    { name: 'isTrending', value: input.isTrending },
                    { name: 'viewCountMin', value: input.viewCountMin },
                    { name: 'viewCountMax', value: input.viewCountMax },
                    { name: 'likeCountMin', value: input.likeCountMin },
                    { name: 'likeCountMax', value: input.likeCountMax },
                    { name: 'shareCountMin', value: input.shareCountMin },
                    { name: 'shareCountMax', value: input.shareCountMax },
                    { name: 'commentCountMin', value: input.commentCountMin },
                    { name: 'commentCountMax', value: input.commentCountMax },
                    { name: 'readingTimeMin', value: input.readingTimeMin },
                    { name: 'readingTimeMax', value: input.readingTimeMax },
                    { name: 'seoTitle', value: input.seoTitle },
                    { name: 'seoDescription', value: input.seoDescription },
                    { name: 'seoKeywords', value: input.seoKeywords },
                    { name: 'articleCategoryId', value: input.articleCategoryId },
                ]);

            var downloadWindow = window.open(url, '_blank');
            downloadWindow.focus();
        });
    });

    $('#AdvancedFilterSectionToggler').on('click', function (e) {
        $('#AdvancedFilterSection').toggle();
        var iconCss = $('#AdvancedFilterSection').is(':visible')
            ? 'fa ms-1 fa-angle-up'
            : 'fa ms-1 fa-angle-down';
        $(this).find('i').attr('class', iconCss);
    });

    $('#AdvancedFilterSection').on('keypress', function (e) {
        if (e.which === 13) {
            dataTable.ajax.reloadEx();
            selectOrUnselectAllCheckboxes(false);
            showOrHideContextMenu();
        }
    });

    //<suite-custom-code-block-1>
    //</suite-custom-code-block-1>

    //<suite-custom-code-block-2>
    //</suite-custom-code-block-2>

    $('#AdvancedFilterSection select').change(function () {
        dataTable.ajax.reloadEx();
        selectOrUnselectAllCheckboxes(false);
        showOrHideContextMenu();
    });

    //<suite-custom-code-block-3>
    //</suite-custom-code-block-3>
});
