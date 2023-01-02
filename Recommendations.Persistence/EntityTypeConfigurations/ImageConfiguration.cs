using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Recommendations.Domain;

namespace Recommendations.Persistence.EntityTypeConfigurations;

public class ImageConfiguration : IEntityTypeConfiguration<Image>
{
    public void Configure(EntityTypeBuilder<Image> builder)
    {
        builder.HasKey(image => image.Id);
        builder.HasIndex(image => image.Id).IsUnique();

        builder.Property(image => image.FileName).IsRequired();
        builder.Property(image => image.Url).IsRequired();
        builder.Property(image => image.FolderName).IsRequired();
    }
}