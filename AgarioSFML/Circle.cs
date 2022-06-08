using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;

namespace AgarioSFML
{

    public enum Radius
    {
        Player = 30,
        Bot = 20,
        Food = 7,
        Min = 20
    }

    public class Circle : CircleShape
    {
        private Vector2f Direction;
        private float DistancePerMove;
        private const int MovesToChangeDirection = 20;
        private int Moves;

        private static Random random = new Random();

        private List<Color> colors = new List<Color>()
        {
            Color.Cyan, Color.Green, Color.Yellow, Color.Blue,
            Color.White, Color.Magenta, Color.Red
        };

        public Circle()
        {
            Moves = 0;
        }

        public void CreateCircle(int radius)
        {
            FillColor = colors[random.Next(0, colors.Count)];
            Radius = radius;
            Origin = new Vector2f(Radius, Radius);
        }

        public void SetPosition(Vector2f? position)
        {
            if (position == null)
            {
                SetRandomPosition();
                return;
            }
            Position = (Vector2f)position;
        }

        public void SetRandomPosition()
        {
            Vector2f position = new Vector2f();
            position.X = random.Next((int)Radius, (int)(Game.Width - Radius));
            position.Y = random.Next((int)Radius, (int)(Game.Heigh - Radius));
            Position = position;
        }

        public void SetSpeedAndAnchor()
        {
            DistancePerMove = 1 / Radius * 100;
            Origin = new Vector2f(Radius, Radius);
        }

        public void DecreaseRadius()
        {
            if (Radius <= (int)AgarioSFML.Radius.Min) return;
            Radius *= 0.999f;
            SetSpeedAndAnchor();
        }

        public void IncreaseRadius(float radius)
        {
            Radius = Calculations.CalculateNewRadius(Radius, radius);
            SetSpeedAndAnchor();
        }

        public void ChangeDirection(Vector2f? endPosition = null)
        {
            if (endPosition == null)
            {
                ChangeRandomDirection();
                return;
            }

            Direction = Calculations.ClaculateDirection(Position, (Vector2f)endPosition);
        }

        public void ChangeRandomDirection()
        {
            if (Moves < MovesToChangeDirection) return;
            Moves = 0;

            float X = random.Next(-(int)DistancePerMove, (int)DistancePerMove);
            float Y = Calculations.CalculateVectorY(X, DistancePerMove);

            double signPercentage = random.NextDouble();
            if (signPercentage > 0.5)
                Y = -Y;

            Direction = new Vector2f(X, Y);
        }

        public void MoveCircle()
        {
            float squaredDirectionLength = Calculations.ClaculateSquaredLength(Direction);
            Vector2f newDirection = Direction;
            if (squaredDirectionLength > DistancePerMove * DistancePerMove)
            {
                Vector2f smallerDirection = Calculations.CalculateCollinearVector(Direction, DistancePerMove);
                smallerDirection = Calculations.MakeVectorCoDirectional(smallerDirection, Direction);
                newDirection = smallerDirection;
            }
            Move(newDirection);
        }

        public void Move(Vector2f direction)
        {
            Position += direction;
            ChangePositionIfOutside();
            Moves++;
        }

        private void ChangePositionIfOutside()
        {
            Vector2f vector = Position;

            vector.X = Calculations.Min(vector.X, Game.Width - Radius);
            vector.X = Calculations.Max(vector.X, Radius);

            vector.Y = Calculations.Min(vector.Y, Game.Heigh - Radius);
            vector.Y = Calculations.Max(vector.Y, Radius);

            Position = vector;
        }

        public bool IsObjectInside(Circle circle)
        {
            if (Radius < circle.Radius) return false;
            float SquaredDistance = Calculations.CalculateSquaredDistance(Position, circle.Position);
            return SquaredDistance <= (Radius - circle.Radius) * (Radius - circle.Radius);
        }
    }
}