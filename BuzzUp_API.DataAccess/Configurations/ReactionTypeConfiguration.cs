using BuzzUp_API.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuzzUp_API.DataAccess.Configurations
{
    internal class ReactionTypeConfiguration : NamedEntityConfiguration<ReactionType>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<ReactionType> builder)
        {
            builder
                .Property(x => x.Icon)
                .IsRequired()
                .HasMaxLength(20);
        }
    }
}
