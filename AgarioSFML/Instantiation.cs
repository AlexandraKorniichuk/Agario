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
            AddToLists(circle, game);

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
            AddToLists(text, game);
            return text;
        }

        public static void AddToLists<T>(T obj, Game game)
        {
            if (obj is Drawable)
                game.DrawableObjects.Add((Drawable)obj);
            if (obj is EatableObject eatable)
                game.EatableObjects.Add(eatable);
            if (obj is PredatorObject predator)
                game.Predators.Add(predator);
        }

        public static void RemoveFromLists<T>(T obj, Game game)
        {
            if (obj is Drawable drawable)
                game.DrawableObjects.Remove(drawable);
            if (obj is EatableObject eatable)
                game.EatableObjects.Remove(eatable);
            if (obj is PredatorObject predator)
                game.Predators.Remove(predator);
        }
    }
}