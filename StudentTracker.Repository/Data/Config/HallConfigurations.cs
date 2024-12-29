using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentTracker.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTracker.Repository.Data.Config
{
    internal class HallConfigurations : IEntityTypeConfiguration<Hall>
    {
        public void Configure(EntityTypeBuilder<Hall> builder)
        {
            builder.Property(p => p.Name)
               .IsRequired()
                .HasMaxLength(15);

            builder.Property(p => p.Building)
                .IsRequired()
                .HasMaxLength(15);

    
        }
    }
}
