using System;
using Microsoft.EntityFrameworkCore;

namespace DataCollectionService.Entities
{
    public class ClientCardContext : DbContext
    {
        public ClientCardContext()
           : base()
        {
            this.Database.EnsureCreated();
        }

        public ClientCardContext(DbContextOptions<ClientCardContext> options)
            : base(options)
        {
            this.Database.EnsureCreated();
        }

        public DbSet<ClientCard> ClientCards { get; set; }

        public DbSet<RepairEquipment> RepairEquipments { get; set; }

        public DbSet<Work> Works { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Work>()
                .HasOne(c => c.ClientCard)
                .WithMany(w => w.Works)
                .HasForeignKey(c => c.ClientCardId);
            modelBuilder.Entity<RepairEquipment>()
                .HasOne(c => c.ClientCard)
                .WithMany(r => r.RepairEquipments)
                .HasForeignKey(c => c.ClientCardId);
        }
    }
}
