using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTracker.Core.Entities
{
    public class Course : BaseEntity
    {
        public string Code { get; set; }
        public string Title { get; set; }


        // Foreign Keys
        
        public int DeptId { get; set; }
        public int LevelId { get; set; }


        //Navigation Properties
        public virtual ICollection<Student> Students { get; set; } = new HashSet<Student>();
        public virtual ICollection<Professor> Professors { get; set; } = new HashSet<Professor>();
        public virtual ICollection<Lecture> Lectures { get; set; } = new HashSet<Lecture>();
        [ForeignKey("DeptId")]
        public virtual Department Department { get; set; }
        public virtual Level Level { get; set; }
    }
}
