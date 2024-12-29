using Microsoft.EntityFrameworkCore;
using StudentTracker.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StudentTracker.Repository.Data.Config
{
    internal class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.Property(d => d.DepartmentTitle).IsRequired();


            
        }
    }
}
