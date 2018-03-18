using System;
using System.Runtime.Serialization;

namespace Lab04Magur.Exceptions
{
    [Serializable]
    class DiedPersonException : Exception
    {
        public DiedPersonException() : base() { }

        public DiedPersonException(string message) : base(message) { }

        public DiedPersonException(string message, Exception inner) : base(message, inner) { }

        protected DiedPersonException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
