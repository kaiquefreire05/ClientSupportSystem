using ClientSupportSystem.Models;

namespace ClientSupportSystem.ViewModels
{
    public class TicketsCommentViewModel
    {
        public TicketModel Ticket { get; set; }
        public IEnumerable<TicketCommentModel>? Comments { get; set; }
    }
}
