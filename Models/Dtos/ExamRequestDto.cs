using MultiLanguageExamManagementSystem.Models.Entities;

namespace MultiLanguageExamManagementSystem.Models.Dtos
{
    public class ExamRequestDto
    {
        public int ExamRequestId { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public int ExamId { get; set; }
        public string ExamTitle { get; set; }
        public ExamRequestStatus Status { get; set; }
    }
}
