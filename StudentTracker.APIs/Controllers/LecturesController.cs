using AutoMapper;
using Castle.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentTracker.APIs.DTOs.Lecture;
using StudentTracker.Core.Entities;
using StudentTracker.Core.Repositories.Contract;
using StudentTracker.Core.Specifications;
using StudentTracker.Repository.Data;

namespace StudentTracker.APIs.Controllers
{
    public class LecturesController : BaseApiController
    {

        #region Fields
        private readonly IGenericRepository<Lecture> _lectureRepo;
        private readonly IMapper _mapper;
        private readonly TrackerContext _trackerContext;
        #endregion

        #region Constructor
        public LecturesController(IGenericRepository<Lecture> lectureRepo, IMapper mapper, TrackerContext trackerContext)
        {
            _lectureRepo = lectureRepo;
            _mapper = mapper;
            _trackerContext = trackerContext;
        }
        #endregion

        #region EndPoints
        [HttpGet("{id}")] // get student lectures of the courses he's enrolled in day by day
        public async Task<ActionResult<IEnumerable<LectureToReturnDto>>> GetStudentLectures(string id)
        {
            string today = DateTime.Now.DayOfWeek.ToString().ToLower();

            var spec = new LectureSpecifications(id, today);
            var lectures = await _lectureRepo.GetAllWithSpecAsync(spec);
            if (!lectures.Any())
                return NotFound("No lectures found for this student");

            return Ok(_mapper.Map<IEnumerable<Lecture>, IEnumerable<LectureToReturnDto>>(lectures));
        }

        [HttpGet]
        [Route("all")] // Get all lectures
        public async Task<IActionResult> GetLectures()
        {
            return Ok(await _trackerContext.lectures.ToListAsync());
        }

        [HttpGet]
        [Route("current")] // Get current lectures
        public async Task<IActionResult> GetCurrentLectures()
        { 
            var now = DateTime.Now.TimeOfDay;
            var today = DateTime.Now.DayOfWeek.ToString().ToLower();
            var lectures = await _trackerContext.lectures.Where(l => (l.Date == DateTime.Today.Date && l.StartTime <= now && l.EndTime >= now) || l.Date == null && l.Day ==today && l.StartTime <= now && l.EndTime >= now).ToListAsync();
            
            List<LectureToReturnDto> CurrentLectures = new List<LectureToReturnDto>();
            foreach (var l in lectures)
            {
                var course = _trackerContext.Courses.FirstOrDefault(c => c.Id == l.CourseId);
                CurrentLectures.Add(new LectureToReturnDto
                {
                    CourseTitle = _trackerContext.Courses.FirstOrDefault(c => c.Id == l.CourseId).Title,
                    ProfessorName = _trackerContext.Professors.FirstOrDefault(p => p.NationalId == l.ProfessorNationalId).Name,
                    DepartmentTitle = _trackerContext.Departments.FirstOrDefault(d => d.Id == l.DeptId).DepartmentTitle,
                    HallName = _trackerContext.Halls.FirstOrDefault(h => h.Id == l.HallId).Name,
                    LevelTitle = course.Level.Title,
                    TimeRemaining = l.EndTime - now
                });
            }

            if (CurrentLectures.Any()) return Ok(CurrentLectures);
            return Ok("There are no current lectures.");
        }

        [HttpGet("Attendance-log")]
        public async Task<IActionResult> LectureAttendance(int LectureId)
        {
            var lecture = _trackerContext.lectures.FirstOrDefault(l => l.Id == LectureId);
            if (lecture == null) return BadRequest($"There is no lecture with Id : {LectureId}");
            List<StudentAttendanceDto> names = new List<StudentAttendanceDto>();
            foreach (var item in lecture.Course.Students)
            {
                names.Add(new StudentAttendanceDto
                {
                    Name = item.Name,
                    HasAttended = item.Lectures.Contains(lecture),
                });
            }

            return Ok(new AttendanceLogDto
            {
                CourseTitle = lecture.Course.Title,
                ProfessorName = lecture.Professor.Name,
                Date = lecture.Date,
                Day = lecture.Day,
                StartTime = lecture.StartTime,
                Hall = lecture.Hall.Name,
                TotalRegistrants = lecture.Course.Students.Count,
                NumberOfAttendees = lecture.Students.Count,
                NumberOfAbsentees = (lecture.Course.Students.Count) - (lecture.Students.Count),
                AttendanceRate = (decimal)(lecture.Students.Count) / (lecture.Course.Students.Count),
                StudentsNames = names,
            });
        }

        [HttpPost("irregular")] // Add New Irregular Lecture
        public async Task<IActionResult> AddIrregularLecture(AddIrregularLectureDto dto)
        {
            var lecture = MapIrregularLecture(dto);
            if (lecture == null)
                return NotFound("Either Course,Professor, Department, Hall not found");

            _trackerContext.lectures.Add(lecture);
            await _trackerContext.SaveChangesAsync();
            return Ok("Lecture added successfuly");
        }

        [HttpPost("regular")] // Add New Regular Lecture
        public async Task<IActionResult> AddRegularLecture(AddRegularLectureDto dto)
        {
            var lecture = MapRegularLecture(dto);
            if (lecture == null) 
                return NotFound("Either Course,Professor, Department, Hall not found");

            _trackerContext.lectures.Add(lecture);
            await _trackerContext.SaveChangesAsync();
            return Ok("Lecture added successfuly");
        }
        #endregion

        private Lecture MapRegularLecture(AddRegularLectureDto dto)
        {
            var lecture = _mapper.Map<Lecture>(dto);
            var course = _trackerContext.Courses.FirstOrDefault(c => c.Title == dto.Course);
            var professor = _trackerContext.Professors.FirstOrDefault(p => p.Name == dto.Professor);
            var department = _trackerContext.Departments.FirstOrDefault(d => d.DepartmentTitle == dto.Department);
            var hall = _trackerContext.Halls.FirstOrDefault(h => h.Name == dto.Hall);

            if (lecture == null || course == null || professor == null || department == null)
                return null;

            lecture.CourseId = course.Id;
            lecture.ProfessorNationalId = professor.NationalId;
            lecture.DeptId = department.Id;
            lecture.HallId = hall.Id;
            lecture.Date = DateTime.Today.Date;
            return (lecture);
        }

        private Lecture MapIrregularLecture(AddIrregularLectureDto dto)
        {
            var lecture = _mapper.Map<Lecture>(dto);
            var course = _trackerContext.Courses.FirstOrDefault(c => c.Title == dto.Course);
            var professor = _trackerContext.Professors.FirstOrDefault(p => p.Name == dto.Professor);
            var department = _trackerContext.Departments.FirstOrDefault(d => d.DepartmentTitle == dto.Department);
            var hall = _trackerContext.Halls.FirstOrDefault(h => h.Name == dto.Hall);

            if (lecture == null || course == null || professor == null || department == null)
                return null;

            lecture.CourseId = course.Id;
            lecture.ProfessorNationalId = professor.NationalId;
            lecture.DeptId = department.Id;
            lecture.HallId = hall.Id;
            lecture.Day = DateTime.Today.DayOfWeek.ToString().ToLower();

            return (lecture);
        }
        
    }
}
