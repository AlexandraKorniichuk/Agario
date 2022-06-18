using SFML.Window;
using System;

namespace AgarioSFML
{
    public class Converting
    {
        public static Key GetKey(KeyEventArgs key)
        {
            switch (key.Code)
            {
                case Keyboard.Key.F: return Key.Shoot;
                case Keyboard.Key.R: return Key.ChangePlayer;
                default: return Key.Other;
            }
        }
    }
}