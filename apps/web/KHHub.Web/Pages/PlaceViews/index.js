$(function () {
    var l = abp.localization.getResource('MasterDataService');

    var placeViewService = window.kHHub.masterDataService.services.placeViews.placeViews;

    var createModal = new abp.ModalManager({
        viewUrl: abp.appPath + 'PlaceViews/CreateModal',
        scriptUrl: abp.appPath + 'Pages/PlaceViews/createModal.js',
        modalClass: 'placeViewCreate',
    });

    var editModal = new abp.ModalManager({
        viewUrl: abp.appPath + 'PlaceViews/EditModal',
        scriptUrl: abp.appPath + 'Pages/PlaceViews/editModal.js',
        modalClass: 'placeViewEdit',
    });

    var getFilter = function () {
        return {
            filterText: $('#FilterText').val(),
            placeId: $('#PlaceIdFilter').val(),
        };
    };

    var canEdit = abp.auth.isGranted('MasterDataService.PlaceViews.Edit');
    var canDelete = abp.auth.isGranted('MasterDataService.PlaceViews.Delete');

    var dataTable = $('#PlaceViewsTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            processing: true,
            serverSide: true,
            paging: true,
            searching: false,
            responsive: true,
            order: [[4, 'desc']],
            ajax: abp.libs.datatables.createAjax(placeViewService.getList, getFilter),
            columnDefs: [
                {
                    data: null,
                    orderable: false,
                    render: function (data, type, row) {
                        var html = '<div class="d-flex gap-1">';
                        var id = row.placeView.id;

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
                    data: 'placeView.userId',
                    defaultContent: '',
                    render: function (userId) {
                        return userId || '-';
                    },
                },
                {
                    data: 'placeView.ipAddress',
                    defaultContent: '',
                },
                {
                    data: 'placeView.device',
                    defaultContent: '',
                },
                {
                    data: 'placeView.viewedAt',
                    render: function (viewedAt) {
                        if (!viewedAt) {
                            return '-';
                        }
                        return new Date(Date.parse(viewedAt)).toLocaleString(abp.localization.currentCulture.name);
                    },
                },
                { data: 'placeView.duration' },
                {
                    data: 'placeView.source',
                    defaultContent: '',
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

    $('#NewPlaceViewButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });

    $('#PlaceViewsTable').on('click', '.action-edit', function (e) {
        e.preventDefault();
        e.stopPropagation();
        editModal.open({ id: $(this).data('id') });
    });

    $('#PlaceViewsTable').on('click', '.action-delete', function (e) {
        e.preventDefault();
        e.stopPropagation();
        var id = $(this).data('id');

        abp.message.confirm(l('DeleteConfirmationMessage')).then(function (confirmed) {
            if (!confirmed) {
                return;
            }

            placeViewService.delete(id).then(function () {
                abp.notify.success(l('SuccessfullyDeleted'));
                dataTable.ajax.reload();
            });
        });
    });

    $('#SearchForm').on('submit', function (e) {
        e.preventDefault();
        dataTable.ajax.reload();
    });

    $('#PlaceIdFilter').on('change', function () {
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

        placeViewService.getDownloadToken().then(function (result) {
            var input = getFilter();
            var url =
                abp.appPath +
                'api/masterdata/place-views/as-excel-file' +
                abp.utils.buildQueryString([
                    { name: 'downloadToken', value: result.token },
                    { name: 'filterText', value: input.filterText },
                    { name: 'placeId', value: input.placeId },
                ]);

            var downloadWindow = window.open(url, '_blank');
            downloadWindow.focus();
        });
    });
});
