using System.ComponentModel.DataAnnotations;

namespace CustomerSupportSystem.DTOs
{
    public class ResetPasswordDto
    {
        [Required(ErrorMessage = "Enter the email")]
        public string Email { get; set; }
    }
}