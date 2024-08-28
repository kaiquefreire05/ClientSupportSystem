using ClientSupportSystem.Enums;
using ClientSupportSystem.Models;

namespace ClientSupportSystem.DTOs
{
    public class TicketsViewModel
    {
        public IEnumerable<TicketModel> Tickets { get; set; }
        public RoleEnum UserRole { get; set; }
    }
}
