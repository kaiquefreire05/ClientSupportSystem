using System.ComponentModel.DataAnnotations;
using CustomerSupportSystem.Enums;

namespace CustomerSupportSystem.DTOs
{
    public class UserNoPassDto
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Enter the name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Enter the email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter the role")]
        public RoleEnum Role { get; set; }
    }
}