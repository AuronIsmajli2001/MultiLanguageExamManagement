namespace MultiLanguageExamManagementSystem.Models.Dtos
{
    public class ExamResultDto
    {
        public int ExamId { get; set; }
        public int UserId { get; set; }
        public double Score { get; set; }
        public int TotalQuestions { get; set; }
        public int CorrectAnswers { get; set; }
        public ICollection<ExamResultDetailDto> Results { get; set; }
    }
}
