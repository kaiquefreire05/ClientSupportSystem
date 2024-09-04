using CustomerSupportSystem.Enums;
using CustomerSupportSystem.Models;

namespace CustomerSupportSystem.ViewModels
{
    public class TicketsCommentViewModel
    {
        public TicketModel Ticket { get; set; }
        public IEnumerable<TicketCommentModel>? Comments { get; set; }
    }
}
