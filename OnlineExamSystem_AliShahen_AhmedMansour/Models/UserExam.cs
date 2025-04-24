using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineExamSystem_AliShahen_AhmedMansour.Models
{
    public class UserExam
    {
        public int Id { get; set; }
        
        [Required]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        
        [Required]
        public int ExamId { get; set; }
        public virtual Exam Exam { get; set; }
        
        [Required]
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        
        [Range(0, 100)]
        public int Score { get; set; }

        [Range(0, int.MaxValue)]
        public int CorrectAnswers { get; set; }

        [Range(0, int.MaxValue)]
        public int TotalQuestions { get; set; }

        public bool HasPassed { get; set; }

        // Calculated property
        public TimeSpan? Duration => EndTime.HasValue ? EndTime.Value - StartTime : null;
    }
} 