using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentTracker.APIs.DTOs.Course;
using StudentTracker.APIs.DTOs.Lecture;
using StudentTracker.Core.Entities;
using StudentTracker.Repository.Data;

namespace StudentTracker.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        #region Fields
        private readonly TrackerContext _trackerContext;
        private readonly IMapper _mapper;
        #endregion
        #region Constructor
        public CoursesController(TrackerContext trackerContext, IMapper mapper)
        {
            _trackerContext = trackerContext;
            _mapper = mapper;
        }
        #endregion
        #region EndPoints
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllCourses()
        {
            var allCourses = await _trackerContext.Courses.ToListAsync();

            var courses = _mapper.Map<List<CourseReturnDto>>(allCourses);
            return Ok(courses);
        }
        
        [HttpGet("lectures-log")]
        public async Task<IActionResult>CourseLectures(string code)
        {
            var course = _trackerContext.Courses.FirstOrDefault(c => c.Code == code);
            if (course == null) return BadRequest($"There is no course with this code {code}");
            List<LectureDetailsDto> log = new List<LectureDetailsDto>();
            foreach (var item in course.Lectures)
            {
                log.Add(new LectureDetailsDto
                {
                    Professor = item.Professor.Name,
                    Date = item.Date,
                    Day = item.Day,
                    StartTime = item.StartTime,
                    Hall = item.Hall.Name,
                });
            }
            return Ok(log);
        }

        [HttpPost("add")] // Add New Course
        public async Task<IActionResult> AddCourse(AddCourseDto dto)
        {
            var course = await _trackerContext.Courses.FirstOrDefaultAsync(c=> c.Title == dto.Title);
            if (course != null)
                return BadRequest("This Course has been added before");

            course = _mapper.Map<Course>(dto);
            course.Department = _trackerContext.Departments.FirstOrDefault(d => d.DepartmentTitle == dto.Department);
            course.Level = _trackerContext.Levels.FirstOrDefault(l => l.Title == dto.level);

            _trackerContext.Courses.Add(course);
            await _trackerContext.SaveChangesAsync();
            return Ok("Course added successfully");
        }
        #endregion
        


    }
}
