$(function () {
    var $title = $('#JobTitle');
    var $summary = $('#JobSummary');
    var $slug = $('#JobSlug');
    var $seoTitle = $('#JobSeoTitle');
    var $seoDescription = $('#JobSeoDescription');
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
        $('#JobSlugPreview').text(($slug.val() || '').trim());
    }

    function detectCustomState() {
        var generatedSlug = slugifyVietnamese($title.val());
        hasCustomSlug = !!$slug.val() && $slug.val() !== generatedSlug;
        hasCustomSeoTitle = !!$seoTitle.val() && $seoTitle.val() !== ($title.val() || '');
        hasCustomSeoDescription =
            !!$seoDescription.val() && $seoDescription.val() !== toSeoDescription($summary.val());
    }

    function setPreview(previewSelector, url) {
        var $img = $(previewSelector);
        var preview = window.KHHubMediaPreview;
        if (!$img.length || !preview || typeof preview.applyPreviewToImg !== 'function') {
            return;
        }
        preview.applyPreviewToImg($img, url);
    }

    function initTinyMce() {
        if (typeof tinymce === 'undefined') {
            return;
        }

        tinymce.init({
            selector: '#JobDescription,#JobRequirements,#JobBenefits',
            height: 420,
            menubar: false,
            license_key: 'gpl',
            plugins: 'lists link image autoresize code',
            toolbar:
                'undo redo | blocks | bold italic underline | alignleft aligncenter alignright | bullist numlist | link image | removeformat | code',
            relative_urls: false,
            convert_urls: true,
            setup: function (editor) {
                editor.on('change keyup undo redo', function () {
                    editor.save();
                });
            },
        });
    }

    if (!$title.length) {
        return;
    }

    detectCustomState();
    syncSeoFields();
    initTinyMce();

    $title.on('input', syncSeoFields);
    $summary.on('input', syncSeoFields);
    $seoTitle.on('input', function () {
        hasCustomSeoTitle = !!$(this).val();
    });
    $seoDescription.on('input', function () {
        hasCustomSeoDescription = !!$(this).val();
    });

    $(document).on('change input', '#JobThumbnailUrl, #JobCoverImageUrl', function () {
        var id = $(this).attr('id');
        var url = $(this).val();
        if (id === 'JobThumbnailUrl') {
            setPreview('#JobThumbnailPreview', url);
        } else {
            setPreview('#JobCoverPreview', url);
        }
    });

    var jobService = window.kHHub.masterDataService.services.jobs.jobs;
    $('#DeleteJobButton').on('click', function (e) {
        e.preventDefault();
        var id = $('input[name="Id"]').val();
        if (!id) {
            return;
        }

        abp.message.confirm(l('DeleteConfirmationMessage')).then(function (confirmed) {
            if (!confirmed) {
                return;
            }

            var $form = $('#JobEditForm');
            abp.ui.setBusy($form);

            jobService
                .delete(id)
                .then(function () {
                    abp.notify.success(l('SuccessfullyDeleted'));
                    window.location.href = abp.appPath + 'Jobs';
                })
                .fail(function (xhr) {
                    var msg =
                        xhr &&
                        xhr.responseJSON &&
                        xhr.responseJSON.error &&
                        xhr.responseJSON.error.message
                            ? xhr.responseJSON.error.message
                            : l('AnErrorOccurred');
                    abp.notify.error(msg);
                })
                .always(function () {
                    abp.ui.clearBusy($form);
                });
        });
    });
});
