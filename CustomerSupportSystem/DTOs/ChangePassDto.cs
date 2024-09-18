using System.ComponentModel.DataAnnotations;

namespace CustomerSupportSystem.DTOs
{
    public class ChangePassDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter your current password")]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "Enter your new password")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Confirm your new password")]
        [Compare("NewPassword", ErrorMessage = "Passwords are not the same")]
        public string ConfirmNewPassword { get; set; }
    }
}