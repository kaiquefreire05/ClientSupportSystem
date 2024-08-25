using ClientSupportSystem.Database;
using ClientSupportSystem.Enums;
using ClientSupportSystem.Models;
using ClientSupportSystem.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ClientSupportSystem.Repositories
{
    public class TicketRepository : RepositoryBase<TicketModel>, ITicketRepository
    {
        // Dependence Injection
        public TicketRepository(ApplicationDBContext context) : base(context)
        {
            
        }
        public IEnumerable<TicketModel> GetTicketByStatus(StatusEnum status)
        {
            return _dbSet.Where(t => t.Status == status).ToList();
        }

        public TicketModel GetTicketWithFeedback(int ticketId)
        {
            var ticket = _dbSet.Include(t => t.Feedback)
                .FirstOrDefault(t => t.Id == ticketId && t.Feedback != null);

            if (ticket == null)
            {
                throw new InvalidOperationException("Ticket not found.");
            }

            if (ticket.Feedback == null)
            {
                throw new InvalidOperationException("Ticket does not have feedback associated.");
            }

            return ticket;
        }
    }
}
