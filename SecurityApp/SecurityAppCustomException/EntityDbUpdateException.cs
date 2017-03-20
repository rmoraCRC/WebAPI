using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SecurityAppCustomException
{
    [Serializable]
    public class EntityDbUpdateException : Exception
    {      
        public EntityDbUpdateException()
        {
        }

        public EntityDbUpdateException(string message) : base(message)
        {
        }

        public EntityDbUpdateException(string message, Exception innerException) : base(message, innerException)
        {

        }

        protected EntityDbUpdateException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public override string StackTrace => string.Empty;
    }
}
