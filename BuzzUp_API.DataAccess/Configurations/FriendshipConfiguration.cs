using BuzzUp_API.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuzzUp_API.DataAccess.Configurations
{
    internal class FriendshipConfiguration : EntityConfiguration<Friendship>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<Friendship> builder)
        {

            builder.HasOne(x => x.FriendRequestStatus)
                   .WithMany(x => x.Friendships)
                   .HasForeignKey(x => x.FriendRequestStatusId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Sender)
                   .WithMany(x => x.SentFriendships)
                   .HasForeignKey(x => x.SenderUserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Receiver)
                   .WithMany(x => x.ReceivedFriendships)
                   .HasForeignKey(x => x.ReceiverUserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => new { x.SenderUserId, x.ReceiverUserId });
        }
    }
}
