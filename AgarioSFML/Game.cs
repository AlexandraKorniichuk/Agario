using SFML.System;
using SFML.Graphics;
using SFML.Window;
using System.Threading;
using System.Collections.Generic;
using System;

namespace AgarioSFML
{
    public class Game
    {
        private GamePlayer Player;
        private PredatorObject PlayerPredator;
        public List<Drawable> DrawableObjects;
        private List<EatableObject> FoodObjects;
        private List<PredatorObject> Predators;
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
            Predators = Instantiation.CreateObjectsList<PredatorObject>(this, BotsAmount, Radius.Bot);
            FoodObjects = Instantiation.CreateObjectsList<EatableObject>(this, FoodAmount, Radius.Food);
            Player.Predator = Instantiation.CreateCircleObject<PredatorObject>(this, Radius.Player, new Vector2f (Width / 2, Heigh / 2));
            PlayerPredator = Player.Predator;
            Predators.Add(PlayerPredator);
            TextInGame = Instantiation.CreateText(20, DefineFoodAndBotText(), Color.White, 30);
            DrawableObjects.Add(TextInGame);

            Mouse.SetPosition((Vector2i)PlayerPredator.Position, Window);

            GameLoop();
        }

        private void GameLoop()
        {
            do
            {
                Window.DispatchEvents();

                Vector2f mousePosition = InputController.GetMousePosition(Window);
                UpdatePredators(mousePosition);
                CheckForEating();
                SetTextInGameString();
                Draw();
                Wait();

                Window.Display();
                Window.Clear();
            } while (!IsEndGame());
        }

        private void UpdatePredators(Vector2f mousePosition)
        {
            foreach (PredatorObject predator in Predators)
            {
                Vector2f? endPosition = null;
                if (predator == PlayerPredator)
                    endPosition = mousePosition;
                predator.UpdatePredator(endPosition);
            }
        }

        private void CheckForEating()
        {
            for (int i = 0; i < Predators.Count; i++)
            {
                TryEat(eater: Predators[i]);
            }
        }

        public void TryEat(PredatorObject eater)
        {
            TryEatFood(eater);
            TryEatPredator(eater);
        }

        private void TryEatFood(PredatorObject eater)
        {
            foreach (EatableObject eatable in FoodObjects)
            {
                if (eater.IsObjectInside(eatable))
                {
                    eater.Eat(eatable.Radius, foodEaten: 1);
                    eatable.SetRandomPosition();
                }
            }
        }

        private void TryEatPredator(PredatorObject eater)
        {
            for (int i = 0; i < Predators.Count; i++)
            {
                if (eater != Predators[i] && eater.IsObjectInside(Predators[i]))
                {
                    eater.Eat(Predators[i].Radius);
                    MakePredatorEaten(Predators[i]);
                }
            }
        }

        private void MakePredatorEaten(PredatorObject eaten)
        {
            DrawableObjects.Remove(eaten);
            Predators.Remove(eaten);
            eaten = null;
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
            Predators.Count == 1;

        public void DrawResults()
        {
            string resultText = DefineResultText();
            Text ResultText = Instantiation.CreateText(40, resultText, PlayerPredator.FillColor, Heigh / 2 - 40);
            string foodEatenText = DefineFoodAndBotText();
            Text FoodEatenText = Instantiation.CreateText(40, foodEatenText, Color.White, Heigh / 2 + 40);

            while (!InputController.IsButtonPressed(Keyboard.Key.Enter))
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
            $"Food eaten: {PlayerPredator.FoodEaten}, Bots left: {Predators.Count}";
    }
}