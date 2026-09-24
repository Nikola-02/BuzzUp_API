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
    internal class NotificationConfiguration : EntityConfiguration<Notification>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<Notification> builder)
        {
            builder.Property(x => x.IsRead)
                   .HasDefaultValue(false);

            builder.HasOne(x => x.Recipient)
                   .WithMany(x => x.ReceivedNotifications)
                   .HasForeignKey(x => x.RecipientUserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Actor)
                   .WithMany(x => x.SentNotifications)
                   .HasForeignKey(x => x.ActorUserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.NotificationType)
                   .WithMany(x => x.Notifications)
                   .HasForeignKey(x => x.NotificationTypeId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Post)
                   .WithMany(x => x.Notifications)
                   .HasForeignKey(x => x.PostId)
                   .IsRequired(false)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.ReactionType)
                   .WithMany(x => x.Notifications)
                   .HasForeignKey(x => x.ReactionTypeId)
                   .IsRequired(false)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => new { x.RecipientUserId, x.IsRead });
        }
    }
}
