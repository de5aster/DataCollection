using System;
using System.Collections.Generic;
using System.Linq;
using DataCollectionService.Entities;
using DataCollectionService.Helpers;

namespace DataCollectionService.Services
{
    public class ClientCardDatabaseService
    {
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

        public List<ClientCard> GetAllClientCards()
        {
            List<ClientCard> clientCards;
            using (var db = new ClientCardContext())
            {
                clientCards = db.ClientCards.ToList();
            }

            return clientCards;
        }
    }
}
