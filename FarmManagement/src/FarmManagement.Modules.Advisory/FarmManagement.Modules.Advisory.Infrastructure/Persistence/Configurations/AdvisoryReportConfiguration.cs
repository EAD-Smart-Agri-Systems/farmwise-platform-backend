using FarmManagement.Modules.Advisory.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FarmManagement.Modules.Advisory.Infrastructure.Persistence.Configurations;

public class AdvisoryReportConfiguration : IEntityTypeConfiguration<AdvisoryReport>
{
    public void Configure(EntityTypeBuilder<AdvisoryReport> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.FarmId).IsRequired();
        builder.Property(x => x.CropType).IsRequired();
        builder.Property(x => x.GeneratedOn).IsRequired();

        builder.OwnsOne(x => x.PestRiskLevel, pr =>
        {
            pr.Property(p => p.Level)
              .HasColumnName("PestRiskLevel")
              .IsRequired();
        });

        builder.Property(x => x.Recommendation).IsRequired();

        // Ignore DomainEvents collection - it's not persisted
        builder.Ignore(x => x.DomainEvents);
    }
}
