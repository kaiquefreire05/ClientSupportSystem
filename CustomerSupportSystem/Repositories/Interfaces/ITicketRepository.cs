using CustomerSupportSystem.Enums;
using CustomerSupportSystem.Models;

namespace CustomerSupportSystem.Repositories.Interfaces
{
    public interface ITicketRepository : IRepository<TicketModel>
    {
        IEnumerable<TicketModel> GetTicketByStatus (StatusEnum status);
        TicketModel GetTicketWithFeedback(int ticketId);

    }
}
