using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace SoccerTeamsManager.Exceptions
{
    [Serializable]
    public class PlayerNotFoundException : Exception
    {
        public PlayerNotFoundException()
        {
        }

        public PlayerNotFoundException(string message)
            : base(message)
        {
        }

        public PlayerNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected PlayerNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
