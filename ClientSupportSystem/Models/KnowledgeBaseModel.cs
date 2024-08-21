using ClientSupportSystem.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ClientSupportSystem.Models
{
    public class KnowledgeBaseModel
    {
        public int Id { get; set; }
        public string Title { get; set; }    
        public string Content { get; set; }
        public CategoryEnum Category { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties

        public virtual UserModel CreatedByUser { get; set; }
    }
}
