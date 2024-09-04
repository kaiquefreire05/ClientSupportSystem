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
    }
}
