$(function () {
    var l = abp.localization.getResource('MasterDataService');

    var wardService = window.kHHub.masterDataService.services.wards.wards;

    var createModal = new abp.ModalManager({
        viewUrl: abp.appPath + 'Wards/CreateModal',
        scriptUrl: abp.appPath + 'Pages/Wards/createModal.js',
        modalClass: 'wardCreate',
    });

    var editModal = new abp.ModalManager({
        viewUrl: abp.appPath + 'Wards/EditModal',
        scriptUrl: abp.appPath + 'Pages/Wards/editModal.js',
        modalClass: 'wardEdit',
    });

    var getFilter = function () {
        return {
            filterText: $('#FilterText').val(),
            code: $('#CodeFilter').val(),
            name: $('#NameFilter').val(),
            provinceId: $('#ProvinceIdFilter').val(),
        };
    };

    var canEdit = abp.auth.isGranted('MasterDataService.Wards.Edit');
    var canDelete = abp.auth.isGranted('MasterDataService.Wards.Delete');

    var dataTable = $('#WardsTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            processing: true,
            serverSide: true,
            paging: true,
            searching: false,
            responsive: true,
            order: [[2, 'asc']],
            ajax: abp.libs.datatables.createAjax(wardService.getList, getFilter),
            columnDefs: [
                {
                    data: null,
                    orderable: false,
                    render: function (data, type, row) {
                        var wardId = row.ward.id;
                        var html = '<div class="d-flex gap-1">';

                        if (canEdit) {
                            html +=
                                '<button type="button" class="btn btn-sm btn-outline-primary action-edit" data-id="' +
                                wardId +
                                '" title="' +
                                l('Edit') +
                                '"><i class="fa fa-pen"></i></button>';
                        }

                        if (canDelete) {
                            html +=
                                '<button type="button" class="btn btn-sm btn-outline-danger action-delete" data-id="' +
                                wardId +
                                '" title="' +
                                l('Delete') +
                                '"><i class="fa fa-trash"></i></button>';
                        }

                        html += '</div>';
                        return html;
                    },
                },
                { data: 'ward.code' },
                { data: 'ward.name' },
                {
                    data: 'province.name',
                    defaultContent: '',
                    render: function (v) {
                        return v || '-';
                    },
                },
            ],
        })
    );

    $('#NewWardButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });

    $('#WardsTable').on('click', '.action-edit', function (e) {
        e.preventDefault();
        e.stopPropagation();
        editModal.open({ id: $(this).data('id') });
    });

    $('#WardsTable').on('click', '.action-delete', function (e) {
        e.preventDefault();
        e.stopPropagation();
        var id = $(this).data('id');

        abp.message.confirm(l('DeleteConfirmationMessage')).then(function (confirmed) {
            if (!confirmed) {
                return;
            }

            wardService.delete(id).then(function () {
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

    $('#ProvinceIdFilter').on('change', function () {
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

        wardService.getDownloadToken().then(function (result) {
            var input = getFilter();
            var url =
                abp.appPath +
                'api/masterdata/wards/as-excel-file' +
                abp.utils.buildQueryString([
                    { name: 'downloadToken', value: result.token },
                    { name: 'filterText', value: input.filterText },
                    { name: 'code', value: input.code },
                    { name: 'name', value: input.name },
                    { name: 'provinceId', value: input.provinceId },
                ]);

            var downloadWindow = window.open(url, '_blank');
            downloadWindow.focus();
        });
    });
});
