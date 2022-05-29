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
        private List<Color> colors = new List<Color>()
            {
                Color.Cyan, Color.Green, Color.Yellow, Color.Blue,
                Color.White, Color.Magenta, Color.Red
            };


        public Circle()
        {
            random = new Random();
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
        public void SetSpeed()
        {
            Speed = 1 / Radius * 100;
        }

        public void ChangeDirection(Vector2f endPosition)
        {
            Direction = VectorMethods.ClaculateDirection(Position, endPosition);
        }

        public void MoveCircle()
        {
            float directionLength = VectorMethods.ClaculateSquaredLength(Direction);
            if (directionLength <= Speed)
            {
                Position += Direction;
                return;
            }
            Vector2f smallerDirection = VectorMethods.CalculateCollinearVector(Direction, Speed);
            smallerDirection = VectorMethods.MakeVectorCoDirectional(smallerDirection, Direction);
            Position += smallerDirection;
            CheckCircleInside();
        }

        private void CheckCircleInside()
        {
            Vector2f vector = new Vector2f(Position.X, Position.Y);

            vector.X = VectorMethods.MakeSmallerIfBigger(vector.X, Game.Width - Radius);
            vector.X = VectorMethods.MakeBiggerIfSmaller(vector.X, Radius);

            vector.Y = VectorMethods.MakeSmallerIfBigger(vector.Y, Game.Heigh - Radius);
            vector.Y = VectorMethods.MakeBiggerIfSmaller(vector.Y, Radius);

            Position = vector;
        }
    }
}