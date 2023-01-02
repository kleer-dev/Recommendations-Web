using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Recommendations.Domain;

namespace Recommendations.Persistence.EntityTypeConfigurations;

public class RatingConfiguration : IEntityTypeConfiguration<Rating>
{
    public void Configure(EntityTypeBuilder<Rating> builder)
    {
        builder.HasKey(rating => rating.Id);
        builder.HasIndex(rating => rating.Id).IsUnique();

        builder.Property(rating => rating.Value)
            .IsRequired();
    }
}