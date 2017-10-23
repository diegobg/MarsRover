using System;

namespace MarsRover.Commands
{
    public class TurnCommand : RoverCommand
    {
        public TurnDirection Direction { get; }

        public TurnCommand(TurnDirection direction)
        {
            Direction = direction;
        }

        public override void Execute(Rover rover)
        {
            var receiver = rover as ITurnCommandReceiver;

            if (receiver == null)
                throw new ArgumentException($"Argument must implement {nameof(ITurnCommandReceiver)}", nameof(rover));

            receiver.Turn(Direction);
        }
    }
}
