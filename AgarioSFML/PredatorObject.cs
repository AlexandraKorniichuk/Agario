using SFML.System;
using System.Collections.Generic;

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

        public PredatorObject FindNearestPredator(in List<PredatorObject> Predators)
        {
            PredatorObject Predator = null;
            float squaredMinDistance = float.MaxValue;
            foreach (PredatorObject predator in Predators)
            {
                float squaredDistance = Calculations.CalculateSquaredDistance(predator.Position, Position);
                if (predator != this && squaredMinDistance > squaredDistance)
                {
                    squaredMinDistance = squaredDistance;
                    Predator = predator;
                }

            }
            return Predator;
        }
    }
}