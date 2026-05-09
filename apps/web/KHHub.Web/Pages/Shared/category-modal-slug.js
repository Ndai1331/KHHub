/**
 * Auto-generate Slug from Name for category modals (Article / Job / Place).
 * Delegates on document — works when modal content is reparented under body.
 */
(function () {
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

    var bindings = [
        {
            ns: 'khArticleCategorySlug',
            name: '[name="ArticleCategory.Name"], [name="ArticleCategory[Name]"]',
            slug: '[name="ArticleCategory.Slug"], [name="ArticleCategory[Slug]"]',
        },
        {
            ns: 'khJobCategorySlug',
            name: '[name="JobCategory.Name"], [name="JobCategory[Name]"]',
            slug: '[name="JobCategory.Slug"], [name="JobCategory[Slug]"]',
        },
        {
            ns: 'khPlaceCategorySlug',
            name: '[name="PlaceCategory.Name"], [name="PlaceCategory[Name]"]',
            slug: '[name="PlaceCategory.Slug"], [name="PlaceCategory[Slug]"]',
        },
    ];

    function resolveSlugScopeFromInput($input) {
        var $modal = $input.closest('.modal');
        if ($modal.length) {
            return $modal;
        }
        return $input.closest('form');
    }

    function installDelegation($) {
        bindings.forEach(function (b) {
            $(document)
                .off('input.' + b.ns, b.name)
                .on('input.' + b.ns, b.name, function () {
                    var $name = $(this);
                    var $scope = resolveSlugScopeFromInput($name);
                    if (!$scope.length) {
                        return;
                    }
                    var $slug = $scope.find(b.slug);
                    if (!$slug.length) {
                        return;
                    }
                    $slug.val(slugifyVietnamese($name.val()));
                });
        });

        $(document)
            .off('shown.bs.modal.khCategorySlug', '.modal')
            .on('shown.bs.modal.khCategorySlug', '.modal', function () {
                var $modal = $(this);
                bindings.forEach(function (b) {
                    var $name = $modal.find(b.name);
                    var $slug = $modal.find(b.slug);
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
            });
    }

    if (typeof jQuery !== 'undefined') {
        jQuery(function ($) {
            installDelegation($);
        });
    }
})();
