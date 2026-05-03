$(function () {
    var $title = $('#PlaceName');
    var $short = $('#PlaceShortDescription');
    var $slug = $('#PlaceSlug');
    var $seoTitle = $('#PlaceSeoTitle');
    var $seoDescription = $('#PlaceSeoDescription');
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
            $seoDescription.val(toSeoDescription($short.val()));
        }
        renderSlugPreview();
    }

    function renderSlugPreview() {
        var $pv = $('#PlaceSlugPreview');
        if (!$pv.length) {
            return;
        }
        var s = ($slug.val() || '').trim();
        $pv.text(s);
    }

    function detectCustomState() {
        var generatedSlug = slugifyVietnamese($title.val());
        hasCustomSlug = !!$slug.val() && $slug.val() !== generatedSlug;
        hasCustomSeoTitle = !!$seoTitle.val();
        hasCustomSeoDescription = !!$seoDescription.val();
    }

    if (!$title.length) {
        return;
    }

    detectCustomState();
    syncSeoFields();

    $title.on('input', function () {
        syncSeoFields();
    });

    $short.on('input', function () {
        syncSeoFields();
    });

    $seoTitle.on('input', function () {
        hasCustomSeoTitle = !!$(this).val();
    });

    $seoDescription.on('input', function () {
        hasCustomSeoDescription = !!$(this).val();
    });
});
