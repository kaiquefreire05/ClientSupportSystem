using ClientSupportSystem.Enums;
using ClientSupportSystem.Models;

namespace ClientSupportSystem.Repositories.Interfaces
{
    public interface IKnowledgeRepository : IRepository<KnowledgeBaseModel>
    {
        IEnumerable<KnowledgeBaseModel> GetArticlesByCategory(CategoryEnum category);
        IEnumerable<KnowledgeBaseModel> GetArticlesByUserId(int userId);
    }
}
