namespace MarsRover.Commands
{
    public interface ITurnCommandReceiver
    {
        void Turn(TurnDirection direction);
    }
}
