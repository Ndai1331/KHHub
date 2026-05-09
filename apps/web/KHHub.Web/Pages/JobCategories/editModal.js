var abp = abp || {};

function jobCategoryNormalizeHexToRrggbb(raw) {
    var v = (raw || '').trim();
    if (!v) {
        return null;
    }
    if (v[0] !== '#') {
        v = '#' + v;
    }
    var m3 = /^#([0-9a-f]{3})$/i.exec(v);
    if (m3) {
        var s = m3[1];
        return ('#' + s[0] + s[0] + s[1] + s[1] + s[2] + s[2]).toLowerCase();
    }
    var m6 = /^#([0-9a-f]{6})$/i.exec(v);
    if (m6) {
        return ('#' + m6[1]).toLowerCase();
    }
    return null;
}

function initJobCategoryEditIconPicker($modal, inputSelector, pickerSelector) {
    if (typeof jQuery === 'undefined' || !jQuery.fn || !jQuery.fn.iconpicker) {
        return;
    }

    var $input = $modal.find(inputSelector).first();
    var $picker = $modal.find(pickerSelector).first();
    if (!$input.length || !$picker.length) {
        return;
    }

    try {
        $picker.iconpicker('destroy');
    } catch (e) {}

    $picker.iconpicker({
        iconset: 'fontawesome5',
        placement: 'bottom',
        search: true,
        container: 'body',
        animation: false,
    });

    var current = ($input.val() || '').trim();
    if (current) {
        try {
            $picker.iconpicker('setIcon', current);
        } catch (e) {}
    }

    $picker.off('.khJobEditIconPicker');
    $input.off('.khJobEditIconPicker');

    function positionIconPickerPopover() {
        var $pop = $('.iconpicker-popover').last();
        if (!$pop.length) {
            return;
        }

        var pickerEl = $picker.get(0);
        if (!pickerEl || !pickerEl.getBoundingClientRect) {
            return;
        }

        var rect = pickerEl.getBoundingClientRect();

        $pop
            .addClass('show in')
            .removeClass('fade')
            .css({
                position: 'fixed',
                display: 'block',
                opacity: 1,
                visibility: 'visible',
                zIndex: 20000,
                pointerEvents: 'auto',
                transform: 'none',
            });

        var popWidth = $pop.outerWidth() || 260;
        var left = Math.min(rect.left, window.innerWidth - popWidth - 8);

        $pop.css({
            top: rect.bottom + 8,
            left: Math.max(8, left),
        });
    }

    $picker.on('mousedown.khJobEditIconPicker', function (e) {
        e.preventDefault();
    });

    $picker.on('click.khJobEditIconPicker', function (e) {
        e.preventDefault();
        e.stopPropagation();

        try {
            $picker.iconpicker('show');
            setTimeout(positionIconPickerPopover, 0);
            setTimeout(positionIconPickerPopover, 50);
            setTimeout(positionIconPickerPopover, 150);
        } catch (err) {}
    });

    $picker.on('iconpickerShow.khJobEditIconPicker iconpickerShown.khJobEditIconPicker', function () {
        setTimeout(positionIconPickerPopover, 0);
    });

    $picker.on('iconpickerSelected.khJobEditIconPicker', function (event) {
        var icon = event.iconpickerValue || '';
        $input.val(icon).trigger('input');

        try {
            $picker.iconpicker('hide');
        } catch (err) {}
    });

    $input.on('input.khJobEditIconPicker change.khJobEditIconPicker', function () {
        var value = ($(this).val() || '').trim();
        if (!value) {
            return;
        }
        try {
            $picker.iconpicker('setIcon', value);
        } catch (e) {}
    });
}

function bindJobCategoryColorPreview(publicApi, pickerSelector) {
    var ns = '.khJobCatColorEdit';

    publicApi.onOpen(function () {
        setTimeout(function () {
            var $modal = $(publicApi.getModal && publicApi.getModal());
            if (!$modal.length) {
                return;
            }

            $modal.off(ns);

            var lock = false;

            function syncPreview() {
                var v = ($modal.find('[name="JobCategory.Color"]').first().val() || '').trim();
                var bg = /^#([0-9a-f]{3}|[0-9a-f]{6})$/i.test(v) ? v : '#e5e7eb';
                $modal.find('#JobCategoryColorPreview').css('background-color', bg);
            }

            function syncTextToPicker() {
                var $color = $modal.find('[name="JobCategory.Color"]').first();
                var hex = jobCategoryNormalizeHexToRrggbb($color.val());
                var $picker = $modal.find(pickerSelector).first();
                if ($picker.length) {
                    $picker.val(hex || '#e5e7eb');
                }
                syncPreview();
            }

            $modal.on(
                'input' + ns + ' change' + ns,
                '[name="JobCategory.Color"]',
                function () {
                    if (lock) {
                        return;
                    }
                    lock = true;
                    syncTextToPicker();
                    lock = false;
                }
            );

            $modal.on('input' + ns + ' change' + ns, pickerSelector, function () {
                if (lock) {
                    return;
                }
                lock = true;
                var pv = $(this).val();
                var $color = $modal.find('[name="JobCategory.Color"]').first();
                if (pv && $color.length) {
                    $color.val(pv.toUpperCase());
                }
                syncPreview();
                lock = false;
            });

            syncTextToPicker();
            initJobCategoryEditIconPicker($modal, '#JobCategoryEditIconInput', '#JobCategoryEditIconPicker');
        }, 0);
    });
}

abp.modals.jobCategoryEdit = function () {
    return {
        initModal: function (publicApi) {
            bindJobCategoryColorPreview(publicApi, '#JobCategoryEditColorPicker');
        },
    };
};
