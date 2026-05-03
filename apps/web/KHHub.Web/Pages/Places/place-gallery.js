(function () {
    var MD = 'MasterDataService';

    function l(key) {
        return abp.localization.getResource(MD)(key);
    }

    function lFmt(key, arg0) {
        return l(key).replace(/\{0\}/g, String(arg0));
    }

    function resolveImgSrc(stored) {
        var s = (stored || '').trim();
        if (!s) {
            return '';
        }
        if (/^https?:\/\//i.test(s)) {
            return s;
        }
        if (s.indexOf('//') === 0) {
            return window.location.protocol + s;
        }
        var baseRaw = (typeof window.__khhubMediaPublicBaseUrl === 'string' ? window.__khhubMediaPublicBaseUrl : '').trim();
        if (!baseRaw || s.indexOf('/') !== 0) {
            return s;
        }
        try {
            return new URL(baseRaw).origin + s;
        } catch (e) {
            return s;
        }
    }

    function applyMediaPreviewToImg($img, mf) {
        var stored = '';
        if (mf) {
            // Prefer publicPath so KHHubMediaPreview uses presigned GET; mf.url is often unsigned public URL from DB.
            stored = ((mf.publicPath || mf.url || mf.path || '') + '').trim();
        }
        var preview = window.KHHubMediaPreview;
        if (preview && typeof preview.applyPreviewToImg === 'function') {
            preview.applyPreviewToImg($img, stored);
        } else if ($img && $img.length) {
            var src = resolveImgSrc(stored);
            if (src) {
                $img.attr('src', src).removeClass('d-none');
            } else {
                $img.attr('src', '').addClass('d-none');
            }
        }
    }

    var galleryModalCleanupBound = false;

    function bindGalleryModalCleanup() {
        if (galleryModalCleanupBound) {
            return;
        }
        galleryModalCleanupBound = true;
        $(document).on('hidden.bs.modal', '#khhub-media-library-modal', function (e) {
            // Ignore bubbled hidden from nested image preview modal (same issue as media-library-picker.js).
            if (e.target !== this) {
                return;
            }
            var $modal = $('#khhub-media-library-modal');
            var hadGalleryFooter = $modal.find('.khhub-place-gallery-batch-footer').length > 0;
            $modal.find('.khhub-place-gallery-batch-footer').remove();
            $(document).off('khhub:mlp-gallery-batch-selection-changed.placeGalFooter');
            $('#khhub-mlp-explorer-root').removeAttr('data-khhub-gallery-batch');
            window.__KHHubSyncGalleryBatchFooter = undefined;
            if (hadGalleryFooter && window.kHHub && typeof window.kHHub.clearMlpGalleryBatchSelection === 'function') {
                window.kHHub.clearMlpGalleryBatchSelection();
            }
        });
    }

    function initGallery($root) {
        var placeId = ($root.data('place-id') || '').toString();
        if (!placeId || placeId === '00000000-0000-0000-0000-000000000000') {
            return;
        }

        var entityFileService = window.kHHub.masterDataService.services.entityFiles.entityFiles;
        var itemsCache = [];

        var $thumbs = $root.find('.khhub-place-gallery-thumbs');
        var $moreWrap = $root.find('.khhub-place-gallery-more-wrap');
        var $busy = $root.find('.khhub-place-gallery-busy');
        var maxVisible = 5;

        function setBusy(on) {
            if (on) {
                $busy.removeClass('d-none');
                abp.ui.setBusy($root);
            } else {
                $busy.addClass('d-none');
                abp.ui.clearBusy($root);
            }
        }

        function renderThumbs() {
            $thumbs.empty();
            var total = itemsCache.length;
            var visible = itemsCache.slice(0, maxVisible);
            visible.forEach(function (row, idx) {
                var ef = row.entityFile;
                var mf = row.mediaFile;
                var $wrap = $('<div class="khhub-place-gallery-thumb-wrap"/>');
                var $img = $('<img loading="lazy" class="khhub-place-gallery-thumb"/>').attr('alt', '');
                applyMediaPreviewToImg($img, mf);
                $img.on('click', function () {
                    openCarousel(idx);
                });
                var $rm = $(
                    '<button type="button" class="khhub-place-gallery-thumb-remove" aria-label="remove"><i class="fa fa-times"></i></button>'
                );
                $rm.on('click', function (e) {
                    e.stopPropagation();
                    abp.message.confirm(l('DeleteConfirmationMessage')).then(function (ok) {
                        if (!ok) {
                            return;
                        }
                        entityFileService
                            .delete(ef.id)
                            .done(function () {
                                abp.notify.success(l('SuccessfullyDeleted'));
                                reload();
                            })
                            .fail(function () {
                                abp.notify.error(l('AnErrorOccurred'));
                            });
                    });
                });
                $wrap.append($img, $rm);
                $thumbs.append($wrap);
            });

            if (total > maxVisible) {
                $moreWrap.removeClass('d-none');
            } else {
                $moreWrap.addClass('d-none');
            }
        }

        function rebuildCarouselDom() {
            var $inner = $('#PlaceGalleryCarouselModal .khhub-place-gallery-carousel-inner');
            $inner.empty();
            itemsCache.forEach(function (row, idx) {
                var mf = row.mediaFile;
                var active = idx === 0 ? ' active' : '';
                var $item = $('<div class="carousel-item' + active + '"/>');
                var $img = $('<img loading="lazy" class="d-block w-100"/>').attr('alt', '');
                applyMediaPreviewToImg($img, mf);
                $item.append($img);
                $inner.append($item);
            });
        }

        function openCarousel(startIndex) {
            rebuildCarouselDom();
            var el = document.querySelector('#PlaceGalleryCarousel');
            if (!el || !window.bootstrap) {
                return;
            }
            var car = bootstrap.Carousel.getOrCreateInstance(el, { interval: false });
            car.to(startIndex || 0);
            bootstrap.Modal.getOrCreateInstance(document.getElementById('PlaceGalleryCarouselModal')).show();
        }

        function reload() {
            setBusy(true);
            entityFileService
                .getList({
                    entityType: 'Place',
                    entityId: placeId,
                    collection: 'Gallery',
                    maxResultCount: 200,
                    skipCount: 0,
                    sorting: 'entityFile.sortOrder asc',
                })
                .done(function (page) {
                    itemsCache = page.items || [];
                    renderThumbs();
                    setBusy(false);
                })
                .fail(function () {
                    setBusy(false);
                    abp.notify.error(l('AnErrorOccurred'));
                });
        }

        $root.find('.khhub-place-gallery-view-more').on('click', function () {
            openCarousel(0);
        });

        $root.find('.khhub-place-gallery-pick').on('click', function (e) {
            e.preventDefault();
            bindGalleryModalCleanup();
            var $modal = $('#khhub-media-library-modal');
            var $explorerRoot = $('#khhub-mlp-explorer-root');
            if (!$modal.length || !$explorerRoot.length) {
                abp.notify.warn(l('AnErrorOccurred'));
                return;
            }
            $modal.data('url-target', '#khhub-place-gallery-url-dummy');

            $modal.find('.khhub-place-gallery-batch-footer').remove();

            $explorerRoot.removeData('khhub-batch-selection');

            var $footer = $(
                '<div class="khhub-place-gallery-batch-footer border-top bg-light px-3 py-3 d-flex flex-wrap gap-2 align-items-center justify-content-between">' +
                    '<span class="khhub-place-gallery-batch-count text-muted small"></span>' +
                    '<div class="d-flex flex-wrap gap-2">' +
                    '<button type="button" class="btn btn-sm btn-outline-secondary khhub-place-gallery-batch-clear"></button>' +
                    '<button type="button" class="btn btn-sm btn-primary khhub-place-gallery-batch-confirm"></button>' +
                    '</div>' +
                    '</div>'
            );
            $footer.find('.khhub-place-gallery-batch-clear').text(l('PlaceGalleryBatchClear'));
            $footer.find('.khhub-place-gallery-batch-confirm').text(l('PlaceGalleryBatchConfirm'));

            function updateBatchFooter() {
                var records =
                    window.kHHub && typeof window.kHHub.getMlpGalleryBatchRecords === 'function'
                        ? window.kHHub.getMlpGalleryBatchRecords()
                        : [];
                var n = records.length;
                $footer.find('.khhub-place-gallery-batch-count').text(lFmt('PlaceGalleryBatchSelectedCount', n));
            }

            window.__KHHubSyncGalleryBatchFooter = updateBatchFooter;

            $(document)
                .off('khhub:mlp-gallery-batch-selection-changed.placeGalFooter')
                .on('khhub:mlp-gallery-batch-selection-changed.placeGalFooter', updateBatchFooter);

            $footer.find('.khhub-place-gallery-batch-clear').on('click', function () {
                if (window.kHHub && typeof window.kHHub.clearMlpGalleryBatchSelection === 'function') {
                    window.kHHub.clearMlpGalleryBatchSelection();
                }
                updateBatchFooter();
            });

            $footer.find('.khhub-place-gallery-batch-confirm').on('click', function () {
                var queue =
                    window.kHHub && typeof window.kHHub.getMlpGalleryBatchRecords === 'function'
                        ? window.kHHub.getMlpGalleryBatchRecords().slice()
                        : [];
                if (!queue.length) {
                    abp.notify.warn(l('PlaceGalleryBatchNothingSelected'));
                    return;
                }

                var nextOrder =
                    itemsCache.length === 0
                        ? 1
                        : Math.max.apply(
                              null,
                              itemsCache.map(function (x) {
                                  return x.entityFile.sortOrder || 0;
                              })
                          ) + 1;

                setBusy(true);

                function failNotify(xhr) {
                    var msg =
                        xhr &&
                        xhr.responseJSON &&
                        xhr.responseJSON.error &&
                        xhr.responseJSON.error.message
                            ? xhr.responseJSON.error.message
                            : l('AnErrorOccurred');
                    abp.notify.error(msg);
                }

                var index = 0;

                function step() {
                    if (index >= queue.length) {
                        setBusy(false);
                        abp.notify.success(l('PlaceGalleryBatchSuccess'));
                        $(document).off('khhub:mlp-gallery-batch-selection-changed.placeGalFooter');
                        if (window.kHHub && typeof window.kHHub.clearMlpGalleryBatchSelection === 'function') {
                            window.kHHub.clearMlpGalleryBatchSelection();
                        }
                        reload();
                        $modal.find('.khhub-place-gallery-batch-footer').remove();
                        $explorerRoot.removeAttr('data-khhub-gallery-batch');
                        var mi = bootstrap.Modal.getInstance($modal[0]);
                        if (mi) {
                            mi.hide();
                        }
                        return;
                    }
                    var record = queue[index];
                    index += 1;
                    var mediaId = record.id !== undefined && record.id !== null ? record.id : record.Id;
                    entityFileService
                        .create({
                            entityType: 'Place',
                            entityId: placeId,
                            collection: 'Gallery',
                            sortOrder: nextOrder,
                            isPrimary: false,
                            mediaFileId: mediaId,
                        })
                        .done(function () {
                            nextOrder += 1;
                            step();
                        })
                        .fail(function (xhr) {
                            setBusy(false);
                            failNotify(xhr);
                        });
                }

                step();
            });

            $explorerRoot.attr('data-khhub-gallery-batch', 'true');

            $modal.off('shown.bs.modal.placeGalClearSel').on('shown.bs.modal.placeGalClearSel', function (ev) {
                if (ev.target !== $modal[0]) {
                    return;
                }
                if (window.kHHub && typeof window.kHHub.clearMlpGalleryBatchSelection === 'function') {
                    window.kHHub.clearMlpGalleryBatchSelection();
                }
                updateBatchFooter();
            });

            $modal.find('.modal-content').append($footer);
            updateBatchFooter();

            bootstrap.Modal.getOrCreateInstance($modal[0]).show();
        });

        reload();
    }

    $(function () {
        $('.khhub-place-gallery').each(function () {
            initGallery($(this));
        });
    });
})();
