using CustomerSupportSystem.Enums;
using CustomerSupportSystem.Models;

namespace CustomerSupportSystem.Repositories.Interfaces
{
    public interface ITicketRepository : IRepository<TicketModel>
    {
        IEnumerable<TicketModel> GetTicketByStatusWithFeedback (StatusEnum status);
        TicketModel GetTicketWithFeedback(int ticketId);

    }
}
