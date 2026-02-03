using Microsoft.EntityFrameworkCore;
using StoryAPI1.Models;

namespace StoryAPI1.Data
{
    public class StoryDbContext: DbContext
    {
        public StoryDbContext(DbContextOptions<StoryDbContext> options) : base(options) { }

        public DbSet<Story> Stories { get; set; }
    }
}
