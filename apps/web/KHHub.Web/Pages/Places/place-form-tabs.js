(function () {
    var l = function (key) {
        return abp.localization.getResource('MasterDataService')(key);
    };

    function getReviewStatusBadge(status) {
        if (status === 1) {
            return '<span class="badge bg-success-subtle text-success">' + l('Enum:PlaceReviewStatus.1') + '</span>';
        }
        if (status === 0) {
            return '<span class="badge bg-warning-subtle text-warning">' + l('Enum:PlaceReviewStatus.0') + '</span>';
        }
        if (status === 2) {
            return '<span class="badge bg-danger-subtle text-danger">' + l('Enum:PlaceReviewStatus.2') + '</span>';
        }
        return '<span class="badge bg-light text-dark">' + l('Enum:PlaceReviewStatus.' + status) + '</span>';
    }

    function initEmbeddedReviews(placeId) {
        var placeReviewService = window.kHHub.masterDataService.services.placeReviews.placeReviews;

        var createModal = new abp.ModalManager({
            viewUrl: abp.appPath + 'PlaceReviews/CreateModal',
            scriptUrl: abp.appPath + 'Pages/PlaceReviews/createModal.js',
            modalClass: 'placeReviewCreate',
        });

        var editModal = new abp.ModalManager({
            viewUrl: abp.appPath + 'PlaceReviews/EditModal',
            scriptUrl: abp.appPath + 'Pages/PlaceReviews/editModal.js',
            modalClass: 'placeReviewEdit',
        });

        function syncCreateUrl() {
            createModal.options.viewUrl =
                abp.appPath + 'PlaceReviews/CreateModal?placeId=' + encodeURIComponent(placeId);
        }

        $('#EmbeddedNewPlaceReviewBtn').on('click', function (e) {
            e.preventDefault();
            syncCreateUrl();
            createModal.open();
        });

        var getFilter = function () {
            return {
                filterText: '',
                placeId: placeId,
                status: '',
            };
        };

        var canEdit = abp.auth.isGranted('MasterDataService.PlaceReviews.Edit');
        var canDelete = abp.auth.isGranted('MasterDataService.PlaceReviews.Delete');

        var dataTable = $('#EmbeddedPlaceReviewsTable').DataTable(
            abp.libs.datatables.normalizeConfiguration({
                processing: true,
                serverSide: true,
                paging: true,
                pageLength: 10,
                searching: false,
                responsive: true,
                order: [[3, 'desc']],
                ajax: abp.libs.datatables.createAjax(placeReviewService.getList, getFilter),
                columnDefs: [
                    {
                        data: null,
                        orderable: false,
                        render: function (data, type, row) {
                            var html = '<div class="d-flex gap-1">';
                            var id = row.placeReview.id;

                            if (canEdit) {
                                html +=
                                    '<button type="button" class="btn btn-sm btn-outline-primary action-edit" data-id="' +
                                    id +
                                    '"><i class="fa fa-pen"></i></button>';
                            }

                            if (canDelete) {
                                html +=
                                    '<button type="button" class="btn btn-sm btn-outline-danger action-delete" data-id="' +
                                    id +
                                    '"><i class="fa fa-trash"></i></button>';
                            }

                            html += '</div>';
                            return html;
                        },
                    },
                    { data: 'placeReview.rating' },
                    {
                        data: 'placeReview.title',
                        defaultContent: '',
                        render: function (v) {
                            return v || '-';
                        },
                    },
                    {
                        data: 'placeReview.creationTime',
                        render: function (t) {
                            if (!t) {
                                return '-';
                            }
                            try {
                                return new Date(t).toLocaleString();
                            } catch (e) {
                                return t;
                            }
                        },
                    },
                    {
                        data: 'placeReview.comment',
                        defaultContent: '',
                        orderable: false,
                        render: function (v) {
                            return v ? String(v).substring(0, 120) + (String(v).length > 120 ? '…' : '') : '-';
                        },
                    },
                    {
                        data: 'placeReview.status',
                        render: function (status) {
                            if (status === undefined || status === null) {
                                return '-';
                            }
                            return getReviewStatusBadge(status);
                        },
                    },
                    {
                        data: 'placeReview.userId',
                        defaultContent: '',
                        orderable: false,
                        render: function (userId) {
                            return userId || '-';
                        },
                    },
                ],
            })
        );

        $('#EmbeddedPlaceReviewsTable').on('click', '.action-edit', function (e) {
            e.preventDefault();
            editModal.open({ id: $(this).data('id') });
        });

        $('#EmbeddedPlaceReviewsTable').on('click', '.action-delete', function (e) {
            e.preventDefault();
            var id = $(this).data('id');

            abp.message.confirm(l('DeleteConfirmationMessage')).then(function (confirmed) {
                if (!confirmed) {
                    return;
                }

                placeReviewService.delete(id).then(function () {
                    abp.notify.success(l('SuccessfullyDeleted'));
                    dataTable.ajax.reload();
                });
            });
        });

        createModal.onResult(function () {
            dataTable.ajax.reload();
        });

        editModal.onResult(function () {
            dataTable.ajax.reload();
        });
    }

    function initEmbeddedFavorites(placeId) {
        var placeFavoriteService = window.kHHub.masterDataService.services.placeFavorites.placeFavorites;

        var getFilter = function () {
            return {
                filterText: '',
                placeId: placeId,
                userId: '',
            };
        };

        var dataTable = $('#EmbeddedPlaceFavoritesTable').DataTable(
            abp.libs.datatables.normalizeConfiguration({
                processing: true,
                serverSide: true,
                paging: true,
                pageLength: 10,
                searching: false,
                responsive: true,
                order: [[1, 'desc']],
                ajax: abp.libs.datatables.createAjax(placeFavoriteService.getList, getFilter),
                columnDefs: [
                    {
                        data: null,
                        orderable: false,
                        render: function (data, type, row) {
                            var uid = row.placeFavorite.userId;
                            var label = uid ? String(uid).substring(0, 10) : '?';
                            var src =
                                'https://ui-avatars.com/api/?background=3B82F6&color=fff&size=64&rounded=true&name=' +
                                encodeURIComponent(label);
                            return (
                                '<div class="d-flex align-items-center gap-3">' +
                                '<img src="' +
                                src +
                                '" class="rounded-circle border" width="40" height="40" alt="" loading="lazy"/>' +
                                '<div><div class="fw-semibold text-monospace small">' +
                                (uid || '-') +
                                '</div></div></div>'
                            );
                        },
                    },
                    {
                        data: 'placeFavorite.creationTime',
                        render: function (t) {
                            if (!t) {
                                return '-';
                            }
                            try {
                                return new Date(t).toLocaleString();
                            } catch (e) {
                                return t;
                            }
                        },
                    },
                ],
            })
        );
    }

    $(function () {
        var $editForm = $('#PlaceEditForm[data-place-mode="edit"]');
        if (!$editForm.length) {
            return;
        }

        var placeId = ($editForm.find('input[name="Id"]').val() || '').toString();
        if (!placeId || placeId === '00000000-0000-0000-0000-000000000000') {
            return;
        }

        if ($('#EmbeddedPlaceReviewsTable').length) {
            initEmbeddedReviews(placeId);
        }

        if ($('#EmbeddedPlaceFavoritesTable').length) {
            initEmbeddedFavorites(placeId);
        }
    });
})();
