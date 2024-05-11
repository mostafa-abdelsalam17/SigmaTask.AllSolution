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
        [Key]
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

        public void Update(string FirstName,string LastName,string PhoneNumber,string Email
            ,string BestCallTime,string LinkedInProfileUrl,string GitHubProfileUrl,string Comment)
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.PhoneNumber = PhoneNumber;
            this.Email = Email;
            this.BestCallTime = BestCallTime;
            this.LinkedInProfileUrl = LinkedInProfileUrl;
            this.GitHubProfileUrl = GitHubProfileUrl;
            this.Comment = Comment;
        }
    }
}
