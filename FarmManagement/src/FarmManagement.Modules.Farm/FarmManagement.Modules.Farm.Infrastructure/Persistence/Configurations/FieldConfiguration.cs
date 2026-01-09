using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FieldAggregate = FarmManagement.Modules.Farm.Domain.Aggregates.FarmAggregate.Field;
using FarmManagement.SharedKernel.Domain;

namespace FarmManagement.Modules.Farm.Infrastructure.Persistence.Configurations;

internal sealed class FieldConfiguration : IEntityTypeConfiguration<FieldAggregate>
{
    public void Configure(EntityTypeBuilder<FieldAggregate> builder)
    {
        builder.ToTable("Fields");

        builder.HasKey(f => f.Id);

        builder.Property(f => f.Id)
            .HasConversion(
                id => id.Value,
                value => FieldId.From(value));

        builder.Property(f => f.Name)
            .IsRequired()
            .HasMaxLength(200);
    }
}
