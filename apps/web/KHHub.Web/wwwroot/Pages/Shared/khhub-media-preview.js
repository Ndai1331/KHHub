/**
 * Shared helpers: short media paths + optional presigned MinIO reads.
 * Requires _MediaPreviewSettings (globals) when using presigned mode.
 */
(function (window, $) {
    'use strict';

    var PLACEHOLDER_THUMB_SRC =
        'data:image/gif;base64,R0lGODlhAQABAIAAAAAAAP///yH5BAEAAAAALAAAAAABAAEAAAIBRAA7';

    function resolveMediaImageSrcForPreview(storedValue) {
        var s = (storedValue || '').trim();
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

    function usePresignedPreviewForStoredPath(stored) {
        return (
            window.__khhubMediaUsePresignedReadUrls === true &&
            stored.length > 0 &&
            stored.indexOf('/') === 0 &&
            !/^https?:\/\//i.test(stored)
        );
    }

    function fetchPresignedReadUrlByPublicPath(publicPath) {
        var svc =
            window.kHHub &&
            window.kHHub.masterDataService &&
            window.kHHub.masterDataService.services &&
            window.kHHub.masterDataService.services.mediaFiles &&
            window.kHHub.masterDataService.services.mediaFiles.mediaFiles;
        if (svc && typeof svc.getPresignedReadUrlByPublicPath === 'function') {
            return svc.getPresignedReadUrlByPublicPath(publicPath);
        }
        return abp.ajax({
            type: 'GET',
            url: abp.appPath + 'api/masterdata/media-files/presigned-read-url-by-public-path',
            data: { publicPath: publicPath },
        });
    }

    function applyPreviewToImg($img, storedPath) {
        if (!$img || !$img.length) {
            return;
        }
        var stored = (storedPath || '').trim();

        function applySrc(src) {
            var t = (src || '').trim();
            if (!t) {
                $img.attr('src', '').addClass('d-none');
                return;
            }
            $img.attr('src', t).removeClass('d-none');
        }

        if (!stored) {
            $img.attr('src', '').addClass('d-none');
            return;
        }

        if (usePresignedPreviewForStoredPath(stored)) {
            fetchPresignedReadUrlByPublicPath(stored)
                .then(function (signed) {
                    var src =
                        signed && String(signed).trim()
                            ? String(signed).trim()
                            : resolveMediaImageSrcForPreview(stored);
                    applySrc(src);
                })
                .catch(function () {
                    applySrc(resolveMediaImageSrcForPreview(stored));
                });
            return;
        }

        applySrc(resolveMediaImageSrcForPreview(stored));
    }

    /**
     * List/grid thumbnails: use src={placeholder} and data-khhub-thumb-path="/khhub-articles/...".
     */
    function hydrateThumbImages(root) {
        var $root = root ? $(root) : $(document);
        $root.find('img[data-khhub-thumb-path]').each(function () {
            var $el = $(this);
            var path = ($el.attr('data-khhub-thumb-path') || '').trim();
            if (!path) {
                return;
            }

            function applyListSrc(src) {
                var t = (src || '').trim();
                if (!t) {
                    $el.attr('src', PLACEHOLDER_THUMB_SRC);
                    return;
                }
                $el.attr('src', t);
            }

            if (usePresignedPreviewForStoredPath(path)) {
                fetchPresignedReadUrlByPublicPath(path)
                    .then(function (signed) {
                        var s =
                            signed && String(signed).trim()
                                ? String(signed).trim()
                                : resolveMediaImageSrcForPreview(path);
                        applyListSrc(s);
                    })
                    .catch(function () {
                        applyListSrc(resolveMediaImageSrcForPreview(path));
                    });
                return;
            }

            applyListSrc(resolveMediaImageSrcForPreview(path));
        });
    }

    window.KHHubMediaPreview = {
        PLACEHOLDER_THUMB_SRC: PLACEHOLDER_THUMB_SRC,
        resolveMediaImageSrcForPreview: resolveMediaImageSrcForPreview,
        usePresignedPreviewForStoredPath: usePresignedPreviewForStoredPath,
        fetchPresignedReadUrlByPublicPath: fetchPresignedReadUrlByPublicPath,
        applyPreviewToImg: applyPreviewToImg,
        hydrateThumbImages: hydrateThumbImages,
    };
})(window, jQuery);
