using AutoMapper;
using backend.Models.Domain.Student;
using backend.Models.DTO.Student;

namespace backend.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AddStudentRequestDTO, Student>().ReverseMap();
            CreateMap<Student, StudentDTO>().ReverseMap();
            CreateMap<Club, ClubDTO>().ReverseMap();
            CreateMap<Contact, ContactDTO>().ReverseMap();
            CreateMap<Financial, FinancialDTO>().ReverseMap();
            CreateMap<Major, MajorDTO>().ReverseMap();
            CreateMap<Academic, AcademicDTO>().ReverseMap();
        }
    }
}
