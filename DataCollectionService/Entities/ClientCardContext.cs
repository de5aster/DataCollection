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

        public DbSet<ClientCard> ClientCards { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=clientcardstestdb;Trusted_Connection=True;");
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Works>(
                b =>
                {
                    b.HasKey("WorkId");
                    b.Property(e => e.Work);
                });
            modelBuilder.Entity<Works>()
                .HasOne(p => p.ClientCard)
                .WithMany(m => m.WorkList);
            modelBuilder.Entity<RepairEquipment>(
                b =>
                {
                    b.HasKey("Id");
                    b.Property(e => e.Name);
                    b.Property(e => e.Count);
                });
            modelBuilder.Entity<RepairEquipment>()
                .HasOne(p => p.ClientCard)
                .WithMany(p => p.RepairEquipments);
        }
    }
}
