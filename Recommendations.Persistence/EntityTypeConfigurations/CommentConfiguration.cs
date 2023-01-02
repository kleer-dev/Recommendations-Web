using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Recommendations.Domain;

namespace Recommendations.Persistence.EntityTypeConfigurations;

public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.HasKey(comment => comment.Id);
        builder.HasIndex(comment => comment.Id).IsUnique();

        builder.Property(comment => comment.Text)
            .HasMaxLength(400)
            .IsRequired();

        builder.Property(comment => comment.CreationDate).IsRequired();
    }
}