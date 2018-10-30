using System;
using DataCollectionService.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataCollectionService.Services
{
    public class ClientCardDatabaseService
    {
        // private DbContextOptions<ClientCardContext> options;

        // public ClientCardDatabaseService()
        // {
        //    var optionsBuilder = new DbContextOptionsBuilder<ClientCardContext>();
        //    optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=clientcards1db;Trusted_Connection=True;");
        //    this.options = optionsBuilder.Options;
        // }
        public void AddClientCard(ClientCard clientCard)
        {
            try
            {
            using (var dbContext = new ClientCardContext())
            {
                dbContext.Add(clientCard);
                dbContext.SaveChanges();
            }
            }
            catch (Exception)
            {
            throw new ArgumentException("Result Add method is invalid");
            }
        }
    }
}
