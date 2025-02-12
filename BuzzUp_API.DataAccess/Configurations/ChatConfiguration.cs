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
    internal class ChatConfiguration : EntityConfiguration<Chat>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<Chat> builder)
        {
            builder.HasMany(x => x.UserChats)
               .WithOne(x => x.Chat)
               .HasForeignKey(x => x.ChatId)
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
