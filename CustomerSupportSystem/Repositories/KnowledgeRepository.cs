using CustomerSupportSystem.Database;
using CustomerSupportSystem.Enums;
using CustomerSupportSystem.Models;
using CustomerSupportSystem.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CustomerSupportSystem.Repositories
{
    public class KnowledgeRepository : RepositoryBase<KnowledgeBaseModel>, IKnowledgeRepository
    {
        // Dependence Injection
        public KnowledgeRepository(ApplicationDBContext context) : base(context)
        {
        }

        public IEnumerable<KnowledgeBaseModel> GetAllWithUser()
        {
            return _dbSet.Include(kb => kb.CreatedByUser).ToList();
        }

        public IEnumerable<KnowledgeBaseModel> GetArticlesByCategory(CategoryEnum category)
        {
            return _dbSet.Where(k => k.Category == category).ToList();
        }

        public IEnumerable<KnowledgeBaseModel> GetArticlesByUserId(int userId)
        {
            return _dbSet.Include(k => k.CreatedByUser).Where(k => k.CreatedByUserId == userId).ToList();
        }

        public KnowledgeBaseModel GetByIdWithUser(int id)
        {
            return _dbSet.Include(kb => kb.CreatedByUser)
                .FirstOrDefault(kb => kb.Id == id);
        }

        public override KnowledgeBaseModel Update(KnowledgeBaseModel article)
        {
            // Verifyng if article exist
            var existingArticle = _dbSet.Find(article.Id);
            if (existingArticle == null)
            {
                throw new InvalidOperationException("Article not found.");
            }

            // Updating values
            existingArticle.Title = article.Title;
            existingArticle.Content = article.Content;
            existingArticle.Category = article.Category;
            existingArticle.UpdatedAt = DateTime.Now;

            _context.SaveChanges();
            return existingArticle;
        }
    }
}