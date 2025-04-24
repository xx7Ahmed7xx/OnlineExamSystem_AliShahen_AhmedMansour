using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineExamSystem_AliShahen_AhmedMansour.Models
{
    public class Exam
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Required]
        [StringLength(2000)]
        public string Description { get; set; }

        [Required]
        [Range(1, 480)]
        public int DurationMinutes { get; set; } = 60;

        [Required]
        [Range(0, 100)]
        public int PassingScore { get; set; } = 60;

        [Required]
        [Range(0, 100)]
        public int PassingPercentage { get; set; } = 60;

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedDate { get; set; }

        public bool IsActive { get; set; } = true;

        [Required]
        [Range(1, int.MaxValue)]
        public int TotalQuestions { get; set; }

        [Required]
        [Range(1, 10)]
        public int MaxAttempts { get; set; } = 1;  // Default to 1 attempt, max 10 attempts

        // Navigation properties
        public virtual ICollection<Question> Questions { get; set; } = new List<Question>();
        public virtual ICollection<UserExam> UserExams { get; set; } = new List<UserExam>();
    }
} 