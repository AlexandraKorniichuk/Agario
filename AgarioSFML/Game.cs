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
        private PredatorObject PlayerPredator;
        private List<Drawable> DrawableObjects;
        private List<EatableObject> EatableObjects;
        private List<PredatorObject> Predators;
        private Text TextInGame;

        private RenderWindow Window;
        public const uint Width = 1000, Heigh = 500;
        public const int BotsAmount = 10, FoodAmount = 50;

        public Game(RenderWindow window)
        {
            Window = window;
            Window.KeyPressed += InputKey;

            Player = new GamePlayer();
            DrawableObjects = new List<Drawable>();
            EatableObjects = new List<EatableObject>();
            Predators = new List<PredatorObject>();
        }

        public void StartNewGame()
        {
            EatableObjects = Instantiation.CreateObjectsList<EatableObject>(this, FoodAmount, Radius.Food);
            Predators = Instantiation.CreateObjectsList<PredatorObject>(this, BotsAmount, Radius.Bot);
            PlayerPredator = Instantiation.CreateCircleObject<PredatorObject>(this, Radius.Player,
                new Vector2f(Width / 2, Heigh / 2));

            Player.Predator = PlayerPredator;
            TextInGame = Instantiation.CreateText(20, DefineFoodAndBotText(), Color.White, 30, this);
            
            Player.SetTexture();
            Mouse.SetPosition((Vector2i)PlayerPredator.Position, Window);

            GameLoop();
            DrawableObjects.Clear();
            Window.KeyPressed -= InputKey;
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

        private void InputKey(object sender, KeyEventArgs KEA)
        {
            Key key = Converting.GetKey(KEA);

            if (key == Key.Shoot)
                Shoot(PlayerPredator);
            if (key == Key.ChangePlayer)
                ChangePlayer();
        }

        private void Shoot(PredatorObject predator)
        {
            Bullet bullet = Instantiation.CreateCircleObject<Bullet>(this, Radius.Bullet, predator.Position);
            bullet.Init(predator);
            predator.DecreaseRadius();
        }

        private void ChangePlayer()
        {
            PredatorObject NearestBot = PlayerPredator.FindNearestPredator(Predators);
            PlayerPredator = NearestBot;
            Player.ChangePlayer(NearestBot);
        }

        private void UpdatePredators(Vector2f mousePosition)
        {
            foreach (PredatorObject predator in Predators)
            {
                Vector2f? endPosition = null;
                if (predator == PlayerPredator)
                    endPosition = mousePosition;
                predator.UpdateObject(endPosition);
            }
        }

        private void CheckForEating()
        {
            for (int i = 0; i < Predators.Count; i++)
                TryEat(eater: Predators[i]);
        }

        private void TryEat(PredatorObject eater)
        {
            List<EatableObject> Deletable = new List<EatableObject>();
            foreach (EatableObject eatable in EatableObjects)
            {
                if (eater.IsObjectInside(eatable) && eater != eatable)
                {
                    switch (eatable)
                    {
                        case Bullet bullet:
                            if (bullet.Shooter == eater) break;
                            eater.EatBullet();
                            Deletable.Add(bullet);
                            break;

                        case PredatorObject predator:
                            eater.Eat(predator.Radius);
                            Deletable.Add(predator);
                            break;

                        default:
                            eater.Eat(eatable.Radius);
                            eatable.SetRandomPosition();
                            break;
                    }
                }
            }

            foreach (EatableObject deletable in Deletable)
                RemoveFromLists(deletable);
        }

        public void AddToLists<T>(T obj)
        {
            if (obj is Drawable drawable)
                DrawableObjects.Add(drawable);
            if (obj is EatableObject eatable)
                EatableObjects.Add(eatable);
            if (obj is PredatorObject predator)
                Predators.Add(predator);
        }

        private void RemoveFromLists<T>(T obj)
        {
            if (obj is Drawable drawable)
                DrawableObjects.Remove(drawable);
            if (obj is EatableObject eatable)
                EatableObjects.Remove(eatable);
            if (obj is PredatorObject predator)
                Predators.Remove(predator);
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
            HasPlayerWon() || HasPlayerLost();

        private bool HasPlayerLost() =>
            !Predators.Contains(PlayerPredator) || 
            PlayerPredator.FoodEaten >= FoodAmount + BotsAmount;

        private bool HasPlayerWon() =>
            CountPredators() == 1 && Predators.Contains(PlayerPredator);

        private int CountPredators()
        {
            int bulletCount = 0;
            foreach (PredatorObject predator in Predators)
            {
                if (predator is Bullet)
                    bulletCount++;
            }
            return Predators.Count - bulletCount;
        }

        public void DrawResults()
        {
            CreateResultText();
            while (!InputController.IsButtonPressed(Keyboard.Key.Enter))
            {
                Draw();
                Wait();
                Window.Display();
                Window.Clear();
            }
        }

        private void CreateResultText()
        {
            (string text, Color color) resultText = DefineResultText();
            Instantiation.CreateText(40, resultText.text, resultText.color, Heigh / 2 - 40, this);
            string foodEatenText = DefineFoodAndBotText();
            Color foodEatenColor = PlayerPredator.FillColor == null ? Color.White : PlayerPredator.FillColor;
            Instantiation.CreateText(40, foodEatenText, foodEatenColor, Heigh / 2 + 40, this);
        }

        private (string, Color) DefineResultText()
        {
            if (HasPlayerWon())
                return ("Congratulations, you won", Color.Green);
            return ("Unfortunately, you lost", Color.Red);
        }

        private string DefineFoodAndBotText() =>
            $"Food eaten: {PlayerPredator.FoodEaten}, Bots left: {Predators.Count - 1}";
    }
}