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
    internal class CountryConfiguration : NamedEntityConfiguration<Country>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<Country> builder)
        {
            builder.Property(x => x.Name)
                   .HasMaxLength(100);

            builder.HasMany(x => x.Users)
                   .WithOne(x => x.Country)
                   .HasForeignKey(x => x.CountryId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
