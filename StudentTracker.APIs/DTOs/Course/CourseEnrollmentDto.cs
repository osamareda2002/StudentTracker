namespace StudentTracker.APIs.DTOs.Course
{
    public class CourseEnrollmentDto
    {
        public string StudentNationalId { get; set; }
        public List<string> CoursesTitle { get; set; }

    }
}
