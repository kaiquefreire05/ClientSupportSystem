using CustomerSupportSystem.Database;
using CustomerSupportSystem.Enums;
using CustomerSupportSystem.Models;
using CustomerSupportSystem.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CustomerSupportSystem.Repositories
{
    public class TicketRepository : RepositoryBase<TicketModel>, ITicketRepository
    {
        // Dependence Injection
        public TicketRepository(ApplicationDBContext context) : base(context)
        {
            
        }
        public IEnumerable<TicketModel> GetTicketByStatusWithFeedback(StatusEnum status)
        {
            return _dbSet.Include(t => t.Feedback).Where(t => t.Status == status).ToList();
        }

        public TicketModel GetTicketWithFeedback(int ticketId)
        {
            var ticket = _dbSet.Include(t => t.Feedback)
                .FirstOrDefault(t => t.Id == ticketId);

            if (ticket == null)
            {
                throw new InvalidOperationException("Ticket not found.");
            }

            return ticket;
        }

        public override TicketModel Update(TicketModel ticket)
        {
            // Verifyng if ticket exist
            var existingTicket = _dbSet.Find(ticket.Id);
            if (existingTicket == null)
            {
                throw new InvalidOperationException("Ticket not found.");
            }

            // Updating values
            existingTicket.Title = ticket.Title;
            existingTicket.Description = ticket.Description;
            existingTicket.Status = ticket.Status;
            existingTicket.Category = ticket.Category;
            existingTicket.Priority = ticket.Priority;
            existingTicket.UpdatedAt = DateTime.Now;

            _context.SaveChanges();
            return existingTicket;
        }
    }
}
