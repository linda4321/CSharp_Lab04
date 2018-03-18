using System;
using System.Runtime.Serialization;

namespace Lab04Magur.Exceptions
{
    [Serializable]
    class NegativeAgeException : Exception
    {
        public NegativeAgeException() : base() { }

        public NegativeAgeException(string message) : base(message) { }

        public NegativeAgeException(string message, Exception inner) : base(message, inner) { }

        protected NegativeAgeException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
