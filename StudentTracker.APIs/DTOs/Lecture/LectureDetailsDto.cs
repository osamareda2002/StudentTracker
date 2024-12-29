namespace StudentTracker.APIs.DTOs.Lecture
{
    public class LectureDetailsDto
    {
        public string Professor { get; set; }
        public DateTime Date { get; set; }
        public string Day { get; set; }
        public TimeSpan StartTime { get; set; }
        public string Hall { get; set; }
    }
}
