using SFML.Graphics;
using SFML.Window;
using System;

namespace AgarioSFML
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            Settings.LoadSettings(game);
            game.PostLoad();
            game.StartNewGame();
            game.DrawResults();
        }
    }
}
