using ClientSupportSystem.Models;

namespace ClientSupportSystem.Repositories.Interfaces
{
    public interface ITicketCommentRepository : IRepository<TicketCommentModel>
    {
        IEnumerable<TicketCommentModel> GetCommentsByTicketId(int ticketId);
    }
}
