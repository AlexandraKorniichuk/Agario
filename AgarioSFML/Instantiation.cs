using SFML.System;
using SFML.Graphics;
using System.Collections.Generic;

namespace AgarioSFML
{
    public class Instantiation
    {
        public static T CreateCircleObject<T>(Game game, Radius radius, Vector2f? position = null) where T : Circle, new()
        {
            T circle = new T();
            circle.CreateCircle((int)radius);
            circle.SetPosition(position);
            circle.SetSpeedAndAnchor();
            game.AddToLists(circle);

            return circle;
        }

        public static List<T> CreateObjectsList<T>(Game game, int itemsAmount, Radius radius) where T : Circle, new()
        {
            List<T> objects = new List<T>();

            for (int i = 0; i < itemsAmount; i++)
                objects.Add(CreateCircleObject<T>(game, radius));

            return objects;
        }

        public static Text CreateText(uint size, string displayedText, Color color, float positionHeigh, Game game)
        {
            Text text = new Text
            {
                DisplayedString = displayedText,
                CharacterSize = size,
                Origin = new Vector2f((displayedText.Length / 2 + displayedText.Length % 2) * size / 2, size / 2),
                Position = new Vector2f(Game.Width / 2, positionHeigh),
                Font = new Font("BasicText.ttf"),
                FillColor = color
            };
            game.AddToLists(text);
            return text;
        }
    }

    public interface IUpdatable
    {
        void Update(Vector2f? endPosition);
    }
}