using BuzzUp_API.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuzzUp_API.DataAccess.Configurations
{
    internal class SavedPostConfiguration : CompositeEntityConfiguration<SavedPost>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<SavedPost> builder)
        {
            builder.HasKey(sp => new { sp.UserId, sp.PostId });
        }
    }
}
