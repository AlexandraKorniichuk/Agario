﻿using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;

namespace AgarioSFML
{

    public enum Radius
    {
        Player = 30,
        Bot = 20,
        Food = 5,
        Min = 15
    }

    public class Circle : CircleShape
    {
        public Vector2f Direction;
        public float DistancePerMove;
        private const int MovesToChangeDirection = 20;
        private int Moves;

        private static Random random = new Random();

        public Circle()
        {
            Moves = 0;
        }

        private List<Color> colors = new List<Color>()
        {
            Color.Cyan, Color.Green, Color.Yellow, Color.Blue,
            Color.White, Color.Magenta, Color.Red
        };

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
            }
            Position = position;
        }

        public void SetSpeed()
        {
            DistancePerMove = 1 / Radius * 100;
        }

        public void DecreaseRadius()
        {
            if (Radius <= (int)AgarioSFML.Radius.Min) return;
            Radius *= 0.999f;
        }

        public void ChangeDirection(Vector2f endPosition)
        {
            Direction = ExtentionMethods.ClaculateDirection(Position, endPosition);
        }

        public void ChangeRandomDirection()
        {
            if (Moves != MovesToChangeDirection) return;
            Moves = 0;
            
            float X = random.Next(-(int)DistancePerMove, (int)DistancePerMove);
            float Y = ExtentionMethods.CalculateVectorY(X, DistancePerMove);

            double signPercentage = random.NextDouble();
            if (signPercentage > 0.5)
                Y = -Y;

            Direction = new Vector2f(X, Y);
        }

        public void MoveCircle()
        {
            float squaredDirectionLength = ExtentionMethods.ClaculateSquaredLength(Direction);
            if (squaredDirectionLength <= DistancePerMove * DistancePerMove)
            {
                Move(Direction);
                return;
            }
            Vector2f smallerDirection = ExtentionMethods.CalculateCollinearVector(Direction, DistancePerMove);
            smallerDirection = ExtentionMethods.MakeVectorCoDirectional(smallerDirection, Direction);
            Move(smallerDirection);
        }

        public void Move(Vector2f direction)
        {
            Position += direction;
            CheckCircleInside();
            Moves++;
        }

        private void CheckCircleInside()
        {
            Vector2f vector = new Vector2f(Position.X, Position.Y);

            vector.X = ExtentionMethods.MakeSmallerIfBigger(vector.X, Game.Width - Radius);
            vector.X = ExtentionMethods.MakeBiggerIfSmaller(vector.X, Radius);

            vector.Y = ExtentionMethods.MakeSmallerIfBigger(vector.Y, Game.Heigh - Radius);
            vector.Y = ExtentionMethods.MakeBiggerIfSmaller(vector.Y, Radius);

            Position = vector;
        }
    }
}