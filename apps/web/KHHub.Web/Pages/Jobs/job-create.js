$(function () {
    var $title = $('#JobTitle');
    var $summary = $('#JobSummary');
    var $slug = $('#JobSlug');
    var $seoTitle = $('#JobSeoTitle');
    var $seoDescription = $('#JobSeoDescription');
    var hasCustomSlug = false;
    var hasCustomSeoTitle = false;
    var hasCustomSeoDescription = false;

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
        $('#JobSlugPreview').text(($slug.val() || '').trim());
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
});
