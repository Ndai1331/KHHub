(function () {
    var MD = 'MasterDataService';

    function l(key) {
        return abp.localization.getResource(MD)(key);
    }

    function slugifyTagName(name) {
        var s = (name || '').trim().toLowerCase().replace(/\s+/g, '-');
        s = s.replace(/[^a-z0-9\u00C0-\u024F\u1E00-\u1EFF\-]/gi, '');
        s = s.replace(/-+/g, '-').replace(/^-|-$/g, '');
        return s || 'tag-' + Date.now();
    }

    function pick(fnBase, names) {
        for (var i = 0; i < names.length; i++) {
            var n = names[i];
            if (typeof fnBase[n] === 'function') {
                return fnBase[n].bind(fnBase);
            }
        }
        return null;
    }

    function getServices(kind) {
        if (kind === 'article') {
            return {
                tagSvc: window.kHHub.masterDataService.services.articleTags.articleTags,
                mappingSvc: window.kHHub.masterDataService.services.articleTagMappings.articleTagMappings,
                lookup: function (input) {
                    var fn =
                        pick(window.kHHub.masterDataService.services.articleTagMappings.articleTagMappings, [
                            'getArticleTagLookupAsync',
                            'getArticleTagLookup',
                        ]) || function () {};
                    return fn(input);
                },
                listMappings: function (input) {
                    return window.kHHub.masterDataService.services.articleTagMappings.articleTagMappings.getList(input);
                },
                deleteMapping: function (id) {
                    return window.kHHub.masterDataService.services.articleTagMappings.articleTagMappings.delete(id);
                },
                createMapping: function (dto) {
                    return window.kHHub.masterDataService.services.articleTagMappings.articleTagMappings.create(dto);
                },
                entityKey: 'articleId',
                mappingTagIdKey: 'articleTagId',
                mappingSortKey: 'order',
                rowMapping: 'articleTagMapping',
                rowTag: 'articleTag',
            };
        }
        return {
            tagSvc: window.kHHub.masterDataService.services.placeTags.placeTags,
            mappingSvc: window.kHHub.masterDataService.services.placeTagMappings.placeTagMappings,
            lookup: function (input) {
                var fn =
                    pick(window.kHHub.masterDataService.services.placeTagMappings.placeTagMappings, [
                        'getPlaceTagLookupAsync',
                        'getPlaceTagLookup',
                    ]) || function () {};
                return fn(input);
            },
            listMappings: function (input) {
                return window.kHHub.masterDataService.services.placeTagMappings.placeTagMappings.getList(input);
            },
            deleteMapping: function (id) {
                return window.kHHub.masterDataService.services.placeTagMappings.placeTagMappings.delete(id);
            },
            createMapping: function (dto) {
                return window.kHHub.masterDataService.services.placeTagMappings.placeTagMappings.create(dto);
            },
            entityKey: 'placeId',
            mappingTagIdKey: 'placeTagId',
            mappingSortKey: 'sortOrder',
            rowMapping: 'placeTagMapping',
            rowTag: 'placeTag',
        };
    }

    function readState($root) {
        var raw = $root.find('.khhub-tag-picker-state').val() || '[]';
        try {
            var parsed = JSON.parse(raw);
            return Array.isArray(parsed) ? parsed : [];
        } catch (e) {
            return [];
        }
    }

    function writeState($root, items) {
        $root.find('.khhub-tag-picker-state').val(JSON.stringify(items));
    }

    function renderChips($root, items) {
        var $chips = $root.find('.khhub-tag-picker-chips');
        $chips.empty();
        items.forEach(function (item) {
            var $chip = $('<span class="khhub-tag-picker-chip"/>').text(item.displayName);
            var $rm = $(
                '<button type="button" class="khhub-tag-picker-chip-remove" aria-label="remove"><i class="fa fa-times"></i></button>'
            );
            $rm.on('click', function () {
                var next = readState($root).filter(function (x) {
                    return x.id !== item.id;
                });
                writeState($root, next);
                renderChips($root, next);
            });
            $chip.append($rm);
            $chips.append($chip);
        });
    }

    function addTag($root, svc, id, displayName) {
        var items = readState($root);
        if (items.some(function (x) { return x.id === id; })) {
            abp.notify.warn(l('ItemAlreadyAdded'));
            return;
        }
        items.push({ id: id, displayName: displayName });
        writeState($root, items);
        renderChips($root, items);
    }

    function whenAll(promises) {
        if (!promises.length) {
            var d = $.Deferred();
            d.resolve();
            return d.promise();
        }
        return $.when.apply($, promises);
    }

    /**
     * Returns ordered tag ids from picker inside form scope (or document).
     */
    function readSelectedIds($form, kind) {
        var $picker = $form.find('.khhub-tag-picker[data-kind="' + kind + '"]');
        if (!$picker.length) {
            return [];
        }
        return readState($picker).map(function (x) {
            return x.id;
        });
    }

    function syncMappings(entityId, desiredIds, svc) {
        var q = {};
        q[svc.entityKey] = entityId;
        q.maxResultCount = 500;
        q.skipCount = 0;

        var outer = $.Deferred();

        svc
            .listMappings(q)
            .done(function (page) {
                var rows = page.items || [];
                var existingTagToMappingId = {};
                rows.forEach(function (row) {
                    var m = row[svc.rowMapping];
                    var tag = row[svc.rowTag];
                    var tid = tag && tag.id ? tag.id : m[svc.mappingTagIdKey];
                    existingTagToMappingId[tid] = m.id;
                });

                var desiredSet = {};
                desiredIds.forEach(function (id) {
                    desiredSet[id] = true;
                });

                var deletePromises = [];
                Object.keys(existingTagToMappingId).forEach(function (tid) {
                    if (!desiredSet[tid]) {
                        deletePromises.push(svc.deleteMapping(existingTagToMappingId[tid]));
                    }
                });

                whenAll(deletePromises)
                    .done(function () {
                        var createPromises = [];
                        desiredIds.forEach(function (tid, index) {
                            if (!existingTagToMappingId[tid]) {
                                var dto = { isPrimary: index === 0 };
                                dto[svc.mappingSortKey] = index + 1;
                                dto[svc.mappingTagIdKey] = tid;
                                dto[svc.entityKey] = entityId;
                                createPromises.push(svc.createMapping(dto));
                            }
                        });
                        whenAll(createPromises)
                            .done(function () {
                                outer.resolve();
                            })
                            .fail(function (xhr) {
                                outer.reject(xhr);
                            });
                    })
                    .fail(function (xhr) {
                        outer.reject(xhr);
                    });
            })
            .fail(function (xhr) {
                outer.reject(xhr);
            });

        return outer.promise();
    }

    function loadExisting($root, svc, entityId) {
        var q = {};
        q[svc.entityKey] = entityId;
        q.maxResultCount = 500;
        q.skipCount = 0;

        svc.listMappings(q).done(function (page) {
            var rows = page.items || [];
            var items = [];
            rows.forEach(function (row) {
                var m = row[svc.rowMapping];
                var tag = row[svc.rowTag];
                var tid = tag && tag.id ? tag.id : m[svc.mappingTagIdKey];
                var label =
                    tag && tag.name
                        ? tag.name
                        : tag && tag.slug
                          ? tag.slug
                          : tid;
                items.push({ id: tid, displayName: label });
            });
            writeState($root, items);
            renderChips($root, items);
        });
    }

    function bindPicker($root) {
        var kind = ($root.data('kind') || '').toString();
        if (kind !== 'article' && kind !== 'place') {
            return;
        }

        var svc = getServices(kind);
        var permLookup =
            kind === 'article'
                ? abp.auth.isGranted('MasterDataService.ArticleTagMappings')
                : abp.auth.isGranted('MasterDataService.PlaceTagMappings');
        var permTagCreate =
            kind === 'article'
                ? abp.auth.isGranted('MasterDataService.ArticleTags.Create')
                : abp.auth.isGranted('MasterDataService.PlaceTags.Create');

        if (!permLookup && !permTagCreate) {
            $root.addClass('khhub-tag-picker-disabled');
            $root.find('.khhub-tag-picker-search').attr('placeholder', l('TagPickerNoPermission'));
            return;
        }

        var $input = $root.find('.khhub-tag-picker-search');
        var $menu = $root.find('.khhub-tag-picker-menu');
        var suggestions = [];
        var activeIdx = -1;
        var debounceTimer;

        function hideMenu() {
            $menu.addClass('d-none').empty();
            suggestions = [];
            activeIdx = -1;
        }

        function renderMenu() {
            $menu.empty();
            if (!suggestions.length) {
                $menu.addClass('d-none');
                return;
            }
            suggestions.forEach(function (item, idx) {
                var $b = $('<button type="button" class="khhub-tag-picker-item"/>').text(item.displayName);
                $b.attr('data-idx', idx);
                $b.on('mousedown', function (e) {
                    e.preventDefault();
                    addTag($root, svc, item.id, item.displayName);
                    $input.val('');
                    hideMenu();
                });
                $menu.append($b);
            });
            $menu.removeClass('d-none');
        }

        function setActive(idx) {
            activeIdx = idx;
            $menu.find('.khhub-tag-picker-item').removeClass('active');
            var $active = $menu.find('.khhub-tag-picker-item[data-idx="' + idx + '"]');
            $active.addClass('active');
        }

        function runLookup(filterText) {
            if (!permLookup) {
                suggestions = [];
                renderMenu();
                return;
            }
            svc
                .lookup({ filter: filterText || '', skipCount: 0, maxResultCount: 20 })
                .done(function (page) {
                    var items = page.items || [];
                    suggestions = items.map(function (x) {
                        return { id: x.id, displayName: x.displayName };
                    });
                    renderMenu();
                    if (suggestions.length) {
                        setActive(0);
                    }
                })
                .fail(function () {
                    suggestions = [];
                    hideMenu();
                });
        }

        $input.on('input', function () {
            clearTimeout(debounceTimer);
            var v = $input.val().toString();
            debounceTimer = setTimeout(function () {
                runLookup(v);
            }, 250);
        });

        $input.on('keydown', function (e) {
            if (e.key === 'ArrowDown') {
                if (!suggestions.length) {
                    return;
                }
                e.preventDefault();
                var n = activeIdx + 1;
                if (n >= suggestions.length) {
                    n = 0;
                }
                setActive(n);
            } else if (e.key === 'ArrowUp') {
                if (!suggestions.length) {
                    return;
                }
                e.preventDefault();
                var p = activeIdx - 1;
                if (p < 0) {
                    p = suggestions.length - 1;
                }
                setActive(p);
            } else if (e.key === 'Enter') {
                e.preventDefault();
                var term = $input.val().toString().trim();
                if (suggestions.length && activeIdx >= 0 && suggestions[activeIdx]) {
                    var sel = suggestions[activeIdx];
                    addTag($root, svc, sel.id, sel.displayName);
                    $input.val('');
                    hideMenu();
                    return;
                }
                var match = suggestions.filter(function (x) {
                    return x.displayName.toLowerCase() === term.toLowerCase();
                })[0];
                if (match) {
                    addTag($root, svc, match.id, match.displayName);
                    $input.val('');
                    hideMenu();
                    return;
                }
                if (!permTagCreate || !term) {
                    return;
                }
                abp.ui.setBusy($root);
                svc.tagSvc
                    .create({
                        name: term,
                        slug: slugifyTagName(term),
                        usageCount: 0,
                        description: null,
                    })
                    .done(function (created) {
                        addTag($root, svc, created.id, created.name || term);
                        $input.val('');
                        hideMenu();
                        abp.notify.success(l('TagPickerTagCreated'));
                    })
                    .fail(function (xhr) {
                        var msg =
                            xhr &&
                            xhr.responseJSON &&
                            xhr.responseJSON.error &&
                            xhr.responseJSON.error.message
                                ? xhr.responseJSON.error.message
                                : l('AnErrorOccurred');
                        abp.notify.error(msg);
                    })
                    .always(function () {
                        abp.ui.clearBusy($root);
                    });
            } else if (e.key === 'Escape') {
                hideMenu();
            } else if (e.key === 'Backspace' && !$input.val()) {
                var st = readState($root);
                if (st.length) {
                    st.pop();
                    writeState($root, st);
                    renderChips($root, st);
                }
            }
        });

        $input.on('blur', function () {
            setTimeout(function () {
                hideMenu();
            }, 180);
        });

        var entityId = ($root.data('entity-id') || '').toString();
        if (entityId && entityId !== '00000000-0000-0000-0000-000000000000') {
            loadExisting($root, svc, entityId);
        } else {
            renderChips($root, readState($root));
        }
    }

    $(function () {
        $('.khhub-tag-picker').each(function () {
            bindPicker($(this));
        });
    });

    window.kHHubTagPicker = {
        readSelectedIds: readSelectedIds,
        syncArticleTags: function (articleId, $form) {
            var ids = readSelectedIds($form, 'article');
            return syncMappings(articleId, ids, getServices('article'));
        },
        syncPlaceTags: function (placeId, $form) {
            var ids = readSelectedIds($form, 'place');
            return syncMappings(placeId, ids, getServices('place'));
        },
    };
})();
