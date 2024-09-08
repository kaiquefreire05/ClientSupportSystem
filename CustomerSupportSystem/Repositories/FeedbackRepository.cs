using CustomerSupportSystem.Database;
using CustomerSupportSystem.Models;
using CustomerSupportSystem.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CustomerSupportSystem.Repositories
{
    public class FeedbackRepository : RepositoryBase<FeedbackModel>, IFeedbackRepository
    {
        // Dependence Injection
        public FeedbackRepository(ApplicationDBContext context) : base(context)
        {
        }

        public IEnumerable<TicketModel> GetTicketsWithFeedback()
        {
            return _dbSet.Include(f => f.Ticket)
                         .ThenInclude(t => t.Feedback)
                         .Select(f => f.Ticket)
                         .Distinct()
                         .ToList();
        }

        public override FeedbackModel Update(FeedbackModel feedback)
        {
            var existentFeedback = _dbSet.Find(feedback.Id);
            if (existentFeedback == null) throw new InvalidOperationException("Feedback not found.");

            // Updating values
            existentFeedback.UpdatedAt = DateTime.Now;
            existentFeedback.Comments = feedback.Comments;
            existentFeedback.Rating = feedback.Rating;

            _context.SaveChanges();
            return feedback;
        }
    }
}
