using System.Collections.Generic;
using MarsRover.Commands;
using MarsRover.Exceptions;

namespace MarsRover
{
    public class Rover : IMoveCommandReceiver, ITurnCommandReceiver
    {
        private readonly SurfaceArea _surfaceArea;
        private readonly Queue<RoverCommand> _commandQueue;

        public Position Position { get; internal set; }

        public int GridReference => _surfaceArea.GridReferenceFromPosition(Position);

        public Orientation Orientation { get; internal set; }

        public Rover(SurfaceArea area, int gridReference = 1, Orientation orientation = Orientation.South)
        {
            Position = area.PositionFromGridReference(gridReference);
            Orientation = orientation;
            _surfaceArea = area;
            _commandQueue = new Queue<RoverCommand>(5);
        }

        public void AddCommand(RoverCommand command)
        {
            if (_commandQueue.Count == 5)
                throw new CommandQueueDepthExceeded();

            _commandQueue.Enqueue(command);
        }

        public RoverGridPositionAndHeading ExecuteCommands()
        {
            while (_commandQueue.Count > 0)
            {
                var nextCommand = _commandQueue.Dequeue();

                nextCommand.Execute(this);
            }

            return new RoverGridPositionAndHeading(GridReference, Orientation);
        }

        public void Move(int distance)
        {
            var newPosition = Position.AddDistance(Orientation, distance);
            var isOutOfBounds = _surfaceArea.PositionIsOutOfBounds(newPosition);

            if (isOutOfBounds)
            {
                Position = _surfaceArea.FixOutOfBoundsPosition(newPosition);

                throw new PositionOutOfBoundsException();
            }

            Position = newPosition;
        }

        public void Turn(TurnDirection direction)
        {
            var newOrientation = (Orientation)((int) Orientation + direction);

            if (newOrientation > Orientation.West)
            {
                newOrientation = Orientation.North;
            }
            else if (newOrientation < Orientation.North)
            {
                newOrientation = Orientation.West;
            }

            Orientation = newOrientation;
        }
    }
}
