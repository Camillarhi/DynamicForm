using AutoMapper;
using DynamicForm.Dtos;
using DynamicForm.DTOs;
using DynamicForm.Models;

namespace DynamicForm.Configurations
{
    public class MapperInitializer : Profile
    {
        public MapperInitializer()
        {
            CreateMap<ProgramConfiguration, ProgramConfigurationDTO>().ReverseMap();
            CreateMap<ProgramConfiguration, CreateProgramConfigurationDTO>().ReverseMap();
            CreateMap<ApplicationFormConfiguration, ApplicationFormConfigurationDTO>().ReverseMap();
            CreateMap<ApplicationFormConfiguration, CreateApplicationFormConfigurationDTO>().ReverseMap();
            CreateMap<QuestionConfiguration, QuestionConfigurationDTO>().ReverseMap();
            CreateMap<QuestionConfiguration, CreateQuestionConfigurationDTO>().ReverseMap();
            CreateMap<FieldConfiguration, FieldConfigurationDTO>().ReverseMap();
            CreateMap<Choice, ChoiceDTO>().ReverseMap();
            CreateMap<CandidateApplication, CandidateApplicationDTO>().ReverseMap();
            CreateMap<CandidateApplication, CreateCandidateApplicationDTO>().ReverseMap();
            CreateMap<Question, QuestionDTO>().ReverseMap();
        }
    }
}
