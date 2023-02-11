using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Recommendations.Application.Common.Constants;
using Recommendations.Domain;

namespace Recommendations.Persistence.EntityTypeConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(user => user.Id);
        builder.HasIndex(user => user.Id).IsUnique();

        builder.Property(user => user.LikesCount)
            .HasDefaultValue(0);

        builder.Property(user => user.AccessStatus)
            .HasDefaultValue(UserAccessStatuses.Unblocked);
    }
}