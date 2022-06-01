using SFML.System;
using SFML.Graphics;
using SFML.Window;
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
        private Text TextInGame;

        private RenderWindow Window;
        public const uint Width = 1000, Heigh = 500;
        public const int BotsAmount = 10, FoodAmount = 50;

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
            TextInGame = Instantiation.CreateText(20, DefineFoodAndBotText(), Color.White, 30);
            DrawableObjects.Add(TextInGame);

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
                SetTextInGameString();
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
            Player.EatPlayer(eater);
        }

        private void EatFood(Circle eater)
        {
            foreach (Circle eatable in FoodObjects)
            {
                if (eater.IsObjectInside(eatable))
                {
                    eater.IncreaseRadius(eatable.Radius);
                    eatable.SetRandomPosition();
                    eater.IncreaseFoodEaten(); 
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

        private void SetTextInGameString() =>
            TextInGame.DisplayedString = DefineFoodAndBotText();

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
            string resultText = DefineResultText();
            Text ResultText = Instantiation.CreateText(40, resultText, Player.circle.FillColor, Heigh / 2 - 40);
            string foodEatenText = DefineFoodAndBotText();
            Text FoodEatenText = Instantiation.CreateText(40, foodEatenText, Color.White, Heigh / 2 + 40);

            while (!InputController.IsEnterPressed())
            {
                Window.Draw(ResultText);
                Window.Draw(FoodEatenText);
                Window.Display();
                Window.Clear();
            }
        }

        private string DefineResultText()
        {
            if (HasPlayerWon())
                return "Congratulations, you won";
            return "Unfortunately, you lost";
        }

        private string DefineFoodAndBotText() =>
            $"Food eaten: {Player.circle.FoodEaten}, Bots left: {Bots.Count}";
    }
}