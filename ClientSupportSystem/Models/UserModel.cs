using ClientSupportSystem.Enums;

namespace ClientSupportSystem.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public RoleEnum Role { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties

        public virtual ICollection<TicketModel> Tickets { get; set; } = new List<TicketModel>();
        public virtual ICollection<KnowledgeBaseModel> KnowledgeArticles { get; set; } = new List<KnowledgeBaseModel>();
        public virtual ICollection<TicketCommentModel> Comments { get; set; } = new List<TicketCommentModel>();
        public virtual ICollection<FeedbackModel> Feedbacks { get; set; } = new List<FeedbackModel>();
    }
}
