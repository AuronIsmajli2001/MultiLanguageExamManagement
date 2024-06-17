namespace MultiLanguageExamManagementSystem.Models.Entities
{
    public class ExamResultDetail
    {
        public int QuestionId { get; set; }
        public string GivenAnswer { get; set; }
        public bool IsCorrect { get; set; }
    }
}
