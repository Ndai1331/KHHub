$(function () {
    var $title = $('#PlaceName');
    var $short = $('#PlaceShortDescription');
    var $slug = $('#PlaceSlug');
    var $seoTitle = $('#PlaceSeoTitle');
    var $seoDescription = $('#PlaceSeoDescription');
    var hasCustomSlug = true;
    var hasCustomSeoTitle = true;
    var hasCustomSeoDescription = true;

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
    }

    function detectCustomState() {
        var generatedSlug = slugifyVietnamese($title.val());
        hasCustomSlug = !!$slug.val() && $slug.val() !== generatedSlug;
        hasCustomSeoTitle = !!$seoTitle.val() && $seoTitle.val() !== ($title.val() || '');
        hasCustomSeoDescription =
            !!$seoDescription.val() && $seoDescription.val() !== toSeoDescription($short.val());
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

    $slug.on('input', function () {
        hasCustomSlug = !!$(this).val();
    });

    $seoTitle.on('input', function () {
        hasCustomSeoTitle = !!$(this).val();
    });

    $seoDescription.on('input', function () {
        hasCustomSeoDescription = !!$(this).val();
    });

    var placeService = window.kHHub.masterDataService.services.places.places;
    var l = abp.localization.getResource('MasterDataService');

    var $deleteBtn = $('#DeletePlaceButton');
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

                var $form = $('#PlaceEditForm');
                abp.ui.setBusy($form);

                placeService
                    .delete(id)
                    .then(function () {
                        abp.notify.success(l('SuccessfullyDeleted'));
                        window.location.href = abp.appPath + 'Places';
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
