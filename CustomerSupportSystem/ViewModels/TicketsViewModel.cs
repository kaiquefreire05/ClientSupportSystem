using CustomerSupportSystem.Enums;
using CustomerSupportSystem.Models;

namespace CustomerSupportSystem.DTOs
{
    public class TicketsViewModel
    {
        public IEnumerable<TicketModel>? Tickets { get; set; }
        public RoleEnum UserRole { get; set; }
    }
}
