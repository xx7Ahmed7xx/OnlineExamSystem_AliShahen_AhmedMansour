using System.ComponentModel.DataAnnotations;

namespace OnlineExamSystem_AliShahen_AhmedMansour.Models
{
    public class Question
    {
        public int Id { get; set; }

        [Required]
        [StringLength(500)]
        public string Title { get; set; }

        [Required]
        [StringLength(200)]
        public string OptionA { get; set; }

        [Required]
        [StringLength(200)]
        public string OptionB { get; set; }

        [Required]
        [StringLength(200)]
        public string OptionC { get; set; }

        [Required]
        [StringLength(200)]
        public string OptionD { get; set; }

        [Required]
        [StringLength(200)]
        public string CorrectAnswer { get; set; }

        [Required]
        public int ExamId { get; set; }

        // Navigation property
        public virtual Exam Exam { get; set; }

        public int OrderInExam { get; set; }
    }
} 