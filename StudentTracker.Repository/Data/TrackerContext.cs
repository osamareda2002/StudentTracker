using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using StudentTracker.Core.Entities;
using System.Reflection;

namespace StudentTracker.Repository.Data
{
    public class TrackerContext:DbContext
    {
        public TrackerContext(DbContextOptions<TrackerContext>options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Professor> Professors { get; set; }
        public DbSet<Lecture> lectures { get; set; }
        public DbSet<Hall> Halls { get; set; }
        public DbSet<PortalUser> PortalUsers { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Level> Levels { get; set; }

    }
}
