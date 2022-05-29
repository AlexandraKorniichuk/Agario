using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace AgarioSFML
{
    public class InputController
    {
        public static Vector2f GetMousePosition(RenderWindow Window) =>
            (Vector2f)Mouse.GetPosition(Window);

        public static bool IsEnterPressed() =>
            Keyboard.IsKeyPressed(Keyboard.Key.Enter);
    }
}