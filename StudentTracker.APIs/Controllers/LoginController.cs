using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentTracker.APIs.DTOs;
using StudentTracker.APIs.Errors;
using StudentTracker.Core.Entities;
using StudentTracker.Core.Repositories.Contract;

namespace StudentTracker.APIs.Controllers
{
    public class LoginController : BaseApiController
    {
        private readonly IGenericRepository<Student> _studentRepo;

        public LoginController(IGenericRepository<Student> studentRepo)
        {
            _studentRepo = studentRepo;
        }
        [HttpPost] //login validation
        public async Task<ActionResult> Login(LoginDto loginDto)
        {
            var student = await _studentRepo.FirstOrDefaultAsync(s => s.NationalId == loginDto.NationalId);
            if (student == null)
            {
                return NotFound(new ApiResponse(404));
            }
            if (!string.IsNullOrEmpty(student.MacId))
            {
                if (student.MacId == loginDto.MacId)
                {
                    return Ok("Login successful.");
                }
                else
                {
                    return BadRequest("This National ID is already registered with another device.");
                }
            }

            student.MacId = loginDto.MacId;
            _studentRepo.Update(student);
            await _studentRepo.SaveChangesAsync();
            return Ok("Login succeful, MAC address Registered.");
        }
    }
}
