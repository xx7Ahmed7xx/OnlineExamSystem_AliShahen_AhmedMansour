using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineExamSystem_AliShahen_AhmedMansour.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        // Navigation property
        public virtual ICollection<UserExam> UserExams { get; set; } = new List<UserExam>();
    }
} 