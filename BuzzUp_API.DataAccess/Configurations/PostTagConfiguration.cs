using BuzzUp_API.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Emit;

namespace BuzzUp_API.DataAccess.Configurations
{
    internal class PostTagConfiguration : CompositeEntityConfiguration<PostTag>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<PostTag> builder)
        {
            builder.HasKey(x => new
            {
                x.PostId,
                x.TagId
            });
        }
    }
}
