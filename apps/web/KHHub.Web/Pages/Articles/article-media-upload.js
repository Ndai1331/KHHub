$(function () {
    var l = abp.localization.getResource('MasterDataService');

    function getAntiForgeryToken() {
        return $('input[name="__RequestVerificationToken"]').val();
    }

    function setPreview(previewSelector, url) {
        if (!previewSelector || !url) {
            return;
        }
        var $img = $(previewSelector);
        if (!$img.length) {
            return;
        }
        $img.attr('src', url).removeClass('d-none');
    }

    $('.article-media-upload').on('change', function () {
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
        abp.ui.setBusy($form);

        abp.ajax({
            url: abp.appPath + 'Articles/MediaUpload',
            type: 'POST',
            data: formData,
            processData: false,
            contentType: false,
        })
            .then(function (result) {
                $(targetSelector).val(result.url).trigger('change');
                setPreview($fileInput.data('preview-target'), result.url);
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

    $('#ArticleThumbnailUrl, #ArticleCoverImageUrl').on('change input', function () {
        var id = $(this).attr('id');
        var url = $(this).val();
        if (id === 'ArticleThumbnailUrl') {
            setPreview('#ArticleThumbnailPreview', url);
        } else if (id === 'ArticleCoverImageUrl') {
            setPreview('#ArticleCoverPreview', url);
        }
    });
});
