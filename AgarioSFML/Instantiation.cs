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

        public Text CreateText()
        {
            Text text = new Text();

            return text;
        }
    }
}