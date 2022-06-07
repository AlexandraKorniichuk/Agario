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

        public void Eat(float radius, int foodEaten = 0)
        {
            IncreaseRadius(radius);
            IncreaseFoodEaten(foodEaten);
        }

        private void IncreaseFoodEaten(int foodEaten) =>
            FoodEaten += foodEaten;

        public void UpdatePredator(Vector2f? endPosition)
        {
            ChangeDirection(endPosition);
            MoveCircle();
            DecreaseRadius();
        }
    }
}