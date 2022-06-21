using SFML.System;
using System.Collections.Generic;

namespace AgarioSFML
{
    public class PredatorObject : EatableObject, IUpdatable
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

        public void EatBullet() =>
            DecreaseRadius(0.9f);

        private void IncreaseFoodEaten() =>
            FoodEaten++;

        public void Update(Vector2f? endPosition)
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