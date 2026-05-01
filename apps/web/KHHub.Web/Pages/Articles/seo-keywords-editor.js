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

    function syncToHidden() {
        var $root = $('#SeoKeywordsEditor');
        if (!$root.length) {
            return;
        }
        var parts = [];
        $root.find('.seo-keyword-input').each(function () {
            var v = ($(this).val() || '').trim();
            if (v) {
                parts.push(v);
            }
        });
        $('#ArticleSeoKeywords').val(parts.join('|'));
    }

    function addRow(value) {
        var $rows = $('#SeoKeywordsRows');
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
        if (!$('#SeoKeywordsEditor').length) {
            return;
        }

        var initial = ($('#ArticleSeoKeywords').val() || '').toString();
        var list = parseKeywords(initial);
        if (list.length === 0) {
            addRow('');
        } else {
            list.forEach(function (k) {
                addRow(k);
            });
        }
        syncToHidden();

        $('#SeoKeywordsAddBtn').on('click', function () {
            addRow('');
            syncToHidden();
            $('#SeoKeywordsRows .seo-keyword-input').last().trigger('focus');
        });

        $('#SeoKeywordsRows').on('click', '.seo-keyword-remove', function () {
            $(this).closest('.seo-keyword-row').remove();
            if ($('#SeoKeywordsRows .seo-keyword-row').length === 0) {
                addRow('');
            }
            syncToHidden();
        });

        $('#SeoKeywordsRows').on('input', '.seo-keyword-input', function () {
            syncToHidden();
        });

        window.kHHubArticleSeoKeywordsSync = syncToHidden;
    });
})();
