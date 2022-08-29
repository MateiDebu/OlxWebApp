using DataAccess.Database.Models;
using DataAccess.DataBase.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Database.Context
{
    public class OlxContext : DbContext
    {
        public OlxContext(DbContextOptions<OlxContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Ad> Announcements { get; set; }
    }
}
