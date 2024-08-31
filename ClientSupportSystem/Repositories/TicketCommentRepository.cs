using ClientSupportSystem.Database;
using ClientSupportSystem.Models;
using ClientSupportSystem.Repositories.Interfaces;

namespace ClientSupportSystem.Repositories
{
    public class TicketCommentRepository : RepositoryBase<TicketCommentModel>, ITicketCommentRepository
    {
        public TicketCommentRepository(ApplicationDBContext context) : base(context)
        {
        }

        public IEnumerable<TicketCommentModel> GetCommentsByTicketId(int ticketId)
        {
            return _dbSet.Where(tc => tc.TicketId == ticketId).ToList();
        }
    }
}
