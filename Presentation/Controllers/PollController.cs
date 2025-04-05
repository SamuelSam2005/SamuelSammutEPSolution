using Microsoft.AspNetCore.Mvc;
using DataAccess;
using Domain;

namespace Presentation.Controllers
{
    public class PollController : Controller
    {
        private readonly PollRepository _pollRepository;

        public PollController(PollRepository pollRepository)
        {
            _pollRepository = pollRepository;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(string title, string option1, string option2, string option3)
        {
            CreatePoll(_pollRepository, title, option1, option2, option3);
            return RedirectToAction("Index");
        }

        private void CreatePoll(PollRepository repo, string title, string o1, string o2, string o3)
        {
            repo.CreatePoll(title, o1, o2, o3);
        }

        public IActionResult Index()
        {
            var polls = _pollRepository.GetAllPolls();
            return View(polls);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var poll = _pollRepository.GetPollById(id);
            if (poll == null)
                return NotFound();

            return View(poll);
        }

        [HttpPost]
        public IActionResult Edit(Poll poll)
        {
            if (!ModelState.IsValid)
                return View(poll);

            _pollRepository.UpdatePoll(poll);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _pollRepository.DeletePoll(id);
            return RedirectToAction("Index");
        }

        // ✅ Step 7: Voting logic
        [HttpGet]
        public IActionResult Vote(int id)
        {
            var poll = _pollRepository.GetPollById(id);
            if (poll == null)
                return NotFound();

            return View(poll);
        }

        [HttpPost]
        public IActionResult Vote(int pollId, int selectedOption)
        {
            _pollRepository.Vote(pollId, selectedOption);
            return RedirectToAction("Index");
        }
    }
}
