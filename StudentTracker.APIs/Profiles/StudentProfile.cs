using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentTracker.APIs.DTOs.Student;
using StudentTracker.Core.Entities;
namespace StudentTracker.Repository.Data.Profiles
{
    public class StudentProfile : Profile
    {

        public StudentProfile()
        {
            CreateMap<AddStudentDto, Student>()
                .ForMember(dest => dest.Department, opt => opt.Ignore())
                .ForMember(dest => dest.Level, opt => opt.Ignore());

            CreateMap<Student, StudentToReturnDto>()
                .ForMember(dest => dest.Department, opt => opt.Ignore())
                .ForMember(dest => dest.Level, opt => opt.Ignore());
        }
    }
}
