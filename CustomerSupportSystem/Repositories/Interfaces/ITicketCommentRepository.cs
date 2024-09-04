using CustomerSupportSystem.Models;

namespace CustomerSupportSystem.Repositories.Interfaces
{
    public interface ITicketCommentRepository : IRepository<TicketCommentModel>
    {
        IEnumerable<TicketCommentModel> GetCommentsByTicketId(int ticketId);
    }
}
