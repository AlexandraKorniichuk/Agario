using SFML.System;

namespace AgarioSFML
{
    public class Bullet : EatableObject, IUpdatable
    {
        public PredatorObject Shooter;

        public void Init(PredatorObject predator)
        {
            Shooter = predator;
            Direction = predator.Direction;
        }

        public void Update(Vector2f? endPosition) =>
            MoveCircle();
    }
}