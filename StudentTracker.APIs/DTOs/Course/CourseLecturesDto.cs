using StudentTracker.APIs.DTOs.Lecture;

namespace StudentTracker.APIs.DTOs.Course
{
    public class CourseLecturesDto
    {
        public string CourseTitle { get; set; }
        public List<LectureAttendanceDto> Lectures { get; set; }
    }
}
