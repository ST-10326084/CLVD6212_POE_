using System.ComponentModel.DataAnnotations;

namespace testCLVD.Models
{
    public class User
    {
        public string Id { get; set; } // Unique identifier
        [Required]
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        public DateTime DateCreated { get; set; }
        public bool LoggedIn { get; set; } // Optional field
        public DateTime LastLoginDate { get; set; }
        public string Role { get; set; }
        public string ProfileImageUrl { get; set; }

    }
}
