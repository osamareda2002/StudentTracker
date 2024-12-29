using Castle.Core;

namespace StudentTracker.APIs.DTOs.Lecture
{
    public class AttendanceLogDto
    {
        public string CourseTitle { get; set; }
        public string ProfessorName { get; set; }
        public DateTime Date { get; set; }
        public string Day { get; set; }
        public TimeSpan StartTime { get; set; }
        public string Hall { get; set; }
        public int TotalRegistrants { get; set; }
        public int NumberOfAttendees { get; set; }
        public int NumberOfAbsentees { get; set; }
        public decimal AttendanceRate { get; set; }
        public List<StudentAttendanceDto> StudentsNames { get; set; } = new List<StudentAttendanceDto>();
    }

    public class StudentAttendanceDto
    {
        public string Name { get; set; }
        public bool HasAttended { get; set; }
    }
}
