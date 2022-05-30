namespace AgarioSFML
{
    public class GamePlayer
    {
        public Circle circle;

        public bool HasPlayerLost() =>
            circle == null;

        public void EatPlayer(Circle eater)
        {
            if (eater != circle && eater.IsObjectInside(circle))
            {
                circle = null;
            }
        }
    }
}