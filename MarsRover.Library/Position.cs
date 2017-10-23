namespace MarsRover
{
    public struct Position
    {
        public int Top { get; }

        public int Left { get; }

        public Position(int top, int left)
        {
            Top = top;
            Left = left;
        }

        public Position AddDistance(Orientation orientation, int distance)
        {
            var newTop = Top;
            var newLeft = Left;

            switch (orientation)
            {
                case Orientation.North:
                    newTop -= distance;
                    break;
                case Orientation.East:
                    newLeft += distance;
                    break;
                case Orientation.South:
                    newTop += distance;
                    break;
                case Orientation.West:
                    newLeft -= distance;
                    break;
            }

            return new Position(newTop, newLeft);
        }
    }
}
