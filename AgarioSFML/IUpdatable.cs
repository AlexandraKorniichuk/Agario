using SFML.System;

namespace AgarioSFML
{
    public interface IUpdatable
    {
        void Update(Vector2f? endPosition);
    }
}