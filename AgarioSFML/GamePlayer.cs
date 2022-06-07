namespace AgarioSFML
{
    public class GamePlayer
    {
        public PredatorObject Predator;

        public bool HasPlayerLost() =>
            Predator == null || Predator.FoodEaten == Game.FoodAmount;
    }
}