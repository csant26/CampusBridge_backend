using AutoMapper;
using backend.Models.Domain.Content.Article;
using backend.Models.Domain.Student;
using backend.Models.DTO.Content.Article;
using backend.Models.DTO.Student;

namespace backend.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            //Student
            CreateMap<AddStudentDTO, Student>().ReverseMap();
            CreateMap<UpdateStudentDTO, Student>().ReverseMap();
            CreateMap<Student, StudentDTO>()
                .ForMember(dest => dest.AcademicDTO, opt => opt.MapFrom(src => src.Academic))
                .ForMember(dest => dest.FinancialDTO, opt => opt.MapFrom(src => src.Financial))
                .ForMember(dest => dest.MajorsDTO, opt => opt.MapFrom(src => src.Majors))
                .ForMember(dest => dest.ClubsDTO, opt => opt.MapFrom(src => src.Clubs))
                .ReverseMap();
            CreateMap<Club, ClubDTO>().ReverseMap();
            CreateMap<Financial, FinancialDTO>().ReverseMap();
            CreateMap<Major, MajorDTO>().ReverseMap();
            CreateMap<Academic, AcademicDTO>().ReverseMap();

            //Article
            CreateMap<AddArticleDTO, Article>().ReverseMap();
            CreateMap<UpdateArticleDTO, Article>().ReverseMap();
            CreateMap<AuthorDTO, Author>().ReverseMap();
            CreateMap<Article, ArticleDTO>()
                .ForMember(dest => dest.AuthorDTO, opt => opt.MapFrom(src => src.Author))
                .ReverseMap();

        }
    }
}
