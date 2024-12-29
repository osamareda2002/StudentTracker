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
    internal class PortalUserConfigurations:IEntityTypeConfiguration<PortalUser>
    {
        public void Configure (EntityTypeBuilder<PortalUser>builder)
        {
            builder.Property(u => u.NationalId)
           .IsRequired()
           .HasMaxLength(14)
           .IsFixedLength(true);

            builder.Property(u => u.Name)
            .IsRequired()
            .HasMaxLength(100);

            builder.Property(u => u.Password)
            .IsRequired()
            .HasMaxLength(255); //for hashed pass

            builder.Property(u => u.UserName)
            .IsRequired()
            .HasMaxLength(100);
            // many - to - many portalUser <->Permission

            builder.HasMany(u => u.Permissions)
                .WithMany(p => p.PortalUsers)
                .UsingEntity(j => j.ToTable("UserPermissions"));

        }
    }
}
