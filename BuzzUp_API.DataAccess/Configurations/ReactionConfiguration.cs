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
    internal class ReactionConfiguration : CompositeEntityConfiguration<Reaction>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<Reaction> builder)
        {
            builder
                .HasKey(x => new
                {
                    x.PostId,
                    x.UserId
                });

            builder.HasOne(x=>x.User)
                   .WithMany(x=>x.Reactions)
                   .HasForeignKey(x => x.UserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Post)
                   .WithMany(x => x.Reactions)
                   .HasForeignKey(x => x.PostId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.ReactionType)
                   .WithMany(x => x.Reactions)
                   .HasForeignKey(x => x.ReactionTypeId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
