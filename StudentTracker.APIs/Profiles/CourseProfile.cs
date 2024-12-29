using AutoMapper;
using StudentTracker.APIs.DTOs.Course;
using StudentTracker.Core.Entities;

namespace StudentTracker.APIs.Profiles
{
    public class CourseProfile : Profile
    {
        public CourseProfile()
        {
            CreateMap<AddCourseDto, Course>()
                .ForMember(dest => dest.Department, opt => opt.Ignore())
                .ForMember(dest => dest.Level, opt => opt.Ignore());


            CreateMap<Course, CourseReturnDto>();
        }
    }
}
