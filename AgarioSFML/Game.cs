using SFML.System;
using SFML.Graphics;
using SFML.Window;
using System;
using System.Threading;
using System.Collections.Generic;

namespace AgarioSFML
{
    public class Game
    {
        private GamePlayer Player;
        public List<Drawable> DrawableObjects;
        private List<Circle> FoodObjects;
        private List<Circle> Bots;

        private RenderWindow Window;
        public const uint Width = 1000, Heigh = 500;
        private const int BotsAmount = 10, FoodAmount = 50;

        public Game(RenderWindow window)
        {
            Window = window;
            Player = new GamePlayer();
            DrawableObjects = new List<Drawable>();
        }

        public void StartNewGame()
        {
            Bots = Instantiation.CreateObjectsList<Circle>(this, BotsAmount, Radius.Bot);
            FoodObjects = Instantiation.CreateObjectsList<Circle>(this, FoodAmount, Radius.Food);
            Player.circle = (Circle)Instantiation.CreateCircleObject(this, Radius.Player, new Vector2f (Width / 2, Heigh / 2));
            Mouse.SetPosition((Vector2i)Player.circle.Position, Window);

            GameLoop();
        }

        private void GameLoop()
        {
            do
            {
                Window.DispatchEvents();

                Vector2f mousePosition = InputController.GetMousePosition(Window);
                ChangeDirections(mousePosition);
                Move();
                DecreaseSize();
                CheckForEating();
                Draw();
                Wait();

                Window.Display();
                Window.Clear();
            } while (!IsEndGame());
        }

        private void ChangeDirections(Vector2f mousePosition)
        {
            Player.circle.ChangeDirection(mousePosition);
            foreach (Circle bot in Bots)
                bot.ChangeRandomDirection();
        }

        private void Move()
        {
            Player.circle.MoveCircle();
            foreach (Circle bot in Bots)
                bot.MoveCircle();
        }

        private void DecreaseSize()
        {
            Player.circle.DecreaseRadius();
            foreach (Circle bot in Bots)
                bot.DecreaseRadius();
        }

        private void CheckForEating()
        {
            Eat(Player.circle);
            for (int i = 0; i < Bots.Count; i++)
            {
                Eat(Bots[i]);
            }
        }

        private void Eat(Circle eater)
        {
            EatFood(eater);
            EatBot(eater);
            EatPlayer(eater);
        }

        private void EatFood(Circle eater)
        {
            foreach (Circle eatable in FoodObjects)
            {
                if (eater.IsObjectInside(eatable))
                {
                    eater.IncreaseRadius(eatable.Radius);
                    eatable.SetRandomPosition();
                }
            }
        }

        private void EatBot(Circle eater)
        {
            for (int i = 0; i < Bots.Count; i++)
            {
                if (eater != Bots[i] && eater.IsObjectInside(Bots[i]))
                {
                    eater.IncreaseRadius(Bots[i].Radius);
                    Bots[i] = null;
                    DrawableObjects.RemoveAt(i);
                    Bots.Remove(Bots[i]);
                }
            }
        }

        private void EatPlayer(Circle eater)
        {
            if (eater != Player.circle && eater.IsObjectInside(Player.circle))
            {
                Player.circle = null;
                DrawableObjects.Remove(Player.circle);
            }
        }

        private void Draw()
        {
            foreach (Drawable gameObject in DrawableObjects)
                Window.Draw(gameObject);
        }

        private void Wait()
        {
            const int timeToWait = 1;
            Thread.Sleep(timeToWait);
        }

        private bool IsEndGame() =>
            HasPlayerWon() || Player.HasPlayerLost();

        private bool HasPlayerWon() =>
            Bots.Count == 0;

        public void DrawResults()
        {
            string text = DefineText();
            Text ResultText = Instantiation.CreateText(40, text);

            while (!InputController.IsEnterPressed())
            {
                Window.Draw(ResultText);
                Window.Display();
                Window.Clear();
            }
        }

        private string DefineText()
        {
            if (HasPlayerWon())
                return "Congratulations, you won";
            return "Unfortunately, you lost";
        }
    }
}