using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Layer.Models
{
    public class Candidate
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [MaxLength(20)]
        public string? PhoneNumber { get; set; }

        [Key]
        [Required]
        [MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        [RegularExpression("^\\d{2}:\\d{2}:\\d{2}$",
            ErrorMessage = "Invalid time format. Please enter a valid time in the format 'hh:mm:ss' with 2 digits for each component.")]
        public string? BestCallTime { get; set; }

        [MaxLength(200)]
        public string? LinkedInProfileUrl { get; set; }

        [MaxLength(200)]
        public string? GitHubProfileUrl { get; set; }

        [MaxLength(500)]
        [Required]
        public string Comment { get; set; }
    }
}
