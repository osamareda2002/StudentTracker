namespace StudentTracker.APIs.DTOs.Lecture
{
    public class LectureAttendanceDto
    {
        public int LectureId { get; set; }
        public string LectureName { get; set; }

        public bool Attended { get; set; }
    }
}
