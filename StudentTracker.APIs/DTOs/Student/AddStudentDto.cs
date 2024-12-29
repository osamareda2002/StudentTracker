namespace StudentTracker.APIs.DTOs.Student
{
    public class AddStudentDto
    {
        public string NationalId { get; set; }
        public string Name { get; set; }
        public string Group { get; set; }
        public string FaceId { get; set; }
        public string? MacId { get; set; }


        //Foreign Keys
        public string Department { get; set; }
        public string Level { get; set; }

    }
}
