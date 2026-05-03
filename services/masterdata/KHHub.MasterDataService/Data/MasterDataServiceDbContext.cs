using KHHub.MasterDataService.Entities.HomeBanners;
using KHHub.MasterDataService.Entities.PlaceViews;
using KHHub.MasterDataService.Entities.PlaceFavorites;
using KHHub.MasterDataService.Entities.PlaceReviews;
using KHHub.MasterDataService.Entities.EntityFiles;
using KHHub.MasterDataService.Entities.PlaceTagMappings;
using KHHub.MasterDataService.Entities.Places;
using KHHub.MasterDataService.Entities.PlaceTags;
using KHHub.MasterDataService.Entities.PlaceCategories;
using KHHub.MasterDataService.Entities.MediaFiles;
using KHHub.MasterDataService.Entities.ArticleViews;
using KHHub.MasterDataService.Entities.ArticleTagMappings;
using KHHub.MasterDataService.Entities.Articles;
using KHHub.MasterDataService.Entities.ArticleTags;
using KHHub.MasterDataService.Entities.ArticleCategories;
using KHHub.MasterDataService.Entities.Wards;
using KHHub.MasterDataService.Entities.Provinces;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.DistributedEvents;

namespace KHHub.MasterDataService.Data;

[ConnectionStringName(DatabaseName)]
public class MasterDataServiceDbContext : AbpDbContext<MasterDataServiceDbContext>, IHasEventInbox, IHasEventOutbox
{
    public DbSet<HomeBanner> HomeBanners { get; set; } = null!;
    public DbSet<PlaceView> PlaceViews { get; set; } = null!;
    public DbSet<PlaceFavorite> PlaceFavorites { get; set; } = null!;
    public DbSet<PlaceReview> PlaceReviews { get; set; } = null!;
    public DbSet<EntityFile> EntityFiles { get; set; } = null!;
    public DbSet<PlaceTagMapping> PlaceTagMappings { get; set; } = null!;
    public DbSet<Place> Places { get; set; } = null!;
    public DbSet<PlaceTag> PlaceTags { get; set; } = null!;
    public DbSet<PlaceCategory> PlaceCategories { get; set; } = null!;
    public DbSet<MediaFile> MediaFiles { get; set; } = null!;
    public DbSet<ArticleView> ArticleViews { get; set; } = null!;
    public DbSet<ArticleTagMapping> ArticleTagMappings { get; set; } = null!;
    public DbSet<Article> Articles { get; set; } = null!;
    public DbSet<ArticleTag> ArticleTags { get; set; } = null!;
    public DbSet<ArticleCategory> ArticleCategories { get; set; } = null!;
    public DbSet<Ward> Wards { get; set; } = null!;
    public DbSet<Province> Provinces { get; set; } = null!;

    public const string DbTablePrefix = "";
    public const string DbSchema = null;
    public const string DatabaseName = "MasterDataService";

    public DbSet<IncomingEventRecord> IncomingEvents { get; set; }

    public DbSet<OutgoingEventRecord> OutgoingEvents { get; set; }

    public MasterDataServiceDbContext(DbContextOptions<MasterDataServiceDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ConfigureEventInbox();
        builder.ConfigureEventOutbox();
        if (builder.IsHostDatabase())
        {
            builder.Entity<Province>(b => {
                b.ToTable(DbTablePrefix + "Provinces", DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.Code).HasColumnName(nameof(Province.Code)).IsRequired().HasMaxLength(ProvinceConsts.CodeMaxLength);
                b.Property(x => x.Name).HasColumnName(nameof(Province.Name)).IsRequired().HasMaxLength(ProvinceConsts.NameMaxLength);
            });
        }

        if (builder.IsHostDatabase())
        {
            builder.Entity<Ward>(b => {
                b.ToTable(DbTablePrefix + "Wards", DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.Code).HasColumnName(nameof(Ward.Code)).IsRequired().HasMaxLength(WardConsts.CodeMaxLength);
                b.Property(x => x.Name).HasColumnName(nameof(Ward.Name)).IsRequired().HasMaxLength(WardConsts.NameMaxLength);
                b.HasOne<Province>().WithMany().IsRequired().HasForeignKey(x => x.ProvinceId).OnDelete(DeleteBehavior.NoAction);
            });
        }

        if (builder.IsHostDatabase())
        {
            builder.Entity<ArticleCategory>(b => {
                b.ToTable(DbTablePrefix + "ArticleCategories", DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.Name).HasColumnName(nameof(ArticleCategory.Name)).IsRequired().HasMaxLength(ArticleCategoryConsts.NameMaxLength);
                b.Property(x => x.Slug).HasColumnName(nameof(ArticleCategory.Slug)).IsRequired().HasMaxLength(ArticleCategoryConsts.SlugMaxLength);
                b.Property(x => x.Description).HasColumnName(nameof(ArticleCategory.Description));
                b.Property(x => x.Icon).HasColumnName(nameof(ArticleCategory.Icon)).HasMaxLength(ArticleCategoryConsts.IconMaxLength);
                b.Property(x => x.ParentId).HasColumnName(nameof(ArticleCategory.ParentId));
                b.Property(x => x.DisplayOrder).HasColumnName(nameof(ArticleCategory.DisplayOrder));
                b.Property(x => x.IsActive).HasColumnName(nameof(ArticleCategory.IsActive));
                b.Property(x => x.ThumbnailUrl).HasColumnName(nameof(ArticleCategory.ThumbnailUrl)).HasMaxLength(ArticleCategoryConsts.ThumbnailUrlMaxLength);
            });
        }

        if (builder.IsHostDatabase())
        {
            builder.Entity<ArticleTag>(b => {
                b.ToTable(DbTablePrefix + "ArticleTags", DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.Name).HasColumnName(nameof(ArticleTag.Name)).IsRequired().HasMaxLength(ArticleTagConsts.NameMaxLength);
                b.Property(x => x.Slug).HasColumnName(nameof(ArticleTag.Slug)).IsRequired().HasMaxLength(ArticleTagConsts.SlugMaxLength);
                b.Property(x => x.Description).HasColumnName(nameof(ArticleTag.Description));
                b.Property(x => x.UsageCount).HasColumnName(nameof(ArticleTag.UsageCount));
            });
        }

        if (builder.IsHostDatabase())
        {
            builder.Entity<Article>(b => {
                b.ToTable(DbTablePrefix + "Articles", DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.Title).HasColumnName(nameof(Article.Title)).IsRequired().HasMaxLength(ArticleConsts.TitleMaxLength);
                b.Property(x => x.Slug).HasColumnName(nameof(Article.Slug)).IsRequired().HasMaxLength(ArticleConsts.SlugMaxLength);
                b.Property(x => x.Summary).HasColumnName(nameof(Article.Summary)).IsRequired().HasMaxLength(ArticleConsts.SummaryMaxLength);
                b.Property(x => x.Content).HasColumnName(nameof(Article.Content)).IsRequired();
                b.Property(x => x.ThumbnailUrl).HasColumnName(nameof(Article.ThumbnailUrl)).HasMaxLength(ArticleConsts.ThumbnailUrlMaxLength);
                b.Property(x => x.CoverImageUrl).HasColumnName(nameof(Article.CoverImageUrl)).HasMaxLength(ArticleConsts.CoverImageUrlMaxLength);
                b.Property(x => x.Type).HasColumnName(nameof(Article.Type));
                b.Property(x => x.AuthorName).HasColumnName(nameof(Article.AuthorName)).IsRequired().HasMaxLength(ArticleConsts.AuthorNameMaxLength);
                b.Property(x => x.Source).HasColumnName(nameof(Article.Source)).HasMaxLength(ArticleConsts.SourceMaxLength);
                b.Property(x => x.SourceUrl).HasColumnName(nameof(Article.SourceUrl)).HasMaxLength(ArticleConsts.SourceUrlMaxLength);
                b.Property(x => x.Status).HasColumnName(nameof(Article.Status));
                b.Property(x => x.PublishedAt).HasColumnName(nameof(Article.PublishedAt));
                b.Property(x => x.IsFeatured).HasColumnName(nameof(Article.IsFeatured));
                b.Property(x => x.IsHot).HasColumnName(nameof(Article.IsHot));
                b.Property(x => x.IsTrending).HasColumnName(nameof(Article.IsTrending));
                b.Property(x => x.ViewCount).HasColumnName(nameof(Article.ViewCount));
                b.Property(x => x.LikeCount).HasColumnName(nameof(Article.LikeCount));
                b.Property(x => x.ShareCount).HasColumnName(nameof(Article.ShareCount));
                b.Property(x => x.CommentCount).HasColumnName(nameof(Article.CommentCount));
                b.Property(x => x.ReadingTime).HasColumnName(nameof(Article.ReadingTime));
                b.Property(x => x.SeoTitle).HasColumnName(nameof(Article.SeoTitle)).IsRequired().HasMaxLength(ArticleConsts.SeoTitleMaxLength);
                b.Property(x => x.SeoDescription).HasColumnName(nameof(Article.SeoDescription));
                b.Property(x => x.SeoKeywords).HasColumnName(nameof(Article.SeoKeywords));
                b.HasOne<ArticleCategory>().WithMany().IsRequired().HasForeignKey(x => x.ArticleCategoryId).OnDelete(DeleteBehavior.NoAction);
            });
        }

        if (builder.IsHostDatabase())
        {
            builder.Entity<ArticleTagMapping>(b => {
                b.ToTable(DbTablePrefix + "ArticleTagMappings", DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.IsPrimary).HasColumnName(nameof(ArticleTagMapping.IsPrimary));
                b.Property(x => x.Order).HasColumnName(nameof(ArticleTagMapping.Order));
                b.HasOne<ArticleTag>().WithMany().IsRequired().HasForeignKey(x => x.ArticleTagId).OnDelete(DeleteBehavior.NoAction);
                b.HasOne<Article>().WithMany().IsRequired().HasForeignKey(x => x.ArticleId).OnDelete(DeleteBehavior.NoAction);
            });
        }

        if (builder.IsHostDatabase())
        {
            builder.Entity<ArticleView>(b => {
                b.ToTable(DbTablePrefix + "ArticleViews", DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.IpAddress).HasColumnName(nameof(ArticleView.IpAddress)).HasMaxLength(ArticleViewConsts.IpAddressMaxLength);
                b.Property(x => x.Device).HasColumnName(nameof(ArticleView.Device)).HasMaxLength(ArticleViewConsts.DeviceMaxLength);
                b.Property(x => x.Source).HasColumnName(nameof(ArticleView.Source)).HasMaxLength(ArticleViewConsts.SourceMaxLength);
                b.Property(x => x.ViewedAt).HasColumnName(nameof(ArticleView.ViewedAt));
                b.Property(x => x.Duration).HasColumnName(nameof(ArticleView.Duration));
                b.Property(x => x.UserId).HasColumnName(nameof(ArticleView.UserId));
                b.HasOne<Article>().WithMany().IsRequired().HasForeignKey(x => x.ArticleId).OnDelete(DeleteBehavior.NoAction);
            });
        }

        if (builder.IsHostDatabase())
        {
            builder.Entity<MediaFile>(b => {
                b.ToTable(DbTablePrefix + "MediaFiles", DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.FileName).HasColumnName(nameof(MediaFile.FileName)).HasMaxLength(MediaFileConsts.FileNameMaxLength);
                b.Property(x => x.OriginalFileName).HasColumnName(nameof(MediaFile.OriginalFileName)).HasMaxLength(MediaFileConsts.OriginalFileNameMaxLength);
                b.Property(x => x.Extension).HasColumnName(nameof(MediaFile.Extension)).HasMaxLength(MediaFileConsts.ExtensionMaxLength);
                b.Property(x => x.ContentType).HasColumnName(nameof(MediaFile.ContentType)).HasMaxLength(MediaFileConsts.ContentTypeMaxLength);
                b.Property(x => x.Size).HasColumnName(nameof(MediaFile.Size));
                b.Property(x => x.StorageProvider).HasColumnName(nameof(MediaFile.StorageProvider)).HasMaxLength(MediaFileConsts.StorageProviderMaxLength);
                b.Property(x => x.Bucket).HasColumnName(nameof(MediaFile.Bucket)).HasMaxLength(MediaFileConsts.BucketMaxLength);
                b.Property(x => x.Folder).HasColumnName(nameof(MediaFile.Folder)).HasMaxLength(MediaFileConsts.FolderMaxLength);
                b.Property(x => x.Path).HasColumnName(nameof(MediaFile.Path)).HasMaxLength(MediaFileConsts.PathMaxLength);
                b.Property(x => x.Url).HasColumnName(nameof(MediaFile.Url)).HasMaxLength(MediaFileConsts.UrlMaxLength);
                b.Property(x => x.Checksum).HasColumnName(nameof(MediaFile.Checksum)).HasMaxLength(MediaFileConsts.ChecksumMaxLength);
                b.Property(x => x.Width).HasColumnName(nameof(MediaFile.Width));
                b.Property(x => x.Height).HasColumnName(nameof(MediaFile.Height));
                b.Property(x => x.Duration).HasColumnName(nameof(MediaFile.Duration));
                b.Property(x => x.FileType).HasColumnName(nameof(MediaFile.FileType));
                b.Property(x => x.Status).HasColumnName(nameof(MediaFile.Status));
            });
        }

        if (builder.IsHostDatabase())
        {
        }

        if (builder.IsHostDatabase())
        {
            builder.Entity<PlaceTag>(b => {
                b.ToTable(DbTablePrefix + "PlaceTags", DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.Name).HasColumnName(nameof(PlaceTag.Name)).IsRequired().HasMaxLength(PlaceTagConsts.NameMaxLength);
                b.Property(x => x.Slug).HasColumnName(nameof(PlaceTag.Slug)).IsRequired().HasMaxLength(PlaceTagConsts.SlugMaxLength);
                b.Property(x => x.Description).HasColumnName(nameof(PlaceTag.Description)).HasMaxLength(PlaceTagConsts.DescriptionMaxLength);
                b.Property(x => x.UsageCount).HasColumnName(nameof(PlaceTag.UsageCount));
            });
        }

        if (builder.IsHostDatabase())
        {
            builder.Entity<Place>(b => {
                b.ToTable(DbTablePrefix + "Places", DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.Name).HasColumnName(nameof(Place.Name)).IsRequired().HasMaxLength(PlaceConsts.NameMaxLength);
                b.Property(x => x.Slug).HasColumnName(nameof(Place.Slug)).IsRequired().HasMaxLength(PlaceConsts.SlugMaxLength);
                b.Property(x => x.ShortDescription).HasColumnName(nameof(Place.ShortDescription)).HasMaxLength(PlaceConsts.ShortDescriptionMaxLength);
                b.Property(x => x.Description).HasColumnName(nameof(Place.Description));
                b.Property(x => x.ThumbnailUrl).HasColumnName(nameof(Place.ThumbnailUrl)).HasMaxLength(PlaceConsts.ThumbnailUrlMaxLength);
                b.Property(x => x.CoverImageUrl).HasColumnName(nameof(Place.CoverImageUrl)).HasMaxLength(PlaceConsts.CoverImageUrlMaxLength);
                b.Property(x => x.Address).HasColumnName(nameof(Place.Address)).HasMaxLength(PlaceConsts.AddressMaxLength);
                b.Property(x => x.Latitude).HasColumnName(nameof(Place.Latitude));
                b.Property(x => x.Longituded).HasColumnName(nameof(Place.Longituded));
                b.Property(x => x.PhoneNumber).HasColumnName(nameof(Place.PhoneNumber)).HasMaxLength(PlaceConsts.PhoneNumberMaxLength);
                b.Property(x => x.Email).HasColumnName(nameof(Place.Email)).HasMaxLength(PlaceConsts.EmailMaxLength);
                b.Property(x => x.Website).HasColumnName(nameof(Place.Website)).HasMaxLength(PlaceConsts.WebsiteMaxLength);
                b.Property(x => x.OpeningHours).HasColumnName(nameof(Place.OpeningHours)).HasMaxLength(PlaceConsts.OpeningHoursMaxLength);
                b.Property(x => x.PriceRange).HasColumnName(nameof(Place.PriceRange));
                b.Property(x => x.GoogleMapUrl).HasColumnName(nameof(Place.GoogleMapUrl)).HasMaxLength(PlaceConsts.GoogleMapUrlMaxLength);
                b.Property(x => x.Status).HasColumnName(nameof(Place.Status));
                b.Property(x => x.ViewCount).HasColumnName(nameof(Place.ViewCount));
                b.Property(x => x.FavoriteCount).HasColumnName(nameof(Place.FavoriteCount));
                b.Property(x => x.ReviewCount).HasColumnName(nameof(Place.ReviewCount));
                b.Property(x => x.RatingAveraged).HasColumnName(nameof(Place.RatingAveraged));
                b.Property(x => x.RatingTotal).HasColumnName(nameof(Place.RatingTotal));
                b.Property(x => x.IsFeatured).HasColumnName(nameof(Place.IsFeatured));
                b.Property(x => x.IsHot).HasColumnName(nameof(Place.IsHot));
                b.Property(x => x.IsVerified).HasColumnName(nameof(Place.IsVerified));
                b.Property(x => x.SeoTitle).HasColumnName(nameof(Place.SeoTitle)).IsRequired().HasMaxLength(PlaceConsts.SeoTitleMaxLength);
                b.Property(x => x.SeoDescription).HasColumnName(nameof(Place.SeoDescription)).HasMaxLength(PlaceConsts.SeoDescriptionMaxLength);
                b.Property(x => x.SeoKeywords).HasColumnName(nameof(Place.SeoKeywords)).HasMaxLength(PlaceConsts.SeoKeywordsMaxLength);
                b.HasOne<PlaceCategory>().WithMany().IsRequired().HasForeignKey(x => x.PlaceCategoryId).OnDelete(DeleteBehavior.NoAction);
                b.HasOne<Province>().WithMany().IsRequired().HasForeignKey(x => x.ProvinceId).OnDelete(DeleteBehavior.NoAction);
                b.HasOne<Ward>().WithMany().IsRequired().HasForeignKey(x => x.WardId).OnDelete(DeleteBehavior.NoAction);
            });
        }

        if (builder.IsHostDatabase())
        {
            builder.Entity<PlaceTagMapping>(b => {
                b.ToTable(DbTablePrefix + "PlaceTagMappings", DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.IsPrimary).HasColumnName(nameof(PlaceTagMapping.IsPrimary));
                b.Property(x => x.SortOrder).HasColumnName(nameof(PlaceTagMapping.SortOrder));
                b.HasOne<PlaceTag>().WithMany().IsRequired().HasForeignKey(x => x.PlaceTagId).OnDelete(DeleteBehavior.NoAction);
                b.HasOne<Place>().WithMany().IsRequired().HasForeignKey(x => x.PlaceId).OnDelete(DeleteBehavior.NoAction);
            });
        }

        if (builder.IsHostDatabase())
        {
            builder.Entity<EntityFile>(b => {
                b.ToTable(DbTablePrefix + "EntityFiles", DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.EntityType).HasColumnName(nameof(EntityFile.EntityType)).IsRequired().HasMaxLength(EntityFileConsts.EntityTypeMaxLength);
                b.Property(x => x.EntityId).HasColumnName(nameof(EntityFile.EntityId));
                b.Property(x => x.Collection).HasColumnName(nameof(EntityFile.Collection)).HasMaxLength(EntityFileConsts.CollectionMaxLength);
                b.Property(x => x.SortOrder).HasColumnName(nameof(EntityFile.SortOrder));
                b.Property(x => x.IsPrimary).HasColumnName(nameof(EntityFile.IsPrimary));
                b.HasOne<MediaFile>().WithMany().IsRequired().HasForeignKey(x => x.MediaFileId).OnDelete(DeleteBehavior.NoAction);
            });
        }

        if (builder.IsHostDatabase())
        {
            builder.Entity<PlaceReview>(b => {
                b.ToTable(DbTablePrefix + "PlaceReviews", DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.Rating).HasColumnName(nameof(PlaceReview.Rating)).IsRequired().HasMaxLength(PlaceReviewConsts.RatingMaxLength);
                b.Property(x => x.Title).HasColumnName(nameof(PlaceReview.Title)).IsRequired().HasMaxLength(PlaceReviewConsts.TitleMaxLength);
                b.Property(x => x.Comment).HasColumnName(nameof(PlaceReview.Comment));
                b.Property(x => x.LikeCount).HasColumnName(nameof(PlaceReview.LikeCount));
                b.Property(x => x.Status).HasColumnName(nameof(PlaceReview.Status));
                b.Property(x => x.UserId).HasColumnName(nameof(PlaceReview.UserId));
                b.HasOne<Place>().WithMany().IsRequired().HasForeignKey(x => x.PlaceId).OnDelete(DeleteBehavior.NoAction);
            });
        }

        if (builder.IsHostDatabase())
        {
            builder.Entity<PlaceFavorite>(b => {
                b.ToTable(DbTablePrefix + "PlaceFavorites", DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.UserId).HasColumnName(nameof(PlaceFavorite.UserId));
                b.HasOne<Place>().WithMany().IsRequired().HasForeignKey(x => x.PlaceId).OnDelete(DeleteBehavior.NoAction);
            });
        }

        if (builder.IsHostDatabase())
        {
            builder.Entity<PlaceView>(b => {
                b.ToTable(DbTablePrefix + "PlaceViews", DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.UserId).HasColumnName(nameof(PlaceView.UserId));
                b.Property(x => x.IpAddress).HasColumnName(nameof(PlaceView.IpAddress)).HasMaxLength(PlaceViewConsts.IpAddressMaxLength);
                b.Property(x => x.Device).HasColumnName(nameof(PlaceView.Device)).HasMaxLength(PlaceViewConsts.DeviceMaxLength);
                b.Property(x => x.ViewedAt).HasColumnName(nameof(PlaceView.ViewedAt));
                b.Property(x => x.Duration).HasColumnName(nameof(PlaceView.Duration));
                b.Property(x => x.Source).HasColumnName(nameof(PlaceView.Source)).HasMaxLength(PlaceViewConsts.SourceMaxLength);
                b.HasOne<Place>().WithMany().IsRequired().HasForeignKey(x => x.PlaceId).OnDelete(DeleteBehavior.NoAction);
            });
        }

        if (builder.IsHostDatabase())
        {
            builder.Entity<PlaceCategory>(b => {
                b.ToTable(DbTablePrefix + "PlaceCategories", DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.Name).HasColumnName(nameof(PlaceCategory.Name)).IsRequired().HasMaxLength(PlaceCategoryConsts.NameMaxLength);
                b.Property(x => x.Slug).HasColumnName(nameof(PlaceCategory.Slug)).IsRequired().HasMaxLength(PlaceCategoryConsts.SlugMaxLength);
                b.Property(x => x.Description).HasColumnName(nameof(PlaceCategory.Description)).HasMaxLength(PlaceCategoryConsts.DescriptionMaxLength);
                b.Property(x => x.Icon).HasColumnName(nameof(PlaceCategory.Icon)).HasMaxLength(PlaceCategoryConsts.IconMaxLength);
                b.Property(x => x.Color).HasColumnName(nameof(PlaceCategory.Color)).HasMaxLength(PlaceCategoryConsts.ColorMaxLength);
                b.Property(x => x.ParentId).HasColumnName(nameof(PlaceCategory.ParentId));
                b.Property(x => x.DisplayOrder).HasColumnName(nameof(PlaceCategory.DisplayOrder));
                b.Property(x => x.IsActive).HasColumnName(nameof(PlaceCategory.IsActive));
            });
        }

        if (builder.IsHostDatabase())
        {
            builder.Entity<HomeBanner>(b => {
                b.ToTable(DbTablePrefix + "HomeBanners", DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.Title).HasColumnName(nameof(HomeBanner.Title)).IsRequired().HasMaxLength(HomeBannerConsts.TitleMaxLength);
                b.Property(x => x.Subtitle).HasColumnName(nameof(HomeBanner.Subtitle)).HasMaxLength(HomeBannerConsts.SubtitleMaxLength);
                b.Property(x => x.Description).HasColumnName(nameof(HomeBanner.Description));
                b.Property(x => x.ImageUrl).HasColumnName(nameof(HomeBanner.ImageUrl)).IsRequired().HasMaxLength(HomeBannerConsts.ImageUrlMaxLength);
                b.Property(x => x.MobileImageUrl).HasColumnName(nameof(HomeBanner.MobileImageUrl)).HasMaxLength(HomeBannerConsts.MobileImageUrlMaxLength);
                b.Property(x => x.ButtonText).HasColumnName(nameof(HomeBanner.ButtonText)).HasMaxLength(HomeBannerConsts.ButtonTextMaxLength);
                b.Property(x => x.ButtonUrl).HasColumnName(nameof(HomeBanner.ButtonUrl)).HasMaxLength(HomeBannerConsts.ButtonUrlMaxLength);
                b.Property(x => x.TargetType).HasColumnName(nameof(HomeBanner.TargetType));
                b.Property(x => x.TargetId).HasColumnName(nameof(HomeBanner.TargetId));
                b.Property(x => x.SortOrder).HasColumnName(nameof(HomeBanner.SortOrder));
                b.Property(x => x.IsActive).HasColumnName(nameof(HomeBanner.IsActive));
                b.Property(x => x.StartDate).HasColumnName(nameof(HomeBanner.StartDate));
                b.Property(x => x.EndDate).HasColumnName(nameof(HomeBanner.EndDate));
            });
        }
    }
}