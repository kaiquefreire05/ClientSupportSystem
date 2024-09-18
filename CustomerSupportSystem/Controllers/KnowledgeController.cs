using CustomerSupportSystem.DTOs;
using CustomerSupportSystem.Helper.Interfaces;
using CustomerSupportSystem.Models;
using CustomerSupportSystem.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CustomerSupportSystem.Controllers
{
    public class KnowledgeController : Controller
    {
        // Dependencies Injection
        private readonly IKnowledgeRepository _knowledgeRepository;
        private readonly ISessionService _sessionService;

        public KnowledgeController(IKnowledgeRepository knowledgeRepository, ISessionService sessionService)
        {
            _knowledgeRepository = knowledgeRepository;
            _sessionService = sessionService;
        }

        public IActionResult Index()
        {
            var knowledges = _knowledgeRepository.GetAllWithUser();
            return View(knowledges);
        }

        public IActionResult Details(int id)
        {
            var article = _knowledgeRepository.GetByIdWithUser(id);
            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult YourArticles()
        {
            var userId = _sessionService.GetUserId();
            var yoursArticles = _knowledgeRepository.GetArticlesByUserId(userId.Value);
            return View(yoursArticles);
        }

        public IActionResult DeleteConfirm(int id)
        {
            var article = _knowledgeRepository.GetById(id);
            return View(article);
        }

        public IActionResult Edit(int id)
        {
            var article = _knowledgeRepository.GetById(id);
            if (article == null)
            {
                return NotFound("Article not found.");
            }

            // Converting KnowledgeBaseModel to KnowledgeDto
            var articleDto = new KnowledgeDto
            {
                Id = article.Id,
                Title = article.Title,
                Content = article.Content,
                Category = article.Category,
                CreatedByUserId = article.CreatedByUserId
            };

            return View(articleDto);
        }

        [HttpGet]
        public IActionResult Search(string query)
        {
            ViewData["query"] = query;

            // Checking if the query field is empty
            if (string.IsNullOrEmpty(query))
            {
                var allArticles = _knowledgeRepository.GetAllWithUser();
                return View("Index", allArticles);
            }

            // Filtering articles when searching contains the search field
            var filteredArticles = _knowledgeRepository.GetAllWithUser()
                .Where(kb => kb.Title.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                             kb.Content.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                             kb.Category.ToString().Contains(query, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (!filteredArticles.Any())
            {
                TempData["NoResults"] = "No result found for your search.";
                return RedirectToAction("Index");
            }

            return View("Index", filteredArticles);
        }

        [HttpPost]
        public IActionResult Create(KnowledgeDto knowledgeDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userId = _sessionService.GetUserId();
                    var knowledgeModel = new KnowledgeBaseModel
                    {
                        Title = knowledgeDto.Title,
                        Content = knowledgeDto.Content,
                        Category = knowledgeDto.Category,
                        CreatedByUserId = userId.Value,
                        CreatedAt = DateTime.Now
                    };
                    _knowledgeRepository.Create(knowledgeModel);
                    TempData["SuccessMessage"] = "Knowledge Base created successfully.";
                    return RedirectToAction("Index");
                }

                TempData["ErrorMessage"] = "Invalid data. Plese check the form.";
                return View("Create", knowledgeDto);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Article was not registered. Try again, error detail: {ex.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Edit(KnowledgeDto knowledgeDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var existingArticle = knowledgeDto.Id.HasValue
                        ? _knowledgeRepository.GetById(knowledgeDto.Id.Value)
                        : null;
                    if (existingArticle == null)
                    {
                        return NotFound("Article not found.");
                    }

                    // Updating values
                    existingArticle.Title = knowledgeDto.Title;
                    existingArticle.Content = knowledgeDto.Content;
                    existingArticle.Category = knowledgeDto.Category;
                    existingArticle.UpdatedAt = DateTime.Now;

                    _knowledgeRepository.Update(existingArticle);

                    TempData["SuccessMessage"] = "Article updated successfully.";
                    return RedirectToAction("YourArticles");
                }

                return View(knowledgeDto);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error ocurred. Error details: {ex.Message}";
                return View(knowledgeDto);
            }
        }

        public IActionResult Delete(int id)
        {
            try
            {
                var article = _knowledgeRepository.GetById(id);
                var success = _knowledgeRepository.Delete(id);
                if (success)
                {
                    TempData["SuccessMessage"] = "Article deleted successfully.";
                }
                else
                {
                    TempData["ErrorMessage"] = "Article was not deleted.";
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error. Article was not deleted. Error details: {ex.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}