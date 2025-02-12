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
    internal class UserChatConfiguration : CompositeEntityConfiguration<UserChat>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<UserChat> builder)
        {
            builder.HasKey(x => new { x.UserId, x.ChatId });

            builder.HasOne(x => x.User)
                   .WithMany(x => x.UserChats)
                   .HasForeignKey(x => x.UserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Chat)
                   .WithMany(x => x.UserChats)
                   .HasForeignKey(x => x.ChatId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
