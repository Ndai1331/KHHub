(function () {
    var l = function (key) {
        return abp.localization.getResource('MasterDataService')(key);
    };

    function getErrorMessage(xhrOrError) {
        if (!xhrOrError) {
            return l('AnErrorOccurred');
        }
        if (typeof xhrOrError === 'string') {
            return xhrOrError;
        }
        if (xhrOrError.message && typeof xhrOrError.message === 'string') {
            return xhrOrError.message;
        }
        var json = xhrOrError.responseJSON || xhrOrError;
        if (json && json.error && json.error.message) {
            return json.error.message;
        }
        if (json && json.message) {
            return json.message;
        }
        return l('AnErrorOccurred');
    }

    function isEmptyGuid(s) {
        return !s || s === '00000000-0000-0000-0000-000000000000';
    }

    function val($f, prop) {
        var $e = $f.find('[name="Place.' + prop + '"]');
        return $e.length ? ($e.val() || '').toString() : '';
    }

    function iv($f, prop, defaultValue) {
        var $e = $f.find('[name="Place.' + prop + '"]');
        if (!$e.length) {
            return defaultValue !== undefined ? defaultValue : 0;
        }
        var n = parseInt($e.val(), 10);
        return isNaN(n) ? (defaultValue !== undefined ? defaultValue : 0) : n;
    }

    function fv($f, prop, defaultValue) {
        var $e = $f.find('[name="Place.' + prop + '"]');
        if (!$e.length) {
            return defaultValue !== undefined ? defaultValue : 0;
        }
        var n = parseFloat(($e.val() || '').toString().replace(',', '.'));
        return isNaN(n) ? (defaultValue !== undefined ? defaultValue : 0) : n;
    }

    function bv($f, prop) {
        var $e = $f.find('input[name="Place.' + prop + '"][type="checkbox"]');
        return $e.length ? $e.is(':checked') : false;
    }

    function syncTinyMceToTextareas() {
        if (typeof tinymce !== 'undefined') {
            tinymce.triggerSave();
        }
    }

    function syncSeoKeywordsField() {
        if (typeof window.kHHubSeoKeywordsSync === 'function') {
            window.kHHubSeoKeywordsSync();
        } else if (typeof window.kHHubArticleSeoKeywordsSync === 'function') {
            window.kHHubArticleSeoKeywordsSync();
        }
    }

    function readCreateDto($f) {
        return {
            name: val($f, 'Name'),
            slug: val($f, 'Slug'),
            shortDescription: val($f, 'ShortDescription') || null,
            description: val($f, 'Description') || null,
            thumbnailUrl: val($f, 'ThumbnailUrl') || null,
            coverImageUrl: val($f, 'CoverImageUrl') || null,
            address: val($f, 'Address') || null,
            latitude: fv($f, 'Latitude', 0),
            longituded: fv($f, 'Longituded', 0),
            phoneNumber: val($f, 'PhoneNumber') || null,
            email: val($f, 'Email') || null,
            website: val($f, 'Website') || null,
            openingHours: val($f, 'OpeningHours') || null,
            priceRange: iv($f, 'PriceRange', 0),
            googleMapUrl: val($f, 'GoogleMapUrl') || null,
            status: iv($f, 'Status', 0),
            viewCount: iv($f, 'ViewCount', 0),
            favoriteCount: iv($f, 'FavoriteCount', 0),
            reviewCount: iv($f, 'ReviewCount', 0),
            ratingAveraged: fv($f, 'RatingAveraged', 0),
            ratingTotal: iv($f, 'RatingTotal', 0),
            isFeatured: bv($f, 'IsFeatured'),
            isHot: bv($f, 'IsHot'),
            isVerified: bv($f, 'IsVerified'),
            seoTitle: val($f, 'SeoTitle'),
            seoDescription: val($f, 'SeoDescription') || null,
            seoKeywords: val($f, 'SeoKeywords') || null,
            placeCategoryId: val($f, 'PlaceCategoryId'),
            provinceId: val($f, 'ProvinceId'),
            wardId: val($f, 'WardId'),
        };
    }

    function readUpdateDto($f) {
        var dto = readCreateDto($f);
        dto.concurrencyStamp = val($f, 'ConcurrencyStamp');
        return dto;
    }

    function runWithBusy($form, promiseFactory) {
        abp.ui.setBusy($form);
        var p = promiseFactory();
        if (p && typeof p.always === 'function') {
            p.always(function () {
                abp.ui.clearBusy($form);
            });
        } else if (p && typeof p.finally === 'function') {
            p.finally(function () {
                abp.ui.clearBusy($form);
            });
        } else {
            abp.ui.clearBusy($form);
        }
        return p;
    }

    /** PlaceStatus.Published = 1 */
    function applySaveAction(dto, action) {
        if (action === 'save-and-publish') {
            dto.status = 1;
        }
    }

    function validateLocation(dto) {
        if (isEmptyGuid(dto.placeCategoryId)) {
            abp.notify.error(l('PlaceCategoryRequired'));
            return false;
        }
        if (isEmptyGuid(dto.provinceId)) {
            abp.notify.error(l('ProvinceRequired'));
            return false;
        }
        if (isEmptyGuid(dto.wardId)) {
            abp.notify.error(l('WardRequired'));
            return false;
        }
        return true;
    }

    $(function () {
        var placeService = window.kHHub.masterDataService.services.places.places;

        var $createForm = $('#PlaceForm[data-place-mode="create"]');
        if ($createForm.length) {
            $createForm.on('submit', function (e) {
                e.preventDefault();
            });

            $(document).on(
                'click',
                'button[type="submit"][form="PlaceForm"][name="action"]',
                function (e) {
                    e.preventDefault();
                    var action = $(this).val() || 'save';
                    syncTinyMceToTextareas();
                    syncSeoKeywordsField();
                    var dto = readCreateDto($createForm);
                    applySaveAction(dto, action);

                    if (!validateLocation(dto)) {
                        return;
                    }

                    runWithBusy($createForm, function () {
                        return placeService.create(dto);
                    })
                        .done(function (created) {
                            function finishSave() {
                                abp.notify.success(l('PlaceSavedSuccessfully'));
                                window.location.href = abp.appPath + 'Places';
                            }
                            var placeId = created && created.id;
                            if (
                                !placeId ||
                                !window.kHHubTagPicker ||
                                typeof window.kHHubTagPicker.syncPlaceTags !== 'function'
                            ) {
                                finishSave();
                                return;
                            }
                            window.kHHubTagPicker
                                .syncPlaceTags(placeId, $createForm)
                                .done(finishSave)
                                .fail(function () {
                                    abp.notify.warn(l('TagPickerSyncFailed'));
                                    finishSave();
                                });
                        })
                        .fail(function (xhr) {
                            abp.notify.error(getErrorMessage(xhr));
                        });
                }
            );
        }

        var $editForm = $('#PlaceEditForm[data-place-mode="edit"]');
        if ($editForm.length) {
            $editForm.on('submit', function (e) {
                e.preventDefault();
            });

            $(document).on(
                'click',
                'button[type="submit"][form="PlaceEditForm"][name="action"]',
                function (e) {
                    e.preventDefault();
                    var action = $(this).val() || 'save';
                    syncTinyMceToTextareas();
                    syncSeoKeywordsField();
                    var id = $editForm.find('input[name="Id"]').val();
                    if (!id || isEmptyGuid(id)) {
                        abp.notify.error(l('AnErrorOccurred'));
                        return;
                    }

                    var dto = readUpdateDto($editForm);
                    applySaveAction(dto, action);

                    if (!validateLocation(dto)) {
                        return;
                    }

                    runWithBusy($editForm, function () {
                        return placeService.update(id, dto);
                    })
                        .done(function () {
                            function finishSave() {
                                abp.notify.success(l('PlaceSavedSuccessfully'));
                                window.location.href = abp.appPath + 'Places';
                            }
                            if (
                                !window.kHHubTagPicker ||
                                typeof window.kHHubTagPicker.syncPlaceTags !== 'function'
                            ) {
                                finishSave();
                                return;
                            }
                            window.kHHubTagPicker
                                .syncPlaceTags(id, $editForm)
                                .done(finishSave)
                                .fail(function () {
                                    abp.notify.warn(l('TagPickerSyncFailed'));
                                    finishSave();
                                });
                        })
                        .fail(function (xhr) {
                            abp.notify.error(getErrorMessage(xhr));
                        });
                }
            );
        }
    });
})();
