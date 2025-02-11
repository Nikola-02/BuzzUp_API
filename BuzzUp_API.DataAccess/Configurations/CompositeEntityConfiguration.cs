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
    internal abstract class CompositeEntityConfiguration<T> : IEntityTypeConfiguration<T>
        where T : CompositeEntity
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(x => x.CreatedAt)
                  .HasDefaultValueSql("GETDATE()");

            builder.Property(x => x.IsActive).HasDefaultValue(true);

            ConfigureEntity(builder);
        }

        protected abstract void ConfigureEntity(EntityTypeBuilder<T> builder);
    }
}
