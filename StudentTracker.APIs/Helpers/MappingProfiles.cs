using AutoMapper;
using AutoMapper.Internal;
using StudentTracker.APIs.DTOs.Course;
using StudentTracker.APIs.DTOs.Lecture;
using StudentTracker.APIs.DTOs.Student;
using StudentTracker.Core.Entities;

namespace StudentTracker.APIs.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() 
        {
            CreateMap<Lecture, LectureToReturnDto>()
                .ForMember(dest => dest.ProfessorName, opt => opt.MapFrom(src => src.Professor.Name))
                .ForMember(dest => dest.CourseTitle, opt => opt.MapFrom(src => src.Course.Title))
                .ForMember(dest => dest.HallName, opt => opt.MapFrom(src => src.Hall.Name));

            CreateMap<Student, StudentToReturnDto>()
                .ForMember(dest => dest.Courses, opt => opt.MapFrom(src => src.Courses.Select(c => c.Title)));

            /*CreateMap<Hall, HallNutsDto>()
                .ForMember(dest => dest.Nut1, opt => opt.MapFrom(src => src.Nut1))
                .ForMember(dest => dest.Nut2, opt => opt.MapFrom(src => src.Nut2))
                .ForMember(dest => dest.Nut3, opt => opt.MapFrom(src => src.Nut3));*/

            CreateMap<Course, CourseLecturesDto>()
                .ForMember(dest => dest.CourseTitle, opt => opt.MapFrom(c => c.Title))
                .ForMember(dest => dest.Lectures, opt => opt.MapFrom(src => src.Lectures));

            CreateMap<Lecture, LectureAttendanceDto>()
                .ForMember(dest => dest.LectureId, opt => opt.MapFrom(c => c.Id))
                .ForMember(dest => dest.LectureName, opt => opt.MapFrom(src => $"Lecture {src.Id}"))
                .ForMember(dest => dest.Attended, opt => opt.MapFrom((src, dest, destMember, context) => { 
                  var studentId = (string)context.Items["studentId"];
         
                  var attended = src.Students.Any(s => s.NationalId == studentId);
              
                return attended;
                }));

          

        }
    }
}
