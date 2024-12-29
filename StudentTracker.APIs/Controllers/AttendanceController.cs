using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentTracker.APIs.DTOs;
using StudentTracker.Core.Entities;
using StudentTracker.Core.Repositories.Contract;
using StudentTracker.Core.Specifications;

namespace StudentTracker.APIs.Controllers
{
    public class AttendanceController : BaseApiController
    {
        private readonly IGenericRepository<Student> _studentRepo;
        private readonly IGenericRepository<Lecture> _lectureRepo;
        public AttendanceController(IGenericRepository<Student> studentRepo, IGenericRepository<Lecture> lectureRepo) 
        {
            _studentRepo = studentRepo;
            _lectureRepo = lectureRepo;
        }
        [HttpPost("add-attendence")] // add the student attendance 
        public async Task<ActionResult>AddAttendance(AttendanceDto attendanceDto)
        {//
            var spec = new StudentSpecifications(attendanceDto.StudentId);
            var student = await _studentRepo.GetWithSpecAsync(spec);

            var lecture = await _lectureRepo.GetAsync(attendanceDto.LectureId); 
            if(student == null) 
            {
                return NotFound("Student Id not found.");
            }
            if(lecture == null)
            {
                return NotFound("Lecture Id not found."); 
            }
            
            student.Lectures.Add(lecture);
            await _lectureRepo.SaveChangesAsync();
            

            return Ok("Attendence Recorded!");
        }
    }
}
