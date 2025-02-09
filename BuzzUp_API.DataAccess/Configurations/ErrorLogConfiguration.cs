﻿using BuzzUp_API.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuzzUp_API.DataAccess.Configurations
{
    internal class ErrorLogConfiguration : IEntityTypeConfiguration<ErrorLog>
    {
        public void Configure(EntityTypeBuilder<ErrorLog> builder)
        {
            builder
                .Property(x => x.Message)
                .IsRequired();
            builder
                .Property(x => x.StrackTrace)
                .IsRequired();

            builder
                .HasKey(x => x.ErrorId);
        }
    }
}
