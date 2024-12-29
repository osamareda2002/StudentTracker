namespace StudentTracker.APIs.DTOs.Professor
{
    public class ProfessorCoursesDto
    {
        public string NationalId { get; set; }
        public List<string> courses { get; set; } = new List<string>();
    }
}
