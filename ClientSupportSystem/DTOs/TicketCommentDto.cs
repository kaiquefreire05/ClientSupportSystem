using System.ComponentModel.DataAnnotations;

namespace ClientSupportSystem.DTOs
{
    public class TicketCommentDto
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = "Enter the comment")]
        public string CommentText { get; set; }
        public int? TicketId { get; set; }
        public int UserId { get; set; }
    }
}
