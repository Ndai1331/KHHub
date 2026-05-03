$(function () {
    var $title = $('#ArticleTitle');
    var $summary = $('#ArticleSummary');
    var $slug = $('#ArticleSlug');
    var $seoTitle = $('#ArticleSeoTitle');
    var $seoDescription = $('#ArticleSeoDescription');
    var hasCustomSlug = true;
    var hasCustomSeoTitle = true;
    var hasCustomSeoDescription = true;
    var l = abp.localization.getResource('MasterDataService');

    function normalizeVietnamese(text) {
        return (text || '')
            .replace(/[àáạảãâầấậẩẫăằắặẳẵ]/g, 'a')
            .replace(/[èéẹẻẽêềếệểễ]/g, 'e')
            .replace(/[ìíịỉĩ]/g, 'i')
            .replace(/[òóọỏõôồốộổỗơờớợởỡ]/g, 'o')
            .replace(/[ùúụủũưừứựửữ]/g, 'u')
            .replace(/[ỳýỵỷỹ]/g, 'y')
            .replace(/đ/g, 'd')
            .replace(/[ÀÁẠẢÃÂẦẤẬẨẪĂẰẮẶẲẴ]/g, 'A')
            .replace(/[ÈÉẸẺẼÊỀẾỆỂỄ]/g, 'E')
            .replace(/[ÌÍỊỈĨ]/g, 'I')
            .replace(/[ÒÓỌỎÕÔỒỐỘỔỖƠỜỚỢỞỠ]/g, 'O')
            .replace(/[ÙÚỤỦŨƯỪỨỰỬỮ]/g, 'U')
            .replace(/[ỲÝỴỶỸ]/g, 'Y')
            .replace(/Đ/g, 'D');
    }

    function slugifyVietnamese(text) {
        return (text || '')
            .toString()
            .trim()
            .normalize('NFD')
            .replace(/[\u0300-\u036f]/g, '')
            .replace(/đ/g, 'd')
            .replace(/Đ/g, 'D')
            .replace(/[^0-9a-zA-Z\s-]/g, '')
            .toLowerCase()
            .replace(/\s+/g, '-')
            .replace(/-+/g, '-')
            .replace(/^-+|-+$/g, '');
    }

    function toSeoDescription(text) {
        return normalizeVietnamese((text || '').toString().trim());
    }

    function syncSeoFields() {
        if (!hasCustomSlug) {
            $slug.val(slugifyVietnamese($title.val()));
        }
        if (!hasCustomSeoTitle) {
            $seoTitle.val($title.val());
        }
        if (!hasCustomSeoDescription) {
            $seoDescription.val(toSeoDescription($summary.val()));
        }
        renderSlugPreview();
    }

    function renderSlugPreview() {
        var $pv = $('#ArticleSlugPreview');
        if (!$pv.length) {
            return;
        }
        var s = ($slug.val() || '').trim();
        $pv.text(s);
    }

    function detectCustomState() {
        var generatedSlug = slugifyVietnamese($title.val());
        hasCustomSlug = !!$slug.val() && $slug.val() !== generatedSlug;
        hasCustomSeoTitle = !!$seoTitle.val() && $seoTitle.val() !== ($title.val() || '');
        hasCustomSeoDescription =
            !!$seoDescription.val() && $seoDescription.val() !== toSeoDescription($summary.val());
    }

    function initTinyMce() {
        if (typeof tinymce === 'undefined') {
            return;
        }

        var $token = $('input[name="__RequestVerificationToken"]');

        tinymce.init({
            selector: '#ArticleContent',
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

                    var $form = $('#ArticleContent').closest('form');
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
                                l('AnErrorOccurred');
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
    }

    detectCustomState();
    syncSeoFields();
    initTinyMce();

    $title.on('input', function () {
        syncSeoFields();
    });

    $summary.on('input', function () {
        syncSeoFields();
    });

    $seoTitle.on('input', function () {
        hasCustomSeoTitle = !!$(this).val();
    });

    $seoDescription.on('input', function () {
        hasCustomSeoDescription = !!$(this).val();
    });

    $('#ArticleEditForm').on('submit', function () {
        if (typeof tinymce !== 'undefined') {
            tinymce.triggerSave();
        }
    });

    var articleService = window.kHHub.masterDataService.services.articles.articles;

    var $deleteBtn = $('#DeleteArticleButton');
    if ($deleteBtn.length) {
        $deleteBtn.on('click', function (e) {
            e.preventDefault();
            var id = $('input[name="Id"]').val();
            if (!id) {
                return;
            }

            abp.message.confirm(l('DeleteConfirmationMessage')).then(function (confirmed) {
                if (!confirmed) {
                    return;
                }

                var $form = $('#ArticleEditForm');
                abp.ui.setBusy($form);

                articleService
                    .delete(id)
                    .then(function () {
                        abp.notify.success(l('SuccessfullyDeleted'));
                        window.location.href = abp.appPath + 'Articles';
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
            });
        });
    }
});
