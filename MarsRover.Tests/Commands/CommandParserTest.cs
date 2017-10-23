using NUnit.Framework;

namespace MarsRover.Commands
{
    [TestFixture]
    public class CommandParserTest
    {
        private CommandParser _commandParser;

        [SetUp]
        public void SetUp()
        {
            _commandParser = new CommandParser();
        }

        [Test]
        public void Parse_TurnCommand()
        {
            var command = _commandParser.Parse("Left") as TurnCommand;

            Assert.IsNotNull(command);
            Assert.AreEqual(TurnDirection.Left, command.Direction);
        }

        [Test]
        public void Parse_MoveCommand()
        {
            var command = _commandParser.Parse("50m") as MoveCommand;

            Assert.IsNotNull(command);
            Assert.AreEqual(50, command.Distance);
        }

        [Test]
        public void Parse_Invalid()
        {
            var command = _commandParser.Parse("Foo");

            Assert.IsNull(command);
        }
    }
}
