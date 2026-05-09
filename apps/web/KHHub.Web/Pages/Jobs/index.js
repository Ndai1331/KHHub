$(function () {
    var l = abp.localization.getResource('MasterDataService');

    var jobService = window.kHHub.masterDataService.services.jobs.jobs;

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

    var getFilter = function () {
        return {
            filterText: $('#FilterText').val(),
            title: $('#TitleFilter').val(),
            slug: $('#SlugFilter').val(),
            summary: $('#SummaryFilter').val(),
            description: $('#DescriptionFilter').val(),
            requirements: $('#RequirementsFilter').val(),
            benefits: $('#BenefitsFilter').val(),
            thumbnailUrl: $('#ThumbnailUrlFilter').val(),
            coverImageUrl: $('#CoverImageUrlFilter').val(),
            employmentType: $('#EmploymentTypeFilter').val(),
            workMode: $('#WorkModeFilter').val(),
            experienceLevel: $('#ExperienceLevelFilter').val(),
            salaryMinMin: $('#SalaryMinFilterMin').val(),
            salaryMinMax: $('#SalaryMinFilterMax').val(),
            salaryMaxMin: $('#SalaryMaxFilterMin').val(),
            salaryMaxMax: $('#SalaryMaxFilterMax').val(),
            salaryText: $('#SalaryTextFilter').val(),
            salaryCurrency: $('#SalaryCurrencyFilter').val(),
            location: $('#LocationFilter').val(),
            contactEmail: $('#ContactEmailFilter').val(),
            contactPhone: $('#ContactPhoneFilter').val(),
            applicationUrl: $('#ApplicationUrlFilter').val(),
            publishedAtMin: $('#PublishedAtFilterMin').val(),
            publishedAtMax: $('#PublishedAtFilterMax').val(),
            status: $('#StatusFilter').val(),
            viewCountMin: $('#ViewCountFilterMin').val(),
            viewCountMax: $('#ViewCountFilterMax').val(),
            applicationCountMin: $('#ApplicationCountFilterMin').val(),
            applicationCountMax: $('#ApplicationCountFilterMax').val(),
            favoriteCountMin: $('#FavoriteCountFilterMin').val(),
            favoriteCountMax: $('#FavoriteCountFilterMax').val(),
            shareCountMin: $('#ShareCountFilterMin').val(),
            shareCountMax: $('#ShareCountFilterMax').val(),
            isFeatured: (function () {
                var value = $('#IsFeaturedFilter').val();
                if (value === undefined || value === null || value === '') {
                    return '';
                }
                return value === 'true';
            })(),
            isUrgent: (function () {
                var value = $('#IsUrgentFilter').val();
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
            seoTitle: $('#SeoTitleFilter').val(),
            seoDescription: $('#SeoDescriptionFilter').val(),
            seoKeywords: $('#SeoKeywordsFilter').val(),
            provinceId: $('#ProvinceIdFilter').val(),
            wardId: $('#WardIdFilter').val(),
            jobCategoryId: $('#JobCategoryIdFilter').val(),
        };
    };

    var dataTableColumns = [
        {
            rowAction: {
                items: [
                    {
                        text: l('Edit'),
                        visible: abp.auth.isGranted('MasterDataService.Jobs.Edit'),
                        action: function (data) {
                            window.location.href = abp.appPath + 'Jobs/Edit?id=' + data.record.job.id;
                        },
                    },
                    {
                        text: l('Delete'),
                        visible: abp.auth.isGranted('MasterDataService.Jobs.Delete'),
                        confirmMessage: function () {
                            return l('DeleteConfirmationMessage');
                        },
                        action: function (data) {
                            jobService.delete(data.record.job.id).then(function () {
                                abp.notify.success(l('SuccessfullyDeleted'));
                                dataTable.ajax.reloadEx();
                            });
                        },
                    },
                ],
            },
        },
        { data: 'job.title' },
        {
            data: 'job.summary',
            render: function (summary) {
                if (!summary) {
                    return '';
                }
                var s = String(summary);
                return s.length > 120 ? s.substring(0, 117) + '...' : s;
            },
        },
        {
            data: 'job.employmentType',

            render: function (employmentType) {
                if (employmentType === undefined || employmentType === null) {
                    return '';
                }

                var localizationKey = 'Enum:EmploymentType.' + employmentType;
                var localized = l(localizationKey);

                if (localized === localizationKey) {
                    abp.log.warn('No localization found for ' + localizationKey);
                    return '';
                }

                return localized;
            },
        },
        {
            data: 'job.workMode',

            render: function (workMode) {
                if (workMode === undefined || workMode === null) {
                    return '';
                }

                var localizationKey = 'Enum:WorkMode.' + workMode;
                var localized = l(localizationKey);

                if (localized === localizationKey) {
                    abp.log.warn('No localization found for ' + localizationKey);
                    return '';
                }

                return localized;
            },
        },
        {
            data: 'job.experienceLevel',

            render: function (experienceLevel) {
                if (experienceLevel === undefined || experienceLevel === null) {
                    return '';
                }

                var localizationKey = 'Enum:ExperienceLevel.' + experienceLevel;
                var localized = l(localizationKey);

                if (localized === localizationKey) {
                    abp.log.warn('No localization found for ' + localizationKey);
                    return '';
                }

                return localized;
            },
        },
        { data: 'job.salaryText' },
        { data: 'job.location' },
        {
            data: 'job.publishedAt',

            render: function (publishedAt) {
                if (!publishedAt) {
                    return '';
                }

                var date = Date.parse(publishedAt);
                return new Date(date).toLocaleDateString(abp.localization.currentCulture.name);
            },
        },
        {
            data: 'job.status',

            render: function (status) {
                if (status === undefined || status === null) {
                    return '';
                }

                var localizationKey = 'Enum:JobStatus.' + status;
                var localized = l(localizationKey);

                if (localized === localizationKey) {
                    abp.log.warn('No localization found for ' + localizationKey);
                    return '';
                }

                return localized;
            },
        },
        { data: 'job.applicationCount' },
        { data: 'job.favoriteCount' },
        {
            data: 'job.isFeatured',

            render: function (isFeatured) {
                return isFeatured ? '<i class="fa fa-check"></i>' : '<i class="fa fa-times"></i>';
            },
        },
        {
            data: 'job.isUrgent',

            render: function (isUrgent) {
                return isUrgent ? '<i class="fa fa-check"></i>' : '<i class="fa fa-times"></i>';
            },
        },
        {
            data: 'job.isHot',

            render: function (isHot) {
                return isHot ? '<i class="fa fa-check"></i>' : '<i class="fa fa-times"></i>';
            },
        },
        {
            data: 'province.name',

            defaultContent: '',
        },
        {
            data: 'ward.name',

            defaultContent: '',
        },
        {
            data: 'jobCategory.name',

            defaultContent: '',
        },
    ];

    if (abp.auth.isGranted('MasterDataService.Jobs.Delete')) {
        dataTableColumns.unshift({
            targets: 0,
            data: null,
            orderable: false,
            className: 'select-checkbox',
            width: '0.5rem',
            render: function (data) {
                return (
                    '<input type="checkbox" class="form-check-input select-row-checkbox" data-id="' +
                    data.job.id +
                    '"/>'
                );
            },
        });
    } else {
        $('#BulkDeleteCheckboxTheader').remove();
    }

    var dataTable = $('#JobsTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            processing: true,
            serverSide: true,
            paging: true,
            searching: false,
            responsive: true,
            order: [[2, 'desc']],
            ajax: abp.libs.datatables.createAjax(jobService.getList, getFilter),
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

                        jobService.deleteAll(getFilter()).then(function () {
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

                            jobService.deleteByIds(selectedRecordsIds).then(function () {
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

    $('#SearchForm').submit(function (e) {
        e.preventDefault();
        dataTable.ajax.reloadEx();
        selectOrUnselectAllCheckboxes(false);
        showOrHideContextMenu();
    });

    $('#ExportToExcelButton').click(function (e) {
        e.preventDefault();

        jobService.getDownloadToken().then(function (result) {
            var input = getFilter();
            var url =
                abp.appPath +
                'api/masterdata/jobs/as-excel-file' +
                abp.utils.buildQueryString([
                    { name: 'downloadToken', value: result.token },
                    { name: 'filterText', value: input.filterText },
                    { name: 'title', value: input.title },
                    { name: 'slug', value: input.slug },
                    { name: 'summary', value: input.summary },
                    { name: 'description', value: input.description },
                    { name: 'requirements', value: input.requirements },
                    { name: 'benefits', value: input.benefits },
                    { name: 'thumbnailUrl', value: input.thumbnailUrl },
                    { name: 'coverImageUrl', value: input.coverImageUrl },
                    { name: 'employmentType', value: input.employmentType },
                    { name: 'workMode', value: input.workMode },
                    { name: 'experienceLevel', value: input.experienceLevel },
                    { name: 'salaryMinMin', value: input.salaryMinMin },
                    { name: 'salaryMinMax', value: input.salaryMinMax },
                    { name: 'salaryMaxMin', value: input.salaryMaxMin },
                    { name: 'salaryMaxMax', value: input.salaryMaxMax },
                    { name: 'salaryText', value: input.salaryText },
                    { name: 'salaryCurrency', value: input.salaryCurrency },
                    { name: 'location', value: input.location },
                    { name: 'contactEmail', value: input.contactEmail },
                    { name: 'contactPhone', value: input.contactPhone },
                    { name: 'applicationUrl', value: input.applicationUrl },
                    { name: 'publishedAtMin', value: input.publishedAtMin },
                    { name: 'publishedAtMax', value: input.publishedAtMax },
                    { name: 'status', value: input.status },
                    { name: 'viewCountMin', value: input.viewCountMin },
                    { name: 'viewCountMax', value: input.viewCountMax },
                    { name: 'applicationCountMin', value: input.applicationCountMin },
                    { name: 'applicationCountMax', value: input.applicationCountMax },
                    { name: 'favoriteCountMin', value: input.favoriteCountMin },
                    { name: 'favoriteCountMax', value: input.favoriteCountMax },
                    { name: 'shareCountMin', value: input.shareCountMin },
                    { name: 'shareCountMax', value: input.shareCountMax },
                    { name: 'isFeatured', value: input.isFeatured },
                    { name: 'isUrgent', value: input.isUrgent },
                    { name: 'isHot', value: input.isHot },
                    { name: 'seoTitle', value: input.seoTitle },
                    { name: 'seoDescription', value: input.seoDescription },
                    { name: 'seoKeywords', value: input.seoKeywords },
                    { name: 'provinceId', value: input.provinceId },
                    { name: 'wardId', value: input.wardId },
                    { name: 'jobCategoryId', value: input.jobCategoryId },
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
