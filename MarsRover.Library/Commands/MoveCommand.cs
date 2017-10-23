using System;

namespace MarsRover.Commands
{
    public class MoveCommand : RoverCommand
    {
        public int Distance { get; }

        public MoveCommand(int distance)
        {
            Distance = distance;
        }

        public override void Execute(Rover rover)
        {
            var receiver = rover as IMoveCommandReceiver;

            if (receiver == null)
                throw new ArgumentException($"Argument must implement {nameof(IMoveCommandReceiver)}", nameof(rover));

            receiver.Move(Distance);
        }
    }
}
