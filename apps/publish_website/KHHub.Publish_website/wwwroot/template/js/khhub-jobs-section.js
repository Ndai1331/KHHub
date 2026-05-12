/**
 * Landing jobs: forum-style list + infinite load (IntersectionObserver).
 */
(function () {
  'use strict';

  function decodeHtmlEntities(input) {
    if (!input) {
      return '';
    }
    var el = document.createElement('textarea');
    el.innerHTML = input;
    return el.value;
  }

  function formatNumber(value, locale) {
    var n = typeof value === 'number' ? value : parseInt(value, 10) || 0;
    try {
      return new Intl.NumberFormat(locale || undefined, { maximumFractionDigits: 0 }).format(n);
    } catch (e) {
      return String(n);
    }
  }

  function formatJobDate(iso, locale) {
    if (!iso) {
      return '';
    }
    var d = new Date(iso);
    if (isNaN(d.getTime())) {
      return '';
    }
    try {
      return new Intl.DateTimeFormat(locale || undefined, {
        year: 'numeric',
        month: 'short',
        day: 'numeric'
      }).format(d);
    } catch (e2) {
      return d.toLocaleDateString();
    }
  }

  function buildExcerpt(job) {
    var parts = [];
    if (job.metaLabel) {
      parts.push(job.metaLabel);
    }
    var loc = [job.province, job.ward].filter(Boolean).join(', ');
    if (loc) {
      parts.push(loc);
    }
    return parts.join(' · ');
  }

  function buildJobsFilterUrl(basePath, params) {
    var u = new URL(basePath || '/jobs', window.location.origin);
    Object.keys(params).forEach(function (k) {
      var v = params[k];
      if (v) {
        u.searchParams.set(k, v);
      }
    });
    return u.pathname + u.search;
  }

  function firstCharUpper(title) {
    var t = (title || '').trim();
    if (!t.length) {
      return '\u2022';
    }
    return t.charAt(0).toUpperCase();
  }

  function buildJobArticle(job, viewsLabel, uiLang, childIndex, jobsListPath) {
    var rowTone = childIndex % 2 === 0 ? 'even' : 'odd';
    var article = document.createElement('article');
    article.className = 'khhub-job-forum-row khhub-job-forum-row--' + rowTone;
    article.setAttribute('role', 'listitem');

    var row = document.createElement('div');
    row.className = 'khhub-job-forum-row__link';

    var base = jobsListPath || '/jobs';

    var avatar = document.createElement('a');
    avatar.className = 'khhub-job-forum-avatar';
    avatar.href = job.url || '#';
    avatar.setAttribute('aria-label', job.title || '');
    var glyph = document.createElement('span');
    glyph.className = 'khhub-job-forum-avatar__glyph';
    glyph.setAttribute('aria-hidden', 'true');
    glyph.textContent = firstCharUpper(job.title);
    avatar.appendChild(glyph);

    var main = document.createElement('div');
    main.className = 'khhub-job-forum-main';

    var badges = document.createElement('div');
    badges.className = 'khhub-job-forum-badges';

    if (job.category) {
      var catA = document.createElement('a');
      catA.className = 'khhub-job-forum-badge';
      catA.href = buildJobsFilterUrl(base, { Category: job.category });
      catA.setAttribute('rel', 'tag');
      if (job.categoryIcon) {
        var ic = document.createElement('i');
        ic.className = job.categoryIcon;
        ic.setAttribute('aria-hidden', 'true');
        ic.style.marginRight = '4px';
        if (job.categoryIconColor) {
          ic.style.color = job.categoryIconColor;
        }
        catA.appendChild(ic);
      }
      catA.appendChild(document.createTextNode(job.category));
      badges.appendChild(catA);
    }

    if (job.ward) {
      var wParams = { Ward: job.ward };
      if (job.province) {
        wParams.Province = job.province;
      }
      var wardA = document.createElement('a');
      wardA.className = 'khhub-job-forum-badge khhub-job-forum-badge--ward';
      wardA.href = buildJobsFilterUrl(base, wParams);
      wardA.textContent = job.ward;
      var wHex = job.wardBadgeColorHex;
      if (wHex && wHex.length === 7 && wHex.charAt(0) === '#') {
        wardA.style.color = '#fff';
        wardA.style.borderColor = wHex;
        wardA.style.backgroundColor = wHex;
      }
      badges.appendChild(wardA);
    }

    if (badges.childNodes.length > 0) {
      main.appendChild(badges);
    }

    var title = document.createElement('h3');
    title.className = 'khhub-job-forum-title';
    var titleA = document.createElement('a');
    titleA.className = 'khhub-job-forum-title__a';
    titleA.href = job.url || '#';
    titleA.textContent = job.title || '';
    title.appendChild(titleA);

    main.appendChild(title);

    var excerptText = buildExcerpt(job);
    if (excerptText) {
      var excerpt = document.createElement('p');
      excerpt.className = 'khhub-job-forum-excerpt';
      excerpt.textContent = excerptText;
      main.appendChild(excerpt);
    }

    var side = document.createElement('div');
    side.className = 'khhub-job-forum-side';

    var stats = document.createElement('div');
    stats.className = 'khhub-job-forum-stats';
    stats.setAttribute('aria-label', viewsLabel || '');
    var statsLbl = document.createElement('span');
    statsLbl.className = 'khhub-job-forum-stats__label';
    statsLbl.textContent = viewsLabel || '';
    var statsVal = document.createElement('strong');
    statsVal.className = 'khhub-job-forum-stats__value';
    statsVal.textContent = formatNumber(job.popularity, uiLang);
    stats.appendChild(statsLbl);
    stats.appendChild(statsVal);

    var activity = document.createElement('div');
    activity.className = 'khhub-job-forum-activity';
    var timeEl = document.createElement('time');
    timeEl.className = 'khhub-job-forum-activity__time';
    if (job.createdAt) {
      timeEl.setAttribute('datetime', job.createdAt);
    }
    timeEl.textContent = formatJobDate(job.createdAt, uiLang);

    activity.appendChild(timeEl);

    side.appendChild(stats);
    side.appendChild(activity);

    row.appendChild(avatar);
    row.appendChild(main);
    row.appendChild(side);
    article.appendChild(row);
    return article;
  }

  function init() {
    var host = document.getElementById('khhub-jobs-scroll-host');
    if (!host || !host.dataset.feedUrl) {
      return;
    }

    var listEl = document.getElementById('khhub-jobs-list');
    var loadingEl = document.getElementById('khhub-jobs-loading');
    var endEl = document.getElementById('khhub-jobs-end');
    var sentinel = document.getElementById('khhub-jobs-sentinel');
    if (!listEl || !loadingEl || !sentinel) {
      return;
    }

    var feedUrl = host.dataset.feedUrl;
    var take = parseInt(host.dataset.take || '10', 10) || 10;
    var viewsLabel = decodeHtmlEntities(host.dataset.viewsLabel || '');
    var jobsListPath = host.dataset.jobsListPath || '/jobs';
    var uiLang = host.dataset.uiLang || '';
    var errorText = decodeHtmlEntities(host.dataset.errorText || 'Could not load more jobs.');
    var skip = parseInt(host.dataset.initialSkip || '0', 10) || 0;
    var total = parseInt(host.dataset.totalCount || '0', 10);
    if (!Number.isFinite(total) || total < 0) {
      total = 0;
    }

    var loading = false;
    var exhausted = skip >= total || total === 0;
    var lastLoadSucceeded = false;

    function setLoading(on) {
      loading = on;
      loadingEl.hidden = !on;
      host.classList.toggle('khhub-job-forum-board--loading', on);
    }

    function appendJobs(items) {
      for (var i = 0; i < items.length; i++) {
        listEl.appendChild(
          buildJobArticle(items[i], viewsLabel, uiLang, listEl.children.length, jobsListPath)
        );
      }
    }

    function loadMore() {
      if (loading || exhausted) {
        return;
      }
      if (endEl) {
        endEl.hidden = true;
      }
      lastLoadSucceeded = false;
      setLoading(true);
      var url =
        feedUrl +
        (feedUrl.indexOf('?') >= 0 ? '&' : '?') +
        'skip=' +
        encodeURIComponent(String(skip)) +
        '&take=' +
        encodeURIComponent(String(take));

      fetch(url, { credentials: 'same-origin', headers: { Accept: 'application/json' } })
        .then(function (res) {
          if (!res.ok) {
            throw new Error('http');
          }
          return res.json();
        })
        .then(function (data) {
          lastLoadSucceeded = true;
          var items = data.items || [];
          var totalCount =
            typeof data.totalCount === 'number' ? data.totalCount : parseInt(data.totalCount, 10) || 0;
          appendJobs(items);
          skip += items.length;
          total = totalCount;
          if (skip >= total || items.length === 0) {
            exhausted = true;
            sentinel.hidden = true;
            if (observer) {
              observer.disconnect();
            }
          }
        })
        .catch(function () {
          window.alert(errorText);
          exhausted = true;
          sentinel.hidden = true;
          if (observer) {
            observer.disconnect();
          }
        })
        .finally(function () {
          setLoading(false);
          if (exhausted && lastLoadSucceeded && endEl) {
            endEl.hidden = false;
          }
        });
    }

    var observer = new IntersectionObserver(
      function (entries) {
        for (var e = 0; e < entries.length; e++) {
          if (entries[e].isIntersecting) {
            loadMore();
            break;
          }
        }
      },
      { root: host, rootMargin: '0px 0px 120px 0px', threshold: 0 }
    );

    if (!exhausted) {
      observer.observe(sentinel);
    } else {
      sentinel.hidden = true;
    }
  }

  if (document.readyState === 'loading') {
    document.addEventListener('DOMContentLoaded', init);
  } else {
    init();
  }
})();
