using AutoMapper;
using MultiLanguageExamManagementSystem.Models.Dtos;
using MultiLanguageExamManagementSystem.Models.Entities;

namespace LifeEcommerce.Helpers
{
    public class AutoMapperConfigurations : Profile
    {
        public AutoMapperConfigurations() 
        {
            CreateMap<Language, LanguageRequest>().ReverseMap();
            CreateMap<Language, LanguageResponse>().ReverseMap();
            CreateMap<LocalizationResource, LocalizationResourceRequest>().ReverseMap();
            CreateMap<LocalizationResource, LocalizationResourceResponse>().ReverseMap();

            CreateMap<LocalizationResource, LocalizationResource>();
        }
    }
}
