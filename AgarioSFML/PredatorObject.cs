using SFML.System;

namespace AgarioSFML
{
    public class PredatorObject : EatableObject
    {
        public int FoodEaten { get; private set; }

        public PredatorObject()
        {
            FoodEaten = 0;
        }

        public void Eat(float radius)
        {
            IncreaseRadius(radius);
            IncreaseFoodEaten();
        }

        private void IncreaseFoodEaten() =>
            FoodEaten++;

        public void UpdatePredator(Vector2f? endPosition)
        {
            ChangeDirection(endPosition);
            MoveCircle();
            DecreaseRadius();
        }
    }
}