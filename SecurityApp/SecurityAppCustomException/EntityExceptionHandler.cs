using System;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Runtime.Serialization;

namespace SecurityAppCustomException
{
    [Serializable]
    public class EntityExceptionHandler : Exception
    {
        public EntityExceptionHandler()
        {
        }

        public EntityExceptionHandler(string message) : base(message)
        {
        }

        public EntityExceptionHandler(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected EntityExceptionHandler(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public EntityExceptionHandler(Exception exception)
        {
            if (exception.GetType() == typeof(DbEntityValidationException))
            {
                var dbException = (DbEntityValidationException)exception;
                throw new EntityValidationException(dbException.EntityValidationErrors);
            }
            else if (exception.GetType() == typeof(DbUpdateException))
            {
                var dbException = (DbUpdateException)exception;
                throw new EntityDbUpdateException(dbException.Message, dbException.InnerException);
            }
            else
                throw new Exception(exception.Message);
        }

        public override string StackTrace => string.Empty;
    }
}