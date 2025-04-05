using DataAccess;
using Domain;
using Microsoft.AspNetCore.Mvc;


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

        // ✅ GET: Create Poll Form
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // ✅ POST: Create Poll using Method Injection
        [HttpPost]
        public IActionResult Create(string title, string option1, string option2, string option3)
        {
            CreatePoll(_pollRepository, title, option1, option2, option3);
            return RedirectToAction("Index");
        }

        // ✅ Method Injection
        private void CreatePoll(PollRepository repo, string title, string o1, string o2, string o3)
        {
            repo.CreatePoll(title, o1, o2, o3);
        }

        // ✅ READ: Show all polls
        public IActionResult Index()
        {
            var polls = _pollRepository.GetAllPolls();
            return View(polls);
        }

        // ✅ GET: Edit poll by id
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var poll = _pollRepository.GetPollById(id);
            if (poll == null)
                return NotFound();

            return View(poll);
        }

        // ✅ POST: Save edited poll
        [HttpPost]
        public IActionResult Edit(Poll poll)
        {
            if (!ModelState.IsValid)
                return View(poll);

            _pollRepository.UpdatePoll(poll);
            return RedirectToAction("Index");
        }

        // ✅ POST: Delete poll
        [HttpPost]
        public IActionResult Delete(int id)
        {
            _pollRepository.DeletePoll(id);
            return RedirectToAction("Index");
        }
    }
}
