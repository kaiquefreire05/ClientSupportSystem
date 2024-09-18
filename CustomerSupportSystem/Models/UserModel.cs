using CustomerSupportSystem.Enums;
using CustomerSupportSystem.Helper;

namespace CustomerSupportSystem.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public RoleEnum Role { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties

        public virtual ICollection<TicketModel> Tickets { get; set; } = new List<TicketModel>();
        public virtual ICollection<KnowledgeBaseModel> KnowledgeArticles { get; set; } = new List<KnowledgeBaseModel>();
        public virtual ICollection<TicketCommentModel> Comments { get; set; } = new List<TicketCommentModel>();
        public virtual ICollection<FeedbackModel> Feedbacks { get; set; } = new List<FeedbackModel>();

        // Methods

        public bool ValidPassword(string password)
        {
            return Password == password.GenerateHash();
        }

        public void setPasswordHash()
        {
            Password = Password.GenerateHash();
        }

        public string GenerateNewPass()
        {
            var newPass = Guid.NewGuid().ToString().Substring(0, 8);
            Password = newPass.GenerateHash();
            return newPass;
        }

        public void SetNewPass(string newPass)
        {
            Password = newPass.GenerateHash();
        }
    }
}