using System.ComponentModel.DataAnnotations;
using CustomerSupportSystem.Enums;

namespace CustomerSupportSystem.DTOs
{
    public class KnowledgeDto
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Enter the title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Enter the content")]
        public string Content { get; set; }

        [Required(ErrorMessage = "Enter the category")]
        public CategoryEnum Category { get; set; }

        public int CreatedByUserId { get; set; }
    }
}