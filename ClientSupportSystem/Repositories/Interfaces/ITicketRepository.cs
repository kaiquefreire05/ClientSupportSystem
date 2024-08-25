using ClientSupportSystem.Enums;
using ClientSupportSystem.Models;

namespace ClientSupportSystem.Repositories.Interfaces
{
    public interface ITicketRepository : IRepository<TicketModel>
    {
        IEnumerable<TicketModel> GetTicketByStatus (StatusEnum status);
        TicketModel GetTicketWithFeedback(int ticketId);

    }
}
