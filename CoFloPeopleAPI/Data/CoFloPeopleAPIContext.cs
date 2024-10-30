using Microsoft.EntityFrameworkCore;

namespace CoFloPeopleAPI.Data
{
    public class CoFloPeopleAPIContext : DbContext
    {
        public CoFloPeopleAPIContext (DbContextOptions<CoFloPeopleAPIContext> options)
            : base(options)
        {
        }

        public DbSet<PersonModelDB> PersonModel { get; set; } = default!;
        public DbSet<PersonModelDB> PersonList { get; set; } = default!;
    }
}
