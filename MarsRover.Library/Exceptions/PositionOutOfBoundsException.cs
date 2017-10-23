using System;
using System.Runtime.Serialization;

namespace MarsRover.Exceptions
{
    public class PositionOutOfBoundsException : Exception
    {
        public PositionOutOfBoundsException()
            : base("The distance entered would cause the rover to go outside the exploration area.")
        {
        }

        public PositionOutOfBoundsException(string message) : base(message)
        {
        }

        public PositionOutOfBoundsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected PositionOutOfBoundsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
