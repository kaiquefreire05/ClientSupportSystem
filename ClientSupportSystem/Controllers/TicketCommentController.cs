using ClientSupportSystem.DTOs;
using ClientSupportSystem.Models;
using ClientSupportSystem.Repositories.Interfaces;
using ClientSupportSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ClientSupportSystem.Controllers
{
    public class TicketCommentController : Controller
    {
        // Dependencies Injection
        private readonly ITicketCommentRepository _commentRepository;
        private readonly ITicketRepository _ticketRepository;

        public TicketCommentController(ITicketCommentRepository commentRepository, ITicketRepository ticketRepository)
        {
            _commentRepository = commentRepository;
            _ticketRepository = ticketRepository;
        }

        public IActionResult Index(int ticketId)
        {
            TicketModel ticket = _ticketRepository.GetById(ticketId);
            IEnumerable<TicketCommentModel> ticketsComments = _commentRepository.GetCommentsByTicketId(ticketId);

            var ticketsCommentView = new TicketsCommentViewModel
            {
                Ticket = ticket,
                Comments = ticketsComments
            };

            return View(ticketsComments);
        }
    }
}
