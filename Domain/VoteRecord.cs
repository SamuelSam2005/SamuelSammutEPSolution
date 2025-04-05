using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class VoteRecord
    {
        public int Id { get; set; }
        public int PollId { get; set; }
        public string UserId { get; set; }
        public DateTime VoteDate { get; set; } = DateTime.UtcNow;
    }
}
