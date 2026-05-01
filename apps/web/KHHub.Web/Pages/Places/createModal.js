var abp = abp || {};

//<suite-custom-code-block-1>
//</suite-custom-code-block-1>

abp.modals.placeCreate = function () {
    var initModal = function (publicApi, args) {
        var l = abp.localization.getResource('MasterDataService');

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

        publicApi.onOpen(function () {
            $('#Place_PlaceCategoryId').select2({
                dropdownParent: $('#PlaceCreateModal'),
                ajax: {
                    url: abp.appPath + 'api/masterdata/places/place-category-lookup',
                    type: 'GET',
                    data: function (params) {
                        return { filter: params.term, maxResultCount: 10 };
                    },
                    processResults: function (data) {
                        var mappedItems = _.map(data.items, function (item) {
                            return { id: item.id, text: item.displayName };
                        });

                        return { results: mappedItems };
                    },
                },
            });
        });
        publicApi.onOpen(function () {
            $('#Place_ProvinceId').select2({
                dropdownParent: $('#PlaceCreateModal'),
                ajax: {
                    url: abp.appPath + 'api/masterdata/places/province-lookup',
                    type: 'GET',
                    data: function (params) {
                        return { filter: params.term, maxResultCount: 10 };
                    },
                    processResults: function (data) {
                        var mappedItems = _.map(data.items, function (item) {
                            return { id: item.id, text: item.displayName };
                        });

                        return { results: mappedItems };
                    },
                },
            });
        });
        publicApi.onOpen(function () {
            $('#Place_WardId').select2({
                dropdownParent: $('#PlaceCreateModal'),
                ajax: {
                    url: abp.appPath + 'api/masterdata/places/ward-lookup',
                    type: 'GET',
                    data: function (params) {
                        return { filter: params.term, maxResultCount: 10 };
                    },
                    processResults: function (data) {
                        var mappedItems = _.map(data.items, function (item) {
                            return { id: item.id, text: item.displayName };
                        });

                        return { results: mappedItems };
                    },
                },
            });
        });
    };

    //<suite-custom-code-block-2>
    //</suite-custom-code-block-2>

    return {
        initModal: initModal,
    };
};

//<suite-custom-code-block-3>
//</suite-custom-code-block-3>
