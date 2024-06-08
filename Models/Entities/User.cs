﻿using MultiLanguageExamManagementSystem.Models.Entities;

public class User
{
    public int UserId { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public UserRole Role { get; set; }
    public ICollection<Exam> CreatedExams { get; set; }
    public ICollection<TakenExam> TakenExams { get; set; }
}

