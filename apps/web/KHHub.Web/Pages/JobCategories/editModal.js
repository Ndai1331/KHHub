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
    });

    var current = ($input.val() || '').trim();
    if (current) {
        try {
            $picker.iconpicker('setIcon', current);
        } catch (e) {}
    }

    $picker.off('.khJobEditIconPicker');
    $input.off('.khJobEditIconPicker');

    $picker.on('iconpickerSelected.khJobEditIconPicker', function (event) {
        var icon = event.iconpickerValue || '';
        $input.val(icon).trigger('input');
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
