(function () {
    var l = function (key) {
        return abp.localization.getResource('MasterDataService')(key);
    };

    function getErrorMessage(xhrOrError) {
        var json = xhrOrError && (xhrOrError.responseJSON || xhrOrError);
        if (json && json.error && json.error.message) {
            return json.error.message;
        }
        if (xhrOrError && xhrOrError.message) {
            return xhrOrError.message;
        }
        return l('AnErrorOccurred');
    }

    function isEmptyGuid(s) {
        return !s || s === '00000000-0000-0000-0000-000000000000';
    }

    function val($f, prop) {
        var $e = $f.find('[name="Job.' + prop + '"]');
        return $e.length ? ($e.val() || '').toString() : '';
    }

    function iv($f, prop, defaultValue) {
        var raw = val($f, prop);
        var n = parseInt(raw, 10);
        return isNaN(n) ? (defaultValue !== undefined ? defaultValue : 0) : n;
    }

    function fvNullable($f, prop) {
        var raw = val($f, prop);
        if (!raw) {
            return null;
        }
        var n = parseFloat(raw.replace(',', '.'));
        return isNaN(n) ? null : n;
    }

    function bv($f, prop) {
        var $e = $f.find('input[name="Job.' + prop + '"][type="checkbox"]');
        return $e.length ? $e.is(':checked') : false;
    }

    function syncRichEditors() {
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
            title: val($f, 'Title'),
            slug: val($f, 'Slug'),
            summary: val($f, 'Summary') || null,
            description: val($f, 'Description') || null,
            requirements: val($f, 'Requirements') || null,
            benefits: val($f, 'Benefits') || null,
            thumbnailUrl: val($f, 'ThumbnailUrl') || null,
            coverImageUrl: val($f, 'CoverImageUrl') || null,
            employmentType: iv($f, 'EmploymentType', 0),
            workMode: iv($f, 'WorkMode', 0),
            experienceLevel: iv($f, 'ExperienceLevel', 0),
            salaryMin: fvNullable($f, 'SalaryMin'),
            salaryMax: fvNullable($f, 'SalaryMax'),
            salaryText: val($f, 'SalaryText') || null,
            salaryCurrency: val($f, 'SalaryCurrency') || null,
            location: val($f, 'Location') || null,
            contactEmail: val($f, 'ContactEmail') || null,
            contactPhone: val($f, 'ContactPhone') || null,
            applicationUrl: val($f, 'ApplicationUrl') || null,
            publishedAt: val($f, 'PublishedAt') || null,
            status: iv($f, 'Status', 0),
            viewCount: iv($f, 'ViewCount', 0),
            applicationCount: iv($f, 'ApplicationCount', 0),
            favoriteCount: iv($f, 'FavoriteCount', 0),
            shareCount: iv($f, 'ShareCount', 0),
            isFeatured: bv($f, 'IsFeatured'),
            isUrgent: bv($f, 'IsUrgent'),
            isHot: bv($f, 'IsHot'),
            seoTitle: val($f, 'SeoTitle'),
            seoDescription: val($f, 'SeoDescription') || null,
            seoKeywords: val($f, 'SeoKeywords') || null,
            provinceId: val($f, 'ProvinceId'),
            wardId: val($f, 'WardId'),
            jobCategoryId: val($f, 'JobCategoryId'),
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

    /** JobStatus.Published = 1 */
    function applySaveAction(dto, action) {
        if (action === 'save-and-publish') {
            dto.status = 1;
        }
    }

    function validateRequired(dto) {
        if (isEmptyGuid(dto.jobCategoryId)) {
            abp.notify.error(l('JobCategoryRequired'));
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

    function syncJobTags(jobId, $form) {
        if (!window.kHHubTagPicker || typeof window.kHHubTagPicker.syncJobTags !== 'function') {
            var done = $.Deferred();
            done.resolve();
            return done.promise();
        }
        return window.kHHubTagPicker.syncJobTags(jobId, $form);
    }

    function finishSave() {
        abp.notify.success(l('JobSavedSuccessfully'));
        window.location.href = abp.appPath + 'Jobs';
    }

    $(function () {
        var jobService = window.kHHub.masterDataService.services.jobs.jobs;

        var $createForm = $('#JobForm[data-job-mode="create"]');
        if ($createForm.length) {
            $createForm.on('submit', function (e) {
                e.preventDefault();
            });

            $(document).on('click', 'button[type="submit"][form="JobForm"][name="action"]', function (e) {
                e.preventDefault();
                syncRichEditors();
                syncSeoKeywordsField();
                var dto = readCreateDto($createForm);
                applySaveAction(dto, $(this).val() || 'save');
                if (!validateRequired(dto)) {
                    return;
                }

                runWithBusy($createForm, function () {
                    return jobService.create(dto);
                })
                    .done(function (created) {
                        var jobId = created && created.id;
                        if (!jobId) {
                            finishSave();
                            return;
                        }
                        syncJobTags(jobId, $createForm)
                            .done(finishSave)
                            .fail(function () {
                                abp.notify.warn(l('TagPickerSyncFailed'));
                                finishSave();
                            });
                    })
                    .fail(function (xhr) {
                        abp.notify.error(getErrorMessage(xhr));
                    });
            });
        }

        var $editForm = $('#JobEditForm[data-job-mode="edit"]');
        if ($editForm.length) {
            $editForm.on('submit', function (e) {
                e.preventDefault();
            });

            $(document).on('click', 'button[type="submit"][form="JobEditForm"][name="action"]', function (e) {
                e.preventDefault();
                syncRichEditors();
                syncSeoKeywordsField();
                var id = $editForm.find('input[name="Id"]').val();
                if (!id || isEmptyGuid(id)) {
                    abp.notify.error(l('AnErrorOccurred'));
                    return;
                }

                var dto = readUpdateDto($editForm);
                applySaveAction(dto, $(this).val() || 'save');
                if (!validateRequired(dto)) {
                    return;
                }

                runWithBusy($editForm, function () {
                    return jobService.update(id, dto);
                })
                    .done(function () {
                        syncJobTags(id, $editForm)
                            .done(finishSave)
                            .fail(function () {
                                abp.notify.warn(l('TagPickerSyncFailed'));
                                finishSave();
                            });
                    })
                    .fail(function (xhr) {
                        abp.notify.error(getErrorMessage(xhr));
                    });
            });
        }
    });
})();
