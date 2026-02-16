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
    internal class PasswordResetTokenConfiguration : EntityConfiguration<PasswordResetToken>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<PasswordResetToken> builder)
        {
            builder.HasOne(x => x.User)
               .WithMany(u => u.PasswordResetTokens)
               .HasForeignKey(x => x.UserId)
               .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.Token)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(x => x.ExpiresAt)
                   .IsRequired();

            builder.Property(x => x.IsUsed)
                   .HasDefaultValue(false)
                   .IsRequired();
        }
    }
}
