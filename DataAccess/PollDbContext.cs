using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Domain;

namespace DataAccess
{
    public class PollDbContext : IdentityDbContext
    {
        public PollDbContext(DbContextOptions<PollDbContext> options)
            : base(options)
        {
        }

        public DbSet<Poll> Polls { get; set; }

        // ✅ You must have this line:
        public DbSet<VoteRecord> VoteRecords { get; set; }
    }
}
