$(function () {
    var l = abp.localization.getResource('MasterDataService');

    var placeService = window.kHHub.masterDataService.services.places.places;

    function parseBoolFilter(sel) {
        var value = $(sel).val();
        if (value === undefined || value === null || value === '') {
            return '';
        }
        return value === 'true';
    }

    var getFilter = function () {
        return {
            filterText: $('#FilterText').val(),
            status: $('#StatusFilter').val(),
            priceRange: $('#PriceRangeFilter').val(),
            placeCategoryId: $('#PlaceCategoryIdFilter').val(),
            provinceId: $('#ProvinceIdFilter').val(),
            wardId: $('#WardIdFilter').val(),
            isFeatured: parseBoolFilter('#IsFeaturedFilter'),
            isHot: parseBoolFilter('#IsHotFilter'),
            isVerified: parseBoolFilter('#IsVerifiedFilter'),
        };
    };

    var canEdit = abp.auth.isGranted('MasterDataService.Places.Edit');
    var canDelete = abp.auth.isGranted('MasterDataService.Places.Delete');

    var getPlaceStatusBadge = function (status) {
        if (status === 1) {
            return '<span class="badge bg-success-subtle text-success">' + l('Enum:PlaceStatus.1') + '</span>';
        }
        if (status === 0) {
            return '<span class="badge bg-secondary-subtle text-secondary">' + l('Enum:PlaceStatus.0') + '</span>';
        }
        if (status === 2) {
            return '<span class="badge bg-warning-subtle text-warning">' + l('Enum:PlaceStatus.2') + '</span>';
        }
        if (status === 3) {
            return '<span class="badge bg-light text-dark">' + l('Enum:PlaceStatus.3') + '</span>';
        }
        return '<span class="badge bg-light text-dark">' + l('Enum:PlaceStatus.' + status) + '</span>';
    };

    var dataTable = $('#PlacesTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            processing: true,
            serverSide: true,
            paging: true,
            searching: false,
            responsive: true,
            order: [[1, 'asc']],
            ajax: abp.libs.datatables.createAjax(placeService.getList, getFilter),
            rowCallback: function (row, data) {
                $(row).attr('data-id', data.place.id);
                $(row).addClass('place-row');
            },
            columnDefs: [
                {
                    data: null,
                    orderable: false,
                    render: function (data, type, row) {
                        var html = '<div class="d-flex gap-1">';
                        var id = row.place.id;

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
                    data: 'place.name',
                    render: function (name, type, row) {
                        return (
                            '<div class="fw-semibold">' +
                            (name || '') +
                            '</div><div class="text-muted small">' +
                            (row.place.slug || '') +
                            '</div>'
                        );
                    },
                },
                {
                    data: 'placeCategory.name',
                    defaultContent: '',
                    render: function (v) {
                        return v || '-';
                    },
                },
                {
                    data: 'province.name',
                    defaultContent: '',
                    render: function (v) {
                        return v || '-';
                    },
                },
                {
                    data: 'ward.name',
                    defaultContent: '',
                    render: function (v) {
                        return v || '-';
                    },
                },
                {
                    data: 'place.priceRange',
                    render: function (priceRange) {
                        if (priceRange === undefined || priceRange === null) {
                            return '-';
                        }
                        return l('Enum:PriceRange.' + priceRange);
                    },
                },
                {
                    data: 'place.status',
                    render: function (status) {
                        if (status === undefined || status === null) {
                            return '-';
                        }
                        return getPlaceStatusBadge(status);
                    },
                },
                {
                    data: 'place.isFeatured',
                    render: function (v) {
                        return v
                            ? '<span class="badge bg-primary-subtle text-primary">' + l('Yes') + '</span>'
                            : '<span class="badge bg-secondary-subtle text-secondary">' + l('No') + '</span>';
                    },
                },
                {
                    data: 'place.isHot',
                    render: function (v) {
                        return v
                            ? '<span class="badge bg-primary-subtle text-primary">' + l('Yes') + '</span>'
                            : '<span class="badge bg-secondary-subtle text-secondary">' + l('No') + '</span>';
                    },
                },
                {
                    data: 'place.isVerified',
                    render: function (v) {
                        return v
                            ? '<span class="badge bg-primary-subtle text-primary">' + l('Yes') + '</span>'
                            : '<span class="badge bg-secondary-subtle text-secondary">' + l('No') + '</span>';
                    },
                },
            ],
        })
    );

    $('#NewPlaceButton').click(function (e) {
        e.preventDefault();
        window.location.href = abp.appPath + 'Places/Create';
    });

    $('#PlacesTable').on('click', '.action-edit', function (e) {
        e.preventDefault();
        e.stopPropagation();
        var id = $(this).data('id');
        window.location.href = abp.appPath + 'Places/Edit?id=' + id;
    });

    $('#PlacesTable tbody').on('click', 'tr.place-row', function (e) {
        if ($(e.target).closest('button, a, input, .dropdown').length > 0) {
            return;
        }

        if (!canEdit) {
            return;
        }

        var id = $(this).attr('data-id');
        if (id) {
            window.location.href = abp.appPath + 'Places/Edit?id=' + id;
        }
    });

    $('#PlacesTable').on('click', '.action-delete', function (e) {
        e.preventDefault();
        e.stopPropagation();
        var id = $(this).data('id');

        abp.message.confirm(l('DeleteConfirmationMessage')).then(function (confirmed) {
            if (!confirmed) {
                return;
            }

            placeService.delete(id).then(function () {
                abp.notify.success(l('SuccessfullyDeleted'));
                dataTable.ajax.reload();
            });
        });
    });

    $('#SearchForm').on('submit', function (e) {
        e.preventDefault();
        dataTable.ajax.reload();
    });

    $(
        '#StatusFilter, #PriceRangeFilter, #PlaceCategoryIdFilter, #ProvinceIdFilter, #WardIdFilter, #IsFeaturedFilter, #IsHotFilter, #IsVerifiedFilter'
    ).on('change', function () {
        dataTable.ajax.reload();
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
                    { name: 'status', value: input.status },
                    { name: 'priceRange', value: input.priceRange },
                    { name: 'placeCategoryId', value: input.placeCategoryId },
                    { name: 'provinceId', value: input.provinceId },
                    { name: 'wardId', value: input.wardId },
                    { name: 'isFeatured', value: input.isFeatured },
                    { name: 'isHot', value: input.isHot },
                    { name: 'isVerified', value: input.isVerified },
                ]);

            var downloadWindow = window.open(url, '_blank');
            downloadWindow.focus();
        });
    });
});
