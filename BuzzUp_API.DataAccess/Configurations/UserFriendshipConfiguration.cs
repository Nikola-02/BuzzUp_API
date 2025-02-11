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
    internal class UserFriendshipConfiguration : CompositeEntityConfiguration<UserFriendship>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<UserFriendship> builder)
        {
            builder.HasKey(uf => new { uf.UserId, uf.FriendshipId });

            builder.HasOne(uf => uf.User)
                   .WithMany(u => u.Friendships)
                   .HasForeignKey(uf => uf.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(uf => uf.Friendship)
                   .WithMany(f => f.Users)
                   .HasForeignKey(uf => uf.FriendshipId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
