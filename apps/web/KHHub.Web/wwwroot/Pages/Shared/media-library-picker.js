(function () {
    var $modal = $('#khhub-media-library-modal');
    var $root = $('#khhub-mlp-explorer-root');
    if (!$modal.length || !$root.length) {
        return;
    }

    var l = abp.localization.getResource('MasterDataService');

    function applySelection(url) {
        var urlTarget = ($modal.data('url-target') || '').trim();
        if (!urlTarget) {
            return;
        }
        var $field = $(urlTarget).first();
        if (!$field.length) {
            return;
        }
        $field.val(url).trigger('change');
        abp.notify.success(l('MediaLibrary:ImageSelected'));
        var mi = bootstrap.Modal.getInstance($modal[0]);
        if (mi) {
            mi.hide();
        }
    }

    $(document).on('click', '.khhub-media-library-open', function (e) {
        e.preventDefault();
        var $btn = $(this);
        var urlTarget = ($btn.data('url-target') || '').trim();
        if (!urlTarget) {
            return;
        }
        var previewTarget = ($btn.data('preview-target') || '').trim();
        $modal.data('url-target', urlTarget);
        $modal.data('preview-target', previewTarget);
        window.__KHHubMediaExplorerOnPick = function (pickedUrl) {
            applySelection(pickedUrl);
        };
        bootstrap.Modal.getOrCreateInstance($modal[0]).show();
    });

    $modal.on('shown.bs.modal', function () {
        var wasMounted = !!$root.data('mf-mounted');
        if (window.kHHub && typeof window.kHHub.initMediaExplorerRoot === 'function') {
            window.kHHub.initMediaExplorerRoot($root[0]);
        }
        if (wasMounted) {
            var r = $root.data('mf-reload');
            if (typeof r === 'function') {
                r();
            }
        }
    });

    $modal.on('hidden.bs.modal', function () {
        window.__KHHubMediaExplorerOnPick = undefined;
    });
})();
