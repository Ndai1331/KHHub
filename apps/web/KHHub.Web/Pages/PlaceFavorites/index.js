$(function () {
    var l = abp.localization.getResource('MasterDataService');

    var placeFavoriteService = window.kHHub.masterDataService.services.placeFavorites.placeFavorites;

    var createModal = new abp.ModalManager({
        viewUrl: abp.appPath + 'PlaceFavorites/CreateModal',
        scriptUrl: abp.appPath + 'Pages/PlaceFavorites/createModal.js',
        modalClass: 'placeFavoriteCreate',
    });

    var editModal = new abp.ModalManager({
        viewUrl: abp.appPath + 'PlaceFavorites/EditModal',
        scriptUrl: abp.appPath + 'Pages/PlaceFavorites/editModal.js',
        modalClass: 'placeFavoriteEdit',
    });

    var getFilter = function () {
        return {
            filterText: $('#FilterText').val(),
            placeId: $('#PlaceIdFilter').val(),
            userId: $('#UserIdFilter').val(),
        };
    };

    var canEdit = abp.auth.isGranted('MasterDataService.PlaceFavorites.Edit');
    var canDelete = abp.auth.isGranted('MasterDataService.PlaceFavorites.Delete');

    var dataTable = $('#PlaceFavoritesTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            processing: true,
            serverSide: true,
            paging: true,
            searching: false,
            responsive: true,
            order: [[1, 'asc']],
            ajax: abp.libs.datatables.createAjax(placeFavoriteService.getList, getFilter),
            columnDefs: [
                {
                    data: null,
                    orderable: false,
                    render: function (data, type, row) {
                        var html = '<div class="d-flex gap-1">';
                        var id = row.placeFavorite.id;

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
                    data: 'placeFavorite.userId',
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

    $('#NewPlaceFavoriteButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });

    $('#PlaceFavoritesTable').on('click', '.action-edit', function (e) {
        e.preventDefault();
        e.stopPropagation();
        editModal.open({ id: $(this).data('id') });
    });

    $('#PlaceFavoritesTable').on('click', '.action-delete', function (e) {
        e.preventDefault();
        e.stopPropagation();
        var id = $(this).data('id');

        abp.message.confirm(l('DeleteConfirmationMessage')).then(function (confirmed) {
            if (!confirmed) {
                return;
            }

            placeFavoriteService.delete(id).then(function () {
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

    $('#UserIdFilter').on('blur', function () {
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

        placeFavoriteService.getDownloadToken().then(function (result) {
            var input = getFilter();
            var url =
                abp.appPath +
                'api/masterdata/place-favorites/as-excel-file' +
                abp.utils.buildQueryString([
                    { name: 'downloadToken', value: result.token },
                    { name: 'filterText', value: input.filterText },
                    { name: 'placeId', value: input.placeId },
                    { name: 'userId', value: input.userId },
                ]);

            var downloadWindow = window.open(url, '_blank');
            downloadWindow.focus();
        });
    });
});
