using SFML.System;

namespace AgarioSFML
{
    public class Bullet : PredatorObject
    {
        public PredatorObject Shooter;

        public void Init(PredatorObject predator)
        {
            Shooter = predator;
            Direction = predator.Direction;
        }

        public override void UpdateObject(Vector2f? endPosition) =>
            MoveCircle();
    }
}