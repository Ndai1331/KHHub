var abp = abp || {};

function bindPlaceCategoryColorPreview(publicApi) {
    publicApi.onOpen(function () {
        var $modal = publicApi.getModal && $(publicApi.getModal());
        if (!$modal || !$modal.length) {
            return;
        }
        var $prev = $modal.find('#PlaceCategoryColorPreview');
        var $color = $modal.find('[name="PlaceCategory.Color"]');
        if (!$prev.length || !$color.length) {
            return;
        }
        function sync() {
            var v = ($color.val() || '').trim();
            var bg = /^#([0-9a-f]{3}|[0-9a-f]{6})$/i.test(v) ? v : '#e5e7eb';
            $prev.css('background-color', bg);
        }
        $color.off('input.placeCategoryColor').on('input.placeCategoryColor', sync);
        sync();
    });
}

abp.modals.placeCategoryEdit = function () {
    return {
        initModal: function (publicApi) {
            bindPlaceCategoryColorPreview(publicApi);
        },
    };
};
