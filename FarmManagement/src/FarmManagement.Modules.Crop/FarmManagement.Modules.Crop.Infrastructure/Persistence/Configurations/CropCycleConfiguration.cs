using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FarmManagement.Modules.Crop.Domain.Aggregates;
using FarmManagement.Modules.Crop.Domain.ValueObjects;
using FarmManagement.SharedKernel.Domain;

namespace FarmManagement.Modules.Crop.Infrastructure.Persistence.Configurations;

internal sealed class CropCycleConfiguration : IEntityTypeConfiguration<CropCycle>
{
    public void Configure(EntityTypeBuilder<CropCycle> builder)
    {
        builder.ToTable("CropCycles");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .HasConversion(
                id => id.Value,
                value => CropCycleId.From(value));

        builder.Property(c => c.FarmId)
            .HasConversion(
                id => id.Value,
                value => FarmId.From(value));

        builder.Property(c => c.FieldId)
            .HasConversion(
                id => id.Value,
                value => FieldId.From(value));

        builder.OwnsOne(c => c.CropType, ct =>
        {
            ct.Property(t => t.CropCode).HasColumnName("CropCode").IsRequired();
            ct.Property(t => t.Name).HasColumnName("CropName").IsRequired().HasMaxLength(200);
            ct.Property(t => t.TypicalStages).HasColumnName("TypicalStages").IsRequired().HasMaxLength(500);
            ct.Property(t => t.DurationDays).HasColumnName("DurationDays").IsRequired();
        });

        builder.Property(c => c.CurrentStage).IsRequired();
        builder.Property(c => c.PlantingDate).IsRequired();
        builder.Property(c => c.ExpectedHarvestDate).IsRequired();
        builder.Property(c => c.Status).IsRequired();

        builder.OwnsOne(c => c.YieldRecord, yr =>
        {
            yr.Property(y => y.Quantity).HasColumnName("YieldQuantity");
            yr.Property(y => y.Unit).HasColumnName("YieldUnit");
            yr.Property(y => y.HarvestDate).HasColumnName("HarvestDate");
        });

        // Ignore DomainEvents collection - it's not persisted
        builder.Ignore(c => c.DomainEvents);
    }
}
