using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTracker.Core.Entities
{
    public class Department : BaseEntity
    {
        public string DepartmentTitle { get; set; }



        // Navigation Properties
        public virtual ICollection<Student> Students { get; set; } = new HashSet<Student>();
        public virtual ICollection<Lecture> Lectures { get; set; } = new HashSet<Lecture>();
        public virtual ICollection<Course> Courses { get; set; } = new HashSet<Course>();
        public virtual ICollection<Professor> Professors { get; set; } = new HashSet<Professor>();
    }
}
