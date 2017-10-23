namespace MarsRover
{
    public class RoverGridPositionAndHeading
    {
        public int Position { get; set; }

        public Orientation Orientation { get; set; }

        public RoverGridPositionAndHeading(int position, Orientation orientation)
        {
            Position = position;
            Orientation = orientation;
        }

        public override string ToString()
        {
            return $"{Position} {Orientation}";
        }
    }
}
