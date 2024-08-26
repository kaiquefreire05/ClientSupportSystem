using ClientSupportSystem.DTOs;
using ClientSupportSystem.Filters;
using ClientSupportSystem.Helper.Interfaces;
using ClientSupportSystem.Models;
using ClientSupportSystem.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ClientSupportSystem.Controllers
{
    [UserLoggedPage]
    public class TicketController : Controller
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly ISessionService _sessionService;
        public TicketController(ITicketRepository ticketRepository, ISessionService sessionService)
        {
            _ticketRepository = ticketRepository;
            _sessionService = sessionService;
        }
        public IActionResult Index()
        {
            IEnumerable<TicketModel> tickets = _ticketRepository.GetAll();
            return View(tickets);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(TicketDto ticketDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userId = _sessionService.GetUserId();
                    var ticket = new TicketModel
                    {
                        Title = ticketDto.Title,
                        Description = ticketDto.Description,
                        Status = ticketDto.Status,
                        Category = ticketDto.Category,
                        UserId = userId.Value,
                        Priority = ticketDto.Priority,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = null
                    };
                    _ticketRepository.Create(ticket);
                    TempData["SuccessMessage"] = "Ticket created successfully.";
                    return RedirectToAction("Index");
                }

                TempData["ErrorMessage"] = "Invalid data, please check the form.";
                return View("Create", ticketDto);
            }
            catch (System.Exception ex)
            {
                TempData["ErrorMessage"] = $"Ticket was not registered. Try again, error detail: {ex.Message}";
                return RedirectToAction("Index");
            }
        }

    }
}
