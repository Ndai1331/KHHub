$(function () {
    var l = abp.localization.getResource('MasterDataService');

    var provinceService = window.kHHub.masterDataService.services.provinces.provinces;

    var createModal = new abp.ModalManager({
        viewUrl: abp.appPath + 'Provinces/CreateModal',
        scriptUrl: abp.appPath + 'Pages/Provinces/createModal.js',
        modalClass: 'provinceCreate',
    });

    var editModal = new abp.ModalManager({
        viewUrl: abp.appPath + 'Provinces/EditModal',
        scriptUrl: abp.appPath + 'Pages/Provinces/editModal.js',
        modalClass: 'provinceEdit',
    });

    var getFilter = function () {
        return {
            filterText: $('#FilterText').val(),
            code: $('#CodeFilter').val(),
            name: $('#NameFilter').val(),
        };
    };

    var canEdit = abp.auth.isGranted('MasterDataService.Provinces.Edit');
    var canDelete = abp.auth.isGranted('MasterDataService.Provinces.Delete');

    var dataTable = $('#ProvincesTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            processing: true,
            serverSide: true,
            paging: true,
            searching: false,
            responsive: true,
            order: [[2, 'asc']],
            ajax: abp.libs.datatables.createAjax(provinceService.getList, getFilter),
            columnDefs: [
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
                { data: 'code' },
                { data: 'name' },
            ],
        })
    );

    $('#NewProvinceButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });

    $('#ProvincesTable').on('click', '.action-edit', function (e) {
        e.preventDefault();
        e.stopPropagation();
        editModal.open({ id: $(this).data('id') });
    });

    $('#ProvincesTable').on('click', '.action-delete', function (e) {
        e.preventDefault();
        e.stopPropagation();
        var id = $(this).data('id');

        abp.message.confirm(l('DeleteConfirmationMessage')).then(function (confirmed) {
            if (!confirmed) {
                return;
            }

            provinceService.delete(id).then(function () {
                abp.notify.success(l('SuccessfullyDeleted'));
                dataTable.ajax.reload();
            });
        });
    });

    $('#SearchForm').on('submit', function (e) {
        e.preventDefault();
        dataTable.ajax.reload();
    });

    $('#CodeFilter, #NameFilter').on('change', function () {
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

        provinceService.getDownloadToken().then(function (result) {
            var input = getFilter();
            var url =
                abp.appPath +
                'api/masterdata/provinces/as-excel-file' +
                abp.utils.buildQueryString([
                    { name: 'downloadToken', value: result.token },
                    { name: 'filterText', value: input.filterText },
                    { name: 'code', value: input.code },
                    { name: 'name', value: input.name },
                ]);

            var downloadWindow = window.open(url, '_blank');
            downloadWindow.focus();
        });
    });
});
