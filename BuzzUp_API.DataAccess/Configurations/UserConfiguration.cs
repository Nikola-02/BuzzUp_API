﻿using BuzzUp_API.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuzzUp_API.DataAccess.Configurations
{
    internal class UserConfiguration : EntityConfiguration<User>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<User> builder)
        {
            builder
                .Property(x => x.Username)
                .IsRequired()
                .HasMaxLength(30);

            builder
                .HasIndex(x => x.Username)
                .IsUnique();

            builder
                .Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(30);

            builder
                .HasIndex(x => x.FirstName);

            builder
                .Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(30);

            builder
                .HasIndex(x => x.LastName);

            builder
                .Property(x => x.Email)
                .HasMaxLength(50);

            builder
                .HasIndex(x => x.Email)
                .IsUnique();

            builder
                .Property(x => x.Password)
                .IsRequired()
                .HasMaxLength(120);

            builder
                .Property(x => x.Country)
                .IsRequired()
                .HasMaxLength(20);

            builder
                .Property(x => x.City)
                .IsRequired()
                .HasMaxLength(20);

            builder
                .Property(x => x.Workplace)
                .IsRequired()
                .HasMaxLength(20);

            builder
                .Property(x => x.University)
                .IsRequired()
                .HasMaxLength(20);
        }
    }
}
