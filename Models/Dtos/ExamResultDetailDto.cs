namespace MultiLanguageExamManagementSystem.Models.Dtos
{
    public class ExamResultDetailDto
    {
        public int QuestionId { get; set; }
        public string GivenAnswer { get; set; }
        public bool IsCorrect { get; set; }
    }
}
