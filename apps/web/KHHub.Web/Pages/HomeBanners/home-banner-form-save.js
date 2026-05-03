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
        var $e = $f.find('[name="HomeBanner.' + prop + '"]');
        return $e.length ? ($e.val() || '').toString() : '';
    }

    function iv($f, prop, defaultValue) {
        var $e = $f.find('[name="HomeBanner.' + prop + '"]');
        if (!$e.length) {
            return defaultValue !== undefined ? defaultValue : 0;
        }
        var n = parseInt($e.val(), 10);
        return isNaN(n) ? (defaultValue !== undefined ? defaultValue : 0) : n;
    }

    function bv($f, prop) {
        var $e = $f.find('input[name="HomeBanner.' + prop + '"][type="checkbox"]');
        return $e.length ? $e.is(':checked') : false;
    }

    function optionalGuid($f, prop) {
        var s = val($f, prop);
        if (!s || isEmptyGuid(s)) {
            return null;
        }
        return s;
    }

    function optionalTargetType($f) {
        var s = val($f, 'TargetType');
        if (s === '' || s === undefined) {
            return null;
        }
        var n = parseInt(s, 10);
        return isNaN(n) ? null : n;
    }

    function readCreateDto($f) {
        return {
            title: val($f, 'Title'),
            subtitle: val($f, 'Subtitle') || null,
            description: val($f, 'Description') || null,
            imageUrl: val($f, 'ImageUrl'),
            mobileImageUrl: val($f, 'MobileImageUrl') || null,
            buttonText: val($f, 'ButtonText') || null,
            buttonUrl: val($f, 'ButtonUrl') || null,
            targetType: optionalTargetType($f),
            targetId: optionalGuid($f, 'TargetId'),
            sortOrder: iv($f, 'SortOrder', 1),
            isActive: bv($f, 'IsActive'),
            startDate: val($f, 'StartDate') || null,
            endDate: val($f, 'EndDate') || null,
        };
    }

    function readUpdateDto($f) {
        var dto = readCreateDto($f);
        dto.concurrencyStamp = val($f, 'ConcurrencyStamp');
        return dto;
    }

    $(function () {
        var homeBannerService = window.kHHub.masterDataService.services.homeBanners.homeBanners;

        var $createForm = $('#HomeBannerCreateForm[data-home-banner-mode="create"]');
        if ($createForm.length) {
            $createForm.on('submit', function (e) {
                e.preventDefault();
            });

            $(document).on('click', 'button[type="submit"][form="HomeBannerCreateForm"]', function (e) {
                e.preventDefault();
                var dto = readCreateDto($createForm);
                if (!dto.title || !(dto.imageUrl || '').trim()) {
                    abp.notify.error(l('HomeBannerTitleAndImageRequired'));
                    return;
                }

                abp.ui.setBusy($createForm);
                homeBannerService
                    .create(dto)
                    .then(function () {
                        abp.notify.success(l('HomeBannerSavedSuccessfully'));
                        window.location.href = abp.appPath + 'HomeBanners';
                    })
                    .catch(function (xhr) {
                        abp.notify.error(getErrorMessage(xhr));
                    })
                    .finally(function () {
                        abp.ui.clearBusy($createForm);
                    });
            });
        }

        var $editForm = $('#HomeBannerEditForm[data-home-banner-mode="edit"]');
        if ($editForm.length) {
            $editForm.on('submit', function (e) {
                e.preventDefault();
            });

            $(document).on('click', 'button[type="submit"][form="HomeBannerEditForm"]', function (e) {
                e.preventDefault();
                var id = $editForm.find('input[name="Id"]').val();
                if (!id || isEmptyGuid(id)) {
                    abp.notify.error(l('AnErrorOccurred'));
                    return;
                }

                var dto = readUpdateDto($editForm);
                if (!dto.title || !(dto.imageUrl || '').trim()) {
                    abp.notify.error(l('HomeBannerTitleAndImageRequired'));
                    return;
                }

                abp.ui.setBusy($editForm);
                homeBannerService
                    .update(id, dto)
                    .then(function () {
                        abp.notify.success(l('HomeBannerSavedSuccessfully'));
                        window.location.href = abp.appPath + 'HomeBanners';
                    })
                    .catch(function (xhr) {
                        abp.notify.error(getErrorMessage(xhr));
                    })
                    .finally(function () {
                        abp.ui.clearBusy($editForm);
                    });
            });
        }

        var $deleteBtn = $('#DeleteHomeBannerButton');
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

                    var $form = $('#HomeBannerEditForm');
                    abp.ui.setBusy($form);

                    homeBannerService
                        .delete(id)
                        .then(function () {
                            abp.notify.success(l('SuccessfullyDeleted'));
                            window.location.href = abp.appPath + 'HomeBanners';
                        })
                        .catch(function (xhr) {
                            abp.notify.error(getErrorMessage(xhr));
                        })
                        .finally(function () {
                            abp.ui.clearBusy($form);
                        });
                });
            });
        }
    });
})();
