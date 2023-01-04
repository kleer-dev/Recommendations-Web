using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Recommendations.Domain;

namespace Recommendations.Persistence.EntityTypeConfigurations;

public class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.HasKey(review => review.Id);
        builder.HasIndex(review => review.Id).IsUnique();

        builder.Property(review => review.Title)
            .HasMaxLength(100);

        builder.Property(review => review.Description)
            .HasMaxLength(5000);

        builder.Property(review => review.AuthorRate)
            .HasDefaultValue(1);

        builder.HasOne(rating => rating.Product)
            .WithOne(product => product.Review)
            .HasForeignKey<Product>(product => product.Id);
    }
}