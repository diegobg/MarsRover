namespace MarsRover
{
    public class SurfaceArea
    {
        public int Height { get; }

        public int Width { get; }
        
        public SurfaceArea(int width = 100, int height = 100)
        {
            Width = width;
            Height = height;
        }

        public Position FixOutOfBoundsPosition(Position position)
        {
            var newLeft = position.Left > Width ? Width : position.Left;
            var newTop = position.Top > Height ? Height : position.Top;

            if (newLeft < 1)
            {
                newLeft = 1;
            }

            if (newTop < 1)
            {
                newTop = 1;
            }

            return new Position(newTop, newLeft);
        }

        public int GridReferenceFromPosition(Position position)
        {
            return (position.Top - 1) * Width + position.Left;
        }

        public bool PositionIsOutOfBounds(Position position)
        {
            if (position.Top > Height)
            {
                return true;
            }

            if (position.Top < 1)
            {
                return true;
            }

            if (position.Left > Width)
            {
                return true;
            }

            if (position.Left < 1)
            {
                return true;
            }

            return false;
        }

        public Position PositionFromGridReference(int gridReference)
        {
            var left = gridReference % Width;

            if (left == 0)
                left = Width;

            var top = (gridReference - left) / Width + 1;

            return new Position(top, left);
        }
    }
}
