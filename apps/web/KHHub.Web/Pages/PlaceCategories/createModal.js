var abp = abp || {};
var placeCategoryCreateColorFallbackBound = false;
var DEBUG_PLACE_CATEGORY_COLOR = true;

function placeCategoryColorLog() {
    if (!DEBUG_PLACE_CATEGORY_COLOR || typeof console === 'undefined' || !console.log) {
        return;
    }
    var args = Array.prototype.slice.call(arguments);
    args.unshift('[PlaceCategoryColor]');
    console.log.apply(console, args);
}
placeCategoryColorLog('script loaded');

function placeCategoryNormalizeHexToRrggbb(raw) {
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

function initPlaceCategoryIconPicker($scope, inputSelector, pickerSelector) {
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

    $picker.off('.khPlaceIconPicker');
    $input.off('.khPlaceIconPicker');

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

    $picker.on('mousedown.khPlaceIconPicker', function (e) {
        e.preventDefault();
    });

    $picker.on('click.khPlaceIconPicker', function (e) {
        e.preventDefault();
        e.stopPropagation();

        try {
            $picker.iconpicker('show');
            setTimeout(positionIconPickerPopover, 0);
            setTimeout(positionIconPickerPopover, 50);
            setTimeout(positionIconPickerPopover, 150);
        } catch (err) {}
    });

    $picker.on('iconpickerShow.khPlaceIconPicker iconpickerShown.khPlaceIconPicker', function () {
        setTimeout(positionIconPickerPopover, 0);
    });

    $picker.on('iconpickerSelected.khPlaceIconPicker', function (event) {
        var icon = event.iconpickerValue || '';
        $input.val(icon).trigger('input');

        try {
            $picker.iconpicker('hide');
        } catch (err) {}
    });

    $input.on('input.khPlaceIconPicker change.khPlaceIconPicker', function () {
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
function placeCategoryColorModalRoot(publicApi, pickerSelector) {
    var $fromApi = $(publicApi.getModal && publicApi.getModal());
    if ($fromApi.length && $fromApi.find(pickerSelector).length) {
        return $fromApi;
    }
    var $byPicker = $(pickerSelector).closest('.modal');
    return $byPicker.length ? $byPicker : $fromApi;
}

function bindPlaceCategoryColorPreviewCreate(publicApi) {
    var ns = '.khPlaceCatColorCreate';
    var pickerSelector = '#PlaceCategoryCreateColorPicker';

    publicApi.onOpen(function () {
        placeCategoryColorLog('onOpen fired');
        setTimeout(function () {
            var $modal = placeCategoryColorModalRoot(publicApi, pickerSelector);
            if (!$modal.length) {
                placeCategoryColorLog('onOpen: modal not found');
                return;
            }
            placeCategoryColorLog('onOpen: modal found', $modal.attr('id') || '(no id)');

            $modal.off(ns);

            var lock = false;

            function syncPreview() {
                var v = ($modal.find('[name="PlaceCategory.Color"]').first().val() || '').trim();
                var bg = /^#([0-9a-f]{3}|[0-9a-f]{6})$/i.test(v) ? v : '#e5e7eb';
                $modal.find('#PlaceCategoryColorPreview').css('background-color', bg);
                placeCategoryColorLog('syncPreview', { text: v, preview: bg });
            }

            function syncTextToPicker() {
                var $color = $modal.find('[name="PlaceCategory.Color"]').first();
                var hex = placeCategoryNormalizeHexToRrggbb($color.val());
                var $picker = $modal.find(pickerSelector).first();
                if ($picker.length) {
                    $picker.val(hex || '#e5e7eb');
                }
                syncPreview();
            }

            $modal.on(
                'input' + ns + ' change' + ns,
                '[name="PlaceCategory.Color"]',
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
                var $color = $modal.find('[name="PlaceCategory.Color"]').first();
                if (pv && $color.length) {
                    $color.val(pv.toUpperCase());
                }
                syncPreview();
                lock = false;
            });

            syncTextToPicker();
            initPlaceCategoryIconPicker(
                $modal,
                '#PlaceCategoryCreateIconInput',
                '#PlaceCategoryCreateIconPicker'
            );
        }, 0);
    });
}

function bindPlaceCategoryColorPreviewCreateFallback() {
    if (placeCategoryCreateColorFallbackBound) {
        return;
    }
    placeCategoryCreateColorFallbackBound = true;

    var ns = '.khPlaceCatColorCreateFallback';
    var pickerSelector = '#PlaceCategoryCreateColorPicker';
    var lock = false;

    function getCreateModalScopeFrom(el) {
        var $modal = $(el).closest('.modal');
        if ($modal.length && $modal.find(pickerSelector).length) {
            return $modal;
        }
        return $();
    }

    function syncPreview($scope) {
        var v = ($scope.find('[name="PlaceCategory.Color"]').first().val() || '').trim();
        var bg = /^#([0-9a-f]{3}|[0-9a-f]{6})$/i.test(v) ? v : '#e5e7eb';
        $scope.find('#PlaceCategoryColorPreview').css('background-color', bg);
    }

    function syncTextToPicker($scope) {
        var $color = $scope.find('[name="PlaceCategory.Color"]').first();
        var hex = placeCategoryNormalizeHexToRrggbb($color.val());
        var $picker = $scope.find(pickerSelector).first();
        if ($picker.length) {
            $picker.val(hex || '#e5e7eb');
        }
                placeCategoryColorLog('syncTextToPicker', {
                    raw: $color.val(),
                    normalized: hex,
                    pickerValue: $picker.val(),
                });
        syncPreview($scope);
    }

    $(document).off(ns);
    placeCategoryColorLog('fallback delegation bound');

    $(document).on('input' + ns + ' change' + ns, '[name="PlaceCategory.Color"]', function (e) {
        if (lock) {
            return;
        }
        var $scope = getCreateModalScopeFrom(this);
        if (!$scope.length) {
            return;
        }
        placeCategoryColorLog('text event', { type: e.type, value: $(this).val() });
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
        placeCategoryColorLog('picker event', { type: e.type, value: $(this).val() });
        lock = true;
        var pv = $(this).val();
        var $color = $scope.find('[name="PlaceCategory.Color"]').first();
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
        placeCategoryColorLog('shown.bs.modal -> initial sync');
        syncTextToPicker($scope);
        initPlaceCategoryIconPicker(
            $scope,
            '#PlaceCategoryCreateIconInput',
            '#PlaceCategoryCreateIconPicker'
        );
    });
}

abp.modals.placeCategoryCreate = function () {
    return {
        initModal: function (publicApi) {
            placeCategoryColorLog('initModal called');
            bindPlaceCategoryColorPreviewCreate(publicApi);
            bindPlaceCategoryColorPreviewCreateFallback();
        },
    };
};

if (typeof jQuery !== 'undefined') {
    jQuery(function () {
        placeCategoryColorLog('document ready -> bind fallback');
        bindPlaceCategoryColorPreviewCreateFallback();
    });
}
