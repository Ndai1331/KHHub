$(function () {
    var l = abp.localization.getResource('MasterDataService');

    var placeService = window.kHHub.masterDataService.services.places.places;

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
        viewUrl: abp.appPath + 'Places/CreateModal',
        scriptUrl: abp.appPath + 'Pages/Places/createModal.js',
        modalClass: 'placeCreate',
    });

    var editModal = new abp.ModalManager({
        viewUrl: abp.appPath + 'Places/EditModal',
        scriptUrl: abp.appPath + 'Pages/Places/editModal.js',
        modalClass: 'placeEdit',
    });

    var getFilter = function () {
        return {
            filterText: $('#FilterText').val(),
            name: $('#NameFilter').val(),
            slug: $('#SlugFilter').val(),
            shortDescription: $('#ShortDescriptionFilter').val(),
            description: $('#DescriptionFilter').val(),
            thumbnailUrl: $('#ThumbnailUrlFilter').val(),
            coverImageUrl: $('#CoverImageUrlFilter').val(),
            address: $('#AddressFilter').val(),
            latitudeMin: $('#LatitudeFilterMin').val(),
            latitudeMax: $('#LatitudeFilterMax').val(),
            longitudedMin: $('#LongitudedFilterMin').val(),
            longitudedMax: $('#LongitudedFilterMax').val(),
            phoneNumber: $('#PhoneNumberFilter').val(),
            email: $('#EmailFilter').val(),
            website: $('#WebsiteFilter').val(),
            openingHours: $('#OpeningHoursFilter').val(),
            priceRange: $('#PriceRangeFilter').val(),
            googleMapUrl: $('#GoogleMapUrlFilter').val(),
            status: $('#StatusFilter').val(),
            viewCountMin: $('#ViewCountFilterMin').val(),
            viewCountMax: $('#ViewCountFilterMax').val(),
            favoriteCountMin: $('#FavoriteCountFilterMin').val(),
            favoriteCountMax: $('#FavoriteCountFilterMax').val(),
            reviewCountMin: $('#ReviewCountFilterMin').val(),
            reviewCountMax: $('#ReviewCountFilterMax').val(),
            ratingAveragedMin: $('#RatingAveragedFilterMin').val(),
            ratingAveragedMax: $('#RatingAveragedFilterMax').val(),
            ratingTotalMin: $('#RatingTotalFilterMin').val(),
            ratingTotalMax: $('#RatingTotalFilterMax').val(),
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
            isVerified: (function () {
                var value = $('#IsVerifiedFilter').val();
                if (value === undefined || value === null || value === '') {
                    return '';
                }
                return value === 'true';
            })(),
            seoTitle: $('#SeoTitleFilter').val(),
            seoDescription: $('#SeoDescriptionFilter').val(),
            seoKeywords: $('#SeoKeywordsFilter').val(),
            placeCategoryId: $('#PlaceCategoryIdFilter').val(),
            provinceId: $('#ProvinceIdFilter').val(),
            wardId: $('#WardIdFilter').val(),
        };
    };

    var dataTableColumns = [
        {
            rowAction: {
                items: [
                    {
                        text: l('Edit'),
                        visible: abp.auth.isGranted('MasterDataService.Places.Edit'),
                        action: function (data) {
                            editModal.open({
                                id: data.record.place.id,
                            });
                        },
                    },
                    {
                        text: l('Delete'),
                        visible: abp.auth.isGranted('MasterDataService.Places.Delete'),
                        confirmMessage: function () {
                            return l('DeleteConfirmationMessage');
                        },
                        action: function (data) {
                            placeService.delete(data.record.place.id).then(function () {
                                abp.notify.success(l('SuccessfullyDeleted'));
                                dataTable.ajax.reloadEx();
                            });
                        },
                    },
                ],
            },
        },
        { data: 'place.name' },
        { data: 'place.slug' },
        { data: 'place.shortDescription' },
        { data: 'place.description' },
        { data: 'place.thumbnailUrl' },
        { data: 'place.coverImageUrl' },
        { data: 'place.address' },
        { data: 'place.latitude' },
        { data: 'place.longituded' },
        { data: 'place.phoneNumber' },
        { data: 'place.email' },
        { data: 'place.website' },
        { data: 'place.openingHours' },
        {
            data: 'place.priceRange',

            render: function (priceRange) {
                if (priceRange === undefined || priceRange === null) {
                    return '';
                }

                var localizationKey = 'Enum:PriceRange.' + priceRange;
                var localized = l(localizationKey);

                if (localized === localizationKey) {
                    abp.log.warn('No localization found for ' + localizationKey);
                    return '';
                }

                return localized;
            },
        },
        { data: 'place.googleMapUrl' },
        {
            data: 'place.status',

            render: function (status) {
                if (status === undefined || status === null) {
                    return '';
                }

                var localizationKey = 'Enum:PlaceStatus.' + status;
                var localized = l(localizationKey);

                if (localized === localizationKey) {
                    abp.log.warn('No localization found for ' + localizationKey);
                    return '';
                }

                return localized;
            },
        },
        { data: 'place.viewCount' },
        { data: 'place.favoriteCount' },
        { data: 'place.reviewCount' },
        { data: 'place.ratingAveraged' },
        { data: 'place.ratingTotal' },
        {
            data: 'place.isFeatured',

            render: function (isFeatured) {
                return isFeatured ? '<i class="fa fa-check"></i>' : '<i class="fa fa-times"></i>';
            },
        },
        {
            data: 'place.isHot',

            render: function (isHot) {
                return isHot ? '<i class="fa fa-check"></i>' : '<i class="fa fa-times"></i>';
            },
        },
        {
            data: 'place.isVerified',

            render: function (isVerified) {
                return isVerified ? '<i class="fa fa-check"></i>' : '<i class="fa fa-times"></i>';
            },
        },
        { data: 'place.seoTitle' },
        { data: 'place.seoDescription' },
        { data: 'place.seoKeywords' },
        {
            data: 'placeCategory.name',

            defaultContent: '',
        },
        {
            data: 'province.name',

            defaultContent: '',
        },
        {
            data: 'ward.name',

            defaultContent: '',
        },
    ];

    if (abp.auth.isGranted('MasterDataService.Places.Delete')) {
        dataTableColumns.unshift({
            targets: 0,
            data: null,
            orderable: false,
            className: 'select-checkbox',
            width: '0.5rem',
            render: function (data) {
                return (
                    '<input type="checkbox" class="form-check-input select-row-checkbox" data-id="' +
                    data.place.id +
                    '"/>'
                );
            },
        });
    } else {
        $('#BulkDeleteCheckboxTheader').remove();
    }

    var dataTable = $('#PlacesTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            processing: true,
            serverSide: true,
            paging: true,
            searching: false,
            responsive: true,
            order: [[2, 'desc']],
            ajax: abp.libs.datatables.createAjax(placeService.getList, getFilter),
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

                        placeService.deleteAll(getFilter()).then(function () {
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

                            placeService.deleteByIds(selectedRecordsIds).then(function () {
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

    $('#NewPlaceButton').click(function (e) {
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

        placeService.getDownloadToken().then(function (result) {
            var input = getFilter();
            var url =
                abp.appPath +
                'api/masterdata/places/as-excel-file' +
                abp.utils.buildQueryString([
                    { name: 'downloadToken', value: result.token },
                    { name: 'filterText', value: input.filterText },
                    { name: 'name', value: input.name },
                    { name: 'slug', value: input.slug },
                    { name: 'shortDescription', value: input.shortDescription },
                    { name: 'description', value: input.description },
                    { name: 'thumbnailUrl', value: input.thumbnailUrl },
                    { name: 'coverImageUrl', value: input.coverImageUrl },
                    { name: 'address', value: input.address },
                    { name: 'latitudeMin', value: input.latitudeMin },
                    { name: 'latitudeMax', value: input.latitudeMax },
                    { name: 'longitudedMin', value: input.longitudedMin },
                    { name: 'longitudedMax', value: input.longitudedMax },
                    { name: 'phoneNumber', value: input.phoneNumber },
                    { name: 'email', value: input.email },
                    { name: 'website', value: input.website },
                    { name: 'openingHours', value: input.openingHours },
                    { name: 'priceRange', value: input.priceRange },
                    { name: 'googleMapUrl', value: input.googleMapUrl },
                    { name: 'status', value: input.status },
                    { name: 'viewCountMin', value: input.viewCountMin },
                    { name: 'viewCountMax', value: input.viewCountMax },
                    { name: 'favoriteCountMin', value: input.favoriteCountMin },
                    { name: 'favoriteCountMax', value: input.favoriteCountMax },
                    { name: 'reviewCountMin', value: input.reviewCountMin },
                    { name: 'reviewCountMax', value: input.reviewCountMax },
                    { name: 'ratingAveragedMin', value: input.ratingAveragedMin },
                    { name: 'ratingAveragedMax', value: input.ratingAveragedMax },
                    { name: 'ratingTotalMin', value: input.ratingTotalMin },
                    { name: 'ratingTotalMax', value: input.ratingTotalMax },
                    { name: 'isFeatured', value: input.isFeatured },
                    { name: 'isHot', value: input.isHot },
                    { name: 'isVerified', value: input.isVerified },
                    { name: 'seoTitle', value: input.seoTitle },
                    { name: 'seoDescription', value: input.seoDescription },
                    { name: 'seoKeywords', value: input.seoKeywords },
                    { name: 'placeCategoryId', value: input.placeCategoryId },
                    { name: 'provinceId', value: input.provinceId },
                    { name: 'wardId', value: input.wardId },
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
