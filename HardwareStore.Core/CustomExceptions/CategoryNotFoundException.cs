using System;
using System.Runtime.Serialization;

namespace HardwareStore.Core.CustomExceptions
{
    public class CategoryNotFoundException : Exception
    {
        public CategoryNotFoundException()
        {
        }

        public CategoryNotFoundException(string message) : base(message)
        {
        }

        public CategoryNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CategoryNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
