using Microsoft.EntityFrameworkCore;
using MultiLanguageExamManagementSystem.Data;
using MultiLanguageExamManagementSystem.Models.Dtos;
using MultiLanguageExamManagementSystem.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiLanguageExamManagementSystem.Services
{
    public class ExamService
    {
        private readonly ApplicationDbContext _context;

        public ExamService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ExamDto>> GetExamsAsync()
        {
            var exams = await _context.Exams.ToListAsync();
            return exams.Select(e => new ExamDto
            {
                ExamId = e.ExamId,
                Title = e.Title,
                CreatedDate = e.CreatedDate,
                CreatorId = e.CreatorId
            });
        }

        public async Task<ExamDto> GetExamAsync(int examId)
        {
            var exam = await _context.Exams
                .Include(e => e.ExamQuestions)
                .ThenInclude(eq => eq.Question)
                .FirstOrDefaultAsync(e => e.ExamId == examId);

            if (exam == null)
            {
                throw new ApplicationException("Exam not found.");
            }

            // Map to DTO
            var examDto = new ExamDto
            {
                ExamId = exam.ExamId,
                Title = exam.Title,
                CreatedDate = exam.CreatedDate,
                CreatorId = exam.CreatorId,
                Questions = exam.ExamQuestions.Select(eq => new QuestionDto
                {
                    QuestionId = eq.QuestionId,
                    Text = eq.Question.Text,
                    // Do not include correct answer in the DTO for security reasons
                }).ToList()
            };

            return examDto;
        }

        public async Task<IEnumerable<ExamRequestDto>> GetExamRequestsByUserAsync(int userId)
        {
            var requests = await _context.ExamRequests
                .Include(r => r.Exam)
                .Where(r => r.UserId == userId)
                .ToListAsync();

            return requests.Select(r => new ExamRequestDto
            {
                ExamRequestId = r.ExamRequestId,
                UserId = r.UserId,
                ExamId = r.ExamId,
                Status = r.Status
            });
        }

        public async Task AddExamRequestAsync(ExamRequestDto requestDto)
        {
            // Assuming ExamRequest is the entity corresponding to ExamRequestDto
            var request = new ExamRequest
            {
                UserId = requestDto.UserId,
                ExamId = requestDto.ExamId,
                Status = ExamRequestStatus.Pending // Default to Pending; professor approval required
            };

            _context.ExamRequests.Add(request);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ExamDto>> GetApprovedExamsByUserAsync(int userId)
        {
            var approvedRequests = await _context.ExamRequests
                .Include(r => r.Exam)
                .Where(r => r.UserId == userId && r.Status == ExamRequestStatus.Approved)
                .ToListAsync();

            return approvedRequests.Select(r => new ExamDto
            {
                ExamId = r.ExamId,
                Title = r.Exam.Title,
                CreatedDate = r.Exam.CreatedDate,
                CreatorId = r.Exam.CreatorId
            });
        }

        public async Task<ExamDto> GetExamForTakingAsync(int examId, int userId)
        {
            var exam = await _context.Exams
                .Include(e => e.ExamQuestions)
                .ThenInclude(eq => eq.Question)
                .FirstOrDefaultAsync(e => e.ExamId == examId);

            if (exam == null)
            {
                throw new ApplicationException("Exam not found.");
            }

            // Check if the user's request for this exam is approved
            var examRequest = await _context.ExamRequests
                .FirstOrDefaultAsync(r => r.ExamId == examId && r.UserId == userId && r.Status == ExamRequestStatus.Approved);

            if (examRequest == null)
            {
                throw new ApplicationException("You are not authorized to take this exam.");
            }

            // Map to DTO
            var examDto = new ExamDto
            {
                ExamId = exam.ExamId,
                Title = exam.Title,
                CreatedDate = exam.CreatedDate,
                CreatorId = exam.CreatorId,
                Questions = exam.ExamQuestions.Select(eq => new QuestionDto
                {
                    QuestionId = eq.QuestionId,
                    Text = eq.Question.Text,
                    // Do not include correct answer in the DTO for security reasons
                }).ToList()
            };

            return examDto;
        }

        public async Task<ExamResultDto> SubmitExamAsync(int examId, int userId, Dictionary<int, string> answers)
        {
            // Fetch the exam
            var exam = await _context.Exams.FindAsync(examId);
            if (exam == null)
            {
                throw new ApplicationException("Exam not found.");
            }

            // Check if the user has already taken this exam
            var existingTakenExam = await _context.TakenExams
                .FirstOrDefaultAsync(te => te.ExamId == examId && te.UserId == userId && te.IsCompleted);

            if (existingTakenExam != null)
            {
                throw new ApplicationException("You have already completed this exam.");
            }

            // Fetch all questions for the exam
            var examQuestions = _context.ExamQuestions
                .Where(eq => eq.ExamId == examId)
                .Select(eq => new
                {
                    QuestionId = eq.QuestionId,
                    CorrectAnswer = eq.Question.CorrectAnswer
                })
                .ToList();

            // Calculate results
            int correctAnswersCount = 0;
            var results = new List<ExamResultDetailDto>();

            foreach (var examQuestion in examQuestions)
            {
                var questionId = examQuestion.QuestionId;
                var correctAnswer = examQuestion.CorrectAnswer;
                var givenAnswer = answers.ContainsKey(questionId) ? answers[questionId] : null;

                bool isCorrect = string.Equals(correctAnswer, givenAnswer, StringComparison.OrdinalIgnoreCase);
                if (isCorrect)
                {
                    correctAnswersCount++;
                }

                results.Add(new ExamResultDetailDto
                {
                    QuestionId = questionId,
                    GivenAnswer = givenAnswer,
                    IsCorrect = isCorrect
                });
            }

            // Calculate score (if needed)
            double score = (double)correctAnswersCount / examQuestions.Count * 100;

            // Save taken exam
            var takenExam = new TakenExam
            {
                UserId = userId,
                ExamId = examId,
                IsCompleted = true,
                TakenDate = DateTime.UtcNow
            };
            _context.TakenExams.Add(takenExam);
            await _context.SaveChangesAsync();

            // Save exam results
            var examResult = new ExamResult
            {
                UserId = userId,
                ExamId = examId,
                Score = score,
                TotalQuestions = examQuestions.Count,
                CorrectAnswers = correctAnswersCount,
                ExamResultDetails = results.Select(r => new ExamResultDetail
                {
                    QuestionId = r.QuestionId,
                    GivenAnswer = r.GivenAnswer,
                    IsCorrect = r.IsCorrect
                }).ToList()
            };

            _context.ExamResults.Add(examResult);
            await _context.SaveChangesAsync();

            // Prepare result DTO
            var resultDto = new ExamResultDto
            {
                ExamId = examId,
                UserId = userId,
                Score = score,
                TotalQuestions = examQuestions.Count,
                CorrectAnswers = correctAnswersCount,
                Results = results
            };

            return resultDto;
        }

        public async Task<ExamResultDto> GetExamResultAsync(int userId, int examId)
        {
            var examResult = await _context.ExamResults
                .Include(er => er.ExamResultDetails)
                .FirstOrDefaultAsync(er => er.UserId == userId && er.ExamId == examId);

            if (examResult == null)
            {
                throw new ApplicationException("Exam result not found.");
            }

            // Map to DTO
            var resultDto = new ExamResultDto
            {
                ExamId = examResult.ExamId,
                UserId = examResult.UserId,
                Score = examResult.Score,
                TotalQuestions = examResult.TotalQuestions,
                CorrectAnswers = examResult.CorrectAnswers,
                Results = examResult.ExamResultDetails.Select(d => new ExamResultDetailDto
                {
                    QuestionId = d.QuestionId,
                    GivenAnswer = d.GivenAnswer,
                    IsCorrect = d.IsCorrect
                }).ToList()
            };

            return resultDto;
        }

        public async Task<UserDto> GetUserByIdAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);

            if (user == null)
            {
                throw new ApplicationException("User not found.");
            }

            // Map to DTO
            var userDto = new UserDto
            {
                UserId = user.UserId,
                Username = user.Username,
                Role = user.Role
            };

            return userDto;
        }
    }
}
