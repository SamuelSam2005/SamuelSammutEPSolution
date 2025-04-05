using Microsoft.AspNetCore.Identity.EntityFrameworkCore;  // For IdentityDbContext
using Microsoft.EntityFrameworkCore;                      // For DbContext, DbSet
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
    }
}
