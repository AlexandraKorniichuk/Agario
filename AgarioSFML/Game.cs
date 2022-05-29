using SFML.System;
using SFML.Graphics;
using SFML.Window;
using System;
using System.Threading;
using System.Collections.Generic;

namespace AgarioSFML
{
    internal class Game
    {
        private GamePlayer Player;
        public List<Drawable> ObjectsToDraw;
        private List<Circle> FoodObjects;
        private List<Circle> Bots;

        private RenderWindow Window;
        public const uint Width = 1000, Heigh = 500;
        private const int BotsAmount = 4, FoodAmount = 50;

        //private Vector2f LastPlayerPosition;
        //private Vector2f PlayerDirection;

        //private Action OnFoodEaten;

        public Game(RenderWindow window)
        {
            Window = window;
            Player = new GamePlayer();
            ObjectsToDraw = new List<Drawable>();
        }

        public void StartNewGame()
        {
            FoodObjects = Instantiation.CreateObjectsList<Circle>(ObjectsToDraw, FoodAmount, Radius.Food);
            Bots = Instantiation.CreateObjectsList<Circle>(ObjectsToDraw, BotsAmount, Radius.Bot);
            Player.circle = (Circle)Instantiation.CreateCircleObject(ObjectsToDraw, Radius.Player, new Vector2f (Width / 2, Heigh / 2));
            Mouse.SetPosition((Vector2i)Player.circle.Position, Window);

            GameLoop();
        }

        private void GameLoop()
        {
            do
            {
                Window.DispatchEvents();

                Vector2f MousePosition = InputController.GetMousePosition(Window);
                Player.circle.ChangeDirection(MousePosition);
                Move();
                Draw();
                Wait();

                Window.Display();
                Window.Clear();
            } while (!IsEndGame());
        }

        private void Move()
        {
            Player.circle.MoveCircle();
        }

        private void Draw()
        {
            foreach (Drawable gameObject in ObjectsToDraw)
                Window.Draw(gameObject);
        }

        private void Wait()
        {
            const int timeToWait = 1;
            Thread.Sleep(timeToWait);
        }

        private bool IsEndGame() =>
            Player.State != PlayerState.Playing;

        public void DrawResults()
        {
            Text ResultText = Instantiation.CreateText(40, "");

            while (!InputController.IsEnterPressed())
            {
                Window.Draw(ResultText);
                Window.Display();
                Window.Clear();
            }
        }
    }
}