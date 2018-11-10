using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollectionService.Exceptions
{
    public class EntitiesException : Exception
    {
        public EntitiesException()
        {
        }

        public EntitiesException(string message)
            : base(message)
        {
        }
    }
}
