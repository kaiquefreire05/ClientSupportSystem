using CustomerSupportSystem.Enums;
using System.ComponentModel.DataAnnotations;

namespace CustomerSupportSystem.DTOs
{
    public class TicketDto
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = "Enter the title")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Enter the description")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Enter the status")]
        public StatusEnum Status { get; set; }
        [Required(ErrorMessage = "Enter the category")]
        public CategoryEnum Category { get; set; }
        [Required(ErrorMessage = "Enter the priority")]
        public PriorityEnum Priority { get; set; }
    }
}
