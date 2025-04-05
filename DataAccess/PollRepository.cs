using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class PollRepository : IPollRepository
    {
        private readonly PollDbContext _context;

        public PollRepository(PollDbContext context)
        {
            _context = context;
        }

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

        public List<Poll> GetAllPolls()
        {
            return _context.Polls
                .OrderByDescending(p => p.DateCreated)
                .ToList();
        }

        public Poll? GetPollById(int id)
        {
            return _context.Polls.FirstOrDefault(p => p.Id == id);
        }

        public void UpdatePoll(Poll updatedPoll)
        {
            _context.Polls.Update(updatedPoll);
            _context.SaveChanges();
        }

        public void DeletePoll(int id)
        {
            var poll = _context.Polls.FirstOrDefault(p => p.Id == id);
            if (poll != null)
            {
                _context.Polls.Remove(poll);
                _context.SaveChanges();
            }
        }

        // New vote methods:
        public void Vote(int pollId, int optionNumber, string userId)
        {
            // Assumes that duplicate votes are already blocked by the ActionFilter.
            var poll = _context.Polls.FirstOrDefault(p => p.Id == pollId);
            if (poll == null) return;

            switch (optionNumber)
            {
                case 1:
                    poll.Option1VotesCount++;
                    break;
                case 2:
                    poll.Option2VotesCount++;
                    break;
                case 3:
                    poll.Option3VotesCount++;
                    break;
            }

            _context.Polls.Update(poll);
            _context.SaveChanges();

            // Record the vote
            RecordVote(pollId, userId);
        }

        public bool HasUserVoted(int pollId, string userId)
        {
            return _context.VoteRecords.Any(v => v.PollId == pollId && v.UserId == userId);
        }

        public void RecordVote(int pollId, string userId)
        {
            var voteRecord = new VoteRecord
            {
                PollId = pollId,
                UserId = userId,
                VoteDate = DateTime.UtcNow
            };
            _context.VoteRecords.Add(voteRecord);
            _context.SaveChanges();
        }
    }
}
