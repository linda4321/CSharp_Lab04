using System;
using System.Runtime.Serialization;

namespace Lab04Magur.Exceptions
{
    [Serializable]
    class InvalidNameException : Exception
    {
        public InvalidNameException() : base() { }

        public InvalidNameException(string message) : base(message) { }

        public InvalidNameException(string message, Exception inner) : base(message, inner) { }

        protected InvalidNameException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
