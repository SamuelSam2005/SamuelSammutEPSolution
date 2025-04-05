using Domain;

namespace DataAccess
{
    public interface IPollRepository
    {
        void CreatePoll(string title, string option1Text, string option2Text, string option3Text);
        List<Poll> GetAllPolls();
        Poll? GetPollById(int id);
        void UpdatePoll(Poll updatedPoll);
        void DeletePoll(int id);
        void Vote(int pollId, int optionNumber);
    }
}
