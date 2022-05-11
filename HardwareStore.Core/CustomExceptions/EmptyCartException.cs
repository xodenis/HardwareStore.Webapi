using System;
using System.Runtime.Serialization;

namespace HardwareStore.Core.CustomExceptions
{
    public class EmptyCartException : Exception
    {
        public EmptyCartException()
        {
        }

        public EmptyCartException(string message) : base(message)
        {
        }

        public EmptyCartException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected EmptyCartException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
