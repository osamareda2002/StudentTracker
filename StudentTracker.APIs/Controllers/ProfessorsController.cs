using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentTracker.APIs.DTOs.Professor;
using StudentTracker.Core.Entities;
using StudentTracker.Repository.Data;

namespace StudentTracker.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessorsController : ControllerBase
    {
        #region Fields
        private readonly TrackerContext _trackerContext;
        #endregion
        #region Constructor
        public ProfessorsController(TrackerContext trackerContext)
        {
            _trackerContext = trackerContext;
        }
        #endregion
        #region Endpoints
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var profressors = await _trackerContext.Professors.ToListAsync();
            List<ProfessorToReturnDto> professorsList = new List<ProfessorToReturnDto>();
            foreach (var prof in profressors)
            {
                var professordto = new ProfessorToReturnDto
                {
                    Name = prof.Name,
                    NationalId = prof.NationalId,
                    Department = _trackerContext.Departments.FirstOrDefault(d => d.Id == prof.DeptId).DepartmentTitle
                };
                foreach (var course in prof.Courses)
                {
                    professordto.RegisteredSubjects.Add(course.Title);
                }
                professorsList.Add(professordto);
            }
            return Ok(professorsList);
        }

        [HttpPost("add-professor")]
        public async Task<IActionResult> AddProfessor(ProfessorToReturnDto dto)
        {
            if (_trackerContext.Professors.FirstOrDefault(p => p.NationalId == dto.NationalId) != null) 
                return BadRequest($"Professor with Id : {dto.NationalId} registered already");
            var professor = new Professor
            {
                Name= dto.Name,
                NationalId= dto.NationalId,
                DeptId = _trackerContext.Departments.FirstOrDefault(d => d.DepartmentTitle == dto.Department).Id
            };
            foreach (var item in dto.RegisteredSubjects)
            {
                var course = _trackerContext.Courses.FirstOrDefault(c => c.Title == item);
                professor.Courses.Add(course);
            }
            _trackerContext.Add(professor);
            await _trackerContext.SaveChangesAsync();
            return Ok("professor added successfuly");
        }

        [HttpPost("add-course")]
        public async Task<IActionResult>AddCourse(ProfessorCoursesDto dto)
        {
            var professor =  _trackerContext.Professors.FirstOrDefault(p => p.NationalId == dto.NationalId);
            if (professor == null) return BadRequest($"There is no professor with this Id : {dto.NationalId}");
            foreach (var item in dto.courses)
            {
                var course = _trackerContext.Courses.FirstOrDefault(c => c.Title == item);
                if (course == null) return BadRequest($"There is no course with this Title : {item}");
                if (professor.Courses.Contains(course)) return BadRequest($"this course is already registered : {course.Title}");
                professor.Courses.Add(course);
            }
            _trackerContext.Update(professor);
            await _trackerContext.SaveChangesAsync();
            return Ok("Successfully registeration");
        }

        [HttpDelete("remove-course")]
        public async Task<IActionResult>RemoveCourse(ProfessorCoursesDto dto)
        {
            var professor = _trackerContext.Professors.FirstOrDefault(p => p.NationalId == dto.NationalId);
            if (professor == null) return BadRequest($"There is no professor with this Id : {dto.NationalId}");
            foreach (var item in dto.courses)
            {
                var course = _trackerContext.Courses.FirstOrDefault(c => c.Title == item);
                if (course == null) return BadRequest($"There is no course with this Title : {item}");
                if (!professor.Courses.Contains(course)) return BadRequest($"{course.Title} isn't registered by {professor}");
                professor.Courses.Remove(course);
            }
            _trackerContext.Update(professor);
            await _trackerContext.SaveChangesAsync();
            return Ok("Course removed successfuly");
        }

        #endregion
    }
}
