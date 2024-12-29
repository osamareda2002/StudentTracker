namespace StudentTracker.APIs.DTOs.Professor
{
    public class ProfessorToReturnDto
    {
        public string NationalId { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public List<string> RegisteredSubjects { get; set; } = new List<string>();
    }
}
