using Microsoft.AspNetCore.Mvc;
using MultiLanguageExamManagementSystem.Models.Dtos;
using MultiLanguageExamManagementSystem.Services.IServices;

namespace MultiLanguageExamManagementSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LocalizationsController : ControllerBase
    {
        private readonly ILogger<LocalizationsController> _logger;
        private readonly ICultureService _cultureService;

        public LocalizationsController(ILogger<LocalizationsController> logger, ICultureService cultureService)
        {
            _logger = logger;
            _cultureService = cultureService;
        }


        [HttpGet(Name = "GetLocalizationResource")]
        public IActionResult GetLocalizationResource(string @namespace, string key)
        {
            var locator = _cultureService.GetLocator(@namespace, key);
            var message = _cultureService[locator];

            if (message is null)
            {
                return NotFound();
            }

            return Ok(message.Value);
        }

        [HttpGet("/Languages")]
        public async Task<IActionResult> GetLanguages()
        {
            var languages = await _cultureService.GetLanguages();

            return Ok(languages);
        }

        [HttpGet("/Languages/{id}")]
        public async Task<IActionResult> GetLanguageById(int id)
        {
            var language = await _cultureService.GetLanguageById(id);

            if (language is null)
            {
                return NotFound();
            }

            return Ok(language);
        }

        [HttpPost("/Languages")]
        public async Task<IActionResult> CreateLanguage(LanguageResponse languageResponse)
        {
            await _cultureService.CreateLanguage(languageResponse);

            return Ok();
        }

        [HttpPut("/languages/{id}")]
        public IActionResult UpdateLanguage(int id, LanguageResponse languageResponse)
        {
            _cultureService.UpdateLanguage(id, languageResponse);

            return Ok();
        }

        [HttpDelete("/languages/{id}")]
        public IActionResult DeleteLanguage(int id)
        {
            _cultureService.DeleteLanguage(id);

            return Ok();
        }

        [HttpGet("/LocalizationResources")]
        public async Task<IActionResult> GetLocalizationResources()
        {
            var localizationResources = await _cultureService.GetLocalizationResources();

            return Ok(localizationResources);
        }

        [HttpGet("/LocalizationResources/{id:int}")]
        public async Task<IActionResult> GetLocalizationResourceById(int id)
        {
            var localizationResource = await _cultureService.GetLocalizationResourceById(id);

            if (localizationResource is null)
            {
                return NotFound();
            }

            return Ok(localizationResource);
        }

        [HttpGet("/LocalizationResources/Language/{languageId:int}")]
        public async Task<IActionResult> GetLocalizationResourcesByLanguageId(int languageId)
        {
            var localizationResources = await _cultureService.GetLocalizationResourcesByLanguageId(languageId);

            return Ok(localizationResources);
        }

        [HttpPost("/LocalizationResources")]
        public async Task<IActionResult> CreateLocalizationResource(LocalizationResourceResponse localizationResponse)
        {
            var locator = _cultureService.GetLocator(localizationResponse.Namespace, localizationResponse.Key);
            var message = _cultureService[locator];
            if (message is not null)
            {
                return BadRequest();
            }

            await _cultureService.CreateLocalizationResource(localizationResponse);

            return Ok();
        }

        [HttpPut("/LocalizationResources/{id:int}")]
        public async Task<IActionResult> UpdateLocalizationResource(int id, LocalizationResourceResponse localizationResourceResponse)
        {
            await _cultureService.UpdateLocalizationResource(id, localizationResourceResponse);

            return Ok();
        }

        [HttpDelete("/LocalizationResources/{namespace}/{key}")]
        public async Task<IActionResult> DeleteLocalizationResource(string @namespace, string key)
        {
            await _cultureService.DeleteLocalizationResource(@namespace, key);

            return Ok();
        }
    }
}
