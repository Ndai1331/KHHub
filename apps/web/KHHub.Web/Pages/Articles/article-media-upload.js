$(function () {
    var l = abp.localization.getResource('MasterDataService');

    function getAntiForgeryToken() {
        return $('input[name="__RequestVerificationToken"]').val();
    }

    /** When $context is set, resolves preview img inside modal (avoid wrong #id in multi-modal DOM). */
    function setPreview(previewSelector, url, $context) {
        if (!previewSelector) {
            console.log('[article-media-upload] setPreview: no previewSelector');
            return;
        }
        var $img =
            $context && $context.length ? $context.find(previewSelector) : $(previewSelector);
        if (!$img.length) {
            console.warn('[article-media-upload] setPreview: img not found', {
                previewSelector: previewSelector,
                contextIsModal: !!($context && $context.length),
            });
            return;
        }
        if (!(url || '').trim()) {
            $img.attr('src', '').addClass('d-none');
            return;
        }
        var trimmed = url.trim();
        $img.attr('src', trimmed).removeClass('d-none');
        console.log('[article-media-upload] setPreview applied src length=', trimmed.length);
    }

    function syncArticleCategoryPreviewInModal($modal) {
        if (!$modal || !$modal.length) {
            console.log('[article-media-upload] syncArticleCategoryPreviewInModal: no modal');
            return;
        }
        var $inp = $modal.find('.article-category-thumbnail-url').first();
        var url = ($inp.val() || '').trim();
        var $img = $modal.find('#ArticleCategoryThumbnailPreview');
        console.log('[article-media-upload] sync thumbnail', {
            modalFound: !!$modal.length,
            inputFound: $inp.length,
            urlLength: url.length,
            urlPreview: url.slice(0, 80),
            imgFound: $img.length,
        });
        setPreview('#ArticleCategoryThumbnailPreview', url, $modal);
    }

    window.KHHub = window.KHHub || {};
    window.KHHub.syncArticleCategoryPreviewInModal = syncArticleCategoryPreviewInModal;

    /**
     * ABP ModalManager `modalClass` maps to `abp.modals.*` JS hooks — it does not guarantee a DOM class on `.modal`.
     * Delegate on `.modal` and gate by markup used only in Article Category modals.
     */
    $(document).on('shown.bs.modal', '.modal', function () {
        var $modal = $(this);
        if (!$modal.find('.article-category-thumbnail-url').length) {
            return;
        }
        console.log('[article-media-upload] shown.bs.modal (article category)', {
            modalClasses: this.className,
        });
        syncArticleCategoryPreviewInModal($modal);
    });

    // Delegated: works when inputs are injected (e.g. modal partials via ModalManager).
    $(document).on('change', '.article-media-upload', function () {
        var $fileInput = $(this);
        var file = $fileInput[0].files && $fileInput[0].files[0];
        if (!file) {
            return;
        }

        var targetSelector = $fileInput.data('url-target');
        if (!targetSelector) {
            return;
        }

        var formData = new FormData();
        formData.append('file', file);

        var token = getAntiForgeryToken();
        if (token) {
            formData.append('__RequestVerificationToken', token);
        }

        var $form = $fileInput.closest('form');
        var $modal = $fileInput.closest('.modal');
        abp.ui.setBusy($form);

        abp.ajax({
            url: abp.appPath + 'Articles/MediaUpload',
            type: 'POST',
            data: formData,
            processData: false,
            contentType: false,
        })
            .then(function (result) {
                var $urlField = ($modal.length ? $modal : $form).find(targetSelector).first();
                $urlField.val(result.url).trigger('change');
                setPreview($fileInput.data('preview-target'), result.url, $modal.length ? $modal : undefined);
                abp.notify.success(l('ImageUploaded'));
            })
            .fail(function (xhr) {
                var msg =
                    (xhr &&
                        xhr.responseJSON &&
                        xhr.responseJSON.error &&
                        xhr.responseJSON.error.message) ||
                    l('AnErrorOccurred');
                abp.notify.error(msg);
            })
            .always(function () {
                abp.ui.clearBusy($form);
            });

        $fileInput.val('');
    });

    $(document).on('change input', '#ArticleThumbnailUrl, #ArticleCoverImageUrl', function () {
        var id = $(this).attr('id');
        var url = $(this).val();
        if (id === 'ArticleThumbnailUrl') {
            setPreview('#ArticleThumbnailPreview', url);
        } else if (id === 'ArticleCoverImageUrl') {
            setPreview('#ArticleCoverPreview', url);
        }
    });

    $(document).on('change input', '#PlaceThumbnailUrl, #PlaceCoverImageUrl', function () {
        var id = $(this).attr('id');
        var url = $(this).val();
        if (id === 'PlaceThumbnailUrl') {
            setPreview('#PlaceThumbnailPreview', url);
        } else if (id === 'PlaceCoverImageUrl') {
            setPreview('#PlaceCoverPreview', url);
        }
    });

    $(document).on('change input', 'input.article-category-thumbnail-url', function () {
        var $m = $(this).closest('.modal');
        setPreview('#ArticleCategoryThumbnailPreview', $(this).val(), $m.length ? $m : undefined);
    });
});
