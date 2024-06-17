namespace MultiLanguageExamManagementSystem.Models.Dtos
{
    public class ExamDto
    {
        public int ExamId { get; set; }
        public string Title { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatorId { get; set; }
        public string CreatorUsername { get; set; }
        public ICollection<QuestionDto> Questions { get; set; }
    }
}
