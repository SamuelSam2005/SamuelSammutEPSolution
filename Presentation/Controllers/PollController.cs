using DataAccess;
using Microsoft.AspNetCore.Mvc;
using DataAccess;

namespace Presentation.Controllers
{
    public class PollController : Controller
    {
        private readonly PollRepository _pollRepository;

        // ✅ Constructor Injection
        public PollController(PollRepository pollRepository)
        {
            _pollRepository = pollRepository;
        }

        // Show Create Form
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Handle Form Submission
        [HttpPost]
        public IActionResult Create(string title, string option1, string option2, string option3)
        {
            // ✅ Method Injection: inject via parameter
            CreatePoll(_pollRepository, title, option1, option2, option3);
            return RedirectToAction("Index"); // We'll implement Index next step
        }

        // ✅ Method Injection
        private void CreatePoll(PollRepository repo, string title, string o1, string o2, string o3)
        {
            repo.CreatePoll(title, o1, o2, o3);
        }
    }
}
