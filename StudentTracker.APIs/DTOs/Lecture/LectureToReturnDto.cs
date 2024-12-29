namespace StudentTracker.APIs.DTOs.Lecture
{
    public class LectureToReturnDto
    {
        public string CourseTitle { get; set; }
        public string ProfessorName { get; set; }
        public string DepartmentTitle { get; set; }
        public string LevelTitle { get; set; }
        public TimeSpan TimeRemaining { get; set; }
        public string HallName { get; set; }

    }
}
