﻿using MultiLanguageExamManagementSystem.Models.Dtos;
using MultiLanguageExamManagementSystem.Models.Entities;

public class Exam
{
    public int ExamId { get; set; }
    public string Title { get; set; }
    public DateTime CreatedDate { get; set; }
    public int CreatorId { get; set; }
    public User Creator { get; set; }
    public ICollection<ExamQuestion> ExamQuestions { get; set; }
    public ICollection<TakenExam> TakenExams { get; set; }
    public ICollection<ExamRequest> ExamRequests { get; set; }
    public ICollection<ExamResult> ExamResults { get; set; }
}