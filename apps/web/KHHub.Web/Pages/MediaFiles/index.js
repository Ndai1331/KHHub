(function () {
    var l = abp.localization.getResource('MasterDataService');
    var mediaFileService = window.kHHub.masterDataService.services.mediaFiles.mediaFiles;

    var editModal = new abp.ModalManager({
        viewUrl: abp.appPath + 'MediaFiles/EditModal',
        scriptUrl: abp.appPath + 'Pages/MediaFiles/editModal.js',
        modalClass: 'mediaFileEdit',
    });

    var pageSize = 48;

    function mountMediaExplorer($root) {
        if ($root.data('mf-mounted')) {
            return;
        }
        $root.data('mf-mounted', true);

        function mf(name) {
            return $root.find('[data-mf="' + name + '"]');
        }

        var pickerMode =
            $root.attr('data-picker-mode') === 'true' ||
            $root.data('picker-mode') === true;

        function notifyPickerSelection(url) {
            if (!pickerMode) {
                return false;
            }

            var u = (url || '').trim();
            if (!u) {
                return false;
            }

            var hook = window.__KHHubMediaExplorerOnPick;
            if (typeof hook === 'function') {
                hook(u);
                return true;
            }

            if (window.parent && window.parent !== window) {
                window.parent.postMessage({ type: 'KHHub_MEDIA_PICK', url: u }, window.location.origin);
                return true;
            }

            return false;
        }

        var state = {
            parentPath: null,
            filterText: '',
            sortMode: 'lastMod',
            viewMode: 'grid',
            skip: 0,
            totalCount: 0,
        };

        var $busyRoot = mf('busy-root');

        function getAntiForgeryToken() {
            var t = mf('busy-root').find('input[name="__RequestVerificationToken"]').val();
            if (t) {
                return t;
            }
            return $('input[name="__RequestVerificationToken"]').first().val();
        }

        function getSortingParam() {
            if (state.sortMode === 'name') {
                return 'OriginalFileName asc';
            }
            return 'LastModificationTime DESC';
        }

        function truncate(name, len) {
            if (!name) {
                return '';
            }

            len = len || 32;
            return name.length > len ? name.substring(0, len - 1) + '\u2026' : name;
        }

        function formatBytes(bytes) {
            bytes = Number(bytes) || 0;
            if (bytes === 0) {
                return '\u2014';
            }

            var u = ['B', 'KB', 'MB', 'GB'];
            var i = Math.floor(Math.log(bytes) / Math.log(1024));
            var v = bytes / Math.pow(1024, i);
            return v.toFixed(i > 1 ? (v >= 100 ? 0 : v >= 10 ? 1 : 2) : 0) + ' ' + u[i];
        }

        function friendlyDate(dateStr) {
            if (!dateStr) {
                return '';
            }

            var d = new Date(dateStr);
            var now = new Date();
            var startToday = new Date(now.getFullYear(), now.getMonth(), now.getDate());
            var startY = new Date(d.getFullYear(), d.getMonth(), d.getDate());
            var diffDays = Math.round((startToday - startY) / (24 * 3600 * 1000));

            if (diffDays === 0) {
                return l('MediaExplorer:Today');
            }

            if (diffDays === 1) {
                return l('MediaExplorer:Yesterday');
            }

            return d.toLocaleDateString(undefined, { day: '2-digit', month: 'short' });
        }



        var explorerImagePreviewExts = ['jpg', 'jpeg', 'png', 'gif', 'webp', 'svg', 'bmp'];



        function explorerNormalizeExt(ext) {


            if (!ext) {
                return '';
            }



            return String(ext)
                .replace(/^\./, '')
                .toLowerCase();


        }



        function fileIcon(record) {
            if (record.fileType === 6) {
                return '<i class="fa fa-folder text-primary"></i>';
            }

            var ext = explorerNormalizeExt(record.extension || '');
            if (['jpg', 'jpeg', 'png', 'gif', 'webp', 'svg', 'bmp'].indexOf(ext) >= 0) {
                return '<i class="fa fa-file-photo-o text-success"></i>';
            }

            if (ext === 'pdf') {
                return '<i class="fa fa-file-pdf-o text-danger"></i>';
            }

            if (['zip', 'rar', '7z'].indexOf(ext) >= 0) {
                return '<i class="fa fa-file-archive-o text-info"></i>';
            }

            if (['doc', 'docx'].indexOf(ext) >= 0) {
                return '<i class="fa fa-file-word-o text-primary"></i>';
            }

            if (['xls', 'xlsx', 'csv'].indexOf(ext) >= 0) {
                return '<i class="fa fa-file-excel-o text-success"></i>';
            }

            return '<i class="fa fa-file-o text-muted"></i>';
        }


        function isExplorerImageRecord(record) {
            if (!record || record.fileType === 6) {


                return false;

            }



            var ex = explorerNormalizeExt(record.extension || '');


            return explorerImagePreviewExts.indexOf(ex) >= 0;

        }

        function buildExplorerItemThumb(record, isFolder) {
            var rawUrl =
                record &&
                record.url &&
                String(record.url)



                    .trim();


            var $wrap = $('<div class="mf-icon-wrap"></div>');



            if (!isFolder && isExplorerImageRecord(record) && rawUrl) {


                var $frame = $('<div class="mf-thumb-frame" role="presentation"></div>');





                var $img = $('<img class="mf-thumb-img" draggable="false" decoding="async" loading="lazy" alt="" />');



                $img.attr('src', rawUrl);


                $img.one('error', function () {


                    $wrap.removeClass('mf-thumb-image');


                    $wrap.empty();


                    $wrap.html(fileIcon(record));


                });


                $wrap.addClass('mf-thumb-image');


                $frame.append($img);


                $wrap.append($frame);


                return $wrap;


            }


            $wrap.html(fileIcon(record));


            return $wrap;


        }



        var explorerImageZoomScale = 1;
        var explorerPreviewPickUrl = '';


        function getExplorerImageBootstrapModal() {
            var el = mf('image-preview-modal')[0];

            if (!el || !(window.bootstrap && bootstrap.Modal)) {
                return null;
            }


            return bootstrap.Modal.getOrCreateInstance(el);

        }



        function clampExplorerImageZoom(scale) {


            var s = Number(scale) || 1;

            if (s < 0.25) {
                return 0.25;
            }



            if (s > 6) {
                return 6;


            }



            return s;

        }

        function refreshExplorerImageZoomChrome() {


            explorerImageZoomScale = clampExplorerImageZoom(explorerImageZoomScale);

            mf('image-preview-modal').find('.mf-img-preview-inner').css('transform', 'scale(' + explorerImageZoomScale + ')');


            mf('img-zoom-indicator').text(Math.round(explorerImageZoomScale * 100) + '%');


            mf('img-zoom-out').prop('disabled', explorerImageZoomScale <= 0.2501);



            mf('img-zoom-in').prop('disabled', explorerImageZoomScale >= 5.999);

        }

        function resetExplorerImageZoomForModal() {


            explorerImageZoomScale = 1;

            refreshExplorerImageZoomChrome();

        }




        function openExplorerImagePreview(url, displayTitle) {


            var modalInst = getExplorerImageBootstrapModal();



            if (!modalInst) {
                if (url) {
                    window.open(url, '_blank');

                }



                return;

            }



            mf('img-preview-stage').scrollTop(0).scrollLeft(0);


            resetExplorerImageZoomForModal();


            mf('image-preview-title').text(displayTitle || l('MediaExplorer:ImagePreview'));



            var elImg = mf('img-preview-img')[0];

            if (!elImg) {
                modalInst.hide();

                return;

            }



            elImg.alt = displayTitle || '';


            elImg.removeAttribute('src');

            elImg.src = url || '';

            explorerPreviewPickUrl = (url || '').trim();

            if (pickerMode) {
                mf('img-pick-btn').removeClass('d-none');
            } else {
                mf('img-pick-btn').addClass('d-none');
            }

            modalInst.show();


        }





        function caption() {
            var text = state.parentPath ? truncate(state.parentPath, 220) : l('MediaExplorer:BreadcrumbRoot');
            mf('folder-caption').text(text);
        }

        function renderBreadcrumbs() {
            var $bc = mf('breadcrumbs').empty();

            var $root = $('<a href="#" class="mf-bc-item"></a>').text(l('MediaExplorer:BreadcrumbRoot'));
            $bc.append($root);

            $root.on('click', function (ev) {
                ev.preventDefault();
                navigateTo(null);
            });

            if (!state.parentPath) {
                return;
            }

            var segments = state.parentPath.split('/');
            var acc = '';

            segments.forEach(function (seg) {
                if (!seg) {
                    return;
                }

                acc = acc ? acc + '/' + seg : seg;
                $bc.append($('<span class="mf-bc-sep">/</span>'));

                var $a = $('<a href="#" class="mf-bc-item"></a>').text(seg);
                $a.data('path', acc);

                $bc.append($a);

                $a.on('click', function (ev) {
                    ev.preventDefault();
                    var p = $(this).data('path');
                    navigateTo(p || null);
                });
            });
        }

        function navigateTo(path) {
            state.parentPath = path || null;

            state.skip = 0;
            caption();

            reload();
        }

        function renderPagination() {
            var $p = mf('pager').empty();
            if (state.totalCount <= pageSize) {
                return;
            }

            var cur = Math.floor(state.skip / pageSize);

            function addBtn(label, dis, cb) {

                $('<button type="button" class="btn btn-outline-secondary btn-sm rounded-2"></button>')
                    .text(label)
                    .prop('disabled', dis)
                    .on('click', cb)

                    .appendTo($p);

            }


            addBtn('Prev', cur <= 0, function () {


                state.skip = Math.max(0, state.skip - pageSize);
                reload();

            });


            addBtn(
                'Next',
                state.skip + pageSize >= state.totalCount,
                function () {

                    state.skip += pageSize;
                    reload();

                },

            );


        }



        function buildItemMarkup(record) {


            var isFolder = record.fileType === 6;

            var name = record.originalFileName || record.fileName || '';



            var dateLbl =
                friendlyDate(record.lastModificationTime || record.creationTime) || '';

            var metaLine = isFolder
                ? '<div class="mf-meta">' + dateLbl + '</div>'
                : '<div class="mf-meta">' + dateLbl + ' \u2022 ' + formatBytes(record.size) + '</div>';

            var permissionEdit = abp.auth.isGranted('MasterDataService.MediaFiles.Edit');


            var permissionDel = abp.auth.isGranted('MasterDataService.MediaFiles.Delete');


            var menuParts = '';

            if (pickerMode && !isFolder && isExplorerImageRecord(record)) {
                menuParts +=
                    '<li><a href="#" class="dropdown-item mf-pick-image text-primary">' +
                    l('MediaExplorer:SelectImage') +
                    '</a></li>';
            }

            menuParts +=
                '<li><a href="#" class="dropdown-item mf-open-url">' +
                l('MediaExplorer:OpenUrl') +
                '</a></li>';

            menuParts +=
                '<li><a href="#" class="dropdown-item mf-download-url">' +
                l('MediaExplorer:Download') +
                '</a></li>';

            if (permissionEdit && !isFolder) {
                menuParts +=
                    '<li><a href="#" class="dropdown-item mf-rename">' +
                    l('Rename') +
                    '</a></li>';

                menuParts +=
                    '<li><a href="#" class="dropdown-item mf-edit-details">' +
                    l('MediaExplorer:EditDetails') +
                    '</a></li>';

            }



            if (permissionDel) {
                menuParts +=
                    '<li><a href="#" class="dropdown-item mf-delete text-danger">' +
                    l('Delete') +
                    '</a></li>';
            }

            var $card = $('<div class="mf-item-card" tabindex="0"></div>');

            $card.attr('data-id', record.id);

            var $dots = $('<div class="dropdown mf-item-menu-btn"></div>');

            $dots.html(

                '<button class="btn btn-sm btn-link text-muted rounded-2" type="button" data-bs-toggle="dropdown" aria-expanded="false">' +

                    '<i class="fa fa-ellipsis-v"></i>' +

                    '</button>' +

                    '<ul class="dropdown-menu dropdown-menu-end">' +


                    menuParts +

                    '</ul>',

            );



            if (!record.url || isFolder) {
                $dots.find('.mf-open-url').closest('li').hide();

                $dots.find('.mf-download-url').closest('li').hide();

            }



            var $wrap = buildExplorerItemThumb(record, isFolder);



            var $nm = $('<div class="mf-name"></div>').text(truncate(name, 56));


            $card.append($dots);


            $card.append($wrap);

            $card.append($('<div></div>').append($nm).append(metaLine));


            $card.on('click', function (e) {

                if ($(e.target).closest('.dropdown').length > 0) {
                    return;
                }

                if (isFolder) {
                    navigateTo(record.path);


                }


            });


            $card.on('dblclick', function (e) {


                if ($(e.target).closest('.dropdown').length > 0) {


                    return;


                }



                if (isFolder || !isExplorerImageRecord(record)) {
                    return;


                }



                var u = record.url;


                if (!(u || '').trim()) {


                    abp.notify.warn(l('MediaExplorer:NoImageUrlHint'));


                    return;

                }



                if (notifyPickerSelection(u)) {

                    return;

                }



                openExplorerImagePreview(u, record.originalFileName || record.fileName || '');

            });


            function openHref(u) {

                window.open(u, '_blank');

            }



            $card.find('.mf-pick-image').on('click', function (e) {
                e.preventDefault();
                e.stopPropagation();

                var u = record.url;
                if (!(u || '').trim()) {
                    abp.notify.warn(l('MediaExplorer:NoImageUrlHint'));
                    return;
                }

                notifyPickerSelection(u);
            });


            $card.find('.mf-open-url').on('click', function (e) {


                e.preventDefault();

                e.stopPropagation();


                openHref(record.url);


            });


            $card.find('.mf-download-url').on('click', function (e) {


                e.preventDefault();

                e.stopPropagation();


                openHref(record.url);

            });


            $card.find('.mf-edit-details').on('click', function (e) {

                e.preventDefault();

                e.stopPropagation();

                editModal.open({
                    id: record.id,

                });

            });


            $card.find('.mf-rename').on('click', function (e) {

                e.preventDefault();

                e.stopPropagation();

                var nx = window.prompt(l('MediaExplorer:RenamePrompt'), record.originalFileName || '');

                if (!(nx || '').trim()) {
                    return;
                }



                callRenameExplorer(record.id, nx.trim());

            });


            $card.find('.mf-delete').on('click', function (e) {

                e.preventDefault();

                e.stopPropagation();

                abp.message.confirm(l('MediaExplorer:DeleteConfirm')).then(function (ok) {


                    if (!ok) {


                        return;

                    }



                    deleteExplorer(record.id);



                });


            });


            return $card;

        }



        function applyViewMode(host) {


            host.toggleClass('list-view', state.viewMode === 'list');

        }



        function renderItems(items) {

            var host = mf('items-host').empty();


            applyViewMode(host);



            caption();

            if (!items.length) {


                $('<div class="text-center text-muted py-32"></div>')

                    .text(l('MediaExplorer:EmptyState'))


                    .appendTo(host);

                return;

            }



            items.forEach(function (r) {

                host.append(buildItemMarkup(r));

            });

        }



        async function reload() {
            abp.ui.setBusy($busyRoot);

            try {            var input = {


                    skipCount: state.skip,


                    maxResultCount: pageSize,


                    sorting: getSortingParam(),



                    filterText: state.filterText || undefined,

                    parentFolderPath: state.parentPath || undefined,


                };



                var result = await mediaFileService.getExplorerList(input);



                state.totalCount = result.totalCount;

                renderItems(result.items);

                renderBreadcrumbs();


                caption();


                renderPagination();

            } catch (error) {

                notifyError(error);

            } finally {

                abp.ui.clearBusy($busyRoot);

            }

        }



        function notifyError(error) {


            var msg =

                error &&


                error.responseJSON &&


                error.responseJSON.error &&


                error.responseJSON.error.message

                    ? error.responseJSON.error.message

                    : error && error.message

                      ? error.message

                      : l('AnErrorOccurred');

            abp.notify.error(msg);



        }



        async function uploadOne(file) {


            var formData = new FormData();

            formData.append('file', file);

            formData.append('parentFolderPath', state.parentPath || '');



            var token = getAntiForgeryToken();

            if (token) {

                formData.append('__RequestVerificationToken', token);

            }



            return abp.ajax({

                url: abp.appPath + 'MediaFiles/ExplorerUpload',

                type: 'POST',

                data: formData,

                processData: false,

                contentType: false,

            });

        }



        async function handleFiles(fileList) {

            if (!fileList || !fileList.length) {
                return;
            }



            abp.ui.setBusy($busyRoot);



            try {

                for (var i = 0; i < fileList.length; i++) {

                    await uploadOne(fileList[i]);


                }



                abp.notify.success(l('MediaExplorer:UploadSuccess'));

                state.skip = 0;

                await reload();


            } catch (error) {

                notifyError(error);

            } finally {


                abp.ui.clearBusy($busyRoot);

            }

        }



        async function deleteExplorer(id) {


            abp.ui.setBusy($busyRoot);

            try {

                await mediaFileService.deleteExplorerEntry(id);

                abp.notify.success(l('SuccessfullyDeleted'));

                state.skip = 0;

                await reload();

            } catch (error) {

                notifyError(error);


            } finally {

                abp.ui.clearBusy($busyRoot);

            }

        }



        async function callRenameExplorer(id, displayName) {


            abp.ui.setBusy($busyRoot);

            try {

                await mediaFileService.renameExplorerItem(id, {

                    displayName: displayName,


                });

                abp.notify.success(l('Successful'));

                await reload();

            } catch (error) {

                notifyError(error);

            } finally {


                abp.ui.clearBusy($busyRoot);



            }



        }





        renderBreadcrumbs();

        caption();


        reload();


        mf('img-zoom-in').on('click', function () {

            explorerImageZoomScale *= 1.2;


            refreshExplorerImageZoomChrome();

        });


        mf('img-zoom-out').on('click', function () {


            explorerImageZoomScale /= 1.2;


            refreshExplorerImageZoomChrome();


        });


        mf('img-zoom-reset').on('click', function () {


            resetExplorerImageZoomForModal();

        });


        mf('img-pick-btn').on('click', function () {
            if (notifyPickerSelection(explorerPreviewPickUrl)) {
                var mi = getExplorerImageBootstrapModal();
                if (mi) {
                    mi.hide();
                }
            }
        });


        mf('image-preview-modal').on('hidden.bs.modal', function () {


            var imgEl = mf('img-preview-img')[0];


            if (imgEl) {


                imgEl.removeAttribute('src');


                imgEl.alt = '';

            }


            explorerImageZoomScale = 1;


            explorerPreviewPickUrl = '';


            mf('img-pick-btn').addClass('d-none');


            refreshExplorerImageZoomChrome();


            mf('img-preview-stage').scrollTop(0).scrollLeft(0);

        });



        mf('search-input').on('keydown', function (e) {


            if (e.key === 'Enter') {

                e.preventDefault();

                state.filterText = ($(this).val() || '').trim();


                state.skip = 0;

                reload();


            }


        });


        mf('sort-select').on('change', function () {

            state.sortMode = $(this).val();


            reload();

        });



        mf('view-grid').on('click', function () {


            state.viewMode = 'grid';

            mf('view-grid').addClass('active');


            mf('view-list').removeClass('active');

            applyViewMode(mf('items-host'));

        });



        mf('view-list').on('click', function () {


            state.viewMode = 'list';


            mf('view-list').addClass('active');

            mf('view-grid').removeClass('active');


            applyViewMode(mf('items-host'));

        });



        mf('new-folder-action').on('click', async function (e) {
            e.preventDefault();
            if (!abp.auth.isGranted('MasterDataService.MediaFiles.Create')) {
                return;
            }

            var name = window.prompt(l('MediaExplorer:NewFolderPrompt'), '');
            if (!(name || '').trim()) {
                return;
            }

            abp.ui.setBusy($busyRoot);

            try {
                await mediaFileService.createFolder({
                    name: name.trim(),
                    parentFolderPath: state.parentPath || '',
                });
                abp.notify.success(l('Successful'));
                state.skip = 0;
                await reload();
            } catch (error) {
                notifyError(error);
            } finally {
                abp.ui.clearBusy($busyRoot);
            }
        });

        mf('open-file-picker-btn').add(mf('browse-btn')).on('click', function (e) {

            e.preventDefault();

            mf('file-input').trigger('click');


        });



        mf('file-input').on('change', function () {


            var fs = this.files;

            handleFiles(fs);

            $(this).val('');


        });



        var $zone = mf('upload-zone');


        $zone.on('dragenter dragover', function (e) {


            e.preventDefault();

            $zone.addClass('mf-drag');

        });


        $zone.on('dragleave', function () {


            $zone.removeClass('mf-drag');

        });



        $zone.on('drop', function (e) {


            e.preventDefault();

            $zone.removeClass('mf-drag');


            var dt =


                e.originalEvent &&



                e.originalEvent.dataTransfer;



            if (!dt || !dt.files || !dt.files.length) {


                return;



            }



            handleFiles(dt.files);



        });



        $root.data('mf-reload', reload);
    }

    $(function () {
        editModal.onResult(function () {
            $('.mf-explorer-root').each(function () {
                var fn = $(this).data('mf-reload');
                if (typeof fn === 'function') {
                    fn();
                }
            });
        });

        $('.mf-explorer-root').each(function () {
            var $r = $(this);
            if ($r.attr('data-mf-lazy') === 'true') {
                return;
            }
            mountMediaExplorer($r);
        });
    });

    window.kHHub = window.kHHub || {};
    window.kHHub.initMediaExplorerRoot = function (el) {
        mountMediaExplorer($(el));
    };
    window.kHHub.mediaExplorerReload = function () {
        var fn = $('#mf-explorer-page').data('mf-reload');
        if (typeof fn === 'function') {
            fn();
        }
    };
})();
