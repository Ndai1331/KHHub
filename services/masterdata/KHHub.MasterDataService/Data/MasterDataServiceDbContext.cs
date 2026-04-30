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
    }
}