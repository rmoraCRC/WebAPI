using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Runtime.Serialization;
using System.Text;

namespace SecurityAppCustomException
{
    [Serializable]
    public class EntityValidationException : Exception
    {
        public EntityValidationException(IEnumerable<DbEntityValidationResult> entityValidationErrors): base()
        {
            StringBuilder entityValidationErrorsMessage = new StringBuilder();
            foreach (var entityErr in entityValidationErrors)
            {
                foreach (var error in entityErr.ValidationErrors)
                {
                    entityValidationErrorsMessage.AppendFormat("Error Property Name {0} : Error Message: {1}",
                        error.PropertyName, error.ErrorMessage);
                }
            }

            Message = entityValidationErrorsMessage.ToString();
        }     

        public EntityValidationException(string message)
        : base(message) { }

        public EntityValidationException(string format, params object[] args)
        : base(string.Format(format, args)) { }

        public EntityValidationException(string message, Exception innerException)
        : base(message, innerException) { }

        public EntityValidationException(string format, Exception innerException, params object[] args)
        : base(string.Format(format, args), innerException) { }

        protected EntityValidationException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }

        public override string StackTrace => string.Empty;
        public override string Message { get; }
    }
}
