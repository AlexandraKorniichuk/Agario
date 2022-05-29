using SFML.System;
using System;

namespace AgarioSFML
{
    public class VectorMethods
    {
        public static Vector2f ClaculateDirection(Vector2f start, Vector2f end) =>
            end - start;

        public static float ClaculateSquaredLength(Vector2f vector) =>
            vector.X * vector.X + vector.Y * vector.Y;

        public static Vector2f CalculateCollinearVector(Vector2f vector, float length)
        {
            Vector2f v = new Vector2f(length, 0);

            if (vector.Y == 0)
                return v;

            float xDividedy = vector.X / vector.Y;
            v.Y = (float)Math.Sqrt((length * length / (xDividedy * xDividedy + 1)));
            v.X = xDividedy * v.Y;
            return v;
        }

        public static Vector2f MakeVectorCoDirectional(Vector2f vector1, Vector2f vector2)
        {
            vector1.X = CheckForDifferentSigns(vector1.X, vector2.X);
            vector1.Y = CheckForDifferentSigns(vector1.Y, vector2.Y);
            return vector1;
        }

        private static float CheckForDifferentSigns(float num1, float num2) =>
            num1 * num2 < 0 ? -1 * num1 : num1;

        public static float MakeSmallerIfBigger(float num1, float num2) =>
            num1 > num2 ? num2 : num1;

        public static float MakeBiggerIfSmaller(float num1, float num2) =>
            num1 < num2 ? num2 : num1;
    }
}