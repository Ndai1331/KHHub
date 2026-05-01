$(function () {
    var l = abp.localization.getResource('MasterDataService');

    var placeTagMappingService =
        window.kHHub.masterDataService.services.placeTagMappings.placeTagMappings;

    var createModal = new abp.ModalManager({
        viewUrl: abp.appPath + 'PlaceTagMappings/CreateModal',
        scriptUrl: abp.appPath + 'Pages/PlaceTagMappings/createModal.js',
        modalClass: 'placeTagMappingCreate',
    });

    var editModal = new abp.ModalManager({
        viewUrl: abp.appPath + 'PlaceTagMappings/EditModal',
        scriptUrl: abp.appPath + 'Pages/PlaceTagMappings/editModal.js',
        modalClass: 'placeTagMappingEdit',
    });

    var getFilter = function () {
        var isPrimaryVal = $('#IsPrimaryFilter').val();
        var isPrimary =
            isPrimaryVal === undefined || isPrimaryVal === null || isPrimaryVal === ''
                ? ''
                : isPrimaryVal === 'true';

        return {
            filterText: $('#FilterText').val(),
            placeId: $('#PlaceIdFilter').val(),
            placeTagId: $('#PlaceTagIdFilter').val(),
            isPrimary: isPrimary,
        };
    };

    var canEdit = abp.auth.isGranted('MasterDataService.PlaceTagMappings.Edit');
    var canDelete = abp.auth.isGranted('MasterDataService.PlaceTagMappings.Delete');

    var dataTable = $('#PlaceTagMappingsTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            processing: true,
            serverSide: true,
            paging: true,
            searching: false,
            responsive: true,
            order: [[2, 'asc']],
            ajax: abp.libs.datatables.createAjax(placeTagMappingService.getList, getFilter),
            columnDefs: [
                {
                    data: null,
                    orderable: false,
                    render: function (data, type, row) {
                        var html = '<div class="d-flex gap-1">';
                        var id = row.placeTagMapping.id;

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
                    data: 'placeTagMapping.isPrimary',
                    render: function (isPrimary) {
                        return isPrimary
                            ? '<span class="badge bg-primary-subtle text-primary">' + l('Yes') + '</span>'
                            : '<span class="badge bg-secondary-subtle text-secondary">' + l('No') + '</span>';
                    },
                },
                { data: 'placeTagMapping.sortOrder' },
                {
                    data: 'placeTag.name',
                    defaultContent: '',
                    render: function (name, type, row) {
                        return name || row.placeTag.slug || '-';
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

    $('#NewPlaceTagMappingButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });

    $('#PlaceTagMappingsTable').on('click', '.action-edit', function (e) {
        e.preventDefault();
        e.stopPropagation();
        editModal.open({ id: $(this).data('id') });
    });

    $('#PlaceTagMappingsTable').on('click', '.action-delete', function (e) {
        e.preventDefault();
        e.stopPropagation();
        var id = $(this).data('id');

        abp.message.confirm(l('DeleteConfirmationMessage')).then(function (confirmed) {
            if (!confirmed) {
                return;
            }

            placeTagMappingService.delete(id).then(function () {
                abp.notify.success(l('SuccessfullyDeleted'));
                dataTable.ajax.reload();
            });
        });
    });

    $('#SearchForm').on('submit', function (e) {
        e.preventDefault();
        dataTable.ajax.reload();
    });

    $('#PlaceIdFilter, #PlaceTagIdFilter, #IsPrimaryFilter').on('change', function () {
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

        placeTagMappingService.getDownloadToken().then(function (result) {
            var input = getFilter();
            var url =
                abp.appPath +
                'api/masterdata/place-tag-mappings/as-excel-file' +
                abp.utils.buildQueryString([
                    { name: 'downloadToken', value: result.token },
                    { name: 'filterText', value: input.filterText },
                    { name: 'placeId', value: input.placeId },
                    { name: 'placeTagId', value: input.placeTagId },
                    { name: 'isPrimary', value: input.isPrimary },
                ]);

            var downloadWindow = window.open(url, '_blank');
            downloadWindow.focus();
        });
    });
});
