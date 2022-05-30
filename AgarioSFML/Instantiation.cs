using SFML.System;
using SFML.Graphics;
using System.Collections.Generic;
using System;

namespace AgarioSFML
{
    public class Instantiation
    {
        public static Drawable CreateCircleObject(Game game, Radius radius, Vector2f position = new Vector2f(), bool CanEat = false)
        {
            Circle circle = new Circle();
            circle.CreateCircle((int)radius);
            circle.SetPosition(position);
            circle.SetSpeed();
            game.ObjectsToDraw.Add(circle);
            if (CanEat)
                game.ObjectsCanEat.Add(circle);

            return circle;
        }

        public static List<T> CreateObjectsList<T>(Game game, int itemsAmount, Radius radius, bool CanEat = false)
        {
            List<T> objects = new List<T>(itemsAmount);

            for (int i = 0; i < itemsAmount; i++)
                objects.Add((T)CreateCircleObject(game, radius, CanEat: CanEat));

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