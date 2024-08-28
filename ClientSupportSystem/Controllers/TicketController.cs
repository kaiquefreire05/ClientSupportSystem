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
        // Dependencies Injection
        private readonly ITicketRepository _ticketRepository;
        private readonly ISessionService _sessionService;
        public TicketController(ITicketRepository ticketRepository, ISessionService sessionService)
        {
            _ticketRepository = ticketRepository;
            _sessionService = sessionService;
        }
        public IActionResult Index()
        {
            var userRole = _sessionService.GetUserRole();
            IEnumerable<TicketModel> tickets = _ticketRepository.GetAll();

            var ticketsView = new TicketsViewModel
            {
                Tickets = tickets,
                UserRole = userRole
            };

            return View(ticketsView);
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Edit(int id)
        {
            var ticket = _ticketRepository.GetById(id);
            if (ticket == null)
            {
                return NotFound("Ticket not found.");
            }

            // Converting TicketModel to TicketDto
            var ticketDto = new TicketDto
            {
                Id = ticket.Id,
                Title = ticket.Title,
                Description = ticket.Description,
                Status = ticket.Status,
                Category = ticket.Category,
                Priority = ticket.Priority
            };

            return View(ticketDto);
        }

        public IActionResult DeleteConfirm(int id)
        {
            TicketModel ticket = _ticketRepository.GetById(id);
            return View(ticket);
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
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Ticket was not registered. Try again, error detail: {ex.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Edit(TicketDto ticketDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var existingTicket = ticketDto.Id.HasValue? _ticketRepository.GetById(ticketDto.Id.Value) : null;
                    if (existingTicket == null)
                    {
                        return NotFound("Ticket not found");
                    }

                    existingTicket.Title = ticketDto.Title;
                    existingTicket.Description = ticketDto.Description;
                    existingTicket.Status = ticketDto.Status;
                    existingTicket.Category = ticketDto.Category;
                    existingTicket.Priority = ticketDto.Priority;
                    existingTicket.UpdatedAt = DateTime.Now;

                    _ticketRepository.Update(existingTicket);

                    TempData["SuccessMessage"] = "Ticket updated successfully.";
                    return RedirectToAction("Index");
                }
                return View(ticketDto);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error ocurred. Error details: {ex.Message}";
                return View(ticketDto);
            }
        }

        public IActionResult Delete(int id)
        {
            try
            {
                bool success = _ticketRepository.Delete(id);
                if (success)
                {
                    TempData["SuccessMessage"] = "Ticket deleted successfully.";
                }
                else
                {
                    TempData["ErrorMessage"] = "Error. Ticket was not deleted.";
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error. Ticket was not deleted. Details: {ex.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
