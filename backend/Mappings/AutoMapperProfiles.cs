using AutoMapper;
using backend.Models.Domain.Colleges;
using backend.Models.Domain.Content.Articles;
using backend.Models.Domain.Content.Assignments;
using backend.Models.Domain.Content.Events;
using backend.Models.Domain.Content.FAQs;
using backend.Models.Domain.Content.Help;
using backend.Models.Domain.Content.Notices;
using backend.Models.Domain.Content.Results;
using backend.Models.Domain.Content.Schedules;
using backend.Models.Domain.Content.Syllabi;
using backend.Models.Domain.Students;
using backend.Models.Domain.Teachers;
using backend.Models.Domain.Universities;
using backend.Models.DTO.College;
using backend.Models.DTO.Content.Article;
using backend.Models.DTO.Content.Assignment;
using backend.Models.DTO.Content.Events;
using backend.Models.DTO.Content.FAQ;
using backend.Models.DTO.Content.Help;
using backend.Models.DTO.Content.Notice;
using backend.Models.DTO.Content.Result;
using backend.Models.DTO.Content.Schedule;
using backend.Models.DTO.Content.Syllabus;
using backend.Models.DTO.Student;
using backend.Models.DTO.Teacher;
using backend.Models.DTO.University;

namespace backend.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            //Student
            CreateMap<AddStudentDTO, Student>().ReverseMap();
            CreateMap<UpdateStudentDTO, Student>()
                .ForMember(dest => dest.CollegeId, opt => opt.MapFrom(src => src.CreatorId))
                .ReverseMap();
            CreateMap<Student, StudentDTO>()
                .ForMember(dest => dest.AcademicDTO, opt => opt.MapFrom(src => src.Academic))
                .ForMember(dest => dest.FinancialDTO, opt => opt.MapFrom(src => src.Financial))
                .ForMember(dest => dest.ClubsDTO, opt => opt.MapFrom(src => src.Clubs))
                .ReverseMap();
            CreateMap<Club, ClubDTO>()
                .ForMember(dest=>dest.StudentDTO,opt=>opt.MapFrom(src=>src.Students))
                .ReverseMap();
            CreateMap<AddClubDTO,Club>().ReverseMap();
            CreateMap<UpdateClubDTO, Club>().ReverseMap();

            CreateMap<Financial, FinancialDTO>().ReverseMap();
            CreateMap<Academic, AcademicDTO>().ReverseMap();

            //Article
            CreateMap<AddArticleDTO, Article>().ReverseMap();
            CreateMap<UpdateArticleDTO, Article>().ReverseMap();
            CreateMap<AuthorDTO, Author>().ReverseMap();
            CreateMap<Article, ArticleDTO>()
                .ForMember(dest => dest.AuthorDTO, opt => opt.MapFrom(src => src.Author))
                .ReverseMap();

            //Syllabus
            CreateMap<Unit, UnitDTO>().ReverseMap();
            CreateMap<AddCourseDTO, Course>()
                .ForMember(dest => dest.Units, opt => opt.MapFrom(src => src.UnitsDTO))
                .ReverseMap();
            CreateMap<Course, CourseDTO>()
                .ForMember(dest=>dest.UnitsDTO, opt=>opt.MapFrom(src=>src.Units))
                .ForMember(dest => dest.TeacherDTO, opt => opt.MapFrom(src => src.Teachers))
                .ForMember(dest => dest.StudentDTO, opt => opt.MapFrom(src => src.Students))
                .ReverseMap();
            CreateMap<UpdateCourseDTO, Course>()
                .ForMember(dest => dest.Units, opt => opt.MapFrom(src => src.UnitsDTO))
                .ReverseMap();

            CreateMap<AddSyllabusDTO, Syllabus>().ReverseMap();
            CreateMap<UpdateSyllabusDTO, Syllabus>().ReverseMap();
            CreateMap<Syllabus, SyllabusDTO>()
                .ForMember(dest => dest.CourseDTO, opt => opt.MapFrom(src => src.Courses))
                .ReverseMap();

            //Assignment
            CreateMap<Submission, SubmissionDTO>()
                .ForMember(dest => dest.StudentDTO, opt => opt.MapFrom(src => src.Student))
                .ForMember(dest => dest.AssignmentDTO, opt => opt.MapFrom(src => src.Assignment))
                .ReverseMap();
            CreateMap<AddSubmissionDTO, Submission>()
                .ReverseMap();
            CreateMap<UpdateSubmissionDTO, Submission>()
                .ReverseMap();

            CreateMap<AddAssignmentDTO, Assignment>().ReverseMap();
            CreateMap<UpdateAssignmentDTO, Assignment>().ReverseMap();
            CreateMap<Assignment, AssignmentDTO>()
                .ForMember(dest=>dest.TeacherDTO,opt=>opt.MapFrom(src=>src.Teacher))
                .ForMember(dest => dest.CourseDTO, opt => opt.MapFrom(src => src.Course))
                .ReverseMap();

            //Notice
            CreateMap<AddNoticeDTO, Notice>().ReverseMap();
            CreateMap<UpdateNoticeDTO, Notice>().ReverseMap();
            CreateMap<Notice, NoticeDTO>().ReverseMap();


            //Help
            CreateMap<AddAnswerDTO, Answer>().ReverseMap();
            CreateMap<UpdateAnswerDTO, Answer>().ReverseMap();
            CreateMap<Answer, AnswerDTO>().ReverseMap();

            CreateMap<Question, QuestionDTO>()
                .ForMember(dest => dest.StudentDTO, opt => opt.MapFrom(src => src.Student))
                .ForMember(dest => dest.AnswerDTO, opt => opt.MapFrom(src => src.Answers))
                .ReverseMap();
            CreateMap<AddQuestionDTO, Question>().ReverseMap();
            CreateMap<UpdateQuestionDTO, Question>().ReverseMap();

            //Event
            CreateMap<AddEventDTO, Event>().ReverseMap();
            CreateMap<UpdateEventDTO, Event>().ReverseMap();
            CreateMap<Event, AddEventDTO>().ReverseMap();

            //Teacher
            CreateMap<Teacher, TeacherDTO>()
                .ForMember(dest => dest.CourseDTO, opt => opt.MapFrom(src => src.Courses))
                .ForMember(dest => dest.CollegeDTO, opt => opt.MapFrom(src => src.Colleges))
                .ForMember(dest => dest.AssignmentDTO, opt => opt.MapFrom(src => src.Assignments))
                .ReverseMap();
            CreateMap<AddTeacherDTO, Teacher>().ReverseMap();
            CreateMap<UpdateTeacherDTO, Teacher>().ReverseMap();

            //University
            CreateMap<University, UniversityDTO>()
                .ForMember(dest=>dest.CollegeDTO, opt=>opt.MapFrom(src=>src.Colleges))
                .ReverseMap();
            CreateMap<AddUniversityDTO, University>()
                .ForMember(dest=>dest.CreatorId,opt=>opt.MapFrom(src=>src.DeveloperId))
                .ReverseMap();
            CreateMap<UpdateUniversityDTO, University>().ReverseMap();

            //College
            CreateMap<College, CollegeDTO>()
                .ForMember(dest => dest.StudentDTO, opt => opt.MapFrom(src => src.Students))
                .ForMember(dest => dest.TeacherDTO, opt => opt.MapFrom(src => src.Teachers))
                .ReverseMap();
            CreateMap<AddCollegeDTO, College>().ReverseMap();
            CreateMap<UpdateCollegeDTO, College>()
                .ForMember(dest=>dest.UniversityId,opt=>opt.MapFrom(src=>src.CreatorId))
                .ReverseMap();

            //Result
            CreateMap<Result, ResultDTO>().ReverseMap();
            CreateMap<AddResultDTO, Result>().ReverseMap();
            CreateMap<UpdateResultDTO, Result>().ReverseMap();

            //FAQ
            CreateMap<AddFAQDTO, FAQ>().ReverseMap();
            CreateMap<FAQ, FAQDTO>().ReverseMap();

            //Schedule
            CreateMap<AddScheduleDTO, Schedule>().ReverseMap();
            CreateMap<UpdateScheduleDTO, Schedule>().ReverseMap();
            CreateMap<Schedule, ScheduleDTO>().ReverseMap();
            CreateMap<AddExamSchedule, ExamSchedule>().ReverseMap();
        }
    }
}
