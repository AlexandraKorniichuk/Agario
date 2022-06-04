using SFML.System;
using SFML.Graphics;
using System.Collections.Generic;
using System;

namespace AgarioSFML
{
    public class Instantiation
    {
        public static Drawable CreateCircleObject(Game game, Radius radius, Vector2f position = new Vector2f())
        {
            Circle circle = new Circle();
            circle.CreateCircle((int)radius);
            circle.SetPosition(position);
            circle.SetSpeedAndAnchor();
            game.DrawableObjects.Add(circle);

            return circle;
        }

        public static List<T> CreateObjectsList<T>(Game game, int itemsAmount, Radius radius)
        {
            List<T> objects = new List<T>(itemsAmount);

            for (int i = 0; i < itemsAmount; i++)
                objects.Add((T)CreateCircleObject(game, radius));

            return objects;
        }

        public static Text CreateText(uint size, string displayedText, Color color, float positionHeigh)
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
            return text;
        }
    }
}