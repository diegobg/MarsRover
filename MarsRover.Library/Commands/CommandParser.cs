using System;
using System.Text.RegularExpressions;

namespace MarsRover.Commands
{
    public class CommandParser
    {
        private static readonly Regex _turnCommandRegex = new Regex(@"^Left|Right$", RegexOptions.IgnoreCase);
        private static readonly Regex _moveCommandRegex = new Regex(@"^(\d+)m$", RegexOptions.IgnoreCase);

        public RoverCommand Parse(string input)
        {
            input = input.Trim();

            var turnMatch = _turnCommandRegex.Match(input);

            if (turnMatch.Success)
            {
                var turnDirection = (TurnDirection)Enum.Parse(typeof(TurnDirection), input, true);

                return new TurnCommand(turnDirection);
            }

            var moveMatch = _moveCommandRegex.Match(input);

            if (moveMatch.Success)
            {
                var distance = int.Parse(moveMatch.Groups[1].Value);

                return new MoveCommand(distance);
            }

            return null;
        }
    }
}
