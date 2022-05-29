using SFML.System;
using SFML.Graphics;
using System.Collections.Generic;
using System;

namespace AgarioSFML
{
    public class Instantiation
    {
        public static Drawable CreateCircleObject(List<Drawable> ObjectsToDraw, Radius radius, Vector2f position = new Vector2f())
        {
            Circle circle = new Circle();
            circle.CreateCircle((int)radius);
            circle.SetPosition(position);
            circle.SetSpeed();
            ObjectsToDraw.Add(circle);

            return circle;
        }

        public static List<T> CreateObjectsList<T>(List<Drawable> ObjectsToDraw, int itemsAmount, Radius radius)
        {
            List<T> objects = new List<T>(itemsAmount);

            for (int i = 0; i < itemsAmount; i++)
                objects.Add((T)CreateCircleObject(ObjectsToDraw, radius));

            return objects;
        }

        public static Text CreateText(uint size, string mass)
        {
            Text text = new Text
            {
                DisplayedString = $"Highest mass {mass}",
                CharacterSize = size,
                Position = new Vector2f(Game.Width / 2 - size, Game.Heigh / 2),
                Font = new Font("BasicText.ttf"),
                FillColor = Color.Yellow
            };
            return text;
        }
    }
}