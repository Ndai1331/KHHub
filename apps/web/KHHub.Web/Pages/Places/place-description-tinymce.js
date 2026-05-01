/**
 * TinyMCE for Place.Description — same options as Articles (create.js / edit.js).
 */
$(function () {
    if (typeof tinymce === 'undefined') {
        return;
    }

    var $ta = $('#PlaceDescription');
    if (!$ta.length) {
        return;
    }

    var lr = abp.localization.getResource('MasterDataService');
    var $token = $('input[name="__RequestVerificationToken"]');

    tinymce.init({
        selector: '#PlaceDescription',
        height: 520,
        menubar: false,
        license_key: 'gpl',
        plugins: 'lists link image autoresize code',
        toolbar:
            'undo redo | blocks | bold italic underline | alignleft aligncenter alignright | bullist numlist | link image | removeformat | code',
        relative_urls: false,
        convert_urls: true,
        images_upload_handler: function (blobInfo) {
            return new Promise(function (resolve, reject) {
                var formData = new FormData();
                formData.append('file', blobInfo.blob(), blobInfo.filename());
                var token = $token.val();
                if (token) {
                    formData.append('__RequestVerificationToken', token);
                }

                var $form = $ta.closest('form');
                abp.ui.setBusy($form);

                abp.ajax({
                    url: abp.appPath + 'Articles/MediaUpload',
                    type: 'POST',
                    data: formData,
                    processData: false,
                    contentType: false,
                })
                    .then(function (res) {
                        var url = res.url;
                        if (url && url.indexOf('http') !== 0 && url.indexOf('//') !== 0) {
                            url = window.location.origin + url;
                        }
                        resolve(url);
                    })
                    .fail(function (xhr) {
                        var msg =
                            (xhr &&
                                xhr.responseJSON &&
                                xhr.responseJSON.error &&
                                xhr.responseJSON.error.message) ||
                            lr('AnErrorOccurred');
                        abp.notify.error(msg);
                        reject(xhr);
                    })
                    .always(function () {
                        abp.ui.clearBusy($form);
                    });
            });
        },
        setup: function (editor) {
            editor.on('change keyup undo redo', function () {
                editor.save();
            });
        },
    });
});
