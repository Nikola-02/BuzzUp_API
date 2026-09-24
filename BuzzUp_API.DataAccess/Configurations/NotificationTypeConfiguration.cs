using BuzzUp_API.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuzzUp_API.DataAccess.Configurations
{
    internal class NotificationTypeConfiguration : NamedEntityConfiguration<NotificationType>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<NotificationType> builder)
        {
            builder.HasData(
                new NotificationType { Id = 1, Name = "FriendRequest", IsActive = true, CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new NotificationType { Id = 2, Name = "FriendAccepted", IsActive = true, CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new NotificationType { Id = 3, Name = "Comment", IsActive = true, CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new NotificationType { Id = 4, Name = "Reaction", IsActive = true, CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) }
            );
        }
    }
}
