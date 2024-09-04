using CustomerSupportSystem.Models;

namespace CustomerSupportSystem.Repositories.Interfaces
{
    public interface IFeedbackRepository : IRepository<FeedbackModel>
    {
        IEnumerable<TicketModel> GetTicketsWithFeedback();
    }
}
