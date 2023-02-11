using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Recommendations.Domain;

namespace Recommendations.Persistence.EntityTypeConfigurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(product => product.Id);
        builder.HasIndex(product => product.Id).IsUnique();

        builder.HasIndex(product => product.Name).IsUnique();
        builder.Property(product => product.Name)
            .HasMaxLength(100);

        builder.Property(product => product.AverageRate)
            .HasDefaultValue(1);
    }
}