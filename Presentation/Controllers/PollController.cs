using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using DataAccess;
using Domain;
using Presentation.Filters;

namespace Presentation.Controllers
{
    public class PollController : Controller
    {
        private readonly IPollRepository _pollRepository;

        public PollController(IPollRepository pollRepository)
        {
            _pollRepository = pollRepository;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(string title, string option1, string option2, string option3)
        {
            CreatePoll(_pollRepository, title, option1, option2, option3);
            return RedirectToAction("Index");
        }

        private void CreatePoll(IPollRepository repo, string title, string o1, string o2, string o3)
        {
            repo.CreatePoll(title, o1, o2, o3);
        }

        public IActionResult Index()
        {
            var polls = _pollRepository.GetAllPolls();
            return View(polls);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var poll = _pollRepository.GetPollById(id);
            if (poll == null)
                return NotFound();

            return View(poll);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit(Poll poll)
        {
            if (!ModelState.IsValid)
                return View(poll);

            _pollRepository.UpdatePoll(poll);
            return RedirectToAction("Index");
        }

        [Authorize]
        [HttpPost]
        public IActionResult Delete(int id)
        {
            _pollRepository.DeletePoll(id);
            return RedirectToAction("Index");
        }

        [Authorize]
        [HttpGet]
        public IActionResult Vote(int id)
        {
            var poll = _pollRepository.GetPollById(id);
            if (poll == null)
                return NotFound();

            return View(poll);
        }

        [Authorize]
        [HttpPost]
        [PreventMultipleVotes]
        public IActionResult Vote(int pollId, int selectedOption)
        {
            // Get the current user id from claims
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _pollRepository.Vote(pollId, selectedOption, userId);
            return RedirectToAction("Index");
        }

        public IActionResult Results(int id)
        {
            var poll = _pollRepository.GetPollById(id);
            if (poll == null)
                return NotFound();

            return View(poll);
        }
    }
}
