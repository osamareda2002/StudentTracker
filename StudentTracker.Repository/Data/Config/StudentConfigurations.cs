using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentTracker.Core.Entities;

namespace StudentTracker.Repository.Data.Config
{
    internal class StudentConfigurations : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.Property(p => p.NationalId)
               .HasMaxLength(14)
               .IsFixedLength(true);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Group)
                .IsRequired()
                .HasMaxLength(15);

            //many-to-many student <-> course

            builder.HasMany(s => s.Courses)
                .WithMany(c => c.Students)
                .UsingEntity(j => j.ToTable("Enrollments"));

            //many-to-many student <-> lecture

            builder.HasMany(s => s.Lectures)
                .WithMany(l => l.Students)
                .UsingEntity(j => j.ToTable("Attendances"));


            // many - to - one student <-> department
            builder.HasOne(s => s.Department)
                .WithMany(d => d.Students)
                .HasForeignKey(s => s.DeptId)
                .OnDelete(DeleteBehavior.Restrict); // prevents deletion of a department if it has associated students


            // many - to - one student <-> level
            builder.HasOne(s => s.Level)
                .WithMany(l => l.Students)
                .HasForeignKey(s => s.LevelId)
                .OnDelete(DeleteBehavior.Restrict); // prevent deletion of a level if it has associated students
        }
    }
}
