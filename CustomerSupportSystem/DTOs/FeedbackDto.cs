using System.ComponentModel.DataAnnotations;

namespace CustomerSupportSystem.DTOs
{
    public class FeedbackDto
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Enter the rating")]
        public int Rating { get; set; }

        [Required(ErrorMessage = "Enter the comment")]
        public string Comments { get; set; }

        public int TicketId { get; set; }
        public int UserId { get; set; }
    }
}