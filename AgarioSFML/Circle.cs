using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Threading;

namespace AgarioSFML
{

    public enum Radius
    {
        Player = 30,
        Bot = 20,
        Food = 5
    }

    public class Circle : CircleShape
    {
        public Vector2f Direction;
        public float Speed;

        private Random random;
        private List<Color> colors;

        public Circle()
        {
            random = new Random();
            Speed = 0;
            SetColors();
        }

        private void SetColors()
        {
            colors = new List<Color>()
            {
                Color.Cyan, Color.Green, Color.Yellow, Color.Blue,
                Color.White, Color.Magenta, Color.Red
            };
        }

        public void CreateCircle(int radius)
        {
            FillColor = colors[random.Next(0, colors.Count)];
            Radius = radius;
            Origin = new Vector2f(Radius, Radius);
        }

        public void SetPosition(Vector2f position)
        {
            if (position.X == 0 && position.Y == 0)
            {
                position.X = random.Next((int)Radius, (int)(Game.Width - Radius));
                position.Y = random.Next((int)Radius, (int)(Game.Heigh - Radius));
                Thread.Sleep(1);
            }
            Position = position;
        }
    }
}