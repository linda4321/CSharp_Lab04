using System;
using System.Runtime.Serialization;

namespace Lab04Magur.Exceptions
{
    [Serializable]
    class InvalidEmailException : Exception
    {
        public InvalidEmailException() : base() { }

        public InvalidEmailException(string message) : base(message) { }

        public InvalidEmailException(string message, Exception inner) : base(message, inner) { }

        protected InvalidEmailException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
