namespace MultiLanguageExamManagementSystem.Models.Entities
{
    public class ExamRequest
    {
        public int ExamRequestId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int ExamId { get; set; }
        public Exam Exam { get; set; }
        public ExamRequestStatus Status { get; set; }
    }
}
