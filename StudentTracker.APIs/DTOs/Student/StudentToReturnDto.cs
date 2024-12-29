using StudentTracker.Core.Entities;

namespace StudentTracker.APIs.DTOs.Student
{
    public class StudentToReturnDto
    {
        public string NationalId { get; set; }
        public string Name { get; set; }
        public string Level { get; set; }
        public string Department { get; set; }
        public ICollection<string> Courses { get; set; }
    }
}
