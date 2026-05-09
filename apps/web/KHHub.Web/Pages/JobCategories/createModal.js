var abp = abp || {};
var jobCategoryCreateColorFallbackBound = false;
var DEBUG_PLACE_CATEGORY_COLOR = true;

function jobCategoryColorLog() {
    if (!DEBUG_PLACE_CATEGORY_COLOR || typeof console === 'undefined' || !console.log) {
        return;
    }
    var args = Array.prototype.slice.call(arguments);
    args.unshift('[JobCategoryColor]');
    console.log.apply(console, args);
}
jobCategoryColorLog('script loaded');

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

function initJobCategoryIconPicker($scope, inputSelector, pickerSelector) {
    if (typeof jQuery === 'undefined' || !jQuery.fn || !jQuery.fn.iconpicker) {
        return;
    }

    var $input = $scope.find(inputSelector).first();
    var $picker = $scope.find(pickerSelector).first();
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

    $picker.off('.khJobIconPicker');
    $input.off('.khJobIconPicker');

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

    $picker.on('mousedown.khJobIconPicker', function (e) {
        e.preventDefault();
    });

    $picker.on('click.khJobIconPicker', function (e) {
        e.preventDefault();
        e.stopPropagation();

        try {
            $picker.iconpicker('show');
            setTimeout(positionIconPickerPopover, 0);
            setTimeout(positionIconPickerPopover, 50);
            setTimeout(positionIconPickerPopover, 150);
        } catch (err) {}
    });

    $picker.on('iconpickerShow.khJobIconPicker iconpickerShown.khJobIconPicker', function () {
        setTimeout(positionIconPickerPopover, 0);
    });

    $picker.on('iconpickerSelected.khJobIconPicker', function (event) {
        var icon = event.iconpickerValue || '';
        $input.val(icon).trigger('input');

        try {
            $picker.iconpicker('hide');
        } catch (err) {}
    });

    $input.on('input.khJobIconPicker change.khJobIconPicker', function () {
        var value = ($(this).val() || '').trim();
        if (!value) {
            return;
        }
        try {
            $picker.iconpicker('setIcon', value);
        } catch (e) {}
    });
}

/**
 * After Bootstrap moves .modal to body, the outer <form> may no longer wrap inputs.
 * Prefer getModal(); fall back to the live modal that contains the color picker (same as Edit).
 */
function jobCategoryColorModalRoot(publicApi, pickerSelector) {
    var $fromApi = $(publicApi.getModal && publicApi.getModal());
    if ($fromApi.length && $fromApi.find(pickerSelector).length) {
        return $fromApi;
    }
    var $byPicker = $(pickerSelector).closest('.modal');
    return $byPicker.length ? $byPicker : $fromApi;
}

function bindJobCategoryColorPreviewCreate(publicApi) {
    var ns = '.khJobCatColorCreate';
    var pickerSelector = '#JobCategoryCreateColorPicker';

    publicApi.onOpen(function () {
        jobCategoryColorLog('onOpen fired');
        setTimeout(function () {
            var $modal = jobCategoryColorModalRoot(publicApi, pickerSelector);
            if (!$modal.length) {
                jobCategoryColorLog('onOpen: modal not found');
                return;
            }
            jobCategoryColorLog('onOpen: modal found', $modal.attr('id') || '(no id)');

            $modal.off(ns);

            var lock = false;

            function syncPreview() {
                var v = ($modal.find('[name="JobCategory.Color"]').first().val() || '').trim();
                var bg = /^#([0-9a-f]{3}|[0-9a-f]{6})$/i.test(v) ? v : '#e5e7eb';
                $modal.find('#JobCategoryColorPreview').css('background-color', bg);
                jobCategoryColorLog('syncPreview', { text: v, preview: bg });
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
            initJobCategoryIconPicker($modal, '#JobCategoryCreateIconInput', '#JobCategoryCreateIconPicker');
        }, 0);
    });
}

function bindJobCategoryColorPreviewCreateFallback() {
    if (jobCategoryCreateColorFallbackBound) {
        return;
    }
    jobCategoryCreateColorFallbackBound = true;

    var ns = '.khJobCatColorCreateFallback';
    var pickerSelector = '#JobCategoryCreateColorPicker';
    var lock = false;

    function getCreateModalScopeFrom(el) {
        var $modal = $(el).closest('.modal');
        if ($modal.length && $modal.find(pickerSelector).length) {
            return $modal;
        }
        return $();
    }

    function syncPreview($scope) {
        var v = ($scope.find('[name="JobCategory.Color"]').first().val() || '').trim();
        var bg = /^#([0-9a-f]{3}|[0-9a-f]{6})$/i.test(v) ? v : '#e5e7eb';
        $scope.find('#JobCategoryColorPreview').css('background-color', bg);
    }

    function syncTextToPicker($scope) {
        var $color = $scope.find('[name="JobCategory.Color"]').first();
        var hex = jobCategoryNormalizeHexToRrggbb($color.val());
        var $picker = $scope.find(pickerSelector).first();
        if ($picker.length) {
            $picker.val(hex || '#e5e7eb');
        }
                jobCategoryColorLog('syncTextToPicker', {
                    raw: $color.val(),
                    normalized: hex,
                    pickerValue: $picker.val(),
                });
        syncPreview($scope);
    }

    $(document).off(ns);
    jobCategoryColorLog('fallback delegation bound');

    $(document).on('input' + ns + ' change' + ns, '[name="JobCategory.Color"]', function (e) {
        if (lock) {
            return;
        }
        var $scope = getCreateModalScopeFrom(this);
        if (!$scope.length) {
            return;
        }
        jobCategoryColorLog('text event', { type: e.type, value: $(this).val() });
        lock = true;
        syncTextToPicker($scope);
        lock = false;
    });

    $(document).on('input' + ns + ' change' + ns, pickerSelector, function (e) {
        if (lock) {
            return;
        }
        var $scope = getCreateModalScopeFrom(this);
        if (!$scope.length) {
            return;
        }
        jobCategoryColorLog('picker event', { type: e.type, value: $(this).val() });
        lock = true;
        var pv = $(this).val();
        var $color = $scope.find('[name="JobCategory.Color"]').first();
        if (pv && $color.length) {
            $color.val(pv.toUpperCase());
        }
        syncPreview($scope);
        lock = false;
    });

    $(document).on('shown.bs.modal' + ns, '.modal', function () {
        var $scope = getCreateModalScopeFrom(this);
        if (!$scope.length) {
            return;
        }
        jobCategoryColorLog('shown.bs.modal -> initial sync');
        syncTextToPicker($scope);
        initJobCategoryIconPicker($scope, '#JobCategoryCreateIconInput', '#JobCategoryCreateIconPicker');
    });
}

abp.modals.jobCategoryCreate = function () {
    return {
        initModal: function (publicApi) {
            jobCategoryColorLog('initModal called');
            bindJobCategoryColorPreviewCreate(publicApi);
            bindJobCategoryColorPreviewCreateFallback();
        },
    };
};

if (typeof jQuery !== 'undefined') {
    jQuery(function () {
        jobCategoryColorLog('document ready -> bind fallback');
        bindJobCategoryColorPreviewCreateFallback();
    });
}
