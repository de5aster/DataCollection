using Microsoft.EntityFrameworkCore;

namespace DataCollectionService.Entities
{
    public sealed class ClientCardContext : DbContext
    {
        public ClientCardContext(DbContextOptions options)
           : base(options)
        {
            this.Database.EnsureCreated();
        }

        public ClientCardContext()
            : base()
        {
            this.Database.EnsureCreated();
        }

        public DbSet<ClientCard> ClientCards { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=clientcardsdb;Trusted_Connection=True;");
            }

            base.OnConfiguring(optionsBuilder);
        }
    }
}
