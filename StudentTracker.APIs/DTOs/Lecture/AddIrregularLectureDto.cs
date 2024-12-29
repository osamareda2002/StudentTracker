namespace StudentTracker.APIs.DTOs.Lecture
{
    public class AddIrregularLectureDto
    {
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public DateTime Date { get; set; }

        //Foreign Keys
        public string Professor { get; set; }
        public string Course { get; set; }
        public string Hall { get; set; }
        public string Department { get; set; }
    }
}
