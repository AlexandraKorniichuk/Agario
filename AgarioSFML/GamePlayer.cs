namespace AgarioSFML
{
    public class GamePlayer
    {
        public Circle circle;
        public PlayerState State;
        public GamePlayer()
        {
            State = PlayerState.Playing;
        }
    }

    public enum PlayerState
    {
        Win,
        Lose,
        Playing
    }
}