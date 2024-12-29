using AutoMapper;
using AutoMapper.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentTracker.APIs.DTOs.Course;
using StudentTracker.Core.Entities;
using StudentTracker.Core.Repositories.Contract;
using StudentTracker.Core.Specifications;
using StudentTracker.Repository.Data;

namespace StudentTracker.APIs.Controllers
{
    public class AttendanceRecordsController : BaseApiController
    {
        private readonly IGenericRepository<Course> _courseRepo;
        private readonly IMapper _mapper;

        public AttendanceRecordsController(IGenericRepository<Course> courseRepo, IMapper mapper)
        {
            _courseRepo = courseRepo;
            _mapper = mapper;
        }
        [HttpGet("{id}/lectures")]  // get the lectures attendence recordes of all courses the student is enrolled in 
        public async Task<ActionResult<IEnumerable<CourseLecturesDto>>> GetCoursesLecturesForStudent(string id) 
        {
            var spec = new CoursesWithLecturesForStudentSpecification(id);
            var courses = await _courseRepo.GetAllWithSpecAsync(spec);
            if(!courses.Any()) 
            { 
               return NotFound("Student Not Enrolled In Any Courses.");
            }

            var coursesDto = _mapper.Map<List<CourseLecturesDto>>(courses, opts => {
                opts.Items["studentId"] = id; 
            });

            return Ok(coursesDto);
        }


    }
}
