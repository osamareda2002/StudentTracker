using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTracker.Core.Entities
{
    public class Student
    {
        [Key]
        public string NationalId { get; set; }
        public string Name { get; set; }
        public string Group { get; set; }
        public string FaceId { get; set; }
        public string? MacId { get; set; }
        public bool Suspended { get; set; } = false;

        //Foreign Keys
        public int DeptId { get; set; }
        public int LevelId { get; set; }


        //Navigation Properties
        public virtual ICollection<Course> Courses { get; set; } = new HashSet<Course>();
        public virtual ICollection<Lecture> Lectures { get; set; } = new HashSet<Lecture>();
        public virtual Department Department { get; set; }
        public virtual Level Level { get; set; }


    }
}
