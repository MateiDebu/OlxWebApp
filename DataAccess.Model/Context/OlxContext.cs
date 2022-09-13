using DataAccess.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace DataAccess.Database.Context
{
    public class OlxContext : IdentityDbContext<ApplicationUser, IdentityRole<int>,int>
    {
        public OlxContext(DbContextOptions<OlxContext> options) : base(options)
        {}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<Ad> Announcements { get; set; }
    }
}
