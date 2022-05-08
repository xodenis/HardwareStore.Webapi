using System;
using System.Runtime.Serialization;

namespace HardwareStore.Core.CustomExceptions
{
    public class AddProductException : Exception
    {
        public AddProductException()
        {
        }

        public AddProductException(string message) : base(message)
        {
        }

        public AddProductException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AddProductException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
