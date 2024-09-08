using CustomerSupportSystem.DTOs;
using CustomerSupportSystem.Helper.Interfaces;
using CustomerSupportSystem.Models;
using CustomerSupportSystem.Repositories;
using CustomerSupportSystem.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CustomerSupportSystem.Controllers
{
    public class FeedbackController : Controller
    {
        // Dependencies Injection
        private readonly IFeedbackRepository _feedbackRepository;
        private readonly ITicketRepository _ticketRepository;
        private readonly ISessionService _sessionService;
        private readonly IUserRepository _userRepository;

        public FeedbackController(IFeedbackRepository feedbackRepository, ITicketRepository ticketRepository
            , ISessionService sessionServive, IUserRepository userRepository)
        {
            _feedbackRepository = feedbackRepository;
            _ticketRepository = ticketRepository;
            _sessionService = sessionServive;
            _userRepository = userRepository;
        }

        public IActionResult Index()
        {
            IEnumerable<TicketModel> ticketsWithFeedback = _feedbackRepository.GetTicketsWithFeedback();
            return View(ticketsWithFeedback);
        }

        public IActionResult Create(int ticketId)
        {
            var userId = _sessionService.GetUserId();
            if (!userId.HasValue)
            {
                TempData["ErrorMessage"] = "User is not logged in.";
                return RedirectToAction("Login", "User");
            }

            var user = _userRepository.GetById(userId.Value);
            if (user == null)
            {
                TempData["ErrorMessage"] = "Invalid User ID.";
                return RedirectToAction("Index", "Ticket");
            }

            var feedbackDto = new FeedbackDto
            {
                TicketId = ticketId,
                UserId = userId.Value
            };

            return View(feedbackDto);
        }

        public IActionResult Edit(int id)
        {
            var userId = _sessionService.GetUserId();
            if (!userId.HasValue)
            {
                TempData["ErrorMessage"] = "User is not logged in.";
                return RedirectToAction("Login", "User");
            }

            var feedback = _feedbackRepository.GetById(id);
            if (feedback == null)
            {
                TempData["ErrorMessage"] = "Invalid Feedback ID.";
                return RedirectToAction("ClosedTickets", "Ticket");
            }

            var feedbackDto = new FeedbackDto
            {
                Rating = feedback.Rating,
                Comments = feedback.Comments,
                TicketId = feedback.TicketId,
                UserId = userId.Value
            };
            return View(feedbackDto);
        }

        public IActionResult AllFeedbacks()
        {
            IEnumerable<FeedbackModel> feedbacks = _feedbackRepository.GetAll();
            return View(feedbacks);
        }

        public IActionResult DeleteConfirm(int id)
        {
            var findedFeedback = _feedbackRepository.GetById(id);
            return View(findedFeedback);
        }

        [HttpPost]
        public IActionResult Edit(FeedbackDto feedbackDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var existentFeedback = feedbackDto.Id.HasValue ? _feedbackRepository.GetById(feedbackDto.Id.Value) : null;
                    if (existentFeedback == null)
                    {
                        return NotFound("Comment not found.");
                    }

                    existentFeedback.Rating = feedbackDto.Rating;
                    existentFeedback.Comments = feedbackDto.Comments;
                    existentFeedback.UpdatedAt = DateTime.Now;

                    _feedbackRepository.Update(existentFeedback);
                    TempData["SuccessMessage"] = "Feedback updated successfully.";
                    return RedirectToAction("");
                }
                TempData["ErrorMessage"] = "Invalid data, please check the form.";
                return View(feedbackDto);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred. Error details: {ex.Message}";
                return View("ClosedTickets", "Ticket");
            }
        }

        [HttpPost]
        public IActionResult Create(FeedbackDto feedbackDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TicketModel ticket = _ticketRepository.GetTicketWithFeedback(feedbackDto.TicketId);

                    // Check if the ticket exists and if its status is closed
                    if (ticket == null || ticket.Status != Enums.StatusEnum.CLOSED)
                    {
                        TempData["ErrorMessage"] = $"Ticket with id: {feedbackDto.TicketId} not found or not yet closed.";
                        return RedirectToAction("Index", "Ticket");
                    }

                    // Check if the ticket already has feedback associated with it
                    if (ticket.Feedback != null)
                    {
                        TempData["ErrorMessage"] = "This ticket already has feedback associated with it.";
                        return RedirectToAction("Index", "Ticket");
                    }

                    // Retrieve the user by their ID
                    var user = _userRepository.GetById(feedbackDto.UserId);
                    if (user == null)
                    {
                        TempData["ErrorMessage"] = "Invalid User ID.";
                        return View(feedbackDto);
                    }

                    // Create a new feedback instance
                    var feedback = new FeedbackModel
                    {
                        TicketId = feedbackDto.TicketId,
                        UserId = feedbackDto.UserId,
                        Comments = feedbackDto.Comments,
                        Rating = feedbackDto.Rating,
                        CreatedAt = DateTime.Now,
                    };

                    _feedbackRepository.Create(feedback);
                    TempData["SuccessMessage"] = "Feedback added successfully.";
                    return RedirectToAction("Index", "Ticket");
                }
                TempData["ErrorMessage"] = "Invalid data. Please check the form.";
                return View(feedbackDto);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred while adding feedback. Please try again. Error details: {ex.Message}";
                return View(feedbackDto);
            }
        }

        public IActionResult Delete(int id)
        {
            bool success = _feedbackRepository.Delete(id);
            if (success)
            {
                TempData["SuccessMessage"] = "Feedback deleted successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Feedback was not deleted.";
            }
            return RedirectToAction("AllFeedbacks");
        }

    }
}
