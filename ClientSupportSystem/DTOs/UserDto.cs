﻿using ClientSupportSystem.Enums;
using System.ComponentModel.DataAnnotations;

namespace ClientSupportSystem.DTOs
{
    public class UserDto
    {
        public int? Id  { get; set; }
        [Required(ErrorMessage ="Enter the name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Enter the email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Enter the password")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Enter the role")]
        public RoleEnum Role { get; set; }
    }
}
