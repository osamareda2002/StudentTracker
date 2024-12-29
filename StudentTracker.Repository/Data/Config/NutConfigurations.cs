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
    internal class NutConfigurations: IEntityTypeConfiguration<Nut>
    {
       public void Configure(EntityTypeBuilder<Nut> builder)
        {
            builder.Property(n => n.Name)
            .IsRequired()
            .HasMaxLength(5);

            builder.Property(n => n.SerialNumber)
           .IsRequired()
           .HasMaxLength(50);

            // many - to - one Nut <->hall
            builder.HasOne(n => n.Hall)
                .WithMany(h => h.Nuts)
                .HasForeignKey(n => n.HallId)
                .OnDelete(DeleteBehavior.Cascade); // Automatically delete Nuts if Hall is deleted
        }
    }
}
