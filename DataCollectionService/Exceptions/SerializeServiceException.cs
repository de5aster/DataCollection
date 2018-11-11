using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollectionService.Exceptions
{
    public class SerializeServiceException : Exception
    {
        public SerializeServiceException()
        {
        }

        public SerializeServiceException(string message)
            : base(message)
        {
        }
    }
}
