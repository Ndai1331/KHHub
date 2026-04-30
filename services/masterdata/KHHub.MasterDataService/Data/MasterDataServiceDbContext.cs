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
    }
}