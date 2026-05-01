/**
 * Vietnamese slug helpers + sync Name → Slug for Article Tags modals.
 * Slug is readonly in Razor; logic uses $(document) delegation — works without ModalManager initModal.
 */
(function () {
    function normalizeVietnamese(text) {
        return (text || '')
            .replace(/[àáạảãâầấậẩẫăằắặẳẵ]/g, 'a')
            .replace(/[èéẹẻẽêềếệểễ]/g, 'e')
            .replace(/[ìíịỉĩ]/g, 'i')
            .replace(/[òóọỏõôồốộổỗơờớợởỡ]/g, 'o')
            .replace(/[ùúụủũưừứựửữ]/g, 'u')
            .replace(/[ỳýỵỷỹ]/g, 'y')
            .replace(/đ/g, 'd')
            .replace(/[ÀÁẠẢÃÂẦẤẬẨẪĂẰẮẶẲẴ]/g, 'A')
            .replace(/[ÈÉẸẺẼÊỀẾỆỂỄ]/g, 'E')
            .replace(/[ÌÍỊỈĨ]/g, 'I')
            .replace(/[ÒÓỌỎÕÔỒỐỘỔỖƠỜỚỢỞỠ]/g, 'O')
            .replace(/[ÙÚỤỦŨƯỪỨỰỬỮ]/g, 'U')
            .replace(/[ỲÝỴỶỸ]/g, 'Y')
            .replace(/Đ/g, 'D');
    }

    function slugifyVietnamese(text) {
        return (text || '')
            .toString()
            .trim()
            .normalize('NFD')
            .replace(/[\u0300-\u036f]/g, '')
            .replace(/đ/g, 'd')
            .replace(/Đ/g, 'D')
            .replace(/[^0-9a-zA-Z\s-]/g, '')
            .toLowerCase()
            .replace(/\s+/g, '-')
            .replace(/-+/g, '-')
            .replace(/^-+|-+$/g, '');
    }

    var selectors = {
        name: '[name="ArticleTag.Name"], [name="ArticleTag[Name]"]',
        slug: '[name="ArticleTag.Slug"], [name="ArticleTag[Slug]"]',
    };

    /** Modal content is often reparented to body; ancestor <form> may be missing — scope by .modal first. */
    function resolveSlugScopeFromInput($input) {
        var $modal = $input.closest('.modal');
        if ($modal.length) {
            return $modal;
        }
        return $input.closest('form');
    }

    function installDelegation($) {
        $(document)
            .off('input.articleTagSlugDel', selectors.name)
            .on(
                'input.articleTagSlugDel',
                selectors.name,
                function () {
                    var $name = $(this);
                    var $scope = resolveSlugScopeFromInput($name);
                    if (!$scope.length) {
                        return;
                    }
                    var $slug = $scope.find(selectors.slug);
                    if (!$slug.length) {
                        return;
                    }
                    $slug.val(slugifyVietnamese($name.val()));
                }
            );

        $(document)
            .off('shown.bs.modal.articleTagSlug', '.modal')
            .on('shown.bs.modal.articleTagSlug', '.modal', function () {
                var $modal = $(this);
                var $name = $modal.find(selectors.name);
                var $slug = $modal.find(selectors.slug);
                if (!$name.length || !$slug.length) {
                    return;
                }
                var slugVal = ($slug.val() || '').trim();

                if (!$name.val() && !slugVal) {
                    setTimeout(function () {
                        var el = $name[0];
                        if (el && typeof el.focus === 'function') {
                            try {
                                el.focus({ preventScroll: true });
                            } catch (e) {
                                $name.trigger('focus');
                            }
                        }
                    }, 100);
                }
            });
    }

    function noopBind() {}

    if (typeof jQuery !== 'undefined') {
        jQuery(function ($) {
            installDelegation($);
        });
    }

    window.kHHub = window.kHHub || {};
    window.kHHub.articleTagForm = {
        bindSlug: noopBind,
        normalizeVietnamese: normalizeVietnamese,
        slugify: slugifyVietnamese,
    };
})();
