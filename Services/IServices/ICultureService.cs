using MultiLanguageExamManagementSystem.Models.Dtos;
using MultiLanguageExamManagementSystem.Models.Entities;

namespace MultiLanguageExamManagementSystem.Services.IServices
{
    public interface ICultureService
    {
        LocalizationResource this[string locator] { get; }

        LocalizationResource GetString(string locator);

        string GetLocator(string @namespace, string key);


        Task<List<LanguageRequest>> GetLanguages();

        Task<LanguageRequest> GetLanguageById(int id);
        Task CreateLanguage(LanguageResponse languageResponse);
        Task UpdateLanguage(int id, LanguageResponse languageResponse);
        Task DeleteLanguage(int id);


        Task<List<LocalizationResourceRequest>> GetLocalizationResources();
        Task<LocalizationResourceRequest> GetLocalizationResourceById(int id);
        Task<List<LocalizationResourceRequest>> GetLocalizationResourcesByLanguageId(int languageId);
        Task CreateLocalizationResource(LocalizationResourceResponse resourceResponse);
        Task UpdateLocalizationResource(int id, LocalizationResourceResponse resourceResponse);
        Task DeleteLocalizationResource(string @namespace, string key);

        // Your code here
        // methods for string localization, languages and localization resources
    }
}
