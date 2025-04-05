using System.Collections.Generic;
using System.Linq;
using DataAccess;
using Domain;
using Domain;

namespace DataAccess
{
    public class PollRepository
    {
        private readonly PollDbContext _context;

        public PollRepository(PollDbContext context)
        {
            _context = context;
        }

        // Already done
        public void CreatePoll(string title, string option1Text, string option2Text, string option3Text)
        {
            var poll = new Poll
            {
                Title = title,
                Option1Text = option1Text,
                Option2Text = option2Text,
                Option3Text = option3Text,
                Option1VotesCount = 0,
                Option2VotesCount = 0,
                Option3VotesCount = 0,
                DateCreated = DateTime.UtcNow
            };

            _context.Polls.Add(poll);
            _context.SaveChanges();
        }

        // ✅ Get all polls, sorted by date (LINQ)
        public List<Poll> GetAllPolls()
        {
            return _context.Polls
                .OrderByDescending(p => p.DateCreated)
                .ToList();
        }

        // ✅ Find poll by ID
        public Poll? GetPollById(int id)
        {
            return _context.Polls.FirstOrDefault(p => p.Id == id);
        }

        // ✅ Update poll
        public void UpdatePoll(Poll updatedPoll)
        {
            _context.Polls.Update(updatedPoll);
            _context.SaveChanges();
        }

        // ✅ Delete poll
        public void DeletePoll(int id)
        {
            var poll = _context.Polls.FirstOrDefault(p => p.Id == id);
            if (poll != null)
            {
                _context.Polls.Remove(poll);
                _context.SaveChanges();
            }
        }
    }
}
