using System;
using System.Runtime.Serialization;

namespace MarsRover.Exceptions
{
    public class CommandQueueDepthExceeded : Exception
    {
        public CommandQueueDepthExceeded()
            : base("The maximum number of commands has been reached")
        {
        }

        public CommandQueueDepthExceeded(string message) : base(message)
        {
        }

        public CommandQueueDepthExceeded(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CommandQueueDepthExceeded(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
