using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace SoccerTeamsManager.Exceptions
{
    [Serializable]
    public class CaptainNotFoundException : Exception
    {
        public CaptainNotFoundException()
        {
        }

        public CaptainNotFoundException(string message)
            : base(message)
        {
        }

        public CaptainNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected CaptainNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
