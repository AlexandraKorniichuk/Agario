using SFML.Graphics;

namespace AgarioSFML
{
    public class GamePlayer
    {
        public PredatorObject Predator;

        public void MakePlayerDifferent()
        {
            Predator.Texture = new Texture("smile.png"); 
        }

        public bool HasPlayerLost() =>
            Predator == null || Predator.FoodEaten >= Game.FoodAmount + Game.BotsAmount;
    }
}