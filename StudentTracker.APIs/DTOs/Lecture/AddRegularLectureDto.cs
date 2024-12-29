namespace StudentTracker.APIs.DTOs.Lecture
{
    public class AddRegularLectureDto
    {
        public string Day { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string RedundantType { get; set; }

        //Foreign Keys
        public string Professor { get; set; }
        public string Course { get; set; }
        public string Hall { get; set; }
        public string Department { get; set; }
    }
}
