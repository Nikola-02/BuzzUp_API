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
    internal class PostMediaConfiguration : EntityConfiguration<PostMedia>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<PostMedia> builder)
        {
            builder.Property(x => x.Path)
                .IsRequired();

            builder.HasOne(x => x.PostMediaType)
                .WithMany(x => x.PostMedias)
                .HasForeignKey(x => x.PostMediaTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Post)
                .WithMany(x => x.PostMedias)
                .HasForeignKey(x => x.PostId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
