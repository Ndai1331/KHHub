$(function () {
    var l = abp.localization.getResource('MasterDataService');

    var mediaFileService = window.kHHub.masterDataService.services.mediaFiles.mediaFiles;

    var createModal = new abp.ModalManager({
        viewUrl: abp.appPath + 'MediaFiles/CreateModal',
        scriptUrl: abp.appPath + 'Pages/MediaFiles/createModal.js',
        modalClass: 'mediaFileCreate',
    });

    var editModal = new abp.ModalManager({
        viewUrl: abp.appPath + 'MediaFiles/EditModal',
        scriptUrl: abp.appPath + 'Pages/MediaFiles/editModal.js',
        modalClass: 'mediaFileEdit',
    });

    var getFilter = function () {
        return {
            filterText: $('#FilterText').val(),
            fileName: $('#FileNameFilter').val(),
            originalFileName: $('#OriginalFileNameFilter').val(),
            extension: $('#ExtensionFilter').val(),
            contentType: $('#ContentTypeFilter').val(),
            storageProvider: $('#StorageProviderFilter').val(),
            bucket: $('#BucketFilter').val(),
            folder: $('#FolderFilter').val(),
            path: $('#PathFilter').val(),
            url: $('#UrlFilter').val(),
            checksum: $('#ChecksumFilter').val(),
            widthMin: $('#WidthFilterMin').val(),
            widthMax: $('#WidthFilterMax').val(),
            heightMin: $('#HeightFilterMin').val(),
            heightMax: $('#HeightFilterMax').val(),
            fileType: $('#FileTypeFilter').val(),
            status: $('#StatusFilter').val(),
        };
    };

    var dataTableColumns = [
        {
            rowAction: {
                items: [
                    {
                        text: l('Edit'),
                        visible: abp.auth.isGranted('MasterDataService.MediaFiles.Edit'),
                        action: function (data) {
                            editModal.open({
                                id: data.record.id,
                            });
                        },
                    },
                    {
                        text: l('Delete'),
                        visible: abp.auth.isGranted('MasterDataService.MediaFiles.Delete'),
                        confirmMessage: function () {
                            return l('DeleteConfirmationMessage');
                        },
                        action: function (data) {
                            mediaFileService.delete(data.record.id).then(function () {
                                abp.notify.success(l('SuccessfullyDeleted'));
                                dataTable.ajax.reloadEx();
                            });
                        },
                    },
                ],
            },
        },
        { data: 'fileName' },
        { data: 'originalFileName' },
        { data: 'extension' },
        { data: 'contentType' },
        { data: 'size' },
        { data: 'storageProvider' },
        { data: 'bucket' },
        { data: 'folder' },
        { data: 'path' },
        { data: 'url' },
        { data: 'checksum' },
        { data: 'width' },
        { data: 'height' },
        { data: 'duration' },
        {
            data: 'fileType',

            render: function (fileType) {
                if (fileType === undefined || fileType === null) {
                    return '';
                }

                var localizationKey = 'Enum:FileType.' + fileType;
                var localized = l(localizationKey);

                if (localized === localizationKey) {
                    abp.log.warn('No localization found for ' + localizationKey);
                    return '';
                }

                return localized;
            },
        },
        {
            data: 'status',

            render: function (status) {
                if (status === undefined || status === null) {
                    return '';
                }

                var localizationKey = 'Enum:FileStatus.' + status;
                var localized = l(localizationKey);

                if (localized === localizationKey) {
                    abp.log.warn('No localization found for ' + localizationKey);
                    return '';
                }

                return localized;
            },
        },
    ];

    if (abp.auth.isGranted('MasterDataService.MediaFiles.Delete')) {
        dataTableColumns.unshift({
            targets: 0,
            data: null,
            orderable: false,
            className: 'select-checkbox',
            width: '0.5rem',
            render: function (data) {
                return (
                    '<input type="checkbox" class="form-check-input select-row-checkbox" data-id="' +
                    data.id +
                    '"/>'
                );
            },
        });
    } else {
        $('#BulkDeleteCheckboxTheader').remove();
    }

    var dataTable = $('#MediaFilesTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            processing: true,
            serverSide: true,
            paging: true,
            searching: false,
            responsive: true,
            order: [[2, 'desc']],
            ajax: abp.libs.datatables.createAjax(mediaFileService.getList, getFilter),
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

                        mediaFileService.deleteAll(getFilter()).then(function () {
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

                            mediaFileService.deleteByIds(selectedRecordsIds).then(function () {
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

    $('#NewMediaFileButton').click(function (e) {
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

        mediaFileService.getDownloadToken().then(function (result) {
            var input = getFilter();
            var url =
                abp.appPath +
                'api/masterdata/media-files/as-excel-file' +
                abp.utils.buildQueryString([
                    { name: 'downloadToken', value: result.token },
                    { name: 'filterText', value: input.filterText },
                    { name: 'fileName', value: input.fileName },
                    { name: 'originalFileName', value: input.originalFileName },
                    { name: 'extension', value: input.extension },
                    { name: 'contentType', value: input.contentType },
                    { name: 'storageProvider', value: input.storageProvider },
                    { name: 'bucket', value: input.bucket },
                    { name: 'folder', value: input.folder },
                    { name: 'path', value: input.path },
                    { name: 'url', value: input.url },
                    { name: 'checksum', value: input.checksum },
                    { name: 'widthMin', value: input.widthMin },
                    { name: 'widthMax', value: input.widthMax },
                    { name: 'heightMin', value: input.heightMin },
                    { name: 'heightMax', value: input.heightMax },
                    { name: 'fileType', value: input.fileType },
                    { name: 'status', value: input.status },
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
