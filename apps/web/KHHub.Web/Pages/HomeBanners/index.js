$(function () {
    var l = abp.localization.getResource('MasterDataService');

    var homeBannerService = window.kHHub.masterDataService.services.homeBanners.homeBanners;
    var MP = window.KHHubMediaPreview;
    var thumbPlaceholder =
        (MP && MP.PLACEHOLDER_THUMB_SRC) ||
        'data:image/gif;base64,R0lGODlhAQABAIAAAAAAAP///yH5BAEAAAAALAAAAAABAAEAAAIBRAA7';

    function escapeAttr(s) {
        return String(s || '')
            .replace(/&/g, '&amp;')
            .replace(/"/g, '&quot;')
            .replace(/</g, '&lt;');
    }

    var canEdit = abp.auth.isGranted('MasterDataService.HomeBanners.Edit');
    var canDelete = abp.auth.isGranted('MasterDataService.HomeBanners.Delete');
    var canBulkDelete = canDelete;

    var getFilter = function () {
        return {
            filterText: $('#FilterText').val(),
            title: $('#TitleFilter').val(),
            subtitle: $('#SubtitleFilter').val(),
            description: $('#DescriptionFilter').val(),
            imageUrl: $('#ImageUrlFilter').val(),
            mobileImageUrl: $('#MobileImageUrlFilter').val(),
            buttonText: $('#ButtonTextFilter').val(),
            buttonUrl: $('#ButtonUrlFilter').val(),
            targetType: $('#TargetTypeFilter').val(),
            targetId: $('#TargetIdFilter').val(),
            sortOrderMin: $('#SortOrderFilterMin').val(),
            sortOrderMax: $('#SortOrderFilterMax').val(),
            isActive: (function () {
                var value = $('#IsActiveFilter').val();
                if (value === undefined || value === null || value === '') {
                    return '';
                }
                return value === 'true';
            })(),
            startDateMin: $('#StartDateFilterMin').val(),
            startDateMax: $('#StartDateFilterMax').val(),
            endDateMin: $('#EndDateFilterMin').val(),
            endDateMax: $('#EndDateFilterMax').val(),
        };
    };

    var dataTableColumns = [
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
            data: 'imageUrl',
            orderable: false,
            render: function (imageUrl, type, row) {
                var src = imageUrl || '';
                if (!src) {
                    return '<div class="home-banner-thumb bg-light border"></div>';
                }
                return (
                    '<img class="home-banner-thumb border" src="' +
                    thumbPlaceholder +
                    '" data-khhub-thumb-path="' +
                    escapeAttr(src) +
                    '" alt="' +
                    escapeAttr(row.title || '') +
                    '" />'
                );
            },
        },
        {
            data: 'title',
            render: function (title, type, row) {
                return (
                    '<div class="fw-semibold">' +
                    (title || '') +
                    '</div><div class="text-muted small">' +
                    (row.subtitle || '') +
                    '</div>'
                );
            },
        },
        { data: 'sortOrder' },
        {
            data: 'isActive',
            render: function (isActive) {
                return isActive ? '<i class="fa fa-check"></i>' : '<i class="fa fa-times"></i>';
            },
        },
        {
            data: 'startDate',
            render: function (startDate) {
                if (!startDate) {
                    return '';
                }
                return new Date(Date.parse(startDate)).toLocaleDateString(abp.localization.currentCulture.name);
            },
        },
        {
            data: 'endDate',
            render: function (endDate) {
                if (!endDate) {
                    return '';
                }
                return new Date(Date.parse(endDate)).toLocaleDateString(abp.localization.currentCulture.name);
            },
        },
        {
            data: 'targetType',
            render: function (targetType) {
                if (targetType === undefined || targetType === null) {
                    return '';
                }
                var localizationKey = 'Enum:TargetType.' + targetType;
                var localized = l(localizationKey);
                if (localized === localizationKey) {
                    return '';
                }
                return localized;
            },
        },
    ];

    if (canBulkDelete) {
        dataTableColumns.unshift({
            targets: 0,
            data: null,
            orderable: false,
            className: 'select-checkbox',
            width: '0.5rem',
            render: function (data) {
                return (
                    '<input type="checkbox" class="form-check-input select-row-checkbox" data-id="' +
                    data.id +
                    '"/>'
                );
            },
        });
    } else {
        $('#BulkDeleteCheckboxTheader').remove();
    }

    var sortOrderColIndex = canBulkDelete ? 4 : 3;

    var dataTable = $('#HomeBannersTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            processing: true,
            serverSide: true,
            paging: true,
            searching: false,
            responsive: true,
            order: [[sortOrderColIndex, 'asc']],
            ajax: abp.libs.datatables.createAjax(homeBannerService.getList, getFilter),
            columnDefs: dataTableColumns,
            rowCallback: function (row, data) {
                $(row).attr('data-id', data.id);
                $(row).addClass('home-banner-row');
            },
        })
    );

    $('#HomeBannersTable').on('draw.dt', function () {
        if (MP && typeof MP.hydrateThumbImages === 'function') {
            MP.hydrateThumbImages('#HomeBannersTable');
        }
    });

    $('#NewHomeBannerButton').click(function (e) {
        e.preventDefault();
        window.location.href = abp.appPath + 'HomeBanners/Create';
    });

    $('#HomeBannersTable').on('click', '.action-edit', function (e) {
        e.preventDefault();
        e.stopPropagation();
        var id = $(this).data('id');
        window.location.href = abp.appPath + 'HomeBanners/Edit?id=' + id;
    });

    $('#HomeBannersTable').on('click', '.action-delete', function (e) {
        e.preventDefault();
        e.stopPropagation();
        var id = $(this).data('id');
        abp.message.confirm(l('DeleteConfirmationMessage')).then(function (confirmed) {
            if (!confirmed) {
                return;
            }
            homeBannerService.delete(id).then(function () {
                abp.notify.success(l('SuccessfullyDeleted'));
                dataTable.ajax.reloadEx();
            });
        });
    });

    $('#HomeBannersTable tbody').on('click', 'tr.home-banner-row', function (e) {
        if ($(e.target).closest('button, a, input, .dropdown').length > 0) {
            return;
        }
        if (!canEdit) {
            return;
        }
        var id = $(this).attr('data-id');
        if (id) {
            window.location.href = abp.appPath + 'HomeBanners/Edit?id=' + id;
        }
    });

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
            selectOrUnselectAllCheckboxes(false);
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
                        homeBannerService.deleteAll(getFilter()).then(function () {
                            dataTable.ajax.reloadEx();
                            selectOrUnselectAllCheckboxes(false);
                            showOrHideContextMenu();
                        });
                    });
                } else {
                    var selected = $("input[type='checkbox'].select-row-checkbox:is(:checked)");
                    var selectedRecordsIds = [];
                    for (var i = 0; i < selected.length; i++) {
                        selectedRecordsIds.push($(selected[i]).data('id'));
                    }
                    abp.message.confirm(l('DeleteSelectedRecords', selected.length), function (confirmed) {
                        if (!confirmed) {
                            return;
                        }
                        homeBannerService.deleteByIds(selectedRecordsIds).then(function () {
                            dataTable.ajax.reloadEx();
                            selectOrUnselectAllCheckboxes(false);
                            showOrHideContextMenu();
                        });
                    });
                }
            });
        } else {
            $('#bulk-delete-context-menu').addClass('d-none');
            $('#select-all-items-btn').addClass('d-none');
            $('#items-selected-info-message').addClass('d-none');
            $('#clear-selection-btn').addClass('d-none');
        }
    };

    $('#SearchForm').submit(function (e) {
        e.preventDefault();
        dataTable.ajax.reloadEx();
        selectOrUnselectAllCheckboxes(false);
        showOrHideContextMenu();
    });

    $('#ExportToExcelButton').click(function (e) {
        e.preventDefault();
        homeBannerService.getDownloadToken().then(function (result) {
            var input = getFilter();
            var url =
                abp.appPath +
                'api/masterdata/home-banners/as-excel-file' +
                abp.utils.buildQueryString([
                    { name: 'downloadToken', value: result.token },
                    { name: 'filterText', value: input.filterText },
                    { name: 'title', value: input.title },
                    { name: 'subtitle', value: input.subtitle },
                    { name: 'description', value: input.description },
                    { name: 'imageUrl', value: input.imageUrl },
                    { name: 'mobileImageUrl', value: input.mobileImageUrl },
                    { name: 'buttonText', value: input.buttonText },
                    { name: 'buttonUrl', value: input.buttonUrl },
                    { name: 'targetType', value: input.targetType },
                    { name: 'targetId', value: input.targetId },
                    { name: 'sortOrderMin', value: input.sortOrderMin },
                    { name: 'sortOrderMax', value: input.sortOrderMax },
                    { name: 'isActive', value: input.isActive },
                    { name: 'startDateMin', value: input.startDateMin },
                    { name: 'startDateMax', value: input.startDateMax },
                    { name: 'endDateMin', value: input.endDateMin },
                    { name: 'endDateMax', value: input.endDateMax },
                ]);
            var downloadWindow = window.open(url, '_blank');
            downloadWindow.focus();
        });
    });

    $('#AdvancedFilterSectionToggler').on('click', function () {
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

    $('#AdvancedFilterSection select').change(function () {
        dataTable.ajax.reloadEx();
        selectOrUnselectAllCheckboxes(false);
        showOrHideContextMenu();
    });
});
