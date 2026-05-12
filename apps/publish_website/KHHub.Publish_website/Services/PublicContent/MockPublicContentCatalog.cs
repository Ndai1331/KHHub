namespace KHHub.Publish_website.Services.PublicContent;

public sealed class MockPublicContentCatalog : IPublicContentCatalog
{
    private static readonly DateTimeOffset SeedDate = new(2026, 5, 1, 8, 0, 0, TimeSpan.FromHours(7));

    private static readonly IReadOnlyDictionary<string, string> EnglishSlugs = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
    {
        ["khanh-hoa-ra-mat-ban-do-du-lich-so"] = "khanh-hoa-launches-digital-tourism-map",
        ["le-hoi-bien-2026-cong-bo-chuoi-hoat-dong-moi"] = "sea-festival-2026-announces-new-activities",
        ["cam-ranh-tang-ket-noi-viec-lam-nganh-dich-vu"] = "cam-ranh-expands-service-job-connections",
        ["dien-khanh-phat-trien-tuyen-tham-quan-di-san"] = "dien-khanh-develops-heritage-tour-route",
        ["hon-tre-thu-hut-cac-tour-trai-nghiem-xanh"] = "hon-tre-attracts-green-experience-tours",
        ["ninh-hoa-mo-rong-khong-gian-am-thuc-dia-phuong"] = "ninh-hoa-expands-local-food-space",
        ["van-ninh-hoan-thien-san-pham-du-lich-cong-dong"] = "van-ninh-improves-community-tourism-products",
        ["khanh-son-quang-ba-nong-san-gan-voi-du-lich"] = "khanh-son-promotes-farm-products-with-tourism",
        ["khanh-vinh-phat-trien-tour-suoi-va-rung"] = "khanh-vinh-develops-stream-and-forest-tours",
        ["startup-dia-phuong-ung-dung-ai-cho-huong-dan-du-lich"] = "local-startup-applies-ai-to-travel-guides",
        ["cho-dem-moi-tao-suc-hut-cho-trung-tam-nha-trang"] = "new-night-market-attracts-central-nha-trang",
        ["cong-dong-freelancer-khanh-hoa-to-chuc-meetup-thang-5"] = "khanh-hoa-freelancer-community-hosts-may-meetup",
        ["frontend-developer-razor-pages"] = "frontend-developer-razor-pages",
        ["nhan-vien-dieu-phoi-tour"] = "tour-coordinator",
        ["chuyen-vien-marketing-dia-diem"] = "destination-marketing-specialist",
        ["quan-ly-van-hanh-homestay"] = "homestay-operations-manager",
        ["thuc-tap-sinh-du-lieu"] = "data-intern",
        ["huong-dan-vien-trekking"] = "trekking-guide",
        ["dau-bep-mon-dia-phuong"] = "local-cuisine-chef",
        ["nhan-vien-cham-soc-khach-hang"] = "customer-support-specialist",
        ["designer-thuong-hieu-du-lich"] = "travel-brand-designer",
        ["nhan-vien-ban-hang-dac-san"] = "local-specialty-salesperson",
        ["seo-content-writer"] = "seo-content-writer",
        ["ky-thuat-vien-media"] = "media-technician",
        ["thap-ba-ponagar"] = "ponagar-tower",
        ["vien-hai-duong-hoc"] = "institute-of-oceanography",
        ["bai-dai-cam-ranh"] = "bai-dai-cam-ranh",
        ["dam-nha-phu"] = "nha-phu-lagoon",
        ["thanh-co-dien-khanh"] = "dien-khanh-citadel",
        ["mui-doi-hon-dau"] = "mui-doi-hon-dau",
        ["suoi-ba-ho"] = "ba-ho-waterfall",
        ["khu-du-lich-yang-bay"] = "yang-bay-tourist-area",
        ["hon-mun"] = "hon-mun-island",
        ["doc-let"] = "doc-let-beach",
        ["khanh-son-orchard"] = "khanh-son-orchard",
        ["chua-long-son"] = "long-son-pagoda"
    };

    private static readonly IReadOnlyList<NewsItem> News =
    [
        CreateNews("Khanh Hoa ra mat ban do du lich so", "khanh-hoa-ra-mat-ban-do-du-lich-so", "Du khach co the tim dia diem, su kien va dich vu cong dong tren mot ban do thong minh.", "Chuyen doi so du lich", "Nha Trang", "Loc Tho", "Du lich", 980, ["du-lich-so", "ban-do", "nha-trang"]),
        CreateNews("Le hoi bien 2026 cong bo chuoi hoat dong moi", "le-hoi-bien-2026-cong-bo-chuoi-hoat-dong-moi", "Chuoi am nhac, am thuc va the thao bien duoc thiet ke cho ca nguoi dan lan du khach.", "Su kien", "Nha Trang", "Tan Tien", "Su kien", 1260, ["le-hoi-bien", "am-thuc", "giai-tri"]),
        CreateNews("Cam Ranh tang ket noi viec lam nganh dich vu", "cam-ranh-tang-ket-noi-viec-lam-nganh-dich-vu", "Nhieu doanh nghiep luu tru va logistics cong bo nhu cau tuyen dung mua cao diem.", "Viec lam", "Khanh Hoa", "Cam Nghia", "Kinh te", 730, ["viec-lam", "logistics", "dich-vu"]),
        CreateNews("Dien Khanh phat trien tuyen tham quan di san", "dien-khanh-phat-trien-tuyen-tham-quan-di-san", "Cac diem den lich su duoc lien ket thanh hanh trinh nua ngay cho nhom gia dinh.", "Van hoa", "Khanh Hoa", "Dien Khanh", "Van hoa", 660, ["di-san", "gia-dinh", "lich-su"]),
        CreateNews("Hon Tre thu hut cac tour trai nghiem xanh", "hon-tre-thu-hut-cac-tour-trai-nghiem-xanh", "Nhieu don vi khai thac san pham trekking nhe, ngam bien va giao duc moi truong.", "Du lich xanh", "Nha Trang", "Vinh Nguyen", "Du lich", 910, ["ecotourism", "bien", "trai-nghiem"]),
        CreateNews("Ninh Hoa mo rong khong gian am thuc dia phuong", "ninh-hoa-mo-rong-khong-gian-am-thuc-dia-phuong", "Nem Ninh Hoa va hai san dam Nha Phu tro thanh diem nhan trong lich trinh cuoi tuan.", "Am thuc", "Khanh Hoa", "Ninh Hoa", "Am thuc", 540, ["am-thuc", "dia-phuong", "cuoi-tuan"]),
        CreateNews("Van Ninh hoan thien san pham du lich cong dong", "van-ninh-hoan-thien-san-pham-du-lich-cong-dong", "Nguoi dan dia phuong tham gia van hanh homestay, tour lang chai va trai nghiem van hoa.", "Cong dong", "Khanh Hoa", "Van Ninh", "Du lich", 820, ["cong-dong", "lang-chai", "homestay"]),
        CreateNews("Khanh Son quang ba nong san gan voi du lich", "khanh-son-quang-ba-nong-san-gan-voi-du-lich", "Sau rieng, ca phe va trai cay ban dia duoc dua vao hanh trinh trai nghiem mien nui.", "Nong nghiep", "Khanh Hoa", "Khanh Son", "Kinh te", 610, ["nong-san", "mien-nui", "trai-nghiem"]),
        CreateNews("Khanh Vinh phat trien tour suoi va rung", "khanh-vinh-phat-trien-tour-suoi-va-rung", "Cac tuyen tham quan thien nhien duoc khuyen khich theo huong nhom nho va co huong dan vien.", "Thien nhien", "Khanh Hoa", "Khanh Vinh", "Du lich", 790, ["rung", "suoi", "eco"]),
        CreateNews("Startup dia phuong ung dung AI cho huong dan du lich", "startup-dia-phuong-ung-dung-ai-cho-huong-dan-du-lich", "Nen tang moi de xuat lich trinh theo thoi gian, ngan sach va so thich cua nguoi dung.", "Cong nghe", "Nha Trang", "Phuoc Hai", "Cong nghe", 1180, ["ai", "startup", "lich-trinh"]),
        CreateNews("Cho dem moi tao suc hut cho trung tam Nha Trang", "cho-dem-moi-tao-suc-hut-cho-trung-tam-nha-trang", "Khong gian mua sam va am thuc ve dem duoc thiet ke than thien voi gia dinh.", "Doi song", "Nha Trang", "Loc Tho", "Am thuc", 690, ["cho-dem", "mua-sam", "gia-dinh"]),
        CreateNews("Cong dong freelancer Khanh Hoa to chuc meetup thang 5", "cong-dong-freelancer-khanh-hoa-to-chuc-meetup-thang-5", "Su kien ket noi lap trinh vien, nha thiet ke va marketer dang lam viec tu xa.", "Cong dong", "Nha Trang", "Phuoc Long", "Cong nghe", 500, ["freelancer", "meetup", "remote"])
    ];

    private static readonly IReadOnlyList<JobItem> Jobs =
    [
        Job("Frontend Developer Razor Pages", "frontend-developer-razor-pages", "Cong nghe", "KHHub Labs", "18 - 30 trieu", "Nha Trang", "Loc Tho", 900, ["dotnet", "javascript", "ui"], "25748"),
        Job("Nhan vien dieu phoi tour", "nhan-vien-dieu-phoi-tour", "Du lich", "Blue Sea Travel", "10 - 15 trieu", "Nha Trang", "Tan Tien", 720, ["tour", "english", "customer-service"]),
        Job("Chuyen vien marketing dia diem", "chuyen-vien-marketing-dia-diem", "Marketing", "Khanh Hoa Local Guide", "12 - 18 trieu", "Nha Trang", "Phuoc Hai", 680, ["content", "seo", "social"]),
        Job("Quan ly van hanh homestay", "quan-ly-van-hanh-homestay", "Luu tru", "Van Ninh Bay Stay", "14 - 20 trieu", "Khanh Hoa", "Van Ninh", 620, ["homestay", "operations", "hospitality"]),
        Job("Thuc tap sinh du lieu", "thuc-tap-sinh-du-lieu", "Cong nghe", "KH Data Studio", "4 - 7 trieu", "Nha Trang", "Phuoc Long", 540, ["data", "excel", "internship"]),
        Job("Huong dan vien trekking", "huong-dan-vien-trekking", "Du lich", "Green Trail Co.", "Theo chuyen", "Khanh Hoa", "Khanh Vinh", 760, ["trekking", "eco", "safety"]),
        Job("Dau bep mon dia phuong", "dau-bep-mon-dia-phuong", "Am thuc", "Ninh Hoa Kitchen", "13 - 22 trieu", "Khanh Hoa", "Ninh Hoa", 830, ["chef", "local-food", "restaurant"]),
        Job("Nhan vien cham soc khach hang", "nhan-vien-cham-soc-khach-hang", "Dich vu", "Cam Ranh Logistics", "9 - 13 trieu", "Khanh Hoa", "Cam Nghia", 450, ["support", "crm", "logistics"]),
        Job("Designer thuong hieu du lich", "designer-thuong-hieu-du-lich", "Thiet ke", "Travel Brand House", "15 - 25 trieu", "Nha Trang", "Vinh Nguyen", 710, ["design", "branding", "figma"]),
        Job("Nhan vien ban hang dac san", "nhan-vien-ban-hang-dac-san", "Ban le", "Khanh Son Farm", "8 - 12 trieu", "Khanh Hoa", "Khanh Son", 410, ["retail", "farm", "sales"]),
        Job("SEO Content Writer", "seo-content-writer", "Marketing", "KHHub Media", "10 - 16 trieu", "Nha Trang", "Loc Tho", 640, ["seo", "writer", "news"]),
        Job("Ky thuat vien media", "ky-thuat-vien-media", "Media", "Festival Studio", "11 - 17 trieu", "Nha Trang", "Tan Tien", 590, ["video", "photo", "event"])
    ];

    private static readonly IReadOnlyList<LocationItem> Locations =
    [
        Location("Thap Ba Ponagar", "thap-ba-ponagar", "Nha Trang", "Vinh Phuoc", "2 Thang 4, Nha Trang", 4.8m, 1320, ["di-san", "van-hoa"], ["Di san", "Check-in"]),
        Location("Vien Hai Duong Hoc", "vien-hai-duong-hoc", "Nha Trang", "Vinh Nguyen", "1 Cau Da, Nha Trang", 4.6m, 980, ["gia-dinh", "giao-duc"], ["Bao tang", "Gia dinh"]),
        Location("Bai Dai Cam Ranh", "bai-dai-cam-ranh", "Khanh Hoa", "Cam Nghia", "Bai Dai, Cam Ranh", 4.7m, 1210, ["bien", "resort"], ["Bien", "Nghi duong"]),
        Location("Dam Nha Phu", "dam-nha-phu", "Khanh Hoa", "Ninh Hoa", "Ninh Ich, Ninh Hoa", 4.4m, 760, ["sinh-thai", "hai-san"], ["Sinh thai", "Am thuc"]),
        Location("Thanh co Dien Khanh", "thanh-co-dien-khanh", "Khanh Hoa", "Dien Khanh", "Thi tran Dien Khanh", 4.3m, 620, ["lich-su", "di-san"], ["Di san", "Lich su"]),
        Location("Mui Doi Hon Dau", "mui-doi-hon-dau", "Khanh Hoa", "Van Ninh", "Dam Mon, Van Ninh", 4.9m, 1180, ["trekking", "binh-minh"], ["Thien nhien", "Trekking"]),
        Location("Suoi Ba Ho", "suoi-ba-ho", "Khanh Hoa", "Ninh Hoa", "Ninh Ich, Ninh Hoa", 4.5m, 890, ["suoi", "adventure"], ["Thien nhien", "Adventure"]),
        Location("Khu du lich Yang Bay", "khu-du-lich-yang-bay", "Khanh Hoa", "Khanh Vinh", "Khanh Phu, Khanh Vinh", 4.4m, 810, ["thac", "rung"], ["Sinh thai", "Gia dinh"]),
        Location("Hon Mun", "hon-mun", "Nha Trang", "Vinh Nguyen", "Vinh Nha Trang", 4.8m, 1400, ["lan-bien", "bien"], ["Bien", "Sinh thai"]),
        Location("Doc Let", "doc-let", "Khanh Hoa", "Ninh Hoa", "Ninh Hai, Ninh Hoa", 4.6m, 930, ["bien", "gia-dinh"], ["Bien", "Nghi duong"]),
        Location("Khanh Son Orchard", "khanh-son-orchard", "Khanh Hoa", "Khanh Son", "Son Binh, Khanh Son", 4.2m, 520, ["nong-san", "mien-nui"], ["Nong nghiep", "Trai nghiem"]),
        Location("Chua Long Son", "chua-long-son", "Nha Trang", "Phuong Son", "23 Thang 10, Nha Trang", 4.5m, 850, ["tam-linh", "van-hoa"], ["Tam linh", "Di san"])
    ];

    public PagedContentResult<PublicContentCardViewModel> GetCards(PublicContentKind kind, PublicContentQuery query)
    {
        var cards = GetAllCards(kind);
        cards = ApplyFilters(cards, query);
        cards = ApplySort(cards, query.Sort);

        var page = Math.Max(1, query.Page);
        var pageSize = Math.Clamp(query.PageSize, 3, 24);
        var totalCount = cards.Count;

        return new PagedContentResult<PublicContentCardViewModel>
        {
            Items = cards.Skip((page - 1) * pageSize).Take(pageSize).ToList(),
            TotalCount = totalCount,
            CurrentPage = page,
            PageSize = pageSize
        };
    }

    public FilterSidebarViewModel GetFilters(PublicContentKind kind, PublicContentQuery query, string actionPath, string title)
    {
        var cards = GetAllCards(kind);

        return new FilterSidebarViewModel
        {
            ActionPath = actionPath,
            Title = title,
            Query = query,
            Provinces = BuildOptions(cards.Where(x => !string.IsNullOrWhiteSpace(x.Province)).GroupBy(x => x.Province!)),
            Wards = BuildOptions(cards.Where(x => !string.IsNullOrWhiteSpace(x.Ward)).GroupBy(x => x.Ward!)),
            Categories = BuildOptions(cards.GroupBy(x => x.Category)),
            Tags = BuildOptions(cards.SelectMany(x => x.Tags).GroupBy(x => x))
        };
    }

    public DetailPageViewModel? GetDetail(PublicContentKind kind, string slug)
    {
        return kind switch
        {
            PublicContentKind.News => News.FirstOrDefault(x => SlugMatches(x.Slug, x.EnglishSlug, slug)) is { } item ? ToDetail(item) : null,
            PublicContentKind.Job => Jobs.FirstOrDefault(x => SlugMatches(x.Slug, x.EnglishSlug, slug)) is { } item ? ToDetail(item) : null,
            PublicContentKind.Location => Locations.FirstOrDefault(x => SlugMatches(x.Slug, x.EnglishSlug, slug)) is { } item ? ToDetail(item) : null,
            _ => null
        };
    }

    public IReadOnlyList<PublicContentCardViewModel> GetRelated(PublicContentKind kind, string slug, int maxCount)
    {
        var cards = GetAllCards(kind);
        var current = cards.FirstOrDefault(x => x.Slug == slug);
        if (current == null)
        {
            return [];
        }

        return cards
            .Where(x => x.Slug != slug)
            .OrderByDescending(x => x.Category == current.Category)
            .ThenByDescending(x => x.Tags.Intersect(current.Tags, StringComparer.OrdinalIgnoreCase).Count())
            .ThenByDescending(x => x.Popularity)
            .Take(maxCount)
            .ToList();
    }

    private static List<PublicContentCardViewModel> GetAllCards(PublicContentKind kind)
    {
        return kind switch
        {
            PublicContentKind.News => News.Select(ToCard).ToList(),
            PublicContentKind.Job => Jobs.Select(ToCard).ToList(),
            PublicContentKind.Location => Locations.Select(ToCard).ToList(),
            _ => []
        };
    }

    private static List<PublicContentCardViewModel> ApplyFilters(
        IEnumerable<PublicContentCardViewModel> cards,
        PublicContentQuery query)
    {
        var filtered = cards;
        if (!string.IsNullOrWhiteSpace(query.Search))
        {
            filtered = filtered.Where(x =>
                x.Title.Contains(query.Search, StringComparison.OrdinalIgnoreCase) ||
                x.Description.Contains(query.Search, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(query.Province))
        {
            filtered = filtered.Where(x => string.Equals(x.Province, query.Province, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(query.Ward))
        {
            filtered = filtered.Where(x => string.Equals(x.Ward, query.Ward, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(query.Category))
        {
            filtered = filtered.Where(x => string.Equals(x.Category, query.Category, StringComparison.OrdinalIgnoreCase));
        }

        var tags = query.Tags.Where(x => !string.IsNullOrWhiteSpace(x)).ToHashSet(StringComparer.OrdinalIgnoreCase);
        if (tags.Count > 0)
        {
            filtered = filtered.Where(x => x.Tags.Any(tags.Contains));
        }

        return filtered.ToList();
    }

    private static List<PublicContentCardViewModel> ApplySort(IEnumerable<PublicContentCardViewModel> cards, string? sort)
    {
        return (sort ?? PublicContentSort.Newest).ToLowerInvariant() switch
        {
            PublicContentSort.Popular => cards.OrderByDescending(x => x.Popularity).ToList(),
            PublicContentSort.Relevant => cards
                .OrderByDescending(x => x.Rating ?? 0)
                .ThenByDescending(x => x.Popularity)
                .ToList(),
            _ => cards.OrderByDescending(x => x.CreatedAt).ToList()
        };
    }

    private static IReadOnlyList<FilterOption> BuildOptions(IEnumerable<IGrouping<string, PublicContentCardViewModel>> groups)
    {
        return groups
            .OrderBy(x => x.Key)
            .Select(x => new FilterOption(x.Key, x.Key, x.Count()))
            .ToList();
    }

    private static IReadOnlyList<FilterOption> BuildOptions(IEnumerable<IGrouping<string, string>> groups)
    {
        return groups
            .OrderBy(x => x.Key)
            .Select(x => new FilterOption(x.Key, x.Key, x.Count()))
            .ToList();
    }

    private static PublicContentCardViewModel ToCard(NewsItem item)
    {
        return new PublicContentCardViewModel
        {
            Kind = PublicContentKind.News,
            Title = item.Title,
            Slug = item.Slug,
            EnglishSlug = item.EnglishSlug,
            Url = $"/news/{item.Slug}",
            Description = item.Description,
            ThumbnailUrl = item.ThumbnailUrl,
            Category = item.Category,
            Province = item.Province,
            Ward = item.Ward,
            MetaLabel = item.Author,
            CreatedAt = item.CreatedAt,
            Popularity = item.Popularity,
            Tags = item.Tags
        };
    }

    private static PublicContentCardViewModel ToCard(JobItem item)
    {
        return new PublicContentCardViewModel
        {
            Kind = PublicContentKind.Job,
            Title = item.Title,
            Slug = item.Slug,
            EnglishSlug = item.EnglishSlug,
            Url = $"/jobs/{item.Slug}",
            Description = item.Description,
            ThumbnailUrl = Image("office", item.Slug),
            Category = item.Category,
            Province = item.Province,
            Ward = item.Ward,
            WardCode = item.WardCode,
            MetaLabel = $"{item.Company} · {item.Salary}",
            CreatedAt = item.CreatedAt,
            Popularity = item.Popularity,
            Tags = item.Tags
        };
    }

    private static PublicContentCardViewModel ToCard(LocationItem item)
    {
        return new PublicContentCardViewModel
        {
            Kind = PublicContentKind.Location,
            Title = item.Name,
            Slug = item.Slug,
            EnglishSlug = item.EnglishSlug,
            Url = $"/places/{item.Slug}",
            Description = item.Description,
            ThumbnailUrl = item.Images.FirstOrDefault() ?? Image("travel", item.Slug),
            Category = item.Categories.FirstOrDefault() ?? "Dia diem",
            Province = item.Province,
            Ward = item.Ward,
            MetaLabel = item.Address,
            CreatedAt = item.CreatedAt,
            Popularity = item.Popularity,
            Rating = item.Rating,
            Tags = item.Tags
        };
    }

    private DetailPageViewModel ToDetail(NewsItem item)
    {
        return new DetailPageViewModel
        {
            Kind = PublicContentKind.News,
            Title = item.Title,
            Description = item.Description,
            ContentHtml = item.Content,
            Category = item.Category,
            ThumbnailUrl = item.ThumbnailUrl,
            CreatedAt = item.CreatedAt,
            AuthorOrCompany = item.Author,
            Province = item.Province,
            Ward = item.Ward,
            Rating = 4.6m,
            Tags = item.Tags,
            RelatedItems = GetRelated(PublicContentKind.News, item.Slug, 3),
            Comments = Comments()
        };
    }

    private DetailPageViewModel ToDetail(JobItem item)
    {
        return new DetailPageViewModel
        {
            Kind = PublicContentKind.Job,
            Title = item.Title,
            Description = item.Description,
            ContentHtml = item.Description,
            Category = item.Category,
            ThumbnailUrl = Image("office", item.Slug),
            CreatedAt = item.CreatedAt,
            AuthorOrCompany = item.Company,
            Salary = item.Salary,
            Requirements = item.Requirements,
            Province = item.Province,
            Ward = item.Ward,
            Rating = 4.5m,
            Tags = item.Tags,
            JobTagLinks = item.Tags.Select(t => new JobDetailTagLink(t, t)).ToList(),
            RelatedItems = GetRelated(PublicContentKind.Job, item.Slug, 3),
            Comments = Comments()
        };
    }

    private DetailPageViewModel ToDetail(LocationItem item)
    {
        return new DetailPageViewModel
        {
            Kind = PublicContentKind.Location,
            Title = item.Name,
            Description = item.Description,
            ContentHtml = item.Description,
            Category = item.Categories.FirstOrDefault() ?? "Dia diem",
            ThumbnailUrl = item.Images.FirstOrDefault() ?? Image("travel", item.Slug),
            CreatedAt = item.CreatedAt,
            Province = item.Province,
            Ward = item.Ward,
            Address = item.Address,
            Rating = item.Rating,
            Images = item.Images,
            Tags = item.Tags,
            RelatedItems = GetRelated(PublicContentKind.Location, item.Slug, 3),
            Comments = Comments()
        };
    }

    private static NewsItem CreateNews(
        string title,
        string slug,
        string description,
        string author,
        string province,
        string ward,
        string category,
        int popularity,
        IReadOnlyList<string> tags)
    {
        return new NewsItem
        {
            Title = title,
            Slug = slug,
            EnglishSlug = EnglishSlug(slug),
            Description = description,
            Content = $"<p>{description}</p><p>Noi dung noi bat gom thong tin boi canh, gia tri cho cong dong va goi y hanh dong de doc gia tiep tuc kham pha Khanh Hoa.</p>",
            Author = author,
            Province = province,
            Ward = ward,
            Category = category,
            Popularity = popularity,
            CreatedAt = SeedDate.AddDays(-NewsSeedOffset(popularity)),
            ThumbnailUrl = Image("vietnam travel", slug),
            Tags = tags
        };
    }

    private static JobItem Job(
        string title,
        string slug,
        string category,
        string company,
        string salary,
        string province,
        string ward,
        int popularity,
        IReadOnlyList<string> tags,
        string? wardCode = null)
    {
        return new JobItem
        {
            Title = title,
            Slug = slug,
            EnglishSlug = EnglishSlug(slug),
            Category = category,
            Company = company,
            Salary = salary,
            Province = province,
            Ward = ward,
            WardCode = wardCode,
            Popularity = popularity,
            CreatedAt = SeedDate.AddDays(-NewsSeedOffset(popularity)),
            Description = $"Co hoi {title.ToLowerInvariant()} tai {company}, moi truong tre va uu tien ung vien hieu thi truong Khanh Hoa.",
            Requirements = "Can ky nang giao tiep tot, tu duy dich vu, kha nang tu hoc va san sang phoi hop voi cac nhom lien quan.",
            Tags = tags
        };
    }

    private static LocationItem Location(
        string name,
        string slug,
        string province,
        string ward,
        string address,
        decimal rating,
        int popularity,
        IReadOnlyList<string> tags,
        IReadOnlyList<string> categories)
    {
        return new LocationItem
        {
            Name = name,
            Slug = slug,
            EnglishSlug = EnglishSlug(slug),
            Province = province,
            Ward = ward,
            Address = address,
            Rating = rating,
            Popularity = popularity,
            CreatedAt = SeedDate.AddDays(-NewsSeedOffset(popularity)),
            Description = $"{name} la diem den phu hop de kham pha van hoa, canh quan va trai nghiem dia phuong tai {province}.",
            Images =
            [
                Image("nha trang beach", slug),
                Image("khanh hoa travel", $"{slug}-gallery-1"),
                Image("vietnam landscape", $"{slug}-gallery-2"),
                Image("coastal sunset", $"{slug}-gallery-3"),
                Image("local culture", $"{slug}-gallery-4"),
                Image("street food", $"{slug}-gallery-5")
            ],
            Tags = tags,
            Categories = categories
        };
    }

    private static IReadOnlyList<CommentViewModel> Comments()
    {
        return
        [
            new CommentViewModel("Minh Anh", "Thong tin huu ich, minh da luu lai de len lich cuoi tuan.", SeedDate.AddDays(-2), 4.5m),
            new CommentViewModel("Quoc Bao", "Giao dien de xem va noi dung kha sat voi nhu cau dia phuong.", SeedDate.AddDays(-4), 5m)
        ];
    }

    private static int NewsSeedOffset(int popularity)
    {
        return Math.Abs(popularity % 18);
    }

    private static bool SlugMatches(string vietnameseSlug, string englishSlug, string requestedSlug)
    {
        return string.Equals(vietnameseSlug, requestedSlug, StringComparison.OrdinalIgnoreCase) ||
            string.Equals(englishSlug, requestedSlug, StringComparison.OrdinalIgnoreCase);
    }

    private static string EnglishSlug(string vietnameseSlug)
    {
        return EnglishSlugs.TryGetValue(vietnameseSlug, out var englishSlug) ? englishSlug : vietnameseSlug;
    }

    private static string Image(string query, string seed)
    {
        // Picsum is used so mock data still renders without an Unsplash account or external CDN credentials.
        var seedSlug = Uri.EscapeDataString($"{query}-{seed}".ToLowerInvariant());
        return $"https://picsum.photos/seed/{seedSlug}/1200/720";
    }
}
