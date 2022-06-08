using SFML.Graphics;
using System;

namespace AgarioSFML
{
    public class GamePlayer
    {
        public PredatorObject Predator;
        private Texture texture;

        public GamePlayer()
        {
            texture = new Texture("smile.png");
        }

        public void SetTexture()
        {
            Predator.Texture = texture;
        }

        public void ChangePlayer(PredatorObject newPlayer)
        {
            Predator.Texture = null;
            Predator = newPlayer;
            SetTexture();
        }

        public bool HasPlayerLost() =>
            Predator == null || Predator.FoodEaten >= Game.FoodAmount + Game.BotsAmount;
    }
}