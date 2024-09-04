namespace CustomerSupportSystem.Models
{
    public class TicketCommentModel
    {
        public int Id { get; set; }
        public string CommentText { get; set; }
        public int TicketId { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties

        public virtual TicketModel Ticket { get; set; }
        public virtual UserModel User { get; set; }
    }
}
