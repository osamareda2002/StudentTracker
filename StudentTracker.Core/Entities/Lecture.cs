using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace StudentTracker.Core.Entities
{
    public class Lecture : BaseEntity
    {
        public string Day { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public DateTime Date { get; set; }
        public string RedundantType { get; set; }

        //Foreign Keys
        public string? ProfessorNationalId { get; set; }
        public int CourseId { get; set; }
        public int HallId { get; set; }
        public int DeptId { get; set; }

        //Navigation Properties
        public virtual Professor Professor { get; set; }
        public virtual Course Course { get; set; }
        public virtual Hall Hall { get; set; }
        public virtual ICollection<Student> Students { get; set; } = new HashSet<Student>();
        public virtual Department Department { get; set; }

    }
}
