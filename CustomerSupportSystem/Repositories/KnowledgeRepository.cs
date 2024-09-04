using CustomerSupportSystem.Database;
using CustomerSupportSystem.Enums;
using CustomerSupportSystem.Models;
using CustomerSupportSystem.Repositories.Interfaces;

namespace CustomerSupportSystem.Repositories
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
