using CustomerSupportSystem.Enums;
using CustomerSupportSystem.Models;

namespace CustomerSupportSystem.Repositories.Interfaces
{
    public interface IKnowledgeRepository : IRepository<KnowledgeBaseModel>
    {
        IEnumerable<KnowledgeBaseModel> GetArticlesByCategory(CategoryEnum category);
        IEnumerable<KnowledgeBaseModel> GetArticlesByUserId(int userId);
    }
}
