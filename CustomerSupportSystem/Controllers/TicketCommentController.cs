using CustomerSupportSystem.DTOs;
using CustomerSupportSystem.Helper.Interfaces;
using CustomerSupportSystem.Models;
using CustomerSupportSystem.Repositories.Interfaces;
using CustomerSupportSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CustomerSupportSystem.Controllers
{
    public class TicketCommentController : Controller
    {
        // Dependencies Injection
        private readonly ITicketCommentRepository _commentRepository;
        private readonly ITicketRepository _ticketRepository;
        private readonly ISessionService _sessionService;

        public TicketCommentController(ITicketCommentRepository commentRepository, ITicketRepository ticketRepository
            , ISessionService sessionService)
        {
            _commentRepository = commentRepository;
            _ticketRepository = ticketRepository;
            _sessionService = sessionService;
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

            return View(ticketsCommentView);
        }

        public IActionResult Create(int ticketId)
        {

            var ticketCommentDto = new TicketCommentDto
            {
                TicketId = ticketId
            };
            return View(ticketCommentDto);
        }

        public IActionResult Edit(int id)
        {
            var comment = _commentRepository.GetById(id);
            if (comment == null)
            {
                return NotFound("Comment not found.");
            }

            var commentDto = new TicketCommentDto
            {
                Id = comment.Id,
                CommentText = comment.CommentText,
                TicketId = comment.TicketId,
                UserId = comment.UserId
            };

            return View(commentDto);
        }

        public IActionResult DeleteConfirm(int id)
        {
            TicketCommentModel comment = _commentRepository.GetById(id);
            return View(comment);
        }

        [HttpPost]
        public IActionResult Create(TicketCommentDto ticketCommentDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var ticketId = ticketCommentDto.TicketId;
                    var ticket = _ticketRepository.GetById(ticketId.Value);

                    if (ticket == null)
                    {
                        TempData["ErrorMessage"] = $"Ticket with ID {ticketId} not found.";
                        return RedirectToAction("Index", new { ticketId });
                    }

                    var userId = _sessionService.GetUserId();
                    var comment = new TicketCommentModel
                    {
                        CommentText = ticketCommentDto.CommentText,
                        TicketId = ticketCommentDto.TicketId.Value,
                        UserId = userId.Value,
                        CreatedAt = DateTime.UtcNow
                    };
                    _commentRepository.Create(comment);
                    TempData["SuccessMessage"] = "Comment added successfully.";
                    return RedirectToAction("Index", new { ticketId = ticketCommentDto.TicketId });
                }
                TempData["ErrorMessage"] = "Invalid data. Please check the form.";
                return View(ticketCommentDto);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Comment not added. Try again, error details: {ex.Message}";
                return RedirectToAction("Index", new { ticketId = ticketCommentDto.TicketId });
            }
        }

        [HttpPost]
        public IActionResult Edit(TicketCommentDto ticketCommentDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var existentComment = ticketCommentDto.Id.HasValue ? _commentRepository.GetById(ticketCommentDto.Id.Value) : null;
                    if (existentComment == null)
                    {
                        return NotFound("Comment not found.");
                    }

                    existentComment.CommentText = ticketCommentDto.CommentText;
                    existentComment.UpdatedAt = DateTime.UtcNow;

                    // Save changes to the database
                    _commentRepository.Update(existentComment);

                    TempData["SuccessMessage"] = "Comment updated successfully.";
                    return RedirectToAction("Index", new { ticketId = ticketCommentDto.TicketId });
                }
                TempData["ErrorMessage"] = "Invalid data, please check the form.";
                return View(ticketCommentDto);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred. Error details: {ex.Message}";
                return View(ticketCommentDto);
            }
        }

        public IActionResult Delete(int id)
        {
            var comment = _commentRepository.GetById(id);
            bool success =  _commentRepository.Delete(id);
            if (success)
            {
                TempData["SuccessMessage"] = "Comment deleted successfuly.";
            }
            else
            {
                TempData["ErrorMessage"] = "Comment was not deleted.";
            }
            return RedirectToAction("Index", new { ticketId = comment.TicketId });
        }
    }
}
