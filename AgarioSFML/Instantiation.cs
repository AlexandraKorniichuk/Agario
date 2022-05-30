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

        public static Text CreateText(uint size, string displyedText)
        {
            Text text = new Text
            {
                DisplayedString = displyedText,
                CharacterSize = size,
                Position = new Vector2f(Game.Width / 2 - 5 * size, Game.Heigh / 2),
                Font = new Font("BasicText.ttf"),
                FillColor = Color.Yellow
            };
            return text;
        }
    }
}