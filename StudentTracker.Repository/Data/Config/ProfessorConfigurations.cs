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
    internal class ProfessorConfigurations : IEntityTypeConfiguration<Professor>
    {
        public void Configure(EntityTypeBuilder<Professor> builder)
        {
            builder.Property(p => p.NationalId)
                .HasMaxLength(14)
                .IsFixedLength(true);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);


            //many-to-many Professor <-> course

            builder.HasMany(p => p.Courses)
                .WithMany(c => c.Professors)
                .UsingEntity(j => j.ToTable("Teaching"));


            // many - to - one Professor <-> department
            builder.HasOne(p => p.Department)
                .WithMany(d => d.Professors)
                .HasForeignKey(p => p.DeptId)
                .OnDelete(DeleteBehavior.NoAction); 
        }
    }
}
