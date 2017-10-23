using MarsRover.Commands;
using MarsRover.Exceptions;
using NUnit.Framework;

namespace MarsRover
{
    [TestFixture]
    public class RoverTest
    {
        private Rover _rover;

        [SetUp]
        public void SetUp()
        {
            _rover = new Rover(new SurfaceArea());
        }

        [Test]
        public void Turn_CanTurnLeft()
        {
            _rover.Turn(TurnDirection.Left);

            Assert.AreEqual(Orientation.East, _rover.Orientation);
        }

        [Test]
        public void Turn_CanTurnLeftTwice()
        {
            _rover.Turn(TurnDirection.Left);
            _rover.Turn(TurnDirection.Left);

            Assert.AreEqual(Orientation.North, _rover.Orientation);
        }

        [Test]
        public void Turn_CanTurnLeftThrice()
        {
            _rover.Turn(TurnDirection.Left);
            _rover.Turn(TurnDirection.Left);
            _rover.Turn(TurnDirection.Left);

            Assert.AreEqual(Orientation.West, _rover.Orientation);
        }

        [Test]
        public void Turn_CanTurnLeftFullCircle()
        {
            _rover.Turn(TurnDirection.Left);
            _rover.Turn(TurnDirection.Left);
            _rover.Turn(TurnDirection.Left);
            _rover.Turn(TurnDirection.Left);

            Assert.AreEqual(Orientation.South, _rover.Orientation);
        }

        [Test]
        public void Turn_CanTurnRight()
        {
            _rover.Turn(TurnDirection.Right);

            Assert.AreEqual(Orientation.West, _rover.Orientation);
        }

        [Test]
        public void Turn_CanTurnRightTwice()
        {
            _rover.Turn(TurnDirection.Right);
            _rover.Turn(TurnDirection.Right);

            Assert.AreEqual(Orientation.North, _rover.Orientation);
        }

        [Test]
        public void Turn_CanTurnRightThrice()
        {
            _rover.Turn(TurnDirection.Right);
            _rover.Turn(TurnDirection.Right);
            _rover.Turn(TurnDirection.Right);

            Assert.AreEqual(Orientation.East, _rover.Orientation);
        }

        [Test]
        public void Turn_CanTurnRightFullCircle()
        {
            _rover.Turn(TurnDirection.Right);
            _rover.Turn(TurnDirection.Right);
            _rover.Turn(TurnDirection.Right);
            _rover.Turn(TurnDirection.Right);

            Assert.AreEqual(Orientation.South, _rover.Orientation);
        }

        [TestCase(50, 5001)]
        [TestCase(99, 9901)]
        public void Move_CanMoveSouth(int distance, int expectedPosition)
        {
            _rover.Move(distance);

            Assert.AreEqual(expectedPosition, _rover.GridReference);
        }

        [TestCase(50, 51)]
        [TestCase(99, 100)]
        public void Move_CanMoveEast(int distance, int expectedPosition)
        {
            _rover.Orientation = Orientation.East;

            _rover.Move(distance);

            Assert.AreEqual(expectedPosition, _rover.GridReference);
        }

        [TestCase(50, 4901)]
        [TestCase(99, 1)]
        public void Move_CanMoveNorth(int distance, int expectedPosition)
        {
            _rover.Position = new Position(100, 1);
            _rover.Orientation = Orientation.North;

            _rover.Move(distance);

            Assert.AreEqual(expectedPosition, _rover.GridReference);
        }

        [TestCase(50, 50)]
        [TestCase(99, 1)]
        public void Move_CanMoveWest(int distance, int expectedPosition)
        {
            _rover.Position = new Position(1, 100);
            _rover.Orientation = Orientation.West;

            _rover.Move(distance);

            Assert.AreEqual(expectedPosition, _rover.GridReference);
        }

        [TestCase(100)]
        [TestCase(101)]
        [TestCase(150)]
        public void Move_CantMovePastSouthBoundary(int distance)
        {
            Assert.Throws<PositionOutOfBoundsException>(() => _rover.Move(distance));

            Assert.AreEqual(9901, _rover.GridReference);
        }

        [TestCase(100)]
        [TestCase(101)]
        [TestCase(150)]
        public void Move_CantMovePastNorthBoundary(int distance)
        {
            _rover.Position = new Position(99, 1);
            _rover.Orientation = Orientation.North;

            Assert.Throws<PositionOutOfBoundsException>(() => _rover.Move(distance));

            Assert.AreEqual(1, _rover.GridReference);
        }

        [TestCase(100)]
        [TestCase(101)]
        [TestCase(150)]
        public void Move_CantMovePastEastBoundary(int distance)
        {
            _rover.Orientation = Orientation.East;

            Assert.Throws<PositionOutOfBoundsException>(() => _rover.Move(distance));

            Assert.AreEqual(100, _rover.GridReference);
        }

        [TestCase(100)]
        [TestCase(101)]
        [TestCase(150)]
        public void Move_CantMovePastWestBoundary(int distance)
        {
            _rover.Position = new Position(1, 100);
            _rover.Orientation = Orientation.West;

            Assert.Throws<PositionOutOfBoundsException>(() => _rover.Move(distance));

            Assert.AreEqual(1, _rover.GridReference);
        }

        [Test]
        public void AddCommand_CantAddMoreThanFive()
        {
            _rover.AddCommand(new MoveCommand(50));
            _rover.AddCommand(new TurnCommand(TurnDirection.Left));
            _rover.AddCommand(new MoveCommand(23));
            _rover.AddCommand(new TurnCommand(TurnDirection.Left));
            _rover.AddCommand(new MoveCommand(4));

            Assert.Throws<CommandQueueDepthExceeded>(() => _rover.AddCommand(new TurnCommand(TurnDirection.Left)));
        }

        [Test]
        public void ExecuteCommands()
        {
            _rover.AddCommand(new MoveCommand(50));
            _rover.AddCommand(new TurnCommand(TurnDirection.Left));
            _rover.AddCommand(new MoveCommand(23));
            _rover.AddCommand(new TurnCommand(TurnDirection.Left));
            _rover.AddCommand(new MoveCommand(4));

            var result = _rover.ExecuteCommands();

            Assert.AreEqual("4624 North", result.ToString());
        }

        [Test]
        public void ExecuteCommands_StopsWhenOutOfBounds()
        {
            _rover.AddCommand(new MoveCommand(50));
            _rover.AddCommand(new TurnCommand(TurnDirection.Left));
            _rover.AddCommand(new MoveCommand(100));
            _rover.AddCommand(new TurnCommand(TurnDirection.Left));
            _rover.AddCommand(new MoveCommand(4));

            Assert.Throws<PositionOutOfBoundsException>(() => _rover.ExecuteCommands());

            Assert.AreEqual(5100, _rover.GridReference);
        }
    }
}
