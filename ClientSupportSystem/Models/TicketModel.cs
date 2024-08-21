using ClientSupportSystem.Enums;

namespace ClientSupportSystem.Models
{
    public class TicketModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public StatusEnum Status { get; set; }
        public CategoryEnum Category { get; set; }
        public PriorityEnum Priority { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties

        public virtual UserModel User { get; set; }
        public virtual FeedbackModel Feedback { get; set; }
        public virtual ICollection<TicketCommentModel> TicketComments { get; set; }
    }
}
