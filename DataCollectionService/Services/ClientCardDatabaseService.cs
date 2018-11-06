using System;
using System.Collections.Generic;
using System.Linq;
using DataCollectionService.Entities;
using DataCollectionService.Helpers;
using Microsoft.EntityFrameworkCore;

namespace DataCollectionService.Services
{
    public class ClientCardDatabaseService
    {
        private DbContextOptions<ClientCardContext> options;

        public ClientCardDatabaseService(DbContextOptions<ClientCardContext> options)
        {
            this.options = options;
        }

        public void AddClientCard(ClientCard clientCard)
        {
            try
            {
                using (var dbContext = new ClientCardContext(this.options))
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

        public List<ClientCard> GetAllClientCards()
        {
            List<ClientCard> clientCards;
            using (var db = new ClientCardContext(this.options))
            {
                clientCards = db.ClientCards.ToList();
                return clientCards;
            }
        }

        public void AddClientCardWithContext(ClientCard clientCard, ClientCardContext context)
        {
            try
            {
                    context.Add(clientCard);
                    context.SaveChanges();
            }
            catch (Exception)
            {
                throw new ArgumentException("Result Add method is invalid");
            }
        }

        public List<ClientCard> GetAllClientCardsWithContext(ClientCardContext context)
        {
            var clientCards = context.ClientCards.ToList();
            return clientCards;
        }
    }
}
