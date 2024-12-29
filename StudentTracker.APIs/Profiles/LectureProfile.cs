using AutoMapper;
using StudentTracker.APIs.DTOs.Lecture;
using StudentTracker.Core.Entities;

namespace StudentTracker.APIs.Profiles
{
    public class LectureProfile : Profile
    {
        public LectureProfile()
        {
            CreateMap<AddIrregularLectureDto, Lecture>()
                .ForMember(dest => dest.Professor, opt => opt.Ignore())
                .ForMember(dest => dest.Course, opt => opt.Ignore())
                .ForMember(dest => dest.Hall, opt => opt.Ignore())
                .ForMember(dest => dest.Department, opt => opt.Ignore());

            CreateMap<AddRegularLectureDto, Lecture>()
                .ForMember(dest => dest.Professor, opt => opt.Ignore())
                .ForMember(dest => dest.Course, opt => opt.Ignore())
                .ForMember(dest => dest.Hall, opt => opt.Ignore())
                .ForMember(dest => dest.Department, opt => opt.Ignore())
                .ForMember(dest => dest.Day, opt => opt.MapFrom(src => src.Day.ToLower()));

            
        }
    }
}
