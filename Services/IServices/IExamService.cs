using MultiLanguageExamManagementSystem.Models.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MultiLanguageExamManagementSystem.Services
{
    public interface IExamService
    {
        Task<IEnumerable<ExamDto>> GetExamsAsync();
        Task<ExamDto> GetExamAsync(int examId);
        Task<IEnumerable<QuestionDto>> GetExamQuestionsAsync(int examId);
        Task<ResultDto> SubmitExamAsync(int examId, int userId, Dictionary<int, string> answers);
        Task<IEnumerable<ExamResultDto>> GetExamResultsAsync(int examId);
        Task<IEnumerable<ExamRequestDto>> GetExamRequestsByUserAsync(int userId);
        Task AddExamRequestAsync(ExamRequestDto requestDto);
        Task<IEnumerable<ExamDto>> GetApprovedExamsByUserAsync(int userId);
        Task AddExamResultAsync(ExamResultDto resultDto);
        Task<UserDto> GetUserByIdAsync(int userId);
        Task<ExamResultDto> GetExamResultAsync(int userId, int examId);
    }
}
