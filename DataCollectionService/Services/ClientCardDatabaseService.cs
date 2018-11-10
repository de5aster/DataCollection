using System;
using System.Collections.Generic;
using System.Linq;
using DataCollectionService.Entities;
using DataCollectionService.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace DataCollectionService.Services
{
    public class ClientCardDatabaseService
    {
        private readonly DbContextOptions<ClientCardContext> options;

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
            var res = context.ClientCards.FirstOrDefault(p => p.ContractId == clientCard.ContractId);
            if (res == null)
            {
                context.Add(clientCard);
                context.SaveChanges();
            }
            else
            {
                throw new DatabaseException("ContractId already exists");
            }
        }

        public IList<ClientCard> GetAllClientCardsWithContext(ClientCardContext context)
        {
            var clientCards = context.ClientCards.ToList();
            var orderedEnumerable = clientCards.OrderBy(c => c.ContractId).Select(c => c).ToList();
            return orderedEnumerable;
        }

        public int GetContractCountWithContext(ClientCardContext context)
        {
            return context.ClientCards.Count();
        }
    }
}
