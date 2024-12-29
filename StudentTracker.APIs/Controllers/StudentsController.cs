using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentTracker.APIs.DTOs.Course;
using StudentTracker.APIs.DTOs.Student;
using StudentTracker.APIs.Errors;
using StudentTracker.Core.Entities;
using StudentTracker.Core.Repositories.Contract;
using StudentTracker.Core.Specifications;
using StudentTracker.Repository.Data;

namespace StudentTracker.APIs.Controllers
{
    public class StudentsController : BaseApiController
    {
        #region Fields
            //The generic of Student database.
            private readonly IGenericRepository<Student> _studentRepo;
            private readonly IMapper _mapper;
            private readonly TrackerContext _trackerContext;
        #endregion
        #region Constructor
            public StudentsController(IGenericRepository<Student> studentRepo, IMapper mapper, TrackerContext trackerContext)
            {
                _studentRepo = studentRepo;
                _mapper = mapper;
                _trackerContext = trackerContext;
            }
        #endregion
        #region EndPoints
        [HttpGet("get-all")]
        public async Task<ActionResult> GetAllStudents()
        {
            var st = await _trackerContext.Students.ToListAsync();
            List<StudentToReturnDto> students = new List<StudentToReturnDto>();
            foreach (var s in st)
            {
                var student = _mapper.Map<StudentToReturnDto>(s);
                student.Level = _trackerContext.Levels.FirstOrDefault(l => l.Id == s.LevelId).Title;
                student.Department = _trackerContext.Departments.FirstOrDefault(d => d.Id == s.DeptId).DepartmentTitle;

                students.Add(student);
            }
            
            return Ok(students);
        }

        [HttpGet("Attendance-history")]
        public async Task<IActionResult> AttendanceHistory(string id)
        {
            var student = await _trackerContext.Students.FindAsync(id);
            if (student == null) return NotFound("student not found");
            var lectures = student.Lectures.ToList();
            if (!lectures.Any()) return Ok("No lectures attendent");
            List<AttendanceHistoryDto> AttendanceHistory = new List<AttendanceHistoryDto>();
            foreach (var lecture in lectures)
            {
                var course = await _trackerContext.Courses.FirstOrDefaultAsync(c => c.Id == lecture.CourseId);
                var professor = await _trackerContext.Professors.FirstOrDefaultAsync(p => p.NationalId == lecture.ProfessorNationalId);
                AttendanceHistory.Add(new AttendanceHistoryDto
                {
                    Course = course.Title,
                    Professor = professor.Name,
                    Date = lecture.Date,
                });
            }
            return Ok(AttendanceHistory);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddStudent(AddStudentDto dto)
        {
            var student = await _studentRepo.FirstOrDefaultAsync(s => s.NationalId == dto.NationalId);

            if (student != null)
                return BadRequest("National Id Registered Before");

            student = _mapper.Map<Student>(dto);
            var level = _trackerContext.Levels.FirstOrDefault(l => l.Title == dto.Level);
            var department = _trackerContext.Departments.FirstOrDefault(d => d.DepartmentTitle == dto.Department);
            if (level == null || department == null)
            {
                return NotFound("Level or Department Not Found");
            }
            student.LevelId = level.Id;
            student.DeptId = department.Id;

            _trackerContext.Add(student);
            await _trackerContext.SaveChangesAsync();
            return Ok("Student added successfully");
        }

        [HttpPost("add-mac-id")]
        public async Task<IActionResult> AddMacId(StudentMacIdDto dto)
        {
            var student = await _trackerContext.Students.FirstOrDefaultAsync(s=> s.NationalId==dto.NationalId);
            if(student ==null) return NotFound("Student Not Found");
            student.MacId = dto.MacId;

            _trackerContext.Students.Update(student);
            await _trackerContext.SaveChangesAsync();
            return Ok("Mac-Id registered successfuly");
            
        }

        [HttpPost("enroll-course")]
        public async Task<IActionResult> EnrollInCourse(CourseEnrollmentDto dto)
        {
            var student = await _trackerContext.Students.FirstOrDefaultAsync(s => s.NationalId == dto.StudentNationalId);
            if (student ==null) return NotFound("student not found");
            foreach (var item in dto.CoursesTitle)
            {
                var course = await _trackerContext.Courses.FirstOrDefaultAsync(c=>c.Title == item);
                if (course == null) return BadRequest($"{item} not found");
                if (student.Courses.Contains(course)) return BadRequest($"student has enrolled {item} already");
                student.Courses.Add(course);
            }

            _trackerContext.Update(student);
            await _trackerContext.SaveChangesAsync();
            return Ok("Courses added successfully");
        }
        [HttpPost("suspend")]
        public async Task<IActionResult> SuspensionToggle(string id)
        {
            var student = _trackerContext.Students.FirstOrDefault(s => s.NationalId == id);
            if (student == null) return NotFound("student not found");
            student.Suspended = !student.Suspended;
            _trackerContext.Update(student);
            await _trackerContext.SaveChangesAsync();
            return Ok($"Student Suspension : {student.Suspended}");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveStudent(string id)
        {
            var student = await _trackerContext.Students.FindAsync(id);
            if (student == null)
                return NotFound("Student Not Found");
            _trackerContext.Remove(student);
            await _trackerContext.SaveChangesAsync();
            return Ok("Student removed successfuly");
        }

        [HttpPost("add-face-id")] //add student face id
        public async Task<ActionResult> AddStudentPicUrl(StudentFaceIdDto studentFaceIdDto)
        {
            var student = await _studentRepo.FirstOrDefaultAsync(s => s.NationalId == studentFaceIdDto.id);
            if (student == null)
                return NotFound("Student not found.");


            student.FaceId = studentFaceIdDto.faceId;
            _studentRepo.Update(student);
            await _studentRepo.SaveChangesAsync();
            return Ok("Student Picture Face data Added.");
        }
        [HttpGet("get-face-id/{id}")] //get student face id using his ID
        public async Task<ActionResult> GetStudentPicUrl(string id)
        {
            var student = await _studentRepo.FirstOrDefaultAsync(s => s.NationalId == id);
            if (student == null)
            {
                return NotFound("Student with {id} not found.");

            }
            if (string.IsNullOrEmpty(student.FaceId))
            {
                return NotFound("No image URL available for this student.");
            }
            return Ok(student.FaceId);

        }
        #endregion
    }
}
