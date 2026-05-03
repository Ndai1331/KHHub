(function () {
    function parseKeywords(raw) {
        if (!raw) {
            return [];
        }
        return raw
            .split('|')
            .map(function (s) {
                return s.trim();
            })
            .filter(function (s) {
                return s.length > 0;
            });
    }

    function getEditorRoot() {
        return $('#SeoKeywordsEditor');
    }

    function getHiddenInput($root) {
        return $root.find('input[type="hidden"]').filter(function () {
            return (this.name || '').toString().indexOf('SeoKeywords') !== -1;
        });
    }

    function syncToHidden() {
        var $root = getEditorRoot();
        if (!$root.length) {
            return;
        }
        var $hidden = getHiddenInput($root);
        if (!$hidden.length) {
            return;
        }
        var parts = [];
        $root.find('.seo-keyword-input').each(function () {
            var v = ($(this).val() || '').trim();
            if (v) {
                parts.push(v);
            }
        });
        $hidden.val(parts.join('|'));
    }

    function addRow($root, value) {
        var $rows = $root.find('#SeoKeywordsRows');
        var $row = $(
            '<div class="seo-keyword-row input-group mb-2">' +
                '<input type="text" class="form-control seo-keyword-input" autocomplete="off" />' +
                '<button type="button" class="btn btn-danger seo-keyword-remove" title="Remove">' +
                '<i class="fa fa-trash"></i>' +
                '</button>' +
                '</div>'
        );
        $row.find('.seo-keyword-input').val(value || '');
        $rows.append($row);
    }

    $(function () {
        var $root = getEditorRoot();
        if (!$root.length) {
            return;
        }
        var $hidden = getHiddenInput($root);
        if (!$hidden.length) {
            return;
        }

        var initial = ($hidden.val() || '').toString();
        var list = parseKeywords(initial);
        if (list.length === 0) {
            addRow($root, '');
        } else {
            list.forEach(function (k) {
                addRow($root, k);
            });
        }
        syncToHidden();

        $root.find('#SeoKeywordsAddBtn').on('click', function () {
            addRow($root, '');
            syncToHidden();
            $root.find('#SeoKeywordsRows .seo-keyword-input').last().trigger('focus');
        });

        $root.find('#SeoKeywordsRows').on('click', '.seo-keyword-remove', function () {
            $(this).closest('.seo-keyword-row').remove();
            if ($root.find('#SeoKeywordsRows .seo-keyword-row').length === 0) {
                addRow($root, '');
            }
            syncToHidden();
        });

        $root.find('#SeoKeywordsRows').on('input', '.seo-keyword-input', function () {
            syncToHidden();
        });

        window.kHHubSeoKeywordsSync = syncToHidden;
        window.kHHubArticleSeoKeywordsSync = syncToHidden;
    });
})();
