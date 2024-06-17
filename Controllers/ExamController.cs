using Microsoft.AspNetCore.Mvc;
using MultiLanguageExamManagementSystem.Models.Dtos;
using MultiLanguageExamManagementSystem.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MultiLanguageExamManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExamController : ControllerBase
    {
        private readonly IExamService _examService;

        public ExamController(IExamService examService)
        {
            _examService = examService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExamDto>>> GetExamsAsync()
        {
            try
            {
                var exams = await _examService.GetExamsAsync();
                return Ok(exams);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{examId}")]
        public async Task<ActionResult<ExamDto>> GetExamAsync(int examId)
        {
            try
            {
                var examDto = await _examService.GetExamAsync(examId);
                return Ok(examDto);
            }
            catch (ApplicationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{examId}/questions")]
        public async Task<ActionResult<IEnumerable<QuestionDto>>> GetExamQuestionsAsync(int examId)
        {
            try
            {
                var questions = await _examService.GetExamQuestionsAsync(examId);
                return Ok(questions);
            }
            catch (ApplicationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("{examId}/submit")]
        public async Task<ActionResult<ResultDto>> SubmitExamAsync(int examId, [FromBody] Dictionary<int, string> answers)
        {
            try
            {
                // Assume you have userId from authentication or context
                int userId = 1; // Replace with actual user identification logic

                var resultDto = await _examService.SubmitExamAsync(examId, userId, answers);
                return Ok(resultDto);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{examId}/results")]
        public async Task<ActionResult<IEnumerable<ExamResultDto>>> GetExamResultsAsync(int examId)
        {
            try
            {
                var results = await _examService.GetExamResultsAsync(examId);
                return Ok(results);
            }
            catch (ApplicationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Other endpoints as needed

    }
}
