
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FarmAggregate = FarmManagement.Modules.Farm.Domain.Aggregates.FarmAggregate.Farm;
using FarmManagement.SharedKernel.Domain;

namespace FarmManagement.Modules.Farm.Infrastructure.Persistence.Configurations;

internal sealed class FarmConfiguration : IEntityTypeConfiguration<FarmAggregate>
{
    public void Configure(EntityTypeBuilder<FarmAggregate> builder)
    {
        builder.ToTable("Farms");

        builder.HasKey(f => f.Id);

        builder.Property(f => f.Id)
            .HasConversion(
                id => id.Value,
                value => FarmId.From(value));

        builder.OwnsOne(f => f.Location);

        builder.HasMany(f => f.Fields)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
