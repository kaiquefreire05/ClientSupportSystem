using ClientSupportSystem.Database;
using ClientSupportSystem.Enums;
using ClientSupportSystem.Models;
using ClientSupportSystem.Repositories.Interfaces;

namespace ClientSupportSystem.Repositories
{
    public class KnowledgeRepository : RepositoryBase<KnowledgeBaseModel>, IKnowledgeRepository
    {
        // Dependence Injection
        public KnowledgeRepository(ApplicationDBContext context) : base(context)
        {
        }

        public IEnumerable<KnowledgeBaseModel> GetArticlesByCategory(CategoryEnum category)
        {
            return _dbSet.Where(k => k.Category == category).ToList();
        }

        public IEnumerable<KnowledgeBaseModel> GetArticlesByUserId(int userId)
        {
            return _dbSet.Where(k => k.CreatedByUserId == userId).ToList();
        }
    }
}
