using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentTracker.APIs.DTOs;
using StudentTracker.APIs.Errors;
using StudentTracker.Core.Entities;
using StudentTracker.Core.Repositories.Contract;

namespace StudentTracker.APIs.Controllers
{
    public class HallNutsController : BaseApiController
    {
        private readonly IGenericRepository<Hall> _hallRepo;
        private readonly IMapper _mapper;

        public HallNutsController(IGenericRepository<Hall> hallRepo, IMapper mapper) 
        {
            _hallRepo = hallRepo;
            _mapper = mapper;
        }
        [HttpGet("{id}")] // get hall nuts giving the hall id
        public async Task<ActionResult> GetHallNuts(int id)
        {
            var hall = await _hallRepo.GetAsync(id);
            if (hall == null)
            {
                return NotFound(new ApiResponse(404));
            }
            return Ok (_mapper.Map<HallNutsDto>(hall));
        }
    }
}
