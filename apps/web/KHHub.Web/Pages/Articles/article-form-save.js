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
        var $e = $f.find('[name="Article.' + prop + '"]');
        return $e.length ? ($e.val() || '').toString() : '';
    }

    function iv($f, prop, defaultValue) {
        var $e = $f.find('[name="Article.' + prop + '"]');
        if (!$e.length) {
            return defaultValue !== undefined ? defaultValue : 0;
        }
        var n = parseInt($e.val(), 10);
        return isNaN(n) ? (defaultValue !== undefined ? defaultValue : 0) : n;
    }

    function bv($f, prop) {
        var $e = $f.find('input[name="Article.' + prop + '"][type="checkbox"]');
        return $e.length ? $e.is(':checked') : false;
    }

    function readCreateDto($f) {
        return {
            title: val($f, 'Title'),
            slug: val($f, 'Slug'),
            summary: val($f, 'Summary'),
            content: val($f, 'Content'),
            thumbnailUrl: val($f, 'ThumbnailUrl') || null,
            coverImageUrl: val($f, 'CoverImageUrl') || null,
            type: iv($f, 'Type', 0),
            authorName: val($f, 'AuthorName'),
            source: val($f, 'Source') || null,
            sourceUrl: val($f, 'SourceUrl') || null,
            status: iv($f, 'Status', 0),
            publishedAt: val($f, 'PublishedAt') || null,
            isFeatured: bv($f, 'IsFeatured'),
            isHot: bv($f, 'IsHot'),
            isTrending: bv($f, 'IsTrending'),
            viewCount: iv($f, 'ViewCount', 0),
            likeCount: iv($f, 'LikeCount', 0),
            shareCount: iv($f, 'ShareCount', 0),
            commentCount: iv($f, 'CommentCount', 0),
            readingTime: iv($f, 'ReadingTime', 0),
            seoTitle: val($f, 'SeoTitle'),
            seoDescription: val($f, 'SeoDescription') || null,
            seoKeywords: val($f, 'SeoKeywords') || null,
            articleCategoryId: val($f, 'ArticleCategoryId'),
        };
    }

    function readUpdateDto($f) {
        var dto = readCreateDto($f);
        dto.concurrencyStamp = val($f, 'ConcurrencyStamp');
        return dto;
    }

    function syncTinyMce() {
        if (typeof tinymce !== 'undefined') {
            tinymce.triggerSave();
        }
    }

    function syncSeoKeywordsField() {
        if (typeof window.kHHubArticleSeoKeywordsSync === 'function') {
            window.kHHubArticleSeoKeywordsSync();
        }
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

    function applySaveAction(dto, action) {
        if (action === 'save-and-publish') {
            dto.status = 2;
        }
    }

    $(function () {
        var articleService = window.kHHub.masterDataService.services.articles.articles;

        var $createForm = $('#ArticleForm[data-article-mode="create"]');
        if ($createForm.length) {
            $createForm.on('submit', function (e) {
                e.preventDefault();
            });

            // Toolbar buttons use form="ArticleForm" but live outside the form; do not use $form.find().
            // Use [form] + [name=action] so it still works if tag helper puts id on a wrapper, not the <button>.
            $(document).on(
                'click',
                'button[type="submit"][form="ArticleForm"][name="action"]',
                function (e) {
                    e.preventDefault();
                    syncTinyMce();
                    syncSeoKeywordsField();
                    var action = $(this).val() || 'save';
                    var dto = readCreateDto($createForm);
                    applySaveAction(dto, action);

                    if (isEmptyGuid(dto.articleCategoryId)) {
                        abp.notify.error(l('ArticleCategoryRequired'));
                        return;
                    }

                    runWithBusy($createForm, function () {
                        return articleService.create(dto);
                    })
                        .done(function () {
                            abp.notify.success(l('ArticleSavedSuccessfully'));
                            window.location.href = abp.appPath + 'Articles';
                        })
                        .fail(function (xhr) {
                            abp.notify.error(getErrorMessage(xhr));
                        });
                }
            );
        }

        var $editForm = $('#ArticleEditForm[data-article-mode="edit"]');
        if ($editForm.length) {
            $editForm.on('submit', function (e) {
                e.preventDefault();
            });

            $(document).on(
                'click',
                'button[type="submit"][form="ArticleEditForm"][name="action"]',
                function (e) {
                    e.preventDefault();
                    syncTinyMce();
                    syncSeoKeywordsField();
                    var action = $(this).val() || 'save';
                    var id = $editForm.find('input[name="Id"]').val();
                    if (!id || isEmptyGuid(id)) {
                        abp.notify.error(l('AnErrorOccurred'));
                        return;
                    }

                    var dto = readUpdateDto($editForm);
                    applySaveAction(dto, action);

                    if (isEmptyGuid(dto.articleCategoryId)) {
                        abp.notify.error(l('ArticleCategoryRequired'));
                        return;
                    }

                    runWithBusy($editForm, function () {
                        return articleService.update(id, dto);
                    })
                        .done(function () {
                            abp.notify.success(l('ArticleSavedSuccessfully'));
                            window.location.href = abp.appPath + 'Articles';
                        })
                        .fail(function (xhr) {
                            abp.notify.error(getErrorMessage(xhr));
                        });
                }
            );
        }
    });
})();
