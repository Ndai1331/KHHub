$(function () {
    var l = abp.localization.getResource('MasterDataService');

    var jobCategoryService =
        window.kHHub.masterDataService.services.jobCategories.jobCategories;

    var createModal = new abp.ModalManager({
        viewUrl: abp.appPath + 'JobCategories/CreateModal',
        scriptUrl: abp.appPath + 'Pages/JobCategories/createModal.js',
        modalClass: 'jobCategoryCreate',
    });

    var editModal = new abp.ModalManager({
        viewUrl: abp.appPath + 'JobCategories/EditModal',
        scriptUrl: abp.appPath + 'Pages/JobCategories/editModal.js',
        modalClass: 'jobCategoryEdit',
    });

    var getFilter = function () {
        var isActiveVal = $('#IsActiveFilter').val();
        var isActive =
            isActiveVal === undefined || isActiveVal === null || isActiveVal === ''
                ? ''
                : isActiveVal === 'true';

        return {
            filterText: $('#FilterText').val(),
            parentId: $('#ParentIdFilter').val(),
            isActive: isActive,
        };
    };

    var canEdit = abp.auth.isGranted('MasterDataService.JobCategories.Edit');
    var canDelete = abp.auth.isGranted('MasterDataService.JobCategories.Delete');

    function renderColorPreviewCell(row) {
        var color = (row.color || '').trim();
        var icon = (row.icon || '').trim();
        var bg = /^#([0-9a-f]{3}|[0-9a-f]{6})$/i.test(color) ? color : '#e5e7eb';
        var iconHtml = '';
        if (icon && icon.indexOf('fa') >= 0) {
            var safeIcon = String(icon)
                .replace(/"/g, '&quot;')
                .replace(/</g, '')
                .replace(/>/g, '');
            iconHtml =
                '<i class="' +
                safeIcon +
                '" style="font-size:1.25rem;color:'+bg+';" aria-hidden="true"></i>';
        }
        return (
            '<div class="d-inline-flex align-items-center justify-content-center rounded border bg-body" ' +
            'style="width:48px;height:48px;background:#ffffff' +
            '!important;">' +
            iconHtml +
            '</div>'
        );
    }

    var dataTable = $('#JobCategoriesTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            processing: true,
            serverSide: true,
            paging: true,
            searching: false,
            responsive: true,
            order: [[7, 'asc']],
            ajax: abp.libs.datatables.createAjax(jobCategoryService.getList, getFilter),
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
                {
                    data: null,
                    orderable: false,
                    className: 'text-nowrap',
                    render: function (data, type, row) {
                        return renderColorPreviewCell(row);
                    },
                },
                { data: 'name' },
                { data: 'slug' },
                {
                    data: 'description',
                    defaultContent: '',
                    render: function (v) {
                        return v || '-';
                    },
                },
                {
                    data: 'icon',
                    defaultContent: '',
                    render: function (v) {
                        return v || '-';
                    },
                },
                {
                    data: 'parentId',
                    defaultContent: '',
                    render: function (id) {
                        return id ? id : '-';
                    },
                },
                { data: 'displayOrder' },
                {
                    data: 'isActive',
                    render: function (isActive) {
                        return isActive
                            ? '<span class="badge bg-success-subtle text-success">' +
                                  l('Yes') +
                                  '</span>'
                            : '<span class="badge bg-secondary-subtle text-secondary">' +
                                  l('No') +
                                  '</span>';
                    },
                },
            ],
        })
    );

    $('#NewJobCategoryButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });

    $('#JobCategoriesTable').on('click', '.action-edit', function (e) {
        e.preventDefault();
        e.stopPropagation();
        editModal.open({ id: $(this).data('id') });
    });

    $('#JobCategoriesTable').on('click', '.action-delete', function (e) {
        e.preventDefault();
        e.stopPropagation();
        var id = $(this).data('id');

        abp.message.confirm(l('DeleteConfirmationMessage')).then(function (confirmed) {
            if (!confirmed) {
                return;
            }

            jobCategoryService.delete(id).then(function () {
                abp.notify.success(l('SuccessfullyDeleted'));
                dataTable.ajax.reload();
            });
        });
    });

    $('#SearchForm').on('submit', function (e) {
        e.preventDefault();
        dataTable.ajax.reload();
    });

    $('#ParentIdFilter, #IsActiveFilter').on('change', function () {
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

        jobCategoryService.getDownloadToken().then(function (result) {
            var input = getFilter();
            var url =
                abp.appPath +
                'api/masterdata/job-categories/as-excel-file' +
                abp.utils.buildQueryString([
                    { name: 'downloadToken', value: result.token },
                    { name: 'filterText', value: input.filterText },
                    { name: 'parentId', value: input.parentId },
                    { name: 'isActive', value: input.isActive },
                ]);

            var downloadWindow = window.open(url, '_blank');
            downloadWindow.focus();
        });
    });
});
