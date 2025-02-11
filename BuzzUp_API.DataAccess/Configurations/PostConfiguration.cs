using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuzzUp_API.Domain;

namespace BuzzUp_API.DataAccess.Configurations
{
    internal class PostConfiguration : EntityConfiguration<Post>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<Post> builder)
        {
            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(30);

            builder
                .HasIndex(x => new { x.Title, x.IsActive });

            builder.Property(x => x.Description)
                .HasMaxLength(50);

            builder.Property(x => x.Location)
                .HasMaxLength(50);

            builder
                .HasIndex(x => new { x.Location, x.IsActive });

            builder.HasOne(x => x.VisibilityType)
                .WithMany(x => x.Posts)
                .HasForeignKey(x => x.VisibilityTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.FeelingType)
                .WithMany(x => x.Posts)
                .HasForeignKey(x => x.FeelingTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            //Tags
            builder.HasMany(x => x.Tags)
                .WithMany(x => x.Posts)
                .UsingEntity<PostTag>();

            //builder.HasMany(x => x.Likes)
            //    .WithOne(x => x.Track)
            //    .HasForeignKey(x => x.TrackId);

            //builder.HasMany(x => x.Playlists)
            //    .WithMany(x => x.Tracks)
            //    .UsingEntity<PlaylistTrack>();
        }
    }
}
