$(function () {
    var l = abp.localization.getResource('MasterDataService');

    var placeReviewService = window.kHHub.masterDataService.services.placeReviews.placeReviews;

    var createModal = new abp.ModalManager({
        viewUrl: abp.appPath + 'PlaceReviews/CreateModal',
        scriptUrl: abp.appPath + 'Pages/PlaceReviews/createModal.js',
        modalClass: 'placeReviewCreate',
    });

    var editModal = new abp.ModalManager({
        viewUrl: abp.appPath + 'PlaceReviews/EditModal',
        scriptUrl: abp.appPath + 'Pages/PlaceReviews/editModal.js',
        modalClass: 'placeReviewEdit',
    });

    var getFilter = function () {
        return {
            filterText: $('#FilterText').val(),
            placeId: $('#PlaceIdFilter').val(),
            status: $('#StatusFilter').val(),
        };
    };

    var canEdit = abp.auth.isGranted('MasterDataService.PlaceReviews.Edit');
    var canDelete = abp.auth.isGranted('MasterDataService.PlaceReviews.Delete');

    var getReviewStatusBadge = function (status) {
        if (status === 1) {
            return '<span class="badge bg-success-subtle text-success">' + l('Enum:PlaceReviewStatus.1') + '</span>';
        }
        if (status === 0) {
            return '<span class="badge bg-warning-subtle text-warning">' + l('Enum:PlaceReviewStatus.0') + '</span>';
        }
        if (status === 2) {
            return '<span class="badge bg-danger-subtle text-danger">' + l('Enum:PlaceReviewStatus.2') + '</span>';
        }
        return '<span class="badge bg-light text-dark">' + l('Enum:PlaceReviewStatus.' + status) + '</span>';
    };

    var dataTable = $('#PlaceReviewsTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            processing: true,
            serverSide: true,
            paging: true,
            searching: false,
            responsive: true,
            order: [[1, 'desc']],
            ajax: abp.libs.datatables.createAjax(placeReviewService.getList, getFilter),
            columnDefs: [
                {
                    data: null,
                    orderable: false,
                    render: function (data, type, row) {
                        var html = '<div class="d-flex gap-1">';
                        var id = row.placeReview.id;

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
                { data: 'placeReview.rating' },
                {
                    data: 'placeReview.title',
                    defaultContent: '',
                    render: function (v) {
                        return v || '-';
                    },
                },
                {
                    data: 'placeReview.comment',
                    defaultContent: '',
                    render: function (v) {
                        return v ? String(v).substring(0, 80) + (String(v).length > 80 ? '…' : '') : '-';
                    },
                },
                { data: 'placeReview.likeCount' },
                {
                    data: 'placeReview.status',
                    render: function (status) {
                        if (status === undefined || status === null) {
                            return '-';
                        }
                        return getReviewStatusBadge(status);
                    },
                },
                {
                    data: 'placeReview.userId',
                    defaultContent: '',
                    render: function (userId) {
                        return userId || '-';
                    },
                },
                {
                    data: 'place.name',
                    defaultContent: '',
                    render: function (name) {
                        return name || '-';
                    },
                },
            ],
        })
    );

    $('#NewPlaceReviewButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });

    $('#PlaceReviewsTable').on('click', '.action-edit', function (e) {
        e.preventDefault();
        e.stopPropagation();
        editModal.open({ id: $(this).data('id') });
    });

    $('#PlaceReviewsTable').on('click', '.action-delete', function (e) {
        e.preventDefault();
        e.stopPropagation();
        var id = $(this).data('id');

        abp.message.confirm(l('DeleteConfirmationMessage')).then(function (confirmed) {
            if (!confirmed) {
                return;
            }

            placeReviewService.delete(id).then(function () {
                abp.notify.success(l('SuccessfullyDeleted'));
                dataTable.ajax.reload();
            });
        });
    });

    $('#SearchForm').on('submit', function (e) {
        e.preventDefault();
        dataTable.ajax.reload();
    });

    $('#PlaceIdFilter, #StatusFilter').on('change', function () {
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

        placeReviewService.getDownloadToken().then(function (result) {
            var input = getFilter();
            var url =
                abp.appPath +
                'api/masterdata/place-reviews/as-excel-file' +
                abp.utils.buildQueryString([
                    { name: 'downloadToken', value: result.token },
                    { name: 'filterText', value: input.filterText },
                    { name: 'placeId', value: input.placeId },
                    { name: 'status', value: input.status },
                ]);

            var downloadWindow = window.open(url, '_blank');
            downloadWindow.focus();
        });
    });
});
