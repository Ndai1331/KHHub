(function () {
    var favoriteKey = 'khhub-public-favorites';
    var favorites = new Set(JSON.parse(localStorage.getItem(favoriteKey) || '[]'));

    // Client-side view toggle (grid/list) without reloading or hitting the controller.
    var viewKey = 'khhub-public-view';
    var validViews = { grid: 'kh-grid', list: 'kh-list' };

    var applyView = function (view) {
        if (!validViews[view]) {
            return;
        }

        document.querySelectorAll('[data-cards-container]').forEach(function (container) {
            Object.keys(validViews).forEach(function (key) {
                container.classList.remove(validViews[key]);
            });
            container.classList.add(validViews[view]);
            container.setAttribute('data-current-view', view);
        });

        document.querySelectorAll('[data-view-toggle]').forEach(function (button) {
            var active = button.getAttribute('data-view-toggle') === view;
            button.classList.toggle('is-active', active);
            button.setAttribute('aria-pressed', active ? 'true' : 'false');
        });
    };

    var savedView = localStorage.getItem(viewKey);
    var toolbar = document.querySelector('[data-view-toolbar]');
    var initialView = (toolbar && toolbar.getAttribute('data-initial-view')) || 'list';
    applyView(validViews[savedView] ? savedView : initialView);

    document.querySelectorAll('[data-view-toggle]').forEach(function (button) {
        button.addEventListener('click', function () {
            var view = button.getAttribute('data-view-toggle');
            if (!validViews[view]) {
                return;
            }

            localStorage.setItem(viewKey, view);
            applyView(view);
        });
    });

    document.querySelectorAll('.kh-card__media img').forEach(function (image) {
        var markLoaded = function () {
            image.closest('.kh-card__media')?.classList.add('is-loaded');
        };

        if (image.complete) {
            markLoaded();
        } else {
            image.addEventListener('load', markLoaded, { once: true });
        }
    });

    document.querySelectorAll('[data-favorite]').forEach(function (button) {
        var id = button.getAttribute('data-favorite');
        if (!id) {
            return;
        }

        var sync = function () {
            var active = favorites.has(id);
            button.classList.toggle('is-active', active);
            button.setAttribute('aria-pressed', active ? 'true' : 'false');
            button.innerHTML = active
                ? '<i class="fa fa-heart" aria-hidden="true"></i> Đã lưu'
                : '<i class="fa fa-heart-o" aria-hidden="true"></i> Lưu';
        };

        button.addEventListener('click', function () {
            if (favorites.has(id)) {
                favorites.delete(id);
            } else {
                favorites.add(id);
            }

            localStorage.setItem(favoriteKey, JSON.stringify(Array.from(favorites)));
            sync();
        });

        sync();
    });

    document.querySelectorAll('.kh-comment-form button').forEach(function (button) {
        button.addEventListener('click', function () {
            var form = button.closest('.kh-comment-form');
            var textarea = form?.querySelector('textarea');
            if (textarea) {
                textarea.value = '';
            }

            button.textContent = 'Đã ghi nhận';
            window.setTimeout(function () {
                button.textContent = 'Gửi bình luận';
            }, 1600);
        });
    });

    // Lightweight image lightbox with prev/next, counter and keyboard support.
    var lightbox = (function () {
        var root = null;
        var imageEl = null;
        var counterEl = null;
        var captionEl = null;
        var prevBtn = null;
        var nextBtn = null;
        var images = [];
        var caption = '';
        var index = 0;

        var ensureRoot = function () {
            if (root) {
                return;
            }

            root = document.createElement('div');
            root.className = 'kh-lightbox';
            root.setAttribute('aria-hidden', 'true');
            root.innerHTML = ''
                + '<div class="kh-lightbox__backdrop" data-lightbox-close></div>'
                + '<button type="button" class="kh-lightbox__close" data-lightbox-close aria-label="Đóng">'
                + '<i class="fa fa-times" aria-hidden="true"></i>'
                + '</button>'
                + '<button type="button" class="kh-lightbox__nav kh-lightbox__nav--prev" data-lightbox-prev aria-label="Ảnh trước">'
                + '<i class="fa fa-angle-left" aria-hidden="true"></i>'
                + '</button>'
                + '<figure class="kh-lightbox__stage">'
                + '<img class="kh-lightbox__image" alt="">'
                + '<figcaption class="kh-lightbox__caption"></figcaption>'
                + '</figure>'
                + '<button type="button" class="kh-lightbox__nav kh-lightbox__nav--next" data-lightbox-next aria-label="Ảnh kế tiếp">'
                + '<i class="fa fa-angle-right" aria-hidden="true"></i>'
                + '</button>'
                + '<div class="kh-lightbox__counter" data-lightbox-counter></div>';

            document.body.appendChild(root);

            imageEl = root.querySelector('.kh-lightbox__image');
            counterEl = root.querySelector('[data-lightbox-counter]');
            captionEl = root.querySelector('.kh-lightbox__caption');
            prevBtn = root.querySelector('[data-lightbox-prev]');
            nextBtn = root.querySelector('[data-lightbox-next]');

            root.querySelectorAll('[data-lightbox-close]').forEach(function (el) {
                el.addEventListener('click', close);
            });
            prevBtn.addEventListener('click', function () { go(-1); });
            nextBtn.addEventListener('click', function () { go(1); });

            document.addEventListener('keydown', function (event) {
                if (!root.classList.contains('is-open')) {
                    return;
                }

                if (event.key === 'Escape') {
                    close();
                } else if (event.key === 'ArrowLeft') {
                    go(-1);
                } else if (event.key === 'ArrowRight') {
                    go(1);
                }
            });
        };

        var render = function () {
            if (!images.length) {
                return;
            }

            var src = images[index];
            imageEl.setAttribute('src', src);
            imageEl.setAttribute('alt', caption ? caption + ' (' + (index + 1) + '/' + images.length + ')' : '');
            counterEl.textContent = (index + 1) + ' / ' + images.length;
            captionEl.textContent = caption || '';
            captionEl.style.display = caption ? '' : 'none';

            var multiple = images.length > 1;
            prevBtn.style.display = multiple ? '' : 'none';
            nextBtn.style.display = multiple ? '' : 'none';
        };

        var go = function (delta) {
            if (!images.length) {
                return;
            }

            index = (index + delta + images.length) % images.length;
            render();
        };

        var open = function (sources, startIndex, captionText) {
            if (!sources || !sources.length) {
                return;
            }

            ensureRoot();
            images = sources;
            caption = captionText || '';
            index = Math.max(0, Math.min(startIndex || 0, images.length - 1));
            render();
            root.classList.add('is-open');
            root.setAttribute('aria-hidden', 'false');
            document.body.classList.add('kh-lightbox-open');
        };

        var close = function () {
            if (!root) {
                return;
            }

            root.classList.remove('is-open');
            root.setAttribute('aria-hidden', 'true');
            document.body.classList.remove('kh-lightbox-open');
        };

        return { open: open, close: close };
    })();

    document.querySelectorAll('.kh-gallery').forEach(function (gallery) {
        var cover = gallery.querySelector('[data-gallery-cover]');
        var coverImage = gallery.querySelector('.kh-gallery__cover-img');
        var thumbs = Array.prototype.slice.call(gallery.querySelectorAll('.kh-gallery__thumb'));
        var track = gallery.querySelector('[data-gallery-track]');
        var prevBtn = gallery.querySelector('[data-gallery-prev]');
        var nextBtn = gallery.querySelector('[data-gallery-next]');
        var seeAllBtn = gallery.querySelector('[data-gallery-see-all]');

        if (!cover || !coverImage || thumbs.length === 0) {
            return;
        }

        var sources = thumbs.map(function (thumb) { return thumb.getAttribute('data-gallery-thumb'); });
        var captionText = coverImage.getAttribute('alt') || '';
        var activeIndex = 0;

        var setActive = function (newIndex) {
            if (newIndex < 0 || newIndex >= thumbs.length) {
                return;
            }

            activeIndex = newIndex;
            var src = sources[newIndex];
            if (src && coverImage.getAttribute('src') !== src) {
                coverImage.style.opacity = '0';
                window.requestAnimationFrame(function () {
                    coverImage.setAttribute('src', src);
                    coverImage.style.opacity = '1';
                });
            }

            thumbs.forEach(function (item, idx) {
                item.classList.toggle('is-active', idx === newIndex);
            });
        };

        thumbs.forEach(function (thumb, idx) {
            thumb.addEventListener('click', function () {
                setActive(idx);
            });
        });

        cover.addEventListener('click', function () {
            lightbox.open(sources, activeIndex, captionText);
        });

        if (seeAllBtn) {
            seeAllBtn.addEventListener('click', function () {
                lightbox.open(sources, activeIndex, captionText);
            });
        }

        var scrollByThumb = function (direction) {
            if (!track) {
                return;
            }

            var firstThumb = thumbs[0];
            var step = firstThumb ? firstThumb.offsetHeight + 12 : 120;
            track.scrollBy({ top: direction * step, behavior: 'smooth' });
        };

        if (prevBtn) {
            prevBtn.addEventListener('click', function () { scrollByThumb(-1); });
        }

        if (nextBtn) {
            nextBtn.addEventListener('click', function () { scrollByThumb(1); });
        }

        var refreshNav = function () {
            if (!track || !prevBtn || !nextBtn) {
                return;
            }

            var atTop = track.scrollTop <= 4;
            var atBottom = track.scrollTop + track.clientHeight >= track.scrollHeight - 4;
            prevBtn.disabled = atTop;
            nextBtn.disabled = atBottom;
        };

        if (track) {
            track.addEventListener('scroll', refreshNav);
            window.addEventListener('resize', refreshNav);
            refreshNav();
        }
    });
})();
