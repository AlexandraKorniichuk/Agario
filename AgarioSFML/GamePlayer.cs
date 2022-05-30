namespace AgarioSFML
{
    public class GamePlayer
    {
        public Circle circle;
        public bool HasPlayerLost() =>
            circle == null;
    }
}